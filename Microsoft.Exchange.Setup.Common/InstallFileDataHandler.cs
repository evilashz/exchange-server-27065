using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Configuration.MonadDataProvider;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Setup.Common
{
	// Token: 0x02000032 RID: 50
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class InstallFileDataHandler : FileDataHandler
	{
		// Token: 0x060001E1 RID: 481 RVA: 0x000075FC File Offset: 0x000057FC
		public InstallFileDataHandler(ISetupContext context, MsiConfigurationInfo msiConfig, MonadConnection connection) : base(context, "install-msipackage", msiConfig, connection)
		{
			if (msiConfig is DatacenterMsiConfigurationInfo)
			{
				base.WorkUnit.Text = Strings.CopyDatacenterFileText;
			}
			else
			{
				base.WorkUnit.Text = Strings.CopyFileText;
			}
			this.WatsonEnabled = base.SetupContext.WatsonEnabled;
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000765C File Offset: 0x0000585C
		protected override void AddParameters()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.AddParameters();
			base.Parameters.AddWithValue("targetdirectory", this.TargetDirectory);
			base.Parameters.AddWithValue("features", base.GetFeatures().ToArray());
			base.Parameters.AddWithValue("packagepath", Path.Combine(this.PackagePath, this.MsiConfigurationInfo.Name));
			base.Parameters.AddWithValue("updatesdir", this.UpdatesDir);
			base.Parameters.AddWithValue("PropertyValues", string.Format("DISABLEERRORREPORTING={0} PRODUCTLANGUAGELCID={1} DEFAULTLANGUAGENAME={2} DEFAULTLANGUAGELCID={3} INSTALLCOMMENT=\"{4}{5}\"", new object[]
			{
				this.WatsonEnabled ? 0 : 1,
				base.SetupContext.ExchangeCulture.LCID,
				base.SetupContext.ExchangeCulture.ThreeLetterWindowsLanguageName,
				base.SetupContext.ExchangeCulture.LCID,
				Strings.InstalledLanguageComment,
				base.SetupContext.ExchangeCulture.EnglishName
			}));
			SetupLogger.TraceExit();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000778C File Offset: 0x0000598C
		public override ValidationError[] Validate()
		{
			SetupLogger.TraceEnter(new object[]
			{
				this.PackagePath,
				this.TargetDirectory
			});
			List<ValidationError> list = new List<ValidationError>();
			if (base.SetupContext.ParsedArguments.ContainsKey("sourcedir"))
			{
				if (!Directory.Exists(this.PackagePath))
				{
					list.Add(new SetupValidationError(Strings.MsiDirectoryNotFound(this.PackagePath)));
				}
				else
				{
					string path = Path.Combine(this.PackagePath, this.MsiConfigurationInfo.Name);
					if (!File.Exists(path))
					{
						list.Add(new SetupValidationError(Strings.MsiFileNotFound(this.PackagePath, this.MsiConfigurationInfo.Name)));
					}
				}
				if (this.UpdatesDir != null && !Directory.Exists(this.UpdatesDir.PathName))
				{
					list.Add(new SetupValidationError(Strings.MsiDirectoryNotFound(this.UpdatesDir.PathName)));
				}
			}
			list.AddRange(base.Validate());
			SetupLogger.TraceExit();
			return list.ToArray();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000788F File Offset: 0x00005A8F
		protected override void OnSaveData()
		{
			SetupLogger.TraceEnter(new object[0]);
			base.OnSaveData();
			ConfigurationContext.Setup.ResetInstallPath();
			SetupLogger.TraceExit();
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x060001E5 RID: 485 RVA: 0x000078AC File Offset: 0x00005AAC
		private string PackagePath
		{
			get
			{
				if (!(null == base.SetupContext.SourceDir))
				{
					return base.SetupContext.SourceDir.PathName;
				}
				return "";
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000078D7 File Offset: 0x00005AD7
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x000078DF File Offset: 0x00005ADF
		public string TargetDirectory
		{
			get
			{
				return this.targetDirectory;
			}
			set
			{
				this.targetDirectory = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000078E8 File Offset: 0x00005AE8
		// (set) Token: 0x060001E9 RID: 489 RVA: 0x000078F0 File Offset: 0x00005AF0
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

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x060001EA RID: 490 RVA: 0x000078F9 File Offset: 0x00005AF9
		public LongPath UpdatesDir
		{
			get
			{
				return base.SetupContext.UpdatesDir;
			}
		}

		// Token: 0x04000075 RID: 117
		private bool watsonEnabled;

		// Token: 0x04000076 RID: 118
		private string targetDirectory;
	}
}
