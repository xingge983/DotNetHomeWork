using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSystem
{
    public class Customer
    {
        public uint Id
        {
            get;
            set;

        }

        public string Name
        {
            get;
            set;
        }

        public Customer(uint i_id, string i_name)
        {
            this.Id = i_id;
            this.Name = i_name;
        }

        public Customer()
        {
            
        }

        public override bool Equals(object i_obj)
        {
            var i_customer = i_obj as Customer;
            if(i_customer!=null)
            {
                return this.Id == i_customer.Id;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"Customer Inf: Id:{this.Id}, Name:{this.Name}.";
        }
    }
}
