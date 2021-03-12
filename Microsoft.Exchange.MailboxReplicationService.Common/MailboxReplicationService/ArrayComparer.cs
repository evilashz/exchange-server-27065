using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000E0 RID: 224
	internal class ArrayComparer<T> : IComparer<T[]>, IEqualityComparer<T[]> where T : IComparable<T>, IEquatable<T>
	{
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060008A9 RID: 2217 RVA: 0x0001071C File Offset: 0x0000E91C
		public static IComparer<T[]> Comparer
		{
			get
			{
				return ArrayComparer<T>.instance;
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x060008AA RID: 2218 RVA: 0x00010723 File Offset: 0x0000E923
		public static IEqualityComparer<T[]> EqualityComparer
		{
			get
			{
				return ArrayComparer<T>.instance;
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x0001072C File Offset: 0x0000E92C
		public int Compare(T[] a1, T[] a2)
		{
			if (a1 == a2)
			{
				return 0;
			}
			if (a1 == null)
			{
				return -1;
			}
			if (a2 == null)
			{
				return 1;
			}
			int num = 0;
			while (num < a1.Length && num < a2.Length)
			{
				int num2 = a1[num].CompareTo(a2[num]);
				if (num2 != 0)
				{
					return num2;
				}
				num++;
			}
			return a1.Length.CompareTo(a2.Length);
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x0001078C File Offset: 0x0000E98C
		public bool Equals(T[] a1, T[] a2)
		{
			return this.Compare(a1, a2) == 0;
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x0001079C File Offset: 0x0000E99C
		public int GetHashCode(T[] a1)
		{
			if (typeof(T) == typeof(byte))
			{
				return (int)MurmurHash.Hash(a1 as byte[]);
			}
			int num = 0;
			for (int i = 0; i < a1.Length; i++)
			{
				int num2 = 8 * (i % 4);
				int num3 = a1[i].GetHashCode() << num2;
				num ^= num3;
			}
			return num;
		}

		// Token: 0x0400050A RID: 1290
		private static ArrayComparer<T> instance = new ArrayComparer<T>();
	}
}
