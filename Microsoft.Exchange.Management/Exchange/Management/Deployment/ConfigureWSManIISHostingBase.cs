using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Net;
using Microsoft.Web.Administration;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000196 RID: 406
	public abstract class ConfigureWSManIISHostingBase : Task
	{
		// Token: 0x1700045C RID: 1116
		// (get) Token: 0x06000EC9 RID: 3785 RVA: 0x00041F47 File Offset: 0x00040147
		protected string System32Path
		{
			get
			{
				return this.system32Path;
			}
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06000ECA RID: 3786 RVA: 0x00041F4F File Offset: 0x0004014F
		protected string WinrmFilePath
		{
			get
			{
				return this.winrmFilePath;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000ECB RID: 3787 RVA: 0x00041F57 File Offset: 0x00040157
		protected string NetFilePath
		{
			get
			{
				return this.netFilePath;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000ECC RID: 3788 RVA: 0x00041F5F File Offset: 0x0004015F
		protected string PSVdirPhysicalPath
		{
			get
			{
				return this.psVdirPhysicalPath;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000ECD RID: 3789 RVA: 0x00041F67 File Offset: 0x00040167
		protected string PSDCCasVdirPhysicalPath
		{
			get
			{
				return this.psDCCasVdirPhysicalPath;
			}
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06000ECE RID: 3790 RVA: 0x00041F6F File Offset: 0x0004016F
		protected string InetsrvPath
		{
			get
			{
				return this.inetsrvPath;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00041F77 File Offset: 0x00040177
		protected string AppcmdPath
		{
			get
			{
				return this.appcmdPath;
			}
		}

		// Token: 0x17000463 RID: 1123
		// (get) Token: 0x06000ED0 RID: 3792 RVA: 0x00041F7F File Offset: 0x0004017F
		protected string IISConfigFilePath
		{
			get
			{
				return this.iisConfigFilePath;
			}
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x00041F87 File Offset: 0x00040187
		protected string WSManCfgFilePath
		{
			get
			{
				return this.wsmanCfgFilePath;
			}
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00041F8F File Offset: 0x0004018F
		protected string WSManDCCasCfgFilePath
		{
			get
			{
				return this.wsmanDCCasCfgFilePath;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00041F97 File Offset: 0x00040197
		protected string WSManCfgSchemaFileOriginalPath
		{
			get
			{
				return this.wsmanCfgSchemaFileOriginalPath;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000ED4 RID: 3796 RVA: 0x00041F9F File Offset: 0x0004019F
		protected string WSManCfgSchemaFilePath
		{
			get
			{
				return this.wsmanCfgSchemaFilePath;
			}
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00041FA7 File Offset: 0x000401A7
		protected string WSManModuleFilePath
		{
			get
			{
				return this.wsmanModuleFilePath;
			}
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000ED6 RID: 3798 RVA: 0x00041FAF File Offset: 0x000401AF
		protected string KerberosAuthModuleFilePath
		{
			get
			{
				return this.kerbModuleFilePath;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00041FB7 File Offset: 0x000401B7
		protected string IISBackupFileName
		{
			get
			{
				return this.iisBackupFileName;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x00041FBF File Offset: 0x000401BF
		// (set) Token: 0x06000ED9 RID: 3801 RVA: 0x00041FC7 File Offset: 0x000401C7
		protected string DefaultSiteName
		{
			get
			{
				return this.defaultSiteName;
			}
			set
			{
				this.defaultSiteName = value;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x00041FD0 File Offset: 0x000401D0
		protected string PSVdirName
		{
			get
			{
				if (this.psVdirName == null)
				{
					this.psVdirName = this.DefaultSiteName + "/PowerShell";
				}
				return this.psVdirName;
			}
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00041FF6 File Offset: 0x000401F6
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (base.HasErrors)
			{
				return;
			}
			this.ResolveConfigurationFilePaths();
			TaskLogger.LogExit();
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00042018 File Offset: 0x00040218
		private void ResolveConfigurationFilePaths()
		{
			this.system32Path = Path.Combine(Environment.GetEnvironmentVariable("windir"), "system32");
			this.winrmFilePath = Path.Combine(this.system32Path, "winrm.cmd");
			this.netFilePath = Path.Combine(this.system32Path, "net.exe");
			this.inetsrvPath = Path.Combine(this.system32Path, "inetsrv");
			this.appcmdPath = Path.Combine(this.inetsrvPath, "appcmd.exe");
			string path = Path.Combine(this.inetsrvPath, "config");
			this.iisConfigFilePath = Path.Combine(path, "ApplicationHost.Config");
			this.wsmanCfgSchemaFilePath = Path.Combine(path, "schema");
			this.wsmanCfgSchemaFilePath = Path.Combine(this.wsmanCfgSchemaFilePath, "wsmanconfig_schema.xml");
			string path2 = Path.Combine(ConfigurationContext.Setup.InstallPath, "ClientAccess");
			this.psVdirPhysicalPath = Path.Combine(path2, "PowerShell");
			this.psDCCasVdirPhysicalPath = Path.Combine(path2, "PowerShell-LiveID");
			this.wsmanCfgFilePath = Path.Combine(this.psVdirPhysicalPath, "web.config");
			this.wsmanDCCasCfgFilePath = Path.Combine(this.psDCCasVdirPhysicalPath, "web.config");
			this.wsmanCfgSchemaFileOriginalPath = Path.Combine(this.system32Path, "wsmanconfig_schema.xml");
			this.wsmanModuleFilePath = Path.Combine(this.system32Path, "wsmsvc.dll");
			this.kerbModuleFilePath = Path.Combine(ConfigurationContext.Setup.BinPath, "kerbauth.dll");
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00042180 File Offset: 0x00040380
		protected void RebuildWSManRegistry()
		{
			this.RestartWSManService();
			string arguments = "i restore winrm/config";
			base.WriteVerbose(Strings.VerboseRebuildWSManRegistry);
			this.ExecuteCmd(this.WinrmFilePath, arguments, this.InetsrvPath, false, false);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x000421B9 File Offset: 0x000403B9
		protected void RestartWSManService()
		{
			base.WriteVerbose(Strings.VerboseRestartWSManService);
			this.ExecuteCmd(this.NetFilePath, "stop winrm", this.InetsrvPath, false, false);
			this.ExecuteCmd(this.NetFilePath, "start winrm", this.InetsrvPath, true, false);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000421F8 File Offset: 0x000403F8
		protected void RestartDefaultWebSite()
		{
			base.WriteVerbose(Strings.VerboseRestartDefaultWebSite);
			using (ServerManager serverManager = new ServerManager())
			{
				Site site = serverManager.Sites[this.DefaultSiteName];
				try
				{
					site.Stop();
					site.Start();
				}
				catch (ServerManagerException ex)
				{
					base.WriteWarning(ex.Message);
				}
			}
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x00042270 File Offset: 0x00040470
		protected void RestartWSManAppPool(string appPool)
		{
			base.WriteVerbose(Strings.VerboseRestartWSManAppPool(appPool));
			string arguments = "stop apppool /apppool.name:" + appPool;
			string arguments2 = "start apppool /apppool.name:" + appPool;
			this.ExecuteCmd(this.AppcmdPath, arguments, this.InetsrvPath, false, false);
			this.ExecuteCmd(this.AppcmdPath, arguments2, this.InetsrvPath, true, true);
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x000422CC File Offset: 0x000404CC
		protected void BackupIISConfig()
		{
			if (this.iisBackupFileName == null)
			{
				this.iisBackupFileName = "IISBackup" + ExDateTime.Now.ToFileTimeUtc().ToString();
				base.WriteVerbose(Strings.VerboseBackupIISConfig(this.iisBackupFileName));
				string arguments = "add backup " + this.iisBackupFileName;
				this.ExecuteCmd(this.AppcmdPath, arguments, this.InetsrvPath, true, false);
			}
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x00042340 File Offset: 0x00040540
		protected void RestoreOriginalIISConfig()
		{
			base.WriteVerbose(Strings.VerboseRestoreIISConfig(this.iisBackupFileName));
			string arguments = "restore backup " + this.iisBackupFileName;
			this.ExecuteCmd(this.AppcmdPath, arguments, this.InetsrvPath, false, false);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x00042384 File Offset: 0x00040584
		protected void OpenHttpPortsOnFirewall()
		{
			string text = Path.Combine(this.System32Path, "netsh.exe");
			if (File.Exists(text))
			{
				string arguments = "firewall set portopening TCP 80 HTTP";
				base.WriteVerbose(Strings.VerboseOpenFirewallPort("80", "HTTP"));
				this.ExecuteCmd(text, arguments, this.InetsrvPath, false, false);
				string arguments2 = "firewall set portopening TCP 443 HTTPS";
				base.WriteVerbose(Strings.VerboseOpenFirewallPort("443", "HTTPS"));
				this.ExecuteCmd(text, arguments2, this.InetsrvPath, false, false);
				return;
			}
			this.WriteWarning(Strings.ErrorSystemFileNotFound(text));
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00042410 File Offset: 0x00040610
		protected void EnableBasicAuthForWSMan()
		{
			string arguments = "p winrm/config/client/auth @{Basic=\"true\"}";
			base.WriteVerbose(Strings.VerboseEnableBasicAuthForWSMan);
			this.ExecuteCmd(this.WinrmFilePath, arguments, this.InetsrvPath, false, false);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00042444 File Offset: 0x00040644
		protected void ExecuteCmd(string appPath, string arguments, string executionPath, bool writeError, bool needToRestoreIIS)
		{
			string output;
			string errors;
			int num = ProcessRunner.Run(appPath, arguments, -1, executionPath, out output, out errors);
			if (num == 0)
			{
				base.WriteVerbose(Strings.VerboseExecuteCmd(appPath, arguments, output));
				return;
			}
			if (!writeError)
			{
				this.WriteWarning(Strings.ErrorCmdExecutionFailed(appPath, arguments, output, errors, num.ToString()));
				return;
			}
			if (needToRestoreIIS && this.iisBackupFileName != null)
			{
				this.RestoreOriginalIISConfig();
			}
			base.WriteError(new InvalidOperationException(Strings.ErrorCmdExecutionFailed(appPath, arguments, output, errors, num.ToString())), ErrorCategory.InvalidOperation, null);
		}

		// Token: 0x040006E8 RID: 1768
		protected const string strPSVdirName = "PowerShell";

		// Token: 0x040006E9 RID: 1769
		protected const string strPSDCCasVdirName = "PowerShell-LiveID";

		// Token: 0x040006EA RID: 1770
		protected const string strPSVdirAppPool = "MSExchangePowerShellAppPool";

		// Token: 0x040006EB RID: 1771
		protected const string strPSDCCasVdirAppPool = "MSExchangePowerShellLiveIDAppPool";

		// Token: 0x040006EC RID: 1772
		protected const string strWSManModuleName = "WSMan";

		// Token: 0x040006ED RID: 1773
		protected const string strKerbAuthModuleName = "Kerbauth";

		// Token: 0x040006EE RID: 1774
		protected const string strWSManConfigSectionName = "system.management.wsmanagement.config";

		// Token: 0x040006EF RID: 1775
		private const string strClientAccess = "ClientAccess";

		// Token: 0x040006F0 RID: 1776
		private const string strBin = "Bin";

		// Token: 0x040006F1 RID: 1777
		private const string strWSManCfgFileName = "web.config";

		// Token: 0x040006F2 RID: 1778
		private const string strWSManCfgSchemaFileName = "wsmanconfig_schema.xml";

		// Token: 0x040006F3 RID: 1779
		private const string strWSManModuleFileName = "wsmsvc.dll";

		// Token: 0x040006F4 RID: 1780
		private const string strKerbauthModuleFileName = "kerbauth.dll";

		// Token: 0x040006F5 RID: 1781
		private const string strApplicationHostFileName = "ApplicationHost.Config";

		// Token: 0x040006F6 RID: 1782
		private const string strHttpPort = "80";

		// Token: 0x040006F7 RID: 1783
		private const string strHttpsPort = "443";

		// Token: 0x040006F8 RID: 1784
		private string system32Path;

		// Token: 0x040006F9 RID: 1785
		private string winrmFilePath;

		// Token: 0x040006FA RID: 1786
		private string netFilePath;

		// Token: 0x040006FB RID: 1787
		private string psVdirPhysicalPath;

		// Token: 0x040006FC RID: 1788
		private string psDCCasVdirPhysicalPath;

		// Token: 0x040006FD RID: 1789
		private string inetsrvPath;

		// Token: 0x040006FE RID: 1790
		private string appcmdPath;

		// Token: 0x040006FF RID: 1791
		private string iisConfigFilePath;

		// Token: 0x04000700 RID: 1792
		private string wsmanCfgFilePath;

		// Token: 0x04000701 RID: 1793
		private string wsmanDCCasCfgFilePath;

		// Token: 0x04000702 RID: 1794
		private string wsmanCfgSchemaFileOriginalPath;

		// Token: 0x04000703 RID: 1795
		private string wsmanCfgSchemaFilePath;

		// Token: 0x04000704 RID: 1796
		private string wsmanModuleFilePath;

		// Token: 0x04000705 RID: 1797
		private string kerbModuleFilePath;

		// Token: 0x04000706 RID: 1798
		private string iisBackupFileName;

		// Token: 0x04000707 RID: 1799
		private string defaultSiteName;

		// Token: 0x04000708 RID: 1800
		private string psVdirName;
	}
}
