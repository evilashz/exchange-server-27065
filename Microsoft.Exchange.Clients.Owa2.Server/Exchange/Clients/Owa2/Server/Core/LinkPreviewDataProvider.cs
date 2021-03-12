using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002E6 RID: 742
	internal abstract class LinkPreviewDataProvider
	{
		// Token: 0x060018FB RID: 6395 RVA: 0x00056D50 File Offset: 0x00054F50
		public LinkPreviewDataProvider(Uri uri, GetLinkPreviewRequest request, RequestDetailsLogger logger)
		{
			this.uri = uri;
			this.request = request;
			this.logger = logger;
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060018FC RID: 6396 RVA: 0x00056D6D File Offset: 0x00054F6D
		// (set) Token: 0x060018FD RID: 6397 RVA: 0x00056D75 File Offset: 0x00054F75
		public long ContentLength { get; set; }

		// Token: 0x060018FE RID: 6398
		public abstract GetLinkPreviewResponse CreatePreview(DataProviderInformation dataProviderInformation);

		// Token: 0x060018FF RID: 6399
		protected abstract Task<DataProviderInformation> GetDataProviderInformation(HttpClient httpClient);

		// Token: 0x06001900 RID: 6400
		protected abstract void RequireContentType(MediaTypeHeaderValue contentType);

		// Token: 0x06001901 RID: 6401
		protected abstract void RestrictContentLength(long? contentLength);

		// Token: 0x06001902 RID: 6402
		protected abstract int GetMaxByteCount(Uri responseUri);

		// Token: 0x06001903 RID: 6403 RVA: 0x00056F5C File Offset: 0x0005515C
		public async Task<DataProviderInformation> GetDataProviderInformation()
		{
			DataProviderInformation result;
			using (HttpClientHandler httpClientHandler = new HttpClientHandler())
			{
				httpClientHandler.CookieContainer = new CookieContainer();
				using (HttpClient httpClient = new HttpClient(httpClientHandler))
				{
					httpClient.Timeout = TimeSpan.FromMilliseconds(6000.0);
					httpClient.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
					httpClient.DefaultRequestHeaders.Add("accept", "text/html, application/xhtml+xml, */*");
					result = await this.GetDataProviderInformation(httpClient);
				}
			}
			return result;
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x00056FA4 File Offset: 0x000551A4
		public static LinkPreviewDataProvider GetDataProvider(GetLinkPreviewRequest request, RequestDetailsLogger logger, bool activeViewsConvergenceEnabled)
		{
			Uri uri = LinkPreviewDataProvider.CreateUri(request.Url);
			LinkPreviewDataProvider result;
			Uri uri2;
			if (activeViewsConvergenceEnabled && OEmbedVideoPreviewBuilder.IsOEmbedVideoUri(uri, logger))
			{
				result = new OEmbedDataProvider(uri, request, logger);
			}
			else if (WikipediaLinkPreviewBuilder.TryGetWikipediaServiceUri(uri, out uri2))
			{
				result = new WikipediaDataProvider(uri2, request, logger);
			}
			else
			{
				result = new WebPageDataProvider(uri, request, logger);
			}
			return result;
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0005748C File Offset: 0x0005568C
		protected async Task<DataProviderInformation> MakeAndProcessHttpRequest(HttpClient httpClient, Uri requestUri, LinkPreviewDataProvider.ProcessResponseStreamDelegate processResponseStream)
		{
			DataProviderInformation dataProviderInformation;
			using (HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead))
			{
				if (!httpResponseMessage.IsSuccessStatusCode)
				{
					GetLinkPreview.ThrowInvalidRequestException(httpResponseMessage);
				}
				this.RequireContentType(httpResponseMessage.Content.Headers.ContentType);
				this.RestrictContentLength(httpResponseMessage.Content.Headers.ContentLength);
				this.ContentLength = ((httpResponseMessage.Content.Headers.ContentLength != null) ? httpResponseMessage.Content.Headers.ContentLength.Value : 0L);
				Encoding responseHeaderEncoding = this.GetResponseHeaderEncoding(httpResponseMessage.Content.Headers.ContentType.CharSet);
				Uri responseUri = httpResponseMessage.RequestMessage.RequestUri;
				int maxByteCount = this.GetMaxByteCount(responseUri);
				using (MemoryStream memoryStream = new MemoryStream(maxByteCount))
				{
					using (Stream responseStream = await httpResponseMessage.Content.ReadAsStreamAsync())
					{
						byte[] buffer = new byte[1024];
						int readCount = 0;
						int totalReadCount = 0;
						do
						{
							readCount = await responseStream.ReadAsync(buffer, 0, buffer.Length);
							memoryStream.Write(buffer, 0, readCount);
							totalReadCount += readCount;
						}
						while (readCount > 0 && totalReadCount < maxByteCount);
						if (totalReadCount <= 0)
						{
							GetLinkPreview.ThrowInvalidRequestException("EmptyContent", "Url returns no content.");
						}
					}
					dataProviderInformation = processResponseStream(responseUri, responseHeaderEncoding, memoryStream, this.logger);
				}
			}
			return dataProviderInformation;
		}

		// Token: 0x06001906 RID: 6406 RVA: 0x000574EC File Offset: 0x000556EC
		private Encoding GetResponseHeaderEncoding(string characterSet)
		{
			Encoding result = null;
			if (characterSet != null)
			{
				try
				{
					return Encoding.GetEncoding(characterSet);
				}
				catch (ArgumentException)
				{
					GetLinkPreview.ThrowInvalidRequestException("GetEncodingFailed", string.Format("Get encoding failed for {0}", characterSet));
					return result;
				}
			}
			result = Encoding.GetEncoding("ISO-8859-1");
			return result;
		}

		// Token: 0x06001907 RID: 6407 RVA: 0x0005753C File Offset: 0x0005573C
		private static Uri CreateUri(string url)
		{
			Uri uri = null;
			if (Uri.TryCreate(url, UriKind.Absolute, out uri))
			{
				string text = null;
				try
				{
					IdnMapping idnMapping = new IdnMapping();
					text = idnMapping.GetAscii(uri.Host);
				}
				catch (ArgumentException)
				{
					GetLinkPreview.ThrowInvalidRequestException("InvalidUrl", "Request url is invalid");
				}
				if (string.CompareOrdinal(text, uri.Host) != 0)
				{
					uri = new UriBuilder(uri)
					{
						Host = text
					}.Uri;
				}
				return uri;
			}
			GetLinkPreview.ThrowInvalidRequestException("InvalidUrl", "Request url is invalid");
			return uri;
		}

		// Token: 0x04000DA2 RID: 3490
		private const string DefaultCharSet = "ISO-8859-1";

		// Token: 0x04000DA3 RID: 3491
		protected const string HtmlContentType = "text/html";

		// Token: 0x04000DA4 RID: 3492
		protected const string XHtmlContentType = "application/xhtml+xml";

		// Token: 0x04000DA5 RID: 3493
		protected const string XmlContentType = "text/xml";

		// Token: 0x04000DA6 RID: 3494
		protected const int MaxContentLength = 524288;

		// Token: 0x04000DA7 RID: 3495
		protected const int MaxByteCount = 32768;

		// Token: 0x04000DA8 RID: 3496
		protected const int RequestTimeoutInterval = 6000;

		// Token: 0x04000DA9 RID: 3497
		protected Uri uri;

		// Token: 0x04000DAA RID: 3498
		protected readonly RequestDetailsLogger logger;

		// Token: 0x04000DAB RID: 3499
		protected readonly GetLinkPreviewRequest request;

		// Token: 0x020002E7 RID: 743
		// (Invoke) Token: 0x06001909 RID: 6409
		protected delegate DataProviderInformation ProcessResponseStreamDelegate(Uri responseUri, Encoding responseHeaderEncoding, MemoryStream memoryStream, RequestDetailsLogger logger);
	}
}
