using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001F8 RID: 504
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Install", "MonitoringRole", SupportsShouldProcess = true)]
	public sealed class InstallMonitoringRole : ManageMonitoringRole
	{
		// Token: 0x06001141 RID: 4417 RVA: 0x0004C399 File Offset: 0x0004A599
		public InstallMonitoringRole()
		{
			base.Fields["CustomerFeedbackEnabled"] = null;
		}

		// Token: 0x17000543 RID: 1347
		// (get) Token: 0x06001142 RID: 4418 RVA: 0x0004C3B2 File Offset: 0x0004A5B2
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallMonitoringRoleDescription;
			}
		}

		// Token: 0x17000544 RID: 1348
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x0004C3B9 File Offset: 0x0004A5B9
		// (set) Token: 0x06001144 RID: 4420 RVA: 0x0004C3D0 File Offset: 0x0004A5D0
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
