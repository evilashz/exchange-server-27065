using System;
using System.Management.Automation;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008CD RID: 2253
	[Cmdlet("Upgrade", "MSExchangeReplService")]
	public class UpgradeReplService : ManageReplService
	{
		// Token: 0x06005010 RID: 20496 RVA: 0x0014F420 File Offset: 0x0014D620
		private bool ShouldUpgradeService()
		{
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("system\\currentcontrolset\\services\\msexchangerepl");
			bool result;
			if (registryKey == null)
			{
				result = true;
			}
			else
			{
				using (registryKey)
				{
					string input = (string)registryKey.GetValue("imagepath");
					string str = Regex.Replace(input, "\"", "");
					string str2 = base.ServiceInstallContext.Parameters["assemblypath"];
					result = !SharedHelper.StringIEquals(str, str2);
				}
			}
			return result;
		}

		// Token: 0x06005011 RID: 20497 RVA: 0x0014F4B0 File Offset: 0x0014D6B0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.ShouldUpgradeService())
			{
				base.Uninstall();
				base.Install();
			}
			base.UninstallEventManifest();
			base.InstallEventManifest();
			TaskLogger.LogExit();
		}
	}
}
