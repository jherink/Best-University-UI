using System;
using System.Collections.Generic;
using System.Text;

namespace Demo.DTO
{
    public class Ratio
    {
        public int Numerator { get; set; }
        public int Denominator { get; set; }

        public virtual void Simplify()
        {
            var gcd = GreatestCommonDenominator( Numerator, Denominator );
            Numerator /= gcd;
            Denominator /= gcd;
        }

        private int GreatestCommonDenominator( int a, int b )
        {
            while ( b > 0 )
            {
                int rem = a % b;
                a = b;
                b = rem;
            }
            return a;
        }
    }

    public class StudentTeacherRatio : Ratio
    {
        public override void Simplify()
        {
            base.Simplify();

            double n = Numerator;
            double d = Denominator;

            Numerator = (int)Math.Round( n / d );
            Denominator /= Denominator;
        }
    }
}
