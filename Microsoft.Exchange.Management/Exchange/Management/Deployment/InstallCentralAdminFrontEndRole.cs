using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001E4 RID: 484
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Install", "CentralAdminFrontEndRole", SupportsShouldProcess = true)]
	public sealed class InstallCentralAdminFrontEndRole : ManageRole
	{
		// Token: 0x0600108A RID: 4234 RVA: 0x000496E7 File Offset: 0x000478E7
		public InstallCentralAdminFrontEndRole()
		{
			base.Fields["CustomerFeedbackEnabled"] = null;
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x00049700 File Offset: 0x00047900
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallCentralAdminFrontEndRoleDescription;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00049707 File Offset: 0x00047907
		// (set) Token: 0x0600108D RID: 4237 RVA: 0x0004971E File Offset: 0x0004791E
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
