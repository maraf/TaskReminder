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

            context.PropertyKeys.Add(new PropertyKey
            {
                Domain = domain,
                Name = "DIČ",
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
                    FirstName = "admin",
                    LastName = "admin",
                    Domain = domain,
                    Role = Roles.SuperAdmin
                };
                context.Users.Add(admin);

                User hfp = context.Users.Add(new User
                {
                    Username = "hfp",
                    Password = "ukolovnik",
                    FirstName = "HF",
                    LastName = "Protection",
                    Domain = domain,
                    Role = Roles.Admin
                });

                //User vedouci = context.Users.Add(new User
                //{
                //    Username = "vedouci",
                //    Password = "1111",
                //    FirstName = "Hlavní",
                //    LastName = "Vedoucí",
                //    Domain = domain,
                //    Role = Roles.Manager
                //});

                //User pracovnik = context.Users.Add(new User
                //{
                //    Username = "pracovnik",
                //    Password = "1112",
                //    FirstName = "Běžný",
                //    LastName = "Pracovník",
                //    Domain = domain,
                //    Role = Roles.Worker,
                //    Boss = vedouci
                //});

                //User ucetni = context.Users.Add(new User
                //{
                //    Username = "ucetni",
                //    Password = "1114",
                //    FirstName = "Paní",
                //    LastName = "Účetní",
                //    Domain = domain,
                //    Role = Roles.BookKeeper,
                //    Boss = vedouci
                //});
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

            //#region Zákaznící
            //Company company = new Company
            //{
            //    Name = "Pepova společnost",
            //    Address = new Address
            //    {
            //        Street = "Krátká",
            //        HouseNumber = "1",
            //        City = "Poholice",
            //        PostalCode = "400 11"
            //    },
            //    Domain = domain
            //};
            //context.Companies.Add(company);

            //context.Offices.Add(new Office
            //{
            //    Name = "Hlavní obchod",
            //    Address = new Address
            //    {
            //        Street = "Bretaňská",
            //        HouseNumber = "5",
            //        City = "Poholice",
            //        PostalCode = "400 11"
            //    },
            //    Company = company
            //});


            //company = new Company
            //{
            //    Name = "Alojzova společnost",
            //    Address = new Address
            //    {
            //        Street = "Bretaňská",
            //        HouseNumber = "5",
            //        City = "Poholice",
            //        PostalCode = "400 11"
            //    },
            //    Domain = domain
            //};
            //context.Companies.Add(company);

            //context.Offices.Add(new Office
            //{
            //    Name = "Hlavní obchod",
            //    Address = new Address
            //    {
            //        Street = "Bretaňská",
            //        HouseNumber = "5",
            //        City = "Poholice",
            //        PostalCode = "400 11"
            //    },
            //    Company = company
            //});
            //context.Offices.Add(new Office
            //{
            //    Name = "Pobočka 01",
            //    Address = new Address
            //    {
            //        Street = "Dlouhá",
            //        HouseNumber = "110",
            //        City = "Poholice",
            //        PostalCode = "400 11"
            //    },
            //    Company = company
            //});
            //context.Offices.Add(new Office
            //{
            //    Name = "Pobočka 02",
            //    Address = new Address
            //    {
            //        Street = "Strašnická",
            //        HouseNumber = "431",
            //        City = "Poholice",
            //        PostalCode = "400 11"
            //    },
            //    Company = company
            //});
            //context.Offices.Add(new Office
            //{
            //    Name = "Pobočka 03",
            //    Address = new Address
            //    {
            //        Street = "Strašecí",
            //        HouseNumber = "53",
            //        City = "Poholice",
            //        PostalCode = "400 11"
            //    },
            //    Company = company
            //});
            //context.SaveChanges();
            //#endregion

            //#region Úkoly
            //if (context.Tasks.Count() == 0)
            //{
            //    User admin = context.Users.First(u => u.Username == "admin");
            //    User hfp = context.Users.First(u => u.Username == "hfp");
            //    User vedouci = context.Users.First(u => u.Username == "vedouci");
            //    User pracovnik = context.Users.First(u => u.Username == "pracovnik");
            //    User ucetni = context.Users.First(u => u.Username == "ucetni");

            //    TaskState zadano = context.TaskStates.First(s => s.Flag == TaskStateFlag.Assigned);
            //    TaskState vytvoreno = context.TaskStates.First(s => s.Flag == TaskStateFlag.Created);

            //    Office hlObchodPepa = context.Offices.First(o => o.Name == "Hlavní obchod" && o.CompanyID == 1);
            //    Office hlObchodAlois = context.Offices.First(o => o.Name == "Hlavní obchod" && o.CompanyID == 2);

            //    context.Tasks.Add(new Task
            //    {
            //        Name = "Testovací úkol",
            //        Description = "Pouze testovací úkol bez velkého smyslu",
            //        Domain = domain,
            //        Created = DateTime.Now,
            //        CreatedBy = hfp,
            //        TaskState = zadano,
            //        Assigned = DateTime.Now,
            //        AssignedTo = vedouci,
            //        Office = hlObchodPepa,
            //        ToComplete = DateTime.Now.AddDays(2)
            //    });

            //    context.Tasks.Add(new Task
            //    {
            //        Name = "Testovací úkol 2",
            //        Description = "Pouze testovací úkol bez velkého smyslu",
            //        Domain = domain,
            //        Created = DateTime.Now,
            //        CreatedBy = hfp,
            //        TaskState = zadano,
            //        Assigned = DateTime.Now,
            //        AssignedTo = vedouci,
            //        Office = hlObchodPepa,
            //        ToComplete = DateTime.Now.AddDays(2)
            //    });

            //    context.Tasks.Add(new Task
            //    {
            //        Name = "Revize vedení",
            //        Description = "...",
            //        Domain = domain,
            //        Created = DateTime.Now,
            //        CreatedBy = hfp,
            //        TaskState = zadano,
            //        Assigned = DateTime.Now,
            //        AssignedTo = vedouci,
            //        Office = hlObchodPepa,
            //        ToComplete = DateTime.Now.AddDays(2)
            //    });

            //    context.Tasks.Add(new Task
            //    {
            //        Name = "Jednorázová revize",
            //        Description = "Nejeden filozof by mohl tvrdit, že balónky se sluncem závodí, ale fyzikové by to jistě vyvrátili. Z fyzikálního pohledu totiž balónky působí zcela nezajímavě. Nejvíc bezpochyby zaujmou děti - jedna malá holčička zrovna včera div nebrečela, že by snad balónky mohly prasknout. A co teprve ta stuha.",
            //        Domain = domain,
            //        Created = DateTime.Now,
            //        CreatedBy = vedouci,
            //        TaskState = zadano,
            //        Assigned = DateTime.Now,
            //        AssignedTo = pracovnik,
            //        Office = hlObchodPepa,
            //        ToComplete = DateTime.Now.AddDays(4)
            //    });

            //    context.Tasks.Add(new Task
            //    {
            //        Name = "Nákup nového vybavení",
            //        Description = "Stuha, kterou je každý z trojice balónků uvázán, aby se nevypustil.",
            //        Domain = domain,
            //        Created = DateTime.Now,
            //        CreatedBy = vedouci,
            //        TaskState = zadano,
            //        Assigned = DateTime.Now,
            //        AssignedTo = pracovnik,
            //        Office = hlObchodAlois
            //    });

            //    context.Tasks.Add(new Task
            //    {
            //        Name = "Revize hasících přístrojů",
            //        Description = "...",
            //        Domain = domain,
            //        Created = DateTime.Now,
            //        CreatedBy = vedouci,
            //        TaskState = zadano,
            //        Assigned = DateTime.Now,
            //        AssignedTo = pracovnik,
            //        Office = hlObchodAlois,
            //        ToComplete = DateTime.Now.AddDays(6)
            //    });

            //    context.Tasks.Add(new Task
            //    {
            //        Name = "Chtěl bych dostat přidáno",
            //        Description = "Šéfe, prsím ...",
            //        Domain = domain,
            //        Created = DateTime.Now,
            //        CreatedBy = pracovnik,
            //        TaskState = zadano,
            //        Assigned = DateTime.Now,
            //        AssignedTo = vedouci,
            //        Office = hlObchodAlois
            //    });

            //    context.Tasks.Add(new Task
            //    {
            //        Name = "Testovací úkol 3",
            //        Description = "Pouze testovací úkol bez velkého smyslu",
            //        Domain = domain,
            //        Created = DateTime.Now,
            //        CreatedBy = hfp,
            //        TaskState = vytvoreno,
            //        Office = hlObchodAlois
            //    });

            //    context.Tasks.Add(new Task
            //    {
            //        Name = "Testovací úkol 4",
            //        Description = "Pouze testovací úkol bez velkého smyslu",
            //        Domain = domain,
            //        Created = DateTime.Now,
            //        CreatedBy = vedouci,
            //        TaskState = vytvoreno,
            //        Office = hlObchodPepa
            //    });

            //    context.Tasks.Add(new Task
            //    {
            //        Name = "Nákup vrtaček",
            //        Description = "...",
            //        Domain = domain,
            //        Created = DateTime.Now,
            //        CreatedBy = hfp,
            //        TaskState = zadano,
            //        Assigned = DateTime.Now,
            //        AssignedTo = vedouci,
            //        Office = hlObchodAlois,
            //        ToComplete = DateTime.Now.AddDays(6)
            //    });

            //    context.Tasks.Add(new Task
            //    {
            //        Name = "Nákup hasících přístrojů",
            //        Description = "6ks",
            //        Domain = domain,
            //        Created = DateTime.Now,
            //        CreatedBy = hfp,
            //        TaskState = zadano,
            //        Assigned = DateTime.Now,
            //        AssignedTo = hfp,
            //        Office = hlObchodAlois,
            //        ToComplete = DateTime.Now.AddDays(14)
            //    });

            //    context.Tasks.Add(new Task
            //    {
            //        Name = "Testovací úkol 5",
            //        Description = "Pouze testovací úkol bez velkého smyslu",
            //        Domain = domain,
            //        Created = DateTime.Now,
            //        CreatedBy = hfp,
            //        TaskState = zadano,
            //        Assigned = DateTime.Now,
            //        AssignedTo = vedouci,
            //        Office = hlObchodPepa,
            //        ToComplete = DateTime.Now.AddDays(2)
            //    });

            //    context.Tasks.Add(new Task
            //    {
            //        Name = "Revize u Pepy",
            //        Description = "Revizi přístrojů ve 2.patře",
            //        Domain = domain,
            //        Created = DateTime.Now,
            //        CreatedBy = vedouci,
            //        TaskState = zadano,
            //        Assigned = DateTime.Now,
            //        AssignedTo = pracovnik,
            //        Office = hlObchodPepa,
            //        ToComplete = DateTime.Now.AddDays(4)
            //    });
            //}
            //#endregion

            //context.SaveChanges();
        }
    }
}