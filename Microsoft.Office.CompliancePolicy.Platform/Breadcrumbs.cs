using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Microsoft.Office.CompliancePolicy
{
	// Token: 0x02000036 RID: 54
	internal sealed class Breadcrumbs<T> : IEnumerable<T>, IEnumerable
	{
		// Token: 0x060000BA RID: 186 RVA: 0x000034FC File Offset: 0x000016FC
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
			throw new ArgumentException("The size of the breadcrumb trail must be either 8, 16, 32, 64, 128, or 256", "size");
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003567 File Offset: 0x00001767
		public int LastFilledIndex
		{
			get
			{
				return this.index & this.mask;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003576 File Offset: 0x00001776
		public T[] BreadCrumb
		{
			get
			{
				return this.trail;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003580 File Offset: 0x00001780
		public void Drop(T bc)
		{
			int num = Interlocked.Increment(ref this.index);
			this.trail[num & this.mask] = bc;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000035B0 File Offset: 0x000017B0
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

		// Token: 0x060000BF RID: 191 RVA: 0x00003730 File Offset: 0x00001930
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

		// Token: 0x060000C0 RID: 192 RVA: 0x0000374C File Offset: 0x0000194C
		IEnumerator IEnumerable.GetEnumerator()
		{
			return ((IEnumerable<!0>)this).GetEnumerator();
		}

		// Token: 0x04000061 RID: 97
		private readonly int mask;

		// Token: 0x04000062 RID: 98
		private int index = -1;

		// Token: 0x04000063 RID: 99
		private T[] trail;
	}
}
