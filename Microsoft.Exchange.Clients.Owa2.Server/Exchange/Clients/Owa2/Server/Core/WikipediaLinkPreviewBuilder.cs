using System;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Clients.Owa2.Server.Diagnostics;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002F0 RID: 752
	internal class WikipediaLinkPreviewBuilder : WebPageLinkPreviewBuilder
	{
		// Token: 0x06001958 RID: 6488 RVA: 0x000584D8 File Offset: 0x000566D8
		public WikipediaLinkPreviewBuilder(GetLinkPreviewRequest request, string responseString, RequestDetailsLogger logger, Uri responseUri) : base(request, responseString, logger, responseUri, false)
		{
		}

		// Token: 0x06001959 RID: 6489 RVA: 0x000584E6 File Offset: 0x000566E6
		protected override string GetImage(out int imageTagCount)
		{
			imageTagCount = 0;
			return null;
		}

		// Token: 0x0600195A RID: 6490 RVA: 0x000584EC File Offset: 0x000566EC
		protected override string GetTitle()
		{
			string text = null;
			Match match = LinkPreviewBuilder.ExecuteRegEx(WikipediaLinkPreviewBuilder.GetTitleRegEx, this.responseString, "title");
			if (match.Groups["title"].Captures.Count > 0)
			{
				text = LinkPreviewBuilder.ConvertToSafeHtml(match.Groups["title"].Value);
			}
			this.logger.Set(GetLinkPreviewMetadata.TitleLength, WebPageLinkPreviewBuilder.GetStringLength(text));
			return WebPageLinkPreviewBuilder.Truncate(text, 400);
		}

		// Token: 0x0600195B RID: 6491 RVA: 0x00058574 File Offset: 0x00056774
		protected override string GetDescription(out int descriptionTagCount)
		{
			string text = null;
			descriptionTagCount = 0;
			Match match = LinkPreviewBuilder.ExecuteRegEx(WikipediaLinkPreviewBuilder.GetDescriptionRegEx, this.responseString, "title");
			if (match.Groups["description"].Captures.Count > 0)
			{
				text = LinkPreviewBuilder.ConvertToSafeHtml(match.Groups["description"].Value);
				descriptionTagCount = 1;
			}
			this.logger.Set(GetLinkPreviewMetadata.DescriptionLength, WebPageLinkPreviewBuilder.GetStringLength(text));
			return WebPageLinkPreviewBuilder.Truncate(text, 1000);
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x00058600 File Offset: 0x00056800
		public static bool IsWikipediaUri(Uri uri)
		{
			return uri.Host != null && uri.Host.ToUpper().EndsWith(".WIKIPEDIA.ORG");
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x00058624 File Offset: 0x00056824
		public static bool TryGetWikipediaServiceUri(Uri uri, out Uri wikipediaServiceUri)
		{
			wikipediaServiceUri = null;
			if (WikipediaLinkPreviewBuilder.IsWikipediaUri(uri))
			{
				string wikipediaServiceUrl = WikipediaLinkPreviewBuilder.GetWikipediaServiceUrl(uri);
				if (wikipediaServiceUrl != null)
				{
					wikipediaServiceUri = new Uri(wikipediaServiceUrl);
				}
			}
			return wikipediaServiceUri != null;
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x00058658 File Offset: 0x00056858
		private static string GetWikipediaServiceUrl(Uri uri)
		{
			string result = null;
			if ((uri.Host.Length == WikipediaLinkPreviewBuilder.DesktopHostLength || uri.Host.Length == WikipediaLinkPreviewBuilder.MobileHostLength) && uri.AbsolutePath.ToUpper().StartsWith("/WIKI/") && uri.Segments.Length == 3)
			{
				result = string.Format("http://{0}/w/api.php?action=query&prop=extracts|info&titles={1}&exintro=1&explaintext=1&exchars=150&inprop=displaytitle&format=xml", uri.Host, uri.Segments[2]);
			}
			return result;
		}

		// Token: 0x04000DD5 RID: 3541
		private const string HostSuffixUpperCase = ".WIKIPEDIA.ORG";

		// Token: 0x04000DD6 RID: 3542
		private const int HostLocaleLength = 2;

		// Token: 0x04000DD7 RID: 3543
		private const int HostMobileLength = 1;

		// Token: 0x04000DD8 RID: 3544
		private const int HostDelimiterLength = 1;

		// Token: 0x04000DD9 RID: 3545
		private const int WikipediaDesktopSegmentsLength = 3;

		// Token: 0x04000DDA RID: 3546
		private const int WikipediaMobileSegmentsLength = 4;

		// Token: 0x04000DDB RID: 3547
		private const string WikipediaPathPrefixUpperCase = "/WIKI/";

		// Token: 0x04000DDC RID: 3548
		private const string WikipediaServiceFormatString = "http://{0}/w/api.php?action=query&prop=extracts|info&titles={1}&exintro=1&explaintext=1&exchars=150&inprop=displaytitle&format=xml";

		// Token: 0x04000DDD RID: 3549
		private const string DescriptionCharacterLength = "150";

		// Token: 0x04000DDE RID: 3550
		private const string TitleRegExKey = "title";

		// Token: 0x04000DDF RID: 3551
		private const string DescriptionRegExKey = "description";

		// Token: 0x04000DE0 RID: 3552
		private const string TitleRegEx = "<page\\s[^<>]*?title=('|\")(?<title>.*?)('|\")[^<>]*?>";

		// Token: 0x04000DE1 RID: 3553
		private const string DescriptionRegEx = "<extract[^<>]*?>(?<description>.*?)</extract>";

		// Token: 0x04000DE2 RID: 3554
		private static readonly int DesktopHostLength = 2 + ".WIKIPEDIA.ORG".Length;

		// Token: 0x04000DE3 RID: 3555
		private static readonly int MobileHostLength = 4 + ".WIKIPEDIA.ORG".Length;

		// Token: 0x04000DE4 RID: 3556
		private static Regex GetTitleRegEx = new Regex("<page\\s[^<>]*?title=('|\")(?<title>.*?)('|\")[^<>]*?>", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);

		// Token: 0x04000DE5 RID: 3557
		private static Regex GetDescriptionRegEx = new Regex("<extract[^<>]*?>(?<description>.*?)</extract>", WebPageLinkPreviewBuilder.RegExOptions, WebPageLinkPreviewBuilder.RegExTimeoutInterval);
	}
}
