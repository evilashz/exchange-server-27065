using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Tasks;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200006E RID: 110
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UpgradeModeDataHandler : ModeDataHandler
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x000111DD File Offset: 0x0000F3DD
		public UpgradeModeDataHandler(ISetupContext setupContext, MonadConnection connection) : base(setupContext, connection)
		{
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x000111F4 File Offset: 0x0000F3F4
		public override List<string> SelectedInstallableUnits
		{
			get
			{
				base.SelectedInstallableUnits.Clear();
				bool flag = false;
				if (this.IsBridgeheadEnabled && base.IsBridgeheadChecked)
				{
					base.SelectedInstallableUnits.Add("BridgeheadRole");
					flag = true;
				}
				if (this.IsFrontendTransportEnabled && base.IsFrontendTransportChecked)
				{
					base.SelectedInstallableUnits.Add("FrontendTransportRole");
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
				if (this.IsMonitoringEnabled)
				{
					base.SelectedInstallableUnits.Add("MonitoringRole");
					flag = true;
				}
				if (this.IsAdminToolsEnabled && base.IsAdminToolsChecked)
				{
					base.SelectedInstallableUnits.Add("AdminToolsRole");
					flag = true;
				}
				if (this.IsCafeEnabled && base.IsCafeChecked)
				{
					base.SelectedInstallableUnits.Add("CafeRole");
					flag = true;
				}
				if (this.IsCentralAdminDatabaseEnabled)
				{
					base.SelectedInstallableUnits.Add("CentralAdminDatabaseRole");
					flag = true;
				}
				if (this.IsCentralAdminEnabled)
				{
					base.SelectedInstallableUnits.Add("CentralAdminRole");
					flag = true;
				}
				if ((flag || base.SetupContext.IsLanguagePackOperation) && base.SetupContext.NeedToUpdateLanguagePacks)
				{
					base.SelectedInstallableUnits.Insert(0, "LanguagePacks");
				}
				return base.SelectedInstallableUnits;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x000113A0 File Offset: 0x0000F5A0
		public override InstallationModes Mode
		{
			get
			{
				return InstallationModes.BuildToBuildUpgrade;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x000113A3 File Offset: 0x0000F5A3
		public override PreCheckDataHandler PreCheckDataHandler
		{
			get
			{
				if (this.preCheckDataHandler == null)
				{
					this.preCheckDataHandler = new UpgradePrecheckDataHandler(base.SetupContext, this, base.Connection);
				}
				return this.preCheckDataHandler;
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x000113CC File Offset: 0x0000F5CC
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

		// Token: 0x060004EE RID: 1262 RVA: 0x00011448 File Offset: 0x0000F648
		protected override void AddNeededFileDataHandlers()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddNeededFileDataHandlers();
			string text = null;
			if (this.targetDir != null)
			{
				text = this.targetDir.PathName;
			}
			if (this.preFileCopyDataHandler == null)
			{
				this.preFileCopyDataHandler = new SetupBindingTaskDataHandler(BindingCategory.PreFileCopy, base.SetupContext, base.Connection);
			}
			this.preFileCopyDataHandler.Mode = this.Mode;
			this.preFileCopyDataHandler.PreviousVersion = base.PreviousVersion;
			this.preFileCopyDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
			base.DataHandlers.Add(this.preFileCopyDataHandler);
			if (!base.SetupContext.IsCleanMachine)
			{
				if (base.SetupContext.NeedToUpdateLanguagePacks && base.SetupContext.InstalledLanguagePacks.Count > 0)
				{
					if (this.removeLanguagePackDataHandler == null)
					{
						this.removeLanguagePackDataHandler = new RemoveLanguagePackCfgDataHandler(base.SetupContext, base.Connection);
					}
					base.DataHandlers.Add(this.removeLanguagePackDataHandler);
				}
				if (this.uninstallFileDataHandler == null)
				{
					this.uninstallFileDataHandler = new UninstallFileDataHandler(base.SetupContext, new ProductMsiConfigurationInfo(), base.Connection);
				}
				this.uninstallFileDataHandler.IsUpgrade = true;
				if (base.SetupContext.IsDatacenter || base.SetupContext.IsDatacenterDedicated)
				{
					UninstallFileDataHandler uninstallFileDataHandler = new UninstallFileDataHandler(base.SetupContext, new DatacenterMsiConfigurationInfo(), base.Connection);
					uninstallFileDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
					uninstallFileDataHandler.IsUpgrade = true;
					base.DataHandlers.Add(uninstallFileDataHandler);
				}
				base.DataHandlers.Add(this.uninstallFileDataHandler);
			}
			else
			{
				SetupLogger.Log(Strings.MSINotPresent(text));
			}
			if (this.midFileCopyDataHandler == null)
			{
				this.midFileCopyDataHandler = new SetupBindingTaskDataHandler(BindingCategory.MidFileCopy, base.SetupContext, base.Connection);
			}
			this.midFileCopyDataHandler.Mode = this.Mode;
			this.midFileCopyDataHandler.PreviousVersion = base.PreviousVersion;
			this.midFileCopyDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
			base.DataHandlers.Add(this.midFileCopyDataHandler);
			if (this.installFileDataHandler == null)
			{
				this.installFileDataHandler = new InstallFileDataHandler(base.SetupContext, new ProductMsiConfigurationInfo(), base.Connection);
			}
			this.installFileDataHandler.SelectedInstallableUnits.Clear();
			foreach (string text2 in this.SelectedInstallableUnits.ToArray())
			{
				Role roleByName = RoleManager.GetRoleByName(text2);
				if (roleByName == null || !RoleManager.GetRoleByName(text2).IsDatacenterOnly)
				{
					this.installFileDataHandler.SelectedInstallableUnits.Add(text2);
				}
			}
			this.installFileDataHandler.TargetDirectory = text;
			base.DataHandlers.Add(this.installFileDataHandler);
			if (base.SetupContext.IsDatacenter || base.SetupContext.IsDatacenterDedicated)
			{
				InstallFileDataHandler installFileDataHandler = new InstallFileDataHandler(base.SetupContext, new DatacenterMsiConfigurationInfo(), base.Connection);
				installFileDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
				installFileDataHandler.TargetDirectory = base.InstallationPath.PathName;
				base.DataHandlers.Add(installFileDataHandler);
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001174C File Offset: 0x0000F94C
		private void AddOrgLevelConfigurationDataHandlers()
		{
			SetupLogger.TraceEnter(new object[0]);
			if (!this.SelectedInstallableUnits.Contains("GatewayRole") && this.SelectedInstallableUnits.Count > 1 && base.OrgLevelConfigRequired())
			{
				if (this.installOrgCfgDataHandler == null)
				{
					this.installOrgCfgDataHandler = new InstallOrgCfgDataHandler(base.SetupContext, base.Connection);
				}
				this.installOrgCfgDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
				base.DataHandlers.Add(this.installOrgCfgDataHandler);
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000117D4 File Offset: 0x0000F9D4
		protected override ConfigurationDataHandler GetInstallableUnitConfigurationDataHandler(string installableUnitName)
		{
			ConfigurationDataHandler configurationDataHandler = null;
			if (base.HasConfigurationDataHandler(installableUnitName) && !this.upgradeConfigDataHandlers.TryGetValue(installableUnitName, out configurationDataHandler))
			{
				if (InstallableUnitConfigurationInfoManager.IsUmLanguagePackInstallableUnit(installableUnitName))
				{
					InstallableUnitConfigurationInfo installableUnitConfigurationInfoByName = InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(installableUnitName);
					UmLanguagePackConfigurationInfo umLanguagePackConfigurationInfo = installableUnitConfigurationInfoByName as UmLanguagePackConfigurationInfo;
					configurationDataHandler = new AddUmLanguagePackCfgDataHandler(base.SetupContext, base.Connection, umLanguagePackConfigurationInfo.Culture, base.UmSourceDir);
				}
				else if (InstallableUnitConfigurationInfoManager.IsLanguagePacksInstallableUnit(installableUnitName))
				{
					configurationDataHandler = new AddLanguagePackCfgDataHandler(base.SetupContext, base.Connection);
				}
				else if (installableUnitName == "AdminToolsRole")
				{
					configurationDataHandler = new UpgradeAtCfgDataHandler(base.SetupContext, RoleManager.GetRoleByName(installableUnitName), base.Connection);
				}
				else if (installableUnitName == "CentralAdminDatabaseRole")
				{
					configurationDataHandler = new UpgradeCentralAdminDatabaseCfgDataHandler(base.SetupContext, RoleManager.GetRoleByName(installableUnitName), base.Connection);
				}
				else if (installableUnitName == "CentralAdminRole")
				{
					configurationDataHandler = new UpgradeCentralAdminCfgDataHandler(base.SetupContext, RoleManager.GetRoleByName(installableUnitName), base.Connection);
				}
				else if (installableUnitName == "MonitoringRole")
				{
					configurationDataHandler = new UpgradeMonitoringCfgDataHandler(base.SetupContext, RoleManager.GetRoleByName(installableUnitName), base.Connection);
				}
				else
				{
					configurationDataHandler = new UpgradeCfgDataHandler(base.SetupContext, RoleManager.GetRoleByName(installableUnitName), base.Connection);
				}
				this.upgradeConfigDataHandlers.Add(installableUnitName, configurationDataHandler);
			}
			return configurationDataHandler;
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0001191F File Offset: 0x0000FB1F
		public override bool IsBridgeheadEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledAD("BridgeheadRole");
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060004F2 RID: 1266 RVA: 0x00011931 File Offset: 0x0000FB31
		public override bool IsClientAccessEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledAD("ClientAccessRole");
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00011943 File Offset: 0x0000FB43
		public override bool IsGatewayEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledAD("GatewayRole");
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00011955 File Offset: 0x0000FB55
		public override bool IsMailboxEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledAD("MailboxRole");
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00011967 File Offset: 0x0000FB67
		public override bool IsMonitoringEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("MonitoringRole");
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00011979 File Offset: 0x0000FB79
		public override bool IsUnifiedMessagingEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledAD("UnifiedMessagingRole");
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0001198B File Offset: 0x0000FB8B
		public override bool IsAdminToolsEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("AdminToolsRole");
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x0001199D File Offset: 0x0000FB9D
		public override bool IsCafeEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("CafeRole");
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000119AF File Offset: 0x0000FBAF
		public override bool IsCentralAdminEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("CentralAdminRole");
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x000119C1 File Offset: 0x0000FBC1
		public override bool IsCentralAdminDatabaseEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("CentralAdminDatabaseRole");
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060004FB RID: 1275 RVA: 0x000119D3 File Offset: 0x0000FBD3
		public override bool CanChangeSourceDir
		{
			get
			{
				return !base.SetupContext.IsCleanMachine;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060004FC RID: 1276 RVA: 0x000119E3 File Offset: 0x0000FBE3
		public override string RoleSelectionDescription
		{
			get
			{
				return Strings.UpgradeServerRoleText;
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x000119EF File Offset: 0x0000FBEF
		public override LocalizedString ConfigurationSummary
		{
			get
			{
				return Strings.UpgradeRolesToInstall;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060004FE RID: 1278 RVA: 0x000119F6 File Offset: 0x0000FBF6
		public override string CompletionDescription
		{
			get
			{
				return base.WorkUnits.HasFailures ? Strings.UpgradeFailText : Strings.UpgradeSuccessText;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x00011A16 File Offset: 0x0000FC16
		public override string CompletionStatus
		{
			get
			{
				return base.WorkUnits.HasFailures ? Strings.UpgradeFailStatus : Strings.UpgradeSuccessStatus;
			}
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00011A38 File Offset: 0x0000FC38
		protected override void UpdateDataHandlers()
		{
			SetupLogger.TraceEnter(new object[0]);
			this.targetDir = (base.SetupContext.InstalledPath ?? base.SetupContext.BackupInstalledPath);
			base.PreviousVersion = (base.SetupContext.InstalledVersion ?? base.SetupContext.BackupInstalledVersion);
			this.fileUpgradeNeeded = (base.SetupContext.IsCleanMachine || base.SetupContext.InstalledVersion < base.SetupContext.RunningVersion);
			base.DataHandlers.Clear();
			if (!base.IsLanguagePacksChecked && (base.SetupContext.IsCleanMachine || base.SetupContext.IsLanguagePackOperation || base.SetupContext.LanguagePacksToInstall.Keys.Count != 0))
			{
				base.SetIsLanguagePacksCheckedInternal(true);
			}
			this.AddOrgLevelConfigurationDataHandlers();
			if (this.fileUpgradeNeeded)
			{
				ConfigurationContext.Setup.UseAssemblyPathAsInstallPath();
				base.AddPreSetupDataHandlers();
				base.AddFileDataHandlers();
			}
			else
			{
				SetupLogger.Log(Strings.MSIIsCurrent);
			}
			if (base.SetupContext.NeedToUpdateLanguagePacks)
			{
				base.AddLanguagePackFileDataHandlers();
			}
			base.AddPostCopyFileDataHandlers();
			this.AddConfigurationDataHandlers();
			base.AddPostSetupDataHandlers();
			foreach (DataHandler dataHandler in base.DataHandlers)
			{
				SetupSingleTaskDataHandler setupSingleTaskDataHandler = (SetupSingleTaskDataHandler)dataHandler;
				ExTraceGlobals.TraceTracer.Information(0L, setupSingleTaskDataHandler.CommandText);
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00011BB4 File Offset: 0x0000FDB4
		public override ValidationError[] Validate()
		{
			SetupLogger.TraceEnter(new object[0]);
			List<ValidationError> list = new List<ValidationError>();
			SetupLogger.Log(Strings.ValidatingRoleOptions(base.SetupContext.RequestedRoles.Count));
			List<string> list2 = new List<string>();
			foreach (Role role in base.SetupContext.RequestedRoles)
			{
				if (!base.SetupContext.IsInstalledAD(role.RoleName))
				{
					list2.Add(InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(role.RoleName).DisplayName);
				}
			}
			if (list2.Count > 0)
			{
				string missingRoles = string.Join(", ", list2.ToArray());
				list.Add(new SetupValidationError(Strings.RoleNotInstalledError(missingRoles)));
			}
			if (this.targetDir == null)
			{
				list.Add(new SetupValidationError(Strings.UnknownDestinationPath));
			}
			if (base.PreviousVersion == null)
			{
				list.Add(new SetupValidationError(Strings.UnknownPreviousVersion));
			}
			SetupLogger.Log(Strings.UpgradeModeDataHandlerHandlersAndWorkUnits(base.DataHandlers.Count, base.WorkUnits.Count));
			list.AddRange(base.Validate());
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x0400019C RID: 412
		private SetupBindingTaskDataHandler preFileCopyDataHandler;

		// Token: 0x0400019D RID: 413
		private SetupBindingTaskDataHandler midFileCopyDataHandler;

		// Token: 0x0400019E RID: 414
		private InstallOrgCfgDataHandler installOrgCfgDataHandler;

		// Token: 0x0400019F RID: 415
		private Dictionary<string, ConfigurationDataHandler> upgradeConfigDataHandlers = new Dictionary<string, ConfigurationDataHandler>();

		// Token: 0x040001A0 RID: 416
		private NonRootLocalLongFullPath targetDir;

		// Token: 0x040001A1 RID: 417
		private bool fileUpgradeNeeded;

		// Token: 0x040001A2 RID: 418
		private UpgradePrecheckDataHandler preCheckDataHandler;

		// Token: 0x040001A3 RID: 419
		private InstallFileDataHandler installFileDataHandler;

		// Token: 0x040001A4 RID: 420
		private UninstallFileDataHandler uninstallFileDataHandler;

		// Token: 0x040001A5 RID: 421
		private RemoveLanguagePackCfgDataHandler removeLanguagePackDataHandler;
	}
}
