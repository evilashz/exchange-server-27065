using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.ExSetup
{
	// Token: 0x02000006 RID: 6
	internal static class Strings
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00003034 File Offset: 0x00001234
		static Strings()
		{
			Strings.stringIDs.Add(1413825337U, "ProgressSummary");
			Strings.stringIDs.Add(292337732U, "HelpUrlHeaderText");
			Strings.stringIDs.Add(2924129942U, "RestartSetup");
			Strings.stringIDs.Add(2127358907U, "ExecutionCompleted");
			Strings.stringIDs.Add(3235827753U, "InstallETNotAllowed");
			Strings.stringIDs.Add(128584875U, "BuildToBuildUpgradeFailure");
			Strings.stringIDs.Add(4292992724U, "MoreInformationText");
			Strings.stringIDs.Add(2883940283U, "AdditionalErrorDetails");
			Strings.stringIDs.Add(1435679081U, "ExecutionFailed");
			Strings.stringIDs.Add(2448875626U, "FileCopyComplete");
			Strings.stringIDs.Add(3972136246U, "FileCopyText");
			Strings.stringIDs.Add(2380070307U, "InvalidOSVersion");
			Strings.stringIDs.Add(3981406291U, "NoUpdates");
			Strings.stringIDs.Add(3926021942U, "EarlierVersionsExist");
			Strings.stringIDs.Add(1648760670U, "InsufficientDiskSpace");
			Strings.stringIDs.Add(3147702355U, "InvalidCommandLineArgs");
			Strings.stringIDs.Add(1509727053U, "UpdatesNotOnFreshInstall");
			Strings.stringIDs.Add(4290716783U, "InvalidUpdates");
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000031D8 File Offset: 0x000013D8
		public static LocalizedString ProgressSummary
		{
			get
			{
				return new LocalizedString("ProgressSummary", "ExCDC5B0", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000031F6 File Offset: 0x000013F6
		public static LocalizedString HelpUrlHeaderText
		{
			get
			{
				return new LocalizedString("HelpUrlHeaderText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00003214 File Offset: 0x00001414
		public static LocalizedString RestartSetup
		{
			get
			{
				return new LocalizedString("RestartSetup", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00003232 File Offset: 0x00001432
		public static LocalizedString ExecutionCompleted
		{
			get
			{
				return new LocalizedString("ExecutionCompleted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003250 File Offset: 0x00001450
		public static LocalizedString InstallETNotAllowed
		{
			get
			{
				return new LocalizedString("InstallETNotAllowed", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000326E File Offset: 0x0000146E
		public static LocalizedString BuildToBuildUpgradeFailure
		{
			get
			{
				return new LocalizedString("BuildToBuildUpgradeFailure", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000328C File Offset: 0x0000148C
		public static LocalizedString UnhandledErrorMessage(string message)
		{
			return new LocalizedString("UnhandledErrorMessage", "", false, false, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000038 RID: 56 RVA: 0x000032BB File Offset: 0x000014BB
		public static LocalizedString MoreInformationText
		{
			get
			{
				return new LocalizedString("MoreInformationText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000032D9 File Offset: 0x000014D9
		public static LocalizedString AdditionalErrorDetails
		{
			get
			{
				return new LocalizedString("AdditionalErrorDetails", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003A RID: 58 RVA: 0x000032F7 File Offset: 0x000014F7
		public static LocalizedString ExecutionFailed
		{
			get
			{
				return new LocalizedString("ExecutionFailed", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003318 File Offset: 0x00001518
		public static LocalizedString FileCopyException(string message)
		{
			return new LocalizedString("FileCopyException", "", false, false, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00003347 File Offset: 0x00001547
		public static LocalizedString FileCopyComplete
		{
			get
			{
				return new LocalizedString("FileCopyComplete", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00003365 File Offset: 0x00001565
		public static LocalizedString FileCopyText
		{
			get
			{
				return new LocalizedString("FileCopyText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00003383 File Offset: 0x00001583
		public static LocalizedString InvalidOSVersion
		{
			get
			{
				return new LocalizedString("InvalidOSVersion", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000033A4 File Offset: 0x000015A4
		public static LocalizedString FileCopyFailed(string srcDir, string dstDir)
		{
			return new LocalizedString("FileCopyFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				srcDir,
				dstDir
			});
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000033D7 File Offset: 0x000015D7
		public static LocalizedString NoUpdates
		{
			get
			{
				return new LocalizedString("NoUpdates", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000033F5 File Offset: 0x000015F5
		public static LocalizedString EarlierVersionsExist
		{
			get
			{
				return new LocalizedString("EarlierVersionsExist", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00003413 File Offset: 0x00001613
		public static LocalizedString InsufficientDiskSpace
		{
			get
			{
				return new LocalizedString("InsufficientDiskSpace", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00003431 File Offset: 0x00001631
		public static LocalizedString InvalidCommandLineArgs
		{
			get
			{
				return new LocalizedString("InvalidCommandLineArgs", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000044 RID: 68 RVA: 0x0000344F File Offset: 0x0000164F
		public static LocalizedString UpdatesNotOnFreshInstall
		{
			get
			{
				return new LocalizedString("UpdatesNotOnFreshInstall", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003470 File Offset: 0x00001670
		public static LocalizedString SetupChecksFailed(string message)
		{
			return new LocalizedString("SetupChecksFailed", "", false, false, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000046 RID: 70 RVA: 0x0000349F File Offset: 0x0000169F
		public static LocalizedString InvalidUpdates
		{
			get
			{
				return new LocalizedString("InvalidUpdates", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000034BD File Offset: 0x000016BD
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000012 RID: 18
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(18);

		// Token: 0x04000013 RID: 19
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Setup.ExSetup.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000007 RID: 7
		public enum IDs : uint
		{
			// Token: 0x04000015 RID: 21
			ProgressSummary = 1413825337U,
			// Token: 0x04000016 RID: 22
			HelpUrlHeaderText = 292337732U,
			// Token: 0x04000017 RID: 23
			RestartSetup = 2924129942U,
			// Token: 0x04000018 RID: 24
			ExecutionCompleted = 2127358907U,
			// Token: 0x04000019 RID: 25
			InstallETNotAllowed = 3235827753U,
			// Token: 0x0400001A RID: 26
			BuildToBuildUpgradeFailure = 128584875U,
			// Token: 0x0400001B RID: 27
			MoreInformationText = 4292992724U,
			// Token: 0x0400001C RID: 28
			AdditionalErrorDetails = 2883940283U,
			// Token: 0x0400001D RID: 29
			ExecutionFailed = 1435679081U,
			// Token: 0x0400001E RID: 30
			FileCopyComplete = 2448875626U,
			// Token: 0x0400001F RID: 31
			FileCopyText = 3972136246U,
			// Token: 0x04000020 RID: 32
			InvalidOSVersion = 2380070307U,
			// Token: 0x04000021 RID: 33
			NoUpdates = 3981406291U,
			// Token: 0x04000022 RID: 34
			EarlierVersionsExist = 3926021942U,
			// Token: 0x04000023 RID: 35
			InsufficientDiskSpace = 1648760670U,
			// Token: 0x04000024 RID: 36
			InvalidCommandLineArgs = 3147702355U,
			// Token: 0x04000025 RID: 37
			UpdatesNotOnFreshInstall = 1509727053U,
			// Token: 0x04000026 RID: 38
			InvalidUpdates = 4290716783U
		}

		// Token: 0x02000008 RID: 8
		private enum ParamIDs
		{
			// Token: 0x04000028 RID: 40
			UnhandledErrorMessage,
			// Token: 0x04000029 RID: 41
			FileCopyException,
			// Token: 0x0400002A RID: 42
			FileCopyFailed,
			// Token: 0x0400002B RID: 43
			SetupChecksFailed
		}
	}
}
