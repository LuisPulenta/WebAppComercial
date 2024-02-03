using WebAppComercial.Api.Data;
using WebAppComercial.Shared.Entities;

namespace WebAppComercial.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;

        public SeedDb(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckCountriesAsync();
            await CheckStoresAsync();
        }

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
