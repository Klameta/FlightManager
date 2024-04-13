using ASPFlightManager.Areas.Identity.Pages.Account;
using Data;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPFlightManager.Controllers
{
    public class UsersController : Controller
    {
        private readonly FlightManagerDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UsersController> _logger;


        public UsersController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<UsersController> logger,
            FlightManagerDbContext flightManagerDbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _context = flightManagerDbContext;
        }


        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,SecondName,LastName,Email,SSN,PhoneNumber,Address,Nationality,TicketType,PasswordHash")] User user)
        {
            User newUser = new User
            {
                UserName = user.Email,
                Email = user.Email,
                LockoutEnabled = false,
                EmailConfirmed = true,
                Address = user.Address,
                FirstName = user.FirstName,
                LastName =  user.LastName,
                SSN = user.SSN
            };
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(newUser, user.PasswordHash);
                /*_context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));*/
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        //(TRQBWA DA SE OPRAVI)!!!!!!!!!!

        public async Task<IActionResult> Edit(string id,
            [Bind("Id,FirstName,LastName,SSN,Email,Address")] User user)
        {

            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var currentUser = await _context.Users.FindAsync(id);
                    currentUser.FirstName = user.FirstName;
                    currentUser.LastName = user.LastName;
                    currentUser.SSN = user.SSN;
                    currentUser.Email = user.Email;
                    currentUser.Address = user.Address;
                    _context.Update(currentUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: User/Delete/5
        public async Task<IActionResult> Delete(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
