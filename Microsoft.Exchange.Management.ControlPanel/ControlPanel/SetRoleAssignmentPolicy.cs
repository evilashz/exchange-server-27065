using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000534 RID: 1332
	[DataContract]
	public class SetRoleAssignmentPolicy : SetObjectProperties
	{
		// Token: 0x170024A6 RID: 9382
		// (get) Token: 0x06003F23 RID: 16163 RVA: 0x000BE354 File Offset: 0x000BC554
		public override string AssociatedCmdlet
		{
			get
			{
				return "Set-RoleAssignmentPolicy";
			}
		}

		// Token: 0x170024A7 RID: 9383
		// (get) Token: 0x06003F24 RID: 16164 RVA: 0x000BE35B File Offset: 0x000BC55B
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x170024A8 RID: 9384
		// (get) Token: 0x06003F25 RID: 16165 RVA: 0x000BE362 File Offset: 0x000BC562
		// (set) Token: 0x06003F26 RID: 16166 RVA: 0x000BE374 File Offset: 0x000BC574
		[DataMember]
		public string Name
		{
			get
			{
				return (string)base[ADObjectSchema.Name];
			}
			set
			{
				base[ADObjectSchema.Name] = value;
			}
		}

		// Token: 0x170024A9 RID: 9385
		// (get) Token: 0x06003F27 RID: 16167 RVA: 0x000BE382 File Offset: 0x000BC582
		// (set) Token: 0x06003F28 RID: 16168 RVA: 0x000BE394 File Offset: 0x000BC594
		[DataMember]
		public string Description
		{
			get
			{
				return (string)base[RoleAssignmentPolicySchema.Description];
			}
			set
			{
				base[RoleAssignmentPolicySchema.Description] = value;
			}
		}

		// Token: 0x170024AA RID: 9386
		// (get) Token: 0x06003F29 RID: 16169 RVA: 0x000BE3A2 File Offset: 0x000BC5A2
		// (set) Token: 0x06003F2A RID: 16170 RVA: 0x000BE3AA File Offset: 0x000BC5AA
		[DataMember]
		public IEnumerable<Identity> AssignedEndUserRoles { get; set; }
	}
}
