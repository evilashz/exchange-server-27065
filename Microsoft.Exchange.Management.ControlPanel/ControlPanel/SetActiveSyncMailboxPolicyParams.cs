using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002F8 RID: 760
	[DataContract]
	public class SetActiveSyncMailboxPolicyParams : BaseActiveSyncMailboxPolicyParams
	{
		// Token: 0x17001E84 RID: 7812
		// (get) Token: 0x06002E04 RID: 11780 RVA: 0x0008C361 File Offset: 0x0008A561
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-MobileMailboxPolicy";
			}
		}

		// Token: 0x06002E05 RID: 11781 RVA: 0x0008C368 File Offset: 0x0008A568
		public override void ProcessPolicyParams(ActiveSyncMailboxPolicyObject originalPolicy)
		{
			if (originalPolicy == null)
			{
				throw new ArgumentNullException("originalPolicy");
			}
			base.ProcessPolicyParams(originalPolicy);
		}
	}
}
