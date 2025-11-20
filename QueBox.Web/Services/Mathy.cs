// Mathy.cs
namespace QueBox.Services
{
    public class Mathy
    {
        public static List<object> LinspaceList(double start, double stop, int num, bool endpoint = true)
        {
            if (num < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(num), "Number of samples must be non-negative.");
            }

            if (num == 0)
            {
                return new List<object>();
            }

            List<object> result = new List<object>(num);

            if (num == 1)
            {
                result.Add(start);
                return result;
            }

            double step;
            if (endpoint)
            {
                step = (stop - start) / (num - 1);
            }
            else
            {
                step = (stop - start) / num;
            }

            for (int i = 0; i < num; i++)
            {
                result.Add(start + i * step);
            }

            // Adjust the last element if endpoint is true to ensure it's exactly 'stop'
            // due to potential floating-point inaccuracies
            if (endpoint && num > 1)
            {
                result[num - 1] = stop;
            }

            return result;
        }

        public static Tuple<List<object>, List<object>> CreateMeshGrid(List<object> xValues, List<object> yValues)
        {
            int numX = xValues.Count;
            int numY = yValues.Count;

            // Initialize the 2D lists for X and Y grids
            List<object> X = new List<object>();
            List<object> Y = new List<object>();

            // Populate the X grid
            for (int i = 0; i < numY; i++)
            {
                List<object> rowX = new List<object>();
                for (int j = 0; j < numX; j++)
                {
                    rowX.Add(xValues[j]);
                }
                X.Add(rowX);
            }

            // Populate the Y grid
            for (int i = 0; i < numY; i++)
            {
                List<object> rowY = new List<object>();
                for (int j = 0; j < numX; j++)
                {
                    rowY.Add(yValues[i]);
                }
                Y.Add(rowY);
            }

            return Tuple.Create(X, Y);
        }

        public static List<object> CreateListOfListOfOnesCapacity(int count)
        {

            List<object> onesList = new List<object>(count); // Initialize with capacity
            for (int i = 0; i < count; i++)
            {
                onesList.Add(1);
            }

            List<object> onesListList = new List<object>(count); // Initialize with capacity
            for (int i = 0; i < count; i++)
            {
                onesListList.Add(onesList);
            }

            return onesListList;
        }

        public static List<object> CreateListOfListOfZerosCapacity(int count)
        {

            List<object> onesList = new List<object>(count); // Initialize with capacity
            for (int i = 0; i < count; i++)
            {
                onesList.Add(0);
            }

            List<object> onesListList = new List<object>(count); // Initialize with capacity
            for (int i = 0; i < count; i++)
            {
                onesListList.Add(onesList);
            }

            return onesListList;
        }
    }
}