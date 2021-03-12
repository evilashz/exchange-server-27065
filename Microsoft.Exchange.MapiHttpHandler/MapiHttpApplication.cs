using System;
using System.Web;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration.ConfigurationSettings;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000040 RID: 64
	public class MapiHttpApplication : HttpApplication
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000E184 File Offset: 0x0000C384
		static MapiHttpApplication()
		{
			MapiHttpApplication.fileSearchAssemblyResolver.FileNameFilter = new Func<string, bool>(MapiHttpApplication.AssemblyFileNameFilter);
			MapiHttpApplication.fileSearchAssemblyResolver.SearchPaths = new string[]
			{
				ExchangeSetupContext.InstallPath
			};
			MapiHttpApplication.fileSearchAssemblyResolver.Recursive = true;
			MapiHttpApplication.fileSearchAssemblyResolver.Install();
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000E208 File Offset: 0x0000C408
		private static bool AssemblyFileNameFilter(string fileName)
		{
			if (AssemblyResolver.ExchangePrefixedAssembliesOnly(fileName))
			{
				return true;
			}
			foreach (string text in MapiHttpApplication.approvedAssemblies)
			{
				if (text.EndsWith("."))
				{
					if (fileName.StartsWith(text, StringComparison.OrdinalIgnoreCase))
					{
						return true;
					}
				}
				else if (string.Compare(fileName, text, StringComparison.OrdinalIgnoreCase) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000E263 File Offset: 0x0000C463
		private void Application_Start(object sender, EventArgs e)
		{
			this.InitializePerformanceCounter();
			SettingOverrideSync.Instance.Start(true);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000E278 File Offset: 0x0000C478
		private void Application_End(object sender, EventArgs e)
		{
			try
			{
				MapiHttpHandler.ShutdownHandler();
				SettingOverrideSync.Instance.Stop();
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000E2AC File Offset: 0x0000C4AC
		private void Application_Error(object sender, EventArgs e)
		{
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000E2AE File Offset: 0x0000C4AE
		private void InitializePerformanceCounter()
		{
			Globals.InitializeMultiPerfCounterInstance("MapiHttp");
		}

		// Token: 0x040000FF RID: 255
		private static FileSearchAssemblyResolver fileSearchAssemblyResolver = new FileSearchAssemblyResolver();

		// Token: 0x04000100 RID: 256
		private static string[] approvedAssemblies = new string[]
		{
			"Microsoft.",
			"System."
		};
	}
}
