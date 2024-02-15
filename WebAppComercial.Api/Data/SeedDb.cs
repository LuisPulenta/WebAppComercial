﻿using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
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
            await CheckCategoriesAsync();
            await CheckStoresAsync();
            await CheckDocumentTypesAsync();
            await CheckIvasAsync();
            await CheckMeasuresAsync();
            await CheckConceptsAsync();
            await CheckCustomersAsync();
            await CheckSuppliersAsync();
            await CheckProductsAsync();
            
            await CheckRolesAsync();
            await CheckUserAsync("Luis", "Núñez", "luis@yopmail.com", "156 814 963", UserType.Admin);
            await CheckUserAsync("Pablo", "Lacuadri", "pablo@yopmail.com", "1556 555 555", UserType.Admin);
            await CheckUserAsync("Lionel", "Messi", "messi@yopmail.com", "111 111 111", UserType.Inventory);
            await CheckUserAsync("Diego", "Maradona", "maradona@yopmail.com", "111 111 111", UserType.Inventory);
            await CheckUserAsync("Ganriel", "Batistuta", "batistuta@yopmail.com", "111 111 111", UserType.Sale);
            await CheckUserAsync("Mario", "Kempes", "kempes@yopmail.com", "111 111 111", UserType.Sale);
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

                var token = await _userHelper.GenerateEmailConfirmationTokenAsync(user);
                await _userHelper.ConfirmEmailAsync(user, token);
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
        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Alimentos" });
                _context.Categories.Add(new Category { Name = "Arte" });
                _context.Categories.Add(new Category { Name = "Aseo Hogar" });
                _context.Categories.Add(new Category { Name = "Aseo Personal" });
                _context.Categories.Add(new Category { Name = "Bebidas" });
                _context.Categories.Add(new Category { Name = "Belleza Mujer" });
                _context.Categories.Add(new Category { Name = "Carnicería" });
                _context.Categories.Add(new Category { Name = "Charcutería" });
                _context.Categories.Add(new Category { Name = "Deporte" });
                _context.Categories.Add(new Category { Name = "Electrodomésticos" });
                _context.Categories.Add(new Category { Name = "Farmacia" });
                _context.Categories.Add(new Category { Name = "Ferretería" });
                _context.Categories.Add(new Category { Name = "Frutas y Verduras" });
                _context.Categories.Add(new Category { Name = "Granos   " });
                _context.Categories.Add(new Category { Name = "Herramientas" });
                _context.Categories.Add(new Category { Name = "Interior Femenino" });
                _context.Categories.Add(new Category { Name = "Interior Masculino" });
                _context.Categories.Add(new Category { Name = "Jugos Naturales" });
                _context.Categories.Add(new Category { Name = "Juguetes" });
                _context.Categories.Add(new Category { Name = "Lácteos" });
                _context.Categories.Add(new Category { Name = "Licores" });
                _context.Categories.Add(new Category { Name = "Literatura" });
                _context.Categories.Add(new Category { Name = "Niños" });
                _context.Categories.Add(new Category { Name = "Panadería" });
                _context.Categories.Add(new Category { Name = "Salsas" });
                _context.Categories.Add(new Category { Name = "Seguridad Personal" });
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
        
        //----------------------------------------------------------------------------------------------
        private async Task CheckDocumentTypesAsync()
        {
            if (!_context.DocumentTypes.Any())
            {
                _context.DocumentTypes.Add(new DocumentType { Name = "DNI" });
                _context.DocumentTypes.Add(new DocumentType { Name = "CUIL" });
                _context.DocumentTypes.Add(new DocumentType { Name = "CUIT" });
                _context.DocumentTypes.Add(new DocumentType { Name = "Cédula Federal" });
                _context.DocumentTypes.Add(new DocumentType { Name = "Pasaporte" });
            }
            await _context.SaveChangesAsync();
        }

        //----------------------------------------------------------------------------------------------
        private async Task CheckIvasAsync()
        {
            if (!_context.Ivas.Any())
            {
                _context.Ivas.Add(new Iva { Name = "Excluído", Percentage=0 });
                _context.Ivas.Add(new Iva { Name = "Excento", Percentage = 0 });
                _context.Ivas.Add(new Iva { Name = "IVA 10.5%", Percentage = 0.105M });
                _context.Ivas.Add(new Iva { Name = "Iva 21%", Percentage = 0.210M });
            }
            await _context.SaveChangesAsync();
        }

        //----------------------------------------------------------------------------------------------
        private async Task CheckMeasuresAsync()
        {
            if (!_context.Measures.Any())
            {
                _context.Measures.Add(new Measure { Unit = "GR", Name = "Gramo" });
                _context.Measures.Add(new Measure { Unit = "KG", Name = "Kilogramo" });
                _context.Measures.Add(new Measure { Unit = "LT", Name = "Litro" });
                _context.Measures.Add(new Measure { Unit = "MT", Name = "Metro" });
                _context.Measures.Add(new Measure { Unit = "OZ", Name = "Onza" });
                _context.Measures.Add(new Measure { Unit = "UN", Name = "Unidad" });
            }
            await _context.SaveChangesAsync();
        }

        //----------------------------------------------------------------------------------------------
        private async Task CheckConceptsAsync()
        {
            if (!_context.Concepts.Any())
            {
                _context.Concepts.Add(new Concept { Name = "Autoconsumo" });
                _context.Concepts.Add(new Concept { Name = "Avería" });
                _context.Concepts.Add(new Concept { Name = "Donación" });
                _context.Concepts.Add(new Concept { Name = "Hurto" });
                _context.Concepts.Add(new Concept { Name = "Pérdida" });
            }
            await _context.SaveChangesAsync();
        }

        //----------------------------------------------------------------------------------------------
        private async Task CheckCustomersAsync()
        {
            if (!_context.Customers.Any())
            {
                _context.Customers.Add(new Customer { Name = "Luis Núñez", DocumentTypeId=1, Document= "17157729", ContactName= "Luis Núñez",Address= "Espora 2052 B° Rosedal-Córdoba",LandPhone= "4659552",CellPhone= "156814963",Email= "luis@yopmail.com",Remarks= "Hola y chauuuuuuuuuuuuu" });
                _context.Customers.Add(new Customer { Name = "Casa Tito3", DocumentTypeId = 2, Document = "33333333", ContactName = "Gilardo Gilardi", Address = "Maestro Vidal 3333 Los Robles", LandPhone = "3333333", CellPhone = "333 333 333", Email = "casatito3@gmail.com", Remarks = "Hasdasda3333333333" });
                _context.Customers.Add(new Customer { Name = "Kiosco el Choto", DocumentTypeId = 3, Document = "123456789", ContactName = "Choto Perez", Address = "Alla 666", LandPhone = "4 666 666", CellPhone = "156 666 666", Email = "choto@yopmail.com", Remarks = "chotoooooooooooo" });
                _context.Customers.Add(new Customer { Name = "JUAN CARLOS ZULUAGA CARMONA", DocumentTypeId = 1, Document = "98622480", ContactName = "JUAN CARLOS ZULUAGA CARMONA", Address = "Calle Luna Calle Sol", LandPhone = "2660000", CellPhone = "2660000", Email = "jzuluaga55@gmail.com", Remarks = "Es muy apuesto, y extremandamente sexy" });
                _context.Customers.Add(new Customer { Name = "POLITÉCNICO GRANCOLOMBIANO", DocumentTypeId = 1, Document = "98622480", ContactName = "POLITÉCNICO GRANCOLOMBIANO", Address = "Calle 81 #67 54", LandPhone = "3446677", CellPhone = "4567788", Email = "willi@hotmail.com", Remarks = "Es una muy buena universidad, donde estudian los mejores aprendices de ADSI de todo Medellín" });
                _context.Customers.Add(new Customer { Name = "RESTAURANTE EL CAN FRITO", DocumentTypeId = 1, Document = "980909099", ContactName = "RESTAURANTE EL CAN FRITO", Address = "Diagonal 45 #56 54", LandPhone = "3448899", CellPhone = "3126789090", Email = "domicilios@canfrito.com", Remarks = "El mejor perro frito de la ciudad" });
                _context.Customers.Add(new Customer { Name = "DURLEY LÓPEZ", DocumentTypeId = 1, Document = "98622480", ContactName = "DURLEY LÓPEZ", Address = "Carrera 67 #55 45", LandPhone = "2445566", CellPhone = "3118009056", Email = "duru@gmail.com", Remarks = "Es un poquito brava" });
                _context.Customers.Add(new Customer { Name = "LEDYS BEDOYA CANO", DocumentTypeId = 1, Document = "43264234", ContactName = "LEDYS BEDOYA CANO", Address = "Carrera 56 #43 43", LandPhone = "2780967", CellPhone = "3126780909", Email = "ledys@hotmail.com", Remarks = "Su esposo es el hombre más lindo del mundo" });
                _context.Customers.Add(new Customer { Name = "VINOS & VINOS", DocumentTypeId = 1, Document = "9008007005", ContactName = "VINOS & VINOS", Address = "Circula 14 #45 45", LandPhone = "3445678", CellPhone = "3124567656", Email = "pedidos@vinosyvinos.com", Remarks = "Los mejores vinos nacionales e importados" });
                _context.Customers.Add(new Customer { Name = "HELBERT SHIWSTHZ", DocumentTypeId = 1, Document = "3423323", ContactName = "HELBERT SHIWSTHZ", Address = "Calle 1 #34 34", LandPhone = "3455678", CellPhone = "4567888", Email = "Shiwsthz@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "RONAL BEDOYA", DocumentTypeId = 1, Document = "290290909", ContactName = "RONAL BEDOYA", Address = "Carrera 45 #45 67", LandPhone = "2897889", CellPhone = "", Email = "ronal@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "FERNANDO FERNANDEZ", DocumentTypeId = 1, Document = "6434544", ContactName = "FERNANDO FERNANDEZ", Address = "Calle 34 #34 56", LandPhone = "5678998", CellPhone = "", Email = "fercho@yahoo.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "DISTRIBUIDORA SNEIDER", DocumentTypeId = 1, Document = "589898", ContactName = "DISTRIBUIDORA SNEIDER", Address = "Calle 45 #43 54", LandPhone = "5617889", CellPhone = "5678990", Email = "distrisne@hotmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "CAROLINA ZAFRA", DocumentTypeId = 1, Document = "908989", ContactName = "CAROLINA ZAFRA", Address = "Carrer 50 #45 43", LandPhone = "4567898", CellPhone = "3105446677", Email = "carozafra@gmail.com", Remarks = "Es muy guapa" });
                _context.Customers.Add(new Customer { Name = "NAVELIS MIELES", DocumentTypeId = 1, Document = "86441", ContactName = "NAVELIS MIELES", Address = "Calle 45#96 56", LandPhone = "4655881654", CellPhone = "784555", Email = "Nave@hotmail.com", Remarks = "Soy Negra" });
                _context.Customers.Add(new Customer { Name = "CAROLINA TAMAYO", DocumentTypeId = 1, Document = "5145114", ContactName = "CAROLINA TAMAYO", Address = "cll 53 # 11 b 41", LandPhone = "62484454", CellPhone = "3015489824", Email = "carotamayo@gmail.com", Remarks = "se perdio" });
                _context.Customers.Add(new Customer { Name = "MILVIA HENAO", DocumentTypeId = 1, Document = "64741411", ContactName = "MILVIA HENAO", Address = "cll 50 # 45 b 44", LandPhone = "5285996", CellPhone = "30151449824", Email = "tamayomilvia@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "JESENIA TORREZ", DocumentTypeId = 1, Document = "482154", ContactName = "JESENIA TORREZ", Address = "cll 90 # 45 b 84", LandPhone = "3254810", CellPhone = "3115749824", Email = "jesnia@gmail.com", Remarks = "para traer todo" });
                _context.Customers.Add(new Customer { Name = "JONATHAN CORREA", DocumentTypeId = 1, Document = "24154", ContactName = "JONATHAN CORREA", Address = "crr 40 # 45 b 44", LandPhone = "5249872", CellPhone = "31442542155", Email = "correa@gmail.com", Remarks = "151174 años" });
                _context.Customers.Add(new Customer { Name = "JUAN ZAFRA", DocumentTypeId = 1, Document = "52441", ContactName = "JUAN ZAFRA", Address = "cll 50 # 80 ee 44", LandPhone = "5293740", CellPhone = "4544", Email = "juan@gmail.com", Remarks = "nada" });
                _context.Customers.Add(new Customer { Name = "LEIDY LOPERA", DocumentTypeId = 1, Document = "2144145", ContactName = "LEIDY LOPERA", Address = "cll 20 # 33 b 44", LandPhone = "53245514", CellPhone = "30185825544", Email = "leidy@gmail.com", Remarks = "feaa loca" });
                _context.Customers.Add(new Customer { Name = "ELVIS MIELES", DocumentTypeId = 1, Document = "5244144", ContactName = "ELVIS MIELES", Address = "cll 33 # 45 b 44", LandPhone = "5291440", CellPhone = "30148249824", Email = "guarin@gmail.com", Remarks = "nada 20154" });
                _context.Customers.Add(new Customer { Name = "JULIAN AMAYA", DocumentTypeId = 1, Document = "5247256", ContactName = "JULIAN AMAYA", Address = "cll 108 # 45 b 44", LandPhone = "5720972", CellPhone = "30482452445", Email = "amaya@gmail.com", Remarks = "mmmmm" });
                _context.Customers.Add(new Customer { Name = "PAOLA VARGAS", DocumentTypeId = 1, Document = "5681154", ContactName = "PAOLA VARGAS", Address = "cll 55 # 45 b 44", LandPhone = "3547441", CellPhone = "3015751824", Email = "paola@gmail.com", Remarks = "nada :" });
                _context.Customers.Add(new Customer { Name = "EUGENIA CORREA", DocumentTypeId = 1, Document = "56415442", ContactName = "EUGENIA CORREA", Address = "cll 48 # 45 b 44", LandPhone = "3545444", CellPhone = "30157498555", Email = "eugenia@gmail.com", Remarks = "nada tido" });
                _context.Customers.Add(new Customer { Name = "LEYDI VILLA", DocumentTypeId = 1, Document = "57825285", ContactName = "LEYDI VILLA", Address = "cll 80 # 33 b 44", LandPhone = "5249572", CellPhone = "325454421", Email = "leyd@gmail.com", Remarks = "nada33" });
                _context.Customers.Add(new Customer { Name = "LIVIS MIELES", DocumentTypeId = 1, Document = "1235", ContactName = "LIVIS MIELES", Address = "Calle 45#96 56", LandPhone = "7842210", CellPhone = "12354445", Email = "Livis@hotmail.com", Remarks = "Soy pelicortica" });
                _context.Customers.Add(new Customer { Name = "OLVIS", DocumentTypeId = 1, Document = "496554", ContactName = "OLVIS", Address = "Calle 47#56 56", LandPhone = "8654545", CellPhone = "78445525", Email = "Olvis@hotmail.com", Remarks = "Soy un viejito amargado" });
                _context.Customers.Add(new Customer { Name = "EDALI GUARIN", DocumentTypeId = 1, Document = "44587", ContactName = "EDALI GUARIN", Address = "Calle 45#96 56", LandPhone = "454875", CellPhone = "8622455", Email = "Edalig@hotmail.com", Remarks = "Soy la mejor madre del mundo" });
                _context.Customers.Add(new Customer { Name = "ANDREA", DocumentTypeId = 1, Document = "41258", ContactName = "ANDREA", Address = "Calle 45#56 14", LandPhone = "965200", CellPhone = "147822", Email = "AndreaCardona@hotmail.com", Remarks = "Soy Prima" });
                _context.Customers.Add(new Customer { Name = "MARIA MERCEDES", DocumentTypeId = 1, Document = "963014", ContactName = "MARIA MERCEDES", Address = "Calle 45#56 96", LandPhone = "789620", CellPhone = "45", Email = "Mariamercedes@hotmail.com", Remarks = "soy una mamita" });
                _context.Customers.Add(new Customer { Name = "FERNANDA", DocumentTypeId = 1, Document = "45124", ContactName = "FERNANDA", Address = "Calle 95#56 56", LandPhone = "454", CellPhone = "4512", Email = "fernds@hotmail.com", Remarks = "usted quiere lo mismo que yo" });
                _context.Customers.Add(new Customer { Name = "ALEX", DocumentTypeId = 1, Document = "96324", ContactName = "ALEX", Address = "Calle 45#96 56", LandPhone = "46544", CellPhone = "451242", Email = "elvismieles@hotmail.com", Remarks = "Lo unico que se es que anda con mis piernas" });
                _context.Customers.Add(new Customer { Name = "MECHAS", DocumentTypeId = 1, Document = "2585", ContactName = "MECHAS", Address = "calle 55 # 82 53", LandPhone = "5702638", CellPhone = "2468756", Email = "mechas@yahoo.com", Remarks = "tiene muchas mechas" });
                _context.Customers.Add(new Customer { Name = "SACAPIOJOS", DocumentTypeId = 1, Document = "54689", ContactName = "SACAPIOJOS", Address = "calle 65 # 2 73", LandPhone = "5549638", CellPhone = "2468756", Email = "sacapiojos@yahoo.com", Remarks = "tiene muchas piojos" });
                _context.Customers.Add(new Customer { Name = "PELUSA", DocumentTypeId = 1, Document = "5458", ContactName = "PELUSA", Address = "calle 92 # 8 73", LandPhone = "22401058", CellPhone = "5103624", Email = "pelusa@yahoo.com", Remarks = "tiene muchas pelitos" });
                _context.Customers.Add(new Customer { Name = "CARMAS", DocumentTypeId = 1, Document = "785", ContactName = "CARMAS", Address = "calle 85 # 82 53", LandPhone = "5210364", CellPhone = "1254789", Email = "carmas@yahoo.com", Remarks = "tiene muchas dolores" });
                _context.Customers.Add(new Customer { Name = "MORTEROS", DocumentTypeId = 1, Document = "688", ContactName = "MORTEROS", Address = "calle 15 # 82 98", LandPhone = "5578903", CellPhone = "4895624", Email = "morteros@yahoo.com", Remarks = "tiene muchas mrteros" });
                _context.Customers.Add(new Customer { Name = "BLUE", DocumentTypeId = 1, Document = "7956", ContactName = "BLUE", Address = "calle 58 # 65 5", LandPhone = "4451023", CellPhone = "4512367", Email = "blue@yahoo.com", Remarks = "tiene muchas azules" });
                _context.Customers.Add(new Customer { Name = "NEER", DocumentTypeId = 1, Document = "589", ContactName = "NEER", Address = "calle 87 # 42 13", LandPhone = "7802569", CellPhone = "8451267", Email = "neer@yahoo.com", Remarks = "tiene muchas tareas" });
                _context.Customers.Add(new Customer { Name = "PECAS", DocumentTypeId = 1, Document = "7487", ContactName = "PECAS", Address = "calle 25 # 83 6", LandPhone = "2360158", CellPhone = "5701245", Email = "pecas@yahoo.com", Remarks = "parece un milo mal revuelto" });
                _context.Customers.Add(new Customer { Name = "RECHA", DocumentTypeId = 1, Document = "8798", ContactName = "RECHA", Address = "calle 95 # 85 63", LandPhone = "1456987", CellPhone = "74569823", Email = "recha@yahoo.com", Remarks = "tiene muchas caras" });
                _context.Customers.Add(new Customer { Name = "BICTH", DocumentTypeId = 1, Document = "989", ContactName = "BICTH", Address = "calle 78 # 57 3", LandPhone = "4569872", CellPhone = "1472583", Email = "bicth@yahoo.com", Remarks = "tiene muchas nenas" });
                _context.Customers.Add(new Customer { Name = "DANIEL", DocumentTypeId = 1, Document = "1234567", ContactName = "DANIEL", Address = "cll 54 # 14 b 42", LandPhone = "3636398", CellPhone = "3012365696", Email = "daniel@gmail.com", Remarks = "conchitas" });
                _context.Customers.Add(new Customer { Name = "CARLOS BERRIO", DocumentTypeId = 1, Document = "8910111", ContactName = "CARLOS BERRIO", Address = "cll 55 # 15 b 43", LandPhone = "2365623", CellPhone = "3013625458", Email = "carlos@hotmail.com", Remarks = "lds" });
                _context.Customers.Add(new Customer { Name = "FONCECA CARNEDAS", DocumentTypeId = 1, Document = "3215648", ContactName = "FONCECA CARNEDAS", Address = "cll 56 # 16 b 46", LandPhone = "62454654", CellPhone = "3015899824", Email = "cardenas@gmail.com", Remarks = "ganas" });
                _context.Customers.Add(new Customer { Name = "SANTANA", DocumentTypeId = 1, Document = "1232322", ContactName = "SANTANA", Address = "cll 23 # 31 b 61", LandPhone = "36521454", CellPhone = "304236824", Email = "santana@gmail.com", Remarks = "muy rico" });
                _context.Customers.Add(new Customer { Name = "BOLIVAR", DocumentTypeId = 1, Document = "4565654", ContactName = "BOLIVAR", Address = "cll 12 # 31 b 81", LandPhone = "2654564", CellPhone = "30126549824", Email = "bolivar@gmail.com", Remarks = "gas" });
                _context.Customers.Add(new Customer { Name = "SAN DIEGO", DocumentTypeId = 1, Document = "5465435", ContactName = "SAN DIEGO", Address = "cll 43 # 51 b 71", LandPhone = "2926586", CellPhone = "3015658963", Email = "diego@gmail.com", Remarks = "delicioso" });
                _context.Customers.Add(new Customer { Name = "LAURA GOMEZ", DocumentTypeId = 1, Document = "4856546", ContactName = "LAURA GOMEZ", Address = "cll 15 # 21 b 36", LandPhone = "2924454", CellPhone = "3032014569", Email = "laura@gmail.com", Remarks = "bueno" });
                _context.Customers.Add(new Customer { Name = "PAOLA", DocumentTypeId = 1, Document = "2163546", ContactName = "PAOLA", Address = "cll 83 # 90 b 105", LandPhone = "2569852", CellPhone = "3045698635", Email = "paola@gmail.com", Remarks = "esta bien" });
                _context.Customers.Add(new Customer { Name = "JUAN SANCHEZ", DocumentTypeId = 1, Document = "4565353", ContactName = "JUAN SANCHEZ", Address = "cll 50 # 06 b 75", LandPhone = "2926536", CellPhone = "3012569878", Email = "juan@gmail.com", Remarks = "aguanta" });
                _context.Customers.Add(new Customer { Name = "LE BLANC", DocumentTypeId = 1, Document = "96101114969", ContactName = "LE BLANC", Address = "calle32 #54 18", LandPhone = "6845131", CellPhone = "31865454", Email = "Pepe@hotmail.com", Remarks = "Aprendi Mucho" });
                _context.Customers.Add(new Customer { Name = "YASUO", DocumentTypeId = 1, Document = "4565456", ContactName = "YASUO", Address = "calle52 #65 96", LandPhone = "4474177", CellPhone = "31874742", Email = "Yasuoooo@hotmail.com", Remarks = "el pro de pros" });
                _context.Customers.Add(new Customer { Name = "CAI", DocumentTypeId = 1, Document = "56414565", ContactName = "CAI", Address = "calle51 #10 98", LandPhone = "7523453", CellPhone = "54141", Email = "Pablito@hotmail.com", Remarks = "Alcance OP" });
                _context.Customers.Add(new Customer { Name = "VOLI", DocumentTypeId = 1, Document = "4697646", ContactName = "VOLI", Address = "calle78 #96 18", LandPhone = "45254271", CellPhone = "752542", Email = "El.SEñor@hotmail.com", Remarks = "Gruaaaaaaur" });
                _context.Customers.Add(new Customer { Name = "JINX", DocumentTypeId = 1, Document = "8946151", ContactName = "JINX", Address = "calle89 #75 115", LandPhone = "86451", CellPhone = "984561231", Email = "Jinx@hotmail.com", Remarks = "Cargado y preparado" });
                _context.Customers.Add(new Customer { Name = "FIORA", DocumentTypeId = 1, Document = "684411", ContactName = "FIORA", Address = "calle99 #87 92", LandPhone = "5423453", CellPhone = "310225454", Email = "Luchito@hotmail.com", Remarks = "La Flor Velasquez" });
                _context.Customers.Add(new Customer { Name = "DRAVEN", DocumentTypeId = 1, Document = "86421654", ContactName = "DRAVEN", Address = "calle71 #54 18", LandPhone = "57641561", CellPhone = "96846541", Email = "Creido@hotmail.com", Remarks = "Draven no. DRAAAAAVEN" });
                _context.Customers.Add(new Customer { Name = "EVELYN", DocumentTypeId = 1, Document = "45274534", ContactName = "EVELYN", Address = "calle98 #10 1558", LandPhone = "8752", CellPhone = "5723543", Email = "IVYYYY@hotmail.com", Remarks = "Arañaaaaaas" });
                _context.Customers.Add(new Customer { Name = "CHOGATH", DocumentTypeId = 1, Document = "86453453", ContactName = "CHOGATH", Address = "calle12 #96 74", LandPhone = "453543453", CellPhone = "37865454", Email = "Chogi@hotmail.com", Remarks = "Muereeeeee" });
                _context.Customers.Add(new Customer { Name = "ISA", DocumentTypeId = 1, Document = "453453453", ContactName = "ISA", Address = "Diagonal 87 #96 82", LandPhone = "8651541", CellPhone = "84184", Email = "MariaBonita@hotmail.com", Remarks = "Te quiero mucho" });
                _context.Customers.Add(new Customer { Name = "GRANERO DON JUAN", DocumentTypeId = 1, Document = "11111111", ContactName = "GRANERO DON JUAN", Address = "Calle 22 # 85 20", LandPhone = "2642070", CellPhone = "3015562238", Email = "andre@live.com", Remarks = "Molesta Mucho" });
                _context.Customers.Add(new Customer { Name = "MORCILLERIA MARIELA", DocumentTypeId = 1, Document = "22222222", ContactName = "MORCILLERIA MARIELA", Address = "Diagonal 10 # 70 70", LandPhone = "4526633", CellPhone = "3026558923", Email = "morcilla@hotmail.es", Remarks = "Es Muy Inteligente" });
                _context.Customers.Add(new Customer { Name = "TAQUERIA JUACO", DocumentTypeId = 1, Document = "33333333", ContactName = "TAQUERIA JUACO", Address = "Calle 70 # 77 29", LandPhone = "7895622", CellPhone = "3015263333", Email = "juaco@live.com", Remarks = "Que Man Tan Canzon" });
                _context.Customers.Add(new Customer { Name = "TIENDA DON LUIS", DocumentTypeId = 1, Document = "44444444", ContactName = "TIENDA DON LUIS", Address = "Calle 10 # 74 45", LandPhone = "4852210", CellPhone = "2506332015", Email = "luismejo@live.com", Remarks = "No Le Gusta Que Lo Molesten" });
                _context.Customers.Add(new Customer { Name = "PIZZAS PICOLO", DocumentTypeId = 1, Document = "55555555", ContactName = "PIZZAS PICOLO", Address = "Calle 45 # 44 50", LandPhone = "2554646", CellPhone = "3035201113", Email = "mano@gmail.com", Remarks = "Feo A Morir" });
                _context.Customers.Add(new Customer { Name = "SUPERMERCADO EL CALIDOSO", DocumentTypeId = 1, Document = "66666666", ContactName = "SUPERMERCADO EL CALIDOSO", Address = "Diagonal 101 # 33 30", LandPhone = "4521110", CellPhone = "3014562389", Email = "serrucho@live.com", Remarks = "Solo Le Gusta Lo Barato" });
                _context.Customers.Add(new Customer { Name = "MANGOS DON PEPE", DocumentTypeId = 1, Document = "77777777", ContactName = "MANGOS DON PEPE", Address = "Calle 66 # 25 56", LandPhone = "2303030", CellPhone = "3014562385", Email = "india@live.com", Remarks = "Vende Mangos Robados" });
                _context.Customers.Add(new Customer { Name = "LIBRERIA ANTON", DocumentTypeId = 1, Document = "88888888", ContactName = "LIBRERIA ANTON", Address = "Calle 00 # 14 13", LandPhone = "9602356", CellPhone = "3098562211", Email = "pelagra@yahoo.com", Remarks = "Solo Pirata" });
                _context.Customers.Add(new Customer { Name = "TIENDA ELBALAZO", DocumentTypeId = 1, Document = "99999999", ContactName = "TIENDA ELBALAZO", Address = "Carrera 55 # 69 289", LandPhone = "6033030", CellPhone = "3001234567", Email = "melo@hotmail.com", Remarks = "Mera Frontera" });
                _context.Customers.Add(new Customer { Name = "LEGUMBRERIA EL PRECIO MENOS", DocumentTypeId = 1, Document = "542040", ContactName = "LEGUMBRERIA EL PRECIO MENOS", Address = "Calle 44 # 11 99", LandPhone = "4215285", CellPhone = "30001025636", Email = "joselo@live.com", Remarks = "Solo Lo Mejor" });
                _context.Customers.Add(new Customer { Name = "LICORES FLA", DocumentTypeId = 1, Document = "98521452", ContactName = "LICORES FLA", Address = "Sol a la una 45", LandPhone = "5342512", CellPhone = "3114872540", Email = "Licoresgmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "CAFE SALUD", DocumentTypeId = 1, Document = "87451265", ContactName = "CAFE SALUD", Address = "Sol a la  35", LandPhone = "5442512", CellPhone = "3124872540", Email = "Diosagmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "SENA", DocumentTypeId = 1, Document = "24456789", ContactName = "SENA", Address = "Sol  la  45", LandPhone = "5547552", CellPhone = "3184872540", Email = "sena@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "CHOCOLOTES", DocumentTypeId = 1, Document = "754781236", ContactName = "CHOCOLOTES", Address = "Luna la 74 # 84 21", LandPhone = "5874512", CellPhone = "3548790012", Email = "Chocolate@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "SUSANAFB", DocumentTypeId = 1, Document = "21874254", ContactName = "SUSANAFB", Address = "Noche la 34", LandPhone = "5348712", CellPhone = "3114981234", Email = "Susana@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "PASCUAL", DocumentTypeId = 1, Document = "88030952795", ContactName = "PASCUAL", Address = "CALLE MORELOS NO. 24", LandPhone = "913367385", CellPhone = "2374409", Email = "Pascual@gmail.com", Remarks = "no esta en contacto" });
                _context.Customers.Add(new Customer { Name = "TOMAS", DocumentTypeId = 1, Document = "76166725425", ContactName = "TOMAS", Address = "CALLE MATAMOROS NO. 225", LandPhone = "913367385", CellPhone = "2374410", Email = "tomas@gmail.com", Remarks = "no esta casado y esta solo" });
                _context.Customers.Add(new Customer { Name = "ARMANDO", DocumentTypeId = 1, Document = "89120569377", ContactName = "ARMANDO", Address = "CAV.INDEPENDENCIA NO. 574", LandPhone = "913367385", CellPhone = "2344409", Email = "armando@gmail.com", Remarks = "tiene novia" });
                _context.Customers.Add(new Customer { Name = "ARTURO", DocumentTypeId = 1, Document = "90020268726", ContactName = "ARTURO", Address = "AV. INDEPENDENCIA NO. 565-A", LandPhone = "913367385", CellPhone = "2374533", Email = "arturo@gmail.com", Remarks = "no esta en contacto" });
                _context.Customers.Add(new Customer { Name = "FREDY", DocumentTypeId = 1, Document = "89062176506", ContactName = "FREDY", Address = "AV. LIBERTAD NO. 956", LandPhone = "913367385", CellPhone = "2363396", Email = "fredy@gmail.com", Remarks = "tiene sueños malos" });
                _context.Customers.Add(new Customer { Name = "LAURA", DocumentTypeId = 1, Document = "88070179002", ContactName = "LAURA", Address = "AV. LIBERTAD NO. 897", LandPhone = "913367385", CellPhone = "2374414", Email = "laura@gmail.com", Remarks = "come mucho" });
                _context.Customers.Add(new Customer { Name = "ARNULIO", DocumentTypeId = 1, Document = "89112363808", ContactName = "ARNULIO", Address = "CALLE AGUSTIN LARA NO. 69-B", LandPhone = "913367385", CellPhone = "2374623", Email = "arnulio@gmail.com", Remarks = "es super normal" });
                _context.Customers.Add(new Customer { Name = "CAROLINA", DocumentTypeId = 1, Document = "89022457765", ContactName = "CAROLINA", Address = "CALLE AGUSTIN LARA NO. 69-B", LandPhone = "913367385", CellPhone = "2375369", Email = "carolina@gmail.com", Remarks = "y no tiene mascota" });
                _context.Customers.Add(new Customer { Name = "BRAYAN", DocumentTypeId = 1, Document = "90040855555", ContactName = "BRAYAN", Address = "CALLE AGUSTIN LARA NO. 69-B", LandPhone = "913367385", CellPhone = "2774409", Email = "brayan@gmail.com", Remarks = "tiene autoestima alto" });
                _context.Customers.Add(new Customer { Name = "JOHANA", DocumentTypeId = 1, Document = "90032672999", ContactName = "JOHANA", Address = "CALLE AGUSTIN LARA NO. 69-B", LandPhone = "913367385", CellPhone = "2374499", Email = "johana@gmail.com", Remarks = "si tiene plata" });
                _context.Customers.Add(new Customer { Name = "TAMARINDOS", DocumentTypeId = 1, Document = "970203640", ContactName = "TAMARINDOS", Address = "Calle 58b #07c 12", LandPhone = "3121067", CellPhone = "320245", Email = "TamaJuan@hotmail.com", Remarks = "Tamarindo Dulce" });
                _context.Customers.Add(new Customer { Name = "PASTAS LA MUÑECA", DocumentTypeId = 1, Document = "970408094", ContactName = "PASTAS LA MUÑECA", Address = "Calle 69a #14c 20", LandPhone = "3148596", CellPhone = "2699874", Email = "Gloria@hotmail.com", Remarks = "Las mejores pastas" });
                _context.Customers.Add(new Customer { Name = "BOCADILLOS EL CARIBE", DocumentTypeId = 1, Document = "950206450", ContactName = "BOCADILLOS EL CARIBE", Address = "Calle 70 #07 12", LandPhone = "2110100", CellPhone = "2203000", Email = "Alvarez@hotmail.com", Remarks = "Para toda la vida" });
                _context.Customers.Add(new Customer { Name = "LICORES ANU", DocumentTypeId = 1, Document = "112185486", ContactName = "LICORES ANU", Address = "Calle 69b #47c 12", LandPhone = "2045263", CellPhone = "2016869", Email = "Manu@hotmail.com", Remarks = "Solo para mayores de edad" });
                _context.Customers.Add(new Customer { Name = "GOMITAS KATHE", DocumentTypeId = 1, Document = "112698978", ContactName = "GOMITAS KATHE", Address = "Calle 65b 26 12", LandPhone = "2345658", CellPhone = "4203236", Email = "Kathe@hotmail.com", Remarks = "Gomitas Trululu" });
                _context.Customers.Add(new Customer { Name = "CHOCOLATES", DocumentTypeId = 1, Document = "112568412", ContactName = "CHOCOLATES", Address = "Calle 62 #12 26", LandPhone = "4225669", CellPhone = "4484649", Email = "Santii@hotmail.com", Remarks = "Cocolates Jet" });
                _context.Customers.Add(new Customer { Name = "MENTAS CHAO", DocumentTypeId = 1, Document = "415651586", ContactName = "MENTAS CHAO", Address = "Calle 29a #69 20", LandPhone = "4986321", CellPhone = "3204569", Email = "Mentitas@hotmail.com", Remarks = "Chao Chao" });
                _context.Customers.Add(new Customer { Name = "GORRAS", DocumentTypeId = 1, Document = "850485455", ContactName = "GORRAS", Address = "Calle 15 #56b 12", LandPhone = "2016589", CellPhone = "4562136", Email = "pani@hotmail.com", Remarks = "Gorras a 5000" });
                _context.Customers.Add(new Customer { Name = "ZAPATOS", DocumentTypeId = 1, Document = "412331341", ContactName = "ZAPATOS", Address = "Calle 39b #58c 12", LandPhone = "2299865", CellPhone = "2299897", Email = "Nare@hotmail.com", Remarks = "ADIDAS" });
                _context.Customers.Add(new Customer { Name = "CAFE", DocumentTypeId = 1, Document = "415654621", ContactName = "CAFE", Address = "Calle 37b 15 12", LandPhone = "2016965", CellPhone = "4556362", Email = "ramon@hotmail.com", Remarks = "EL cafe de colombia" });
                _context.Customers.Add(new Customer { Name = "DDC", DocumentTypeId = 1, Document = "999484876", ContactName = "DDC", Address = "Calle 39C #116 18", LandPhone = "1027834", CellPhone = "3189856", Email = "david@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "CLOTILDE71", DocumentTypeId = 1, Document = "8345734985", ContactName = "CLOTILDE71", Address = "Calle 54 #34 78", LandPhone = "5724398", CellPhone = "4945637", Email = "clo71@yahoo.com", Remarks = "Mujer joven" });
                _context.Customers.Add(new Customer { Name = "FABRICATO S.A.", DocumentTypeId = 1, Document = "23125878", ContactName = "FABRICATO S.A.", Address = "Calle 2 #234 78", LandPhone = "7452738", CellPhone = "7456573", Email = "angelav@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "I.E. EDOS", DocumentTypeId = 1, Document = "576303459", ContactName = "I.E. EDOS", Address = "Calle 126C #16 128", LandPhone = "3748596", CellPhone = "3478394", Email = "susanaedos@hotmail.es", Remarks = "Coordinadora superior" });
                _context.Customers.Add(new Customer { Name = "CLARO S.A.", DocumentTypeId = 1, Document = "345423523", ContactName = "CLARO S.A.", Address = "Carrera 52A #28 34", LandPhone = "5746293", CellPhone = "5826533", Email = "guarin123@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "BIMBO", DocumentTypeId = 1, Document = "68944654", ContactName = "BIMBO", Address = "Calle 58C #98B 07", LandPhone = "5485734", CellPhone = "6549659", Email = "santi@hotmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "P&G", DocumentTypeId = 1, Document = "4544564", ContactName = "P&G", Address = "Carrera 39C #116 18", LandPhone = "9835467", CellPhone = "9856374", Email = "builes@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "COLGATE", DocumentTypeId = 1, Document = "656546456", ContactName = "COLGATE", Address = "Avenida 3 #16 8", LandPhone = "3547586", CellPhone = "9806738", Email = "colgatejose@outlook.es", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "EPM", DocumentTypeId = 1, Document = "738293057", ContactName = "EPM", Address = "Calle 87E #21 1", LandPhone = "3467894", CellPhone = "9678456", Email = "frnk@outlook.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "ANDRÉS ZAPATA", DocumentTypeId = 1, Document = "96081913784", ContactName = "ANDRÉS ZAPATA", Address = "calle 49 # 99 E 86", LandPhone = "4906895", CellPhone = "3004957867", Email = "", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "ADALVERTO", DocumentTypeId = 1, Document = "3224564", ContactName = "ADALVERTO", Address = "calle 67 # 54 98", LandPhone = "2345442", CellPhone = "31346543", Email = "", Remarks = "profesor de economia" });
                _context.Customers.Add(new Customer { Name = "CRISTINA RUIZ", DocumentTypeId = 1, Document = "7865688", ContactName = "CRISTINA RUIZ", Address = "calle 34 # 68 E 98 interior 204", LandPhone = "49065433", CellPhone = "300654343", Email = "", Remarks = "matematicas" });
                _context.Customers.Add(new Customer { Name = "HIGINIO", DocumentTypeId = 1, Document = "98657865455", ContactName = "HIGINIO", Address = "calle 65 # 67 E 88", LandPhone = "4906543", CellPhone = "3004957543", Email = "", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "DUVAN CARVAJAL", DocumentTypeId = 1, Document = "96544335677", ContactName = "DUVAN CARVAJAL", Address = "calle 67 # 65 45", LandPhone = "4906895", CellPhone = "3004957867", Email = "", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "ANDRÉS MAZO", DocumentTypeId = 1, Document = "9687655787", ContactName = "ANDRÉS MAZO", Address = "calle 49 # 87 E 56 interior 301", LandPhone = "2563332", CellPhone = "300654344", Email = "", Remarks = "vecino" });
                _context.Customers.Add(new Customer { Name = "ORLANDO TERRI", DocumentTypeId = 1, Document = "4543223453", ContactName = "ORLANDO TERRI", Address = "calle 78 # 65  97", LandPhone = "49065533", CellPhone = "3006545676", Email = "", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "MILO VALENCIA", DocumentTypeId = 1, Document = "4544323566", ContactName = "MILO VALENCIA", Address = "calle 54 # 78 AA 86", LandPhone = "490556", CellPhone = "30075678786", Email = "", Remarks = "mi musica" });
                _context.Customers.Add(new Customer { Name = "OSCAR EL EMO", DocumentTypeId = 1, Document = "656875432", ContactName = "OSCAR EL EMO", Address = "calle 65 # 75 EE 76", LandPhone = "4906567", CellPhone = "3004654567", Email = "", Remarks = "la sangre" });
                _context.Customers.Add(new Customer { Name = "SEBAS", DocumentTypeId = 1, Document = "5435776543", ContactName = "SEBAS", Address = "calle 45 # 99 E 86", LandPhone = "49066436", CellPhone = "3004956778", Email = "", Remarks = "el metro" });
                _context.Customers.Add(new Customer { Name = "JUAN", DocumentTypeId = 1, Document = "95100621844", ContactName = "JUAN", Address = "Restrepo", LandPhone = "Cra50A-87#28", CellPhone = "2365868", Email = "3135099693", Remarks = "Responsable" });
                _context.Customers.Add(new Customer { Name = "DAYAN", DocumentTypeId = 1, Document = "1152701134", ContactName = "DAYAN", Address = "Tirado", LandPhone = "Cra50A-87#28", CellPhone = "2365868", Email = "3207068179", Remarks = "Trabajador" });
                _context.Customers.Add(new Customer { Name = "CLARA", DocumentTypeId = 1, Document = "39325313", ContactName = "CLARA", Address = "Franco", LandPhone = "Cra49A-56#18", CellPhone = "2368545", Email = "3147232154", Remarks = "Una Mujer Fuerte" });
                _context.Customers.Add(new Customer { Name = "OSCAR", DocumentTypeId = 1, Document = "39325816", ContactName = "OSCAR", Address = "Franco", LandPhone = "Avenida Caracas", CellPhone = "4165287", Email = "3103596564", Remarks = "extaordinario" });
                _context.Customers.Add(new Customer { Name = "MARIA", DocumentTypeId = 1, Document = "1452368", ContactName = "MARIA", Address = "Franco", LandPhone = "Cra50A-87#28", CellPhone = "2355868", Email = "313509456", Remarks = "Hermosa" });
                _context.Customers.Add(new Customer { Name = "ANDREZ", DocumentTypeId = 1, Document = "236584879", ContactName = "ANDREZ", Address = "Restrepo", LandPhone = "Cra50A-87#28", CellPhone = "2365868", Email = "3135099693", Remarks = "Responsable" });
                _context.Customers.Add(new Customer { Name = "EMPANADAS LA LUPA", DocumentTypeId = 1, Document = "98754632599", ContactName = "EMPANADAS LA LUPA", Address = "Calle 59 # 69 51", LandPhone = "2309263", CellPhone = "3126068617", Email = "empanaditas@gmail.com", Remarks = "ricas" });
                _context.Customers.Add(new Customer { Name = "MARTA FRANCO", DocumentTypeId = 1, Document = "43456357", ContactName = "MARTA FRANCO", Address = "cra 80 # 69 51", LandPhone = "4364983", CellPhone = "5216398", Email = "martica@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "ANA UPEGUI", DocumentTypeId = 1, Document = "718965236", ContactName = "ANA UPEGUI", Address = "Calle 25 # 65 51", LandPhone = "5236984", CellPhone = "7452396", Email = "anaU@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "RESTAURANTE NELLY", DocumentTypeId = 1, Document = "87546293", ContactName = "RESTAURANTE NELLY", Address = "Calle 70 # 43 61", LandPhone = "2605846", CellPhone = "3135248657", Email = "restauranteNelly@hotmail.com", Remarks = "innovador, y delicioso" });
                _context.Customers.Add(new Customer { Name = "LA ESPERANZA", DocumentTypeId = 1, Document = "54268964", ContactName = "LA ESPERANZA", Address = "cra 59 # 78 23", LandPhone = "2695874", CellPhone = "2695362", Email = ";Laesperanza@yahoo.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "DARIO MURILLO", DocumentTypeId = 1, Document = "98654123699", ContactName = "DARIO MURILLO", Address = "cra 38 # 52 63", LandPhone = "5239687", CellPhone = "3116311907", Email = "dario@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "JAIRO MUÑOZ", DocumentTypeId = 1, Document = "71896523", ContactName = "JAIRO MUÑOZ", Address = "calle 24 # 32 65", LandPhone = "2308545", CellPhone = "312563984", Email = "jairito@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "LA MAZAMORRA PAISA", DocumentTypeId = 1, Document = "9865236", ContactName = "LA MAZAMORRA PAISA", Address = "cra 52 # 56 32", LandPhone = "25639850", CellPhone = "3125648796", Email = "mazamorrapaisa@hotmail.com", Remarks = "sustanciosa" });
                _context.Customers.Add(new Customer { Name = "CIGARRERIA", DocumentTypeId = 1, Document = "98563241", ContactName = "CIGARRERIA", Address = "Calle 56 # 87 56", LandPhone = "5896321", CellPhone = "3047859632", Email = "delarua@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "AMANDA LOPEZ", DocumentTypeId = 1, Document = "43563256", ContactName = "AMANDA LOPEZ", Address = "cra 32 # 69 51", LandPhone = "2605636", CellPhone = "3135226366", Email = "Amanda@gmail.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "LA CHARCUTERIA", DocumentTypeId = 1, Document = "785695", ContactName = "LA CHARCUTERIA", Address = "calle 70 # 65 95", LandPhone = "2306666", CellPhone = "2605555", Email = "charcuteria@yahoo.com", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "EL PEPINO", DocumentTypeId = 1, Document = "1215453246", ContactName = "EL PEPINO", Address = "Cr 81 C CL4 SUR 9 (interior 124)", LandPhone = "3452145", CellPhone = "2382156", Email = "PepinoFres@gmail.com", Remarks = "Frescos" });
                _context.Customers.Add(new Customer { Name = "PASTEL", DocumentTypeId = 1, Document = "1216545420", ContactName = "PASTEL", Address = "Cll5 # 30 a 10", LandPhone = "2541102", CellPhone = "2365456", Email = "PepinoFres@hotmail.com", Remarks = "Delicioso con Gaseosa" });
                _context.Customers.Add(new Customer { Name = "SALCHIPAPAS", DocumentTypeId = 1, Document = "7852123232", ContactName = "SALCHIPAPAS", Address = "Cll21 # 45 a 23", LandPhone = "5124565", CellPhone = "3215278", Email = "Salchi10@yahoo.es", Remarks = "Deliciosas" });
                _context.Customers.Add(new Customer { Name = "EL SABOREO", DocumentTypeId = 1, Document = "1152445569", ContactName = "EL SABOREO", Address = "Cll32 # 35 a 23", LandPhone = "3432105", CellPhone = "3438016", Email = "ElSaboreo@gmail.com", Remarks = "Delicioas" });
                _context.Customers.Add(new Customer { Name = "SALCHICHA", DocumentTypeId = 1, Document = "6446413568", ContactName = "SALCHICHA", Address = "Cll54 # 30 a 30", LandPhone = "2310801", CellPhone = "2252150", Email = "Salchicha@outlook.com", Remarks = "Frescas" });
                _context.Customers.Add(new Customer { Name = "LECHE", DocumentTypeId = 1, Document = "1216453046", ContactName = "LECHE", Address = "Cll21 # 54 a 68", LandPhone = "3413115", CellPhone = "3421029", Email = "Leche@gmail.com", Remarks = "Leche del Campo" });
                _context.Customers.Add(new Customer { Name = "BOLIS", DocumentTypeId = 1, Document = "4443213684", ContactName = "BOLIS", Address = "Cll35 # 54 a 68", LandPhone = "3252205", CellPhone = "3435446", Email = "Bolis@hotmail.com", Remarks = "Dulces y FResco" });
                _context.Customers.Add(new Customer { Name = "EL BOROJO", DocumentTypeId = 1, Document = "5435432867", ContactName = "EL BOROJO", Address = "Cll30 # 64 a 68", LandPhone = "3665285", CellPhone = "3682056", Email = "Elborojo@gmail.com", Remarks = "Fresco y bitaminoso" });
                _context.Customers.Add(new Customer { Name = "CHONTADURO", DocumentTypeId = 1, Document = "1225485325", ContactName = "CHONTADURO", Address = "Cll10 # 64 a 68", LandPhone = "3021450", CellPhone = "2365456", Email = "Chontaduro@outlook.com", Remarks = "Frescos" });
                _context.Customers.Add(new Customer { Name = "LOSCOCOS", DocumentTypeId = 1, Document = "1152456568", ContactName = "LOSCOCOS", Address = "C82a # 54 a 50", LandPhone = "3045758605", CellPhone = "4215733", Email = "sba123@gmail.com", Remarks = "rico" });
                _context.Customers.Add(new Customer { Name = "LOS MANGOS", DocumentTypeId = 1, Document = "1155436567", ContactName = "LOS MANGOS", Address = "C86a # 58 a 50", LandPhone = "3045430000000", CellPhone = "4234545", Email = "carlos@gmail.com", Remarks = "dulce" });
                _context.Customers.Add(new Customer { Name = "LAS PAPAS", DocumentTypeId = 1, Document = "1232124345", ContactName = "LAS PAPAS", Address = "C82a # 50 a 50", LandPhone = "3035759605", CellPhone = "4215533", Email = "cano@gmail.com", Remarks = "fritas" });
                _context.Customers.Add(new Customer { Name = "AZUCAR", DocumentTypeId = 1, Document = "1124355486", ContactName = "AZUCAR", Address = "C87a # 54 a 50", LandPhone = "3045758605", CellPhone = "4445733", Email = "andres@gmail.com", Remarks = "dulce" });
                _context.Customers.Add(new Customer { Name = "ARROZ", DocumentTypeId = 1, Document = "1112466568", ContactName = "ARROZ", Address = "C83a # 54 a 50", LandPhone = "3045754605", CellPhone = "4211133", Email = "luisa@gmail.com", Remarks = "duradero" });
                _context.Customers.Add(new Customer { Name = "LOS PEPINOS", DocumentTypeId = 1, Document = "1142434368", ContactName = "LOS PEPINOS", Address = "C82a # 34 a 50", LandPhone = "3045756605", CellPhone = "4215799", Email = "michel@gmail.com", Remarks = "rico" });
                _context.Customers.Add(new Customer { Name = "EXITO", DocumentTypeId = 1, Document = "1162456768", ContactName = "EXITO", Address = "C88a # 55 a 51", LandPhone = "3045766605", CellPhone = "4417733", Email = "isabel@gmail.com", Remarks = "ricos" });
                _context.Customers.Add(new Customer { Name = "LOS PANES", DocumentTypeId = 1, Document = "1172454568", ContactName = "LOS PANES", Address = "C80a # 54 a 50", LandPhone = "3045778605", CellPhone = "2315533", Email = "andrea@gmail.com", Remarks = "dulces" });
                _context.Customers.Add(new Customer { Name = "LOS PASTELES", DocumentTypeId = 1, Document = "1182423368", ContactName = "LOS PASTELES", Address = "C80a # 40 a 50", LandPhone = "3048858605", CellPhone = "3315733", Email = "ramirez@gmail.com", Remarks = "grandes" });
                _context.Customers.Add(new Customer { Name = "PIPE", DocumentTypeId = 1, Document = "102345", ContactName = "PIPE", Address = "Calle 56 N 32-12", LandPhone = "4540234", CellPhone = "3015948758", Email = "duquefelipe95@gmail.com", Remarks = "Es muy grande" });
                _context.Customers.Add(new Customer { Name = "CHEPE", DocumentTypeId = 1, Document = "102346", ContactName = "CHEPE", Address = "Calle 85 N 45-32", LandPhone = "8564489", CellPhone = "3035948754", Email = "chepe@gmail.com", Remarks = "Es pequeño" });
                _context.Customers.Add(new Customer { Name = "CALICHE", DocumentTypeId = 1, Document = "102347", ContactName = "CALICHE", Address = "Carrera 25 N 65-82", LandPhone = "4444402", CellPhone = "3085699878", Email = "kaliche@gmail.com", Remarks = "Es muy trabajador" });
                _context.Customers.Add(new Customer { Name = "KIKE", DocumentTypeId = 1, Document = "102348", ContactName = "KIKE", Address = "Calle 12 N 56-87", LandPhone = "4444408", CellPhone = "3105623667", Email = "kike5465@gmail.com", Remarks = "Es muy inteligente" });
                _context.Customers.Add(new Customer { Name = "PACHO", DocumentTypeId = 1, Document = "102349", ContactName = "PACHO", Address = "Calle 56 N 32-12", LandPhone = "4540234", CellPhone = "3015948758", Email = "pachito@gmail.com", Remarks = "Es muy grande" });
                _context.Customers.Add(new Customer { Name = "ANA", DocumentTypeId = 1, Document = "102341", ContactName = "ANA", Address = "Calle 21 N 78-65", LandPhone = "2336587", CellPhone = "3185896932", Email = "anamelosa@gmail.com", Remarks = "Es muy melosa" });
                _context.Customers.Add(new Customer { Name = "JAVI", DocumentTypeId = 1, Document = "102343", ContactName = "JAVI", Address = "Carrera 88 N 36-85", LandPhone = "8888888", CellPhone = "3026665645", Email = "golcaracol@gmail.com", Remarks = "Goooooooooooool" });
                _context.Customers.Add(new Customer { Name = "ALFA", DocumentTypeId = 1, Document = "102340", ContactName = "ALFA", Address = "Carrera 55 N 25-84", LandPhone = "4444565", CellPhone = "3147575116", Email = "alfalfasoyyo@gmail.com", Remarks = "Se me para el pelo" });
                _context.Customers.Add(new Customer { Name = "JIME", DocumentTypeId = 1, Document = "102545", ContactName = "JIME", Address = "Carrera 89 N 23-15", LandPhone = "9566962", CellPhone = "3012569863", Email = "wskhfeiughweiu@gmail.com", Remarks = "Te atiendo" });
                _context.Customers.Add(new Customer { Name = "J & F", DocumentTypeId = 1, Document = "1567459236", ContactName = "J & F", Address = "C65b # 54 a 20", LandPhone = "3045695245", CellPhone = "6584512", Email = "Quientero14@gmail.com", Remarks = "ala Fresca" });
                _context.Customers.Add(new Customer { Name = "LAS PALMERAS", DocumentTypeId = 1, Document = "4569853125", ContactName = "LAS PALMERAS", Address = "C95b # 33 a 10", LandPhone = "4325941638", CellPhone = "7416523", Email = "Orozkito_4@gmail.com", Remarks = "Delicias para el paladar" });
                _context.Customers.Add(new Customer { Name = "ZENU", DocumentTypeId = 1, Document = "3524896215", ContactName = "ZENU", Address = "C84b # 67 a 15", LandPhone = "1895614263", CellPhone = "6584125", Email = "Arnulito_rastas@gmail.com", Remarks = "Refrescante" });
                _context.Customers.Add(new Customer { Name = "ALPINA", DocumentTypeId = 1, Document = "5482444458", ContactName = "ALPINA", Address = "C75b # 10 a 44", LandPhone = "8531274596", CellPhone = "1478523", Email = "Danielrxn@gmail.com", Remarks = "Dulce y Sabroso" });
                _context.Customers.Add(new Customer { Name = "JET", DocumentTypeId = 1, Document = "3571596548", ContactName = "JET", Address = "C13b # 55 a 99", LandPhone = "3541268754", CellPhone = "4754295", Email = "Mateo_lds@gmail.com", Remarks = "Dulce Chocolate" });
                _context.Customers.Add(new Customer { Name = "SYSTEMS", DocumentTypeId = 1, Document = "9874563165", ContactName = "SYSTEMS", Address = "C48b # 64 a 65", LandPhone = "6825473120", CellPhone = "1203640", Email = "jzulu_55@gmail.com", Remarks = "Tecnologia" });
                _context.Customers.Add(new Customer { Name = "FRITO LAY", DocumentTypeId = 1, Document = "4563211597", ContactName = "FRITO LAY", Address = "C41b # 65 a 20", LandPhone = "2014795631", CellPhone = "7539512", Email = "Vera15@gmail.com", Remarks = "Fritos" });
                _context.Customers.Add(new Customer { Name = "ABURRA", DocumentTypeId = 1, Document = "4598261574", ContactName = "ABURRA", Address = "C77b # 66 a 66", LandPhone = "7519530265", CellPhone = "7851354", Email = "Camilochochis@gmail.com", Remarks = "Grano" });
                _context.Customers.Add(new Customer { Name = "MIRASOL", DocumentTypeId = 1, Document = "4136589746", ContactName = "MIRASOL", Address = "C33b # 57 a 17", LandPhone = "9874123654", CellPhone = "1539578", Email = "Zapatika@gmail.com", Remarks = "Lacteos" });
                _context.Customers.Add(new Customer { Name = "LAVANDER", DocumentTypeId = 1, Document = "6985213489", ContactName = "LAVANDER", Address = "C52b # 36 a 10", LandPhone = "3698741025", CellPhone = "2015638", Email = "Levispala@gmail.com", Remarks = "Cosmeticos" });
                _context.Customers.Add(new Customer { Name = "SINGUENTHON", DocumentTypeId = 1, Document = "4821684", ContactName = "SINGUENTHON", Address = "CALLE 90 NO 65 APT 201", LandPhone = "254887424", CellPhone = "4554142", Email = "singuenthon218@yahoo.es", Remarks = "casado" });
                _context.Customers.Add(new Customer { Name = "SEBASTIAN", DocumentTypeId = 1, Document = "8119784", ContactName = "SEBASTIAN", Address = "CALLE 45 NO 33 APT 301", LandPhone = "57461564", CellPhone = "4597144", Email = "sebas@yahoo.es", Remarks = "casado" });
                _context.Customers.Add(new Customer { Name = "VALERIA", DocumentTypeId = 1, Document = "4681154", ContactName = "VALERIA", Address = "CALLE 20 NO 45 APT 203", LandPhone = "841687", CellPhone = "43541565", Email = "valeria18@yahoo.es", Remarks = "soltero" });
                _context.Customers.Add(new Customer { Name = "JULISSA", DocumentTypeId = 1, Document = "4846584", ContactName = "JULISSA", Address = "CALLE 10 NO 45 APT 20", LandPhone = "2546744", CellPhone = "456444", Email = "julissaon218@yahoo.es", Remarks = "casada" });
                _context.Customers.Add(new Customer { Name = "EL MACHETEADERO", DocumentTypeId = 1, Document = "15645616", ContactName = "EL MACHETEADERO", Address = "Calle 50 # 96 40", LandPhone = "4552566", CellPhone = "3114555440", Email = "pedro.r@gmail.com", Remarks = "Sele Brinda Respeto" });
                _context.Customers.Add(new Customer { Name = "SOLO PALETAS", DocumentTypeId = 1, Document = "3541331", ContactName = "SOLO PALETAS", Address = "Calle 80 # 92 45", LandPhone = "4682435", CellPhone = "3215568424", Email = "juan.g@gmail.com", Remarks = "Para Esos Calores Matadores" });
                _context.Customers.Add(new Customer { Name = "SUPER HELADO", DocumentTypeId = 1, Document = "54512545", ContactName = "SUPER HELADO", Address = "Carrera 20 # 50 92", LandPhone = "4552266", CellPhone = "3186654422", Email = "liusito@gmail.com", Remarks = "Deleitate Con El Super Helado" });
                _context.Customers.Add(new Customer { Name = "POLLO FRESCO", DocumentTypeId = 1, Document = "65425454", ContactName = "POLLO FRESCO", Address = "Calle 56 # 42 65", LandPhone = "8554466", CellPhone = "3115588552", Email = "fransisc@gmail.com", Remarks = "Pollo A Carbon" });
                _context.Customers.Add(new Customer { Name = "EL CHORIZERO", DocumentTypeId = 1, Document = "75624521", ContactName = "EL CHORIZERO", Address = "Calle 30 # 61 56", LandPhone = "2554445", CellPhone = "3144553322", Email = "caliche.i.@gmail.com", Remarks = "Grandes y Deliciosos" });
                _context.Customers.Add(new Customer { Name = "EL RESUCITE", DocumentTypeId = 1, Document = "85754255", ContactName = "EL RESUCITE", Address = "Calle 50 # 89 22", LandPhone = "6554422", CellPhone = "3114506588", Email = "doris.r@gmail.com", Remarks = "Caldito Levanta Muertos" });
                _context.Customers.Add(new Customer { Name = "SOLO FRIAS", DocumentTypeId = 1, Document = "62654525", ContactName = "SOLO FRIAS", Address = "Calle 45 # 65 21", LandPhone = "2445587", CellPhone = "3217896544", Email = "fernando@gmail.com", Remarks = "Para Que Te Refresques" });
                _context.Customers.Add(new Customer { Name = "EL REFRESQUE", DocumentTypeId = 1, Document = "85455642", ContactName = "EL REFRESQUE", Address = "Calle 90 # 54 54", LandPhone = "5528224", CellPhone = "3124789622", Email = "alfred@gmail.com", Remarks = "Pola Refrescante" });
                _context.Customers.Add(new Customer { Name = "SOLO POLAS", DocumentTypeId = 1, Document = "32354122", ContactName = "SOLO POLAS", Address = "Calle 50 # 11 14", LandPhone = "4772255", CellPhone = "3186635512", Email = "milo.l.@gmail.com", Remarks = "Polas Las Frescuras  " });
                _context.Customers.Add(new Customer { Name = "EL AGUARDIENTERO", DocumentTypeId = 1, Document = "23564554", ContactName = "EL AGUARDIENTERO", Address = "Calle 120 # 54 14", LandPhone = "4552566", CellPhone = "3127895244", Email = "julian.g.@gmail.com", Remarks = "Para Subirte El Animo" });
                _context.Customers.Add(new Customer { Name = "DOMO", DocumentTypeId = 1, Document = "11224588", ContactName = "DOMO", Address = "CRR 113 34-23", LandPhone = "47563256", CellPhone = "9513520", Email = "ZUÑIGA@GMAIL.COM", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "MTV", DocumentTypeId = 1, Document = "84553095", ContactName = "MTV", Address = "CLL 3-23", LandPhone = "2577885", CellPhone = "42355789", Email = "CAMI@GMAIL.COM", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "TOTTO", DocumentTypeId = 1, Document = "11224581", ContactName = "TOTTO", Address = "CRR 113 54-93", LandPhone = "997563256", CellPhone = "9513527", Email = "JAMESA@GMAIL.COM", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "ALQUERIA", DocumentTypeId = 1, Document = "11224583", ContactName = "ALQUERIA", Address = "CRR 102 34-23", LandPhone = "78963256", CellPhone = "9513525", Email = "DAVIDLUIZ@GMAIL.COM", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "IBIZA", DocumentTypeId = 1, Document = "11224584", ContactName = "IBIZA", Address = "CLL 74 34-23", LandPhone = "4755448256", CellPhone = "9513524", Email = "ELVIO@GMAIL.COM", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "JGB", DocumentTypeId = 1, Document = "11224585", ContactName = "JGB", Address = "CRR 65 34-93", LandPhone = "448553256", CellPhone = "9513523", Email = "LADERAS@GMAIL.COM", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "LENOVO", DocumentTypeId = 1, Document = "11224586", ContactName = "LENOVO", Address = "CL 78 34-91", LandPhone = "47123256", CellPhone = "9513522", Email = "PONCHIS@GMAIL.COM", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "MONTEFRIO", DocumentTypeId = 1, Document = "11224587", ContactName = "MONTEFRIO", Address = "CRR 11 44-18", LandPhone = "47568256", CellPhone = "9513521", Email = "jUAÑA@GMAIL.COM", Remarks = "" });
                _context.Customers.Add(new Customer { Name = "DIEGO", DocumentTypeId = 1, Document = "50978164", ContactName = "DIEGO", Address = "CALLE 58 NO 54 APT 210", LandPhone = "254657924", CellPhone = "467144", Email = "diegoandresn218@yahoo.es", Remarks = "soltero" });
                _context.Customers.Add(new Customer { Name = "JULIETA", DocumentTypeId = 1, Document = "4547821", ContactName = "JULIETA", Address = "CALLE 45 NO 65 APT 401", LandPhone = "2564564", CellPhone = "48759644", Email = "julieta8@yahoo.es", Remarks = "casado" });
                _context.Customers.Add(new Customer { Name = "ALEJANDRO", DocumentTypeId = 1, Document = "64721054", ContactName = "ALEJANDRO", Address = "CALLE 25 NO 45 APT 410", LandPhone = "2525424", CellPhone = "46544", Email = "alejo18@yahoo.es", Remarks = "soltero" });
                _context.Customers.Add(new Customer { Name = "ALEXANDRA", DocumentTypeId = 1, Document = "100024785", ContactName = "ALEXANDRA", Address = "CALLE 80 NO 65 APT 410", LandPhone = "254887424", CellPhone = "4597144", Email = "alexandra@yahoo.es", Remarks = "casado" });
                _context.Customers.Add(new Customer { Name = "EL ENCANTO", DocumentTypeId = 1, Document = "952307745", ContactName = "EL ENCANTO", Address = "Cra 83 # 54 - 16", LandPhone = "3664555", CellPhone = "32144565578", Email = "omarMon@Gmail.es", Remarks = "don omar es mejor que jaime" });
                _context.Customers.Add(new Customer { Name = "LOS BILLARES DE PACHO", DocumentTypeId = 1, Document = "10626327542", ContactName = "LOS BILLARES DE PACHO", Address = "Cra 81 # 56 - 06", LandPhone = "25478887", CellPhone = "3206569515", Email = "Gulle_Garden@Hotmail.com", Remarks = "bola 8 te atiende" });
                _context.Customers.Add(new Customer { Name = "MERCAYOLANDA", DocumentTypeId = 1, Document = "962604547", ContactName = "MERCAYOLANDA", Address = "Cra 80 # 54", LandPhone = "45566877", CellPhone = "3186521471", Email = "Beatriz@Merca.es", Remarks = "todos sus productos en un mismo sitio" });
                _context.Customers.Add(new Customer { Name = "LA EMPANADA DE MI ESPOSA", DocumentTypeId = 1, Document = "26075458", ContactName = "LA EMPANADA DE MI ESPOSA", Address = "Circular 1 # 46 - 21", LandPhone = "8955214", CellPhone = "2564115", Email = "Francisco@elHombre.es", Remarks = "empanada no hay igual" });
            }
            await _context.SaveChangesAsync();
        }


        //----------------------------------------------------------------------------------------------
        private async Task CheckSuppliersAsync()
        {
            if (!_context.Suppliers.Any())
            {
                _context.Suppliers.Add(new Supplier { Name = "Luis Núñez", DocumentTypeId = 1, Document = "17157729", ContactName = "Luis Núñez", Address = "Espora 2052 B° Rosedal-Córdoba", LandPhone = "4659552", CellPhone = "156814963", Email = "luis@yopmail.com", Remarks = "Hola y chauuuuuuuuuuuuu" });
                _context.Suppliers.Add(new Supplier { Name = "Peugeot", DocumentTypeId = 2, Document = "123123123", ContactName = "a", Address = "Flores 444 Bulnes La Pampa", LandPhone = "5666777", CellPhone = "155444777", Email = "omar@yopmail.com", Remarks = "El Capitan" });
                _context.Suppliers.Add(new Supplier { Name = "Fiat", DocumentTypeId = 3, Document = "666 666 666", ContactName = "Juan Carlos Scarafia", Address = "Bulnes 666 Flores BsAs", LandPhone = "11 5555 6666", CellPhone = "11 6666 5555", Email = "juanca@yopmail.com", Remarks = "656656" });
                _context.Suppliers.Add(new Supplier { Name = "ELVIS MIELES", DocumentTypeId = 1, Document = "4561521", ContactName = "ELVIS ELVIS M MIELES", Address = "Calle 45#56 56", LandPhone = "45654", CellPhone = "45654", Email = "elvismieles@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "ORLEDIS MIELES", DocumentTypeId = 1, Document = "451", ContactName = "ORLEDIS OLREDISMIELES", Address = "Calle 45#56 56", LandPhone = "654", CellPhone = "654", Email = "ORLEDISmieles@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "LIVIS MIELES", DocumentTypeId = 1, Document = "521", ContactName = "LIVIS LIVISMIELES", Address = "Calle 45#56 56", LandPhone = "46554", CellPhone = "46554", Email = "Livismieles@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "NAVELIS MIELES", DocumentTypeId = 1, Document = "4521", ContactName = "NAVELIS NAVELISMIELES", Address = "Calle 45#56 56", LandPhone = "454544654", CellPhone = "454544654", Email = "Navelismieles@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "EDALI MIELES", DocumentTypeId = 1, Document = "45521", ContactName = "EDALI EDALIMIELES", Address = "Calle 45#56 56", LandPhone = "45445654", CellPhone = "45445654", Email = "EdaliMadremieles@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "OLVIS MIELES", DocumentTypeId = 1, Document = "45261", ContactName = "OLVIS OLVISMIELES", Address = "Calle 45#56 56", LandPhone = "96335654", CellPhone = "96335654", Email = "Olvismieles@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "MAIRA MIELES", DocumentTypeId = 1, Document = "45621", ContactName = "MAIRA 5425MIELES", Address = "Calle 45#56 56", LandPhone = "42302245654", CellPhone = "42302245654", Email = "Mairasmieles@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "MECHIS MIELES", DocumentTypeId = 1, Document = "455521", ContactName = "MECHIS RHHMIELES", Address = "Calle 45#56 56", LandPhone = "9632154", CellPhone = "9632154", Email = "Mechismieles@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "YURANI MIELES", DocumentTypeId = 1, Document = "49521", ContactName = "YURA 444MIELES", Address = "Calle 45#56 56", LandPhone = "46545452444", CellPhone = "46545452444", Email = "yuramieles@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "MABEL MIELES", DocumentTypeId = 1, Document = "459521", ContactName = "MABEL MIELES44", Address = "Calle 45#56 56", LandPhone = "46552211", CellPhone = "46552211", Email = "Mabelmieles@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "POMONA", DocumentTypeId = 1, Document = "4562", ContactName = "ROCIO PESOS", Address = "calle 86 # 88 53", LandPhone = "4526892", CellPhone = "4526892", Email = "pomona@yahoo.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CARREFULL", DocumentTypeId = 1, Document = "54656", ContactName = "JORGE BETANCUR", Address = "calle 48 # 59 3", LandPhone = "7893256", CellPhone = "7893256", Email = "carrefull@yahoo.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "PUNTO", DocumentTypeId = 3, Document = "48454", ContactName = "BENJAMIN VALLEJO", Address = "calle 69 # 82 52", LandPhone = "1236598", CellPhone = "1236598", Email = "punto@yahoo.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "VETER", DocumentTypeId = 4, Document = "45486859", ContactName = "ANDREA CIELO", Address = "calle 56 # 82 64", LandPhone = "7851643", CellPhone = "7851643", Email = "veter@yahoo.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "EXITO", DocumentTypeId = 3, Document = "789653145", ContactName = "ISABEL SANCHEZ", Address = "calle 75 # 82 53", LandPhone = "5702638", CellPhone = "5702638", Email = "mechas@yahoo.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CARULLA", DocumentTypeId = 4, Document = "79565635", ContactName = "CARMEN MILLONES", Address = "calle 65 # 82 53", LandPhone = "7896542", CellPhone = "7896542", Email = "carulla@yahoo.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "NEXTOP", DocumentTypeId = 1, Document = "54859865", ContactName = "JOSHEP MIEL", Address = "calle 77 # 82 58", LandPhone = "1235648", CellPhone = "1235648", Email = "nextop@yahoo.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CARLOS", DocumentTypeId = 3, Document = "97010413452", ContactName = "GONZALO JARAMILLO", Address = "cll 52 # 12 b 42", LandPhone = "2969636", CellPhone = "2969636", Email = "jaramillo@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "DANIEL", DocumentTypeId = 3, Document = "980215652", ContactName = "DANIEL LOPEZ", Address = "cll 55 # 52 b 52", LandPhone = "2963636", CellPhone = "2963636", Email = "lopez@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "SEBASTIAN", DocumentTypeId = 3, Document = "96501041365", ContactName = "SEBASTIAN BERRIO", Address = "cll 58 # 112 b 22", LandPhone = "2985634", CellPhone = "2985634", Email = "sebastian@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "ESTEBAN", DocumentTypeId = 3, Document = "120065652", ContactName = "ESTABAN CORREA", Address = "cll 12 # 32 b 72", LandPhone = "5896352", CellPhone = "5896352", Email = "estaban@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CRISTINA", DocumentTypeId = 3, Document = "32601046552", ContactName = "CRISTINA GOMEZ", Address = "cll 02 # 62 b 72", LandPhone = "5296356", CellPhone = "5296356", Email = "cristina@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "DANIELA", DocumentTypeId = 3, Document = "54011543452", ContactName = "DANIELA TORRES", Address = "cll 62 # 22 b 02", LandPhone = "2364512", CellPhone = "2364512", Email = "daniela@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "SANDRA", DocumentTypeId = 3, Document = "6571054", ContactName = "SANDRA MARTINEZ", Address = "cll 012 # 12 b 32", LandPhone = "2563695", CellPhone = "2563695", Email = "sandra@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "MARIA", DocumentTypeId = 3, Document = "447010465", ContactName = "MARIA CELIS", Address = "cll 53 # 20 b 10", LandPhone = "5632587", CellPhone = "5632587", Email = "maria@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "BEATRIZ", DocumentTypeId = 3, Document = "656010465", ContactName = "BEATRIZ VALENCIA", Address = "cll 10 # 33 b 09", LandPhone = "2928179", CellPhone = "2928179", Email = "beatriz@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "MANUELA", DocumentTypeId = 3, Document = "9860100000000", ContactName = "MANUELA MORALES", Address = "cll 68 # 78 b 12", LandPhone = "2653698", CellPhone = "2653698", Email = "manuela@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "SANTY", DocumentTypeId = 1, Document = "96101114969", ContactName = "PEPITO PEREZ", Address = "calle32 #54 18", LandPhone = "6845131", CellPhone = "6845131", Email = "Pepe@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "HECTOR", DocumentTypeId = 1, Document = "6456151", ContactName = "HECTORSITO GOMEZ", Address = "calle52 #65 96", LandPhone = "4474177", CellPhone = "4474177", Email = "Hector@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "PABLO", DocumentTypeId = 1, Document = "56415163", ContactName = "PABLITO HERNANDO VALENCIA", Address = "calle51 #10 98", LandPhone = "7523453", CellPhone = "7523453", Email = "Pablito@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "ESTELLA", DocumentTypeId = 1, Document = "5468541", ContactName = "ESTELLITA CARVAJAL", Address = "calle78 #96 18", LandPhone = "45254271", CellPhone = "45254271", Email = "Estelita@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "JINX", DocumentTypeId = 1, Document = "8946151", ContactName = "JINXELIADORA DUQUE", Address = "calle89 #75 115", LandPhone = "86451", CellPhone = "86451", Email = "Jinx@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "LUCIAN", DocumentTypeId = 1, Document = "561354", ContactName = "LUCHO VELASQUEZ", Address = "calle99 #87 92", LandPhone = "5423453", CellPhone = "5423453", Email = "Luchito@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "DRAVEN", DocumentTypeId = 1, Document = "86421654", ContactName = "DRAVEN ARTURO GOMEZ", Address = "calle71 #54 18", LandPhone = "57641561", CellPhone = "57641561", Email = "Creido@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "EVELYN", DocumentTypeId = 1, Document = "45274534", ContactName = "EVEE ARANGO", Address = "calle98 #10 1558", LandPhone = "8752", CellPhone = "8752", Email = "IVYYYY@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CHOGATH", DocumentTypeId = 1, Document = "86453453", ContactName = "CHO GATH CANGREJO", Address = "calle12 #96 74", LandPhone = "453543453", CellPhone = "453543453", Email = "Chogi@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "FELO", DocumentTypeId = 1, Document = "453453453", ContactName = "FELIPE ANTONIO DUQUE", Address = "carrera48 #96 82", LandPhone = "345234", CellPhone = "345234", Email = "felicho@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "MARCELO GOMEZ", DocumentTypeId = 3, Document = "141415", ContactName = "MARCELO CESAN", Address = "Diagonal 14 # 33 38", LandPhone = "2569856", CellPhone = "2569856", Email = "elmatematico@live.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "ANGELA PINEDA", DocumentTypeId = 1, Document = "23702024", ContactName = "ANGEL GONER", Address = "Calle 25 # 33 14", LandPhone = "4568923", CellPhone = "4568923", Email = "marranazo21@live.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "FRANCISCO PELON", DocumentTypeId = 4, Document = "2860634", ContactName = "ANTONIO BANDERAS", Address = "Carreara 10 # 14 14", LandPhone = "7895622", CellPhone = "7895622", Email = "serna@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "SANDRA SARMIENTO", DocumentTypeId = 1, Document = "86890", ContactName = "FRANCO HONESTO", Address = "Diagonal 22 # 85 90", LandPhone = "4525566", CellPhone = "4525566", Email = "sanfer@live.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "FRANCIA YUCA", DocumentTypeId = 4, Document = "3043", ContactName = "JULIANA SERNA", Address = "Diagonal 78 # 99 103", LandPhone = "4236566", CellPhone = "4236566", Email = "retro@live.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "DAGOBERTO GONSALEZ", DocumentTypeId = 4, Document = "870654", ContactName = "MARTA PANIAGUA", Address = "Diagonal 20 # 22 01", LandPhone = "2642018", CellPhone = "2642018", Email = "retifdg@live.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CAMILO ANDES", DocumentTypeId = 4, Document = "74868406", ContactName = "MARIA POSADA", Address = "Calle 47 # 78 85", LandPhone = "2642121", CellPhone = "2642121", Email = "lehaco@live.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "SANTIAGO TUTE", DocumentTypeId = 4, Document = "37064", ContactName = "ANTONIA RODRIGUEZ", Address = "Carrera 21 # 21 88", LandPhone = "4523333", CellPhone = "4523333", Email = "regno@live.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "DANILO", DocumentTypeId = 1, Document = "35789210", ContactName = "DIOSA GUTIERREZ", Address = "Calle 123 # 54-89", LandPhone = "5281945", CellPhone = "5281945", Email = "Diosa@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "MEZA", DocumentTypeId = 1, Document = "35789312", ContactName = "ALEJANDRA MEZA", Address = "Calle 123 # 54-89", LandPhone = "5289854", CellPhone = "5289854", Email = "mesa@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "ANTONIA", DocumentTypeId = 1, Document = "35786542", ContactName = "MARTHA GUTIERREZ", Address = "Calle 123 # 54-89", LandPhone = "5288451", CellPhone = "5288451", Email = "antonia@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CARLA", DocumentTypeId = 1, Document = "35789854", ContactName = "LEYDI ESPINOSA", Address = "Calle 123 # 54-89", LandPhone = "5281919", CellPhone = "5281919", Email = "carla@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "IVVONE", DocumentTypeId = 1, Document = "35788542", ContactName = "DIOSA GUTIERREZ", Address = "Calle 123 # 54-89", LandPhone = "5281936", CellPhone = "5281936", Email = "ivonne@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "SANTIAGO", DocumentTypeId = 1, Document = "35789520", ContactName = "ANDRES DAVID", Address = "Calle 123 # 54-89", LandPhone = "5281984", CellPhone = "5281984", Email = "santiago@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "BRAYAN", DocumentTypeId = 1, Document = "35787812", ContactName = "DANIELA RENTERIA", Address = "Calle 123 # 54-89", LandPhone = "5281935", CellPhone = "5281935", Email = "brayan@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "ELVIS", DocumentTypeId = 1, Document = "35782058", ContactName = "FELIPE VELEZ", Address = "Calle 123 # 54-89", LandPhone = "5281984", CellPhone = "5281984", Email = "elvis@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "LEIDY", DocumentTypeId = 1, Document = "35789654", ContactName = "MILENA DUQUE", Address = "Calle 123 # 54-89", LandPhone = "5281912", CellPhone = "5281912", Email = "Leidy@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "MATEO", DocumentTypeId = 1, Document = "695454", ContactName = "MATIAS BENITEZ", Address = "AV. 20 DE NOVIEMBRE NO 1053", LandPhone = "913367387", CellPhone = "913367387", Email = "mateo@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "GORGEL", DocumentTypeId = 1, Document = "86092", ContactName = "GORGITO VERGARA", Address = "AV. INDEPENDENCIA NO. 545", LandPhone = "913367384", CellPhone = "913367384", Email = "gorgel@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "VERONICA", DocumentTypeId = 1, Document = "10411975467", ContactName = "VERO VELEZ", Address = "AV. INDEPENDENCIA NO. 1282-A", LandPhone = "913367383", CellPhone = "913367383", Email = "veronicao@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "TABO", DocumentTypeId = 1, Document = "87061372448", ContactName = "GUSTABO OCAMPO", Address = "AV.INDEPENDENCIA NO.1010", LandPhone = "913367379", CellPhone = "913367379", Email = "tabo@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "ANGEL", DocumentTypeId = 1, Document = "86032631756", ContactName = "JUANITO CRUZ", Address = "AV. 5 DE MAYO NO. 1652", LandPhone = "913367374", CellPhone = "913367374", Email = "angel@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "SERTGIO", DocumentTypeId = 1, Document = "88022563216", ContactName = "TATO MARTINEZ", Address = "AV. INDEPENDENCIA NO. 748", LandPhone = "913367387", CellPhone = "913367387", Email = "sergio@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "JUAN", DocumentTypeId = 1, Document = "11820095", ContactName = "JUAN PABLO MOSQUERA CORDOBA", Address = "Calle 59b #98c 07", LandPhone = "2245152", CellPhone = "2245152", Email = "Juapa@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "SNEIDER", DocumentTypeId = 1, Document = "12698412", ContactName = "SNEIDER DAVID VILLA", Address = "Calle 77 #98c 26", LandPhone = "2365152", CellPhone = "2365152", Email = "Sneider@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "DIEGO", DocumentTypeId = 1, Document = "214568454", ContactName = "DIEGO MAURICIO CARDONA CARDONA", Address = "Calle 78 #18c 12", LandPhone = "3121069", CellPhone = "3121069", Email = "DiegoGo@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "KATHE", DocumentTypeId = 1, Document = "96050203640", ContactName = "KATHERINE ORTIZ DUARTE", Address = "Calle 25b #98 45", LandPhone = "3121067", CellPhone = "3121067", Email = "Kathe@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "NACY", DocumentTypeId = 1, Document = "15165641", ContactName = "NANCY LUCIA GOMES", Address = "Calle 69b #15 07", LandPhone = "4444846", CellPhone = "4444846", Email = "Gordis@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "SANDRITA", DocumentTypeId = 1, Document = "12456854", ContactName = "SANDRA MOSQUERA", Address = "Calle 49b #95 47", LandPhone = "2245152", CellPhone = "2245152", Email = "sandra@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "SERGIO", DocumentTypeId = 1, Document = "155544662", ContactName = "SERGIO ANTONIO CARO", Address = "Calle 20b #98c 01", LandPhone = "2022645", CellPhone = "2022645", Email = "Sergio@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "ALBERTO", DocumentTypeId = 1, Document = "119805698", ContactName = "ALBERTO CONDOR", Address = "Calle 103b #98c 07", LandPhone = "2456898", CellPhone = "2456898", Email = "alber@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "HOMERO", DocumentTypeId = 1, Document = "546542341", ContactName = "HOMERO SIMPSON", Address = "Calle siempre viva 123", LandPhone = "2445663", CellPhone = "2445663", Email = "Homo@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "JOSÉ", DocumentTypeId = 1, Document = "4343290345", ContactName = "JOSÉ VELEZ", Address = "Calle 30 #28 90", LandPhone = "5638492", CellPhone = "5638492", Email = "jose@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "DESMER", DocumentTypeId = 1, Document = "654563454", ContactName = "DESMER YEPES", Address = "Calle 30 #28 90", LandPhone = "5638492", CellPhone = "5638492", Email = "des@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "ARMANDO", DocumentTypeId = 1, Document = "83409656", ContactName = "ARMANDO CARDENAS", Address = "Calle 40 #28 91", LandPhone = "7845564", CellPhone = "7845564", Email = "armando@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "GERARDO", DocumentTypeId = 1, Document = "654363454", ContactName = "GERARDO DAVID", Address = "Calle 70 #89 90", LandPhone = "5647389", CellPhone = "5647389", Email = "gerd@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "DAMÍN", DocumentTypeId = 1, Document = "63634645", ContactName = "DAMÍN CARDONA", Address = "Calle 87 #29 93", LandPhone = "9780463", CellPhone = "9780463", Email = "damin@outlook.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "DUVÁN", DocumentTypeId = 1, Document = "343296435", ContactName = "DUVÁN PATIÑO", Address = "Calle 98 #2 54", LandPhone = "4609826", CellPhone = "4609826", Email = "duvan@yahoo.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "FRANCISCO", DocumentTypeId = 1, Document = "634534534", ContactName = "FRANCISCO RENTERÍA", Address = "Calle 654 #18 07", LandPhone = "452893", CellPhone = "452893", Email = "francis@yahoo.es", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "JHOAN", DocumentTypeId = 1, Document = "456346537", ContactName = "JHOAN RUIZ", Address = "Calle 08 #28 70", LandPhone = "1923475", CellPhone = "1923475", Email = "jhoan@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "TOTTO", DocumentTypeId = 1, Document = "788848633", ContactName = "ANDRÉS ZAPATA", Address = "calle 49 # 99 E 86", LandPhone = "4906895", CellPhone = "4906895", Email = "andres.11996@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "MONTERREY", DocumentTypeId = 1, Document = "435654", ContactName = "CRISTINA RUIZ", Address = "calle 34 # 68 E 98 interior 204", LandPhone = "49065433", CellPhone = "49065433", Email = "cris@yahoo.es", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "ALMACENES SUSCESO", DocumentTypeId = 1, Document = "35676544", ContactName = "JUAN RIOS", Address = "calle 65 # 67 E 88", LandPhone = "4906543", CellPhone = "4906543", Email = "juan@gmail.es", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "LICARESDE ANTIOQUIA", DocumentTypeId = 1, Document = "4564333", ContactName = "ORLANDO RIOS", Address = "calle 78 # 65  97", LandPhone = "49065533", CellPhone = "49065533", Email = "ricapalma@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "BABARIA", DocumentTypeId = 1, Document = "45665433", ContactName = "CAMILO VALENCIA", Address = "calle 54 # 78 AA 86", LandPhone = "490556", CellPhone = "490556", Email = "valencia@yahoo.es", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "BEBIDAS RAMAHAT", DocumentTypeId = 1, Document = "1036659420", ContactName = "SEBASTIAN VALLEJO VILLA", Address = "Cra 54 D # 10 - 18", LandPhone = "2550007", CellPhone = "2550007", Email = "sebasv511@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "COCOS JUANMA", DocumentTypeId = 1, Document = "96458410", ContactName = "JUAN MANUEL SANTOS CALDERON", Address = "cll 23# 43 - 10", LandPhone = "3645211", CellPhone = "3645211", Email = "presidente-Chuki@yahoo.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "PAPAS ZULU", DocumentTypeId = 1, Document = "74528621", ContactName = "JUAN CARLOS ZULUAGA CARDONA", Address = "cra 56# 78-45", LandPhone = "3654122", CellPhone = "3654122", Email = "juanzuluaga@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CHONTADURO SA", DocumentTypeId = 1, Document = "1036452177", ContactName = "ARNULIO RAMIREZ GUISAO", Address = "cra 34 B # 66 - 89", LandPhone = "2364522", CellPhone = "2364522", Email = "RasTasTas@hotmail.es", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "SALCHICHONCHON", DocumentTypeId = 1, Document = "1035478210", ContactName = "DAVID FERNANDO ESCOBAR GAVIRIA", Address = "cra 45# 77-12", LandPhone = "2557410", CellPhone = "2557410", Email = "salchi-chon@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "AUTOPARTES EL PETIRROJO", DocumentTypeId = 1, Document = "1036659411", ContactName = "ANDRES ZAPATA ZAPATA", Address = "av. 15# 12-89", LandPhone = "3614788", CellPhone = "3614788", Email = "andrespartes@hotmail.es", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CHOCHIS TOCINOS", DocumentTypeId = 1, Document = "1036542210", ContactName = "CAMILO RIOS TORRES", Address = "cra33# 98-07", LandPhone = "3697844", CellPhone = "3697844", Email = "chochis-cr10@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "COSMETICOS LA JUANDA", DocumentTypeId = 1, Document = "1216717716", ContactName = "JUAN DAVID QUINTERO HERNANDEZ", Address = "cra 34# 23-12", LandPhone = "3254788", CellPhone = "3254788", Email = "elNegritoJuanda@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "LICORES JF", DocumentTypeId = 1, Document = "1036659842", ContactName = "SEBASTIAN OROZCO MENDEZ", Address = "cll 25# 78 - 101", LandPhone = "2558944", CellPhone = "2558944", Email = "sebasTatabro@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "PANADERIA BERRIO", DocumentTypeId = 1, Document = "1032548852", ContactName = "CARLOS ALBERTO BERRIO BERMUDEZ", Address = "cra 67# 54-33", LandPhone = "2584410", CellPhone = "2584410", Email = "panBerrio@hotmail.es", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "DIRECTV", DocumentTypeId = 3, Document = "486845328", ContactName = "SANTIAGO UPEGUI", Address = "Carrera 54 # 45 54", LandPhone = "5542245", CellPhone = "5542245", Email = "santiago@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "GASEOSAS DE OCCIDENTE", DocumentTypeId = 4, Document = "484444444", ContactName = "DANIEL ORREGO", Address = "Carrera 55 # 56 42", LandPhone = "5534444", CellPhone = "5534444", Email = "daniel@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "TELEFONIA MAOVISTAR", DocumentTypeId = 4, Document = "484845552", ContactName = "CRISTIAN TORO", Address = "Carrera 75 # 97 58", LandPhone = "2656644", CellPhone = "2656644", Email = "cristian@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CLARO", DocumentTypeId = 1, Document = "487554552", ContactName = "ANDRES GONZALEZ", Address = "Carrera 54 # 45 57", LandPhone = "2745244", CellPhone = "2745244", Email = "andres@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "EPM", DocumentTypeId = 1, Document = "847885552", ContactName = "DARIO CARO", Address = "Carrera 78 # 56 55", LandPhone = "267456", CellPhone = "267456", Email = "dario@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "COCA COLA", DocumentTypeId = 4, Document = "897771225", ContactName = "FELIPE CIFUENTES", Address = "Carrera 87 # 74 54", LandPhone = "214425", CellPhone = "214425", Email = "felipe@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "ADIDAS", DocumentTypeId = 1, Document = "94782593", ContactName = "JULIANA TELOUNDO", Address = "CALLE 35 # 98-06", LandPhone = "9805786", CellPhone = "9805786", Email = "TELOUNDOJULIANA@GMAIL.COM", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "P & G", DocumentTypeId = 1, Document = "97573954", ContactName = "EDGAR GAJO", Address = "CALLE 79 # 67-129", LandPhone = "67400753", CellPhone = "67400753", Email = "EDGAR@HOTMAIL.COM", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "JOHNSON & JOHNSON", DocumentTypeId = 1, Document = "4855465463", ContactName = "ELVIO LADO", Address = "CALLE 87 # 93 -20", LandPhone = "3364656", CellPhone = "3364656", Email = "PEREZFELIPE@GMAIL.COM", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "PARRANDA EN EL CAFETAL", DocumentTypeId = 1, Document = "906545788", ContactName = "JORGE CELEDON", Address = "Circular 90 con la 33B", LandPhone = "3046665", CellPhone = "3046665", Email = "Jorge@Cafetal.es", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "PANADERÍA JUANA DE ARCO", DocumentTypeId = 1, Document = "30254801", ContactName = "JUANA LOZANO", Address = "Cl 29A  95-59", LandPhone = "2314568", CellPhone = "2314568", Email = "Juana@delArco.es", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "MILOS DE LUCHO", DocumentTypeId = 1, Document = "25449556", ContactName = "LUIS FABIAN", Address = "Cl 32B # 10 - 65", LandPhone = "23458874", CellPhone = "23458874", Email = "LuisF@MilosL.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "PLAZA MAYORITATIA", DocumentTypeId = 1, Document = "24121994", ContactName = "PABLO LEON JARAMILLO", Address = "Cra 82 N° 26A", LandPhone = "2331145", CellPhone = "2331145", Email = "Pablo@leonj.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "LICORES", DocumentTypeId = 1, Document = "146489564", ContactName = "MANUELA PADIERNA", Address = "Calle 69b #78c 06", LandPhone = "2342202", CellPhone = "2342202", Email = "Manu@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "PAPELERIA", DocumentTypeId = 1, Document = "112555445", ContactName = "JEAN CARLOS MOSQUERA", Address = "Calle 11 #96 26", LandPhone = "2455152", CellPhone = "2455152", Email = "jean@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CHOCOLATES", DocumentTypeId = 1, Document = "216984454", ContactName = "LISA SIMPSON", Address = "Calle 46 #15a 12", LandPhone = "3129865", CellPhone = "3129865", Email = "lisa@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "NOEL", DocumentTypeId = 1, Document = "94523640", ContactName = "LUISA FERNADA AGUDELO", Address = "Calle 68b #49c 45", LandPhone = "3121867", CellPhone = "3121867", Email = "Luisa@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "VERDURAS", DocumentTypeId = 1, Document = "16489464", ContactName = "OLGA PEREZ", Address = "Calle 45a #15b 19", LandPhone = "4698846", CellPhone = "4698846", Email = "Verduras@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "PANADERIA", DocumentTypeId = 1, Document = "12479854", ContactName = "DIDIER VERA", Address = "Calle 56a #05 47", LandPhone = "2356152", CellPhone = "2356152", Email = "Didier@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CIGARRILLOS", DocumentTypeId = 1, Document = "119554698", ContactName = "JULINA PATIÑO", Address = "Calle 106a #98b 07", LandPhone = "2696898", CellPhone = "2696898", Email = "juli@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "RELOJES", DocumentTypeId = 1, Document = "54652345", ContactName = "MARI MISAKI", Address = "Calle 98b #07 123", LandPhone = "2455663", CellPhone = "2455663", Email = "maria@hotmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "KAREN", DocumentTypeId = 1, Document = "32545681", ContactName = "CARDONA LOPES", Address = "Calle 123 # 54-89", LandPhone = "2281945", CellPhone = "2281945", Email = "Karen@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "SAN PACHO FIESTERO", DocumentTypeId = 1, Document = "1524455649", ContactName = "JULIAN MONTLLA", Address = "Cl56 # 45 a 65", LandPhone = "3422156", CellPhone = "3422156", Email = "Sanpacho20@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "JOE MIX", DocumentTypeId = 1, Document = "6543516", ContactName = "EDUARDO PADIERMA", Address = "Cl80 # 35 a 25", LandPhone = "3022012", CellPhone = "3022012", Email = "JoeMix@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "PIZZA DOBLE PIZZA", DocumentTypeId = 1, Document = "1524566908", ContactName = "SANTIAGO MORENO", Address = "Cl80 # 63 - 95", LandPhone = "3002532", CellPhone = "3002532", Email = "PizzaDoblePizza@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "POLLOS MARIO", DocumentTypeId = 1, Document = "4513646", ContactName = "SNAIDER HERNADEZ", Address = "Cl70 # 70 a 85", LandPhone = "1356235", CellPhone = "1356235", Email = "PollosMario@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "COME PUES", DocumentTypeId = 1, Document = "2554136431", ContactName = "FELIPE ZALASAR", Address = "Cl70 # 75 a 80", LandPhone = "5616463", CellPhone = "5616463", Email = "ComePues@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CHORICERIA", DocumentTypeId = 1, Document = "4962294", ContactName = "SANIER MONSALVE", Address = "Cl60 # 65 a 70", LandPhone = "6249620", CellPhone = "6249620", Email = "Choriceria@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "VASO DE LECHE", DocumentTypeId = 1, Document = "1202567813", ContactName = "SARA LOPERA", Address = "Cl36 # 45 a 60", LandPhone = "2645891", CellPhone = "2645891", Email = "Vaso de leche@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "LOS CUATES", DocumentTypeId = 1, Document = "3145679021", ContactName = "DANIELA CEBALLOS", Address = "Cl80 #  75 a 56", LandPhone = "8462103", CellPhone = "8462103", Email = "LosCuates@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "EL PLANTIO", DocumentTypeId = 1, Document = "1546327", ContactName = "CRISTINA MOLINA", Address = "Cl45 #  50 a 60", LandPhone = "1264782", CellPhone = "1264782", Email = "ElPlantio@gmail.com", Remarks = "" });
                _context.Suppliers.Add(new Supplier { Name = "CARNELLY", DocumentTypeId = 1, Document = "1254307895", ContactName = "TOMASA FLORES", Address = "Cl35 #  55 a 40", LandPhone = "3642015", CellPhone = "3642015", Email = "Carnelly@gmail.com", Remarks = "" });
            }
            await _context.SaveChangesAsync();
        }

        //----------------------------------------------------------------------------------------------
        private async Task CheckProductsAsync()
        {
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Product { Name = "Carne",CategoryId=1,IvaId=1, MeasureId = 1, Price =123456M,Remarks= "Esto es una prueba",Image="" ,Quantity=10, Category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == 1), Iva = await _context.Ivas.FirstOrDefaultAsync(x => x.Id == 1), Measure = await _context.Measures.FirstOrDefaultAsync(x => x.Id == 1), });
                _context.Products.Add(new Product { Name = "Asado", CategoryId = 1, IvaId = 2, MeasureId = 1, Price = 23456M, Remarks = "Otra prueba", Image = "",  Quantity = 20, Category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == 1), Iva = await _context.Ivas.FirstOrDefaultAsync(x => x.Id == 2), Measure = await _context.Measures.FirstOrDefaultAsync(x => x.Id == 1), });
                _context.Products.Add(new Product { Name = "Coca Cola", CategoryId = 2, IvaId = 3, MeasureId = 1, Price = 100M, Remarks = "Gaseosa", Image = "", Quantity = 5,Category=await _context.Categories.FirstOrDefaultAsync(x=>x.Id==2), Iva = await _context.Ivas.FirstOrDefaultAsync(x => x.Id == 3), Measure = await _context.Measures.FirstOrDefaultAsync(x => x.Id == 1),});
            }
            await _context.SaveChangesAsync();
        }
    }
}