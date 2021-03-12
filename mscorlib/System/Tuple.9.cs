using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x02000067 RID: 103
	[__DynamicallyInvokable]
	[Serializable]
	public class Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00009D72 File Offset: 0x00007F72
		[__DynamicallyInvokable]
		public T1 Item1
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x00009D7A File Offset: 0x00007F7A
		[__DynamicallyInvokable]
		public T2 Item2
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00009D82 File Offset: 0x00007F82
		[__DynamicallyInvokable]
		public T3 Item3
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00009D8A File Offset: 0x00007F8A
		[__DynamicallyInvokable]
		public T4 Item4
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00009D92 File Offset: 0x00007F92
		[__DynamicallyInvokable]
		public T5 Item5
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item5;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00009D9A File Offset: 0x00007F9A
		[__DynamicallyInvokable]
		public T6 Item6
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item6;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00009DA2 File Offset: 0x00007FA2
		[__DynamicallyInvokable]
		public T7 Item7
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item7;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00009DAA File Offset: 0x00007FAA
		[__DynamicallyInvokable]
		public TRest Rest
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Rest;
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x00009DB4 File Offset: 0x00007FB4
		[__DynamicallyInvokable]
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
		{
			if (!(rest is ITupleInternal))
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_TupleLastArgumentNotATuple"));
			}
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
			this.m_Item7 = item7;
			this.m_Rest = rest;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00009E22 File Offset: 0x00008022
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00009E30 File Offset: 0x00008030
		[__DynamicallyInvokable]
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5) && comparer.Equals(this.m_Item6, tuple.m_Item6) && comparer.Equals(this.m_Item7, tuple.m_Item7)) && comparer.Equals(this.m_Rest, tuple.m_Rest);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00009F47 File Offset: 0x00008147
		[__DynamicallyInvokable]
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00009F58 File Offset: 0x00008158
		[__DynamicallyInvokable]
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
			if (tuple == null)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_TupleIncorrectType", new object[]
				{
					base.GetType().ToString()
				}), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item2, tuple.m_Item2);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item3, tuple.m_Item3);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item4, tuple.m_Item4);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item5, tuple.m_Item5);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item6, tuple.m_Item6);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item7, tuple.m_Item7);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Rest, tuple.m_Rest);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000A0A9 File Offset: 0x000082A9
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000A0B8 File Offset: 0x000082B8
		[__DynamicallyInvokable]
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			ITupleInternal tupleInternal = (ITupleInternal)((object)this.m_Rest);
			if (tupleInternal.Length >= 8)
			{
				return tupleInternal.GetHashCode(comparer);
			}
			switch (8 - tupleInternal.Length)
			{
			case 1:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 2:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 3:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 4:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 5:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 6:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 7:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			default:
				return -1;
			}
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x0000A351 File Offset: 0x00008551
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000A35C File Offset: 0x0000855C
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x0000A384 File Offset: 0x00008584
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(", ");
			sb.Append(this.m_Item4);
			sb.Append(", ");
			sb.Append(this.m_Item5);
			sb.Append(", ");
			sb.Append(this.m_Item6);
			sb.Append(", ");
			sb.Append(this.m_Item7);
			sb.Append(", ");
			return ((ITupleInternal)((object)this.m_Rest)).ToString(sb);
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060003DA RID: 986 RVA: 0x0000A479 File Offset: 0x00008679
		int ITuple.Length
		{
			get
			{
				return 7 + ((ITupleInternal)((object)this.Rest)).Length;
			}
		}

		// Token: 0x17000071 RID: 113
		object ITuple.this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.Item1;
				case 1:
					return this.Item2;
				case 2:
					return this.Item3;
				case 3:
					return this.Item4;
				case 4:
					return this.Item5;
				case 5:
					return this.Item6;
				case 6:
					return this.Item7;
				default:
					return ((ITupleInternal)((object)this.Rest))[index - 7];
				}
			}
		}

		// Token: 0x04000257 RID: 599
		private readonly T1 m_Item1;

		// Token: 0x04000258 RID: 600
		private readonly T2 m_Item2;

		// Token: 0x04000259 RID: 601
		private readonly T3 m_Item3;

		// Token: 0x0400025A RID: 602
		private readonly T4 m_Item4;

		// Token: 0x0400025B RID: 603
		private readonly T5 m_Item5;

		// Token: 0x0400025C RID: 604
		private readonly T6 m_Item6;

		// Token: 0x0400025D RID: 605
		private readonly T7 m_Item7;

		// Token: 0x0400025E RID: 606
		private readonly TRest m_Rest;
	}
}
