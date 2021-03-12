using System;
using System.Net.Http.Headers;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002EF RID: 751
	internal class WikipediaDataProvider : WebPageDataProvider
	{
		// Token: 0x06001954 RID: 6484 RVA: 0x00058428 File Offset: 0x00056628
		public WikipediaDataProvider(Uri uri, GetLinkPreviewRequest request, RequestDetailsLogger logger) : base(uri, request, logger)
		{
		}

		// Token: 0x06001955 RID: 6485 RVA: 0x00058434 File Offset: 0x00056634
		public override GetLinkPreviewResponse CreatePreview(DataProviderInformation dataProviderInformation)
		{
			LinkPreviewBuilder linkPreviewBuilder = new WikipediaLinkPreviewBuilder(this.request, ((WebPageInformation)dataProviderInformation).Text, this.logger, dataProviderInformation.ResponseUri);
			return linkPreviewBuilder.Execute();
		}

		// Token: 0x06001956 RID: 6486 RVA: 0x0005846A File Offset: 0x0005666A
		protected override int GetMaxByteCount(Uri responseUri)
		{
			return 2048;
		}

		// Token: 0x06001957 RID: 6487 RVA: 0x00058474 File Offset: 0x00056674
		protected override void RequireContentType(MediaTypeHeaderValue contentType)
		{
			if (contentType == null || string.IsNullOrWhiteSpace(contentType.MediaType))
			{
				GetLinkPreview.ThrowInvalidRequestException("UnsupportedContentType", string.Format("Content type {0} is not supported.", "null"));
			}
			string text = contentType.MediaType.ToLower();
			if (!text.Contains("text/xml"))
			{
				GetLinkPreview.ThrowInvalidRequestException("UnsupportedContentType", string.Format("Content type {0} is not supported.", text));
			}
		}

		// Token: 0x04000DD4 RID: 3540
		protected new const int MaxByteCount = 2048;
	}
}
