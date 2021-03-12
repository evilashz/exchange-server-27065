using System;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000C1C RID: 3100
	public abstract class ManageReportingVirtualDirectory : Task
	{
		// Token: 0x04003B5B RID: 15195
		protected const string ReportingVDirName = "Reporting";

		// Token: 0x04003B5C RID: 15196
		protected const string ReportingVDirPath = "ClientAccess\\Reporting";

		// Token: 0x04003B5D RID: 15197
		protected const string ReportingDefaultAppPoolId = "MSExchangeReportingAppPool";
	}
}
