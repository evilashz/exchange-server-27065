using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000427 RID: 1063
	[DataContract]
	public class RmsTemplateFilter : WebServiceParameters
	{
		// Token: 0x170020F1 RID: 8433
		// (get) Token: 0x06003560 RID: 13664 RVA: 0x000A5EFA File Offset: 0x000A40FA
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-RMSTemplate";
			}
		}

		// Token: 0x170020F2 RID: 8434
		// (get) Token: 0x06003561 RID: 13665 RVA: 0x000A5F01 File Offset: 0x000A4101
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}
	}
}
