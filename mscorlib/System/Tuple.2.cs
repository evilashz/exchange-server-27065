using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x02000060 RID: 96
	[__DynamicallyInvokable]
	[Serializable]
	public class Tuple<T1> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00008702 File Offset: 0x00006902
		[__DynamicallyInvokable]
		public T1 Item1
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000870A File Offset: 0x0000690A
		[__DynamicallyInvokable]
		public Tuple(T1 item1)
		{
			this.m_Item1 = item1;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00008719 File Offset: 0x00006919
		[__DynamicallyInvokable]
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00008728 File Offset: 0x00006928
		[__DynamicallyInvokable]
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1> tuple = other as Tuple<T1>;
			return tuple != null && comparer.Equals(this.m_Item1, tuple.m_Item1);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00008762 File Offset: 0x00006962
		[__DynamicallyInvokable]
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00008770 File Offset: 0x00006970
		[__DynamicallyInvokable]
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1> tuple = other as Tuple<T1>;
			if (tuple == null)
			{
				throw new ArgumentException(Environment.GetResourceString("ArgumentException_TupleIncorrectType", new object[]
				{
					base.GetType().ToString()
				}), "other");
			}
			return comparer.Compare(this.m_Item1, tuple.m_Item1);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000087D1 File Offset: 0x000069D1
		[__DynamicallyInvokable]
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000087DE File Offset: 0x000069DE
		[__DynamicallyInvokable]
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.m_Item1);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000087F1 File Offset: 0x000069F1
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x000087FC File Offset: 0x000069FC
		[__DynamicallyInvokable]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00008822 File Offset: 0x00006A22
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(")");
			return sb.ToString();
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000363 RID: 867 RVA: 0x00008848 File Offset: 0x00006A48
		int ITuple.Length
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000040 RID: 64
		object ITuple.this[int index]
		{
			get
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this.Item1;
			}
		}

		// Token: 0x0400023B RID: 571
		private readonly T1 m_Item1;
	}
}
