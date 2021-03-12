using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000294 RID: 660
	public class SetCasMailbox : SetObjectProperties
	{
		// Token: 0x17001D5A RID: 7514
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x000868B8 File Offset: 0x00084AB8
		// (set) Token: 0x06002B29 RID: 11049 RVA: 0x000868CA File Offset: 0x00084ACA
		[DataMember]
		public bool? ActiveSyncEnabled
		{
			get
			{
				return (bool?)base["ActiveSyncEnabled"];
			}
			set
			{
				base["ActiveSyncEnabled"] = value;
			}
		}

		// Token: 0x17001D5B RID: 7515
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x000868DD File Offset: 0x00084ADD
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-CasMailbox";
			}
		}

		// Token: 0x17001D5C RID: 7516
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x000868E4 File Offset: 0x00084AE4
		public override string RbacScope
		{
			get
			{
				return "@W:Self|Organization";
			}
		}
	}
}
