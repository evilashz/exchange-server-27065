using System;
using System.ComponentModel;
using System.IO;
using System.Management.Automation;
using System.Security;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Web.Administration;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000197 RID: 407
	[ClassAccessLevel(AccessLevel.Consumer)]
	[Cmdlet("Configure", "WSManIISHosting", SupportsShouldProcess = true)]
	public sealed class ConfigureWSManIISHosting : ConfigureWSManIISHostingBase
	{
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000EE7 RID: 3815 RVA: 0x000424C8 File Offset: 0x000406C8
		// (set) Token: 0x06000EE8 RID: 3816 RVA: 0x000424EE File Offset: 0x000406EE
		[Parameter]
		public SwitchParameter DataCenterCAS
		{
			get
			{
				return (SwitchParameter)(base.Fields["DataCenterCAS"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DataCenterCAS"] = value;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000EE9 RID: 3817 RVA: 0x00042506 File Offset: 0x00040706
		// (set) Token: 0x06000EEA RID: 3818 RVA: 0x0004252C File Offset: 0x0004072C
		[Parameter]
		public SwitchParameter EnableKerberosModule
		{
			get
			{
				return (SwitchParameter)(base.Fields["EnableKerbAuth"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["EnableKerbAuth"] = value;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000EEB RID: 3819 RVA: 0x00042544 File Offset: 0x00040744
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageConfigureWSManIISHosting;
			}
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0004254C File Offset: 0x0004074C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (base.HasErrors)
			{
				return;
			}
			this.ChangeWinrmServiceSettings();
			base.WriteVerbose(Strings.VerboseCheckRequiredFiles);
			this.CheckConfigurationFilePaths();
			if (this.EnableKerberosModule)
			{
				this.InstallKerberosAuthenticationModule();
			}
			if (!this.isWSManInstalled)
			{
				return;
			}
			base.WriteVerbose(Strings.VerboseCheckRequiredRegistryKeys);
			this.CheckRequiredRegistryKeys();
			if (!this.isWSManInstalled)
			{
				return;
			}
			base.WriteVerbose(Strings.VerboseCheckIISConfiguration);
			this.CheckIISConfigurationFile();
			if (this.needToRestartWSMan)
			{
				base.RestartWSManService();
			}
			base.OpenHttpPortsOnFirewall();
			if (this.DataCenterCAS)
			{
				base.EnableBasicAuthForWSMan();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x000425F8 File Offset: 0x000407F8
		private void ChangeWinrmServiceSettings()
		{
			base.WriteVerbose(Strings.VerboseChangeWinrmStartType);
			try
			{
				string keyName = "HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\WinRM";
				Registry.SetValue(keyName, "Start", ServiceStartMode.Automatic, RegistryValueKind.DWord);
				Registry.SetValue(keyName, "DelayedAutoStart", 1, RegistryValueKind.DWord);
			}
			catch (SecurityException ex)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorChangeWinrmStartType(ex.ToString()), ex), ErrorCategory.InvalidOperation, null);
			}
			catch (IOException ex2)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorChangeWinrmStartType(ex2.ToString()), ex2), ErrorCategory.InvalidOperation, null);
			}
			base.WriteVerbose(Strings.VerboseStartWinrm);
			try
			{
				using (ServiceController serviceController = new ServiceController("winrm"))
				{
					if (serviceController.Status == ServiceControllerStatus.Stopped)
					{
						serviceController.Start();
					}
				}
			}
			catch (InvalidOperationException ex3)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorStartWinrm(ex3.ToString()), ex3), ErrorCategory.InvalidOperation, null);
			}
			catch (Win32Exception ex4)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorStartWinrm(ex4.ToString()), ex4), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0004273C File Offset: 0x0004093C
		private void CheckConfigurationFilePaths()
		{
			if (!File.Exists(base.AppcmdPath))
			{
				base.WriteError(new FileNotFoundException(Strings.ErrorAppCmdNotExist(base.AppcmdPath)), ErrorCategory.ObjectNotFound, null);
			}
			if (!File.Exists(base.IISConfigFilePath))
			{
				base.WriteError(new FileNotFoundException(Strings.ErrorAppCmdNotExist(base.IISConfigFilePath)), ErrorCategory.ObjectNotFound, null);
			}
			if (!File.Exists(base.WSManCfgSchemaFilePath))
			{
				if (!File.Exists(base.WSManCfgSchemaFileOriginalPath))
				{
					this.WriteWarning(Strings.ErrorWSManConfigSchemaFileNotFound(base.WSManCfgSchemaFilePath));
					this.isWSManInstalled = false;
					return;
				}
				File.Copy(base.WSManCfgSchemaFileOriginalPath, base.WSManCfgSchemaFilePath, true);
			}
			if (!File.Exists(base.WSManModuleFilePath))
			{
				this.WriteWarning(Strings.ErrorWSManModuleFileNotFound(base.WSManModuleFilePath));
				this.isWSManInstalled = false;
				return;
			}
			if (this.EnableKerberosModule && !File.Exists(base.KerberosAuthModuleFilePath))
			{
				this.WriteWarning(Strings.ErrorKerbauthModuleFileNotFound(base.KerberosAuthModuleFilePath));
				return;
			}
			if (!File.Exists(base.WinrmFilePath))
			{
				this.WriteWarning(Strings.ErrorWinRMCmdNotFound(base.WinrmFilePath));
				this.isWSManInstalled = false;
				return;
			}
			if (!File.Exists(base.NetFilePath))
			{
				base.WriteError(new FileNotFoundException(Strings.ErrorSystemFileNotFound(base.NetFilePath)), ErrorCategory.ObjectNotFound, null);
			}
			using (ServerManager serverManager = new ServerManager())
			{
				foreach (Site site in serverManager.Sites)
				{
					if (site.Id == 1L)
					{
						base.DefaultSiteName = site.Name;
						break;
					}
				}
			}
			if (base.DefaultSiteName == null)
			{
				base.WriteError(new InvalidOperationException(Strings.ErrorDefaultWebSiteNotExist(base.IISConfigFilePath)), ErrorCategory.InvalidOperation, null);
			}
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0004291C File Offset: 0x00040B1C
		private void CheckRequiredRegistryKeys()
		{
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\WSMAN"))
			{
				if (registryKey == null || registryKey.GetValue("StackVersion") == null)
				{
					this.WriteWarning(Strings.ErrorWSManRegistryCorrupted);
					this.isWSManInstalled = false;
				}
				else
				{
					if (registryKey.GetValue("UpdatedConfig") == null)
					{
						base.RebuildWSManRegistry();
						this.needToRestartWSMan = true;
					}
					using (RegistryKey registryKey2 = registryKey.OpenSubKey("Listener"))
					{
						if (registryKey2 == null)
						{
							base.RebuildWSManRegistry();
							this.needToRestartWSMan = true;
						}
					}
					using (RegistryKey registryKey3 = registryKey.OpenSubKey("Plugin"))
					{
						if (registryKey3 == null || registryKey3.SubKeyCount < 3)
						{
							base.RebuildWSManRegistry();
							this.needToRestartWSMan = true;
						}
					}
				}
			}
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00042A04 File Offset: 0x00040C04
		private void CheckIISConfigurationFile()
		{
			bool flag = false;
			bool flag2 = false;
			using (ServerManager serverManager = new ServerManager())
			{
				Site site = serverManager.Sites[base.DefaultSiteName];
				if (serverManager.ApplicationPools["MSExchangePowerShellAppPool"] == null)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorPowerShellVdirAppPoolNotExist("MSExchangePowerShellAppPool", base.IISConfigFilePath)), ErrorCategory.InvalidOperation, null);
				}
				if (site.Applications["/PowerShell"] == null)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorApplicationNotExist("PowerShell", base.IISConfigFilePath)), ErrorCategory.InvalidOperation, null);
				}
				if (site.Applications["/PowerShell"].VirtualDirectories["/"] == null)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorVdirNotExisted("PowerShell", base.IISConfigFilePath)), ErrorCategory.InvalidOperation, null);
				}
				if (this.DataCenterCAS)
				{
					if (site.Applications["/PowerShell-LiveID"] == null)
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorApplicationNotExist("PowerShell-LiveID", base.IISConfigFilePath)), ErrorCategory.InvalidOperation, null);
					}
					if (site.Applications["/PowerShell-LiveID"].VirtualDirectories["/"] == null)
					{
						base.WriteError(new InvalidOperationException(Strings.ErrorVdirNotExisted("PowerShell-LiveID", base.IISConfigFilePath)), ErrorCategory.InvalidOperation, null);
					}
				}
				bool flag3 = false;
				for (int i = 0; i < site.Bindings.Count; i++)
				{
					Binding binding = site.Bindings[i];
					if (string.Equals("https", binding.Protocol, StringComparison.InvariantCultureIgnoreCase))
					{
						flag3 = true;
						break;
					}
				}
				if (!flag3)
				{
					base.WriteError(new InvalidOperationException(Strings.ErrorHttpsBindingNotExist(base.IISConfigFilePath)), ErrorCategory.InvalidOperation, null);
				}
				SectionGroup sectionGroup = serverManager.GetApplicationHostConfiguration().RootSectionGroup.SectionGroups["system.webServer"];
				SectionDefinition sectionDefinition = sectionGroup.Sections["system.management.wsmanagement.config"];
				if (sectionDefinition == null)
				{
					base.BackupIISConfig();
					base.WriteVerbose(Strings.VerboseAddWSManConfigSection(base.IISConfigFilePath));
					sectionDefinition = sectionGroup.Sections.Add("system.management.wsmanagement.config");
					sectionDefinition.OverrideModeDefault = "Allow";
				}
				else if (!string.Equals(sectionDefinition.OverrideModeDefault, "Allow", StringComparison.InvariantCultureIgnoreCase))
				{
					base.BackupIISConfig();
					sectionDefinition.OverrideModeDefault = "Allow";
				}
				ConfigurationElementCollection collection = serverManager.GetApplicationHostConfiguration().GetSection("system.webServer/globalModules").GetCollection();
				ConfigurationElement configurationElement = null;
				for (int j = 0; j < collection.Count; j++)
				{
					ConfigurationElement configurationElement2 = collection[j];
					object attributeValue = configurationElement2.GetAttributeValue("name");
					if (attributeValue != null && string.Equals("WSMan", attributeValue.ToString(), StringComparison.InvariantCultureIgnoreCase))
					{
						configurationElement = configurationElement2;
						break;
					}
				}
				if (configurationElement == null)
				{
					flag2 = true;
				}
				else
				{
					object value = configurationElement.Attributes["image"].Value;
					if (value == null || !string.Equals(value.ToString(), base.WSManModuleFilePath, StringComparison.InvariantCultureIgnoreCase))
					{
						flag = true;
					}
				}
				if (base.IISBackupFileName != null)
				{
					serverManager.CommitChanges();
				}
			}
			if (flag || flag2)
			{
				base.BackupIISConfig();
			}
			if (flag)
			{
				string arguments = "uninstall module WSMan";
				base.WriteVerbose(Strings.VerboseUninstallWSManModule("WSMan"));
				base.ExecuteCmd(base.AppcmdPath, arguments, base.InetsrvPath, true, true);
			}
			if (flag2)
			{
				string arguments2 = "unlock config -section:system.webServer/modules";
				base.WriteVerbose(Strings.VerboseUnlockingModulesSection);
				base.ExecuteCmd(base.AppcmdPath, arguments2, base.InetsrvPath, true, true);
				arguments2 = "install module /name:WSMan /image:" + base.WSManModuleFilePath + " /add:false";
				base.WriteVerbose(Strings.VerboseInstallWSManModule("WSMan"));
				base.ExecuteCmd(base.AppcmdPath, arguments2, base.InetsrvPath, true, true);
			}
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00042DD0 File Offset: 0x00040FD0
		private void InstallKerberosAuthenticationModule()
		{
			bool flag = true;
			using (ServerManager serverManager = new ServerManager())
			{
				ConfigurationElementCollection collection = serverManager.GetApplicationHostConfiguration().GetSection("system.webServer/globalModules").GetCollection();
				ConfigurationElement configurationElement = null;
				for (int i = 0; i < collection.Count; i++)
				{
					ConfigurationElement configurationElement2 = collection[i];
					object attributeValue = configurationElement2.GetAttributeValue("name");
					if (attributeValue != null && string.Equals("Kerbauth", attributeValue.ToString(), StringComparison.OrdinalIgnoreCase))
					{
						configurationElement = configurationElement2;
						break;
					}
				}
				if (configurationElement != null)
				{
					flag = false;
				}
			}
			if (flag)
			{
				string arguments = "install module /name:kerbauth /image:\"" + base.KerberosAuthModuleFilePath + "\" /add:false";
				base.WriteVerbose(Strings.VerboseInstallKerberosAuthenticationModule(base.KerberosAuthModuleFilePath));
				base.ExecuteCmd(base.AppcmdPath, arguments, base.InetsrvPath, true, false);
			}
		}

		// Token: 0x04000709 RID: 1801
		private const string strWSManRegistryRoot = "Software\\Microsoft\\Windows\\CurrentVersion\\WSMAN";

		// Token: 0x0400070A RID: 1802
		private const string strWSManStackVersion = "StackVersion";

		// Token: 0x0400070B RID: 1803
		private const string strWSManUpdatedConfig = "UpdatedConfig";

		// Token: 0x0400070C RID: 1804
		private const string strWSManListener = "Listener";

		// Token: 0x0400070D RID: 1805
		private const string strWSManPlugin = "Plugin";

		// Token: 0x0400070E RID: 1806
		private const string paramDataCenterCAS = "DataCenterCAS";

		// Token: 0x0400070F RID: 1807
		private const string paramEnableKerbAuth = "EnableKerbAuth";

		// Token: 0x04000710 RID: 1808
		private const string strAllow = "Allow";

		// Token: 0x04000711 RID: 1809
		private const string strWSManAuthPlugin = "Microsoft.Exchange.AuthorizationPlugin.dll";

		// Token: 0x04000712 RID: 1810
		private bool needToRestartWSMan;

		// Token: 0x04000713 RID: 1811
		private bool isWSManInstalled = true;
	}
}
