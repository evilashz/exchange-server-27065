using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002AD RID: 685
	[DataContract]
	public class SetHotmailSubscription : PimSubscriptionParameter
	{
		// Token: 0x17001D98 RID: 7576
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x00088553 File Offset: 0x00086753
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-HotmailSubscription";
			}
		}

		// Token: 0x17001D99 RID: 7577
		// (get) Token: 0x06002BD1 RID: 11217 RVA: 0x0008855A File Offset: 0x0008675A
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}
	}
}
