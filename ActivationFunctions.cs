using System;
namespace ActivationFunctions
{
    public class ActivationFunction
    {
        public double StepActivation(double x)
        {
            if (x >= 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
