using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TobbbformosPizzaAlkalmazasEgyTabla.Model;

namespace _2019TobbformosMvcPizzaEgyTabla.model
{
     partial class Megrendelo
    {
        private int id;
        private string name;
        private string location;



        public Megrendelo(int id, string name, string location)
        {
            this.id = id;
            if (!isValidName(name))
                throw new ModelMegrendeloNotValidNameException("A Megrendelő neve nem megfelelő!");
            if (!isValidLocation(location))
                throw new ModelMegrendeloNotValidLocationException("A Megrendelő címe nem megfelelő!");
            this.name = name;
            this.location = location;
             
           
        }

        private bool isValidName(string name)
        {
            if (name == string.Empty)
                return false;
            if (!char.IsUpper(name.ElementAt(0)))
                return false;
            for (int i = 1; i < name.Length; i = i + 1)
                if (
                    !char.IsLetter(name.ElementAt(i))
                        &&
                    (!char.IsWhiteSpace(name.ElementAt(i)))

                    )
                    return false;
                    return true;
        }


        private bool isValidLocation(string location)
        {
            if (name == string.Empty)
                return false;
            return true;
        }

        public void setID(int id)
        {
            this.id = id;
        }
        public void setName(string name)
        {
            this.name = name;
        }
        public void setLocation(string location)
        {
            this.location = location;
        }
        public int getId()
        {
            return id;
        }
        public string getName()
        {
            return name;
        }
        public string getLocation()
        {
            return location;
        }

        public void update(Megrendelo modified)
        {
            this.name = modified.getName();
            this.location = modified.getLocation();
        }
        



    }
}
