namespace WingboxCalculator;

public class MathHelpers
{
    public static PolynomialEquation IntegrateViaPowerRule(PolynomialEquation eq)
    {
        double[] output = new double[eq.Coefficients.Length + 1];
        for (int i = 0; i < eq.Coefficients.Length; i++)
        {
            output[i + 1] = eq.Coefficients[i] / (i+1.0);
        }

        return new PolynomialEquation(output);
    }

    public static double SolveForC(PolynomialEquation eq, double xLocation, double functionValue)
    {
        return functionValue - eq.EvaluateAtLocation(xLocation);
    }

    public static PolynomialEquation IntegrateWithBoundaryCondition(PolynomialEquation eq, double xLocation,
        double functionValue)
    {
        var integral = IntegrateViaPowerRule(eq);
        integral.Coefficients[0] = SolveForC(integral, xLocation, functionValue);
        return integral;
    }
    
    public static double SolveDefiniteIntegral(PolynomialEquation eq, double from, double to)
    {
        PolynomialEquation integral = IntegrateViaPowerRule(eq);
        return integral.EvaluateAtLocation(to) - integral.EvaluateAtLocation(from);
    }
    
    
}