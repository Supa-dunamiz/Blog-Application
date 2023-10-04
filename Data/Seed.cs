using IBlogWebApp.Data.Enum;
using IBlogWebApp.Models;
using Microsoft.AspNetCore.Identity;

namespace IBlogWebApp.Data
{
    public class Seed
    {
        public static void SeedData(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.BlogPosts.Any())
                {
                    context.BlogPosts.AddRange(new List<BlogPost>()
                    {
                        new BlogPost()
                        {
                            Title = "Techies",
                            Content = "This is the description of the blog",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            CreatedAt = DateTime.Now,
                            Author = "Dunamis Abaku",
                            BlogPostCategory = BlogPostCategory.Technology,
                            Comments = new List<Comment>()
                            {
                                new Comment()
                                {
                                    Author = "Senibo alova",
                                    Content = "Bullshit",
                                    CreatedAt = DateTime.Now,
                                },
                                new Comment()
                                {
                                    Author = "Senibo alova",
                                    Content = "Bullshit",
                                    CreatedAt = DateTime.Now,
                                }
                            }

                        },
                        new BlogPost()
                        {
                            Title = "Money",
                            Content = "This is the description of the Second",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            CreatedAt = DateTime.Now,
                            Author = "Dunamis Abaku",
                            BlogPostCategory = BlogPostCategory.Finance,
                            Comments = new List<Comment>()
                            {
                                new Comment()
                                {
                                    Author = "Senibo alova",
                                    Content = "Bullshit",
                                    CreatedAt = DateTime.Now,
                                },
                                new Comment()
                                {
                                    Author = "Senibo alova",
                                    Content = "Bullshit",
                                    CreatedAt = DateTime.Now,
                                }
                            }

                        },
                        new BlogPost()
                        {
                            Title = "OutFit",
                            Content = "This is the description the third blog",
                            Image = "https://www.eatthis.com/wp-content/uploads/sites/4/2020/05/running.jpg?quality=82&strip=1&resize=640%2C360",
                            CreatedAt = DateTime.Now,
                            Author = "Dunamis Abaku",
                            BlogPostCategory = BlogPostCategory.Fashion,
                            Comments = new List<Comment>()
                            {
                                new Comment()
                                {
                                    Author = "Senibo alova",
                                    Content = "Bullshit",
                                    CreatedAt = DateTime.Now,
                                },
                                new Comment()
                                {
                                    Author = "Senibo alova",
                                    Content = "Bullshit",
                                    CreatedAt = DateTime.Now,
                                }
                            }

                        }
                    });
                    context.SaveChanges();
                }
            }
        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string adminUserEmail = "abakugodpower@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        FirstName = "Dunamis",
                        LastName = "Benjamin",
                        State = "Lagos",
                        City = "Ikeja",
                        UserName = "Supa_Dunamis",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "user@etickets.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {

                        FirstName = "appUser",
                        LastName = "appUser",
                        State = "Rivers",
                        City = "Port-Harcourt",
                        UserName = "appUser",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}