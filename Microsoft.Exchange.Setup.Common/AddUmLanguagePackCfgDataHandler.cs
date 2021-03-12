using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AddUmLanguagePackCfgDataHandler : ConfigurationDataHandler
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00004184 File Offset: 0x00002384
		public AddUmLanguagePackCfgDataHandler(ISetupContext context, MonadConnection connection, CultureInfo umlang, LongPath directoryContainingLanguagePack) : base(context, "", "add-umlanguagepack", connection)
		{
			this.Culture = umlang;
			string umLanguagePackNameForCultureInfo = UmLanguagePackConfigurationInfo.GetUmLanguagePackNameForCultureInfo(this.Culture);
			InstallableUnitConfigurationInfo installableUnitConfigurationInfo = InstallableUnitConfigurationInfoManager.GetInstallableUnitConfigurationInfoByName(umLanguagePackNameForCultureInfo);
			if (installableUnitConfigurationInfo == null)
			{
				installableUnitConfigurationInfo = new UmLanguagePackConfigurationInfo(this.Culture);
				InstallableUnitConfigurationInfoManager.AddInstallableUnit(umLanguagePackNameForCultureInfo, installableUnitConfigurationInfo);
			}
			base.InstallableUnitName = installableUnitConfigurationInfo.Name;
			this.logFilePath = UMLanguagePackHelper.GetAddUMLanguageLogPath(ConfigurationContext.Setup.SetupLoggingPath, this.Culture);
			this.LanguagePackExecutablePath = UMLanguagePackHelper.GetUMLanguagePackFilename(directoryContainingLanguagePack.PathName, this.Culture);
			if (this.LanguagePackExecutablePath != null)
			{
				SetupLogger.Log(Strings.PackagePathSetTo(this.LanguagePackExecutablePath.PathName));
			}
			this.shouldRestartUMService = !base.SetupContext.IsDatacenter;
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00004244 File Offset: 0x00002444
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			SetupLogger.Log(Strings.UmLanguagePackPackagePath((this.LanguagePackExecutablePath == null) ? "" : this.LanguagePackExecutablePath.PathName));
			base.Parameters.AddWithValue("LanguagePackExecutablePath", this.LanguagePackExecutablePath);
			SetupLogger.Log(Strings.AddUmLanguagePackLogFilePath((this.LogFilePath == null) ? "" : this.LogFilePath.PathName));
			base.Parameters.AddWithValue("logfilepath", this.LogFilePath);
			base.Parameters.AddWithValue("propertyvalues", string.Format("INSTALLDIR='{0}' ESE=1", this.LanguagePackExecutablePath));
			base.Parameters.AddWithValue("Language", this.Culture);
			base.Parameters.AddWithValue("InstallPath", this.InstallPath);
			base.Parameters.AddWithValue("updatesdir", this.UpdatesDir);
			base.Parameters.AddWithValue("ShouldRestartUMService", this.shouldRestartUMService);
			SetupLogger.TraceExit();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000436C File Offset: 0x0000256C
		public override ValidationError[] ValidateConfiguration()
		{
			SetupLogger.TraceEnter(new object[0]);
			List<ValidationError> list = new List<ValidationError>(base.ValidateConfiguration());
			if (this.LanguagePackExecutablePath == null)
			{
				list.Add(new SetupValidationError(Strings.UmLanguagePackPackagePathNotSpecified));
			}
			else if (this.LogFilePath == null)
			{
				list.Add(new SetupValidationError(Strings.UmLanguagePathLogFilePathNotSpecified));
			}
			else if (!File.Exists(this.LanguagePackExecutablePath.PathName))
			{
				list.Add(new SetupValidationError(Strings.UmLanguagePackFileNotFound(this.LanguagePackExecutablePath.PathName)));
			}
			if (this.InstallPath == null)
			{
				list.Add(new SetupValidationError(Strings.InstallationPathNotSet));
			}
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00004426 File Offset: 0x00002626
		public override void UpdatePreCheckTaskDataHandler()
		{
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00004428 File Offset: 0x00002628
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00004430 File Offset: 0x00002630
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

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004439 File Offset: 0x00002639
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00004441 File Offset: 0x00002641
		private LongPath LanguagePackExecutablePath
		{
			get
			{
				return this.languagePackExecutablePath;
			}
			set
			{
				this.languagePackExecutablePath = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000097 RID: 151 RVA: 0x0000444A File Offset: 0x0000264A
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00004457 File Offset: 0x00002657
		private LongPath UpdatesDir
		{
			get
			{
				return base.SetupContext.UpdatesDir;
			}
			set
			{
				base.SetupContext.UpdatesDir = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00004465 File Offset: 0x00002665
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000446D File Offset: 0x0000266D
		public CultureInfo Culture
		{
			get
			{
				return this.umlang;
			}
			private set
			{
				this.umlang = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004476 File Offset: 0x00002676
		public LocalLongFullPath LogFilePath
		{
			get
			{
				return this.logFilePath;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000447E File Offset: 0x0000267E
		public NonRootLocalLongFullPath InstallPath
		{
			get
			{
				return base.SetupContext.TargetDir;
			}
		}

		// Token: 0x04000025 RID: 37
		private bool watsonEnabled;

		// Token: 0x04000026 RID: 38
		private CultureInfo umlang;

		// Token: 0x04000027 RID: 39
		private LocalLongFullPath logFilePath;

		// Token: 0x04000028 RID: 40
		private LongPath languagePackExecutablePath;

		// Token: 0x04000029 RID: 41
		private readonly bool shouldRestartUMService;
	}
}
