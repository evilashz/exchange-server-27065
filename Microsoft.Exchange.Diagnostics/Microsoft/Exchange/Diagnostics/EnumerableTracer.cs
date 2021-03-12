using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000082 RID: 130
	public class EnumerableTracer<T>
	{
		// Token: 0x060002ED RID: 749 RVA: 0x0000A7C6 File Offset: 0x000089C6
		public EnumerableTracer(IEnumerable<T> data) : this(data, -1)
		{
		}

		// Token: 0x060002EE RID: 750 RVA: 0x0000A7D0 File Offset: 0x000089D0
		public EnumerableTracer(IEnumerable<T> data, int limit)
		{
			this.data = data;
			this.limit = limit;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x0000A7E8 File Offset: 0x000089E8
		public override string ToString()
		{
			if (this.data != null && this.limit != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int num = this.limit;
				foreach (T t in this.data)
				{
					if (num == 0)
					{
						stringBuilder.Append(", ...");
						break;
					}
					if (num != this.limit)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(t.ToString());
					num--;
				}
				return stringBuilder.ToString();
			}
			return "<null>";
		}

		// Token: 0x040002B1 RID: 689
		private IEnumerable<T> data;

		// Token: 0x040002B2 RID: 690
		private int limit;
	}
}
