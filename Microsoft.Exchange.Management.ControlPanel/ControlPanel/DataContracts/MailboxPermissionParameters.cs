using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel.DataContracts
{
	// Token: 0x020000D8 RID: 216
	[DataContract]
	public abstract class MailboxPermissionParameters : SetObjectProperties
	{
		// Token: 0x17001959 RID: 6489
		// (get) Token: 0x06001D85 RID: 7557 RVA: 0x0005A67A File Offset: 0x0005887A
		// (set) Token: 0x06001D86 RID: 7558 RVA: 0x0005A68C File Offset: 0x0005888C
		[DataMember]
		public Identity Identity
		{
			get
			{
				return (Identity)base["Identity"];
			}
			set
			{
				base["Identity"] = value;
			}
		}
	}
}
