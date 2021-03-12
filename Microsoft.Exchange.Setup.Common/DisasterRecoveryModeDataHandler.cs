using System;
using System.Collections.Generic;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DisasterRecoveryModeDataHandler : ModeDataHandler
	{
		// Token: 0x06000184 RID: 388 RVA: 0x000062E4 File Offset: 0x000044E4
		public DisasterRecoveryModeDataHandler(ISetupContext setupContext, MonadConnection connection) : base(setupContext, connection)
		{
			SetupLogger.TraceEnter(new object[0]);
			if (base.SetupContext.ParsedArguments.ContainsKey("targetdir"))
			{
				base.InstallationPath = (NonRootLocalLongFullPath)base.SetupContext.ParsedArguments["targetdir"];
				SetupLogger.Log(Strings.UserSpecifiedTargetDir(base.InstallationPath.PathName));
			}
			if (setupContext.IsInstalledAD("BridgeheadRole"))
			{
				base.SetIsBridgeheadCheckedInternal(true);
			}
			if (setupContext.IsInstalledAD("ClientAccessRole"))
			{
				base.SetIsClientAccessCheckedInternal(true);
			}
			if (setupContext.IsInstalledAD("GatewayRole"))
			{
				base.SetIsGatewayCheckedInternal(true);
			}
			if (setupContext.IsInstalledAD("MailboxRole"))
			{
				base.SetIsMailboxCheckedInternal(true);
			}
			if (setupContext.IsInstalledAD("UnifiedMessagingRole"))
			{
				base.SetIsUnifiedMessagingCheckedInternal(true);
			}
			if (setupContext.IsInstalledAD("CafeRole"))
			{
				base.SetIsCafeCheckedInternal(true);
			}
			if (setupContext.IsInstalledAD("FrontendTransportRole"))
			{
				base.SetIsFrontendTransportCheckedInternal(true);
			}
			if (base.IsBridgeheadChecked || base.IsClientAccessChecked || base.IsGatewayChecked || base.IsMailboxChecked || base.IsUnifiedMessagingChecked || base.IsCafeChecked || base.IsFrontendTransportChecked)
			{
				base.SetIsAdminToolsCheckedInternal(true);
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000642C File Offset: 0x0000462C
		public override List<string> SelectedInstallableUnits
		{
			get
			{
				base.SelectedInstallableUnits.Clear();
				base.SelectedInstallableUnits.Add("LanguagePacks");
				if (this.IsBridgeheadEnabled && base.IsBridgeheadChecked)
				{
					base.SelectedInstallableUnits.Add("BridgeheadRole");
				}
				if (this.IsClientAccessEnabled && base.IsClientAccessChecked)
				{
					base.SelectedInstallableUnits.Add("ClientAccessRole");
				}
				if (this.IsGatewayEnabled && base.IsGatewayChecked)
				{
					base.SelectedInstallableUnits.Add("GatewayRole");
				}
				if (this.IsUnifiedMessagingEnabled && base.IsUnifiedMessagingChecked)
				{
					base.SelectedInstallableUnits.Add("UnifiedMessagingRole");
				}
				if (this.IsMailboxEnabled && base.IsMailboxChecked)
				{
					base.SelectedInstallableUnits.Add("MailboxRole");
				}
				if (base.IsAdminToolsChecked)
				{
					base.SelectedInstallableUnits.Add("AdminToolsRole");
				}
				if (base.IsCafeChecked)
				{
					base.SelectedInstallableUnits.Add("CafeRole");
				}
				if (base.IsFrontendTransportChecked)
				{
					base.SelectedInstallableUnits.Add("FrontendTransportRole");
				}
				return base.SelectedInstallableUnits;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00006542 File Offset: 0x00004742
		public override InstallationModes Mode
		{
			get
			{
				return InstallationModes.DisasterRecovery;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00006545 File Offset: 0x00004745
		public override bool IsBridgeheadEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledAD("BridgeheadRole");
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00006557 File Offset: 0x00004757
		public override bool IsClientAccessEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledAD("ClientAccessRole");
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00006569 File Offset: 0x00004769
		public override bool IsGatewayEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledAD("GatewayRole");
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000657B File Offset: 0x0000477B
		public override bool IsMailboxEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledAD("MailboxRole");
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600018B RID: 395 RVA: 0x0000658D File Offset: 0x0000478D
		public override bool IsUnifiedMessagingEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledAD("UnifiedMessagingRole");
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600018C RID: 396 RVA: 0x0000659F File Offset: 0x0000479F
		public override bool IsCafeEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledAD("CafeRole");
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000065B1 File Offset: 0x000047B1
		public override bool IsFrontendTransportEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledAD("FrontendTransportRole");
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000065C3 File Offset: 0x000047C3
		public override bool IsAdminToolsEnabled
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000065C6 File Offset: 0x000047C6
		public override PreCheckDataHandler PreCheckDataHandler
		{
			get
			{
				if (this.preCheckDataHandler == null)
				{
					this.preCheckDataHandler = new DisasterRecoveryPrecheckDataHandler(base.SetupContext, this, base.Connection);
				}
				return this.preCheckDataHandler;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000065F0 File Offset: 0x000047F0
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

		// Token: 0x06000191 RID: 401 RVA: 0x00006680 File Offset: 0x00004880
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
			if (this.installFileDataHandler == null)
			{
				this.installFileDataHandler = new InstallFileDataHandler(base.SetupContext, new ProductMsiConfigurationInfo(), base.Connection);
			}
			this.installFileDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
			this.installFileDataHandler.TargetDirectory = base.InstallationPath.PathName;
			base.DataHandlers.Add(this.installFileDataHandler);
			if (base.SetupContext.IsDatacenter || base.SetupContext.IsDatacenterDedicated)
			{
				InstallFileDataHandler installFileDataHandler = new InstallFileDataHandler(base.SetupContext, new DatacenterMsiConfigurationInfo(), base.Connection);
				installFileDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
				installFileDataHandler.TargetDirectory = base.InstallationPath.PathName;
				base.DataHandlers.Add(installFileDataHandler);
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x000067B0 File Offset: 0x000049B0
		protected override ConfigurationDataHandler GetInstallableUnitConfigurationDataHandler(string installableUnitName)
		{
			ConfigurationDataHandler configurationDataHandler = null;
			if (base.HasConfigurationDataHandler(installableUnitName) && !this.drConfigDataHandlers.TryGetValue(installableUnitName, out configurationDataHandler))
			{
				if (installableUnitName != null)
				{
					if (installableUnitName == "BridgeheadRole")
					{
						configurationDataHandler = new DisasterRecoveryBhdCfgDataHandler(base.SetupContext, base.Connection);
						goto IL_CD;
					}
					if (installableUnitName == "GatewayRole")
					{
						configurationDataHandler = new DisasterRecoveryGwyCfgDataHandler(base.SetupContext, base.Connection);
						goto IL_CD;
					}
					if (installableUnitName == "AdminToolsRole")
					{
						configurationDataHandler = new DisasterRecoveryAtCfgDataHandler(base.SetupContext, RoleManager.GetRoleByName(installableUnitName), base.Connection);
						goto IL_CD;
					}
					if (installableUnitName == "LanguagePacks")
					{
						configurationDataHandler = new AddLanguagePackCfgDataHandler(base.SetupContext, base.Connection);
						goto IL_CD;
					}
				}
				configurationDataHandler = new DisasterRecoveryCfgDataHandler(base.SetupContext, RoleManager.GetRoleByName(installableUnitName), base.Connection);
				IL_CD:
				this.drConfigDataHandlers.Add(installableUnitName, configurationDataHandler);
			}
			return configurationDataHandler;
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00006898 File Offset: 0x00004A98
		public override bool CanChangeSourceDir
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000689B File Offset: 0x00004A9B
		public override string RoleSelectionDescription
		{
			get
			{
				return Strings.DRServerRoleText;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000195 RID: 405 RVA: 0x000068A7 File Offset: 0x00004AA7
		public override LocalizedString ConfigurationSummary
		{
			get
			{
				return Strings.DRRolesToInstall;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000068AE File Offset: 0x00004AAE
		public override string CompletionDescription
		{
			get
			{
				return base.WorkUnits.HasFailures ? Strings.DRFailText : Strings.DRSuccessText;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000197 RID: 407 RVA: 0x000068CE File Offset: 0x00004ACE
		public override string CompletionStatus
		{
			get
			{
				return base.WorkUnits.HasFailures ? Strings.DRFailStatus : Strings.DRSuccessStatus;
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000068F0 File Offset: 0x00004AF0
		protected override void UpdateDataHandlers()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.DataHandlers.Clear();
			base.AddPreSetupDataHandlers();
			ConfigurationContext.Setup.UseAssemblyPathAsInstallPath();
			SetupLogger.Log(Strings.SetupWillRunFromPath(ConfigurationContext.Setup.InstallPath));
			base.AddFileDataHandlers();
			base.AddLanguagePackFileDataHandlers();
			base.AddPostCopyFileDataHandlers();
			this.AddConfigurationDataHandlers();
			base.AddPostSetupDataHandlers();
			SetupLogger.Log(Strings.DRModeDataHandlerCount(base.DataHandlers.Count));
			SetupLogger.TraceExit();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006970 File Offset: 0x00004B70
		public override ValidationError[] Validate()
		{
			SetupLogger.TraceEnter(new object[0]);
			List<ValidationError> list = new List<ValidationError>();
			if (!base.SetupContext.IsServerFoundInAD)
			{
				list.Add(new SetupValidationError(Strings.DRServerNotFoundInAD));
			}
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
			if (base.SetupContext.UnpackedRoles.Count != 0)
			{
				bool flag = true;
				foreach (Role role2 in base.SetupContext.UnpackedRoles)
				{
					bool flag2 = false;
					if (role2 is AdminToolsRole)
					{
						break;
					}
					foreach (Role role3 in base.SetupContext.PartiallyConfiguredRoles)
					{
						if (string.Compare(role2.RoleName, role3.RoleName, true) == 0)
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						flag = false;
						break;
					}
				}
				if (!flag)
				{
					SetupLogger.Log(Strings.HasConfiguredRoles);
					string[] value = base.SetupContext.UnpackedRoles.ConvertAll<string>((Role r) => r.RoleName).ToArray();
					list.Add(new SetupValidationError(Strings.DRRoleAlreadyInstalledError(string.Join(", ", value))));
				}
			}
			list.AddRange(base.Validate());
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x0400005A RID: 90
		private SetupBindingTaskDataHandler preFileCopyDataHandler;

		// Token: 0x0400005B RID: 91
		private DisasterRecoveryPrecheckDataHandler preCheckDataHandler;

		// Token: 0x0400005C RID: 92
		private InstallFileDataHandler installFileDataHandler;

		// Token: 0x0400005D RID: 93
		private Dictionary<string, ConfigurationDataHandler> drConfigDataHandlers = new Dictionary<string, ConfigurationDataHandler>();
	}
}
