using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using Microsoft.Exchange.Setup.Bootstrapper.Common;
using Microsoft.Exchange.Setup.CommonBase;

namespace Microsoft.Exchange.Bootstrapper.Setup
{
	// Token: 0x02000002 RID: 2
	public class Setup : BootstrapperBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		[STAThread]
		public static int Main(string[] args)
		{
			if (!SetupHelper.ValidDotNetFrameworkInstalled())
			{
				CultureInfo currentUICulture = Thread.CurrentThread.CurrentUICulture;
				string twoLetterISOLanguageName = currentUICulture.TwoLetterISOLanguageName;
				string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				string path = string.Empty;
				if (twoLetterISOLanguageName == "en")
				{
					string text = "Microsoft.Exchange.Setup.Bootstrapper.Common.Resources.dll";
					path = Path.Combine(directoryName, text.Replace(".Resources.", "."));
				}
				else
				{
					path = Path.Combine(Path.Combine(directoryName, twoLetterISOLanguageName), "Microsoft.Exchange.Setup.Bootstrapper.Common.Resources.dll");
					if (!File.Exists(path))
					{
						path = Path.Combine(Path.Combine(directoryName, currentUICulture.Name), "Microsoft.Exchange.Setup.Bootstrapper.Common.Resources.dll");
					}
				}
				if (File.Exists(path))
				{
					Assembly assembly = Assembly.LoadFile(path);
					try
					{
						string baseName = string.Format("{0}.{1}", "Microsoft.Exchange.Setup.Bootstrapper.Common.Strings", twoLetterISOLanguageName);
						if (twoLetterISOLanguageName == "en")
						{
							baseName = "Microsoft.Exchange.Setup.Bootstrapper.Common.Strings";
						}
						ResourceManager resourceManager = new ResourceManager(baseName, assembly);
						Console.WriteLine(resourceManager.GetString("InvalidNetFwVersion", currentUICulture));
					}
					catch (Exception ex)
					{
						Console.WriteLine(ex.Message);
					}
				}
				return 1;
			}
			Setup.setupArgs = args;
			BootstrapperBase.SourceDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			BootstrapperBase.IsConsole = true;
			return BootstrapperBase.MainCore<Setup>(args);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000022A4 File Offset: 0x000004A4
		protected override int Run()
		{
			int num = 0;
			int num2 = 23;
			BootstrapperBase.ShowMessage(string.Empty);
			if (num2 > 0)
			{
				BootstrapperBase.ShowMessage(Strings.StartupTextCumulativeUpdate(Strings.MessageHeaderText, num2));
				BootstrapperBase.Logger.Log(Strings.StartupTextCumulativeUpdate(Strings.MessageHeaderText, num2));
			}
			else
			{
				BootstrapperBase.ShowMessage(Strings.StartupText(Strings.MessageHeaderText));
				BootstrapperBase.Logger.Log(Strings.StartupText(Strings.MessageHeaderText));
			}
			if (!string.IsNullOrEmpty(Setup.setupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().StartsWith("/help") || a.ToLowerInvariant().StartsWith("/h") || a.ToLowerInvariant().StartsWith("/?"))))
			{
				Setup.SetHelpMessage(string.Join(" ", Setup.setupArgs));
				return 0;
			}
			BootstrapperBase.UninstallMode = !string.IsNullOrEmpty(Setup.setupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().IndexOf("uninstall") > 0));
			if (!BootstrapperBase.IsExchangeInstalled && BootstrapperBase.UninstallMode)
			{
				BootstrapperBase.ShowMessage(Strings.ErrorExchangeNotInstalled);
				BootstrapperBase.Logger.LogWarning(Strings.ErrorExchangeNotInstalled);
				return 1;
			}
			bool flag;
			if (!BootstrapperBase.UninstallMode)
			{
				if (string.IsNullOrEmpty(Setup.setupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().StartsWith("/removeumlanguagepack"))))
				{
					flag = !string.IsNullOrEmpty(Setup.setupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().StartsWith("/removeprovisionedserver")));
					goto IL_188;
				}
			}
			flag = true;
			IL_188:
			Setup.noLicenseTermsRequired = flag;
			if (!Setup.noLicenseTermsRequired)
			{
				if (string.IsNullOrEmpty(Setup.setupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().StartsWith("/iacceptexchangeserverlicenseterms"))))
				{
					BootstrapperBase.ShowMessage(Strings.IAcceptLicenseParameterRequired);
					return 1;
				}
			}
			if (!string.IsNullOrEmpty(Setup.setupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().StartsWith("/installwindowscomponents"))) && SetupHelper.IsClientVersionOS())
			{
				BootstrapperBase.ShowMessage(Strings.ParameterNotSupportedOnClientOS("InstallWindowsComponents"));
				return 1;
			}
			if (BootstrapperBase.UninstallMode || BootstrapperBase.IsFromInstalledExchangeDir)
			{
				BootstrapperBase.DestinationDir = Path.Combine(BootstrapperBase.InstalledExchangeDir, "bin");
			}
			else
			{
				num = base.CopySetupBootstrapperFiles();
			}
			if (num == 0)
			{
				string cmdLineArgs = BootstrapperBase.AddParameters(Setup.setupArgs, false);
				try
				{
					num = BootstrapperBase.StartSetup(cmdLineArgs, "ExSetup.exe", true);
				}
				catch (StartSetupFileNotFoundException e)
				{
					BootstrapperBase.Logger.LogError(e);
					num = 1;
				}
			}
			if (num == 2)
			{
				string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp\\ExchangeSetup\\MspTemp");
				num = base.TryUpdateSetupRequiredFiles(SetupChecksFileConstant.GetSetupRequiredFiles(), base.GetSetupRequiredFilesFromSetupAssembly(text), text);
				if (num == 0)
				{
					string cmdLineArgs2 = BootstrapperBase.AddParameters(Setup.setupArgs, true);
					try
					{
						num = BootstrapperBase.StartSetup(cmdLineArgs2, "ExSetup.exe", true);
					}
					catch (StartSetupFileNotFoundException e2)
					{
						BootstrapperBase.Logger.LogError(e2);
						num = 1;
					}
				}
			}
			return num;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000025A4 File Offset: 0x000007A4
		private static void SetHelpMessage(string cmdLineArgs)
		{
			string[] array = cmdLineArgs.Split(new char[]
			{
				':'
			});
			if (array.Length == 1)
			{
				BootstrapperBase.ShowMessage(Strings.ExsetupGeneralHelp);
				return;
			}
			string text = array[1];
			text = text.ToLowerInvariant();
			string key;
			switch (key = text)
			{
			case "install":
				BootstrapperBase.ShowMessage(Strings.ExsetupInstallModeHelp);
				return;
			case "uninstall":
				BootstrapperBase.ShowMessage(Strings.ExsetupUninstallModeHelp);
				return;
			case "recoverserver":
				BootstrapperBase.ShowMessage(Strings.ExsetupRecoverServerModeHelp);
				return;
			case "upgrade":
				BootstrapperBase.ShowMessage(Strings.ExsetupUpgradeModeHelp);
				return;
			case "preparetopology":
				BootstrapperBase.ShowMessage(Strings.ExsetupPrepareTopologyHelp);
				return;
			case "delegation":
				BootstrapperBase.ShowMessage(Strings.ExsetupDelegationHelp);
				return;
			case "umlanguagepacks":
				BootstrapperBase.ShowMessage(Strings.ExsetupUmLanguagePacksHelp);
				return;
			}
			BootstrapperBase.ShowMessage(Strings.ExsetupGeneralHelp);
		}

		// Token: 0x04000001 RID: 1
		private const string UninstallModeParameter = "uninstall";

		// Token: 0x04000002 RID: 2
		private const string IAcceptExchangeServerLicenseTermsParameter = "/iacceptexchangeserverlicenseterms";

		// Token: 0x04000003 RID: 3
		private const string RemoveUMLanguagePackParameter = "/removeumlanguagepack";

		// Token: 0x04000004 RID: 4
		private const string RemoveProvisionedServerParameter = "/removeprovisionedserver";

		// Token: 0x04000005 RID: 5
		private const string InstallWindowsComponentsParameter = "/installwindowscomponents";

		// Token: 0x04000006 RID: 6
		private static string[] setupArgs;

		// Token: 0x04000007 RID: 7
		private static bool noLicenseTermsRequired;
	}
}
