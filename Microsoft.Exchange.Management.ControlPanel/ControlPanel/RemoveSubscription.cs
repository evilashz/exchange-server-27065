using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002A0 RID: 672
	[DataContract]
	public class RemoveSubscription : SelfMailboxParameters
	{
		// Token: 0x17001D75 RID: 7541
		// (get) Token: 0x06002B7C RID: 11132 RVA: 0x00087CB2 File Offset: 0x00085EB2
		public override string AssociatedCmdlet
		{
			get
			{
				return "Remove-Subscription";
			}
		}

		// Token: 0x17001D76 RID: 7542
		// (get) Token: 0x06002B7D RID: 11133 RVA: 0x00087CB9 File Offset: 0x00085EB9
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}
	}
}
