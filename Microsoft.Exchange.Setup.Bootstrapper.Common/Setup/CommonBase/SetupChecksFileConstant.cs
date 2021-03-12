using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Setup.CommonBase
{
	// Token: 0x02000009 RID: 9
	public static class SetupChecksFileConstant
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00004020 File Offset: 0x00002220
		public static IList<string> GetSetupRequiredFiles()
		{
			return SetupChecksFileConstant.SetupRequiredFiles;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x00004027 File Offset: 0x00002227
		public static IList<string> GetExcludedPaths()
		{
			return SetupChecksFileConstant.ExcludedPaths;
		}

		// Token: 0x0400003E RID: 62
		public const string BinaryFileNamePattern = "^(.+)\\.(exe|com|dll)$";

		// Token: 0x0400003F RID: 63
		public const string ExchangeSetupUpdatesPath = "Temp\\ExchangeSetup";

		// Token: 0x04000040 RID: 64
		public const string ExchangeSetupUpdatesMspPath = "Temp\\ExchangeSetup\\MspTemp";

		// Token: 0x04000041 RID: 65
		public const string ExchangeSetupSourcePath = "Setup\\ServerRoles\\Common";

		// Token: 0x04000042 RID: 66
		public const string ExchangeSPSourcePath = "Setup\\ServerRoles\\ClientAccess\\ServicePlans";

		// Token: 0x04000043 RID: 67
		public const string ExchangeMailboxSourcePath = "Setup\\ServerRoles\\Mailbox";

		// Token: 0x04000044 RID: 68
		public const string ServerLanguagePackMSIFileName = "ServerLanguagePack.msi";

		// Token: 0x04000045 RID: 69
		public const string SetupRequiredFilesContainingAssembly = "Microsoft.Exchange.Setup.Bootstrapper.Common.dll";

		// Token: 0x04000046 RID: 70
		public const string SetupExeFileName = "Setup.exe";

		// Token: 0x04000047 RID: 71
		public const string ExSetupExeFileName = "ExSetup.exe";

		// Token: 0x04000048 RID: 72
		public const string ExSetupUIExeFileName = "ExSetupUI.exe";

		// Token: 0x04000049 RID: 73
		public const string SetupUIExeFileName = "SetupUI.exe";

		// Token: 0x0400004A RID: 74
		public const string ExchangeServerMsi = "ExchangeServer.msi";

		// Token: 0x0400004B RID: 75
		public const string BinFolderName = "bin";

		// Token: 0x0400004C RID: 76
		public const int WindowsMajorVersion = 6;

		// Token: 0x0400004D RID: 77
		public const int WindowsMinorVersion = 1;

		// Token: 0x0400004E RID: 78
		public const int WindowsServicePack = 1;

		// Token: 0x0400004F RID: 79
		public const string GetSetupRequiredFilesMethodName = "GetSetupRequiredFiles";

		// Token: 0x04000050 RID: 80
		public const string TempDirName = "Temp";

		// Token: 0x04000051 RID: 81
		public const string DefaultCultureName = "en";

		// Token: 0x04000052 RID: 82
		public const string LanguagePackBundleFileName = "LanguagePackBundle.exe";

		// Token: 0x04000053 RID: 83
		public const string LanguagePackBundlePath = "ExchangeSetupLogs\\ExchangeLanguagePack";

		// Token: 0x04000054 RID: 84
		public const string ResourceBaseName = "Microsoft.Exchange.Setup.Bootstrapper.Common.Strings";

		// Token: 0x04000055 RID: 85
		public const string ResourceFileName = "Microsoft.Exchange.Setup.Bootstrapper.Common.Resources.dll";

		// Token: 0x04000056 RID: 86
		public const int CumulativeUpdateVersion = 23;

		// Token: 0x04000057 RID: 87
		private static readonly List<string> ExcludedPaths = new List<string>
		{
			"\\search\\"
		};

		// Token: 0x04000058 RID: 88
		private static readonly IList<string> SetupRequiredFiles = new List<string>
		{
			"ExSetup.exe",
			"ExSetupUI.exe",
			"Interop.NetFw.dll",
			"LPVersioning.xml",
			"Microsoft.Exchange.CabUtility.dll",
			"Microsoft.Exchange.Common.dll",
			"Microsoft.Exchange.Compliance.dll",
			"Microsoft.Exchange.Configuration.ObjectModel.dll",
			"Microsoft.Exchange.Data.Common.dll",
			"Microsoft.Exchange.Data.Directory.dll",
			"Microsoft.Exchange.Data.Storage.dll",
			"Microsoft.Exchange.Diagnostics.dll",
			"Microsoft.Exchange.Setup.CommonBase.dll",
			"Microsoft.Exchange.HelpProvider.dll",
			"Microsoft.Exchange.Management.dll",
			"Microsoft.Exchange.Management.Deployment.dll",
			"Microsoft.Exchange.Management.RbacDefinition.dll",
			"Microsoft.Exchange.Net.dll",
			"Microsoft.Exchange.Rpc.dll",
			"Microsoft.Exchange.Security.dll",
			"Microsoft.Exchange.Setup.AcquireLanguagePack.dll",
			"Microsoft.Exchange.Setup.Bootstrapper.Common.dll",
			"Microsoft.Exchange.Setup.Common.dll",
			"Microsoft.Exchange.Setup.Parser.dll",
			"Microsoft.Exchange.Setup.SignVerfWrapper.dll",
			"msvcp110.dll",
			"msvcr110.dll",
			"res\\SetupClose.png",
			"res\\SetupError.png",
			"res\\SetupHelp.png",
			"res\\SetupPrint.png",
			"res\\SetupPrint_h.png",
			"res\\SetupWarning.png",
			"res\\ExchangeLogo.png"
		};
	}
}
