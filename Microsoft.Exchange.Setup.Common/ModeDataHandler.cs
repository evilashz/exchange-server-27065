using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Win32;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200000B RID: 11
	internal abstract class ModeDataHandler : SetupSingleTaskDataHandler, IPrecheckEnabled
	{
		// Token: 0x0600009D RID: 157 RVA: 0x0000448B File Offset: 0x0000268B
		protected void SetIsBridgeheadCheckedInternal(bool value)
		{
			this.isBridgeheadChecked = value;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004494 File Offset: 0x00002694
		protected void SetIsClientAccessCheckedInternal(bool value)
		{
			this.isClientAccessChecked = value;
		}

		// Token: 0x0600009F RID: 159 RVA: 0x0000449D File Offset: 0x0000269D
		protected void SetIsGatewayCheckedInternal(bool value)
		{
			this.isGatewayChecked = value;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x000044A6 File Offset: 0x000026A6
		protected void SetIsMailboxCheckedInternal(bool value)
		{
			this.isMailboxChecked = value;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000044AF File Offset: 0x000026AF
		protected void SetIsUnifiedMessagingCheckedInternal(bool value)
		{
			this.isUnifiedMessagingChecked = value;
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000044B8 File Offset: 0x000026B8
		protected void SetIsFrontendTransportCheckedInternal(bool value)
		{
			this.isFrontendTransportChecked = value;
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000044C1 File Offset: 0x000026C1
		protected void SetIsCafeCheckedInternal(bool value)
		{
			this.isCafeChecked = value;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000044CA File Offset: 0x000026CA
		protected void SetIsAdminToolsCheckedInternal(bool value)
		{
			this.isAdminToolsChecked = value;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000044D3 File Offset: 0x000026D3
		protected void SetIsCentralAdminCheckedInternal(bool value)
		{
			this.isCentralAdminChecked = value;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000044DC File Offset: 0x000026DC
		protected void SetIsCentralAdminDatabaseCheckedInternal(bool value)
		{
			this.isCentralAdminDatabaseChecked = value;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000044E5 File Offset: 0x000026E5
		protected void SetIsCentralAdminFrontEndCheckedInternal(bool value)
		{
			this.isCentralAdminFrontEndChecked = value;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000044EE File Offset: 0x000026EE
		protected void SetIsMonitoringCheckedInternal(bool value)
		{
			this.isMonitoringChecked = value;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x000044F7 File Offset: 0x000026F7
		protected void SetIsLanguagePacksCheckedInternal(bool value)
		{
			this.isLanguagePacksChecked = value;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004500 File Offset: 0x00002700
		protected void SetIsOSPCheckedInternal(bool value)
		{
			this.isOSPChecked = value;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000450C File Offset: 0x0000270C
		public ModeDataHandler(ISetupContext context, MonadConnection connection) : base(context, "", connection)
		{
			this.ResetInstallSettings();
			if (base.SetupContext.IsInstalledLocalOrAD("BridgeheadRole"))
			{
				this.isBridgeheadChecked = true;
			}
			if (base.SetupContext.IsInstalledLocalOrAD("FrontendTransportRole"))
			{
				this.isFrontendTransportChecked = true;
			}
			if (base.SetupContext.IsInstalledLocalOrAD("ClientAccessRole"))
			{
				this.isClientAccessChecked = true;
			}
			if (base.SetupContext.IsInstalledLocalOrAD("GatewayRole"))
			{
				this.isGatewayChecked = true;
			}
			if (base.SetupContext.IsInstalledLocalOrAD("MailboxRole"))
			{
				this.isMailboxChecked = true;
			}
			if (base.SetupContext.IsInstalledLocalOrAD("UnifiedMessagingRole"))
			{
				this.isUnifiedMessagingChecked = true;
			}
			if (base.SetupContext.IsInstalledLocal("AdminToolsRole"))
			{
				this.isAdminToolsChecked = true;
			}
			if (base.SetupContext.IsInstalledLocalOrAD("CafeRole"))
			{
				this.isCafeChecked = true;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000045F4 File Offset: 0x000027F4
		internal void ResetInstallSettings()
		{
			this.isBridgeheadChecked = false;
			this.isClientAccessChecked = false;
			this.isGatewayChecked = false;
			this.isMailboxChecked = false;
			this.isUnifiedMessagingChecked = false;
			this.isAdminToolsChecked = false;
			this.isCafeChecked = false;
			this.selectedInstallableUnits = new List<string>();
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AD RID: 173 RVA: 0x00004632 File Offset: 0x00002832
		// (set) Token: 0x060000AE RID: 174 RVA: 0x0000463A File Offset: 0x0000283A
		protected Version PreviousVersion
		{
			get
			{
				return this.previousVersion;
			}
			set
			{
				this.previousVersion = value;
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004643 File Offset: 0x00002843
		public bool IsInstalledLocal(string roleName)
		{
			return base.SetupContext.IsInstalledLocal(roleName);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00004651 File Offset: 0x00002851
		public bool ExchangeOrganizationExists
		{
			get
			{
				return base.SetupContext.ExchangeOrganizationExists;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000465E File Offset: 0x0000285E
		public bool ExchangeOrganizationFoundInAD
		{
			get
			{
				return base.SetupContext.OrganizationNameFoundInAD != null && !string.IsNullOrEmpty(base.SetupContext.OrganizationNameFoundInAD.EscapedName);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00004687 File Offset: 0x00002887
		public bool IsADPrepTasksRequired
		{
			get
			{
				return base.SetupContext.IsSchemaUpdateRequired || base.SetupContext.IsOrgConfigUpdateRequired;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000046A3 File Offset: 0x000028A3
		public bool IsInstalledAD(string roleName)
		{
			return base.SetupContext.IsInstalledAD(roleName);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000046B1 File Offset: 0x000028B1
		public bool HasMailboxServers
		{
			get
			{
				return base.SetupContext.HasMailboxServers;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000046BE File Offset: 0x000028BE
		public bool HasLegacyServers
		{
			get
			{
				return base.SetupContext.HasLegacyServers;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000046CB File Offset: 0x000028CB
		public bool HasBridgeheadServers
		{
			get
			{
				return base.SetupContext.HasBridgeheadServers;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000046D8 File Offset: 0x000028D8
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x000046E5 File Offset: 0x000028E5
		public bool InstallWindowsComponents
		{
			get
			{
				return base.SetupContext.InstallWindowsComponents;
			}
			set
			{
				base.SetupContext.InstallWindowsComponents = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x000046F3 File Offset: 0x000028F3
		public bool InstallWindowsComponentsEnabled
		{
			get
			{
				return this.Mode != InstallationModes.Uninstall;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00004701 File Offset: 0x00002901
		public virtual List<string> SelectedInstallableUnits
		{
			get
			{
				return this.selectedInstallableUnits;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000BB RID: 187 RVA: 0x0000470C File Offset: 0x0000290C
		protected bool ServerRoleIsSelected
		{
			get
			{
				return this.SelectedInstallableUnits.Contains("ClientAccessRole") || this.SelectedInstallableUnits.Contains("BridgeheadRole") || this.SelectedInstallableUnits.Contains("UnifiedMessagingRole") || this.SelectedInstallableUnits.Contains("MailboxRole") || this.SelectedInstallableUnits.Contains("GatewayRole") || this.SelectedInstallableUnits.Contains("FrontendTransportRole") || this.SelectedInstallableUnits.Contains("CafeRole");
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000BC RID: 188
		public abstract InstallationModes Mode { get; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000BD RID: 189
		public abstract PreCheckDataHandler PreCheckDataHandler { get; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004798 File Offset: 0x00002998
		public bool IsLanguagePackOnlyInstallation
		{
			get
			{
				bool result = false;
				if (!base.SetupContext.IsCleanMachine && this.Mode == InstallationModes.Install)
				{
					int num = 0;
					if (this.SelectedInstallableUnits.Contains("LanguagePacks"))
					{
						num++;
					}
					result = (num > 0 && num == this.SelectedInstallableUnits.Count);
				}
				return result;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000047EC File Offset: 0x000029EC
		protected void AddFileDataHandlers()
		{
			if (this.NeedFileDataHandler())
			{
				this.AddNeededFileDataHandlers();
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000047FC File Offset: 0x000029FC
		protected void AddPreSetupDataHandlers()
		{
			if (this.NeedPrePostSetupDataHandlers)
			{
				if (this.preSetupDataHandler == null)
				{
					this.preSetupDataHandler = new SetupBindingTaskDataHandler(BindingCategory.PreSetup, base.SetupContext, base.Connection);
				}
				this.preSetupDataHandler.Mode = this.Mode;
				this.preSetupDataHandler.PreviousVersion = this.previousVersion;
				this.preSetupDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
				base.DataHandlers.Add(this.preSetupDataHandler);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004878 File Offset: 0x00002A78
		protected void AddPostSetupDataHandlers()
		{
			if (this.NeedPrePostSetupDataHandlers)
			{
				if (this.postSetupDataHandler == null)
				{
					this.postSetupDataHandler = new SetupBindingTaskDataHandler(BindingCategory.PostSetup, base.SetupContext, base.Connection);
				}
				this.postSetupDataHandler.Mode = this.Mode;
				this.postSetupDataHandler.PreviousVersion = this.previousVersion;
				this.postSetupDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
				base.DataHandlers.Add(this.postSetupDataHandler);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000048F1 File Offset: 0x00002AF1
		protected virtual bool NeedPrePostSetupDataHandlers
		{
			get
			{
				return this.SelectedInstallableUnits.Count != 0;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004904 File Offset: 0x00002B04
		protected virtual bool NeedFileDataHandler()
		{
			return this.SelectedInstallableUnits.Count != 0;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004917 File Offset: 0x00002B17
		protected virtual void AddNeededFileDataHandlers()
		{
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000491C File Offset: 0x00002B1C
		protected void AddPostCopyFileDataHandlers()
		{
			SetupLogger.TraceEnter(new object[0]);
			if (this.NeedFileDataHandler())
			{
				if (this.postFileCopyDataHandler == null)
				{
					this.postFileCopyDataHandler = new SetupBindingTaskDataHandler(BindingCategory.PostFileCopy, base.SetupContext, base.Connection);
				}
				this.postFileCopyDataHandler.Mode = this.Mode;
				this.postFileCopyDataHandler.PreviousVersion = this.PreviousVersion;
				this.postFileCopyDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
				base.DataHandlers.Add(this.postFileCopyDataHandler);
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x060000C6 RID: 198
		protected abstract void UpdateDataHandlers();

		// Token: 0x060000C7 RID: 199 RVA: 0x000049A8 File Offset: 0x00002BA8
		protected virtual void AddConfigurationDataHandlers()
		{
			foreach (string installableUnitName in this.SelectedInstallableUnits.ToArray())
			{
				ConfigurationDataHandler installableUnitConfigurationDataHandler = this.GetInstallableUnitConfigurationDataHandler(installableUnitName);
				if (installableUnitConfigurationDataHandler != null)
				{
					installableUnitConfigurationDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
					base.DataHandlers.Add(installableUnitConfigurationDataHandler);
				}
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000049F8 File Offset: 0x00002BF8
		protected void AddLanguagePackFileDataHandlers()
		{
			if (this.NeedFileDataHandler())
			{
				AddLanguagePackFileCopyDataHandler addLanguagePackFileCopyDataHandler = new AddLanguagePackFileCopyDataHandler(base.SetupContext, base.Connection);
				addLanguagePackFileCopyDataHandler.SelectedInstallableUnits = this.SelectedInstallableUnits;
				base.DataHandlers.Add(addLanguagePackFileCopyDataHandler);
			}
		}

		// Token: 0x060000C9 RID: 201
		protected abstract ConfigurationDataHandler GetInstallableUnitConfigurationDataHandler(string installableUnitName);

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000CA RID: 202
		public abstract string RoleSelectionDescription { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000CB RID: 203
		public abstract LocalizedString ConfigurationSummary { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00004A37 File Offset: 0x00002C37
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00004A3F File Offset: 0x00002C3F
		public bool IsBridgeheadChecked
		{
			get
			{
				return this.isBridgeheadChecked;
			}
			set
			{
				if (value != this.IsBridgeheadChecked)
				{
					this.isBridgeheadChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004A57 File Offset: 0x00002C57
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00004A5F File Offset: 0x00002C5F
		public bool IsClientAccessChecked
		{
			get
			{
				return this.isClientAccessChecked;
			}
			set
			{
				if (value != this.IsClientAccessChecked)
				{
					this.isClientAccessChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004A77 File Offset: 0x00002C77
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00004A7F File Offset: 0x00002C7F
		public bool IsGatewayChecked
		{
			get
			{
				return this.isGatewayChecked;
			}
			set
			{
				if (value != this.IsGatewayChecked)
				{
					this.isGatewayChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004A97 File Offset: 0x00002C97
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00004A9F File Offset: 0x00002C9F
		public bool IsMailboxChecked
		{
			get
			{
				return this.isMailboxChecked;
			}
			set
			{
				if (value != this.IsMailboxChecked)
				{
					this.isMailboxChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004AB7 File Offset: 0x00002CB7
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004ABF File Offset: 0x00002CBF
		public bool IsCafeChecked
		{
			get
			{
				return this.isCafeChecked;
			}
			set
			{
				if (value != this.IsCafeChecked)
				{
					this.isCafeChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004AD7 File Offset: 0x00002CD7
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00004ADF File Offset: 0x00002CDF
		public bool IsUnifiedMessagingChecked
		{
			get
			{
				return this.isUnifiedMessagingChecked;
			}
			set
			{
				if (value != this.IsUnifiedMessagingChecked)
				{
					this.isUnifiedMessagingChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004AF7 File Offset: 0x00002CF7
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00004AFF File Offset: 0x00002CFF
		public bool IsFrontendTransportChecked
		{
			get
			{
				return this.isFrontendTransportChecked;
			}
			set
			{
				if (value != this.IsFrontendTransportChecked)
				{
					this.isFrontendTransportChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004B17 File Offset: 0x00002D17
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00004B1F File Offset: 0x00002D1F
		public bool IsCentralAdminChecked
		{
			get
			{
				return this.isCentralAdminChecked;
			}
			set
			{
				if (value != this.IsCentralAdminChecked)
				{
					this.isCentralAdminChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00004B37 File Offset: 0x00002D37
		// (set) Token: 0x060000DD RID: 221 RVA: 0x00004B3F File Offset: 0x00002D3F
		public bool IsCentralAdminDatabaseChecked
		{
			get
			{
				return this.isCentralAdminDatabaseChecked;
			}
			set
			{
				if (value != this.IsCentralAdminDatabaseChecked)
				{
					this.isCentralAdminDatabaseChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00004B57 File Offset: 0x00002D57
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00004B5F File Offset: 0x00002D5F
		public bool IsCentralAdminFrontEndChecked
		{
			get
			{
				return this.isCentralAdminFrontEndChecked;
			}
			set
			{
				if (value != this.IsCentralAdminFrontEndChecked)
				{
					this.isCentralAdminFrontEndChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00004B77 File Offset: 0x00002D77
		// (set) Token: 0x060000E1 RID: 225 RVA: 0x00004B7F File Offset: 0x00002D7F
		public bool IsMonitoringChecked
		{
			get
			{
				return this.isMonitoringChecked;
			}
			set
			{
				if (value != this.IsMonitoringChecked)
				{
					this.isMonitoringChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00004B98 File Offset: 0x00002D98
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00004C1A File Offset: 0x00002E1A
		public bool IsAdminToolsChecked
		{
			get
			{
				return (!base.SetupContext.HasPrepareADParameters && (this.IsBridgeheadChecked || this.IsClientAccessChecked || this.IsGatewayChecked || this.IsMailboxChecked || this.IsUnifiedMessagingChecked || this.IsFrontendTransportChecked || this.IsCafeChecked || this.IsCentralAdminChecked || this.IsCentralAdminDatabaseChecked || this.IsCentralAdminFrontEndChecked || this.IsMonitoringChecked || this.IsOSPChecked)) || this.isAdminToolsChecked;
			}
			set
			{
				if (value != this.IsAdminToolsChecked)
				{
					this.isAdminToolsChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00004C32 File Offset: 0x00002E32
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00004C3A File Offset: 0x00002E3A
		public bool IsLanguagePacksChecked
		{
			get
			{
				return this.isLanguagePacksChecked;
			}
			set
			{
				if (value != this.IsLanguagePacksChecked)
				{
					this.isLanguagePacksChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004C52 File Offset: 0x00002E52
		// (set) Token: 0x060000E7 RID: 231 RVA: 0x00004C5A File Offset: 0x00002E5A
		public bool IsOSPChecked
		{
			get
			{
				return this.isOSPChecked;
			}
			set
			{
				if (value != this.IsOSPChecked)
				{
					this.isOSPChecked = value;
					this.UpdateDataHandlers();
				}
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00004C72 File Offset: 0x00002E72
		public virtual bool IsBridgeheadEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("BridgeheadRole");
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00004C84 File Offset: 0x00002E84
		public virtual bool IsClientAccessEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("ClientAccessRole");
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00004C96 File Offset: 0x00002E96
		public virtual bool IsGatewayEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("GatewayRole");
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00004CA8 File Offset: 0x00002EA8
		public virtual bool IsMailboxEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("MailboxRole");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00004CBA File Offset: 0x00002EBA
		public virtual bool IsCentralAdminDatabaseEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("CentralAdminDatabaseRole");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00004CCC File Offset: 0x00002ECC
		public virtual bool IsCentralAdminEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("CentralAdminRole");
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00004CDE File Offset: 0x00002EDE
		public virtual bool IsCentralAdminFrontEndEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("CentralAdminFrontEndRole");
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00004CF0 File Offset: 0x00002EF0
		public virtual bool IsMonitoringEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("MonitoringRole");
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00004D02 File Offset: 0x00002F02
		protected bool IsADDependentRoleChecked
		{
			get
			{
				return this.IsCafeChecked || this.IsFrontendTransportChecked || this.IsBridgeheadChecked || this.IsClientAccessChecked || this.IsMailboxChecked || this.IsUnifiedMessagingChecked;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00004D34 File Offset: 0x00002F34
		public virtual bool IsUnifiedMessagingEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("UnifiedMessagingRole");
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00004D46 File Offset: 0x00002F46
		public virtual bool IsFrontendTransportEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("FrontendTransportRole");
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00004D58 File Offset: 0x00002F58
		public virtual bool IsCafeEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("CafeRole");
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x00004D6A File Offset: 0x00002F6A
		public virtual bool IsAdminToolsEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("AdminToolsRole");
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00004D7C File Offset: 0x00002F7C
		public virtual bool IsOSPEnabled
		{
			get
			{
				return base.SetupContext.IsInstalledLocal("OSPRole");
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00004D90 File Offset: 0x00002F90
		public virtual decimal RequiredDiskSpace
		{
			get
			{
				decimal num = 0m;
				foreach (string installableUnitName in this.SelectedInstallableUnits)
				{
					num += InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(installableUnitName).Size;
				}
				return num;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00004DF8 File Offset: 0x00002FF8
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00004EB6 File Offset: 0x000030B6
		public NonRootLocalLongFullPath InstallationPath
		{
			get
			{
				if (base.SetupContext.TargetDir == null)
				{
					if (!base.SetupContext.IsCleanMachine)
					{
						base.SetupContext.TargetDir = base.SetupContext.InstalledPath;
					}
					else if (base.SetupContext.BackupInstalledPath != null)
					{
						base.SetupContext.TargetDir = base.SetupContext.BackupInstalledPath;
					}
					else
					{
						string path = "V" + 15.ToString();
						string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
						string path2 = Path.Combine("Microsoft\\Exchange Server", path);
						base.SetupContext.TargetDir = NonRootLocalLongFullPath.Parse(Path.Combine(folderPath, path2));
					}
				}
				return base.SetupContext.TargetDir;
			}
			set
			{
				if (this.InstallationPath != value && this.CanChangeInstallationPath)
				{
					base.SetupContext.TargetDir = value;
				}
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00004EDA File Offset: 0x000030DA
		public bool CanChangeInstallationPath
		{
			get
			{
				return base.SetupContext.IsCleanMachine;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000FA RID: 250
		public abstract bool CanChangeSourceDir { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004EE7 File Offset: 0x000030E7
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00004EF4 File Offset: 0x000030F4
		public LongPath SourceDir
		{
			get
			{
				return base.SetupContext.SourceDir;
			}
			set
			{
				if (null == value)
				{
					throw new SourceDirNotSpecifiedException();
				}
				base.SetupContext.SourceDir = value;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00004F14 File Offset: 0x00003114
		public decimal AvailableDiskSpace
		{
			get
			{
				decimal result = 0.0m;
				if (this.InstallationPath != null)
				{
					try
					{
						DriveInfo driveInfo = new DriveInfo(this.InstallationPath.DriveName);
						result = driveInfo.AvailableFreeSpace / 1048576m;
					}
					catch (ArgumentException)
					{
					}
					catch (IOException)
					{
					}
				}
				return result;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004F8C File Offset: 0x0000318C
		public virtual bool HasChanges
		{
			get
			{
				return this.SelectedInstallableUnits.Count > 0;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004F9C File Offset: 0x0000319C
		public virtual bool RebootRequired
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004FA0 File Offset: 0x000031A0
		public static ValidationError[] ValidateInstallationPath(string path, bool skipDriveFormatCheck = false)
		{
			SetupLogger.TraceEnter(new object[0]);
			List<ValidationError> list = new List<ValidationError>();
			try
			{
				DriveInfo driveInfo = new DriveInfo(path);
				if (driveInfo.DriveType != DriveType.Fixed)
				{
					list.Add(new SetupValidationError(Strings.InstallationPathInvalidDriveTypeInformation(path)));
				}
				if (!skipDriveFormatCheck && driveInfo.DriveFormat != "NTFS")
				{
					list.Add(new SetupValidationError(Strings.InstallationPathInvalidDriveFormatInformation(path)));
				}
				string targetPath = ModeDataHandler.NormalizePath(path);
				if (ModeDataHandler.UnderPaths(targetPath, new string[]
				{
					ModeDataHandler.GetProfilesDirectory()
				}))
				{
					list.Add(new SetupValidationError(Strings.InstallationPathInvalidProfilesInformation(path)));
				}
				if (ModeDataHandler.UnderPaths(targetPath, ModeDataHandler.GetShellFolders()))
				{
					list.Add(new SetupValidationError(Strings.InstallationPathUnderUserProfileInformation(path)));
				}
			}
			catch (IOException)
			{
				list.Add(new SetupValidationError(Strings.InstallationPathInvalidInformation(path)));
			}
			catch (ArgumentException)
			{
				list.Add(new SetupValidationError(Strings.InstallationPathInvalidInformation(path)));
			}
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000050A4 File Offset: 0x000032A4
		private static string NormalizePath(string path)
		{
			string text = null;
			if (path != null)
			{
				text = path.Trim();
				if (text != string.Empty && text[text.Length - 1] != Path.DirectorySeparatorChar)
				{
					StringBuilder stringBuilder = new StringBuilder(text);
					stringBuilder.Append(Path.DirectorySeparatorChar);
					text = stringBuilder.ToString();
				}
			}
			return text;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000050FC File Offset: 0x000032FC
		private static string GetProfilesDirectory()
		{
			string path = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\ProfileList", "ProfilesDirectory", string.Empty);
			return ModeDataHandler.NormalizePath(path);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000512C File Offset: 0x0000332C
		private static string[] GetShellFolders()
		{
			List<string> list = new List<string>();
			string[] result;
			using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Explorer\\Shell Folders"))
			{
				if (registryKey != null)
				{
					string[] valueNames = registryKey.GetValueNames();
					foreach (string name in valueNames)
					{
						string path = registryKey.GetValue(name) as string;
						list.Add(ModeDataHandler.NormalizePath(path));
					}
				}
				result = list.ToArray();
			}
			return result;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000051B8 File Offset: 0x000033B8
		private static bool UnderPaths(string targetPath, params string[] paths)
		{
			bool flag = false;
			foreach (string text in paths)
			{
				flag = (!string.IsNullOrEmpty(text) && targetPath.StartsWith(text, true, CultureInfo.InvariantCulture));
				if (flag)
				{
					SetupLogger.Log(Strings.TheFirstPathUnderTheSecondPath(targetPath, text));
					break;
				}
			}
			return flag;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00005208 File Offset: 0x00003408
		public ValidationError[] CheckForADErrors(bool throwADErrors)
		{
			List<ValidationError> list = new List<ValidationError>();
			if (((this.IsADDependentRoleChecked || base.SetupContext.HasPrepareADParameters || base.SetupContext.HasNewProvisionedServerParameters || base.SetupContext.HasRemoveProvisionedServerParameters) && !this.IgnoreValidatingRoleChanges && !base.SetupContext.ADInitializedSuccessfully) || (this.IgnoreValidatingRoleChanges && !base.SetupContext.ADInitializedSuccessfully))
			{
				LocalizedException ex;
				if (base.SetupContext.ADInitializationError != null)
				{
					ex = base.SetupContext.ADInitializationError;
				}
				else
				{
					ex = new ADRelatedUnknownErrorException();
				}
				SetupLogger.Log(ex.LocalizedString);
				if (throwADErrors)
				{
					throw ex;
				}
				list.Add(new SetupValidationError(ex.LocalizedString));
			}
			return list.ToArray();
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000052BC File Offset: 0x000034BC
		public override ValidationError[] Validate()
		{
			SetupLogger.TraceEnter(new object[0]);
			List<ValidationError> list = new List<ValidationError>();
			list.AddRange(ModeDataHandler.ValidateInstallationPath(this.InstallationPath.PathName, base.SetupContext.InstallationMode == InstallationModes.Uninstall));
			if (!this.IgnoreValidatingRoleChanges && this.RequiredDiskSpace > this.AvailableDiskSpace)
			{
				list.Add(new SetupValidationError(Strings.NotEnoughSpace(Math.Round(this.RequiredDiskSpace / 1024m, 2, MidpointRounding.AwayFromZero).ToString())));
			}
			list.AddRange(base.Validate());
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00005364 File Offset: 0x00003564
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000536C File Offset: 0x0000356C
		public bool IgnoreValidatingRoleChanges
		{
			get
			{
				return this.ignoreValidatingRoleChanges;
			}
			set
			{
				this.ignoreValidatingRoleChanges = value;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005375 File Offset: 0x00003575
		protected bool HasConfigurationDataHandler(string installableUnitName)
		{
			return InstallableUnitConfigurationInfoManager.IsRoleBasedConfigurableInstallableUnit(installableUnitName) || installableUnitName == "LanguagePacks" || InstallableUnitConfigurationInfoManager.IsUmLanguagePackInstallableUnit(installableUnitName);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00005394 File Offset: 0x00003594
		public virtual void UpdatePreCheckTaskDataHandler()
		{
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00005396 File Offset: 0x00003596
		public override void UpdateWorkUnits()
		{
			SetupLogger.TraceEnter(new object[0]);
			this.UpdateDataHandlers();
			base.UpdateWorkUnits();
			SetupLogger.TraceExit();
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000053B4 File Offset: 0x000035B4
		protected bool OrgLevelConfigRequired()
		{
			bool result = false;
			if (this.Mode == InstallationModes.Install || this.Mode == InstallationModes.BuildToBuildUpgrade)
			{
				if (new InstallOrgCfgDataHandler(base.SetupContext, base.Connection)
				{
					SelectedInstallableUnits = this.SelectedInstallableUnits
				}.WillDataHandlerDoAnyWork())
				{
					result = true;
				}
			}
			else if (this.Mode == InstallationModes.Uninstall)
			{
				UninstallOrgCfgDataHandler uninstallOrgCfgDataHandler = new UninstallOrgCfgDataHandler(base.SetupContext, base.Connection);
				if (uninstallOrgCfgDataHandler.WillDataHandlerDoAnyWork())
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00005428 File Offset: 0x00003628
		[Conditional("DEBUG")]
		protected void AssertDataHandlerIsAddedAtMostOnce(DataHandler datahandler, string name)
		{
			int num = 0;
			foreach (DataHandler dataHandler in base.DataHandlers)
			{
				if (dataHandler == datahandler)
				{
					num++;
				}
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00005478 File Offset: 0x00003678
		protected LongPath UmSourceDir
		{
			get
			{
				if (base.SetupContext.SourceDir == null)
				{
					return null;
				}
				string path = Path.Combine(base.SetupContext.SourceDir.PathName, "en");
				LongPath result;
				if (!LongPath.TryParse(path, out result))
				{
					return null;
				}
				return result;
			}
		}

		// Token: 0x0400002A RID: 42
		private const string DefaultInstallationPath = "Microsoft\\Exchange Server";

		// Token: 0x0400002B RID: 43
		private bool isBridgeheadChecked;

		// Token: 0x0400002C RID: 44
		private bool isClientAccessChecked;

		// Token: 0x0400002D RID: 45
		private bool isGatewayChecked;

		// Token: 0x0400002E RID: 46
		private bool isMailboxChecked;

		// Token: 0x0400002F RID: 47
		private bool isUnifiedMessagingChecked;

		// Token: 0x04000030 RID: 48
		private bool isFrontendTransportChecked;

		// Token: 0x04000031 RID: 49
		private bool isCafeChecked;

		// Token: 0x04000032 RID: 50
		private bool isAdminToolsChecked;

		// Token: 0x04000033 RID: 51
		private bool isLanguagePacksChecked;

		// Token: 0x04000034 RID: 52
		private bool isCentralAdminChecked;

		// Token: 0x04000035 RID: 53
		private bool isCentralAdminDatabaseChecked;

		// Token: 0x04000036 RID: 54
		private bool isCentralAdminFrontEndChecked;

		// Token: 0x04000037 RID: 55
		private bool isMonitoringChecked;

		// Token: 0x04000038 RID: 56
		private bool isOSPChecked;

		// Token: 0x04000039 RID: 57
		private List<string> selectedInstallableUnits;

		// Token: 0x0400003A RID: 58
		private SetupBindingTaskDataHandler preSetupDataHandler;

		// Token: 0x0400003B RID: 59
		private SetupBindingTaskDataHandler postSetupDataHandler;

		// Token: 0x0400003C RID: 60
		private SetupBindingTaskDataHandler postFileCopyDataHandler;

		// Token: 0x0400003D RID: 61
		private Version previousVersion;

		// Token: 0x0400003E RID: 62
		private bool ignoreValidatingRoleChanges;
	}
}
