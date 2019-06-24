using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        double price;
        int nightsCount;
        double discount;
        double total;

        public double Price
        {
            get
            {
                return price;
            }
            set
            {
                if (value < 0) throw new ArgumentException();
                price = value;
                Notify(nameof(Price));
				total = CalculateTotal();
            }
        }

        public int NightsCount
        {
            get
            {
                return nightsCount;
            }
            set
            {
                if (value <= 0) throw new ArgumentException();
                nightsCount = value;
                Notify(nameof(NightsCount));
				total = CalculateTotal();
            }
        }

        public double Discount
        {
            get
            {
                return discount;
            }
            set
            {
                discount = value;
                Notify(nameof(Discount));
				total = CalculateTotal();
			}
        }

        public double Total
        {
            get
            {
                return total;
            }
            set
            {
                if (value < 0) throw new ArgumentException();
                total = value;
                Notify(nameof(Total));
                discount = CalculateDiscount();
            }
        }

        public double CalculateTotal()
        {
			var calculatedTotal = Price * NightsCount * (1 - Discount / 100);
            if (calculatedTotal < 0) throw new ArgumentException();
            Notify(nameof(Total));
            return calculatedTotal;
        }

        public double CalculateDiscount()
        {
			var calculatedDiscount = (1 - total / (Price * NightsCount)) * 100;
            Notify(nameof(Discount));
            return calculatedDiscount;
        }
    }
}
