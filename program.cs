using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("=== VETERINARIA ===");

        Console.Write("Nombre de la mascota: ");
        string nombre = Console.ReadLine() ?? "";

        Console.Write("Especie (Perro/Gato): ");
        string especie = Console.ReadLine() ?? "";

        Console.Write("Nombre del dueño: ");
        string dueno = Console.ReadLine() ?? "";

        Console.WriteLine("\n=== DATOS REGISTRADOS ===");
        Console.WriteLine("Mascota: " + nombre);
        Console.WriteLine("Especie: " + especie);
        Console.WriteLine("Dueño: " + dueno);

        Console.WriteLine("\nRegistro completado.");
    }
}