using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001E0 RID: 480
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Install", "BridgeheadRole", SupportsShouldProcess = true)]
	public sealed class InstallBridgeheadRole : ManageBridgeheadRole
	{
		// Token: 0x06001075 RID: 4213 RVA: 0x000490B7 File Offset: 0x000472B7
		public InstallBridgeheadRole()
		{
			base.Fields["CustomerFeedbackEnabled"] = null;
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x000490D0 File Offset: 0x000472D0
		protected override LocalizedString Description
		{
			get
			{
				return Strings.InstallBridgeheadRoleDescription;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06001077 RID: 4215 RVA: 0x000490D7 File Offset: 0x000472D7
		// (set) Token: 0x06001078 RID: 4216 RVA: 0x000490EE File Offset: 0x000472EE
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
