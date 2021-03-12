using System;
using System.CodeDom.Compiler;
using System.DirectoryServices;
using System.IO;
using System.Security.Principal;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x020002F4 RID: 756
	internal class ManageAdamService
	{
		// Token: 0x060019F2 RID: 6642 RVA: 0x00073620 File Offset: 0x00071820
		public static void InstallAdam(string instanceName, string dataPath, string logPath, int port, int sslPort, WriteVerboseDelegate writeVerbose)
		{
			Utils.CreateDirectory(dataPath, "DataFilesPath");
			Utils.CreateDirectory(logPath, "LogFilesPath");
			AdamServiceSettings.DeleteFromRegistry(instanceName);
			AdamServiceSettings adamServiceSettings = new AdamServiceSettings(instanceName, Path.Combine(dataPath, "Adam"), Path.Combine(logPath, "Adam"), port, sslPort);
			using (TempFileCollection tempFileCollection = new TempFileCollection())
			{
				string answerFileName = ManageAdamService.MakeAnswerFile(tempFileCollection, adamServiceSettings);
				ManageAdamService.InstallAdamInstance(answerFileName, adamServiceSettings, writeVerbose);
				adamServiceSettings.SaveToRegistry();
			}
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x000736A4 File Offset: 0x000718A4
		public static void UninstallAdam(string instanceName)
		{
			ManageAdamService.RunAdamUninstall(instanceName);
			AdamServiceSettings.DeleteFromRegistry(instanceName);
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000736B4 File Offset: 0x000718B4
		public static void ImportAdamSchema(string instanceName, string schemaFilePath, string macroName, string macroValue)
		{
			string tempDir = Utils.GetTempDir();
			Directory.CreateDirectory(tempDir);
			try
			{
				int num = ManageAdamService.RunAdamSchemaImport(instanceName, schemaFilePath, macroName, macroValue, tempDir);
				string schemaImportLogFilePath = Path.Combine(tempDir, "ldif.log");
				ManageAdamService.AppendSchemaImportLogFileIfExists(schemaImportLogFilePath, ManageAdamService.AdamSchemaImportCumulativeLogFilePath, instanceName, schemaFilePath, macroName, macroValue);
				string schemaImportLogFilePath2 = Path.Combine(tempDir, "ldif.err");
				ManageAdamService.AppendSchemaImportLogFileIfExists(schemaImportLogFilePath2, ManageAdamService.AdamSchemaImportCumulativeErrorFilePath, instanceName, schemaFilePath, macroName, macroValue);
				if (num != 0)
				{
					throw new AdamSchemaImportProcessFailureException("ldifde.exe", num);
				}
			}
			finally
			{
				Directory.Delete(tempDir, true);
			}
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x00073738 File Offset: 0x00071938
		public static void AppendSchemaImportLogFileIfExists(string schemaImportLogFilePath, string schemaImportCumulativeLogFilePath, string instanceName, string schemaFilePath, string macroName, string macroValue)
		{
			if (File.Exists(schemaImportLogFilePath))
			{
				bool flag = File.Exists(schemaImportCumulativeLogFilePath);
				using (StreamWriter streamWriter = File.AppendText(schemaImportCumulativeLogFilePath))
				{
					if (flag)
					{
						streamWriter.WriteLine("");
					}
					streamWriter.WriteLine(ExDateTime.Now);
					streamWriter.WriteLine("Parameters for ADAM LDF file import:");
					streamWriter.WriteLine("Instance name: \"" + instanceName + "\"");
					streamWriter.WriteLine("Schema file path: \"" + schemaFilePath + "\"");
					streamWriter.WriteLine("Macro name: \"" + macroName + "\"");
					streamWriter.WriteLine("Macro value: \"" + macroValue + "\"");
					streamWriter.WriteLine("Log output:");
					string value = File.ReadAllText(schemaImportLogFilePath);
					streamWriter.Write(value);
				}
			}
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x00073818 File Offset: 0x00071A18
		public static string GetAdamServiceName(string instanceName)
		{
			return "ADAM_" + instanceName;
		}

		// Token: 0x060019F7 RID: 6647 RVA: 0x00073825 File Offset: 0x00071A25
		private static void InstallAdamInstance(string answerFileName, AdamServiceSettings adamServiceSettings, WriteVerboseDelegate writeVerbose)
		{
			TaskLogger.LogEnter();
			ManageAdamService.RunAdamInstall(answerFileName);
			ManageAdamService.SetAdamServiceArgs(adamServiceSettings.InstanceName, writeVerbose);
			ManageAdamService.SetAcls(adamServiceSettings);
			TaskLogger.LogExit();
		}

		// Token: 0x060019F8 RID: 6648 RVA: 0x0007384C File Offset: 0x00071A4C
		private static void RunAdamInstall(string answerFileName)
		{
			string arguments = "/answer:\"" + answerFileName + "\"";
			ManageAdamService.RunAdamInstallUninstallProcess(true, "adamInstall.exe", arguments, ManageAdamService.GetAdamToolsDir());
		}

		// Token: 0x060019F9 RID: 6649 RVA: 0x0007387C File Offset: 0x00071A7C
		private static void SetAdamServiceArgs(string instanceName, WriteVerboseDelegate writeVerbose)
		{
			string arguments = string.Concat(new string[]
			{
				"description ",
				ManageAdamService.GetAdamServiceName(instanceName),
				" \"",
				Strings.AdamServiceDescription,
				"\""
			});
			int num = Utils.LogRunProcess("sc.exe", arguments, null);
			if (num != 0)
			{
				writeVerbose(Strings.AdamFailedSetServiceArgs("sc.exe", num, "Description"));
			}
			arguments = string.Concat(new string[]
			{
				"config ",
				ManageAdamService.GetAdamServiceName(instanceName),
				" DisplayName= \"",
				Strings.AdamServiceDisplayName,
				"\""
			});
			num = Utils.LogRunProcess("sc.exe", arguments, null);
			if (num != 0)
			{
				writeVerbose(Strings.AdamFailedSetServiceArgs("sc.exe", num, "DisplayName"));
			}
		}

		// Token: 0x060019FA RID: 6650 RVA: 0x00073950 File Offset: 0x00071B50
		private static string GetAdamInstallExePath()
		{
			string adamToolsDir = ManageAdamService.GetAdamToolsDir();
			return Path.Combine(adamToolsDir, "adamInstall.exe");
		}

		// Token: 0x060019FB RID: 6651 RVA: 0x00073970 File Offset: 0x00071B70
		private static void RunAdamUninstall(string instanceName)
		{
			string arguments = "/q /force /i:\"" + instanceName + "\"";
			ManageAdamService.RunAdamInstallUninstallProcess(false, "adamUninstall.exe", arguments, ManageAdamService.GetAdamToolsDir());
		}

		// Token: 0x060019FC RID: 6652 RVA: 0x000739A0 File Offset: 0x00071BA0
		private static void SetAcls(AdamServiceSettings adamServiceSettings)
		{
			ManageAdamService.RunDsAcls(adamServiceSettings, "OU=MSExchangeGateway");
			using (DirectoryEntry rootDirectoryEntry = AdsUtils.GetRootDirectoryEntry(adamServiceSettings.LdapPort))
			{
				string text = (string)rootDirectoryEntry.Properties["ConfigurationNamingContext"].Value;
				ManageAdamService.RunDsAcls(adamServiceSettings, text);
				string subTreeDn = "CN=Deleted Objects," + text;
				ManageAdamService.RunDsAcls(adamServiceSettings, subTreeDn);
				ManageAdamService.SetAdministrator(adamServiceSettings, text);
			}
		}

		// Token: 0x060019FD RID: 6653 RVA: 0x00073A1C File Offset: 0x00071C1C
		private static void SetAdministrator(AdamServiceSettings adamServiceSettings, string configContainerDn)
		{
			string path = string.Format("{0}:{1}/CN=Administrators,CN=Roles,{2}", "LDAP://localhost", adamServiceSettings.LdapPort, configContainerDn);
			string value = string.Format("<SID={0}>", ManageAdamService.BuiltinAdminSid);
			using (DirectoryEntry directoryEntry = new DirectoryEntry(path))
			{
				directoryEntry.Properties["member"].Add(value);
				directoryEntry.CommitChanges();
			}
		}

		// Token: 0x060019FE RID: 6654 RVA: 0x00073A98 File Offset: 0x00071C98
		private static void RunDsAcls(AdamServiceSettings adamServiceSettings, string subTreeDn)
		{
			string arguments = string.Format("\"\\\\localhost:{0}\\{1}\" /I:T /G \"NT AUTHORITY\\SYSTEM\":GR;; \"NT AUTHORITY\\NETWORKSERVICE\":GR;; \"{2}\":GA;;", adamServiceSettings.LdapPort, subTreeDn, ManageAdamService.BuiltinAdminSid);
			int num = Utils.LogRunProcess("dsacls.exe", arguments, ManageAdamService.GetAdamToolsDir());
			if (num != 0)
			{
				throw new AdamSetAclsProcessFailureException("dsacls.exe", num, subTreeDn);
			}
		}

		// Token: 0x060019FF RID: 6655 RVA: 0x00073AE2 File Offset: 0x00071CE2
		private static string GetBuiltinAdminSid()
		{
			return new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null).Value;
		}

		// Token: 0x06001A00 RID: 6656 RVA: 0x00073AF4 File Offset: 0x00071CF4
		private static int RunAdamSchemaImport(string instanceName, string schemaFilePath, string macroName, string macroValue, string tempDir)
		{
			int ldapPort = AdamServiceSettings.GetFromRegistry(instanceName).LdapPort;
			string schemaImportProcessArguments = ManageAdamService.GetSchemaImportProcessArguments(schemaFilePath, ldapPort, tempDir, macroName, macroValue);
			return Utils.LogRunProcess("ldifde.exe", schemaImportProcessArguments, ManageAdamService.GetAdamToolsDir());
		}

		// Token: 0x06001A01 RID: 6657 RVA: 0x00073B2C File Offset: 0x00071D2C
		private static string GetSchemaImportProcessArguments(string schemaFilePath, int serviceLdapPort, string tempDir, string macroName, string macroValue)
		{
			return string.Format("-i -f \"{0}\" -s localhost:{1} -j \"{2}\" -c \"{3}\" \"{4}\"", new object[]
			{
				schemaFilePath,
				serviceLdapPort,
				tempDir,
				macroName,
				macroValue
			});
		}

		// Token: 0x06001A02 RID: 6658 RVA: 0x00073B68 File Offset: 0x00071D68
		private static void RunAdamInstallUninstallProcess(bool installing, string fileName, string arguments, string path)
		{
			ManageAdamService.ClearAdamInstallUninstallResults();
			int num = Utils.LogRunProcess(fileName, arguments, path);
			ManageAdamService.CheckAdamInstallUninstallResults(installing);
			if (num == 0)
			{
				return;
			}
			if (!installing)
			{
				Strings.AdamUninstallProcessFailure(fileName, num);
			}
			else
			{
				Strings.AdamInstallProcessFailure(fileName, num);
			}
			if (installing)
			{
				throw new AdamInstallProcessFailureException(fileName, num);
			}
			throw new AdamUninstallProcessFailureException(fileName, num);
		}

		// Token: 0x06001A03 RID: 6659 RVA: 0x00073BB4 File Offset: 0x00071DB4
		private static void ClearAdamInstallUninstallResults()
		{
			Utils.DeleteRegSubKeyTreeIfExist(Registry.LocalMachine, "Software\\Microsoft\\Windows\\CurrentVersion\\ADAM_Installer_Results");
		}

		// Token: 0x06001A04 RID: 6660 RVA: 0x00073BC8 File Offset: 0x00071DC8
		private static void CheckAdamInstallUninstallResults(bool installing)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\ADAM_Installer_Results"))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue(installing ? "ADAMInstallWarnings" : "ADAMUninstallWarnings");
					if (value != null)
					{
						string warning = value as string;
						TaskLogger.Log(installing ? Strings.AdamInstallWarning(warning) : Strings.AdamUninstallWarning(warning));
					}
					object value2 = registryKey.GetValue(installing ? "ADAMInstallErrorCode" : "ADAMUninstallErrorCode");
					object value3 = registryKey.GetValue(installing ? "ADAMInstallErrorMessage" : "ADAMUninstallErrorMessage");
					if (value2 != null || value3 != null)
					{
						if (value3 != null)
						{
							string error = value3 as string;
							TaskLogger.Log(installing ? Strings.AdamInstallError(error) : Strings.AdamUninstallError(error));
							if (installing)
							{
								throw new AdamInstallErrorException(error);
							}
							throw new AdamUninstallErrorException(error);
						}
						else
						{
							int num = (int)value2;
							if (installing && 20033 == num)
							{
								TaskLogger.Log(Strings.AdamInstallFailureDataOrLogFolderNotEmpty);
								throw new AdamInstallFailureDataOrLogFolderNotEmptyException();
							}
							TaskLogger.Log(installing ? Strings.AdamInstallGeneralFailureWithResult(num) : Strings.AdamUninstallGeneralFailureWithResult(num));
							if (installing)
							{
								throw new AdamInstallGeneralFailureWithResultException(num);
							}
							throw new AdamUninstallGeneralFailureWithResultException(num);
						}
					}
				}
			}
		}

		// Token: 0x06001A05 RID: 6661 RVA: 0x00073D00 File Offset: 0x00071F00
		private static string MakeAnswerFile(TempFileCollection tempFiles, AdamServiceSettings adamServiceSettings)
		{
			string text = tempFiles.AddExtension("ini");
			string path = Path.Combine(ConfigurationContext.Setup.SetupDataPath, "AdamInstallAnswer.ini");
			using (StreamReader streamReader = File.OpenText(path))
			{
				using (StreamWriter streamWriter = File.CreateText(text))
				{
					string text2;
					while ((text2 = streamReader.ReadLine()) != null)
					{
						if (!string.IsNullOrEmpty(text2) && text2[0] != ';')
						{
							if (text2.StartsWith("InstanceName"))
							{
								text2 = Utils.MakeIniFileSetting("InstanceName", adamServiceSettings.InstanceName);
							}
							else if (text2.StartsWith("DataFilesPath"))
							{
								text2 = Utils.MakeIniFileSetting("DataFilesPath", adamServiceSettings.DataFilesPath);
							}
							else if (text2.StartsWith("LogFilesPath"))
							{
								text2 = Utils.MakeIniFileSetting("LogFilesPath", adamServiceSettings.LogFilesPath);
							}
							else if (text2.StartsWith("LocalLDAPPortToListenOn"))
							{
								text2 = Utils.MakeIniFileSetting("LocalLDAPPortToListenOn", adamServiceSettings.LdapPort.ToString());
							}
							else if (text2.StartsWith("LocalSSLPortToListenOn"))
							{
								text2 = Utils.MakeIniFileSetting("LocalSSLPortToListenOn", adamServiceSettings.SslPort.ToString());
							}
							else if (text2.StartsWith("NewApplicationPartitionToCreate"))
							{
								text2 = Utils.MakeIniFileSetting("NewApplicationPartitionToCreate", "OU=MSExchangeGateway");
							}
							streamWriter.WriteLine(text2);
							TaskLogger.Log(new LocalizedString("Answer File:" + text2));
						}
					}
					streamWriter.Flush();
				}
			}
			return text;
		}

		// Token: 0x06001A06 RID: 6662 RVA: 0x00073EB8 File Offset: 0x000720B8
		private static string GetAdamToolsDir()
		{
			return Path.Combine(Utils.GetWindowsDir(), "ADAM");
		}

		// Token: 0x04000B46 RID: 2886
		private const string InstanceIniKey = "InstanceName";

		// Token: 0x04000B47 RID: 2887
		private const string DataFilesIniKey = "DataFilesPath";

		// Token: 0x04000B48 RID: 2888
		private const string LogFilesPathIniKey = "LogFilesPath";

		// Token: 0x04000B49 RID: 2889
		private const string LdapPortIniKey = "LocalLDAPPortToListenOn";

		// Token: 0x04000B4A RID: 2890
		private const string SslPortIniKey = "LocalSSLPortToListenOn";

		// Token: 0x04000B4B RID: 2891
		internal const string NewPartitionIniKey = "NewApplicationPartitionToCreate";

		// Token: 0x04000B4C RID: 2892
		private const string AdamInstallerResultsRegKey = "Software\\Microsoft\\Windows\\CurrentVersion\\ADAM_Installer_Results";

		// Token: 0x04000B4D RID: 2893
		private const string AdamInstallErrorCodeRegValueName = "ADAMInstallErrorCode";

		// Token: 0x04000B4E RID: 2894
		private const string AdamInstallErrorMessageRegValueName = "ADAMInstallErrorMessage";

		// Token: 0x04000B4F RID: 2895
		private const string AdamInstallWarningsRegValueName = "ADAMInstallWarnings";

		// Token: 0x04000B50 RID: 2896
		private const string AdamUninstallErrorCodeRegValueName = "ADAMUninstallErrorCode";

		// Token: 0x04000B51 RID: 2897
		private const string AdamUninstallErrorMessageRegValueName = "ADAMUninstallErrorMessage";

		// Token: 0x04000B52 RID: 2898
		private const string AdamUninstallWarningsRegValueName = "ADAMUninstallWarnings";

		// Token: 0x04000B53 RID: 2899
		private const string AdamSharedToolsFolderName = "ADAM";

		// Token: 0x04000B54 RID: 2900
		private const string AdamInstallExeFileName = "adamInstall.exe";

		// Token: 0x04000B55 RID: 2901
		private const string AdamUninstallExeFileName = "adamUninstall.exe";

		// Token: 0x04000B56 RID: 2902
		private const string AdamSchemaImportExportExeFileName = "ldifde.exe";

		// Token: 0x04000B57 RID: 2903
		private const string AdamSchemaImportExportLogFileName = "ldif.log";

		// Token: 0x04000B58 RID: 2904
		private const string AdamSchemaImportExportErrorFileName = "ldif.err";

		// Token: 0x04000B59 RID: 2905
		private const string AdamUnattendTemplateFileName = "AdamInstallAnswer.ini";

		// Token: 0x04000B5A RID: 2906
		private const string AdamDsAclsExeFileName = "dsacls.exe";

		// Token: 0x04000B5B RID: 2907
		private const string AdamExchangeSubdirName = "Adam";

		// Token: 0x04000B5C RID: 2908
		public const string AdamServiceNamePrefix = "ADAM_";

		// Token: 0x04000B5D RID: 2909
		private const string ServiceControlExeFileName = "sc.exe";

		// Token: 0x04000B5E RID: 2910
		internal static readonly string AdamSchemaImportCumulativeLogFilePath = Path.Combine(Utils.GetSetupLogDir(), "ldif.log");

		// Token: 0x04000B5F RID: 2911
		internal static readonly string AdamSchemaImportCumulativeErrorFilePath = Path.Combine(Utils.GetSetupLogDir(), "ldif.err");

		// Token: 0x04000B60 RID: 2912
		private static readonly string BuiltinAdminSid = ManageAdamService.GetBuiltinAdminSid();

		// Token: 0x020002F5 RID: 757
		private enum AdamInstallExitCodes
		{
			// Token: 0x04000B62 RID: 2914
			DataOrLogFolderNotEmpty = 20033
		}
	}
}
