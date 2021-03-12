using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.Bootstrapper.Common
{
	// Token: 0x0200000B RID: 11
	internal static class Strings
	{
		// Token: 0x06000065 RID: 101 RVA: 0x00004268 File Offset: 0x00002468
		static Strings()
		{
			Strings.stringIDs.Add(1648760670U, "InsufficientDiskSpace");
			Strings.stringIDs.Add(3176520328U, "InvalidPSVersion");
			Strings.stringIDs.Add(1489536333U, "HalfAsterixLine");
			Strings.stringIDs.Add(700738506U, "ButtonTexkOk");
			Strings.stringIDs.Add(3878879664U, "SetupLogStarted");
			Strings.stringIDs.Add(2069127725U, "Bit64Only");
			Strings.stringIDs.Add(3926021942U, "EarlierVersionsExist");
			Strings.stringIDs.Add(1966425445U, "ExsetupInstallModeHelp");
			Strings.stringIDs.Add(4195359116U, "SetupLogEnd");
			Strings.stringIDs.Add(404444338U, "ExsetupUninstallModeHelp");
			Strings.stringIDs.Add(3167874725U, "InvalidNetFwVersion");
			Strings.stringIDs.Add(353833589U, "ExsetupDelegationHelp");
			Strings.stringIDs.Add(2953524114U, "NoLanguageAvailable");
			Strings.stringIDs.Add(469255115U, "Cancelled");
			Strings.stringIDs.Add(1207267855U, "ExsetupRecoverServerModeHelp");
			Strings.stringIDs.Add(2476235438U, "AsterixLine");
			Strings.stringIDs.Add(2387220348U, "ErrorExchangeNotInstalled");
			Strings.stringIDs.Add(2256981146U, "IAcceptLicenseParameterRequired");
			Strings.stringIDs.Add(2390584075U, "TreatPreReqErrorsAsWarnings");
			Strings.stringIDs.Add(1639033074U, "CannotRunMultipleInstances");
			Strings.stringIDs.Add(579043712U, "AttemptingToRunFromInstalledDirectory");
			Strings.stringIDs.Add(3536712912U, "ExsetupUpgradeModeHelp");
			Strings.stringIDs.Add(692269882U, "NotAdmin");
			Strings.stringIDs.Add(1773862023U, "ExsetupUmLanguagePacksHelp");
			Strings.stringIDs.Add(4145606535U, "ExsetupPrepareTopologyHelp");
			Strings.stringIDs.Add(71513441U, "MessageHeaderText");
			Strings.stringIDs.Add(3980248438U, "ExchangeNotInstalled");
			Strings.stringIDs.Add(2380070307U, "InvalidOSVersion");
			Strings.stringIDs.Add(2305405629U, "ExsetupGeneralHelp");
			Strings.stringIDs.Add(871634395U, "UnableToFindBuildVersion");
			Strings.stringIDs.Add(3231985017U, "AddRoleNotPossible");
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004510 File Offset: 0x00002710
		public static LocalizedString InsufficientDiskSpace
		{
			get
			{
				return new LocalizedString("InsufficientDiskSpace", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00004527 File Offset: 0x00002727
		public static LocalizedString InvalidPSVersion
		{
			get
			{
				return new LocalizedString("InvalidPSVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004540 File Offset: 0x00002740
		public static LocalizedString SetupLogInitializeFailure(string msg)
		{
			return new LocalizedString("SetupLogInitializeFailure", Strings.ResourceManager, new object[]
			{
				msg
			});
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004568 File Offset: 0x00002768
		public static LocalizedString LocalCopyBSFilesStart(string srcDir, string dstDir)
		{
			return new LocalizedString("LocalCopyBSFilesStart", Strings.ResourceManager, new object[]
			{
				srcDir,
				dstDir
			});
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00004594 File Offset: 0x00002794
		public static LocalizedString HalfAsterixLine
		{
			get
			{
				return new LocalizedString("HalfAsterixLine", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000045AC File Offset: 0x000027AC
		public static LocalizedString DirectoryAclInfo(string name, string acl)
		{
			return new LocalizedString("DirectoryAclInfo", Strings.ResourceManager, new object[]
			{
				name,
				acl
			});
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000045D8 File Offset: 0x000027D8
		public static LocalizedString CommandLine(string launcher, string cmdLine)
		{
			return new LocalizedString("CommandLine", Strings.ResourceManager, new object[]
			{
				launcher,
				cmdLine
			});
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004604 File Offset: 0x00002804
		public static LocalizedString UserName(string userName)
		{
			return new LocalizedString("UserName", Strings.ResourceManager, new object[]
			{
				userName
			});
		}

		// Token: 0x0600006E RID: 110 RVA: 0x0000462C File Offset: 0x0000282C
		public static LocalizedString FileCopyFailed(string srcDir, string dstDir)
		{
			return new LocalizedString("FileCopyFailed", Strings.ResourceManager, new object[]
			{
				srcDir,
				dstDir
			});
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004658 File Offset: 0x00002858
		public static LocalizedString OSVersion(string version)
		{
			return new LocalizedString("OSVersion", Strings.ResourceManager, new object[]
			{
				version
			});
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00004680 File Offset: 0x00002880
		public static LocalizedString ButtonTexkOk
		{
			get
			{
				return new LocalizedString("ButtonTexkOk", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00004698 File Offset: 0x00002898
		public static LocalizedString SecurityIssueFoundWhenInit(string errMsg)
		{
			return new LocalizedString("SecurityIssueFoundWhenInit", Strings.ResourceManager, new object[]
			{
				errMsg
			});
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000046C0 File Offset: 0x000028C0
		public static LocalizedString SetupLogStarted
		{
			get
			{
				return new LocalizedString("SetupLogStarted", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000046D8 File Offset: 0x000028D8
		public static LocalizedString RemovePrivileges(int win32Error)
		{
			return new LocalizedString("RemovePrivileges", Strings.ResourceManager, new object[]
			{
				win32Error
			});
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004705 File Offset: 0x00002905
		public static LocalizedString Bit64Only
		{
			get
			{
				return new LocalizedString("Bit64Only", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x0000471C File Offset: 0x0000291C
		public static LocalizedString LocalCopyAllSetupFilesEnd(string srcDir, string dstDir)
		{
			return new LocalizedString("LocalCopyAllSetupFilesEnd", Strings.ResourceManager, new object[]
			{
				srcDir,
				dstDir
			});
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004748 File Offset: 0x00002948
		public static LocalizedString EarlierVersionsExist
		{
			get
			{
				return new LocalizedString("EarlierVersionsExist", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000077 RID: 119 RVA: 0x0000475F File Offset: 0x0000295F
		public static LocalizedString ExsetupInstallModeHelp
		{
			get
			{
				return new LocalizedString("ExsetupInstallModeHelp", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004778 File Offset: 0x00002978
		public static LocalizedString LocalCopyBSFilesEnd(string srcDir, string dstDir)
		{
			return new LocalizedString("LocalCopyBSFilesEnd", Strings.ResourceManager, new object[]
			{
				srcDir,
				dstDir
			});
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000047A4 File Offset: 0x000029A4
		public static LocalizedString LocalTimeZone(string LocalZone)
		{
			return new LocalizedString("LocalTimeZone", Strings.ResourceManager, new object[]
			{
				LocalZone
			});
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000047CC File Offset: 0x000029CC
		public static LocalizedString DirectoryNotFound(string dirName)
		{
			return new LocalizedString("DirectoryNotFound", Strings.ResourceManager, new object[]
			{
				dirName
			});
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000047F4 File Offset: 0x000029F4
		public static LocalizedString UserNameError(string error)
		{
			return new LocalizedString("UserNameError", Strings.ResourceManager, new object[]
			{
				error
			});
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000481C File Offset: 0x00002A1C
		public static LocalizedString SetupLogEnd
		{
			get
			{
				return new LocalizedString("SetupLogEnd", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00004833 File Offset: 0x00002A33
		public static LocalizedString ExsetupUninstallModeHelp
		{
			get
			{
				return new LocalizedString("ExsetupUninstallModeHelp", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000484A File Offset: 0x00002A4A
		public static LocalizedString InvalidNetFwVersion
		{
			get
			{
				return new LocalizedString("InvalidNetFwVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00004861 File Offset: 0x00002A61
		public static LocalizedString ExsetupDelegationHelp
		{
			get
			{
				return new LocalizedString("ExsetupDelegationHelp", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00004878 File Offset: 0x00002A78
		public static LocalizedString NoLanguageAvailable
		{
			get
			{
				return new LocalizedString("NoLanguageAvailable", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000081 RID: 129 RVA: 0x0000488F File Offset: 0x00002A8F
		public static LocalizedString Cancelled
		{
			get
			{
				return new LocalizedString("Cancelled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000048A8 File Offset: 0x00002AA8
		public static LocalizedString LocalCopyAllSetupFilesStart(string srcDir, string dstDir)
		{
			return new LocalizedString("LocalCopyAllSetupFilesStart", Strings.ResourceManager, new object[]
			{
				srcDir,
				dstDir
			});
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000048D4 File Offset: 0x00002AD4
		public static LocalizedString AssemblyVersion(string assemblyVersion)
		{
			return new LocalizedString("AssemblyVersion", Strings.ResourceManager, new object[]
			{
				assemblyVersion
			});
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000048FC File Offset: 0x00002AFC
		public static LocalizedString ExsetupRecoverServerModeHelp
		{
			get
			{
				return new LocalizedString("ExsetupRecoverServerModeHelp", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00004913 File Offset: 0x00002B13
		public static LocalizedString AsterixLine
		{
			get
			{
				return new LocalizedString("AsterixLine", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000086 RID: 134 RVA: 0x0000492A File Offset: 0x00002B2A
		public static LocalizedString ErrorExchangeNotInstalled
		{
			get
			{
				return new LocalizedString("ErrorExchangeNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00004944 File Offset: 0x00002B44
		public static LocalizedString ParameterNotSupportedOnClientOS(string name)
		{
			return new LocalizedString("ParameterNotSupportedOnClientOS", Strings.ResourceManager, new object[]
			{
				name
			});
		}

		// Token: 0x06000088 RID: 136 RVA: 0x0000496C File Offset: 0x00002B6C
		public static LocalizedString StartupText(string productName)
		{
			return new LocalizedString("StartupText", Strings.ResourceManager, new object[]
			{
				productName
			});
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00004994 File Offset: 0x00002B94
		public static LocalizedString ExSetupCompleted(string message)
		{
			return new LocalizedString("ExSetupCompleted", Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000049BC File Offset: 0x00002BBC
		public static LocalizedString IAcceptLicenseParameterRequired
		{
			get
			{
				return new LocalizedString("IAcceptLicenseParameterRequired", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008B RID: 139 RVA: 0x000049D3 File Offset: 0x00002BD3
		public static LocalizedString TreatPreReqErrorsAsWarnings
		{
			get
			{
				return new LocalizedString("TreatPreReqErrorsAsWarnings", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000049EC File Offset: 0x00002BEC
		public static LocalizedString FileNotExistsError(string fullFilePath)
		{
			return new LocalizedString("FileNotExistsError", Strings.ResourceManager, new object[]
			{
				fullFilePath
			});
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004A14 File Offset: 0x00002C14
		public static LocalizedString CannotRunMultipleInstances
		{
			get
			{
				return new LocalizedString("CannotRunMultipleInstances", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00004A2C File Offset: 0x00002C2C
		public static LocalizedString AssemblyDLLFileLocation(string fullFilePath)
		{
			return new LocalizedString("AssemblyDLLFileLocation", Strings.ResourceManager, new object[]
			{
				fullFilePath
			});
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004A54 File Offset: 0x00002C54
		public static LocalizedString AttemptingToRunFromInstalledDirectory
		{
			get
			{
				return new LocalizedString("AttemptingToRunFromInstalledDirectory", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000090 RID: 144 RVA: 0x00004A6B File Offset: 0x00002C6B
		public static LocalizedString ExsetupUpgradeModeHelp
		{
			get
			{
				return new LocalizedString("ExsetupUpgradeModeHelp", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00004A84 File Offset: 0x00002C84
		public static LocalizedString FileNotFound(string fullFilePath)
		{
			return new LocalizedString("FileNotFound", Strings.ResourceManager, new object[]
			{
				fullFilePath
			});
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00004AAC File Offset: 0x00002CAC
		public static LocalizedString NotAdmin
		{
			get
			{
				return new LocalizedString("NotAdmin", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public static LocalizedString RemoteCopyAllSetupFilesEnd(string srcDir, string dstDir)
		{
			return new LocalizedString("RemoteCopyAllSetupFilesEnd", Strings.ResourceManager, new object[]
			{
				srcDir,
				dstDir
			});
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00004AF0 File Offset: 0x00002CF0
		public static LocalizedString StartSetupFileNotFound(string fileName)
		{
			return new LocalizedString("StartSetupFileNotFound", Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00004B18 File Offset: 0x00002D18
		public static LocalizedString ExsetupUmLanguagePacksHelp
		{
			get
			{
				return new LocalizedString("ExsetupUmLanguagePacksHelp", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004B2F File Offset: 0x00002D2F
		public static LocalizedString ExsetupPrepareTopologyHelp
		{
			get
			{
				return new LocalizedString("ExsetupPrepareTopologyHelp", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00004B46 File Offset: 0x00002D46
		public static LocalizedString MessageHeaderText
		{
			get
			{
				return new LocalizedString("MessageHeaderText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00004B5D File Offset: 0x00002D5D
		public static LocalizedString ExchangeNotInstalled
		{
			get
			{
				return new LocalizedString("ExchangeNotInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004B74 File Offset: 0x00002D74
		public static LocalizedString StartupTextCumulativeUpdate(string productName, int cumulativeUpdateVersion)
		{
			return new LocalizedString("StartupTextCumulativeUpdate", Strings.ResourceManager, new object[]
			{
				productName,
				cumulativeUpdateVersion
			});
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004BA8 File Offset: 0x00002DA8
		public static LocalizedString RemoteCopyAllSetupFilesStart(string srcDir, string dstDir)
		{
			return new LocalizedString("RemoteCopyAllSetupFilesStart", Strings.ResourceManager, new object[]
			{
				srcDir,
				dstDir
			});
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004BD4 File Offset: 0x00002DD4
		public static LocalizedString InvalidOSVersion
		{
			get
			{
				return new LocalizedString("InvalidOSVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004BEC File Offset: 0x00002DEC
		public static LocalizedString MissingRegistryKey(string regKey)
		{
			return new LocalizedString("MissingRegistryKey", Strings.ResourceManager, new object[]
			{
				regKey
			});
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004C14 File Offset: 0x00002E14
		public static LocalizedString ExsetupGeneralHelp
		{
			get
			{
				return new LocalizedString("ExsetupGeneralHelp", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004C2C File Offset: 0x00002E2C
		public static LocalizedString RemoteCopyBSFilesEnd(string srcDir, string dstDir)
		{
			return new LocalizedString("RemoteCopyBSFilesEnd", Strings.ResourceManager, new object[]
			{
				srcDir,
				dstDir
			});
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00004C58 File Offset: 0x00002E58
		public static LocalizedString RemoteCopyBSFilesStart(string srcDir, string dstDir)
		{
			return new LocalizedString("RemoteCopyBSFilesStart", Strings.ResourceManager, new object[]
			{
				srcDir,
				dstDir
			});
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004C84 File Offset: 0x00002E84
		public static LocalizedString UnableToFindBuildVersion
		{
			get
			{
				return new LocalizedString("UnableToFindBuildVersion", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004C9C File Offset: 0x00002E9C
		public static LocalizedString InvalidSourceDir(string sourceDir)
		{
			return new LocalizedString("InvalidSourceDir", Strings.ResourceManager, new object[]
			{
				sourceDir
			});
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004CC4 File Offset: 0x00002EC4
		public static LocalizedString AddRoleNotPossible
		{
			get
			{
				return new LocalizedString("AddRoleNotPossible", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004CDC File Offset: 0x00002EDC
		public static LocalizedString UnhandledErrorMessage(string errMsg)
		{
			return new LocalizedString("UnhandledErrorMessage", Strings.ResourceManager, new object[]
			{
				errMsg
			});
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004D04 File Offset: 0x00002F04
		public static LocalizedString SetupFailed(string errorMessage)
		{
			return new LocalizedString("SetupFailed", Strings.ResourceManager, new object[]
			{
				errorMessage
			});
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004D2C File Offset: 0x00002F2C
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x04000078 RID: 120
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(31);

		// Token: 0x04000079 RID: 121
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Setup.Bootstrapper.Common.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x0200000C RID: 12
		public enum IDs : uint
		{
			// Token: 0x0400007B RID: 123
			InsufficientDiskSpace = 1648760670U,
			// Token: 0x0400007C RID: 124
			InvalidPSVersion = 3176520328U,
			// Token: 0x0400007D RID: 125
			HalfAsterixLine = 1489536333U,
			// Token: 0x0400007E RID: 126
			ButtonTexkOk = 700738506U,
			// Token: 0x0400007F RID: 127
			SetupLogStarted = 3878879664U,
			// Token: 0x04000080 RID: 128
			Bit64Only = 2069127725U,
			// Token: 0x04000081 RID: 129
			EarlierVersionsExist = 3926021942U,
			// Token: 0x04000082 RID: 130
			ExsetupInstallModeHelp = 1966425445U,
			// Token: 0x04000083 RID: 131
			SetupLogEnd = 4195359116U,
			// Token: 0x04000084 RID: 132
			ExsetupUninstallModeHelp = 404444338U,
			// Token: 0x04000085 RID: 133
			InvalidNetFwVersion = 3167874725U,
			// Token: 0x04000086 RID: 134
			ExsetupDelegationHelp = 353833589U,
			// Token: 0x04000087 RID: 135
			NoLanguageAvailable = 2953524114U,
			// Token: 0x04000088 RID: 136
			Cancelled = 469255115U,
			// Token: 0x04000089 RID: 137
			ExsetupRecoverServerModeHelp = 1207267855U,
			// Token: 0x0400008A RID: 138
			AsterixLine = 2476235438U,
			// Token: 0x0400008B RID: 139
			ErrorExchangeNotInstalled = 2387220348U,
			// Token: 0x0400008C RID: 140
			IAcceptLicenseParameterRequired = 2256981146U,
			// Token: 0x0400008D RID: 141
			TreatPreReqErrorsAsWarnings = 2390584075U,
			// Token: 0x0400008E RID: 142
			CannotRunMultipleInstances = 1639033074U,
			// Token: 0x0400008F RID: 143
			AttemptingToRunFromInstalledDirectory = 579043712U,
			// Token: 0x04000090 RID: 144
			ExsetupUpgradeModeHelp = 3536712912U,
			// Token: 0x04000091 RID: 145
			NotAdmin = 692269882U,
			// Token: 0x04000092 RID: 146
			ExsetupUmLanguagePacksHelp = 1773862023U,
			// Token: 0x04000093 RID: 147
			ExsetupPrepareTopologyHelp = 4145606535U,
			// Token: 0x04000094 RID: 148
			MessageHeaderText = 71513441U,
			// Token: 0x04000095 RID: 149
			ExchangeNotInstalled = 3980248438U,
			// Token: 0x04000096 RID: 150
			InvalidOSVersion = 2380070307U,
			// Token: 0x04000097 RID: 151
			ExsetupGeneralHelp = 2305405629U,
			// Token: 0x04000098 RID: 152
			UnableToFindBuildVersion = 871634395U,
			// Token: 0x04000099 RID: 153
			AddRoleNotPossible = 3231985017U
		}

		// Token: 0x0200000D RID: 13
		private enum ParamIDs
		{
			// Token: 0x0400009B RID: 155
			SetupLogInitializeFailure,
			// Token: 0x0400009C RID: 156
			LocalCopyBSFilesStart,
			// Token: 0x0400009D RID: 157
			DirectoryAclInfo,
			// Token: 0x0400009E RID: 158
			CommandLine,
			// Token: 0x0400009F RID: 159
			UserName,
			// Token: 0x040000A0 RID: 160
			FileCopyFailed,
			// Token: 0x040000A1 RID: 161
			OSVersion,
			// Token: 0x040000A2 RID: 162
			SecurityIssueFoundWhenInit,
			// Token: 0x040000A3 RID: 163
			RemovePrivileges,
			// Token: 0x040000A4 RID: 164
			LocalCopyAllSetupFilesEnd,
			// Token: 0x040000A5 RID: 165
			LocalCopyBSFilesEnd,
			// Token: 0x040000A6 RID: 166
			LocalTimeZone,
			// Token: 0x040000A7 RID: 167
			DirectoryNotFound,
			// Token: 0x040000A8 RID: 168
			UserNameError,
			// Token: 0x040000A9 RID: 169
			LocalCopyAllSetupFilesStart,
			// Token: 0x040000AA RID: 170
			AssemblyVersion,
			// Token: 0x040000AB RID: 171
			ParameterNotSupportedOnClientOS,
			// Token: 0x040000AC RID: 172
			StartupText,
			// Token: 0x040000AD RID: 173
			ExSetupCompleted,
			// Token: 0x040000AE RID: 174
			FileNotExistsError,
			// Token: 0x040000AF RID: 175
			AssemblyDLLFileLocation,
			// Token: 0x040000B0 RID: 176
			FileNotFound,
			// Token: 0x040000B1 RID: 177
			RemoteCopyAllSetupFilesEnd,
			// Token: 0x040000B2 RID: 178
			StartSetupFileNotFound,
			// Token: 0x040000B3 RID: 179
			StartupTextCumulativeUpdate,
			// Token: 0x040000B4 RID: 180
			RemoteCopyAllSetupFilesStart,
			// Token: 0x040000B5 RID: 181
			MissingRegistryKey,
			// Token: 0x040000B6 RID: 182
			RemoteCopyBSFilesEnd,
			// Token: 0x040000B7 RID: 183
			RemoteCopyBSFilesStart,
			// Token: 0x040000B8 RID: 184
			InvalidSourceDir,
			// Token: 0x040000B9 RID: 185
			UnhandledErrorMessage,
			// Token: 0x040000BA RID: 186
			SetupFailed
		}
	}
}
