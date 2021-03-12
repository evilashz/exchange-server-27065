using System;
using System.Net;

namespace Microsoft.Exchange.Services.OnlineMeetings
{
	// Token: 0x0200003A RID: 58
	internal abstract class UcwaRequestFactory
	{
		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000219 RID: 537
		public abstract string LandingPageToken { get; }

		// Token: 0x0600021A RID: 538 RVA: 0x00007A90 File Offset: 0x00005C90
		internal virtual UcwaWebRequest CreateRequest(string httpMethod, string url)
		{
			return new UcwaWebRequest(this.CreateHttpWebRequest(httpMethod, url));
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00007AA0 File Offset: 0x00005CA0
		protected virtual HttpWebRequest CreateHttpWebRequest(string httpMethod, string url)
		{
			if (httpMethod == null)
			{
				throw new ArgumentNullException("httpMethod");
			}
			if (!UcwaHttpMethod.IsSupportedMethod(httpMethod))
			{
				throw new ArgumentOutOfRangeException("httpMethod");
			}
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			Uri uri = new Uri(url, UriKind.Absolute);
			if (!uri.IsAbsoluteUri)
			{
				throw new ArgumentException("Argument must be an absolute URI", "url");
			}
			HttpWebRequest httpWebRequest = WebRequest.Create(url) as HttpWebRequest;
			if (httpWebRequest == null)
			{
				throw new InvalidOperationException("HttpWebRequest could not be created");
			}
			httpWebRequest.Accept = "application/xml";
			httpWebRequest.AllowAutoRedirect = true;
			httpWebRequest.MaximumAutomaticRedirections = 3;
			httpWebRequest.Method = UcwaHttpMethod.Normalize(httpMethod);
			httpWebRequest.Timeout = 30000;
			httpWebRequest.UserAgent = "UCWA Online Meeting Scheduler Library";
			if (UcwaHttpMethod.IsPatchMethod(httpMethod) || UcwaHttpMethod.IsPostMethod(httpMethod) || UcwaHttpMethod.IsPutMethod(httpMethod))
			{
				httpWebRequest.ContentType = "application/xml";
			}
			return httpWebRequest;
		}

		// Token: 0x04000166 RID: 358
		public const string UserAgentDefault = "UCWA Online Meeting Scheduler Library";

		// Token: 0x04000167 RID: 359
		private const int RequestTimeoutDefault = 30000;

		// Token: 0x04000168 RID: 360
		private const string ContentTypeDefault = "application/xml";

		// Token: 0x04000169 RID: 361
		private const int MaximumAutomaticRedirectionsDefault = 3;

		// Token: 0x0400016A RID: 362
		private const bool AllowAutoRedirectDefault = true;
	}
}
