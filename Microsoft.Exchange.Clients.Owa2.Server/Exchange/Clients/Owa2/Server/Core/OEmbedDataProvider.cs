using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002E8 RID: 744
	internal class OEmbedDataProvider : LinkPreviewDataProvider
	{
		// Token: 0x0600190C RID: 6412 RVA: 0x000575C4 File Offset: 0x000557C4
		public OEmbedDataProvider(Uri uri, GetLinkPreviewRequest request, RequestDetailsLogger logger) : base(uri, request, logger)
		{
		}

		// Token: 0x0600190D RID: 6413 RVA: 0x0005785C File Offset: 0x00055A5C
		protected override async Task<DataProviderInformation> GetDataProviderInformation(HttpClient httpClient)
		{
			DataProviderInformation result;
			if (this.request.Id.StartsWith("UpdateOEmbed"))
			{
				result = await base.MakeAndProcessHttpRequest(httpClient, OEmbedDataProvider.GetOEmbedRequestUri(this.uri), new LinkPreviewDataProvider.ProcessResponseStreamDelegate(OEmbedDataProvider.ProcessOEmbedResponseStream));
			}
			else
			{
				WebPageInformation webPageInformation = (WebPageInformation)(await base.MakeAndProcessHttpRequest(httpClient, this.uri, new LinkPreviewDataProvider.ProcessResponseStreamDelegate(WebPageDataProvider.ProcessResponseStream)));
				OEmbedInformation oembedInformation = (OEmbedInformation)(await base.MakeAndProcessHttpRequest(httpClient, OEmbedDataProvider.GetOEmbedRequestUri(this.uri), new LinkPreviewDataProvider.ProcessResponseStreamDelegate(OEmbedDataProvider.ProcessOEmbedResponseStream)));
				oembedInformation.Text = webPageInformation.Text;
				result = oembedInformation;
			}
			return result;
		}

		// Token: 0x0600190E RID: 6414 RVA: 0x000578AC File Offset: 0x00055AAC
		private static Uri GetOEmbedRequestUri(Uri uri)
		{
			string oembedQueryForUri = OEmbedVideoPreviewBuilder.GetOEmbedQueryForUri(uri);
			if (oembedQueryForUri != null)
			{
				return new Uri(oembedQueryForUri);
			}
			GetLinkPreview.ThrowInvalidRequestException("OEmbedQueryStringNotFound", string.Format("Could not get OEmbed query string for url {0}", uri.AbsoluteUri));
			return null;
		}

		// Token: 0x0600190F RID: 6415 RVA: 0x000578E8 File Offset: 0x00055AE8
		protected static DataProviderInformation ProcessOEmbedResponseStream(Uri responseUri, Encoding responseHeaderEncoding, MemoryStream memoryStream, RequestDetailsLogger logger)
		{
			memoryStream.Position = 0L;
			OEmbedResponse oembedResponse = null;
			try
			{
				DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(OEmbedResponse));
				oembedResponse = (OEmbedResponse)dataContractJsonSerializer.ReadObject(memoryStream);
			}
			catch (SerializationException ex)
			{
				GetLinkPreview.ThrowInvalidRequestException("OEmbedResponseSerializationReadFailed", string.Format("Failed to read the OEmbed response object. Error {0}", ex.Message));
			}
			return new OEmbedInformation
			{
				Text = null,
				ResponseUri = responseUri,
				OEmbedResponse = oembedResponse
			};
		}

		// Token: 0x06001910 RID: 6416 RVA: 0x00057968 File Offset: 0x00055B68
		protected override void RequireContentType(MediaTypeHeaderValue contentType)
		{
			if (contentType == null || string.IsNullOrWhiteSpace(contentType.MediaType))
			{
				GetLinkPreview.ThrowInvalidRequestException("UnsupportedContentType", string.Format("Content type {0} is not supported.", "null"));
			}
			string text = contentType.MediaType.ToLower();
			if (!text.Contains("text/html") && !text.Contains("application/xhtml+xml") && !text.Contains("application/json"))
			{
				GetLinkPreview.ThrowInvalidRequestException("UnsupportedContentType", string.Format("Content type {0} is not supported.", text));
			}
		}

		// Token: 0x06001911 RID: 6417 RVA: 0x000579E8 File Offset: 0x00055BE8
		public override GetLinkPreviewResponse CreatePreview(DataProviderInformation dataProviderInformation)
		{
			OEmbedInformation oembedInformation = (OEmbedInformation)dataProviderInformation;
			string text = oembedInformation.Text;
			OEmbedResponse oembedResponse = oembedInformation.OEmbedResponse;
			LinkPreviewBuilder linkPreviewBuilder = new OEmbedVideoPreviewBuilder(this.request, text, oembedResponse, this.logger, dataProviderInformation.ResponseUri);
			return linkPreviewBuilder.Execute();
		}

		// Token: 0x06001912 RID: 6418 RVA: 0x00057A2C File Offset: 0x00055C2C
		protected override void RestrictContentLength(long? contentLength)
		{
			if (contentLength != null && contentLength.Value > 524288L)
			{
				GetLinkPreview.ThrowInvalidRequestException("MaxContentLengthExceeded", string.Format("Content length {0} exceeds maximum size allowed.", contentLength.Value));
			}
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x00057A66 File Offset: 0x00055C66
		protected override int GetMaxByteCount(Uri responseUri)
		{
			return 32768;
		}

		// Token: 0x04000DAD RID: 3501
		private const string JsonContentType = "application/json";

		// Token: 0x04000DAE RID: 3502
		private const string OEmbedRequestIdPrefix = "UpdateOEmbed";
	}
}
