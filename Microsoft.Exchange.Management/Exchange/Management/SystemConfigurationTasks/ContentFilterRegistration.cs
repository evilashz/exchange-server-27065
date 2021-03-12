using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A31 RID: 2609
	public class ContentFilterRegistration : Task
	{
		// Token: 0x06005D4B RID: 23883 RVA: 0x001892B8 File Offset: 0x001874B8
		internal ContentFilterRegistration(bool register)
		{
			this.register = register;
		}

		// Token: 0x06005D4C RID: 23884 RVA: 0x001892C8 File Offset: 0x001874C8
		protected override void InternalProcessRecord()
		{
			Process process = new Process();
			process.StartInfo.FileName = Path.Combine(ConfigurationContext.Setup.InstallPath, "TransportRoles\\agents\\Hygiene\\Microsoft.Exchange.ContentFilter.Wrapper.exe");
			process.StartInfo.Arguments = (this.register ? "-regserver" : "-unregserver");
			LocalizedString localizedString = this.register ? Strings.RegisterFilterFailed : Strings.UnregisterFilterFailed;
			try
			{
				process.Start();
				if (!process.WaitForExit(60000))
				{
					process.Kill();
					process.Close();
					base.WriteError(new LocalizedException(localizedString), ErrorCategory.InvalidOperation, null);
				}
			}
			catch (Win32Exception innerException)
			{
				base.ThrowTerminatingError(new LocalizedException(localizedString, innerException), ErrorCategory.InvalidOperation, null);
			}
			finally
			{
				process.Close();
			}
		}

		// Token: 0x040034A8 RID: 13480
		private readonly bool register;
	}
}
