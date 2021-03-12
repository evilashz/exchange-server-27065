using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000ED RID: 237
	public class Breadcrumbs<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x060006B6 RID: 1718 RVA: 0x0001B63C File Offset: 0x0001983C
		public Breadcrumbs(int size)
		{
			if (size <= 32)
			{
				if (size != 8 && size != 16 && size != 32)
				{
					goto IL_4F;
				}
			}
			else if (size != 64 && size != 128 && size != 256)
			{
				goto IL_4F;
			}
			this.trail = new T[size];
			this.mask = size - 1;
			return;
			IL_4F:
			throw new ArgumentException(DiagnosticsResources.BreadCrumbSize, "size");
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0001B6AC File Offset: 0x000198AC
		public int LastFilledIndex
		{
			get
			{
				return this.index & this.mask;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060006B8 RID: 1720 RVA: 0x0001B6BB File Offset: 0x000198BB
		public T[] BreadCrumb
		{
			get
			{
				return this.trail;
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0001B6C4 File Offset: 0x000198C4
		public void Drop(T bc)
		{
			int num = Interlocked.Increment(ref this.index);
			this.trail[num & this.mask] = bc;
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x0001B6F4 File Offset: 0x000198F4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.trail.Length * 32);
			int num = this.index;
			int num2 = Math.Max(0, num - this.trail.Length);
			stringBuilder.AppendFormat("LastIndex: {0}{1}", num, Environment.NewLine);
			for (int i = num2; i <= num; i++)
			{
				stringBuilder.AppendFormat("{0}: {1}{2}", i, this.trail[i & this.mask], Environment.NewLine);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x0001B874 File Offset: 0x00019A74
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			int last = this.index;
			int first = Math.Max(0, last - this.trail.Length);
			for (int i = first; i <= last; i++)
			{
				yield return this.trail[i & this.mask];
			}
			yield break;
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0001B890 File Offset: 0x00019A90
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		// Token: 0x0400047F RID: 1151
		private readonly int mask;

		// Token: 0x04000480 RID: 1152
		private int index = -1;

		// Token: 0x04000481 RID: 1153
		private T[] trail;
	}
}
