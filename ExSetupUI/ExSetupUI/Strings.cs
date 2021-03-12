using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x0200002F RID: 47
	internal static class Strings
	{
		// Token: 0x06000208 RID: 520 RVA: 0x0000BE3C File Offset: 0x0000A03C
		static Strings()
		{
			Strings.stringIDs.Add(1884817196U, "ProgressPageTitle");
			Strings.stringIDs.Add(22442200U, "Error");
			Strings.stringIDs.Add(362807949U, "OpenSetupFileLog");
			Strings.stringIDs.Add(1949480476U, "CheckForUpdatesPageTitle");
			Strings.stringIDs.Add(3298162955U, "InitializingSetupText");
			Strings.stringIDs.Add(1748135463U, "UserDownloadAbort");
			Strings.stringIDs.Add(3541939918U, "ErrorCreatingRegKey");
			Strings.stringIDs.Add(683317649U, "RemoveDownloadedUpdates");
			Strings.stringIDs.Add(1370326773U, "DownloadStarted");
			Strings.stringIDs.Add(914633731U, "BtnCancelCopy");
			Strings.stringIDs.Add(1325396371U, "CheckForUpdatePreviouslyDownloadedExistText");
			Strings.stringIDs.Add(1395665443U, "InvalidLanguageBundle");
			Strings.stringIDs.Add(2995245679U, "CheckForUpdateLabelText");
			Strings.stringIDs.Add(1026289097U, "ApplyingUpdatesPageTitle");
			Strings.stringIDs.Add(665255466U, "OlderVersionsOfServerRolesInstalled");
			Strings.stringIDs.Add(577084905U, "btnPrevious");
			Strings.stringIDs.Add(2900920565U, "ExchangeServerRoles");
			Strings.stringIDs.Add(2716635429U, "DownloadInstallationCompleted");
			Strings.stringIDs.Add(1316856433U, "CheckForUpdateYesRadioButtonText");
			Strings.stringIDs.Add(3738111846U, "SplashText");
			Strings.stringIDs.Add(2358390244U, "Cancel");
			Strings.stringIDs.Add(2358183302U, "Checking");
			Strings.stringIDs.Add(766700124U, "PreviousDownloadedUpdatesRadioButtonText");
			Strings.stringIDs.Add(2440240611U, "ResumeChoiceText");
			Strings.stringIDs.Add(1333650384U, "UnifiedMessagingLanguagePacks");
			Strings.stringIDs.Add(3013797053U, "CheckForUpdateYesNoLabelText");
			Strings.stringIDs.Add(3057699019U, "DiskSpaceCapacityUnit");
			Strings.stringIDs.Add(816751771U, "btnNext");
			Strings.stringIDs.Add(1263419505U, "DownloadNoUpdatesFound");
			Strings.stringIDs.Add(2847757096U, "CancelDownloadBtn");
			Strings.stringIDs.Add(3608358242U, "Upgrade");
			Strings.stringIDs.Add(319275761U, "Install");
			Strings.stringIDs.Add(2998280118U, "Introduction");
			Strings.stringIDs.Add(3394043311U, "NotFound");
			Strings.stringIDs.Add(3641255076U, "btnRetry");
			Strings.stringIDs.Add(509984218U, "CheckForAvailableSpace");
			Strings.stringIDs.Add(2753928508U, "CancellingDownload");
			Strings.stringIDs.Add(983858003U, "FileCopyingError");
			Strings.stringIDs.Add(4184993237U, "BrowserLaunchError");
			Strings.stringIDs.Add(2744422095U, "CopyFilesPageTitle");
			Strings.stringIDs.Add(3339504604U, "VerifyingLangPackBundle");
			Strings.stringIDs.Add(3857751296U, "SetupFailedPrintDocument");
			Strings.stringIDs.Add(4173214713U, "DownloadCompleted");
			Strings.stringIDs.Add(3836876260U, "btnCancel");
			Strings.stringIDs.Add(686568928U, "NotEnoughDiskSpace");
			Strings.stringIDs.Add(1865457820U, "UpdatesDownloadsPageTitle");
			Strings.stringIDs.Add(17553301U, "friendlyInvalidLanguagePackBundle");
			Strings.stringIDs.Add(4081297114U, "LanguagePacksUpToDate");
			Strings.stringIDs.Add(3898878242U, "NoExchangeInstalled");
			Strings.stringIDs.Add(2254969718U, "btnExit");
			Strings.stringIDs.Add(1962719391U, "UnableToExtract");
			Strings.stringIDs.Add(2475917809U, "DeletingTempFiles");
			Strings.stringIDs.Add(1648760670U, "InsufficientDiskSpace");
			Strings.stringIDs.Add(2041152115U, "CopyfileInstallText");
			Strings.stringIDs.Add(4244802355U, "btnFinish");
			Strings.stringIDs.Add(2295868152U, "ResumeChoiceTitle");
			Strings.stringIDs.Add(2009416511U, "FileCopyDone");
			Strings.stringIDs.Add(3779522776U, "Uninstall");
			Strings.stringIDs.Add(2981091031U, "CheckForUpdateNoRadioButtonText");
			Strings.stringIDs.Add(2463541198U, "NoText");
			Strings.stringIDs.Add(3365230170U, "YesText");
			Strings.stringIDs.Add(2225800973U, "ClosingHTTPConnection");
			Strings.stringIDs.Add(2909533909U, "CancelFileCopying");
			Strings.stringIDs.Add(1054423051U, "Failed");
			Strings.stringIDs.Add(378004180U, "AssemblyLoadError");
			Strings.stringIDs.Add(2116473977U, "DiskSpaceAllocationTitle");
			Strings.stringIDs.Add(2263082167U, "CancelMessageBoxMessage");
			Strings.stringIDs.Add(2487084378U, "AlreadyDownloadedRadio");
			Strings.stringIDs.Add(1891569513U, "FileCopyingFinished");
			Strings.stringIDs.Add(72796564U, "VerifyInstallationCompleted");
			Strings.stringIDs.Add(1861340610U, "Warning");
			Strings.stringIDs.Add(3004026682U, "SetupWizardCaption");
			Strings.stringIDs.Add(3210618713U, "btnComplete");
			Strings.stringIDs.Add(454692482U, "friendlyWebException");
			Strings.stringIDs.Add(367349671U, "RequiredDiskSpaceDescriptionText");
			Strings.stringIDs.Add(1912677597U, "CopyFilesText");
			Strings.stringIDs.Add(3074405412U, "RegistryKeyForLanguagePackFound");
			Strings.stringIDs.Add(2431595041U, "OkText");
			Strings.stringIDs.Add(3036876617U, "InitializingSetupPageTitle");
			Strings.stringIDs.Add(273661265U, "Initializing");
			Strings.stringIDs.Add(2354229627U, "StartFileCopying");
			Strings.stringIDs.Add(678271266U, "Passed");
			Strings.stringIDs.Add(1883156336U, "MicrosoftExchangeServer");
			Strings.stringIDs.Add(622196391U, "AvailableDiskSpaceDescriptionText");
			Strings.stringIDs.Add(3065873541U, "IncompatibleBundle");
			Strings.stringIDs.Add(1128547615U, "AttemptingToCleanTheRegistry");
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000C530 File Offset: 0x0000A730
		public static LocalizedString ProgressPageTitle
		{
			get
			{
				return new LocalizedString("ProgressPageTitle", "Ex6A547E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600020A RID: 522 RVA: 0x0000C54E File Offset: 0x0000A74E
		public static LocalizedString Error
		{
			get
			{
				return new LocalizedString("Error", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000C56C File Offset: 0x0000A76C
		public static LocalizedString OpenSetupFileLog
		{
			get
			{
				return new LocalizedString("OpenSetupFileLog", "ExC9BEF6", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600020C RID: 524 RVA: 0x0000C58A File Offset: 0x0000A78A
		public static LocalizedString CheckForUpdatesPageTitle
		{
			get
			{
				return new LocalizedString("CheckForUpdatesPageTitle", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600020D RID: 525 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		public static LocalizedString InvalidLocalLPVersioningXML(string filePath)
		{
			return new LocalizedString("InvalidLocalLPVersioningXML", "ExBC0925", false, true, Strings.ResourceManager, new object[]
			{
				filePath
			});
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600020E RID: 526 RVA: 0x0000C5D7 File Offset: 0x0000A7D7
		public static LocalizedString InitializingSetupText
		{
			get
			{
				return new LocalizedString("InitializingSetupText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000C5F5 File Offset: 0x0000A7F5
		public static LocalizedString UserDownloadAbort
		{
			get
			{
				return new LocalizedString("UserDownloadAbort", "ExAE5A47", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000C613 File Offset: 0x0000A813
		public static LocalizedString ErrorCreatingRegKey
		{
			get
			{
				return new LocalizedString("ErrorCreatingRegKey", "ExBD5353", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000211 RID: 529 RVA: 0x0000C631 File Offset: 0x0000A831
		public static LocalizedString RemoveDownloadedUpdates
		{
			get
			{
				return new LocalizedString("RemoveDownloadedUpdates", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000C64F File Offset: 0x0000A84F
		public static LocalizedString DownloadStarted
		{
			get
			{
				return new LocalizedString("DownloadStarted", "Ex64E70F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000C66D File Offset: 0x0000A86D
		public static LocalizedString BtnCancelCopy
		{
			get
			{
				return new LocalizedString("BtnCancelCopy", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000C68C File Offset: 0x0000A88C
		public static LocalizedString RegularStatusMessage(string message)
		{
			return new LocalizedString("RegularStatusMessage", "Ex6E8952", false, true, Strings.ResourceManager, new object[]
			{
				message
			});
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000C6BB File Offset: 0x0000A8BB
		public static LocalizedString CheckForUpdatePreviouslyDownloadedExistText
		{
			get
			{
				return new LocalizedString("CheckForUpdatePreviouslyDownloadedExistText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000C6D9 File Offset: 0x0000A8D9
		public static LocalizedString InvalidLanguageBundle
		{
			get
			{
				return new LocalizedString("InvalidLanguageBundle", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000C6F7 File Offset: 0x0000A8F7
		public static LocalizedString CheckForUpdateLabelText
		{
			get
			{
				return new LocalizedString("CheckForUpdateLabelText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000C715 File Offset: 0x0000A915
		public static LocalizedString ApplyingUpdatesPageTitle
		{
			get
			{
				return new LocalizedString("ApplyingUpdatesPageTitle", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000C733 File Offset: 0x0000A933
		public static LocalizedString OlderVersionsOfServerRolesInstalled
		{
			get
			{
				return new LocalizedString("OlderVersionsOfServerRolesInstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000C751 File Offset: 0x0000A951
		public static LocalizedString btnPrevious
		{
			get
			{
				return new LocalizedString("btnPrevious", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000C76F File Offset: 0x0000A96F
		public static LocalizedString ExchangeServerRoles
		{
			get
			{
				return new LocalizedString("ExchangeServerRoles", "ExCCFFA7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000C78D File Offset: 0x0000A98D
		public static LocalizedString DownloadInstallationCompleted
		{
			get
			{
				return new LocalizedString("DownloadInstallationCompleted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000C7AB File Offset: 0x0000A9AB
		public static LocalizedString CheckForUpdateYesRadioButtonText
		{
			get
			{
				return new LocalizedString("CheckForUpdateYesRadioButtonText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000C7C9 File Offset: 0x0000A9C9
		public static LocalizedString SplashText
		{
			get
			{
				return new LocalizedString("SplashText", "Ex1A1857", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000C7E7 File Offset: 0x0000A9E7
		public static LocalizedString Cancel
		{
			get
			{
				return new LocalizedString("Cancel", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000C805 File Offset: 0x0000AA05
		public static LocalizedString Checking
		{
			get
			{
				return new LocalizedString("Checking", "Ex4893DC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000C823 File Offset: 0x0000AA23
		public static LocalizedString PreviousDownloadedUpdatesRadioButtonText
		{
			get
			{
				return new LocalizedString("PreviousDownloadedUpdatesRadioButtonText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000C844 File Offset: 0x0000AA44
		public static LocalizedString StartEsmPath(string esmpath)
		{
			return new LocalizedString("StartEsmPath", "Ex450FAA", false, true, Strings.ResourceManager, new object[]
			{
				esmpath
			});
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000223 RID: 547 RVA: 0x0000C873 File Offset: 0x0000AA73
		public static LocalizedString ResumeChoiceText
		{
			get
			{
				return new LocalizedString("ResumeChoiceText", "Ex5CE8AE", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000224 RID: 548 RVA: 0x0000C891 File Offset: 0x0000AA91
		public static LocalizedString UnifiedMessagingLanguagePacks
		{
			get
			{
				return new LocalizedString("UnifiedMessagingLanguagePacks", "Ex69FC36", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000225 RID: 549 RVA: 0x0000C8AF File Offset: 0x0000AAAF
		public static LocalizedString CheckForUpdateYesNoLabelText
		{
			get
			{
				return new LocalizedString("CheckForUpdateYesNoLabelText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000C8CD File Offset: 0x0000AACD
		public static LocalizedString DiskSpaceCapacityUnit
		{
			get
			{
				return new LocalizedString("DiskSpaceCapacityUnit", "Ex0D5436", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000C8EB File Offset: 0x0000AAEB
		public static LocalizedString btnNext
		{
			get
			{
				return new LocalizedString("btnNext", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000C909 File Offset: 0x0000AB09
		public static LocalizedString DownloadNoUpdatesFound
		{
			get
			{
				return new LocalizedString("DownloadNoUpdatesFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000C927 File Offset: 0x0000AB27
		public static LocalizedString CancelDownloadBtn
		{
			get
			{
				return new LocalizedString("CancelDownloadBtn", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000C945 File Offset: 0x0000AB45
		public static LocalizedString Upgrade
		{
			get
			{
				return new LocalizedString("Upgrade", "Ex24F459", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000C963 File Offset: 0x0000AB63
		public static LocalizedString Install
		{
			get
			{
				return new LocalizedString("Install", "ExA3B7DC", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000C981 File Offset: 0x0000AB81
		public static LocalizedString Introduction
		{
			get
			{
				return new LocalizedString("Introduction", "Ex69105E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000C9A0 File Offset: 0x0000ABA0
		public static LocalizedString MESI(string productName)
		{
			return new LocalizedString("MESI", "Ex0FD49F", false, true, Strings.ResourceManager, new object[]
			{
				productName
			});
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000C9CF File Offset: 0x0000ABCF
		public static LocalizedString NotFound
		{
			get
			{
				return new LocalizedString("NotFound", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600022F RID: 559 RVA: 0x0000C9ED File Offset: 0x0000ABED
		public static LocalizedString btnRetry
		{
			get
			{
				return new LocalizedString("btnRetry", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000230 RID: 560 RVA: 0x0000CA0B File Offset: 0x0000AC0B
		public static LocalizedString CheckForAvailableSpace
		{
			get
			{
				return new LocalizedString("CheckForAvailableSpace", "Ex6CD438", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000CA2C File Offset: 0x0000AC2C
		public static LocalizedString CantFindSourceDir(string srcDir)
		{
			return new LocalizedString("CantFindSourceDir", "ExCA3E63", false, true, Strings.ResourceManager, new object[]
			{
				srcDir
			});
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000232 RID: 562 RVA: 0x0000CA5B File Offset: 0x0000AC5B
		public static LocalizedString CancellingDownload
		{
			get
			{
				return new LocalizedString("CancellingDownload", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000233 RID: 563 RVA: 0x0000CA79 File Offset: 0x0000AC79
		public static LocalizedString FileCopyingError
		{
			get
			{
				return new LocalizedString("FileCopyingError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000234 RID: 564 RVA: 0x0000CA97 File Offset: 0x0000AC97
		public static LocalizedString BrowserLaunchError
		{
			get
			{
				return new LocalizedString("BrowserLaunchError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000CAB8 File Offset: 0x0000ACB8
		public static LocalizedString DiskSpaceAvailable(string requiredSpace)
		{
			return new LocalizedString("DiskSpaceAvailable", "", false, false, Strings.ResourceManager, new object[]
			{
				requiredSpace
			});
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000236 RID: 566 RVA: 0x0000CAE7 File Offset: 0x0000ACE7
		public static LocalizedString CopyFilesPageTitle
		{
			get
			{
				return new LocalizedString("CopyFilesPageTitle", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000CB08 File Offset: 0x0000AD08
		public static LocalizedString NoFilesToCopy(string sourceDir)
		{
			return new LocalizedString("NoFilesToCopy", "", false, false, Strings.ResourceManager, new object[]
			{
				sourceDir
			});
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000CB37 File Offset: 0x0000AD37
		public static LocalizedString VerifyingLangPackBundle
		{
			get
			{
				return new LocalizedString("VerifyingLangPackBundle", "Ex4B0CF5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000239 RID: 569 RVA: 0x0000CB55 File Offset: 0x0000AD55
		public static LocalizedString SetupFailedPrintDocument
		{
			get
			{
				return new LocalizedString("SetupFailedPrintDocument", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600023A RID: 570 RVA: 0x0000CB73 File Offset: 0x0000AD73
		public static LocalizedString DownloadCompleted
		{
			get
			{
				return new LocalizedString("DownloadCompleted", "ExB64433", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000CB94 File Offset: 0x0000AD94
		public static LocalizedString SignatureVerificationFailed(string pathToFile)
		{
			return new LocalizedString("SignatureVerificationFailed", "Ex2BD1E3", false, true, Strings.ResourceManager, new object[]
			{
				pathToFile
			});
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600023C RID: 572 RVA: 0x0000CBC3 File Offset: 0x0000ADC3
		public static LocalizedString btnCancel
		{
			get
			{
				return new LocalizedString("btnCancel", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600023D RID: 573 RVA: 0x0000CBE1 File Offset: 0x0000ADE1
		public static LocalizedString NotEnoughDiskSpace
		{
			get
			{
				return new LocalizedString("NotEnoughDiskSpace", "ExBD18E2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600023E RID: 574 RVA: 0x0000CBFF File Offset: 0x0000ADFF
		public static LocalizedString UpdatesDownloadsPageTitle
		{
			get
			{
				return new LocalizedString("UpdatesDownloadsPageTitle", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600023F RID: 575 RVA: 0x0000CC1D File Offset: 0x0000AE1D
		public static LocalizedString friendlyInvalidLanguagePackBundle
		{
			get
			{
				return new LocalizedString("friendlyInvalidLanguagePackBundle", "ExC9CED5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0000CC3B File Offset: 0x0000AE3B
		public static LocalizedString LanguagePacksUpToDate
		{
			get
			{
				return new LocalizedString("LanguagePacksUpToDate", "ExCF3830", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000CC59 File Offset: 0x0000AE59
		public static LocalizedString NoExchangeInstalled
		{
			get
			{
				return new LocalizedString("NoExchangeInstalled", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000CC78 File Offset: 0x0000AE78
		public static LocalizedString OpenSetupFileLogError(string file, string reason)
		{
			return new LocalizedString("OpenSetupFileLogError", "Ex469720", false, true, Strings.ResourceManager, new object[]
			{
				file,
				reason
			});
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000243 RID: 579 RVA: 0x0000CCAB File Offset: 0x0000AEAB
		public static LocalizedString btnExit
		{
			get
			{
				return new LocalizedString("btnExit", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000244 RID: 580 RVA: 0x0000CCC9 File Offset: 0x0000AEC9
		public static LocalizedString UnableToExtract
		{
			get
			{
				return new LocalizedString("UnableToExtract", "Ex6531D5", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000245 RID: 581 RVA: 0x0000CCE7 File Offset: 0x0000AEE7
		public static LocalizedString DeletingTempFiles
		{
			get
			{
				return new LocalizedString("DeletingTempFiles", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000246 RID: 582 RVA: 0x0000CD05 File Offset: 0x0000AF05
		public static LocalizedString InsufficientDiskSpace
		{
			get
			{
				return new LocalizedString("InsufficientDiskSpace", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000CD24 File Offset: 0x0000AF24
		public static LocalizedString CopyingDirectoryFromTo(string dirFrom, string dirTo)
		{
			return new LocalizedString("CopyingDirectoryFromTo", "", false, false, Strings.ResourceManager, new object[]
			{
				dirFrom,
				dirTo
			});
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000CD58 File Offset: 0x0000AF58
		public static LocalizedString MESU(string productName)
		{
			return new LocalizedString("MESU", "Ex0ED6F2", false, true, Strings.ResourceManager, new object[]
			{
				productName
			});
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000249 RID: 585 RVA: 0x0000CD87 File Offset: 0x0000AF87
		public static LocalizedString CopyfileInstallText
		{
			get
			{
				return new LocalizedString("CopyfileInstallText", "ExE52B54", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600024A RID: 586 RVA: 0x0000CDA5 File Offset: 0x0000AFA5
		public static LocalizedString btnFinish
		{
			get
			{
				return new LocalizedString("btnFinish", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600024B RID: 587 RVA: 0x0000CDC3 File Offset: 0x0000AFC3
		public static LocalizedString ResumeChoiceTitle
		{
			get
			{
				return new LocalizedString("ResumeChoiceTitle", "Ex027A2E", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600024C RID: 588 RVA: 0x0000CDE1 File Offset: 0x0000AFE1
		public static LocalizedString FileCopyDone
		{
			get
			{
				return new LocalizedString("FileCopyDone", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600024D RID: 589 RVA: 0x0000CDFF File Offset: 0x0000AFFF
		public static LocalizedString Uninstall
		{
			get
			{
				return new LocalizedString("Uninstall", "ExA1B98F", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600024E RID: 590 RVA: 0x0000CE1D File Offset: 0x0000B01D
		public static LocalizedString CheckForUpdateNoRadioButtonText
		{
			get
			{
				return new LocalizedString("CheckForUpdateNoRadioButtonText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x0600024F RID: 591 RVA: 0x0000CE3B File Offset: 0x0000B03B
		public static LocalizedString NoText
		{
			get
			{
				return new LocalizedString("NoText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000250 RID: 592 RVA: 0x0000CE59 File Offset: 0x0000B059
		public static LocalizedString YesText
		{
			get
			{
				return new LocalizedString("YesText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000251 RID: 593 RVA: 0x0000CE77 File Offset: 0x0000B077
		public static LocalizedString ClosingHTTPConnection
		{
			get
			{
				return new LocalizedString("ClosingHTTPConnection", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x0000CE98 File Offset: 0x0000B098
		public static LocalizedString BundleDoesntExist(string pathName)
		{
			return new LocalizedString("BundleDoesntExist", "ExCB4C12", false, true, Strings.ResourceManager, new object[]
			{
				pathName
			});
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000253 RID: 595 RVA: 0x0000CEC7 File Offset: 0x0000B0C7
		public static LocalizedString CancelFileCopying
		{
			get
			{
				return new LocalizedString("CancelFileCopying", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000254 RID: 596 RVA: 0x0000CEE8 File Offset: 0x0000B0E8
		public static LocalizedString MESICumulativeUpdate(string productName, int cumulativeUpdateVersion)
		{
			return new LocalizedString("MESICumulativeUpdate", "", false, false, Strings.ResourceManager, new object[]
			{
				productName,
				cumulativeUpdateVersion
			});
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000255 RID: 597 RVA: 0x0000CF20 File Offset: 0x0000B120
		public static LocalizedString Failed
		{
			get
			{
				return new LocalizedString("Failed", "ExE5BE21", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0000CF40 File Offset: 0x0000B140
		public static LocalizedString FinishedWithError(string pathToLog)
		{
			return new LocalizedString("FinishedWithError", "", false, false, Strings.ResourceManager, new object[]
			{
				pathToLog
			});
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000257 RID: 599 RVA: 0x0000CF6F File Offset: 0x0000B16F
		public static LocalizedString AssemblyLoadError
		{
			get
			{
				return new LocalizedString("AssemblyLoadError", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000CF8D File Offset: 0x0000B18D
		public static LocalizedString DiskSpaceAllocationTitle
		{
			get
			{
				return new LocalizedString("DiskSpaceAllocationTitle", "Ex060B5D", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000CFAB File Offset: 0x0000B1AB
		public static LocalizedString CancelMessageBoxMessage
		{
			get
			{
				return new LocalizedString("CancelMessageBoxMessage", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000CFC9 File Offset: 0x0000B1C9
		public static LocalizedString AlreadyDownloadedRadio
		{
			get
			{
				return new LocalizedString("AlreadyDownloadedRadio", "Ex3BA18A", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000CFE7 File Offset: 0x0000B1E7
		public static LocalizedString FileCopyingFinished
		{
			get
			{
				return new LocalizedString("FileCopyingFinished", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000D005 File Offset: 0x0000B205
		public static LocalizedString VerifyInstallationCompleted
		{
			get
			{
				return new LocalizedString("VerifyInstallationCompleted", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000D023 File Offset: 0x0000B223
		public static LocalizedString Warning
		{
			get
			{
				return new LocalizedString("Warning", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000D041 File Offset: 0x0000B241
		public static LocalizedString SetupWizardCaption
		{
			get
			{
				return new LocalizedString("SetupWizardCaption", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000D05F File Offset: 0x0000B25F
		public static LocalizedString btnComplete
		{
			get
			{
				return new LocalizedString("btnComplete", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000D07D File Offset: 0x0000B27D
		public static LocalizedString friendlyWebException
		{
			get
			{
				return new LocalizedString("friendlyWebException", "ExA07AC7", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000D09B File Offset: 0x0000B29B
		public static LocalizedString RequiredDiskSpaceDescriptionText
		{
			get
			{
				return new LocalizedString("RequiredDiskSpaceDescriptionText", "Ex227940", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000262 RID: 610 RVA: 0x0000D0B9 File Offset: 0x0000B2B9
		public static LocalizedString CopyFilesText
		{
			get
			{
				return new LocalizedString("CopyFilesText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000D0D7 File Offset: 0x0000B2D7
		public static LocalizedString RegistryKeyForLanguagePackFound
		{
			get
			{
				return new LocalizedString("RegistryKeyForLanguagePackFound", "Ex8D5C84", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000264 RID: 612 RVA: 0x0000D0F5 File Offset: 0x0000B2F5
		public static LocalizedString OkText
		{
			get
			{
				return new LocalizedString("OkText", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0000D113 File Offset: 0x0000B313
		public static LocalizedString InitializingSetupPageTitle
		{
			get
			{
				return new LocalizedString("InitializingSetupPageTitle", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000266 RID: 614 RVA: 0x0000D131 File Offset: 0x0000B331
		public static LocalizedString Initializing
		{
			get
			{
				return new LocalizedString("Initializing", "Ex3C5EA2", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000D14F File Offset: 0x0000B34F
		public static LocalizedString StartFileCopying
		{
			get
			{
				return new LocalizedString("StartFileCopying", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D170 File Offset: 0x0000B370
		public static LocalizedString AssemblyLoadFileNotFound(string fullFileName, string setupArgs, string sourceDir, string targetDir, bool isExInstalled)
		{
			return new LocalizedString("AssemblyLoadFileNotFound", "", false, false, Strings.ResourceManager, new object[]
			{
				fullFileName,
				setupArgs,
				sourceDir,
				targetDir,
				isExInstalled
			});
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000D1B8 File Offset: 0x0000B3B8
		public static LocalizedString InvalidFile(string fileName)
		{
			return new LocalizedString("InvalidFile", "ExD10C03", false, true, Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600026A RID: 618 RVA: 0x0000D1E7 File Offset: 0x0000B3E7
		public static LocalizedString Passed
		{
			get
			{
				return new LocalizedString("Passed", "ExF3A909", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000D208 File Offset: 0x0000B408
		public static LocalizedString DiskSpaceRequired(string requiredSpace)
		{
			return new LocalizedString("DiskSpaceRequired", "", false, false, Strings.ResourceManager, new object[]
			{
				requiredSpace
			});
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000D237 File Offset: 0x0000B437
		public static LocalizedString MicrosoftExchangeServer
		{
			get
			{
				return new LocalizedString("MicrosoftExchangeServer", "", false, false, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000D258 File Offset: 0x0000B458
		public static LocalizedString PageLoaded(string pageName)
		{
			return new LocalizedString("PageLoaded", "", false, false, Strings.ResourceManager, new object[]
			{
				pageName
			});
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000D287 File Offset: 0x0000B487
		public static LocalizedString AvailableDiskSpaceDescriptionText
		{
			get
			{
				return new LocalizedString("AvailableDiskSpaceDescriptionText", "Ex0576F1", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600026F RID: 623 RVA: 0x0000D2A5 File Offset: 0x0000B4A5
		public static LocalizedString IncompatibleBundle
		{
			get
			{
				return new LocalizedString("IncompatibleBundle", "Ex9BA123", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000D2C3 File Offset: 0x0000B4C3
		public static LocalizedString AttemptingToCleanTheRegistry
		{
			get
			{
				return new LocalizedString("AttemptingToCleanTheRegistry", "Ex4463D9", false, true, Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D2E1 File Offset: 0x0000B4E1
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x0400011C RID: 284
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(86);

		// Token: 0x0400011D RID: 285
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Setup.ExSetupUI.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000030 RID: 48
		public enum IDs : uint
		{
			// Token: 0x0400011F RID: 287
			ProgressPageTitle = 1884817196U,
			// Token: 0x04000120 RID: 288
			Error = 22442200U,
			// Token: 0x04000121 RID: 289
			OpenSetupFileLog = 362807949U,
			// Token: 0x04000122 RID: 290
			CheckForUpdatesPageTitle = 1949480476U,
			// Token: 0x04000123 RID: 291
			InitializingSetupText = 3298162955U,
			// Token: 0x04000124 RID: 292
			UserDownloadAbort = 1748135463U,
			// Token: 0x04000125 RID: 293
			ErrorCreatingRegKey = 3541939918U,
			// Token: 0x04000126 RID: 294
			RemoveDownloadedUpdates = 683317649U,
			// Token: 0x04000127 RID: 295
			DownloadStarted = 1370326773U,
			// Token: 0x04000128 RID: 296
			BtnCancelCopy = 914633731U,
			// Token: 0x04000129 RID: 297
			CheckForUpdatePreviouslyDownloadedExistText = 1325396371U,
			// Token: 0x0400012A RID: 298
			InvalidLanguageBundle = 1395665443U,
			// Token: 0x0400012B RID: 299
			CheckForUpdateLabelText = 2995245679U,
			// Token: 0x0400012C RID: 300
			ApplyingUpdatesPageTitle = 1026289097U,
			// Token: 0x0400012D RID: 301
			OlderVersionsOfServerRolesInstalled = 665255466U,
			// Token: 0x0400012E RID: 302
			btnPrevious = 577084905U,
			// Token: 0x0400012F RID: 303
			ExchangeServerRoles = 2900920565U,
			// Token: 0x04000130 RID: 304
			DownloadInstallationCompleted = 2716635429U,
			// Token: 0x04000131 RID: 305
			CheckForUpdateYesRadioButtonText = 1316856433U,
			// Token: 0x04000132 RID: 306
			SplashText = 3738111846U,
			// Token: 0x04000133 RID: 307
			Cancel = 2358390244U,
			// Token: 0x04000134 RID: 308
			Checking = 2358183302U,
			// Token: 0x04000135 RID: 309
			PreviousDownloadedUpdatesRadioButtonText = 766700124U,
			// Token: 0x04000136 RID: 310
			ResumeChoiceText = 2440240611U,
			// Token: 0x04000137 RID: 311
			UnifiedMessagingLanguagePacks = 1333650384U,
			// Token: 0x04000138 RID: 312
			CheckForUpdateYesNoLabelText = 3013797053U,
			// Token: 0x04000139 RID: 313
			DiskSpaceCapacityUnit = 3057699019U,
			// Token: 0x0400013A RID: 314
			btnNext = 816751771U,
			// Token: 0x0400013B RID: 315
			DownloadNoUpdatesFound = 1263419505U,
			// Token: 0x0400013C RID: 316
			CancelDownloadBtn = 2847757096U,
			// Token: 0x0400013D RID: 317
			Upgrade = 3608358242U,
			// Token: 0x0400013E RID: 318
			Install = 319275761U,
			// Token: 0x0400013F RID: 319
			Introduction = 2998280118U,
			// Token: 0x04000140 RID: 320
			NotFound = 3394043311U,
			// Token: 0x04000141 RID: 321
			btnRetry = 3641255076U,
			// Token: 0x04000142 RID: 322
			CheckForAvailableSpace = 509984218U,
			// Token: 0x04000143 RID: 323
			CancellingDownload = 2753928508U,
			// Token: 0x04000144 RID: 324
			FileCopyingError = 983858003U,
			// Token: 0x04000145 RID: 325
			BrowserLaunchError = 4184993237U,
			// Token: 0x04000146 RID: 326
			CopyFilesPageTitle = 2744422095U,
			// Token: 0x04000147 RID: 327
			VerifyingLangPackBundle = 3339504604U,
			// Token: 0x04000148 RID: 328
			SetupFailedPrintDocument = 3857751296U,
			// Token: 0x04000149 RID: 329
			DownloadCompleted = 4173214713U,
			// Token: 0x0400014A RID: 330
			btnCancel = 3836876260U,
			// Token: 0x0400014B RID: 331
			NotEnoughDiskSpace = 686568928U,
			// Token: 0x0400014C RID: 332
			UpdatesDownloadsPageTitle = 1865457820U,
			// Token: 0x0400014D RID: 333
			friendlyInvalidLanguagePackBundle = 17553301U,
			// Token: 0x0400014E RID: 334
			LanguagePacksUpToDate = 4081297114U,
			// Token: 0x0400014F RID: 335
			NoExchangeInstalled = 3898878242U,
			// Token: 0x04000150 RID: 336
			btnExit = 2254969718U,
			// Token: 0x04000151 RID: 337
			UnableToExtract = 1962719391U,
			// Token: 0x04000152 RID: 338
			DeletingTempFiles = 2475917809U,
			// Token: 0x04000153 RID: 339
			InsufficientDiskSpace = 1648760670U,
			// Token: 0x04000154 RID: 340
			CopyfileInstallText = 2041152115U,
			// Token: 0x04000155 RID: 341
			btnFinish = 4244802355U,
			// Token: 0x04000156 RID: 342
			ResumeChoiceTitle = 2295868152U,
			// Token: 0x04000157 RID: 343
			FileCopyDone = 2009416511U,
			// Token: 0x04000158 RID: 344
			Uninstall = 3779522776U,
			// Token: 0x04000159 RID: 345
			CheckForUpdateNoRadioButtonText = 2981091031U,
			// Token: 0x0400015A RID: 346
			NoText = 2463541198U,
			// Token: 0x0400015B RID: 347
			YesText = 3365230170U,
			// Token: 0x0400015C RID: 348
			ClosingHTTPConnection = 2225800973U,
			// Token: 0x0400015D RID: 349
			CancelFileCopying = 2909533909U,
			// Token: 0x0400015E RID: 350
			Failed = 1054423051U,
			// Token: 0x0400015F RID: 351
			AssemblyLoadError = 378004180U,
			// Token: 0x04000160 RID: 352
			DiskSpaceAllocationTitle = 2116473977U,
			// Token: 0x04000161 RID: 353
			CancelMessageBoxMessage = 2263082167U,
			// Token: 0x04000162 RID: 354
			AlreadyDownloadedRadio = 2487084378U,
			// Token: 0x04000163 RID: 355
			FileCopyingFinished = 1891569513U,
			// Token: 0x04000164 RID: 356
			VerifyInstallationCompleted = 72796564U,
			// Token: 0x04000165 RID: 357
			Warning = 1861340610U,
			// Token: 0x04000166 RID: 358
			SetupWizardCaption = 3004026682U,
			// Token: 0x04000167 RID: 359
			btnComplete = 3210618713U,
			// Token: 0x04000168 RID: 360
			friendlyWebException = 454692482U,
			// Token: 0x04000169 RID: 361
			RequiredDiskSpaceDescriptionText = 367349671U,
			// Token: 0x0400016A RID: 362
			CopyFilesText = 1912677597U,
			// Token: 0x0400016B RID: 363
			RegistryKeyForLanguagePackFound = 3074405412U,
			// Token: 0x0400016C RID: 364
			OkText = 2431595041U,
			// Token: 0x0400016D RID: 365
			InitializingSetupPageTitle = 3036876617U,
			// Token: 0x0400016E RID: 366
			Initializing = 273661265U,
			// Token: 0x0400016F RID: 367
			StartFileCopying = 2354229627U,
			// Token: 0x04000170 RID: 368
			Passed = 678271266U,
			// Token: 0x04000171 RID: 369
			MicrosoftExchangeServer = 1883156336U,
			// Token: 0x04000172 RID: 370
			AvailableDiskSpaceDescriptionText = 622196391U,
			// Token: 0x04000173 RID: 371
			IncompatibleBundle = 3065873541U,
			// Token: 0x04000174 RID: 372
			AttemptingToCleanTheRegistry = 1128547615U
		}

		// Token: 0x02000031 RID: 49
		private enum ParamIDs
		{
			// Token: 0x04000176 RID: 374
			InvalidLocalLPVersioningXML,
			// Token: 0x04000177 RID: 375
			RegularStatusMessage,
			// Token: 0x04000178 RID: 376
			StartEsmPath,
			// Token: 0x04000179 RID: 377
			MESI,
			// Token: 0x0400017A RID: 378
			CantFindSourceDir,
			// Token: 0x0400017B RID: 379
			DiskSpaceAvailable,
			// Token: 0x0400017C RID: 380
			NoFilesToCopy,
			// Token: 0x0400017D RID: 381
			SignatureVerificationFailed,
			// Token: 0x0400017E RID: 382
			OpenSetupFileLogError,
			// Token: 0x0400017F RID: 383
			CopyingDirectoryFromTo,
			// Token: 0x04000180 RID: 384
			MESU,
			// Token: 0x04000181 RID: 385
			BundleDoesntExist,
			// Token: 0x04000182 RID: 386
			MESICumulativeUpdate,
			// Token: 0x04000183 RID: 387
			FinishedWithError,
			// Token: 0x04000184 RID: 388
			AssemblyLoadFileNotFound,
			// Token: 0x04000185 RID: 389
			InvalidFile,
			// Token: 0x04000186 RID: 390
			DiskSpaceRequired,
			// Token: 0x04000187 RID: 391
			PageLoaded
		}
	}
}
