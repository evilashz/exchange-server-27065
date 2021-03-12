using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000536 RID: 1334
	[DataContract]
	public class RoleAssignmentPolicyFilter : WebServiceParameters
	{
		// Token: 0x170024B0 RID: 9392
		// (get) Token: 0x06003F35 RID: 16181 RVA: 0x000BE43D File Offset: 0x000BC63D
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-RoleAssignmentPolicy";
			}
		}

		// Token: 0x170024B1 RID: 9393
		// (get) Token: 0x06003F36 RID: 16182 RVA: 0x000BE444 File Offset: 0x000BC644
		public override string RbacScope
		{
			get
			{
				return "@R:Organization";
			}
		}

		// Token: 0x170024B2 RID: 9394
		// (get) Token: 0x06003F37 RID: 16183 RVA: 0x000BE44B File Offset: 0x000BC64B
		// (set) Token: 0x06003F38 RID: 16184 RVA: 0x000BE453 File Offset: 0x000BC653
		[DataMember]
		public Identity Policy { get; set; }
	}
}
