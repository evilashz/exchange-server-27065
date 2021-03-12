using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000363 RID: 867
	[DataContract]
	public class UMDialPlanFilterWithIdentity : WebServiceParameters
	{
		// Token: 0x17001F1A RID: 7962
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x00091D54 File Offset: 0x0008FF54
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-UMDialPlan";
			}
		}

		// Token: 0x17001F1B RID: 7963
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x00091D5B File Offset: 0x0008FF5B
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x17001F1C RID: 7964
		// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x00091D62 File Offset: 0x0008FF62
		// (set) Token: 0x06002FE1 RID: 12257 RVA: 0x00091D6A File Offset: 0x0008FF6A
		[DataMember]
		public Identity DialPlanIdentity { get; set; }

		// Token: 0x17001F1D RID: 7965
		// (get) Token: 0x06002FE2 RID: 12258 RVA: 0x00091D73 File Offset: 0x0008FF73
		// (set) Token: 0x06002FE3 RID: 12259 RVA: 0x00091D7B File Offset: 0x0008FF7B
		[DataMember]
		public bool IsInternational { get; set; }
	}
}
