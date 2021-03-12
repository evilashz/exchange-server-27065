using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.CabUtility;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200016F RID: 367
	[Cmdlet("add", "umlanguagepack", SupportsShouldProcess = true)]
	[ClassAccessLevel(AccessLevel.Consumer)]
	public sealed class AddUmLanguagePack : ManageUmLanguagePack
	{
		// Token: 0x06000DB0 RID: 3504 RVA: 0x0003F740 File Offset: 0x0003D940
		protected override void PopulateContextVariables()
		{
			base.Fields["IsPostInstallUMAddLP"] = true;
			base.PopulateContextVariables();
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0003F75E File Offset: 0x0003D95E
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x0003F766 File Offset: 0x0003D966
		[Parameter(Mandatory = true)]
		public LongPath LanguagePackExecutablePath { get; set; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0003F76F File Offset: 0x0003D96F
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x0003F786 File Offset: 0x0003D986
		private LongPath PackagePath
		{
			get
			{
				return (LongPath)base.Fields["PackagePath"];
			}
			set
			{
				base.Fields["PackagePath"] = value;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0003F799 File Offset: 0x0003D999
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x0003F7B0 File Offset: 0x0003D9B0
		[Parameter(Mandatory = true)]
		public NonRootLocalLongFullPath InstallPath
		{
			get
			{
				return (NonRootLocalLongFullPath)base.Fields["InstallPath"];
			}
			set
			{
				base.Fields["InstallPath"] = value;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0003F7C3 File Offset: 0x0003D9C3
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x0003F7DA File Offset: 0x0003D9DA
		private LongPath TelePackagePath
		{
			get
			{
				return (LongPath)base.Fields["TelePackagePath"];
			}
			set
			{
				base.Fields["TelePackagePath"] = value;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0003F7ED File Offset: 0x0003D9ED
		// (set) Token: 0x06000DBA RID: 3514 RVA: 0x0003F804 File Offset: 0x0003DA04
		private LongPath TransPackagePath
		{
			get
			{
				return (LongPath)base.Fields["TransPackagePath"];
			}
			set
			{
				base.Fields["TransPackagePath"] = value;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0003F817 File Offset: 0x0003DA17
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x0003F82E File Offset: 0x0003DA2E
		private LongPath TtsPackagePath
		{
			get
			{
				return (LongPath)base.Fields["TtsPackagePath"];
			}
			set
			{
				base.Fields["TtsPackagePath"] = value;
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0003F841 File Offset: 0x0003DA41
		public AddUmLanguagePack()
		{
			base.Fields["InstallationMode"] = InstallationModes.Install;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x0003F85F File Offset: 0x0003DA5F
		protected override void InternalValidate()
		{
			if (!UMLanguagePackHelper.IsUmLanguagePack(this.LanguagePackExecutablePath.PathName))
			{
				base.WriteError(new TaskException(Strings.ADDUMInvalidLanguagePack(this.LanguagePackExecutablePath.PathName)), ErrorCategory.InvalidArgument, 0);
			}
			base.InternalValidate();
		}

		// Token: 0x06000DBF RID: 3519 RVA: 0x0003F89C File Offset: 0x0003DA9C
		protected override void InternalProcessRecord()
		{
			LongPath packagePath = null;
			LongPath telePackagePath = null;
			LongPath transPackagePath = null;
			LongPath ttsPackagePath = null;
			try
			{
				DirectoryInfo directoryInfo = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()));
				this.extractionDirectory = directoryInfo.FullName;
				EmbeddedCabWrapper.ExtractFiles(this.LanguagePackExecutablePath.PathName, this.extractionDirectory, null);
				string[] directories = Directory.GetDirectories(this.extractionDirectory);
				if (directories.Length != 1)
				{
					base.WriteError(new TaskException(Strings.UmLanguagePackInvalidExtraction), ErrorCategory.NotSpecified, 0);
				}
				string extractionPathWithLanguageFolder = directories[0];
				this.SetPackagePath(extractionPathWithLanguageFolder, "UMLanguagePack", ref packagePath);
				this.SetPackagePath(extractionPathWithLanguageFolder, "MSSpeech_SR_TELE", ref telePackagePath);
				this.SetPackagePath(extractionPathWithLanguageFolder, "MSSpeech_SR_TRANS", ref transPackagePath);
				this.SetPackagePath(extractionPathWithLanguageFolder, "MSSpeech_TTS", ref ttsPackagePath);
			}
			catch (Exception ex)
			{
				base.WriteError(new TaskException(Strings.UmLanguagePackException(ex.Message)), ErrorCategory.NotSpecified, 0);
			}
			this.PackagePath = packagePath;
			this.TelePackagePath = telePackagePath;
			this.TransPackagePath = transPackagePath;
			this.TtsPackagePath = ttsPackagePath;
			if (!File.Exists(this.PackagePath.PathName))
			{
				base.WriteError(new TaskException(Strings.UmLanguagePackMsiFileNotFound(this.PackagePath.PathName)), ErrorCategory.InvalidArgument, 0);
			}
			if (!File.Exists(this.TelePackagePath.PathName))
			{
				base.WriteError(new TaskException(Strings.UmLanguagePackMsiFileNotFound(this.TelePackagePath.PathName)), ErrorCategory.InvalidArgument, 0);
			}
			if (!File.Exists(this.TransPackagePath.PathName))
			{
				this.TransPackagePath = null;
			}
			if (!File.Exists(this.TtsPackagePath.PathName))
			{
				base.WriteError(new TaskException(Strings.UmLanguagePackMsiFileNotFound(this.TtsPackagePath.PathName)), ErrorCategory.InvalidArgument, 0);
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x0003FA74 File Offset: 0x0003DC74
		protected override void InternalEndProcessing()
		{
			if (!string.IsNullOrEmpty(this.extractionDirectory) && Directory.Exists(this.extractionDirectory))
			{
				try
				{
					Directory.Delete(this.extractionDirectory, true);
				}
				catch (Exception ex)
				{
					this.WriteWarning(Strings.UmLanguagePackTempFilesNotDeleted(ex.Message));
				}
			}
			base.InternalEndProcessing();
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0003FAD4 File Offset: 0x0003DCD4
		private void SetPackagePath(string extractionPathWithLanguageFolder, string packagePrefix, ref LongPath packagePath)
		{
			string path = string.Format("{0}.{1}.msi", packagePrefix, base.Language.ToString());
			string path2 = Path.Combine(extractionPathWithLanguageFolder, path);
			if (!LongPath.TryParse(path2, out packagePath))
			{
				packagePath = null;
				base.WriteError(new TaskException(Strings.UmLanguagePackPackagePathNotSpecified), ErrorCategory.NotSpecified, 0);
			}
		}

		// Token: 0x0400069D RID: 1693
		private string extractionDirectory;
	}
}
