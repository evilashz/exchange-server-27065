using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000422 RID: 1058
	[DataContract]
	public class MessageClassificationFilter : WebServiceParameters
	{
		// Token: 0x170020EF RID: 8431
		// (get) Token: 0x06003555 RID: 13653 RVA: 0x000A5A0E File Offset: 0x000A3C0E
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-MessageClassification";
			}
		}

		// Token: 0x170020F0 RID: 8432
		// (get) Token: 0x06003556 RID: 13654 RVA: 0x000A5A15 File Offset: 0x000A3C15
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}
