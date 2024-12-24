using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CreditManagement.Models;
using CreditManagement.Data;
using CreditManagement.Models.Entities;

namespace CreditManagement.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private const int PageSize = 10;

        public CustomerController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        private async Task<bool> IsDuplicateCustomerInfo(string email, string idNumber, string phoneNumber, Guid? excludeCustomerId = null)
        {
            bool emailExists = await dbContext.Customers
                .AnyAsync(c => c.Email == email && (excludeCustomerId == null || c.CustomerId != excludeCustomerId));
            if (emailExists)
            {
                ModelState.AddModelError("Email", "Email already exists.");
            }

            bool idNumberExists = await dbContext.Customers
                .AnyAsync(c => c.IdentificationNumber == idNumber && (excludeCustomerId == null || c.CustomerId != excludeCustomerId));
            if (idNumberExists)
            {
                ModelState.AddModelError("IdentificationNumber", "Identification number already exists.");
            }

            bool phoneNumberExists = await dbContext.Customers
                .AnyAsync(c => c.PhoneNumber == phoneNumber && (excludeCustomerId == null || c.CustomerId != excludeCustomerId));
            if (phoneNumberExists)
            {
                ModelState.AddModelError("PhoneNumber", "Phone number already exists.");
            }

            return emailExists || idNumberExists || phoneNumberExists;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCustomerViewModel viewModel)
        {
            // Validate unique email, identification number, and phone number
            if (await IsDuplicateCustomerInfo(viewModel.Email, viewModel.IdentificationNumber, viewModel.PhoneNumber))
            {
                return View(viewModel);
            }

            // Convert CreatedAt to Vietnam time (ICT = UTC+7)
            if (viewModel.CreatedAt == default)
            {
                ModelState.AddModelError("CreatedAt", "CreatedAt must be a valid date.");
                return View(viewModel);
            }

            var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var vietnamTime = TimeZoneInfo.ConvertTimeFromUtc(viewModel.CreatedAt, vietnamTimeZone);

            var customer = new Customer
            {
                FullName = viewModel.FullName,
                IdentificationNumber = viewModel.IdentificationNumber,
                Email = viewModel.Email,
                PhoneNumber = viewModel.PhoneNumber,
                DateOfBirth = viewModel.DateOfBirth,
                Address = viewModel.Address,
                CustomerType = viewModel.CustomerType,
                CreatedAt = vietnamTime
            };

            await dbContext.Customers.AddAsync(customer);
            await dbContext.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm khách hàng thành công!";
            return RedirectToAction("List");
        }


        [HttpGet]
        public async Task<IActionResult> List(string searchString, string filterType, string filterValue, int page = 1)
        {
            var query = dbContext.Customers.AsQueryable();

            if (!string.IsNullOrEmpty(filterType) && !string.IsNullOrEmpty(filterValue))
            {
                query = filterType.ToLower() switch
                {
                    "name" => query.Where(c => c.FullName.ToLower().Contains(filterValue.ToLower())),
                    "identity" => query.Where(c => c.IdentificationNumber.Contains(filterValue.ToLower())),
                    "email" => query.Where(c => c.Email.ToLower().Contains(filterValue.ToLower())),
                    "phone" => query.Where(c => c.PhoneNumber.Contains(filterValue)),
                    "address" => query.Where(c => c.Address.Contains(filterValue)),

                    _ => query
                };
            }
            else if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(c =>
                    c.FullName.ToLower().Contains(searchString.ToLower()) ||
                    c.IdentificationNumber.Contains(searchString.ToLower()) ||
                    c.Email.ToLower().Contains(searchString.ToLower()) ||
                    c.PhoneNumber.Contains(searchString) ||
                    c.Address.Contains(searchString)
                );
            }

            int skipAmount = (page - 1) * PageSize;
            int totalCustomers = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalCustomers / PageSize);

            var customers = await query
                .OrderBy(c => c.FullName)
                .Skip(skipAmount)
                .Take(PageSize)
                .ToListAsync();

            var viewModel = new CustomerListViewModel
            {
                Customers = customers,
                CurrentPage = page,
                TotalPages = totalPages,
                TotalCustomers = totalCustomers,
                SearchString = searchString,
                FilterType = filterType,
                FilterValue = filterValue
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var customer = await dbContext.Customers.FindAsync(id);

            return View(customer); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer viewMethod)
        {
            if (await IsDuplicateCustomerInfo(viewMethod.Email, viewMethod.IdentificationNumber, viewMethod.PhoneNumber, viewMethod.CustomerId))
            {
                return View(viewMethod);
            }

            var customer = await dbContext.Customers.FindAsync(viewMethod.CustomerId);
            if (customer is not null)
            {
                customer.FullName = viewMethod.FullName;
                customer.IdentificationNumber = viewMethod.IdentificationNumber;
                customer.Email = viewMethod.Email;
                customer.PhoneNumber = viewMethod.PhoneNumber;
                customer.Address = viewMethod.Address;
                customer.DateOfBirth = viewMethod.DateOfBirth;
                customer.CustomerType = viewMethod.CustomerType;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Customer");
        }

        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var customer = await dbContext.Customers
                .Where(c => c.CustomerId == id)
                .Include(c => c.Accounts)  // Lấy thông tin tài khoản liên quan
                .Include(c => c.CreditScores)  // Lấy thông tin điểm tín dụng liên quan
                .Include(c => c.Loans)  // Lấy tất cả các khoản vay liên quan
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            var viewModel = new CustomerDetailsViewModel
            {
                Customer = customer,
                Accounts = customer.Accounts.ToList(),
                CreditScores = customer.CreditScores.ToList(),
                Loans = customer.Loans.ToList()
            };

            return View(viewModel);  // Trả về View với ViewModel
        }



        // Phương thức xóa khách hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var customer = dbContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            // Xóa khách hàng từ cơ sở dữ liệu
            dbContext.Customers.Remove(customer);
            dbContext.SaveChanges();
            TempData["SuccessMessage"] = "Xóa khách hàng thành công";

            // Điều hướng lại trang danh sách khách hàng
            return RedirectToAction("List", "Customer");
        }
    }
}
