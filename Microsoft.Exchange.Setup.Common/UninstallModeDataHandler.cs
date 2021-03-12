using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Win32;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000065 RID: 101
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UninstallModeDataHandler : ModeDataHandler
	{
		// Token: 0x060004B1 RID: 1201 RVA: 0x0001000C File Offset: 0x0000E20C
		public UninstallModeDataHandler(ISetupContext setupContext, MonadConnection connection) : base(setupContext, connection)
		{
			SetupLogger.Log(Strings.ApplyingDefaultRoleSelectionState);
			if (setupContext.IsUnpackedOrInstalledAD("BridgeheadRole"))
			{
				base.SetIsBridgeheadCheckedInternal(false);
			}
			if (setupContext.IsUnpackedOrInstalledAD("ClientAccessRole"))
			{
				base.SetIsClientAccessCheckedInternal(false);
			}
			if (setupContext.IsUnpackedOrInstalledAD("GatewayRole"))
			{
				base.SetIsGatewayCheckedInternal(false);
			}
			if (setupContext.IsUnpackedOrInstalledAD("MailboxRole"))
			{
				base.SetIsMailboxCheckedInternal(false);
			}
			if (setupContext.IsUnpackedOrInstalledAD("UnifiedMessagingRole"))
			{
				base.SetIsUnifiedMessagingCheckedInternal(false);
			}
			if (setupContext.IsUnpacked("FrontendTransportRole"))
			{
				base.SetIsFrontendTransportCheckedInternal(false);
			}
			if (setupContext.IsUnpacked("AdminToolsRole"))
			{
				base.SetIsAdminToolsCheckedInternal(false);
			}
			if (setupContext.IsUnpacked("CafeRole"))
			{
				base.SetIsCafeCheckedInternal(false);
			}
			if (setupContext.IsUnpacked("CentralAdminRole"))
			{
				base.SetIsCentralAdminCheckedInternal(false);
			}
			if (setupContext.IsUnpacked("CentralAdminDatabaseRole"))
			{
				base.SetIsCentralAdminDatabaseCheckedInternal(false);
			}
			if (setupContext.IsUnpacked("CentralAdminFrontEndRole"))
			{
				base.SetIsCentralAdminFrontEndCheckedInternal(false);
			}
			if (setupContext.IsUnpacked("MonitoringRole"))
			{
				base.SetIsMonitoringCheckedInternal(false);
			}
			if (setupContext.IsUnpacked("OSPRole"))
			{
				base.SetIsOSPCheckedInternal(false);
			}
		}

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x00010145 File Offset: 0x0000E345
		protected override bool NeedPrePostSetupDataHandlers
		{
			get
			{
				return base.NeedPrePostSetupDataHandlers && !base.SetupContext.HasRemoveProvisionedServerParameters;
			}
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001015F File Offset: 0x0000E35F
		protected override bool NeedFileDataHandler()
		{
			return base.NeedFileDataHandler() && !base.SetupContext.HasRemoveProvisionedServerParameters;
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0001017C File Offset: 0x0000E37C
		public override List<string> SelectedInstallableUnits
		{
			get
			{
				base.SelectedInstallableUnits.Clear();
				if (base.SetupContext.HasRemoveProvisionedServerParameters)
				{
					return base.SelectedInstallableUnits;
				}
				if (this.IsMailboxEnabled && !base.IsMailboxChecked)
				{
					base.SelectedInstallableUnits.Add("MailboxRole");
				}
				if (this.IsUnifiedMessagingEnabled && !base.IsUnifiedMessagingChecked)
				{
					base.SelectedInstallableUnits.AddRange(this.GetInstalledUmLanguagePacks());
					base.SelectedInstallableUnits.Add("UnifiedMessagingRole");
				}
				if (this.IsClientAccessEnabled && !base.IsClientAccessChecked)
				{
					base.SelectedInstallableUnits.Add("ClientAccessRole");
				}
				if (this.IsBridgeheadEnabled && !base.IsBridgeheadChecked)
				{
					base.SelectedInstallableUnits.Add("BridgeheadRole");
				}
				if (this.IsGatewayEnabled && !base.IsGatewayChecked)
				{
					base.SelectedInstallableUnits.Add("GatewayRole");
				}
				if (this.IsFrontendTransportEnabled && !base.IsFrontendTransportChecked)
				{
					base.SelectedInstallableUnits.Add("FrontendTransportRole");
				}
				if ((this.IsAdminToolsEnabled || this.IsGatewayEnabled) && !base.IsAdminToolsChecked)
				{
					base.SelectedInstallableUnits.Add("AdminToolsRole");
				}
				if (this.IsCafeEnabled && !base.IsCafeChecked)
				{
					base.SelectedInstallableUnits.Add("CafeRole");
				}
				if (this.IsCentralAdminEnabled && !base.IsCentralAdminChecked)
				{
					base.SelectedInstallableUnits.Add("CentralAdminRole");
				}
				if (this.IsCentralAdminDatabaseEnabled && !base.IsCentralAdminDatabaseChecked)
				{
					base.SelectedInstallableUnits.Add("CentralAdminDatabaseRole");
				}
				if (this.IsMonitoringEnabled && !base.IsMonitoringChecked)
				{
					base.SelectedInstallableUnits.Add("MonitoringRole");
				}
				if (this.IsRemoveAllRoles)
				{
					base.SelectedInstallableUnits.Add("LanguagePacks");
				}
				if (this.IsOSPEnabled && !base.IsOSPChecked)
				{
					base.SelectedInstallableUnits.Add("OSPRole");
				}
				return base.SelectedInstallableUnits;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060004B5 RID: 1205 RVA: 0x0001035F File Offset: 0x0000E55F
		public override InstallationModes Mode
		{
			get
			{
				return InstallationModes.Uninstall;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x00010362 File Offset: 0x0000E562
		public override PreCheckDataHandler PreCheckDataHandler
		{
			get
			{
				if (this.preCheckDataHandler == null)
				{
					this.preCheckDataHandler = new UninstallPreCheckDataHandler(base.SetupContext, this, base.Connection);
				}
				return this.preCheckDataHandler;
			}
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001038C File Offset: 0x0000E58C
		protected override void AddNeededFileDataHandlers()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddNeededFileDataHandlers();
			if (this.preFileCopyDataHandler == null)
			{
				this.preFileCopyDataHandler = new SetupBindingTaskDataHandler(BindingCategory.PreFileCopy, base.SetupContext, base.Connection);
			}
			this.preFileCopyDataHandler.Mode = this.Mode;
			this.preFileCopyDataHandler.PreviousVersion = base.PreviousVersion;
			this.preFileCopyDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
			base.DataHandlers.Add(this.preFileCopyDataHandler);
			if (this.uninstallFileDataHandler == null)
			{
				this.uninstallFileDataHandler = new UninstallFileDataHandler(base.SetupContext, new ProductMsiConfigurationInfo(), base.Connection);
			}
			this.uninstallFileDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
			base.DataHandlers.Add(this.uninstallFileDataHandler);
			if (base.SetupContext.IsDatacenter || base.SetupContext.IsDatacenterDedicated)
			{
				UninstallFileDataHandler uninstallFileDataHandler = new UninstallFileDataHandler(base.SetupContext, new DatacenterMsiConfigurationInfo(), base.Connection);
				uninstallFileDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
				base.DataHandlers.Add(uninstallFileDataHandler);
			}
			if (this.postFileCopyDataHandler == null)
			{
				this.postFileCopyDataHandler = new SetupBindingTaskDataHandler(BindingCategory.PostFileCopy, base.SetupContext, base.Connection);
			}
			this.postFileCopyDataHandler.Mode = this.Mode;
			this.postFileCopyDataHandler.PreviousVersion = base.PreviousVersion;
			this.postFileCopyDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
			base.DataHandlers.Add(this.postFileCopyDataHandler);
			SetupLogger.TraceExit();
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x00010508 File Offset: 0x0000E708
		protected override void AddConfigurationDataHandlers()
		{
			base.AddConfigurationDataHandlers();
			if (!this.SelectedInstallableUnits.Contains("GatewayRole"))
			{
				if (this.IsRemoveAllConfiguredServerRoles && base.OrgLevelConfigRequired() && (this.SelectedInstallableUnits.Count != 1 || !this.SelectedInstallableUnits.Contains("AdminToolsRole")))
				{
					if (this.uninstallOrgCfgDataHandler == null)
					{
						this.uninstallOrgCfgDataHandler = new UninstallOrgCfgDataHandler(base.SetupContext, base.Connection);
					}
					this.uninstallOrgCfgDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
					base.DataHandlers.Add(this.uninstallOrgCfgDataHandler);
				}
				if (base.SetupContext.HasRemoveProvisionedServerParameters)
				{
					if (this.provisionServerDataHandler == null)
					{
						this.provisionServerDataHandler = new RemoveProvisionedServerDataHandler(base.SetupContext, base.Connection);
					}
					this.provisionServerDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
					base.DataHandlers.Add(this.provisionServerDataHandler);
				}
			}
			if (this.IsRemoveAllRoles && base.SetupContext.InstalledLanguagePacks.Count > 0 && !base.SetupContext.HasRemoveProvisionedServerParameters)
			{
				RemoveLanguagePackCfgDataHandler item = (RemoveLanguagePackCfgDataHandler)this.GetInstallableUnitConfigurationDataHandler("LanguagePacks");
				if (!(base.DataHandlers[base.DataHandlers.Count - 1] is RemoveLanguagePackCfgDataHandler))
				{
					for (int i = base.DataHandlers.Count - 2; i >= 0; i--)
					{
						if (base.DataHandlers[i] is RemoveLanguagePackCfgDataHandler)
						{
							base.DataHandlers.RemoveAt(i);
							break;
						}
					}
					base.DataHandlers.Add(item);
				}
			}
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x00010694 File Offset: 0x0000E894
		protected override ConfigurationDataHandler GetInstallableUnitConfigurationDataHandler(string installableUnitName)
		{
			ConfigurationDataHandler result = null;
			if (base.HasConfigurationDataHandler(installableUnitName))
			{
				if (InstallableUnitConfigurationInfoManager.IsRoleBasedConfigurableInstallableUnit(installableUnitName))
				{
					Role roleByName = RoleManager.GetRoleByName(installableUnitName);
					if (base.SetupContext.IsInstalledLocalOrAD(installableUnitName) || (base.SetupContext.IsPartiallyConfigured(installableUnitName) && roleByName.IsPartiallyInstalled))
					{
						UninstallCfgDataHandler uninstallCfgDataHandler;
						if (!this.uninstallCfgDataHandlers.TryGetValue(installableUnitName, out uninstallCfgDataHandler))
						{
							if (installableUnitName == "AdminToolsRole")
							{
								uninstallCfgDataHandler = new UninstallAtCfgDataHandler(base.SetupContext, RoleManager.GetRoleByName(installableUnitName), base.Connection);
							}
							else if (installableUnitName == "MailboxRole")
							{
								this.uninstallMailboxRole = new UninstallMbxCfgDataHandler(base.SetupContext, RoleManager.GetRoleByName(installableUnitName), base.Connection);
								uninstallCfgDataHandler = this.uninstallMailboxRole;
							}
							else if (installableUnitName == "CentralAdminRole")
							{
								uninstallCfgDataHandler = new UninstallCentralAdminCfgDataHandler(base.SetupContext, RoleManager.GetRoleByName(installableUnitName), base.Connection);
							}
							else if (installableUnitName == "CentralAdminDatabaseRole")
							{
								uninstallCfgDataHandler = new UninstallCentralAdminDatabaseCfgDataHandler(base.SetupContext, RoleManager.GetRoleByName(installableUnitName), base.Connection);
							}
							else
							{
								uninstallCfgDataHandler = new UninstallCfgDataHandler(base.SetupContext, RoleManager.GetRoleByName(installableUnitName), base.Connection);
							}
							this.uninstallCfgDataHandlers.Add(installableUnitName, uninstallCfgDataHandler);
						}
						result = uninstallCfgDataHandler;
					}
				}
				else if (InstallableUnitConfigurationInfoManager.IsUmLanguagePackInstallableUnit(installableUnitName))
				{
					InstallableUnitConfigurationInfo installableUnitConfigurationInfoByName = InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(installableUnitName);
					UmLanguagePackConfigurationInfo umLanguagePackConfigurationInfo = installableUnitConfigurationInfoByName as UmLanguagePackConfigurationInfo;
					if (umLanguagePackConfigurationInfo != null)
					{
						RemoveUmLanguagePackCfgDataHandler removeUmLanguagePackCfgDataHandler;
						if (!this.removeUmLanguagePackDataHandlers.TryGetValue(installableUnitName, out removeUmLanguagePackCfgDataHandler))
						{
							removeUmLanguagePackCfgDataHandler = new RemoveUmLanguagePackCfgDataHandler(base.SetupContext, base.Connection, umLanguagePackConfigurationInfo.Culture);
							this.removeUmLanguagePackDataHandlers.Add(installableUnitName, removeUmLanguagePackCfgDataHandler);
						}
						result = removeUmLanguagePackCfgDataHandler;
					}
				}
				else if (InstallableUnitConfigurationInfoManager.IsLanguagePacksInstallableUnit(installableUnitName))
				{
					result = new RemoveLanguagePackCfgDataHandler(base.SetupContext, base.Connection);
				}
			}
			return result;
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x00010845 File Offset: 0x0000EA45
		public override bool IsBridgeheadEnabled
		{
			get
			{
				return base.SetupContext.IsPartiallyConfigured("BridgeheadRole") || base.SetupContext.IsInstalledLocal("BridgeheadRole") || base.SetupContext.IsUnpackedOrInstalledAD("BridgeheadRole");
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060004BB RID: 1211 RVA: 0x0001087D File Offset: 0x0000EA7D
		public override bool IsClientAccessEnabled
		{
			get
			{
				return base.SetupContext.IsPartiallyConfigured("ClientAccessRole") || base.SetupContext.IsInstalledLocal("ClientAccessRole") || base.SetupContext.IsUnpackedOrInstalledAD("ClientAccessRole");
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000108B5 File Offset: 0x0000EAB5
		public override bool IsGatewayEnabled
		{
			get
			{
				return base.SetupContext.IsPartiallyConfigured("GatewayRole") || base.SetupContext.IsInstalledLocal("GatewayRole") || base.SetupContext.IsUnpackedOrInstalledAD("GatewayRole");
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060004BD RID: 1213 RVA: 0x000108ED File Offset: 0x0000EAED
		public override bool IsMailboxEnabled
		{
			get
			{
				return base.SetupContext.IsPartiallyConfigured("MailboxRole") || base.SetupContext.IsInstalledLocal("MailboxRole") || base.SetupContext.IsUnpackedOrInstalledAD("MailboxRole");
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060004BE RID: 1214 RVA: 0x00010925 File Offset: 0x0000EB25
		public override bool IsUnifiedMessagingEnabled
		{
			get
			{
				return base.SetupContext.IsPartiallyConfigured("UnifiedMessagingRole") || base.SetupContext.IsInstalledLocal("UnifiedMessagingRole") || base.SetupContext.IsUnpackedOrInstalledAD("UnifiedMessagingRole");
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060004BF RID: 1215 RVA: 0x0001095D File Offset: 0x0000EB5D
		public override bool IsFrontendTransportEnabled
		{
			get
			{
				return base.SetupContext.IsPartiallyConfigured("FrontendTransportRole") || base.SetupContext.IsInstalledLocal("FrontendTransportRole") || base.SetupContext.IsUnpackedOrInstalledAD("FrontendTransportRole");
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060004C0 RID: 1216 RVA: 0x00010995 File Offset: 0x0000EB95
		public override bool IsCafeEnabled
		{
			get
			{
				return base.SetupContext.IsPartiallyConfigured("CafeRole") || base.SetupContext.IsInstalledLocal("CafeRole") || base.SetupContext.IsUnpackedOrInstalledAD("CafeRole");
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060004C1 RID: 1217 RVA: 0x000109CD File Offset: 0x0000EBCD
		public override bool IsCentralAdminEnabled
		{
			get
			{
				return base.SetupContext.IsPartiallyConfigured("CentralAdminRole") || base.SetupContext.IsInstalledLocal("CentralAdminRole") || base.SetupContext.IsUnpacked("CentralAdminRole");
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060004C2 RID: 1218 RVA: 0x00010A05 File Offset: 0x0000EC05
		public override bool IsCentralAdminDatabaseEnabled
		{
			get
			{
				return base.SetupContext.IsPartiallyConfigured("CentralAdminDatabaseRole") || base.SetupContext.IsInstalledLocal("CentralAdminDatabaseRole") || base.SetupContext.IsUnpacked("CentralAdminDatabaseRole");
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060004C3 RID: 1219 RVA: 0x00010A3D File Offset: 0x0000EC3D
		public override bool IsMonitoringEnabled
		{
			get
			{
				return base.SetupContext.IsPartiallyConfigured("MonitoringRole") || base.SetupContext.IsInstalledLocal("MonitoringRole") || base.SetupContext.IsUnpackedOrInstalledAD("MonitoringRole");
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060004C4 RID: 1220 RVA: 0x00010A75 File Offset: 0x0000EC75
		public override bool IsAdminToolsEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("AdminToolsRole") && this.IsRemoveAllServerRoles && !this.IsGatewayEnabled;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060004C5 RID: 1221 RVA: 0x00010A9C File Offset: 0x0000EC9C
		public override bool IsOSPEnabled
		{
			get
			{
				return base.SetupContext.IsPartiallyConfigured("OSPRole") || base.SetupContext.IsInstalledLocal("OSPRole") || base.SetupContext.IsUnpackedOrInstalledAD("OSPRole");
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060004C6 RID: 1222 RVA: 0x00010AD4 File Offset: 0x0000ECD4
		public bool IsRemoveAllRoles
		{
			get
			{
				return this.IsRemoveAllServerRoles && !base.IsAdminToolsChecked;
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00010AE9 File Offset: 0x0000ECE9
		public bool IsRemoveAllServerRoles
		{
			get
			{
				return !base.IsBridgeheadChecked && !base.IsClientAccessChecked && !base.IsGatewayChecked && !base.IsMailboxChecked && !base.IsUnifiedMessagingChecked;
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060004C8 RID: 1224 RVA: 0x00010B18 File Offset: 0x0000ED18
		public bool IsRemoveAllConfiguredServerRoles
		{
			get
			{
				bool result = true;
				foreach (Role role in base.SetupContext.InstalledRolesLocal)
				{
					if (base.HasConfigurationDataHandler(role.RoleName) && !this.SelectedInstallableUnits.Contains(role.RoleName))
					{
						result = false;
						break;
					}
				}
				return result;
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00010B94 File Offset: 0x0000ED94
		public override decimal RequiredDiskSpace
		{
			get
			{
				return 0m;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060004CA RID: 1226 RVA: 0x00010B9C File Offset: 0x0000ED9C
		public override bool CanChangeSourceDir
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00010B9F File Offset: 0x0000ED9F
		public override string RoleSelectionDescription
		{
			get
			{
				return Strings.RemoveServerRoleText;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060004CC RID: 1228 RVA: 0x00010BAB File Offset: 0x0000EDAB
		public override LocalizedString ConfigurationSummary
		{
			get
			{
				if (base.SetupContext.HasRemoveProvisionedServerParameters)
				{
					return LocalizedString.Empty;
				}
				return Strings.RemoveRolesToInstall;
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060004CD RID: 1229 RVA: 0x00010BC5 File Offset: 0x0000EDC5
		public override string CompletionDescription
		{
			get
			{
				return base.WorkUnits.HasFailures ? Strings.RemoveFailText : Strings.RemoveSuccessText;
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060004CE RID: 1230 RVA: 0x00010BE5 File Offset: 0x0000EDE5
		public override string CompletionStatus
		{
			get
			{
				return base.WorkUnits.HasFailures ? Strings.RemoveFailStatus : Strings.RemoveSuccessStatus;
			}
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00010C08 File Offset: 0x0000EE08
		protected override void UpdateDataHandlers()
		{
			SetupLogger.TraceEnter(new object[0]);
			if (this.IsGatewayEnabled && base.IsGatewayChecked != base.IsAdminToolsChecked)
			{
				base.SetIsAdminToolsCheckedInternal(base.IsGatewayChecked);
			}
			if (base.SetupContext.HasRemoveProvisionedServerParameters)
			{
				ConfigurationContext.Setup.UseAssemblyPathAsInstallPath();
			}
			SetupLogger.Log(Strings.SetupWillRunFromPath(ConfigurationContext.Setup.InstallPath));
			base.DataHandlers.Clear();
			base.AddPreSetupDataHandlers();
			this.AddConfigurationDataHandlers();
			base.AddFileDataHandlers();
			base.AddPostSetupDataHandlers();
			SetupLogger.Log(Strings.UninstallModeDataHandlerCount(base.DataHandlers.Count));
			SetupLogger.TraceExit();
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x00010CA0 File Offset: 0x0000EEA0
		public override bool HasChanges
		{
			get
			{
				return this.SelectedInstallableUnits.Count > 0 || base.SetupContext.HasRemoveProvisionedServerParameters;
			}
		}

		// Token: 0x060004D1 RID: 1233 RVA: 0x00010CD0 File Offset: 0x0000EED0
		public override ValidationError[] Validate()
		{
			SetupLogger.TraceEnter(new object[0]);
			List<ValidationError> list = new List<ValidationError>();
			if (!base.IgnoreValidatingRoleChanges)
			{
				SetupLogger.Log(Strings.ValidatingRoleOptions(base.SetupContext.RequestedRoles.Count));
				List<string> list2 = new List<string>();
				foreach (Role role in base.SetupContext.RequestedRoles)
				{
					if (!base.SetupContext.IsInstalledLocal(role.RoleName) && !base.SetupContext.IsPartiallyConfigured(role.RoleName) && !base.SetupContext.IsUnpackedOrInstalledAD(role.RoleName))
					{
						list2.Add(InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(role.RoleName).DisplayName);
					}
				}
				if (list2.Count > 0)
				{
					string missingRoles = string.Join(", ", list2.ToArray());
					list.Add(new SetupValidationError(Strings.RoleNotInstalledError(missingRoles)));
				}
				string admintools = InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName("AdminToolsRole").DisplayName;
				bool flag = base.SetupContext.RequestedRoles.Exists((Role current) => current.RoleName == "AdminToolsRole");
				if (base.SetupContext.RequestedRoles.Count < base.SetupContext.UnpackedRoles.Count && flag)
				{
					list.Add(new SetupValidationError(Strings.AdminToolCannotBeUninstalledWhenSomeRolesRemained(admintools)));
				}
				if (!this.HasChanges)
				{
					list.Add(new SetupValidationError(Strings.NoRoleSelectedForUninstall));
				}
				SetupLogger.Log(Strings.UninstallModeDataHandlerHandlersAndWorkUnits(base.DataHandlers.Count, base.WorkUnits.Count));
			}
			list.AddRange(base.Validate());
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x060004D2 RID: 1234 RVA: 0x00010EAC File Offset: 0x0000F0AC
		private List<string> GetInstalledUmLanguagePacks()
		{
			List<string> list = new List<string>();
			List<string> result;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\UnifiedMessagingRole\\LanguagePacks\\"))
			{
				if (registryKey != null)
				{
					foreach (string text in registryKey.GetValueNames())
					{
						if (text.ToLower() == "en-us")
						{
							SetupLogger.Log(Strings.EnglishUSLanguagePackInstalled);
						}
						else
						{
							try
							{
								CultureInfo cultureInfo = CultureInfo.CreateSpecificCulture(text);
								string umLanguagePackNameForCultureInfo = UmLanguagePackConfigurationInfo.GetUmLanguagePackNameForCultureInfo(cultureInfo);
								list.Add(umLanguagePackNameForCultureInfo);
								SetupLogger.Log(Strings.UnifiedMessagingLanguagePackInstalled(cultureInfo.ToString()));
								if (InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(umLanguagePackNameForCultureInfo) == null)
								{
									SetupLogger.Log(Strings.NoConfigurationInfoFoundForInstallableUnit(umLanguagePackNameForCultureInfo));
									InstallableUnitConfigurationInfoManager.AddInstallableUnit(umLanguagePackNameForCultureInfo, new UmLanguagePackConfigurationInfo(cultureInfo));
									SetupLogger.Log(Strings.AddedConfigurationInfoForInstallableUnit(umLanguagePackNameForCultureInfo));
								}
							}
							catch (ArgumentException)
							{
								SetupLogger.Log(Strings.NonCultureRegistryEntryFound(text));
							}
						}
					}
				}
				result = list;
			}
			return result;
		}

		// Token: 0x04000190 RID: 400
		private SetupBindingTaskDataHandler preFileCopyDataHandler;

		// Token: 0x04000191 RID: 401
		private SetupBindingTaskDataHandler postFileCopyDataHandler;

		// Token: 0x04000192 RID: 402
		private RemoveProvisionedServerDataHandler provisionServerDataHandler;

		// Token: 0x04000193 RID: 403
		private UninstallPreCheckDataHandler preCheckDataHandler;

		// Token: 0x04000194 RID: 404
		private UninstallFileDataHandler uninstallFileDataHandler;

		// Token: 0x04000195 RID: 405
		private UninstallMbxCfgDataHandler uninstallMailboxRole;

		// Token: 0x04000196 RID: 406
		private UninstallOrgCfgDataHandler uninstallOrgCfgDataHandler;

		// Token: 0x04000197 RID: 407
		private Dictionary<string, UninstallCfgDataHandler> uninstallCfgDataHandlers = new Dictionary<string, UninstallCfgDataHandler>();

		// Token: 0x04000198 RID: 408
		private Dictionary<string, RemoveUmLanguagePackCfgDataHandler> removeUmLanguagePackDataHandlers = new Dictionary<string, RemoveUmLanguagePackCfgDataHandler>();
	}
}
