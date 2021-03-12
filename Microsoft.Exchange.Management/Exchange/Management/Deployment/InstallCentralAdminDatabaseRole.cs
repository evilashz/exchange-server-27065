using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001E3 RID: 483
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Install", "CentralAdminDatabaseRole", SupportsShouldProcess = true)]
	public sealed class InstallCentralAdminDatabaseRole : ManageRole
	{
		// Token: 0x06001086 RID: 4230 RVA: 0x00049698 File Offset: 0x00047898
		public InstallCentralAdminDatabaseRole()
		{
			base.Fields["CustomerFeedbackEnabled"] = null;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x000496B1 File Offset: 0x000478B1
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallCentralAdminDatabaseRoleDescription;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x000496B8 File Offset: 0x000478B8
		// (set) Token: 0x06001089 RID: 4233 RVA: 0x000496CF File Offset: 0x000478CF
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
