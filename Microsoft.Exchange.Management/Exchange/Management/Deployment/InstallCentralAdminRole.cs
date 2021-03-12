using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001E5 RID: 485
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Install", "CentralAdminRole", SupportsShouldProcess = true)]
	public sealed class InstallCentralAdminRole : ManageRole
	{
		// Token: 0x0600108E RID: 4238 RVA: 0x00049736 File Offset: 0x00047936
		public InstallCentralAdminRole()
		{
			base.Fields["CustomerFeedbackEnabled"] = null;
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600108F RID: 4239 RVA: 0x0004974F File Offset: 0x0004794F
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallCentralAdminRoleDescription;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x00049756 File Offset: 0x00047956
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x0004976D File Offset: 0x0004796D
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
