using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002EB RID: 747
	internal class OEmbedVideoPreviewBuilder : WebPageLinkPreviewBuilder
	{
		// Token: 0x0600191A RID: 6426 RVA: 0x00057A9F File Offset: 0x00055C9F
		public OEmbedVideoPreviewBuilder(GetLinkPreviewRequest request, string responseString, OEmbedResponse oEmbedResponse, RequestDetailsLogger logger, Uri responseUri) : base(request, responseString, logger, responseUri, true)
		{
			this.oEmbedResponse = oEmbedResponse;
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x0600191B RID: 6427 RVA: 0x00057AB8 File Offset: 0x00055CB8
		private static OEmbedVideoPreviewBuilder.OEmbedProviderDetails[] OEmbedProviderDetailsArray
		{
			get
			{
				if (OEmbedVideoPreviewBuilder.oembedProviderDetails == null)
				{
					OEmbedVideoPreviewBuilder.oembedProviderDetails = new OEmbedVideoPreviewBuilder.OEmbedProviderDetails[3];
					OEmbedVideoPreviewBuilder.oembedProviderDetails[0] = new OEmbedVideoPreviewBuilder.OEmbedProviderDetails();
					OEmbedVideoPreviewBuilder.oembedProviderDetails[0].UrlScheme = "https?://(?:www\\.)?dailymotion\\.com/video.*";
					OEmbedVideoPreviewBuilder.oembedProviderDetails[0].ApiEndPoint = "https://www.dailymotion.com/services/oembed?format=json&url={0}&maxwidth=640&maxheight=480";
					OEmbedVideoPreviewBuilder.oembedProviderDetails[1] = new OEmbedVideoPreviewBuilder.OEmbedProviderDetails();
					OEmbedVideoPreviewBuilder.oembedProviderDetails[1].UrlScheme = "https?://(?:www\\.)?hulu\\.com/watch.*";
					OEmbedVideoPreviewBuilder.oembedProviderDetails[1].ApiEndPoint = "https://secure.hulu.com/api/oembed.json?url={0}&maxwidth=640&maxheight=480";
					OEmbedVideoPreviewBuilder.oembedProviderDetails[2] = new OEmbedVideoPreviewBuilder.OEmbedProviderDetails();
					OEmbedVideoPreviewBuilder.oembedProviderDetails[2].UrlScheme = "https?://(?:www\\.)?vimeo\\.com.*";
					OEmbedVideoPreviewBuilder.oembedProviderDetails[2].ApiEndPoint = "https://vimeo.com/api/oembed.json?url={0}&maxwidth=640&maxheight=480";
				}
				return OEmbedVideoPreviewBuilder.oembedProviderDetails;
			}
		}

		// Token: 0x0600191C RID: 6428 RVA: 0x00057B6C File Offset: 0x00055D6C
		internal static bool IsOEmbedVideoUri(Uri uri, RequestDetailsLogger logger)
		{
			foreach (OEmbedVideoPreviewBuilder.OEmbedProviderDetails oembedProviderDetails in OEmbedVideoPreviewBuilder.OEmbedProviderDetailsArray)
			{
				if (oembedProviderDetails.UrlSchemeRegex.IsMatch(uri.AbsoluteUri))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600191D RID: 6429 RVA: 0x00057BAC File Offset: 0x00055DAC
		internal static string GetOEmbedQueryForUri(Uri uri)
		{
			foreach (OEmbedVideoPreviewBuilder.OEmbedProviderDetails oembedProviderDetails in OEmbedVideoPreviewBuilder.OEmbedProviderDetailsArray)
			{
				if (oembedProviderDetails.UrlSchemeRegex.IsMatch(uri.AbsoluteUri))
				{
					return string.Format(oembedProviderDetails.ApiEndPoint, uri.AbsoluteUri);
				}
			}
			return null;
		}

		// Token: 0x0600191E RID: 6430 RVA: 0x00057BFB File Offset: 0x00055DFB
		protected override LinkPreview CreateLinkPreviewInstance()
		{
			return new OEmbedVideoPreview();
		}

		// Token: 0x0600191F RID: 6431 RVA: 0x00057C04 File Offset: 0x00055E04
		protected override void SetAdditionalProperties(LinkPreview linkPreview)
		{
			if (this.oEmbedResponse == null)
			{
				GetLinkPreview.ThrowInvalidRequestException("OEmbedResponseNull", string.Format("The OEmbedResponse was null for the webpage information for {0}", this.responseUri.AbsoluteUri));
			}
			if (this.oEmbedResponse.Html == null)
			{
				GetLinkPreview.ThrowInvalidRequestException("OEmbedResponseHtmlNull", string.Format("The OEmbedResponse HTML was null for the webpage information for {0}", this.responseUri.AbsoluteUri));
			}
			string text = this.oEmbedResponse.Html;
			if (this.oEmbedResponse.ProviderName.Equals("YouTube") || this.oEmbedResponse.ProviderName.Equals("Dailymotion"))
			{
				text = text.Replace("http://", "https://");
			}
			else if (this.oEmbedResponse.ProviderName.Equals("Hulu"))
			{
				text = text.Replace("http://www.hulu.com", "https://secure.hulu.com");
			}
			((OEmbedVideoPreview)linkPreview).EmbeddedHtml = text;
		}

		// Token: 0x06001920 RID: 6432 RVA: 0x00057CE8 File Offset: 0x00055EE8
		protected override string GetImage(out int imageTagCount)
		{
			string thumbnailUrl = this.oEmbedResponse.ThumbnailUrl;
			if (thumbnailUrl != null)
			{
				if (thumbnailUrl.Length > 500)
				{
					GetLinkPreview.ThrowInvalidRequestException("MaxImageUrlLengthExceeded", string.Format("Image url length {0} exceeds the maximum length allowed.", thumbnailUrl.Length));
				}
				imageTagCount = 1;
				return thumbnailUrl;
			}
			imageTagCount = 0;
			return null;
		}

		// Token: 0x06001921 RID: 6433 RVA: 0x00057D3C File Offset: 0x00055F3C
		protected override string GetTitle()
		{
			string text = this.oEmbedResponse.Title;
			if (text != null)
			{
				text = LinkPreviewBuilder.ConvertToSafeHtml(text);
				text = WebPageLinkPreviewBuilder.ReplaceSelectedHtmlEntities(text);
				return WebPageLinkPreviewBuilder.Truncate(text, 400);
			}
			return null;
		}

		// Token: 0x06001922 RID: 6434 RVA: 0x00057D73 File Offset: 0x00055F73
		protected override string GetDescription(out int descriptionTagCount)
		{
			if (this.responseString == null)
			{
				descriptionTagCount = 0;
				return null;
			}
			return base.GetDescription(out descriptionTagCount);
		}

		// Token: 0x04000DB1 RID: 3505
		private const string VideoWidthHeightQueryString = "&maxwidth=640&maxheight=480";

		// Token: 0x04000DB2 RID: 3506
		private const string UrlPrefixRegex = "https?://(?:www\\.)?";

		// Token: 0x04000DB3 RID: 3507
		private static OEmbedVideoPreviewBuilder.OEmbedProviderDetails[] oembedProviderDetails;

		// Token: 0x04000DB4 RID: 3508
		private readonly OEmbedResponse oEmbedResponse;

		// Token: 0x020002EC RID: 748
		private class OEmbedProviderDetails
		{
			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x06001924 RID: 6436 RVA: 0x00057D8B File Offset: 0x00055F8B
			// (set) Token: 0x06001925 RID: 6437 RVA: 0x00057D93 File Offset: 0x00055F93
			internal string UrlScheme { private get; set; }

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x06001926 RID: 6438 RVA: 0x00057D9C File Offset: 0x00055F9C
			// (set) Token: 0x06001927 RID: 6439 RVA: 0x00057DA4 File Offset: 0x00055FA4
			internal string ApiEndPoint { get; set; }

			// Token: 0x17000604 RID: 1540
			// (get) Token: 0x06001928 RID: 6440 RVA: 0x00057DAD File Offset: 0x00055FAD
			internal Regex UrlSchemeRegex
			{
				get
				{
					if (this.urlSchemeRegex == null)
					{
						this.urlSchemeRegex = new Regex(this.UrlScheme, WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);
					}
					return this.urlSchemeRegex;
				}
			}

			// Token: 0x04000DB5 RID: 3509
			private Regex urlSchemeRegex;
		}
	}
}
