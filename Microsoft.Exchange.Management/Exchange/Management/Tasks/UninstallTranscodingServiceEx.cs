using System;
using System.ComponentModel;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000E5 RID: 229
	[LocDescription(Strings.IDs.UninstallTranscodingServiceEx)]
	[Cmdlet("Uninstall", "TranscodingServiceEx")]
	public class UninstallTranscodingServiceEx : Task
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x0001C160 File Offset: 0x0001A360
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				ComRunAsPwdUtil.SetRunAsPassword(InstallTranscodingServiceEx.TranscodingServiceAppId, null);
			}
			catch (Win32Exception e)
			{
				base.WriteError(new TaskWin32Exception(e), ErrorCategory.WriteError, null);
			}
			TaskLogger.LogExit();
		}
	}
}
