using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002FA RID: 762
	[DataContract]
	public class ActiveSyncMailboxPolicyFilter : WebServiceParameters
	{
		// Token: 0x17001E86 RID: 7814
		// (get) Token: 0x06002E0A RID: 11786 RVA: 0x0008C39F File Offset: 0x0008A59F
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-MobileMailboxPolicy";
			}
		}

		// Token: 0x17001E87 RID: 7815
		// (get) Token: 0x06002E0B RID: 11787 RVA: 0x0008C3A6 File Offset: 0x0008A5A6
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}
	}
}
