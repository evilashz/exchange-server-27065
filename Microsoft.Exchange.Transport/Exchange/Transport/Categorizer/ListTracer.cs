using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000273 RID: 627
	internal class ListTracer<T>
	{
		// Token: 0x06001B3E RID: 6974 RVA: 0x0006FBF2 File Offset: 0x0006DDF2
		public ListTracer(IList<T> source)
		{
			this.list = source;
		}

		// Token: 0x06001B3F RID: 6975 RVA: 0x0006FC04 File Offset: 0x0006DE04
		public override string ToString()
		{
			if (this.list == null)
			{
				return "<null>";
			}
			StringBuilder stringBuilder = new StringBuilder("List length=");
			stringBuilder.Append(this.list.Count);
			stringBuilder.Append(" { ");
			int num = 0;
			foreach (T t in this.list)
			{
				if (num++ > 0)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append(t.ToString());
			}
			stringBuilder.Append(" }");
			return stringBuilder.ToString();
		}

		// Token: 0x04000CDB RID: 3291
		private IList<T> list;
	}
}
