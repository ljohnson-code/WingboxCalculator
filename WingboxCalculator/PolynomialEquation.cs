namespace WingboxCalculator;

/// <summary>
/// Class to hold a polynomial equation with coefficients in ascending degree of the polynomial
/// <example>
/// a + bx + cx^2 etc.
/// </example>
/// </summary>
public class PolynomialEquation
{
    public double[] Coefficients { get; private set; }
    
    public int Degree
    {
        get => Coefficients.Length - 1;
    }
        
    public PolynomialEquation(double[] coeffs)
    {
        Coefficients = coeffs;
    }

    public PolynomialEquation()
    {
        Coefficients = new double[] { 0.0 };
    }
    public double EvaluateAtLocation(double x)
    {
        double val = 0.0;
        int i = 0;
        int size = Coefficients.Length;
        while (i < size) {
            int j = 0;
            double tmp = 1.0;
            while (j < i) {
                tmp *= x;
                j++;
            }
            tmp *= Coefficients[i];
            val += tmp;
            i++;
        }
        return val;
    }

    public static PolynomialEquation operator *(PolynomialEquation poly, double value)
    {
        return new PolynomialEquation(poly.Coefficients.Select(x => x * value).ToArray());
    }

    public static PolynomialEquation operator +(PolynomialEquation eq1, PolynomialEquation eq2)
    {
        double[] output = new double[(int)Math.Max(eq1.Coefficients.Length, eq2.Coefficients.Length)];
        for (int i = 0; i < output.Length; i++)
        {
            if (i < eq1.Coefficients.Length)
            {
                output[i] += eq1.Coefficients[i];
            }
            if (i < eq2.Coefficients.Length)
            {
                output[i] += eq2.Coefficients[i];
            }
        }

        return new PolynomialEquation(output);
    }
}