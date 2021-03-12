using System;
using System.Net;
using System.Runtime.Serialization;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000DF RID: 223
	internal class WnsClient : HttpClientBase
	{
		// Token: 0x06000758 RID: 1880 RVA: 0x000173DC File Offset: 0x000155DC
		public WnsClient(HttpClient httpClient) : base(httpClient)
		{
		}

		// Token: 0x06000759 RID: 1881 RVA: 0x000173E5 File Offset: 0x000155E5
		public virtual ICancelableAsyncResult BeginSendAuthRequest(WnsAuthRequest authRequest)
		{
			return base.HttpClient.BeginDownload(authRequest.Uri, authRequest, null, null);
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x000173FC File Offset: 0x000155FC
		public virtual WnsResult<WnsAccessToken> EndSendAuthRequest(ICancelableAsyncResult asyncResult)
		{
			DownloadResult downloadResult = base.HttpClient.EndDownload(asyncResult);
			Exception exception = null;
			if (downloadResult.IsSucceeded)
			{
				try
				{
					try
					{
						WnsAccessToken response = JsonConverter.Deserialize<WnsAccessToken>(downloadResult.ResponseStream, null);
						return new WnsResult<WnsAccessToken>(response, null);
					}
					catch (InvalidDataContractException ex)
					{
						exception = ex;
					}
					catch (SerializationException ex2)
					{
						exception = ex2;
					}
					goto IL_54;
				}
				finally
				{
					downloadResult.ResponseStream.Close();
				}
			}
			exception = downloadResult.Exception;
			IL_54:
			return new WnsResult<WnsAccessToken>(exception);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x00017490 File Offset: 0x00015690
		public virtual ICancelableAsyncResult BeginSendNotificationRequest(WnsRequest notificationRequest)
		{
			return base.HttpClient.BeginDownload(notificationRequest.Uri, notificationRequest, null, null);
		}

		// Token: 0x0600075C RID: 1884 RVA: 0x000174A8 File Offset: 0x000156A8
		public virtual WnsResult<WnsResponse> EndSendNotificationRequest(ICancelableAsyncResult asyncResult)
		{
			DownloadResult downloadResult = base.HttpClient.EndDownload(asyncResult);
			if (downloadResult.IsSucceeded)
			{
				return WnsClient.NotificationSucceeded;
			}
			WebException ex = downloadResult.Exception as WebException;
			if (ex != null)
			{
				HttpWebResponse httpWebResponse = ex.Response as HttpWebResponse;
				if (httpWebResponse != null)
				{
					return new WnsResult<WnsResponse>(WnsResponse.Create(httpWebResponse), downloadResult.Exception);
				}
			}
			return new WnsResult<WnsResponse>(downloadResult.Exception);
		}

		// Token: 0x040003B5 RID: 949
		private static readonly WnsResult<WnsResponse> NotificationSucceeded = new WnsResult<WnsResponse>(WnsResponse.Succeeded, null);
	}
}
