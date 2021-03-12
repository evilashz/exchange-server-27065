using System;

namespace Microsoft.Exchange.Extensions
{
	// Token: 0x0200001A RID: 26
	public static class ArrayExtensions
	{
		// Token: 0x06000080 RID: 128 RVA: 0x000038BF File Offset: 0x00001ABF
		public static void Swap<T>(this T[] array, int firstIndex, int secondIndex)
		{
			if (firstIndex != secondIndex)
			{
				ArrayExtensions.Swap<T>(ref array[firstIndex], ref array[secondIndex]);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000038D8 File Offset: 0x00001AD8
		public static void Shuffle<T>(this T[] array)
		{
			array.Shuffle(0);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000038E1 File Offset: 0x00001AE1
		public static void Shuffle<T>(this T[] array, int index)
		{
			array.Shuffle(index, array.Length - index);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000038F0 File Offset: 0x00001AF0
		public static void Shuffle<T>(this T[] array, int index, int length)
		{
			array.ValidateRange(index, length);
			if (length > 1)
			{
				Random random = new Random();
				int num = checked(index + 1);
				int i = index + length;
				while (i > num)
				{
					int firstIndex = random.Next(index, i);
					i--;
					array.Swap(firstIndex, i);
				}
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003932 File Offset: 0x00001B32
		public static bool IsNullOrEmpty<T>(this T[] array)
		{
			return array == null || 0 == array.Length;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003940 File Offset: 0x00001B40
		private static void Swap<T>(ref T first, ref T second)
		{
			T t = first;
			first = second;
			second = t;
		}
	}
}
