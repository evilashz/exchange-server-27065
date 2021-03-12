using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002EE RID: 750
	internal class WebPageDataProvider : LinkPreviewDataProvider
	{
		// Token: 0x06001949 RID: 6473 RVA: 0x00057EE7 File Offset: 0x000560E7
		public WebPageDataProvider(Uri uri, GetLinkPreviewRequest request, RequestDetailsLogger logger) : base(uri, request, logger)
		{
		}

		// Token: 0x0600194A RID: 6474 RVA: 0x00057EF4 File Offset: 0x000560F4
		public override GetLinkPreviewResponse CreatePreview(DataProviderInformation dataProviderInformation)
		{
			string text = ((WebPageInformation)dataProviderInformation).Text;
			Dictionary<string, string> queryParmDictionary;
			LinkPreviewBuilder linkPreviewBuilder;
			if (YouTubeLinkPreviewBuilder.TryGetYouTubePlayerQueryParms(dataProviderInformation.ResponseUri, this.logger, out queryParmDictionary))
			{
				linkPreviewBuilder = new YouTubeLinkPreviewBuilder(queryParmDictionary, this.request, text, this.logger, dataProviderInformation.ResponseUri);
			}
			else if (AmazonLinkPreviewBuilder.IsAmazonUri(dataProviderInformation.ResponseUri))
			{
				linkPreviewBuilder = new AmazonLinkPreviewBuilder(this.request, text, this.logger, dataProviderInformation.ResponseUri);
			}
			else if (CraigsListLinkPreviewBuilder.IsCraigsListUri(dataProviderInformation.ResponseUri))
			{
				linkPreviewBuilder = new CraigsListLinkPreviewBuilder(this.request, text, this.logger, dataProviderInformation.ResponseUri);
			}
			else
			{
				linkPreviewBuilder = new WebPageLinkPreviewBuilder(this.request, text, this.logger, dataProviderInformation.ResponseUri, false);
			}
			return linkPreviewBuilder.Execute();
		}

		// Token: 0x0600194B RID: 6475 RVA: 0x000580A0 File Offset: 0x000562A0
		protected override async Task<DataProviderInformation> GetDataProviderInformation(HttpClient httpClient)
		{
			return await base.MakeAndProcessHttpRequest(httpClient, this.uri, new LinkPreviewDataProvider.ProcessResponseStreamDelegate(WebPageDataProvider.ProcessResponseStream));
		}

		// Token: 0x0600194C RID: 6476 RVA: 0x000580F0 File Offset: 0x000562F0
		public static DataProviderInformation ProcessResponseStream(Uri responseUri, Encoding responseHeaderEncoding, MemoryStream memoryStream, RequestDetailsLogger logger)
		{
			byte[] buffer = memoryStream.GetBuffer();
			string @string = WebPageDataProvider.GetString(responseHeaderEncoding, buffer);
			Encoding webPageEncoding = WebPageDataProvider.GetWebPageEncoding(@string, logger);
			if (webPageEncoding != null && !responseHeaderEncoding.Equals(webPageEncoding))
			{
				@string = WebPageDataProvider.GetString(webPageEncoding, buffer);
				logger.Set(GetLinkPreviewMetadata.WebPageEncodingUsed, 1);
			}
			return new WebPageInformation
			{
				Text = @string,
				ResponseUri = responseUri
			};
		}

		// Token: 0x0600194D RID: 6477 RVA: 0x00058151 File Offset: 0x00056351
		protected override int GetMaxByteCount(Uri responseUri)
		{
			if (AmazonLinkPreviewBuilder.IsAmazonUri(responseUri))
			{
				return 491520;
			}
			if (responseUri.Host != null && responseUri.Host.ToUpper().Equals("WWW.GROUPON.COM", StringComparison.Ordinal))
			{
				return 98304;
			}
			return 32768;
		}

		// Token: 0x0600194E RID: 6478 RVA: 0x0005818C File Offset: 0x0005638C
		private static string GetString(Encoding encoding, byte[] bytes)
		{
			string result = null;
			try
			{
				result = encoding.GetString(bytes);
			}
			catch (DecoderFallbackException ex)
			{
				GetLinkPreview.ThrowInvalidRequestException("EncodingGetStringFailed", string.Format("Encoding {0} failed with {1}", encoding.EncodingName, ex.Message));
			}
			catch (ArgumentException ex2)
			{
				GetLinkPreview.ThrowInvalidRequestException("EncodingGetStringFailed", string.Format("Encoding {0} failed with {1}", encoding.EncodingName, ex2.Message));
			}
			return result;
		}

		// Token: 0x0600194F RID: 6479 RVA: 0x00058208 File Offset: 0x00056408
		private static Encoding GetWebPageEncoding(string webPageString, RequestDetailsLogger logger)
		{
			Encoding result = null;
			string text = null;
			int num = 0;
			Stopwatch stopwatch = Stopwatch.StartNew();
			text = WebPageDataProvider.GetWebPageEncoding(webPageString, WebPageDataProvider.GetXmlEncodingRegEx, "xml encoding");
			num++;
			if (text == null)
			{
				text = WebPageDataProvider.GetWebPageEncoding(webPageString, WebPageDataProvider.GetMetaEncodingRegEx, "meta encoding");
				num++;
			}
			if (text == null)
			{
				text = WebPageDataProvider.GetWebPageEncoding(webPageString, WebPageDataProvider.GetMeta5EncodingRegEx, "meta encoding");
				num++;
			}
			if (text != null)
			{
				try
				{
					result = Encoding.GetEncoding(text);
				}
				catch (ArgumentException)
				{
					GetLinkPreview.ThrowInvalidRequestException("GetEncodingFailed", string.Format("Get encoding failed for {0}", text));
				}
			}
			stopwatch.Stop();
			long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
			logger.Set(GetLinkPreviewMetadata.EncodingRegExCount, num);
			logger.Set(GetLinkPreviewMetadata.ElapsedTimeToGetWebPageEncoding, elapsedMilliseconds);
			return result;
		}

		// Token: 0x06001950 RID: 6480 RVA: 0x000582D8 File Offset: 0x000564D8
		private static string GetWebPageEncoding(string webPageString, Regex regEx, string propertyName)
		{
			string result = null;
			Match match = LinkPreviewBuilder.ExecuteRegEx(regEx, webPageString, propertyName);
			if (match.Groups["encoding"].Captures.Count > 0)
			{
				result = match.Groups["encoding"].Value;
			}
			return result;
		}

		// Token: 0x06001951 RID: 6481 RVA: 0x00058324 File Offset: 0x00056524
		protected override void RequireContentType(MediaTypeHeaderValue contentType)
		{
			if (contentType == null || string.IsNullOrWhiteSpace(contentType.MediaType))
			{
				GetLinkPreview.ThrowInvalidRequestException("UnsupportedContentType", string.Format("Content type {0} is not supported.", "null"));
			}
			string text = contentType.MediaType.ToLower();
			if (!text.Contains("text/html") && !text.Contains("application/xhtml+xml"))
			{
				GetLinkPreview.ThrowInvalidRequestException("UnsupportedContentType", string.Format("Content type {0} is not supported.", text));
			}
		}

		// Token: 0x06001952 RID: 6482 RVA: 0x00058395 File Offset: 0x00056595
		protected override void RestrictContentLength(long? contentLength)
		{
			if (contentLength != null && contentLength.Value > 524288L)
			{
				GetLinkPreview.ThrowInvalidRequestException("MaxContentLengthExceeded", string.Format("Content length {0} exceeds maximum size allowed.", contentLength.Value));
			}
		}

		// Token: 0x04000DC7 RID: 3527
		private const int AmazonMaxByteCount = 491520;

		// Token: 0x04000DC8 RID: 3528
		private const int GrouponMaxByteCount = 98304;

		// Token: 0x04000DC9 RID: 3529
		private const string GrouponHostUpperCase = "WWW.GROUPON.COM";

		// Token: 0x04000DCA RID: 3530
		private const string EncodingRegExKey = "encoding";

		// Token: 0x04000DCB RID: 3531
		private const string XmlEncodingRegEx = "<\\?xml [^><]*?encoding=('|\")(?<encoding>.*?)('|\")[^><]*?>";

		// Token: 0x04000DCC RID: 3532
		private const string MetaEncodingRegEx = "<meta [^><]*?content=('|\")[^><]*?charset=(?<encoding>.*?)('|\")[^><]*?>";

		// Token: 0x04000DCD RID: 3533
		private const string Meta5EncodingRegEx = "<meta charset=('|\")(?<encoding>.*?)('|\")[^><]*?>";

		// Token: 0x04000DCE RID: 3534
		private const string XmlEncodingPropertyName = "xml encoding";

		// Token: 0x04000DCF RID: 3535
		private const string MetaEncodingPropertyName = "meta encoding";

		// Token: 0x04000DD0 RID: 3536
		private const string Meta5EncodingPropertyName = "meta 5 encoding";

		// Token: 0x04000DD1 RID: 3537
		private static Regex GetXmlEncodingRegEx = new Regex("<\\?xml [^><]*?encoding=('|\")(?<encoding>.*?)('|\")[^><]*?>", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);

		// Token: 0x04000DD2 RID: 3538
		private static Regex GetMetaEncodingRegEx = new Regex("<meta [^><]*?content=('|\")[^><]*?charset=(?<encoding>.*?)('|\")[^><]*?>", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);

		// Token: 0x04000DD3 RID: 3539
		private static Regex GetMeta5EncodingRegEx = new Regex("<meta charset=('|\")(?<encoding>.*?)('|\")[^><]*?>", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);
	}
}
