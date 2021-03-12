using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001B9 RID: 441
	[Cmdlet("DisasterRecovery", "FrontendTransportRole", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class DisasterRecoveryFrontendTransportRole : ManageRole
	{
		// Token: 0x06000F66 RID: 3942 RVA: 0x00044339 File Offset: 0x00042539
		public DisasterRecoveryFrontendTransportRole()
		{
			this.StartTransportService = true;
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000F67 RID: 3943 RVA: 0x00044348 File Offset: 0x00042548
		protected override LocalizedString Description
		{
			get
			{
				return Strings.DisasterRecoveryFrontendTransportRoleDescription;
			}
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000F68 RID: 3944 RVA: 0x0004434F File Offset: 0x0004254F
		// (set) Token: 0x06000F69 RID: 3945 RVA: 0x00044366 File Offset: 0x00042566
		[Parameter(Mandatory = false)]
		public bool StartTransportService
		{
			get
			{
				return (bool)base.Fields["StartTransportService"];
			}
			set
			{
				base.Fields["StartTransportService"] = value;
			}
		}
	}
}
