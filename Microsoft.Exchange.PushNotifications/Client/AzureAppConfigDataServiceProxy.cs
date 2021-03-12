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
	// Token: 0x02000012 RID: 18
	internal class AzureAppConfigDataServiceProxy : DisposeTrackableBase, IAzureAppConfigDataServiceContract
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00003133 File Offset: 0x00001333
		public AzureAppConfigDataServiceProxy(Uri endPointUri, ICredentials credentials)
		{
			ArgumentValidator.ThrowIfNull("endPointUri", endPointUri);
			this.Client = new HttpClient();
			this.ProxyRequest = new PushNotificationProxyRequest(credentials, new Uri(endPointUri, "PushNotifications/service.svc/AppConfig/GetAppConfigData"));
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003168 File Offset: 0x00001368
		// (set) Token: 0x0600007D RID: 125 RVA: 0x00003170 File Offset: 0x00001370
		public PushNotificationProxyRequest ProxyRequest { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00003179 File Offset: 0x00001379
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00003181 File Offset: 0x00001381
		private HttpClient Client { get; set; }

		// Token: 0x06000080 RID: 128 RVA: 0x0000318C File Offset: 0x0000138C
		public virtual IAsyncResult BeginGetAppConfigData(AzureAppConfigRequestInfo requestConfig, AsyncCallback asyncCallback, object asyncState)
		{
			base.CheckDisposed();
			this.ProxyRequest.ClientRequestId = Guid.NewGuid().ToString();
			this.ProxyRequest.RequestStream = new MemoryStream(Encoding.UTF8.GetBytes(requestConfig.ToJson()));
			return this.Client.BeginDownload(this.ProxyRequest.Uri, this.ProxyRequest, (asyncCallback != null) ? new CancelableAsyncCallback(asyncCallback.Invoke) : null, asyncState);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x0000320C File Offset: 0x0000140C
		public virtual AzureAppConfigResponseInfo EndGetAppConfigData(IAsyncResult result)
		{
			base.CheckDisposed();
			ICancelableAsyncResult asyncResult = result as ICancelableAsyncResult;
			DownloadResult downloadResult = this.Client.EndDownload(asyncResult);
			if (this.ProxyRequest.RequestStream != null)
			{
				this.ProxyRequest.RequestStream.Close();
				this.ProxyRequest.RequestStream = null;
			}
			AzureAppConfigResponseInfo result2 = null;
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
						throw new PushNotificationTransientException(Strings.ExceptionPushNotificationError(this.ProxyRequest.Uri.ToString(), error), downloadResult.Exception);
					}
					throw new PushNotificationPermanentException(Strings.ExceptionPushNotificationError(this.ProxyRequest.Uri.ToString(), error), downloadResult.Exception);
				}
				else
				{
					PushNotificationsLogHelper.LogOnPremPublishingResponse(downloadResult.ResponseHeaders);
					if (downloadResult.ResponseStream != null)
					{
						result2 = JsonConverter.Deserialize<AzureAppConfigResponseInfo>(downloadResult.ResponseStream, null);
					}
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
			return result2;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000033A8 File Offset: 0x000015A8
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.Client != null)
			{
				this.Client.Dispose();
				this.Client = null;
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x000033C7 File Offset: 0x000015C7
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AzureAppConfigDataServiceProxy>(this);
		}
	}
}
