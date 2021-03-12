using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Bootstrapper.Setup;
using Microsoft.Exchange.CabUtility;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Setup.Bootstrapper.Common;
using Microsoft.Exchange.Setup.CommonBase;

namespace Microsoft.Exchange.Bootstrapper.SetupUI
{
	// Token: 0x02000002 RID: 2
	public class SetupUI : BootstrapperBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		[STAThread]
		public static int Main(string[] args)
		{
			SetupUI.GetExResourceManager();
			if (args.Length != 1)
			{
				BootstrapperBase.ShowMessage(SetupUI.GetLocalizedString(Strings.InvalidSourceDir(string.Empty), "InvalidSourceDir", new string[]
				{
					string.Empty
				}));
				BootstrapperBase.Logger.LogWarning(SetupUI.GetLocalizedString(Strings.InvalidSourceDir(string.Empty), "InvalidSourceDir", new string[]
				{
					string.Empty
				}));
				return 1;
			}
			BootstrapperBase.SourceDir = args[0];
			if (!Directory.Exists(BootstrapperBase.SourceDir))
			{
				BootstrapperBase.ShowMessage(SetupUI.GetLocalizedString(Strings.InvalidSourceDir(BootstrapperBase.SourceDir), "InvalidSourceDir", new string[]
				{
					BootstrapperBase.SourceDir
				}));
				BootstrapperBase.Logger.LogWarning(SetupUI.GetLocalizedString(Strings.InvalidSourceDir(BootstrapperBase.SourceDir), "InvalidSourceDir", new string[]
				{
					BootstrapperBase.SourceDir
				}));
				return 1;
			}
			BootstrapperBase.IsConsole = false;
			return BootstrapperBase.MainCore<SetupUI>(args);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000021C4 File Offset: 0x000003C4
		protected override int Run()
		{
			int num = 0;
			bool flag = false;
			if (BootstrapperBase.IsExchangeInstalled)
			{
				string fullFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "ExSetup.exe");
				if (SetupLauncherHelper.CompareRunningVersionWithInstalledVersion(fullFileName) == 0)
				{
					flag = true;
				}
			}
			if (BootstrapperBase.IsFromInstalledExchangeDir && flag)
			{
				BootstrapperBase.ShowMessage(Strings.AttemptingToRunFromInstalledDirectory);
				BootstrapperBase.Logger.LogWarning(Strings.AttemptingToRunFromInstalledDirectory);
				return 1;
			}
			if (BootstrapperBase.IsFromInstalledExchangeDir || flag)
			{
				BootstrapperBase.DestinationDir = Path.Combine(BootstrapperBase.InstalledExchangeDir, "bin");
			}
			else
			{
				num = base.CopySetupBootstrapperFiles();
			}
			if (num == 0)
			{
				string cmdLineArgs = string.Format("{0}\"{1}\"", "/sourcedir:", BootstrapperBase.SourceDir);
				try
				{
					num = BootstrapperBase.StartSetup(cmdLineArgs, Path.Combine(BootstrapperBase.DestinationDir, "ExSetupUI.exe"), true);
				}
				catch (StartSetupFileNotFoundException e)
				{
					BootstrapperBase.Logger.LogError(e);
					num = 1;
				}
			}
			if (num == 2)
			{
				num = 0;
				string text;
				string text2;
				SetupHelper.TryFindUpdates(SetupLauncherHelper.GetUpdatesDirFromRegistry(), out text, out text2);
				if (text != null)
				{
					string text3 = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Temp\\ExchangeSetup\\MspTemp");
					num = base.TryUpdateSetupRequiredFiles(SetupChecksFileConstant.GetSetupRequiredFiles(), base.GetSetupRequiredFilesFromSetupAssembly(text3), text3);
				}
				if (num == 0 && text2 != null)
				{
					num = (this.InitializeLangPackInfo(BootstrapperBase.SourceDir, BootstrapperBase.DestinationDir, text2, BootstrapperBase.IsExchangeInstalled) ? 0 : 1);
				}
				if (num == 0)
				{
					string cmdLineArgs2 = BootstrapperBase.AddParameters(new string[0], true);
					try
					{
						num = BootstrapperBase.StartSetup(cmdLineArgs2, "ExSetupUI.exe", true);
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

		// Token: 0x06000003 RID: 3 RVA: 0x00002350 File Offset: 0x00000550
		private static void GetExResourceManager()
		{
			string text = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			int num = text.IndexOf("Setup\\ServerRoles\\Common");
			if (num > 0)
			{
				text = text.Substring(0, num);
			}
			CultureInfo cultureInfo = Thread.CurrentThread.CurrentUICulture;
			CultureInfo cultureInfo2 = null;
			string path = Path.Combine(Path.Combine(text, cultureInfo.Name), "Microsoft.Exchange.Setup.Bootstrapper.Common.Resources.dll");
			while (!File.Exists(path) && cultureInfo != cultureInfo2)
			{
				cultureInfo2 = cultureInfo;
				cultureInfo = cultureInfo.Parent;
				path = Path.Combine(Path.Combine(text, cultureInfo.Name), "Microsoft.Exchange.Setup.Bootstrapper.Common.Resources.dll");
			}
			if (File.Exists(path))
			{
				Assembly assembly = Assembly.LoadFile(path);
				try
				{
					string baseName = string.Format("{0}.{1}", "Microsoft.Exchange.Setup.Bootstrapper.Common.Strings", cultureInfo.Name);
					SetupUI.resourceManager = ExchangeResourceManager.GetResourceManager(baseName, assembly);
				}
				catch (Exception)
				{
				}
			}
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002428 File Offset: 0x00000628
		private static LocalizedString GetLocalizedString(LocalizedString defaultString, string stringName, params string[] args)
		{
			Strings.InvalidSourceDir(string.Empty);
			if (SetupUI.resourceManager == null)
			{
				return defaultString;
			}
			if (args == null)
			{
				return new LocalizedString(SetupUI.resourceManager.GetString(stringName));
			}
			return new LocalizedString(string.Format(SetupUI.resourceManager.GetString(stringName), args));
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002468 File Offset: 0x00000668
		private bool InitializeLangPackInfo(string srcDir, string dstDir, string languagePackDir, bool launchedfromInstalledLocation)
		{
			if (string.IsNullOrEmpty(languagePackDir))
			{
				return true;
			}
			CultureInfo cultureInfo = CultureInfo.CurrentUICulture;
			FileInfo fileInfo = new FileInfo(languagePackDir);
			if ((fileInfo.Attributes & FileAttributes.Directory) != FileAttributes.Directory)
			{
				Dictionary<string, long> dictionary = EmbeddedCabWrapper.FindAllBaseDirectoriesAndMSIFiles(languagePackDir);
				if (dictionary.Count == 0)
				{
					return true;
				}
				long sizeOfLangFile = 0L;
				string key = Path.Combine(cultureInfo.Name.ToLower(), "ServerLanguagePack.msi");
				if (!dictionary.TryGetValue(key, out sizeOfLangFile))
				{
					if (cultureInfo.Parent == null)
					{
						cultureInfo = CultureInfo.GetCultureInfo("en");
					}
					else
					{
						cultureInfo = cultureInfo.Parent;
					}
					key = Path.Combine(cultureInfo.Name.ToLower(), "ServerLanguagePack.msi");
					if (!dictionary.TryGetValue(key, out sizeOfLangFile))
					{
						cultureInfo = CultureInfo.GetCultureInfo("en");
					}
				}
				if (!this.ExtractLangPack(srcDir, dstDir, languagePackDir, cultureInfo.Name.ToLower(), sizeOfLangFile, launchedfromInstalledLocation))
				{
					return false;
				}
				string srcDir2 = Path.Combine(dstDir, Path.Combine("Temp", Path.Combine(cultureInfo.Name, Path.Combine("Setup\\ServerRoles\\Common", cultureInfo.Name))));
				SetupHelper.MoveFiles(srcDir2, Path.Combine(dstDir, cultureInfo.Name));
			}
			return true;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000257C File Offset: 0x0000077C
		private bool ExtractLangPack(string srcDir, string dstDir, string languagePackDir, string cultureName, long sizeOfLangFile, bool launchedfromInstalledLocation)
		{
			if (!Directory.Exists(dstDir))
			{
				BootstrapperBase.Logger.Log(SetupUI.GetLocalizedString(Strings.DirectoryNotFound(srcDir), "DirectoryNotFound", new string[]
				{
					srcDir
				}));
				return false;
			}
			if (!Directory.Exists(srcDir))
			{
				BootstrapperBase.Logger.Log(Strings.DirectoryNotFound(dstDir));
				return false;
			}
			string text = Path.Combine(dstDir, "Temp");
			DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(text));
			if (driveInfo.AvailableFreeSpace < sizeOfLangFile)
			{
				BootstrapperBase.Logger.Log(Strings.InsufficientDiskSpace);
				return false;
			}
			string text2 = Path.Combine(Path.Combine(cultureName, "Setup\\ServerRoles\\Common"), cultureName);
			text2 = BootstrapperBase.PathAppendBackslash(text2);
			int num = EmbeddedCabWrapper.ExtractFiles(languagePackDir, text, text2);
			return num > 0;
		}

		// Token: 0x04000001 RID: 1
		private static ExchangeResourceManager resourceManager;
	}
}
