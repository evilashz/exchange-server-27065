using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000056 RID: 86
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RootDataHandler : SetupSingleTaskDataHandler
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060003B3 RID: 947 RVA: 0x0000CF4B File Offset: 0x0000B14B
		public LongPath SetupSourceDir
		{
			get
			{
				return base.SetupContext.SourceDir;
			}
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000CF58 File Offset: 0x0000B158
		public bool IsCleanMachine
		{
			get
			{
				return base.SetupContext.IsCleanMachine;
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000CF65 File Offset: 0x0000B165
		public RootDataHandler(Dictionary<string, object> parsedArguments, MonadConnection connection) : base(null, "", connection)
		{
			this.SuppressValidateSourceDir = false;
			this.hasReadData = false;
			this.parsedArguments = parsedArguments;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000CF89 File Offset: 0x0000B189
		public RootDataHandler(ISetupContext context, MonadConnection connection) : base(context, "", connection)
		{
			this.SuppressValidateSourceDir = false;
			this.hasReadData = false;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000CFA6 File Offset: 0x0000B1A6
		public void LoadState(UserChoiceState userChoiceState)
		{
			if (userChoiceState != null)
			{
				userChoiceState.WriteToContext(base.SetupContext, this.ModeDatahandler);
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000CFBD File Offset: 0x0000B1BD
		protected override void OnSaveData()
		{
			UserChoiceState.DeleteFile();
			base.OnSaveData();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000CFCA File Offset: 0x0000B1CA
		protected override void OnReadData()
		{
			SetupLogger.TraceEnter(new object[0]);
			if (!this.hasReadData)
			{
				if (base.SetupContext == null)
				{
					base.SetupContext = new SetupContext(this.parsedArguments);
				}
				this.hasReadData = true;
				this.OnReadDataFinished();
			}
			SetupLogger.TraceExit();
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060003BA RID: 954 RVA: 0x0000D00C File Offset: 0x0000B20C
		// (remove) Token: 0x060003BB RID: 955 RVA: 0x0000D044 File Offset: 0x0000B244
		public event EventHandler ReadDataFinished;

		// Token: 0x060003BC RID: 956 RVA: 0x0000D079 File Offset: 0x0000B279
		private void OnReadDataFinished()
		{
			if (this.ReadDataFinished != null)
			{
				this.ReadDataFinished(this, EventArgs.Empty);
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000D094 File Offset: 0x0000B294
		// (set) Token: 0x060003BE RID: 958 RVA: 0x0000D0A1 File Offset: 0x0000B2A1
		public string CurrentWizardPageName
		{
			get
			{
				return base.SetupContext.CurrentWizardPageName;
			}
			set
			{
				base.SetupContext.CurrentWizardPageName = value;
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060003BF RID: 959 RVA: 0x0000D0AF File Offset: 0x0000B2AF
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x0000D0BC File Offset: 0x0000B2BC
		public bool IsRestoredFromPreviousState
		{
			get
			{
				return base.SetupContext.IsRestoredFromPreviousState;
			}
			set
			{
				base.SetupContext.IsRestoredFromPreviousState = value;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x0000D0CA File Offset: 0x0000B2CA
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x0000D0D7 File Offset: 0x0000B2D7
		public bool IsUmLanguagePackOperation
		{
			get
			{
				return base.SetupContext.IsUmLanguagePackOperation;
			}
			set
			{
				base.SetupContext.IsUmLanguagePackOperation = value;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000D0E5 File Offset: 0x0000B2E5
		public List<CultureInfo> InstalledUMLanguagePacks
		{
			get
			{
				return base.SetupContext.InstalledUMLanguagePacks;
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000D0F2 File Offset: 0x0000B2F2
		public List<CultureInfo> SelectedCultures
		{
			get
			{
				return base.SetupContext.SelectedCultures;
			}
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000D0FF File Offset: 0x0000B2FF
		public RoleCollection RequestedRoles
		{
			get
			{
				return base.SetupContext.RequestedRoles;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000D10C File Offset: 0x0000B30C
		public List<string> RequestedInstallableUnits
		{
			get
			{
				return this.ModeDatahandler.SelectedInstallableUnits;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000D119 File Offset: 0x0000B319
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000D126 File Offset: 0x0000B326
		public InstallationModes Mode
		{
			get
			{
				return base.SetupContext.InstallationMode;
			}
			set
			{
				base.SetupContext.InstallationMode = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000D134 File Offset: 0x0000B334
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000D141 File Offset: 0x0000B341
		public bool IsLanguagePackOperation
		{
			get
			{
				return base.SetupContext.IsLanguagePackOperation;
			}
			set
			{
				base.SetupContext.IsLanguagePackOperation = value;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000D14F File Offset: 0x0000B34F
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000D15C File Offset: 0x0000B35C
		public LongPath LanguagePackPath
		{
			get
			{
				return base.SetupContext.LanguagePackPath;
			}
			set
			{
				base.SetupContext.LanguagePackPath = value;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000D16C File Offset: 0x0000B36C
		public ModeDataHandler ModeDatahandler
		{
			get
			{
				ModeDataHandler result = null;
				switch (this.Mode)
				{
				case InstallationModes.Install:
					if (base.SetupContext.IsUmLanguagePackOperation)
					{
						if (this.addUmLanguagePackModeDataHandler == null)
						{
							this.addUmLanguagePackModeDataHandler = new AddUmLanguagePackModeDataHandler(base.SetupContext, base.Connection);
						}
						result = this.addUmLanguagePackModeDataHandler;
					}
					else
					{
						if (this.installModeDataHandler == null)
						{
							this.installModeDataHandler = new InstallModeDataHandler(base.SetupContext, base.Connection);
						}
						result = this.installModeDataHandler;
					}
					break;
				case InstallationModes.BuildToBuildUpgrade:
					if (this.upgradeModeDataHandler == null)
					{
						this.upgradeModeDataHandler = new UpgradeModeDataHandler(base.SetupContext, base.Connection);
					}
					result = this.upgradeModeDataHandler;
					break;
				case InstallationModes.DisasterRecovery:
					if (this.disasterRecoveryModeDataHandler == null)
					{
						this.disasterRecoveryModeDataHandler = new DisasterRecoveryModeDataHandler(base.SetupContext, base.Connection);
					}
					result = this.disasterRecoveryModeDataHandler;
					break;
				case InstallationModes.Uninstall:
					if (base.SetupContext.IsUmLanguagePackOperation)
					{
						if (this.removeUmLanguagePackModeDataHandler == null)
						{
							this.removeUmLanguagePackModeDataHandler = new RemoveUmLanguagePackModeDataHandler(base.SetupContext, base.Connection);
						}
						result = this.removeUmLanguagePackModeDataHandler;
					}
					else
					{
						if (this.uninstallModeDataHandler == null)
						{
							this.uninstallModeDataHandler = new UninstallModeDataHandler(base.SetupContext, base.Connection);
						}
						result = this.uninstallModeDataHandler;
					}
					break;
				}
				return result;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060003CE RID: 974 RVA: 0x0000D2AE File Offset: 0x0000B4AE
		public bool TreatPreReqErrorsAsWarningsInDC
		{
			get
			{
				return base.SetupContext.IsDatacenter && (!base.SetupContext.IsDatacenter || base.SetupContext.TreatPreReqErrorsAsWarnings);
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000D2DC File Offset: 0x0000B4DC
		public string IntroTitle
		{
			get
			{
				string result = string.Empty;
				switch (this.Mode)
				{
				case InstallationModes.Install:
				case InstallationModes.Uninstall:
					result = (this.IsCleanMachine ? Strings.FreshIntroduction : Strings.MaintenanceIntroduction);
					break;
				case InstallationModes.BuildToBuildUpgrade:
					result = Strings.UpgradeIntroduction;
					break;
				}
				return result;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x0000D338 File Offset: 0x0000B538
		public string IntroDescription
		{
			get
			{
				string result = string.Empty;
				switch (this.Mode)
				{
				case InstallationModes.Install:
					result = (this.IsCleanMachine ? Strings.FreshIntroductionText : Strings.AddIntroductionText);
					break;
				case InstallationModes.BuildToBuildUpgrade:
					result = Strings.UpgradeIntroductionText;
					break;
				case InstallationModes.Uninstall:
					result = ((this.InstalledUMLanguagePacks.Count > 1) ? Strings.RemoveUnifiedMessagingServerDescription : Strings.RemoveIntroductionText);
					break;
				}
				return result;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
		public string LicenseAgreementShortDescription
		{
			get
			{
				string result = string.Empty;
				switch (this.Mode)
				{
				case InstallationModes.Install:
					result = Strings.InstallationLicenseAgreementShortDescription;
					break;
				case InstallationModes.BuildToBuildUpgrade:
					result = Strings.UpgradeLicenseAgreementShortDescription;
					break;
				}
				return result;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x0000D400 File Offset: 0x0000B600
		public string ProgressDescription
		{
			get
			{
				string result = string.Empty;
				switch (this.Mode)
				{
				case InstallationModes.Install:
					result = Strings.AddProgressText;
					break;
				case InstallationModes.BuildToBuildUpgrade:
					result = Strings.UpgradeProgressText;
					break;
				case InstallationModes.Uninstall:
					result = Strings.RemoveProgressText;
					break;
				}
				return result;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060003D3 RID: 979 RVA: 0x0000D45A File Offset: 0x0000B65A
		// (set) Token: 0x060003D4 RID: 980 RVA: 0x0000D462 File Offset: 0x0000B662
		public bool SuppressValidateSourceDir { get; set; }

		// Token: 0x060003D5 RID: 981 RVA: 0x0000D46C File Offset: 0x0000B66C
		public override ValidationError[] Validate()
		{
			SetupLogger.TraceEnter(new object[0]);
			List<ValidationError> list = new List<ValidationError>();
			if (this.IsCleanMachine)
			{
				if (!base.SetupContext.HasPrepareADParameters)
				{
					if (base.SetupContext.InstalledRolesLocal.Count == 0 && base.SetupContext.InstalledRolesAD.Count == 0)
					{
						if (this.Mode != InstallationModes.Install && !base.SetupContext.HasRemoveProvisionedServerParameters)
						{
							list.Add(new SetupValidationError(Strings.ModeErrorForFreshInstall));
						}
					}
					else if (base.SetupContext.IsBackupKeyPresent)
					{
						if (this.Mode != InstallationModes.BuildToBuildUpgrade)
						{
							list.Add(new SetupValidationError(Strings.ModeErrorForUpgrade));
						}
					}
					else if (this.Mode != InstallationModes.DisasterRecovery)
					{
						list.Add(new SetupValidationError(Strings.ModeErrorForDisasterRecovery));
					}
				}
			}
			else
			{
				bool flag = true;
				foreach (Role role in base.SetupContext.InstalledRolesLocal)
				{
					if (!base.SetupContext.IsUnpacked(role.RoleName))
					{
						flag = false;
						break;
					}
				}
				if (flag && base.SetupContext.UnpackedRoles.Count > 0 && !base.SetupContext.IsUnpacked("AdminToolsRole"))
				{
					flag = false;
				}
				if (!flag && this.Mode != InstallationModes.DisasterRecovery)
				{
					list.Add(new SetupValidationError(Strings.ModeErrorForDisasterRecovery));
				}
			}
			if (base.SetupContext.ParsedArguments.ContainsKey("domaincontroller") && base.SetupContext.IsInstalledLocal("GatewayRole"))
			{
				list.Add(new SetupValidationError(Strings.ParameterNotValidForCurrentRoles("domaincontroller")));
			}
			if (base.SetupContext.ExchangeOrganizationExists && base.SetupContext.ParsedArguments.ContainsKey("organizationname") && !base.SetupContext.OrganizationName.EscapedName.Equals(base.SetupContext.OrganizationNameFoundInAD.EscapedName, StringComparison.InvariantCultureIgnoreCase))
			{
				list.Add(new SetupValidationError(Strings.OrganizationAlreadyExists(base.SetupContext.OrganizationNameFoundInAD.EscapedName)));
			}
			if (base.SetupContext.RegistryError != null)
			{
				list.Add(new SetupValidationError(base.SetupContext.RegistryError.LocalizedString));
			}
			if (base.SetupContext.OrganizationNameValidationException != null)
			{
				list.Add(new SetupValidationError(base.SetupContext.OrganizationNameValidationException.LocalizedString));
			}
			if (base.SetupContext.DomainController != null)
			{
				list.AddRange(this.ModeDatahandler.CheckForADErrors(false));
			}
			list.AddRange(base.Validate());
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000D71C File Offset: 0x0000B91C
		public override void UpdateWorkUnits()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.DataHandlers.Clear();
			base.DataHandlers.Add(this.ModeDatahandler);
			base.UpdateWorkUnits();
			foreach (WorkUnit workUnit in base.WorkUnits)
			{
				workUnit.CanShowExecutedCommand = false;
			}
			SetupLogger.Log(Strings.RootDataHandlerCount(base.DataHandlers.Count));
			SetupLogger.TraceExit();
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		public override string CompletionDescription
		{
			get
			{
				return this.ModeDatahandler.CompletionDescription;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000D7BD File Offset: 0x0000B9BD
		public override string CompletionStatus
		{
			get
			{
				return this.ModeDatahandler.CompletionStatus;
			}
		}

		// Token: 0x04000134 RID: 308
		private bool hasReadData;

		// Token: 0x04000135 RID: 309
		private Dictionary<string, object> parsedArguments;

		// Token: 0x04000137 RID: 311
		private InstallModeDataHandler installModeDataHandler;

		// Token: 0x04000138 RID: 312
		private UninstallModeDataHandler uninstallModeDataHandler;

		// Token: 0x04000139 RID: 313
		private DisasterRecoveryModeDataHandler disasterRecoveryModeDataHandler;

		// Token: 0x0400013A RID: 314
		private UpgradeModeDataHandler upgradeModeDataHandler;

		// Token: 0x0400013B RID: 315
		private AddUmLanguagePackModeDataHandler addUmLanguagePackModeDataHandler;

		// Token: 0x0400013C RID: 316
		private RemoveUmLanguagePackModeDataHandler removeUmLanguagePackModeDataHandler;
	}
}
