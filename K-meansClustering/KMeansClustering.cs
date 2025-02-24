namespace K_meansClustering
{
    public class KMeansClustering
    {
        public static float[][] KMeans(float[][] points, int k, int maxIterations = 100)
        {
            // Validate input
            if (points == null || points.Length == 0 || k <= 0)
                throw new ArgumentException("Invalid input.");

            // Step 1: Initialize centroids randomly
            Random random = new Random();
            float[][] centroids = points.OrderBy(x => random.Next()).Take(k).ToArray();

            int iteration = 0;
            bool changed;
            do
            {
                // Step 2: Assign each point to the nearest centroid
                int[] assignments = AssignPointsToCentroids(points, centroids);

                // Step 3: Update centroids
                float[][] newCentroids = UpdateCentroids(points, assignments, k);

                // Check if centroids have changed
                changed = !CentroidsEqual(centroids, newCentroids);
                centroids = newCentroids;

                iteration++;
            } while (changed && iteration < maxIterations);

            return centroids;
        }

        // Assign each point to the nearest centroid
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

        // Update centroids as the mean of assigned points
        private static float[][] UpdateCentroids(float[][] points, int[] assignments, int k)
        {
            float[][] newCentroids = new float[k][];
            int[] counts = new int[k];

            // Initialize new centroids
            for (int i = 0; i < k; i++)
                newCentroids[i] = new float[3]; // 3D points

            // Sum all points assigned to each centroid
            for (int i = 0; i < points.Length; i++)
            {
                int cluster = assignments[i];
                for (int j = 0; j < 3; j++)
                    newCentroids[cluster][j] += points[i][j];
                counts[cluster]++;
            }

            // Calculate the mean for each centroid
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

        // Calculate Euclidean distance between two 3D points
        private static float EuclideanDistance(float[] a, float[] b)
        {
            float sum = 0;
            for (int i = 0; i < 3; i++)
                sum += (a[i] - b[i]) * (a[i] - b[i]);
            return (float)Math.Sqrt(sum);
        }

        // Check if two sets of centroids are equal
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
