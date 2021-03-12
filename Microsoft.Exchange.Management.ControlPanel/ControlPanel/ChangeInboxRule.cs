using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003FF RID: 1023
	[DataContract]
	public class ChangeInboxRule : WebServiceParameters
	{
		// Token: 0x170020A9 RID: 8361
		// (get) Token: 0x060034A1 RID: 13473 RVA: 0x000A3826 File Offset: 0x000A1A26
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-InboxRule";
			}
		}

		// Token: 0x170020AA RID: 8362
		// (get) Token: 0x060034A2 RID: 13474 RVA: 0x000A382D File Offset: 0x000A1A2D
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x170020AB RID: 8363
		// (get) Token: 0x060034A3 RID: 13475 RVA: 0x000A3834 File Offset: 0x000A1A34
		public override string SuppressConfirmParameterName
		{
			get
			{
				return "AlwaysDeleteOutlookRulesBlob";
			}
		}
	}
}
