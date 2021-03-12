﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x02000062 RID: 98
	[__DynamicallyInvokable]
	[Serializable]
	public class Tuple<T1, T2, T3> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000373 RID: 883 RVA: 0x00008A66 File Offset: 0x00006C66
		[__DynamicallyInvokable]
		public T1 Item1
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00008A6E File Offset: 0x00006C6E
		[__DynamicallyInvokable]
		public T2 Item2
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00008A76 File Offset: 0x00006C76
		[__DynamicallyInvokable]
		public T3 Item3
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00008A7E File Offset: 0x00006C7E
		[__DynamicallyInvokable]
		public Tuple(T1 item1, T2 item2, T3 item3)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00008A9B File Offset: 0x00006C9B
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00008AAC File Offset: 0x00006CAC
		[__DynamicallyInvokable]
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2)) && comparer.Equals(this.m_Item3, tuple.m_Item3);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00008B24 File Offset: 0x00006D24
		[__DynamicallyInvokable]
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00008B34 File Offset: 0x00006D34
		[__DynamicallyInvokable]
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
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
			return comparer.Compare(this.m_Item3, tuple.m_Item3);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00008BDB File Offset: 0x00006DDB
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x00008BE8 File Offset: 0x00006DE8
		[__DynamicallyInvokable]
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3));
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00008C22 File Offset: 0x00006E22
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x0600037E RID: 894 RVA: 0x00008C2C File Offset: 0x00006E2C
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00008C54 File Offset: 0x00006E54
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(")");
			return sb.ToString();
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000380 RID: 896 RVA: 0x00008CC1 File Offset: 0x00006EC1
		int ITuple.Length
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000049 RID: 73
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
				default:
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x0400023E RID: 574
		private readonly T1 m_Item1;

		// Token: 0x0400023F RID: 575
		private readonly T2 m_Item2;

		// Token: 0x04000240 RID: 576
		private readonly T3 m_Item3;
	}
}
