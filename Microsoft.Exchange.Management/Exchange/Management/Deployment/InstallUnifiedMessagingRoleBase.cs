using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Win32;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x020001C1 RID: 449
	[ClassAccessLevel(AccessLevel.Consumer)]
	public abstract class InstallUnifiedMessagingRoleBase : ManageUnifiedMessagingRole
	{
		// Token: 0x06000F83 RID: 3971 RVA: 0x000445A4 File Offset: 0x000427A4
		protected override void PopulateContextVariables()
		{
			base.Fields["IsPostInstallUMAddLP"] = false;
			base.PopulateContextVariables();
			if (!base.Fields.IsModified("SourcePath"))
			{
				this.SourcePath = this.GetMsiSourcePath();
			}
			string text = Path.Combine((string)base.Fields["SourcePath"], "en\\");
			base.WriteVerbose(Strings.UmLanguagePackDirectory(text));
			this.PackagePath = Path.Combine(text, "UMLanguagePack.en-US.msi");
			base.WriteVerbose(Strings.UmLanguagePackFullPath(this.PackagePath));
			this.TelePackagePath = Path.Combine(text, "MSSpeech_SR_TELE.en-US.msi");
			base.WriteVerbose(Strings.UmLanguagePackFullPath(this.TelePackagePath));
			this.TransPackagePath = Path.Combine(text, "MSSpeech_SR_TRANS.en-US.msi");
			base.WriteVerbose(Strings.UmLanguagePackFullPath(this.TransPackagePath));
			this.TtsPackagePath = Path.Combine(text, "MSSpeech_TTS.en-US.msi");
			base.WriteVerbose(Strings.UmLanguagePackFullPath(this.TtsPackagePath));
			base.LogFilePath = Path.Combine((string)base.Fields["SetupLoggingPath"], "add-UMLanguagePack.en-US.msilog");
			base.WriteVerbose(Strings.UmLanguagePackLogFile(base.LogFilePath));
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x000446D8 File Offset: 0x000428D8
		private string GetMsiSourcePath()
		{
			string text = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{4934D1EA-BE46-48B1-8847-F1AF20E892C1}", "InstallSource", null);
			if (text == null)
			{
				base.WriteError(new LocalizedException(Strings.ExceptionRegistryKeyNotFound("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{4934D1EA-BE46-48B1-8847-F1AF20E892C1}\\InstallSource")), ErrorCategory.InvalidData, null);
			}
			return text;
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000F85 RID: 3973 RVA: 0x00044716 File Offset: 0x00042916
		// (set) Token: 0x06000F86 RID: 3974 RVA: 0x0004472D File Offset: 0x0004292D
		[Parameter(Mandatory = false)]
		public string SourcePath
		{
			get
			{
				return (string)base.Fields["SourcePath"];
			}
			set
			{
				base.Fields["SourcePath"] = value;
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000F87 RID: 3975 RVA: 0x00044740 File Offset: 0x00042940
		// (set) Token: 0x06000F88 RID: 3976 RVA: 0x00044757 File Offset: 0x00042957
		[Parameter(Mandatory = false)]
		public string PackagePath
		{
			get
			{
				return (string)base.Fields["PackagePath"];
			}
			set
			{
				base.Fields["PackagePath"] = value;
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000F89 RID: 3977 RVA: 0x0004476A File Offset: 0x0004296A
		// (set) Token: 0x06000F8A RID: 3978 RVA: 0x00044781 File Offset: 0x00042981
		[Parameter(Mandatory = false)]
		public string TelePackagePath
		{
			get
			{
				return (string)base.Fields["TelePackagePath"];
			}
			set
			{
				base.Fields["TelePackagePath"] = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000F8B RID: 3979 RVA: 0x00044794 File Offset: 0x00042994
		// (set) Token: 0x06000F8C RID: 3980 RVA: 0x000447AB File Offset: 0x000429AB
		[Parameter(Mandatory = false)]
		public string TransPackagePath
		{
			get
			{
				return (string)base.Fields["TransPackagePath"];
			}
			set
			{
				base.Fields["TransPackagePath"] = value;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000F8D RID: 3981 RVA: 0x000447BE File Offset: 0x000429BE
		// (set) Token: 0x06000F8E RID: 3982 RVA: 0x000447D5 File Offset: 0x000429D5
		[Parameter(Mandatory = false)]
		public string TtsPackagePath
		{
			get
			{
				return (string)base.Fields["TtsPackagePath"];
			}
			set
			{
				base.Fields["TtsPackagePath"] = value;
			}
		}
	}
}
