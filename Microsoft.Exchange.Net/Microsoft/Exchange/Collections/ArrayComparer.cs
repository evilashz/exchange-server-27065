using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000032 RID: 50
	public sealed class ArrayComparer<T> : IEqualityComparer<T[]>, IComparer<T[]> where T : IComparable<T>, IEquatable<T>
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006A3B File Offset: 0x00004C3B
		public static ArrayComparer<T> Comparer
		{
			get
			{
				return ArrayComparer<T>.comparer;
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006A44 File Offset: 0x00004C44
		public bool Equals(T[] x, T[] y)
		{
			if (object.ReferenceEquals(x, y))
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (x.Length != y.Length)
			{
				return false;
			}
			for (int i = 0; i < x.Length; i++)
			{
				if ((x[i] != null) ? (!x[i].Equals(y[i])) : (y[i] != null))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006AC4 File Offset: 0x00004CC4
		public int Compare(T[] x, T[] y)
		{
			if (object.ReferenceEquals(x, y))
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			int num = Math.Min(x.Length, y.Length);
			for (int i = 0; i < num; i++)
			{
				int num2 = (x[i] != null) ? x[i].CompareTo(y[i]) : ((y[i] != null) ? -1 : 0);
				if (num2 != 0)
				{
					return num2;
				}
			}
			return x.Length - y.Length;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00006B48 File Offset: 0x00004D48
		public int GetHashCode(T[] obj)
		{
			if (obj == null)
			{
				return 0;
			}
			int num = 0;
			foreach (T t in obj)
			{
				if (t != null)
				{
					num = (num << 1 ^ t.GetHashCode());
				}
				else
				{
					num <<= 1;
				}
			}
			return num;
		}

		// Token: 0x040000F5 RID: 245
		private static readonly ArrayComparer<T> comparer = new ArrayComparer<T>();
	}
}
