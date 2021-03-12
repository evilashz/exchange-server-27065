using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Setup.Parser;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000035 RID: 53
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class InstallModeDataHandler : ModeDataHandler
	{
		// Token: 0x060001FC RID: 508 RVA: 0x00007BE8 File Offset: 0x00005DE8
		public InstallModeDataHandler(ISetupContext setupContext, MonadConnection connection) : base(setupContext, connection)
		{
			SetupLogger.Log(Strings.ApplyingDefaultRoleSelectionState);
			foreach (Role role in base.SetupContext.RequestedRoles)
			{
				bool flag = true;
				if (base.SetupContext.IsInstalledLocal(role.RoleName))
				{
					flag = false;
				}
				string roleName;
				switch (roleName = role.RoleName)
				{
				case "BridgeheadRole":
					base.SetIsBridgeheadCheckedInternal(flag);
					break;
				case "ClientAccessRole":
					base.SetIsClientAccessCheckedInternal(flag);
					break;
				case "GatewayRole":
					base.SetIsGatewayCheckedInternal(flag);
					break;
				case "MailboxRole":
					base.SetIsMailboxCheckedInternal(flag);
					break;
				case "UnifiedMessagingRole":
					base.SetIsUnifiedMessagingCheckedInternal(flag);
					break;
				case "FrontendTransportRole":
					base.SetIsFrontendTransportCheckedInternal(flag);
					break;
				case "CafeRole":
					base.SetIsCafeCheckedInternal(flag);
					break;
				case "AdminToolsRole":
					base.SetIsAdminToolsCheckedInternal(flag);
					break;
				case "CentralAdminRole":
					base.SetIsCentralAdminCheckedInternal(flag);
					break;
				case "CentralAdminDatabaseRole":
					base.SetIsCentralAdminDatabaseCheckedInternal(flag);
					break;
				case "CentralAdminFrontEndRole":
					base.SetIsCentralAdminFrontEndCheckedInternal(flag);
					break;
				case "MonitoringRole":
					base.SetIsMonitoringCheckedInternal(flag);
					break;
				case "OSPRole":
					base.SetIsOSPCheckedInternal(true);
					break;
				}
			}
			this.SetPartiallyConfiguredRoleChecked();
			if (base.CanChangeInstallationPath && base.SetupContext.ParsedArguments.ContainsKey("targetdir"))
			{
				base.InstallationPath = (NonRootLocalLongFullPath)base.SetupContext.ParsedArguments["targetdir"];
				SetupLogger.Log(Strings.UserSpecifiedTargetDir(base.InstallationPath.PathName));
			}
			this.watsonEnabled = base.SetupContext.WatsonEnabled;
			this.hasRolesToInstall = base.SetupContext.HasRolesToInstall;
			if (base.SetupContext.ParsedArguments.ContainsKey("customerfeedbackenabled"))
			{
				this.CustomerFeedbackEnabled = new bool?((bool)base.SetupContext.ParsedArguments["customerfeedbackenabled"]);
			}
			base.SetupContext.Industry = IndustryType.NotSpecified;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00007ED0 File Offset: 0x000060D0
		public static object ParseFqdn(string fqdnStr)
		{
			Fqdn result;
			try
			{
				result = Fqdn.Parse(fqdnStr);
			}
			catch (FormatException innerException)
			{
				throw new ParseException(Strings.NotAValidFqdn(fqdnStr), innerException);
			}
			return result;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00007F08 File Offset: 0x00006108
		public override List<string> SelectedInstallableUnits
		{
			get
			{
				base.SelectedInstallableUnits.Clear();
				bool flag = false;
				if (base.IsAdminToolsChecked)
				{
					Role roleByName = RoleManager.GetRoleByName("AdminToolsRole");
					if (this.IsAdminToolsEnabled || !roleByName.IsInstalled)
					{
						base.SelectedInstallableUnits.Add("AdminToolsRole");
						flag = true;
					}
				}
				if (this.IsBridgeheadEnabled && base.IsBridgeheadChecked)
				{
					base.SelectedInstallableUnits.Add("BridgeheadRole");
					flag = true;
				}
				if (this.IsClientAccessEnabled && base.IsClientAccessChecked)
				{
					base.SelectedInstallableUnits.Add("ClientAccessRole");
					flag = true;
				}
				if (this.IsGatewayEnabled && base.IsGatewayChecked)
				{
					base.SelectedInstallableUnits.Add("GatewayRole");
					flag = true;
				}
				if (this.IsUnifiedMessagingEnabled && base.IsUnifiedMessagingChecked)
				{
					base.SelectedInstallableUnits.Add("UnifiedMessagingRole");
					flag = true;
				}
				if (this.IsMailboxEnabled && base.IsMailboxChecked)
				{
					base.SelectedInstallableUnits.Add("MailboxRole");
					flag = true;
				}
				if (this.IsFrontendTransportEnabled && base.IsFrontendTransportChecked)
				{
					base.SelectedInstallableUnits.Add("FrontendTransportRole");
					flag = true;
				}
				if (this.IsCafeEnabled && base.IsCafeChecked)
				{
					base.SelectedInstallableUnits.Add("CafeRole");
					flag = true;
				}
				if (this.IsCentralAdminDatabaseEnabled && base.IsCentralAdminDatabaseChecked)
				{
					base.SelectedInstallableUnits.Add("CentralAdminDatabaseRole");
					flag = true;
				}
				if (this.IsCentralAdminEnabled && base.IsCentralAdminChecked)
				{
					base.SelectedInstallableUnits.Add("CentralAdminRole");
					flag = true;
				}
				if (this.IsCentralAdminFrontEndEnabled && base.IsCentralAdminFrontEndChecked)
				{
					base.SelectedInstallableUnits.Add("CentralAdminFrontEndRole");
					flag = true;
				}
				if (this.IsMonitoringEnabled && base.IsMonitoringChecked)
				{
					base.SelectedInstallableUnits.Add("MonitoringRole");
					flag = true;
				}
				if ((flag || base.SetupContext.IsLanguagePackOperation) && base.IsLanguagePacksChecked && base.SetupContext.NeedToUpdateLanguagePacks)
				{
					base.SelectedInstallableUnits.Insert(0, "LanguagePacks");
				}
				if (this.IsOSPEnabled && base.IsOSPChecked)
				{
					base.SelectedInstallableUnits.Add("OSPRole");
				}
				return base.SelectedInstallableUnits;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001FF RID: 511 RVA: 0x0000812B File Offset: 0x0000632B
		public override InstallationModes Mode
		{
			get
			{
				return InstallationModes.Install;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000200 RID: 512 RVA: 0x0000812E File Offset: 0x0000632E
		public override PreCheckDataHandler PreCheckDataHandler
		{
			get
			{
				if (this.preCheckDataHandler == null)
				{
					this.preCheckDataHandler = new InstallPreCheckDataHandler(base.SetupContext, this, base.Connection);
				}
				return this.preCheckDataHandler;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000201 RID: 513 RVA: 0x00008156 File Offset: 0x00006356
		protected override bool NeedPrePostSetupDataHandlers
		{
			get
			{
				return base.NeedPrePostSetupDataHandlers && !base.SetupContext.HasPrepareADParameters && !base.SetupContext.HasNewProvisionedServerParameters;
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008180 File Offset: 0x00006380
		protected override bool NeedFileDataHandler()
		{
			if (!base.IsLanguagePackOnlyInstallation)
			{
				return base.NeedFileDataHandler() && !base.SetupContext.HasPrepareADParameters && !base.SetupContext.HasNewProvisionedServerParameters && !this.RequestedRolesAreAllUnpacked();
			}
			return base.NeedFileDataHandler() && !base.SetupContext.HasPrepareADParameters && !base.SetupContext.HasNewProvisionedServerParameters;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000081E8 File Offset: 0x000063E8
		private bool RequestedRolesAreAllUnpacked()
		{
			foreach (string roleName in this.SelectedInstallableUnits)
			{
				Role roleByName = RoleManager.GetRoleByName(roleName);
				if (roleByName != null && (!base.SetupContext.UnpackedRoles.Contains(roleByName) || (base.SetupContext.IsDatacenter && !base.SetupContext.UnpackedDatacenterRoles.Contains(roleByName))))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00008278 File Offset: 0x00006478
		protected override void AddNeededFileDataHandlers()
		{
			base.AddNeededFileDataHandlers();
			if (this.preFileCopyDataHandler == null)
			{
				this.preFileCopyDataHandler = new SetupBindingTaskDataHandler(BindingCategory.PreFileCopy, base.SetupContext, base.Connection);
			}
			this.preFileCopyDataHandler.Mode = this.Mode;
			this.preFileCopyDataHandler.PreviousVersion = base.PreviousVersion;
			this.preFileCopyDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
			base.DataHandlers.Add(this.preFileCopyDataHandler);
			if (!base.IsLanguagePackOnlyInstallation)
			{
				if (this.installFileDataHandler == null)
				{
					this.installFileDataHandler = new InstallFileDataHandler(base.SetupContext, new ProductMsiConfigurationInfo(), base.Connection);
				}
				this.installFileDataHandler.SelectedInstallableUnits.Clear();
				foreach (string text in this.SelectedInstallableUnits.ToArray())
				{
					Role roleByName = RoleManager.GetRoleByName(text);
					if (roleByName == null || !RoleManager.GetRoleByName(text).IsDatacenterOnly)
					{
						this.installFileDataHandler.SelectedInstallableUnits.Add(text);
					}
				}
				this.installFileDataHandler.TargetDirectory = base.InstallationPath.PathName;
				this.installFileDataHandler.WatsonEnabled = this.WatsonEnabled;
				base.DataHandlers.Add(this.installFileDataHandler);
			}
			if (base.SetupContext.IsDatacenter || base.SetupContext.IsDatacenterDedicated)
			{
				InstallFileDataHandler installFileDataHandler = new InstallFileDataHandler(base.SetupContext, new DatacenterMsiConfigurationInfo(), base.Connection);
				installFileDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
				installFileDataHandler.TargetDirectory = base.InstallationPath.PathName;
				base.DataHandlers.Add(installFileDataHandler);
			}
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000840C File Offset: 0x0000660C
		private void AddOrgLevelConfigurationDataHandlers()
		{
			if (!this.SelectedInstallableUnits.Contains("GatewayRole"))
			{
				if (base.SetupContext.HasNewProvisionedServerParameters)
				{
					if (this.provisionServerDataHandler == null)
					{
						this.provisionServerDataHandler = new ProvisionServerDataHandler(base.SetupContext, base.Connection);
					}
					this.provisionServerDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
					base.DataHandlers.Add(this.provisionServerDataHandler);
					return;
				}
				if (base.OrgLevelConfigRequired())
				{
					base.DataHandlers.Add(this.InstallOrgCfgDataHandler);
				}
			}
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00008494 File Offset: 0x00006694
		protected override ConfigurationDataHandler GetInstallableUnitConfigurationDataHandler(string installableUnitName)
		{
			ConfigurationDataHandler result = null;
			switch (installableUnitName)
			{
			case "BridgeheadRole":
				return this.InstallBhdCfgDataHandler;
			case "ClientAccessRole":
				return this.InstallCacCfgDataHandler;
			case "GatewayRole":
				return this.InstallGwyCfgDataHandler;
			case "MailboxRole":
				return this.InstallMbxCfgDataHandler;
			case "UnifiedMessagingRole":
				return this.InstallUMCfgDataHandler;
			case "FrontendTransportRole":
				return this.InstallFetCfgDataHandler;
			case "CafeRole":
				return this.InstallCafeCfgDataHandler;
			case "AdminToolsRole":
				return this.InstallAtCfgDataHandler;
			case "LanguagePacks":
				return this.AddLanguagePackCfgDataHandler;
			case "CentralAdminRole":
				return this.InstallCentralAdminCfgDataHandler;
			case "CentralAdminDatabaseRole":
				return this.InstallCentralAdminDatabaseCfgDataHandler;
			case "CentralAdminFrontEndRole":
				return this.InstallCentralAdminFrontEndCfgDataHandler;
			case "MonitoringRole":
				return this.InstallMonitoringCfgDataHandler;
			case "OSPRole":
				return this.InstallOSPCfgDataHandler;
			}
			if (InstallableUnitConfigurationInfoManager.IsUmLanguagePackInstallableUnit(installableUnitName))
			{
				InstallableUnitConfigurationInfo installableUnitConfigurationInfoByName = InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(installableUnitName);
				UmLanguagePackConfigurationInfo umLanguagePackConfigurationInfo = installableUnitConfigurationInfoByName as UmLanguagePackConfigurationInfo;
				AddUmLanguagePackCfgDataHandler addUmLanguagePackCfgDataHandler;
				if (!this.addUmLanguagePackDataHandlers.TryGetValue(installableUnitName, out addUmLanguagePackCfgDataHandler))
				{
					addUmLanguagePackCfgDataHandler = new AddUmLanguagePackCfgDataHandler(base.SetupContext, base.Connection, umLanguagePackConfigurationInfo.Culture, base.UmSourceDir);
					this.addUmLanguagePackDataHandlers.Add(installableUnitName, addUmLanguagePackCfgDataHandler);
				}
				result = addUmLanguagePackCfgDataHandler;
			}
			return result;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000086B9 File Offset: 0x000068B9
		public InstallOrgCfgDataHandler InstallOrgCfgDataHandler
		{
			get
			{
				if (this.installOrgCfgDataHandler == null)
				{
					this.installOrgCfgDataHandler = new InstallOrgCfgDataHandler(base.SetupContext, base.Connection);
				}
				this.installOrgCfgDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
				return this.installOrgCfgDataHandler;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000208 RID: 520 RVA: 0x000086F1 File Offset: 0x000068F1
		public InstallBhdCfgDataHandler InstallBhdCfgDataHandler
		{
			get
			{
				if (this.installBhdCfgDataHandler == null)
				{
					this.installBhdCfgDataHandler = new InstallBhdCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.installBhdCfgDataHandler;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00008718 File Offset: 0x00006918
		public InstallCacCfgDataHandler InstallCacCfgDataHandler
		{
			get
			{
				if (this.installCacCfgDataHandler == null)
				{
					this.installCacCfgDataHandler = new InstallCacCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.installCacCfgDataHandler;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000873F File Offset: 0x0000693F
		public InstallGwyCfgDataHandler InstallGwyCfgDataHandler
		{
			get
			{
				if (this.installGwyCfgDataHandler == null)
				{
					this.installGwyCfgDataHandler = new InstallGwyCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.installGwyCfgDataHandler;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00008766 File Offset: 0x00006966
		public InstallMbxCfgDataHandler InstallMbxCfgDataHandler
		{
			get
			{
				if (this.installMbxCfgDataHandler == null)
				{
					this.installMbxCfgDataHandler = new InstallMbxCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.installMbxCfgDataHandler;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000878D File Offset: 0x0000698D
		public InstallUMCfgDataHandler InstallUMCfgDataHandler
		{
			get
			{
				if (this.installUMCfgDataHandler == null)
				{
					this.installUMCfgDataHandler = new InstallUMCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.installUMCfgDataHandler;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600020D RID: 525 RVA: 0x000087B4 File Offset: 0x000069B4
		public InstallFetCfgDataHandler InstallFetCfgDataHandler
		{
			get
			{
				if (this.installFetCfgDataHandler == null)
				{
					this.installFetCfgDataHandler = new InstallFetCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.installFetCfgDataHandler;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600020E RID: 526 RVA: 0x000087DB File Offset: 0x000069DB
		public InstallCafeCfgDataHandler InstallCafeCfgDataHandler
		{
			get
			{
				if (this.installCafeCfgDataHandler == null)
				{
					this.installCafeCfgDataHandler = new InstallCafeCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.installCafeCfgDataHandler;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x0600020F RID: 527 RVA: 0x00008802 File Offset: 0x00006A02
		public InstallAtCfgDataHandler InstallAtCfgDataHandler
		{
			get
			{
				if (this.installAtCfgDataHandler == null)
				{
					this.installAtCfgDataHandler = new InstallAtCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.installAtCfgDataHandler;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000210 RID: 528 RVA: 0x00008829 File Offset: 0x00006A29
		public InstallCentralAdminDatabaseCfgDataHandler InstallCentralAdminDatabaseCfgDataHandler
		{
			get
			{
				if (this.installCentralAdminDatabaseCfgDataHandler == null)
				{
					this.installCentralAdminDatabaseCfgDataHandler = new InstallCentralAdminDatabaseCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.installCentralAdminDatabaseCfgDataHandler;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000211 RID: 529 RVA: 0x00008850 File Offset: 0x00006A50
		public InstallCentralAdminCfgDataHandler InstallCentralAdminCfgDataHandler
		{
			get
			{
				if (this.installCentralAdminCfgDataHandler == null)
				{
					this.installCentralAdminCfgDataHandler = new InstallCentralAdminCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.installCentralAdminCfgDataHandler;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000212 RID: 530 RVA: 0x00008877 File Offset: 0x00006A77
		public InstallCentralAdminFrontEndCfgDataHandler InstallCentralAdminFrontEndCfgDataHandler
		{
			get
			{
				if (this.installCentralAdminFrontEndCfgDataHandler == null)
				{
					this.installCentralAdminFrontEndCfgDataHandler = new InstallCentralAdminFrontEndCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.installCentralAdminFrontEndCfgDataHandler;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000889E File Offset: 0x00006A9E
		public InstallMonitoringCfgDataHandler InstallMonitoringCfgDataHandler
		{
			get
			{
				if (this.installMonitoringCfgDataHandler == null)
				{
					this.installMonitoringCfgDataHandler = new InstallMonitoringCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.installMonitoringCfgDataHandler;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000214 RID: 532 RVA: 0x000088C5 File Offset: 0x00006AC5
		public AddLanguagePackCfgDataHandler AddLanguagePackCfgDataHandler
		{
			get
			{
				if (this.addLanguagePackCfgDataHandler == null)
				{
					this.addLanguagePackCfgDataHandler = new AddLanguagePackCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.addLanguagePackCfgDataHandler;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000215 RID: 533 RVA: 0x000088EC File Offset: 0x00006AEC
		public InstallOSPCfgDataHandler InstallOSPCfgDataHandler
		{
			get
			{
				if (this.installOSPCfgDataHandler == null)
				{
					this.installOSPCfgDataHandler = new InstallOSPCfgDataHandler(base.SetupContext, base.Connection);
				}
				return this.installOSPCfgDataHandler;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00008913 File Offset: 0x00006B13
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000891C File Offset: 0x00006B1C
		public bool TypicalInstallation
		{
			get
			{
				return this.typicalInstallation;
			}
			set
			{
				if (value || value != this.typicalInstallation)
				{
					this.typicalInstallation = value;
					base.IsBridgeheadChecked = this.TypicalInstallation;
					base.IsMailboxChecked = this.TypicalInstallation;
					base.IsGatewayChecked = false;
					base.IsUnifiedMessagingChecked = false;
					base.IsClientAccessChecked = this.TypicalInstallation;
					base.IsAdminToolsChecked = this.TypicalInstallation;
					base.IsLanguagePacksChecked = this.TypicalInstallation;
				}
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000218 RID: 536 RVA: 0x00008986 File Offset: 0x00006B86
		public override bool IsBridgeheadEnabled
		{
			get
			{
				return (!base.SetupContext.IsInstalledLocalOrAD("BridgeheadRole") | base.SetupContext.IsPartiallyConfigured("BridgeheadRole")) && !base.IsGatewayChecked;
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000219 RID: 537 RVA: 0x000089B9 File Offset: 0x00006BB9
		public override bool IsClientAccessEnabled
		{
			get
			{
				return (!base.SetupContext.IsInstalledLocalOrAD("ClientAccessRole") | base.SetupContext.IsPartiallyConfigured("ClientAccessRole")) && !base.IsGatewayChecked;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600021A RID: 538 RVA: 0x000089EC File Offset: 0x00006BEC
		public override bool IsGatewayEnabled
		{
			get
			{
				return (!base.SetupContext.IsInstalledLocalOrAD("GatewayRole") | base.SetupContext.IsPartiallyConfigured("GatewayRole")) && !base.IsBridgeheadChecked && !base.IsClientAccessChecked && !base.IsMailboxChecked && !base.IsUnifiedMessagingChecked;
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600021B RID: 539 RVA: 0x00008A42 File Offset: 0x00006C42
		public override bool IsMailboxEnabled
		{
			get
			{
				return (!base.SetupContext.IsInstalledLocalOrAD("MailboxRole") | base.SetupContext.IsPartiallyConfigured("MailboxRole")) && !base.IsGatewayChecked;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600021C RID: 540 RVA: 0x00008A75 File Offset: 0x00006C75
		public override bool IsUnifiedMessagingEnabled
		{
			get
			{
				return (!base.SetupContext.IsInstalledLocalOrAD("UnifiedMessagingRole") | base.SetupContext.IsPartiallyConfigured("UnifiedMessagingRole")) && !base.IsGatewayChecked;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600021D RID: 541 RVA: 0x00008AA8 File Offset: 0x00006CA8
		public override bool IsFrontendTransportEnabled
		{
			get
			{
				return (!base.SetupContext.IsInstalledLocal("FrontendTransportRole") | base.SetupContext.IsPartiallyConfigured("FrontendTransportRole")) && !base.IsGatewayChecked;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600021E RID: 542 RVA: 0x00008ADB File Offset: 0x00006CDB
		public override bool IsCentralAdminEnabled
		{
			get
			{
				return (!base.SetupContext.IsInstalledLocal("CentralAdminRole") | base.SetupContext.IsPartiallyConfigured("CentralAdminRole")) && !base.IsGatewayChecked;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00008B0E File Offset: 0x00006D0E
		public override bool IsCentralAdminDatabaseEnabled
		{
			get
			{
				return (!base.SetupContext.IsInstalledLocal("CentralAdminDatabaseRole") | base.SetupContext.IsPartiallyConfigured("CentralAdminDatabaseRole")) && !base.IsGatewayChecked;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000220 RID: 544 RVA: 0x00008B41 File Offset: 0x00006D41
		public override bool IsCentralAdminFrontEndEnabled
		{
			get
			{
				return (!base.SetupContext.IsInstalledLocal("CentralAdminFrontEndRole") | base.SetupContext.IsPartiallyConfigured("CentralAdminFrontEndRole")) && !base.IsGatewayChecked;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00008B74 File Offset: 0x00006D74
		public override bool IsMonitoringEnabled
		{
			get
			{
				return (!base.SetupContext.IsInstalledLocal("MonitoringRole") | base.SetupContext.IsPartiallyConfigured("MonitoringRole")) && !base.IsGatewayChecked;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000222 RID: 546 RVA: 0x00008BA7 File Offset: 0x00006DA7
		public override bool IsCafeEnabled
		{
			get
			{
				return (!base.SetupContext.IsInstalledLocal("CafeRole") | base.SetupContext.IsPartiallyConfigured("CafeRole")) && !base.IsGatewayChecked;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000223 RID: 547 RVA: 0x00008BDA File Offset: 0x00006DDA
		public override bool IsAdminToolsEnabled
		{
			get
			{
				return !base.SetupContext.IsInstalledLocal("AdminToolsRole") && !base.IsMailboxChecked && !base.IsClientAccessChecked && !base.IsBridgeheadChecked && !base.IsGatewayChecked && !base.IsUnifiedMessagingChecked;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000224 RID: 548 RVA: 0x00008C19 File Offset: 0x00006E19
		public override bool IsOSPEnabled
		{
			get
			{
				return (!base.SetupContext.IsInstalledLocal("OSPRole") | base.SetupContext.IsPartiallyConfigured("OSPRole")) && !base.IsGatewayChecked;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000225 RID: 549 RVA: 0x00008C4C File Offset: 0x00006E4C
		public override bool CanChangeSourceDir
		{
			get
			{
				return !base.SetupContext.IsCleanMachine;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000226 RID: 550 RVA: 0x00008C5C File Offset: 0x00006E5C
		public override string RoleSelectionDescription
		{
			get
			{
				return Strings.AddServerRoleText;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000227 RID: 551 RVA: 0x00008C68 File Offset: 0x00006E68
		public override LocalizedString ConfigurationSummary
		{
			get
			{
				if (this.hasRolesToInstall)
				{
					return Strings.AddRolesToInstall;
				}
				if (base.IsLanguagePackOnlyInstallation)
				{
					return Strings.LanguagePacksToInstall;
				}
				if (base.SetupContext.IsLanguagePackOperation)
				{
					return Strings.LanguagePacksUpToDate;
				}
				return Strings.NoServerRolesToInstall;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000228 RID: 552 RVA: 0x00008CA0 File Offset: 0x00006EA0
		public override string CompletionDescription
		{
			get
			{
				if (base.IsLanguagePackOnlyInstallation)
				{
					return base.WorkUnits.HasFailures ? Strings.AddLanguagePacksFailText : Strings.AddLanguagePacksSuccessText;
				}
				return base.WorkUnits.HasFailures ? Strings.AddFailText : Strings.AddSuccessText;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000229 RID: 553 RVA: 0x00008CF2 File Offset: 0x00006EF2
		public override string CompletionStatus
		{
			get
			{
				return base.WorkUnits.HasFailures ? Strings.AddFailStatus : Strings.AddSuccessStatus;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00008D12 File Offset: 0x00006F12
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00008D1A File Offset: 0x00006F1A
		public bool WatsonEnabled
		{
			get
			{
				return this.watsonEnabled;
			}
			set
			{
				this.watsonEnabled = value;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00008D23 File Offset: 0x00006F23
		public bool WillSetGlobalCEIP
		{
			get
			{
				return base.SetupContext.ParsedArguments.ContainsKey("preparead") || !base.ExchangeOrganizationExists || (base.SetupContext.IsOrgConfigUpdateRequired && !base.SetupContext.HasE14OrLaterServers);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00008D63 File Offset: 0x00006F63
		public LocalizedString OptDescription
		{
			get
			{
				if (this.WillSetGlobalCEIP)
				{
					return Strings.GlobalOptDescriptionText;
				}
				return Strings.ServerOptDescriptionText;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00008D78 File Offset: 0x00006F78
		public IndustryType Industry
		{
			get
			{
				return base.SetupContext.Industry;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x0600022F RID: 559 RVA: 0x00008D85 File Offset: 0x00006F85
		public bool? OriginalGlobalCustomerFeedbackEnabled
		{
			get
			{
				return base.SetupContext.OriginalGlobalCustomerFeedbackEnabled;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00008D94 File Offset: 0x00006F94
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00008DF0 File Offset: 0x00006FF0
		public bool? CustomerFeedbackEnabled
		{
			get
			{
				if (this.WillSetGlobalCEIP)
				{
					return base.SetupContext.GlobalCustomerFeedbackEnabled;
				}
				if (base.SetupContext.ServerCustomerFeedbackEnabled == null && base.SetupContext.IsCleanMachine)
				{
					return base.SetupContext.GlobalCustomerFeedbackEnabled;
				}
				return base.SetupContext.ServerCustomerFeedbackEnabled;
			}
			set
			{
				if (this.CustomerFeedbackEnabled != value)
				{
					if (this.WillSetGlobalCEIP)
					{
						base.SetupContext.GlobalCustomerFeedbackEnabled = value;
						base.SetupContext.ServerCustomerFeedbackEnabled = value;
						return;
					}
					base.SetupContext.ServerCustomerFeedbackEnabled = value;
				}
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00008E5C File Offset: 0x0000705C
		public override bool RebootRequired
		{
			get
			{
				if (!base.WorkUnits.IsDataChanged)
				{
					return false;
				}
				bool flag = base.SetupContext.IsUnpacked("AdminToolsRole") && base.SetupContext.UnpackedRoles.Count == 1;
				bool flag2 = (base.SetupContext.IsCleanMachine || flag) && base.ServerRoleIsSelected;
				bool flag3 = this.SelectedInstallableUnits.Contains("MailboxRole") && !base.SetupContext.IsUnpacked("MailboxRole");
				return flag2 || flag3;
			}
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00008EEC File Offset: 0x000070EC
		public override void UpdatePreCheckTaskDataHandler()
		{
			SetupLogger.TraceEnter(new object[0]);
			if (!base.IsLanguagePackOnlyInstallation && this.SelectedInstallableUnits.Contains("AdminToolsRole") && !base.ServerRoleIsSelected)
			{
				PrerequisiteAnalysisTaskDataHandler instance = PrerequisiteAnalysisTaskDataHandler.GetInstance(base.SetupContext, base.Connection);
				instance.AddRoleByUnitName("AdminToolsRole");
				instance.TargetDir = base.SetupContext.TargetDir;
				instance.AddSelectedInstallableUnits(this.SelectedInstallableUnits);
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008F68 File Offset: 0x00007168
		protected override void UpdateDataHandlers()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.DataHandlers.Clear();
			if (!base.IsLanguagePacksChecked && (base.SetupContext.IsCleanMachine || base.SetupContext.IsLanguagePackOperation || base.SetupContext.InstalledLanguagePacks.Count == 0 || base.SetupContext.LanguagePacksToInstall.Keys.Count != 0 || !base.SetupContext.InstalledLanguagePacks.Contains("{DEDFFB64-42EC-4E26-0409-430E86DF378C}") || !base.SetupContext.InstalledLanguagePacks.Contains("{521E6064-B4B1-4CBC-0409-25AD697801FA}")))
			{
				base.SetIsLanguagePacksCheckedInternal(true);
			}
			this.AddOrgLevelConfigurationDataHandlers();
			if (base.DataHandlers.Count > 0 || base.SetupContext.IsCleanMachine)
			{
				ConfigurationContext.Setup.UseAssemblyPathAsInstallPath();
			}
			SetupLogger.Log(Strings.SetupWillRunFromPath(ConfigurationContext.Setup.InstallPath));
			base.AddPreSetupDataHandlers();
			base.AddFileDataHandlers();
			if (base.IsLanguagePacksChecked)
			{
				base.AddLanguagePackFileDataHandlers();
			}
			base.AddPostCopyFileDataHandlers();
			this.AddConfigurationDataHandlers();
			base.AddPostSetupDataHandlers();
			SetupLogger.Log(Strings.InstallModeDataHandlerCount(base.DataHandlers.Count));
			SetupLogger.TraceExit();
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000235 RID: 565 RVA: 0x00009086 File Offset: 0x00007286
		public override bool HasChanges
		{
			get
			{
				return base.SetupContext.HasPrepareADParameters || base.SetupContext.HasNewProvisionedServerParameters || this.SelectedInstallableUnits.Count > 0;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000236 RID: 566 RVA: 0x000090B4 File Offset: 0x000072B4
		public bool WillInstallOnlyAdminAndLanguagePacks
		{
			get
			{
				bool result = false;
				if (this.SelectedInstallableUnits.Count <= 2)
				{
					int num = 0;
					if (this.SelectedInstallableUnits.Contains("AdminToolsRole"))
					{
						num++;
					}
					if (this.SelectedInstallableUnits.Contains("LanguagePacks"))
					{
						num++;
					}
					if (num == this.SelectedInstallableUnits.Count)
					{
						result = true;
					}
				}
				return result;
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000091F0 File Offset: 0x000073F0
		public override ValidationError[] Validate()
		{
			SetupLogger.TraceEnter(new object[0]);
			List<ValidationError> list = new List<ValidationError>();
			if (!base.IgnoreValidatingRoleChanges)
			{
				SetupLogger.Log(Strings.ValidatingOptionsForRoles(base.SetupContext.RequestedRoles.Count));
				bool flag = base.SetupContext.RequestedRoles.Exists((Role current) => current.RoleName == "GatewayRole") || this.SelectedInstallableUnits.Contains("GatewayRole");
				bool flag2;
				if (!base.SetupContext.RequestedRoles.Exists((Role current) => current.RoleName != "GatewayRole" && current.RoleName != "AdminToolsRole" && current.RoleName != "LanguagePacks"))
				{
					flag2 = this.SelectedInstallableUnits.Exists((string current) => current != "GatewayRole" && current != "AdminToolsRole" && current != "LanguagePacks");
				}
				else
				{
					flag2 = true;
				}
				bool flag3 = flag2;
				if (flag && flag3)
				{
					list.Add(new SetupValidationError(Strings.AddConflictedRolesError));
				}
				else if (base.SetupContext.IsInstalledLocal("GatewayRole"))
				{
					if (base.SetupContext.RequestedRoles.Count > 0)
					{
						list.Add(new SetupValidationError(Strings.AddOtherRolesError));
					}
				}
				else if (base.SetupContext.IsUnpacked("GatewayRole"))
				{
					if (flag3)
					{
						list.Add(new SetupValidationError(Strings.AddOtherRolesError));
					}
				}
				else if (flag && base.SetupContext.UnpackedRoles.Count > 1)
				{
					list.Add(new SetupValidationError(Strings.AddGatewayAloneError));
				}
				if (!this.HasChanges || (base.SetupContext.IsCleanMachine && this.SelectedInstallableUnits.Count == 1 && (this.SelectedInstallableUnits.Contains("LanguagePacks") || this.SelectedInstallableUnits.Contains("UmLanguagePack"))))
				{
					list.Add(new SetupValidationError(Strings.NoRoleSelectedForInstall));
				}
			}
			if (!base.CanChangeInstallationPath && base.SetupContext.ParsedArguments.ContainsKey("targetdir") && (NonRootLocalLongFullPath)base.SetupContext.ParsedArguments["targetdir"] != base.InstallationPath)
			{
				list.Add(new SetupValidationError(Strings.AddCannotChangeTargetDirectoryError));
			}
			if (this.IsUnifiedMessagingEnabled && base.IsUnifiedMessagingChecked && base.UmSourceDir != null && !Directory.Exists(base.UmSourceDir.PathName))
			{
				list.Add(new SetupValidationError(Strings.CannotFindPath(base.UmSourceDir.PathName)));
			}
			if (!base.SetupContext.IsDatacenter)
			{
				foreach (Role role in base.SetupContext.RequestedRoles)
				{
					if (role.IsDatacenterOnly)
					{
						list.Add(new SetupValidationError(Strings.CannotInstallDatacenterRole(role.RoleName)));
					}
				}
			}
			if (!base.SetupContext.IsDatacenter)
			{
				bool flag4 = base.SetupContext.RequestedRoles.Exists((Role current) => current.RoleName == "FrontendTransportRole");
				bool flag5 = base.SetupContext.RequestedRoles.Exists((Role current) => current.RoleName == "CafeRole");
				bool flag6 = base.SetupContext.RequestedRoles.Exists((Role current) => current.RoleName == "BridgeheadRole");
				bool flag7 = base.SetupContext.RequestedRoles.Exists((Role current) => current.RoleName == "MailboxRole");
				bool flag8 = base.SetupContext.RequestedRoles.Exists((Role current) => current.RoleName == "ClientAccessRole");
				bool flag9 = base.SetupContext.RequestedRoles.Exists((Role current) => current.RoleName == "UnifiedMessagingRole");
				if (flag4 && !flag5)
				{
					list.Add(new SetupValidationError(Strings.CannotInstallDatacenterRole("FrontendTransportRole")));
				}
				if (flag6 && !flag7 && !flag8 && !flag9)
				{
					list.Add(new SetupValidationError(Strings.CannotInstallDatacenterRole("BridgeheadRole")));
				}
			}
			if (base.SetupContext.ParsedArguments.ContainsKey("customerfeedbackenabled"))
			{
				if (base.SetupContext.ParsedArguments.ContainsKey("preparead"))
				{
					if (base.ExchangeOrganizationExists && base.SetupContext.HasE14OrLaterServers)
					{
						list.Add(new SetupValidationError(Strings.CannotSpecifyCEIPWhenOrganizationHasE14OrLaterServersDuringPrepareAD));
					}
				}
				else if (!this.WillSetGlobalCEIP)
				{
					if (base.SetupContext.OriginalGlobalCustomerFeedbackEnabled == false)
					{
						list.Add(new SetupValidationError(Strings.CannotSpecifyServerCEIPWhenGlobalCEIPIsOptedOutDuringServerInstallation));
					}
					else if (!base.SetupContext.IsCleanMachine)
					{
						list.Add(new SetupValidationError(Strings.CannotSpecifyServerCEIPWhenMachineIsNotCleanDuringServerInstallation));
					}
					else if (this.WillInstallOnlyAdminAndLanguagePacks)
					{
						list.Add(new SetupValidationError(Strings.DoesNotSupportCEIPForAdminOnlyInstallation));
					}
				}
			}
			list.AddRange(base.Validate());
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00009730 File Offset: 0x00007930
		private void SetPartiallyConfiguredRoleChecked()
		{
			if (base.SetupContext.IsPartiallyConfigured("BridgeheadRole"))
			{
				base.SetIsBridgeheadCheckedInternal(true);
			}
			if (base.SetupContext.IsPartiallyConfigured("ClientAccessRole"))
			{
				base.SetIsClientAccessCheckedInternal(true);
			}
			if (base.SetupContext.IsPartiallyConfigured("GatewayRole"))
			{
				base.SetIsGatewayCheckedInternal(true);
			}
			if (base.SetupContext.IsPartiallyConfigured("MailboxRole"))
			{
				base.SetIsMailboxCheckedInternal(true);
			}
			if (base.SetupContext.IsPartiallyConfigured("UnifiedMessagingRole"))
			{
				base.SetIsUnifiedMessagingCheckedInternal(true);
			}
			if (base.SetupContext.IsPartiallyConfigured("FrontendTransportRole"))
			{
				base.SetIsFrontendTransportCheckedInternal(true);
			}
			if (base.SetupContext.IsPartiallyConfigured("AdminToolsRole"))
			{
				base.SetIsAdminToolsCheckedInternal(true);
			}
			if (base.SetupContext.IsPartiallyConfigured("CafeRole"))
			{
				base.SetIsCafeCheckedInternal(true);
			}
			if (base.SetupContext.IsPartiallyConfigured("CentralAdminRole"))
			{
				base.SetIsCentralAdminCheckedInternal(true);
			}
			if (base.SetupContext.IsPartiallyConfigured("CentralAdminDatabaseRole"))
			{
				base.SetIsCentralAdminDatabaseCheckedInternal(true);
			}
			if (base.SetupContext.IsPartiallyConfigured("MonitoringRole"))
			{
				base.SetIsMonitoringCheckedInternal(true);
			}
			if (base.SetupContext.IsPartiallyConfigured("OSPRole"))
			{
				base.SetIsOSPCheckedInternal(true);
			}
		}

		// Token: 0x04000079 RID: 121
		private SetupBindingTaskDataHandler preFileCopyDataHandler;

		// Token: 0x0400007A RID: 122
		private InstallFileDataHandler installFileDataHandler;

		// Token: 0x0400007B RID: 123
		private InstallOrgCfgDataHandler installOrgCfgDataHandler;

		// Token: 0x0400007C RID: 124
		private ProvisionServerDataHandler provisionServerDataHandler;

		// Token: 0x0400007D RID: 125
		private bool watsonEnabled;

		// Token: 0x0400007E RID: 126
		private bool typicalInstallation;

		// Token: 0x0400007F RID: 127
		private readonly bool hasRolesToInstall;

		// Token: 0x04000080 RID: 128
		private readonly Dictionary<string, AddUmLanguagePackCfgDataHandler> addUmLanguagePackDataHandlers = new Dictionary<string, AddUmLanguagePackCfgDataHandler>();

		// Token: 0x04000081 RID: 129
		private InstallPreCheckDataHandler preCheckDataHandler;

		// Token: 0x04000082 RID: 130
		private InstallBhdCfgDataHandler installBhdCfgDataHandler;

		// Token: 0x04000083 RID: 131
		private InstallCacCfgDataHandler installCacCfgDataHandler;

		// Token: 0x04000084 RID: 132
		private InstallGwyCfgDataHandler installGwyCfgDataHandler;

		// Token: 0x04000085 RID: 133
		private InstallMbxCfgDataHandler installMbxCfgDataHandler;

		// Token: 0x04000086 RID: 134
		private InstallUMCfgDataHandler installUMCfgDataHandler;

		// Token: 0x04000087 RID: 135
		private InstallFetCfgDataHandler installFetCfgDataHandler;

		// Token: 0x04000088 RID: 136
		private InstallCafeCfgDataHandler installCafeCfgDataHandler;

		// Token: 0x04000089 RID: 137
		private InstallAtCfgDataHandler installAtCfgDataHandler;

		// Token: 0x0400008A RID: 138
		private InstallCentralAdminDatabaseCfgDataHandler installCentralAdminDatabaseCfgDataHandler;

		// Token: 0x0400008B RID: 139
		private InstallCentralAdminCfgDataHandler installCentralAdminCfgDataHandler;

		// Token: 0x0400008C RID: 140
		private InstallCentralAdminFrontEndCfgDataHandler installCentralAdminFrontEndCfgDataHandler;

		// Token: 0x0400008D RID: 141
		private InstallMonitoringCfgDataHandler installMonitoringCfgDataHandler;

		// Token: 0x0400008E RID: 142
		private AddLanguagePackCfgDataHandler addLanguagePackCfgDataHandler;

		// Token: 0x0400008F RID: 143
		private InstallOSPCfgDataHandler installOSPCfgDataHandler;
	}
}
