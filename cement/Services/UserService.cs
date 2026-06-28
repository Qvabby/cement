using cement.Data;
using cement.Interfaces;
using cement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace cement.Services
{
    internal class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
       
        public UserService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private Random random = new Random();
        private List<string> names = new()
            {
    "Liam", "Olivia", "Noah", "Emma", "Oliver", "Charlotte", "Elijah", "Amelia", "James", "Sophia",
    "William", "Isabella", "Benjamin", "Mia", "Lucas", "Evelyn", "Henry", "Harper", "Alexander", "Camila",
    "Michael", "Gianna", "Daniel", "Abigail", "Matthew", "Luna", "Jackson", "Ella", "Sebastian", "Elizabeth",
    "David", "Sofia", "Joseph", "Emily", "Carter", "Avery", "Owen", "Mila", "Wyatt", "Scarlett",
    "John", "Eleanor", "Jack", "Madison", "Luke", "Layla", "Jayden", "Penelope", "Dylan", "Aria",
    "Grayson", "Chloe", "Levi", "Grace", "Isaac", "Ellie", "Gabriel", "Nora", "Julian", "Hazel",
    "Mateo", "Zoey", "Anthony", "Riley", "Jaxon", "Victoria", "Lincoln", "Lily", "Joshua", "Aurora",
    "Christopher", "Violet", "Andrew", "Nova", "Theodore", "Hannah", "Caleb", "Emilia", "Ryan", "Zoe",
    "Asher", "Stella", "Nathan", "Everly", "Thomas", "Isla", "Leo", "Leah", "Isaiah", "Lucy",
    "Charles", "Paisley", "Josiah", "Natalie", "Hudson", "Naomi", "Christian", "Eliana", "Hunter", "Brooklyn"
            };
        private List<string> usernames = new()
            {
    "ShadowFox", "PixelKnight", "SilentWolf", "BlueComet", "NeonTiger", "LunarDrift", "CrimsonByte", "EchoStorm", "DarkFalcon", "NovaBlaze",
    "CyberRaven", "IronPanda", "RapidViper", "GhostNinja", "StormWizard", "CosmicBear", "MysticFlame", "TurboPenguin", "SolarPirate", "FrostDragon",
    "QuantumCat", "StealthShark", "ElectricKoala", "FrozenPhoenix", "AlphaRocket", "NightCrawler", "EpicBadger", "SilverJaguar", "OmegaCobra", "GoldenOtter",
    "PixelSamurai", "RustyKnight", "AtomicDuck", "VelvetTiger", "BrokenArrow", "CloudRunner", "DizzyWizard", "TurboGoose", "MagicLobster", "BlueWarden",
    "CodePirate", "DreamHunter", "CyberMonk", "EmeraldWolf", "LoneRanger", "RapidComet", "SilentFury", "TinyTitan", "VoidWalker", "BrightFalcon",
    "DarkPigeon", "StormRider", "ElectricOtter", "HappyBadger", "RedVortex", "CrystalPanda", "GoldenWizard", "FrostByte", "PixelNomad", "NightPhoenix",
    "ShadowCrab", "SwiftTiger", "CosmicKnight", "LuckyFox", "EpicDragon", "IronFalcon", "RapidWolf", "GhostRider", "NeonSamurai", "BlueShadow",
    "SilverComet", "CyberTiger", "DarkNomad", "FrozenBear", "TurboKnight", "SolarWolf", "AtomicViper", "LunarPhoenix", "EchoNinja", "QuantumWizard",
    "MysticFalcon", "StormPanda", "TinyDragon", "VelvetFox", "AlphaBadger", "MagicTiger", "DreamViper", "GoldenRocket", "PixelWolf", "CodeSamurai",
    "ShadowByte", "CrimsonKnight", "NightTiger", "BrightPhoenix", "SwiftFalcon", "CloudFox", "IronDragon", "ElectricWizard", "NovaRider", "CosmicOtter"
            };

        public async Task<ServiceResponse<User>> AddUserAsync(User user)
        {
            try
            {
                
                if (user == null)
                    return await Task.FromResult(new ServiceResponse<User> { Data = null, Success = false, Description = "User is null" });
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(new ServiceResponse<User> { Data = user, Success = true, Description = "User added successfully" });
            }
            catch (Exception e)
            {
                return new ServiceResponse<User> { Data = null, Success = false, Description = e.Message };
            }
        }
        public async Task<ServiceResponse<List<User>>> GetUsersAsync()
        {
            try
            {
                var users = await _dbContext.Users.ToListAsync();
                return await Task.FromResult(new ServiceResponse<List<User>> { Data = users, Success = true, Description = "Users retrieved successfully" });
            }
            catch (Exception e)
            {

                return await Task.FromResult(new ServiceResponse<List<User>> { Data = null, Success = false, Description = e.Message });
            }
        }

        public async Task<ServiceResponse<List<User>>> CreateUsersAsync(int amount)
        {
            List<User> users = new List<User>();
            for (int i = 0; i < amount; i++)
            {
                int id = random.Next(1, 100000);
                string name = names[random.Next(names.Count)];
                string username = usernames[random.Next(usernames.Count)];
                users.Add(new User() { Id = id, Name = name, UserName = username, CreatedAt = DateTime.Now });
            }
            return await Task.FromResult(new ServiceResponse<List<User>> { Data = users, Success = true, Description = $"Successfully Created {amount} fake users" });
        }

        public async Task<ServiceResponse<User>> GetUserByNameAsync(string name, List<User> users)
            {
                List<User> usersWithUsername = users.Where(x => x.Name == name).ToList();
                if (usersWithUsername.Count == 0)
                {
                    return await Task.FromResult(new ServiceResponse<User> { Data = null, Success = false, Description = "User not found" });
                }
                else if (usersWithUsername.Count == 1)
                {
                    return await Task.FromResult(new ServiceResponse<User> { Data = usersWithUsername.FirstOrDefault(), Success = true, Description = "Multiple Users found but the first one was taken." });
                }
                else
                {
                    return await Task.FromResult(new ServiceResponse<User> { Data = usersWithUsername.FirstOrDefault(), Success = true, Description = "Multiple Users found but the first one was taken." });
                }
            }

        public async Task<ServiceResponse<string>> GetUsernameAsync(int userId)
            {
                if (userId == 1)
                {
                    return await Task.FromResult(new ServiceResponse<string> { Data = "John Doe", Success = true, Description = "User found", });
                }
                else
                {
                    return await Task.FromResult(new ServiceResponse<string> { Data = null, Success = false, Description = "User not found" });
                }
            }

        
    }
}

