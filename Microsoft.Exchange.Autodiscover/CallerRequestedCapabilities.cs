using System;
using System.Web;
using Microsoft.Exchange.Autodiscover.ConfigurationSettings;
using Microsoft.Exchange.Autodiscover.WCF;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x02000011 RID: 17
	internal class CallerRequestedCapabilities
	{
		// Token: 0x06000072 RID: 114 RVA: 0x000040C0 File Offset: 0x000022C0
		protected CallerRequestedCapabilities(bool canFollowRedirect, bool canHandleExHttpNodesInResponse, Version outlookClientVersion, bool isCallerCrossForestAvailabilityService, bool isWindow7OrNewerClient, bool supportsNego, bool supportsNegoEx)
		{
			this.CanFollowRedirect = canFollowRedirect;
			this.CanHandleExHttpNodesInAutoDiscResponse = canHandleExHttpNodesInResponse;
			this.OutlookClientVersion = outlookClientVersion;
			this.IsCallerCrossForestAvailabilityService = isCallerCrossForestAvailabilityService;
			this.IsWindow7OrNewerClient = isWindow7OrNewerClient;
			this.CanHandleNegotiateCorrectly = supportsNego;
			if (supportsNegoEx && !supportsNego)
			{
				throw new ArgumentException("You cannot specify only NegotiateEx, Negotiate must also be supported or none of them!");
			}
			this.CanHandleNegotiateEx = supportsNegoEx;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00004148 File Offset: 0x00002348
		public static CallerRequestedCapabilities GetInstance(HttpContext requestHttpContext)
		{
			if (requestHttpContext == null)
			{
				throw new ArgumentNullException("requestHttpContext", "You must call this GetInstance method only in the cases where an HttpContext is available");
			}
			Version requestingClientVersion = null;
			Version windowsVersion = null;
			string text = Common.SafeGetUserAgent(requestHttpContext.Request);
			bool canFollowRedirect = !AutodiscoverProxy.CanRedirectOutlookClient(text);
			bool canHandleExHttpNodesInResponse = UserAgentHelper.IsWindowsClient(text);
			bool flag = UserAgentHelper.ValidateClientSoftwareVersions(text, delegate(int major, int minor)
			{
				windowsVersion = new Version(major, minor);
				return true;
			}, delegate(int major, int minor, int buildMajor)
			{
				requestingClientVersion = new Version(major, minor, buildMajor, 0);
				return true;
			}) && windowsVersion != null && requestingClientVersion != null;
			bool isCallerCrossForestAvailabilityService = !string.IsNullOrWhiteSpace(text) && text.StartsWith("ASAutoDiscover/CrossForest", StringComparison.OrdinalIgnoreCase) && requestHttpContext.User != null && requestHttpContext.User.Identity is ExternalIdentity;
			bool flag2 = flag && UserAgentHelper.IsClientWin7OrGreater(text);
			bool flag3 = false;
			bool flag4 = false;
			if (flag2 && CallerRequestedCapabilities.CheckIfClientSupportsNegotiate(requestingClientVersion, out flag3))
			{
				flag4 = true;
			}
			OptInCapabilities optInCapabilities;
			if (CallerRequestedCapabilities.TryParseOptInCapabillitiesHeader(requestHttpContext, out optInCapabilities))
			{
				if ((optInCapabilities & OptInCapabilities.Negotiate) != OptInCapabilities.None)
				{
					flag4 = true;
				}
				if ((optInCapabilities & OptInCapabilities.ExHttpInfo) != OptInCapabilities.None)
				{
					canHandleExHttpNodesInResponse = true;
				}
			}
			return new CallerRequestedCapabilities(canFollowRedirect, canHandleExHttpNodesInResponse, requestingClientVersion, isCallerCrossForestAvailabilityService, flag2, flag4, flag4 && flag3);
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00004277 File Offset: 0x00002477
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000427F File Offset: 0x0000247F
		public bool CanFollowRedirect { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00004288 File Offset: 0x00002488
		// (set) Token: 0x06000077 RID: 119 RVA: 0x00004290 File Offset: 0x00002490
		public bool CanHandleExHttpNodesInAutoDiscResponse { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00004299 File Offset: 0x00002499
		// (set) Token: 0x06000079 RID: 121 RVA: 0x000042A1 File Offset: 0x000024A1
		public Version OutlookClientVersion { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600007A RID: 122 RVA: 0x000042AA File Offset: 0x000024AA
		// (set) Token: 0x0600007B RID: 123 RVA: 0x000042B2 File Offset: 0x000024B2
		public bool IsCallerCrossForestAvailabilityService { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000042BB File Offset: 0x000024BB
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000042C3 File Offset: 0x000024C3
		public bool IsWindow7OrNewerClient { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000042CC File Offset: 0x000024CC
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000042D4 File Offset: 0x000024D4
		public bool CanHandleNegotiateCorrectly { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000042DD File Offset: 0x000024DD
		// (set) Token: 0x06000081 RID: 129 RVA: 0x000042E5 File Offset: 0x000024E5
		public bool CanHandleNegotiateEx { get; private set; }

		// Token: 0x06000082 RID: 130 RVA: 0x000042F0 File Offset: 0x000024F0
		private static bool CheckIfClientSupportsNegotiate(Version clientVersion, out bool useNego2)
		{
			useNego2 = false;
			if (clientVersion.Major > 14 || (clientVersion.Major == 14 && clientVersion.Build >= 5133) || (clientVersion.Major == 12 && clientVersion.Build >= 6552))
			{
				return true;
			}
			if (clientVersion.Major == 14)
			{
				useNego2 = true;
				return true;
			}
			return false;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000434C File Offset: 0x0000254C
		private static bool TryParseOptInCapabillitiesHeader(HttpContext requestHttpContext, out OptInCapabilities optInCapabilities)
		{
			optInCapabilities = OptInCapabilities.None;
			string text = requestHttpContext.Request.Headers[CallerRequestedCapabilities.CapabillitiesHeaderName];
			if (!string.IsNullOrWhiteSpace(text))
			{
				bool result = false;
				foreach (string value in text.Split(new char[]
				{
					' '
				}, StringSplitOptions.RemoveEmptyEntries))
				{
					OptInCapabilities optInCapabilities2;
					if (Enum.TryParse<OptInCapabilities>(value, true, out optInCapabilities2))
					{
						optInCapabilities |= optInCapabilities2;
						result = true;
					}
				}
				return result;
			}
			return false;
		}

		// Token: 0x040000A2 RID: 162
		private static readonly string CapabillitiesHeaderName = "X-ClientCanHandle";
	}
}
