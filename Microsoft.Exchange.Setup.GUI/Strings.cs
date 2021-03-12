using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x02000017 RID: 23
	internal static class Strings
	{
		// Token: 0x060000F8 RID: 248 RVA: 0x0000B364 File Offset: 0x00009564
		static Strings()
		{
			Strings.stringIDs.Add(2116473977U, "DiskSpaceAllocationTitle");
			Strings.stringIDs.Add(3281626719U, "ExchangeOrganizationPageTitle");
			Strings.stringIDs.Add(3919765759U, "ExchangeOrganizationName");
			Strings.stringIDs.Add(846084549U, "AddRemoveServerRolePageTitle");
			Strings.stringIDs.Add(3060531531U, "ActiveDirectorySplitPermissions");
			Strings.stringIDs.Add(1689769030U, "HybridConfigurationEnterCredentialLabelText");
			Strings.stringIDs.Add(3411794799U, "LanguageBundleCannotRunInstall");
			Strings.stringIDs.Add(2014064605U, "SetupWillNotContinue");
			Strings.stringIDs.Add(4058204109U, "InvalidCredentials");
			Strings.stringIDs.Add(4099293073U, "ProtectionSettingsPageTitle");
			Strings.stringIDs.Add(3750498566U, "HybridConfigurationCredentialsFinished");
			Strings.stringIDs.Add(2235382510U, "MailboxRole");
			Strings.stringIDs.Add(479704857U, "NotAcceptEULAText");
			Strings.stringIDs.Add(1732413395U, "SetupCompletedPageEACText");
			Strings.stringIDs.Add(1256637832U, "NoEndUserLicenseAgreement");
			Strings.stringIDs.Add(3534072816U, "ReadMoreAboutUsageLinkText");
			Strings.stringIDs.Add(3606962380U, "AdminToolsRole");
			Strings.stringIDs.Add(3537994597U, "CannotRunWithoutParameter");
			Strings.stringIDs.Add(3619730788U, "AlreadyInstalled");
			Strings.stringIDs.Add(2079091583U, "ProtectionSettingsYesNoLabelText");
			Strings.stringIDs.Add(3057699019U, "DiskSpaceCapacityUnit");
			Strings.stringIDs.Add(4252207216U, "BrowseInstallationPathButtonText");
			Strings.stringIDs.Add(2549121878U, "UninstallWelcomeTitle");
			Strings.stringIDs.Add(1785456672U, "CannotRunInstalled");
			Strings.stringIDs.Add(2998280118U, "Introduction");
			Strings.stringIDs.Add(2886682374U, "DisableMalwareNoRadioButtonText");
			Strings.stringIDs.Add(1954427109U, "OpenExchangeAdminCenterCheckBoxText");
			Strings.stringIDs.Add(3767526541U, "SetupProgressPageTitle");
			Strings.stringIDs.Add(3093934206U, "PlanDeploymentLinkLabel2Link");
			Strings.stringIDs.Add(593134813U, "SetupCompletedPageTitle");
			Strings.stringIDs.Add(3055213040U, "PreCheckPageTitle");
			Strings.stringIDs.Add(1873628080U, "SetupCompletedPageText");
			Strings.stringIDs.Add(3109201548U, "AcceptEULAText");
			Strings.stringIDs.Add(894991490U, "PreCheckDescriptionText");
			Strings.stringIDs.Add(1883156336U, "MicrosoftExchangeServer");
			Strings.stringIDs.Add(4102307169U, "btnPrint");
			Strings.stringIDs.Add(1286725797U, "InstallationPathTitle");
			Strings.stringIDs.Add(3088556892U, "HybridConfigurationEnterCredentialForAccountLabelText");
			Strings.stringIDs.Add(1923571563U, "EULAPageText");
			Strings.stringIDs.Add(1608263701U, "UseRecommendedSettingsDescription");
			Strings.stringIDs.Add(574235583U, "RoleSelectionPageTitle");
			Strings.stringIDs.Add(544429720U, "PlanDeploymentLabel");
			Strings.stringIDs.Add(3708323460U, "DisableMalwareYesRadioButtonText");
			Strings.stringIDs.Add(516668141U, "PlanDeploymentLinkLabel1Link");
			Strings.stringIDs.Add(1290564665U, "InstallWindowsPrereqCheckBoxText");
			Strings.stringIDs.Add(1269906618U, "DoNotUseSettingsRadioButtonText");
			Strings.stringIDs.Add(3706612651U, "ExchangeOrganizationNameError");
			Strings.stringIDs.Add(2419732849U, "UninstallPageTitle");
			Strings.stringIDs.Add(4050885606U, "FatalError");
			Strings.stringIDs.Add(1499387377U, "ClientAccessRole");
			Strings.stringIDs.Add(257944843U, "RoleSelectionLabelText");
			Strings.stringIDs.Add(1180534834U, "InvalidInstallationLocation");
			Strings.stringIDs.Add(367349671U, "RequiredDiskSpaceDescriptionText");
			Strings.stringIDs.Add(614788730U, "PartiallyConfiguredCannotRunUnInstall");
			Strings.stringIDs.Add(1319431375U, "PlanDeploymentLinkLabel2Text");
			Strings.stringIDs.Add(3712487120U, "SetupCompleted");
			Strings.stringIDs.Add(2106877831U, "PreCheckFail");
			Strings.stringIDs.Add(2352415574U, "DescriptionTitle");
			Strings.stringIDs.Add(3771677870U, "PrereqNoteText");
			Strings.stringIDs.Add(3256873468U, "EulaLabelText");
			Strings.stringIDs.Add(3418450723U, "HybridConfigurationSystemExceptionText");
			Strings.stringIDs.Add(1319299077U, "FolderBrowserDialogDescriptionText");
			Strings.stringIDs.Add(267801009U, "HybridConfigurationCredentialsChecks");
			Strings.stringIDs.Add(4176057518U, "NotUseRecommendedSettingsDescription");
			Strings.stringIDs.Add(2263082167U, "CancelMessageBoxMessage");
			Strings.stringIDs.Add(1634809916U, "UseSettingsRadioButtonText");
			Strings.stringIDs.Add(4109048229U, "ActiveDirectorySplitPermissionsDescription");
			Strings.stringIDs.Add(3379112507U, "IncompleteInstallationDetectedPageTitle");
			Strings.stringIDs.Add(2442879412U, "PasswordLabelText");
			Strings.stringIDs.Add(163021091U, "PlanDeploymentLinkLabel3Link");
			Strings.stringIDs.Add(3553989528U, "PreCheckSuccess");
			Strings.stringIDs.Add(4171294242U, "UninstallWelcomeDiscription");
			Strings.stringIDs.Add(1850975354U, "HybridConfigurationCredentialPageTitle");
			Strings.stringIDs.Add(1938529548U, "SetupFailedPrintEULA");
			Strings.stringIDs.Add(4101971563U, "HybridConfigurationCredentialsFailed");
			Strings.stringIDs.Add(2026922639U, "IncompleteInstallationDetectedSummaryLabelText");
			Strings.stringIDs.Add(396014875U, "HybridConfigurationStatusPageTitle");
			Strings.stringIDs.Add(851625806U, "RecommendedSettingsTitle");
			Strings.stringIDs.Add(3846284970U, "PlanDeploymentLinkLabel3Text");
			Strings.stringIDs.Add(1612790765U, "ProtectionSettingsLabelText");
			Strings.stringIDs.Add(3004026682U, "SetupWizardCaption");
			Strings.stringIDs.Add(2339675629U, "UserNameLabelText");
			Strings.stringIDs.Add(1635017634U, "ReadMoreAboutCheckingForErrorSolutionsLinkText");
			Strings.stringIDs.Add(2230563552U, "PlanDeploymentLinkLabel1Text");
			Strings.stringIDs.Add(622196391U, "AvailableDiskSpaceDescriptionText");
			Strings.stringIDs.Add(307954797U, "InstallationSpaceAndLocationPageTitle");
			Strings.stringIDs.Add(2343429092U, "SetupCompletedPageLinkText");
			Strings.stringIDs.Add(3664928351U, "EdgeRole");
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000BA80 File Offset: 0x00009C80
		public static LocalizedString DiskSpaceAllocationTitle
		{
			get
			{
				return new LocalizedString("DiskSpaceAllocationTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000FA RID: 250 RVA: 0x0000BA97 File Offset: 0x00009C97
		public static LocalizedString ExchangeOrganizationPageTitle
		{
			get
			{
				return new LocalizedString("ExchangeOrganizationPageTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000BAAE File Offset: 0x00009CAE
		public static LocalizedString ExchangeOrganizationName
		{
			get
			{
				return new LocalizedString("ExchangeOrganizationName", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000BAC5 File Offset: 0x00009CC5
		public static LocalizedString AddRemoveServerRolePageTitle
		{
			get
			{
				return new LocalizedString("AddRemoveServerRolePageTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000BADC File Offset: 0x00009CDC
		public static LocalizedString ActiveDirectorySplitPermissions
		{
			get
			{
				return new LocalizedString("ActiveDirectorySplitPermissions", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000FE RID: 254 RVA: 0x0000BAF3 File Offset: 0x00009CF3
		public static LocalizedString HybridConfigurationEnterCredentialLabelText
		{
			get
			{
				return new LocalizedString("HybridConfigurationEnterCredentialLabelText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000BB0A File Offset: 0x00009D0A
		public static LocalizedString LanguageBundleCannotRunInstall
		{
			get
			{
				return new LocalizedString("LanguageBundleCannotRunInstall", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000100 RID: 256 RVA: 0x0000BB21 File Offset: 0x00009D21
		public static LocalizedString SetupWillNotContinue
		{
			get
			{
				return new LocalizedString("SetupWillNotContinue", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000BB38 File Offset: 0x00009D38
		public static LocalizedString InvalidCredentials
		{
			get
			{
				return new LocalizedString("InvalidCredentials", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000BB4F File Offset: 0x00009D4F
		public static LocalizedString ProtectionSettingsPageTitle
		{
			get
			{
				return new LocalizedString("ProtectionSettingsPageTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000BB68 File Offset: 0x00009D68
		public static LocalizedString UnifiedMessagingInstallSummaryText(string culture)
		{
			return new LocalizedString("UnifiedMessagingInstallSummaryText", Strings.ResourceManager, new object[]
			{
				culture
			});
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000104 RID: 260 RVA: 0x0000BB90 File Offset: 0x00009D90
		public static LocalizedString HybridConfigurationCredentialsFinished
		{
			get
			{
				return new LocalizedString("HybridConfigurationCredentialsFinished", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000BBA7 File Offset: 0x00009DA7
		public static LocalizedString MailboxRole
		{
			get
			{
				return new LocalizedString("MailboxRole", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000106 RID: 262 RVA: 0x0000BBBE File Offset: 0x00009DBE
		public static LocalizedString NotAcceptEULAText
		{
			get
			{
				return new LocalizedString("NotAcceptEULAText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000BBD5 File Offset: 0x00009DD5
		public static LocalizedString SetupCompletedPageEACText
		{
			get
			{
				return new LocalizedString("SetupCompletedPageEACText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000108 RID: 264 RVA: 0x0000BBEC File Offset: 0x00009DEC
		public static LocalizedString NoEndUserLicenseAgreement
		{
			get
			{
				return new LocalizedString("NoEndUserLicenseAgreement", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000BC03 File Offset: 0x00009E03
		public static LocalizedString ReadMoreAboutUsageLinkText
		{
			get
			{
				return new LocalizedString("ReadMoreAboutUsageLinkText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000BC1A File Offset: 0x00009E1A
		public static LocalizedString AdminToolsRole
		{
			get
			{
				return new LocalizedString("AdminToolsRole", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000BC31 File Offset: 0x00009E31
		public static LocalizedString CannotRunWithoutParameter
		{
			get
			{
				return new LocalizedString("CannotRunWithoutParameter", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000BC48 File Offset: 0x00009E48
		public static LocalizedString AlreadyInstalled
		{
			get
			{
				return new LocalizedString("AlreadyInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000BC5F File Offset: 0x00009E5F
		public static LocalizedString ProtectionSettingsYesNoLabelText
		{
			get
			{
				return new LocalizedString("ProtectionSettingsYesNoLabelText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600010E RID: 270 RVA: 0x0000BC76 File Offset: 0x00009E76
		public static LocalizedString DiskSpaceCapacityUnit
		{
			get
			{
				return new LocalizedString("DiskSpaceCapacityUnit", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000BC8D File Offset: 0x00009E8D
		public static LocalizedString BrowseInstallationPathButtonText
		{
			get
			{
				return new LocalizedString("BrowseInstallationPathButtonText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000110 RID: 272 RVA: 0x0000BCA4 File Offset: 0x00009EA4
		public static LocalizedString UninstallWelcomeTitle
		{
			get
			{
				return new LocalizedString("UninstallWelcomeTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000BCBB File Offset: 0x00009EBB
		public static LocalizedString CannotRunInstalled
		{
			get
			{
				return new LocalizedString("CannotRunInstalled", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000BCD2 File Offset: 0x00009ED2
		public static LocalizedString Introduction
		{
			get
			{
				return new LocalizedString("Introduction", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000113 RID: 275 RVA: 0x0000BCE9 File Offset: 0x00009EE9
		public static LocalizedString DisableMalwareNoRadioButtonText
		{
			get
			{
				return new LocalizedString("DisableMalwareNoRadioButtonText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000BD00 File Offset: 0x00009F00
		public static LocalizedString OpenExchangeAdminCenterCheckBoxText
		{
			get
			{
				return new LocalizedString("OpenExchangeAdminCenterCheckBoxText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000BD17 File Offset: 0x00009F17
		public static LocalizedString SetupProgressPageTitle
		{
			get
			{
				return new LocalizedString("SetupProgressPageTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000BD2E File Offset: 0x00009F2E
		public static LocalizedString PlanDeploymentLinkLabel2Link
		{
			get
			{
				return new LocalizedString("PlanDeploymentLinkLabel2Link", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000BD45 File Offset: 0x00009F45
		public static LocalizedString SetupCompletedPageTitle
		{
			get
			{
				return new LocalizedString("SetupCompletedPageTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000BD5C File Offset: 0x00009F5C
		public static LocalizedString PreCheckPageTitle
		{
			get
			{
				return new LocalizedString("PreCheckPageTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000119 RID: 281 RVA: 0x0000BD73 File Offset: 0x00009F73
		public static LocalizedString SetupCompletedPageText
		{
			get
			{
				return new LocalizedString("SetupCompletedPageText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000BD8A File Offset: 0x00009F8A
		public static LocalizedString AcceptEULAText
		{
			get
			{
				return new LocalizedString("AcceptEULAText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000BDA1 File Offset: 0x00009FA1
		public static LocalizedString PreCheckDescriptionText
		{
			get
			{
				return new LocalizedString("PreCheckDescriptionText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		public static LocalizedString MicrosoftExchangeServer
		{
			get
			{
				return new LocalizedString("MicrosoftExchangeServer", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000BDCF File Offset: 0x00009FCF
		public static LocalizedString btnPrint
		{
			get
			{
				return new LocalizedString("btnPrint", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000BDE6 File Offset: 0x00009FE6
		public static LocalizedString InstallationPathTitle
		{
			get
			{
				return new LocalizedString("InstallationPathTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600011F RID: 287 RVA: 0x0000BDFD File Offset: 0x00009FFD
		public static LocalizedString HybridConfigurationEnterCredentialForAccountLabelText
		{
			get
			{
				return new LocalizedString("HybridConfigurationEnterCredentialForAccountLabelText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000BE14 File Offset: 0x0000A014
		public static LocalizedString EULAPageText
		{
			get
			{
				return new LocalizedString("EULAPageText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000BE2C File Offset: 0x0000A02C
		public static LocalizedString CurrentStep(string currentStep, string totalSteps, string currentTask)
		{
			return new LocalizedString("CurrentStep", Strings.ResourceManager, new object[]
			{
				currentStep,
				totalSteps,
				currentTask
			});
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000BE5C File Offset: 0x0000A05C
		public static LocalizedString UseRecommendedSettingsDescription
		{
			get
			{
				return new LocalizedString("UseRecommendedSettingsDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000BE73 File Offset: 0x0000A073
		public static LocalizedString RoleSelectionPageTitle
		{
			get
			{
				return new LocalizedString("RoleSelectionPageTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000BE8A File Offset: 0x0000A08A
		public static LocalizedString PlanDeploymentLabel
		{
			get
			{
				return new LocalizedString("PlanDeploymentLabel", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000BEA1 File Offset: 0x0000A0A1
		public static LocalizedString DisableMalwareYesRadioButtonText
		{
			get
			{
				return new LocalizedString("DisableMalwareYesRadioButtonText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		public static LocalizedString PlanDeploymentLinkLabel1Link
		{
			get
			{
				return new LocalizedString("PlanDeploymentLinkLabel1Link", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000BECF File Offset: 0x0000A0CF
		public static LocalizedString InstallWindowsPrereqCheckBoxText
		{
			get
			{
				return new LocalizedString("InstallWindowsPrereqCheckBoxText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000BEE6 File Offset: 0x0000A0E6
		public static LocalizedString DoNotUseSettingsRadioButtonText
		{
			get
			{
				return new LocalizedString("DoNotUseSettingsRadioButtonText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000BEFD File Offset: 0x0000A0FD
		public static LocalizedString ExchangeOrganizationNameError
		{
			get
			{
				return new LocalizedString("ExchangeOrganizationNameError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000BF14 File Offset: 0x0000A114
		public static LocalizedString UninstallPageTitle
		{
			get
			{
				return new LocalizedString("UninstallPageTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000BF2B File Offset: 0x0000A12B
		public static LocalizedString FatalError
		{
			get
			{
				return new LocalizedString("FatalError", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000BF42 File Offset: 0x0000A142
		public static LocalizedString ClientAccessRole
		{
			get
			{
				return new LocalizedString("ClientAccessRole", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000BF59 File Offset: 0x0000A159
		public static LocalizedString RoleSelectionLabelText
		{
			get
			{
				return new LocalizedString("RoleSelectionLabelText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000BF70 File Offset: 0x0000A170
		public static LocalizedString InvalidInstallationLocation
		{
			get
			{
				return new LocalizedString("InvalidInstallationLocation", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600012F RID: 303 RVA: 0x0000BF87 File Offset: 0x0000A187
		public static LocalizedString RequiredDiskSpaceDescriptionText
		{
			get
			{
				return new LocalizedString("RequiredDiskSpaceDescriptionText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000BF9E File Offset: 0x0000A19E
		public static LocalizedString PartiallyConfiguredCannotRunUnInstall
		{
			get
			{
				return new LocalizedString("PartiallyConfiguredCannotRunUnInstall", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000131 RID: 305 RVA: 0x0000BFB5 File Offset: 0x0000A1B5
		public static LocalizedString PlanDeploymentLinkLabel2Text
		{
			get
			{
				return new LocalizedString("PlanDeploymentLinkLabel2Text", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000BFCC File Offset: 0x0000A1CC
		public static LocalizedString SetupCompleted
		{
			get
			{
				return new LocalizedString("SetupCompleted", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000133 RID: 307 RVA: 0x0000BFE3 File Offset: 0x0000A1E3
		public static LocalizedString PreCheckFail
		{
			get
			{
				return new LocalizedString("PreCheckFail", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000134 RID: 308 RVA: 0x0000BFFA File Offset: 0x0000A1FA
		public static LocalizedString DescriptionTitle
		{
			get
			{
				return new LocalizedString("DescriptionTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000135 RID: 309 RVA: 0x0000C011 File Offset: 0x0000A211
		public static LocalizedString PrereqNoteText
		{
			get
			{
				return new LocalizedString("PrereqNoteText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000136 RID: 310 RVA: 0x0000C028 File Offset: 0x0000A228
		public static LocalizedString EulaLabelText
		{
			get
			{
				return new LocalizedString("EulaLabelText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000137 RID: 311 RVA: 0x0000C03F File Offset: 0x0000A23F
		public static LocalizedString HybridConfigurationSystemExceptionText
		{
			get
			{
				return new LocalizedString("HybridConfigurationSystemExceptionText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000138 RID: 312 RVA: 0x0000C056 File Offset: 0x0000A256
		public static LocalizedString FolderBrowserDialogDescriptionText
		{
			get
			{
				return new LocalizedString("FolderBrowserDialogDescriptionText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000C070 File Offset: 0x0000A270
		public static LocalizedString UnifiedMessagingCannotRunInstall(string culture)
		{
			return new LocalizedString("UnifiedMessagingCannotRunInstall", Strings.ResourceManager, new object[]
			{
				culture
			});
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600013A RID: 314 RVA: 0x0000C098 File Offset: 0x0000A298
		public static LocalizedString HybridConfigurationCredentialsChecks
		{
			get
			{
				return new LocalizedString("HybridConfigurationCredentialsChecks", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000C0AF File Offset: 0x0000A2AF
		public static LocalizedString NotUseRecommendedSettingsDescription
		{
			get
			{
				return new LocalizedString("NotUseRecommendedSettingsDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600013C RID: 316 RVA: 0x0000C0C6 File Offset: 0x0000A2C6
		public static LocalizedString CancelMessageBoxMessage
		{
			get
			{
				return new LocalizedString("CancelMessageBoxMessage", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000C0DD File Offset: 0x0000A2DD
		public static LocalizedString UseSettingsRadioButtonText
		{
			get
			{
				return new LocalizedString("UseSettingsRadioButtonText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000C0F4 File Offset: 0x0000A2F4
		public static LocalizedString ActiveDirectorySplitPermissionsDescription
		{
			get
			{
				return new LocalizedString("ActiveDirectorySplitPermissionsDescription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000C10B File Offset: 0x0000A30B
		public static LocalizedString IncompleteInstallationDetectedPageTitle
		{
			get
			{
				return new LocalizedString("IncompleteInstallationDetectedPageTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000C122 File Offset: 0x0000A322
		public static LocalizedString PasswordLabelText
		{
			get
			{
				return new LocalizedString("PasswordLabelText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000C139 File Offset: 0x0000A339
		public static LocalizedString PlanDeploymentLinkLabel3Link
		{
			get
			{
				return new LocalizedString("PlanDeploymentLinkLabel3Link", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000142 RID: 322 RVA: 0x0000C150 File Offset: 0x0000A350
		public static LocalizedString PreCheckSuccess
		{
			get
			{
				return new LocalizedString("PreCheckSuccess", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000C167 File Offset: 0x0000A367
		public static LocalizedString UninstallWelcomeDiscription
		{
			get
			{
				return new LocalizedString("UninstallWelcomeDiscription", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000144 RID: 324 RVA: 0x0000C17E File Offset: 0x0000A37E
		public static LocalizedString HybridConfigurationCredentialPageTitle
		{
			get
			{
				return new LocalizedString("HybridConfigurationCredentialPageTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000C195 File Offset: 0x0000A395
		public static LocalizedString SetupFailedPrintEULA
		{
			get
			{
				return new LocalizedString("SetupFailedPrintEULA", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000C1AC File Offset: 0x0000A3AC
		public static LocalizedString HybridConfigurationCredentialsFailed
		{
			get
			{
				return new LocalizedString("HybridConfigurationCredentialsFailed", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000C1C3 File Offset: 0x0000A3C3
		public static LocalizedString IncompleteInstallationDetectedSummaryLabelText
		{
			get
			{
				return new LocalizedString("IncompleteInstallationDetectedSummaryLabelText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000C1DA File Offset: 0x0000A3DA
		public static LocalizedString HybridConfigurationStatusPageTitle
		{
			get
			{
				return new LocalizedString("HybridConfigurationStatusPageTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000C1F1 File Offset: 0x0000A3F1
		public static LocalizedString RecommendedSettingsTitle
		{
			get
			{
				return new LocalizedString("RecommendedSettingsTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000C208 File Offset: 0x0000A408
		public static LocalizedString PlanDeploymentLinkLabel3Text
		{
			get
			{
				return new LocalizedString("PlanDeploymentLinkLabel3Text", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600014B RID: 331 RVA: 0x0000C21F File Offset: 0x0000A41F
		public static LocalizedString ProtectionSettingsLabelText
		{
			get
			{
				return new LocalizedString("ProtectionSettingsLabelText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600014C RID: 332 RVA: 0x0000C236 File Offset: 0x0000A436
		public static LocalizedString SetupWizardCaption
		{
			get
			{
				return new LocalizedString("SetupWizardCaption", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000C24D File Offset: 0x0000A44D
		public static LocalizedString UserNameLabelText
		{
			get
			{
				return new LocalizedString("UserNameLabelText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600014E RID: 334 RVA: 0x0000C264 File Offset: 0x0000A464
		public static LocalizedString ReadMoreAboutCheckingForErrorSolutionsLinkText
		{
			get
			{
				return new LocalizedString("ReadMoreAboutCheckingForErrorSolutionsLinkText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000C27B File Offset: 0x0000A47B
		public static LocalizedString PlanDeploymentLinkLabel1Text
		{
			get
			{
				return new LocalizedString("PlanDeploymentLinkLabel1Text", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000C294 File Offset: 0x0000A494
		public static LocalizedString PageLoaded(string pageName)
		{
			return new LocalizedString("PageLoaded", Strings.ResourceManager, new object[]
			{
				pageName
			});
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		public static LocalizedString AvailableDiskSpaceDescriptionText
		{
			get
			{
				return new LocalizedString("AvailableDiskSpaceDescriptionText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000C2D4 File Offset: 0x0000A4D4
		public static LocalizedString SetupNotFoundInSourceDirError(string fileName)
		{
			return new LocalizedString("SetupNotFoundInSourceDirError", Strings.ResourceManager, new object[]
			{
				fileName
			});
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000C2FC File Offset: 0x0000A4FC
		public static LocalizedString InstallationSpaceAndLocationPageTitle
		{
			get
			{
				return new LocalizedString("InstallationSpaceAndLocationPageTitle", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000C313 File Offset: 0x0000A513
		public static LocalizedString SetupCompletedPageLinkText
		{
			get
			{
				return new LocalizedString("SetupCompletedPageLinkText", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000C32A File Offset: 0x0000A52A
		public static LocalizedString EdgeRole
		{
			get
			{
				return new LocalizedString("EdgeRole", Strings.ResourceManager, new object[0]);
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000C341 File Offset: 0x0000A541
		public static LocalizedString GetLocalizedString(Strings.IDs key)
		{
			return new LocalizedString(Strings.stringIDs[(uint)key], Strings.ResourceManager, new object[0]);
		}

		// Token: 0x0400009F RID: 159
		private static Dictionary<uint, string> stringIDs = new Dictionary<uint, string>(88);

		// Token: 0x040000A0 RID: 160
		private static ExchangeResourceManager ResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Setup.GUI.Strings", typeof(Strings).GetTypeInfo().Assembly);

		// Token: 0x02000018 RID: 24
		public enum IDs : uint
		{
			// Token: 0x040000A2 RID: 162
			DiskSpaceAllocationTitle = 2116473977U,
			// Token: 0x040000A3 RID: 163
			ExchangeOrganizationPageTitle = 3281626719U,
			// Token: 0x040000A4 RID: 164
			ExchangeOrganizationName = 3919765759U,
			// Token: 0x040000A5 RID: 165
			AddRemoveServerRolePageTitle = 846084549U,
			// Token: 0x040000A6 RID: 166
			ActiveDirectorySplitPermissions = 3060531531U,
			// Token: 0x040000A7 RID: 167
			HybridConfigurationEnterCredentialLabelText = 1689769030U,
			// Token: 0x040000A8 RID: 168
			LanguageBundleCannotRunInstall = 3411794799U,
			// Token: 0x040000A9 RID: 169
			SetupWillNotContinue = 2014064605U,
			// Token: 0x040000AA RID: 170
			InvalidCredentials = 4058204109U,
			// Token: 0x040000AB RID: 171
			ProtectionSettingsPageTitle = 4099293073U,
			// Token: 0x040000AC RID: 172
			HybridConfigurationCredentialsFinished = 3750498566U,
			// Token: 0x040000AD RID: 173
			MailboxRole = 2235382510U,
			// Token: 0x040000AE RID: 174
			NotAcceptEULAText = 479704857U,
			// Token: 0x040000AF RID: 175
			SetupCompletedPageEACText = 1732413395U,
			// Token: 0x040000B0 RID: 176
			NoEndUserLicenseAgreement = 1256637832U,
			// Token: 0x040000B1 RID: 177
			ReadMoreAboutUsageLinkText = 3534072816U,
			// Token: 0x040000B2 RID: 178
			AdminToolsRole = 3606962380U,
			// Token: 0x040000B3 RID: 179
			CannotRunWithoutParameter = 3537994597U,
			// Token: 0x040000B4 RID: 180
			AlreadyInstalled = 3619730788U,
			// Token: 0x040000B5 RID: 181
			ProtectionSettingsYesNoLabelText = 2079091583U,
			// Token: 0x040000B6 RID: 182
			DiskSpaceCapacityUnit = 3057699019U,
			// Token: 0x040000B7 RID: 183
			BrowseInstallationPathButtonText = 4252207216U,
			// Token: 0x040000B8 RID: 184
			UninstallWelcomeTitle = 2549121878U,
			// Token: 0x040000B9 RID: 185
			CannotRunInstalled = 1785456672U,
			// Token: 0x040000BA RID: 186
			Introduction = 2998280118U,
			// Token: 0x040000BB RID: 187
			DisableMalwareNoRadioButtonText = 2886682374U,
			// Token: 0x040000BC RID: 188
			OpenExchangeAdminCenterCheckBoxText = 1954427109U,
			// Token: 0x040000BD RID: 189
			SetupProgressPageTitle = 3767526541U,
			// Token: 0x040000BE RID: 190
			PlanDeploymentLinkLabel2Link = 3093934206U,
			// Token: 0x040000BF RID: 191
			SetupCompletedPageTitle = 593134813U,
			// Token: 0x040000C0 RID: 192
			PreCheckPageTitle = 3055213040U,
			// Token: 0x040000C1 RID: 193
			SetupCompletedPageText = 1873628080U,
			// Token: 0x040000C2 RID: 194
			AcceptEULAText = 3109201548U,
			// Token: 0x040000C3 RID: 195
			PreCheckDescriptionText = 894991490U,
			// Token: 0x040000C4 RID: 196
			MicrosoftExchangeServer = 1883156336U,
			// Token: 0x040000C5 RID: 197
			btnPrint = 4102307169U,
			// Token: 0x040000C6 RID: 198
			InstallationPathTitle = 1286725797U,
			// Token: 0x040000C7 RID: 199
			HybridConfigurationEnterCredentialForAccountLabelText = 3088556892U,
			// Token: 0x040000C8 RID: 200
			EULAPageText = 1923571563U,
			// Token: 0x040000C9 RID: 201
			UseRecommendedSettingsDescription = 1608263701U,
			// Token: 0x040000CA RID: 202
			RoleSelectionPageTitle = 574235583U,
			// Token: 0x040000CB RID: 203
			PlanDeploymentLabel = 544429720U,
			// Token: 0x040000CC RID: 204
			DisableMalwareYesRadioButtonText = 3708323460U,
			// Token: 0x040000CD RID: 205
			PlanDeploymentLinkLabel1Link = 516668141U,
			// Token: 0x040000CE RID: 206
			InstallWindowsPrereqCheckBoxText = 1290564665U,
			// Token: 0x040000CF RID: 207
			DoNotUseSettingsRadioButtonText = 1269906618U,
			// Token: 0x040000D0 RID: 208
			ExchangeOrganizationNameError = 3706612651U,
			// Token: 0x040000D1 RID: 209
			UninstallPageTitle = 2419732849U,
			// Token: 0x040000D2 RID: 210
			FatalError = 4050885606U,
			// Token: 0x040000D3 RID: 211
			ClientAccessRole = 1499387377U,
			// Token: 0x040000D4 RID: 212
			RoleSelectionLabelText = 257944843U,
			// Token: 0x040000D5 RID: 213
			InvalidInstallationLocation = 1180534834U,
			// Token: 0x040000D6 RID: 214
			RequiredDiskSpaceDescriptionText = 367349671U,
			// Token: 0x040000D7 RID: 215
			PartiallyConfiguredCannotRunUnInstall = 614788730U,
			// Token: 0x040000D8 RID: 216
			PlanDeploymentLinkLabel2Text = 1319431375U,
			// Token: 0x040000D9 RID: 217
			SetupCompleted = 3712487120U,
			// Token: 0x040000DA RID: 218
			PreCheckFail = 2106877831U,
			// Token: 0x040000DB RID: 219
			DescriptionTitle = 2352415574U,
			// Token: 0x040000DC RID: 220
			PrereqNoteText = 3771677870U,
			// Token: 0x040000DD RID: 221
			EulaLabelText = 3256873468U,
			// Token: 0x040000DE RID: 222
			HybridConfigurationSystemExceptionText = 3418450723U,
			// Token: 0x040000DF RID: 223
			FolderBrowserDialogDescriptionText = 1319299077U,
			// Token: 0x040000E0 RID: 224
			HybridConfigurationCredentialsChecks = 267801009U,
			// Token: 0x040000E1 RID: 225
			NotUseRecommendedSettingsDescription = 4176057518U,
			// Token: 0x040000E2 RID: 226
			CancelMessageBoxMessage = 2263082167U,
			// Token: 0x040000E3 RID: 227
			UseSettingsRadioButtonText = 1634809916U,
			// Token: 0x040000E4 RID: 228
			ActiveDirectorySplitPermissionsDescription = 4109048229U,
			// Token: 0x040000E5 RID: 229
			IncompleteInstallationDetectedPageTitle = 3379112507U,
			// Token: 0x040000E6 RID: 230
			PasswordLabelText = 2442879412U,
			// Token: 0x040000E7 RID: 231
			PlanDeploymentLinkLabel3Link = 163021091U,
			// Token: 0x040000E8 RID: 232
			PreCheckSuccess = 3553989528U,
			// Token: 0x040000E9 RID: 233
			UninstallWelcomeDiscription = 4171294242U,
			// Token: 0x040000EA RID: 234
			HybridConfigurationCredentialPageTitle = 1850975354U,
			// Token: 0x040000EB RID: 235
			SetupFailedPrintEULA = 1938529548U,
			// Token: 0x040000EC RID: 236
			HybridConfigurationCredentialsFailed = 4101971563U,
			// Token: 0x040000ED RID: 237
			IncompleteInstallationDetectedSummaryLabelText = 2026922639U,
			// Token: 0x040000EE RID: 238
			HybridConfigurationStatusPageTitle = 396014875U,
			// Token: 0x040000EF RID: 239
			RecommendedSettingsTitle = 851625806U,
			// Token: 0x040000F0 RID: 240
			PlanDeploymentLinkLabel3Text = 3846284970U,
			// Token: 0x040000F1 RID: 241
			ProtectionSettingsLabelText = 1612790765U,
			// Token: 0x040000F2 RID: 242
			SetupWizardCaption = 3004026682U,
			// Token: 0x040000F3 RID: 243
			UserNameLabelText = 2339675629U,
			// Token: 0x040000F4 RID: 244
			ReadMoreAboutCheckingForErrorSolutionsLinkText = 1635017634U,
			// Token: 0x040000F5 RID: 245
			PlanDeploymentLinkLabel1Text = 2230563552U,
			// Token: 0x040000F6 RID: 246
			AvailableDiskSpaceDescriptionText = 622196391U,
			// Token: 0x040000F7 RID: 247
			InstallationSpaceAndLocationPageTitle = 307954797U,
			// Token: 0x040000F8 RID: 248
			SetupCompletedPageLinkText = 2343429092U,
			// Token: 0x040000F9 RID: 249
			EdgeRole = 3664928351U
		}

		// Token: 0x02000019 RID: 25
		private enum ParamIDs
		{
			// Token: 0x040000FB RID: 251
			UnifiedMessagingInstallSummaryText,
			// Token: 0x040000FC RID: 252
			CurrentStep,
			// Token: 0x040000FD RID: 253
			UnifiedMessagingCannotRunInstall,
			// Token: 0x040000FE RID: 254
			PageLoaded,
			// Token: 0x040000FF RID: 255
			SetupNotFoundInSourceDirError
		}
	}
}
