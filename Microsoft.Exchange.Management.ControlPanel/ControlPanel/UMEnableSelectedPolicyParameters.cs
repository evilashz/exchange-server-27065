using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004C8 RID: 1224
	[DataContract]
	public class UMEnableSelectedPolicyParameters : WebServiceParameters
	{
		// Token: 0x170023A6 RID: 9126
		// (get) Token: 0x06003BF5 RID: 15349 RVA: 0x000B4C2D File Offset: 0x000B2E2D
		public override string AssociatedCmdlet
		{
			get
			{
				return "Enable-UMMailbox";
			}
		}

		// Token: 0x170023A7 RID: 9127
		// (get) Token: 0x06003BF6 RID: 15350 RVA: 0x000B4C34 File Offset: 0x000B2E34
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x170023A8 RID: 9128
		// (get) Token: 0x06003BF7 RID: 15351 RVA: 0x000B4C3B File Offset: 0x000B2E3B
		// (set) Token: 0x06003BF8 RID: 15352 RVA: 0x000B4C4D File Offset: 0x000B2E4D
		[DataMember]
		public Identity UMMailboxPolicy
		{
			get
			{
				return (Identity)base["UMMailboxPolicy"];
			}
			set
			{
				base["UMMailboxPolicy"] = value;
			}
		}
	}
}
