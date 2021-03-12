using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001D5 RID: 469
	public class ErrorPageContents
	{
		// Token: 0x0600256C RID: 9580 RVA: 0x00072D14 File Offset: 0x00070F14
		static ErrorPageContents()
		{
			ErrorPageContents.contents["unexpected"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.ErrorPage, Strings.ErrorPageMessage, HttpStatusCode.InternalServerError, "{219108EE-434F-4fe1-BBD0-1394ECC1F9D0}");
			ErrorPageContents.contents["noroles"] = new ErrorPageContents(Strings.AccessDeniedPageTitle, Strings.AccessDenied, Strings.AccessDeniedMessage, HttpStatusCode.Forbidden, "{0625DA3C-35EC-4027-8905-B924F3B8764B}");
			ErrorPageContents.contents["nonmailbox"] = new ErrorPageContents(Strings.NoneUserMailboxTryLogonEcpPageTitle, Strings.NoneUserMailboxTryLogonEcp, Strings.NoneUserMailboxTryLogonEcpMessage, HttpStatusCode.Forbidden, "{FEE4FCBB-25E7-4e14-95CA-015FE48F44F1}");
			ErrorPageContents.contents["lowversion"] = new ErrorPageContents(Strings.LowVersionUserDeniedPageTitle, Strings.LowVersionUserDenied, Strings.LowVersionUserDeniedMessage, HttpStatusCode.Forbidden, "{C7B61A13-1F89-4511-B103-69B9CD9F72FC}");
			ErrorPageContents.contents["noscripts"] = new ErrorPageContents(Strings.AccessDeniedPageTitle, Strings.ScriptingDisabled, Strings.ScriptingDisabledMessage, HttpStatusCode.PreconditionFailed, "{F4B5916E-2918-4ccf-BA70-BDA87086AC67}");
			ErrorPageContents.contents["nocookies"] = new ErrorPageContents(Strings.AccessDeniedPageTitle, Strings.CookiesDisabled, Strings.CookiesDisabledMessage, HttpStatusCode.PreconditionFailed, "{6741ACE8-BCD8-44f0-A347-F83FF2AF7BAB}");
			ErrorPageContents.contents["transientserviceerror"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.ServiceDown, new LocalizedString(Strings.TransientServiceErrorMessage), HttpStatusCode.InternalServerError, 100, "{BAECCE0F-A1F7-4396-B834-FA3F63A06DFB}");
			ErrorPageContents.contents["badrequest"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.BadRequestTitle, Strings.BadRequestMessage, HttpStatusCode.BadRequest, "{DBF4E0B7-A1A5-4612-8DE0-13765ACC1C32}");
			ErrorPageContents.contents["badqueryparameter"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.BadQueryParameterTitle, Strings.BadQueryParameterMessage, HttpStatusCode.BadRequest, "{AA3FDA36-21E8-42C4-A0F2-97AF7B8EDC5D}");
			ErrorPageContents.contents["urlnotfound"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.UrlNotFound, Strings.UrlNotFoundMessage, HttpStatusCode.NotFound, "{F20FE81B-89F2-4ef1-8FDC-8F522EDBD9F9}");
			ErrorPageContents.contents["urlnotfoundornoaccess"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.AccessDenied, Strings.UrlNotFoundOrNoAccessMessage, HttpStatusCode.Forbidden, "{10D85825-CE48-47f9-9DD2-34DE9992DB8E}");
			ErrorPageContents.contents["browsernotsupported"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.BrowserNotSupported, Strings.BrowserNotSupportedMessage, HttpStatusCode.PreconditionFailed, "{AEA05820-5619-4f79-A5A1-69B43AF6FAEC}");
			ErrorPageContents.contents["owalogin"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.UrlNotFound, Strings.OwaFirstLoginMessage, HttpStatusCode.PreconditionFailed, "{D3A51030-A690-4b8a-98A9-705924DFA0AB}");
			ErrorPageContents.contents["owapremium"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.OwaUnsupportedFeature, Strings.OwaPremiumMessage, HttpStatusCode.PreconditionFailed, "{F9246DFC-11C2-4e53-AB25-496D5FB84AAF}");
			ErrorPageContents.contents["overbudget"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.OverBudgetTitle, Strings.OverBudgetExceptionTranslation, HttpStatusCode.ServiceUnavailable, 2, "{E3D04400-2050-49b1-A85B-775581BC35F9}");
			ErrorPageContents.contents["upgrading"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.UpgradingTitle, Strings.UpgradingMessage, HttpStatusCode.InternalServerError, 19, "{83D84498-82C7-46cd-841F-81638E0C606F}");
			ErrorPageContents.contents["nonuniquerecipient"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.NonUniqueRecipientTitle, Strings.NonUniqueRecipientMessage, HttpStatusCode.Forbidden, "{A9E5C855-51A7-434c-A166-B95C36C51DF1}");
			ErrorPageContents.contents["proxy"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.ProxyCantFindCasServerTitle, Strings.ProxyCantFindCasServerMessage, HttpStatusCode.NotFound, "{AB2947B3-AFB9-4365-BD31-89284BC68EB0}");
			ErrorPageContents.contents["notsupportrap"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.RAPObjectNotSupportedTitle, Strings.RAPObjectNotSupportedMessage, HttpStatusCode.InternalServerError, 19, "{3EBAAF4A-EE62-46df-9DFF-800CFAEEFCD4}");
			ErrorPageContents.contents["anonymousauthenticationdisabled"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.AnonymousAuthenticationDisabledErrorTitle, Strings.AnonymousAuthenticationDisabledErrorMessage, HttpStatusCode.InternalServerError, "{63668E51-23A5-4F34-B950-1607F42280B0}");
			ErrorPageContents.contents["liveidmismatch"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.LiveIDMismatchTitle, Strings.LiveIDMismatchMessage, HttpStatusCode.Forbidden, "{76CA7E8D-50A0-415e-BF66-501FD7FD6BB9}");
			ErrorPageContents.contents["verificationfailed"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.VerificationFailedTitle, Strings.VerificationFailedMessage, HttpStatusCode.BadRequest, "{8E001AD5-2C65-4a92-9B19-D84BF7DD4EFB}");
			ErrorPageContents.contents["verificationprocessingerror"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.VerificationProcessingErrorTitle, Strings.VerificationProcessingErrorMessage, HttpStatusCode.InternalServerError, "{D0D6EBCC-AF9B-42f7-A760-EB82E1F22C52}");
			ErrorPageContents.contents["noeso"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.NoEsoTitle, Strings.NoEsoMessage, HttpStatusCode.InternalServerError, "{5C100BC7-4975-4fb9-89F6-F3082B70B647}");
			ErrorPageContents.contents["regionalsettingsnotconfigured"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.NoRegionalSettingsTitle, Strings.NoRegionalSettingsMessage, HttpStatusCode.PreconditionFailed, "{88BE340C-10B5-4B6E-9EF6-702A30198293}");
			ErrorPageContents.contents["messagenotfound"] = new ErrorPageContents(Strings.ErrorMessageNotFoundTitle, Strings.ErrorMessageNotFound, Strings.ErrorMessageNotFoundMessage, HttpStatusCode.InternalServerError, "{31F23924-7106-4132-8CF2-F0E094CE5DD2}");
			ErrorPageContents.contents["tokenrenewed"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.SecurityTokenRenewedTitle, Strings.SecurityTokenRenewedMessage, (HttpStatusCode)443, "{A99A4004-4BEA-48e5-8681-500461CA0C14}");
			ErrorPageContents.contents["loopdetected"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.ServiceDown, OwaLocalizedStrings.ErrorWrongCASServerBecauseOfOutOfDateDNSCache, HttpStatusCode.InternalServerError, "{A03DCEA8-99E5-4748-932C-55CE0966715D}");
			ErrorPageContents.contents["badofficecallback"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.BadRequestTitle, Strings.BadOfficeCallbackMessage, HttpStatusCode.BadRequest, "{DF6D03FD-C280-4054-860D-3E25B1A7E568}");
			ErrorPageContents.contents["badlinkedinconfiguration"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.BadLinkedInConfigurationErrorPageTitle, Strings.BadLinkedInConfigurationErrorPageMessage, HttpStatusCode.InternalServerError, "{D0526CCD-14D5-4A67-AAAF-3677B6531E4F}");
			ErrorPageContents.contents["linkedinauthorizationerror"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.LinkedInAuthorizationErrorPageTitle, Strings.LinkedInAuthorizationErrorPageMessage, HttpStatusCode.InternalServerError, "{22F735E4-B120-4A56-BF73-F164AD79DE98}");
			ErrorPageContents.contents["badrequesttopeopleconnectmainbadproviderparameter"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.BadRequestToPeopleConnectMainPageErrorPageTitle, Strings.BadRequestToPeopleConnectMainPageProviderParamErrorPageMessage, HttpStatusCode.BadRequest, "{28BDB11A-8716-47B2-977E-4C09C57DC1C4}");
			ErrorPageContents.contents["badfacebookconfiguration"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.BadFacebookConfigurationErrorPageTitle, Strings.BadFacebookConfigurationErrorPageMessage, HttpStatusCode.InternalServerError, "{0310CCFF-46F3-41A8-B009-E69F1202F3A6}");
			ErrorPageContents.contents["cannotaccessoptionswithbeparamorcookie"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.CannotAccessOptionsWithBEParamOrCookieTitle, Strings.CannotAccessOptionsWithBEParamOrCookieMessage, HttpStatusCode.BadRequest, "{B8F74463-3AA4-403E-BD77-C572962E49E0}");
			ErrorPageContents.contents["denycrossdomainhost"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.DenyCrossDomainHostTitle, Strings.DenyCrossDomainHostMessage, HttpStatusCode.BadRequest, "{84D91D65-CE50-4F01-82E5-C5476F780191}");
			ErrorPageContents.contents["hybridsyncfail"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.HybridSyncFailTitle, Strings.HybridSyncFailMessage, HttpStatusCode.BadRequest, "{84D91D65-CE50-4F01-82E5-C5476F780191}");
			ErrorPageContents.contents["reportnotfound"] = new ErrorPageContents(Strings.ErrorPageTitle, Strings.ReportNotFoundTitle, Strings.ReportNotFoundMessage, HttpStatusCode.BadRequest, "{A71A58BD-0FCA-4536-985E-0B8206D9EAA6}");
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x000733BE File Offset: 0x000715BE
		private ErrorPageContents(LocalizedString pageTitle, LocalizedString errorMessageTitle, LocalizedString errorMessageText, HttpStatusCode statusCode, string causeMarker) : this(pageTitle, errorMessageTitle, errorMessageText, statusCode, 0, causeMarker)
		{
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000733CE File Offset: 0x000715CE
		private ErrorPageContents(LocalizedString pageTitle, LocalizedString errorMessageTitle, LocalizedString errorMessageText, HttpStatusCode statusCode, int subStatusCode, string causeMarker)
		{
			this.PageTitle = pageTitle;
			this.ErrorMessageTitle = errorMessageTitle;
			this.ErrorMessageText = errorMessageText;
			this.StatusCode = statusCode;
			this.SubStatusCode = subStatusCode;
			this.CauseMarker = causeMarker;
		}

		// Token: 0x17001B8B RID: 7051
		// (get) Token: 0x0600256F RID: 9583 RVA: 0x00073403 File Offset: 0x00071603
		// (set) Token: 0x06002570 RID: 9584 RVA: 0x0007340B File Offset: 0x0007160B
		public LocalizedString PageTitle { get; private set; }

		// Token: 0x17001B8C RID: 7052
		// (get) Token: 0x06002571 RID: 9585 RVA: 0x00073414 File Offset: 0x00071614
		// (set) Token: 0x06002572 RID: 9586 RVA: 0x0007341C File Offset: 0x0007161C
		public LocalizedString ErrorMessageTitle { get; private set; }

		// Token: 0x17001B8D RID: 7053
		// (get) Token: 0x06002573 RID: 9587 RVA: 0x00073425 File Offset: 0x00071625
		// (set) Token: 0x06002574 RID: 9588 RVA: 0x0007342D File Offset: 0x0007162D
		public LocalizedString ErrorMessageText { get; private set; }

		// Token: 0x17001B8E RID: 7054
		// (get) Token: 0x06002575 RID: 9589 RVA: 0x00073436 File Offset: 0x00071636
		// (set) Token: 0x06002576 RID: 9590 RVA: 0x0007343E File Offset: 0x0007163E
		public HttpStatusCode StatusCode { get; private set; }

		// Token: 0x17001B8F RID: 7055
		// (get) Token: 0x06002577 RID: 9591 RVA: 0x00073447 File Offset: 0x00071647
		// (set) Token: 0x06002578 RID: 9592 RVA: 0x0007344F File Offset: 0x0007164F
		public int SubStatusCode { get; private set; }

		// Token: 0x17001B90 RID: 7056
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x00073458 File Offset: 0x00071658
		// (set) Token: 0x0600257A RID: 9594 RVA: 0x00073460 File Offset: 0x00071660
		public string CauseMarker { get; private set; }

		// Token: 0x0600257B RID: 9595 RVA: 0x0007346C File Offset: 0x0007166C
		public static ErrorPageContents GetContentsForErrorType(string errorType)
		{
			string key = errorType.ToLower();
			ErrorPageContents result;
			if (ErrorPageContents.contents.ContainsKey(key))
			{
				result = ErrorPageContents.contents[key];
			}
			else
			{
				result = ErrorPageContents.contents["unexpected"];
			}
			return result;
		}

		// Token: 0x04001EDB RID: 7899
		public const string NoRoles = "noroles";

		// Token: 0x04001EDC RID: 7900
		public const string LowVersion = "lowversion";

		// Token: 0x04001EDD RID: 7901
		public const string NoneMailbox = "nonmailbox";

		// Token: 0x04001EDE RID: 7902
		public const string NoCookies = "nocookies";

		// Token: 0x04001EDF RID: 7903
		public const string NoScripts = "noscripts";

		// Token: 0x04001EE0 RID: 7904
		public const string BadRequest = "badrequest";

		// Token: 0x04001EE1 RID: 7905
		public const string BadQueryParameter = "badqueryparameter";

		// Token: 0x04001EE2 RID: 7906
		public const string UrlNotFound = "urlnotfound";

		// Token: 0x04001EE3 RID: 7907
		public const string UrlNotFoundOrNoAccess = "urlnotfoundornoaccess";

		// Token: 0x04001EE4 RID: 7908
		public const string BrowserNotSupported = "browsernotsupported";

		// Token: 0x04001EE5 RID: 7909
		public const string MessageNotFound = "messagenotfound";

		// Token: 0x04001EE6 RID: 7910
		public const string OWALoginRequired = "owalogin";

		// Token: 0x04001EE7 RID: 7911
		public const string OWAPremium = "owapremium";

		// Token: 0x04001EE8 RID: 7912
		public const string OverBudget = "overbudget";

		// Token: 0x04001EE9 RID: 7913
		public const string Upgrading = "upgrading";

		// Token: 0x04001EEA RID: 7914
		public const string TransientServiceError = "transientserviceerror";

		// Token: 0x04001EEB RID: 7915
		public const string NonUniqueRecipient = "nonuniquerecipient";

		// Token: 0x04001EEC RID: 7916
		public const string ProxyCantFindCasServer = "proxy";

		// Token: 0x04001EED RID: 7917
		public const string NotSupportedRAP = "notsupportrap";

		// Token: 0x04001EEE RID: 7918
		public const string AnonymousAuthenticationDisabled = "anonymousauthenticationdisabled";

		// Token: 0x04001EEF RID: 7919
		public const string Unexpected = "unexpected";

		// Token: 0x04001EF0 RID: 7920
		public const string LiveIDMismatch = "liveidmismatch";

		// Token: 0x04001EF1 RID: 7921
		public const string VerificationFailed = "verificationfailed";

		// Token: 0x04001EF2 RID: 7922
		public const string VerificationProcessingError = "verificationprocessingerror";

		// Token: 0x04001EF3 RID: 7923
		public const string NoEso = "noeso";

		// Token: 0x04001EF4 RID: 7924
		public const string RegionalSettingsNotConfigured = "regionalsettingsnotconfigured";

		// Token: 0x04001EF5 RID: 7925
		public const string TokenRenewed = "tokenrenewed";

		// Token: 0x04001EF6 RID: 7926
		public const string LoopDetected = "loopdetected";

		// Token: 0x04001EF7 RID: 7927
		public const string BadOfficeCallback = "badofficecallback";

		// Token: 0x04001EF8 RID: 7928
		public const string BadLinkedInConfiguration = "badlinkedinconfiguration";

		// Token: 0x04001EF9 RID: 7929
		public const string LinkedInAuthorizationError = "linkedinauthorizationerror";

		// Token: 0x04001EFA RID: 7930
		public const string BadRequestToPeopleConnectMainBadProviderParameter = "badrequesttopeopleconnectmainbadproviderparameter";

		// Token: 0x04001EFB RID: 7931
		public const string BadFacebookConfiguration = "badfacebookconfiguration";

		// Token: 0x04001EFC RID: 7932
		public const string CannotAccessOptionsWithBEParamOrCookie = "cannotaccessoptionswithbeparamorcookie";

		// Token: 0x04001EFD RID: 7933
		public const string DenyCrossDomainHost = "denycrossdomainhost";

		// Token: 0x04001EFE RID: 7934
		public const string HybridSyncFail = "hybridsyncfail";

		// Token: 0x04001EFF RID: 7935
		public const string ReportNotFound = "reportnotfound";

		// Token: 0x04001F00 RID: 7936
		private static Dictionary<string, ErrorPageContents> contents = new Dictionary<string, ErrorPageContents>(7);
	}
}
