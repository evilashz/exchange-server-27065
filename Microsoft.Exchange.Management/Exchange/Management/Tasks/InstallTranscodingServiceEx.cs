using System;
using System.ComponentModel;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000E4 RID: 228
	[LocDescription(Strings.IDs.InstallTranscodingServiceEx)]
	[Cmdlet("Install", "TranscodingServiceEx")]
	public class InstallTranscodingServiceEx : Task
	{
		// Token: 0x060006A8 RID: 1704 RVA: 0x0001C100 File Offset: 0x0001A300
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				ComRunAsPwdUtil.SetRunAsPassword(InstallTranscodingServiceEx.TranscodingServiceAppId, "");
			}
			catch (Win32Exception e)
			{
				base.WriteError(new TaskWin32Exception(e), ErrorCategory.WriteError, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x04000316 RID: 790
		internal static string TranscodingServiceAppId = "{E1803C22-3538-4DCB-83A7-853214C9FF20}";
	}
}
