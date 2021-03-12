using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000DF RID: 223
	[LocDescription(Strings.IDs.InstallCentralAdminServiceTask)]
	[Cmdlet("uninstall", "sqldatabase")]
	public class UninstallSqlDatabase : ManageSqlDatabase
	{
		// Token: 0x0600069B RID: 1691 RVA: 0x0001BDF3 File Offset: 0x00019FF3
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
