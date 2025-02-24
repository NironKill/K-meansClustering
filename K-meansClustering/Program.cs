using K_meansClustering;

float[][] points = new float[][]
        {
            new float[] { 1, 2, 3 },
            new float[] { 4, 5, 6 },
            new float[] { 7, 8, 9 },
            new float[] { 10, 11, 12 }
        };

int k = 2;
float[][] centroids = KMeansClustering.KMeans(points, k);

Console.WriteLine("Cluster Centroids:");
foreach (var centroid in centroids)
{
    Console.WriteLine($"({centroid[0]}, {centroid[1]}, {centroid[2]})");
}