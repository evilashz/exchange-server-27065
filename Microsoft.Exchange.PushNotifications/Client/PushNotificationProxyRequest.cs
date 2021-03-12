using System;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.PushNotifications.Extensions;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.PushNotifications.Client
{
	// Token: 0x02000026 RID: 38
	internal class PushNotificationProxyRequest : HttpSessionConfig
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00004452 File Offset: 0x00002652
		public PushNotificationProxyRequest(ICredentials credentials = null, Uri targetUri = null) : this(credentials, targetUri, false)
		{
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004460 File Offset: 0x00002660
		private PushNotificationProxyRequest(ICredentials credentials, Uri targetUri, bool isMonitoring)
		{
			base.Method = "POST";
			base.ContentType = "application/json";
			base.Pipelined = false;
			base.AllowAutoRedirect = false;
			base.KeepAlive = false;
			base.ReadWebExceptionResponseStream = true;
			base.UserAgent = PushNotificationProxyRequest.PushNotificationsUserAgent;
			this.Uri = targetUri;
			base.Headers = new WebHeaderCollection();
			if (isMonitoring)
			{
				WebHeaderCollection c = new WebHeaderCollection
				{
					{
						WellKnownHeader.Probe,
						WellKnownHeader.LocalProbeHeaderValue
					}
				};
				base.Headers.Add(c);
			}
			if (credentials != null)
			{
				base.Credentials = credentials;
				if (credentials is OAuthCredentials)
				{
					base.Headers[HttpRequestHeader.Authorization] = "Bearer";
					base.Headers["return-client-request-id"] = true.ToString();
					base.PreAuthenticate = true;
				}
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000452D File Offset: 0x0000272D
		// (set) Token: 0x0600010B RID: 267 RVA: 0x0000453F File Offset: 0x0000273F
		public string ClientRequestId
		{
			get
			{
				return base.Headers["client-request-id"];
			}
			set
			{
				base.Headers["client-request-id"] = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00004552 File Offset: 0x00002752
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00004564 File Offset: 0x00002764
		public string ComponentId
		{
			get
			{
				return base.Headers[CertificateValidationManager.ComponentIdHeaderName];
			}
			set
			{
				base.Headers[CertificateValidationManager.ComponentIdHeaderName] = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00004577 File Offset: 0x00002777
		// (set) Token: 0x0600010F RID: 271 RVA: 0x00004586 File Offset: 0x00002786
		public string Authorization
		{
			get
			{
				return base.Headers[HttpRequestHeader.Authorization];
			}
			set
			{
				base.Headers[HttpRequestHeader.Authorization] = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000110 RID: 272 RVA: 0x00004596 File Offset: 0x00002796
		// (set) Token: 0x06000111 RID: 273 RVA: 0x0000459E File Offset: 0x0000279E
		public Uri Uri { get; private set; }

		// Token: 0x06000112 RID: 274 RVA: 0x000045A8 File Offset: 0x000027A8
		public string ToTraceString()
		{
			if (this.cachedToTraceString == null)
			{
				this.cachedToTraceString = string.Format("{{Request:'{0}'; Target-Uri:{1}; Method:{2}; Content-Type:{3}; Headers:[{4}]; ClientRequestId:{5}; ComponentId:{6};}}", new object[]
				{
					base.GetType().Name,
					this.Uri,
					base.Method,
					base.ContentType,
					base.Headers.ToTraceString(new string[]
					{
						HttpRequestHeader.Authorization.ToString()
					}),
					this.ClientRequestId,
					this.ComponentId
				});
			}
			return this.cachedToTraceString;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000463B File Offset: 0x0000283B
		internal static PushNotificationProxyRequest CreateMonitoringRequest(ICredentials credentials)
		{
			return new PushNotificationProxyRequest(credentials, null, true);
		}

		// Token: 0x04000057 RID: 87
		private const string JsonContentType = "application/json";

		// Token: 0x04000058 RID: 88
		private const string AuthenticationBearerValue = "Bearer";

		// Token: 0x04000059 RID: 89
		private const string ReturnClientRequestIdHeader = "return-client-request-id";

		// Token: 0x0400005A RID: 90
		private static readonly string PushNotificationsUserAgent = string.Format("Exchange/{0}/PushNotificationsOnPremProxy", ExchangeSetupContext.InstalledVersion);

		// Token: 0x0400005B RID: 91
		private string cachedToTraceString;
	}
}
