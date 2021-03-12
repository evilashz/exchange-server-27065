using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Client
{
	// Token: 0x02000025 RID: 37
	internal class OnPremPublisherServiceProxy : DisposeTrackableBase, IOnPremPublisherServiceContract
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00004176 File Offset: 0x00002376
		public OnPremPublisherServiceProxy(Uri endPointUri, ICredentials credentials = null) : this(endPointUri, credentials, false)
		{
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004184 File Offset: 0x00002384
		private OnPremPublisherServiceProxy(Uri endPointUri, ICredentials credentials, bool isMonitoring)
		{
			ArgumentValidator.ThrowIfNull("endPointUri", endPointUri);
			this.Client = new HttpClient();
			this.Uri = new Uri(endPointUri, "PushNotifications/service.svc/PublishOnPremNotifications");
			if (isMonitoring)
			{
				this.ProxyRequest = PushNotificationProxyRequest.CreateMonitoringRequest(credentials);
				return;
			}
			this.ProxyRequest = new PushNotificationProxyRequest(credentials, null);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000041DB File Offset: 0x000023DB
		// (set) Token: 0x060000FE RID: 254 RVA: 0x000041E3 File Offset: 0x000023E3
		private Uri Uri { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000041EC File Offset: 0x000023EC
		// (set) Token: 0x06000100 RID: 256 RVA: 0x000041F4 File Offset: 0x000023F4
		private HttpClient Client { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000101 RID: 257 RVA: 0x000041FD File Offset: 0x000023FD
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00004205 File Offset: 0x00002405
		private PushNotificationProxyRequest ProxyRequest { get; set; }

		// Token: 0x06000103 RID: 259 RVA: 0x00004210 File Offset: 0x00002410
		public virtual IAsyncResult BeginPublishOnPremNotifications(MailboxNotificationBatch notifications, AsyncCallback asyncCallback, object asyncState)
		{
			base.CheckDisposed();
			this.ProxyRequest.ClientRequestId = Guid.NewGuid().ToString();
			this.ProxyRequest.RequestStream = new MemoryStream(Encoding.UTF8.GetBytes(notifications.ToJson()));
			return this.Client.BeginDownload(this.Uri, this.ProxyRequest, (asyncCallback != null) ? new CancelableAsyncCallback(asyncCallback.Invoke) : null, asyncState);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x0000428C File Offset: 0x0000248C
		public virtual void EndPublishOnPremNotifications(IAsyncResult result)
		{
			base.CheckDisposed();
			ICancelableAsyncResult asyncResult = result as ICancelableAsyncResult;
			DownloadResult downloadResult = this.Client.EndDownload(asyncResult);
			if (this.ProxyRequest.RequestStream != null)
			{
				this.ProxyRequest.RequestStream.Close();
				this.ProxyRequest.RequestStream = null;
			}
			try
			{
				if (!downloadResult.IsSucceeded)
				{
					PushNotificationsLogHelper.LogOnPremPublishingError(downloadResult.Exception, downloadResult.ResponseHeaders);
					string text = null;
					if (downloadResult.ResponseStream != null)
					{
						try
						{
							throw new PushNotificationServerException<PushNotificationFault>(JsonConverter.Deserialize<PushNotificationFault>(downloadResult.ResponseStream, null), downloadResult.Exception);
						}
						catch (InvalidDataContractException)
						{
						}
						catch (SerializationException)
						{
						}
						using (StreamReader streamReader = new StreamReader(downloadResult.ResponseStream))
						{
							text = streamReader.ReadToEnd();
						}
					}
					string error = text ?? downloadResult.Exception.Message;
					if (downloadResult.IsRetryable)
					{
						throw new PushNotificationTransientException(Strings.ExceptionPushNotificationError(this.Uri.ToString(), error), downloadResult.Exception);
					}
					throw new PushNotificationPermanentException(Strings.ExceptionPushNotificationError(this.Uri.ToString(), error), downloadResult.Exception);
				}
				else
				{
					PushNotificationsLogHelper.LogOnPremPublishingResponse(downloadResult.ResponseHeaders);
				}
			}
			finally
			{
				if (downloadResult.ResponseStream != null)
				{
					downloadResult.ResponseStream.Dispose();
					downloadResult.ResponseStream = null;
				}
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004400 File Offset: 0x00002600
		internal static OnPremPublisherServiceProxy CreateMonitoringProxy(Uri endPointUri, ICredentials credentials = null, string certValidationComponent = null)
		{
			OnPremPublisherServiceProxy onPremPublisherServiceProxy = new OnPremPublisherServiceProxy(endPointUri, credentials, true);
			if (!string.IsNullOrEmpty(certValidationComponent))
			{
				onPremPublisherServiceProxy.ProxyRequest.ComponentId = certValidationComponent;
			}
			return onPremPublisherServiceProxy;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000442B File Offset: 0x0000262B
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.Client != null)
			{
				this.Client.Dispose();
				this.Client = null;
			}
		}

		// Token: 0x06000107 RID: 263 RVA: 0x0000444A File Offset: 0x0000264A
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<OnPremPublisherServiceProxy>(this);
		}
	}
}
