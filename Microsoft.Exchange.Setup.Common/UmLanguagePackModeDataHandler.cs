using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Deployment;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200000C RID: 12
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class UmLanguagePackModeDataHandler : ModeDataHandler
	{
		// Token: 0x0600010F RID: 271 RVA: 0x000054C4 File Offset: 0x000036C4
		public UmLanguagePackModeDataHandler(ISetupContext setupContext, MonadConnection connection) : base(setupContext, connection)
		{
			this.SelectedCultures = new List<CultureInfo>();
			if (base.SetupContext.SelectedCultures != null)
			{
				this.SelectedCultures.AddRange(setupContext.SelectedCultures);
			}
			InstallableUnitConfigurationInfoManager.InitializeUmLanguagePacksConfigurationInfo(this.SelectedCultures.ToArray());
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00005512 File Offset: 0x00003712
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000551A File Offset: 0x0000371A
		public List<CultureInfo> SelectedCultures
		{
			get
			{
				return this.selectedCultures;
			}
			set
			{
				this.selectedCultures = value;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00005523 File Offset: 0x00003723
		public List<CultureInfo> InstalledUMLanguagePacks
		{
			get
			{
				return base.SetupContext.InstalledUMLanguagePacks;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00005530 File Offset: 0x00003730
		public override List<string> SelectedInstallableUnits
		{
			get
			{
				base.SelectedInstallableUnits.Clear();
				foreach (CultureInfo umlang in this.SelectedCultures)
				{
					base.SelectedInstallableUnits.Add(UmLanguagePackConfigurationInfo.GetUmLanguagePackNameForCultureInfo(umlang));
				}
				return base.SelectedInstallableUnits;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000114 RID: 276 RVA: 0x000055A0 File Offset: 0x000037A0
		protected override bool NeedPrePostSetupDataHandlers
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000055A3 File Offset: 0x000037A3
		protected override bool NeedFileDataHandler()
		{
			return false;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x000055A8 File Offset: 0x000037A8
		public override ValidationError[] Validate()
		{
			SetupLogger.TraceEnter(new object[0]);
			List<ValidationError> list = new List<ValidationError>();
			if (!RoleManager.GetRoleByName("UnifiedMessagingRole").IsInstalled)
			{
				list.Add(new SetupValidationError(Strings.UnifiedMessagingRoleIsRequiredForLanguagePackInstalls));
			}
			if (!base.IgnoreValidatingRoleChanges && (this.SelectedCultures == null || this.SelectedCultures.Count == 0))
			{
				list.Add(new SetupValidationError(Strings.NoUmLanguagePackSpecified));
			}
			list.AddRange(base.Validate());
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000562B File Offset: 0x0000382B
		public override bool CanChangeSourceDir
		{
			get
			{
				return !base.SetupContext.IsCleanMachine;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000563B File Offset: 0x0000383B
		public override string RoleSelectionDescription
		{
			get
			{
				return Strings.AddServerRoleText;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00005647 File Offset: 0x00003847
		public override LocalizedString ConfigurationSummary
		{
			get
			{
				return Strings.AddRolesToInstall;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000564E File Offset: 0x0000384E
		public override string CompletionDescription
		{
			get
			{
				return base.WorkUnits.HasFailures ? Strings.AddFailText : Strings.AddSuccessText;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000566E File Offset: 0x0000386E
		public override string CompletionStatus
		{
			get
			{
				return base.WorkUnits.HasFailures ? Strings.AddFailStatus : Strings.AddSuccessStatus;
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000568E File Offset: 0x0000388E
		protected override void UpdateDataHandlers()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.DataHandlers.Clear();
			this.AddConfigurationDataHandlers();
			SetupLogger.TraceExit();
		}

		// Token: 0x0400003F RID: 63
		private List<CultureInfo> selectedCultures;
	}
}
