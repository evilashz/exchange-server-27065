using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001F1 RID: 497
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Install", "GatewayRole", SupportsShouldProcess = true)]
	public sealed class InstallGatewayRole : ManageGatewayRole
	{
		// Token: 0x060010ED RID: 4333 RVA: 0x0004A88C File Offset: 0x00048A8C
		public InstallGatewayRole()
		{
			this.AdamLdapPort = 50389;
			this.AdamSslPort = 50636;
			this.StartTransportService = true;
			base.Fields["CustomerFeedbackEnabled"] = null;
			base.Fields["Industry"] = IndustryType.NotSpecified;
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x060010EE RID: 4334 RVA: 0x0004A8E3 File Offset: 0x00048AE3
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallGatewayRoleDescription;
			}
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x0004A8EA File Offset: 0x00048AEA
		// (set) Token: 0x060010F0 RID: 4336 RVA: 0x0004A901 File Offset: 0x00048B01
		[Parameter(Mandatory = false)]
		public ushort AdamLdapPort
		{
			get
			{
				return (ushort)base.Fields["AdamLdapPort"];
			}
			set
			{
				base.Fields["AdamLdapPort"] = value;
			}
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x0004A919 File Offset: 0x00048B19
		// (set) Token: 0x060010F2 RID: 4338 RVA: 0x0004A930 File Offset: 0x00048B30
		[Parameter(Mandatory = false)]
		public ushort AdamSslPort
		{
			get
			{
				return (ushort)base.Fields["AdamSslPort"];
			}
			set
			{
				base.Fields["AdamSslPort"] = value;
			}
		}

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0004A948 File Offset: 0x00048B48
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x0004A95F File Offset: 0x00048B5F
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

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0004A977 File Offset: 0x00048B77
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x0004A98E File Offset: 0x00048B8E
		[Parameter(Mandatory = false)]
		public bool? CustomerFeedbackEnabled
		{
			get
			{
				return (bool?)base.Fields["CustomerFeedbackEnabled"];
			}
			set
			{
				base.Fields["CustomerFeedbackEnabled"] = value;
			}
		}

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0004A9A6 File Offset: 0x00048BA6
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x0004A9BD File Offset: 0x00048BBD
		[Parameter(Mandatory = false)]
		public IndustryType Industry
		{
			get
			{
				return (IndustryType)base.Fields["Industry"];
			}
			set
			{
				base.Fields["Industry"] = value;
			}
		}
	}
}
