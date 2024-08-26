using ExtensionMethods;

namespace sorting_algorithms;

public static class Algorithms
{
    public static List<int> BubbleSort(List<int> items)
    {
        int N = items.Count;

        for (int i = 0; i < N - 2; i++)
        {
            for (int j = 0; j < N - 2; j++)
            {
                if (items[j] <= items[j + 1]) continue;
                items.Swap(j, j + 1);
            }
        }

        return items;
    }
}