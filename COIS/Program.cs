// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");


ProgramInstance sio = new ProgramInstance();
sio.Login("klispawel");
sio.SetZoneType(sio.User.Zones[0]);
sio.SetOrgUnit(sio.Zone.OrganizationUnit());

Console.WriteLine("Ready");
Console.ReadKey();
