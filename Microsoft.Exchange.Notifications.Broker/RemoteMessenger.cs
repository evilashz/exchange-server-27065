using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.Security.OAuth;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000031 RID: 49
	[ClassAccessLevel(AccessLevel.Implementation)]
	public abstract class RemoteMessenger
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x0000B45C File Offset: 0x0000965C
		static RemoteMessenger()
		{
			try
			{
				RemoteMessenger.RemoteMessengerUserAgent = string.Format("Exchange/{0}/NotificationsBroker", Assembly.GetExecutingAssembly().GetName().Version);
			}
			catch
			{
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000B4A8 File Offset: 0x000096A8
		// (set) Token: 0x060001F5 RID: 501 RVA: 0x0000B4B0 File Offset: 0x000096B0
		public Uri Url { get; private set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001F6 RID: 502 RVA: 0x0000B4B9 File Offset: 0x000096B9
		// (set) Token: 0x060001F7 RID: 503 RVA: 0x0000B4C1 File Offset: 0x000096C1
		public string GroupingKey { get; private set; }

		// Token: 0x060001F8 RID: 504 RVA: 0x0000B5B0 File Offset: 0x000097B0
		public async Task<RemoteCommandStatus> SendMessageAsync(BrokerSubscription subscription)
		{
			return await this.InternalSendMessageAsync(this.CreateSubscriptionMessage(subscription));
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x0000B6E4 File Offset: 0x000098E4
		public async Task<RemoteCommandStatus> SendMessageAsync(NotificationBatch notificationBatch)
		{
			return await this.InternalSendMessageAsync(this.CreateNotificationMessage(notificationBatch));
		}

		// Token: 0x060001FA RID: 506
		protected abstract void SetCredentials(HttpWebRequest webRequest);

		// Token: 0x060001FB RID: 507
		protected abstract void SetServerCertificateValidationCallback(HttpWebRequest webRequest);

		// Token: 0x060001FC RID: 508 RVA: 0x0000B734 File Offset: 0x00009934
		private InterbrokerMessage CreateNotificationMessage(NotificationBatch batch)
		{
			return new NotificationMessage
			{
				Version = BrokerConfiguration.MaximumProtocolVersion,
				Notifications = batch.Notifications
			};
		}

		// Token: 0x060001FD RID: 509 RVA: 0x0000B760 File Offset: 0x00009960
		private InterbrokerMessage CreateSubscriptionMessage(BrokerSubscription brokerSubscription)
		{
			return new SubscriptionMessage
			{
				Version = BrokerConfiguration.MaximumProtocolVersion,
				Subscription = brokerSubscription
			};
		}

		// Token: 0x060001FE RID: 510 RVA: 0x0000BBE0 File Offset: 0x00009DE0
		private async Task<RemoteCommandStatus> InternalSendMessageAsync(InterbrokerMessage msg)
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug<Uri>((long)this.GetHashCode(), "SendMessageAsync start, sending message to {0}", this.Url);
			HttpWebResponse httpResponse = null;
			RemoteCommandStatus remoteCommandStatus = RemoteCommandStatus.UnsupportedCommand;
			try
			{
				HttpWebRequest webRequest = WebRequest.CreateHttp(this.Url);
				webRequest.Method = HttpMethod.Post.Method;
				webRequest.UserAgent = RemoteMessenger.RemoteMessengerUserAgent;
				Server localServer = LocalServerCache.LocalServer;
				if (localServer != null && localServer.InternetWebProxy != null)
				{
					webRequest.Proxy = new WebProxy(localServer.InternetWebProxy);
				}
				this.SetServerCertificateValidationCallback(webRequest);
				this.SetCredentials(webRequest);
				string msgContent = msg.ToJson();
				byte[] msgByte = Encoding.UTF8.GetBytes(msgContent);
				using (Stream requestStream = await webRequest.GetRequestStreamAsync())
				{
					await requestStream.WriteAsync(msgByte, 0, msgByte.Length);
				}
				httpResponse = ((await webRequest.GetResponseAsync()) as HttpWebResponse);
			}
			catch (Exception ex)
			{
				ExTraceGlobals.RemoteConduitTracer.TraceDebug<string>((long)this.GetHashCode(), "SendMessageAsync: Exception caught: {0}", ex.ToString());
				BrokerLogger.Set(LogField.Exception, ex);
				WebException ex2 = ex as WebException;
				if (ex2 != null)
				{
					httpResponse = (ex2.Response as HttpWebResponse);
				}
			}
			finally
			{
				if (httpResponse != null)
				{
					ExTraceGlobals.RemoteConduitTracer.TraceDebug<HttpStatusCode>((long)this.GetHashCode(), "SendMessageAsync: HTTP status = {0}", httpResponse.StatusCode);
					remoteCommandStatus = (RemoteCommandStatus)httpResponse.StatusCode;
					httpResponse.Dispose();
				}
			}
			ExTraceGlobals.RemoteConduitTracer.TraceDebug<RemoteCommandStatus>((long)this.GetHashCode(), "SendMessageAsync end, remoteCommandStatus = {0}", remoteCommandStatus);
			return remoteCommandStatus;
		}

		// Token: 0x040000E1 RID: 225
		private static readonly string RemoteMessengerUserAgent = "NotificationsBroker";

		// Token: 0x02000032 RID: 50
		public sealed class BackendToBackendRemoteMessenger : RemoteMessenger
		{
			// Token: 0x06000200 RID: 512 RVA: 0x0000BC38 File Offset: 0x00009E38
			public BackendToBackendRemoteMessenger(string host, string methodName)
			{
				UriBuilder uriBuilder = new UriBuilder("https", host, 444, BrokerConfiguration.VdirName.Value + "/" + methodName);
				base.Url = uriBuilder.Uri;
				base.GroupingKey = host;
			}

			// Token: 0x06000201 RID: 513 RVA: 0x0000BC84 File Offset: 0x00009E84
			protected override void SetCredentials(HttpWebRequest webRequest)
			{
				webRequest.PreAuthenticate = true;
				webRequest.UseDefaultCredentials = true;
			}

			// Token: 0x06000202 RID: 514 RVA: 0x0000BC94 File Offset: 0x00009E94
			protected override void SetServerCertificateValidationCallback(HttpWebRequest webRequest)
			{
				webRequest.ServerCertificateValidationCallback = RemoteMessenger.BackendToBackendRemoteMessenger.allowInternalUntrustedCert;
			}

			// Token: 0x040000E4 RID: 228
			private static readonly RemoteCertificateValidationCallback allowInternalUntrustedCert = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true;
		}

		// Token: 0x02000033 RID: 51
		public sealed class CrossDeploymentRemoteMessenger : RemoteMessenger
		{
			// Token: 0x06000205 RID: 517 RVA: 0x0000BCC8 File Offset: 0x00009EC8
			public CrossDeploymentRemoteMessenger(Uri discoveryEndPoint, OrganizationId organizationId, string anchorMailbox, string methodName)
			{
				UriBuilder uriBuilder = new UriBuilder(discoveryEndPoint);
				uriBuilder.Path = BrokerConfiguration.VdirName.Value + "/" + methodName;
				this.organizationId = organizationId;
				this.anchorMailbox = anchorMailbox;
				base.Url = uriBuilder.Uri;
				base.GroupingKey = string.Format("{0}+{1}", base.Url.Authority, this.anchorMailbox);
			}

			// Token: 0x06000206 RID: 518 RVA: 0x0000BD3C File Offset: 0x00009F3C
			protected override void SetCredentials(HttpWebRequest webRequest)
			{
				webRequest.PreAuthenticate = true;
				webRequest.Credentials = OAuthCredentials.GetOAuthCredentialsForAppToken(this.organizationId, SmtpAddress.Parse(this.anchorMailbox).Domain);
				webRequest.Headers.Add(WellKnownHeader.AnchorMailbox, this.anchorMailbox);
				webRequest.Headers.Add("Authorization", "Bearer");
			}

			// Token: 0x06000207 RID: 519 RVA: 0x0000BD9F File Offset: 0x00009F9F
			protected override void SetServerCertificateValidationCallback(HttpWebRequest webRequest)
			{
				webRequest.ServerCertificateValidationCallback = RemoteMessenger.CrossDeploymentRemoteMessenger.callback;
			}

			// Token: 0x040000E6 RID: 230
			private static readonly RemoteCertificateValidationCallback callback = (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => sslPolicyErrors == SslPolicyErrors.None || SslConfiguration.AllowExternalUntrustedCerts;

			// Token: 0x040000E7 RID: 231
			private readonly OrganizationId organizationId;

			// Token: 0x040000E8 RID: 232
			private readonly string anchorMailbox;
		}
	}
}
