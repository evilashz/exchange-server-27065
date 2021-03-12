using System;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002E5 RID: 741
	internal class LinkPreviewError
	{
		// Token: 0x04000D7D RID: 3453
		public const string InvalidUrl = "InvalidUrl";

		// Token: 0x04000D7E RID: 3454
		public const string UnsupportedContentType = "UnsupportedContentType";

		// Token: 0x04000D7F RID: 3455
		public const string MaxContentLengthExceeded = "MaxContentLengthExceeded";

		// Token: 0x04000D80 RID: 3456
		public const string EmptyContent = "EmptyContent";

		// Token: 0x04000D81 RID: 3457
		public const string TitleAndDescriptionNotFound = "TitleAndDescriptionNotFound";

		// Token: 0x04000D82 RID: 3458
		public const string DescriptionAndImageNotFound = "DescriptionAndImageNotFound";

		// Token: 0x04000D83 RID: 3459
		public const string TitleAndImageNotFound = "TitleAndImageNotFound";

		// Token: 0x04000D84 RID: 3460
		public const string RegExTimeout = "RegExTimeout";

		// Token: 0x04000D85 RID: 3461
		public const string RequestTimeout = "RequestTimeout";

		// Token: 0x04000D86 RID: 3462
		public const string MaxConcurrentRequestExceeded = "MaxConcurrentRequestExceeded";

		// Token: 0x04000D87 RID: 3463
		public const string MaxImageUrlLengthExceeded = "MaxImageUrlLengthExceeded";

		// Token: 0x04000D88 RID: 3464
		public const string InvalidImageUrl = "InvalidImageUrl";

		// Token: 0x04000D89 RID: 3465
		public const string HtmlConversionFailed = "HtmlConversionFailed";

		// Token: 0x04000D8A RID: 3466
		public const string EncodingGetStringFailed = "EncodingGetStringFailed";

		// Token: 0x04000D8B RID: 3467
		public const string GetEncodingFailed = "GetEncodingFailed";

		// Token: 0x04000D8C RID: 3468
		public const string OEmbedQueryStringNotFound = "OEmbedQueryStringNotFound";

		// Token: 0x04000D8D RID: 3469
		public const string OEmbedResponseNull = "OEmbedResponseNull";

		// Token: 0x04000D8E RID: 3470
		public const string OEmbedResponseHtmlNull = "OEmbedResponseHtmlNull";

		// Token: 0x04000D8F RID: 3471
		public const string OEmbedResponseSerializationReadFailed = "OEmbedResponseSerializationReadFailed";

		// Token: 0x04000D90 RID: 3472
		public const string InvalidUrlMessage = "Request url is invalid";

		// Token: 0x04000D91 RID: 3473
		public const string UnsupportedContentTypeMessage = "Content type {0} is not supported.";

		// Token: 0x04000D92 RID: 3474
		public const string MaxContentLengthExceededMessage = "Content length {0} exceeds maximum size allowed.";

		// Token: 0x04000D93 RID: 3475
		public const string EmptyContentMessage = "Url returns no content.";

		// Token: 0x04000D94 RID: 3476
		public const string TitleAndDescriptionNotFoundMessage = "No title or description were found.";

		// Token: 0x04000D95 RID: 3477
		public const string DescriptionAndImageNotFoundMessage = "No description or image were found.";

		// Token: 0x04000D96 RID: 3478
		public const string TitleAndImageNotFoundMessage = "No title or image were found.";

		// Token: 0x04000D97 RID: 3479
		public const string RegExTimeoutMessage = "The regex timed out on property {0}.";

		// Token: 0x04000D98 RID: 3480
		public const string RequestTimeoutMessage = "The web page request timed out.";

		// Token: 0x04000D99 RID: 3481
		public const string MaxConcurrentRequestExceededMessage = "The maximum number of concurrent requests has been exceeded.";

		// Token: 0x04000D9A RID: 3482
		public const string MaxImageUrlLengthExceededMessage = "Image url length {0} exceeds the maximum length allowed.";

		// Token: 0x04000D9B RID: 3483
		public const string InvalidImageUrlMessage = "Image url {0} is invalid.";

		// Token: 0x04000D9C RID: 3484
		public const string EncodingGetStringFailedMessage = "Encoding {0} failed with {1}";

		// Token: 0x04000D9D RID: 3485
		public const string GetEncodingFailedMessage = "Get encoding failed for {0}";

		// Token: 0x04000D9E RID: 3486
		public const string OEmbedQueryStringNotFoundMessage = "Could not get OEmbed query string for url {0}";

		// Token: 0x04000D9F RID: 3487
		public const string OEmbedResponseNullMessage = "The OEmbedResponse was null for the webpage information for {0}";

		// Token: 0x04000DA0 RID: 3488
		public const string OEmbedResponseHtmlNullMessage = "The OEmbedResponse HTML was null for the webpage information for {0}";

		// Token: 0x04000DA1 RID: 3489
		public const string OEmbedResponseSerializationReadFailedMeesage = "Failed to read the OEmbed response object. Error {0}";
	}
}
