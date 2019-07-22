using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeneratePayslip.Models
{
    public class TaxRule
    {
        public int BaseTaxAmount { get; private set; }
        public decimal ExcessAmount { get; private set; }
        public int? MaxTaxIncome { get; private set; }
        public int MinTaxIncome { get; private set; }
        public string TaxYear { get; private set; }

        public TaxRule(int minIncome, int? maxIncome, int baseIncome, decimal excessAmount, string taxYear)
        {
            this.MinTaxIncome = minIncome;
            this.MaxTaxIncome = maxIncome;
            this.BaseTaxAmount = baseIncome;
            this.ExcessAmount = excessAmount;
            this.TaxYear = taxYear;
        }
        
        public static List<TaxRule> LoadRules()
        {
            var rules = new List<TaxRule>();
            var a = new TaxRule(0, 18200, 0, 0, "2017-2018");
            rules.Add(a);

            var b = new TaxRule(18201, 37000, 0, .19m, "2017-2018");
            rules.Add(b);

            var c = new TaxRule(37001, 87000, 3572, .325m, "2017-2018");
            rules.Add(c);

            var d = new TaxRule(87001, 180000, 19822, .37m, "2017-2018");
            rules.Add(d);

            var e = new TaxRule(180001, null, 54232, .45m, "2017-2018");
            rules.Add(e);

            return rules;
        }
    }
}
