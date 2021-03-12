using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000436 RID: 1078
	[Cmdlet("get", "ActiveSyncMailboxPolicy", DefaultParameterSetName = "Identity")]
	public class GetActiveSyncMailboxPolicy : GetMailboxPolicyBase<ActiveSyncMailboxPolicy>
	{
		// Token: 0x0600260C RID: 9740 RVA: 0x00097E82 File Offset: 0x00096082
		protected override void InternalProcessRecord()
		{
			this.WriteWarning(Strings.WarningCmdletIsDeprecated("Get-ActiveSyncMailboxPolicy", "Get-MobileDeviceMailboxPolicy"));
			base.InternalProcessRecord();
		}
	}
}
