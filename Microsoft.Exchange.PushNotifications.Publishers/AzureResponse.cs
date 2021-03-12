using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Extensions;
using Microsoft.Exchange.PushNotifications.Extensions;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000054 RID: 84
	internal class AzureResponse
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000B1A3 File Offset: 0x000093A3
		public AzureResponse(string responseBody, WebHeaderCollection responseHeaders = null)
		{
			this.OriginalStatusCode = new HttpStatusCode?(HttpStatusCode.OK);
			this.OriginalBody = responseBody;
			this.ResponseHeaders = responseHeaders;
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000B1C9 File Offset: 0x000093C9
		public AzureResponse(Exception exception, Uri targetUri, string responseBody)
		{
			ArgumentValidator.ThrowIfNull("exception", exception);
			ArgumentValidator.ThrowIfNull("targetUri", targetUri);
			this.Exception = exception;
			this.TargetUri = targetUri;
			this.OriginalBody = responseBody;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000B1FC File Offset: 0x000093FC
		public AzureResponse(WebException webException, Uri targetUri, string responseBody)
		{
			ArgumentValidator.ThrowIfNull("webException", webException);
			ArgumentValidator.ThrowIfNull("targetUri", targetUri);
			this.Exception = webException;
			this.TargetUri = targetUri;
			this.OriginalBody = responseBody;
			HttpWebResponse httpWebResponse = webException.Response as HttpWebResponse;
			if (httpWebResponse != null)
			{
				this.OriginalStatusCode = new HttpStatusCode?(httpWebResponse.StatusCode);
			}
			if (responseBody == null && webException.Response != null)
			{
				using (StreamReader streamReader = new StreamReader(webException.Response.GetResponseStream()))
				{
					this.OriginalBody = streamReader.ReadToEnd();
				}
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000331 RID: 817 RVA: 0x0000B2A0 File Offset: 0x000094A0
		// (set) Token: 0x06000332 RID: 818 RVA: 0x0000B2A8 File Offset: 0x000094A8
		public Exception Exception { get; private set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0000B2B1 File Offset: 0x000094B1
		// (set) Token: 0x06000334 RID: 820 RVA: 0x0000B2B9 File Offset: 0x000094B9
		public Uri TargetUri { get; private set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0000B2C2 File Offset: 0x000094C2
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0000B2CA File Offset: 0x000094CA
		public HttpStatusCode? OriginalStatusCode { get; private set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0000B2D3 File Offset: 0x000094D3
		// (set) Token: 0x06000338 RID: 824 RVA: 0x0000B2DB File Offset: 0x000094DB
		public string OriginalBody { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000B2E4 File Offset: 0x000094E4
		// (set) Token: 0x0600033A RID: 826 RVA: 0x0000B2EC File Offset: 0x000094EC
		public WebHeaderCollection ResponseHeaders { get; private set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600033B RID: 827 RVA: 0x0000B2F8 File Offset: 0x000094F8
		public bool HasSucceeded
		{
			get
			{
				return this.OriginalStatusCode != null && this.OriginalStatusCode.Value >= HttpStatusCode.OK && this.OriginalStatusCode.Value < HttpStatusCode.MultipleChoices;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000B344 File Offset: 0x00009544
		public string ToTraceString()
		{
			if (this.toFullString == null)
			{
				this.toFullString = string.Format("{{ Target-Uri:{0}; Statue-Code:{1}; Response-Body:{2}; Headers:[{3}]; Exception:{4}; {5}}}", new object[]
				{
					this.TargetUri,
					this.OriginalStatusCode.ToNullableString<HttpStatusCode>(),
					this.OriginalBody.ToNullableString(),
					this.ResponseHeaders.ToTraceString(null),
					this.Exception.ToTraceString(),
					this.InternalToTraceString()
				});
			}
			return this.toFullString;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000B3C2 File Offset: 0x000095C2
		protected virtual string InternalToTraceString()
		{
			return null;
		}

		// Token: 0x04000159 RID: 345
		private string toFullString;
	}
}
