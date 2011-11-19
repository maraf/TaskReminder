using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TaskReminder.Core.EntityFramework;
using TaskReminder.Core.Domain;

namespace TaskReminder.Web.Core
{
    public class DataContextInitializer : DropCreateDatabaseIfModelChanges<DataContext>
    {
        protected override void Seed(DataContext context)
        {
            #region Domény
            Domain domain;
            if (context.Domains.Count() == 0)
            {
                domain = new Domain
                {
                    Name = "localhost",
                    Url = "localhost"
                };
                context.Domains.Add(domain);
            }
            else
            {
                domain = context.Domains.FirstOrDefault(d => d.Url == "localhost");
            }
            #endregion

            #region Vlastnosti

            context.PropertyKeys.Add(new PropertyKey
            {
                Domain = domain,
                Name = "IČO",
                Target = "Company"
            });

            context.PropertyKeys.Add(new PropertyKey
            {
                Domain = domain,
                Name = "Email",
                Target = "Company"
            });

            #endregion

            #region Uživatelé
            if (context.Users.Count() == 0)
            {
                User admin = new User
                {
                    Username = "admin",
                    Password = "admin",
                    FirstName= "admin",
                    LastName = "admin",
                    Domain = domain,
                    Role = Roles.SuperAdmin
                };
                context.Users.Add(admin);

                User vedouci = new User
                {
                    Username = "vedouci",
                    Password = "1111",
                    FirstName = "Hlavní",
                    LastName = "Vedoucí",
                    Domain = domain,
                    Role = Roles.Manager
                };
                context.Users.Add(vedouci);

                context.Users.Add(new User
                {
                    Username = "pracovnik",
                    Password = "1112",
                    FirstName = "Běžný",
                    LastName = "Pracovník",
                    Domain = domain,
                    Role = Roles.Worker,
                    Boss = vedouci
                });

                context.Users.Add(new User
                {
                    Username = "ucetni",
                    FirstName = "Toto je",
                    LastName = "Účetní",
                    Domain = domain,
                    Role = Roles.BookKeeper,
                    Boss = vedouci
                });
            }
            #endregion

            #region Stavy požadavků
            context.TaskStates.Add(new TaskState
            {
                Name = "Nezadán",
                Flag = TaskStateFlag.Created,
                Domain = domain
            });
            context.TaskStates.Add(new TaskState
            {
                Name = "Zadán",
                Flag = TaskStateFlag.Assigned,
                Domain = domain
            });
            context.TaskStates.Add(new TaskState
            {
                Name = "Odmítnut",
                Flag = TaskStateFlag.Rejected,
                Domain = domain
            });
            context.TaskStates.Add(new TaskState
            {
                Name = "Zpracováván",
                Flag = TaskStateFlag.Handled,
                Domain = domain
            });
            context.TaskStates.Add(new TaskState
            {
                Name = "Dokončen",
                Flag = TaskStateFlag.Completed,
                Domain = domain
            });
            context.TaskStates.Add(new TaskState
            {
                Name = "Uzavřen",
                Flag = TaskStateFlag.Approved,
                Domain = domain
            });
            context.TaskStates.Add(new TaskState
            {
                Name = "Zaúčtován",
                Flag = TaskStateFlag.Archived,
                Domain = domain
            });
            #endregion

            #region Zákaznící
            Company company = new Company
            {
                Name = "Pepova společnost",
                Address = new Address
                {
                    Street = "Krátká",
                    HouseNumber = "1",
                    City = "Poholice",
                    PostalCode = "400 11"
                },
                Domain = domain
            };
            context.Companies.Add(company);

            context.Offices.Add(new Office
            {
                Name = "Hlavní obchod",
                Address = new Address
                {
                    Street = "Bretaňská",
                    HouseNumber = "5",
                    City = "Poholice",
                    PostalCode = "400 11"
                },
                Company = company
            });


            company = new Company
            {
                Name = "Alojzova společnost",
                Address = new Address
                {
                    Street = "Bretaňská",
                    HouseNumber = "5",
                    City = "Poholice",
                    PostalCode = "400 11"
                },
                Domain = domain
            };
            context.Companies.Add(company);

            context.Offices.Add(new Office
            {
                Name = "Hlavní obchod",
                Address = new Address
                {
                    Street = "Bretaňská",
                    HouseNumber = "5",
                    City = "Poholice",
                    PostalCode = "400 11"
                },
                Company = company
            });
            context.Offices.Add(new Office
            {
                Name = "Pobočka 01",
                Address = new Address
                {
                    Street = "Dlouhá",
                    HouseNumber = "110",
                    City = "Poholice",
                    PostalCode = "400 11"
                },
                Company = company
            });
            context.Offices.Add(new Office
            {
                Name = "Pobočka 02",
                Address = new Address
                {
                    Street = "Strašnická",
                    HouseNumber = "431",
                    City = "Poholice",
                    PostalCode = "400 11"
                },
                Company = company
            });
            context.Offices.Add(new Office
            {
                Name = "Pobočka 03",
                Address = new Address
                {
                    Street = "Strašecí",
                    HouseNumber = "53",
                    City = "Poholice",
                    PostalCode = "400 11"
                },
                Company = company
            });
            #endregion
        }
    }
}