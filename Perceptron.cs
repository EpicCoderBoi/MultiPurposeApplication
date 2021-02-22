/*
 * Written By Sujash Barman 
 * The program's purpose is to create a perceptron which learns the first and last values of the AND gate
 */


using System;
using ActivationFunctions;

namespace Perceptron
{
    class Point
    {
        private double input1; //creates private field
        public double Input1 { get => input1; set => input1 = value; } //creates public property (important for encapsulation of data)
        private double input2;
        public double Input2 { get => input2; set => input2 = value; }
        private double desiredOutput;
        public double DesiredOutput { get => desiredOutput; set => desiredOutput = value; }

        public Point() /*Creates an empty constructor so that the object of the
                        class can be instantiated to refer variables*/
        {

        }

        public Point(int input1, int input2) //Takes in the predetermined values for the perceptron
        {
            this.input1 = input1;
            this.input2 = input2;

            Console.WriteLine("INITIAL FEEDFORWARD VALUES (PREDETERMINED)");
            Console.WriteLine(" ({0}) ({1}) => ({2})", input1, input2, 0); 
        }

        public Point(int input1, int input2, int desiredOutput) //Takes in the predetermined values for the perceptron
        {
            this.input1 = input1;
            this.input2 = input2;
            this.desiredOutput = desiredOutput;

            Console.WriteLine("DESIRED FEEDFORWARD TEST (THIS VALUE WILL BE SOLVED BY THE PERCEPTRON)");
            Console.WriteLine(" ({0}) ({1}) => ({2})", input1, input2, desiredOutput);
        }
    }

    class NeuralNetwork
    {
        ActivationFunction activationFunction = new ActivationFunction();

        //readonly Point referencePoint = new Point(); //used for referring variables from point to class

        private double[] weights; //declares the randomizable weights

        private double sum; //declares the sum of the weights and the inputs

        private const double bias = 1; //declares constant bias

        private double error; //declares the error between the guessedOutput and desiredOutput

        private double errorsquared; //declaring the errorSquared variable (optional)

        private double guessedOutput; //declaring the guessedOutput variable

        private const double learningRate = 0.01; //constant learningRate

        public NeuralNetwork(int w) //Creating constructor to get the desired amount of weights and also call the RandomiseWeightsAndBias function
        {
            weights = new double[w];
            RandomiseWeightsAndBias();
        }
        public void RandomiseWeightsAndBias() //Randomises the weights and bias
        {
            var rand = new Random(); //Creating a new Random Object (implicit)

            for (var i = 0; i < weights.Length; i++) //loops through the weights
            {
                weights[i] = rand.NextDouble(); //Setting each of the weights to a random double
                weights[i] -= 0.5; // expected range now -0.5 to +0.5
                weights[i] *= 2; //expected range now -1 to 1

            }

            Console.WriteLine("STARTING SYNAPTIC WEIGHTS => {0}, {1}, {2}", weights[0], weights[1], weights[2]);
            Console.WriteLine("BIAS IS => " + bias);
        }

        public double FeedForward(Point inputs) //Creating the method "FeedForward" so that it gets the custom data type "Point" to calculate the sum of the inputs and weights as well as limiting the inputs to be either 1 or 0 and returning the activatedSum
        {
            sum = 0;
            sum = inputs.Input1 * weights[0] + inputs.Input2 * weights[1] + bias * weights[2];

            if (inputs.Input1 > 0.99999 && inputs.Input2 > 0.99999)
            {
                inputs.DesiredOutput = 1;
            }
            else
            {
                inputs.DesiredOutput = 0;
            }
            return activationFunction.StepActivation(sum);

        }

        public void AdjustWeights(Point inputs) //This method is used to call the FeedForward method, calculate the error between the desired and guessed output, 'backpropagate' the weights and print out the those values. 
        {
            guessedOutput = FeedForward(inputs);

            error = inputs.DesiredOutput - guessedOutput;

            Console.WriteLine("GUESSED OUTPUT IS {0}", guessedOutput);
            Console.WriteLine("DESIRED OUTPUT IS {0}", inputs.DesiredOutput);

            Console.WriteLine("THE ERROR IS {0}", error);

            errorsquared = Math.Pow(error, 2);
            Console.WriteLine("THE ERRORSQUARED IS {0}", errorsquared);

            while (error != 0)
            {
                guessedOutput = FeedForward(inputs);

                error = inputs.DesiredOutput - guessedOutput;
                errorsquared = Math.Pow(error, 2);

                weights[0] += learningRate * error * inputs.Input1; //I know I can use for loops to make it more concise and clean but it works for now
                weights[1] += learningRate * error * inputs.Input2;
                weights[2] += learningRate * error * bias;
                Console.WriteLine("ADJUSTED WEIGHTS ARE => {0}, {1}, {2}", weights[0], weights[1], weights[2]);
            }

            Console.WriteLine("FINAL OUTPUT IS {0}", guessedOutput);

            Console.WriteLine();
        }

        public void Train(Point inputs) //Training or "backpropagating the weights"
        {
            AdjustWeights(inputs);
        }

    }
    class TrainPoints //This class is used to train the Points (or the inputs) and the methods inside of the class is called when one of the buttons in the PerceptronForm is called. 
    {
            public void TrainSet1() //This method is used to train the first values of the AND gate
            {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.BackgroundColor = ConsoleColor.Green;

            var points = new Point[4];

            points[0] = new Point(0, 0);
            points[1] = new Point(0, 1);
            points[2] = new Point(1, 0);
            points[3] = new Point(0, 0, 0);

            var neuralNetwork = new NeuralNetwork(3);


            neuralNetwork.Train(points[3]);
            }

            public void TrainSet2() //This method is used to train the last pair of values of the AND gate
            {
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.BackgroundColor = ConsoleColor.Green;

            var points = new Point[4];

            points[0] = new Point(0, 0);
            points[1] = new Point(0, 1);
            points[2] = new Point(1, 0);
            points[3] = new Point(1, 1, 1);
            
            var neuralNetwork = new NeuralNetwork(3);

            neuralNetwork.Train(points[3]);
            }
    }


}
