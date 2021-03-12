using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020004C5 RID: 1221
	[DataContract]
	public class SetUMMailboxPinParameters : UMBasePinSetParameteres
	{
		// Token: 0x170023A4 RID: 9124
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x000B4B62 File Offset: 0x000B2D62
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-UMMailboxPin";
			}
		}

		// Token: 0x170023A5 RID: 9125
		// (get) Token: 0x06003BF0 RID: 15344 RVA: 0x000B4B69 File Offset: 0x000B2D69
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}
	}
}
