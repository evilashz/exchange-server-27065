using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002D8 RID: 728
	[DataContract]
	public class GetPopSubscription : SelfMailboxParameters
	{
		// Token: 0x17001E00 RID: 7680
		// (get) Token: 0x06002CC0 RID: 11456 RVA: 0x00089A39 File Offset: 0x00087C39
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-PopSubscription";
			}
		}

		// Token: 0x17001E01 RID: 7681
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x00089A40 File Offset: 0x00087C40
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}
