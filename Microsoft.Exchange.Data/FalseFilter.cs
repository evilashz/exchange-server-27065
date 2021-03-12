using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000042 RID: 66
	[Serializable]
	internal class FalseFilter : QueryFilter
	{
		// Token: 0x060001FD RID: 509 RVA: 0x00008118 File Offset: 0x00006318
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(False)");
		}
	}
}
