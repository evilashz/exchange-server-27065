using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Setup.Bootstrapper.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.Parser;
using Microsoft.Win32;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000015 RID: 21
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExSetupUI : SetupBase
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00006E70 File Offset: 0x00005070
		public override CommandLineParser Parser
		{
			get
			{
				if (this.parser == null)
				{
					this.parser = new UICommandLineParser(base.Logger);
				}
				return this.parser;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00006E91 File Offset: 0x00005091
		[STAThread]
		public static int Main(string[] args)
		{
			Application.SetCompatibleTextRenderingDefault(false);
			ExSetupUI.UpdateThreadUICulture();
			SplashScreen.SplashInstance.ShowSplash();
			ExSetupUI.SetMessageBoxHelper();
			ExSetupUI.SystemParametersInfo(4107, 0, 1, 2);
			return SetupBase.MainCore<ExSetupUI>(args, new SetupLoggerImpl());
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00006EC6 File Offset: 0x000050C6
		public static void ExitApplication(ExitCode returnCode)
		{
			ExSetupUI.exitCode = returnCode;
			Application.Exit();
		}

		// Token: 0x0600010E RID: 270
		[DllImport("user32.dll")]
		private static extern int SystemParametersInfo(int uAction, int uParam, int lpvParam, int fuWinIni);

		// Token: 0x0600010F RID: 271 RVA: 0x00006ED4 File Offset: 0x000050D4
		private static void UpdateThreadUICulture()
		{
			string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentUICulture;
			bool flag = false;
			while (!flag && cultureInfo != CultureInfo.InvariantCulture)
			{
				string path = directoryName + "\\" + cultureInfo.Name + "\\ExSetupUI.Resources.dll";
				if (File.Exists(path))
				{
					flag = true;
				}
				else
				{
					cultureInfo = cultureInfo.Parent;
				}
			}
			if (!flag)
			{
				Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-us");
			}
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00006F4C File Offset: 0x0000514C
		private static void SetMessageBoxHelper()
		{
			MessageBoxHelper.ButtonTexts = new Dictionary<MsgBoxButtons, string>
			{
				{
					MsgBoxButtons.OK,
					Strings.OkText
				},
				{
					MsgBoxButtons.Yes,
					Strings.YesText
				},
				{
					MsgBoxButtons.No,
					Strings.NoText
				}
			};
			MessageBoxHelper.CaptureTexts = new Dictionary<MsgBoxIcon, string>
			{
				{
					MsgBoxIcon.None,
					string.Empty
				},
				{
					MsgBoxIcon.Error,
					Strings.Error
				},
				{
					MsgBoxIcon.Cancel,
					Strings.Cancel
				},
				{
					MsgBoxIcon.Warning,
					Strings.Warning
				}
			};
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00006FE4 File Offset: 0x000051E4
		public override int Run()
		{
			ExSetupUI.exitCode = (ExitCode)base.Run();
			if (ExSetupUI.exitCode == ExitCode.Success)
			{
				bool flag = false;
				if (base.ParsedArguments.ContainsKey("mode"))
				{
					SetupOperations setupOperations = (SetupOperations)base.ParsedArguments["mode"];
					if (setupOperations == SetupOperations.Install || setupOperations == SetupOperations.Upgrade)
					{
						flag = SetupLauncherHelper.IsRestart(base.ParsedArguments);
						if (flag)
						{
							this.TryAddUpdatesParameterCopyMspFiles(SetupLauncherHelper.GetUpdatesDirFromRegistry(), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp\\ExchangeSetup\\MspTemp"), Path.Combine(SetupHelper.WindowsDir, "Temp\\ExchangeSetup"));
						}
					}
				}
				SetupWizard mainForm = new SetupWizard(this, !flag, SetupLauncherHelper.IsExchangeInstalled());
				if (ExSetupUI.exitCode == ExitCode.Success)
				{
					SplashScreen.SplashInstance.Hide();
					Application.Run(mainForm);
				}
				SplashScreen.SplashInstance.CloseSplash();
			}
			return (int)ExSetupUI.exitCode;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000070FC File Offset: 0x000052FC
		public override ExitCode ProcessArguments()
		{
			if (SetupLauncherHelper.IsFromInstalledExchangeDir() && this.IsInstallMode() && !this.IsLanguagePackMode() && !this.IsAddRolePossible())
			{
				this.ReportMessage(Strings.AddRoleNotPossible);
				return ExitCode.Error;
			}
			bool flag = false;
			bool flag2;
			if (string.IsNullOrEmpty(SetupBase.SetupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().IndexOf("addumlanguagepack") > 0)))
			{
				flag2 = string.IsNullOrEmpty(SetupBase.SetupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().IndexOf("removeumlanguagepack") > 0));
			}
			else
			{
				flag2 = false;
			}
			bool flag3 = flag2;
			if (flag3)
			{
				flag = string.IsNullOrEmpty(SetupBase.SetupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().IndexOf("mode") > 0));
			}
			if (flag)
			{
				SetupOperations setupOperations;
				if (base.IsExchangeInstalled)
				{
					if (string.IsNullOrEmpty(SetupBase.SetupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().IndexOf("removeumlanguagepack") > 0)))
					{
						string text = this.GetSourceDirFromArgs();
						if (string.IsNullOrEmpty(text))
						{
							text = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
						}
						string fullFileName = Path.Combine(Path.Combine(text, "Setup\\ServerRoles\\Common"), "ExSetup.exe");
						int num = SetupLauncherHelper.CompareRunningVersionWithInstalledVersion(fullFileName);
						if (num > 0)
						{
							this.ReportMessage(Strings.OlderVersionsOfServerRolesInstalled);
							return ExitCode.Error;
						}
						if (num < 0)
						{
							setupOperations = SetupOperations.Upgrade;
							goto IL_167;
						}
						setupOperations = SetupOperations.Install;
						goto IL_167;
					}
				}
				setupOperations = SetupOperations.Install;
				IL_167:
				this.AddArgument("mode", setupOperations.ToString());
			}
			return base.ProcessArguments();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000728C File Offset: 0x0000548C
		public override void WriteError(string error)
		{
			MessageBoxHelper.ShowError(error);
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00007295 File Offset: 0x00005495
		public override void ReportError(string error)
		{
			base.ReportError(error);
			this.WriteError(error);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000072A5 File Offset: 0x000054A5
		public override void ReportWarning(string warning)
		{
			base.Logger.LogWarning(new LocalizedString(warning));
			MessageBoxHelper.ShowWarning(warning);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000072BF File Offset: 0x000054BF
		public override void ReportException(Exception e)
		{
			base.ReportException(e);
			this.WriteError(e.Message);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000072D4 File Offset: 0x000054D4
		public override void ReportMessage(string message)
		{
			base.Logger.Log(new LocalizedString(message));
			MessageBoxHelper.ShowError(message);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000072EE File Offset: 0x000054EE
		public override void ReportMessage()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000072F8 File Offset: 0x000054F8
		internal void TryAddUpdatesParameterCopyMspFiles(string dirToCheck, string mspSourceDir, string destDir)
		{
			string text;
			string text2;
			SetupHelper.TryFindUpdates(dirToCheck, out text, out text2);
			if (text != null)
			{
				base.ParsedArguments["updatesdir"] = CommandLineParser.ParseSourceDir(dirToCheck);
				SetupLauncherHelper.CopyMspFiles(mspSourceDir, destDir);
			}
			if (text2 != null)
			{
				base.ParsedArguments["languagepack"] = CommandLineParser.ParseSourceFile(text2);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000734C File Offset: 0x0000554C
		private void AddArgument(string argName, string argValue)
		{
			string[] setupArgs = SetupBase.SetupArgs;
			Array.Resize<string>(ref setupArgs, SetupBase.SetupArgs.Length + 1);
			setupArgs[setupArgs.Length - 1] = string.Format("/{0}:{1}", argName, argValue);
			SetupBase.SetupArgs = setupArgs;
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00007388 File Offset: 0x00005588
		private string GetSourceDirFromArgs()
		{
			string result = null;
			foreach (string text in SetupBase.SetupArgs)
			{
				if (text.ToLowerInvariant().Contains("sourcedir".ToLower()))
				{
					result = this.Parser.Parse(text.Trim().Substring(1)).Value.ToString();
					break;
				}
			}
			return result;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00007440 File Offset: 0x00005640
		private bool IsInstallMode()
		{
			if (!string.IsNullOrEmpty(SetupBase.SetupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().IndexOf("mode") > 0)))
			{
				if (string.IsNullOrEmpty(SetupBase.SetupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().IndexOf(SetupOperations.Uninstall.ToString().ToLowerInvariant()) > 0)))
				{
					return !string.IsNullOrEmpty(SetupBase.SetupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().IndexOf(SetupOperations.Install.ToString().ToLowerInvariant()) > 0));
				}
			}
			return false;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x000074EF File Offset: 0x000056EF
		private bool IsLanguagePackMode()
		{
			return !string.IsNullOrEmpty(SetupBase.SetupArgs.FirstOrDefault((string a) => a.ToLowerInvariant().IndexOf("languagepack") > 0));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00007520 File Offset: 0x00005720
		private bool IsAddRolePossible()
		{
			bool flag = false;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\MailboxRole"))
			{
				flag = (registryKey != null);
			}
			bool flag2 = false;
			using (RegistryKey registryKey2 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\CafeRole"))
			{
				flag2 = (registryKey2 != null);
			}
			bool flag3 = false;
			using (RegistryKey registryKey3 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\FrontendTransportRole"))
			{
				flag3 = (registryKey3 != null);
			}
			bool flag4 = flag2 && flag3;
			bool flag5 = false;
			using (RegistryKey registryKey4 = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole"))
			{
				flag5 = (registryKey4 != null);
			}
			return !flag5 && (!flag || !flag4);
		}

		// Token: 0x04000097 RID: 151
		private const int SPI_SETKEYBOARDCUES = 4107;

		// Token: 0x04000098 RID: 152
		private const int SPIF_SENDWININICHANGE = 2;

		// Token: 0x04000099 RID: 153
		private static ExitCode exitCode;

		// Token: 0x0400009A RID: 154
		private CommandLineParser parser;
	}
}
