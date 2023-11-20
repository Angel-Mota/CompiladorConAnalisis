using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        Console.WriteLine("Ingrese el nombre del archivo:");
        string nombreArchivo = Console.ReadLine();
        string rutaArchivo = $"{nombreArchivo}.txt";

        while (true)
        {
            Console.WriteLine("Seleccione una opción:");
            Console.WriteLine("1. Añadir al archivo");
            Console.WriteLine("2. Leer desde el archivo");
            Console.WriteLine("3. Borrar el archivo");
            Console.WriteLine("4. Salir");

            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("Ingrese el texto para añadir al archivo:");
                    string texto = Console.ReadLine();
                    File.AppendAllText(rutaArchivo, texto + Environment.NewLine);
                    Console.WriteLine("Texto añadido correctamente.");
                    break;

                case "2":
                    if (File.Exists(rutaArchivo))
                    {
                        Console.WriteLine("Contenido del archivo:");
                        string contenido = File.ReadAllText(rutaArchivo);
                        Console.WriteLine(contenido);

                        MostrarResultadoVerificacion(contenido);

                        // Realizar análisis sintáctico adicional
                        if (!VerificarSintaxis(contenido))
                        {
                            Console.WriteLine("El código tiene errores de sintaxis.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("El archivo no existe. Primero, añada contenido al archivo.");
                    }
                    break;

                case "3":
                    if (File.Exists(rutaArchivo))
                    {
                        File.Delete(rutaArchivo);
                        Console.WriteLine("Archivo borrado correctamente.");
                    }
                    else
                    {
                        Console.WriteLine("El archivo no existe. No se puede borrar.");
                    }
                    break;

                case "4":
                    Console.WriteLine("Saliendo del programa.");
                    return;

                default:
                    Console.WriteLine("Opción no válida. Por favor, seleccione una opción válida.");
                    break;
            }
        }
    }

    static void MostrarResultadoVerificacion(string texto)
    {
        string patronSi = @"(?<!\w)si\s*\([^)]*\)\s*\{\s*.*;\s*\}";
        string patronMientras = @"(?<!\w)mientras\s*\([^)]*\)\s*\{\s*.*;\s*\}";
        string patronPara = @"(?<!\w)para\s*\([^;]*;\s*[^;]*;\s*[^)]*\)\s*\{\s*.*;\s*\}";

        if (Regex.IsMatch(texto, patronSi))
        {
            Console.WriteLine("El ciclo 'si' está bien estructurado en el archivo.");
        }
        else if (Regex.IsMatch(texto, patronMientras))
        {
            Console.WriteLine("El ciclo 'mientras' está bien estructurado en el archivo.");
        }
        else if (Regex.IsMatch(texto, patronPara))
        {
            Console.WriteLine("El ciclo 'para' está bien estructurado en el archivo.");
        }
        else
        {
            Console.WriteLine("No se encontraron ciclos bien estructurados en el archivo.");
        }
    }

    static bool VerificarSintaxis(string codigo)
    {
        int countLlaveAbierta = 0;
        int countLlaveCerrada = 0;
        int countParentesisAbierto = 0;
        int countParentesisCerrado = 0;
        int countComillas = 0;
        int countPuntoComa = 0;

        foreach (char c in codigo)
        {
            switch (c)
            {
                case '{':
                    countLlaveAbierta++;
                    break;
                case '}':
                    countLlaveCerrada++;
                    break;
                case '(':
                    countParentesisAbierto++;
                    break;
                case ')':
                    countParentesisCerrado++;
                    break;
                case '"':
                    countComillas++;
                    break;
                case ';':
                    countPuntoComa++;
                    break;
            }
        }

        bool sintaxisCorrecta = true;

        if (countLlaveAbierta != countLlaveCerrada)
        {
            Console.WriteLine("Error: falta de llaves.");
            sintaxisCorrecta = false;
        }

        if (countParentesisAbierto != countParentesisCerrado)
        {
            Console.WriteLine("Error: falta de paréntesis.");
            sintaxisCorrecta = false;
        }

        if (countComillas % 2 != 0)
        {
            Console.WriteLine("Error: falta de comillas.");
            sintaxisCorrecta = false;
        }

        if (countPuntoComa  != 1)
        {
            Console.WriteLine("Error: falta punto y coma.");
            sintaxisCorrecta = false;
        }

        if (sintaxisCorrecta)
        {
            Console.WriteLine("La sintaxis es correcta.");
        }

        return sintaxisCorrecta;
    }


}
