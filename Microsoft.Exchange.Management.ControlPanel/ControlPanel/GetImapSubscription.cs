using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002B6 RID: 694
	[DataContract]
	public class GetImapSubscription : SelfMailboxParameters
	{
		// Token: 0x17001DAE RID: 7598
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x000887E5 File Offset: 0x000869E5
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-ImapSubscription";
			}
		}

		// Token: 0x17001DAF RID: 7599
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000887EC File Offset: 0x000869EC
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}
