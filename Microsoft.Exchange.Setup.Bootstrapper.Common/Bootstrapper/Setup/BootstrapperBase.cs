using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Exchange.Setup.Bootstrapper.Common;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Win32;

namespace Microsoft.Exchange.Bootstrapper.Setup
{
	// Token: 0x02000004 RID: 4
	public abstract class BootstrapperBase
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000248B File Offset: 0x0000068B
		public BootstrapperBase()
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000018 RID: 24 RVA: 0x00002494 File Offset: 0x00000694
		internal static bool IsRunAsAdmin
		{
			get
			{
				WindowsIdentity current = WindowsIdentity.GetCurrent();
				WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
				return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000024B9 File Offset: 0x000006B9
		// (set) Token: 0x0600001A RID: 26 RVA: 0x000024C0 File Offset: 0x000006C0
		protected static bool UninstallMode { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000024C8 File Offset: 0x000006C8
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000024CF File Offset: 0x000006CF
		protected static string InstalledExchangeDir { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000024D7 File Offset: 0x000006D7
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000024DE File Offset: 0x000006DE
		protected static bool IsExchangeInstalled { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001F RID: 31 RVA: 0x000024E6 File Offset: 0x000006E6
		// (set) Token: 0x06000020 RID: 32 RVA: 0x000024ED File Offset: 0x000006ED
		protected static bool IsFromInstalledExchangeDir { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000024F5 File Offset: 0x000006F5
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000024FC File Offset: 0x000006FC
		protected static bool IsConsole { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002504 File Offset: 0x00000704
		// (set) Token: 0x06000024 RID: 36 RVA: 0x0000250B File Offset: 0x0000070B
		protected static string SourceDir { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002513 File Offset: 0x00000713
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000251A File Offset: 0x0000071A
		protected static string DestinationDir { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002522 File Offset: 0x00000722
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002538 File Offset: 0x00000738
		protected static IBootstrapperLogger Logger
		{
			get
			{
				IBootstrapperLogger result;
				if ((result = BootstrapperBase.logger) == null)
				{
					result = (BootstrapperBase.logger = new BootstrapperLogger());
				}
				return result;
			}
			set
			{
				BootstrapperBase.logger = value;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002540 File Offset: 0x00000740
		internal int TryUpdateSetupRequiredFiles(IEnumerable<string> oldList, IEnumerable<string> newList, string dirToCheck)
		{
			int result = 1;
			if (string.IsNullOrEmpty(dirToCheck))
			{
				BootstrapperBase.Logger.LogError(new ArgumentNullException("dirToCheck"));
				return result;
			}
			if (!Directory.Exists(dirToCheck))
			{
				BootstrapperBase.Logger.LogError(new ArgumentException("dirToCheck"));
				return result;
			}
			if (oldList == null)
			{
				BootstrapperBase.Logger.LogError(new ArgumentNullException("oldList"));
				return result;
			}
			result = 0;
			try
			{
				if (newList == null)
				{
					SetupHelper.CopyFiles(oldList, dirToCheck, BootstrapperBase.DestinationDir, true, true);
				}
				else
				{
					SetupHelper.CopyFiles(newList, dirToCheck, BootstrapperBase.DestinationDir, true, true);
					string srcDir = Path.Combine(BootstrapperBase.SourceDir, "Setup\\ServerRoles\\Common");
					SetupHelper.CopyFiles(newList, srcDir, BootstrapperBase.DestinationDir, false, false);
				}
			}
			catch (FileNotFoundException ex)
			{
				BootstrapperBase.Logger.LogError(ex);
				BootstrapperBase.ShowMessage(ex.Message);
				result = 1;
			}
			catch (InsufficientDiskSpaceException)
			{
				BootstrapperBase.Logger.LogWarning(Strings.InsufficientDiskSpace);
				BootstrapperBase.ShowMessage(Strings.InsufficientDiskSpace);
				result = 1;
			}
			catch (FileCopyException ex2)
			{
				BootstrapperBase.Logger.LogError(ex2);
				BootstrapperBase.ShowMessage(ex2.Message);
				result = 1;
			}
			catch (FileNotExistsException ex3)
			{
				BootstrapperBase.Logger.LogError(ex3);
				BootstrapperBase.ShowMessage(ex3.Message);
				result = 1;
			}
			catch (DirectoryNotExistsException ex4)
			{
				BootstrapperBase.Logger.LogError(ex4);
				BootstrapperBase.ShowMessage(ex4.Message);
				result = 1;
			}
			return result;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000026C0 File Offset: 0x000008C0
		protected static int MainCore<T>(string[] args) where T : BootstrapperBase, new()
		{
			BootstrapperBase.setupParameters = args;
			string name = BootstrapperBase.IsConsole ? "Microsoft.Exchange.Bootstrapper.Setup" : "Microsoft.Exchange.Bootstrapper.SetupUI";
			bool flag = false;
			int result;
			using (new Mutex(true, name, ref flag))
			{
				try
				{
					try
					{
						BootstrapperBase.Logger.StartLogging();
					}
					catch (SetupLogInitializeException)
					{
					}
					if (!BootstrapperBase.IsRunAsAdmin)
					{
						BootstrapperBase.Logger.Log(Strings.NotAdmin);
						BootstrapperBase.ShowMessage(Strings.NotAdmin);
						result = 1;
					}
					else
					{
						if (BootstrapperBase.IsConsole && flag)
						{
							Mutex mutex2 = null;
							try
							{
								mutex2 = new Mutex(true, "Microsoft.Exchange.Bootstrapper.SetupUI", ref flag);
							}
							finally
							{
								mutex2.Close();
								mutex2.Dispose();
							}
						}
						if (!flag)
						{
							BootstrapperBase.Logger.Log(Strings.CannotRunMultipleInstances);
							BootstrapperBase.ShowMessage(Strings.CannotRunMultipleInstances);
							result = 1;
						}
						else
						{
							SetupHelper.GetDatacenterSettings();
							if (SetupHelper.IsDatacenter && SetupHelper.TreatPreReqErrorsAsWarnings)
							{
								BootstrapperBase.Logger.Log(Strings.TreatPreReqErrorsAsWarnings);
								BootstrapperBase.ShowMessage(Strings.TreatPreReqErrorsAsWarnings);
							}
							List<string> list = new List<string>();
							bool flag2 = true;
							try
							{
								SetupHelper.ValidOSVersion();
							}
							catch (Bit64OnlyException e)
							{
								BootstrapperBase.Logger.LogError(e);
								list.Add(Strings.Bit64Only);
								flag2 = false;
							}
							catch (InvalidOSVersionException e2)
							{
								BootstrapperBase.Logger.LogError(e2);
								list.Add(Strings.InvalidOSVersion);
								flag2 = false;
							}
							try
							{
								SetupHelper.ValidPowershellInstalled();
							}
							catch (InvalidPSVersionException e3)
							{
								BootstrapperBase.Logger.LogError(e3);
								list.Add(Strings.InvalidPSVersion);
								flag2 = false;
							}
							if (!flag2)
							{
								BootstrapperBase.ShowMessage(string.Join(Environment.NewLine, list));
								if (!SetupHelper.IsDatacenter || (SetupHelper.IsDatacenter && !SetupHelper.TreatPreReqErrorsAsWarnings))
								{
									return 1;
								}
							}
							BootstrapperBase bootstrapperBase = Activator.CreateInstance<T>();
							if (BootstrapperBase.IsConsole && args.Length == 0)
							{
								string sourceDir = BootstrapperBase.SourceDir;
								if (!File.Exists(Path.Combine(BootstrapperBase.SourceDir, "SetupUI.exe")))
								{
									sourceDir = Path.Combine(BootstrapperBase.SourceDir, "Setup\\ServerRoles\\Common");
								}
								try
								{
									BootstrapperBase.SourceDir = BootstrapperBase.PathRemoveBackslash(BootstrapperBase.SourceDir);
									BootstrapperBase.StartSetup(sourceDir, string.Format("\"{0}\"", BootstrapperBase.SourceDir), "SetupUI.exe", false);
								}
								catch (StartSetupFileNotFoundException e4)
								{
									BootstrapperBase.Logger.LogError(e4);
									return 1;
								}
								result = 0;
							}
							else
							{
								BootstrapperBase.DestinationDir = Path.Combine(SetupHelper.WindowsDir, "Temp\\ExchangeSetup");
								BootstrapperBase.IsFromInstalledExchangeDir = BootstrapperBase.GetInstalledExchangeDir();
								result = bootstrapperBase.Run();
							}
						}
					}
				}
				finally
				{
					try
					{
						if (flag && Directory.Exists(BootstrapperBase.DestinationDir))
						{
							if (!BootstrapperBase.IsExchangeInstalled)
							{
								SetupHelper.DeleteDirectory(BootstrapperBase.DestinationDir);
							}
							else if (BootstrapperBase.IsExchangeInstalled && BootstrapperBase.UninstallMode)
							{
								BootstrapperBase.GetInstalledExchangeDir();
								if (!BootstrapperBase.IsExchangeInstalled)
								{
									SetupHelper.DeleteDirectory(BootstrapperBase.DestinationDir);
								}
							}
						}
					}
					catch (Exception e5)
					{
						BootstrapperBase.Logger.LogError(e5);
					}
					try
					{
						BootstrapperBase.Logger.StopLogging();
					}
					catch (IOException ex)
					{
						Console.WriteLine(ex.Message);
					}
				}
			}
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002AA0 File Offset: 0x00000CA0
		protected static int StartSetup(string cmdLineArgs, string fileName, bool waitForExit)
		{
			return BootstrapperBase.StartSetup(BootstrapperBase.DestinationDir, cmdLineArgs, fileName, waitForExit);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002AB0 File Offset: 0x00000CB0
		protected static int StartSetup(string sourceDir, string cmdLineArgs, string fileName, bool waitForExit)
		{
			int result = 0;
			if (!File.Exists(Path.Combine(sourceDir, fileName)))
			{
				BootstrapperBase.ShowMessage(Strings.StartSetupFileNotFound(Path.Combine(sourceDir, fileName)));
				throw new StartSetupFileNotFoundException(Path.Combine(sourceDir, fileName));
			}
			ProcessStartInfo startInfo = new ProcessStartInfo
			{
				WorkingDirectory = sourceDir,
				FileName = Path.Combine(sourceDir, fileName),
				Arguments = cmdLineArgs,
				UseShellExecute = false,
				RedirectStandardInput = false,
				RedirectStandardOutput = false,
				RedirectStandardError = false,
				ErrorDialog = false,
				Verb = "runas"
			};
			Process process = new Process
			{
				EnableRaisingEvents = true,
				StartInfo = startInfo
			};
			try
			{
				process.Start();
				if (waitForExit)
				{
					process.WaitForExit();
					result = process.ExitCode;
				}
			}
			finally
			{
				process.Close();
				process.Dispose();
			}
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002BA0 File Offset: 0x00000DA0
		protected static void ShowMessage(string message)
		{
			BootstrapperBase.ShowMessage(message, true);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002BA9 File Offset: 0x00000DA9
		protected static void ShowMessage(string message, bool useWriteLine)
		{
			if (!BootstrapperBase.IsConsole || (BootstrapperBase.IsConsole && BootstrapperBase.setupParameters.Length == 0))
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new DialogBoxForm(message));
				return;
			}
			if (useWriteLine)
			{
				Console.WriteLine(message);
				return;
			}
			Console.Write(message);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002C10 File Offset: 0x00000E10
		protected static string AddParameters(string[] args, bool restart)
		{
			for (int i = 0; i < args.Length; i++)
			{
				if (args[i].IndexOf(":") > 0)
				{
					args[i] = string.Format("{0}\"{1}\"", args[i].Substring(0, args[i].IndexOf(":") + 1), args[i].Substring(args[i].IndexOf(":") + 1));
				}
			}
			bool flag = string.IsNullOrEmpty(args.FirstOrDefault((string a) => a.ToLowerInvariant().StartsWith("/sourcedir:") || a.ToLowerInvariant().StartsWith("/s:")));
			int num = args.Length;
			if (restart)
			{
				num++;
				Array.Resize<string>(ref args, num);
				args[num - 1] = "/restart";
			}
			if (flag && !BootstrapperBase.UninstallMode)
			{
				num++;
				Array.Resize<string>(ref args, num);
				args[num - 1] = string.Format("{0}\"{1}\"", "/sourcedir:", BootstrapperBase.SourceDir);
			}
			return string.Join(" ", args);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002CF7 File Offset: 0x00000EF7
		protected static string PathAppendBackslash(string path)
		{
			if (path.Length > 0 && path[path.Length - 1] != Path.DirectorySeparatorChar)
			{
				path += Path.DirectorySeparatorChar;
			}
			return path;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002D2A File Offset: 0x00000F2A
		protected static string PathRemoveBackslash(string path)
		{
			if (path.Length > 0 && path[path.Length - 1] == Path.DirectorySeparatorChar)
			{
				path = path.Substring(0, path.Length - 1);
			}
			return path;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002D5C File Offset: 0x00000F5C
		protected int CopySetupBootstrapperFiles()
		{
			int result = 0;
			if (Directory.Exists(BootstrapperBase.DestinationDir))
			{
				SetupHelper.DeleteDirectory(BootstrapperBase.DestinationDir);
			}
			string text = BootstrapperBase.SourceDir;
			text = BootstrapperBase.PathAppendBackslash(text);
			string srcDir = Path.Combine(text, "Setup\\ServerRoles\\Common");
			bool isSourceRemote = SetupHelper.IsSourceRemote;
			if (isSourceRemote)
			{
				BootstrapperBase.Logger.Log(Strings.RemoteCopyBSFilesStart(srcDir, BootstrapperBase.DestinationDir));
			}
			else
			{
				BootstrapperBase.Logger.Log(Strings.LocalCopyBSFilesStart(srcDir, BootstrapperBase.DestinationDir));
			}
			try
			{
				if (SetupHelper.ResumeUpgrade())
				{
					SetupHelper.CopyFiles(srcDir, BootstrapperBase.DestinationDir, false, false, "^(.+)\\.(exe|com|dll)$");
				}
				else
				{
					SetupHelper.CopyFiles(SetupChecksFileConstant.GetSetupRequiredFiles(), srcDir, BootstrapperBase.DestinationDir, false, true);
				}
				this.CopySetupResourceFiles();
			}
			catch (FileNotFoundException ex)
			{
				BootstrapperBase.ShowMessage(ex.Message);
				result = 1;
			}
			catch (InsufficientDiskSpaceException e)
			{
				BootstrapperBase.Logger.LogError(e);
				BootstrapperBase.ShowMessage(Strings.InsufficientDiskSpace);
				result = 1;
			}
			catch (FileCopyException ex2)
			{
				BootstrapperBase.Logger.LogError(ex2);
				BootstrapperBase.ShowMessage(ex2.Message);
				result = 1;
			}
			catch (FileNotExistsException ex3)
			{
				BootstrapperBase.Logger.LogError(ex3);
				BootstrapperBase.ShowMessage(ex3.Message);
				result = 1;
			}
			catch (DirectoryNotExistsException ex4)
			{
				BootstrapperBase.Logger.LogError(ex4);
				BootstrapperBase.ShowMessage(ex4.Message);
				result = 1;
			}
			if (isSourceRemote)
			{
				BootstrapperBase.Logger.Log(Strings.RemoteCopyBSFilesEnd(srcDir, BootstrapperBase.DestinationDir));
			}
			else
			{
				BootstrapperBase.Logger.Log(Strings.LocalCopyBSFilesEnd(srcDir, BootstrapperBase.DestinationDir));
			}
			return result;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002F34 File Offset: 0x00001134
		protected IList<string> GetSetupRequiredFilesFromSetupAssembly(string dirToSetupAssembly)
		{
			if (string.IsNullOrEmpty(dirToSetupAssembly))
			{
				throw new ArgumentNullException("dirToSetupAssembly");
			}
			if (!Directory.Exists(dirToSetupAssembly))
			{
				return null;
			}
			string path = Path.Combine(dirToSetupAssembly, "Microsoft.Exchange.Setup.Bootstrapper.Common.dll");
			if (!File.Exists(path))
			{
				return null;
			}
			byte[] rawAssembly = File.ReadAllBytes(path);
			Assembly assembly = Assembly.Load(rawAssembly);
			string typeFullName = typeof(SetupChecksFileConstant).FullName;
			Type type = assembly.GetTypes().FirstOrDefault((Type x) => x.FullName.Equals(typeFullName));
			if (type == null)
			{
				throw new ArgumentNullException(typeFullName);
			}
			MethodInfo methodInfo = type.GetMethods(BindingFlags.Static | BindingFlags.Public).FirstOrDefault((MethodInfo x) => x.Name.Equals("GetSetupRequiredFiles"));
			if (methodInfo == null)
			{
				throw new MissingMethodException("GetSetupRequiredFiles");
			}
			return (IList<string>)methodInfo.Invoke(type, null);
		}

		// Token: 0x06000034 RID: 52
		protected abstract int Run();

		// Token: 0x06000035 RID: 53 RVA: 0x00003030 File Offset: 0x00001230
		private static bool GetInstalledExchangeDir()
		{
			BootstrapperBase.IsExchangeInstalled = false;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(BootstrapperBase.RegistryExchangeSetupPath))
			{
				if (registryKey != null)
				{
					BootstrapperBase.InstalledExchangeDir = (string)registryKey.GetValue("MsiInstallPath");
					if (string.IsNullOrEmpty(BootstrapperBase.InstalledExchangeDir))
					{
						return false;
					}
					BootstrapperBase.IsExchangeInstalled = true;
					if (!string.IsNullOrEmpty(BootstrapperBase.setupParameters.FirstOrDefault((string a) => a.ToLowerInvariant().StartsWith("/addumlanguagepack"))))
					{
						return true;
					}
					string directoryName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
					if (string.IsNullOrEmpty(directoryName))
					{
						return false;
					}
					string text = string.Format("{0}{1}", BootstrapperBase.InstalledExchangeDir, "bin");
					return text.Equals(directoryName, StringComparison.InvariantCultureIgnoreCase);
				}
			}
			return false;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003114 File Offset: 0x00001314
		private void CopySetupResourceFiles()
		{
			string text = BootstrapperBase.SourceDir;
			text = BootstrapperBase.PathAppendBackslash(text);
			CultureInfo cultureInfo = CultureInfo.CurrentUICulture;
			CultureInfo cultureInfo2 = null;
			string text2 = Path.Combine(Path.Combine(Path.Combine(text, cultureInfo.Name), "Setup\\ServerRoles\\Common"), cultureInfo.Name);
			while (!Directory.Exists(text2) && cultureInfo != cultureInfo2)
			{
				cultureInfo2 = cultureInfo;
				cultureInfo = cultureInfo.Parent;
				text2 = Path.Combine(Path.Combine(Path.Combine(text, cultureInfo.Name), "Setup\\ServerRoles\\Common"), cultureInfo.Name);
			}
			if (!Directory.Exists(text2))
			{
				text2 = string.Empty;
				if (!BootstrapperBase.IsFromInstalledExchangeDir && object.Equals(cultureInfo.Name, "en"))
				{
					BootstrapperBase.Logger.Log(Strings.NoLanguageAvailable);
				}
			}
			if (!string.IsNullOrEmpty(text2))
			{
				string dstDir = Path.Combine(BootstrapperBase.DestinationDir, cultureInfo.Name);
				SetupHelper.CopyFiles(text2, dstDir, true);
			}
		}

		// Token: 0x0400000B RID: 11
		protected const string HelpParameter = "/help";

		// Token: 0x0400000C RID: 12
		protected const string HelpParameterShort = "/h";

		// Token: 0x0400000D RID: 13
		protected const string HelpParameterSymbol = "/?";

		// Token: 0x0400000E RID: 14
		protected const string SourceDirParameter = "/sourcedir:";

		// Token: 0x0400000F RID: 15
		protected const string SourceDirParameterShort = "/s:";

		// Token: 0x04000010 RID: 16
		protected const string RestartParameter = "/restart";

		// Token: 0x04000011 RID: 17
		protected const string AddUMLanguagePackParameter = "/addumlanguagepack";

		// Token: 0x04000012 RID: 18
		private const string SetupMutexName = "Microsoft.Exchange.Bootstrapper.Setup";

		// Token: 0x04000013 RID: 19
		private const string SetupUIMutexName = "Microsoft.Exchange.Bootstrapper.SetupUI";

		// Token: 0x04000014 RID: 20
		protected static readonly string RegistryExchangeSetupPath = Path.Combine(SetupChecksRegistryConstant.RegistryExchangePath, "Setup");

		// Token: 0x04000015 RID: 21
		private static IBootstrapperLogger logger;

		// Token: 0x04000016 RID: 22
		private static string[] setupParameters;
	}
}
