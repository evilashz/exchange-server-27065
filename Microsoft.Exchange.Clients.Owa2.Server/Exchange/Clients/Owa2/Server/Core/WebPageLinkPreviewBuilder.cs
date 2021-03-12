using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002E0 RID: 736
	internal class WebPageLinkPreviewBuilder : LinkPreviewBuilder
	{
		// Token: 0x060018CF RID: 6351 RVA: 0x00055D98 File Offset: 0x00053F98
		public WebPageLinkPreviewBuilder(GetLinkPreviewRequest request, string responseString, RequestDetailsLogger logger, Uri responseUri, bool isVideo) : base(request, logger, responseUri, isVideo)
		{
			this.responseString = responseString;
		}

		// Token: 0x060018D0 RID: 6352 RVA: 0x00055DB0 File Offset: 0x00053FB0
		internal override GetLinkPreviewResponse Execute()
		{
			GetLinkPreviewResponse getLinkPreviewResponse = new GetLinkPreviewResponse();
			LinkPreview linkPreview = this.CreateLinkPreviewInstance();
			linkPreview.Id = this.id;
			linkPreview.Url = this.url;
			linkPreview.RequestStartTimeMilliseconds = this.requestStartTimeMilliseconds;
			int imageTagCount;
			linkPreview.ImageUrl = this.GetImage(out imageTagCount);
			linkPreview.Title = this.GetTitle();
			int descriptionTagCount;
			linkPreview.Description = this.GetDescription(out descriptionTagCount);
			linkPreview.IsVideo = base.IsVideo;
			if (string.IsNullOrWhiteSpace(linkPreview.Title))
			{
				if (string.IsNullOrWhiteSpace(linkPreview.Description))
				{
					GetLinkPreview.ThrowInvalidRequestException("TitleAndDescriptionNotFound", "No title or description were found.");
				}
				else if (string.IsNullOrWhiteSpace(linkPreview.ImageUrl))
				{
					GetLinkPreview.ThrowInvalidRequestException("TitleAndImageNotFound", "No title or image were found.");
				}
			}
			else if (string.IsNullOrWhiteSpace(linkPreview.Description) && string.IsNullOrWhiteSpace(linkPreview.ImageUrl))
			{
				GetLinkPreview.ThrowInvalidRequestException("DescriptionAndImageNotFound", "No description or image were found.");
			}
			this.SetAdditionalProperties(linkPreview);
			getLinkPreviewResponse.LinkPreview = linkPreview;
			getLinkPreviewResponse.ImageTagCount = imageTagCount;
			getLinkPreviewResponse.DescriptionTagCount = descriptionTagCount;
			return getLinkPreviewResponse;
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00055EB4 File Offset: 0x000540B4
		protected string GetAttributeValue(string responseString, Regex getTagRegex, string tagRegexKey, Regex getAttributeRegex, string attributeRegexKey, string propertyName, out int tagCount)
		{
			string text = null;
			MatchCollection matchCollection = LinkPreviewBuilder.ExecuteRegExForMatchCollection(getTagRegex, responseString, propertyName);
			tagCount = matchCollection.Count;
			if (tagCount > 0)
			{
				Match match = LinkPreviewBuilder.ExecuteRegEx(getAttributeRegex, matchCollection[0].Value, propertyName);
				if (match.Groups[attributeRegexKey].Captures.Count > 0)
				{
					text = LinkPreviewBuilder.ConvertToSafeHtml(match.Groups[attributeRegexKey].Value);
					text = WebPageLinkPreviewBuilder.ReplaceSelectedHtmlEntities(text);
				}
			}
			return text;
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00055F2C File Offset: 0x0005412C
		protected static string ReplaceSelectedHtmlEntities(string stringWithEntities)
		{
			if (stringWithEntities == null || stringWithEntities.IndexOf("&", StringComparison.Ordinal) < 0)
			{
				return stringWithEntities;
			}
			string text = stringWithEntities.Replace("&amp;", "&");
			text = text.Replace("&#38;", "&");
			if (stringWithEntities.IndexOf("/", StringComparison.Ordinal) >= 0)
			{
				foreach (string oldValue in WebPageLinkPreviewBuilder.HtmlTagsToRemove)
				{
					text = text.Replace(oldValue, string.Empty);
				}
			}
			return text;
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x00055FA8 File Offset: 0x000541A8
		private static string[] CreateHtmlTagsToRemove()
		{
			List<string> list = new List<string>(12);
			WebPageLinkPreviewBuilder.AddToTagList(list, "i");
			WebPageLinkPreviewBuilder.AddToTagList(list, "b");
			WebPageLinkPreviewBuilder.AddToTagList(list, "u");
			return list.ToArray();
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00055FE4 File Offset: 0x000541E4
		private static void AddToTagList(List<string> tagList, string tagName)
		{
			tagList.Add("&lt;" + tagName + "&gt;");
			tagList.Add("&lt;/" + tagName + "&gt;");
			tagList.Add("&#60;" + tagName + "&#62;");
			tagList.Add("&#60;/" + tagName + "&#62;");
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00056049 File Offset: 0x00054249
		protected static string Truncate(string stringToTruncate, int truncationLength)
		{
			if (stringToTruncate == null)
			{
				return stringToTruncate;
			}
			if (stringToTruncate.Length <= truncationLength)
			{
				return stringToTruncate;
			}
			return stringToTruncate.Substring(0, truncationLength);
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00056063 File Offset: 0x00054263
		protected virtual void SetAdditionalProperties(LinkPreview linkPreview)
		{
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x00056068 File Offset: 0x00054268
		protected virtual string GetImage(out int imageTagCount)
		{
			string attributeValue = this.GetAttributeValue(this.responseString, WebPageLinkPreviewBuilder.GetImageTagRegEx, "imageTag", WebPageLinkPreviewBuilder.GetImageAttributeRegEx, "image", "image", out imageTagCount);
			return this.GetImageUrlAbsolutePath(attributeValue);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x000560A4 File Offset: 0x000542A4
		protected string GetImageUrlAbsolutePath(string imageUrl)
		{
			if (imageUrl == null)
			{
				return imageUrl;
			}
			if (imageUrl.StartsWith("//"))
			{
				this.logger.Set(GetLinkPreviewMetadata.InvalidImageUrl, imageUrl);
				return null;
			}
			Uri uri;
			if (!Uri.TryCreate(imageUrl, UriKind.RelativeOrAbsolute, out uri))
			{
				GetLinkPreview.ThrowInvalidRequestException("InvalidImageUrl", string.Format("Image url {0} is invalid.", imageUrl));
			}
			if (!uri.IsAbsoluteUri)
			{
				UriBuilder uriBuilder = new UriBuilder(this.responseUri.Scheme, this.responseUri.Host);
				if (Uri.TryCreate(uriBuilder.Uri, imageUrl, out uri))
				{
					imageUrl = uri.ToString();
				}
				else
				{
					GetLinkPreview.ThrowInvalidRequestException("InvalidImageUrl", string.Format("Image url {0} is invalid.", imageUrl));
				}
			}
			if (imageUrl != null && imageUrl.Length > 500)
			{
				GetLinkPreview.ThrowInvalidRequestException("MaxImageUrlLengthExceeded", string.Format("Image url length {0} exceeds the maximum length allowed.", imageUrl.Length));
			}
			return imageUrl;
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0005617C File Offset: 0x0005437C
		protected virtual string GetTitle()
		{
			string text = null;
			Match match = LinkPreviewBuilder.ExecuteRegEx(WebPageLinkPreviewBuilder.GetHtmlTitleRegEx, this.responseString, "title");
			if (match.Groups["title"].Captures.Count > 0)
			{
				text = LinkPreviewBuilder.ConvertToSafeHtml(match.Groups["title"].Value);
				text = WebPageLinkPreviewBuilder.ReplaceSelectedHtmlEntities(text);
			}
			this.logger.Set(GetLinkPreviewMetadata.TitleLength, WebPageLinkPreviewBuilder.GetStringLength(text));
			return WebPageLinkPreviewBuilder.Truncate(text, 400);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x00056208 File Offset: 0x00054408
		protected virtual string GetDescription(out int descriptionTagCount)
		{
			string attributeValue = this.GetAttributeValue(this.responseString, WebPageLinkPreviewBuilder.GetDescriptionTagRegEx, "descriptionTag", WebPageLinkPreviewBuilder.GetDescriptionAttributeRegEx, "description", "description", out descriptionTagCount);
			this.logger.Set(GetLinkPreviewMetadata.DescriptionLength, WebPageLinkPreviewBuilder.GetStringLength(attributeValue));
			return WebPageLinkPreviewBuilder.Truncate(attributeValue, 1000);
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x00056265 File Offset: 0x00054465
		protected static int GetStringLength(string text)
		{
			if (text == null)
			{
				return 0;
			}
			return text.Length;
		}

		// Token: 0x04000D4F RID: 3407
		private const string HtmlTitleRegExKey = "title";

		// Token: 0x04000D50 RID: 3408
		private const string DescriptionTagRegExKey = "descriptionTag";

		// Token: 0x04000D51 RID: 3409
		private const string DescriptionAttributeRegExKey = "description";

		// Token: 0x04000D52 RID: 3410
		private const string ImageTagRegExKey = "imageTag";

		// Token: 0x04000D53 RID: 3411
		private const string ImageAttributeRegExKey = "image";

		// Token: 0x04000D54 RID: 3412
		private const string HtmlTitleRegEx = "<title(?:>|\\s[^<]*?>)\\s*(?<title>.*?)\\s*</title>";

		// Token: 0x04000D55 RID: 3413
		private const string DescriptionTagRegEx = "<meta(?<descriptionTag>\\s[^<]*?(name=('|\")description\\2|(name|property)=('|\")og:description\\4)[^<]*?)>";

		// Token: 0x04000D56 RID: 3414
		private const string DescriptionAttributeRegEx = "\\scontent=('|\")(?<description>.*?)\\1";

		// Token: 0x04000D57 RID: 3415
		private const string ImageTagRegEx = "<meta(?<imageTag>\\s[^<]*?(property|name)=('|\")og:image\\2[^<]*?)>";

		// Token: 0x04000D58 RID: 3416
		private const string ImageAttributeRegEx = "\\scontent=('|\")(?<image>.*?)\\1";

		// Token: 0x04000D59 RID: 3417
		private const string LessThanEntityName = "&lt;";

		// Token: 0x04000D5A RID: 3418
		private const string LessThanEntityNumber = "&#60;";

		// Token: 0x04000D5B RID: 3419
		private const string GreaterThanEntityName = "&gt;";

		// Token: 0x04000D5C RID: 3420
		private const string GreaterThanEntityNumber = "&#62;";

		// Token: 0x04000D5D RID: 3421
		private const string Slash = "/";

		// Token: 0x04000D5E RID: 3422
		public static RegexOptions RegExOptions = RegexOptions.IgnoreCase | RegexOptions.Singleline;

		// Token: 0x04000D5F RID: 3423
		public static TimeSpan RegExTimeoutInterval = TimeSpan.FromMilliseconds(300.0);

		// Token: 0x04000D60 RID: 3424
		private static Regex GetHtmlTitleRegEx = new Regex("<title(?:>|\\s[^<]*?>)\\s*(?<title>.*?)\\s*</title>", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);

		// Token: 0x04000D61 RID: 3425
		private static Regex GetDescriptionTagRegEx = new Regex("<meta(?<descriptionTag>\\s[^<]*?(name=('|\")description\\2|(name|property)=('|\")og:description\\4)[^<]*?)>", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);

		// Token: 0x04000D62 RID: 3426
		private static Regex GetDescriptionAttributeRegEx = new Regex("\\scontent=('|\")(?<description>.*?)\\1", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);

		// Token: 0x04000D63 RID: 3427
		private static Regex GetImageTagRegEx = new Regex("<meta(?<imageTag>\\s[^<]*?(property|name)=('|\")og:image\\2[^<]*?)>", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);

		// Token: 0x04000D64 RID: 3428
		private static Regex GetImageAttributeRegEx = new Regex("\\scontent=('|\")(?<image>.*?)\\1", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);

		// Token: 0x04000D65 RID: 3429
		private static string[] HtmlTagsToRemove = WebPageLinkPreviewBuilder.CreateHtmlTagsToRemove();

		// Token: 0x04000D66 RID: 3430
		protected string responseString;
	}
}
