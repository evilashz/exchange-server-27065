using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000096 RID: 150
	internal static class BoxedValueCache<T> where T : struct, IEquatable<T>
	{
		// Token: 0x060006F9 RID: 1785 RVA: 0x00010300 File Offset: 0x0000E500
		public static object GetBoxedValue(T? value)
		{
			if (value == null)
			{
				return null;
			}
			T value2 = value.Value;
			int num = (value2.GetHashCode() & int.MaxValue) % 257;
			IEquatable<T> equatable = BoxedValueCache<T>.BoxedValues[num];
			if (equatable == null || !equatable.Equals(value2))
			{
				equatable = value2;
				BoxedValueCache<T>.BoxedValues[num] = equatable;
			}
			return equatable;
		}

		// Token: 0x0400030E RID: 782
		private const int NumCachedBoxedValues = 257;

		// Token: 0x0400030F RID: 783
		private static readonly IEquatable<T>[] BoxedValues = new IEquatable<T>[257];
	}
}
