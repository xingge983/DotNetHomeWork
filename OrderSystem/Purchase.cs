using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public class Purchase
    {
        public Good Good
        {
            get;
            set;
        }

        public uint  Quantity
        {
            get;
            set;
        }

        public float Amount
        {
            get => Good.Price * this.Quantity;
        }

        public Purchase(Good i_good, uint i_quantity)
        {
            this.Good = i_good;
            this.Quantity = i_quantity;
        }

        public Purchase()
        {
            
        }

        public override bool Equals(object i_obj)
        {
            var i_purchase = i_obj as Purchase;
            if(i_purchase!=null)
            {
                return i_purchase.Good == this.Good;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return (int)this.Good.Id + (int)this.Quantity;
        }

        public override string ToString()
        {
            return $"Purchase inf: {Good}, Quantity: {Quantity}.";
        }
    }
}
