using MiApi.Servicios.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MiApi.Servicios.Implements
{
    public class Perro : IAnimal
    {
        public void Morir()
        {
            Console.WriteLine("Se murio el perro");
        }
    }
}
