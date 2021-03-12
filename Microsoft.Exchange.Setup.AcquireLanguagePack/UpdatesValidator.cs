using System;
using System.IO;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x0200001B RID: 27
	internal sealed class UpdatesValidator : ValidatorBase
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00003AB9 File Offset: 0x00001CB9
		public UpdatesValidator(string mspsPath, string msiFileName, string bundleFileName, string localXMLVersioningFileName, Action<object> callback)
		{
			this.mspsPath = mspsPath;
			this.msiFileName = msiFileName;
			this.bundleFileName = bundleFileName;
			this.localXMLVersioningFileName = localXMLVersioningFileName;
			base.Callback = callback;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public override bool Validate()
		{
			base.InvokeCallback(Strings.VerifyingUpdates);
			ValidationHelper.ThrowIfNullOrEmpty(this.mspsPath, "this.mspsPath");
			ValidationHelper.ThrowIfNullOrEmpty(this.msiFileName, "this.msiFileName");
			ValidationHelper.ThrowIfNullOrEmpty(this.bundleFileName, "this.bundleFileName");
			bool flag = true;
			if (File.Exists(this.bundleFileName))
			{
				using (LanguagePackValidator languagePackValidator = new LanguagePackValidator(this.bundleFileName, this.localXMLVersioningFileName, base.Callback))
				{
					flag = languagePackValidator.Validate();
					if (flag)
					{
						this.validatedFiles.AddRange(languagePackValidator.ValidatedFiles);
					}
				}
			}
			bool flag2 = true;
			if (Directory.Exists(this.mspsPath))
			{
				string[] files = Directory.GetFiles(this.mspsPath, "*.msp", SearchOption.TopDirectoryOnly);
				if (files != null && files.Length > 0)
				{
					using (MspValidator mspValidator = new MspValidator(files, this.msiFileName, File.Exists(this.bundleFileName) ? this.bundleFileName : null, File.Exists(this.localXMLVersioningFileName) ? this.localXMLVersioningFileName : null, base.Callback))
					{
						flag2 = mspValidator.Validate();
						if (flag2)
						{
							this.validatedFiles.AddRange(mspValidator.ValidatedFiles);
						}
					}
				}
			}
			return flag && flag2;
		}

		// Token: 0x04000045 RID: 69
		private readonly string mspsPath;

		// Token: 0x04000046 RID: 70
		private readonly string msiFileName;

		// Token: 0x04000047 RID: 71
		private readonly string bundleFileName;

		// Token: 0x04000048 RID: 72
		private readonly string localXMLVersioningFileName;
	}
}
