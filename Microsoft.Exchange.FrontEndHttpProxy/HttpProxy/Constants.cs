using System;
using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000085 RID: 133
	internal static class Constants
	{
		// Token: 0x040002FC RID: 764
		public static readonly string ExtensionGif = ".gif";

		// Token: 0x040002FD RID: 765
		public static readonly string ExtensionJpg = ".jpg";

		// Token: 0x040002FE RID: 766
		public static readonly string ExtensionCss = ".css";

		// Token: 0x040002FF RID: 767
		public static readonly string ExtensionXap = ".xap";

		// Token: 0x04000300 RID: 768
		public static readonly string ExtensionJs = ".js";

		// Token: 0x04000301 RID: 769
		public static readonly string ExtensionWav = ".wav";

		// Token: 0x04000302 RID: 770
		public static readonly string ExtensionMp3 = ".mp3";

		// Token: 0x04000303 RID: 771
		public static readonly string ExtensionHtm = ".htm";

		// Token: 0x04000304 RID: 772
		public static readonly string ExtensionHtml = ".html";

		// Token: 0x04000305 RID: 773
		public static readonly string ExtensionPng = ".png";

		// Token: 0x04000306 RID: 774
		public static readonly string ExtensionMSI = ".msi";

		// Token: 0x04000307 RID: 775
		public static readonly string ExtensionICO = ".ico";

		// Token: 0x04000308 RID: 776
		public static readonly string ExtensionManifest = ".manifest";

		// Token: 0x04000309 RID: 777
		public static readonly string ExtensionTtf = ".ttf";

		// Token: 0x0400030A RID: 778
		public static readonly string ExtensionEot = ".eot";

		// Token: 0x0400030B RID: 779
		public static readonly string ExtensionChromeWebApp = ".crx";

		// Token: 0x0400030C RID: 780
		public static readonly string ExtensionAxd = ".axd";

		// Token: 0x0400030D RID: 781
		public static readonly string ExtensionSvg = ".svg";

		// Token: 0x0400030E RID: 782
		public static readonly string ExtensionWoff = ".woff";

		// Token: 0x0400030F RID: 783
		public static readonly string ExtensionAspx = ".aspx";

		// Token: 0x04000310 RID: 784
		public static readonly string ExtensionOwa = ".owa";

		// Token: 0x04000311 RID: 785
		public static readonly string MsExchProxyUri = Constants.MsExchProxyUri;

		// Token: 0x04000312 RID: 786
		public static readonly string XIsFromCafe = Constants.XIsFromCafe;

		// Token: 0x04000313 RID: 787
		public static readonly string XSourceCafeServer = Constants.XSourceCafeServer;

		// Token: 0x04000314 RID: 788
		public static readonly string XBackendHeaderPrefix = Constants.XBackendHeaderPrefix;

		// Token: 0x04000315 RID: 789
		public static readonly string XRequestId = WellKnownHeader.XRequestId;

		// Token: 0x04000316 RID: 790
		public static readonly string AnchorMailboxHeaderName = WellKnownHeader.AnchorMailbox;

		// Token: 0x04000317 RID: 791
		public static readonly string ExternalDirectoryObjectIdHeaderName = WellKnownHeader.ExternalDirectoryObjectId;

		// Token: 0x04000318 RID: 792
		public static readonly string TargetDatabaseHeaderName = WellKnownHeader.TargetDatabase;

		// Token: 0x04000319 RID: 793
		public static readonly string ClientVersionHeaderName = WellKnownHeader.ClientVersion;

		// Token: 0x0400031A RID: 794
		public static readonly string BEServerExceptionHeaderName = "X-BEServerException";

		// Token: 0x0400031B RID: 795
		public static readonly string IllegalCrossServerConnectionExceptionType = "Microsoft.Exchange.Data.Storage.IllegalCrossServerConnectionException";

		// Token: 0x0400031C RID: 796
		public static readonly string BEServerRoutingErrorHeaderName = WellKnownHeader.BEServerRoutingError;

		// Token: 0x0400031D RID: 797
		public static readonly string WLIDMemberNameHeaderName = Constants.WLIDMemberNameHeaderName;

		// Token: 0x0400031E RID: 798
		public static readonly string WLIDOrganizationContextHeaderName = "WLID-OrganizationContext";

		// Token: 0x0400031F RID: 799
		public static readonly string LiveIdEnvironment = "RPSEnv";

		// Token: 0x04000320 RID: 800
		public static readonly string LiveIdPuid = WellKnownHeader.LiveIdPuid;

		// Token: 0x04000321 RID: 801
		public static readonly string OrgIdPuid = "RPSOrgIdPUID";

		// Token: 0x04000322 RID: 802
		public static readonly string LiveIdMemberName = Constants.LiveIdMemberName;

		// Token: 0x04000323 RID: 803
		public static readonly string AcceptEncodingHeaderName = WellKnownHeader.AcceptEncoding;

		// Token: 0x04000324 RID: 804
		public static readonly string TestBackEndUrlRequestHeaderKey = WellKnownHeader.TestBackEndUrlRequest;

		// Token: 0x04000325 RID: 805
		public static readonly string CafeErrorCodeHeaderName = WellKnownHeader.CafeErrorCode;

		// Token: 0x04000326 RID: 806
		public static readonly string CaptureResponseIdHeaderKey = WellKnownHeader.CaptureResponseId;

		// Token: 0x04000327 RID: 807
		public static readonly string ProbeHeaderName = Constants.ProbeHeaderName;

		// Token: 0x04000328 RID: 808
		public static readonly string LocalProbeHeaderValue = WellKnownHeader.LocalProbeHeaderValue;

		// Token: 0x04000329 RID: 809
		public static readonly string AuthorizationHeader = WellKnownHeader.Authorization;

		// Token: 0x0400032A RID: 810
		public static readonly string AuthenticationHeader = WellKnownHeader.Authentication;

		// Token: 0x0400032B RID: 811
		public static readonly string FrontEndToBackEndTimeout = WellKnownHeader.FrontEndToBackEndTimeout;

		// Token: 0x0400032C RID: 812
		public static readonly string BEResourcePath = "X-BEResourcePath";

		// Token: 0x0400032D RID: 813
		public static readonly string OriginatingClientIpHeader = Constants.OriginatingClientIpHeader;

		// Token: 0x0400032E RID: 814
		public static readonly string OriginatingClientPortHeader = Constants.OriginatingClientPortHeader;

		// Token: 0x0400032F RID: 815
		public static readonly string VDirObjectID = "X-vDirObjectId";

		// Token: 0x04000330 RID: 816
		public static readonly string MissingDirectoryUserObjectHeader = WellKnownHeader.MissingDirectoryUserObjectHint;

		// Token: 0x04000331 RID: 817
		public static readonly string OrganizationContextHeader = WellKnownHeader.OrganizationContext;

		// Token: 0x04000332 RID: 818
		public static readonly string RequestCompletedHttpContextKeyName = "RequestCompleted";

		// Token: 0x04000333 RID: 819
		public static readonly string LatencyTrackerContextKeyName = "LatencyTracker";

		// Token: 0x04000334 RID: 820
		public static readonly string TraceContextKey = "TraceContext";

		// Token: 0x04000335 RID: 821
		public static readonly string RequestIdHttpContextKeyName = "LogRequestId";

		// Token: 0x04000336 RID: 822
		public static readonly string CallerADRawEntryKeyName = "CallerADRawEntry";

		// Token: 0x04000337 RID: 823
		public static readonly string MissingDirectoryUserObjectKey = "MissingDirectoryUserObject";

		// Token: 0x04000338 RID: 824
		public static readonly string OrganizationContextKey = "OrganizationContext";

		// Token: 0x04000339 RID: 825
		public static readonly string WLIDMemberName = "WLID-MemberName";

		// Token: 0x0400033A RID: 826
		public static readonly string GzipHeaderValue = "gzip";

		// Token: 0x0400033B RID: 827
		public static readonly string DeflateHeaderValue = "deflate";

		// Token: 0x0400033C RID: 828
		public static readonly string IsFromCafeHeaderValue = Constants.IsFromCafeHeaderValue;

		// Token: 0x0400033D RID: 829
		public static readonly string SpnPrefixForHttp = "HTTP/";

		// Token: 0x0400033E RID: 830
		public static readonly string NegotiatePackageValue = "Negotiate";

		// Token: 0x0400033F RID: 831
		public static readonly string NtlmPackageValue = "NTLM";

		// Token: 0x04000340 RID: 832
		public static readonly string KerberosPackageValue = "Kerberos";

		// Token: 0x04000341 RID: 833
		public static readonly string PrefixForKerbAuthBlob = "Negotiate ";

		// Token: 0x04000342 RID: 834
		public static readonly string RPSBackEndServerCookieName = "MS-WSMAN";

		// Token: 0x04000343 RID: 835
		public static readonly string LiveIdRPSAuth = "RPSAuth";

		// Token: 0x04000344 RID: 836
		public static readonly string LiveIdRPSSecAuth = "RPSSecAuth";

		// Token: 0x04000345 RID: 837
		public static readonly string LiveIdRPSTAuth = "RPSTAuth";

		// Token: 0x04000346 RID: 838
		public static readonly string BEResource = "X-BEResource";

		// Token: 0x04000347 RID: 839
		public static readonly string AnonResource = "X-AnonResource";

		// Token: 0x04000348 RID: 840
		public static readonly string AnonResourceBackend = "X-AnonResource-Backend";

		// Token: 0x04000349 RID: 841
		public static readonly string BackEndOverrideCookieName = "X-BackEndOverrideCookie";

		// Token: 0x0400034A RID: 842
		public static readonly string PreferServerAffinityHeader = "X-PreferServerAffinity";

		// Token: 0x0400034B RID: 843
		public static readonly Regex GuidAtDomainRegex = Constants.GuidAtDomainRegex;

		// Token: 0x0400034C RID: 844
		public static readonly Regex AddressRegex = new Regex("(?<address>[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4})", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400034D RID: 845
		public static readonly Regex SidRegex = new Regex("(?<sid>S-1-5-\\d{2}-\\d{9,}-\\d{9,}-\\d{9,}-\\d{4,})@(?<domain>[\\S.]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400034E RID: 846
		public static readonly Regex SidOnlyRegex = new Regex("(?<sid>S-1-5-\\d{2}-\\d{9,}-\\d{9,}-\\d{9,}-\\d{4,})", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400034F RID: 847
		public static readonly Regex ExchClientVerRegex = new Regex("(?<major>\\d{2})\\.(?<minor>\\d{1,})\\.(?<build>\\d{1,})\\.(?<revision>\\d{1,})", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000350 RID: 848
		public static readonly Regex NoLeadingZeroRegex = new Regex("0*([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000351 RID: 849
		public static readonly Regex NoRevisionNumberRegex = new Regex("^([0-9]+\\.[0-9]+\\.[0-9]+)(\\.[0-9]+)*", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x04000352 RID: 850
		public static readonly string CertificateValidationComponentId = "ClientAccessFrontEnd";

		// Token: 0x04000353 RID: 851
		public static readonly string NotImplementedStatusDescription = "Not implemented";

		// Token: 0x04000354 RID: 852
		public static readonly string IntegratedAuthPath = "/integrated";

		// Token: 0x04000355 RID: 853
		public static readonly string IntegratedAuthPathWithTrailingSlash = Constants.IntegratedAuthPath + "/";

		// Token: 0x04000356 RID: 854
		public static readonly string OMAPath = "/oma";

		// Token: 0x04000357 RID: 855
		public static readonly string RequestIdKeyForIISLogs = "&cafeReqId=";

		// Token: 0x04000358 RID: 856
		public static readonly string CorrelationIdKeyForIISLogs = "&CorrelationID=";

		// Token: 0x04000359 RID: 857
		public static readonly string ISO8601DateTimeMsPattern = "yyyy-MM-ddTHH:mm:ss.fff";

		// Token: 0x0400035A RID: 858
		public static readonly string HealthCheckPage = "HealthCheck.htm";

		// Token: 0x0400035B RID: 859
		public static readonly string HealthCheckPageResponse = "200 OK";

		// Token: 0x0400035C RID: 860
		public static readonly string OutlookDomain = "outlook.com";

		// Token: 0x0400035D RID: 861
		public static readonly string Office365Domain = "outlook.office365.com";

		// Token: 0x0400035E RID: 862
		public static readonly string ServerKerberosAuthenticationFailureErrorCode = HttpProxySubErrorCode.ServerKerberosAuthenticationFailure.ToString();

		// Token: 0x0400035F RID: 863
		public static readonly string InvalidOAuthTokenErrorCode = HttpProxySubErrorCode.InvalidOAuthToken.ToString();

		// Token: 0x04000360 RID: 864
		public static readonly string ClientDisconnectErrorCode = HttpProxySubErrorCode.ClientDisconnect.ToString();

		// Token: 0x04000361 RID: 865
		public static readonly string InternalServerErrorStatusCode = HttpStatusCode.InternalServerError.ToString();
	}
}
