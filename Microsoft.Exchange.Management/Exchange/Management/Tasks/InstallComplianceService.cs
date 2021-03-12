using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000148 RID: 328
	[LocDescription(Strings.IDs.InstallComplianceServiceTask)]
	[Cmdlet("Install", "ComplianceService")]
	public sealed class InstallComplianceService : ManageComplianceService
	{
		// Token: 0x06000BCD RID: 3021 RVA: 0x00036F86 File Offset: 0x00035186
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
