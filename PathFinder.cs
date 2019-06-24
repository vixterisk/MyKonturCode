using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RoutePlanning
{
	public static class PathFinderTask
	{
        static double minPathLength = double.MaxValue;

        public static int[] FindBestCheckpointsOrder(Point[] checkpoints)
		{
            minPathLength = double.MaxValue;
            var currentPath = new int[checkpoints.Length];
            currentPath[0] = 0;
            var bestOrder = new List<int[]>();
            MakePermutations(checkpoints, currentPath, 1, 0, bestOrder);
            for (int i = 0; i < bestOrder.Count; i++)
            {
                if (PointExtensions.GetPathLength(checkpoints, bestOrder[i]) == minPathLength)
                    currentPath = bestOrder[i];
            }
            return currentPath;
        }

        static void MakePermutations(Point[] checkpoints, int[] permutation, 
									 int position, double currentPathLength, List<int[]> result)
        {
            if (position == permutation.Length)
            {
                if (currentPathLength < minPathLength)
                    minPathLength = currentPathLength;
                result.Add(permutation.ToArray());
                return;
            }
            for (int i = 1; i < permutation.Length; i++)
            {
                var index = Array.IndexOf(permutation, i, 1, position - 1);
                if (index == -1)
                {
                    permutation[position] = i;
                    double addedPath =  PointExtensions.DistanceTo(checkpoints[permutation[position]],
										checkpoints[permutation[position - 1]]);
                    if (currentPathLength > minPathLength) return;
                    MakePermutations(checkpoints, permutation, position + 1, currentPathLength + addedPath, result);
                }
            }
        }
    }
}
