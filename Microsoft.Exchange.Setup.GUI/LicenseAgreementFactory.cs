using System;
using System.IO;
using System.Reflection;
using Microsoft.Exchange.Management.Deployment;
using Microsoft.Exchange.Setup.Common;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x0200000A RID: 10
	internal static class LicenseAgreementFactory
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00006248 File Offset: 0x00004448
		public static string GetLicenseFileFullPathName(InstallationModes mode)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			string result = null;
			text2 = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Eula");
			if (Directory.Exists(text2))
			{
				text = LicenseAgreementFactory.GetLicenseFileName(mode);
				try
				{
					result = LocalizedResources.GetFile(text2, text);
				}
				catch (FileNotFoundException)
				{
					SetupLogger.Log(Strings.SetupNotFoundInSourceDirError(Path.Combine(text2, text)));
				}
			}
			return result;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000062BC File Offset: 0x000044BC
		private static string GetLicenseFileName(InstallationModes mode)
		{
			string result = string.Empty;
			switch (mode)
			{
			case InstallationModes.Install:
				result = "License.rtf";
				break;
			case InstallationModes.BuildToBuildUpgrade:
				result = "UpgradeLicense.rtf";
				break;
			}
			return result;
		}

		// Token: 0x04000043 RID: 67
		private const string LicenseFileName = "License.rtf";

		// Token: 0x04000044 RID: 68
		private const string UpgradeLicenseFileName = "UpgradeLicense.rtf";
	}
}
