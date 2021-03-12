using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200029D RID: 669
	[DataContract]
	public class EmailSubscriptionFilter : SelfMailboxParameters
	{
		// Token: 0x17001D6D RID: 7533
		// (get) Token: 0x06002B6D RID: 11117 RVA: 0x00087C00 File Offset: 0x00085E00
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-Subscription";
			}
		}

		// Token: 0x17001D6E RID: 7534
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x00087C07 File Offset: 0x00085E07
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}
