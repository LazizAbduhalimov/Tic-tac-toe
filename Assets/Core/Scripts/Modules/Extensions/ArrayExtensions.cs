using System;

public static class ArrayExtensions
{
    private static Random _random = new Random();

    public static T GetRandomElement<T>(this T[] array)
    {
        if (array == null || array.Length == 0)
        {
            throw new ArgumentException("Array cannot be null or empty.");
        }

        var index = _random.Next(array.Length);
        return array[index];
    }
}