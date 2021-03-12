using System;
using System.ComponentModel;
using System.Management.Automation;
using System.Security;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000E2 RID: 226
	[Cmdlet("Install", "OleConverter")]
	[LocDescription(Strings.IDs.ComAdminInstallOleConverter)]
	public class InstallOleConverter : Task
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x0001C038 File Offset: 0x0001A238
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				OleConverterRegistry.RegisterOleConverter();
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
