using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001F9 RID: 505
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Install", "OSPRole", SupportsShouldProcess = true)]
	public sealed class InstallOSPRole : ManageRole
	{
		// Token: 0x06001145 RID: 4421 RVA: 0x0004C3E8 File Offset: 0x0004A5E8
		public InstallOSPRole()
		{
			base.Fields["CustomerFeedbackEnabled"] = null;
		}

		// Token: 0x17000545 RID: 1349
		// (get) Token: 0x06001146 RID: 4422 RVA: 0x0004C401 File Offset: 0x0004A601
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallOSPRoleDescription;
			}
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x0004C408 File Offset: 0x0004A608
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x0004C41F File Offset: 0x0004A61F
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
