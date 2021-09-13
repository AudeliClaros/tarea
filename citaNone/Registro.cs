using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace citaNone
{
    class Registro
    {


        static void Main()
        {
            bool showMenu = true;

            while (showMenu)
            {
                showMenu = Menu();
            }
            Console.ReadKey();
        }

        private static bool Menu()
        {
            Console.WriteLine("SISTEMA DE RESERVACION A CITA MEDICA ");
            Console.WriteLine("Con atencion de Lunes, de 7 a.m - 4.30 p.m ");
            Console.WriteLine("                                    ");
            Console.WriteLine("Seleccion la operación a realizar: ");
            Console.WriteLine("1. Reservar cita");
            Console.WriteLine("2. Cambiar dia dia/hora ");
            Console.WriteLine("3. Anular reservacion ");
            Console.WriteLine("4. Mostrar listado de citas programadas ");
            Console.WriteLine("5. Salir");
            Console.Write("\nOpcion: ");

            switch (Console.ReadLine())
            {
                case "1":
                    registro();
                    return true;
                case "2":
                    updateData();
                    Console.ReadKey();
                    return true;
                case "3":
                    deleteData();

                    return true;
                case "4":
                    //mostrar el contenido del archivo
                    Console.WriteLine("LISTADO DE RESERVACIONES");
                    foreach (KeyValuePair<object, object> data in readFile())
                    {
                        Console.WriteLine("{0}: {1}", data.Key, data.Value);
                    }
                    Console.ReadKey();

                    return true;
                case "5":
                    return false;
                default:
                    return false;
            }
        }

        //metodo para obtener la ruta del archivo
        private static string getPath()
        {
            string path = (@"C:\citaNone\archivo.text");
            return path;
        }

        private static void registro()
        {
            Console.WriteLine("DATOS DEL PACIENTE ");
            Console.Write("Nombre Completo: ");
            string name = Console.ReadLine();
            Console.Write("Hora : ");
            double hora = Convert.ToDouble(Console.ReadLine());

            if (search(name))
            {
                Console.WriteLine("Este espacio de cita ya esta asignado, confirme uno nuevo ");
            }
            else
            {
                using (StreamWriter sw = File.AppendText(getPath()))
                {
                    sw.WriteLine("{0}; {1}", name, hora);
                    sw.Close();
                }
            }

        }

        private static Dictionary<object, object> readFile()
        {
            Dictionary<object, object> listData = new Dictionary<object, object>();

            using (var reader = new StreamReader(getPath()))
            {
                string lines;

                while ((lines = reader.ReadLine()) != null)
                {
                    string[] keyvalue = lines.Split(';');
                    if (keyvalue.Length == 2)
                    {
                        listData.Add(keyvalue[0], keyvalue[1]);
                    }
                }

            }

            return listData;
        }

        private static bool search(string name)
        {
            if (!readFile().ContainsKey(name))
            {
                return false;
            }
            return true;
        }

        private static void updateData()
        {
            Console.Write("Ingrese el nombre del paciente que desea cambiar la hora de la cita : ");
            var nombre = Console.ReadLine();

            if (search(nombre))
            {
                Console.WriteLine("Paciente encontrado ");
                Console.Write("Ingrese la nueva hora a asignar : ");
                var newAge = Console.ReadLine();

                Dictionary<object, object> temporal = new Dictionary<object, object>();
                temporal = readFile();

                temporal[nombre] = newAge;
                Console.WriteLine("Se ha realizado el cambio!!");
                File.Delete(getPath());

                using (StreamWriter sw = File.AppendText(getPath()))
                {
                    foreach (KeyValuePair<object, object> values in temporal)
                    {
                        sw.WriteLine("{0}; {1}", values.Key, values.Value);
                    }
                }

            }
            else
            {
                Console.WriteLine("El paciente no ha sido encontrado.");
            }
        }
        private static void deleteData()
        {
            Console.Write("Escriba el nombre del paciente que desea eliminar su cita: ");
            var name = Console.ReadLine();
            if (search(name))
            {
                Console.WriteLine("El registro existe!");
                Dictionary<object, object> temp = new Dictionary<object, object>();
                temp = readFile();

                temp.Remove(name);

                Console.WriteLine("La reservacion ha sido eliminada!");
                File.Delete(getPath());

                using (StreamWriter sw = File.AppendText(getPath()))
                {
                    foreach (KeyValuePair<object, object> values in temp)
                    {
                        sw.WriteLine("{0}; {1}", values.Key, values.Value);

                    }
                }

            }
            else
            {
                Console.WriteLine("El registro no se encontro!");
            }

        }


    }
}