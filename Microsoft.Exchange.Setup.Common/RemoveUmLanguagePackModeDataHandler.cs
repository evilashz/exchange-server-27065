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
	// Token: 0x02000054 RID: 84
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class RemoveUmLanguagePackModeDataHandler : UmLanguagePackModeDataHandler
	{
		// Token: 0x060003A8 RID: 936 RVA: 0x0000CAF2 File Offset: 0x0000ACF2
		public RemoveUmLanguagePackModeDataHandler(ISetupContext setupContext, MonadConnection connection) : base(setupContext, connection)
		{
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060003A9 RID: 937 RVA: 0x0000CB07 File Offset: 0x0000AD07
		public override InstallationModes Mode
		{
			get
			{
				return InstallationModes.Uninstall;
			}
		}

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060003AA RID: 938 RVA: 0x0000CB0A File Offset: 0x0000AD0A
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

		// Token: 0x060003AB RID: 939 RVA: 0x0000CB34 File Offset: 0x0000AD34
		protected override ConfigurationDataHandler GetInstallableUnitConfigurationDataHandler(string installableUnitName)
		{
			ConfigurationDataHandler configurationDataHandler = null;
			if (!this.removeUmConfigDataHandlers.TryGetValue(installableUnitName, out configurationDataHandler))
			{
				InstallableUnitConfigurationInfo installableUnitConfigurationInfoByName = InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(installableUnitName);
				UmLanguagePackConfigurationInfo umLanguagePackConfigurationInfo = installableUnitConfigurationInfoByName as UmLanguagePackConfigurationInfo;
				if (umLanguagePackConfigurationInfo != null)
				{
					configurationDataHandler = new RemoveUmLanguagePackCfgDataHandler(base.SetupContext, base.Connection, umLanguagePackConfigurationInfo.Culture);
					this.removeUmConfigDataHandlers.Add(installableUnitName, configurationDataHandler);
				}
			}
			return configurationDataHandler;
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060003AC RID: 940 RVA: 0x0000CB8A File Offset: 0x0000AD8A
		public override decimal RequiredDiskSpace
		{
			get
			{
				return 0m;
			}
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000CB94 File Offset: 0x0000AD94
		public override ValidationError[] Validate()
		{
			SetupLogger.TraceEnter(new object[0]);
			List<ValidationError> list = new List<ValidationError>();
			if (base.InstalledUMLanguagePacks.Count == 0)
			{
				list.Add(new SetupValidationError(Strings.NoUmLanguagePackInstalled));
			}
			else
			{
				foreach (CultureInfo cultureInfo in base.SelectedCultures)
				{
					if (cultureInfo.ToString().ToLower() == "en-us")
					{
						list.Add(new SetupValidationError(Strings.CannotRemoveEnglishUSLanguagePack));
					}
					else
					{
						bool flag = false;
						foreach (CultureInfo cultureInfo2 in base.InstalledUMLanguagePacks)
						{
							if (cultureInfo2.Name.ToLower() == cultureInfo.ToString().ToLower())
							{
								flag = true;
								break;
							}
						}
						if (!flag)
						{
							list.Add(new SetupValidationError(Strings.UmLanguagePackNotInstalledForCulture(cultureInfo.ToString())));
						}
					}
				}
			}
			list.AddRange(base.Validate());
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060003AE RID: 942 RVA: 0x0000CCD4 File Offset: 0x0000AED4
		public override string RoleSelectionDescription
		{
			get
			{
				return Strings.RemoveUmLanguagePackText;
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060003AF RID: 943 RVA: 0x0000CCE0 File Offset: 0x0000AEE0
		public override LocalizedString ConfigurationSummary
		{
			get
			{
				return Strings.UmLanguagePacksToRemove;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000CCE7 File Offset: 0x0000AEE7
		public override string CompletionDescription
		{
			get
			{
				return base.WorkUnits.HasFailures ? Strings.RemoveUmLanguagePackFailText : Strings.RemoveUmLanguagePackSuccessText;
			}
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060003B1 RID: 945 RVA: 0x0000CD07 File Offset: 0x0000AF07
		public override string CompletionStatus
		{
			get
			{
				return base.WorkUnits.HasFailures ? Strings.RemoveUmLanguagePackFailStatus : Strings.RemoveSuccessStatus;
			}
		}

		// Token: 0x04000114 RID: 276
		private Dictionary<string, ConfigurationDataHandler> removeUmConfigDataHandlers = new Dictionary<string, ConfigurationDataHandler>();

		// Token: 0x04000115 RID: 277
		private UninstallPreCheckDataHandler preCheckDataHandler;
	}
}
