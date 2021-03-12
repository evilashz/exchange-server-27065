using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001BB RID: 443
	[Cmdlet("DisasterRecovery", "GatewayRole", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class DisasterRecoveryGatewayRole : ManageGatewayRole
	{
		// Token: 0x06000F6B RID: 3947 RVA: 0x00044386 File Offset: 0x00042586
		public DisasterRecoveryGatewayRole()
		{
			this.StartTransportService = true;
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000F6C RID: 3948 RVA: 0x00044395 File Offset: 0x00042595
		protected override LocalizedString Description
		{
			get
			{
				return Strings.DisasterRecoveryGatewayRoleDescription;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000F6D RID: 3949 RVA: 0x0004439C File Offset: 0x0004259C
		// (set) Token: 0x06000F6E RID: 3950 RVA: 0x000443B3 File Offset: 0x000425B3
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
