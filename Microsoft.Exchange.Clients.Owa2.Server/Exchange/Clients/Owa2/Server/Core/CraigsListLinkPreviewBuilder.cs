using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002E2 RID: 738
	internal class CraigsListLinkPreviewBuilder : WebPageLinkPreviewBuilder
	{
		// Token: 0x060018E1 RID: 6369 RVA: 0x000563B6 File Offset: 0x000545B6
		public CraigsListLinkPreviewBuilder(GetLinkPreviewRequest request, string responseString, RequestDetailsLogger logger, Uri responseUri) : base(request, responseString, logger, responseUri, false)
		{
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x000563C4 File Offset: 0x000545C4
		protected override string GetDescription(out int descriptionTagCount)
		{
			string text = null;
			Match match = LinkPreviewBuilder.ExecuteRegEx(CraigsListLinkPreviewBuilder.GetDescriptionRegEx, this.responseString, "description");
			descriptionTagCount = match.Groups["description"].Captures.Count;
			if (descriptionTagCount > 0)
			{
				text = LinkPreviewBuilder.ConvertToSafeHtml(match.Groups["description"].Value);
				text = WebPageLinkPreviewBuilder.ReplaceSelectedHtmlEntities(text);
			}
			this.logger.Set(GetLinkPreviewMetadata.DescriptionLength, WebPageLinkPreviewBuilder.GetStringLength(text));
			return WebPageLinkPreviewBuilder.Truncate(text, 1000);
		}

		// Token: 0x060018E3 RID: 6371 RVA: 0x00056458 File Offset: 0x00054658
		protected override string GetImage(out int imageTagCount)
		{
			string imageUrl = null;
			Match match = LinkPreviewBuilder.ExecuteRegEx(CraigsListLinkPreviewBuilder.GetImageSrcRegEx, this.responseString, "image");
			imageTagCount = match.Groups["imageUrl"].Captures.Count;
			if (imageTagCount > 0)
			{
				imageUrl = LinkPreviewBuilder.ConvertToSafeHtml(match.Groups["imageUrl"].Value);
			}
			return base.GetImageUrlAbsolutePath(imageUrl);
		}

		// Token: 0x060018E4 RID: 6372 RVA: 0x000564C0 File Offset: 0x000546C0
		public static bool IsCraigsListUri(Uri responseUri)
		{
			return responseUri.Host != null && responseUri.Host.ToUpper().Contains(".CRAIGSLIST.");
		}

		// Token: 0x04000D6E RID: 3438
		private const string CraigsListHostSegmentUpperCase = ".CRAIGSLIST.";

		// Token: 0x04000D6F RID: 3439
		private const string DescriptionRegExKey = "description";

		// Token: 0x04000D70 RID: 3440
		private const string ImageSrcRegExKey = "imageUrl";

		// Token: 0x04000D71 RID: 3441
		private const string DescriptionRegEx = "<section id=('|\")postingbody('|\")>(?<description>.*?)</section>";

		// Token: 0x04000D72 RID: 3442
		private const string ImageSrcRegEx = "<img\\sid=('|\")iwi('|\")\\ssrc=('|\")(?<imageUrl>.*?)('|\")[^><]*?title=('|\")image 1('|\")[^><]*?>";

		// Token: 0x04000D73 RID: 3443
		private static Regex GetDescriptionRegEx = new Regex("<section id=('|\")postingbody('|\")>(?<description>.*?)</section>", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);

		// Token: 0x04000D74 RID: 3444
		private static Regex GetImageSrcRegEx = new Regex("<img\\sid=('|\")iwi('|\")\\ssrc=('|\")(?<imageUrl>.*?)('|\")[^><]*?title=('|\")image 1('|\")[^><]*?>", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);
	}
}
