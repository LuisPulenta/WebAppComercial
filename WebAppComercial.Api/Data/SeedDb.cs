using WebAppComercial.Api.Data;
using WebAppComercial.Api.Helpers;
using WebAppComercial.Shared.Entities;
using WebAppComercial.Shared.Enums;

namespace WebAppComercial.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckStoresAsync();
            await CheckRolesAsync();
            await CheckUserAsync("Luis", "Núñez", "luis@yopmail.com", "156 814 963", UserType.Admin);
            await CheckUserAsync("Juan", "Vendedor", "juan@yopmail.com", "111 111 111", UserType.Inventory);
        }

        //----------------------------------------------------------------------------------------------
        private async Task<User> CheckUserAsync(string firstName, string lastName, string email, string phone, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    UserType = userType,
                    Active=true
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        //----------------------------------------------------------------------------------------------
        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.Inventory.ToString());
            await _userHelper.CheckRoleAsync(UserType.Sale.ToString());
        }
        
        //----------------------------------------------------------------------------------------------
        private async Task CheckCountriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Alimentos" });
                _context.Categories.Add(new Category { Name = "Bebidas" });
                _context.Categories.Add(new Category { Name = "Herramientas" });
                _context.Categories.Add(new Category { Name = "Juguetes" });
            }
            await _context.SaveChangesAsync();
        }
        
        //----------------------------------------------------------------------------------------------
        private async Task CheckStoresAsync()
        {
            if (!_context.Stores.Any())
            {
                _context.Stores.Add(new Store { Name = "Principal" });
                _context.Stores.Add(new Store { Name = "Auxiliar1" });
                _context.Stores.Add(new Store { Name = "Auxiliar2" });
            }
            await _context.SaveChangesAsync();
        }
    }
}
