using System;
using System.ComponentModel;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000E3 RID: 227
	[LocDescription(Strings.IDs.ComAdminUninstallOleConverter)]
	[Cmdlet("Uninstall", "OleConverter")]
	public class UninstallOleConverter : Task
	{
		// Token: 0x060006A6 RID: 1702 RVA: 0x0001C09C File Offset: 0x0001A29C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				OleConverterRegistry.UnregisterOleConverter();
			}
			catch (Win32Exception e)
			{
				base.WriteError(new TaskWin32Exception(e), ErrorCategory.WriteError, null);
			}
			catch (SecurityException exception)
			{
				base.WriteError(exception, ErrorCategory.WriteError, null);
			}
			TaskLogger.LogExit();
		}
	}
}
