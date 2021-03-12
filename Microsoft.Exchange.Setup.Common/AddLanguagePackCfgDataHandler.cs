using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AddLanguagePackCfgDataHandler : ConfigurationDataHandler
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00003A84 File Offset: 0x00001C84
		public AddLanguagePackCfgDataHandler(ISetupContext context, MonadConnection connection) : base(context, "", "install-Languages", connection)
		{
			base.InstallableUnitName = "LanguagePacks";
			string path = Path.Combine(ConfigurationContext.Setup.SetupLoggingPath, "install-Languages");
			LocalLongFullPath.TryParse(path, out this.logFilePath);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003ACC File Offset: 0x00001CCC
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			SetupLogger.Log(Strings.LanguagePackPackagePath((base.SetupContext.LanguagePackPath == null) ? "" : base.SetupContext.LanguagePackPath.PathName));
			base.Parameters.AddWithValue("LangPackPath", base.SetupContext.LanguagePackPath);
			SetupLogger.Log(Strings.AddLanguagePacksLogFilePath((this.LogFilePath == null) ? "" : this.LogFilePath.PathName));
			base.Parameters.AddWithValue("LogFilePath", this.LogFilePath);
			base.Parameters.AddWithValue("InstallPath", this.InstallPath);
			base.Parameters.AddWithValue("InstallMode", base.SetupContext.InstallationMode);
			base.Parameters.AddWithValue("SourceIsBundle", base.SetupContext.LanguagePackSourceIsBundle);
			string[] array = new string[base.SetupContext.LanguagePacksToInstall.Keys.Count];
			string[] array2 = new string[base.SetupContext.LanguagePacksToInstall.Keys.Count];
			string[] array3 = new string[base.SetupContext.LanguagePacksToInstall.Keys.Count];
			int num = 0;
			foreach (KeyValuePair<string, Array> keyValuePair in base.SetupContext.LanguagePacksToInstall)
			{
				array[num] = keyValuePair.Key.ToString();
				array2[num] = keyValuePair.Value.GetValue(0).ToString();
				array3[num] = keyValuePair.Value.GetValue(1).ToString();
				num++;
			}
			base.Parameters.AddWithValue("LanguagePacksToInstall", array);
			base.Parameters.AddWithValue("LPclientFlags", array2);
			base.Parameters.AddWithValue("LPserverFlags", array3);
			base.Parameters.AddWithValue("InstallVersion", base.SetupContext.RunningVersion.ToString());
			string value = base.SetupContext.RunningVersion.ToString();
			if (base.SetupContext.LanguagePacksToInstall.Keys.Count > 0)
			{
				value = base.SetupContext.SourceLanguagePacks[array[0]].ClientVersion;
			}
			base.Parameters.AddWithValue("ClientLPVersion", value);
			SetupLogger.TraceExit();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003D60 File Offset: 0x00001F60
		public override ValidationError[] ValidateConfiguration()
		{
			SetupLogger.TraceEnter(new object[0]);
			List<ValidationError> list = new List<ValidationError>(base.ValidateConfiguration());
			if (base.SetupContext.LanguagePackPath == null)
			{
				list.Add(new SetupValidationError(Strings.LanguagePacksPackagePathNotSpecified));
			}
			else if (this.LogFilePath == null)
			{
				list.Add(new SetupValidationError(Strings.LanguagePacksLogFilePathNotSpecified));
			}
			else if (!File.Exists(base.SetupContext.LanguagePackPath.PathName) && !Directory.Exists(base.SetupContext.LanguagePackPath.PathName))
			{
				list.Add(new SetupValidationError(Strings.LanguagePacksPackagePathNotFound(base.SetupContext.LanguagePackPath.PathName)));
			}
			if (this.InstallPath == null)
			{
				list.Add(new SetupValidationError(Strings.InstallationPathNotSet));
			}
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000086 RID: 134 RVA: 0x00003E40 File Offset: 0x00002040
		public LocalLongFullPath LogFilePath
		{
			get
			{
				return this.logFilePath;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003E48 File Offset: 0x00002048
		public NonRootLocalLongFullPath InstallPath
		{
			get
			{
				return base.SetupContext.TargetDir;
			}
		}

		// Token: 0x04000020 RID: 32
		private const string commandText = "install-Languages";

		// Token: 0x04000021 RID: 33
		private LocalLongFullPath logFilePath;
	}
}
