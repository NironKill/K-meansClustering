namespace K_meansClustering
{
    public class KMeansClustering
    {
        public static float[][] KMeans(float[][] points, int k, int maxIterations = 100)
        {
            if (points == null || points.Length == 0 || k <= 0)
                throw new ArgumentException("Invalid input.");

            Random random = new Random();
            float[][] centroids = points.OrderBy(x => random.Next()).Take(k).ToArray();

            int iteration = 0;
            bool changed;
            do
            {
                int[] assignments = AssignPointsToCentroids(points, centroids);

                float[][] newCentroids = UpdateCentroids(points, assignments, k);

                changed = !CentroidsEqual(centroids, newCentroids);
                centroids = newCentroids;

                iteration++;
            } while (changed && iteration < maxIterations);

            return centroids;
        }

        private static int[] AssignPointsToCentroids(float[][] points, float[][] centroids)
        {
            int[] assignments = new int[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                float minDistance = float.MaxValue;
                for (int j = 0; j < centroids.Length; j++)
                {
                    float distance = EuclideanDistance(points[i], centroids[j]);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        assignments[i] = j;
                    }
                }
            }
            return assignments;
        }

        private static float[][] UpdateCentroids(float[][] points, int[] assignments, int k)
        {
            float[][] newCentroids = new float[k][];
            int[] counts = new int[k];

            for (int i = 0; i < k; i++)
                newCentroids[i] = new float[3];

            for (int i = 0; i < points.Length; i++)
            {
                int cluster = assignments[i];
                for (int j = 0; j < 3; j++)
                    newCentroids[cluster][j] += points[i][j];
                counts[cluster]++;
            }

            for (int i = 0; i < k; i++)
            {
                if (counts[i] > 0)
                {
                    for (int j = 0; j < 3; j++)
                        newCentroids[i][j] /= counts[i];
                }
            }

            return newCentroids;
        }

        private static float EuclideanDistance(float[] a, float[] b)
        {
            float sum = 0;
            for (int i = 0; i < 3; i++)
                sum += (a[i] - b[i]) * (a[i] - b[i]);
            return (float)Math.Sqrt(sum);
        }

        private static bool CentroidsEqual(float[][] a, float[][] b)
        {
            if (a.Length != b.Length)
                return false;

            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (a[i][j] != b[i][j])
                        return false;
                }
            }
            return true;
        }
    }
}
