using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000532 RID: 1330
	[DataContract]
	public class RoleAssignmentPolicyRow : BaseRow
	{
		// Token: 0x170024A1 RID: 9377
		// (get) Token: 0x06003F16 RID: 16150 RVA: 0x000BE0E3 File Offset: 0x000BC2E3
		// (set) Token: 0x06003F17 RID: 16151 RVA: 0x000BE0EB File Offset: 0x000BC2EB
		[DataMember]
		public string Name { get; private set; }

		// Token: 0x06003F18 RID: 16152 RVA: 0x000BE0F4 File Offset: 0x000BC2F4
		public RoleAssignmentPolicyRow(RoleAssignmentPolicy policy) : base(policy)
		{
			this.Name = policy.Name;
		}
	}
}
