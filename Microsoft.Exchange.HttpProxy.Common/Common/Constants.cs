using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Net.Protocols;

namespace Microsoft.Exchange.HttpProxy.Common
{
	// Token: 0x02000003 RID: 3
	internal static class Constants
	{
		// Token: 0x04000008 RID: 8
		public const string AnonymousRequestFilterModuleLoggingKey = "AnonymousRequestFilterModule";

		// Token: 0x04000009 RID: 9
		public const string CalendarString = "calendar";

		// Token: 0x0400000A RID: 10
		public const string WLIDMemberName = "WLID-MemberName";

		// Token: 0x0400000B RID: 11
		public const string WLIDOrganizationContextHeaderName = "WLID-OrganizationContext";

		// Token: 0x0400000C RID: 12
		public const string NativeProxyTargetServer = "X-ProxyTargetServer";

		// Token: 0x0400000D RID: 13
		public const string NativeProxyTargetServerVersion = "X-ProxyTargetServerVersion";

		// Token: 0x0400000E RID: 14
		public const string NativeProxyTargetUrlAbsPath = "X-ProxyTargetUrlAbsPath";

		// Token: 0x0400000F RID: 15
		public const string RoutingKeys = "RoutingKeys";

		// Token: 0x04000010 RID: 16
		public const string EwsODataPath = "/ews/odata/";

		// Token: 0x04000011 RID: 17
		public const string OAuthMetadataPath = "/metadata/";

		// Token: 0x04000012 RID: 18
		public const string AutoDiscoverV2Path = "/autodiscover.json";

		// Token: 0x04000013 RID: 19
		public const string AutoDiscoverV2Version1Path = "/autodiscover.json/v1.0";

		// Token: 0x04000014 RID: 20
		public const string ODataPath = "/odata/";

		// Token: 0x04000015 RID: 21
		public const string EwsGetUserPhotoPath = "/ews/exchange.asmx/s/GetUserPhoto";

		// Token: 0x04000016 RID: 22
		public const string BackEndOverrideCookieName = "X-BackEndOverrideCookie";

		// Token: 0x04000017 RID: 23
		public const string PreferServerAffinityHeader = "X-PreferServerAffinity";

		// Token: 0x04000018 RID: 24
		public const string WsSecurityAddressEnd = "wssecurity";

		// Token: 0x04000019 RID: 25
		public const string PartnerAuthAddressEnd = "wssecurity/symmetrickey";

		// Token: 0x0400001A RID: 26
		public const string X509CertAuthAddressEnd = "wssecurity/x509cert";

		// Token: 0x0400001B RID: 27
		public static readonly Regex GuidAtDomainRegex = new Regex("^(?<mailboxguid>[A-Fa-f0-9]{32}|({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?|^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}}))(@(?<domain>[\\S.]+))*", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400001C RID: 28
		public static readonly Regex UsersEntityRegex = new Regex("/Users(\\('|/)(?<address>.+?)(['\\)/]|$)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400001D RID: 29
		public static readonly string ProbeHeaderName = WellKnownHeader.Probe;

		// Token: 0x0400001E RID: 30
		public static readonly string WLIDMemberNameHeaderName = WellKnownHeader.WLIDMemberName;

		// Token: 0x0400001F RID: 31
		public static readonly string XBackendHeaderPrefix = WellKnownHeader.XBackendHeaderPrefix;

		// Token: 0x04000020 RID: 32
		public static readonly string MsExchMonString = "MSEXCHMON";

		// Token: 0x04000021 RID: 33
		public static readonly string ActiveMonProbe = "ACTIVEMONITORING";

		// Token: 0x04000022 RID: 34
		public static readonly string LamProbe = "AMProbe";

		// Token: 0x04000023 RID: 35
		public static readonly string EasProbe = "TestActiveSyncConnectivity";

		// Token: 0x04000024 RID: 36
		public static readonly string MsExchProxyUri = WellKnownHeader.MsExchProxyUri;

		// Token: 0x04000025 RID: 37
		public static readonly string XIsFromCafe = WellKnownHeader.XIsFromCafe;

		// Token: 0x04000026 RID: 38
		public static readonly string XSourceCafeServer = WellKnownHeader.XSourceCafeServer;

		// Token: 0x04000027 RID: 39
		public static readonly string IsFromCafeHeaderValue = "1";

		// Token: 0x04000028 RID: 40
		public static readonly string LiveIdMemberName = WellKnownHeader.LiveIdMemberName;

		// Token: 0x04000029 RID: 41
		public static readonly string OriginatingClientIpHeader = "X-Forwarded-For";

		// Token: 0x0400002A RID: 42
		public static readonly string OriginatingClientPortHeader = "X-Forwarded-Port";
	}
}
