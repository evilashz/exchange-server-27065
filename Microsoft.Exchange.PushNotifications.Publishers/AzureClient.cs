using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000044 RID: 68
	internal class AzureClient : HttpClientBase
	{
		// Token: 0x06000297 RID: 663 RVA: 0x00009C54 File Offset: 0x00007E54
		public AzureClient(HttpClient httpClient) : base(httpClient)
		{
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00009C5D File Offset: 0x00007E5D
		public virtual ICancelableAsyncResult BeginSendNotificationRequest(AzureSendRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			return base.HttpClient.BeginDownload(request.Uri, request, null, null);
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00009C7E File Offset: 0x00007E7E
		public virtual AzureResponse EndSendNotificationRequest(ICancelableAsyncResult asyncResult)
		{
			return this.EndRequest(asyncResult);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00009C87 File Offset: 0x00007E87
		public virtual ICancelableAsyncResult BeginReadRegistrationRequest(AzureReadRegistrationRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			return base.HttpClient.BeginDownload(request.Uri, request, null, null);
		}

		// Token: 0x0600029B RID: 667 RVA: 0x00009CA8 File Offset: 0x00007EA8
		public virtual AzureReadRegistrationResponse EndReadRegistrationRequest(ICancelableAsyncResult asyncResult)
		{
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			DownloadResult result = base.HttpClient.EndDownload(asyncResult);
			string responseBody = this.GetResponseBody(result);
			if (result.IsSucceeded)
			{
				return new AzureReadRegistrationResponse(responseBody, result.ResponseHeaders);
			}
			WebException ex = result.Exception as WebException;
			if (ex == null)
			{
				return new AzureReadRegistrationResponse(result.Exception, result.LastKnownRequestedUri, responseBody);
			}
			return new AzureReadRegistrationResponse(ex, result.LastKnownRequestedUri, responseBody);
		}

		// Token: 0x0600029C RID: 668 RVA: 0x00009D1F File Offset: 0x00007F1F
		public virtual ICancelableAsyncResult BeginNewRegistrationRequest(AzureNewRegistrationRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			return base.HttpClient.BeginDownload(request.Uri, request, null, null);
		}

		// Token: 0x0600029D RID: 669 RVA: 0x00009D40 File Offset: 0x00007F40
		public virtual AzureResponse EndNewRegistrationRequest(ICancelableAsyncResult asyncResult)
		{
			return this.EndRequest(asyncResult);
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00009D49 File Offset: 0x00007F49
		public virtual ICancelableAsyncResult BeginCreateOrUpdateRegistrationRequest(AzureCreateOrUpdateRegistrationRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			return base.HttpClient.BeginDownload(request.Uri, request, null, null);
		}

		// Token: 0x0600029F RID: 671 RVA: 0x00009D6A File Offset: 0x00007F6A
		public virtual AzureResponse EndCreateOrUpdateRegistrationRequest(ICancelableAsyncResult asyncResult)
		{
			return this.EndRequest(asyncResult);
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00009D73 File Offset: 0x00007F73
		public virtual ICancelableAsyncResult BeginNewRegistrationIdRequest(AzureNewRegistrationIdRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			return base.HttpClient.BeginDownload(request.Uri, request, null, null);
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00009D94 File Offset: 0x00007F94
		public virtual AzureNewRegistrationIdResponse EndNewRegistrationIdRequest(ICancelableAsyncResult asyncResult)
		{
			DownloadResult result = base.HttpClient.EndDownload(asyncResult);
			string responseBody = this.GetResponseBody(result);
			if (result.IsSucceeded)
			{
				return new AzureNewRegistrationIdResponse(responseBody, result.ResponseHeaders);
			}
			WebException ex = result.Exception as WebException;
			if (ex == null)
			{
				return new AzureNewRegistrationIdResponse(result.Exception, result.LastKnownRequestedUri, responseBody);
			}
			return new AzureNewRegistrationIdResponse(ex, result.LastKnownRequestedUri, responseBody);
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00009E00 File Offset: 0x00008000
		public virtual ICancelableAsyncResult BeginSendAuthRequest(AcsAuthRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			return base.HttpClient.BeginDownload(request.Uri, request, null, null);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00009E21 File Offset: 0x00008021
		public virtual AzureResponse EndSendAuthRequest(ICancelableAsyncResult asyncResult)
		{
			return this.EndRequest(asyncResult);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00009E2A File Offset: 0x0000802A
		public virtual ICancelableAsyncResult BeginHubCretionRequest(AzureHubCreationRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			return base.HttpClient.BeginDownload(request.Uri, request, null, null);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00009E4B File Offset: 0x0000804B
		public virtual AzureResponse EndHubCretionRequest(ICancelableAsyncResult asyncResult)
		{
			return this.EndRequest(asyncResult);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00009E54 File Offset: 0x00008054
		public virtual ICancelableAsyncResult BeginRegistrationChallengeRequest(AzureRegistrationChallengeRequest request)
		{
			ArgumentValidator.ThrowIfNull("request", request);
			return base.HttpClient.BeginDownload(request.Uri, request, null, null);
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00009E75 File Offset: 0x00008075
		public virtual AzureResponse EndRegistrationChallengeRequest(ICancelableAsyncResult asyncResult)
		{
			return this.EndRequest(asyncResult);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00009E80 File Offset: 0x00008080
		private AzureResponse EndRequest(ICancelableAsyncResult asyncResult)
		{
			ArgumentValidator.ThrowIfNull("asyncResult", asyncResult);
			DownloadResult result = base.HttpClient.EndDownload(asyncResult);
			string responseBody = this.GetResponseBody(result);
			if (result.IsSucceeded)
			{
				return new AzureResponse(responseBody, result.ResponseHeaders);
			}
			WebException ex = result.Exception as WebException;
			if (ex == null)
			{
				return new AzureResponse(result.Exception, result.LastKnownRequestedUri, responseBody);
			}
			return new AzureResponse(ex, result.LastKnownRequestedUri, responseBody);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00009EF8 File Offset: 0x000080F8
		private string GetResponseBody(DownloadResult result)
		{
			string result2 = string.Empty;
			if (result.ResponseStream != null)
			{
				using (StreamReader streamReader = new StreamReader(result.ResponseStream))
				{
					result2 = streamReader.ReadToEnd();
				}
			}
			return result2;
		}
	}
}
