using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Content_Management_System.Classes
{
    public enum Fuel { Diesel , Petrol}
    public class CarSpecifications
    {
        public string Model { get; set; }        
        public string Picture { get; set; }
        public DateTime DateAdd { get; set; }
        public int NumberInRow { get; set; }
        public string Rtf {  get; set; }      
        public string fuel { get; set; }
        

        public CarSpecifications(string model, string carBody, string fuel, string picture, string rtf, int number)
        {
            Model = model;                    
            Picture = picture;
            DateAdd = DateTime.Now;
            NumberInRow = number;
            Rtf = rtf;       
            this.fuel = fuel;
        }

        public CarSpecifications() 
        {
        
        }  
    }
}
