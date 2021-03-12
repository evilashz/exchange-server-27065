using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000041 RID: 65
	[Serializable]
	internal class TrueFilter : QueryFilter
	{
		// Token: 0x060001FB RID: 507 RVA: 0x00008102 File Offset: 0x00006302
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(True)");
		}
	}
}
