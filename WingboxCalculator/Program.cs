using WingboxCalculator;

NonHomogenizedCrossSection c1 = new NonHomogenizedCrossSection()
{
  E = 10.0e6,
  Area = 5,
  Ybar = 2.5,
  Zbar = 5.5,
  Iyy = 5.0 / 12.0,
  Izz = (Math.Pow(5, 3) / 12),
  Iyz = 0.0
};
NonHomogenizedCrossSection c2 = new NonHomogenizedCrossSection()
{
  E=30e6,
  Area = 5,
  Ybar = 0.5,
  Zbar = 2.5,
  Iyy = (Math.Pow(5,3)/12),
  Izz = 5.0/12.0,
  Iyz = 0.0
};

const double DEAD_LOAD_LOCATION_Y = 2;
const double ACTIVE_LOAD_LOCATION_Y = 1;
const double DEAD_LOAD_MAGNITUDE = 5;



LoadCase GetLoadcaseFromCentroid(HomogenizedCrossSection crossSection)
{
  LoadCase loadCase = new()
  {
    P = new PolynomialEquation(new double[]{0}),
    My = new PolynomialEquation(new double[]{5625}),
    Mz = new PolynomialEquation(new double[]{3750}),
    Mx = new PolynomialEquation(new double[]{0}),
    Vz = new PolynomialEquation(new double[]{0}),
    Vy = new PolynomialEquation(new double[]{0})
  };
    //TODO:Implement automated generation of load cases
    return loadCase;
}


Stringer s = new Stringer(4, false, 15, new Point());


HomogenizedCrossSection homogenizedCrossSection = new HomogenizedCrossSection(new()
{
  c1,c2
},c1.E);

double CalculateStressAtPoint(double x, double y, double z, LoadCase loading, HomogenizedCrossSection crossSection)
{
  var sigma = (loading.P.EvaluateAtLocation(x) / crossSection.AStar) - (y / crossSection.ITildaStar) *
              ((loading.Mz.EvaluateAtLocation(x) * crossSection.IyyStar) +
               (loading.My.EvaluateAtLocation(x) * crossSection.IyyStar)) + 
              (z / crossSection.ITildaStar) * ((loading.My.EvaluateAtLocation(x) * crossSection.IzzStar) +
                                           (loading.Mz.EvaluateAtLocation(x) * crossSection.IyzStar));
  return sigma;
}


LoadCase loadCase = new()
{
  P = new PolynomialEquation(new double[]{12500}),
  My = new PolynomialEquation(new double[]{5625}),
  Mz = new PolynomialEquation(new double[]{3750}),
  Mx = new PolynomialEquation(),
  Vz = new PolynomialEquation(),
  Vy = new PolynomialEquation()
};


#region DisplacementCalculations
var v0DoublePrime = loadCase.Mz * homogenizedCrossSection.IyyStar + loadCase.My * homogenizedCrossSection.IyzStar;
var v0Prime = MathHelpers.IntegrateWithBoundaryCondition(v0DoublePrime,0,0);
var v0 = MathHelpers.IntegrateWithBoundaryCondition(v0Prime, 0, 0);
      
var w0DoublePrime =(loadCase.My * homogenizedCrossSection.IzzStar + loadCase.Mz * homogenizedCrossSection.IyzStar) * -1.0;
var w0Prime = MathHelpers.IntegrateWithBoundaryCondition(w0DoublePrime,0,0);
var w0 = MathHelpers.IntegrateWithBoundaryCondition(w0Prime, 0, 0);

v0DoublePrime *= (1 / (homogenizedCrossSection.ITildaStar * homogenizedCrossSection.ER));
v0Prime *= (1 / (homogenizedCrossSection.ITildaStar * homogenizedCrossSection.ER));
v0 *= (1 / (homogenizedCrossSection.ITildaStar * homogenizedCrossSection.ER));
w0DoublePrime *= (1 / (homogenizedCrossSection.ITildaStar * homogenizedCrossSection.ER));
w0Prime *= (1 / (homogenizedCrossSection.ITildaStar * homogenizedCrossSection.ER));
w0 *= (1 / (homogenizedCrossSection.ITildaStar * homogenizedCrossSection.ER));




Console.WriteLine();

#endregion


