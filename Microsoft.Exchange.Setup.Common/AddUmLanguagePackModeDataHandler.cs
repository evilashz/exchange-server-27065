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
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AddUmLanguagePackModeDataHandler : UmLanguagePackModeDataHandler
	{
		// Token: 0x0600011D RID: 285 RVA: 0x000056B1 File Offset: 0x000038B1
		public AddUmLanguagePackModeDataHandler(ISetupContext setupContext, MonadConnection connection) : base(setupContext, connection)
		{
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600011E RID: 286 RVA: 0x000056C6 File Offset: 0x000038C6
		public override InstallationModes Mode
		{
			get
			{
				return InstallationModes.Install;
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x000056CC File Offset: 0x000038CC
		protected override ConfigurationDataHandler GetInstallableUnitConfigurationDataHandler(string installableUnitName)
		{
			ConfigurationDataHandler configurationDataHandler = null;
			if (!this.addUmConfigDataHandlers.TryGetValue(installableUnitName, out configurationDataHandler))
			{
				InstallableUnitConfigurationInfo installableUnitConfigurationInfoByName = InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(installableUnitName);
				UmLanguagePackConfigurationInfo umLanguagePackConfigurationInfo = installableUnitConfigurationInfoByName as UmLanguagePackConfigurationInfo;
				if (umLanguagePackConfigurationInfo != null)
				{
					configurationDataHandler = new AddUmLanguagePackCfgDataHandler(base.SetupContext, base.Connection, umLanguagePackConfigurationInfo.Culture, this.PackageDirectory);
					this.addUmConfigDataHandlers.Add(installableUnitName, configurationDataHandler);
				}
			}
			return configurationDataHandler;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00005728 File Offset: 0x00003928
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

		// Token: 0x06000121 RID: 289 RVA: 0x00005750 File Offset: 0x00003950
		public override void UpdatePreCheckTaskDataHandler()
		{
			PrerequisiteAnalysisTaskDataHandler instance = PrerequisiteAnalysisTaskDataHandler.GetInstance(base.SetupContext, base.Connection);
			instance.AddRoleByUnitName("UmLanguagePack");
			instance.TargetDir = base.SetupContext.TargetDir;
			instance.AddSelectedInstallableUnits(this.SelectedInstallableUnits);
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00005798 File Offset: 0x00003998
		public override decimal RequiredDiskSpace
		{
			get
			{
				decimal num = 0m;
				foreach (CultureInfo umlang in base.SelectedCultures)
				{
					num += UmLanguagePackConfigurationInfo.GetUmLanguagePackSizeForCultureInfo(umlang);
				}
				return num;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000057FC File Offset: 0x000039FC
		public override string RoleSelectionDescription
		{
			get
			{
				return Strings.AddUmLanguagePackText;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00005808 File Offset: 0x00003A08
		public override LocalizedString ConfigurationSummary
		{
			get
			{
				return Strings.UmLanguagePacksToAdd;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000580F File Offset: 0x00003A0F
		public LongPath PackageDirectory
		{
			get
			{
				return base.SetupContext.SourceDir;
			}
		}

		// Token: 0x04000040 RID: 64
		private Dictionary<string, ConfigurationDataHandler> addUmConfigDataHandlers = new Dictionary<string, ConfigurationDataHandler>();

		// Token: 0x04000041 RID: 65
		private InstallPreCheckDataHandler preCheckDataHandler;
	}
}
