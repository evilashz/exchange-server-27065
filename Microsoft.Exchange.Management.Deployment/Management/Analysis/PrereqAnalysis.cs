using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Diagnostics.Components.Setup;
using Microsoft.Exchange.Diagnostics.FaultInjection;
using Microsoft.Exchange.Management.Analysis.Builders;
using Microsoft.Exchange.Management.Analysis.Features;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Setup.Common;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Analysis
{
	// Token: 0x0200006D RID: 109
	internal class PrereqAnalysis : Analysis
	{
		// Token: 0x0600025A RID: 602 RVA: 0x00009670 File Offset: 0x00007870
		public PrereqAnalysis(IDataProviderFactory providers, SetupMode mode, SetupRole role, GlobalParameters globalParameters) : base(providers, delegate(AnalysisMember x)
		{
			AppliesToModeFeature appliesToModeFeature = x.Features.OfType<AppliesToModeFeature>().FirstOrDefault<AppliesToModeFeature>();
			AppliesToRoleFeature appliesToRoleFeature = x.Features.OfType<AppliesToRoleFeature>().FirstOrDefault<AppliesToRoleFeature>();
			return (appliesToModeFeature == null || appliesToModeFeature.Contains(mode)) && (appliesToRoleFeature == null || appliesToRoleFeature.Contains(role));
		}, delegate(AnalysisMember x)
		{
			AppliesToModeFeature appliesToModeFeature = x.Features.OfType<AppliesToModeFeature>().FirstOrDefault<AppliesToModeFeature>();
			AppliesToRoleFeature appliesToRoleFeature = x.Features.OfType<AppliesToRoleFeature>().FirstOrDefault<AppliesToRoleFeature>();
			return x is Rule && (appliesToModeFeature == null || appliesToModeFeature.Contains(mode)) && (appliesToRoleFeature == null || appliesToRoleFeature.Contains(role));
		})
		{
			PrereqAnalysis <>4__this = this;
			if (globalParameters == null)
			{
				throw new ArgumentNullException("globalParameters");
			}
			this.globalParameters = globalParameters;
			this.CreateGlobalParameterPrereqProperties();
			this.ComputerNameNetBIOS = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<string>(base.Providers.ManagedMethodProvider.GetComputerNameEx(ValidationConstant.ComputerNameFormat.ComputerNameNetBIOS)));
			this.ComputerNameDnsHostname = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<string>(base.Providers.ManagedMethodProvider.GetComputerNameEx(ValidationConstant.ComputerNameFormat.ComputerNameDnsHostname)));
			this.ComputerNameDnsDomain = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<string>(base.Providers.ManagedMethodProvider.GetComputerNameEx(ValidationConstant.ComputerNameFormat.ComputerNameDnsDomain)));
			this.ComputerNameDnsFullyQualified = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<string>(base.Providers.ManagedMethodProvider.GetComputerNameEx(ValidationConstant.ComputerNameFormat.ComputerNameDnsFullyQualified)));
			this.ComputerNameDiscrepancy = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.NetBIOSNameNotMatchesDNSHostName).Condition((Result<object> x) => new RuleResult(!string.Equals(this.ComputerNameDnsHostname.Results.Value, this.ComputerNameNetBIOS.Results.Value, StringComparison.CurrentCultureIgnoreCase)));
			this.FqdnMissing = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Gateway).Mode(SetupMode.Install).Error<RuleBuilder<object>>().Message((Result x) => Strings.FqdnMissing).Condition((Result<object> x) => new RuleResult(string.IsNullOrEmpty(this.ComputerNameDnsDomain.Results.Value)));
			this.DNSDomainNameNotValid = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.InvalidDNSDomainName).Condition((Result<object> x) => new RuleResult(!Regex.IsMatch(this.ComputerNameDnsDomain.Results.Value, "^[A-Za-z0-9\\-\\.]*$")));
			this.OrgMicrosoftExchServicesConfig = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesConfigDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesConfigDistinguishedName.Results.Value), new string[]
					{
						"distinguishedName",
						"msExchMixedMode",
						"msExchVersion"
					}, "objectClass=msExchOrganizationContainer", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.CreateMonadPrereqProperties();
			this.ShortServerName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_ComputerSystem")[0].TryGetValue("Name", out obj))
				{
					return new Result<string>(obj.ToString());
				}
				return new Result<string>(string.Empty);
			});
			this.LangPackBundleVersioning = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.LanguagePacks).Mode(SetupMode.Install).Error<RuleBuilder<object>>().Message((Result x) => Strings.LangPackBundleVersioning).Condition((Result<object> x) => new RuleResult(!this.globalParameters.LanguagePackVersioning));
			this.LangPackBundleCheck = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.LanguagePacks).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.LangPackBundleCheck).Condition((Result<object> x) => new RuleResult(!this.globalParameters.LanguagesAvailableToInstall));
			this.LangPackDiskSpaceCheck = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.LanguagePacks).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.LangPackDiskSpaceCheck).Condition((Result<object> x) => new RuleResult(!this.globalParameters.SufficientLanguagePackDiskSpace));
			this.LangPackUpgradeVersioning = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.LanguagePacks).Mode(SetupMode.Upgrade).Error<RuleBuilder<object>>().Message((Result x) => Strings.LangPackUpgradeVersioning).Condition((Result<object> x) => new RuleResult(!this.globalParameters.LanguagePackVersioning));
			this.LangPackInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.LanguagePacks).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Warning<RuleBuilder<object>>().Message((Result x) => Strings.LangPackInstalled).Condition((Result<object> x) => new RuleResult(this.globalParameters.LanguagePacksInstalled));
			this.AlreadyInstalledUMLangPacks = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.UmLanguagePack).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.AlreadyInstalledUMLangPacks(this.globalParameters.AlreadyInstalledUMLanguages)).Condition((Result<object> x) => new RuleResult(this.globalParameters.AlreadyInstalledUMLanguages != null && this.globalParameters.AlreadyInstalledUMLanguages != string.Empty));
			this.UMLangPackDiskSpaceCheck = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.UmLanguagePack).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.LangPackDiskSpaceCheck).Condition((Result<object> x) => new RuleResult(!this.globalParameters.SufficientLanguagePackDiskSpace));
			this.ComputerDomain = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_ComputerSystem")[0].TryGetValue("Domain", out obj))
				{
					return new Result<string>(obj.ToString());
				}
				return new Result<string>(string.Empty);
			});
			this.CreateActiveDirectoryPrereqProperties();
			this.CreateNativePrereqProperties();
			this.CreateWmiPrereqProperties();
			this.CreateRegistryPrereqProperties();
			this.Iis32BitMode = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.Iis32BitMode).Condition((Result<object> x) => new RuleResult(base.Providers.WebAdminDataProvider.Enable32BitAppOnWin64));
			this.HostingModeNotAvailable = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.HostingModeNotAvailable).Condition((Result<object> x) => new RuleResult(this.globalParameters.HostingDeploymentEnabled));
			this.DidTenantSettingCreatedAnException = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message(delegate(Result x)
			{
				if (!this.IsHybridObjectFoundOnPremises.Results.Result.HasException)
				{
					return this.IsExchangeVersionCorrectForTenant.Results.Result.Exception.Message;
				}
				return this.IsHybridObjectFoundOnPremises.Results.Result.Exception.Message;
			}).Condition((Result<object> x) => new RuleResult(this.IsHybridObjectFoundOnPremises.Results.ValueOrDefault && this.IsExchangeVersionCorrectForTenant.Results.Result.HasException));
			this.DidOnPremisesSettingCreatedAnException = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => this.IsHybridObjectFoundOnPremises.Results.Result.Exception.Message).Condition((Result<object> x) => new RuleResult(this.IsHybridObjectFoundOnPremises.Results.Result.HasException));
			this.HybridIsEnabledAndTenantVersionIsNotUpgraded = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.OrgConfigHashDoesNotExist).Condition((Result<object> x) => new RuleResult(!this.IsHybridObjectFoundOnPremises.Results.Result.HasException && !this.IsExchangeVersionCorrectForTenant.Results.Result.HasException && this.IsHybridObjectFoundOnPremises.Results.ValueOrDefault && !this.IsExchangeVersionCorrectForTenant.Results.ValueOrDefault));
			this.IsHybridObjectFoundOnPremises = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install | SetupMode.DisasterRecovery).SetValue((Result<object> x) => new Result<bool>(!this.WasSetupStartedFromGUI.Results.ValueOrDefault && base.Providers.HybridConfigurationDetectionProvider.RunOnPremisesHybridTest()));
			this.WasSetupStartedFromGUI = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				Process parentProcess = SetupPrereqChecks.GetParentProcess();
				string fileName = parentProcess.MainModule.FileName;
				parentProcess.Dispose();
				if (string.IsNullOrEmpty(fileName))
				{
					return new Result<bool>(false);
				}
				return new Result<bool>(fileName.Contains("SetupUI.exe"));
			});
			this.IsExchangeVersionCorrectForTenant = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install | SetupMode.DisasterRecovery).SetValue((Result<object> x) => new Result<bool>(base.Providers.HybridConfigurationDetectionProvider.RunTenantHybridTest(this.globalParameters.PathToDCHybridConfigFile)));
			this.ValidOSSuite = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if ((this.SuiteMask.Results.ValueOrDefault & 2U) > 0U || (this.SuiteMask.Results.ValueOrDefault & 128U) > 0U || this.SuiteMask.Results.ValueOrDefault == 272U)
				{
					value = true;
				}
				return new Result<bool>(value);
			});
			this.Windows2K8R2Version = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				OperatingSystem osversion = Environment.OSVersion;
				int num = 0;
				if (!string.IsNullOrEmpty(osversion.ServicePack))
				{
					MatchCollection matchCollection = Regex.Matches(osversion.ServicePack, "(\\d+\\.?\\d*|\\.\\d+)");
					if (matchCollection.Count == 1)
					{
						int.TryParse(matchCollection[0].Value, out num);
					}
				}
				if (this.OSProductType.Results.ValueOrDefault > 1U && this.ValidOSSuite.Results.ValueOrDefault && osversion.Version.Major == 6 && osversion.Version.Minor == 1 && num >= 1)
				{
					value = true;
				}
				return new Result<bool>(value);
			});
			this.Windows8Version = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				OperatingSystem osversion = Environment.OSVersion;
				if (this.OSProductType.Results.ValueOrDefault > 1U && osversion.Version.Major == 6 && osversion.Version.Minor >= 2)
				{
					value = true;
				}
				return new Result<bool>(value);
			});
			this.Windows8ClientVersion = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				OperatingSystem osversion = Environment.OSVersion;
				if (this.OSProductType.Results.ValueOrDefault == 1U && osversion.Version.Major == 6 && osversion.Version.Minor >= 2)
				{
					value = true;
				}
				return new Result<bool>(value);
			});
			this.Windows7ClientVersion = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				OperatingSystem osversion = Environment.OSVersion;
				int num = 0;
				if (!string.IsNullOrEmpty(osversion.ServicePack))
				{
					MatchCollection matchCollection = Regex.Matches(osversion.ServicePack, "(\\d+\\.?\\d*|\\.\\d+)");
					if (matchCollection.Count == 1)
					{
						int.TryParse(matchCollection[0].Value, out num);
					}
				}
				if (this.OSProductType.Results.ValueOrDefault == 1U && osversion.Version.Major == 6 && osversion.Version.Minor == 1 && num >= 1)
				{
					value = true;
				}
				return new Result<bool>(value);
			});
			this.InstalledWindowsFeatures = Setting<object[]>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				string[] array = new string[]
				{
					"WinRM-IIS-Ext",
					"RSAT-Web-Server",
					"Web-Mgmt-Console",
					"NET-Framework",
					"NET-Framework-45-Features",
					"Web-Net-Ext45",
					"Web-ISAPI-Ext",
					"Web-ASP-NET45",
					"RPC-over-HTTP-proxy",
					"Server-Gui-Mgmt-Infra",
					"NET-WCF-HTTP-Activation45",
					"RSAT-ADDS-Tools",
					"RSAT-Clustering",
					"RSAT-Clustering-Mgmt",
					"RSAT-Clustering-PowerShell",
					"RSAT-Clustering-CmdInterface"
				};
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Import-Module 'ServerManager';");
				for (int i = 0; i < array.Length; i++)
				{
					stringBuilder.Append("'-'");
					stringBuilder.Append("+");
					stringBuilder.Append(string.Format("(Get-WindowsFeature '{0}').Installed;", array[i]));
				}
				return new Result<object[]>(base.Providers.MonadDataProvider.ExecuteCommand(stringBuilder.ToString()));
			});
			this.IsWinRMIISExtensionInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 0)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[0];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsRSATWebServerInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Bridgehead).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 1)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[1];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsNETFrameworkInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 3)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[3];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsNETFramework45FeaturesInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 4)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[4];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsWebNetExt45Installed = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 5)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[5];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsWebISAPIExtInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 6)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[6];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsWebASPNET45Installed = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 7)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[7];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsRPCOverHTTPproxyInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 8)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[8];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsServerGuiMgmtInfraInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 9)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[9];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsWcfHttpActivation45Installed = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 10)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[10];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsRsatAddsToolsInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 11)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[11];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsRsatClusteringInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 12)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[12];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsRsatClusteringMgmtInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 13)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[13];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsRsatClusteringPowerShellInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 14)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[14];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.IsRsatClusteringCmdInterfaceInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.InstalledWindowsFeatures.Results.Value != null && this.InstalledWindowsFeatures.Results.Value.Length > 15)
				{
					string text = (string)this.InstalledWindowsFeatures.Results.Value[15];
					if (text.Length > 1)
					{
						value = Convert.ToBoolean(text.Substring(1));
					}
				}
				return new Result<bool>(value);
			});
			this.VCRedist2012Installed = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.Gateway).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.FileVersionAtl110.Results.Value.CompareTo(new Version("11.00.50727.1")) >= 0)
				{
					value = true;
				}
				return new Result<bool>(value);
			});
			this.VCRedist2013Installed = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.Mailbox | SetupRole.Bridgehead).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				if (this.FileVersionMsvcr120.Results.Value.CompareTo(new Version("12.00.21005.1")) >= 0)
				{
					value = true;
				}
				return new Result<bool>(value);
			});
			this.ValidOSVersion = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.InvalidOSVersion).Condition((Result<object> x) => new RuleResult(!this.Windows2K8R2Version.Results.ValueOrDefault && !this.Windows8Version.Results.ValueOrDefault));
			this.ValidOSVersionForAdminTools = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.InvalidOSVersionForAdminTools).Condition((Result<object> x) => new RuleResult(!this.Windows7ClientVersion.Results.ValueOrDefault && !this.Windows2K8R2Version.Results.ValueOrDefault && !this.Windows8ClientVersion.Results.ValueOrDefault && !this.Windows8Version.Results.ValueOrDefault));
			this.VC2012RedistDependencyRequirement = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Gateway).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.VC2012PrereqMissing).Condition((Result<object> x) => new RuleResult(!this.VCRedist2012Installed.Results.ValueOrDefault));
			this.VC2013RedistDependencyRequirement = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.VC2013PrereqMissing).Condition((Result<object> x) => new RuleResult(!this.VCRedist2013Installed.Results.ValueOrDefault));
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000A849 File Offset: 0x00008A49
		// (set) Token: 0x0600025C RID: 604 RVA: 0x0000A851 File Offset: 0x00008A51
		public Rule HostingModeNotAvailable { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000A85A File Offset: 0x00008A5A
		// (set) Token: 0x0600025E RID: 606 RVA: 0x0000A862 File Offset: 0x00008A62
		public Rule OSCheckedBuild { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000A86B File Offset: 0x00008A6B
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000A873 File Offset: 0x00008A73
		public Rule EventSystemStopped { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000A87C File Offset: 0x00008A7C
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000A884 File Offset: 0x00008A84
		public Rule MSDTCStopped { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000A88D File Offset: 0x00008A8D
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000A895 File Offset: 0x00008A95
		public Rule MpsSvcStopped { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000A89E File Offset: 0x00008A9E
		// (set) Token: 0x06000266 RID: 614 RVA: 0x0000A8A6 File Offset: 0x00008AA6
		public Rule NetTcpPortSharingSvcNotAuto { get; set; }

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000A8AF File Offset: 0x00008AAF
		// (set) Token: 0x06000268 RID: 616 RVA: 0x0000A8B7 File Offset: 0x00008AB7
		public Rule ComputerNotPartofDomain { get; set; }

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		public Rule WarningInstallExchangeRolesOnDomainController { get; set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600026B RID: 619 RVA: 0x0000A8D1 File Offset: 0x00008AD1
		// (set) Token: 0x0600026C RID: 620 RVA: 0x0000A8D9 File Offset: 0x00008AD9
		public Rule InstallOnDCInADSplitPermissionMode { get; set; }

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600026D RID: 621 RVA: 0x0000A8E2 File Offset: 0x00008AE2
		// (set) Token: 0x0600026E RID: 622 RVA: 0x0000A8EA File Offset: 0x00008AEA
		public Rule SetADSplitPermissionWhenExchangeServerRolesOnDC { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000A8F3 File Offset: 0x00008AF3
		// (set) Token: 0x06000270 RID: 624 RVA: 0x0000A8FB File Offset: 0x00008AFB
		public Rule ServerNameNotValid { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000271 RID: 625 RVA: 0x0000A904 File Offset: 0x00008B04
		// (set) Token: 0x06000272 RID: 626 RVA: 0x0000A90C File Offset: 0x00008B0C
		public Rule LocalComputerIsDCInChildDomain { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000A915 File Offset: 0x00008B15
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000A91D File Offset: 0x00008B1D
		public Rule LoggedOntoDomain { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000275 RID: 629 RVA: 0x0000A926 File Offset: 0x00008B26
		// (set) Token: 0x06000276 RID: 630 RVA: 0x0000A92E File Offset: 0x00008B2E
		public Rule PrimaryDNSTestFailed { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000277 RID: 631 RVA: 0x0000A937 File Offset: 0x00008B37
		// (set) Token: 0x06000278 RID: 632 RVA: 0x0000A93F File Offset: 0x00008B3F
		public Rule HostRecordMissing { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000279 RID: 633 RVA: 0x0000A948 File Offset: 0x00008B48
		// (set) Token: 0x0600027A RID: 634 RVA: 0x0000A950 File Offset: 0x00008B50
		public Rule LocalDomainModeMixed { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x0600027B RID: 635 RVA: 0x0000A959 File Offset: 0x00008B59
		// (set) Token: 0x0600027C RID: 636 RVA: 0x0000A961 File Offset: 0x00008B61
		public Rule DomainPrepRequired { get; set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000A96A File Offset: 0x00008B6A
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000A972 File Offset: 0x00008B72
		public Rule ComputerRODC { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000A97B File Offset: 0x00008B7B
		// (set) Token: 0x06000280 RID: 640 RVA: 0x0000A983 File Offset: 0x00008B83
		public Rule InvalidADSite { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000281 RID: 641 RVA: 0x0000A98C File Offset: 0x00008B8C
		// (set) Token: 0x06000282 RID: 642 RVA: 0x0000A994 File Offset: 0x00008B94
		public Rule W2K8R2PrepareSchemaLdifdeNotInstalled { get; set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000283 RID: 643 RVA: 0x0000A99D File Offset: 0x00008B9D
		// (set) Token: 0x06000284 RID: 644 RVA: 0x0000A9A5 File Offset: 0x00008BA5
		public Rule W2K8R2PrepareAdLdifdeNotInstalled { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000A9AE File Offset: 0x00008BAE
		// (set) Token: 0x06000286 RID: 646 RVA: 0x0000A9B6 File Offset: 0x00008BB6
		public Rule MailboxRoleAlreadyExists { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000287 RID: 647 RVA: 0x0000A9BF File Offset: 0x00008BBF
		// (set) Token: 0x06000288 RID: 648 RVA: 0x0000A9C7 File Offset: 0x00008BC7
		public Rule ClientAccessRoleAlreadyExists { get; set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000289 RID: 649 RVA: 0x0000A9D0 File Offset: 0x00008BD0
		// (set) Token: 0x0600028A RID: 650 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		public Rule UnifiedMessagingRoleAlreadyExists { get; set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600028B RID: 651 RVA: 0x0000A9E1 File Offset: 0x00008BE1
		// (set) Token: 0x0600028C RID: 652 RVA: 0x0000A9E9 File Offset: 0x00008BE9
		public Rule BridgeheadRoleAlreadyExists { get; set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000A9F2 File Offset: 0x00008BF2
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000A9FA File Offset: 0x00008BFA
		public Rule CafeRoleAlreadyExists { get; set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000AA03 File Offset: 0x00008C03
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000AA0B File Offset: 0x00008C0B
		public Rule FrontendTransportRoleAlreadyExists { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000AA14 File Offset: 0x00008C14
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000AA1C File Offset: 0x00008C1C
		public Rule ServerWinWebEdition { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000293 RID: 659 RVA: 0x0000AA25 File Offset: 0x00008C25
		// (set) Token: 0x06000294 RID: 660 RVA: 0x0000AA2D File Offset: 0x00008C2D
		public Rule BridgeheadRoleNotPresentInSite { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000AA36 File Offset: 0x00008C36
		// (set) Token: 0x06000296 RID: 662 RVA: 0x0000AA3E File Offset: 0x00008C3E
		public Rule ClientAccessRoleNotPresentInSite { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000AA47 File Offset: 0x00008C47
		// (set) Token: 0x06000298 RID: 664 RVA: 0x0000AA4F File Offset: 0x00008C4F
		public Rule LonghornWmspdmoxNotInstalled { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000AA58 File Offset: 0x00008C58
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000AA60 File Offset: 0x00008C60
		public Rule Exchange2000or2003PresentInOrg { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000AA69 File Offset: 0x00008C69
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000AA71 File Offset: 0x00008C71
		public Rule RebootPending { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000AA7A File Offset: 0x00008C7A
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000AA82 File Offset: 0x00008C82
		public Rule MinimumFrameworkNotInstalled { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000AA8B File Offset: 0x00008C8B
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000AA93 File Offset: 0x00008C93
		public Rule Win7RpcHttpAssocCookieGuidUpdateNotInstalled { get; set; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000AA9C File Offset: 0x00008C9C
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000AAA4 File Offset: 0x00008CA4
		public Rule SearchFoundationAssemblyLoaderKBNotInstalled { get; set; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000AAAD File Offset: 0x00008CAD
		// (set) Token: 0x060002A4 RID: 676 RVA: 0x0000AAB5 File Offset: 0x00008CB5
		public Rule Win2k12UrefsUpdateNotInstalled { get; set; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x0000AABE File Offset: 0x00008CBE
		// (set) Token: 0x060002A6 RID: 678 RVA: 0x0000AAC6 File Offset: 0x00008CC6
		public Rule Win2k12RefsUpdateNotInstalled { get; set; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000AACF File Offset: 0x00008CCF
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x0000AAD7 File Offset: 0x00008CD7
		public Rule Win2k12RollupUpdateNotInstalled { get; set; }

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x0000AAE0 File Offset: 0x00008CE0
		// (set) Token: 0x060002AA RID: 682 RVA: 0x0000AAE8 File Offset: 0x00008CE8
		public Rule UnifiedMessagingRoleNotInstalled { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000AAF1 File Offset: 0x00008CF1
		// (set) Token: 0x060002AC RID: 684 RVA: 0x0000AAF9 File Offset: 0x00008CF9
		public Rule BridgeheadRoleNotInstalled { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000AB02 File Offset: 0x00008D02
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000AB0A File Offset: 0x00008D0A
		public Rule NotLocalAdmin { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000AB13 File Offset: 0x00008D13
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000AB1B File Offset: 0x00008D1B
		public Rule FirstSGFilesExist { get; set; }

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000AB24 File Offset: 0x00008D24
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x0000AB2C File Offset: 0x00008D2C
		public Rule SecondSGFilesExist { get; set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x0000AB35 File Offset: 0x00008D35
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000AB3D File Offset: 0x00008D3D
		public Rule ExchangeVersionBlock { get; set; }

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000AB46 File Offset: 0x00008D46
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000AB4E File Offset: 0x00008D4E
		public Rule VoiceMessagesInQueue { get; set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000AB57 File Offset: 0x00008D57
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000AB5F File Offset: 0x00008D5F
		public Rule ProcessNeedsToBeClosedOnUpgrade { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000AB68 File Offset: 0x00008D68
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000AB70 File Offset: 0x00008D70
		public Rule ProcessNeedsToBeClosedOnUninstall { get; set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000AB79 File Offset: 0x00008D79
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000AB81 File Offset: 0x00008D81
		public Rule SendConnectorException { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000AB8A File Offset: 0x00008D8A
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000AB92 File Offset: 0x00008D92
		public Rule MailboxLogDriveDoesNotExist { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000AB9B File Offset: 0x00008D9B
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000ABA3 File Offset: 0x00008DA3
		public Rule MailboxEDBDriveDoesNotExist { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000ABAC File Offset: 0x00008DAC
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000ABB4 File Offset: 0x00008DB4
		public Rule ServerIsSourceForSendConnector { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x0000ABBD File Offset: 0x00008DBD
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x0000ABC5 File Offset: 0x00008DC5
		public Rule ServerIsGroupExpansionServer { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x0000ABCE File Offset: 0x00008DCE
		// (set) Token: 0x060002C6 RID: 710 RVA: 0x0000ABD6 File Offset: 0x00008DD6
		public Rule ServerIsDynamicGroupExpansionServer { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000ABDF File Offset: 0x00008DDF
		// (set) Token: 0x060002C8 RID: 712 RVA: 0x0000ABE7 File Offset: 0x00008DE7
		public Rule MemberOfDatabaseAvailabilityGroup { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002C9 RID: 713 RVA: 0x0000ABF0 File Offset: 0x00008DF0
		// (set) Token: 0x060002CA RID: 714 RVA: 0x0000ABF8 File Offset: 0x00008DF8
		public Rule DrMinVersionCheck { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060002CB RID: 715 RVA: 0x0000AC01 File Offset: 0x00008E01
		// (set) Token: 0x060002CC RID: 716 RVA: 0x0000AC09 File Offset: 0x00008E09
		public Rule RemoteRegException { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060002CD RID: 717 RVA: 0x0000AC12 File Offset: 0x00008E12
		// (set) Token: 0x060002CE RID: 718 RVA: 0x0000AC1A File Offset: 0x00008E1A
		public Rule WinRMIISExtensionInstalled { get; set; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060002CF RID: 719 RVA: 0x0000AC23 File Offset: 0x00008E23
		// (set) Token: 0x060002D0 RID: 720 RVA: 0x0000AC2B File Offset: 0x00008E2B
		public Rule LangPackBundleVersioning { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060002D1 RID: 721 RVA: 0x0000AC34 File Offset: 0x00008E34
		// (set) Token: 0x060002D2 RID: 722 RVA: 0x0000AC3C File Offset: 0x00008E3C
		public Rule LangPackBundleCheck { get; set; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x0000AC45 File Offset: 0x00008E45
		// (set) Token: 0x060002D4 RID: 724 RVA: 0x0000AC4D File Offset: 0x00008E4D
		public Rule LangPackDiskSpaceCheck { get; set; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x0000AC56 File Offset: 0x00008E56
		// (set) Token: 0x060002D6 RID: 726 RVA: 0x0000AC5E File Offset: 0x00008E5E
		public Rule LangPackInstalled { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x0000AC67 File Offset: 0x00008E67
		// (set) Token: 0x060002D8 RID: 728 RVA: 0x0000AC6F File Offset: 0x00008E6F
		public Rule AlreadyInstalledUMLangPacks { get; set; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002D9 RID: 729 RVA: 0x0000AC78 File Offset: 0x00008E78
		// (set) Token: 0x060002DA RID: 730 RVA: 0x0000AC80 File Offset: 0x00008E80
		public Rule UMLangPackDiskSpaceCheck { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002DB RID: 731 RVA: 0x0000AC89 File Offset: 0x00008E89
		// (set) Token: 0x060002DC RID: 732 RVA: 0x0000AC91 File Offset: 0x00008E91
		public Rule LangPackUpgradeVersioning { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002DD RID: 733 RVA: 0x0000AC9A File Offset: 0x00008E9A
		// (set) Token: 0x060002DE RID: 734 RVA: 0x0000ACA2 File Offset: 0x00008EA2
		public Rule PendingRebootWindowsComponents { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000ACAB File Offset: 0x00008EAB
		// (set) Token: 0x060002E0 RID: 736 RVA: 0x0000ACB3 File Offset: 0x00008EB3
		public Rule Iis32BitMode { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000ACBC File Offset: 0x00008EBC
		// (set) Token: 0x060002E2 RID: 738 RVA: 0x0000ACC4 File Offset: 0x00008EC4
		public Rule SchemaUpdateRequired { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000ACCD File Offset: 0x00008ECD
		// (set) Token: 0x060002E4 RID: 740 RVA: 0x0000ACD5 File Offset: 0x00008ED5
		public Rule AdUpdateRequired { get; set; }

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000ACDE File Offset: 0x00008EDE
		// (set) Token: 0x060002E6 RID: 742 RVA: 0x0000ACE6 File Offset: 0x00008EE6
		public Rule GlobalUpdateRequired { get; set; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000ACEF File Offset: 0x00008EEF
		// (set) Token: 0x060002E8 RID: 744 RVA: 0x0000ACF7 File Offset: 0x00008EF7
		public Rule DomainPrepWithoutADUpdate { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000AD00 File Offset: 0x00008F00
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000AD08 File Offset: 0x00008F08
		public Rule LocalDomainPrep { get; set; }

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000AD11 File Offset: 0x00008F11
		// (set) Token: 0x060002EC RID: 748 RVA: 0x0000AD19 File Offset: 0x00008F19
		public Rule GlobalServerInstall { get; set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000AD22 File Offset: 0x00008F22
		// (set) Token: 0x060002EE RID: 750 RVA: 0x0000AD2A File Offset: 0x00008F2A
		public Rule DelegatedBridgeheadFirstInstall { get; set; }

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000AD33 File Offset: 0x00008F33
		// (set) Token: 0x060002F0 RID: 752 RVA: 0x0000AD3B File Offset: 0x00008F3B
		public Rule DelegatedCafeFirstInstall { get; set; }

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000AD44 File Offset: 0x00008F44
		// (set) Token: 0x060002F2 RID: 754 RVA: 0x0000AD4C File Offset: 0x00008F4C
		public Rule DelegatedFrontendTransportFirstInstall { get; set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000AD55 File Offset: 0x00008F55
		// (set) Token: 0x060002F4 RID: 756 RVA: 0x0000AD5D File Offset: 0x00008F5D
		public Rule DelegatedMailboxFirstInstall { get; set; }

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000AD66 File Offset: 0x00008F66
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0000AD6E File Offset: 0x00008F6E
		public Rule DelegatedClientAccessFirstInstall { get; set; }

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060002F7 RID: 759 RVA: 0x0000AD77 File Offset: 0x00008F77
		// (set) Token: 0x060002F8 RID: 760 RVA: 0x0000AD7F File Offset: 0x00008F7F
		public Rule DelegatedUnifiedMessagingFirstInstall { get; set; }

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x0000AD88 File Offset: 0x00008F88
		// (set) Token: 0x060002FA RID: 762 RVA: 0x0000AD90 File Offset: 0x00008F90
		public Rule DelegatedBridgeheadFirstSP1upgrade { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0000AD99 File Offset: 0x00008F99
		// (set) Token: 0x060002FC RID: 764 RVA: 0x0000ADA1 File Offset: 0x00008FA1
		public Rule DelegatedUnifiedMessagingFirstSP1upgrade { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060002FD RID: 765 RVA: 0x0000ADAA File Offset: 0x00008FAA
		// (set) Token: 0x060002FE RID: 766 RVA: 0x0000ADB2 File Offset: 0x00008FB2
		public Rule DelegatedClientAccessFirstSP1upgrade { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x060002FF RID: 767 RVA: 0x0000ADBB File Offset: 0x00008FBB
		// (set) Token: 0x06000300 RID: 768 RVA: 0x0000ADC3 File Offset: 0x00008FC3
		public Rule DelegatedMailboxFirstSP1upgrade { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000301 RID: 769 RVA: 0x0000ADCC File Offset: 0x00008FCC
		// (set) Token: 0x06000302 RID: 770 RVA: 0x0000ADD4 File Offset: 0x00008FD4
		public Rule CannotUninstallDelegatedServer { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000ADDD File Offset: 0x00008FDD
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000ADE5 File Offset: 0x00008FE5
		public Rule PrepareDomainNotAdmin { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000ADEE File Offset: 0x00008FEE
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000ADF6 File Offset: 0x00008FF6
		public Rule NoE12ServerWarning { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000ADFF File Offset: 0x00008FFF
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000AE07 File Offset: 0x00009007
		public Rule NoE14ServerWarning { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000AE10 File Offset: 0x00009010
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000AE18 File Offset: 0x00009018
		public Rule NotInSchemaMasterDomain { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0000AE21 File Offset: 0x00009021
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0000AE29 File Offset: 0x00009029
		public Rule NotInSchemaMasterSite { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0000AE32 File Offset: 0x00009032
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0000AE3A File Offset: 0x0000903A
		public Rule ProvisionedUpdateRequired { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0000AE43 File Offset: 0x00009043
		// (set) Token: 0x06000310 RID: 784 RVA: 0x0000AE4B File Offset: 0x0000904B
		public Rule SchemaFSMONotWin2003SPn { get; set; }

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000311 RID: 785 RVA: 0x0000AE54 File Offset: 0x00009054
		// (set) Token: 0x06000312 RID: 786 RVA: 0x0000AE5C File Offset: 0x0000905C
		public Rule CannotUninstallClusterNode { get; set; }

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000313 RID: 787 RVA: 0x0000AE65 File Offset: 0x00009065
		// (set) Token: 0x06000314 RID: 788 RVA: 0x0000AE6D File Offset: 0x0000906D
		public Rule CannotUninstallOABServer { get; set; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0000AE76 File Offset: 0x00009076
		// (set) Token: 0x06000316 RID: 790 RVA: 0x0000AE7E File Offset: 0x0000907E
		public Rule DomainControllerIsOutOfSite { get; set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0000AE87 File Offset: 0x00009087
		// (set) Token: 0x06000318 RID: 792 RVA: 0x0000AE8F File Offset: 0x0000908F
		public Rule ComputerNameDiscrepancy { get; set; }

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0000AE98 File Offset: 0x00009098
		// (set) Token: 0x0600031A RID: 794 RVA: 0x0000AEA0 File Offset: 0x000090A0
		public Rule FqdnMissing { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0000AEA9 File Offset: 0x000090A9
		// (set) Token: 0x0600031C RID: 796 RVA: 0x0000AEB1 File Offset: 0x000090B1
		public Rule DNSDomainNameNotValid { get; set; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600031D RID: 797 RVA: 0x0000AEBA File Offset: 0x000090BA
		// (set) Token: 0x0600031E RID: 798 RVA: 0x0000AEC2 File Offset: 0x000090C2
		public Rule DidTenantSettingCreatedAnException { get; set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000AECB File Offset: 0x000090CB
		// (set) Token: 0x06000320 RID: 800 RVA: 0x0000AED3 File Offset: 0x000090D3
		public Rule DidOnPremisesSettingCreatedAnException { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0000AEDC File Offset: 0x000090DC
		// (set) Token: 0x06000322 RID: 802 RVA: 0x0000AEE4 File Offset: 0x000090E4
		public Rule HybridIsEnabledAndTenantVersionIsNotUpgraded { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0000AEED File Offset: 0x000090ED
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0000AEF5 File Offset: 0x000090F5
		public Rule AdcFound { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000AEFE File Offset: 0x000090FE
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000AF06 File Offset: 0x00009106
		public Rule AdInitErrorRule { get; set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000327 RID: 807 RVA: 0x0000AF0F File Offset: 0x0000910F
		// (set) Token: 0x06000328 RID: 808 RVA: 0x0000AF17 File Offset: 0x00009117
		public Rule NoConnectorToStar { get; set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000329 RID: 809 RVA: 0x0000AF20 File Offset: 0x00009120
		// (set) Token: 0x0600032A RID: 810 RVA: 0x0000AF28 File Offset: 0x00009128
		public Rule DuplicateShortProvisionedName { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600032B RID: 811 RVA: 0x0000AF31 File Offset: 0x00009131
		// (set) Token: 0x0600032C RID: 812 RVA: 0x0000AF39 File Offset: 0x00009139
		public Rule ForestLevelNotWin2003Native { get; set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000AF42 File Offset: 0x00009142
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000AF4A File Offset: 0x0000914A
		public Rule InhBlockPublicFolderTree { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000AF53 File Offset: 0x00009153
		// (set) Token: 0x06000330 RID: 816 RVA: 0x0000AF5B File Offset: 0x0000915B
		public Rule PrepareDomainNotFound { get; set; }

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000AF64 File Offset: 0x00009164
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000AF6C File Offset: 0x0000916C
		public Rule PrepareDomainModeMixed { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000AF75 File Offset: 0x00009175
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000AF7D File Offset: 0x0000917D
		public Rule RusMissing { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000AF86 File Offset: 0x00009186
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000AF8E File Offset: 0x0000918E
		public Rule ServerFQDNMatchesSMTPPolicy { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000AF97 File Offset: 0x00009197
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000AF9F File Offset: 0x0000919F
		public Rule SmtpAddressLiteral { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000AFA8 File Offset: 0x000091A8
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000AFB0 File Offset: 0x000091B0
		public Rule UnwillingToRemoveMailboxDatabase { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000AFB9 File Offset: 0x000091B9
		// (set) Token: 0x0600033C RID: 828 RVA: 0x0000AFC1 File Offset: 0x000091C1
		public Rule RootDomainModeMixed { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0000AFCA File Offset: 0x000091CA
		// (set) Token: 0x0600033E RID: 830 RVA: 0x0000AFD2 File Offset: 0x000091D2
		public Rule ServerRemoveProvisioningCheck { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600033F RID: 831 RVA: 0x0000AFDB File Offset: 0x000091DB
		// (set) Token: 0x06000340 RID: 832 RVA: 0x0000AFE3 File Offset: 0x000091E3
		public Rule InconsistentlyConfiguredDomain { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000341 RID: 833 RVA: 0x0000AFEC File Offset: 0x000091EC
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0000AFF4 File Offset: 0x000091F4
		public Rule OffLineABServerDeleted { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000343 RID: 835 RVA: 0x0000AFFD File Offset: 0x000091FD
		// (set) Token: 0x06000344 RID: 836 RVA: 0x0000B005 File Offset: 0x00009205
		public Rule ResourcePropertySchemaException { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0000B00E File Offset: 0x0000920E
		// (set) Token: 0x06000346 RID: 838 RVA: 0x0000B016 File Offset: 0x00009216
		public Rule MessagesInQueue { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000347 RID: 839 RVA: 0x0000B01F File Offset: 0x0000921F
		// (set) Token: 0x06000348 RID: 840 RVA: 0x0000B027 File Offset: 0x00009227
		public Rule AdditionalUMLangPackExists { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000B030 File Offset: 0x00009230
		// (set) Token: 0x0600034A RID: 842 RVA: 0x0000B038 File Offset: 0x00009238
		public Rule ExchangeAlreadyInstalled { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600034B RID: 843 RVA: 0x0000B041 File Offset: 0x00009241
		// (set) Token: 0x0600034C RID: 844 RVA: 0x0000B049 File Offset: 0x00009249
		public Rule InstallWatermark { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000B052 File Offset: 0x00009252
		// (set) Token: 0x0600034E RID: 846 RVA: 0x0000B05A File Offset: 0x0000925A
		public Rule InterruptedUninstallNotContinued { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600034F RID: 847 RVA: 0x0000B063 File Offset: 0x00009263
		// (set) Token: 0x06000350 RID: 848 RVA: 0x0000B06B File Offset: 0x0000926B
		public Rule W3SVCDisabledOrNotInstalled { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000351 RID: 849 RVA: 0x0000B074 File Offset: 0x00009274
		// (set) Token: 0x06000352 RID: 850 RVA: 0x0000B07C File Offset: 0x0000927C
		public Rule ShouldReRunSetupForW3SVC { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000353 RID: 851 RVA: 0x0000B085 File Offset: 0x00009285
		// (set) Token: 0x06000354 RID: 852 RVA: 0x0000B08D File Offset: 0x0000928D
		public Rule SMTPSvcInstalled { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000355 RID: 853 RVA: 0x0000B096 File Offset: 0x00009296
		// (set) Token: 0x06000356 RID: 854 RVA: 0x0000B09E File Offset: 0x0000929E
		public Rule ClusSvcInstalledRoleBlock { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000357 RID: 855 RVA: 0x0000B0A7 File Offset: 0x000092A7
		// (set) Token: 0x06000358 RID: 856 RVA: 0x0000B0AF File Offset: 0x000092AF
		public Rule LonghornIIS6MetabaseNotInstalled { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000359 RID: 857 RVA: 0x0000B0B8 File Offset: 0x000092B8
		// (set) Token: 0x0600035A RID: 858 RVA: 0x0000B0C0 File Offset: 0x000092C0
		public Rule LonghornIIS6MgmtConsoleNotInstalled { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600035B RID: 859 RVA: 0x0000B0C9 File Offset: 0x000092C9
		// (set) Token: 0x0600035C RID: 860 RVA: 0x0000B0D1 File Offset: 0x000092D1
		public Rule LonghornIIS7HttpCompressionDynamicNotInstalled { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600035D RID: 861 RVA: 0x0000B0DA File Offset: 0x000092DA
		// (set) Token: 0x0600035E RID: 862 RVA: 0x0000B0E2 File Offset: 0x000092E2
		public Rule LonghornIIS7HttpCompressionStaticNotInstalled { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600035F RID: 863 RVA: 0x0000B0EB File Offset: 0x000092EB
		// (set) Token: 0x06000360 RID: 864 RVA: 0x0000B0F3 File Offset: 0x000092F3
		public Rule LonghornWASProcessModelInstalled { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000361 RID: 865 RVA: 0x0000B0FC File Offset: 0x000092FC
		// (set) Token: 0x06000362 RID: 866 RVA: 0x0000B104 File Offset: 0x00009304
		public Rule LonghornIIS7BasicAuthNotInstalled { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000363 RID: 867 RVA: 0x0000B10D File Offset: 0x0000930D
		// (set) Token: 0x06000364 RID: 868 RVA: 0x0000B115 File Offset: 0x00009315
		public Rule LonghornIIS7WindowsAuthNotInstalled { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000365 RID: 869 RVA: 0x0000B11E File Offset: 0x0000931E
		// (set) Token: 0x06000366 RID: 870 RVA: 0x0000B126 File Offset: 0x00009326
		public Rule LonghornIIS7DigestAuthNotInstalled { get; set; }

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000367 RID: 871 RVA: 0x0000B12F File Offset: 0x0000932F
		// (set) Token: 0x06000368 RID: 872 RVA: 0x0000B137 File Offset: 0x00009337
		public Rule LonghornIIS7NetExt { get; set; }

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000369 RID: 873 RVA: 0x0000B140 File Offset: 0x00009340
		// (set) Token: 0x0600036A RID: 874 RVA: 0x0000B148 File Offset: 0x00009348
		public Rule LonghornIIS6WMICompatibility { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600036B RID: 875 RVA: 0x0000B151 File Offset: 0x00009351
		// (set) Token: 0x0600036C RID: 876 RVA: 0x0000B159 File Offset: 0x00009359
		public Rule LonghornASPNET { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600036D RID: 877 RVA: 0x0000B162 File Offset: 0x00009362
		// (set) Token: 0x0600036E RID: 878 RVA: 0x0000B16A File Offset: 0x0000936A
		public Rule LonghornISAPIFilter { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600036F RID: 879 RVA: 0x0000B173 File Offset: 0x00009373
		// (set) Token: 0x06000370 RID: 880 RVA: 0x0000B17B File Offset: 0x0000937B
		public Rule LonghornClientCertificateMappingAuthentication { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000371 RID: 881 RVA: 0x0000B184 File Offset: 0x00009384
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000B18C File Offset: 0x0000938C
		public Rule LonghornDirectoryBrowse { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000B195 File Offset: 0x00009395
		// (set) Token: 0x06000374 RID: 884 RVA: 0x0000B19D File Offset: 0x0000939D
		public Rule LonghornHttpErrors { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000375 RID: 885 RVA: 0x0000B1A6 File Offset: 0x000093A6
		// (set) Token: 0x06000376 RID: 886 RVA: 0x0000B1AE File Offset: 0x000093AE
		public Rule LonghornHttpLogging { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000377 RID: 887 RVA: 0x0000B1B7 File Offset: 0x000093B7
		// (set) Token: 0x06000378 RID: 888 RVA: 0x0000B1BF File Offset: 0x000093BF
		public Rule LonghornHttpRedirect { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000379 RID: 889 RVA: 0x0000B1C8 File Offset: 0x000093C8
		// (set) Token: 0x0600037A RID: 890 RVA: 0x0000B1D0 File Offset: 0x000093D0
		public Rule LonghornHttpTracing { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600037B RID: 891 RVA: 0x0000B1D9 File Offset: 0x000093D9
		// (set) Token: 0x0600037C RID: 892 RVA: 0x0000B1E1 File Offset: 0x000093E1
		public Rule LonghornRequestMonitor { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x0600037D RID: 893 RVA: 0x0000B1EA File Offset: 0x000093EA
		// (set) Token: 0x0600037E RID: 894 RVA: 0x0000B1F2 File Offset: 0x000093F2
		public Rule LonghornStaticContent { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600037F RID: 895 RVA: 0x0000B1FB File Offset: 0x000093FB
		// (set) Token: 0x06000380 RID: 896 RVA: 0x0000B203 File Offset: 0x00009403
		public Rule ManagementServiceInstalled { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000381 RID: 897 RVA: 0x0000B20C File Offset: 0x0000940C
		// (set) Token: 0x06000382 RID: 898 RVA: 0x0000B214 File Offset: 0x00009414
		public Rule HttpActivationInstalled { get; set; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0000B21D File Offset: 0x0000941D
		// (set) Token: 0x06000384 RID: 900 RVA: 0x0000B225 File Offset: 0x00009425
		public Rule WcfHttpActivation45Installed { get; set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000385 RID: 901 RVA: 0x0000B22E File Offset: 0x0000942E
		// (set) Token: 0x06000386 RID: 902 RVA: 0x0000B236 File Offset: 0x00009436
		public Rule RsatAddsToolsInstalled { get; set; }

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0000B23F File Offset: 0x0000943F
		// (set) Token: 0x06000388 RID: 904 RVA: 0x0000B247 File Offset: 0x00009447
		public Rule RsatClusteringInstalled { get; set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000389 RID: 905 RVA: 0x0000B250 File Offset: 0x00009450
		// (set) Token: 0x0600038A RID: 906 RVA: 0x0000B258 File Offset: 0x00009458
		public Rule RsatClusteringMgmtInstalled { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600038B RID: 907 RVA: 0x0000B261 File Offset: 0x00009461
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0000B269 File Offset: 0x00009469
		public Rule RsatClusteringPowerShellInstalled { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600038D RID: 909 RVA: 0x0000B272 File Offset: 0x00009472
		// (set) Token: 0x0600038E RID: 910 RVA: 0x0000B27A File Offset: 0x0000947A
		public Rule RsatClusteringCmdInterfaceInstalled { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0000B283 File Offset: 0x00009483
		// (set) Token: 0x06000390 RID: 912 RVA: 0x0000B28B File Offset: 0x0000948B
		public Rule UcmaRedistMsi { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000391 RID: 913 RVA: 0x0000B294 File Offset: 0x00009494
		// (set) Token: 0x06000392 RID: 914 RVA: 0x0000B29C File Offset: 0x0000949C
		public Rule SpeechRedistMsi { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0000B2A5 File Offset: 0x000094A5
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0000B2AD File Offset: 0x000094AD
		public Rule Win7WindowsIdentityFoundationUpdateNotInstalled { get; set; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0000B2B6 File Offset: 0x000094B6
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0000B2BE File Offset: 0x000094BE
		public Rule Win8WindowsIdentityFoundationUpdateNotInstalled { get; set; }

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0000B2C7 File Offset: 0x000094C7
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0000B2CF File Offset: 0x000094CF
		public Rule MailboxRoleNotInstalled { get; set; }

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0000B2D8 File Offset: 0x000094D8
		// (set) Token: 0x0600039A RID: 922 RVA: 0x0000B2E0 File Offset: 0x000094E0
		public Rule MailboxMinVersionCheck { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600039B RID: 923 RVA: 0x0000B2E9 File Offset: 0x000094E9
		// (set) Token: 0x0600039C RID: 924 RVA: 0x0000B2F1 File Offset: 0x000094F1
		public Rule MailboxUpgradeMinVersionBlock { get; set; }

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600039D RID: 925 RVA: 0x0000B2FA File Offset: 0x000094FA
		// (set) Token: 0x0600039E RID: 926 RVA: 0x0000B302 File Offset: 0x00009502
		public Rule UnifiedMessagingMinVersionCheck { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000B30B File Offset: 0x0000950B
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000B313 File Offset: 0x00009513
		public Rule UnifiedMessagingUpgradeMinVersionBlock { get; set; }

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000B31C File Offset: 0x0000951C
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x0000B324 File Offset: 0x00009524
		public Rule BridgeheadMinVersionCheck { get; set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x0000B32D File Offset: 0x0000952D
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x0000B335 File Offset: 0x00009535
		public Rule BridgeheadUpgradeMinVersionBlock { get; set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0000B33E File Offset: 0x0000953E
		// (set) Token: 0x060003A6 RID: 934 RVA: 0x0000B346 File Offset: 0x00009546
		public Rule Exchange2013AnyOnExchange2007Server { get; set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x0000B34F File Offset: 0x0000954F
		// (set) Token: 0x060003A8 RID: 936 RVA: 0x0000B357 File Offset: 0x00009557
		public Rule Exchange2010ServerOnExchange2007AdminTools { get; set; }

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000B360 File Offset: 0x00009560
		// (set) Token: 0x060003AA RID: 938 RVA: 0x0000B368 File Offset: 0x00009568
		public Rule UpdateNeedsReboot { get; set; }

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000B371 File Offset: 0x00009571
		// (set) Token: 0x060003AC RID: 940 RVA: 0x0000B379 File Offset: 0x00009579
		public Rule CannotAccessAD { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060003AD RID: 941 RVA: 0x0000B382 File Offset: 0x00009582
		// (set) Token: 0x060003AE RID: 942 RVA: 0x0000B38A File Offset: 0x0000958A
		public Rule ConfigDCHostNameMismatch { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000B393 File Offset: 0x00009593
		// (set) Token: 0x060003B0 RID: 944 RVA: 0x0000B39B File Offset: 0x0000959B
		public Rule OldADAMInstalled { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000B3A4 File Offset: 0x000095A4
		// (set) Token: 0x060003B2 RID: 946 RVA: 0x0000B3AC File Offset: 0x000095AC
		public Rule ADAMWin7ServerInstalled { get; set; }

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000B3B5 File Offset: 0x000095B5
		// (set) Token: 0x060003B4 RID: 948 RVA: 0x0000B3BD File Offset: 0x000095BD
		public Rule UpgradeGateway605Block { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000B3C6 File Offset: 0x000095C6
		// (set) Token: 0x060003B6 RID: 950 RVA: 0x0000B3CE File Offset: 0x000095CE
		public Rule GatewayMinVersionCheck { get; set; }

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x060003B7 RID: 951 RVA: 0x0000B3D7 File Offset: 0x000095D7
		// (set) Token: 0x060003B8 RID: 952 RVA: 0x0000B3DF File Offset: 0x000095DF
		public Rule GatewayUpgradeMinVersionBlock { get; set; }

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0000B3E8 File Offset: 0x000095E8
		// (set) Token: 0x060003BA RID: 954 RVA: 0x0000B3F0 File Offset: 0x000095F0
		public Rule ADAMSvcStopped { get; set; }

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000B3F9 File Offset: 0x000095F9
		// (set) Token: 0x060003BC RID: 956 RVA: 0x0000B401 File Offset: 0x00009601
		public Rule TargetPathCompressed { get; set; }

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000B40A File Offset: 0x0000960A
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000B412 File Offset: 0x00009612
		public Rule GatewayUpgrade605Block { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000B41B File Offset: 0x0000961B
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000B423 File Offset: 0x00009623
		public Rule ADAMDataPathExists { get; set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000B42C File Offset: 0x0000962C
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000B434 File Offset: 0x00009634
		public Rule EdgeSubscriptionExists { get; set; }

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000B43D File Offset: 0x0000963D
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000B445 File Offset: 0x00009645
		public Rule ADAMPortAlreadyInUse { get; set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000B44E File Offset: 0x0000964E
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000B456 File Offset: 0x00009656
		public Rule ADAMSSLPortAlreadyInUse { get; set; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000B45F File Offset: 0x0000965F
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000B467 File Offset: 0x00009667
		public Rule ServerIsLastHubForEdgeSubscription { get; set; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000B470 File Offset: 0x00009670
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000B478 File Offset: 0x00009678
		public Rule LonghornIIS7ManagementConsoleInstalled { get; set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000B481 File Offset: 0x00009681
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000B489 File Offset: 0x00009689
		public Rule WindowsInstallerServiceDisabledOrNotInstalled { get; set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000B492 File Offset: 0x00009692
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0000B49A File Offset: 0x0000969A
		public Rule WinRMServiceDisabledOrNotInstalled { get; set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000B4A3 File Offset: 0x000096A3
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0000B4AB File Offset: 0x000096AB
		public Rule RSATWebServerNotInstalled { get; set; }

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000B4B4 File Offset: 0x000096B4
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0000B4BC File Offset: 0x000096BC
		public Rule NETFrameworkNotInstalled { get; set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000B4C5 File Offset: 0x000096C5
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0000B4CD File Offset: 0x000096CD
		public Rule NETFramework45FeaturesNotInstalled { get; set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x0000B4D6 File Offset: 0x000096D6
		// (set) Token: 0x060003D6 RID: 982 RVA: 0x0000B4DE File Offset: 0x000096DE
		public Rule WebNetExt45NotInstalled { get; set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000B4E7 File Offset: 0x000096E7
		// (set) Token: 0x060003D8 RID: 984 RVA: 0x0000B4EF File Offset: 0x000096EF
		public Rule WebISAPIExtNotInstalled { get; set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060003D9 RID: 985 RVA: 0x0000B4F8 File Offset: 0x000096F8
		// (set) Token: 0x060003DA RID: 986 RVA: 0x0000B500 File Offset: 0x00009700
		public Rule WebASPNET45NotInstalled { get; set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060003DB RID: 987 RVA: 0x0000B509 File Offset: 0x00009709
		// (set) Token: 0x060003DC RID: 988 RVA: 0x0000B511 File Offset: 0x00009711
		public Rule RPCOverHTTPproxyNotInstalled { get; set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060003DD RID: 989 RVA: 0x0000B51A File Offset: 0x0000971A
		// (set) Token: 0x060003DE RID: 990 RVA: 0x0000B522 File Offset: 0x00009722
		public Rule ServerGuiMgmtInfraNotInstalled { get; set; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060003DF RID: 991 RVA: 0x0000B52B File Offset: 0x0000972B
		// (set) Token: 0x060003E0 RID: 992 RVA: 0x0000B533 File Offset: 0x00009733
		public Rule E15E14CoexistenceMinVersionRequirement { get; set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060003E1 RID: 993 RVA: 0x0000B53C File Offset: 0x0000973C
		// (set) Token: 0x060003E2 RID: 994 RVA: 0x0000B544 File Offset: 0x00009744
		public Rule E15E14CoexistenceMinMajorVersionRequirement { get; set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003E3 RID: 995 RVA: 0x0000B54D File Offset: 0x0000974D
		// (set) Token: 0x060003E4 RID: 996 RVA: 0x0000B555 File Offset: 0x00009755
		public Rule E15E12CoexistenceMinVersionRequirement { get; set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x0000B55E File Offset: 0x0000975E
		// (set) Token: 0x060003E6 RID: 998 RVA: 0x0000B566 File Offset: 0x00009766
		public Rule E15E14CoexistenceMinVersionRequirementForDC { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060003E7 RID: 999 RVA: 0x0000B56F File Offset: 0x0000976F
		// (set) Token: 0x060003E8 RID: 1000 RVA: 0x0000B577 File Offset: 0x00009777
		public Rule AllServersOfHigherVersionRule { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x0000B580 File Offset: 0x00009780
		// (set) Token: 0x060003EA RID: 1002 RVA: 0x0000B588 File Offset: 0x00009788
		public Rule WindowsServer2008CoreServerEdition { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060003EB RID: 1003 RVA: 0x0000B591 File Offset: 0x00009791
		// (set) Token: 0x060003EC RID: 1004 RVA: 0x0000B599 File Offset: 0x00009799
		public Rule ValidOSVersion { get; set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000B5A2 File Offset: 0x000097A2
		// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000B5AA File Offset: 0x000097AA
		public Rule ValidOSVersionForAdminTools { get; set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000B5B3 File Offset: 0x000097B3
		// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000B5BB File Offset: 0x000097BB
		public Rule Exchange2013AnyOnExchange2010Server { get; set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003F1 RID: 1009 RVA: 0x0000B5C4 File Offset: 0x000097C4
		// (set) Token: 0x060003F2 RID: 1010 RVA: 0x0000B5CC File Offset: 0x000097CC
		public Rule ServicesAreMarkedForDeletion { get; set; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000B5D5 File Offset: 0x000097D5
		// (set) Token: 0x060003F4 RID: 1012 RVA: 0x0000B5DD File Offset: 0x000097DD
		public Rule PowerShellExecutionPolicyCheckSet { get; set; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003F5 RID: 1013 RVA: 0x0000B5E6 File Offset: 0x000097E6
		// (set) Token: 0x060003F6 RID: 1014 RVA: 0x0000B5EE File Offset: 0x000097EE
		public Rule VC2012RedistDependencyRequirement { get; set; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003F7 RID: 1015 RVA: 0x0000B5F7 File Offset: 0x000097F7
		// (set) Token: 0x060003F8 RID: 1016 RVA: 0x0000B5FF File Offset: 0x000097FF
		public Rule VC2013RedistDependencyRequirement { get; set; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000B608 File Offset: 0x00009808
		// (set) Token: 0x060003FA RID: 1018 RVA: 0x0000B610 File Offset: 0x00009810
		public Setting<MailboxServer> CmdletGetMailboxServerResult { get; set; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000B619 File Offset: 0x00009819
		// (set) Token: 0x060003FC RID: 1020 RVA: 0x0000B621 File Offset: 0x00009821
		public Setting<ExchangeServer> CmdletGetExchangeServerResult { get; set; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000B62A File Offset: 0x0000982A
		// (set) Token: 0x060003FE RID: 1022 RVA: 0x0000B632 File Offset: 0x00009832
		public Setting<string> ShortServerName { get; set; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000B63B File Offset: 0x0000983B
		// (set) Token: 0x06000400 RID: 1024 RVA: 0x0000B643 File Offset: 0x00009843
		public Setting<bool> DebugVersion { get; set; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000B64C File Offset: 0x0000984C
		// (set) Token: 0x06000402 RID: 1026 RVA: 0x0000B654 File Offset: 0x00009854
		public Setting<ushort> DomainRole { get; set; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000403 RID: 1027 RVA: 0x0000B65D File Offset: 0x0000985D
		// (set) Token: 0x06000404 RID: 1028 RVA: 0x0000B665 File Offset: 0x00009865
		public Setting<bool> LocalComputerIsDomainController { get; set; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000B66E File Offset: 0x0000986E
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000B676 File Offset: 0x00009876
		public Setting<bool> ADSplitPermissionMode { get; set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000407 RID: 1031 RVA: 0x0000B67F File Offset: 0x0000987F
		// (set) Token: 0x06000408 RID: 1032 RVA: 0x0000B687 File Offset: 0x00009887
		public Setting<bool> EventSystemStarted { get; set; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000409 RID: 1033 RVA: 0x0000B690 File Offset: 0x00009890
		// (set) Token: 0x0600040A RID: 1034 RVA: 0x0000B698 File Offset: 0x00009898
		public Setting<bool> MSDTCStarted { get; set; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x0600040B RID: 1035 RVA: 0x0000B6A1 File Offset: 0x000098A1
		// (set) Token: 0x0600040C RID: 1036 RVA: 0x0000B6A9 File Offset: 0x000098A9
		public Setting<bool> MpsSvcStarted { get; set; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x0600040D RID: 1037 RVA: 0x0000B6B2 File Offset: 0x000098B2
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000B6BA File Offset: 0x000098BA
		public Setting<string> NetTcpPortSharingStartMode { get; set; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x0600040F RID: 1039 RVA: 0x0000B6C3 File Offset: 0x000098C3
		// (set) Token: 0x06000410 RID: 1040 RVA: 0x0000B6CB File Offset: 0x000098CB
		public Setting<string> WindowsVersion { get; set; }

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x06000411 RID: 1041 RVA: 0x0000B6D4 File Offset: 0x000098D4
		// (set) Token: 0x06000412 RID: 1042 RVA: 0x0000B6DC File Offset: 0x000098DC
		public Setting<string> WindowsBuild { get; set; }

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000B6E5 File Offset: 0x000098E5
		// (set) Token: 0x06000414 RID: 1044 RVA: 0x0000B6ED File Offset: 0x000098ED
		public Setting<ResultPropertyCollection> MsExServicesConfigDNOtherWellKnownObjects { get; set; }

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000B6F6 File Offset: 0x000098F6
		// (set) Token: 0x06000416 RID: 1046 RVA: 0x0000B6FE File Offset: 0x000098FE
		public Setting<string> MsExServicesConfigDNOtherWellKnownObjectsEWPDN { get; set; }

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0000B707 File Offset: 0x00009907
		// (set) Token: 0x06000418 RID: 1048 RVA: 0x0000B70F File Offset: 0x0000990F
		public Setting<string> MsExServicesConfigDNOtherWellKnownObjectsETSDN { get; set; }

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0000B718 File Offset: 0x00009918
		// (set) Token: 0x0600041A RID: 1050 RVA: 0x0000B720 File Offset: 0x00009920
		public Setting<string> EWPDn { get; set; }

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x0600041B RID: 1051 RVA: 0x0000B729 File Offset: 0x00009929
		// (set) Token: 0x0600041C RID: 1052 RVA: 0x0000B731 File Offset: 0x00009931
		public Setting<string> ETSDn { get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000B73A File Offset: 0x0000993A
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x0000B742 File Offset: 0x00009942
		public Setting<bool> ETSIsMemberOfEWP { get; set; }

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000B74B File Offset: 0x0000994B
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0000B753 File Offset: 0x00009953
		public Setting<string> DomainControllerCN { get; set; }

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000B75C File Offset: 0x0000995C
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x0000B764 File Offset: 0x00009964
		public Setting<int> ExchangeServerRolesOnDomainController { get; set; }

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000B76D File Offset: 0x0000996D
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x0000B775 File Offset: 0x00009975
		public Setting<string> LocalServerName { get; set; }

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000B77E File Offset: 0x0000997E
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x0000B786 File Offset: 0x00009986
		public Setting<bool> IsGlobalCatalogReady { get; set; }

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000B78F File Offset: 0x0000998F
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x0000B797 File Offset: 0x00009997
		public Setting<string> CurrentLogOn { get; set; }

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x0000B7A0 File Offset: 0x000099A0
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x0000B7A8 File Offset: 0x000099A8
		public Setting<ushort> AddressWidth { get; set; }

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000B7B1 File Offset: 0x000099B1
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x0000B7B9 File Offset: 0x000099B9
		public Setting<bool> AddressWidth32Bit { get; set; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000B7C2 File Offset: 0x000099C2
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0000B7CA File Offset: 0x000099CA
		public Setting<bool> AddressWidth64Bit { get; set; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000B7D3 File Offset: 0x000099D3
		// (set) Token: 0x06000430 RID: 1072 RVA: 0x0000B7DB File Offset: 0x000099DB
		public Setting<string> NicCaption { get; set; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000B7E4 File Offset: 0x000099E4
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000B7EC File Offset: 0x000099EC
		public Setting<Dictionary<string, object>> NicConfiguration { get; set; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000B7F5 File Offset: 0x000099F5
		// (set) Token: 0x06000434 RID: 1076 RVA: 0x0000B7FD File Offset: 0x000099FD
		public Setting<string> IPAddress { get; set; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x0000B806 File Offset: 0x00009A06
		// (set) Token: 0x06000436 RID: 1078 RVA: 0x0000B80E File Offset: 0x00009A0E
		public Setting<string> IPv4Address { get; set; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000B817 File Offset: 0x00009A17
		// (set) Token: 0x06000438 RID: 1080 RVA: 0x0000B81F File Offset: 0x00009A1F
		public Setting<bool> IPv6Enabled { get; set; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000B828 File Offset: 0x00009A28
		// (set) Token: 0x0600043A RID: 1082 RVA: 0x0000B830 File Offset: 0x00009A30
		public Setting<string> DnsAddress { get; set; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0000B839 File Offset: 0x00009A39
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0000B841 File Offset: 0x00009A41
		public Setting<string> PrimaryDns { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600043D RID: 1085 RVA: 0x0000B84A File Offset: 0x00009A4A
		// (set) Token: 0x0600043E RID: 1086 RVA: 0x0000B852 File Offset: 0x00009A52
		public Setting<bool> PrimaryDNSPortAvailable { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x0600043F RID: 1087 RVA: 0x0000B85B File Offset: 0x00009A5B
		// (set) Token: 0x06000440 RID: 1088 RVA: 0x0000B863 File Offset: 0x00009A63
		public Setting<Dictionary<string, object[]>> HostRecord { get; set; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000441 RID: 1089 RVA: 0x0000B86C File Offset: 0x00009A6C
		// (set) Token: 0x06000442 RID: 1090 RVA: 0x0000B874 File Offset: 0x00009A74
		public Setting<string> ComputerDomain { get; set; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000443 RID: 1091 RVA: 0x0000B87D File Offset: 0x00009A7D
		// (set) Token: 0x06000444 RID: 1092 RVA: 0x0000B885 File Offset: 0x00009A85
		public Setting<string> ComputerDomainDN { get; set; }

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000445 RID: 1093 RVA: 0x0000B88E File Offset: 0x00009A8E
		// (set) Token: 0x06000446 RID: 1094 RVA: 0x0000B896 File Offset: 0x00009A96
		public Setting<int> NtMixedDomainComputerDomainDN { get; set; }

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000B89F File Offset: 0x00009A9F
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0000B8A7 File Offset: 0x00009AA7
		public Setting<bool> LocalDomainIsPrepped { get; set; }

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000B8B0 File Offset: 0x00009AB0
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		public Setting<string> SiteName { get; set; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000B8C1 File Offset: 0x00009AC1
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000B8C9 File Offset: 0x00009AC9
		public Setting<string> ReadOnlyDC { get; set; }

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000B8D2 File Offset: 0x00009AD2
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000B8DA File Offset: 0x00009ADA
		public Setting<string> ServerRef { get; set; }

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000B8E3 File Offset: 0x00009AE3
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x0000B8EB File Offset: 0x00009AEB
		public Setting<string> ADServerDN { get; set; }

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000B8F4 File Offset: 0x00009AF4
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0000B8FC File Offset: 0x00009AFC
		public Setting<string> OperatingSystemVersion { get; set; }

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000B905 File Offset: 0x00009B05
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0000B90D File Offset: 0x00009B0D
		public Setting<string> WindowsPath { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x06000455 RID: 1109 RVA: 0x0000B916 File Offset: 0x00009B16
		// (set) Token: 0x06000456 RID: 1110 RVA: 0x0000B91E File Offset: 0x00009B1E
		public Setting<Version> FileVersionLdifde { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000B927 File Offset: 0x00009B27
		// (set) Token: 0x06000458 RID: 1112 RVA: 0x0000B92F File Offset: 0x00009B2F
		public Setting<uint> OSProductSuite { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000B938 File Offset: 0x00009B38
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0000B940 File Offset: 0x00009B40
		public Setting<string> BridgeheadRoleInCurrentADSite { get; set; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x0000B949 File Offset: 0x00009B49
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x0000B951 File Offset: 0x00009B51
		public Setting<string> ClientAccessRoleInCurrentADSite { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000B95A File Offset: 0x00009B5A
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x0000B962 File Offset: 0x00009B62
		public Setting<Version> FileVersionWmspdmod { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000B96B File Offset: 0x00009B6B
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x0000B973 File Offset: 0x00009B73
		public Setting<Version> FileVersionMSXML6 { get; set; }

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000B97C File Offset: 0x00009B7C
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000B984 File Offset: 0x00009B84
		public Setting<Version> FileVersionWmspdmoe { get; set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000B98D File Offset: 0x00009B8D
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0000B995 File Offset: 0x00009B95
		public Setting<string> ExchangeSerialNumber { get; set; }

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000B99E File Offset: 0x00009B9E
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0000B9A6 File Offset: 0x00009BA6
		public Setting<bool> Exchange12 { get; set; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000B9AF File Offset: 0x00009BAF
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000B9B7 File Offset: 0x00009BB7
		public Setting<bool> Exchange200x { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000B9C0 File Offset: 0x00009BC0
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		public Setting<int?> ClrReleaseNumber { get; set; }

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000B9D1 File Offset: 0x00009BD1
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0000B9D9 File Offset: 0x00009BD9
		public Setting<Version> FileVersionMSCorLib { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000B9E2 File Offset: 0x00009BE2
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000B9EA File Offset: 0x00009BEA
		public Setting<Version> FileVersionSystemServiceModel { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000B9F3 File Offset: 0x00009BF3
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0000B9FB File Offset: 0x00009BFB
		public Setting<Version> FileVersionSystemWeb { get; set; }

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000BA04 File Offset: 0x00009C04
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0000BA0C File Offset: 0x00009C0C
		public Setting<Version> FileVersionSystemWebServices { get; set; }

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0000BA15 File Offset: 0x00009C15
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x0000BA1D File Offset: 0x00009C1D
		public Setting<Version> FileVersionNtoskrnl { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000BA26 File Offset: 0x00009C26
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x0000BA2E File Offset: 0x00009C2E
		public Setting<Version> FileVersionSecProc { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000BA37 File Offset: 0x00009C37
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0000BA3F File Offset: 0x00009C3F
		public Setting<Version> FileVersionRmActivate { get; set; }

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000BA48 File Offset: 0x00009C48
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000BA50 File Offset: 0x00009C50
		public Setting<Version> FileVersionRpcRT4 { get; set; }

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000BA59 File Offset: 0x00009C59
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000BA61 File Offset: 0x00009C61
		public Setting<Version> FileVersionRpcHttp { get; set; }

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000BA6A File Offset: 0x00009C6A
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000BA72 File Offset: 0x00009C72
		public Setting<Version> FileVersionRpcProxy { get; set; }

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000BA7B File Offset: 0x00009C7B
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000BA83 File Offset: 0x00009C83
		public Setting<Version> FileVersionLbService { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000BA8C File Offset: 0x00009C8C
		// (set) Token: 0x06000482 RID: 1154 RVA: 0x0000BA94 File Offset: 0x00009C94
		public Setting<Version> FileVersionTCPIPSYS { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000BA9D File Offset: 0x00009C9D
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x0000BAA5 File Offset: 0x00009CA5
		public Setting<Version> FileVersionAdsiis { get; set; }

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000BAAE File Offset: 0x00009CAE
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x0000BAB6 File Offset: 0x00009CB6
		public Setting<Version> FileVersionIisext { get; set; }

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000BABF File Offset: 0x00009CBF
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x0000BAC7 File Offset: 0x00009CC7
		public Setting<Version> FileVersionKernel32 { get; set; }

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000BAD0 File Offset: 0x00009CD0
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x0000BAD8 File Offset: 0x00009CD8
		public Setting<Version> FileVersionUrefs { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000BAE1 File Offset: 0x00009CE1
		// (set) Token: 0x0600048C RID: 1164 RVA: 0x0000BAE9 File Offset: 0x00009CE9
		public Setting<Version> FileVersionRefs { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x0600048D RID: 1165 RVA: 0x0000BAF2 File Offset: 0x00009CF2
		// (set) Token: 0x0600048E RID: 1166 RVA: 0x0000BAFA File Offset: 0x00009CFA
		public Setting<Version> FileVersionDiscan { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600048F RID: 1167 RVA: 0x0000BB03 File Offset: 0x00009D03
		// (set) Token: 0x06000490 RID: 1168 RVA: 0x0000BB0B File Offset: 0x00009D0B
		public Setting<Version> FileVersionAtl110 { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x06000491 RID: 1169 RVA: 0x0000BB14 File Offset: 0x00009D14
		// (set) Token: 0x06000492 RID: 1170 RVA: 0x0000BB1C File Offset: 0x00009D1C
		public Setting<Version> FileVersionMsvcr120 { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x0000BB25 File Offset: 0x00009D25
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0000BB2D File Offset: 0x00009D2D
		public Setting<bool> LocalAdmin { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000495 RID: 1173 RVA: 0x0000BB36 File Offset: 0x00009D36
		// (set) Token: 0x06000496 RID: 1174 RVA: 0x0000BB3E File Offset: 0x00009D3E
		public Setting<string> FirstSGFiles { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x0000BB47 File Offset: 0x00009D47
		// (set) Token: 0x06000498 RID: 1176 RVA: 0x0000BB4F File Offset: 0x00009D4F
		public Setting<string> SecondSGFiles { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000499 RID: 1177 RVA: 0x0000BB58 File Offset: 0x00009D58
		// (set) Token: 0x0600049A RID: 1178 RVA: 0x0000BB60 File Offset: 0x00009D60
		public Setting<float> ExchangeVersionPrefix { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600049B RID: 1179 RVA: 0x0000BB69 File Offset: 0x00009D69
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x0000BB71 File Offset: 0x00009D71
		public Setting<string> VoiceMailPath { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600049D RID: 1181 RVA: 0x0000BB7A File Offset: 0x00009D7A
		// (set) Token: 0x0600049E RID: 1182 RVA: 0x0000BB82 File Offset: 0x00009D82
		public Setting<int> VoiceMessages { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600049F RID: 1183 RVA: 0x0000BB8B File Offset: 0x00009D8B
		// (set) Token: 0x060004A0 RID: 1184 RVA: 0x0000BB93 File Offset: 0x00009D93
		public Setting<long> RemoteRegistryServiceId { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x0000BB9C File Offset: 0x00009D9C
		// (set) Token: 0x060004A2 RID: 1186 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		public Setting<long> OneCopyAlertProcessId { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x0000BBAD File Offset: 0x00009DAD
		// (set) Token: 0x060004A4 RID: 1188 RVA: 0x0000BBB5 File Offset: 0x00009DB5
		public Setting<Process> OpenProcesses { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x0000BBBE File Offset: 0x00009DBE
		// (set) Token: 0x060004A6 RID: 1190 RVA: 0x0000BBC6 File Offset: 0x00009DC6
		public Setting<Process> OpenProcessesOnUpgrade { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x0000BBCF File Offset: 0x00009DCF
		// (set) Token: 0x060004A8 RID: 1192 RVA: 0x0000BBD7 File Offset: 0x00009DD7
		public Setting<Process> OpenProcessesOnUninstall { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x0000BBE8 File Offset: 0x00009DE8
		public Setting<object> SendConnector { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x0000BBF1 File Offset: 0x00009DF1
		// (set) Token: 0x060004AC RID: 1196 RVA: 0x0000BBF9 File Offset: 0x00009DF9
		public Setting<object> GroupDN { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004AD RID: 1197 RVA: 0x0000BC02 File Offset: 0x00009E02
		// (set) Token: 0x060004AE RID: 1198 RVA: 0x0000BC0A File Offset: 0x00009E0A
		public Setting<object> DynamicGroupDN { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004AF RID: 1199 RVA: 0x0000BC13 File Offset: 0x00009E13
		// (set) Token: 0x060004B0 RID: 1200 RVA: 0x0000BC1B File Offset: 0x00009E1B
		public Setting<bool> SchemaAdmin { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0000BC24 File Offset: 0x00009E24
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0000BC2C File Offset: 0x00009E2C
		public Setting<bool> EnterpriseAdmin { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004B3 RID: 1203 RVA: 0x0000BC35 File Offset: 0x00009E35
		// (set) Token: 0x060004B4 RID: 1204 RVA: 0x0000BC3D File Offset: 0x00009E3D
		public Setting<string> ExtendedRightsNtSecurityDescriptor { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0000BC46 File Offset: 0x00009E46
		// (set) Token: 0x060004B6 RID: 1206 RVA: 0x0000BC4E File Offset: 0x00009E4E
		public Setting<bool> HasExtendedRightsCreateChildPerms { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0000BC57 File Offset: 0x00009E57
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0000BC5F File Offset: 0x00009E5F
		public Setting<List<string>> RootDSEProperties { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004B9 RID: 1209 RVA: 0x0000BC68 File Offset: 0x00009E68
		// (set) Token: 0x060004BA RID: 1210 RVA: 0x0000BC70 File Offset: 0x00009E70
		public Setting<string> ConfigurationNamingContext { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0000BC79 File Offset: 0x00009E79
		// (set) Token: 0x060004BC RID: 1212 RVA: 0x0000BC81 File Offset: 0x00009E81
		public Setting<string> RootNamingContext { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x0000BC8A File Offset: 0x00009E8A
		// (set) Token: 0x060004BE RID: 1214 RVA: 0x0000BC92 File Offset: 0x00009E92
		public Setting<string> SchemaNamingContext { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0000BC9B File Offset: 0x00009E9B
		// (set) Token: 0x060004C0 RID: 1216 RVA: 0x0000BCA3 File Offset: 0x00009EA3
		public Setting<string> ObjectSid { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x0000BCAC File Offset: 0x00009EAC
		// (set) Token: 0x060004C2 RID: 1218 RVA: 0x0000BCB4 File Offset: 0x00009EB4
		public Setting<int> NtMixedDomain { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000BCBD File Offset: 0x00009EBD
		// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0000BCC5 File Offset: 0x00009EC5
		public Setting<ResultPropertyCollection> MicrosoftExchServicesConfig { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000BCCE File Offset: 0x00009ECE
		// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000BCD6 File Offset: 0x00009ED6
		public Setting<ResultPropertyCollection> MicrosoftExchServicesAdminGroupConfig { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000BCDF File Offset: 0x00009EDF
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0000BCE7 File Offset: 0x00009EE7
		public Setting<ResultPropertyCollection> OrgMicrosoftExchServicesConfig { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x0000BCF0 File Offset: 0x00009EF0
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000BCF8 File Offset: 0x00009EF8
		public Setting<bool> ExchangeMixedMode { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x0000BD01 File Offset: 0x00009F01
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x0000BD09 File Offset: 0x00009F09
		public Setting<ResultPropertyCollection> MicrosoftExchServicesAdminGroupsConfig { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x0000BD12 File Offset: 0x00009F12
		// (set) Token: 0x060004CE RID: 1230 RVA: 0x0000BD1A File Offset: 0x00009F1A
		public Setting<ResultPropertyCollection> MicrosoftExchAdminGroupsMailboxRoleConfig { get; set; }

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060004CF RID: 1231 RVA: 0x0000BD23 File Offset: 0x00009F23
		// (set) Token: 0x060004D0 RID: 1232 RVA: 0x0000BD2B File Offset: 0x00009F2B
		public Setting<ResultPropertyCollection> MicrosoftExchAdminGroupsBridgeheadRoleConfig { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0000BD34 File Offset: 0x00009F34
		// (set) Token: 0x060004D2 RID: 1234 RVA: 0x0000BD3C File Offset: 0x00009F3C
		public Setting<ResultPropertyCollection> MicrosoftExchAdminGroupsClientAccessRoleConfig { get; set; }

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0000BD45 File Offset: 0x00009F45
		// (set) Token: 0x060004D4 RID: 1236 RVA: 0x0000BD4D File Offset: 0x00009F4D
		public Setting<ResultPropertyCollection> MicrosoftExchAdminGroupsUnifiedMessagingRoleConfig { get; set; }

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060004D5 RID: 1237 RVA: 0x0000BD56 File Offset: 0x00009F56
		// (set) Token: 0x060004D6 RID: 1238 RVA: 0x0000BD5E File Offset: 0x00009F5E
		public Setting<ResultPropertyCollection> MicrosoftExchAdminGroupsCafeRoleConfig { get; set; }

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060004D7 RID: 1239 RVA: 0x0000BD67 File Offset: 0x00009F67
		// (set) Token: 0x060004D8 RID: 1240 RVA: 0x0000BD6F File Offset: 0x00009F6F
		public Setting<ResultPropertyCollection> MicrosoftExchAdminGroupsFrontendTransportRoleConfig { get; set; }

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x0000BD78 File Offset: 0x00009F78
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x0000BD80 File Offset: 0x00009F80
		public Setting<string> MicrosoftExchServicesConfigDistinguishedName { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0000BD89 File Offset: 0x00009F89
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0000BD91 File Offset: 0x00009F91
		public Setting<string> MicrosoftExchServicesAdminGroupConfigDistinguishedName { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000BD9A File Offset: 0x00009F9A
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0000BDA2 File Offset: 0x00009FA2
		public Setting<string> MailboxRoleOfEqualOrHigherVersion { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000BDAB File Offset: 0x00009FAB
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0000BDB3 File Offset: 0x00009FB3
		public Setting<string> BridgeheadRoleOfEqualOrHigherVersion { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000BDBC File Offset: 0x00009FBC
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0000BDC4 File Offset: 0x00009FC4
		public Setting<string> ClientAccessRoleOfEqualOrHigherVersion { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000BDCD File Offset: 0x00009FCD
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0000BDD5 File Offset: 0x00009FD5
		public Setting<string> UnifiedMessagingRoleOfEqualOrHigherVersion { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060004E5 RID: 1253 RVA: 0x0000BDDE File Offset: 0x00009FDE
		// (set) Token: 0x060004E6 RID: 1254 RVA: 0x0000BDE6 File Offset: 0x00009FE6
		public Setting<string> CafeRoleOfEqualOrHigherVersion { get; set; }

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060004E7 RID: 1255 RVA: 0x0000BDEF File Offset: 0x00009FEF
		// (set) Token: 0x060004E8 RID: 1256 RVA: 0x0000BDF7 File Offset: 0x00009FF7
		public Setting<string> FrontendTransportRoleOfEqualOrHigherVersion { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060004E9 RID: 1257 RVA: 0x0000BE00 File Offset: 0x0000A000
		// (set) Token: 0x060004EA RID: 1258 RVA: 0x0000BE08 File Offset: 0x0000A008
		public Setting<string> OrgDistinguishedName { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x0000BE11 File Offset: 0x0000A011
		// (set) Token: 0x060004EC RID: 1260 RVA: 0x0000BE19 File Offset: 0x0000A019
		public Setting<int?> SchemaVersionRangeUpper { get; set; }

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060004ED RID: 1261 RVA: 0x0000BE22 File Offset: 0x0000A022
		// (set) Token: 0x060004EE RID: 1262 RVA: 0x0000BE2A File Offset: 0x0000A02A
		public Setting<string> ExchangeServersGroupOtherWellKnownObjects { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0000BE33 File Offset: 0x0000A033
		// (set) Token: 0x060004F0 RID: 1264 RVA: 0x0000BE3B File Offset: 0x0000A03B
		public Setting<string> ExchangeOrgAdminsGroupOtherWellKnownObjects { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000BE44 File Offset: 0x0000A044
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0000BE4C File Offset: 0x0000A04C
		public Setting<ResultPropertyCollection> OrgDN { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000BE55 File Offset: 0x0000A055
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x0000BE5D File Offset: 0x0000A05D
		public Setting<string> ExOrgAdminAccountName { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000BE66 File Offset: 0x0000A066
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x0000BE6E File Offset: 0x0000A06E
		public Setting<string> SidExOrgAdmins { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000BE77 File Offset: 0x0000A077
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x0000BE7F File Offset: 0x0000A07F
		public Setting<string> ServerManagementGroupOtherWellKnownObjects { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000BE88 File Offset: 0x0000A088
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x0000BE90 File Offset: 0x0000A090
		public Setting<ResultPropertyCollection> ServerManagementGroupDN { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x0000BE99 File Offset: 0x0000A099
		// (set) Token: 0x060004FC RID: 1276 RVA: 0x0000BEA1 File Offset: 0x0000A0A1
		public Setting<string> SidServerManagementGroup { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x0000BEAA File Offset: 0x0000A0AA
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x0000BEB2 File Offset: 0x0000A0B2
		public Setting<ResultPropertyCollection> ExchangeServicesConfigExchangeServersGroup { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x0000BEBB File Offset: 0x0000A0BB
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x0000BEC3 File Offset: 0x0000A0C3
		public Setting<string> ExchangeServersGroupAMAccountName { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x0000BECC File Offset: 0x0000A0CC
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public Setting<bool> ExOrgAdmin { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x0000BEDD File Offset: 0x0000A0DD
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x0000BEE5 File Offset: 0x0000A0E5
		public Setting<bool> ServerManagement { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0000BEEE File Offset: 0x0000A0EE
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x0000BEF6 File Offset: 0x0000A0F6
		public Setting<string> ExchangeServersGroupNtSecurityDescriptor { get; set; }

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x0000BEFF File Offset: 0x0000A0FF
		// (set) Token: 0x06000508 RID: 1288 RVA: 0x0000BF07 File Offset: 0x0000A107
		public Setting<bool> HasExchangeServersUSGWritePerms { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000509 RID: 1289 RVA: 0x0000BF10 File Offset: 0x0000A110
		// (set) Token: 0x0600050A RID: 1290 RVA: 0x0000BF18 File Offset: 0x0000A118
		public Setting<bool> HasExchangeServersUSGBasicAccess { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600050B RID: 1291 RVA: 0x0000BF21 File Offset: 0x0000A121
		// (set) Token: 0x0600050C RID: 1292 RVA: 0x0000BF29 File Offset: 0x0000A129
		public Setting<string> ExchangeSecurityGroupsOrgUnit { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x0600050D RID: 1293 RVA: 0x0000BF32 File Offset: 0x0000A132
		// (set) Token: 0x0600050E RID: 1294 RVA: 0x0000BF3A File Offset: 0x0000A13A
		public Setting<string> AllowedChildClassesEffective { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x0600050F RID: 1295 RVA: 0x0000BF43 File Offset: 0x0000A143
		// (set) Token: 0x06000510 RID: 1296 RVA: 0x0000BF4B File Offset: 0x0000A14B
		public Setting<ResultPropertyCollection> LocalComputerDomainDN { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000511 RID: 1297 RVA: 0x0000BF54 File Offset: 0x0000A154
		// (set) Token: 0x06000512 RID: 1298 RVA: 0x0000BF5C File Offset: 0x0000A15C
		public Setting<string> LocalDomainNtSecurityDescriptor { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000513 RID: 1299 RVA: 0x0000BF65 File Offset: 0x0000A165
		// (set) Token: 0x06000514 RID: 1300 RVA: 0x0000BF6D File Offset: 0x0000A16D
		public Setting<bool> LocalDomainAdmin { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000515 RID: 1301 RVA: 0x0000BF76 File Offset: 0x0000A176
		// (set) Token: 0x06000516 RID: 1302 RVA: 0x0000BF7E File Offset: 0x0000A17E
		public Setting<string> ExchangeServers { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x0000BF87 File Offset: 0x0000A187
		// (set) Token: 0x06000518 RID: 1304 RVA: 0x0000BF8F File Offset: 0x0000A18F
		public Setting<int> ServerSetupRoles { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0000BF98 File Offset: 0x0000A198
		// (set) Token: 0x0600051A RID: 1306 RVA: 0x0000BFA0 File Offset: 0x0000A1A0
		public Setting<string> PrereqServerLegacyDN { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0000BFA9 File Offset: 0x0000A1A9
		// (set) Token: 0x0600051C RID: 1308 RVA: 0x0000BFB1 File Offset: 0x0000A1B1
		public Setting<string> PrereqServerDN { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0000BFBA File Offset: 0x0000A1BA
		// (set) Token: 0x0600051E RID: 1310 RVA: 0x0000BFC2 File Offset: 0x0000A1C2
		public Setting<string> NtSecurityDescriptor { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x0000BFCB File Offset: 0x0000A1CB
		// (set) Token: 0x06000520 RID: 1312 RVA: 0x0000BFD3 File Offset: 0x0000A1D3
		public Setting<bool> ServerAlreadyExists { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0000BFDC File Offset: 0x0000A1DC
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x0000BFE4 File Offset: 0x0000A1E4
		public Setting<int> ExchangeCurrentServerRoles { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0000BFED File Offset: 0x0000A1ED
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x0000BFF5 File Offset: 0x0000A1F5
		public Setting<bool> MailboxRoleInstalled { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000BFFE File Offset: 0x0000A1FE
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x0000C006 File Offset: 0x0000A206
		public Setting<bool> ClientAccessRoleInstalled { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0000C00F File Offset: 0x0000A20F
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x0000C017 File Offset: 0x0000A217
		public Setting<bool> CafeRoleInstalled { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0000C020 File Offset: 0x0000A220
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x0000C028 File Offset: 0x0000A228
		public Setting<bool> FrontendTransportRoleInstalled { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0000C031 File Offset: 0x0000A231
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0000C039 File Offset: 0x0000A239
		public Setting<bool> UnifiedMessagingRoleInstalled { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000C042 File Offset: 0x0000A242
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0000C04A File Offset: 0x0000A24A
		public Setting<bool> BridgeheadRoleInstalled { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000C053 File Offset: 0x0000A253
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0000C05B File Offset: 0x0000A25B
		public Setting<bool> GatewayRoleInstalled { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0000C064 File Offset: 0x0000A264
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0000C06C File Offset: 0x0000A26C
		public Setting<bool> ServerIsProvisioned { get; set; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0000C075 File Offset: 0x0000A275
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0000C07D File Offset: 0x0000A27D
		public Setting<string> ComputerNameNetBIOS { get; set; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000C086 File Offset: 0x0000A286
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0000C08E File Offset: 0x0000A28E
		public Setting<string> ComputerNameDnsHostname { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0000C097 File Offset: 0x0000A297
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0000C09F File Offset: 0x0000A29F
		public Setting<string> ComputerNameDnsDomain { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0000C0B0 File Offset: 0x0000A2B0
		public Setting<string> ComputerNameDnsFullyQualified { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000C0B9 File Offset: 0x0000A2B9
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0000C0C1 File Offset: 0x0000A2C1
		public Setting<bool> E12SP1orHigherHubAlreadyExist { get; set; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0000C0CA File Offset: 0x0000A2CA
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0000C0D2 File Offset: 0x0000A2D2
		public Setting<bool> E12SP1orHigherUMAlreadyExists { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000C0DB File Offset: 0x0000A2DB
		// (set) Token: 0x06000540 RID: 1344 RVA: 0x0000C0E3 File Offset: 0x0000A2E3
		public Setting<bool> E12SP1orHigherCASAlreadyExists { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000C0EC File Offset: 0x0000A2EC
		// (set) Token: 0x06000542 RID: 1346 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		public Setting<bool> E12SP1orHigherMBXAlreadyExists { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000C0FD File Offset: 0x0000A2FD
		// (set) Token: 0x06000544 RID: 1348 RVA: 0x0000C105 File Offset: 0x0000A305
		public Setting<uint> HasServerDelegatedPermsBlocked { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000C10E File Offset: 0x0000A30E
		// (set) Token: 0x06000546 RID: 1350 RVA: 0x0000C116 File Offset: 0x0000A316
		public Setting<string> ConnectorToStar { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0000C11F File Offset: 0x0000A31F
		// (set) Token: 0x06000548 RID: 1352 RVA: 0x0000C127 File Offset: 0x0000A327
		public Setting<string> ExchangeConfigurationUnitsConfiguration { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000C130 File Offset: 0x0000A330
		// (set) Token: 0x0600054A RID: 1354 RVA: 0x0000C138 File Offset: 0x0000A338
		public Setting<string> ExchangeConfigurationUnitsDomain { get; set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0000C141 File Offset: 0x0000A341
		// (set) Token: 0x0600054C RID: 1356 RVA: 0x0000C149 File Offset: 0x0000A349
		public Setting<string> E15ServerInTopology { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0000C152 File Offset: 0x0000A352
		// (set) Token: 0x0600054E RID: 1358 RVA: 0x0000C15A File Offset: 0x0000A35A
		public Setting<string> E14ServerInTopology { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000C163 File Offset: 0x0000A363
		// (set) Token: 0x06000550 RID: 1360 RVA: 0x0000C16B File Offset: 0x0000A36B
		public Setting<string> E12ServerInTopology { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000551 RID: 1361 RVA: 0x0000C174 File Offset: 0x0000A374
		// (set) Token: 0x06000552 RID: 1362 RVA: 0x0000C17C File Offset: 0x0000A37C
		public Setting<ResultPropertyCollection> SchemaDN { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0000C185 File Offset: 0x0000A385
		// (set) Token: 0x06000554 RID: 1364 RVA: 0x0000C18D File Offset: 0x0000A38D
		public Setting<string> SmoRoleOwner { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000C196 File Offset: 0x0000A396
		// (set) Token: 0x06000556 RID: 1366 RVA: 0x0000C19E File Offset: 0x0000A39E
		public Setting<ResultPropertyCollection> SmoSchemaDN { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000C1A7 File Offset: 0x0000A3A7
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x0000C1AF File Offset: 0x0000A3AF
		public Setting<string> DnsHostName { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0000C1C0 File Offset: 0x0000A3C0
		public Setting<string> ServerReference { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0000C1C9 File Offset: 0x0000A3C9
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0000C1D1 File Offset: 0x0000A3D1
		public Setting<string> SmoSchemaDomain { get; set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000C1DA File Offset: 0x0000A3DA
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0000C1E2 File Offset: 0x0000A3E2
		public Setting<ResultPropertyCollection> SmoRoleSchemaRef { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000C1EB File Offset: 0x0000A3EB
		// (set) Token: 0x06000560 RID: 1376 RVA: 0x0000C1F3 File Offset: 0x0000A3F3
		public Setting<string> SmoOperatingSystemVersion { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000C1FC File Offset: 0x0000A3FC
		// (set) Token: 0x06000562 RID: 1378 RVA: 0x0000C204 File Offset: 0x0000A404
		public Setting<string> SmoOperatingSystemServicePack { get; set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000C20D File Offset: 0x0000A40D
		// (set) Token: 0x06000564 RID: 1380 RVA: 0x0000C215 File Offset: 0x0000A415
		public Setting<bool> Win2003FSMOSchemaServer { get; set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0000C21E File Offset: 0x0000A41E
		// (set) Token: 0x06000566 RID: 1382 RVA: 0x0000C226 File Offset: 0x0000A426
		public Setting<bool> SmoSchemaServicePack { get; set; }

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000C22F File Offset: 0x0000A42F
		// (set) Token: 0x06000568 RID: 1384 RVA: 0x0000C237 File Offset: 0x0000A437
		public Setting<string> SMOSchemaSiteName { get; set; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000569 RID: 1385 RVA: 0x0000C240 File Offset: 0x0000A440
		// (set) Token: 0x0600056A RID: 1386 RVA: 0x0000C248 File Offset: 0x0000A448
		public Setting<string> OabDN { get; set; }

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000C251 File Offset: 0x0000A451
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x0000C259 File Offset: 0x0000A459
		public Setting<string> OtherPotentialOABServers { get; set; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600056D RID: 1389 RVA: 0x0000C262 File Offset: 0x0000A462
		// (set) Token: 0x0600056E RID: 1390 RVA: 0x0000C26A File Offset: 0x0000A46A
		public Setting<string> OtherPotentialExpansionServers { get; set; }

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000C273 File Offset: 0x0000A473
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x0000C27B File Offset: 0x0000A47B
		public Setting<string> DomainControllerSiteName { get; set; }

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000C284 File Offset: 0x0000A484
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x0000C28C File Offset: 0x0000A48C
		public Setting<string> DomainControllerRef { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000C295 File Offset: 0x0000A495
		// (set) Token: 0x06000574 RID: 1396 RVA: 0x0000C29D File Offset: 0x0000A49D
		public Setting<string> DomainControllerOS { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000C2A6 File Offset: 0x0000A4A6
		// (set) Token: 0x06000576 RID: 1398 RVA: 0x0000C2AE File Offset: 0x0000A4AE
		public Setting<ResultPropertyCollection> MicrosoftExchServicesConfigBridgeheadRoleInTopology { get; set; }

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x0000C2B7 File Offset: 0x0000A4B7
		// (set) Token: 0x06000578 RID: 1400 RVA: 0x0000C2BF File Offset: 0x0000A4BF
		public Setting<ResultPropertyCollection> MicrosoftExchServicesConfigCafeRoleInTopology { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000C2C8 File Offset: 0x0000A4C8
		// (set) Token: 0x0600057A RID: 1402 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		public Setting<ResultPropertyCollection> MicrosoftExchServicesConfigUMRoleInTopology { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000C2D9 File Offset: 0x0000A4D9
		// (set) Token: 0x0600057C RID: 1404 RVA: 0x0000C2E1 File Offset: 0x0000A4E1
		public Setting<ResultPropertyCollection> MicrosoftExchServicesConfigCASRoleInTopology { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0000C2EA File Offset: 0x0000A4EA
		// (set) Token: 0x0600057E RID: 1406 RVA: 0x0000C2F2 File Offset: 0x0000A4F2
		public Setting<ResultPropertyCollection> MicrosoftExchServicesConfigMBXRoleInTopology { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0000C2FB File Offset: 0x0000A4FB
		// (set) Token: 0x06000580 RID: 1408 RVA: 0x0000C303 File Offset: 0x0000A503
		public Setting<string> BridgeheadRoleInTopology { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000581 RID: 1409 RVA: 0x0000C30C File Offset: 0x0000A50C
		// (set) Token: 0x06000582 RID: 1410 RVA: 0x0000C314 File Offset: 0x0000A514
		public Setting<bool> E12SP1orHigherHubAlreadyExists { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0000C31D File Offset: 0x0000A51D
		// (set) Token: 0x06000584 RID: 1412 RVA: 0x0000C325 File Offset: 0x0000A525
		public Setting<bool> E15orHigherHubAlreadyExists { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0000C32E File Offset: 0x0000A52E
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x0000C336 File Offset: 0x0000A536
		public Setting<string> CafeRoleInTopology { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0000C33F File Offset: 0x0000A53F
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0000C347 File Offset: 0x0000A547
		public Setting<string> UnifiedMessagingRoleInTopology { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000C350 File Offset: 0x0000A550
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0000C358 File Offset: 0x0000A558
		public Setting<string> ClientAccessRoleInTopology { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0000C361 File Offset: 0x0000A561
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x0000C369 File Offset: 0x0000A569
		public Setting<string> MailboxRoleInTopology { get; set; }

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0000C372 File Offset: 0x0000A572
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0000C37A File Offset: 0x0000A57A
		public Setting<string> AdcServer { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0000C383 File Offset: 0x0000A583
		// (set) Token: 0x06000590 RID: 1424 RVA: 0x0000C38B File Offset: 0x0000A58B
		public Setting<string> ShortProvisionedName { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0000C394 File Offset: 0x0000A594
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x0000C39C File Offset: 0x0000A59C
		public Setting<int> MsDSBehaviorVersion { get; set; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000C3A5 File Offset: 0x0000A5A5
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x0000C3AD File Offset: 0x0000A5AD
		public Setting<ResultPropertyCollection> ExchangeRecipientPolicyConfiguration { get; set; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0000C3B6 File Offset: 0x0000A5B6
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x0000C3BE File Offset: 0x0000A5BE
		public Setting<string> RecipientPolicyName { get; set; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000C3C7 File Offset: 0x0000A5C7
		// (set) Token: 0x06000598 RID: 1432 RVA: 0x0000C3CF File Offset: 0x0000A5CF
		public Setting<string> ExchNonAuthoritativeDomains { get; set; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0000C3D8 File Offset: 0x0000A5D8
		// (set) Token: 0x0600059A RID: 1434 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		public Setting<string> DisabledGatewayProxy { get; set; }

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000C3E9 File Offset: 0x0000A5E9
		// (set) Token: 0x0600059C RID: 1436 RVA: 0x0000C3F1 File Offset: 0x0000A5F1
		public Setting<string> EnabledSMTPDomain { get; set; }

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000C3FA File Offset: 0x0000A5FA
		// (set) Token: 0x0600059E RID: 1438 RVA: 0x0000C402 File Offset: 0x0000A602
		public Setting<string> GatewayProxy { get; set; }

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600059F RID: 1439 RVA: 0x0000C40B File Offset: 0x0000A60B
		// (set) Token: 0x060005A0 RID: 1440 RVA: 0x0000C413 File Offset: 0x0000A613
		public Setting<string> MicrosoftExchServicesConfigAdminGroupDistinguishedName { get; set; }

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000C41C File Offset: 0x0000A61C
		// (set) Token: 0x060005A2 RID: 1442 RVA: 0x0000C424 File Offset: 0x0000A624
		public Setting<ResultPropertyCollection> MicrosoftExchServicesConfigAdminGroupPublicFoldersConfig { get; set; }

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000C42D File Offset: 0x0000A62D
		// (set) Token: 0x060005A4 RID: 1444 RVA: 0x0000C435 File Offset: 0x0000A635
		public Setting<string> MicrosoftExchServicesConfigAdminGroupPublicFoldersDistinguishedName { get; set; }

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000C43E File Offset: 0x0000A63E
		// (set) Token: 0x060005A6 RID: 1446 RVA: 0x0000C446 File Offset: 0x0000A646
		public Setting<byte[]> MicrosoftExchServicesConfigAdminGroupPublicFoldersNtSecurityDescriptor { get; set; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000C44F File Offset: 0x0000A64F
		// (set) Token: 0x060005A8 RID: 1448 RVA: 0x0000C457 File Offset: 0x0000A657
		public Setting<bool> PrepareDomainAdmin { get; set; }

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0000C460 File Offset: 0x0000A660
		// (set) Token: 0x060005AA RID: 1450 RVA: 0x0000C468 File Offset: 0x0000A668
		public Setting<string> PrepareDomainNCName { get; set; }

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000C471 File Offset: 0x0000A671
		// (set) Token: 0x060005AC RID: 1452 RVA: 0x0000C479 File Offset: 0x0000A679
		public Setting<ResultPropertyCollection> PrepareDomainNCNameConfig { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000C482 File Offset: 0x0000A682
		// (set) Token: 0x060005AE RID: 1454 RVA: 0x0000C48A File Offset: 0x0000A68A
		public Setting<string> SidPrepareDomain { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000C493 File Offset: 0x0000A693
		// (set) Token: 0x060005B0 RID: 1456 RVA: 0x0000C49B File Offset: 0x0000A69B
		public Setting<int> NtMixedDomainPrepareDomain { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		// (set) Token: 0x060005B2 RID: 1458 RVA: 0x0000C4AC File Offset: 0x0000A6AC
		public Setting<string> DomainNCName { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060005B3 RID: 1459 RVA: 0x0000C4B5 File Offset: 0x0000A6B5
		// (set) Token: 0x060005B4 RID: 1460 RVA: 0x0000C4BD File Offset: 0x0000A6BD
		public Setting<string> MicrosoftExchangeSystemObjectsCN { get; set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000C4C6 File Offset: 0x0000A6C6
		// (set) Token: 0x060005B6 RID: 1462 RVA: 0x0000C4CE File Offset: 0x0000A6CE
		public Setting<string> RusName { get; set; }

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000C4D7 File Offset: 0x0000A6D7
		// (set) Token: 0x060005B8 RID: 1464 RVA: 0x0000C4DF File Offset: 0x0000A6DF
		public Setting<List<ResultPropertyCollection>> ExchangePrivateMDB { get; set; }

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		// (set) Token: 0x060005BA RID: 1466 RVA: 0x0000C4F0 File Offset: 0x0000A6F0
		public Setting<List<string>> PrivateDatabaseName { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000C4F9 File Offset: 0x0000A6F9
		// (set) Token: 0x060005BC RID: 1468 RVA: 0x0000C501 File Offset: 0x0000A701
		public Setting<List<string>> PrivateDatabaseNameDN { get; set; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060005BD RID: 1469 RVA: 0x0000C50A File Offset: 0x0000A70A
		// (set) Token: 0x060005BE RID: 1470 RVA: 0x0000C512 File Offset: 0x0000A712
		public Setting<List<string>> PrivateDatabaseEdbDrive { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000C51B File Offset: 0x0000A71B
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x0000C523 File Offset: 0x0000A723
		public Setting<List<string>> PrivateDatabaseLogDrive { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000C52C File Offset: 0x0000A72C
		// (set) Token: 0x060005C2 RID: 1474 RVA: 0x0000C534 File Offset: 0x0000A734
		public Setting<List<string>> MailboxEDBDriveNotExistList { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000C53D File Offset: 0x0000A73D
		// (set) Token: 0x060005C4 RID: 1476 RVA: 0x0000C545 File Offset: 0x0000A745
		public Setting<List<string>> MailboxLogDriveNotExistList { get; set; }

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000C54E File Offset: 0x0000A74E
		// (set) Token: 0x060005C6 RID: 1478 RVA: 0x0000C556 File Offset: 0x0000A756
		public Setting<List<string>> RemoveMailboxDatabaseException { get; set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060005C7 RID: 1479 RVA: 0x0000C55F File Offset: 0x0000A75F
		// (set) Token: 0x060005C8 RID: 1480 RVA: 0x0000C567 File Offset: 0x0000A767
		public Setting<bool> IgnoreFileInUse { get; set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x0000C570 File Offset: 0x0000A770
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x0000C578 File Offset: 0x0000A778
		public Setting<ResultPropertyCollection> RootDN { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x0000C581 File Offset: 0x0000A781
		// (set) Token: 0x060005CC RID: 1484 RVA: 0x0000C589 File Offset: 0x0000A789
		public Setting<string> SidRootDomain { get; set; }

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000C592 File Offset: 0x0000A792
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x0000C59A File Offset: 0x0000A79A
		public Setting<int> NtMixedDomainRoot { get; set; }

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000C5A3 File Offset: 0x0000A7A3
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x0000C5AB File Offset: 0x0000A7AB
		public Setting<string> RemoveServerName { get; set; }

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x0000C5BC File Offset: 0x0000A7BC
		public Setting<int> ExchCurrentServerRoles { get; set; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000C5C5 File Offset: 0x0000A7C5
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x0000C5CD File Offset: 0x0000A7CD
		public Setting<string[]> NonAuthoritativeDomainsArray { get; set; }

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0000C5D6 File Offset: 0x0000A7D6
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0000C5DE File Offset: 0x0000A7DE
		public Setting<string[]> DisabledGatewayProxyArray { get; set; }

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x0000C5E7 File Offset: 0x0000A7E7
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0000C5EF File Offset: 0x0000A7EF
		public Setting<string[]> EnabledGatewayProxyArray { get; set; }

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0000C600 File Offset: 0x0000A800
		public Setting<ResultPropertyCollection> ExchOab { get; set; }

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0000C609 File Offset: 0x0000A809
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x0000C611 File Offset: 0x0000A811
		public Setting<string> OabName { get; set; }

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x0000C61A File Offset: 0x0000A81A
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0000C622 File Offset: 0x0000A822
		public Setting<string> OffLineABServer { get; set; }

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x0000C62B File Offset: 0x0000A82B
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0000C633 File Offset: 0x0000A833
		public Setting<string> TargetDir { get; set; }

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x0000C63C File Offset: 0x0000A83C
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x0000C644 File Offset: 0x0000A844
		public Setting<Version> ExchangeVersion { get; set; }

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x0000C64D File Offset: 0x0000A84D
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x0000C655 File Offset: 0x0000A855
		public Setting<int> AdamPort { get; set; }

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000C65E File Offset: 0x0000A85E
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x0000C666 File Offset: 0x0000A866
		public Setting<int> AdamSSLPort { get; set; }

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000C66F File Offset: 0x0000A86F
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x0000C677 File Offset: 0x0000A877
		public Setting<bool> CreatePublicDB { get; set; }

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000C680 File Offset: 0x0000A880
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x0000C688 File Offset: 0x0000A888
		public Setting<bool> CustomerFeedbackEnabled { get; set; }

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000C691 File Offset: 0x0000A891
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x0000C699 File Offset: 0x0000A899
		public Setting<string> NewProvisionedServerName { get; set; }

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x0000C6A2 File Offset: 0x0000A8A2
		// (set) Token: 0x060005EE RID: 1518 RVA: 0x0000C6AA File Offset: 0x0000A8AA
		public Setting<string> RemoveProvisionedServerName { get; set; }

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x0000C6B3 File Offset: 0x0000A8B3
		// (set) Token: 0x060005F0 RID: 1520 RVA: 0x0000C6BB File Offset: 0x0000A8BB
		public Setting<string> GlobalCatalog { get; set; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000C6C4 File Offset: 0x0000A8C4
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x0000C6CC File Offset: 0x0000A8CC
		public Setting<string> DomainController { get; set; }

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x0000C6D5 File Offset: 0x0000A8D5
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x0000C6DD File Offset: 0x0000A8DD
		public Setting<string> PrepareDomain { get; set; }

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x0000C6E6 File Offset: 0x0000A8E6
		// (set) Token: 0x060005F6 RID: 1526 RVA: 0x0000C6EE File Offset: 0x0000A8EE
		public Setting<bool> PrepareOrganization { get; set; }

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060005F7 RID: 1527 RVA: 0x0000C6F7 File Offset: 0x0000A8F7
		// (set) Token: 0x060005F8 RID: 1528 RVA: 0x0000C6FF File Offset: 0x0000A8FF
		public Setting<bool> PrepareSchema { get; set; }

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060005F9 RID: 1529 RVA: 0x0000C708 File Offset: 0x0000A908
		// (set) Token: 0x060005FA RID: 1530 RVA: 0x0000C710 File Offset: 0x0000A910
		public Setting<bool> PrepareAllDomains { get; set; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060005FB RID: 1531 RVA: 0x0000C719 File Offset: 0x0000A919
		// (set) Token: 0x060005FC RID: 1532 RVA: 0x0000C721 File Offset: 0x0000A921
		public Setting<string> AdInitError { get; set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060005FD RID: 1533 RVA: 0x0000C72A File Offset: 0x0000A92A
		// (set) Token: 0x060005FE RID: 1534 RVA: 0x0000C732 File Offset: 0x0000A932
		public Setting<string> LanguagePackDir { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060005FF RID: 1535 RVA: 0x0000C73B File Offset: 0x0000A93B
		// (set) Token: 0x06000600 RID: 1536 RVA: 0x0000C743 File Offset: 0x0000A943
		public Setting<bool> LanguagesAvailableToInstall { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000601 RID: 1537 RVA: 0x0000C74C File Offset: 0x0000A94C
		// (set) Token: 0x06000602 RID: 1538 RVA: 0x0000C754 File Offset: 0x0000A954
		public Setting<bool> SufficientLanguagePackDiskSpace { get; set; }

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x0000C75D File Offset: 0x0000A95D
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x0000C765 File Offset: 0x0000A965
		public Setting<bool> LanguagePacksInstalled { get; set; }

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x0000C76E File Offset: 0x0000A96E
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x0000C776 File Offset: 0x0000A976
		public Setting<string> AlreadyInstalledUMLanguages { get; set; }

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x0000C77F File Offset: 0x0000A97F
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x0000C787 File Offset: 0x0000A987
		public Setting<bool> LanguagePackVersioning { get; set; }

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x0000C790 File Offset: 0x0000A990
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x0000C798 File Offset: 0x0000A998
		public Setting<bool> ActiveDirectorySplitPermissions { get; set; }

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000C7A1 File Offset: 0x0000A9A1
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0000C7A9 File Offset: 0x0000A9A9
		public Setting<string> SetupRoles { get; set; }

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000C7B2 File Offset: 0x0000A9B2
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0000C7BA File Offset: 0x0000A9BA
		public Setting<bool> E12SP1orHigher { get; set; }

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000C7C3 File Offset: 0x0000A9C3
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0000C7CB File Offset: 0x0000A9CB
		public Setting<int?> NewestBuild { get; set; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000C7D4 File Offset: 0x0000A9D4
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0000C7DC File Offset: 0x0000A9DC
		public Setting<string> ServicesPath { get; set; }

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000C7E5 File Offset: 0x0000A9E5
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0000C7ED File Offset: 0x0000A9ED
		public Setting<string> MsiInstallPath { get; set; }

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0000C7F6 File Offset: 0x0000A9F6
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0000C7FE File Offset: 0x0000A9FE
		public Setting<string> Roles { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000C807 File Offset: 0x0000AA07
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0000C80F File Offset: 0x0000AA0F
		public Setting<string> ServerRoleUnpacked { get; set; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x0000C818 File Offset: 0x0000AA18
		// (set) Token: 0x0600061A RID: 1562 RVA: 0x0000C820 File Offset: 0x0000AA20
		public Setting<string> Watermarks { get; set; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x0600061B RID: 1563 RVA: 0x0000C829 File Offset: 0x0000AA29
		// (set) Token: 0x0600061C RID: 1564 RVA: 0x0000C831 File Offset: 0x0000AA31
		public Setting<string> FilteredRoles { get; set; }

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x0600061D RID: 1565 RVA: 0x0000C83A File Offset: 0x0000AA3A
		// (set) Token: 0x0600061E RID: 1566 RVA: 0x0000C842 File Offset: 0x0000AA42
		public Setting<string> Actions { get; set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x0000C84B File Offset: 0x0000AA4B
		// (set) Token: 0x06000620 RID: 1568 RVA: 0x0000C853 File Offset: 0x0000AA53
		public Setting<string> UcmaRedistVersion { get; set; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x0000C85C File Offset: 0x0000AA5C
		// (set) Token: 0x06000622 RID: 1570 RVA: 0x0000C864 File Offset: 0x0000AA64
		public Setting<string[]> SpeechRedist { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000623 RID: 1571 RVA: 0x0000C86D File Offset: 0x0000AA6D
		// (set) Token: 0x06000624 RID: 1572 RVA: 0x0000C875 File Offset: 0x0000AA75
		public Setting<string> WindowsSPLevel { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000625 RID: 1573 RVA: 0x0000C87E File Offset: 0x0000AA7E
		// (set) Token: 0x06000626 RID: 1574 RVA: 0x0000C886 File Offset: 0x0000AA86
		public Setting<string> WindowsProductName { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000627 RID: 1575 RVA: 0x0000C88F File Offset: 0x0000AA8F
		// (set) Token: 0x06000628 RID: 1576 RVA: 0x0000C897 File Offset: 0x0000AA97
		public Setting<string> ADAMVersion { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0000C8A0 File Offset: 0x0000AAA0
		// (set) Token: 0x0600062A RID: 1578 RVA: 0x0000C8A8 File Offset: 0x0000AAA8
		public Setting<string> SMTPSvcStartMode { get; set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0000C8B1 File Offset: 0x0000AAB1
		// (set) Token: 0x0600062C RID: 1580 RVA: 0x0000C8B9 File Offset: 0x0000AAB9
		public Setting<string> SMTPSvcDisplayName { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000C8C2 File Offset: 0x0000AAC2
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x0000C8CA File Offset: 0x0000AACA
		public Setting<string[]> IISCommonFiles { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0000C8D3 File Offset: 0x0000AAD3
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x0000C8DB File Offset: 0x0000AADB
		public Setting<int?> IIS6MetabaseStatus { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0000C8E4 File Offset: 0x0000AAE4
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		public Setting<int?> IIS6ManagementConsoleStatus { get; set; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000633 RID: 1587 RVA: 0x0000C8F5 File Offset: 0x0000AAF5
		// (set) Token: 0x06000634 RID: 1588 RVA: 0x0000C8FD File Offset: 0x0000AAFD
		public Setting<int?> IIS7CompressionDynamic { get; set; }

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x0000C906 File Offset: 0x0000AB06
		// (set) Token: 0x06000636 RID: 1590 RVA: 0x0000C90E File Offset: 0x0000AB0E
		public Setting<int?> IIS7CompressionStatic { get; set; }

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0000C917 File Offset: 0x0000AB17
		// (set) Token: 0x06000638 RID: 1592 RVA: 0x0000C91F File Offset: 0x0000AB1F
		public Setting<int?> IIS7ManagedCodeAssemblies { get; set; }

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0000C928 File Offset: 0x0000AB28
		// (set) Token: 0x0600063A RID: 1594 RVA: 0x0000C930 File Offset: 0x0000AB30
		public Setting<int?> WASProcessModel { get; set; }

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600063B RID: 1595 RVA: 0x0000C939 File Offset: 0x0000AB39
		// (set) Token: 0x0600063C RID: 1596 RVA: 0x0000C941 File Offset: 0x0000AB41
		public Setting<int?> IIS7BasicAuthentication { get; set; }

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600063D RID: 1597 RVA: 0x0000C94A File Offset: 0x0000AB4A
		// (set) Token: 0x0600063E RID: 1598 RVA: 0x0000C952 File Offset: 0x0000AB52
		public Setting<int?> IIS7WindowAuthentication { get; set; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600063F RID: 1599 RVA: 0x0000C95B File Offset: 0x0000AB5B
		// (set) Token: 0x06000640 RID: 1600 RVA: 0x0000C963 File Offset: 0x0000AB63
		public Setting<int?> IIS7DigestAuthentication { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000641 RID: 1601 RVA: 0x0000C96C File Offset: 0x0000AB6C
		// (set) Token: 0x06000642 RID: 1602 RVA: 0x0000C974 File Offset: 0x0000AB74
		public Setting<int?> IIS7NetExt { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000643 RID: 1603 RVA: 0x0000C97D File Offset: 0x0000AB7D
		// (set) Token: 0x06000644 RID: 1604 RVA: 0x0000C985 File Offset: 0x0000AB85
		public Setting<int?> IIS6WMICompatibility { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000645 RID: 1605 RVA: 0x0000C98E File Offset: 0x0000AB8E
		// (set) Token: 0x06000646 RID: 1606 RVA: 0x0000C996 File Offset: 0x0000AB96
		public Setting<int?> ASPNET { get; set; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000647 RID: 1607 RVA: 0x0000C99F File Offset: 0x0000AB9F
		// (set) Token: 0x06000648 RID: 1608 RVA: 0x0000C9A7 File Offset: 0x0000ABA7
		public Setting<int?> ISAPIFilter { get; set; }

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000649 RID: 1609 RVA: 0x0000C9B0 File Offset: 0x0000ABB0
		// (set) Token: 0x0600064A RID: 1610 RVA: 0x0000C9B8 File Offset: 0x0000ABB8
		public Setting<int?> ClientCertificateMappingAuthentication { get; set; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x0600064B RID: 1611 RVA: 0x0000C9C1 File Offset: 0x0000ABC1
		// (set) Token: 0x0600064C RID: 1612 RVA: 0x0000C9C9 File Offset: 0x0000ABC9
		public Setting<int?> DirectoryBrowse { get; set; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0000C9D2 File Offset: 0x0000ABD2
		// (set) Token: 0x0600064E RID: 1614 RVA: 0x0000C9DA File Offset: 0x0000ABDA
		public Setting<int?> HttpErrors { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600064F RID: 1615 RVA: 0x0000C9E3 File Offset: 0x0000ABE3
		// (set) Token: 0x06000650 RID: 1616 RVA: 0x0000C9EB File Offset: 0x0000ABEB
		public Setting<int?> HttpLogging { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000651 RID: 1617 RVA: 0x0000C9F4 File Offset: 0x0000ABF4
		// (set) Token: 0x06000652 RID: 1618 RVA: 0x0000C9FC File Offset: 0x0000ABFC
		public Setting<int?> HttpRedirect { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x0000CA05 File Offset: 0x0000AC05
		// (set) Token: 0x06000654 RID: 1620 RVA: 0x0000CA0D File Offset: 0x0000AC0D
		public Setting<int?> HttpTracing { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000655 RID: 1621 RVA: 0x0000CA16 File Offset: 0x0000AC16
		// (set) Token: 0x06000656 RID: 1622 RVA: 0x0000CA1E File Offset: 0x0000AC1E
		public Setting<int?> RequestMonitor { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000657 RID: 1623 RVA: 0x0000CA27 File Offset: 0x0000AC27
		// (set) Token: 0x06000658 RID: 1624 RVA: 0x0000CA2F File Offset: 0x0000AC2F
		public Setting<int?> StaticContent { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000659 RID: 1625 RVA: 0x0000CA38 File Offset: 0x0000AC38
		// (set) Token: 0x0600065A RID: 1626 RVA: 0x0000CA40 File Offset: 0x0000AC40
		public Setting<int?> ManagementService { get; set; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600065B RID: 1627 RVA: 0x0000CA49 File Offset: 0x0000AC49
		// (set) Token: 0x0600065C RID: 1628 RVA: 0x0000CA51 File Offset: 0x0000AC51
		public Setting<int> W3SVCStartMode { get; set; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x0600065D RID: 1629 RVA: 0x0000CA5A File Offset: 0x0000AC5A
		// (set) Token: 0x0600065E RID: 1630 RVA: 0x0000CA62 File Offset: 0x0000AC62
		public Setting<int> ClusSvcStartMode { get; set; }

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x0600065F RID: 1631 RVA: 0x0000CA6B File Offset: 0x0000AC6B
		// (set) Token: 0x06000660 RID: 1632 RVA: 0x0000CA73 File Offset: 0x0000AC73
		public Setting<string[]> Wif35Installed { get; set; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000661 RID: 1633 RVA: 0x0000CA7C File Offset: 0x0000AC7C
		// (set) Token: 0x06000662 RID: 1634 RVA: 0x0000CA84 File Offset: 0x0000AC84
		public Setting<string> MailboxConfiguredVersion { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000663 RID: 1635 RVA: 0x0000CA8D File Offset: 0x0000AC8D
		// (set) Token: 0x06000664 RID: 1636 RVA: 0x0000CA95 File Offset: 0x0000AC95
		public Setting<string> MailboxUnpackedVersion { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000665 RID: 1637 RVA: 0x0000CA9E File Offset: 0x0000AC9E
		// (set) Token: 0x06000666 RID: 1638 RVA: 0x0000CAA6 File Offset: 0x0000ACA6
		public Setting<bool> MailboxPreviousBuild { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x0000CAAF File Offset: 0x0000ACAF
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x0000CAB7 File Offset: 0x0000ACB7
		public Setting<string> UnifiedMessagingConfiguredVersion { get; set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x0000CAC0 File Offset: 0x0000ACC0
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x0000CAC8 File Offset: 0x0000ACC8
		public Setting<string> UnifiedMessagingUnpackedVersion { get; set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x0000CAD1 File Offset: 0x0000ACD1
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x0000CAD9 File Offset: 0x0000ACD9
		public Setting<bool> UnifiedMessagingPreviousBuild { get; set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x0000CAE2 File Offset: 0x0000ACE2
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x0000CAEA File Offset: 0x0000ACEA
		public Setting<string> ClientAccessConfiguredVersion { get; set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x0000CAF3 File Offset: 0x0000ACF3
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x0000CAFB File Offset: 0x0000ACFB
		public Setting<string> BridgeheadConfiguredVersion { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0000CB04 File Offset: 0x0000AD04
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x0000CB0C File Offset: 0x0000AD0C
		public Setting<string> BridgeheadUnpackedVersion { get; set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0000CB15 File Offset: 0x0000AD15
		// (set) Token: 0x06000674 RID: 1652 RVA: 0x0000CB1D File Offset: 0x0000AD1D
		public Setting<bool> BridgeheadPreviousBuild { get; set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000675 RID: 1653 RVA: 0x0000CB26 File Offset: 0x0000AD26
		// (set) Token: 0x06000676 RID: 1654 RVA: 0x0000CB2E File Offset: 0x0000AD2E
		public Setting<bool> PreviousBuildDetected { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000677 RID: 1655 RVA: 0x0000CB37 File Offset: 0x0000AD37
		// (set) Token: 0x06000678 RID: 1656 RVA: 0x0000CB3F File Offset: 0x0000AD3F
		public Setting<string> GatewayConfiguredVersion { get; set; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000679 RID: 1657 RVA: 0x0000CB48 File Offset: 0x0000AD48
		// (set) Token: 0x0600067A RID: 1658 RVA: 0x0000CB50 File Offset: 0x0000AD50
		public Setting<string> GatewayUnpackedVersion { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0000CB59 File Offset: 0x0000AD59
		// (set) Token: 0x0600067C RID: 1660 RVA: 0x0000CB61 File Offset: 0x0000AD61
		public Setting<string[]> AdminToolsInstallation { get; set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0000CB6A File Offset: 0x0000AD6A
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x0000CB72 File Offset: 0x0000AD72
		public Setting<string[]> PendingFileRenames { get; set; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x0600067F RID: 1663 RVA: 0x0000CB7B File Offset: 0x0000AD7B
		// (set) Token: 0x06000680 RID: 1664 RVA: 0x0000CB83 File Offset: 0x0000AD83
		public Setting<bool> DST2007Enabled { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0000CB8C File Offset: 0x0000AD8C
		// (set) Token: 0x06000682 RID: 1666 RVA: 0x0000CB94 File Offset: 0x0000AD94
		public Setting<string[]> DynamicDSTKey { get; set; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x0000CB9D File Offset: 0x0000AD9D
		// (set) Token: 0x06000684 RID: 1668 RVA: 0x0000CBA5 File Offset: 0x0000ADA5
		public Setting<string[]> HTTPActivation { get; set; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0000CBAE File Offset: 0x0000ADAE
		// (set) Token: 0x06000686 RID: 1670 RVA: 0x0000CBB6 File Offset: 0x0000ADB6
		public Setting<bool> IsWcfHttpActivation45Installed { get; set; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x0000CBBF File Offset: 0x0000ADBF
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x0000CBC7 File Offset: 0x0000ADC7
		public Setting<bool> IsRsatAddsToolsInstalled { get; set; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x0000CBD0 File Offset: 0x0000ADD0
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x0000CBD8 File Offset: 0x0000ADD8
		public Setting<bool> IsRsatClusteringInstalled { get; set; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x0000CBE1 File Offset: 0x0000ADE1
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0000CBE9 File Offset: 0x0000ADE9
		public Setting<bool> IsRsatClusteringMgmtInstalled { get; set; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x0000CBF2 File Offset: 0x0000ADF2
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x0000CBFA File Offset: 0x0000ADFA
		public Setting<bool> IsRsatClusteringPowerShellInstalled { get; set; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0000CC03 File Offset: 0x0000AE03
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x0000CC0B File Offset: 0x0000AE0B
		public Setting<bool> IsRsatClusteringCmdInterfaceInstalled { get; set; }

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0000CC14 File Offset: 0x0000AE14
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0000CC1C File Offset: 0x0000AE1C
		public Setting<string> NNTPSvcStartMode { get; set; }

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x0000CC25 File Offset: 0x0000AE25
		// (set) Token: 0x06000694 RID: 1684 RVA: 0x0000CC2D File Offset: 0x0000AE2D
		public Setting<string> ProgramFilePath { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000CC36 File Offset: 0x0000AE36
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0000CC3E File Offset: 0x0000AE3E
		public Setting<string> FrameworkPath { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0000CC47 File Offset: 0x0000AE47
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x0000CC4F File Offset: 0x0000AE4F
		public Setting<string> AdamDataPath { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0000CC58 File Offset: 0x0000AE58
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x0000CC60 File Offset: 0x0000AE60
		public Setting<string> ConfigDCHostName { get; set; }

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0000CC69 File Offset: 0x0000AE69
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0000CC71 File Offset: 0x0000AE71
		public Setting<object> CmdletGetQueueResult { get; set; }

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0000CC7A File Offset: 0x0000AE7A
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x0000CC82 File Offset: 0x0000AE82
		public Setting<UMServer> CmdletGetUMServerResult { get; set; }

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x0000CC8B File Offset: 0x0000AE8B
		// (set) Token: 0x060006A0 RID: 1696 RVA: 0x0000CC93 File Offset: 0x0000AE93
		public Setting<string> LanguageInUMServer { get; set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x0000CC9C File Offset: 0x0000AE9C
		// (set) Token: 0x060006A2 RID: 1698 RVA: 0x0000CCA4 File Offset: 0x0000AEA4
		public Setting<string> SiteCanonicalName { get; set; }

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x0000CCAD File Offset: 0x0000AEAD
		// (set) Token: 0x060006A4 RID: 1700 RVA: 0x0000CCB5 File Offset: 0x0000AEB5
		public Setting<object> EdgeSubscriptionForSite { get; set; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0000CCBE File Offset: 0x0000AEBE
		// (set) Token: 0x060006A6 RID: 1702 RVA: 0x0000CCC6 File Offset: 0x0000AEC6
		public Setting<string> HubTransportRoleInCurrentADSite { get; set; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x060006A7 RID: 1703 RVA: 0x0000CCCF File Offset: 0x0000AECF
		// (set) Token: 0x060006A8 RID: 1704 RVA: 0x0000CCD7 File Offset: 0x0000AED7
		public Setting<bool> HttpLocationAccessible { get; set; }

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x060006A9 RID: 1705 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		// (set) Token: 0x060006AA RID: 1706 RVA: 0x0000CCE8 File Offset: 0x0000AEE8
		public Setting<bool> IsHybridObjectFoundOnPremises { get; set; }

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x060006AB RID: 1707 RVA: 0x0000CCF1 File Offset: 0x0000AEF1
		// (set) Token: 0x060006AC RID: 1708 RVA: 0x0000CCF9 File Offset: 0x0000AEF9
		public Setting<bool> WasSetupStartedFromGUI { get; set; }

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x060006AD RID: 1709 RVA: 0x0000CD02 File Offset: 0x0000AF02
		// (set) Token: 0x060006AE RID: 1710 RVA: 0x0000CD0A File Offset: 0x0000AF0A
		public Setting<bool> IsExchangeVersionCorrectForTenant { get; set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x060006AF RID: 1711 RVA: 0x0000CD13 File Offset: 0x0000AF13
		// (set) Token: 0x060006B0 RID: 1712 RVA: 0x0000CD1B File Offset: 0x0000AF1B
		public Setting<int?> IIS7ManagementConsole { get; set; }

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x060006B1 RID: 1713 RVA: 0x0000CD24 File Offset: 0x0000AF24
		// (set) Token: 0x060006B2 RID: 1714 RVA: 0x0000CD2C File Offset: 0x0000AF2C
		public Setting<int> WindowsInstallerServiceStartMode { get; set; }

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x0000CD35 File Offset: 0x0000AF35
		// (set) Token: 0x060006B4 RID: 1716 RVA: 0x0000CD3D File Offset: 0x0000AF3D
		public Setting<int> WinRMServiceStartMode { get; set; }

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0000CD46 File Offset: 0x0000AF46
		// (set) Token: 0x060006B6 RID: 1718 RVA: 0x0000CD4E File Offset: 0x0000AF4E
		public Setting<int> VersionNumber { get; set; }

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0000CD57 File Offset: 0x0000AF57
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0000CD5F File Offset: 0x0000AF5F
		public Setting<bool> Windows2K8R2Version { get; set; }

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x060006B9 RID: 1721 RVA: 0x0000CD68 File Offset: 0x0000AF68
		// (set) Token: 0x060006BA RID: 1722 RVA: 0x0000CD70 File Offset: 0x0000AF70
		public Setting<bool> Windows8Version { get; set; }

		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x0000CD79 File Offset: 0x0000AF79
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x0000CD81 File Offset: 0x0000AF81
		public Setting<bool> Windows8ClientVersion { get; set; }

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x0000CD8A File Offset: 0x0000AF8A
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x0000CD92 File Offset: 0x0000AF92
		public Setting<bool> Windows7ClientVersion { get; set; }

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x0000CD9B File Offset: 0x0000AF9B
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x0000CDA3 File Offset: 0x0000AFA3
		public Setting<object[]> InstalledWindowsFeatures { get; set; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x0000CDAC File Offset: 0x0000AFAC
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x0000CDB4 File Offset: 0x0000AFB4
		public Setting<bool> IsWinRMIISExtensionInstalled { get; set; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x0000CDBD File Offset: 0x0000AFBD
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x0000CDC5 File Offset: 0x0000AFC5
		public Setting<bool> IsRSATWebServerInstalled { get; set; }

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x0000CDCE File Offset: 0x0000AFCE
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x0000CDD6 File Offset: 0x0000AFD6
		public Setting<bool> IsNETFrameworkInstalled { get; set; }

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0000CDDF File Offset: 0x0000AFDF
		// (set) Token: 0x060006C8 RID: 1736 RVA: 0x0000CDE7 File Offset: 0x0000AFE7
		public Setting<bool> IsNETFramework45FeaturesInstalled { get; set; }

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0000CDF0 File Offset: 0x0000AFF0
		// (set) Token: 0x060006CA RID: 1738 RVA: 0x0000CDF8 File Offset: 0x0000AFF8
		public Setting<bool> IsWebNetExt45Installed { get; set; }

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0000CE01 File Offset: 0x0000B001
		// (set) Token: 0x060006CC RID: 1740 RVA: 0x0000CE09 File Offset: 0x0000B009
		public Setting<bool> IsWebISAPIExtInstalled { get; set; }

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0000CE12 File Offset: 0x0000B012
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x0000CE1A File Offset: 0x0000B01A
		public Setting<bool> IsWebASPNET45Installed { get; set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0000CE23 File Offset: 0x0000B023
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0000CE2B File Offset: 0x0000B02B
		public Setting<bool> IsRPCOverHTTPproxyInstalled { get; set; }

		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x060006D1 RID: 1745 RVA: 0x0000CE34 File Offset: 0x0000B034
		// (set) Token: 0x060006D2 RID: 1746 RVA: 0x0000CE3C File Offset: 0x0000B03C
		public Setting<bool> IsServerGuiMgmtInfraInstalled { get; set; }

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x060006D3 RID: 1747 RVA: 0x0000CE45 File Offset: 0x0000B045
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x0000CE4D File Offset: 0x0000B04D
		public Setting<string> E12ServersNotMinVersionRequirement { get; set; }

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060006D5 RID: 1749 RVA: 0x0000CE56 File Offset: 0x0000B056
		// (set) Token: 0x060006D6 RID: 1750 RVA: 0x0000CE5E File Offset: 0x0000B05E
		public Setting<string> E14ServersNotMinVersionRequirement { get; set; }

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060006D7 RID: 1751 RVA: 0x0000CE67 File Offset: 0x0000B067
		// (set) Token: 0x060006D8 RID: 1752 RVA: 0x0000CE6F File Offset: 0x0000B06F
		public Setting<string> E14ServersNotMinMajorVersionRequirement { get; set; }

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x0000CE78 File Offset: 0x0000B078
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x0000CE80 File Offset: 0x0000B080
		public Setting<string> AllServersOfHigherVersion { get; set; }

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0000CE89 File Offset: 0x0000B089
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0000CE91 File Offset: 0x0000B091
		public Setting<bool> IsCoreServer { get; set; }

		// Token: 0x170002C8 RID: 712
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0000CE9A File Offset: 0x0000B09A
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x0000CEA2 File Offset: 0x0000B0A2
		public Setting<uint> OSProductType { get; set; }

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0000CEAB File Offset: 0x0000B0AB
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x0000CEB3 File Offset: 0x0000B0B3
		public Setting<uint> SuiteMask { get; set; }

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0000CEBC File Offset: 0x0000B0BC
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
		public Setting<bool> ValidOSSuite { get; set; }

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0000CECD File Offset: 0x0000B0CD
		// (set) Token: 0x060006E4 RID: 1764 RVA: 0x0000CED5 File Offset: 0x0000B0D5
		public Setting<string> ServiceMarkedForDeletion { get; set; }

		// Token: 0x170002CC RID: 716
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x0000CEDE File Offset: 0x0000B0DE
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x0000CEE6 File Offset: 0x0000B0E6
		public Setting<bool> PowerShellExecutionPolicy { get; set; }

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060006E7 RID: 1767 RVA: 0x0000CEEF File Offset: 0x0000B0EF
		// (set) Token: 0x060006E8 RID: 1768 RVA: 0x0000CEF7 File Offset: 0x0000B0F7
		public Setting<bool> VCRedist2012Installed { get; set; }

		// Token: 0x170002CE RID: 718
		// (get) Token: 0x060006E9 RID: 1769 RVA: 0x0000CF00 File Offset: 0x0000B100
		// (set) Token: 0x060006EA RID: 1770 RVA: 0x0000CF08 File Offset: 0x0000B108
		public Setting<bool> VCRedist2013Installed { get; set; }

		// Token: 0x060006EB RID: 1771 RVA: 0x0000CF14 File Offset: 0x0000B114
		protected override void OnAnalysisStart()
		{
			lock (this.logLock)
			{
				SetupLogger.Log(Strings.PrereqAnalysisStarted);
			}
		}

		// Token: 0x060006EC RID: 1772 RVA: 0x0000CF58 File Offset: 0x0000B158
		protected override void OnAnalysisStop()
		{
			TimeSpan duration = base.StopTime - base.StartTime;
			lock (this.logLock)
			{
				SetupLogger.Log(Strings.PrereqAnalysisStopped(duration));
				foreach (Result result in base.Errors)
				{
					string name = result.Source.Name;
					string message = Strings.PrereqAnalysisNullValue;
					MessageFeature messageFeature = result.Source.Features.OfType<MessageFeature>().FirstOrDefault<MessageFeature>();
					if (messageFeature != null)
					{
						message = messageFeature.Text(result);
					}
					SetupLogger.Log(Strings.PrereqAnalysisFailedRule(name, message));
				}
			}
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0000D034 File Offset: 0x0000B234
		protected override void OnAnalysisMemberStart(AnalysisMember member)
		{
			string name = member.Name;
			string name2 = member.Parent.Name;
			if (member is Rule)
			{
				RuleTypeFeature ruleTypeFeature = member.Features.OfType<RuleTypeFeature>().FirstOrDefault<RuleTypeFeature>();
				string ruleType = (ruleTypeFeature == null) ? RuleType.None.ToString() : ruleTypeFeature.RuleType.ToString();
				lock (this.logLock)
				{
					SetupLogger.Log(Strings.PrereqAnalysisRuleStarted(name, name2, ruleType));
				}
				return;
			}
			string name3 = member.ValueType.Name;
			lock (this.logLock)
			{
				SetupLogger.Log(Strings.PrereqAnalysisSettingStarted(name, name2, name3));
			}
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0000D118 File Offset: 0x0000B318
		protected override void OnAnalysisMemberStop(AnalysisMember member)
		{
			string memberType = (member is Rule) ? "Rule" : "Setting";
			string name = member.Name;
			TimeSpan duration = member.StopTime - member.StartTime;
			lock (this.logLock)
			{
				SetupLogger.Log(Strings.PrereqAnalysisMemberStopped(memberType, name, duration));
			}
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0000D190 File Offset: 0x0000B390
		protected override void OnAnalysisMemberEvaluate(AnalysisMember member, Result result)
		{
			string memberType = (member is Rule) ? "Rule" : "Setting";
			string name = member.Name;
			int managedThreadId = Thread.CurrentThread.ManagedThreadId;
			TimeSpan duration = result.StopTime - result.StartTime;
			bool hasException = result.HasException;
			string value = this.FormatValueOrExceptionText(result);
			string parentValue = result.Parent.HasException ? Strings.PrereqAnalysisParentExceptionValue : this.FormatValueOrExceptionText(result.Parent);
			lock (this.logLock)
			{
				SetupLogger.Log(Strings.PrereqAnalysisMemberEvaluated(memberType, name, hasException, value, parentValue, managedThreadId, duration));
			}
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0000D254 File Offset: 0x0000B454
		private string FormatValueOrExceptionText(Result result)
		{
			string text = Strings.PrereqAnalysisNullValue;
			if (!result.HasException)
			{
				return string.Format("\"{0}\"", (result.ValueAsObject == null) ? text : result.ValueAsObject.ToString());
			}
			Exception ex = result.Exception;
			StringBuilder stringBuilder = new StringBuilder();
			while (ex != null)
			{
				AnalysisException ex2 = ex as AnalysisException;
				if (ex2 != null)
				{
					string name = ex2.AnalysisMemberSource.Name;
					if (ex2 is FailureException)
					{
						stringBuilder.Append(Environment.NewLine + Strings.PrereqAnalysisExpectedFailure(name, ex2.Message));
						break;
					}
					stringBuilder.Append(Environment.NewLine + Strings.PrereqAnalysisFailureToAccessResults(name, ex2.Message));
					ex = ex.InnerException;
				}
				else
				{
					stringBuilder.Append(Environment.NewLine + ex.ToString());
					ex = ex.InnerException;
				}
			}
			return stringBuilder.ToString() + Environment.NewLine;
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00014FE0 File Offset: 0x000131E0
		private void CreateActiveDirectoryPrereqProperties()
		{
			this.RootDSEProperties = Setting<List<string>>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				List<string> value = null;
				if (this.SetupRoles.Results.Count((Result<string> w) => w.Value.Equals(SetupRole.Gateway.ToString(), StringComparison.InvariantCultureIgnoreCase)) == 0)
				{
					value = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/RootDSE", this.GlobalCatalog.Results.Value));
				}
				return new Result<List<string>>(value);
			});
			this.ConfigurationNamingContext = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.RootDSEProperties.Results.ValueOrDefault != null)
				{
					return new Result<string>(this.RootDSEProperties.Results.Value[0]);
				}
				return new Result<string>(string.Empty);
			});
			this.RootNamingContext = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.RootDSEProperties.Results.ValueOrDefault != null)
				{
					return new Result<string>(this.RootDSEProperties.Results.Value[1]);
				}
				return new Result<string>(string.Empty);
			});
			this.SchemaNamingContext = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.RootDSEProperties.Results.ValueOrDefault != null)
				{
					return new Result<string>(this.RootDSEProperties.Results.Value[2]);
				}
				return new Result<string>(string.Empty);
			});
			this.IsGlobalCatalogReady = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).SetValue(delegate(Result<object> x)
			{
				if (this.RootDSEProperties.Results.ValueOrDefault != null)
				{
					bool value = false;
					bool.TryParse(this.RootDSEProperties.Results.Value[3], out value);
					return new Result<bool>(value);
				}
				return new Result<bool>(false);
			});
			this.SchemaDN = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.SchemaNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.SchemaNamingContext.Results.Value), new string[]
					{
						"fsmoroleowner"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.DomainControllerRef = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.DomainController.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Sites,{1}", this.GlobalCatalog.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"serverReference"
					}, string.Format("(&(objectClass=server)(|(cn={0})(dNSHostName={1})))", this.DomainController.Results.Value, this.DomainController.Results.Value), SearchScope.Subtree);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["serverReference"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								return new Result<string>(obj2.ToString());
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.DomainControllerOS = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.DomainControllerRef.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.DomainControllerRef.Results.Value), new string[]
					{
						"operatingSystemVersion"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["operatingSystemVersion"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ComputerDomainDN = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.ComputerDomain.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Partitions,{1}", this.GlobalCatalog.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"nCName"
					}, string.Format("(&(dnsRoot={0})(systemFlags=3))", this.ComputerDomain.Results.Value), SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["nCName"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.LocalComputerDomainDN = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ComputerDomainDN.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.DomainController.Results.Value, this.ComputerDomainDN.Results.Value), new string[]
					{
						"nTMixedDomain",
						"objectSid"
					}, string.Empty, SearchScope.Base);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.LocalDomainNtSecurityDescriptor = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.LocalComputerDomainDN.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.LocalComputerDomainDN.Results.Value["objectSid"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object val = enumerator.Current;
							return new Result<string>(AnalysisHelpers.ConvertToStringSid(val));
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.SmoRoleOwner = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.SchemaDN.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.SchemaDN.Results.Value["fsmoroleowner"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(AnalysisHelpers.Replace(obj.ToString(), "CN=NTDS Settings,CN=(.*?),.*", "$1"));
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.SmoSchemaDN = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.SmoRoleOwner.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Sites, {1}", this.GlobalCatalog.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"dNSHostName",
						"serverReference"
					}, string.Format("(&(objectClass=server)(cn={0}))", this.SmoRoleOwner.Results.Value), SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.DnsHostName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.SmoSchemaDN.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.SmoSchemaDN.Results.Value["dNSHostName"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ServerReference = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.SmoSchemaDN.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.SmoSchemaDN.Results.Value["serverReference"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.SmoSchemaDomain = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ServerReference.Results.ValueOrDefault))
				{
					return new Result<string>(this.ServerReference.Results.Value.Substring(this.ServerReference.Results.Value.IndexOf("DC=")));
				}
				return new Result<string>(string.Empty);
			});
			this.SmoRoleSchemaRef = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ServerReference.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.ServerReference.Results.Value), new string[]
					{
						"operatingSystemVersion",
						"operatingSystemServicePack"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.SmoOperatingSystemVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.SmoRoleSchemaRef.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.SmoRoleSchemaRef.Results.Value["operatingSystemVersion"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.SmoOperatingSystemServicePack = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.SmoRoleSchemaRef.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.SmoRoleSchemaRef.Results.Value["operatingSystemServicePack"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.Win2003FSMOSchemaServer = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>(this.SmoOperatingSystemVersion.Results.Value.StartsWith("5.2")));
			this.SmoSchemaServicePack = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>(this.SmoOperatingSystemServicePack.Results.Value.StartsWith("Service Pack 2")));
			this.MicrosoftExchServicesConfig = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Microsoft Exchange,cn=Services, {1}", this.GlobalCatalog.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"distinguishedName",
						"otherWellKnownObjects"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.MicrosoftExchServicesConfigDistinguishedName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfig.Results.Value["distinguishedName"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.MicrosoftExchServicesAdminGroupsConfig = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					string[] listOfPropertiesToCollect = new string[]
					{
						"distinguishedName",
						"cn",
						"msExchCurrentServerRoles",
						"legacyExchangeDN",
						"nTSecurityDescriptor"
					};
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), listOfPropertiesToCollect, string.Format("(&(objectClass=msExchExchangeServer)(cn={0}))", this.ShortServerName.Results.Value), SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.ServerAlreadyExists = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesAdminGroupsConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesAdminGroupsConfig.Results.Value["cn"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<bool>(!string.IsNullOrEmpty(obj.ToString()));
						}
					}
				}
				return new Result<bool>(false);
			});
			this.PrereqServerDN = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesAdminGroupsConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesAdminGroupsConfig.Results.Value["distinguishedName"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.PrereqServerLegacyDN = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesAdminGroupsConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesAdminGroupsConfig.Results.Value["legacyExchangeDN"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ExchangeCurrentServerRoles = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesAdminGroupsConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesAdminGroupsConfig.Results.Value["msExchCurrentServerRoles"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<int>((int)obj);
						}
					}
				}
				return new Result<int>(0);
			});
			this.NtSecurityDescriptor = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesAdminGroupsConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesAdminGroupsConfig.Results.Value["nTSecurityDescriptor"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object val = enumerator.Current;
							return new Result<string>(AnalysisHelpers.ConvertBinaryToString(val));
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.MailboxRoleInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>((this.ExchangeCurrentServerRoles.Results.Value & 2) == 2));
			this.ClientAccessRoleInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>((this.ExchangeCurrentServerRoles.Results.Value & 4) == 4));
			this.CafeRoleInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>((this.ExchangeCurrentServerRoles.Results.Value & 1) == 1));
			this.UnifiedMessagingRoleInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>((this.ExchangeCurrentServerRoles.Results.Value & 16) == 16));
			this.BridgeheadRoleInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>((this.ExchangeCurrentServerRoles.Results.Value & 32) == 32));
			this.GatewayRoleInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>((this.ExchangeCurrentServerRoles.Results.Value & 64) == 64));
			this.FrontendTransportRoleInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>((this.ExchangeCurrentServerRoles.Results.Value & 16384) == 16384));
			this.ServerIsProvisioned = Setting<bool>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>((this.ExchangeCurrentServerRoles.Results.Value & 4096) == 4096));
			this.ExtendedRightsNtSecurityDescriptor = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Extended-Rights, {1}", this.GlobalCatalog.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"nTSecurityDescriptor"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["nTSecurityDescriptor"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object val = enumerator2.Current;
									return new Result<string>(AnalysisHelpers.ConvertBinaryToString(val));
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ExchangeOrgAdminsGroupOtherWellKnownObjects = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfig.Results.Value["otherWellKnownObjects"];
					foreach (object obj in resultPropertyValueCollection)
					{
						string value = AnalysisHelpers.Replace(obj.ToString(), "(^B:32:C262A929D691B74A9E068728F8F842EA:(?'dn'.*))?.*$", "${dn}");
						if (!string.IsNullOrEmpty(value))
						{
							return new Result<string>(value);
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.OrgDN = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ExchangeOrgAdminsGroupOtherWellKnownObjects.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.ExchangeOrgAdminsGroupOtherWellKnownObjects.Results.Value), new string[]
					{
						"sAMAccountName",
						"objectSid"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.ExOrgAdminAccountName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.OrgDN.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.OrgDN.Results.Value["sAMAccountName"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.SidExOrgAdmins = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.OrgDN.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.OrgDN.Results.Value["objectSid"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object val = enumerator.Current;
							return new Result<string>(AnalysisHelpers.ConvertToStringSid(val));
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ServerManagementGroupOtherWellKnownObjects = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfig.Results.Value["otherWellKnownObjects"];
					foreach (object obj in resultPropertyValueCollection)
					{
						string value = AnalysisHelpers.Replace(obj.ToString(), "(^B:32:4DB8E7754EB6C1439565612E69A80A4F:(?'dn'.*))?.*$", "${dn}");
						if (!string.IsNullOrEmpty(value))
						{
							return new Result<string>(value);
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ServerManagementGroupDN = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ServerManagementGroupOtherWellKnownObjects.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.ServerManagementGroupOtherWellKnownObjects.Results.Value), new string[]
					{
						"sAMAccountName",
						"objectSid"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.SidServerManagementGroup = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.ServerManagementGroupDN.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.ServerManagementGroupDN.Results.Value["objectSid"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object val = enumerator.Current;
							return new Result<string>(AnalysisHelpers.ConvertToStringSid(val));
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ExchangeServersGroupOtherWellKnownObjects = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfig.Results.Value["otherWellKnownObjects"];
					foreach (object obj in resultPropertyValueCollection)
					{
						string value = AnalysisHelpers.Replace(obj.ToString(), "(^B:32:A7D2016C83F003458132789EEB127B84:(?'dn'.*))?.*$", "${dn}");
						if (!string.IsNullOrEmpty(value))
						{
							return new Result<string>(value);
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ExchangeServicesConfigExchangeServersGroup = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ExchangeServersGroupOtherWellKnownObjects.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.ExchangeServersGroupOtherWellKnownObjects.Results.Value), new string[]
					{
						"sAMAccountName",
						"nTSecurityDescriptor"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.ExchangeServersGroupAMAccountName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.ExchangeServicesConfigExchangeServersGroup.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.ExchangeServicesConfigExchangeServersGroup.Results.Value["sAMAccountName"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ExchangeServersGroupNtSecurityDescriptor = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.ExchangeServicesConfigExchangeServersGroup.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.ExchangeServicesConfigExchangeServersGroup.Results.Value["nTSecurityDescriptor"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object val = enumerator.Current;
							return new Result<string>(AnalysisHelpers.ConvertBinaryToString(val));
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.SchemaVersionRangeUpper = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.SchemaNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=ms-Exch-Schema-Version-Pt, {1}", this.GlobalCatalog.Results.Value, this.SchemaNamingContext.Results.Value), new string[]
					{
						"rangeUpper"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["rangeUpper"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<int?>(new int?((int)obj2));
								}
							}
						}
					}
				}
				return new Result<int?>(null);
			});
			this.AdcServer = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetMultipleValues(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Sites,{1}", this.GlobalCatalog.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"cn"
					}, "(objectClass=msExchActiveDirectoryConnector)", SearchScope.Subtree);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
						foreach (object obj2 in resultPropertyValueCollection)
						{
							list.Add(obj2.ToString());
						}
					}
				}
				return from w in list
				select new Result<string>(w);
			});
			this.OrgDistinguishedName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.OrgMicrosoftExchServicesConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.OrgMicrosoftExchServicesConfig.Results.Value["distinguishedName"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ExchangeMixedMode = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.OrgMicrosoftExchServicesConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.OrgMicrosoftExchServicesConfig.Results.Value["msExchMixedMode"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<bool>(Convert.ToBoolean(obj.ToString()));
						}
					}
				}
				return new Result<bool>(false);
			});
			this.ConnectorToStar = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, "(&(objectClass=msExchRoutingSMTPConnector)(routingList=SMTP:\\2a;*))", SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ExchangeVersionPrefix = Setting<float>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.OrgMicrosoftExchServicesConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.OrgMicrosoftExchServicesConfig.Results.Value["msExchVersion"];
					foreach (object obj in resultPropertyValueCollection)
					{
						string[] array = obj.ToString().Split(new string[]
						{
							":"
						}, StringSplitOptions.RemoveEmptyEntries);
						if (array != null && array.Length > 1)
						{
							return new Result<float>(float.Parse(array[0]));
						}
					}
				}
				return new Result<float>(0f);
			});
			this.PrepareDomainNCName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.PrepareDomain.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Partitions,{1}", this.GlobalCatalog.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"nCName"
					}, string.Format("(&(systemFlags=3)(|(cn={0})(dnsRoot={1})))", this.PrepareDomain.Results.Value, this.PrepareDomain.Results.Value), SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["nCName"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.PrepareDomainNCNameConfig = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.PrepareDomainNCName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.PrepareDomainNCName.Results.Value), new string[]
					{
						"objectSid",
						"nTMixedDomain"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.SidPrepareDomain = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.PrepareDomainNCNameConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.PrepareDomainNCNameConfig.Results.Value["objectSid"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object val = enumerator.Current;
							return new Result<string>(AnalysisHelpers.ConvertToStringSid(val));
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.NtMixedDomainPrepareDomain = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.PrepareDomainNCNameConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.PrepareDomainNCNameConfig.Results.Value["nTMixedDomain"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<int>((int)obj);
						}
					}
				}
				return new Result<int>(0);
			});
			this.ExchangeServers = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetMultipleValues(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesConfigDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesConfigDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, "objectClass=msExchExchangeServer", SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
							foreach (object obj2 in resultPropertyValueCollection)
							{
								list.Add(obj2.ToString());
							}
						}
					}
				}
				return from w in list
				select new Result<string>(w);
			});
			this.RootDN = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.RootNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.RootNamingContext.Results.Value), new string[]
					{
						"objectSid",
						"nTMixedDomain"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.SidRootDomain = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.RootDN.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.RootDN.Results.Value["objectSid"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object val = enumerator.Current;
							return new Result<string>(AnalysisHelpers.ConvertToStringSid(val));
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.NtMixedDomainRoot = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.RootDN.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.RootDN.Results.Value["nTMixedDomain"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<int>((int)obj);
						}
					}
				}
				return new Result<int>(0);
			});
			this.ServerSetupRoles = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<int>(this.globalParameters.SetupRoles.Count((string sr) => sr != "Admin Tools")));
			this.MicrosoftExchServicesAdminGroupConfig = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"distinguishedName"
					}, "(objectClass=msExchAdminGroup)", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.MicrosoftExchServicesAdminGroupConfigDistinguishedName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesAdminGroupConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesAdminGroupConfig.Results.Value["distinguishedName"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.MicrosoftExchAdminGroupsMailboxRoleConfig = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers, {1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, string.Format("(&(objectClass=msExchExchangeServer)(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=2)(versionNumber>={0}))", this.VersionNumber.Results.Value.ToString()), SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.MailboxRoleOfEqualOrHigherVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetMultipleValues(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				if (this.MicrosoftExchAdminGroupsMailboxRoleConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchAdminGroupsMailboxRoleConfig.Results.Value["cn"];
					foreach (object obj in resultPropertyValueCollection)
					{
						list.Add(obj.ToString());
					}
				}
				return from w in list
				select new Result<string>(w);
			});
			this.MicrosoftExchAdminGroupsBridgeheadRoleConfig = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers, {1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, string.Format("(&(objectClass=msExchExchangeServer)(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=32)(versionNumber>={0}))", this.VersionNumber.Results.Value.ToString()), SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.BridgeheadRoleOfEqualOrHigherVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetMultipleValues(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				if (this.MicrosoftExchAdminGroupsBridgeheadRoleConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchAdminGroupsBridgeheadRoleConfig.Results.Value["cn"];
					foreach (object obj in resultPropertyValueCollection)
					{
						list.Add(obj.ToString());
					}
				}
				return from w in list
				select new Result<string>(w);
			});
			this.MicrosoftExchAdminGroupsClientAccessRoleConfig = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers, {1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, string.Format("(&(objectClass=msExchExchangeServer)(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=4)(versionNumber>={0}))", this.VersionNumber.Results.Value.ToString()), SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.ClientAccessRoleOfEqualOrHigherVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetMultipleValues(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				if (this.MicrosoftExchAdminGroupsClientAccessRoleConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchAdminGroupsClientAccessRoleConfig.Results.Value["cn"];
					foreach (object obj in resultPropertyValueCollection)
					{
						list.Add(obj.ToString());
					}
				}
				return from w in list
				select new Result<string>(w);
			});
			this.MicrosoftExchAdminGroupsUnifiedMessagingRoleConfig = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers, {1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, string.Format("(&(objectClass=msExchExchangeServer)(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=16)(versionNumber>={0}))", this.VersionNumber.Results.Value.ToString()), SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.UnifiedMessagingRoleOfEqualOrHigherVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetMultipleValues(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				if (this.MicrosoftExchAdminGroupsUnifiedMessagingRoleConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchAdminGroupsUnifiedMessagingRoleConfig.Results.Value["cn"];
					foreach (object obj in resultPropertyValueCollection)
					{
						list.Add(obj.ToString());
					}
				}
				return from w in list
				select new Result<string>(w);
			});
			this.MicrosoftExchAdminGroupsCafeRoleConfig = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers, {1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, string.Format("(&(objectClass=msExchExchangeServer)(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=1)(versionNumber>={0}))", this.VersionNumber.Results.Value.ToString()), SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.CafeRoleOfEqualOrHigherVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetMultipleValues(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				if (this.MicrosoftExchAdminGroupsCafeRoleConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchAdminGroupsCafeRoleConfig.Results.Value["cn"];
					foreach (object obj in resultPropertyValueCollection)
					{
						list.Add(obj.ToString());
					}
				}
				return from w in list
				select new Result<string>(w);
			});
			this.MicrosoftExchAdminGroupsFrontendTransportRoleConfig = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers, {1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, string.Format("(&(objectClass=msExchExchangeServer)(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=16384)(versionNumber>={0}))", this.VersionNumber.Results.Value.ToString()), SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.FrontendTransportRoleOfEqualOrHigherVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetMultipleValues(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				if (this.MicrosoftExchAdminGroupsFrontendTransportRoleConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchAdminGroupsFrontendTransportRoleConfig.Results.Value["cn"];
					foreach (object obj in resultPropertyValueCollection)
					{
						list.Add(obj.ToString());
					}
				}
				return from w in list
				select new Result<string>(w);
			});
			this.OabDN = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Offline Address Lists,cn=Address Lists Container, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"distinguishedName"
					}, string.Format("(&(objectClass=msExchOAB)(offlineABServer={0}))", this.PrereqServerDN.Results.Value), SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["distinguishedName"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.OtherPotentialOABServers = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.All).SetMultipleValues(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers, {1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, "(&(objectClass=msExchExchangeServer)(|(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=2)(!(msExchCurrentServerRoles=*))))", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
							foreach (object obj2 in resultPropertyValueCollection)
							{
								list.Add(obj2.ToString());
							}
						}
					}
				}
				return from w in list
				select new Result<string>(w);
			});
			this.OtherPotentialExpansionServers = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Bridgehead).Mode(SetupMode.All).SetMultipleValues(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers, {1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesAdminGroupConfigDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, "(&(objectClass=msExchExchangeServer)(|(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=32)(!(msExchCurrentServerRoles=*))))", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
							foreach (object obj2 in resultPropertyValueCollection)
							{
								list.Add(obj2.ToString());
							}
						}
					}
				}
				return from w in list
				select new Result<string>(w);
			});
			this.E12ServerInTopology = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, "(&(objectClass=msExchExchangeServer)(serialNumber=Version 8.*))", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.E14ServerInTopology = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, "(&(objectClass=msExchExchangeServer)(serialNumber=Version 14.*))", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.E15ServerInTopology = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, "(&(objectClass=msExchExchangeServer)(serialNumber=Version 15.*))", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.MicrosoftExchServicesConfigBridgeheadRoleInTopology = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn",
						"serialNumber"
					}, "(&(objectClass=msExchExchangeServer)(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=32))", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.MicrosoftExchServicesConfigCafeRoleInTopology = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn",
						"serialNumber"
					}, "(&(objectClass=msExchExchangeServer)(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=8))", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.MicrosoftExchServicesConfigUMRoleInTopology = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn",
						"serialNumber"
					}, "(&(objectClass=msExchExchangeServer)(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=16))", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.MicrosoftExchServicesConfigCASRoleInTopology = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn",
						"serialNumber"
					}, "(&(objectClass=msExchExchangeServer)(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=4))", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.MicrosoftExchServicesConfigMBXRoleInTopology = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn",
						"serialNumber"
					}, "(&(objectClass=msExchExchangeServer)(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=2))", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.BridgeheadRoleInTopology = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfigBridgeheadRoleInTopology.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfigBridgeheadRoleInTopology.Results.Value["cn"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.CafeRoleInTopology = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfigCafeRoleInTopology.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfigCafeRoleInTopology.Results.Value["cn"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.UnifiedMessagingRoleInTopology = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfigUMRoleInTopology.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfigUMRoleInTopology.Results.Value["cn"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ClientAccessRoleInTopology = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfigUMRoleInTopology.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfigUMRoleInTopology.Results.Value["cn"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.MailboxRoleInTopology = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfigUMRoleInTopology.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfigUMRoleInTopology.Results.Value["cn"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.E12SP1orHigherHubAlreadyExists = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfigBridgeheadRoleInTopology.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfigBridgeheadRoleInTopology.Results.Value["serialNumber"];
					foreach (object obj in resultPropertyValueCollection)
					{
						Version v;
						Version.TryParse(AnalysisHelpers.Replace(obj.ToString(), "^Version (\\d+\\.\\d+).*$", "$1"), out v);
						if (v != null && v >= new Version(8, 1))
						{
							return new Result<bool>(true);
						}
					}
				}
				return new Result<bool>(false);
			});
			this.E15orHigherHubAlreadyExists = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfigBridgeheadRoleInTopology.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfigBridgeheadRoleInTopology.Results.Value["serialNumber"];
					foreach (object obj in resultPropertyValueCollection)
					{
						Version v;
						Version.TryParse(AnalysisHelpers.Replace(obj.ToString(), "^Version (\\d+\\.\\d+).*$", "$1"), out v);
						if (v != null && v >= new Version(15, 0))
						{
							return new Result<bool>(true);
						}
					}
				}
				return new Result<bool>(false);
			});
			this.E12SP1orHigherUMAlreadyExists = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfigUMRoleInTopology.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfigUMRoleInTopology.Results.Value["serialNumber"];
					foreach (object obj in resultPropertyValueCollection)
					{
						Version v;
						Version.TryParse(AnalysisHelpers.Replace(obj.ToString(), "^Version (\\d+\\.\\d+).*$", "$1"), out v);
						if (v != null && v >= new Version(8, 1))
						{
							return new Result<bool>(true);
						}
					}
				}
				return new Result<bool>(false);
			});
			this.E12SP1orHigherCASAlreadyExists = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfigUMRoleInTopology.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfigUMRoleInTopology.Results.Value["serialNumber"];
					foreach (object obj in resultPropertyValueCollection)
					{
						Version v;
						Version.TryParse(AnalysisHelpers.Replace(obj.ToString(), "^Version (\\d+\\.\\d+).*$", "$1"), out v);
						if (v != null && v >= new Version(8, 1))
						{
							return new Result<bool>(true);
						}
					}
				}
				return new Result<bool>(false);
			});
			this.E12SP1orHigherMBXAlreadyExists = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfigUMRoleInTopology.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfigUMRoleInTopology.Results.Value["serialNumber"];
					foreach (object obj in resultPropertyValueCollection)
					{
						Version v;
						Version.TryParse(AnalysisHelpers.Replace(obj.ToString(), "^Version (\\d+\\.\\d+).*$", "$1"), out v);
						if (v != null && v >= new Version(8, 1))
						{
							return new Result<bool>(true);
						}
					}
				}
				return new Result<bool>(false);
			});
			this.ExchangeConfigurationUnitsConfiguration = Setting<string>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesConfigDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/CN=ConfigurationUnits,{1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesConfigDistinguishedName.Results.Value), new string[]
					{
						"distinguishedName"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["distinguishedName"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ExchangeConfigurationUnitsDomain = Setting<string>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.RootNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/CN=ConfigurationUnits,{1}", this.GlobalCatalog.Results.Value, this.RootNamingContext.Results.Value), new string[]
					{
						"distinguishedName"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["distinguishedName"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ShortProvisionedName = Setting<string>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.All).Mode(SetupMode.All).SetMultipleValues(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				if (!string.IsNullOrEmpty(this.RootNamingContext.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.NewProvisionedServerName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("GC://{0}/{1}", this.GlobalCatalog.Results.Value, this.RootNamingContext.Results.Value), new string[0], string.Format("(&(objectClass=computer)(name={0}))", this.NewProvisionedServerName.Results.Value), SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							foreach (object obj2 in searchResult.Properties)
							{
								ResultPropertyValueCollection resultPropertyValueCollection = (ResultPropertyValueCollection)obj2;
								foreach (object obj3 in resultPropertyValueCollection)
								{
									list.Add(obj3.ToString());
								}
							}
						}
					}
				}
				return from w in list
				select new Result<string>(w);
			});
			this.MsDSBehaviorVersion = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Partitions,{1}", this.GlobalCatalog.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"msDS-Behavior-Version"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["msDS-Behavior-Version"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<int>((int)obj2);
								}
							}
						}
					}
				}
				return new Result<int>(0);
			});
			this.MicrosoftExchServicesConfigAdminGroupDistinguishedName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"distinguishedName"
					}, "(&(objectCategory=msExchAdminGroup)(cn=*))", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["distinguishedName"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.MicrosoftExchServicesConfigAdminGroupPublicFoldersConfig = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesConfigAdminGroupDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesConfigAdminGroupDistinguishedName.Results.Value), new string[]
					{
						"distinguishedName",
						"ntSecurityDescriptor"
					}, "(&(objectCategory=msExchAdminGroup)(cn=*))", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.MicrosoftExchServicesConfigAdminGroupPublicFoldersDistinguishedName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfigAdminGroupPublicFoldersConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfigAdminGroupPublicFoldersConfig.Results.Value["distinguishedName"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.MicrosoftExchServicesConfigAdminGroupPublicFoldersNtSecurityDescriptor = Setting<byte[]>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.MicrosoftExchServicesConfigAdminGroupPublicFoldersConfig.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MicrosoftExchServicesConfigAdminGroupPublicFoldersConfig.Results.Value["ntSecurityDescriptor"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<byte[]>((byte[])obj);
						}
					}
				}
				return new Result<byte[]>(new byte[0]);
			});
			this.ExchOab = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesConfigDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Offline Address Lists,cn=Address Lists Container, {1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesConfigDistinguishedName.Results.Value), new string[]
					{
						"cn",
						"offLineABServer"
					}, "objectClass=msExchOAB", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.OabName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				if (this.ExchOab.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.ExchOab.Results.Value["cn"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.OffLineABServer = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install).SetMultipleValues(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				if (this.ExchOab.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.ExchOab.Results.Value["offLineABServer"];
					foreach (object obj in resultPropertyValueCollection)
					{
						list.Add(obj.ToString());
					}
				}
				return from w in list
				select new Result<string>(w);
			});
			this.ExchangeRecipientPolicyConfiguration = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Recipient Policies, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn",
						"msExchNonAuthoritativeDomains",
						"disabledGatewayProxy",
						"gatewayProxy"
					}, "objectClass=msExchRecipientPolicy", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.RecipientPolicyName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				if (this.ExchangeRecipientPolicyConfiguration.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.ExchangeRecipientPolicyConfiguration.Results.Value["cn"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(obj.ToString());
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ExchNonAuthoritativeDomains = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				if (this.ExchangeRecipientPolicyConfiguration.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.ExchangeRecipientPolicyConfiguration.Results.Value["msExchNonAuthoritativeDomains"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(AnalysisHelpers.Replace(obj.ToString().ToLower(), "^smtp:.*\\@(?'domain'.*))?.*$", "${domain}"));
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.DisabledGatewayProxy = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				if (this.ExchangeRecipientPolicyConfiguration.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.ExchangeRecipientPolicyConfiguration.Results.Value["disabledGatewayProxy"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(AnalysisHelpers.Replace(obj.ToString().ToLower(), "^smtp:.*\\@(?'domain'.*))?.*$", "${domain}"));
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.EnabledSMTPDomain = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				if (this.ExchangeRecipientPolicyConfiguration.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.ExchangeRecipientPolicyConfiguration.Results.Value["gatewayProxy"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(AnalysisHelpers.Replace(obj.ToString().ToLower(), "^smtp:.*\\@(?'domain'.*))?.*$", "${domain}"));
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.GatewayProxy = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				if (this.ExchangeRecipientPolicyConfiguration.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.ExchangeRecipientPolicyConfiguration.Results.Value["gatewayProxy"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<string>(AnalysisHelpers.Replace(obj.ToString(), "^((?i:smtp)\\:.*\\@(?'smtpaddress'.*))?.*$", "${smtpaddress}"));
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.NonAuthoritativeDomainsArray = Setting<string[]>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ExchNonAuthoritativeDomains.Results.ValueOrDefault))
				{
					return new Result<string[]>(this.ExchNonAuthoritativeDomains.Results.Value.Split(new char[]
					{
						' '
					}));
				}
				return new Result<string[]>(new string[0]);
			});
			this.DisabledGatewayProxyArray = Setting<string[]>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.DisabledGatewayProxy.Results.ValueOrDefault))
				{
					new Result<string[]>(this.DisabledGatewayProxy.Results.Value.Split(new char[]
					{
						' '
					}));
				}
				return new Result<string[]>(new string[0]);
			});
			this.EnabledGatewayProxyArray = Setting<string[]>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.Install).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.EnabledSMTPDomain.Results.ValueOrDefault))
				{
					new Result<string[]>(this.EnabledSMTPDomain.Results.Value.Split(new char[]
					{
						' '
					}));
				}
				return new Result<string[]>(new string[0]);
			});
			this.DomainNCName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Partitions, {1}", this.GlobalCatalog.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"nCName"
					}, "(systemFlags=3)", SearchScope.OneLevel);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["nCName"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.MicrosoftExchangeSystemObjectsCN = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Global).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.DomainNCName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Microsoft Exchange System Objects, {1}", this.GlobalCatalog.Results.Value, this.DomainNCName.Results.Value), new string[]
					{
						"cn"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.RusName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Global).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.MicrosoftExchangeSystemObjectsCN.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Microsoft Exchange,cn=Services, {1}", this.GlobalCatalog.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"cn"
					}, string.Format("(&(objectClass=msExchAddressListService)(msExchDomainLink={0}))", this.MicrosoftExchangeSystemObjectsCN.Results.Value), SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<string>(obj2.ToString());
								}
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ExchangePrivateMDB = Setting<List<ResultPropertyCollection>>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				List<ResultPropertyCollection> list = new List<ResultPropertyCollection>();
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn",
						"distinguishedName",
						"msExchESEParamLogFilePath",
						"msExchEDBFile"
					}, string.Format("(&(objectClass=msExchPrivateMDB)(msExchOwningServer={0}))", this.PrereqServerDN.Results.Value), SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							list.Add(searchResult.Properties);
						}
					}
				}
				return new Result<List<ResultPropertyCollection>>(list);
			});
			this.PrivateDatabaseName = Setting<List<string>>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				foreach (ResultPropertyCollection resultPropertyCollection in this.ExchangePrivateMDB.Results.Value)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = resultPropertyCollection["cn"];
					if (!AnalysisHelpers.IsNullOrEmpty(resultPropertyValueCollection))
					{
						list.Add(resultPropertyValueCollection[0].ToString());
					}
				}
				return new Result<List<string>>(list);
			});
			this.PrivateDatabaseNameDN = Setting<List<string>>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				foreach (ResultPropertyCollection resultPropertyCollection in this.ExchangePrivateMDB.Results.Value)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = resultPropertyCollection["distinguishedName"];
					if (!AnalysisHelpers.IsNullOrEmpty(resultPropertyValueCollection))
					{
						list.Add(resultPropertyValueCollection[0].ToString());
					}
				}
				return new Result<List<string>>(list);
			});
			this.PrivateDatabaseEdbDrive = Setting<List<string>>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				foreach (ResultPropertyCollection resultPropertyCollection in this.ExchangePrivateMDB.Results.Value)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = resultPropertyCollection["msExchEDBFile"];
					if (!AnalysisHelpers.IsNullOrEmpty(resultPropertyValueCollection))
					{
						list.Add(resultPropertyValueCollection[0].ToString());
					}
				}
				return new Result<List<string>>(list);
			});
			this.PrivateDatabaseLogDrive = Setting<List<string>>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				foreach (ResultPropertyCollection resultPropertyCollection in this.ExchangePrivateMDB.Results.Value)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = resultPropertyCollection["msExchESEParamLogFilePath"];
					if (!AnalysisHelpers.IsNullOrEmpty(resultPropertyValueCollection))
					{
						list.Add(resultPropertyValueCollection[0].ToString());
					}
				}
				return new Result<List<string>>(list);
			});
			this.ExchCurrentServerRoles = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MicrosoftExchServicesConfigDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.MicrosoftExchServicesConfigDistinguishedName.Results.Value), new string[]
					{
						"msExchCurrentServerRoles"
					}, string.Format("(&(objectClass=msExchExchangeServer)(cn={0}))", this.RemoveServerName.Results.Value), SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["msExchCurrentServerRoles"];
							using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
							{
								if (enumerator2.MoveNext())
								{
									object obj2 = enumerator2.Current;
									return new Result<int>((int)obj2);
								}
							}
						}
					}
				}
				return new Result<int>(0);
			});
			this.AllServersOfHigherVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				StringBuilder stringBuilder = new StringBuilder();
				string text = this.OrgDistinguishedName.Results.ValueOrDefault;
				if (string.IsNullOrEmpty(text))
				{
					List<string> list = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/RootDSE", this.GlobalCatalog.Results.Value));
					string text2 = list[0];
					if (!string.IsNullOrEmpty(text2))
					{
						SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Microsoft Exchange,cn=Services, {1}", this.GlobalCatalog.Results.Value, text2), new string[]
						{
							"distinguishedName"
						}, null, SearchScope.Base);
						if (searchResultCollection != null)
						{
							string arg = searchResultCollection[0].Properties["distinguishedName"][0].ToString();
							SearchResultCollection searchResultCollection2 = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, arg), new string[]
							{
								"distinguishedName"
							}, "objectClass=msExchOrganizationContainer", SearchScope.OneLevel);
							if (searchResultCollection2 != null)
							{
								text = searchResultCollection2[0].Properties["distinguishedName"][0].ToString();
							}
						}
					}
				}
				if (!string.IsNullOrEmpty(text))
				{
					int num = new ServerVersion(ValidationConstant.AllServersOfHigherVersionMinimum.Major, ValidationConstant.AllServersOfHigherVersionMinimum.Minor, 0, 0).ToInt();
					SearchResultCollection searchResultCollection3 = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, text), new string[]
					{
						"cn",
						"versionnumber"
					}, "(&(objectClass=msExchExchangeServer)(!msExchCurrentServerRoles=0))", SearchScope.OneLevel);
					foreach (object obj in searchResultCollection3)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["versionnumber"];
						foreach (object obj2 in resultPropertyValueCollection)
						{
							int num2 = int.Parse(obj2.ToString());
							if (num2 >= num)
							{
								ResultPropertyValueCollection resultPropertyValueCollection2 = searchResult.Properties["cn"];
								using (IEnumerator enumerator3 = resultPropertyValueCollection2.GetEnumerator())
								{
									while (enumerator3.MoveNext())
									{
										object obj3 = enumerator3.Current;
										if (stringBuilder.Length > 0)
										{
											stringBuilder.Append(",");
										}
										stringBuilder.Append(obj3.ToString());
									}
									continue;
								}
							}
							return new Result<string>("");
						}
					}
				}
				return new Result<string>(stringBuilder.ToString());
			});
			this.AllServersOfHigherVersionRule = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.AllServersOfHigherVersionFailure(this.AllServersOfHigherVersion.Results.Value)).Condition(delegate(Result<object> x)
			{
				bool value = false;
				if (!this.globalParameters.IsDatacenter && !string.IsNullOrEmpty(this.AllServersOfHigherVersion.Results.ValueOrDefault))
				{
					value = true;
				}
				return new RuleResult(value);
			});
			this.CannotUninstallDelegatedServer = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.Uninstall).Error<RuleBuilder<object>>().Message((Result x) => Strings.CannotUninstallDelegatedServer).Condition((Result<object> x) => new RuleResult(this.ServerSetupRoles.Results.Value == this.Roles.Results.Count<Result<string>>() && this.ExchangeServers.Results.Value.Count<char>() == 1 && !this.HasExchangeServersUSGWritePerms.Results.Value && this.SetupRoles.Results.Value.Count<char>() == 1 && this.SetupRoles.Results.Value[0].ToString() != "AdminTools"));
			this.DomainPrepWithoutADUpdate = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.ADUpdateForDomainPrep("Exchange 2010")).Condition((Result<object> x) => new RuleResult((this.SchemaVersionRangeUpper.Results.ValueOrDefault == null || this.SchemaVersionRangeUpper.Results.Value < 14622 || string.IsNullOrEmpty(this.ExchangeServersGroupAMAccountName.Results.ValueOrDefault)) && !this.PrepareSchema.Results.Value && !this.PrepareOrganization.Results.Value && (!string.IsNullOrEmpty(this.PrepareDomain.Results.ValueOrDefault) || this.PrepareAllDomains.Results.Value)));
			this.AdUpdateRequired = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.SchemaNotPreparedExtendedRights).Condition((Result<object> x) => new RuleResult(this.PrepareOrganization.Results.Value && !this.HasExtendedRightsCreateChildPerms.Results.Value && !this.GlobalUpdateRequired.Results.Value));
			this.SchemaUpdateRequired = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Error<RuleBuilder<object>>().Message((Result x) => Strings.SchemaNotPrepared).Condition((Result<object> x) => new RuleResult(this.PrepareSchema.Results.Value && (!this.SchemaAdmin.Results.Value || !this.EnterpriseAdmin.Results.Value)));
			this.GlobalUpdateRequired = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.ServerNotPrepared).Condition((Result<object> x) => new RuleResult((this.PrepareOrganization.Results.Value || this.PrepareAllDomains.Results.Value) && !this.EnterpriseAdmin.Results.Value));
			this.SchemaFSMONotWin2003SPn = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.OSMinVersionForFSMONotMet).Condition((Result<object> x) => new RuleResult(this.Win2003FSMOSchemaServer.Results.Value && !this.SmoSchemaServicePack.Results.Value));
			this.NotInSchemaMasterDomain = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.ServerNotInSchemaMasterDomain(this.SmoSchemaDomain.Results.Value)).Condition((Result<object> x) => new RuleResult((this.PrepareSchema.Results.Value || this.PrepareOrganization.Results.Value) && !string.Equals(this.ComputerDomainDN.Results.Value, this.SmoSchemaDomain.Results.Value, StringComparison.CurrentCultureIgnoreCase) && !string.IsNullOrEmpty(this.ComputerDomainDN.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.SmoSchemaDomain.Results.ValueOrDefault)));
			this.NotInSchemaMasterSite = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.ServerNotInSchemaMasterSite(this.SMOSchemaSiteName.Results.Value)).Condition((Result<object> x) => new RuleResult((this.PrepareSchema.Results.Value || this.PrepareOrganization.Results.Value) && !string.Equals(this.SiteName.Results.Value, this.SMOSchemaSiteName.Results.Value, StringComparison.CurrentCultureIgnoreCase) && !string.IsNullOrEmpty(this.SiteName.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.SMOSchemaSiteName.Results.ValueOrDefault)));
			this.LocalDomainPrep = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.LocalDomainNotPrepared).Condition((Result<object> x) => new RuleResult(this.PrepareDomain.Results.Value == "F63C3A12-7852-4654-B208-125C32EB409A" && (!this.LocalDomainAdmin.Results.Value || !this.HasExchangeServersUSGBasicAccess.Results.Value) && !this.EnterpriseAdmin.Results.Value));
			this.NoE12ServerWarning = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Warning<RuleBuilder<object>>().Message((Result x) => Strings.NoE12ServerWarning).Condition((Result<object> x) => new RuleResult(this.PrepareOrganization.Results.Value && this.E12ServerInTopology.Results.Value.Count<char>() == 0 && this.E15ServerInTopology.Results.Value.Count<char>() == 0));
			this.NoE14ServerWarning = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Warning<RuleBuilder<object>>().Message((Result x) => Strings.NoE14ServerWarning).Condition((Result<object> x) => new RuleResult(this.PrepareOrganization.Results.Value && this.E14ServerInTopology.Results.Value.Count<char>() == 0 && this.E15ServerInTopology.Results.Value.Count<char>() == 0));
			this.DelegatedMailboxFirstInstall = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.DelegatedMailboxFirstInstall).Condition((Result<object> x) => new RuleResult(this.MailboxRoleOfEqualOrHigherVersion.Results.Count<Result<string>>() == 0 && !this.ExOrgAdmin.Results.Value && !this.EnterpriseAdmin.Results.Value));
			this.DelegatedBridgeheadFirstInstall = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Bridgehead).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.DelegatedBridgeheadFirstInstall).Condition((Result<object> x) => new RuleResult(this.BridgeheadRoleOfEqualOrHigherVersion.Results.Count<Result<string>>() == 0 && !this.ExOrgAdmin.Results.Value && !this.EnterpriseAdmin.Results.Value));
			this.DelegatedClientAccessFirstInstall = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.DelegatedClientAccessFirstInstall).Condition((Result<object> x) => new RuleResult(this.ClientAccessRoleOfEqualOrHigherVersion.Results.Count<Result<string>>() == 0 && !this.ExOrgAdmin.Results.Value && !this.EnterpriseAdmin.Results.Value));
			this.DelegatedUnifiedMessagingFirstInstall = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.UnifiedMessaging).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.DelegatedUnifiedMessagingFirstInstall).Condition((Result<object> x) => new RuleResult(this.UnifiedMessagingRoleOfEqualOrHigherVersion.Results.Count<Result<string>>() == 0 && !this.ExOrgAdmin.Results.Value && !this.EnterpriseAdmin.Results.Value));
			this.DelegatedCafeFirstInstall = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.DelegatedCafeFirstInstall).Condition((Result<object> x) => new RuleResult(this.CafeRoleOfEqualOrHigherVersion.Results.Count<Result<string>>() == 0 && !this.ExOrgAdmin.Results.Value && !this.EnterpriseAdmin.Results.Value));
			this.DelegatedFrontendTransportFirstInstall = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.DelegatedCafeFirstInstall).Condition((Result<object> x) => new RuleResult(this.FrontendTransportRoleOfEqualOrHigherVersion.Results.Count<Result<string>>() == 0 && !this.ExOrgAdmin.Results.Value && !this.EnterpriseAdmin.Results.Value));
			this.CannotUninstallClusterNode = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.Uninstall).Error<RuleBuilder<object>>().Message((Result x) => Strings.CannotUninstallClusterNode).Condition((Result<object> x) => new RuleResult(this.ClusSvcStartMode.Results.ValueOrDefault == 2));
			this.CannotUninstallOABServer = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.Uninstall).Error<RuleBuilder<object>>().Message((Result x) => Strings.CannotUninstallOABServer).Condition((Result<object> x) => new RuleResult(this.OabDN.Results.Value.Count<char>() > 0 && this.OtherPotentialOABServers.Results.Count<Result<string>>() > 1));
			this.CannotAccessAD = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.CannotAccessAD).Condition((Result<object> x) => new RuleResult(string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault)));
			this.DelegatedBridgeheadFirstSP1upgrade = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Bridgehead).Mode(SetupMode.Upgrade).Error<RuleBuilder<object>>().Message((Result x) => Strings.DelegatedBridgeheadFirstSP1upgrade).Condition((Result<object> x) => new RuleResult(this.E12SP1orHigher.Results.Value && !this.ExOrgAdmin.Results.Value && this.E12SP1orHigherHubAlreadyExist.Results.Value));
			this.DelegatedUnifiedMessagingFirstSP1upgrade = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.UnifiedMessaging).Mode(SetupMode.Upgrade).Error<RuleBuilder<object>>().Message((Result x) => Strings.DelegatedUnifiedMessagingFirstSP1upgrade).Condition((Result<object> x) => new RuleResult(this.E12SP1orHigher.Results.Value && !this.ExOrgAdmin.Results.Value && this.E12SP1orHigherUMAlreadyExists.Results.Value));
			this.DelegatedClientAccessFirstSP1upgrade = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess).Mode(SetupMode.Upgrade).Error<RuleBuilder<object>>().Message((Result x) => Strings.DelegatedClientAccessFirstSP1upgrade).Condition((Result<object> x) => new RuleResult(this.E12SP1orHigher.Results.Value && !this.ExOrgAdmin.Results.Value && this.E12SP1orHigherCASAlreadyExists.Results.Value));
			this.DelegatedMailboxFirstSP1upgrade = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.Upgrade).Error<RuleBuilder<object>>().Message((Result x) => Strings.DelegatedMailboxFirstSP1upgrade).Condition((Result<object> x) => new RuleResult(this.E12SP1orHigher.Results.Value && !this.ExOrgAdmin.Results.Value && this.E12SP1orHigherMBXAlreadyExists.Results.Value));
			this.AdcFound = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.AdcFound).Condition((Result<object> x) => new RuleResult(this.AdcServer.Results.Count<Result<string>>() > 0));
			this.ProvisionedUpdateRequired = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.ProvisionedUpdateRequired).Condition((Result<object> x) => new RuleResult(!string.IsNullOrEmpty(this.NewProvisionedServerName.Results.ValueOrDefault) && string.IsNullOrEmpty(this.ExOrgAdminAccountName.Results.ValueOrDefault)));
			this.GlobalServerInstall = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.GlobalServerInstall).Condition((Result<object> x) => new RuleResult(!this.ExOrgAdmin.Results.Value && !this.EnterpriseAdmin.Results.Value && (!this.ServerAlreadyExists.Results.Value || (this.HasServerDelegatedPermsBlocked.Results.Count<Result<uint>>() != 0 && !this.ServerManagement.Results.Value))));
			this.NoConnectorToStar = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Bridgehead).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Warning<RuleBuilder<object>>().Message((Result x) => Strings.NoConnectorToStar).Condition((Result<object> x) => new RuleResult(this.E15orHigherHubAlreadyExists.Results.Value && string.IsNullOrEmpty(this.ConnectorToStar.Results.ValueOrDefault)));
			this.DuplicateShortProvisionedName = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.DuplicateShortProvisionedName(this.NewProvisionedServerName.Results.Value)).Condition(delegate(Result<object> x)
			{
				bool value = false;
				if (!string.IsNullOrEmpty(this.NewProvisionedServerName.Results.ValueOrDefault))
				{
					value = (this.ShortProvisionedName.Results.Count<Result<string>>() > 1);
				}
				return new RuleResult(value);
			});
			this.ForestLevelNotWin2003Native = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.ForestLevelNotWin2003Native).Condition((Result<object> x) => new RuleResult(this.MsDSBehaviorVersion.Results.Value < 2));
			this.ServerFQDNMatchesSMTPPolicy = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.InvalidLocalComputerFQDN(this.RecipientPolicyName.Results.Value)).Condition((Result<object> x) => new RuleResult(string.Equals(this.GatewayProxy.Results.Value, this.ComputerNameDnsFullyQualified.Results.Value, StringComparison.CurrentCultureIgnoreCase)));
			this.SmtpAddressLiteral = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Bridgehead | SetupRole.Global).Mode(SetupMode.Install).Error<RuleBuilder<object>>().Message((Result x) => Strings.NotSupportedRecipientPolicyAddressFormatValidator(this.RecipientPolicyName.Results.Value, "^\\[\\d+\\.\\d+\\.\\d+\\.\\d+\\]$")).Condition((Result<object> x) => new RuleResult(AnalysisHelpers.Match("^\\[\\d+\\.\\d+\\.\\d+\\.\\d+\\]$", new string[]
			{
				this.GatewayProxy.Results.Value
			})));
			this.InhBlockPublicFolderTree = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.Install).Error<RuleBuilder<object>>().Message((Result x) => Strings.InhBlockPublicFolderTree("Public Folder tree", this.MicrosoftExchServicesConfigAdminGroupPublicFoldersDistinguishedName.Results.Value)).Condition((Result<object> x) => new RuleResult((AnalysisHelpers.SdGet(this.MicrosoftExchServicesConfigAdminGroupPublicFoldersNtSecurityDescriptor.Results.Value) & 4096) == 4096));
			this.PrepareDomainNotFound = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.InvalidDomainToPrepare(this.PrepareDomain.Results.Value)).Condition(delegate(Result<object> x)
			{
				if (this.PrepareDomain.Results.Value != "F63C3A12-7852-4654-B208-125C32EB409A" && !string.IsNullOrEmpty(this.PrepareDomain.Results.ValueOrDefault))
				{
					return new RuleResult(string.IsNullOrEmpty(this.PrepareDomainNCName.Results.ValueOrDefault));
				}
				return new RuleResult(false);
			});
			this.PrepareDomainNotAdmin = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.PrepareDomainNotAdmin(this.PrepareDomain.Results.Value)).Condition(delegate(Result<object> x)
			{
				if (this.PrepareDomain.Results.Value != "F63C3A12-7852-4654-B208-125C32EB409A" && !string.IsNullOrEmpty(this.PrepareDomain.Results.ValueOrDefault))
				{
					new RuleResult(!string.IsNullOrEmpty(this.PrepareDomainNCName.Results.ValueOrDefault) && (!this.PrepareDomainAdmin.Results.Value || !this.HasExchangeServersUSGBasicAccess.Results.Value) && !this.EnterpriseAdmin.Results.Value);
				}
				return new RuleResult(false);
			});
			this.PrepareDomainModeMixed = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Global).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.PrepareDomainModeMixed(this.PrepareDomainNCName.Results.Value)).Condition((Result<object> x) => new RuleResult(this.NtMixedDomainPrepareDomain.Results.Value == 1));
			this.RusMissing = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Global).Mode(SetupMode.All).Warning<RuleBuilder<object>>().Message((Result x) => Strings.RecipientUpdateServiceNotAvailable(this.DomainNCName.Results.Value)).Condition(delegate(Result<object> x)
			{
				bool value;
				if (string.IsNullOrEmpty(this.RusName.Results.ValueOrDefault))
				{
					value = this.Exchange200x.Results.Any((Result<bool> w) => w.Value);
				}
				else
				{
					value = false;
				}
				return new RuleResult(value);
			});
			this.UnwillingToRemoveMailboxDatabase = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.Uninstall).Error<RuleBuilder<object>>().Message((Result x) => Strings.UnwillingToRemoveMailboxDatabase(string.Join(Environment.NewLine, this.RemoveMailboxDatabaseException.Results.Value))).Condition((Result<object> x) => new RuleResult(this.RemoveMailboxDatabaseException.Results.Value.Count > 0));
			this.ExchangeVersionBlock = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.ExchangeVersionBlock).Condition((Result<object> x) => new RuleResult((double)this.ExchangeVersionPrefix.Results.Value >= 3.0));
			this.RootDomainModeMixed = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Global).Error<RuleBuilder<object>>().Message((Result x) => Strings.PrepareDomainModeMixed(this.RootNamingContext.Results.Value)).Condition((Result<object> x) => new RuleResult(this.NtMixedDomainRoot.Results.Value == 1));
			this.ServerRemoveProvisioningCheck = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Uninstall).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.ServerRemoveProvisioningCheck(this.RemoveServerName.Results.Value)).Condition((Result<object> x) => new RuleResult((this.ExchCurrentServerRoles.Results.Value & 4096) == 4096 && !string.IsNullOrEmpty(this.RemoveServerName.Results.ValueOrDefault)));
			this.InconsistentlyConfiguredDomain = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.Global).Error<RuleBuilder<object>>().Message((Result x) => Strings.InconsistentlyConfiguredDomain(this.ExchNonAuthoritativeDomains.Results.Value)).Condition(delegate(Result<object> x)
			{
				bool value = false;
				if (this.NonAuthoritativeDomainsArray.Results.Value.Count<string>() > 0 && (this.DisabledGatewayProxyArray.Results.Value.Count<string>() > 0 || this.EnabledGatewayProxyArray.Results.Value.Count<string>() > 0))
				{
					string[] array = this.ExchNonAuthoritativeDomains.Results.Value.Split(new char[]
					{
						' '
					});
					foreach (string value2 in array)
					{
						if (this.EnabledGatewayProxyArray.Results.Value.Contains(value2))
						{
							value = true;
							break;
						}
						if (this.DisabledGatewayProxyArray.Results.Value.Contains(value2))
						{
							value = true;
							break;
						}
					}
				}
				return new RuleResult(value);
			});
			this.OffLineABServerDeleted = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.ClientAccess | SetupRole.Cafe).Error<RuleBuilder<object>>().Message((Result x) => Strings.OffLineABServerDeleted(this.OabName.Results.Value)).Condition((Result<object> x) => new RuleResult(this.OffLineABServer.Results.Any((Result<string> w) => w.Value.Contains("DEL:"))));
			this.SiteCanonicalName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Bridgehead).Mode(SetupMode.Uninstall).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.SiteName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn={1},cn=Sites, {2}", this.GlobalCatalog.Results.Value, this.SiteName.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"canonicalName"
					}, null, SearchScope.Base);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["canonicalName"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								return new Result<string>(obj2.ToString());
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.HubTransportRoleInCurrentADSite = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Bridgehead).Mode(SetupMode.Uninstall).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.SiteName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups,{1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, string.Format("(&(objectClass=msExchExchangeServer)(msExchServerSite=cn={0},cn=Sites,{1})(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=32))", this.SiteName.Results.Value, this.ConfigurationNamingContext.Results.Value), SearchScope.Subtree);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								return new Result<string>(obj2.ToString());
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.E12ServersNotMinVersionRequirement = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				string text = string.Empty;
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					int num = new ServerVersion(ValidationConstant.E12MinCoExistVersionNumber.Major, ValidationConstant.E12MinCoExistVersionNumber.Minor, ValidationConstant.E12MinCoExistVersionNumber.Build, ValidationConstant.E12MinCoExistVersionNumber.Revision).ToInt();
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, string.Format("(&(objectClass=msExchExchangeServer)(!msExchCurrentServerRoles=0)(&(serialNumber=Version 8.*)(!(versionnumber>={0}))))", num), SearchScope.OneLevel);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
						foreach (object obj2 in resultPropertyValueCollection)
						{
							text += (string.IsNullOrEmpty(text) ? obj2.ToString() : string.Format(", {0}", obj2.ToString()));
						}
					}
				}
				return new Result<string>(text);
			});
			this.E14ServersNotMinVersionRequirement = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				string text = string.Empty;
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					int num = new ServerVersion(ValidationConstant.E14MinCoExistVersionNumber.Major, ValidationConstant.E14MinCoExistVersionNumber.Minor, ValidationConstant.E14MinCoExistVersionNumber.Build, ValidationConstant.E14MinCoExistVersionNumber.Revision).ToInt();
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, string.Format("(&(objectClass=msExchExchangeServer)(!msExchCurrentServerRoles=0)(&(serialNumber=Version 14.*)(!(versionnumber>={0}))))", num), SearchScope.OneLevel);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
						foreach (object obj2 in resultPropertyValueCollection)
						{
							text += (string.IsNullOrEmpty(text) ? obj2.ToString() : string.Format(", {0}", obj2.ToString()));
						}
					}
				}
				return new Result<string>(text);
			});
			this.E14ServersNotMinMajorVersionRequirement = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				string text = string.Empty;
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					int num = new ServerVersion(ValidationConstant.E14MinCoExistMajorVersionNumber.Major, ValidationConstant.E14MinCoExistMajorVersionNumber.Minor, ValidationConstant.E14MinCoExistMajorVersionNumber.Build, ValidationConstant.E14MinCoExistMajorVersionNumber.Revision).ToInt();
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, string.Format("(&(objectClass=msExchExchangeServer)(!msExchCurrentServerRoles=0)(&(serialNumber=Version 14.*)(!(versionnumber>={0}))))", num), SearchScope.OneLevel);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
						foreach (object obj2 in resultPropertyValueCollection)
						{
							text += (string.IsNullOrEmpty(text) ? obj2.ToString() : string.Format(", {0}", obj2.ToString()));
						}
					}
				}
				return new Result<string>(text);
			});
			this.E15E12CoexistenceMinVersionRequirement = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.E15E12CoexistenceMinOSReqFailure(this.E12ServersNotMinVersionRequirement.Results.Value)).Condition((Result<object> x) => new RuleResult(!string.IsNullOrEmpty(this.E12ServersNotMinVersionRequirement.Results.ValueOrDefault)));
			this.E15E14CoexistenceMinVersionRequirementForDC = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).Warning<RuleBuilder<object>>().Message((Result x) => Strings.E15E14CoexistenceMinOSReqFailureInDC(this.E14ServersNotMinVersionRequirement.Results.Value)).Condition(delegate(Result<object> x)
			{
				bool value = false;
				if (this.globalParameters.IsDatacenter && !string.IsNullOrEmpty(this.E14ServersNotMinVersionRequirement.Results.ValueOrDefault))
				{
					value = true;
				}
				return new RuleResult(value);
			});
			this.E15E14CoexistenceMinVersionRequirement = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).Warning<RuleBuilder<object>>().Message((Result x) => Strings.E15E14CoexistenceMinOSReqFailure(this.E14ServersNotMinVersionRequirement.Results.Value)).Condition(delegate(Result<object> x)
			{
				bool value = false;
				if (!this.globalParameters.IsDatacenter && !string.IsNullOrEmpty(this.E14ServersNotMinVersionRequirement.Results.ValueOrDefault))
				{
					value = true;
				}
				return new RuleResult(value);
			});
			this.E15E14CoexistenceMinMajorVersionRequirement = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.E15E14CoexistenceMinOSReqFailure(this.E14ServersNotMinMajorVersionRequirement.Results.Value)).Condition(delegate(Result<object> x)
			{
				bool value = false;
				if (!this.globalParameters.IsDatacenter && !string.IsNullOrEmpty(this.E14ServersNotMinMajorVersionRequirement.Results.ValueOrDefault))
				{
					value = true;
				}
				return new RuleResult(value);
			});
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x00018334 File Offset: 0x00016534
		private void CreateGlobalParameterPrereqProperties()
		{
			this.TargetDir = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(this.globalParameters.TargetDir));
			this.ExchangeVersion = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<Version>(this.globalParameters.ExchangeVersion));
			this.VersionNumber = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				Version value = this.ExchangeVersion.Results.Value;
				return new Result<int>(new ServerVersion(value.Major, value.Minor, value.Build, value.Revision).ToInt());
			});
			this.AdamPort = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int>(this.globalParameters.AdamPort));
			this.AdamSSLPort = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int>(this.globalParameters.AdamSSLPort));
			this.CreatePublicDB = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<bool>(this.globalParameters.CreatePublicDB));
			this.CustomerFeedbackEnabled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<bool>(this.globalParameters.CustomerFeedbackEnabled));
			this.NewProvisionedServerName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(this.globalParameters.NewProvisionedServerName));
			this.RemoveProvisionedServerName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(this.globalParameters.RemoveProvisionedServerName));
			this.GlobalCatalog = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(this.globalParameters.GlobalCatalog));
			this.DomainController = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(this.globalParameters.DomainController));
			this.PrepareDomain = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(this.globalParameters.PrepareDomain ?? "F63C3A12-7852-4654-B208-125C32EB409A"));
			this.PrepareOrganization = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<bool>(this.globalParameters.PrepareOrganization));
			this.PrepareSchema = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<bool>(this.globalParameters.PrepareSchema));
			this.PrepareAllDomains = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<bool>(this.globalParameters.PrepareAllDomains));
			this.AdInitError = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(this.globalParameters.AdInitError));
			this.LanguagePackDir = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(this.globalParameters.LanguagePackDir));
			this.LanguagesAvailableToInstall = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<bool>(this.globalParameters.LanguagesAvailableToInstall));
			this.SufficientLanguagePackDiskSpace = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<bool>(this.globalParameters.SufficientLanguagePackDiskSpace));
			this.LanguagePacksInstalled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<bool>(this.globalParameters.LanguagePacksInstalled));
			this.AlreadyInstalledUMLanguages = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(this.globalParameters.AlreadyInstalledUMLanguages));
			this.LanguagePackVersioning = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<bool>(this.globalParameters.LanguagePackVersioning));
			this.ActiveDirectorySplitPermissions = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<bool>(this.globalParameters.ActiveDirectorySplitPermissions));
			this.SetupRoles = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetMultipleValues((Result<object> x) => from w in this.globalParameters.SetupRoles
			select new Result<string>(w));
			this.IgnoreFileInUse = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<bool>(this.globalParameters.IgnoreFileInUse));
			this.RemoveServerName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<string>(AnalysisHelpers.Replace(this.RemoveProvisionedServerName.Results.Value, "^(.*?)\\..*$", "$1")));
			this.AdInitErrorRule = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => this.AdInitError.Results.Value).Condition((Result<object> x) => new RuleResult(!string.IsNullOrEmpty(this.AdInitError.Results.ValueOrDefault)));
		}

		// Token: 0x060006F3 RID: 1779 RVA: 0x00019D5C File Offset: 0x00017F5C
		private void CreateMonadPrereqProperties()
		{
			this.CmdletGetMailboxServerResult = Setting<MailboxServer>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.Mailbox).Mode(SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				MailboxServer value = null;
				object[] array = new object[0];
				try
				{
					array = base.Providers.MonadDataProvider.ExecuteCommand(string.Format("get-MailboxServer -Identity {0}", this.ShortServerName.Results.Value));
				}
				catch (Exception ex)
				{
					if (ex.InnerException.Message.StartsWith("Couldn't find the Enterprise Organization container"))
					{
						return new Result<MailboxServer>(value);
					}
					throw;
				}
				if (array != null && array.Length > 0)
				{
					value = (MailboxServer)array[0];
				}
				return new Result<MailboxServer>(value);
			});
			this.MemberOfDatabaseAvailabilityGroup = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.Mailbox).Mode(SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.MemberOfDatabaseAvailabilityGroup).Condition((Result<object> x) => new RuleResult(this.CmdletGetMailboxServerResult.Results.Value.DatabaseAvailabilityGroup != null));
			this.CmdletGetExchangeServerResult = Setting<ExchangeServer>.Build().AsRootSetting().In(this).AsAsync().Mode(SetupMode.Upgrade | SetupMode.Uninstall | SetupMode.DisasterRecovery).SetValue(delegate(Result<object> x)
			{
				ExchangeServer value = null;
				object[] array = new object[0];
				try
				{
					array = base.Providers.MonadDataProvider.ExecuteCommand(string.Format("get-ExchangeServer -Identity {0}", this.ShortServerName.Results.Value));
				}
				catch (Exception ex)
				{
					if (ex.InnerException.Message.StartsWith("Couldn't find the Enterprise Organization container"))
					{
						return new Result<ExchangeServer>(value);
					}
					throw;
				}
				if (array != null && array.Length > 0)
				{
					value = (ExchangeServer)array[0];
				}
				return new Result<ExchangeServer>(value);
			});
			this.DrMinVersionCheck = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.All).Mode(SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.DRMinVersionNotMet(this.CmdletGetExchangeServerResult.Results.Value.AdminDisplayVersion.ToString())).Condition((Result<object> x) => new RuleResult(this.globalParameters.ExchangeVersion.CompareTo(this.CmdletGetExchangeServerResult.Results.Value.AdminDisplayVersion) < 0));
			this.RemoteRegException = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.All).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.RemoteRegException).Condition(delegate(Result<object> x)
			{
				try
				{
					base.Providers.MonadDataProvider.ExecuteCommand("[Microsoft.Win32.RegistryKey]::OpenRemoteBaseKey([Microsoft.Win32.RegistryHive]::LocalMachine, [System.Net.Dns]::GetHostEntry([System.Net.Dns]::GetHostName()).HostName)");
				}
				catch (Exception)
				{
					return new RuleResult(true);
				}
				return new RuleResult(false);
			});
			this.WinRMIISExtensionInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install).Error<RuleBuilder<object>>().Message((Result x) => Strings.WinRMIISExtensionInstalled).Condition((Result<object> x) => new RuleResult(this.IsWinRMIISExtensionInstalled.Results.Value));
			this.ResourcePropertySchemaException = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.Global).Mode(SetupMode.Upgrade).Error<RuleBuilder<object>>().Message((Result x) => Strings.ResourcePropertySchemaException(x.Exception.Message.ToString())).Condition(delegate(Result<object> x)
			{
				try
				{
					base.Providers.MonadDataProvider.ExecuteCommand("$rsCfg = Get-ResourceConfig; Set-ResourceConfig -ResourcePropertySchema $rsCfg.ResourcePropertySchema -whatif");
				}
				catch (Exception)
				{
					return new RuleResult(true);
				}
				return new RuleResult(false);
			});
			this.CmdletGetQueueResult = Setting<object>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.Bridgehead | SetupRole.Gateway).Mode(SetupMode.Uninstall).SetMultipleValues((Result<object> x) => from w in base.Providers.MonadDataProvider.ExecuteCommand("get-Queue")
			select new Result<object>(w));
			this.MessagesInQueue = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Bridgehead | SetupRole.Gateway).Mode(SetupMode.Uninstall).Warning<RuleBuilder<object>>().Message(delegate(Result x)
			{
				List<string> list = new List<string>();
				foreach (Result<object> result in this.CmdletGetQueueResult.Results)
				{
					if (result.Value != null && Convert.ToInt64(AnalysisHelpers.GetObjectPropertyByName(result.Value, "MessageCount")) > 0L)
					{
						list.Add(Convert.ToString(AnalysisHelpers.GetObjectPropertyByName(result.Value, "Identity")));
					}
				}
				return Strings.MessagesInQueue(string.Join("\", \"", list));
			}).Condition((Result<object> x) => new RuleResult(this.CmdletGetQueueResult.Results.Count((Result<object> w) => w.Value != null && Convert.ToInt64(AnalysisHelpers.GetObjectPropertyByName(w.Value, "MessageCount")) > 0L) > 0));
			this.VoiceMailPath = Setting<string>.Build().AsRootSetting().In(this).AsAsync().Mode(SetupMode.Upgrade).Role(SetupRole.UnifiedMessaging).SetValue((Result<object> x) => new Result<string>(string.Format("{0}\\unifiedmessaging\\voicemail", this.TargetDir.Results.Value)));
			this.VoiceMessages = Setting<int>.Build().AsRootSetting().In(this).AsAsync().Mode(SetupMode.Upgrade).Role(SetupRole.UnifiedMessaging).SetValue(delegate(Result<object> x)
			{
				string value = this.VoiceMailPath.Results.Value;
				if (Directory.Exists(value))
				{
					object[] array = base.Providers.MonadDataProvider.ExecuteCommand(string.Format("get-childitem -name -force '{0}'", value));
					if (array != null)
					{
						return new Result<int>(array.Count<object>());
					}
				}
				return new Result<int>(0);
			});
			this.VoiceMessagesInQueue = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Upgrade).Role(SetupRole.UnifiedMessaging).Error<RuleBuilder<object>>().Message((Result x) => Strings.VoiceMessagesInQueue(this.VoiceMailPath.Results.Value)).Condition((Result<object> x) => new RuleResult(this.VoiceMessages.Results.Value > 0));
			this.RemoteRegistryServiceId = Setting<long>.Build().AsRootSetting().In(this).AsAsync().Mode(SetupMode.Upgrade | SetupMode.Uninstall).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).SetValue(delegate(Result<object> x)
			{
				object[] array = base.Providers.MonadDataProvider.ExecuteCommand("get-process | where {$_.ProcessName -like 'svchost'} | where{$_.Modules | where {$_.ModuleName -like 'regsvc.dll*'}}");
				if (!AnalysisHelpers.IsNullOrEmpty(array))
				{
					return new Result<long>((long)((Process)array[0]).Id);
				}
				return new Result<long>(-1L);
			});
			this.OneCopyAlertProcessId = Setting<long>.Build().AsRootSetting().In(this).AsAsync().Mode(SetupMode.Upgrade | SetupMode.Uninstall).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_Process WHERE Name='powershell.exe' AND CommandLine LIKE '%CheckDatabaseRedundancy%'")[0].TryGetValue("ProcessId", out obj))
				{
					return new Result<long>((long)((ulong)((uint)obj)));
				}
				return new Result<long>(-1L);
			});
			this.OpenProcesses = Setting<Process>.Build().AsRootSetting().In(this).AsAsync().Mode(SetupMode.Upgrade | SetupMode.Uninstall).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).SetMultipleValues(delegate(Result<object> x)
			{
				object[] array = base.Providers.MonadDataProvider.ExecuteCommand(string.Format("get-process | ?{{$_.ProcessName -notmatch '{0}'}} | ?{{Get-ProcessModule -ProcessId $_.Id | ?{{$_ -like '{1}*'}}}}", this.globalParameters.IsDatacenter ? "^(Setup|Exsetup|ExSetupUI|WmiPrvSE|MOM|MonitoringHost|w3wp|msftesql|msftefd|EdgeTransport|mad|store|umservice|UMWorkerProcess|TranscodingService|SESWorker|ExBPA|ExFBA|wsbexchange|hostcontrollerservice|noderunner|parserserver|Microsoft\\.Exchange\\..*|MSExchange.*|fms|scanningprocess|FSCConfigurationServer|updateservice|ScanEngineTest|EngineUpdateLogger|sftracing|ForefrontActiveDirectoryConnector|rundll32|MSMessageTracingClient|wsmprovhost|Microsoft.Office.BigData.DataLoader)$" : "^(Setup|Exsetup|ExSetupUI|WmiPrvSE|MOM|MonitoringHost|w3wp|msftesql|msftefd|EdgeTransport|mad|store|umservice|UMWorkerProcess|TranscodingService|SESWorker|ExBPA|ExFBA|wsbexchange|hostcontrollerservice|noderunner|parserserver|Microsoft\\.Exchange\\..*|MSExchange.*|fms|scanningprocess|FSCConfigurationServer|updateservice|ScanEngineTest|EngineUpdateLogger|sftracing|ForefrontActiveDirectoryConnector|rundll32|MSMessageTracingClient)$", this.TargetDir.Results.Value));
				if (array == null)
				{
					array = new object[0];
				}
				return from w in array
				select new Result<Process>((Process)w);
			});
			this.OpenProcessesOnUpgrade = Setting<Process>.Build().WithParent<Process>(() => this.OpenProcesses).In(this).AsAsync().Mode(SetupMode.Upgrade | SetupMode.Uninstall).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).SetValue(delegate(Result<Process> x)
			{
				if (!this.IgnoreFileInUse.Results.Value && x.ValueOrDefault != null && (long)x.Value.Id != this.RemoteRegistryServiceId.Results.Value && (long)x.Value.Id != this.OneCopyAlertProcessId.Results.Value)
				{
					return new Result<Process>(x.Value);
				}
				return new Result<Process>(null);
			});
			this.ProcessNeedsToBeClosedOnUpgrade = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Upgrade).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message(delegate(Result x)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (Result<Process> result in this.OpenProcessesOnUpgrade.Results)
				{
					if (result.Value != null)
					{
						stringBuilder.Append(result.Value.ProcessName);
						stringBuilder.Append(" (");
						stringBuilder.Append(result.Value.Id);
						stringBuilder.Append("), ");
					}
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder = stringBuilder.Remove(stringBuilder.Length - 2, 2);
				}
				return Strings.ProcessNeedsToBeClosedOnUpgrade(stringBuilder.ToString());
			}).Condition((Result<object> x) => new RuleResult(this.OpenProcessesOnUpgrade.Results.Count((Result<Process> w) => w.Value != null) > 0));
			this.OpenProcessesOnUninstall = Setting<Process>.Build().WithParent<Process>(() => this.OpenProcesses).In(this).AsAsync().Mode(SetupMode.Upgrade | SetupMode.Uninstall).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).SetValue(delegate(Result<Process> x)
			{
				if (x.ValueOrDefault != null && (long)x.Value.Id != this.RemoteRegistryServiceId.Results.Value)
				{
					return new Result<Process>(x.Value);
				}
				return new Result<Process>(null);
			});
			this.ProcessNeedsToBeClosedOnUninstall = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Uninstall).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message(delegate(Result x)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (Result<Process> result in this.OpenProcessesOnUpgrade.Results)
				{
					if (result.Value != null)
					{
						stringBuilder.Append(result.Value.ProcessName);
						stringBuilder.Append(" (");
						stringBuilder.Append(result.Value.Id);
						stringBuilder.Append("), ");
					}
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder = stringBuilder.Remove(stringBuilder.Length - 2, 2);
				}
				return Strings.ProcessNeedsToBeClosedOnUninstall(stringBuilder.ToString());
			}).Condition((Result<object> x) => new RuleResult(this.OpenProcessesOnUninstall.Results.Count((Result<Process> w) => w.Value != null) > 0));
			this.SendConnectorException = Rule.Build().WithParent<ResultPropertyCollection>(() => this.OrgMicrosoftExchServicesConfig).In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.Bridgehead).Error<RuleBuilder<ResultPropertyCollection>>().Message((Result x) => Strings.SendConnectorException).Condition(delegate(Result<ResultPropertyCollection> x)
			{
				if (x.Value != null)
				{
					try
					{
						base.Providers.MonadDataProvider.ExecuteCommand("Get-SendConnector");
					}
					catch (Exception)
					{
						return new RuleResult(true);
					}
				}
				return new RuleResult(false);
			});
			this.MailboxLogDriveNotExistList = Setting<List<string>>.Build().AsRootSetting().In(this).AsAsync().Mode(SetupMode.DisasterRecovery).Role(SetupRole.Mailbox).SetValue(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				foreach (string text in this.PrivateDatabaseLogDrive.Results.Value)
				{
					object[] array = base.Providers.MonadDataProvider.ExecuteCommand(string.Format("test-path '{0}'", Path.GetPathRoot(text)));
					if (!AnalysisHelpers.IsNullOrEmpty(array) && !(bool)array[0])
					{
						list.Add(text);
					}
				}
				return new Result<List<string>>(list);
			});
			this.MailboxLogDriveDoesNotExist = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.DisasterRecovery).Role(SetupRole.Mailbox).Error<RuleBuilder<object>>().Message((Result x) => Strings.MailboxLogDriveDoesNotExist(string.Join(",", this.MailboxLogDriveNotExistList.Results.Value.ToArray()))).Condition((Result<object> x) => new RuleResult(this.MailboxLogDriveNotExistList.Results.Value.Count > 0));
			this.MailboxEDBDriveNotExistList = Setting<List<string>>.Build().AsRootSetting().In(this).AsAsync().Mode(SetupMode.DisasterRecovery).Role(SetupRole.Mailbox).SetValue(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				foreach (string text in this.PrivateDatabaseEdbDrive.Results.Value)
				{
					object[] array = base.Providers.MonadDataProvider.ExecuteCommand(string.Format("test-path '{0}'", Path.GetPathRoot(text)));
					if (!AnalysisHelpers.IsNullOrEmpty(array) && !(bool)array[0])
					{
						list.Add(text);
					}
				}
				return new Result<List<string>>(list);
			});
			this.MailboxEDBDriveDoesNotExist = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.DisasterRecovery).Role(SetupRole.Mailbox).Error<RuleBuilder<object>>().Message((Result x) => Strings.MailboxEDBDriveDoesNotExist(string.Join(",", this.MailboxLogDriveNotExistList.Results.Value.ToArray()))).Condition((Result<object> x) => new RuleResult(this.MailboxEDBDriveNotExistList.Results.Value.Count > 0));
			this.RemoveMailboxDatabaseException = Setting<List<string>>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.Mailbox).Mode(SetupMode.Uninstall).SetValue(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				foreach (string arg in this.PrivateDatabaseNameDN.Results.Value)
				{
					try
					{
						base.Providers.MonadDataProvider.ExecuteCommand(string.Format("Remove-MailboxDatabase '{0}' -whatif", arg));
					}
					catch (Exception ex)
					{
						list.Add(ex.Message.ToString());
					}
				}
				return new Result<List<string>>(list);
			});
			this.CmdletGetUMServerResult = Setting<UMServer>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.UnifiedMessaging).Mode(SetupMode.Upgrade).SetValue(delegate(Result<object> x)
			{
				UMServer value = null;
				if (!string.IsNullOrEmpty(this.ShortServerName.Results.ValueOrDefault))
				{
					object[] array = base.Providers.MonadDataProvider.ExecuteCommand(string.Format("Get-UMService -Identity {0}", this.ShortServerName.Results.Value));
					if (array != null && array.Length > 0)
					{
						value = (UMServer)array[0];
					}
				}
				return new Result<UMServer>(value);
			});
			this.LanguageInUMServer = Setting<string>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.UnifiedMessaging).Mode(SetupMode.Upgrade).SetMultipleValues((Result<object> x) => from w in this.CmdletGetUMServerResult.Results.Value.Languages
			where w.Culture.Name.ToUpper() != "EN-US"
			select new Result<string>(w.Culture.Name));
			this.AdditionalUMLangPackExists = Rule.Build().WithParent<string>(() => this.LanguageInUMServer).In(this).AsSync().Role(SetupRole.UnifiedMessaging).Mode(SetupMode.Upgrade).Error<RuleBuilder<string>>().Message((Result x) => Strings.AdditionalUMLangPackExists(x.AncestorOfType<string>(this.LanguageInUMServer).Value)).Condition((Result<string> x) => new RuleResult(true));
			this.SendConnector = Setting<object>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Uninstall).Role(SetupRole.Bridgehead).SetMultipleValues(delegate(Result<object> x)
			{
				object[] array = null;
				if (!string.IsNullOrEmpty(this.ShortServerName.Results.ValueOrDefault))
				{
					array = base.Providers.MonadDataProvider.ExecuteCommand(string.Format("Get-SendConnector | where {{$_.SourceTransportServers -match '^{0}$'}}", this.ShortServerName.Results.Value));
				}
				if (array == null)
				{
					array = new object[0];
				}
				return from w in array
				select new Result<object>((Process)w);
			});
			this.GroupDN = Setting<object>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Uninstall).Role(SetupRole.Bridgehead).SetMultipleValues(delegate(Result<object> x)
			{
				object[] array = null;
				if (!string.IsNullOrEmpty(this.PrereqServerLegacyDN.Results.ValueOrDefault))
				{
					array = base.Providers.MonadDataProvider.ExecuteCommand(string.Format("Get-DistributionGroup | where {{$_.ExpansionServer -eq '{0}'}}", this.PrereqServerLegacyDN.Results.Value));
				}
				if (array == null)
				{
					array = new object[0];
				}
				return from w in array
				select new Result<object>((Process)w);
			});
			this.DynamicGroupDN = Setting<object>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Uninstall).Role(SetupRole.Bridgehead).SetMultipleValues(delegate(Result<object> x)
			{
				object[] array = null;
				if (!string.IsNullOrEmpty(this.PrereqServerLegacyDN.Results.ValueOrDefault))
				{
					array = base.Providers.MonadDataProvider.ExecuteCommand(string.Format("Get-DynamicDistributionGroup | where {{$_.ExpansionServer -eq '{0}'}}", this.PrereqServerLegacyDN.Results.Value));
				}
				if (array == null)
				{
					array = new object[0];
				}
				return from w in array
				select new Result<object>((Process)w);
			});
			this.ServerIsSourceForSendConnector = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Uninstall).Role(SetupRole.Bridgehead).Error<RuleBuilder<object>>().Message((Result x) => Strings.ServerIsSourceForSendConnector(this.SendConnector.Results.Count<Result<object>>())).Condition((Result<object> x) => new RuleResult(this.SendConnector.Results.Count<Result<object>>() > 0));
			this.ServerIsGroupExpansionServer = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Uninstall).Role(SetupRole.Bridgehead).Error<RuleBuilder<object>>().Message((Result x) => Strings.ServerIsGroupExpansionServer(this.GroupDN.Results.Count<Result<object>>())).Condition((Result<object> x) => new RuleResult(this.GroupDN.Results.Count<Result<object>>() > 0));
			this.ServerIsDynamicGroupExpansionServer = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Uninstall).Role(SetupRole.Bridgehead).Error<RuleBuilder<object>>().Message((Result x) => Strings.ServerIsDynamicGroupExpansionServer(this.DynamicGroupDN.Results.Count<Result<object>>())).Condition((Result<object> x) => new RuleResult(this.DynamicGroupDN.Results.Count<Result<object>>() > 0));
			this.ServerIsLastHubForEdgeSubscription = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Uninstall).Role(SetupRole.Bridgehead).Error<RuleBuilder<object>>().Message((Result x) => Strings.ServerIsLastHubForEdgeSubscription(this.SiteName.Results.Value)).Condition((Result<object> x) => new RuleResult(this.EdgeSubscriptionForSite.Results.Count<Result<object>>() > 0 && !string.IsNullOrEmpty(this.HubTransportRoleInCurrentADSite.Results.ValueOrDefault)));
			this.EdgeSubscriptionExists = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Uninstall).Role(SetupRole.Gateway).Error<RuleBuilder<object>>().Message((Result x) => Strings.EdgeSubscriptionExists).Condition(delegate(Result<object> x)
			{
				bool value = false;
				object[] array = base.Providers.MonadDataProvider.ExecuteCommand("get-EdgeSubscription");
				if (array != null && array.Length > 0 && string.Compare(AnalysisHelpers.GetObjectPropertyByName(array[0], "Identity").ToString(), this.ComputerNameNetBIOS.Results.ValueOrDefault, true) == 0)
				{
					value = true;
				}
				return new RuleResult(value);
			});
			this.EdgeSubscriptionForSite = Setting<object>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Bridgehead).SetMultipleValues(delegate(Result<object> x)
			{
				object[] array = null;
				if (!string.IsNullOrEmpty(this.SiteCanonicalName.Results.ValueOrDefault))
				{
					array = base.Providers.MonadDataProvider.ExecuteCommand(string.Format("get-EdgeSubscription | where {{$_.Site -eq '{0}'}}", this.SiteCanonicalName.Results.Value));
				}
				if (array == null)
				{
					array = new object[0];
				}
				return from w in array
				select new Result<object>((Process)w);
			});
			this.RSATWebServerNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("RSAT-Web-Server")).Condition(delegate(Result<object> x)
			{
				bool value = false;
				if (this.Windows2K8R2Version.Results.ValueOrDefault)
				{
					value = !this.IsRSATWebServerInstalled.Results.Value;
				}
				return new RuleResult(value);
			});
			this.NETFrameworkNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("NET-Framework")).Condition((Result<object> x) => new RuleResult(this.Windows2K8R2Version.Results.ValueOrDefault && !this.IsNETFrameworkInstalled.Results.Value));
			this.NETFramework45FeaturesNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("NET-Framework-45-Features")).Condition((Result<object> x) => new RuleResult((this.Windows8Version.Results.ValueOrDefault || this.Windows8ClientVersion.Results.ValueOrDefault) && !this.IsNETFramework45FeaturesInstalled.Results.Value));
			this.WebNetExt45NotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("Web-Net-Ext45")).Condition((Result<object> x) => new RuleResult(this.Windows8Version.Results.ValueOrDefault && !this.IsWebNetExt45Installed.Results.Value));
			this.WebISAPIExtNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("Web-ISAPI-Ext")).Condition((Result<object> x) => new RuleResult(!this.IsWebISAPIExtInstalled.Results.Value));
			this.WebASPNET45NotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("Web-ASP-NET45")).Condition((Result<object> x) => new RuleResult(this.Windows8Version.Results.ValueOrDefault && !this.IsWebASPNET45Installed.Results.Value));
			this.RPCOverHTTPproxyNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("RPC-over-HTTP-proxy")).Condition((Result<object> x) => new RuleResult(!this.IsRPCOverHTTPproxyInstalled.Results.Value));
			this.ServerGuiMgmtInfraNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("Server-Gui-Mgmt-Infra")).Condition((Result<object> x) => new RuleResult((this.Windows8Version.Results.ValueOrDefault || this.Windows8ClientVersion.Results.ValueOrDefault) && !this.IsServerGuiMgmtInfraInstalled.Results.Value));
			this.WcfHttpActivation45Installed = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("NET-WCF-HTTP-Activation45")).Condition((Result<object> x) => new RuleResult(this.Windows8Version.Results.ValueOrDefault && !this.IsWcfHttpActivation45Installed.Results.Value));
			this.RsatAddsToolsInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.Global).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("RSAT-ADDS-Tools")).Condition((Result<object> x) => new RuleResult((this.PrepareSchema.Results.Value || this.PrepareOrganization.Results.Value) && !this.IsRsatAddsToolsInstalled.Results.Value));
			this.RsatClusteringInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.Mailbox | SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("RSAT-Clustering")).Condition((Result<object> x) => new RuleResult(!this.IsRsatClusteringInstalled.Results.Value));
			this.RsatClusteringMgmtInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.Mailbox | SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("RSAT-Clustering-Mgmt")).Condition((Result<object> x) => new RuleResult(this.Windows8Version.Results.ValueOrDefault && !this.IsRsatClusteringMgmtInstalled.Results.Value));
			this.RsatClusteringPowerShellInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.Mailbox | SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("RSAT-Clustering-PowerShell")).Condition((Result<object> x) => new RuleResult(this.Windows8Version.Results.ValueOrDefault && !this.IsRsatClusteringPowerShellInstalled.Results.Value));
			this.RsatClusteringCmdInterfaceInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Role(SetupRole.Mailbox | SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequiredToInstall("RSAT-Clustering-CmdInterface")).Condition((Result<object> x) => new RuleResult(this.Windows8Version.Results.ValueOrDefault && !this.IsRsatClusteringCmdInterfaceInstalled.Results.Value));
			this.PowerShellExecutionPolicyCheckSet = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.All).Role(SetupRole.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.PowerShellExecutionPolicyCheck).Condition((Result<object> x) => new RuleResult(this.PowerShellExecutionPolicy.Results.Value));
			this.PowerShellExecutionPolicy = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				bool flag = new string[]
				{
					"MachinePolicy",
					"UserPolicy"
				}.All((string a) => !base.Providers.MonadDataProvider.ExecuteCommand(string.Format("Get-ExecutionPolicy -Scope {0}", a)).First<object>().ToString().Equals("Restricted", StringComparison.InvariantCultureIgnoreCase));
				return new Result<bool>(!flag);
			});
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0001B218 File Offset: 0x00019418
		private void CreateNativePrereqProperties()
		{
			this.HasExchangeServersUSGWritePerms = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>((base.Providers.NativeMethodProvider.GetAccessCheck(this.ExchangeServersGroupNtSecurityDescriptor.Results.Value, string.Empty) & PrereqAnalysis.adWritePermissions) == PrereqAnalysis.adWritePermissions));
			this.HasExchangeServersUSGBasicAccess = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				bool value = false;
				uint num = base.Providers.NativeMethodProvider.GetAccessCheck(this.ExchangeServersGroupNtSecurityDescriptor.Results.Value, string.Empty) & PrereqAnalysis.adBasicPermissions;
				if (num == PrereqAnalysis.adBasicPermissions)
				{
					value = true;
				}
				return new Result<bool>(value);
			});
			this.HasExtendedRightsCreateChildPerms = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>((base.Providers.NativeMethodProvider.GetAccessCheck(this.ExtendedRightsNtSecurityDescriptor.Results.Value, string.Empty) & 1U) == 1U));
			this.HasServerDelegatedPermsBlocked = Setting<uint>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetMultipleValues(delegate(Result<object> x)
			{
				List<uint> list = new List<uint>();
				if (!string.IsNullOrEmpty(this.NtSecurityDescriptor.Results.Value))
				{
					uint num = base.Providers.NativeMethodProvider.GetAccessCheck(this.NtSecurityDescriptor.Results.Value, "'0;a8df74a7-c5ea-11d1-bbcb-0080c76670c0'|'0;a8df74b2-c5ea-11d1-bbcb-0080c76670c0'|'0;bf967a8b-0de6-11d0-a285-00aa003049e2'|'0;28630ec1-41d5-11d1-a9c1-0000f80367c1'|'0;031b371a-a981-11d2-a9ff-00c04f8eedd8'|'0;3435244a-a982-11d2-a9ff-00c04f8eedd8'|'0;36145cf4-a982-11d2-a9ff-00c04f8eedd8'|'0;966540a1-75f7-4d27-ace9-3858b5dea688'|'0;9432cae6-b09e-11d2-aa06-00c04f8eedd8'|'0;93da93e4-b09e-11d2-aa06-00c04f8eedd8'|'0;a8df74d1-c5ea-11d1-bbcb-0080c76670c0'|'0;a8df74c5-c5ea-11d1-bbcb-0080c76670c0'|'0;a8df74ce-c5ea-11d1-bbcb-0080c76670c0'|'0;3378ca84-a982-11d2-a9ff-00c04f8eedd8'|'0;33bb8c5c-a982-11d2-a9ff-00c04f8eedd8'|'0;3397c916-a982-11d2-a9ff-00c04f8eedd8'|'0;8ef628c6-b093-11d2-aa06-00c04f8eedd8'|'0;8ef628c6-b093-11d2-aa06-00c04f8eedd8'|'0;93bb9552-b09e-11d2-aa06-00c04f8eedd8'|'0;44601346-776a-46e7-b4a4-2472e1c66806'|'0;20309cbd-0ae3-4876-9114-5738c65f845c'") & PrereqAnalysis.adWritePermissions;
					if (num != PrereqAnalysis.adWritePermissions)
					{
						list.Add(num);
					}
				}
				return from w in list
				select new Result<uint>(w);
			});
			this.EnterpriseAdmin = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>(base.Providers.NativeMethodProvider.TokenMembershipCheck(string.Format("{0}-519", this.SidRootDomain.Results.Value))));
			this.SchemaAdmin = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>(base.Providers.NativeMethodProvider.TokenMembershipCheck(string.Format("{0}-518", this.SidRootDomain.Results.Value))));
			this.LocalDomainAdmin = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>(base.Providers.NativeMethodProvider.TokenMembershipCheck(string.Format("{0}-512", this.LocalDomainNtSecurityDescriptor.Results.Value))));
			this.PrepareDomainAdmin = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>(base.Providers.NativeMethodProvider.TokenMembershipCheck(string.Format("{0}-512", this.SidPrepareDomain.Results.Value))));
			this.SMOSchemaSiteName = Setting<string>.Build().AsRootSetting().In(this).AsAsync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<string>(base.Providers.NativeMethodProvider.GetSiteName(this.DnsHostName.Results.ValueOrDefault)));
			this.ExOrgAdmin = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.SidExOrgAdmins.Results.ValueOrDefault))
				{
					return new Result<bool>(base.Providers.NativeMethodProvider.TokenMembershipCheck(this.SidExOrgAdmins.Results.Value));
				}
				return new Result<bool>(false);
			});
			this.ServerManagement = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.SidServerManagementGroup.Results.ValueOrDefault))
				{
					return new Result<bool>(base.Providers.NativeMethodProvider.TokenMembershipCheck(this.SidServerManagementGroup.Results.Value));
				}
				return new Result<bool>(false);
			});
			this.SiteName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<string>(base.Providers.NativeMethodProvider.GetSiteName(string.Empty)));
			this.DomainControllerSiteName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue((Result<object> x) => new Result<string>(base.Providers.NativeMethodProvider.GetSiteName(this.DomainController.Results.Value)));
			this.DomainControllerIsOutOfSite = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.DomainControllerOutOfSiteValidator(this.DomainController.Results.Value, this.DomainControllerSiteName.Results.Value, this.SiteName.Results.Value)).Condition((Result<object> x) => new RuleResult(!string.Equals(this.SiteName.Results.Value, this.DomainControllerSiteName.Results.Value, StringComparison.CurrentCultureIgnoreCase)));
			this.LocalAdmin = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>(base.Providers.NativeMethodProvider.TokenMembershipCheck("S-1-5-32-544")));
			this.NotLocalAdmin = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.NotLocalAdmin(this.CurrentLogOn.Results.Value)).Condition((Result<object> x) => new RuleResult(!this.LocalAdmin.Results.Value));
			this.IsCoreServer = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<bool>(base.Providers.NativeMethodProvider.IsCoreServer()));
			this.WindowsServer2008CoreServerEdition = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.WindowsServer2008CoreServerEdition).Condition((Result<object> x) => new RuleResult(this.WindowsVersion.Results.Value == "6.1" && this.IsCoreServer.Results.Value));
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0001DD34 File Offset: 0x0001BF34
		private void CreateRegistryPrereqProperties()
		{
			this.ExchangeAlreadyInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.PreviousVersionOfExchangeAlreadyInstalled).Condition(delegate(Result<object> x)
			{
				if (this.NewestBuild.Results.ValueOrDefault != null)
				{
					return new RuleResult(this.NewestBuild.Results.Value < 10000 && this.ServicesPath.Results.ValueOrDefault != null);
				}
				return new RuleResult(false);
			});
			this.NewestBuild = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "NewestBuild")));
			this.ServicesPath = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "Services")));
			this.Roles = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetMultipleValues((Result<object> x) => from w in (string[])base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15", null)
			select new Result<string>(w));
			this.ServerRoleUnpacked = Setting<string>.Build().WithParent<string>(() => this.Roles).In(this).AsSync().Role(SetupRole.All).SetValue((Result<string> x) => new Result<string>(AnalysisHelpers.Replace(x.Value, "(.*Role)", "$1")));
			this.Watermarks = Setting<string>.Build().WithParent<string>(() => this.ServerRoleUnpacked).In(this).AsSync().Role(SetupRole.All).SetValue((Result<string> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + x.Value, "Watermark")));
			this.InstallWatermark = Rule.Build().WithParent<string>(() => this.Watermarks).In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.DisasterRecovery).Error<RuleBuilder<string>>().Message((Result x) => Strings.WatermarkPresent(x.AncestorOfType<string>(this.ServerRoleUnpacked).Value)).Condition(delegate(Result<string> x)
			{
				bool value = false;
				if (x.ValueOrDefault != null)
				{
					value = true;
				}
				return new RuleResult(value);
			});
			this.Actions = Setting<string>.Build().WithParent<string>(() => this.ServerRoleUnpacked).In(this).AsSync().Role(SetupRole.All).SetValue((Result<string> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\" + x.Value, "Action")));
			this.FilteredRoles = Setting<string>.Build().WithParent<string>(() => this.Roles).In(this).AsSync().Role(SetupRole.All).SetValue((Result<string> x) => new Result<string>(AnalysisHelpers.Replace(x.Value, "^.*\\\\(.*)Role$", "$1")));
			this.InterruptedUninstallNotContinued = Rule.Build().WithParent<string>(() => this.ServerRoleUnpacked).In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.Uninstall).Error<RuleBuilder<string>>().Message((Result x) => Strings.InterruptedUninstallNotContinued(x.AncestorOfType<string>(this.ServerRoleUnpacked).Value)).Condition(delegate(Result<string> x)
			{
				if (this.Actions.Results.ValueOrDefault != null)
				{
					bool value;
					if (this.Actions.Results.Value == "Uninstall")
					{
						value = (this.SetupRoles.Results.Count((Result<string> w) => w.Value.Equals(SetupRole.Global.ToString(), StringComparison.InvariantCultureIgnoreCase)) <= 0 && !this.SetupRoles.Results.Value.Contains(this.FilteredRoles.Results.Value));
					}
					else
					{
						value = false;
					}
					return new RuleResult(value);
				}
				return new RuleResult(false);
			});
			this.UcmaRedistVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\UCMA\\{902F4F35-D5DC-4363-8671-D5EF0D26C21D}", "Version")));
			this.SpeechRedist = Setting<string[]>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string[]>((string[])base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Classes\\Installer\\Assemblies\\Global", "Microsoft.Speech,version=\"11.0.0.0\",culture=\"neutral\",publicKeyToken=\"31BF3856AD364E35\",processorArchitecture=\"MSIL\"")));
			this.WindowsVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\Windows NT\\CurrentVersion", "CurrentVersion")));
			this.WindowsBuild = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\Windows NT\\CurrentVersion", "CurrentBuild")));
			this.WindowsSPLevel = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\Windows NT\\CurrentVersion", "CSDVersion")));
			this.WindowsProductName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\Windows NT\\CurrentVersion", "ProductName")));
			this.ADAMVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.Gateway).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\Windows\\CurrentVersion\\ADAM_Shared", "InstalledVersion")));
			this.OldADAMInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Gateway).Mode(SetupMode.Install).Error<RuleBuilder<object>>().Message((Result x) => Strings.OldADAMInstalled).Condition(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ADAMVersion.Results.ValueOrDefault))
				{
					new RuleResult(this.ADAMVersion.Results.Value.StartsWith("1.1.3790") && new Version(this.ADAMVersion.Results.Value).Revision < 2075);
				}
				return new RuleResult(false);
			});
			this.SMTPSvcStartMode = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "System\\CurrentControlSet\\Services\\SMTPSVC", "Start")));
			this.SMTPSvcDisplayName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "System\\CurrentControlSet\\Services\\SMTPSVC", "DisplayName")));
			this.MsiInstallPath = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Setup", "MsiInstallPath")));
			this.IISCommonFiles = Setting<string[]>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string[]>((string[])base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp", null)));
			this.IIS6MetabaseStatus = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "Metabase")));
			this.IIS6ManagementConsoleStatus = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "LegacySnapin")));
			this.IIS7CompressionDynamic = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "HttpCompressionDynamic")));
			this.IIS7CompressionStatic = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "HttpCompressionStatic")));
			this.IIS7ManagedCodeAssemblies = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "ManagedCodeAssemblies")));
			this.WASProcessModel = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "ProcessModel")));
			this.IIS7BasicAuthentication = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "BasicAuthentication")));
			this.IIS7WindowAuthentication = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "WindowsAuthentication")));
			this.IIS7DigestAuthentication = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "DigestAuthentication")));
			this.IIS7NetExt = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "NetFxExtensibility")));
			this.IIS6WMICompatibility = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "WMICompatibility")));
			this.ASPNET = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "ASPNET")));
			this.ISAPIFilter = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "ISAPIFilter")));
			this.ClientCertificateMappingAuthentication = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "ClientCertificateMappingAuthentication")));
			this.DirectoryBrowse = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "DirectoryBrowse")));
			this.HttpErrors = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "HttpErrors")));
			this.HttpLogging = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "HttpLogging")));
			this.HttpRedirect = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "HttpRedirect")));
			this.HttpTracing = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "HttpTracing")));
			this.RequestMonitor = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "RequestMonitor")));
			this.StaticContent = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "StaticContent")));
			this.ManagementService = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "AdminService")));
			this.W3SVCDisabledOrNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.ClientAccess | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.W3SVCDisabledOrNotInstalled).Condition((Result<object> x) => new RuleResult(this.W3SVCStartMode.Results.ValueOrDefault == 0 || this.W3SVCStartMode.Results.ValueOrDefault == 4));
			this.W3SVCStartMode = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int>((int)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "System\\CurrentControlSet\\Services\\W3SVC", "Start")));
			this.ShouldReRunSetupForW3SVC = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.ShouldReRunSetupForW3SVC).Condition((Result<object> x) => new RuleResult(this.W3SVCStartMode.Results.Value != 2));
			this.SMTPSvcInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.SMTPSvcInstalled).Condition((Result<object> x) => new RuleResult(this.SMTPSvcStartMode.Results.ValueOrDefault != null && !this.ExchangeAlreadyInstalled.Results.Value));
			this.ClusSvcInstalledRoleBlock = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.Install).Error<RuleBuilder<object>>().Message((Result x) => Strings.ClusSvcInstalledRoleBlock).Condition((Result<object> x) => new RuleResult(this.ClusSvcStartMode.Results.ValueOrDefault == 2));
			this.ClusSvcStartMode = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int>((int)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "System\\CurrentControlSet\\Services\\ClusSvc", "Start")));
			this.LonghornIIS6MetabaseNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("IIS 6 Metabase Compatibility")).Condition((Result<object> x) => new RuleResult(this.IIS6MetabaseStatus.Results.ValueOrDefault == null || this.IIS6MetabaseStatus.Results.Value == 0));
			this.LonghornIIS6MgmtConsoleNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("IIS 6 Management Console")).Condition((Result<object> x) => new RuleResult(this.IIS6ManagementConsoleStatus.Results.ValueOrDefault == null || this.IIS6ManagementConsoleStatus.Results.Value == 0));
			this.LonghornIIS7HttpCompressionDynamicNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("IIS 7 Dynamic Content Compression")).Condition((Result<object> x) => new RuleResult(this.IIS7CompressionDynamic.Results.ValueOrDefault == null || this.IIS7CompressionDynamic.Results.Value == 0));
			this.LonghornIIS7HttpCompressionStaticNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("IIS 7 Static Content Compression")).Condition((Result<object> x) => new RuleResult(this.IIS7CompressionStatic.Results.ValueOrDefault == null || this.IIS7CompressionStatic.Results.Value == 0));
			this.LonghornWASProcessModelInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("Windows Process Activation Service Process Model")).Condition((Result<object> x) => new RuleResult(this.WASProcessModel.Results.ValueOrDefault == null || this.WASProcessModel.Results.Value == 0));
			this.LonghornIIS7BasicAuthNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("IIS 7 Basic Authentication")).Condition((Result<object> x) => new RuleResult(this.IIS7BasicAuthentication.Results.ValueOrDefault == null || this.IIS7BasicAuthentication.Results.Value == 0));
			this.LonghornIIS7WindowsAuthNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("IIS 7 Windows Authentication")).Condition((Result<object> x) => new RuleResult(this.IIS7WindowAuthentication.Results.ValueOrDefault == null || this.IIS7WindowAuthentication.Results.Value == 0));
			this.LonghornIIS7DigestAuthNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("IIS 7 Digest Authentication")).Condition((Result<object> x) => new RuleResult(this.IIS7DigestAuthentication.Results.ValueOrDefault == null || this.IIS7DigestAuthentication.Results.Value == 0));
			this.LonghornIIS7NetExt = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("IIS 7 .NET Extensibility")).Condition((Result<object> x) => new RuleResult(this.WindowsVersion.Results.Value == "6.1" && (this.IIS7NetExt.Results.ValueOrDefault == null || this.IIS7NetExt.Results.Value == 0)));
			this.LonghornIIS6WMICompatibility = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("IIS 6 WMI Compatibility")).Condition((Result<object> x) => new RuleResult(this.IIS6WMICompatibility.Results.ValueOrDefault == null || this.IIS6WMICompatibility.Results.Value == 0));
			this.LonghornASPNET = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("ASP .NET")).Condition((Result<object> x) => new RuleResult(this.WindowsVersion.Results.Value == "6.1" && (this.ASPNET.Results.ValueOrDefault == null || this.ASPNET.Results.Value == 0)));
			this.LonghornISAPIFilter = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("ISAPI Filter")).Condition((Result<object> x) => new RuleResult(this.ISAPIFilter.Results.ValueOrDefault == null || this.ISAPIFilter.Results.Value == 0));
			this.LonghornClientCertificateMappingAuthentication = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("Client Certificate Mapping Authentication")).Condition((Result<object> x) => new RuleResult(this.ClientCertificateMappingAuthentication.Results.ValueOrDefault == null || this.ClientCertificateMappingAuthentication.Results.Value == 0));
			this.LonghornDirectoryBrowse = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("Directory Browsing")).Condition((Result<object> x) => new RuleResult(this.DirectoryBrowse.Results.ValueOrDefault == null || this.DirectoryBrowse.Results.Value == 0));
			this.LonghornHttpErrors = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("HTTP Errors")).Condition((Result<object> x) => new RuleResult(this.HttpErrors.Results.ValueOrDefault == null || this.HttpErrors.Results.Value == 0));
			this.LonghornHttpLogging = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("HTTP Logging")).Condition((Result<object> x) => new RuleResult(this.HttpLogging.Results.ValueOrDefault == null || this.HttpLogging.Results.Value == 0));
			this.LonghornHttpRedirect = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("HTTP Redirection")).Condition((Result<object> x) => new RuleResult(this.HttpRedirect.Results.ValueOrDefault == null || this.HttpRedirect.Results.Value == 0));
			this.LonghornHttpTracing = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("Tracing")).Condition((Result<object> x) => new RuleResult(this.HttpTracing.Results.ValueOrDefault == null || this.HttpTracing.Results.Value == 0));
			this.LonghornRequestMonitor = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("Request Monitor")).Condition((Result<object> x) => new RuleResult(this.RequestMonitor.Results.ValueOrDefault == null || this.RequestMonitor.Results.Value == 0));
			this.LonghornStaticContent = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("Static Content")).Condition((Result<object> x) => new RuleResult(this.StaticContent.Results.ValueOrDefault == null || this.StaticContent.Results.Value == 0));
			this.ManagementServiceInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("Web-Mgmt-Service")).Condition((Result<object> x) => new RuleResult(this.ManagementService.Results.ValueOrDefault == null || this.ManagementService.Results.Value == 0));
			this.ADAMWin7ServerInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Gateway).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ADAMLonghornWin7ServerNotInstalled).Condition((Result<object> x) => new RuleResult(string.IsNullOrEmpty(this.ADAMVersion.Results.ValueOrDefault)));
			string ucmaVersion = "5.0.8308.0";
			this.UcmaRedistMsi = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.UnifiedMessaging | SetupRole.Cafe).Error<RuleBuilder<object>>().Message((Result x) => Strings.UcmaRedistMsi).Condition((Result<object> x) => new RuleResult(this.UcmaRedistVersion.Results.ValueOrDefault == null || AnalysisHelpers.VersionCompare(this.UcmaRedistVersion.Results.Value, ucmaVersion) < 0));
			this.SpeechRedistMsi = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox | SetupRole.UnifiedMessaging | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.SpeechRedistMsi).Condition((Result<object> x) => new RuleResult(AnalysisHelpers.VersionCompare(this.UcmaRedistVersion.Results.Value, ucmaVersion) >= 0 && this.SpeechRedist.Results.ValueOrDefault == null));
			this.Wif35Installed = Setting<string[]>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string[]>((string[])base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Windows Identity Foundation\\Setup\\v3.5", null)));
			this.ClrReleaseNumber = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.Cafe).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\NET Framework Setup\\NDP\\v4\\Full", "Release")));
			this.Win7WindowsIdentityFoundationUpdateNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.Win7WindowsIdentityFoundationUpdateNotInstalled).Condition((Result<object> x) => new RuleResult(this.WindowsVersion.Results.Value == "6.1" && this.Wif35Installed.Results.ValueOrDefault == null));
			this.Win8WindowsIdentityFoundationUpdateNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("Windows-Identity-Foundation")).Condition((Result<object> x) => new RuleResult(this.WindowsVersion.Results.Value == "6.2" && this.Wif35Installed.Results.ValueOrDefault == null));
			this.HttpActivationInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.ClientAccess | SetupRole.Cafe).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("Net-Http-Activation")).Condition((Result<object> x) => new RuleResult(this.WindowsVersion.Results.Value == "6.1" && this.HTTPActivation.Results.ValueOrDefault == null));
			this.MailboxRoleNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.MailboxRoleNotInstalled).Condition((Result<object> x) => new RuleResult(!this.MailboxRoleInstalled.Results.Value && this.ClusSvcStartMode.Results.Value >= 0));
			this.MailboxConfiguredVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\MailBoxRole", "ConfiguredVersion")));
			this.MailboxUnpackedVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\MailBoxRole", "UnpackedVersion")));
			this.MailboxPreviousBuild = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MailboxUnpackedVersion.Results.ValueOrDefault))
				{
					return new Result<bool>(AnalysisHelpers.VersionCompare(this.MailboxUnpackedVersion.Results.Value, this.globalParameters.ExchangeVersion.ToString()) < 0);
				}
				return new Result<bool>(false);
			});
			this.MailboxMinVersionCheck = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.Install).Error<RuleBuilder<object>>().Message((Result x) => Strings.MinVersionCheck).Condition(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MailboxUnpackedVersion.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.MailboxConfiguredVersion.Results.ValueOrDefault))
				{
					return new RuleResult(AnalysisHelpers.VersionCompare(this.MailboxUnpackedVersion.Results.Value, this.globalParameters.ExchangeVersion.ToString()) >= 0 && this.MailboxUnpackedVersion.Results.Value == this.MailboxConfiguredVersion.Results.Value && !this.PreviousBuildDetected.Results.ValueOrDefault);
				}
				return new RuleResult(this.PreviousBuildDetected.Results.ValueOrDefault);
			});
			this.MailboxUpgradeMinVersionBlock = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Mailbox).Mode(SetupMode.Upgrade).Error<RuleBuilder<object>>().Message((Result x) => Strings.UpgradeMinVersionBlock).Condition(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MailboxUnpackedVersion.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.MailboxConfiguredVersion.Results.ValueOrDefault))
				{
					return new RuleResult(AnalysisHelpers.VersionCompare(this.MailboxUnpackedVersion.Results.Value, this.globalParameters.ExchangeVersion.ToString()) > 0 && this.MailboxUnpackedVersion.Results.Value == this.MailboxConfiguredVersion.Results.Value);
				}
				return new RuleResult(false);
			});
			this.UpgradeGateway605Block = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Gateway).Mode(SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.UpgradeGateway605Block).Condition((Result<object> x) => new RuleResult(AnalysisHelpers.VersionCompare(this.GatewayConfiguredVersion.Results.Value, "8.0.606.0") < 0));
			this.UnifiedMessagingConfiguredVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole", "ConfiguredVersion")));
			this.UnifiedMessagingUnpackedVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole", "UnpackedVersion")));
			this.UnifiedMessagingPreviousBuild = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.UnifiedMessagingUnpackedVersion.Results.ValueOrDefault))
				{
					return new Result<bool>(AnalysisHelpers.VersionCompare(this.UnifiedMessagingUnpackedVersion.Results.Value, this.globalParameters.ExchangeVersion.ToString()) < 0);
				}
				return new Result<bool>(false);
			});
			this.UnifiedMessagingMinVersionCheck = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.UnifiedMessaging).Mode(SetupMode.Install).Error<RuleBuilder<object>>().Message((Result x) => Strings.MinVersionCheck).Condition(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.UnifiedMessagingUnpackedVersion.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.UnifiedMessagingConfiguredVersion.Results.ValueOrDefault))
				{
					return new RuleResult(AnalysisHelpers.VersionCompare(this.UnifiedMessagingUnpackedVersion.Results.Value, this.globalParameters.ExchangeVersion.ToString()) >= 0 && this.UnifiedMessagingUnpackedVersion.Results.Value == this.UnifiedMessagingConfiguredVersion.Results.Value && !this.PreviousBuildDetected.Results.ValueOrDefault);
				}
				return new RuleResult(this.PreviousBuildDetected.Results.ValueOrDefault);
			});
			this.UnifiedMessagingUpgradeMinVersionBlock = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.UnifiedMessaging).Mode(SetupMode.Upgrade).Error<RuleBuilder<object>>().Message((Result x) => Strings.UpgradeMinVersionBlock).Condition(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.UnifiedMessagingUnpackedVersion.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.UnifiedMessagingConfiguredVersion.Results.ValueOrDefault))
				{
					return new RuleResult(AnalysisHelpers.VersionCompare(this.UnifiedMessagingUnpackedVersion.Results.Value, this.globalParameters.ExchangeVersion.ToString()) > 0 && this.UnifiedMessagingUnpackedVersion.Results.Value == this.UnifiedMessagingConfiguredVersion.Results.Value);
				}
				return new RuleResult(false);
			});
			this.ClientAccessConfiguredVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ClientAccessRole", "ConfiguredVersion")));
			this.BridgeheadConfiguredVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				string value = null;
				try
				{
					value = (string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\BridgeheadRole", "ConfiguredVersion");
				}
				catch (FailureException)
				{
				}
				if (string.IsNullOrEmpty(value))
				{
					value = (string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\HubTransportRole", "ConfiguredVersion");
				}
				return new Result<string>(value);
			});
			this.BridgeheadUnpackedVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				string value = null;
				try
				{
					value = (string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\BridgeheadRole", "UnpackedVersion");
				}
				catch (FailureException)
				{
				}
				if (string.IsNullOrEmpty(value))
				{
					value = (string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\HubTransportRole", "UnpackedVersion");
				}
				return new Result<string>(value);
			});
			this.BridgeheadPreviousBuild = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.BridgeheadUnpackedVersion.Results.ValueOrDefault))
				{
					return new Result<bool>(AnalysisHelpers.VersionCompare(this.BridgeheadUnpackedVersion.Results.Value, this.globalParameters.ExchangeVersion.ToString()) < 0);
				}
				return new Result<bool>(false);
			});
			this.BridgeheadMinVersionCheck = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Bridgehead).Mode(SetupMode.Install).Error<RuleBuilder<object>>().Message((Result x) => Strings.MinVersionCheck).Condition(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.BridgeheadUnpackedVersion.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.BridgeheadConfiguredVersion.Results.ValueOrDefault))
				{
					return new RuleResult(AnalysisHelpers.VersionCompare(this.BridgeheadUnpackedVersion.Results.Value, this.globalParameters.ExchangeVersion.ToString()) >= 0 && this.BridgeheadUnpackedVersion.Results.Value == this.BridgeheadConfiguredVersion.Results.Value && !this.PreviousBuildDetected.Results.ValueOrDefault);
				}
				return new RuleResult(this.PreviousBuildDetected.Results.ValueOrDefault);
			});
			this.BridgeheadUpgradeMinVersionBlock = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Bridgehead).Mode(SetupMode.Upgrade).Error<RuleBuilder<object>>().Message((Result x) => Strings.UpgradeMinVersionBlock).Condition(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.BridgeheadUnpackedVersion.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.BridgeheadConfiguredVersion.Results.ValueOrDefault))
				{
					return new RuleResult(AnalysisHelpers.VersionCompare(this.BridgeheadUnpackedVersion.Results.Value, this.globalParameters.ExchangeVersion.ToString()) > 0 && this.BridgeheadUnpackedVersion.Results.Value == this.BridgeheadConfiguredVersion.Results.Value);
				}
				return new RuleResult(false);
			});
			this.PreviousBuildDetected = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).SetValue((Result<object> x) => new Result<bool>(this.MailboxPreviousBuild.Results.ValueOrDefault || this.UnifiedMessagingPreviousBuild.Results.ValueOrDefault || this.BridgeheadPreviousBuild.Results.ValueOrDefault));
			this.GatewayConfiguredVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				string value = null;
				try
				{
					value = (string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole", "ConfiguredVersion");
				}
				catch (FailureException)
				{
				}
				if (string.IsNullOrEmpty(value))
				{
					value = (string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\GatewayRole", "ConfiguredVersion");
				}
				return new Result<string>(value);
			});
			this.GatewayUnpackedVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				string value = null;
				try
				{
					value = (string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole", "UnpackedVersion");
				}
				catch (FailureException)
				{
				}
				if (string.IsNullOrEmpty(value))
				{
					value = (string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\GatewayRole", "UnpackedVersion");
				}
				return new Result<string>(value);
			});
			this.GatewayMinVersionCheck = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Gateway).Mode(SetupMode.Install).Error<RuleBuilder<object>>().Message((Result x) => Strings.MinVersionCheck).Condition(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.GatewayConfiguredVersion.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.GatewayUnpackedVersion.Results.ValueOrDefault))
				{
					return new RuleResult(AnalysisHelpers.VersionCompare(this.GatewayUnpackedVersion.Results.Value, this.globalParameters.ExchangeVersion.ToString()) >= 0 && this.GatewayUnpackedVersion.Results.Value == this.GatewayConfiguredVersion.Results.Value);
				}
				return new RuleResult(false);
			});
			this.GatewayUpgrade605Block = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Gateway).Mode(SetupMode.Upgrade).Error<RuleBuilder<object>>().Message((Result x) => Strings.GatewayUpgrade605Block).Condition((Result<object> x) => new RuleResult(AnalysisHelpers.VersionCompare(this.GatewayUnpackedVersion.Results.Value, "8.0.605.11") < 0 && AnalysisHelpers.VersionCompare(this.globalParameters.ExchangeVersion.ToString(), "8.0.606.0") >= 0));
			this.GatewayUpgradeMinVersionBlock = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.Gateway).Mode(SetupMode.Upgrade).Error<RuleBuilder<object>>().Message((Result x) => Strings.UpgradeMinVersionBlock).Condition(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.GatewayUnpackedVersion.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.GatewayConfiguredVersion.Results.ValueOrDefault))
				{
					return new RuleResult(AnalysisHelpers.VersionCompare(this.GatewayUnpackedVersion.Results.Value, this.globalParameters.ExchangeVersion.ToString()) > 0 && this.GatewayUnpackedVersion.Results.Value == this.GatewayConfiguredVersion.Results.Value);
				}
				return new RuleResult(false);
			});
			this.AdminToolsInstallation = Setting<string[]>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string[]>((string[])base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\AdminTools", null)));
			this.Exchange2013AnyOnExchange2007Server = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install).Error<RuleBuilder<object>>().Message((Result x) => Strings.Exchange2013AnyOnExchange2007or2010Server).Condition(delegate(Result<object> x)
			{
				string[] second = new string[]
				{
					"AdminTools",
					"HubTransportRole",
					"MailboxRole",
					"UnifiedMessagingRole",
					"ClientAccessRole",
					"Hygiene"
				};
				string[] first = ((string[])base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\Exchange\\v8.0", null)) ?? new string[0];
				return new RuleResult(first.Intersect(second).Any((string r) => !string.IsNullOrEmpty((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, string.Format("{0}\\{1}", "SOFTWARE\\Microsoft\\Exchange\\v8.0", r), "UnpackedVersion"))));
			});
			this.Exchange2013AnyOnExchange2010Server = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install).Error<RuleBuilder<object>>().Message((Result x) => Strings.Exchange2013AnyOnExchange2007or2010Server).Condition(delegate(Result<object> x)
			{
				string[] second = new string[]
				{
					"AdminTools",
					"HubTransportRole",
					"MailboxRole",
					"UnifiedMessagingRole",
					"ClientAccessRole",
					"Hygiene"
				};
				string[] first = ((string[])base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v14", null)) ?? new string[0];
				return new RuleResult(first.Intersect(second).Any((string r) => !string.IsNullOrEmpty((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, string.Format("{0}\\{1}", "SOFTWARE\\Microsoft\\ExchangeServer\\v14", r), "UnpackedVersion"))));
			});
			this.PendingFileRenames = Setting<string[]>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				string[] value = null;
				object registryKeyValue = base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "System\\CurrentControlSet\\Control\\Session Manager", "PendingFileRenameOperations");
				if (registryKeyValue != null)
				{
					string text = registryKeyValue as string;
					if (text == null)
					{
						value = (registryKeyValue as string[]);
					}
					else if (text != string.Empty)
					{
						value = new string[]
						{
							text
						};
					}
				}
				return new Result<string[]>(value);
			});
			this.UpdateNeedsReboot = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).Condition(delegate(Result<object> x)
			{
				string value = string.Empty;
				try
				{
					value = (string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\Updates\\UpdateExeVolatile", "Flags");
					if (!string.IsNullOrEmpty(value) && Convert.ToInt32(value) != 0)
					{
						return new RuleResult(true);
					}
				}
				catch (FailureException)
				{
				}
				return new RuleResult(false);
			});
			this.PendingRebootWindowsComponents = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.All).Role(SetupRole.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.PendingRebootWindowsComponents).Condition(delegate(Result<object> x)
			{
				try
				{
					string[] source = (string[])base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\Windows\\CurrentVersion\\Component Based Servicing", null);
					if (source.Contains("RebootPending"))
					{
						return new RuleResult(true);
					}
				}
				catch (FailureException)
				{
				}
				return new RuleResult(false);
			});
			this.DST2007Enabled = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<bool>(base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones\\Pacific Standard Time", "TZI").ToString().ToUpper() == "E001000000000000C4FFFFFF00000B0000000100020000000000000000000300000002000200000000000000"));
			this.DynamicDSTKey = Setting<string[]>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string[]>((string[])base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\Windows NT\\CurrentVersion\\Time Zones\\Pacific Standard Time\\Dynamic DST", null)));
			this.HTTPActivation = Setting<string[]>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string[]>((string[])base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\NET Framework Setup\\NDP\\v3.0\\Setup\\Windows Communication Foundation\\HTTPActivation", null)));
			this.NNTPSvcStartMode = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "System\\CurrentControlSet\\Services\\NntpSvc", "Start")));
			this.ProgramFilePath = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\Windows\\CurrentVersion", "ProgramFilesDir").ToString()));
			this.FrameworkPath = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\.NETFramework", "InstallRoot").ToString()));
			this.AdamDataPath = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\EdgeTransportRole\\AdamSettings\\MsExchange", "DataFilesPath").ToString()));
			this.ConfigDCHostName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>((string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "System\\CurrentControlSet\\Services\\MSExchange ADAccess\\Instance0", "ConfigDCHostName")));
			this.ConfigDCHostNameMismatch = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.ConfigDCHostNameMismatch(this.globalParameters.DomainController, this.ConfigDCHostName.Results.Value)).Condition((Result<object> x) => new RuleResult(!string.IsNullOrEmpty(this.ConfigDCHostName.Results.ValueOrDefault) && this.ConfigDCHostName.Results.Value.ToString().ToLower() != this.globalParameters.DomainController.ToLower()));
			this.IIS7ManagementConsole = Setting<int?>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).SetValue((Result<object> x) => new Result<int?>((int?)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\InetStp\\Components", "ManagementConsole")));
			this.LonghornIIS7ManagementConsoleInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComponentIsRequired("IIS Management Console")).Condition(delegate(Result<object> x)
			{
				if (this.SetupRoles.Results.Count((Result<string> w) => w.Value.Equals(SetupRole.AdminTools.ToString(), StringComparison.InvariantCultureIgnoreCase)) > 0 || this.Windows8Version.Results.ValueOrDefault)
				{
					return new RuleResult(this.IIS7ManagementConsole.Results.ValueOrDefault == null || this.IIS7ManagementConsole.Results.Value == 0);
				}
				return new RuleResult(false);
			});
			this.WindowsInstallerServiceDisabledOrNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.WindowsInstallerServiceDisabledOrNotInstalled).Condition((Result<object> x) => new RuleResult(this.WindowsInstallerServiceStartMode.Results.ValueOrDefault == 0 || this.WindowsInstallerServiceStartMode.Results.ValueOrDefault == 4));
			this.WindowsInstallerServiceStartMode = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<int>((int)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "System\\CurrentControlSet\\Services\\msiserver", "Start")));
			this.WinRMServiceDisabledOrNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.WinRMDisabledOrNotInstalled).Condition((Result<object> x) => new RuleResult(this.WinRMServiceStartMode.Results.ValueOrDefault == 0 || this.WinRMServiceStartMode.Results.ValueOrDefault == 4));
			this.WinRMServiceStartMode = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).SetValue((Result<object> x) => new Result<int>((int)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "System\\CurrentControlSet\\Services\\winrm", "Start")));
			this.MinimumFrameworkNotInstalled = Rule.Build().AsRootRule().In(this).AsSync().Role(SetupRole.All).Mode(SetupMode.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.MinimumFrameworkNotInstalled).Condition((Result<object> x) => new RuleResult(this.ClrReleaseNumber.Results.ValueOrDefault == null || this.ClrReleaseNumber.Results.ValueOrDefault.Value < 461808));
		}

		// Token: 0x060006F6 RID: 1782 RVA: 0x00023B0C File Offset: 0x00021D0C
		private void CreateWmiPrereqProperties()
		{
			this.W2K8R2PrepareSchemaLdifdeNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.All).Role(SetupRole.Global).Error<RuleBuilder<object>>().Message((Result x) => Strings.W2K8R2PrepareSchemaLdifdeNotInstalled).Condition((Result<object> x) => new RuleResult(!this.PendingRebootWindowsComponents.Results.Value && this.PrepareSchema.Results.Value && this.FileVersionLdifde.Results.Value.CompareTo(new Version()) == 0));
			this.W2K8R2PrepareAdLdifdeNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.All).Role(SetupRole.Global).Error<RuleBuilder<object>>().Message((Result x) => Strings.W2K8R2PrepareAdLdifdeNotInstalled).Condition((Result<object> x) => new RuleResult(!this.PendingRebootWindowsComponents.Results.Value && this.PrepareOrganization.Results.Value && this.FileVersionLdifde.Results.Value.CompareTo(new Version()) == 0));
			this.SiteName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(base.Providers.NativeMethodProvider.GetSiteName(string.Empty)));
			this.LonghornWmspdmoxNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe).Error<RuleBuilder<object>>().Message((Result x) => Strings.InstallViaServerManager("Windows Media Audio Voice Codec")).Condition((Result<object> x) => new RuleResult(!this.PendingRebootWindowsComponents.Results.Value && (this.FileVersionWmspdmod.Results.Value.CompareTo(new Version()) == 0 || (this.FileVersionWmspdmod.Results.Value.ToString(3).Equals("10.00.00", StringComparison.InvariantCultureIgnoreCase) && this.FileVersionWmspdmod.Results.Value.Revision < 3804) || this.FileVersionWmspdmoe.Results.Value.CompareTo(new Version()) == 0 || (this.FileVersionWmspdmoe.Results.Value.ToString(3).Equals("10.00.00", StringComparison.InvariantCultureIgnoreCase) && this.FileVersionWmspdmoe.Results.Value.Revision < 3804))));
			this.Exchange2000or2003PresentInOrg = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.Global).Error<RuleBuilder<object>>().Message((Result x) => Strings.Exchange2000or2003PresentInOrg).Condition((Result<object> x) => new RuleResult(this.Exchange200x.Results.Any((Result<bool> w) => w.Value)));
			this.ExchangeSerialNumber = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetMultipleValues(delegate(Result<object> x)
			{
				List<string> list = new List<string>();
				if (!string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"serialNumber"
					}, "objectClass=msExchExchangeServer", SearchScope.Subtree);
					if (searchResultCollection != null)
					{
						foreach (object obj in searchResultCollection)
						{
							SearchResult searchResult = (SearchResult)obj;
							ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["serialNumber"];
							foreach (object obj2 in resultPropertyValueCollection)
							{
								list.Add(obj2.ToString());
							}
						}
					}
				}
				return from w in list
				select new Result<string>(w);
			});
			this.Exchange12 = Setting<bool>.Build().WithParent<string>(() => this.ExchangeSerialNumber).In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue((Result<string> x) => new Result<bool>(x.Value.Contains("Version 8")));
			this.Exchange200x = Setting<bool>.Build().WithParent<string>(() => this.ExchangeSerialNumber).In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue((Result<string> x) => new Result<bool>(x.Value.Contains("Version 6")));
			this.RebootPending = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.PendingReboot).Condition((Result<object> x) => new RuleResult((this.PendingFileRenames.Results.ValueOrDefault != null && this.PendingFileRenames.Results.ValueOrDefault.Length > 0) || this.UpdateNeedsReboot.Results.Value));
			this.Win7RpcHttpAssocCookieGuidUpdateNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.Mailbox | SetupRole.ClientAccess | SetupRole.Cafe).Error<RuleBuilder<object>>().Message((Result x) => Strings.Win7RpcHttpAssocCookieGuidUpdateNotInstalled).Condition((Result<object> x) => new RuleResult(this.FileVersionRpcHttp.Results.Value.CompareTo(new Version()) != 0 && ((this.WindowsBuild.Results.Value.Equals("7600", StringComparison.InvariantCultureIgnoreCase) && (this.FileVersionRpcHttp.Results.Value.CompareTo(new Version("6.1.7600.21085")) < 0 || this.FileVersionRpcRT4.Results.Value.CompareTo(new Version("6.1.7600.21085")) < 0)) || (this.WindowsBuild.Results.Value.Equals("7601", StringComparison.InvariantCultureIgnoreCase) && (this.FileVersionRpcHttp.Results.Value.CompareTo(new Version("6.1.7601.21855")) < 0 || this.FileVersionRpcRT4.Results.Value.CompareTo(new Version("6.1.7601.21855")) < 0)))));
			this.SearchFoundationAssemblyLoaderKBNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.SearchFoundationAssemblyLoaderKBNotInstalled).Condition((Result<object> x) => new RuleResult(this.FileVersionKernel32.Results.Value.CompareTo(new Version()) != 0 && ((this.WindowsBuild.Results.Value.Equals("7600", StringComparison.InvariantCultureIgnoreCase) && this.FileVersionKernel32.Results.Value.CompareTo(new Version("6.1.7600.16816")) < 0) || (this.WindowsBuild.Results.Value.Equals("7601", StringComparison.InvariantCultureIgnoreCase) && this.FileVersionKernel32.Results.Value.CompareTo(new Version("6.1.7601.17617")) < 0))));
			this.Win2k12UrefsUpdateNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.Mailbox).Warning<RuleBuilder<object>>().Message((Result x) => Strings.Win2k12UrefsUpdateNotInstalled).Condition((Result<object> x) => new RuleResult(this.FileVersionUrefs.Results.Value.CompareTo(new Version()) != 0 && this.WindowsBuild.Results.Value.Equals("9200", StringComparison.InvariantCultureIgnoreCase) && this.FileVersionUrefs.Results.Value.CompareTo(new Version("6.2.9200.20810")) < 0));
			this.Win2k12RefsUpdateNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.Mailbox).Warning<RuleBuilder<object>>().Message((Result x) => Strings.Win2k12RefsUpdateNotInstalled).Condition((Result<object> x) => new RuleResult(this.FileVersionRefs.Results.Value.CompareTo(new Version()) != 0 && this.WindowsBuild.Results.Value.Equals("9200", StringComparison.InvariantCultureIgnoreCase) && this.FileVersionRefs.Results.Value.CompareTo(new Version("6.2.9200.20838")) < 0));
			this.Win2k12RollupUpdateNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.Mailbox).Warning<RuleBuilder<object>>().Message((Result x) => Strings.Win2k12RollupUpdateNotInstalled).Condition((Result<object> x) => new RuleResult(this.FileVersionDiscan.Results.Value.CompareTo(new Version()) != 0 && this.WindowsBuild.Results.Value.Equals("9200", StringComparison.InvariantCultureIgnoreCase) && this.FileVersionDiscan.Results.Value.CompareTo(new Version("6.2.9200.16548")) < 0));
			this.UnifiedMessagingRoleNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.UnifiedMessaging).Error<RuleBuilder<object>>().Message((Result x) => Strings.UnifiedMessagingRoleNotInstalled).Condition((Result<object> x) => new RuleResult(!this.UnifiedMessagingRoleInstalled.Results.Value));
			this.BridgeheadRoleNotInstalled = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.Bridgehead).Error<RuleBuilder<object>>().Message((Result x) => Strings.BridgeheadRoleNotInstalled).Condition((Result<object> x) => new RuleResult(!this.BridgeheadRoleInstalled.Results.Value));
			this.EventSystemStarted = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_Service WHERE Name='EventSystem'")[0].TryGetValue("Started", out obj))
				{
					return new Result<bool>(bool.Parse(obj.ToString()));
				}
				return new Result<bool>(false);
			});
			this.EventSystemStopped = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.Uninstall).Role(SetupRole.ClientAccess | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.EventSystemStopped).Condition((Result<object> x) => new RuleResult(!this.EventSystemStarted.Results.Value));
			this.MailboxRoleAlreadyExists = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install).Role(SetupRole.Mailbox).Error<RuleBuilder<object>>().Message((Result x) => Strings.MailboxRoleAlreadyExists).Condition((Result<object> x) => new RuleResult(this.ServerAlreadyExists.Results.Value && this.MailboxRoleInstalled.Results.Value));
			this.ClientAccessRoleAlreadyExists = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install).Role(SetupRole.ClientAccess).Error<RuleBuilder<object>>().Message((Result x) => Strings.ClientAccessRoleAlreadyExists).Condition((Result<object> x) => new RuleResult(this.ServerAlreadyExists.Results.Value && this.ClientAccessRoleInstalled.Results.Value));
			this.UnifiedMessagingRoleAlreadyExists = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install).Role(SetupRole.UnifiedMessaging).Error<RuleBuilder<object>>().Message((Result x) => Strings.UnifiedMessagingRoleAlreadyExists).Condition((Result<object> x) => new RuleResult(this.ServerAlreadyExists.Results.Value && this.UnifiedMessagingRoleInstalled.Results.Value));
			this.BridgeheadRoleAlreadyExists = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install).Role(SetupRole.Bridgehead).Error<RuleBuilder<object>>().Message((Result x) => Strings.BridgeheadRoleAlreadyExists).Condition((Result<object> x) => new RuleResult(this.ServerAlreadyExists.Results.Value && this.BridgeheadRoleInstalled.Results.Value));
			this.CafeRoleAlreadyExists = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install).Role(SetupRole.Cafe).Error<RuleBuilder<object>>().Message((Result x) => Strings.CafeRoleAlreadyExists).Condition((Result<object> x) => new RuleResult(this.ServerAlreadyExists.Results.Value && this.CafeRoleInstalled.Results.Value));
			this.ServerWinWebEdition = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.OSWebEditionValidator("Exchange 15")).Condition((Result<object> x) => new RuleResult((this.OSProductSuite.Results.Value & 1024U) != 0U));
			this.BridgeheadRoleNotPresentInSite = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install).Role(SetupRole.Mailbox).Warning<RuleBuilder<object>>().Message((Result x) => Strings.BridgeheadRoleNotPresentInSite(this.SiteName.Results.Value)).Condition(delegate(Result<object> x)
			{
				bool value;
				if (string.IsNullOrEmpty(this.BridgeheadRoleInCurrentADSite.Results.ValueOrDefault))
				{
					value = (this.SetupRoles.Results.Count((Result<string> w) => w.Value.Contains("Bridgehead")) == 0);
				}
				else
				{
					value = false;
				}
				return new RuleResult(value);
			});
			this.ClientAccessRoleNotPresentInSite = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install).Role(SetupRole.Mailbox).Warning<RuleBuilder<object>>().Message((Result x) => Strings.ClientAccessRoleNotPresentInSite(this.SiteName.Results.Value)).Condition(delegate(Result<object> x)
			{
				bool value;
				if (string.IsNullOrEmpty(this.ClientAccessRoleInCurrentADSite.Results.ValueOrDefault))
				{
					value = (this.SetupRoles.Results.Count((Result<string> w) => w.Value.Contains("ClientAccess")) == 0);
				}
				else
				{
					value = false;
				}
				return new RuleResult(value);
			});
			this.BridgeheadRoleInCurrentADSite = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.Mailbox).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, string.Format("(&(objectClass=msExchExchangeServer)(msExchServerSite=cn={0},cn=Sites,{1})(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=32))", this.SiteName.Results.Value, this.ConfigurationNamingContext.Results.Value), SearchScope.OneLevel);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								return new Result<string>(obj2.ToString());
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ClientAccessRoleInCurrentADSite = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.Mailbox).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.OrgDistinguishedName.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Servers,cn=Exchange Administrative Group (FYDIBOHF23SPDLT),cn=Administrative Groups, {1}", this.GlobalCatalog.Results.Value, this.OrgDistinguishedName.Results.Value), new string[]
					{
						"cn"
					}, string.Format("(&(objectClass=msExchExchangeServer)(msExchServerSite=cn={0},cn=Sites,{1})(msExchCurrentServerRoles:1.2.840.113556.1.4.803:=4))", this.SiteName.Results.Value, this.ConfigurationNamingContext.Results.Value), SearchScope.OneLevel);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								return new Result<string>(obj2.ToString());
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.WindowsPath = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				string text = (string)base.Providers.RegistryDataProvider.GetRegistryKeyValue(Registry.LocalMachine, "Software\\Microsoft\\Windows NT\\CurrentVersion", "SystemRoot");
				if (!AnalysisHelpers.IsNullOrEmpty(text))
				{
					return new Result<string>(text);
				}
				return new Result<string>(string.Empty);
			});
			this.FileVersionNtoskrnl = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\System32\\ntoskrnl.exe'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionLdifde = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\system32\\ldifde.exe'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					return new Result<Version>(new Version(obj.ToString()));
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionWmspdmod = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\system32\\wmspdmod.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					return new Result<Version>(new Version(obj.ToString()));
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionWmspdmoe = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\system32\\wmspdmoe.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					return new Result<Version>(new Version(obj.ToString()));
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionMSXML6 = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.UnifiedMessaging).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\system32\\msxml6.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					return new Result<Version>(new Version(obj.ToString()));
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionSecProc = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\System32\\secproc.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionRmActivate = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\System32\\RmActivate.exe'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionRpcRT4 = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\System32\\rpcrt4.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionRpcHttp = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\System32\\rpchttp.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionRpcProxy = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\System32\\RpcProxy\\rpcproxy.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionLbService = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\System32\\RpcProxy\\lbservice.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionTCPIPSYS = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\system32\\drivers\\tcpip.sys'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					return new Result<Version>(new Version(obj.ToString()));
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionAdsiis = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Mailbox | SetupRole.ClientAccess).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\system32\\inetsrv\\adsiis.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					return new Result<Version>(new Version(obj.ToString()));
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionIisext = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Mailbox | SetupRole.ClientAccess).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\system32\\inetsrv\\iisext.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					return new Result<Version>(new Version(obj.ToString()));
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionMSCorLib = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.UnifiedMessaging).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name = '{0}v4.0.30319\\mscorlib.dll'", this.FrameworkPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionSystemServiceModel = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\v4.0.30319\\System.ServiceModel.dll'", this.FrameworkPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionSystemWeb = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name = '{0}v4.0.30319\\System.Web.dll'", this.FrameworkPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionSystemWebServices = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}v4.0.30319\\System.Web.Services.dll'", this.FrameworkPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionKernel32 = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\System32\\Kernel32.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionUrefs = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Mailbox).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\System32\\uReFS.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionRefs = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Mailbox).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\System32\\Drivers\\refs.sys'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionDiscan = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Mailbox).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\System32\\discan.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionAtl110 = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Gateway).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\System32\\atl110.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.FileVersionMsvcr120 = Setting<Version>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Mailbox).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM CIM_Datafile WHERE Name='{0}\\System32\\msvcr120.dll'", this.WindowsPath.Results.Value))[0].TryGetValue("Version", out obj))
				{
					string text = AnalysisHelpers.Replace(obj.ToString(), "^(\\d+\\.\\d+\\.\\d+\\.\\d+).*$", "$1");
					if (!string.IsNullOrEmpty(text))
					{
						return new Result<Version>(new Version(text));
					}
				}
				return new Result<Version>(new Version());
			});
			this.MSDTCStarted = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_Service WHERE Name='MSDTC'")[0].TryGetValue("Started", out obj))
				{
					return new Result<bool>(bool.Parse(obj.ToString()));
				}
				return new Result<bool>(false);
			});
			this.MSDTCStopped = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.Uninstall).Role(SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.MSDTCStopped).Condition((Result<object> x) => new RuleResult(!this.MSDTCStarted.Results.Value));
			this.MpsSvcStarted = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_Service WHERE Name='MpsSvc'")[0].TryGetValue("Started", out obj))
				{
					return new Result<bool>(bool.Parse(obj.ToString()));
				}
				return new Result<bool>(false);
			});
			this.MpsSvcStopped = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.All).Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.FrontendTransport).Warning<RuleBuilder<object>>().Message((Result x) => Strings.MpsSvcStopped).Condition((Result<object> x) => new RuleResult(!this.MpsSvcStarted.Results.Value));
			this.ADAMSvcStopped = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade).Role(SetupRole.Gateway).Error<RuleBuilder<object>>().Message((Result x) => Strings.ADAMSvcStopped).Condition(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_Service WHERE Name='ADAM_MSExchange'")[0].TryGetValue("Started", out obj))
				{
					return new RuleResult(!bool.Parse(obj.ToString()));
				}
				return new RuleResult(false);
			});
			this.NetTcpPortSharingStartMode = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_Service WHERE Name='NetTcpPortSharing'")[0].TryGetValue("StartMode", out obj))
				{
					return new Result<string>(obj.ToString());
				}
				return new Result<string>(string.Empty);
			});
			this.NetTcpPortSharingSvcNotAuto = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade).Role(SetupRole.ClientAccess | SetupRole.Cafe | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.NetTcpPortSharingSvcNotAuto).Condition((Result<object> x) => new RuleResult(!this.NetTcpPortSharingStartMode.Results.Value.Equals("Auto", StringComparison.InvariantCultureIgnoreCase)));
			this.AddressWidth = Setting<ushort>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_Processor")[0].TryGetValue("AddressWidth", out obj))
				{
					return new Result<ushort>((ushort)obj);
				}
				return new Result<ushort>(0);
			});
			this.AddressWidth32Bit = Setting<bool>.Build().AsRootSetting().In(this).AsSync().SetValue((Result<object> x) => new Result<bool>(this.AddressWidth.Results.Value == 32));
			this.AddressWidth64Bit = Setting<bool>.Build().AsRootSetting().In(this).AsSync().SetValue((Result<object> x) => new Result<bool>(this.AddressWidth.Results.Value == 64));
			this.NicCaption = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).SetMultipleValues((Result<object> x) => base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_NetworkAdapter WHERE AdapterTypeID='0'").Select(delegate(Dictionary<string, object> w)
			{
				object obj;
				if (w.TryGetValue("Caption", out obj))
				{
					return new Result<string>(obj.ToString());
				}
				return new Result<string>(string.Empty);
			}));
			this.NicConfiguration = Setting<Dictionary<string, object>>.Build().WithParent<string>(() => this.NicCaption).In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).SetValue((Result<string> x) => new Result<Dictionary<string, object>>(base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE Caption='{0}'", x.Value))[0]));
			this.IPAddress = Setting<string>.Build().WithParent<Dictionary<string, object>>(() => this.NicConfiguration).In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).SetMultipleValues(delegate(Result<Dictionary<string, object>> x)
			{
				string[] source = new string[0];
				object obj;
				if (x.Value.TryGetValue("IPAddress", out obj) && obj != null)
				{
					source = (string[])obj;
				}
				return from w in source
				select new Result<string>(w);
			});
			this.IPv4Address = Setting<string>.Build().WithParent<string>(() => this.IPAddress).In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).SetValue((Result<string> x) => new Result<string>(AnalysisHelpers.Match("^(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}).*$", new string[]
			{
				x.Value
			}) ? x.Value : string.Empty));
			this.IPv6Enabled = Setting<bool>.Build().WithParent<string>(() => this.IPAddress).In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).SetValue((Result<string> x) => new Result<bool>(x.Value.Count((char w) => w.Equals(':')) > 0));
			this.DnsAddress = Setting<string>.Build().WithParent<Dictionary<string, object>>(() => this.NicConfiguration).In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).SetMultipleValues(delegate(Result<Dictionary<string, object>> x)
			{
				string[] source = new string[0];
				object obj;
				if (x.Value.TryGetValue("DNSServerSearchOrder", out obj) && obj != null)
				{
					source = AnalysisHelpers.Replace((string[])obj, "^(\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}\\.\\d{1,3}).*$", "$1");
				}
				return from w in source
				select new Result<string>(w);
			});
			this.PrimaryDns = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!AnalysisHelpers.IsNullOrEmpty(this.DnsAddress.Results))
				{
					return new Result<string>(this.DnsAddress.Results[0].Value);
				}
				return new Result<string>(string.Empty);
			});
			this.PrimaryDNSPortAvailable = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				Dictionary<string, List<string>> commands = new Dictionary<string, List<string>>
				{
					{
						"PrimaryDNSPort",
						new List<string>
						{
							"53",
							string.Empty,
							string.Empty
						}
					}
				};
				Dictionary<string, object[]> dictionary = base.Providers.ManagedMethodProvider.PortAvailable(this.PrimaryDns.Results.Value, commands);
				object[] array;
				if (!AnalysisHelpers.IsNullOrEmpty(dictionary) && dictionary.TryGetValue("PrimaryDNSPort", out array))
				{
					return new Result<bool>(array != null && array.Length > 1 && bool.Parse(array[1] as string));
				}
				return new Result<bool>(false);
			});
			this.PrimaryDNSTestFailed = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).Warning<RuleBuilder<object>>().Message((Result x) => Strings.PrimaryDNSTestFailed(this.PrimaryDns.Results.Value)).Condition(delegate(Result<object> x)
			{
				bool value;
				if (!this.PrimaryDNSPortAvailable.Results.Value)
				{
					value = (this.IPv6Enabled.Results.Count((Result<bool> w) => w.Value) <= 0);
				}
				else
				{
					value = false;
				}
				return new RuleResult(value);
			});
			this.HostRecord = Setting<Dictionary<string, object[]>>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.All).SetValue((Result<object> x) => new Result<Dictionary<string, object[]>>(base.Providers.ManagedMethodProvider.CheckDNS(this.PrimaryDns.Results.Value, this.ComputerNameDnsFullyQualified.Results.Value)));
			this.HostRecordMissing = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.Upgrade | SetupMode.DisasterRecovery).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Warning<RuleBuilder<object>>().Message((Result x) => Strings.MissingDNSHostRecord(this.PrimaryDns.Results.Value)).Condition((Result<object> x) => new RuleResult(((string)this.HostRecord.Results.Value["A"].GetValue(3)).Equals(string.Format("DNS Query Result = {0};", this.PrimaryDns.Results.Value), StringComparison.InvariantCultureIgnoreCase)));
			this.DebugVersion = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_OperatingSystem")[0].TryGetValue("Debug", out obj))
				{
					return new Result<bool>(bool.Parse(obj.ToString()));
				}
				return new Result<bool>(false);
			});
			this.OSCheckedBuild = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.All).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Warning<RuleBuilder<object>>().Message((Result x) => Strings.OSCheckedBuild).Condition((Result<object> x) => new RuleResult(this.DebugVersion.Results.Value));
			this.OSProductSuite = Setting<uint>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_OperatingSystem")[0].TryGetValue("OSProductSuite", out obj))
				{
					return new Result<uint>((uint)obj);
				}
				return new Result<uint>(0U);
			});
			this.DomainRole = Setting<ushort>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).SetValue((Result<object> x) => new Result<ushort>((ushort)base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_ComputerSystem")[0]["DomainRole"]));
			this.ComputerNotPartofDomain = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.All).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComputerNotPartofDomain).Condition((Result<object> x) => new RuleResult(this.DomainRole.Results.Value == 0 || this.DomainRole.Results.Value == 2));
			this.LocalComputerIsDomainController = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).SetValue((Result<object> x) => new Result<bool>(this.DomainRole.Results.Value == 4 || this.DomainRole.Results.Value == 5));
			this.WarningInstallExchangeRolesOnDomainController = Rule.Build().AsRootRule().In(this).AsAsync().Mode(SetupMode.Install | SetupMode.DisasterRecovery).Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Warning<RuleBuilder<object>>().Message((Result x) => Strings.InstallExchangeRolesOnDomainController).Condition((Result<object> x) => new RuleResult(this.LocalComputerIsDomainController.Results.Value));
			this.ADSplitPermissionMode = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).SetValue((Result<object> x) => new Result<bool>(!string.IsNullOrEmpty(this.EWPDn.Results.Value) && !string.IsNullOrEmpty(this.ETSDn.Results.Value) && !this.ETSIsMemberOfEWP.Results.Value));
			this.InstallOnDCInADSplitPermissionMode = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.InstallOnDCInADSplitPermissionMode).Condition((Result<object> x) => new RuleResult(this.LocalComputerIsDomainController.Results.Value && (this.ActiveDirectorySplitPermissions.Results.ValueOrDefault || this.ADSplitPermissionMode.Results.ValueOrDefault)));
			this.SetADSplitPermissionWhenExchangeServerRolesOnDC = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.Global).Error<RuleBuilder<object>>().Message((Result x) => Strings.SetADSplitPermissionWhenExchangeServerRolesOnDC).Condition((Result<object> x) => new RuleResult(this.ActiveDirectorySplitPermissions.Results.Value && this.ExchangeServerRolesOnDomainController.Results.Value > 0));
			this.LocalServerName = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_ComputerSystem")[0].TryGetValue("Name", out obj))
				{
					return new Result<string>(obj.ToString());
				}
				return new Result<string>(string.Empty);
			});
			this.ServerNameNotValid = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.InvalidDNSDomainName).Condition((Result<object> x) => new RuleResult(!Regex.IsMatch(this.LocalServerName.Results.Value, "^[A-Za-z0-9\\-]*$")));
			this.LocalComputerIsDCInChildDomain = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install | SetupMode.DisasterRecovery).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.LocalComputerIsDCInChildDomain).Condition((Result<object> x) => new RuleResult((this.DomainRole.Results.Value == 4 || this.DomainRole.Results.Value == 5) && !this.IsGlobalCatalogReady.Results.Value && !this.RootNamingContext.Results.Value.Equals(this.ComputerDomainDN.Results.Value, StringComparison.InvariantCultureIgnoreCase)));
			this.CurrentLogOn = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue((Result<object> x) => new Result<string>(base.Providers.ManagedMethodProvider.GetUserNameEx(ValidationConstant.ExtendedNameFormat.NameSamCompatible)));
			this.LoggedOntoDomain = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.NotLoggedOntoDomain).Condition((Result<object> x) => new RuleResult(this.CurrentLogOn.Results.Value.StartsWith(this.LocalServerName.Results.Value + "\\", StringComparison.InvariantCultureIgnoreCase)));
			this.MsExServicesConfigDNOtherWellKnownObjects = Setting<ResultPropertyCollection>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn=Microsoft Exchange,cn=Services, {1}", this.GlobalCatalog.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"otherWellKnownObjects"
					}, null, SearchScope.Base);
					if (searchResultCollection != null)
					{
						using (IEnumerator enumerator = searchResultCollection.GetEnumerator())
						{
							if (enumerator.MoveNext())
							{
								SearchResult searchResult = (SearchResult)enumerator.Current;
								return new Result<ResultPropertyCollection>(searchResult.Properties);
							}
						}
					}
				}
				return new Result<ResultPropertyCollection>(null);
			});
			this.MsExServicesConfigDNOtherWellKnownObjectsEWPDN = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (this.MsExServicesConfigDNOtherWellKnownObjects.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MsExServicesConfigDNOtherWellKnownObjects.Results.Value["otherWellKnownObjects"];
					foreach (object obj in resultPropertyValueCollection)
					{
						string value = AnalysisHelpers.Replace(obj.ToString(), "(^B:32:4C17D0117EBE6642AFAEE03BC66D381F:(?'dn'.*))?.*$", "${dn}");
						if (!string.IsNullOrEmpty(value))
						{
							return new Result<string>(value);
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.MsExServicesConfigDNOtherWellKnownObjectsETSDN = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (this.MsExServicesConfigDNOtherWellKnownObjects.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.MsExServicesConfigDNOtherWellKnownObjects.Results.Value["otherWellKnownObjects"];
					foreach (object obj in resultPropertyValueCollection)
					{
						string value = AnalysisHelpers.Replace(obj.ToString(), "(^B:32:EA876A58DB6DD04C9006939818F800EB:(?'dn'.*))?.*$", "${dn}");
						if (!string.IsNullOrEmpty(value))
						{
							return new Result<string>(value);
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.EWPDn = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MsExServicesConfigDNOtherWellKnownObjectsEWPDN.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.MsExServicesConfigDNOtherWellKnownObjectsEWPDN.Results.Value), new string[]
					{
						"distinguishedName"
					}, null, SearchScope.Base);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["distinguishedName"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								return new Result<string>(obj2.ToString());
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ETSDn = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.MsExServicesConfigDNOtherWellKnownObjectsETSDN.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.MsExServicesConfigDNOtherWellKnownObjectsETSDN.Results.Value), new string[]
					{
						"distinguishedName"
					}, null, SearchScope.Base);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["distinguishedName"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								return new Result<string>(obj2.ToString());
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ETSIsMemberOfEWP = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.EWPDn.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.MsExServicesConfigDNOtherWellKnownObjectsETSDN.Results.Value), new string[]
					{
						"memberOf"
					}, null, SearchScope.Base);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["memberOf"];
						foreach (object obj2 in resultPropertyValueCollection)
						{
							if (obj2.ToString().Contains(this.EWPDn.Results.Value))
							{
								return new Result<bool>(true);
							}
						}
					}
				}
				return new Result<bool>(false);
			});
			this.DomainControllerCN = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.Global).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"cn"
					}, "(&(objectClass=server)(dNSHostName=*))", SearchScope.Subtree);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["cn"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								return new Result<string>(obj2.ToString());
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ExchangeServerRolesOnDomainController = Setting<int>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.DomainControllerCN.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"msExchCurrentServerRoles"
					}, string.Format("(&(objectCategory=msExchExchangeServer)(cn={0}))", this.DomainControllerCN.Results.Value), SearchScope.Subtree);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["msExchCurrentServerRoles"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								int num = int.Parse(obj2.ToString());
								return new Result<int>((num & 2) | (num & 4) | (num & 16) | (num & 32));
							}
						}
					}
				}
				return new Result<int>(0);
			});
			this.NtMixedDomainComputerDomainDN = Setting<int>.Build().AsRootSetting().In(this).AsSync().Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Mode(SetupMode.All).SetValue(delegate(Result<object> x)
			{
				if (this.LocalComputerDomainDN.Results.ValueOrDefault != null)
				{
					ResultPropertyValueCollection resultPropertyValueCollection = this.LocalComputerDomainDN.Results.Value["nTMixedDomain"];
					using (IEnumerator enumerator = resultPropertyValueCollection.GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							return new Result<int>((int)obj);
						}
					}
				}
				return new Result<int>(0);
			});
			this.LocalDomainModeMixed = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Global).Error<RuleBuilder<object>>().Message((Result x) => Strings.LocalDomainMixedMode(this.ComputerDomainDN.Results.Value, "Exchange 15")).Condition((Result<object> x) => new RuleResult(this.PrepareDomain.Results.ValueOrDefault != null && this.PrepareDomain.Results.Value.Equals("F63C3A12-7852-4654-B208-125C32EB409A", StringComparison.InvariantCultureIgnoreCase) && this.NtMixedDomainComputerDomainDN.Results.Value == 1));
			this.DomainPrepRequired = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.DomainPrepRequired).Condition((Result<object> x) => new RuleResult(!this.LocalDomainIsPrepped.Results.Value && !this.ComputerDomainDN.Results.Value.Equals(this.RootNamingContext.Results.Value, StringComparison.InvariantCultureIgnoreCase)));
			this.LocalDomainIsPrepped = Setting<bool>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.DomainController.Results.ValueOrDefault) && !string.IsNullOrEmpty(this.ComputerDomainDN.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.DomainController.Results.Value, this.ComputerDomainDN.Results.Value), new string[]
					{
						"objectVersion"
					}, "(objectCategory=msExchSystemObjectsContainer)", SearchScope.Subtree);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["objectVersion"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								return new Result<bool>((int)obj2 >= 12433);
							}
						}
					}
				}
				return new Result<bool>(false);
			});
			this.ReadOnlyDC = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn={1},cn=Servers,cn={2},cn=Sites,{3}", new object[]
					{
						this.GlobalCatalog.Results.Value,
						this.ShortServerName.Results.Value,
						this.SiteName.Results.Value,
						this.ConfigurationNamingContext.Results.Value
					}), new string[]
					{
						"distinguishedName"
					}, "(objectCategory=nTDSDSARO)", SearchScope.Subtree);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["distinguishedName"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								return new Result<string>(obj2.ToString());
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ComputerRODC = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.ComputerRODC).Condition((Result<object> x) => new RuleResult(!string.IsNullOrEmpty(this.ReadOnlyDC.Results.ValueOrDefault)));
			this.ServerRef = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn={1},cn=Sites,{2}", this.GlobalCatalog.Results.Value, this.SiteName.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"serverReference"
					}, "(&(objectClass=server)(dNSHostName=*))", SearchScope.Subtree);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["serverReference"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								return new Result<string>(obj2.ToString());
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.ADServerDN = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ConfigurationNamingContext.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/cn={1},cn=Sites,{2}", this.GlobalCatalog.Results.Value, this.SiteName.Results.Value, this.ConfigurationNamingContext.Results.Value), new string[]
					{
						"distinguishedName"
					}, "(&(objectClass=server)(dNSHostName=*))", SearchScope.Subtree);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["distinguishedName"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								return new Result<string>(obj2.ToString());
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.OperatingSystemVersion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ServerRef.Results.ValueOrDefault))
				{
					SearchResultCollection searchResultCollection = base.Providers.ADDataProvider.Run(false, string.Format("LDAP://{0}/{1}", this.GlobalCatalog.Results.Value, this.ServerRef.Results.Value), new string[]
					{
						"operatingSystemVersion"
					}, string.Empty, SearchScope.Base);
					foreach (object obj in searchResultCollection)
					{
						SearchResult searchResult = (SearchResult)obj;
						ResultPropertyValueCollection resultPropertyValueCollection = searchResult.Properties["operatingSystemVersion"];
						using (IEnumerator enumerator2 = resultPropertyValueCollection.GetEnumerator())
						{
							if (enumerator2.MoveNext())
							{
								object obj2 = enumerator2.Current;
								return new Result<string>(obj2.ToString());
							}
						}
					}
				}
				return new Result<string>(string.Empty);
			});
			this.InvalidADSite = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).Error<RuleBuilder<object>>().Message((Result x) => Strings.InvalidADSite).Condition((Result<object> x) => new RuleResult(string.IsNullOrEmpty(this.SiteName.Results.ValueOrDefault)));
			this.FirstSGFilesExist = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.Mailbox).Error<RuleBuilder<object>>().Message((Result x) => Strings.SGFilesExist(string.Format("{0}\\Mailbox\\First Storage Group", this.TargetDir.Results.Value))).Condition((Result<object> x) => new RuleResult(this.FirstSGFiles.Results.Count<Result<string>>() > 0));
			this.SecondSGFilesExist = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.Mailbox).Error<RuleBuilder<object>>().Message((Result x) => Strings.SGFilesExist(string.Format("{0}\\Mailbox\\Second Storage Group", this.TargetDir.Results.Value))).Condition((Result<object> x) => new RuleResult(this.SecondSGFiles.Results.Count<Result<string>>() > 0));
			this.FirstSGFiles = Setting<string>.Build().AsRootSetting().In(this).AsAsync().Mode(SetupMode.Install).Role(SetupRole.Mailbox).SetMultipleValues(delegate(Result<object> x)
			{
				string path = string.Format("{0}\\Mailbox\\First Storage Group", this.TargetDir.Results.Value);
				string[] source = new string[0];
				if (Directory.Exists(path))
				{
					source = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
				}
				return from w in source
				select new Result<string>(w);
			});
			this.SecondSGFiles = Setting<string>.Build().AsRootSetting().In(this).AsAsync().Mode(SetupMode.Install).Role(SetupRole.Mailbox).SetMultipleValues(delegate(Result<object> x)
			{
				string path = string.Format("{0}\\Mailbox\\Second Storage Group", this.TargetDir.Results.Value);
				string[] source = new string[0];
				if (this.CreatePublicDB.Results.Value && Directory.Exists(path))
				{
					source = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
				}
				return from w in source
				select new Result<string>(w);
			});
			this.TargetPathCompressed = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install | SetupMode.Upgrade).Role(SetupRole.Gateway).Error<RuleBuilder<object>>().Message((Result x) => Strings.TargetPathCompressed(this.globalParameters.TargetDir)).Condition(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM Win32_Directory WHERE Name='{0}'", this.globalParameters.TargetDir))[0].TryGetValue("Compressed", out obj))
				{
					return new RuleResult(bool.Parse(obj.ToString()));
				}
				return new RuleResult(false);
			});
			this.ADAMDataPathExists = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.Gateway).Error<RuleBuilder<object>>().Message((Result x) => Strings.ADAMDataPathExists(this.AdamDataPath.Results.ValueOrDefault)).Condition(delegate(Result<object> x)
			{
				object obj;
				if (!string.IsNullOrEmpty(this.AdamDataPath.Results.ValueOrDefault) && base.Providers.WMIDataProvider.Run(string.Format("SELECT * FROM Win32_Directory WHERE Name='{0}'", this.AdamDataPath.Results.ValueOrDefault))[0].TryGetValue("Name", out obj) && !string.IsNullOrEmpty(obj.ToString()))
				{
					return new RuleResult(true);
				}
				return new RuleResult(false);
			});
			this.ADAMPortAlreadyInUse = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.Gateway).Error<RuleBuilder<object>>().Message((Result x) => Strings.ADAMPortAlreadyInUse(this.AdamPort.Results.Value.ToString())).Condition(delegate(Result<object> x)
			{
				Dictionary<string, List<string>> commands = new Dictionary<string, List<string>>
				{
					{
						this.AdamPort.Results.Value.ToString(),
						new List<string>
						{
							this.AdamPort.Results.Value.ToString(),
							string.Empty,
							string.Empty
						}
					}
				};
				Dictionary<string, object[]> dictionary = base.Providers.ManagedMethodProvider.PortAvailable(string.Empty, commands);
				object[] array;
				if (!AnalysisHelpers.IsNullOrEmpty(dictionary) && dictionary.TryGetValue(this.AdamPort.Results.Value.ToString(), out array))
				{
					return new RuleResult(array != null && array.Length > 1 && bool.Parse(array[1] as string));
				}
				return new RuleResult(false);
			});
			this.ADAMSSLPortAlreadyInUse = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.Gateway).Error<RuleBuilder<object>>().Message((Result x) => Strings.ADAMSSLPortAlreadyInUse(this.AdamSSLPort.Results.Value.ToString())).Condition(delegate(Result<object> x)
			{
				Dictionary<string, List<string>> commands = new Dictionary<string, List<string>>
				{
					{
						this.AdamSSLPort.Results.Value.ToString(),
						new List<string>
						{
							this.AdamSSLPort.Results.Value.ToString(),
							string.Empty,
							string.Empty
						}
					}
				};
				Dictionary<string, object[]> dictionary = base.Providers.ManagedMethodProvider.PortAvailable(string.Empty, commands);
				object[] array;
				if (!AnalysisHelpers.IsNullOrEmpty(dictionary) && dictionary.TryGetValue(this.AdamSSLPort.Results.Value.ToString(), out array))
				{
					return new RuleResult(array != null && array.Length > 1 && bool.Parse(array[1] as string));
				}
				return new RuleResult(false);
			});
			this.OSProductType = Setting<uint>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).SetValue(delegate(Result<object> x)
			{
				object obj;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_OperatingSystem")[0].TryGetValue("ProductType", out obj))
				{
					return new Result<uint>((uint)obj);
				}
				return new Result<uint>(0U);
			});
			this.SuiteMask = Setting<uint>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.All).Role(SetupRole.AdminTools | SetupRole.Mailbox | SetupRole.Bridgehead | SetupRole.ClientAccess | SetupRole.UnifiedMessaging | SetupRole.Gateway | SetupRole.Cafe | SetupRole.Global | SetupRole.FrontendTransport).SetValue(delegate(Result<object> x)
			{
				object value;
				if (base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_OperatingSystem")[0].TryGetValue("SuiteMask", out value))
				{
					ExTraceGlobals.FaultInjectionTracer.TraceTest<object>(3406179645U, ref value);
					return new Result<uint>(Convert.ToUInt32(value));
				}
				return new Result<uint>(0U);
			});
			this.ServiceMarkedForDeletion = Setting<string>.Build().AsRootSetting().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.All).SetValue(delegate(Result<object> x)
			{
				string text = string.Empty;
				Dictionary<string, object>[] array = base.Providers.WMIDataProvider.Run("SELECT * FROM Win32_Service");
				for (int i = 0; i < array.Length; i++)
				{
					object obj;
					if (array[i].TryGetValue("Name", out obj))
					{
						using (ServiceController serviceController = new ServiceController(obj.ToString()))
						{
							try
							{
								ExTraceGlobals.FaultInjectionTracer.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(ExceptionInjectionCallback.Win32InvalidOperationException));
								ExTraceGlobals.FaultInjectionTracer.TraceTest(3439734077U);
								ServiceControllerStatus status = serviceController.Status;
							}
							catch (InvalidOperationException ex)
							{
								Win32Exception ex2 = ex.InnerException as Win32Exception;
								if (ex2 != null && 1072 == ex2.NativeErrorCode)
								{
									text += (string.IsNullOrEmpty(text) ? obj.ToString() : (", " + obj.ToString()));
								}
							}
						}
					}
				}
				return new Result<string>(text);
			});
			this.ServicesAreMarkedForDeletion = Rule.Build().AsRootRule().In(this).AsSync().Mode(SetupMode.Install).Role(SetupRole.All).Error<RuleBuilder<object>>().Message((Result x) => Strings.ServicesAreMarkedForDeletion(this.ServiceMarkedForDeletion.Results.Value)).Condition(delegate(Result<object> x)
			{
				if (!string.IsNullOrEmpty(this.ServiceMarkedForDeletion.Results.Value))
				{
					return new RuleResult(true);
				}
				return new RuleResult(false);
			});
		}

		// Token: 0x0400029B RID: 667
		private object logLock = new object();

		// Token: 0x0400029C RID: 668
		private GlobalParameters globalParameters;

		// Token: 0x0400029D RID: 669
		private static uint adBasicPermissions = 983485U;

		// Token: 0x0400029E RID: 670
		private static uint adWritePermissions = 983295U;
	}
}
