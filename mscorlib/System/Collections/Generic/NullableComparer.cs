using System;

namespace System.Collections.Generic
{
	// Token: 0x0200048F RID: 1167
	[Serializable]
	internal class NullableComparer<T> : Comparer<T?> where T : struct, IComparable<T>
	{
		// Token: 0x06003910 RID: 14608 RVA: 0x000D93FA File Offset: 0x000D75FA
		public override int Compare(T? x, T? y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.value.CompareTo(y.value);
				}
				return 1;
			}
			else
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x06003911 RID: 14609 RVA: 0x000D9438 File Offset: 0x000D7638
		public override bool Equals(object obj)
		{
			NullableComparer<T> nullableComparer = obj as NullableComparer<T>;
			return nullableComparer != null;
		}

		// Token: 0x06003912 RID: 14610 RVA: 0x000D9450 File Offset: 0x000D7650
		public override int GetHashCode()
		{
			return base.GetType().Name.GetHashCode();
		}
	}
}
