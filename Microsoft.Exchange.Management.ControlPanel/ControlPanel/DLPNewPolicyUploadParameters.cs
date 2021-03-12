using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001BD RID: 445
	[DataContract]
	public class DLPNewPolicyUploadParameters : DLPPolicyUploadParameters
	{
		// Token: 0x17001AFA RID: 6906
		// (get) Token: 0x06002404 RID: 9220 RVA: 0x0006E637 File Offset: 0x0006C837
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-DLPPolicy";
			}
		}

		// Token: 0x17001AFB RID: 6907
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x0006E63E File Offset: 0x0006C83E
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x17001AFC RID: 6908
		// (get) Token: 0x06002406 RID: 9222 RVA: 0x0006E645 File Offset: 0x0006C845
		// (set) Token: 0x06002407 RID: 9223 RVA: 0x0006E657 File Offset: 0x0006C857
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base["Name"];
			}
			set
			{
				base["Name"] = value;
			}
		}

		// Token: 0x17001AFD RID: 6909
		// (get) Token: 0x06002408 RID: 9224 RVA: 0x0006E665 File Offset: 0x0006C865
		// (set) Token: 0x06002409 RID: 9225 RVA: 0x0006E677 File Offset: 0x0006C877
		[DataMember]
		public string Description
		{
			get
			{
				return (string)base["Description"];
			}
			set
			{
				base["Description"] = value;
			}
		}
	}
}
