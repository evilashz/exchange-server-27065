using System;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;

namespace Microsoft.Exchange.Clients.Owa2.Server.Diagnostics
{
	// Token: 0x02000440 RID: 1088
	internal enum GetLinkPreviewMetadata
	{
		// Token: 0x04001490 RID: 5264
		[DisplayName("GLP", "URL")]
		Url,
		// Token: 0x04001491 RID: 5265
		[DisplayName("GLP", "ERR")]
		Error,
		// Token: 0x04001492 RID: 5266
		[DisplayName("GLP", "EMSG")]
		ErrorMessage,
		// Token: 0x04001493 RID: 5267
		[DisplayName("GLP", "TWP")]
		ElapsedTimeToWebPageStepCompletion,
		// Token: 0x04001494 RID: 5268
		[DisplayName("GLP", "TRE")]
		ElapsedTimeToRegExStepCompletion,
		// Token: 0x04001495 RID: 5269
		[DisplayName("GLP", "CL")]
		WebPageContentLength,
		// Token: 0x04001496 RID: 5270
		[DisplayName("GLP", "ITC")]
		ImageTagCount,
		// Token: 0x04001497 RID: 5271
		[DisplayName("GLP", "DTC")]
		DescriptionTagCount,
		// Token: 0x04001498 RID: 5272
		[DisplayName("GLP", "TL")]
		TitleLength,
		// Token: 0x04001499 RID: 5273
		[DisplayName("GLP", "DL")]
		DescriptionLength,
		// Token: 0x0400149A RID: 5274
		[DisplayName("GLP", "DSR")]
		DisabledResponse,
		// Token: 0x0400149B RID: 5275
		[DisplayName("GLP", "YTF")]
		YouTubeLinkValidationFailed,
		// Token: 0x0400149C RID: 5276
		[DisplayName("GLP", "WEU")]
		WebPageEncodingUsed,
		// Token: 0x0400149D RID: 5277
		[DisplayName("GLP", "ERC")]
		EncodingRegExCount,
		// Token: 0x0400149E RID: 5278
		[DisplayName("GLP", "TWE")]
		ElapsedTimeToGetWebPageEncoding,
		// Token: 0x0400149F RID: 5279
		[DisplayName("GLP", "IURL")]
		InvalidImageUrl,
		// Token: 0x040014A0 RID: 5280
		[DisplayName("GLP", "UCN")]
		UserContextNull,
		// Token: 0x040014A1 RID: 5281
		[DisplayName("GLP", "AVC")]
		ActiveViewConvergenceEnabled
	}
}
