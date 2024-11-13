using System.Numerics;

namespace WingboxCalculator;

public class Stringer: NonHomogenizedCrossSection
{
    private const double SIDE_LENGTH = 1.0 / 8.0;
    
    /// <summary>
    /// Create a cross section representing a 1/8in. square stringer. This assumes that any rotation due to
    /// the upper skins radius rotates the stringer about its centroid.
    /// </summary>
    /// <param name="horizontalCentroidLocation"></param>
    public Stringer(double horizontalCentroidLocation, bool isOnUpperSkin=false, double circleRadius = 0.0, Point circleCenter = null)
    {
        Iyy = Math.Pow(SIDE_LENGTH, 4) / 12;
        Izz = Math.Pow(SIDE_LENGTH, 4) / 12;
        Iyz = 0;
        if (isOnUpperSkin)
        {
            if (circleCenter == null || circleRadius <= 0.0)
            {
                throw new InvalidDataException(
                    $"Invalid parameters, circle radius must be greater than 0, got: {circleRadius}. circleCenter must also be a point, is supplied value a point: {circleCenter is Point}");
            }
            CalculateCentroidLocationUpperSkin(horizontalCentroidLocation, circleRadius, circleCenter);
        }
        else
        {
            Ybar = horizontalCentroidLocation;
            Zbar = ((1.25 - .75) / 4.0) * horizontalCentroidLocation - 1.0 / (2*SIDE_LENGTH);
        }
    }

    private void CalculateCentroidLocationUpperSkin(double x,double circleRadius = 0.0, Point circleCenter = null)
    {
        double a = circleCenter.x;
        double b = circleCenter.y;
        double y = circleRadius * Math.Sin(Math.Acos((a - x) / circleRadius));
        Vector2 direction = new Vector2((float)(x - a), (float)(y - b));
        direction /= direction.Length();
        Ybar = x + SIDE_LENGTH / 2.0 * direction.X;
        Zbar = y + SIDE_LENGTH / 2.0 * direction.Y;
    }
    

    /// <summary>
    /// Gets the Slope of the line tangent to the upper half of a circle at a given x value
    /// </summary>
    /// <param name="x">X-location to calculate the slope at</param>
    /// <param name="radius">Radius of the circle</param>
    /// <param name="circleCenter">The origin of the circle</param>
    /// <returns></returns>
    private double GetCircleTangentLineSlope(double x, double radius, Point circleCenter)
    {
        double theta = Math.Acos(x / radius);
        double y = radius * Math.Sin(theta);
        double m = -1 * ((x - circleCenter.x) / (y - circleCenter.y));
        return m;
    }

}