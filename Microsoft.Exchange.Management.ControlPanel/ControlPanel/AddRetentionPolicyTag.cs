using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000271 RID: 625
	[DataContract]
	public class AddRetentionPolicyTag : SetObjectProperties
	{
		// Token: 0x17001CB8 RID: 7352
		// (get) Token: 0x06002996 RID: 10646 RVA: 0x00082D91 File Offset: 0x00080F91
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-RetentionPolicyTag";
			}
		}

		// Token: 0x17001CB9 RID: 7353
		// (get) Token: 0x06002997 RID: 10647 RVA: 0x00082D98 File Offset: 0x00080F98
		public override string RbacScope
		{
			get
			{
				return "@C:OrganizationConfig";
			}
		}

		// Token: 0x17001CBA RID: 7354
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x00082D9F File Offset: 0x00080F9F
		// (set) Token: 0x06002999 RID: 10649 RVA: 0x00082DA7 File Offset: 0x00080FA7
		[DataMember]
		public Identity[] OptionalInMailbox { get; set; }
	}
}
