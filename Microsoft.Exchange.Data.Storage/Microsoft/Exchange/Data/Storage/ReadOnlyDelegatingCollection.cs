using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200044A RID: 1098
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal abstract class ReadOnlyDelegatingCollection<T> : ICollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060030F9 RID: 12537 RVA: 0x000C8E65 File Offset: 0x000C7065
		public void Add(T item)
		{
			throw ReadOnlyDelegatingCollection<T>.ReadOnlyViolation();
		}

		// Token: 0x060030FA RID: 12538 RVA: 0x000C8E6C File Offset: 0x000C706C
		public void Clear()
		{
			throw ReadOnlyDelegatingCollection<T>.ReadOnlyViolation();
		}

		// Token: 0x060030FB RID: 12539 RVA: 0x000C8E74 File Offset: 0x000C7074
		public virtual bool Contains(T item)
		{
			foreach (T y in this)
			{
				if (EqualityComparer<T>.Default.Equals(item, y))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060030FC RID: 12540 RVA: 0x000C8ECC File Offset: 0x000C70CC
		public virtual void CopyTo(T[] array, int arrayIndex)
		{
			Util.ThrowOnNullArgument(array, "array");
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex");
			}
			if (arrayIndex + this.Count > array.Length)
			{
				throw new ArgumentException("Cannot fit all elements of a collection into the given array", "array");
			}
			foreach (T t in this)
			{
				array[arrayIndex++] = t;
			}
		}

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x060030FD RID: 12541
		public abstract int Count { get; }

		// Token: 0x17000F5D RID: 3933
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x000C8F54 File Offset: 0x000C7154
		public bool IsReadOnly
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000C8F57 File Offset: 0x000C7157
		public bool Remove(T item)
		{
			throw ReadOnlyDelegatingCollection<T>.ReadOnlyViolation();
		}

		// Token: 0x06003100 RID: 12544
		public abstract IEnumerator<T> GetEnumerator();

		// Token: 0x06003101 RID: 12545 RVA: 0x000C8F5E File Offset: 0x000C715E
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000C8F66 File Offset: 0x000C7166
		private static Exception ReadOnlyViolation()
		{
			return new NotSupportedException("Collection is read-only");
		}
	}
}
