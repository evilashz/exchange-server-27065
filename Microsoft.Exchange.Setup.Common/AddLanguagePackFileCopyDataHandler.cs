using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AddLanguagePackFileCopyDataHandler : SetupSingleTaskDataHandler
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003E55 File Offset: 0x00002055
		public AddLanguagePackFileCopyDataHandler(ISetupContext context, MonadConnection connection) : base(context, "install-LanguageFiles", connection)
		{
			LocalLongFullPath.TryParse(ConfigurationContext.Setup.SetupLoggingPath, out this.logFilePath);
			base.WorkUnit.Text = Strings.CopyLanguagePacksDisplayName;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003E8C File Offset: 0x0000208C
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			SetupLogger.Log(Strings.LanguagePackPackagePath((base.SetupContext.LanguagePackPath == null) ? "" : base.SetupContext.LanguagePackPath.PathName));
			base.Parameters.AddWithValue("LangPackPath", base.SetupContext.LanguagePackPath);
			SetupLogger.Log(Strings.AddLanguagePacksLogFilePath((this.LogFilePath == null) ? "" : this.LogFilePath.PathName));
			base.Parameters.AddWithValue("LogFilePath", this.LogFilePath);
			base.Parameters.AddWithValue("InstallPath", this.TargetDir);
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
			SetupLogger.TraceExit();
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00004084 File Offset: 0x00002284
		public virtual ValidationError[] ValidateConfiguration()
		{
			SetupLogger.TraceEnter(new object[0]);
			List<ValidationError> list = new List<ValidationError>();
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
			if (this.TargetDir == null)
			{
				list.Add(new SetupValidationError(Strings.InstallationPathNotSet));
			}
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000415E File Offset: 0x0000235E
		public LocalLongFullPath LogFilePath
		{
			get
			{
				return this.logFilePath;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00004166 File Offset: 0x00002366
		public NonRootLocalLongFullPath TargetDir
		{
			get
			{
				return base.SetupContext.TargetDir;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004173 File Offset: 0x00002373
		// (set) Token: 0x0600008E RID: 142 RVA: 0x0000417B File Offset: 0x0000237B
		public List<string> SelectedInstallableUnits
		{
			get
			{
				return this.selectedInstallableUnits;
			}
			set
			{
				this.selectedInstallableUnits = value;
			}
		}

		// Token: 0x04000022 RID: 34
		private const string commandText = "install-LanguageFiles";

		// Token: 0x04000023 RID: 35
		private LocalLongFullPath logFilePath;

		// Token: 0x04000024 RID: 36
		private List<string> selectedInstallableUnits;
	}
}
