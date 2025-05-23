using System.Collections.Concurrent;



public interface ICuttingStockStrategy
{
    List<List<List<int>>> Solve(int[] cuts, int stockLength);
}

/// <summary>
/// Strategia: brute-force z Parallel.ForEach
/// </summary>
internal class CuttingStockProblemTests
{
    public void Solve_WhenBruteForceParallelStrategy_ShouldReturnsSolutions()
    {
        var cuts = new[] { 1500, 1500, 1500, 1800, 1800 };
        int stockLength = 6000;

        ICuttingStockStrategy strategy = new BruteForceParallelStrategy();
        var solutions = strategy.Solve(cuts, stockLength);

        foreach (var plan in solutions)
        {
            Console.WriteLine("🔹 Plan:");
            foreach (var group in plan)
                Console.WriteLine($"  - [{string.Join(", ", group)}] = {group.Sum()} mm");

            int waste = plan.Count * stockLength - plan.SelectMany(x => x).Sum();
            Console.WriteLine($"  ✂️ Waste: {waste} mm\n");
        }
    }
}

public class BruteForceParallelStrategy : ICuttingStockStrategy
{
    public List<List<List<int>>> Solve(int[] cuts, int stockLength)
    {
        // var allPermutations = GetPermutations(cuts);

        // Dedupikacja
        var allPermutations = GetPermutations(cuts).Distinct(new SequenceEqualityComparer<int>());


        var bestSolutions = new ConcurrentBag<List<List<int>>>();
        int minWaste = int.MaxValue;

        Parallel.ForEach(allPermutations, permutation =>
        {
            var plan = PackCuts(permutation.ToList(), stockLength);
            int waste = plan.Count * stockLength - plan.SelectMany(x => x).Sum();

            lock (bestSolutions)
            {
                if (waste < minWaste)
                {
                    minWaste = waste;
                    bestSolutions.Clear();
                    bestSolutions.Add(plan);
                }
                else if (waste == minWaste)
                {
                    bestSolutions.Add(plan);
                }
            }
        });

        return bestSolutions.ToList();
    }

    private List<List<int>> PackCuts(List<int> cuts, int stockLength)
    {
        var bins = new List<List<int>>();

        foreach (var cut in cuts)
        {
            var placed = false;
            foreach (var bin in bins)
            {
                if (bin.Sum() + cut <= stockLength)
                {
                    bin.Add(cut);
                    placed = true;
                    break;
                }
            }

            if (!placed)
                bins.Add(new List<int> { cut });
        }

        return bins;
    }

    private IEnumerable<List<int>> GetPermutations(int[] items)
    {
        return Permute(items, 0, items.Length - 1);

        static IEnumerable<List<int>> Permute(int[] array, int l, int r)
        {
            if (l == r)
            {
                yield return array.ToList();
            }
            else
            {
                for (int i = l; i <= r; i++)
                {
                    (array[l], array[i]) = (array[i], array[l]);
                    foreach (var p in Permute(array, l + 1, r))
                        yield return p;
                    (array[l], array[i]) = (array[i], array[l]); // backtrack
                }
            }
        }
    }
}


public class SequenceEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
{
    public bool Equals(IEnumerable<T>? x, IEnumerable<T>? y)
    {
        if (x == null || y == null)
            return false;

        return x.SequenceEqual(y);
    }

    public int GetHashCode(IEnumerable<T> obj)
    {
        unchecked
        {
            int hash = 19;
            foreach (var item in obj)
            {
                hash = hash * 31 + (item?.GetHashCode() ?? 0);
            }
            return hash;
        }
    }
}

