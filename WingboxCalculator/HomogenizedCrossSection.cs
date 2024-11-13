namespace WingboxCalculator;

public class HomogenizedCrossSection
{
    public double IyyStar;
    public double IzzStar;
    public double IyzStar;
    public double ITildaStar
    {
        get => IyyStar * IzzStar - IyzStar * IyzStar;
    }
    public double YbarStar;
    public double ZbarStar;
    public double ER; 
    public double AStar;

    public List<NonHomogenizedCrossSection> CrossSections;

    public HomogenizedCrossSection(List<NonHomogenizedCrossSection> _crossSections, double er)
    {
        ER = er;
        CrossSections = _crossSections;
        double yprimeSum = 0.0;
        double zprimeSum = 0.0;
        //Determine Modulus weighted centroid and area
        foreach (var crossSection in CrossSections)
        {
            AStar += (crossSection.E / ER) * crossSection.Area;
            yprimeSum += ((crossSection.E / ER) * crossSection.Area) * crossSection.Ybar;
            zprimeSum += ((crossSection.E / ER) * crossSection.Area) * crossSection.Zbar;
        }

        YbarStar = yprimeSum / AStar;
        ZbarStar = zprimeSum / AStar;
        
        foreach (var crossSection in CrossSections)
        {
            //Calculate Iyy*
            IyyStar += (crossSection.E / ER) *
                       (crossSection.Iyy + (crossSection.Area * (Math.Pow(crossSection.Zbar - ZbarStar, 2))));
            //Calculate Iyy*
            IzzStar += (crossSection.E / ER) *
                       (crossSection.Izz + (crossSection.Area * (Math.Pow(crossSection.Ybar - YbarStar, 2))));
            
            //Calculate Iyy*
            IyzStar += (crossSection.E / ER) *
                       (crossSection.Iyz + (crossSection.Area * (crossSection.Ybar - YbarStar)*(crossSection.Zbar - ZbarStar)));
        }
    }
}