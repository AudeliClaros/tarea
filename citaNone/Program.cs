using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace citaNone
{
    class Program
    {
        static void main(string[] args)
        {
            //definir la ruta del archivo
            string path = (@"C:\citaNone\archivo.text");


            using (StreamWriter archivo = File.AppendText(path))
            {
                //contenido del archivo
                archivo.WriteLine("COMPRA EN LINEA");
                //archivo.Close();//cierra el archivo creado
            }
            Console.ReadKey();
        }



    }
}
