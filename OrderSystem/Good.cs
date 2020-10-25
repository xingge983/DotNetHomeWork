using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public class Good
    {

        public uint Id
        {
            get;
            set;
        }

        public String Name
        {
            get;
            set;
        }

        private float price;

        public float Price
        {
            get
            {
                return price;
            }
            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException("Price must be Active");
                }
                else
                {
                    price = value;
                }
            }
        }

        public Good(uint i_id,string i_name, float i_price)
        {
            this.Id = i_id;
            this.Name = i_name;
            this.Price = i_price;
        }

        public Good()
        {
            
        }

        public override bool Equals(object i_obj)
        {
            var i_good = i_obj as Good;
            if(i_good!=null)
            {
                return this.Id == i_good.Id;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Good Inf: Id:{this.Id}, Name:{this.Name}, Price:{this.Price}.";
        }
    }
}
