using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200009B RID: 155
	internal class GcmClient : HttpClientBase
	{
		// Token: 0x0600057B RID: 1403 RVA: 0x000126A4 File Offset: 0x000108A4
		public GcmClient(Uri gcmUri, HttpClient httpClient) : base(httpClient)
		{
			ArgumentValidator.ThrowIfNull("gcmUri", gcmUri);
			this.GcmUri = gcmUri;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600057C RID: 1404 RVA: 0x000126BF File Offset: 0x000108BF
		// (set) Token: 0x0600057D RID: 1405 RVA: 0x000126C7 File Offset: 0x000108C7
		private Uri GcmUri { get; set; }

		// Token: 0x0600057E RID: 1406 RVA: 0x000126D0 File Offset: 0x000108D0
		public virtual ICancelableAsyncResult BeginSendNotification(GcmRequest notificationRequest)
		{
			return base.HttpClient.BeginDownload(this.GcmUri, notificationRequest, null, null);
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x000126E8 File Offset: 0x000108E8
		public virtual GcmResponse EndSendNotification(ICancelableAsyncResult asyncResult)
		{
			DownloadResult downloadResult = base.HttpClient.EndDownload(asyncResult);
			GcmResponse result;
			using (downloadResult.ResponseStream)
			{
				Exception ex = null;
				if (downloadResult.IsSucceeded)
				{
					try
					{
						StreamReader streamReader = new StreamReader(downloadResult.ResponseStream);
						string responseBody = streamReader.ReadToEnd();
						return new GcmResponse(responseBody);
					}
					catch (ArgumentNullException ex2)
					{
						ex = ex2;
						goto IL_5C;
					}
					catch (ArgumentException ex3)
					{
						ex = ex3;
						goto IL_5C;
					}
					catch (IOException ex4)
					{
						ex = ex4;
						goto IL_5C;
					}
				}
				ex = downloadResult.Exception;
				IL_5C:
				WebException ex5 = ex as WebException;
				result = ((ex5 != null) ? new GcmResponse(ex5) : new GcmResponse(ex));
			}
			return result;
		}
	}
}
