using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000C8 RID: 200
	[DataContract]
	public class SetVoiceMailPIN : SetObjectProperties
	{
		// Token: 0x17001944 RID: 6468
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x000599FD File Offset: 0x00057BFD
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-UMMailboxPIN";
			}
		}

		// Token: 0x17001945 RID: 6469
		// (get) Token: 0x06001D31 RID: 7473 RVA: 0x00059A04 File Offset: 0x00057C04
		public override string RbacScope
		{
			get
			{
				return "@W:Self";
			}
		}

		// Token: 0x17001946 RID: 6470
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x00059A0B File Offset: 0x00057C0B
		// (set) Token: 0x06001D33 RID: 7475 RVA: 0x00059A1D File Offset: 0x00057C1D
		[DataMember]
		public string PIN
		{
			get
			{
				return (string)base["Pin"];
			}
			set
			{
				base["Pin"] = value;
				base["SendEmail"] = false;
				base["PinExpired"] = false;
			}
		}
	}
}
