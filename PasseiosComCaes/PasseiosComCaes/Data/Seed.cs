using Microsoft.AspNetCore.Identity;
using PasseiosComCaes.Data.Enum;
using PasseiosComCaes.Models;
using System.Net;

namespace PasseiosComCaes.Data
{
    public class Seed
    {
        //public static void SeedData(IApplicationBuilder applicationBuilder)
        //{
        //    using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
        //    {
        //        var context = serviceScope.ServiceProvider.GetService<AppDataContext>();

        //        context.Database.EnsureCreated();

        //        if (!context.Clubs.Any())
        //        {
        //            context.Clubs.AddRange(new List<Club>()
        //            {
        //                new Club()
        //                {
        //                    Nome = "Passeios Club ",
        //                    Imagem = "https://www.dogz.com.br/images/galeria/passeio01.jpg",
        //                    Descricao = "Este é o melhor club para passeios com cachorro",
        //                    TamanhoDoClub = TamanhoDoClub.Pequeno,
        //                    Endereco = new Endereco()
        //                    {
        //                        Bairro = "Centro",
        //                        Cidade = "São Paulo",
        //                        Estado = "SP",
        //                        Cep = "0000-000"
        //                    }
        //                 },
        //                new Club()
        //                {
        //                    Nome = "Passeios Club 1",
        //                    Imagem = "https://www.dogz.com.br/images/galeria/passeio01.jpg",
        //                    Descricao = "Este é o melhor club para passeios com cachorro 1",
        //                    TamanhoDoClub = TamanhoDoClub.Medio,
        //                    Endereco = new Endereco()
        //                    {
        //                        Bairro = "Centro",
        //                        Cidade = "Rio de Janeiro",
        //                        Estado = "RJ",
        //                        Cep = "0000-000"
        //                    }
        //                },
        //                new Club()
        //                {
        //                    Nome = "Passeios Club 2",
        //                    Imagem = "https://www.dogz.com.br/images/galeria/passeio01.jpg",
        //                    Descricao = "Este é o melhor club para passeios com cachorro 2",
        //                    TamanhoDoClub = TamanhoDoClub.Pequeno,
        //                    Endereco = new Endereco()
        //                    {
        //                        Bairro = "Centro",
        //                        Cidade = "Belo Horizonte",
        //                        Estado = "MG",
        //                        Cep = "0000-000"
        //                    }
        //                },
        //                new Club()
        //                {
        //                    Nome = "Passeios Club 3",
        //                    Imagem = "https://www.dogz.com.br/images/galeria/passeio01.jpg",
        //                    Descricao = "Este é o melhor club para passeios com cachorro 3",
        //                    TamanhoDoClub = TamanhoDoClub.Pequeno,
        //                    Endereco = new Endereco()
        //                    {
        //                        Bairro = "Centro",
        //                        Cidade = "FLorianpolis",
        //                        Estado = "SC",
        //                        Cep = "0000-000"
        //                    }
        //                }
        //            });
        //            context.SaveChanges();
        //        }
        //        Passeio
        //        if (!context.Passeios.Any())
        //        {
        //            context.Passeios.AddRange(new List<Passeio>()
        //            {
        //                new Passeio()
        //                {
        //                    Titulo = "Passear Sabado em grupo",
        //                    Imagem = "https://caocidadao.com.br/wp-content/uploads/2019/07/passeadores-de-caes-tudo-o-que-voce%CC%82-precisa-saber.jpg",
        //                    Descricao = "Realizar passeios com pitbulls",
        //                    Inicio = DateTime.Now,
        //                    Endereco = new Endereco()
        //                    {
        //                        Bairro = "Centro",
        //                        Cidade = "Curitiba",
        //                        Estado = "PR",
        //                        Cep = "0000-000"
        //                    }
        //                },
        //                new Passeio()
        //                {
        //                    Titulo = "Passear Sabado em grupo",
        //                    Imagem = "https://caocidadao.com.br/wp-content/uploads/2019/07/passeadores-de-caes-tudo-o-que-voce%CC%82-precisa-saber.jpg",
        //                    Descricao = "Realizar passeios com pitbulls",
        //                    Inicio = DateTime.Now,
        //                    Endereco = new Endereco()
        //                    {
        //                        Bairro = "Centro",
        //                        Cidade = "Florianopolis",
        //                        Estado = "SC",
        //                        Cep = "0000-000"
        //                    }
        //                }
        //            });
        //            context.SaveChanges();
        //        }
        //    }
        //}

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
                string adminUserEmail = "teste@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = "raicyhdev",
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Endereco = new Endereco()
                        {
                            Bairro = "Centro",
                            Cidade = "Rio de Janeiro",
                            Estado = "RJ",
                            Cep = "0000-000"
                        }
                    };
                    await userManager.CreateAsync(newAdminUser, "Teste@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }

                string appUserEmail = "teste@teste.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new AppUser()
                    {
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true,
                        Endereco = new Endereco()
                        {
                            Bairro = "Centro",
                            Cidade = "São Paulo",
                            Estado = "SP",
                            Cep = "0000-000"
                        }
                    };
                    await userManager.CreateAsync(newAppUser, "Teste@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
