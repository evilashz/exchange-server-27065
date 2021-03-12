using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002AE RID: 686
	[DataContract]
	public class GetHotmailSubscription : SelfMailboxParameters
	{
		// Token: 0x17001D9A RID: 7578
		// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x00088569 File Offset: 0x00086769
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-HotmailSubscription";
			}
		}

		// Token: 0x17001D9B RID: 7579
		// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x00088570 File Offset: 0x00086770
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}
