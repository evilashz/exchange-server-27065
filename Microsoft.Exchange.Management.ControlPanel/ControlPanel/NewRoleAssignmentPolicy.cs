using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000535 RID: 1333
	[DataContract]
	public class NewRoleAssignmentPolicy : SetObjectProperties
	{
		// Token: 0x170024AB RID: 9387
		// (get) Token: 0x06003F2C RID: 16172 RVA: 0x000BE3BB File Offset: 0x000BC5BB
		public override string AssociatedCmdlet
		{
			get
			{
				return "New-RoleAssignmentPolicy";
			}
		}

		// Token: 0x170024AC RID: 9388
		// (get) Token: 0x06003F2D RID: 16173 RVA: 0x000BE3C2 File Offset: 0x000BC5C2
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x170024AD RID: 9389
		// (get) Token: 0x06003F2E RID: 16174 RVA: 0x000BE3C9 File Offset: 0x000BC5C9
		// (set) Token: 0x06003F2F RID: 16175 RVA: 0x000BE3DB File Offset: 0x000BC5DB
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

		// Token: 0x170024AE RID: 9390
		// (get) Token: 0x06003F30 RID: 16176 RVA: 0x000BE3E9 File Offset: 0x000BC5E9
		// (set) Token: 0x06003F31 RID: 16177 RVA: 0x000BE3FB File Offset: 0x000BC5FB
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

		// Token: 0x170024AF RID: 9391
		// (get) Token: 0x06003F32 RID: 16178 RVA: 0x000BE409 File Offset: 0x000BC609
		// (set) Token: 0x06003F33 RID: 16179 RVA: 0x000BE41B File Offset: 0x000BC61B
		[DataMember]
		public IEnumerable<Identity> Roles
		{
			get
			{
				return (IEnumerable<Identity>)base["Roles"];
			}
			set
			{
				if (value != null && value.Count<Identity>() > 0)
				{
					base["Roles"] = value;
				}
			}
		}
	}
}
