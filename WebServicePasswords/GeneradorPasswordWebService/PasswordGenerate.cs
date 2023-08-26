using System;
using System.IO;
using System.Text;


namespace WebServiceContraseñas
{
    public class PasswordGenerate
    {
        // generamos un objeto de tipoRandom para la generación de contraseñas
        private Random rdn = new Random();

        public string PasswordFacilRecordar(int complejidad = 2)
        {

            String[] palabrasClave = this.Diccionario();

            //longitud para recorrer las palabras del diccionario
            int longitud = palabrasClave.Length;
            
            // password es la variable que contendra la contraseña 
            string password = string.Empty;

            for (int i = 0; i < complejidad; i++)
            {

                
                string Palabra = palabrasClave[rdn.Next(longitud - 1)];
                StringBuilder sb = new StringBuilder(Palabra);
                // pone la primera letra en mayus
                
                sb[0] = Char.ToUpper(sb[i]);

         
                password += sb.ToString() + rdn.Next(99);
            }

            return password;
        }

        public string GenerarPassword(int complejidad = 8, Boolean simbolos = false, Boolean numero = false)
        {
            // variables que contienen los caracteres para crear las contraseñas;
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string numeros = "1234567890";
            string caracteresEsp ="!@#$%&/()=+?[]";
            string password = "";
            // longitud : numero de caracteres a tomar en cuenta para la creación de la contraseña
            // por defecto la longitud de caracteres
            int longitud = caracteres.Length;

            // comprobación de la longitud minima de la contraseña
            if (complejidad < 8)
            {
                complejidad = 8;
            }
            //se añade los caracteres especiales si se lo solicita
            if (simbolos == true)
            {
                caracteres += caracteresEsp;
                // longitud : numero de caracteres a tomar en cuenta para la creación de la contraseña
                longitud += caracteresEsp.Length;
            }
            // se añade los numeros si se lo solicita
            if (numero == true)
            {
                caracteres += numeros;
                // longitud : numero de caracteres a tomar en cuenta para la creación de la contraseña
                longitud += numeros.Length;
            }
            // se crea la contraseña con caracteres aleatorios en el bucle for
            for (int i = 0; i < complejidad; i++)
            {
                char letra = caracteres[rdn.Next(longitud - 1)];
                password += letra.ToString();
            }
            // se retorna la contraseña
            return password;
        }

        // método para obtener palabras del archivo csv y formar contraseñas faciles de recordar
        public string[] Diccionario()
        {
            string[] palabras;
            //variable que almacena la ruta base de la solución
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            //file path combina la ruta del archivo a leer con la ruta base
            string filePath = Path.Combine(basePath, "GeneradorPasswordWebService/PalabrasPassword/words.csv");
            // leemos el archivo que contiene el diccionario de palabras
            using (StreamReader reader = new StreamReader(filePath))
            {
                string csv = reader.ReadToEnd();
                string[] lines = csv.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                palabras = new string[lines.Length];

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] fields = lines[i].Split(',');
                    palabras[i] = fields[0];
                }
            }
            return palabras;


        }

        
    }
}