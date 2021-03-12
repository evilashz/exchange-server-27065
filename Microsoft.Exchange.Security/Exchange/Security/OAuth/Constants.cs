using System;

namespace Microsoft.Exchange.Security.OAuth
{
	// Token: 0x020000B1 RID: 177
	internal static class Constants
	{
		// Token: 0x040005D4 RID: 1492
		public static readonly OAuthApplication IdTokenApplication = new OAuthApplication(new V1ProfileAppInfo("ID_TOKEN_APP_2038A917-6EE8-4C7D-A755-E7AB95B87AE5", null, null));

		// Token: 0x040005D5 RID: 1493
		public static readonly string BearerAuthenticationType = "Bearer";

		// Token: 0x040005D6 RID: 1494
		public static readonly string BearerPreAuthenticationType = "BearerPreAuth";

		// Token: 0x040005D7 RID: 1495
		public static readonly string WWWAuthenticateHeader = "WWW-Authenticate";

		// Token: 0x040005D8 RID: 1496
		public static readonly string ConsumerMailboxIdentifier = "outlook.com";

		// Token: 0x040005D9 RID: 1497
		public static readonly string MSExchangeSelfIssuingTokenRealm = "00000002-0000-0ff1-ce00-200000000000";

		// Token: 0x040005DA RID: 1498
		public static readonly string RequestCompletedHttpContextKeyName = "RequestCompleted";

		// Token: 0x040005DB RID: 1499
		public static readonly string AzureADCommonEntityIdHint = "{tenantid}";

		// Token: 0x020000B2 RID: 178
		public static class ChallengeTokens
		{
			// Token: 0x040005DC RID: 1500
			public static readonly string Realm = "realm";

			// Token: 0x040005DD RID: 1501
			public static readonly string ClientId = "client_id";

			// Token: 0x040005DE RID: 1502
			public static readonly string TrustedIssuers = "trusted_issuers";

			// Token: 0x040005DF RID: 1503
			public static readonly string AuthorizationUri = "authorization_uri";

			// Token: 0x040005E0 RID: 1504
			public static readonly string Error = "error";
		}

		// Token: 0x020000B3 RID: 179
		public static class ClaimTypes
		{
			// Token: 0x040005E1 RID: 1505
			public static readonly string X509CertificateThumbprint = "x5t";

			// Token: 0x040005E2 RID: 1506
			public static readonly string NameIdentifier = "nameid";

			// Token: 0x040005E3 RID: 1507
			public static readonly string Nii = "nii";

			// Token: 0x040005E4 RID: 1508
			public static readonly string Smtp = "smtp";

			// Token: 0x040005E5 RID: 1509
			public static readonly string Sip = "sip";

			// Token: 0x040005E6 RID: 1510
			public static readonly string Upn = "upn";

			// Token: 0x040005E7 RID: 1511
			public static readonly string ActorToken = "actortoken";

			// Token: 0x040005E8 RID: 1512
			public static readonly string MsExchImmutableId = "msexchuid";

			// Token: 0x040005E9 RID: 1513
			public static readonly string MsExchCallback = "msexchcallback";

			// Token: 0x040005EA RID: 1514
			public static readonly string MsExchProtocol = "msexchprot";

			// Token: 0x040005EB RID: 1515
			public static readonly string MsExchangeDelegatedAuth = "msexchdauth";

			// Token: 0x040005EC RID: 1516
			public static readonly string AppContext = "appctx";

			// Token: 0x040005ED RID: 1517
			public static readonly string TrustedForDelegation = "trustedfordelegation";

			// Token: 0x040005EE RID: 1518
			public static readonly string AppId = "appid";

			// Token: 0x040005EF RID: 1519
			public static readonly string Scp = "scp";

			// Token: 0x040005F0 RID: 1520
			public static readonly string Roles = "roles";

			// Token: 0x040005F1 RID: 1521
			public static readonly string Tid = "tid";

			// Token: 0x040005F2 RID: 1522
			public static readonly string Ver = "ver";

			// Token: 0x040005F3 RID: 1523
			public static readonly string Oid = "oid";

			// Token: 0x040005F4 RID: 1524
			public static readonly string PrimarySid = "primarysid";

			// Token: 0x040005F5 RID: 1525
			public static readonly string OnPremSid = "onprem_sid";

			// Token: 0x040005F6 RID: 1526
			public static readonly string AppCtxSender = "appctxsender";

			// Token: 0x040005F7 RID: 1527
			public static readonly string Scope = "scope";

			// Token: 0x040005F8 RID: 1528
			public static readonly string IsBrowserHostedApp = "isbrowserhostedapp";

			// Token: 0x040005F9 RID: 1529
			public static readonly string AuthMetaDocumentUrl = "amurl";

			// Token: 0x040005FA RID: 1530
			public static readonly string Version = "version";

			// Token: 0x040005FB RID: 1531
			public static readonly string AlternateSecurityId = "altsecid";

			// Token: 0x040005FC RID: 1532
			public static readonly string Audience = "aud";

			// Token: 0x040005FD RID: 1533
			public static readonly string Issuer = "iss";

			// Token: 0x040005FE RID: 1534
			public static readonly string Expiry = "exp";

			// Token: 0x040005FF RID: 1535
			public static readonly string IdentityProvider = "idp";

			// Token: 0x04000600 RID: 1536
			public static readonly string Puid = "puid";

			// Token: 0x04000601 RID: 1537
			public static readonly string EmailAddress = "email";
		}

		// Token: 0x020000B4 RID: 180
		public static class ClaimValues
		{
			// Token: 0x04000602 RID: 1538
			public static readonly string Version1 = "1.0";

			// Token: 0x04000603 RID: 1539
			public static readonly string ExchangeSelfIssuedVersion1 = "MSExchange.SelfIssued.V1";

			// Token: 0x04000604 RID: 1540
			public static readonly string UserImpersonation = "user_impersonation";

			// Token: 0x04000605 RID: 1541
			public static readonly string FullAccess = "full_access";

			// Token: 0x04000606 RID: 1542
			public static readonly string MsExtensionV1 = "MsExtension.V1";

			// Token: 0x04000607 RID: 1543
			public static readonly string MsOabDownloadV1 = "MsOabDownload.V1";

			// Token: 0x04000608 RID: 1544
			public static readonly string ExIdTokV1 = "ExIdTok.V1";

			// Token: 0x04000609 RID: 1545
			public static readonly string ExCallbackV1 = "ExCallback.V1";

			// Token: 0x0400060A RID: 1546
			public static readonly string ProtocolOwa = "owa";

			// Token: 0x0400060B RID: 1547
			public static readonly string ProtocolEws = "ews";

			// Token: 0x0400060C RID: 1548
			public static readonly string ProtocolOab = "oab";
		}

		// Token: 0x020000B5 RID: 181
		public static class NiiClaimValues
		{
			// Token: 0x0400060D RID: 1549
			public static readonly string ActiveDirectory = "urn:office:idp:activedirectory";

			// Token: 0x0400060E RID: 1550
			public static readonly string BusinessLiveId = "urn:federation:MicrosoftOnline";

			// Token: 0x0400060F RID: 1551
			public static readonly string LegacyBusinessLiveId = "urn:office:idp:orgid";
		}

		// Token: 0x020000B6 RID: 182
		public static class TokenCategories
		{
			// Token: 0x04000610 RID: 1552
			public static readonly string S2SAppActAsToken = "S2SAppActAs";

			// Token: 0x04000611 RID: 1553
			public static readonly string S2SAppOnlyToken = "S2SAppOnly";

			// Token: 0x04000612 RID: 1554
			public static readonly string CallbackToken = "Callback";

			// Token: 0x04000613 RID: 1555
			public static readonly string V1AppActAsToken = "V1AppActAs";

			// Token: 0x04000614 RID: 1556
			public static readonly string V1AppOnlyToken = "V1AppOnly";

			// Token: 0x04000615 RID: 1557
			public static readonly string V1IdToken = "V1IdToken";

			// Token: 0x04000616 RID: 1558
			public static readonly string V1ExchangeSelfIssuedToken = "V1ExchangeSelfIssued";

			// Token: 0x04000617 RID: 1559
			public static readonly string V1CallbackToken = "V1Callback";

			// Token: 0x04000618 RID: 1560
			public static readonly string[] All = new string[]
			{
				Constants.TokenCategories.S2SAppActAsToken,
				Constants.TokenCategories.S2SAppOnlyToken,
				Constants.TokenCategories.CallbackToken,
				Constants.TokenCategories.V1AppActAsToken,
				Constants.TokenCategories.V1AppOnlyToken,
				Constants.TokenCategories.V1IdToken,
				Constants.TokenCategories.V1ExchangeSelfIssuedToken,
				Constants.TokenCategories.V1CallbackToken
			};
		}
	}
}
