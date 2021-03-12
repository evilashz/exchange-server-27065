using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001F0 RID: 496
	[Cmdlet("Install", "FrontendTransportRole", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class InstallFrontendTransportRole : ManageRole
	{
		// Token: 0x060010E7 RID: 4327 RVA: 0x0004A807 File Offset: 0x00048A07
		public InstallFrontendTransportRole()
		{
			this.StartTransportService = true;
			base.Fields["CustomerFeedbackEnabled"] = null;
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x060010E8 RID: 4328 RVA: 0x0004A827 File Offset: 0x00048A27
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallFrontendTransportRoleDescription;
			}
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x060010E9 RID: 4329 RVA: 0x0004A82E File Offset: 0x00048A2E
		// (set) Token: 0x060010EA RID: 4330 RVA: 0x0004A845 File Offset: 0x00048A45
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

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x060010EB RID: 4331 RVA: 0x0004A85D File Offset: 0x00048A5D
		// (set) Token: 0x060010EC RID: 4332 RVA: 0x0004A874 File Offset: 0x00048A74
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
	}
}
