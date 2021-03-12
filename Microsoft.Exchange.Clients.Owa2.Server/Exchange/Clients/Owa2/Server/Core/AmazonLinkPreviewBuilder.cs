using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002E1 RID: 737
	internal class AmazonLinkPreviewBuilder : WebPageLinkPreviewBuilder
	{
		// Token: 0x060018DD RID: 6365 RVA: 0x00056322 File Offset: 0x00054522
		public AmazonLinkPreviewBuilder(GetLinkPreviewRequest request, string responseString, RequestDetailsLogger logger, Uri responseUri) : base(request, responseString, logger, responseUri, false)
		{
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x00056330 File Offset: 0x00054530
		protected override string GetImage(out int imageTagCount)
		{
			string attributeValue = base.GetAttributeValue(this.responseString, AmazonLinkPreviewBuilder.GetImageTagRegEx, "imageTag", AmazonLinkPreviewBuilder.GetImageAttributeRegEx, "image", "image", out imageTagCount);
			return base.GetImageUrlAbsolutePath(attributeValue);
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x0005636B File Offset: 0x0005456B
		public static bool IsAmazonUri(Uri responseUri)
		{
			return responseUri.Host.ToUpper().StartsWith("WWW.AMAZON.");
		}

		// Token: 0x04000D67 RID: 3431
		private const string AmazonHostPrefixUpperCase = "WWW.AMAZON.";

		// Token: 0x04000D68 RID: 3432
		private const string ImageTagRegExKey = "imageTag";

		// Token: 0x04000D69 RID: 3433
		private const string ImageAttributeRegExKey = "image";

		// Token: 0x04000D6A RID: 3434
		private const string ImageTagRegEx = "<img(?<imageTag>[^><]*?\\sid=('|\")(imgBlkFront|main-image|landingImage|prod-img|prodImage)\\1[^><]*?)>";

		// Token: 0x04000D6B RID: 3435
		private const string ImageAttributeRegEx = "\\ssrc=('|\")(?<image>.*?)\\1";

		// Token: 0x04000D6C RID: 3436
		private static Regex GetImageTagRegEx = new Regex("<img(?<imageTag>[^><]*?\\sid=('|\")(imgBlkFront|main-image|landingImage|prod-img|prodImage)\\1[^><]*?)>", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);

		// Token: 0x04000D6D RID: 3437
		private static Regex GetImageAttributeRegEx = new Regex("\\ssrc=('|\")(?<image>.*?)\\1", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);
	}
}
