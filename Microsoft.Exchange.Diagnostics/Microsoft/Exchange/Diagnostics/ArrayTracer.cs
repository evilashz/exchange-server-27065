using System;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000071 RID: 113
	public class ArrayTracer<T>
	{
		// Token: 0x060001FA RID: 506 RVA: 0x00006AC1 File Offset: 0x00004CC1
		public ArrayTracer(T[] array) : this(array, int.MaxValue)
		{
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006ACF File Offset: 0x00004CCF
		public ArrayTracer(T[] array, int limit)
		{
			this.array = array;
			this.limit = limit;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006AE8 File Offset: 0x00004CE8
		public override string ToString()
		{
			if (this.array != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("<array length=" + this.array.Length + ">");
				for (int i = 0; i < this.array.Length; i++)
				{
					if (i >= this.limit)
					{
						stringBuilder.Append(" ...");
						break;
					}
					stringBuilder.Append(string.Concat(new object[]
					{
						" [",
						i,
						"]=",
						this.array[i].ToString(),
						";"
					}));
				}
				return stringBuilder.ToString();
			}
			return "<null>";
		}

		// Token: 0x04000245 RID: 581
		private T[] array;

		// Token: 0x04000246 RID: 582
		private int limit;
	}
}
