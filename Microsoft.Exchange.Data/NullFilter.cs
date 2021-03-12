using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000046 RID: 70
	[Serializable]
	internal class NullFilter : QueryFilter
	{
		// Token: 0x06000213 RID: 531 RVA: 0x000084FE File Offset: 0x000066FE
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(Null)");
		}
	}
}
