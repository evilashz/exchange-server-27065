using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.ServiceProcess;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.Setup.CommonBase;
using Microsoft.Exchange.Setup.Parser;
using Microsoft.Win32;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000059 RID: 89
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SetupContext : ISetupContext
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x0000DA10 File Offset: 0x0000BC10
		public SetupContext(Dictionary<string, object> parsedArguments, ExchangeServer server, bool isCleanMachine, RoleCollection unpackedRoles, RoleCollection unpackedDatacenterRoles, RoleCollection installedRolesLocal, RoleCollection partiallyConfiguredRoles, IOrganizationName organizationName, bool isW3SVCStartOk)
		{
			this.WatsonEnabled = false;
			this.ExchangeCulture = CultureInfo.InstalledUICulture;
			this.SetSetupContext(parsedArguments, server, isCleanMachine, unpackedRoles, unpackedDatacenterRoles, installedRolesLocal, partiallyConfiguredRoles, organizationName, isW3SVCStartOk);
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x0000DA4C File Offset: 0x0000BC4C
		public SetupContext(Dictionary<string, object> parsedArguments)
		{
			this.WatsonEnabled = false;
			this.ExchangeCulture = CultureInfo.InstalledUICulture;
			bool isE12Schema = false;
			bool isSchemaUpdateRequired = false;
			bool isOrgConfigUpdateRequired = false;
			bool isDomainConfigUpdateRequired = false;
			bool? flag = null;
			bool? flag2 = null;
			bool hostingDeployment = false;
			IndustryType industryType = IndustryType.NotSpecified;
			bool adinitializedSuccessfully;
			LocalizedException adinitializationError;
			string text;
			string gc;
			SetupContext.InitializeAD(out adinitializedSuccessfully, out adinitializationError, out text, out gc, ref isE12Schema, ref isSchemaUpdateRequired, ref isOrgConfigUpdateRequired, ref isDomainConfigUpdateRequired, parsedArguments);
			SetupLogger.Log(Strings.SetupWillUseDomainController(text));
			SetupLogger.Log(Strings.SetupWillUseGlobalCatalog(gc));
			ExchangeConfigurationContainer exchangeConfigurationContainer = null;
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(null, true, ConsistencyMode.PartiallyConsistent, null, ADSessionSettings.FromRootOrgScopeSet(), 149, ".ctor", "f:\\15.00.1497\\sources\\dev\\Setup\\src\\Common\\DataHandlers\\SetupContext.cs");
			OrganizationName organizationName = null;
			try
			{
				exchangeConfigurationContainer = topologyConfigurationSession.GetExchangeConfigurationContainer();
				SetupLogger.Log(Strings.ExchangeConfigurationContainerName(exchangeConfigurationContainer.DistinguishedName));
			}
			catch (ExchangeConfigurationContainerNotFoundException ex)
			{
				SetupLogger.Log(Strings.NoExchangeConfigurationContainerFound(ex.Message));
			}
			catch (CannotGetDomainInfoException ex2)
			{
				SetupLogger.Log(Strings.NoExchangeConfigurationContainerFound(ex2.Message));
			}
			catch (ADTransientException ex3)
			{
				SetupLogger.Log(Strings.NoExchangeConfigurationContainerFound(ex3.Message));
			}
			catch (ActiveDirectoryObjectNotFoundException ex4)
			{
				SetupLogger.Log(Strings.NoExchangeConfigurationContainerFound(ex4.Message));
			}
			catch (DataSourceOperationException ex5)
			{
				SetupLogger.Log(Strings.NoExchangeConfigurationContainerFound(ex5.Message));
			}
			catch (DataSourceTransientException ex6)
			{
				SetupLogger.Log(Strings.NoExchangeConfigurationContainerFound(ex6.Message));
			}
			if (exchangeConfigurationContainer != null)
			{
				try
				{
					Organization orgContainer = topologyConfigurationSession.GetOrgContainer();
					SetupLogger.Log(Strings.ExchangeOrganizationContainerName(orgContainer.DistinguishedName));
					if (null != orgContainer.Id.Rdn)
					{
						organizationName = new OrganizationName(orgContainer.Id.Rdn);
					}
					flag = orgContainer.CustomerFeedbackEnabled;
					industryType = orgContainer.Industry;
					hostingDeployment = orgContainer.HostingDeploymentEnabled;
				}
				catch (OrgContainerNotFoundException ex7)
				{
					SetupLogger.Log(Strings.NoExchangeOrganizationContainerFound(ex7.Message));
					SetupLogger.Log(Strings.RemoveMESOObjectLink);
					adinitializationError = new ADInitializationException(ex7.LocalizedString, ex7);
				}
				catch (ADTransientException ex8)
				{
					SetupLogger.Log(Strings.NoExchangeOrganizationContainerFound(ex8.Message));
				}
				catch (FormatException ex9)
				{
					this.OrganizationNameValidationException = new LocalizedException(Strings.InvalidExchangeOrganizationName(ex9.Message));
					SetupLogger.Log(Strings.InvalidExchangeOrganizationName(ex9.Message));
				}
			}
			ExchangeServer server = null;
			Server server2 = null;
			if (exchangeConfigurationContainer != null)
			{
				string machineName = Environment.MachineName;
				if (machineName == Environment.MachineName)
				{
					SetupLogger.Log(Strings.WillSearchForAServerObjectForLocalServer(machineName));
					try
					{
						server2 = topologyConfigurationSession.FindLocalServer();
						goto IL_284;
					}
					catch (ComputerNameNotCurrentlyAvailableException)
					{
						goto IL_284;
					}
					catch (LocalServerNotFoundException)
					{
						goto IL_284;
					}
				}
				SetupLogger.Log(Strings.WillSearchForAServerObjectForServer(machineName));
				try
				{
					server2 = topologyConfigurationSession.FindServerByName(machineName);
				}
				catch (ADTransientException ex10)
				{
					SetupLogger.Log(Strings.AttemptToSearchExchangeServerFailed(machineName, ex10.Message));
				}
				catch (DataSourceOperationException ex11)
				{
					SetupLogger.Log(Strings.AttemptToSearchExchangeServerFailed(machineName, ex11.Message));
				}
				IL_284:
				if (server2 != null)
				{
					SetupLogger.Log(Strings.ExchangeServerFound(server2.DistinguishedName));
					server = new ExchangeServer(server2);
					flag2 = server2.CustomerFeedbackEnabled;
				}
				else
				{
					SetupLogger.Log(Strings.ExchangeServerNotFound(machineName));
				}
			}
			bool flag3 = !ConfigurationContext.Setup.IsUnpacked;
			RoleCollection unpackedRoles = RoleManager.GetUnpackedRoles();
			RoleCollection unpackedDatacenterRoles = RoleManager.GetUnpackedDatacenterRoles();
			RoleCollection installedRoles = RoleManager.GetInstalledRoles();
			RoleCollection roleCollection = new RoleCollection();
			foreach (Role role in RoleManager.Roles)
			{
				if (role.IsPartiallyInstalled || (role.IsUnpacked && !role.IsInstalled))
				{
					roleCollection.Add(role);
				}
			}
			if (!flag3)
			{
				SetupLogger.Log(Strings.TheCurrentServerHasExchangeBits);
			}
			else
			{
				SetupLogger.Log(Strings.TheCurrentServerHasNoExchangeBits);
			}
			bool isW3SVCStartOk = SetupContext.DetectIsW3SVCStartOk();
			this.SetSetupContext(parsedArguments, server, flag3, unpackedRoles, unpackedDatacenterRoles, installedRoles, roleCollection, organizationName, isW3SVCStartOk);
			this.HostingDeployment = hostingDeployment;
			this.ADInitializedSuccessfully = adinitializedSuccessfully;
			this.ADInitializationError = adinitializationError;
			this.DomainController = text;
			this.IsE12Schema = isE12Schema;
			this.IsSchemaUpdateRequired = isSchemaUpdateRequired;
			this.IsOrgConfigUpdateRequired = isOrgConfigUpdateRequired;
			this.IsDomainConfigUpdateRequired = isDomainConfigUpdateRequired;
			this.OriginalGlobalCustomerFeedbackEnabled = flag;
			this.GlobalCustomerFeedbackEnabled = flag;
			this.OriginalServerCustomerFeedbackEnabled = flag2;
			this.ServerCustomerFeedbackEnabled = flag2;
			this.OriginalIndustry = industryType;
			this.Industry = industryType;
			SetupLogger.Log(Strings.AdInitializationStatus(this.ADInitializedSuccessfully));
			SetupLogger.Log(Strings.SchemaUpdateRequired(this.IsSchemaUpdateRequired));
			SetupLogger.Log(Strings.OrgConfigUpdateRequired(this.IsOrgConfigUpdateRequired));
			SetupLogger.Log(Strings.DomainConfigUpdateRequired(this.IsDomainConfigUpdateRequired));
			if (!this.IsCleanMachine)
			{
				this.InstalledPath = NonRootLocalLongFullPath.Parse(ConfigurationContext.Setup.InstallPath);
				try
				{
					this.InstalledVersion = ConfigurationContext.Setup.InstalledVersion;
					SetupLogger.Log(Strings.InstalledVersion(this.InstalledVersion));
				}
				catch (SetupVersionInformationCorruptException ex12)
				{
					SetupLogger.Log(ex12.LocalizedString);
					this.RegistryError = ex12;
				}
			}
			this.TargetDir = this.InstalledPath;
			if (this.TargetDir != null)
			{
				SetupLogger.Log(Strings.TargetInstallationDirectory(this.TargetDir.PathName));
			}
			if (this.ExchangeOrganizationExists)
			{
				ADPagedReader<Server> adpagedReader = topologyConfigurationSession.FindPaged<Server>(exchangeConfigurationContainer.Id, QueryScope.SubTree, null, null, 0);
				Server[] array = adpagedReader.ReadAllPages();
				bool canOrgBeRemoved = (server2 == null && array.Length == 0) || (server2 != null && array.Length == 1);
				foreach (Server dataObject in array)
				{
					ExchangeServer exchangeServer = new ExchangeServer(dataObject);
					bool flag4 = false;
					ValidationError validationError;
					if (DirectoryUtilities.IsPropertyValid(exchangeServer, ServerSchema.IsExchange2007OrLater, out validationError))
					{
						flag4 = exchangeServer.IsExchange2007OrLater;
					}
					else
					{
						SetupLogger.Log(Strings.ExchangeVersionInvalid(exchangeServer.Name, validationError.Description));
					}
					bool flag5 = false;
					if (DirectoryUtilities.IsPropertyValid(exchangeServer, ServerSchema.IsE14OrLater, out validationError))
					{
						flag5 = exchangeServer.IsE14OrLater;
					}
					else
					{
						SetupLogger.Log(Strings.ExchangeVersionInvalid(exchangeServer.Name, validationError.Description));
					}
					if (exchangeServer.IsMailboxServer && flag4)
					{
						this.HasMailboxServers = true;
					}
					if (exchangeServer.IsHubTransportServer && flag4)
					{
						this.HasBridgeheadServers = true;
					}
					if (!flag4)
					{
						this.HasLegacyServers = true;
					}
					if (flag5)
					{
						this.HasE14OrLaterServers = true;
					}
					if (server2 != null && string.Compare(server2.DistinguishedName, exchangeServer.DistinguishedName, true, CultureInfo.InvariantCulture) != 0)
					{
						canOrgBeRemoved = false;
					}
				}
				this.CanOrgBeRemoved = canOrgBeRemoved;
			}
			if (this.ParsedArguments.ContainsKey("enableerrorreporting"))
			{
				this.WatsonEnabled = true;
			}
			else
			{
				object value = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15", "DisableErrorReporting", 1);
				if (value != null)
				{
					this.WatsonEnabled = ((int)value == 0);
				}
			}
			this.ExchangeCulture = CultureInfo.GetCultureInfo("en-US");
			this.IsLonghornServer = (Environment.OSVersion.Version.Major >= 6);
			ProvisioningLayer.Disabled = true;
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000E16C File Offset: 0x0000C36C
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000E174 File Offset: 0x0000C374
		public string CurrentWizardPageName { get; set; }

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000E17D File Offset: 0x0000C37D
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x0000E185 File Offset: 0x0000C385
		[DefaultValue(false)]
		public bool IsRestoredFromPreviousState { get; set; }

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0000E18E File Offset: 0x0000C38E
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x0000E196 File Offset: 0x0000C396
		public string ExchangeServerName { get; set; }

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x0000E19F File Offset: 0x0000C39F
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x0000E1A7 File Offset: 0x0000C3A7
		public Dictionary<string, object> ParsedArguments { get; set; }

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x0000E1B0 File Offset: 0x0000C3B0
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x0000E1B8 File Offset: 0x0000C3B8
		public InstallationModes InstallationMode { get; set; }

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x0000E1C1 File Offset: 0x0000C3C1
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x0000E1C9 File Offset: 0x0000C3C9
		public bool InstallWindowsComponents { get; set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000E1D2 File Offset: 0x0000C3D2
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x0000E1DA File Offset: 0x0000C3DA
		public RoleCollection InstalledRolesLocal { get; set; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x0000E1E3 File Offset: 0x0000C3E3
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x0000E1EB File Offset: 0x0000C3EB
		public RoleCollection InstalledRolesAD { get; set; }

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x0000E1F4 File Offset: 0x0000C3F4
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000E1FC File Offset: 0x0000C3FC
		public RoleCollection PartiallyConfiguredRoles { get; set; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x0000E205 File Offset: 0x0000C405
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000E20D File Offset: 0x0000C40D
		public RoleCollection UnpackedRoles { get; set; }

		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x0000E216 File Offset: 0x0000C416
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000E21E File Offset: 0x0000C41E
		public RoleCollection UnpackedDatacenterRoles { get; set; }

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x0000E227 File Offset: 0x0000C427
		// (set) Token: 0x060003FB RID: 1019 RVA: 0x0000E22F File Offset: 0x0000C42F
		public bool WatsonEnabled { get; set; }

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x0000E238 File Offset: 0x0000C438
		// (set) Token: 0x060003FD RID: 1021 RVA: 0x0000E263 File Offset: 0x0000C463
		public RoleCollection RequestedRoles
		{
			get
			{
				if (!this.ParsedArguments.ContainsKey("roles"))
				{
					return null;
				}
				return (RoleCollection)this.ParsedArguments["roles"];
			}
			set
			{
				this.ParsedArguments["roles"] = value;
			}
		}

		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x0000E276 File Offset: 0x0000C476
		// (set) Token: 0x060003FF RID: 1023 RVA: 0x0000E27E File Offset: 0x0000C47E
		public bool IsCleanMachine { get; set; }

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x0000E287 File Offset: 0x0000C487
		// (set) Token: 0x06000401 RID: 1025 RVA: 0x0000E28F File Offset: 0x0000C48F
		public bool IsDatacenter { get; set; }

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x0000E298 File Offset: 0x0000C498
		// (set) Token: 0x06000403 RID: 1027 RVA: 0x0000E2A0 File Offset: 0x0000C4A0
		public bool IsDatacenterDedicated { get; set; }

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x06000404 RID: 1028 RVA: 0x0000E2A9 File Offset: 0x0000C4A9
		// (set) Token: 0x06000405 RID: 1029 RVA: 0x0000E2B1 File Offset: 0x0000C4B1
		public bool TreatPreReqErrorsAsWarnings { get; set; }

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x06000406 RID: 1030 RVA: 0x0000E2BA File Offset: 0x0000C4BA
		// (set) Token: 0x06000407 RID: 1031 RVA: 0x0000E2C2 File Offset: 0x0000C4C2
		public bool IsFfo { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000408 RID: 1032 RVA: 0x0000E2CB File Offset: 0x0000C4CB
		// (set) Token: 0x06000409 RID: 1033 RVA: 0x0000E2D3 File Offset: 0x0000C4D3
		public bool IsPartnerHosted { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x0600040A RID: 1034 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		// (set) Token: 0x0600040B RID: 1035 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		public bool IsBackupKeyPresent { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600040C RID: 1036 RVA: 0x0000E2ED File Offset: 0x0000C4ED
		// (set) Token: 0x0600040D RID: 1037 RVA: 0x0000E2F5 File Offset: 0x0000C4F5
		public NonRootLocalLongFullPath InstalledPath { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000E2FE File Offset: 0x0000C4FE
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000E306 File Offset: 0x0000C506
		public NonRootLocalLongFullPath BackupInstalledPath { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000E30F File Offset: 0x0000C50F
		// (set) Token: 0x06000411 RID: 1041 RVA: 0x0000E317 File Offset: 0x0000C517
		public Version InstalledVersion { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x0000E320 File Offset: 0x0000C520
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x0000E328 File Offset: 0x0000C528
		public Version RunningVersion { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000E331 File Offset: 0x0000C531
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x0000E339 File Offset: 0x0000C539
		public Version BackupInstalledVersion { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x0000E342 File Offset: 0x0000C542
		// (set) Token: 0x06000417 RID: 1047 RVA: 0x0000E34A File Offset: 0x0000C54A
		public ushort AdamLdapPort { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0000E353 File Offset: 0x0000C553
		// (set) Token: 0x06000419 RID: 1049 RVA: 0x0000E35B File Offset: 0x0000C55B
		public ushort AdamSslPort { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0000E364 File Offset: 0x0000C564
		// (set) Token: 0x0600041B RID: 1051 RVA: 0x0000E36C File Offset: 0x0000C56C
		public LongPath SourceDir
		{
			get
			{
				return this.sourceDir;
			}
			set
			{
				this.sourceDir = value;
				if (this.languagePackContext != null && !this.IsLanguagePackOperation && this.SourceDir != null)
				{
					this.LanguagePackPath = this.SourceDir;
				}
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600041C RID: 1052 RVA: 0x0000E39F File Offset: 0x0000C59F
		// (set) Token: 0x0600041D RID: 1053 RVA: 0x0000E3A7 File Offset: 0x0000C5A7
		public LongPath UpdatesDir { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000E3B0 File Offset: 0x0000C5B0
		// (set) Token: 0x0600041F RID: 1055 RVA: 0x0000E3BD File Offset: 0x0000C5BD
		public LongPath LanguagePackPath
		{
			get
			{
				return this.languagePackContext.LanguagePackPath;
			}
			set
			{
				this.languagePackContext.LanguagePackPath = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000420 RID: 1056 RVA: 0x0000E3CB File Offset: 0x0000C5CB
		public bool LanguagePackSourceIsBundle
		{
			get
			{
				return this.languagePackContext.LanguagePackSourceIsBundle;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x0000E3D8 File Offset: 0x0000C5D8
		public Dictionary<string, LanguageInfo> CollectedLanguagePacks
		{
			get
			{
				return this.languagePackContext.CollectedLanguagePacks;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000E3E5 File Offset: 0x0000C5E5
		public Dictionary<string, LanguageInfo> SourceLanguagePacks
		{
			get
			{
				return this.languagePackContext.SourceLanguagePacks;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0000E3F2 File Offset: 0x0000C5F2
		public Dictionary<string, Array> LanguagePacksToInstall
		{
			get
			{
				return this.languagePackContext.LanguagePacksToInstall;
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000E3FF File Offset: 0x0000C5FF
		public Dictionary<string, long> LanguagesToInstall
		{
			get
			{
				return this.languagePackContext.LanguagesToInstall;
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0000E40C File Offset: 0x0000C60C
		public HashSet<string> InstalledLanguagePacks
		{
			get
			{
				return this.languagePackContext.InstalledLanguagePacks;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000E419 File Offset: 0x0000C619
		// (set) Token: 0x06000427 RID: 1063 RVA: 0x0000E421 File Offset: 0x0000C621
		public NonRootLocalLongFullPath TargetDir { get; set; }

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000E42A File Offset: 0x0000C62A
		// (set) Token: 0x06000429 RID: 1065 RVA: 0x0000E432 File Offset: 0x0000C632
		public bool IsW3SVCStartOk { get; private set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600042A RID: 1066 RVA: 0x0000E43B File Offset: 0x0000C63B
		public string NewProvisionedServerName
		{
			get
			{
				return (string)this.ParsedArguments["newprovisionedserver"];
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000E454 File Offset: 0x0000C654
		public string RemoveProvisionedServerName
		{
			get
			{
				if (!this.HasRemoveProvisionedServerParameters)
				{
					return null;
				}
				if (this.ParsedArguments.ContainsKey("removeprovisionedserver"))
				{
					return ((string)this.ParsedArguments["removeprovisionedserver"]) ?? this.ExchangeServerName;
				}
				return this.ExchangeServerName;
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600042C RID: 1068 RVA: 0x0000E4A3 File Offset: 0x0000C6A3
		// (set) Token: 0x0600042D RID: 1069 RVA: 0x0000E4AB File Offset: 0x0000C6AB
		public bool HasMailboxServers { get; set; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000E4B4 File Offset: 0x0000C6B4
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000E4BC File Offset: 0x0000C6BC
		public bool HasBridgeheadServers { get; set; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000E4C5 File Offset: 0x0000C6C5
		// (set) Token: 0x06000431 RID: 1073 RVA: 0x0000E4CD File Offset: 0x0000C6CD
		public bool HasLegacyServers { get; set; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x0000E4D6 File Offset: 0x0000C6D6
		// (set) Token: 0x06000433 RID: 1075 RVA: 0x0000E4DE File Offset: 0x0000C6DE
		public bool HasE14OrLaterServers { get; set; }

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000E4E7 File Offset: 0x0000C6E7
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000E4EF File Offset: 0x0000C6EF
		public bool ExchangeOrganizationExists { get; set; }

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000E4F8 File Offset: 0x0000C6F8
		// (set) Token: 0x06000437 RID: 1079 RVA: 0x0000E500 File Offset: 0x0000C700
		public LocalizedException OrganizationNameValidationException { get; set; }

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000E509 File Offset: 0x0000C709
		// (set) Token: 0x06000439 RID: 1081 RVA: 0x0000E511 File Offset: 0x0000C711
		public bool IsServerFoundInAD { get; private set; }

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x0000E51A File Offset: 0x0000C71A
		// (set) Token: 0x0600043B RID: 1083 RVA: 0x0000E522 File Offset: 0x0000C722
		public IOrganizationName OrganizationName { get; set; }

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x0000E52B File Offset: 0x0000C72B
		// (set) Token: 0x0600043D RID: 1085 RVA: 0x0000E533 File Offset: 0x0000C733
		public IOrganizationName OrganizationNameFoundInAD { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x0000E53C File Offset: 0x0000C73C
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x0000E544 File Offset: 0x0000C744
		public string DomainController { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000E54D File Offset: 0x0000C74D
		// (set) Token: 0x06000441 RID: 1089 RVA: 0x0000E555 File Offset: 0x0000C755
		public bool ADInitializedSuccessfully { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000E55E File Offset: 0x0000C75E
		// (set) Token: 0x06000443 RID: 1091 RVA: 0x0000E566 File Offset: 0x0000C766
		public LocalizedException ADInitializationError { get; private set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000E56F File Offset: 0x0000C76F
		// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000E577 File Offset: 0x0000C777
		public bool IsE12Schema { get; set; }

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000E580 File Offset: 0x0000C780
		// (set) Token: 0x06000447 RID: 1095 RVA: 0x0000E588 File Offset: 0x0000C788
		public bool IsSchemaUpdateRequired { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000E591 File Offset: 0x0000C791
		// (set) Token: 0x06000449 RID: 1097 RVA: 0x0000E599 File Offset: 0x0000C799
		public bool IsOrgConfigUpdateRequired { get; set; }

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000E5A2 File Offset: 0x0000C7A2
		// (set) Token: 0x0600044B RID: 1099 RVA: 0x0000E5AA File Offset: 0x0000C7AA
		public bool IsDomainConfigUpdateRequired { get; private set; }

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x0000E5B3 File Offset: 0x0000C7B3
		// (set) Token: 0x0600044D RID: 1101 RVA: 0x0000E5BB File Offset: 0x0000C7BB
		public bool StartTransportService { get; set; }

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
		// (set) Token: 0x0600044F RID: 1103 RVA: 0x0000E5CC File Offset: 0x0000C7CC
		public bool CanOrgBeRemoved { get; set; }

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000E5D5 File Offset: 0x0000C7D5
		// (set) Token: 0x06000451 RID: 1105 RVA: 0x0000E5DD File Offset: 0x0000C7DD
		public bool HostingDeployment { get; private set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000E5E6 File Offset: 0x0000C7E6
		public string TenantOrganizationConfig
		{
			get
			{
				if (!this.ParsedArguments.ContainsKey("tenantorganizationconfig"))
				{
					return null;
				}
				return this.ParsedArguments["tenantorganizationconfig"].ToString();
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000453 RID: 1107 RVA: 0x0000E611 File Offset: 0x0000C811
		// (set) Token: 0x06000454 RID: 1108 RVA: 0x0000E619 File Offset: 0x0000C819
		public LocalizedException RegistryError { get; private set; }

		// Token: 0x06000455 RID: 1109 RVA: 0x0000E624 File Offset: 0x0000C824
		public bool IsInstalledLocal(string roleName)
		{
			foreach (Role role in this.InstalledRolesLocal)
			{
				if (role.RoleName.Equals(roleName, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0000E688 File Offset: 0x0000C888
		public bool IsInstalledAD(string roleName)
		{
			foreach (Role role in this.InstalledRolesAD)
			{
				if (role.RoleName.Equals(roleName, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0000E6EC File Offset: 0x0000C8EC
		public bool IsInstalledLocalOrAD(string roleName)
		{
			return this.IsInstalledLocal(roleName) || this.IsInstalledAD(roleName);
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0000E700 File Offset: 0x0000C900
		public bool IsUnpacked(string roleName)
		{
			foreach (Role role in this.UnpackedRoles)
			{
				if (role.RoleName.Equals(roleName, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0000E764 File Offset: 0x0000C964
		public bool IsUnpackedOrInstalledAD(string roleName)
		{
			return this.IsUnpacked(roleName) || this.IsInstalledAD(roleName);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x0000E778 File Offset: 0x0000C978
		public bool IsPartiallyConfigured(string roleName)
		{
			foreach (Role role in this.PartiallyConfiguredRoles)
			{
				if (role.RoleName.Equals(roleName, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000E7DC File Offset: 0x0000C9DC
		public bool IsRequested(string roleName)
		{
			foreach (Role role in this.RequestedRoles)
			{
				if (role.RoleName.Equals(roleName, StringComparison.InvariantCultureIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000E840 File Offset: 0x0000CA40
		public bool HasPrepareADParameters
		{
			get
			{
				return this.ParsedArguments != null && (this.ParsedArguments.ContainsKey("prepareschema") || this.ParsedArguments.ContainsKey("preparead") || this.ParsedArguments.ContainsKey("preparesct") || this.ParsedArguments.ContainsKey("preparedomain") || this.ParsedArguments.ContainsKey("preparealldomains"));
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x0000E8B1 File Offset: 0x0000CAB1
		public bool HasRolesToInstall
		{
			get
			{
				return this.RequestedRoles.Count > 0;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000E8C4 File Offset: 0x0000CAC4
		public bool HasNewProvisionedServerParameters
		{
			get
			{
				return this.ParsedArguments != null && this.ParsedArguments.ContainsKey("newprovisionedserver");
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000E8E0 File Offset: 0x0000CAE0
		public bool HasRemoveProvisionedServerParameters
		{
			get
			{
				return this.ParsedArguments != null && this.ParsedArguments.ContainsKey("removeprovisionedserver");
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000E8FC File Offset: 0x0000CAFC
		public bool IsProvisionedServer
		{
			get
			{
				return this.isProvisionedServer;
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000E904 File Offset: 0x0000CB04
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x0000E90C File Offset: 0x0000CB0C
		public List<CultureInfo> InstalledUMLanguagePacks { get; set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000E915 File Offset: 0x0000CB15
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x0000E91D File Offset: 0x0000CB1D
		public List<CultureInfo> SelectedCultures { get; set; }

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0000E926 File Offset: 0x0000CB26
		// (set) Token: 0x06000466 RID: 1126 RVA: 0x0000E933 File Offset: 0x0000CB33
		public bool IsLanguagePackOperation
		{
			get
			{
				return this.languagePackContext.IsLanguagePackOperation;
			}
			set
			{
				this.languagePackContext.IsLanguagePackOperation = value;
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0000E941 File Offset: 0x0000CB41
		// (set) Token: 0x06000468 RID: 1128 RVA: 0x0000E949 File Offset: 0x0000CB49
		public bool IsUmLanguagePackOperation { get; set; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0000E952 File Offset: 0x0000CB52
		// (set) Token: 0x0600046A RID: 1130 RVA: 0x0000E95A File Offset: 0x0000CB5A
		public CultureInfo ExchangeCulture { get; set; }

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0000E963 File Offset: 0x0000CB63
		// (set) Token: 0x0600046C RID: 1132 RVA: 0x0000E96B File Offset: 0x0000CB6B
		public bool IsLonghornServer { get; set; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0000E974 File Offset: 0x0000CB74
		// (set) Token: 0x0600046E RID: 1134 RVA: 0x0000E97C File Offset: 0x0000CB7C
		public bool? OriginalGlobalCustomerFeedbackEnabled { get; set; }

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0000E985 File Offset: 0x0000CB85
		// (set) Token: 0x06000470 RID: 1136 RVA: 0x0000E98D File Offset: 0x0000CB8D
		public bool? GlobalCustomerFeedbackEnabled { get; set; }

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0000E996 File Offset: 0x0000CB96
		// (set) Token: 0x06000472 RID: 1138 RVA: 0x0000E99E File Offset: 0x0000CB9E
		public bool? OriginalServerCustomerFeedbackEnabled { get; set; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0000E9A7 File Offset: 0x0000CBA7
		// (set) Token: 0x06000474 RID: 1140 RVA: 0x0000E9AF File Offset: 0x0000CBAF
		public bool? ServerCustomerFeedbackEnabled { get; set; }

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0000E9B8 File Offset: 0x0000CBB8
		// (set) Token: 0x06000476 RID: 1142 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		public bool? ActiveDirectorySplitPermissions { get; set; }

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000E9C9 File Offset: 0x0000CBC9
		// (set) Token: 0x06000478 RID: 1144 RVA: 0x0000E9D1 File Offset: 0x0000CBD1
		public IndustryType OriginalIndustry { get; set; }

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000E9DA File Offset: 0x0000CBDA
		// (set) Token: 0x0600047A RID: 1146 RVA: 0x0000E9E2 File Offset: 0x0000CBE2
		public IndustryType Industry { get; set; }

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000E9EB File Offset: 0x0000CBEB
		// (set) Token: 0x0600047C RID: 1148 RVA: 0x0000E9F8 File Offset: 0x0000CBF8
		public bool IsLanaguagePacksInstalled
		{
			get
			{
				return this.languagePackContext.IsLanaguagePacksInstalled;
			}
			set
			{
				this.languagePackContext.IsLanaguagePacksInstalled = value;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000EA06 File Offset: 0x0000CC06
		// (set) Token: 0x0600047E RID: 1150 RVA: 0x0000EA0E File Offset: 0x0000CC0E
		public bool DisableAMFiltering { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000EA17 File Offset: 0x0000CC17
		// (set) Token: 0x06000480 RID: 1152 RVA: 0x0000EA24 File Offset: 0x0000CC24
		public bool NeedToUpdateLanguagePacks
		{
			get
			{
				return this.languagePackContext.NeedToUpdateLanguagePacks;
			}
			set
			{
				this.languagePackContext.NeedToUpdateLanguagePacks = value;
			}
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x0000EA32 File Offset: 0x0000CC32
		public void UpdateIsW3SVCStartOk()
		{
			this.IsW3SVCStartOk = SetupContext.DetectIsW3SVCStartOk();
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x0000EA40 File Offset: 0x0000CC40
		private static bool DetectIsW3SVCStartOk()
		{
			bool result;
			using (ServiceController serviceController = new ServiceController("W3SVC"))
			{
				try
				{
					ServiceControllerStatus status = serviceController.Status;
					result = true;
				}
				catch (InvalidOperationException ex)
				{
					Win32Exception ex2 = ex.InnerException as Win32Exception;
					if (ex2 == null || (1060 != ex2.NativeErrorCode && 1072 != ex2.NativeErrorCode && 1058 != ex2.NativeErrorCode))
					{
						throw;
					}
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0000EACC File Offset: 0x0000CCCC
		private void SetSetupContext(Dictionary<string, object> parsedArguments, ExchangeServer server, bool isCleanMachine, RoleCollection unpackedRoles, RoleCollection unpackedDatacenterRoles, RoleCollection installedRolesLocal, RoleCollection partiallyConfiguredRoles, IOrganizationName organizationName, bool isW3SVCStartOk)
		{
			this.IsCleanMachine = true;
			this.InstalledRolesLocal = new RoleCollection();
			this.InstalledRolesAD = new RoleCollection();
			this.PartiallyConfiguredRoles = new RoleCollection();
			this.UnpackedRoles = new RoleCollection();
			this.UnpackedDatacenterRoles = new RoleCollection();
			this.ParsedArguments = (parsedArguments ?? new Dictionary<string, object>());
			this.HasMailboxServers = false;
			this.HasBridgeheadServers = false;
			this.HasLegacyServers = false;
			this.CanOrgBeRemoved = false;
			this.DisableAMFiltering = false;
			SetupLogger.Log(Strings.DisplayServerName(Environment.MachineName));
			if (this.ParsedArguments.ContainsKey("disableamfiltering"))
			{
				this.DisableAMFiltering = true;
				SetupLogger.Log(Strings.WillDisableAMFiltering);
			}
			if (!this.ParsedArguments.ContainsKey("roles"))
			{
				this.ParsedArguments["roles"] = new RoleCollection();
			}
			this.IsCleanMachine = isCleanMachine;
			this.isProvisionedServer = false;
			this.OrganizationName = organizationName;
			this.IsW3SVCStartOk = isW3SVCStartOk;
			this.OrganizationNameFoundInAD = organizationName;
			this.IsServerFoundInAD = (null != server);
			if (this.ParsedArguments.ContainsKey("donotstarttransport"))
			{
				this.StartTransportService = false;
				SetupLogger.Log(Strings.WillNotStartTransportService);
			}
			else
			{
				this.StartTransportService = true;
			}
			this.SelectedCultures = new List<CultureInfo>();
			if (this.ParsedArguments.ContainsKey("addumlanguagepack"))
			{
				this.SelectedCultures = (List<CultureInfo>)this.ParsedArguments["addumlanguagepack"];
			}
			else if (this.ParsedArguments.ContainsKey("removeumlanguagepack"))
			{
				this.SelectedCultures = (List<CultureInfo>)this.ParsedArguments["removeumlanguagepack"];
			}
			this.InstalledUMLanguagePacks = new List<CultureInfo>();
			this.InstalledUMLanguagePacks.AddRange(LanguagePackInfo.GetInstalledLanguagePackCultures(LanguagePackType.UnifiedMessaging));
			InstallableUnitConfigurationInfoManager.InitializeUmLanguagePacksConfigurationInfo(this.InstalledUMLanguagePacks.ToArray());
			if (!this.ParsedArguments.ContainsKey("sourcedir") && !isCleanMachine)
			{
				string path = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{4934D1EA-BE46-48B1-8847-F1AF20E892C1}", "InstallSource", null);
				if (!Directory.Exists(path))
				{
					path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
				}
				LongPath value;
				if (LongPath.TryParse(path, out value))
				{
					this.ParsedArguments["sourcedir"] = value;
				}
			}
			if (this.ParsedArguments.ContainsKey("sourcedir"))
			{
				SetupLogger.Log(Strings.SetupSourceDirectory(((LongPath)this.ParsedArguments["sourcedir"]).PathName));
			}
			this.RunningVersion = ConfigurationContext.Setup.GetExecutingVersion();
			if (!this.IsCleanMachine)
			{
				this.UnpackedRoles = unpackedRoles;
			}
			this.InstalledRolesLocal.AddRange(installedRolesLocal);
			this.PartiallyConfiguredRoles.AddRange(partiallyConfiguredRoles);
			if (this.IsUnpacked("AdminToolsRole"))
			{
				this.InstalledRolesAD.Add(RoleManager.GetRoleByName("AdminToolsRole"));
			}
			this.IsDatacenter = Datacenter.IsMicrosoftHostedOnly(false);
			this.TreatPreReqErrorsAsWarnings = Datacenter.TreatPreReqErrorsAsWarnings(false);
			if (this.IsDatacenter)
			{
				this.IsFfo = DatacenterRegistry.IsForefrontForOffice();
			}
			this.IsDatacenterDedicated = Datacenter.IsDatacenterDedicated(false);
			this.IsPartnerHosted = Datacenter.IsPartnerHostedOnly(false);
			if (!this.IsCleanMachine)
			{
				this.UnpackedDatacenterRoles = unpackedDatacenterRoles;
			}
			if (server != null)
			{
				SetupLogger.Log(Strings.WillGetConfiguredRolesFromServerObject(server.DistinguishedName));
				if (server.IsProvisionedServer)
				{
					SetupLogger.Log(Strings.ServerIsProvisioned);
					this.isProvisionedServer = true;
				}
				if (server.IsHubTransportServer)
				{
					SetupLogger.Log(Strings.RoleInstalledOnServer("BridgeheadRole"));
					this.InstalledRolesAD.Add(RoleManager.GetRoleByName("BridgeheadRole"));
				}
				if ((bool)server[ExchangeServerSchema.IsClientAccessServer])
				{
					SetupLogger.Log(Strings.RoleInstalledOnServer("ClientAccessRole"));
					this.InstalledRolesAD.Add(RoleManager.GetRoleByName("ClientAccessRole"));
				}
				if (server.IsEdgeServer)
				{
					SetupLogger.Log(Strings.RoleInstalledOnServer("GatewayRole"));
					this.InstalledRolesAD.Add(RoleManager.GetRoleByName("GatewayRole"));
				}
				if (server.IsMailboxServer)
				{
					SetupLogger.Log(Strings.RoleInstalledOnServer("MailboxRole"));
					this.InstalledRolesAD.Add(RoleManager.GetRoleByName("MailboxRole"));
				}
				if (server.IsUnifiedMessagingServer)
				{
					SetupLogger.Log(Strings.RoleInstalledOnServer("UnifiedMessagingRole"));
					this.InstalledRolesAD.Add(RoleManager.GetRoleByName("UnifiedMessagingRole"));
				}
				if (server.IsCafeServer)
				{
					SetupLogger.Log(Strings.RoleInstalledOnServer("CafeRole"));
					this.InstalledRolesAD.Add(RoleManager.GetRoleByName("CafeRole"));
				}
				if (server.IsFrontendTransportServer)
				{
					SetupLogger.Log(Strings.RoleInstalledOnServer("FrontendTransportRole"));
					this.InstalledRolesAD.Add(RoleManager.GetRoleByName("FrontendTransportRole"));
				}
			}
			else if (this.IsInstalledLocal("GatewayRole"))
			{
				SetupLogger.Log(Strings.EdgeRoleInstalledButServerObjectNotFound);
				this.InstalledRolesAD.Add(RoleManager.GetRoleByName("GatewayRole"));
			}
			this.ReadBackupRegistry();
			if (this.HasPrepareADParameters || this.HasNewProvisionedServerParameters)
			{
				this.InstallationMode = InstallationModes.Install;
			}
			else if (this.HasRemoveProvisionedServerParameters)
			{
				this.InstallationMode = InstallationModes.Uninstall;
			}
			else if (this.ParsedArguments.ContainsKey("addumlanguagepack"))
			{
				this.InstallationMode = InstallationModes.Install;
				this.IsUmLanguagePackOperation = true;
			}
			else if (this.ParsedArguments.ContainsKey("removeumlanguagepack"))
			{
				this.InstallationMode = InstallationModes.Uninstall;
				this.IsUmLanguagePackOperation = true;
			}
			else
			{
				SetupOperations setupOperations;
				if (this.ParsedArguments.ContainsKey("mode"))
				{
					setupOperations = (SetupOperations)this.ParsedArguments["mode"];
				}
				else
				{
					setupOperations = SetupOperations.Install;
				}
				if (this.IsBackupKeyPresent)
				{
					setupOperations = SetupOperations.Upgrade;
				}
				if ((setupOperations & SetupOperations.Install) != SetupOperations.None)
				{
					this.InstallationMode = InstallationModes.Install;
				}
				else if ((setupOperations & SetupOperations.Uninstall) != SetupOperations.None)
				{
					this.InstallationMode = InstallationModes.Uninstall;
				}
				else if ((setupOperations & SetupOperations.Upgrade) != SetupOperations.None)
				{
					this.InstallationMode = InstallationModes.BuildToBuildUpgrade;
				}
				else if ((setupOperations & SetupOperations.RecoverServer) != SetupOperations.None)
				{
					this.InstallationMode = InstallationModes.DisasterRecovery;
				}
			}
			SetupLogger.Log(Strings.InstallationModeSetTo(this.InstallationMode.ToString()));
			if (this.InstallationMode == InstallationModes.Uninstall)
			{
				if (this.RequestedRoles.Count == 0)
				{
					this.RequestedRoles.AddRange(this.UnpackedRoles);
				}
			}
			else if (this.InstallationMode == InstallationModes.BuildToBuildUpgrade)
			{
				this.RequestedRoles.AddRange(this.InstalledRolesAD);
			}
			else if (this.InstallationMode == InstallationModes.DisasterRecovery)
			{
				this.RequestedRoles.AddRange(this.InstalledRolesAD);
			}
			if (this.ParsedArguments.ContainsKey("adamldapport"))
			{
				this.AdamLdapPort = (ushort)this.ParsedArguments["adamldapport"];
			}
			else
			{
				this.AdamLdapPort = 50389;
			}
			if (this.ParsedArguments.ContainsKey("adamsslport"))
			{
				this.AdamSslPort = (ushort)this.ParsedArguments["adamsslport"];
			}
			else
			{
				this.AdamSslPort = 50636;
			}
			if (server != null)
			{
				this.ExchangeServerName = server.Name;
			}
			this.InstallWindowsComponents = this.ParsedArguments.ContainsKey("installwindowscomponents");
			if (this.ParsedArguments.ContainsKey("sourcedir"))
			{
				this.SourceDir = (LongPath)this.ParsedArguments["sourcedir"];
			}
			if (this.ParsedArguments.ContainsKey("updatesdir"))
			{
				this.UpdatesDir = (LongPath)this.ParsedArguments["updatesdir"];
			}
			this.languagePackContext = new LanguagePackContext(this.InstallationMode, this.ParsedArguments.ContainsKey("languagepack"), (LongPath)(this.ParsedArguments.ContainsKey("languagepack") ? this.ParsedArguments["languagepack"] : null), this.IsCleanMachine, this.IsUmLanguagePackOperation, this.SourceDir);
			if (this.OrganizationName == null)
			{
				this.ExchangeOrganizationExists = false;
				if (this.ParsedArguments.ContainsKey("organizationname"))
				{
					this.OrganizationName = this.ParseOrganizationName((string)this.ParsedArguments["organizationname"]);
					SetupLogger.Log(Strings.SettingOrganizationName(this.OrganizationName.EscapedName));
				}
				else
				{
					SetupLogger.Log(Strings.ExchangeOrganizationNameRequired);
				}
			}
			else
			{
				SetupLogger.Log(Strings.ExistingOrganizationName(this.OrganizationName.EscapedName));
				this.ExchangeOrganizationExists = true;
				if (this.ParsedArguments.ContainsKey("organizationname"))
				{
					SetupLogger.LogWarning(Strings.ExchangeOrganizationAlreadyExists(this.OrganizationName.EscapedName, this.ParseOrganizationName((string)this.ParsedArguments["organizationname"]).EscapedName));
				}
			}
			this.ActiveDirectorySplitPermissions = (bool?)(this.ParsedArguments.ContainsKey("ActiveDirectorySplitPermissions") ? this.ParsedArguments["ActiveDirectorySplitPermissions"] : null);
			InstallableUnitConfigurationInfo.SetupContext = this;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0000F320 File Offset: 0x0000D520
		private void ReadBackupRegistry()
		{
			this.IsBackupKeyPresent = false;
			this.BackupInstalledPath = null;
			this.BackupInstalledVersion = null;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(RegistryConstants.SetupBackupKey))
			{
				if (registryKey != null)
				{
					this.IsBackupKeyPresent = true;
					string text = null;
					try
					{
						text = "MsiInstallPath";
						string text2 = (string)registryKey.GetValue(text);
						if (!string.IsNullOrEmpty(text2))
						{
							this.BackupInstalledPath = NonRootLocalLongFullPath.Parse(text2);
							SetupLogger.Log(Strings.BackupPath(this.BackupInstalledPath.PathName));
						}
						text = "MsiProductMajor";
						int num = (int)registryKey.GetValue(text, -1);
						text = "MsiProductMinor";
						int num2 = (int)registryKey.GetValue(text, -1);
						text = "MsiBuildMajor";
						int num3 = (int)registryKey.GetValue(text, -1);
						text = "MsiBuildMinor";
						int num4 = (int)registryKey.GetValue(text, -1);
						if (num != -1 && num2 != -1 && num3 != -1 && num4 != -1)
						{
							this.BackupInstalledVersion = new Version(num, num2, num3, num4);
							SetupLogger.Log(Strings.BackupVersion(this.BackupInstalledVersion));
						}
					}
					catch (SecurityException innerException)
					{
						throw new BackupKeyInaccessibleException(RegistryConstants.SetupBackupKey, innerException);
					}
					catch (InvalidCastException innerException2)
					{
						throw new BackupKeyIsWrongTypeException(RegistryConstants.SetupBackupKey, text, innerException2);
					}
				}
			}
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0000F4B4 File Offset: 0x0000D6B4
		private static void InitializeAD(out bool adInitPassed, out LocalizedException adError, out string dc, out string gc, ref bool isE12Schema, ref bool schemaUpdateRequired, ref bool orgConfigUpdateRequired, ref bool domainConfigUpdateRequired, Dictionary<string, object> parsedArguments)
		{
			SetupLogger.TraceEnter(new object[0]);
			adInitPassed = false;
			adError = null;
			dc = null;
			gc = null;
			SetupLogger.Log(Strings.ChoosingDomainController);
			try
			{
				ADServer adserver = null;
				bool flag = false;
				if (ADSession.IsBoundToAdam)
				{
					throw new ADInitializationException(Strings.ADDriverBoundToAdam);
				}
				ADServerSettings adserverSettings = SetupServerSettings.CreateSetupServerSettings();
				ADSessionSettings.SetProcessADContext(new ADDriverContext(adserverSettings, ContextMode.Setup));
				if (parsedArguments.ContainsKey("domaincontroller"))
				{
					string text = (string)parsedArguments["domaincontroller"];
					SetupLogger.Log(Strings.DCAlreadySpecified(text));
					adserver = DirectoryUtilities.DomainControllerFromName(text);
					if (adserver == null)
					{
						throw new ADInitializationException(Strings.UserSpecifiedDCDoesNotExistException(text));
					}
					if (!parsedArguments.ContainsKey("preparedomain") && !parsedArguments.ContainsKey("preparealldomains") && !DirectoryUtilities.InLocalDomain(adserver))
					{
						throw new ADInitializationException(Strings.UserSpecifiedDCIsNotInLocalDomainException(adserver.DnsHostName));
					}
					if (!adserver.IsAvailable())
					{
						throw new ADInitializationException(Strings.UserSpecifiedDCIsNotAvailableException(adserver.DnsHostName));
					}
					flag = true;
				}
				else if (!string.IsNullOrEmpty(ADSession.GetSharedConfigDC()))
				{
					string sharedConfigDC = ADSession.GetSharedConfigDC();
					SetupLogger.Log(Strings.PersistedDomainController(sharedConfigDC));
					adserver = DirectoryUtilities.DomainControllerFromName(sharedConfigDC);
					if (adserver != null)
					{
						if (!DirectoryUtilities.InLocalDomain(adserver))
						{
							SetupLogger.Log(Strings.DCNotInLocalDomain(adserver.DnsHostName));
							adserver = null;
						}
						else if (!adserver.IsAvailable())
						{
							SetupLogger.Log(Strings.DCNotResponding(adserver.DnsHostName));
							adserver = null;
						}
					}
					else
					{
						SetupLogger.Log(Strings.DCNameNotValid(sharedConfigDC));
					}
				}
				if (adserver == null)
				{
					SetupLogger.Log(Strings.PickingDomainController);
					adserver = DirectoryUtilities.PickLocalDomainController();
					SetupLogger.Log(Strings.DomainControllerChosen(adserver.DnsHostName));
				}
				int num;
				if (parsedArguments.ContainsKey("prepareschema") && DirectoryUtilities.TryGetSchemaVersionRangeUpper(adserver.DnsHostName, out num) && num > 15312)
				{
					throw new ADInitializationException(Strings.ADSchemaVersionHigherThanSetupException(num, 15312));
				}
				int num2;
				if (parsedArguments.ContainsKey("preparedomain") && DirectoryUtilities.TryGetLocalDomainConfigVersion(out num2) && num2 > MesoContainer.DomainPrepVersion)
				{
					throw new ADInitializationException(Strings.ADDomainConfigVersionHigherThanSetupException(num2, MesoContainer.DomainPrepVersion));
				}
				int num3;
				if (parsedArguments.ContainsKey("preparead") && DirectoryUtilities.TryGetOrgConfigVersion(adserver.DnsHostName, out num3) && num3 > Organization.OrgConfigurationVersion)
				{
					throw new ADInitializationException(Strings.ADOrgConfigVersionHigherThanSetupException(num3, Organization.OrgConfigurationVersion));
				}
				ADSchemaVersion schemaVersion = DirectoryUtilities.GetSchemaVersion(adserver.DnsHostName);
				isE12Schema = (schemaVersion >= ADSchemaVersion.Exchange2007Rtm);
				schemaUpdateRequired = !DirectoryUtilities.IsSchemaUpToDate(adserver.DnsHostName);
				orgConfigUpdateRequired = !DirectoryUtilities.IsOrgConfigUpToDate(adserver.DnsHostName);
				domainConfigUpdateRequired = !DirectoryUtilities.IsLocalDomainConfigUpToDate();
				if (!schemaUpdateRequired && !orgConfigUpdateRequired)
				{
					dc = adserver.DnsHostName;
					SetupLogger.Log(Strings.ForestPrepAlreadyRun(dc));
				}
				else
				{
					ADServer schemaMasterDomainController;
					try
					{
						schemaMasterDomainController = DirectoryUtilities.GetSchemaMasterDomainController();
					}
					catch (SchemaMasterDCNotFoundException innerException)
					{
						throw new ADInitializationException(Strings.SchemaMasterDCNotFoundException, innerException);
					}
					SetupLogger.Log(Strings.ForestPrepNotRun(schemaMasterDomainController.DnsHostName));
					if (flag && !adserver.Id.Equals(schemaMasterDomainController.Id))
					{
						throw new ADInitializationException(Strings.UserSpecifiedDCIsNotSchemaMasterException(adserver.DnsHostName));
					}
					bool flag2 = schemaMasterDomainController.IsAvailable();
					if (flag2)
					{
						SetupLogger.Log(Strings.SchemaMasterAvailable);
					}
					else
					{
						SetupLogger.Log(Strings.SchemaMasterNotAvailable);
					}
					if (DirectoryUtilities.InSameDomain(adserver, schemaMasterDomainController) && DirectoryUtilities.InSameSite(adserver, schemaMasterDomainController))
					{
						if (!flag2)
						{
							throw new ADInitializationException(Strings.SchemaMasterDCNotAvailableException(schemaMasterDomainController.DnsHostName));
						}
						dc = schemaMasterDomainController.DnsHostName;
						SetupLogger.Log(Strings.SchemaMasterIsLocalDC(dc));
						schemaUpdateRequired = !DirectoryUtilities.IsSchemaUpToDate(schemaMasterDomainController.DnsHostName);
						orgConfigUpdateRequired = !DirectoryUtilities.IsOrgConfigUpToDate(schemaMasterDomainController.DnsHostName);
					}
					else
					{
						if (!flag2)
						{
							throw new ADInitializationException(Strings.ForestPrepNotRunOrNotReplicatedException(schemaMasterDomainController.DnsHostName));
						}
						if (DirectoryUtilities.IsSchemaUpToDate(schemaMasterDomainController.DnsHostName) && DirectoryUtilities.IsOrgConfigUpToDate(schemaMasterDomainController.DnsHostName))
						{
							throw new ADInitializationException(Strings.WaitForForestPrepReplicationToLocalDomainException);
						}
						throw new ADInitializationException(Strings.RunForestPrepInSchemaMasterDomainException(schemaMasterDomainController.DomainId.Name, schemaMasterDomainController.Site.Name));
					}
				}
				SetupLogger.Log(Strings.ChoosingGlobalCatalog);
				gc = DirectoryUtilities.PickGlobalCatalog(dc).DnsHostName;
				SetupLogger.Log(Strings.GCChosen(gc));
				adserverSettings.SetConfigurationDomainController(TopologyProvider.LocalForestFqdn, new Fqdn(dc));
				adserverSettings.SetPreferredGlobalCatalog(TopologyProvider.LocalForestFqdn, new Fqdn(gc));
				adserverSettings.AddPreferredDC(new Fqdn(dc));
				ADSessionSettings.SetProcessADContext(new ADDriverContext(adserverSettings, ContextMode.Setup));
				adInitPassed = true;
			}
			catch (DataSourceOperationException ex)
			{
				adError = new ADInitializationException(ex.LocalizedString, ex);
			}
			catch (DataSourceTransientException ex2)
			{
				adError = new ADInitializationException(ex2.LocalizedString, ex2);
			}
			catch (DataValidationException ex3)
			{
				adError = new ADInitializationException(ex3.LocalizedString, ex3);
			}
			catch (ADInitializationException ex4)
			{
				adError = ex4;
			}
			finally
			{
				if (adError != null)
				{
					SetupLogger.LogError(adError);
				}
				SetupLogger.TraceExit();
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000F9C8 File Offset: 0x0000DBC8
		public IOrganizationName ParseOrganizationName(string name)
		{
			try
			{
				this.OrganizationName = new OrganizationName(name);
			}
			catch (FormatException innerException)
			{
				throw new InvalidOrganizationNameException(name, innerException);
			}
			return this.OrganizationName;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0000FA04 File Offset: 0x0000DC04
		public static object ParseFqdnForPrepareLegacyPermissions(string fqdn)
		{
			if (string.IsNullOrEmpty(fqdn) || SmtpAddress.IsValidDomain(fqdn))
			{
				return fqdn;
			}
			throw new InvalidFqdnException(fqdn);
		}

		// Token: 0x04000147 RID: 327
		private const string watsonConfigurationKey = "HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\ExchangeServer\\v15";

		// Token: 0x04000148 RID: 328
		private const string watsonConfigurationName = "DisableErrorReporting";

		// Token: 0x04000149 RID: 329
		private LongPath sourceDir;

		// Token: 0x0400014A RID: 330
		private bool isProvisionedServer;

		// Token: 0x0400014B RID: 331
		private LanguagePackContext languagePackContext;
	}
}
