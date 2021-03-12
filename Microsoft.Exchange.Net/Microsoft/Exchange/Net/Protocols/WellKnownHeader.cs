using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Protocols
{
	// Token: 0x0200082A RID: 2090
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class WellKnownHeader
	{
		// Token: 0x04002691 RID: 9873
		public const string XEXTClientName = "X-EXT-ClientName";

		// Token: 0x04002692 RID: 9874
		public const string XEXTCorrelationId = "X-EXT-CorrelationId";

		// Token: 0x04002693 RID: 9875
		public const string XEXTProxyBegin = "X-EXT-ProxyBegin";

		// Token: 0x04002694 RID: 9876
		public const string XEXTACSBegin = "X-EXT-ACSBegin";

		// Token: 0x04002695 RID: 9877
		public const string XEXTACSEnd = "X-EXT-ACSEnd";

		// Token: 0x04002696 RID: 9878
		public const string XOWACorrelationId = "X-OWA-CorrelationId";

		// Token: 0x04002697 RID: 9879
		public const string XSuiteServiceProxyOrigin = "X-SuiteServiceProxyOrigin";

		// Token: 0x04002698 RID: 9880
		public const string XOWAClientBegin = "X-OWA-ClientBegin";

		// Token: 0x04002699 RID: 9881
		public const string XFrontEndBegin = "X-FrontEnd-Begin";

		// Token: 0x0400269A RID: 9882
		public const string XFrontEndEnd = "X-FrontEnd-End";

		// Token: 0x0400269B RID: 9883
		public const string XBackEndBegin = "X-BackEnd-Begin";

		// Token: 0x0400269C RID: 9884
		public const string XBackEndEnd = "X-BackEnd-End";

		// Token: 0x0400269D RID: 9885
		public const string XFrontEndHandlerBegin = "X-FrontEnd-Handler-Begin";

		// Token: 0x0400269E RID: 9886
		public const string XFromBackEndClientConnection = "X-FromBackEnd-ClientConnection";

		// Token: 0x0400269F RID: 9887
		public const string XFromBackEndServerAffinity = "X-FromBackend-ServerAffinity";

		// Token: 0x040026A0 RID: 9888
		public const string XAuthError = "X-Auth-Error";

		// Token: 0x040026A1 RID: 9889
		public const string XAutoDiscoveryError = "X-AutoDiscovery-Error";

		// Token: 0x040026A2 RID: 9890
		public const string OwaEcpUpnAnchorMailbox = "X-UpnAnchorMailbox";

		// Token: 0x040026A3 RID: 9891
		public const string HttpProxyInfo = "Via";

		// Token: 0x040026A4 RID: 9892
		public const string XOwaExplicitLogonUser = "X-OWA-ExplicitLogonUser";

		// Token: 0x040026A5 RID: 9893
		public static readonly string MsExchProxyUri = "msExchProxyUri";

		// Token: 0x040026A6 RID: 9894
		public static readonly string XIsFromCafe = "X-IsFromCafe";

		// Token: 0x040026A7 RID: 9895
		public static readonly string XIsFromBackend = "X-IsFromBackend";

		// Token: 0x040026A8 RID: 9896
		public static readonly string XSourceCafeServer = "X-SourceCafeServer";

		// Token: 0x040026A9 RID: 9897
		public static readonly string XBackendHeaderPrefix = "X-Backend-Diag-";

		// Token: 0x040026AA RID: 9898
		public static readonly string XFEServer = "X-FEServer";

		// Token: 0x040026AB RID: 9899
		public static readonly string XBEServer = "X-BEServer";

		// Token: 0x040026AC RID: 9900
		public static readonly string XCalculatedBETarget = "X-CalculatedBETarget";

		// Token: 0x040026AD RID: 9901
		public static readonly string XTargetServer = "X-TargetServer";

		// Token: 0x040026AE RID: 9902
		public static readonly string XRequestId = "X-RequestId";

		// Token: 0x040026AF RID: 9903
		public static readonly string XForwardedFor = "X-Forwarded-For";

		// Token: 0x040026B0 RID: 9904
		public static readonly string AnchorMailbox = "X-AnchorMailbox";

		// Token: 0x040026B1 RID: 9905
		public static readonly string ExternalDirectoryObjectId = "X-ExternalDirectoryObjectId";

		// Token: 0x040026B2 RID: 9906
		public static readonly string PublicFolderMailbox = "X-PublicFolderMailbox";

		// Token: 0x040026B3 RID: 9907
		public static readonly string TargetDatabase = "X-TargetDatabase";

		// Token: 0x040026B4 RID: 9908
		public static readonly string ClientVersion = "X-ExchClientVersion";

		// Token: 0x040026B5 RID: 9909
		public static readonly string WLIDMemberName = "X-WLID-MemberName";

		// Token: 0x040026B6 RID: 9910
		public static readonly string LiveIdPuid = "RPSPUID";

		// Token: 0x040026B7 RID: 9911
		public static readonly string LiveIdMemberName = "RPSMemberName";

		// Token: 0x040026B8 RID: 9912
		public static readonly string AcceptEncoding = "Accept-Encoding";

		// Token: 0x040026B9 RID: 9913
		public static readonly string TestBackEndUrlRequest = "TestBackEndUrl";

		// Token: 0x040026BA RID: 9914
		public static readonly string CafeErrorCode = "X-CasErrorCode";

		// Token: 0x040026BB RID: 9915
		public static readonly string CaptureResponseId = "CaptureResponseId";

		// Token: 0x040026BC RID: 9916
		public static readonly string Probe = "X-ProbeType";

		// Token: 0x040026BD RID: 9917
		public static readonly string LocalProbeHeaderValue = "X-MS-ClientAccess-LocalProbe";

		// Token: 0x040026BE RID: 9918
		public static readonly string Authorization = "Authorization";

		// Token: 0x040026BF RID: 9919
		public static readonly string Authentication = "WWW-Authenticate";

		// Token: 0x040026C0 RID: 9920
		public static readonly string E14DiagInfo = "X-DiagInfo";

		// Token: 0x040026C1 RID: 9921
		public static readonly string BEServerRoutingError = "X-BEServerRoutingError";

		// Token: 0x040026C2 RID: 9922
		public static readonly string XDBMountedOnServer = "X-DBMountedOnServer";

		// Token: 0x040026C3 RID: 9923
		public static readonly string Pragma = "Pragma";

		// Token: 0x040026C4 RID: 9924
		public static readonly string MailboxDatabaseGuid = "X-DatabaseGuid";

		// Token: 0x040026C5 RID: 9925
		public static readonly string PreAuthRequest = "X-AuthenticateOnly";

		// Token: 0x040026C6 RID: 9926
		public static readonly string RpcHttpProxyLogonUserName = "X-RpcHttpProxyLogonUserName";

		// Token: 0x040026C7 RID: 9927
		public static readonly string RpcHttpProxyServerTarget = "X-RpcHttpProxyServerTarget";

		// Token: 0x040026C8 RID: 9928
		public static readonly string RpcHttpProxyAssociationGuid = "X-AssociationGuid";

		// Token: 0x040026C9 RID: 9929
		public static readonly string GenericAnchorHint = "X-GenericAnchorHint";

		// Token: 0x040026CA RID: 9930
		public static readonly string FrontEndToBackEndTimeout = "X-FeToBeTimeout";

		// Token: 0x040026CB RID: 9931
		public static readonly string EWSTargetVersion = "X-EWS-TargetVersion";

		// Token: 0x040026CC RID: 9932
		public static readonly string XCafeLastRetryHeaderKey = "X-Cafe-Last-Retry";

		// Token: 0x040026CD RID: 9933
		public static readonly string BackEndOriginatingMailboxDatabase = "X-BackEndOriginatingMailboxDatabase";

		// Token: 0x040026CE RID: 9934
		public static readonly string CmdletProxyIsOn = "proxy";

		// Token: 0x040026CF RID: 9935
		public static readonly string MissingDirectoryUserObjectHint = "X-MissingDirectoryUserObjectHint";

		// Token: 0x040026D0 RID: 9936
		public static readonly string OrganizationContext = "X-OrganizationContext";

		// Token: 0x040026D1 RID: 9937
		public static readonly string MSDiagnostics = "X-MS-Diagnostics";

		// Token: 0x040026D2 RID: 9938
		public static readonly string PrimaryIdentity = "X-PrimaryIdentity";

		// Token: 0x040026D3 RID: 9939
		public static readonly string XOWAErrorMessageID = "X-OWAErrorMessageID";

		// Token: 0x040026D4 RID: 9940
		public static readonly string StrictTransportSecurity = "Strict-Transport-Security";

		// Token: 0x040026D5 RID: 9941
		public static readonly string XErrorTrace = "X-ErrorTrace";

		// Token: 0x040026D6 RID: 9942
		public static readonly string XRealm = "X-Realm";
	}
}
