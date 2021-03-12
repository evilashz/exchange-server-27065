using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Management;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001F6 RID: 502
	[Cmdlet("Install", "Languages", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class InstallLanguages : ComponentInfoBasedTask
	{
		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x0004B9B1 File Offset: 0x00049BB1
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x0004B9C8 File Offset: 0x00049BC8
		[Parameter(Mandatory = true)]
		public LongPath LangPackPath
		{
			get
			{
				return (LongPath)base.Fields["LangPackPath"];
			}
			set
			{
				base.Fields["LangPackPath"] = value;
			}
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x0004B9DB File Offset: 0x00049BDB
		// (set) Token: 0x0600111B RID: 4379 RVA: 0x0004B9F2 File Offset: 0x00049BF2
		[Parameter(Mandatory = true)]
		public NonRootLocalLongFullPath InstallPath
		{
			get
			{
				return (NonRootLocalLongFullPath)base.Fields["InstallPath"];
			}
			set
			{
				base.Fields["InstallPath"] = value;
			}
		}

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x0600111C RID: 4380 RVA: 0x0004BA05 File Offset: 0x00049C05
		// (set) Token: 0x0600111D RID: 4381 RVA: 0x0004BA1C File Offset: 0x00049C1C
		[Parameter(Mandatory = true)]
		public string[] LanguagePacksToInstall
		{
			get
			{
				return (string[])base.Fields["LanguagePacksToInstall"];
			}
			set
			{
				base.Fields["LanguagePacksToInstall"] = value;
			}
		}

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x0600111E RID: 4382 RVA: 0x0004BA2F File Offset: 0x00049C2F
		// (set) Token: 0x0600111F RID: 4383 RVA: 0x0004BA46 File Offset: 0x00049C46
		[Parameter(Mandatory = true)]
		public string[] LPClientFlags
		{
			get
			{
				return (string[])base.Fields["LPClientFlags"];
			}
			set
			{
				base.Fields["LPClientFlags"] = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x06001120 RID: 4384 RVA: 0x0004BA59 File Offset: 0x00049C59
		// (set) Token: 0x06001121 RID: 4385 RVA: 0x0004BA70 File Offset: 0x00049C70
		[Parameter(Mandatory = true)]
		public string[] LPServerFlags
		{
			get
			{
				return (string[])base.Fields["LPServerFlags"];
			}
			set
			{
				base.Fields["LPServerFlags"] = value;
			}
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x06001122 RID: 4386 RVA: 0x0004BA83 File Offset: 0x00049C83
		// (set) Token: 0x06001123 RID: 4387 RVA: 0x0004BA9A File Offset: 0x00049C9A
		[Parameter(Mandatory = true)]
		public bool SourceIsBundle
		{
			get
			{
				return (bool)base.Fields["SourceIsBundle"];
			}
			set
			{
				base.Fields["SourceIsBundle"] = value;
			}
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001124 RID: 4388 RVA: 0x0004BAB2 File Offset: 0x00049CB2
		// (set) Token: 0x06001125 RID: 4389 RVA: 0x0004BABA File Offset: 0x00049CBA
		[Parameter(Mandatory = true)]
		public InstallationModes InstallMode
		{
			get
			{
				return this.InstallationMode;
			}
			set
			{
				this.InstallationMode = value;
			}
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001126 RID: 4390 RVA: 0x0004BAC3 File Offset: 0x00049CC3
		// (set) Token: 0x06001127 RID: 4391 RVA: 0x0004BADA File Offset: 0x00049CDA
		[Parameter(Mandatory = false)]
		public LocalLongFullPath LogFilePath
		{
			get
			{
				return (LocalLongFullPath)base.Fields["LogFilePath"];
			}
			set
			{
				base.Fields["LogFilePath"] = value;
			}
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001128 RID: 4392 RVA: 0x0004BAED File Offset: 0x00049CED
		// (set) Token: 0x06001129 RID: 4393 RVA: 0x0004BB04 File Offset: 0x00049D04
		[Parameter(Mandatory = true)]
		public string InstallVersion
		{
			get
			{
				return (string)base.Fields["InstallVersion"];
			}
			set
			{
				base.Fields["InstallVersion"] = value;
			}
		}

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x0600112A RID: 4394 RVA: 0x0004BB17 File Offset: 0x00049D17
		// (set) Token: 0x0600112B RID: 4395 RVA: 0x0004BB2E File Offset: 0x00049D2E
		[Parameter(Mandatory = true)]
		public string ClientLPVersion
		{
			get
			{
				return (string)base.Fields["ClientLPVersion"];
			}
			set
			{
				base.Fields["ClientLPVersion"] = value;
			}
		}

		// Token: 0x0600112C RID: 4396 RVA: 0x0004BB44 File Offset: 0x00049D44
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			try
			{
				List<string> list = new List<string>();
				list.Add(this.InstallPath.PathName + "\\Setup\\Perf");
				string platform = base.Platform;
				list.Add(this.InstallPath.PathName + "\\Bin\\perf\\" + platform);
				foreach (string text in list)
				{
					if (Directory.Exists(text))
					{
						if (Directory.GetDirectories(text).Length > 1)
						{
							MergePerfCounterIni mergePerfCounterIni = new MergePerfCounterIni();
							mergePerfCounterIni.ParseDirectories(text);
							this.ParsePerfCounterDefinitionFiles(text);
						}
					}
					else
					{
						TaskLogger.Log(Strings.ExceptionDirectoryNotFound(text));
					}
				}
				if (ManageServiceBase.GetServiceStatus("Winmgmt") == ServiceControllerStatus.Running)
				{
					this.UpdateMFLFiles();
				}
				else
				{
					TaskLogger.Log(Strings.SkippedUpdatingMFLFiles);
				}
				this.UpdateOwaLPs();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600112D RID: 4397 RVA: 0x0004BC44 File Offset: 0x00049E44
		private void UpdateOwaLPs()
		{
			TaskLogger.LogEnter();
			TaskLogger.Log(Strings.StartingToCopyOwaLPFiles);
			try
			{
				if (this.InstallVersion == this.ClientLPVersion)
				{
					TaskLogger.Log(Strings.SameOwaVersionDoNotCopyLPFiles);
				}
				else
				{
					string sourcePathForLPfiles = string.Format("{0}\\ClientAccess\\owa\\prem\\{1}\\scripts\\", this.InstallPath.PathName, this.ClientLPVersion);
					string destinationPathForLPfiles = string.Format("{0}\\ClientAccess\\owa\\prem\\{1}\\scripts\\", this.InstallPath.PathName, this.InstallVersion);
					string sourcePathForLPfiles2 = string.Format("{0}\\ClientAccess\\owa\\prem\\{1}\\ext\\def\\", this.InstallPath.PathName, this.ClientLPVersion);
					string destinationPathForLPfiles2 = string.Format("{0}\\ClientAccess\\owa\\prem\\{1}\\ext\\def\\", this.InstallPath.PathName, this.InstallVersion);
					this.CopyLanguageDirectories(sourcePathForLPfiles, destinationPathForLPfiles);
					this.CopyLanguageDirectories(sourcePathForLPfiles2, destinationPathForLPfiles2);
				}
			}
			finally
			{
				TaskLogger.Log(Strings.FinishedCopyingOwaLPFiles);
				TaskLogger.LogExit();
			}
		}

		// Token: 0x0600112E RID: 4398 RVA: 0x0004BD24 File Offset: 0x00049F24
		private void CopyLanguageDirectories(string sourcePathForLPfiles, string destinationPathForLPfiles)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(sourcePathForLPfiles);
			if (directoryInfo.Exists)
			{
				DirectoryInfo[] directories = directoryInfo.GetDirectories();
				foreach (DirectoryInfo directoryInfo2 in directories)
				{
					try
					{
						CultureInfo.GetCultureInfo(directoryInfo2.Name);
						this.CopyDirectory(directoryInfo2.FullName, destinationPathForLPfiles + directoryInfo2.Name);
					}
					catch (CultureNotFoundException)
					{
					}
				}
				return;
			}
			TaskLogger.LogWarning(Strings.WarningLPDirectoryNotFound(sourcePathForLPfiles));
		}

		// Token: 0x0600112F RID: 4399 RVA: 0x0004BDA8 File Offset: 0x00049FA8
		private void UpdateMFLFiles()
		{
			TaskLogger.LogEnter();
			TaskLogger.Log(Strings.StartingToUpdateMFLFiles);
			ManagementObjectSearcher managementObjectSearcher = null;
			try
			{
				ConnectionOptions options = new ConnectionOptions();
				ManagementScope scope = new ManagementScope("root\\cimv2", options);
				ObjectQuery query = new ObjectQuery("select * from __Namespace");
				managementObjectSearcher = new ManagementObjectSearcher(scope, query);
				using (ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get())
				{
					int count = managementObjectCollection.Count;
					int num = 0;
					foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
					{
						ManagementObject managementObject = (ManagementObject)managementBaseObject;
						if (managementObject["Name"].ToString().StartsWith("ms_"))
						{
							string text = managementObject["Name"].ToString();
							string value = text.Substring(3);
							int culture = Convert.ToInt32(value, 16);
							CultureInfo cultureInfo;
							try
							{
								cultureInfo = new CultureInfo(culture);
								goto IL_C8;
							}
							catch (ArgumentException)
							{
								continue;
							}
							goto IL_BF;
							IL_C8:
							if (cultureInfo.Parent == CultureInfo.InvariantCulture)
							{
								string str = "ms_";
								string text2 = str + cultureInfo.LCID.ToString("X3");
								string text3 = this.InstallPath.PathName;
								text3 = Path.Combine(text3, "Bin");
								text3 = Path.Combine(text3, cultureInfo.Name);
								text3 = Path.Combine(text3, "Exchange.mfl");
								this.ReplaceInFile(text3, text2, text);
								TaskLogger.Log(Strings.ChangeMFLFile(text3, text2, text));
								MofCompiler mofCompiler = new MofCompiler();
								WbemCompileStatusInfo wbemCompileStatusInfo = default(WbemCompileStatusInfo);
								wbemCompileStatusInfo.InitializeStatusInfo();
								int num2 = mofCompiler.CompileFile(text3, null, null, null, null, 0, 0, 0, ref wbemCompileStatusInfo);
								if (num2 != 0)
								{
									TaskLogger.Log(Strings.ErrorCompilingMFL(text3, text, num2));
									goto IL_18E;
								}
								goto IL_18E;
							}
							IL_BF:
							cultureInfo = cultureInfo.Parent;
							goto IL_C8;
						}
						IL_18E:
						num++;
						base.WriteProgress(this.Description, this.Description, num * 100 / count);
					}
				}
			}
			finally
			{
				if (managementObjectSearcher != null)
				{
					managementObjectSearcher.Dispose();
				}
				TaskLogger.Log(Strings.FinishedUpdatingMFLFiles);
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06001130 RID: 4400 RVA: 0x0004C00C File Offset: 0x0004A20C
		private void ReplaceInFile(string filePath, string searchText, string replaceText)
		{
			if (File.Exists(filePath))
			{
				StreamReader streamReader = new StreamReader(filePath);
				string text = streamReader.ReadToEnd();
				streamReader.Close();
				text = Regex.Replace(text, searchText, replaceText);
				StreamWriter streamWriter = new StreamWriter(filePath);
				streamWriter.Write(text);
				streamWriter.Close();
				return;
			}
			TaskLogger.Log(Strings.MFLFileNotFound(filePath));
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0004C060 File Offset: 0x0004A260
		private void CopyDirectory(string sourceDirName, string destDirName)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(sourceDirName);
			if (!directoryInfo.Exists)
			{
				TaskLogger.LogWarning(Strings.WarningLPDirectoryNotFound(sourceDirName));
				return;
			}
			DirectoryInfo[] directories = directoryInfo.GetDirectories();
			if (!Directory.Exists(destDirName))
			{
				Directory.CreateDirectory(destDirName);
			}
			FileInfo[] files = directoryInfo.GetFiles();
			foreach (FileInfo fileInfo in files)
			{
				string text = Path.Combine(destDirName, fileInfo.Name);
				if (!File.Exists(text))
				{
					fileInfo.CopyTo(text, true);
				}
			}
			foreach (DirectoryInfo directoryInfo2 in directories)
			{
				string destDirName2 = Path.Combine(destDirName, directoryInfo2.Name);
				this.CopyDirectory(directoryInfo2.FullName, destDirName2);
			}
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0004C120 File Offset: 0x0004A320
		private void ParsePerfCounterDefinitionFiles(string definitionFilePath)
		{
			LoadUnloadPerfCounterLocalizedText loadUnloadText = new LoadUnloadPerfCounterLocalizedText();
			foreach (string path in Directory.GetFiles(definitionFilePath, "*.ini"))
			{
				try
				{
					string text = Path.Combine(definitionFilePath, path);
					string driverName = this.GetDriverName(text);
					this.InstallCounterStrings(loadUnloadText, driverName, text);
				}
				catch (TaskException exception)
				{
					base.WriteError(exception, ErrorCategory.InvalidData, null);
				}
			}
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0004C194 File Offset: 0x0004A394
		private void InstallCounterStrings(LoadUnloadPerfCounterLocalizedText loadUnloadText, string categoryName, string iniFilePath)
		{
			string name = string.Format("SYSTEM\\CurrentControlSet\\Services\\{0}\\Performance", categoryName);
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name))
			{
				if (registryKey != null)
				{
					try
					{
						loadUnloadText.UnloadLocalizedText(iniFilePath, categoryName);
						loadUnloadText.LoadLocalizedText(iniFilePath, categoryName);
					}
					catch (FileNotFoundException exception)
					{
						base.WriteError(exception, ErrorCategory.InvalidData, null);
					}
					catch (TaskException exception2)
					{
						base.WriteError(exception2, ErrorCategory.InvalidData, null);
					}
				}
			}
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06001134 RID: 4404 RVA: 0x0004C21C File Offset: 0x0004A41C
		protected override LocalizedString Description
		{
			get
			{
				return Strings.LanguagePackCfgDescription;
			}
		}

		// Token: 0x06001135 RID: 4405 RVA: 0x0004C224 File Offset: 0x0004A424
		private string GetDriverName(string iniFile)
		{
			string result = string.Empty;
			StringBuilder stringBuilder = new StringBuilder(512);
			if (InstallLanguages.GetPrivateProfileString("info", "drivername", null, stringBuilder, stringBuilder.Capacity, iniFile) != 0)
			{
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x06001136 RID: 4406
		[DllImport("KERNEL32.DLL", CharSet = CharSet.Unicode, EntryPoint = "GetPrivateProfileStringW")]
		private static extern int GetPrivateProfileString(string applicationName, string keyName, string defaultString, StringBuilder returnedString, int size, string fileName);
	}
}
