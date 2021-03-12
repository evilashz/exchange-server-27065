using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.CabUtility;
using Microsoft.Exchange.Setup.SignatureVerification;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000014 RID: 20
	public static class MspUtility
	{
		// Token: 0x06000054 RID: 84 RVA: 0x000030D4 File Offset: 0x000012D4
		public static int UnpackMspCabs(string mspFilePath, string toPath)
		{
			if (!MsiHelper.IsMspFileExtension(mspFilePath))
			{
				throw new MsiException(Strings.WrongFileType("mspFilePath"));
			}
			List<string> list = new List<string>();
			using (MsiDatabase msiDatabase = new MsiDatabase(mspFilePath))
			{
				try
				{
					list = msiDatabase.ExtractCabs(toPath);
				}
				catch (MsiException)
				{
					return 0;
				}
			}
			foreach (string path in list)
			{
				EmbeddedCabWrapper.ExtractCabFiles(Path.Combine(toPath, path), toPath, string.Empty, false);
			}
			return list.Count;
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00003198 File Offset: 0x00001398
		public static bool IsMspInterimUpdate(string mspFilePath)
		{
			if (!MsiHelper.IsMspFileExtension(mspFilePath))
			{
				throw new MsiException(Strings.WrongFileType("mspFilePath"));
			}
			bool result;
			try
			{
				using (MsiDatabase msiDatabase = new MsiDatabase(mspFilePath))
				{
					string text = msiDatabase.QueryProperty("MsiPatchMetadata", "DisplayName");
					result = text.StartsWith("Interim Update", StringComparison.CurrentCultureIgnoreCase);
				}
			}
			catch (MsiException)
			{
				result = false;
			}
			return result;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003218 File Offset: 0x00001418
		public static List<string> GetApplicableMsps(string msiFilePath, bool sort, params string[] mspFiles)
		{
			List<string> result;
			try
			{
				result = MsiHelper.DetermineApplicableMsps(msiFilePath, sort, mspFiles);
			}
			catch (MsiException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003248 File Offset: 0x00001448
		public static bool VerifyMspSignature(string mspFilePath)
		{
			if (!MsiHelper.IsMspFileExtension(mspFilePath))
			{
				throw new MsiException(Strings.WrongFileType("mspFilePath"));
			}
			try
			{
				string location = Assembly.GetExecutingAssembly().Location;
				SignVerfWrapper signVerfWrapper = new SignVerfWrapper();
				bool flag = signVerfWrapper.VerifyEmbeddedSignature(location, false);
				if (flag)
				{
					return signVerfWrapper.IsFileMicrosoftTrusted(mspFilePath, true);
				}
			}
			catch (SignatureVerificationException)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000032B4 File Offset: 0x000014B4
		public static bool IsMspCompatibleWithLanguagPack(string mspFilePath, string lpFilePath, string pathToLocalXML, string pathToLangPackBundleXML)
		{
			if (!MsiHelper.IsMspFileExtension(mspFilePath))
			{
				throw new MsiException(Strings.WrongFileType("mspFilePath"));
			}
			ValidationHelper.ThrowIfFileNotExist(lpFilePath, "lpFilePath");
			ValidationHelper.ThrowIfFileNotExist(pathToLocalXML, "pathToLocalXML");
			ValidationHelper.ThrowIfFileNotExist(pathToLangPackBundleXML, "pathToLangPackBundleXML");
			Version mspVersion = MspUtility.GetMspVersion(mspFilePath);
			Version lpVersion = new Version(FileVersionInfo.GetVersionInfo(lpFilePath).FileVersion);
			LanguagePackVersion languagePackVersion = new LanguagePackVersion(pathToLocalXML, pathToLangPackBundleXML);
			return languagePackVersion.IsExchangeInApplicableRange(lpVersion, mspVersion);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003328 File Offset: 0x00001528
		public static Version GetMspVersion(string mspFilePath)
		{
			if (!MsiHelper.IsMspFileExtension(mspFilePath))
			{
				throw new MsiException(Strings.WrongFileType("mspFilePath"));
			}
			Version result;
			using (MsiDatabase msiDatabase = new MsiDatabase(mspFilePath))
			{
				try
				{
					string text = msiDatabase.QueryProperty("MsiPatchMetadata", "DisplayName");
					int num = text.LastIndexOf(" ");
					if (num == -1)
					{
						result = null;
					}
					else
					{
						string version = text.Substring(num).Trim();
						result = new Version(version);
					}
				}
				catch (MsiException)
				{
					result = null;
				}
				catch (ArgumentException)
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x04000034 RID: 52
		private const string MsiPatchMetadataTable = "MsiPatchMetadata";

		// Token: 0x04000035 RID: 53
		private const string DisplayNameProperty = "DisplayName";

		// Token: 0x04000036 RID: 54
		private const string InterimUpdate = "Interim Update";

		// Token: 0x04000037 RID: 55
		private const string SpaceSeparator = " ";
	}
}
