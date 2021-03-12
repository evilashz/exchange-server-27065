using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000019 RID: 25
	internal sealed class MspValidator : ValidatorBase
	{
		// Token: 0x0600006F RID: 111 RVA: 0x0000381C File Offset: 0x00001A1C
		public MspValidator(string[] msps, string msiFileName, string bundleFileName, string localXMLVersioningFileName, Action<object> callback)
		{
			this.msps = msps;
			this.msiFileName = msiFileName;
			this.bundleFileName = bundleFileName;
			this.localXMLVersioningFileName = localXMLVersioningFileName;
			base.Callback = callback;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x0000384C File Offset: 0x00001A4C
		public override bool Validate()
		{
			base.InvokeCallback(Strings.VerifyingMsps);
			ValidationHelper.ThrowIfNullOrEmpty<string>(this.msps, "this.msps");
			ValidationHelper.ThrowIfNullOrEmpty(this.msiFileName, "this.msiFileName");
			if (!File.Exists(this.msiFileName))
			{
				base.InvokeCallback(Strings.NotExist(this.msiFileName));
				return false;
			}
			string[] array = this.msps;
			int i = 0;
			while (i < array.Length)
			{
				string text = array[i];
				bool result;
				if (!File.Exists(text))
				{
					base.InvokeCallback(Strings.NotExist(text));
					result = false;
				}
				else if (!MspUtility.VerifyMspSignature(text))
				{
					base.InvokeCallback(Strings.MspValidationFailedOn("VerifyMspSignature"));
					result = false;
				}
				else
				{
					if (!MspUtility.IsMspInterimUpdate(text))
					{
						i++;
						continue;
					}
					base.InvokeCallback(Strings.MspValidationFailedOn("IsMspInterimUpdate"));
					result = false;
				}
				return result;
			}
			List<string> applicableMsps = MspUtility.GetApplicableMsps(this.msiFileName, true, this.msps);
			if (applicableMsps == null || applicableMsps.Count != this.msps.Length)
			{
				base.InvokeCallback(Strings.MspValidationFailedOn("GetApplicableMsps"));
				return false;
			}
			if (!string.IsNullOrEmpty(this.localXMLVersioningFileName))
			{
				string location;
				string pathToLangPackBundleXML;
				if (string.IsNullOrEmpty(this.bundleFileName))
				{
					location = Assembly.GetExecutingAssembly().Location;
					pathToLangPackBundleXML = this.localXMLVersioningFileName;
				}
				else
				{
					location = this.bundleFileName;
					Directory.CreateDirectory(ValidatorBase.TempPath);
					pathToLangPackBundleXML = LanguagePackXmlHelper.ExtractXMLFromBundle(this.bundleFileName, ValidatorBase.TempPath);
				}
				if (!MspUtility.IsMspCompatibleWithLanguagPack(applicableMsps[applicableMsps.Count - 1], location, this.localXMLVersioningFileName, pathToLangPackBundleXML))
				{
					base.InvokeCallback(Strings.MspValidationFailedOn("IsMspCompatibleWithLanguagPack"));
					return false;
				}
			}
			this.validatedFiles = applicableMsps;
			return true;
		}

		// Token: 0x04000041 RID: 65
		private readonly string[] msps;

		// Token: 0x04000042 RID: 66
		private readonly string msiFileName;

		// Token: 0x04000043 RID: 67
		private readonly string bundleFileName;

		// Token: 0x04000044 RID: 68
		private readonly string localXMLVersioningFileName;
	}
}
