using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B17 RID: 2839
	[Cmdlet("Set", "RemoteAccountPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetRemoteAccountPolicy : SetSystemConfigurationObjectTask<RemoteAccountPolicyIdParameter, RemoteAccountPolicy>
	{
		// Token: 0x17001EA5 RID: 7845
		// (get) Token: 0x060064CE RID: 25806 RVA: 0x001A4BFB File Offset: 0x001A2DFB
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetRemoteAccountPolicy(this.Identity.ToString());
			}
		}
	}
}
