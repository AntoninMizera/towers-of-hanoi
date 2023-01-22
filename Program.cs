// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

HanoiTowers.HanoiTower tower = new(4);

System.Console.WriteLine(tower.ToString());
System.Console.WriteLine("----");

tower.Move();

System.Console.WriteLine(tower);

System.Console.WriteLine("end!");