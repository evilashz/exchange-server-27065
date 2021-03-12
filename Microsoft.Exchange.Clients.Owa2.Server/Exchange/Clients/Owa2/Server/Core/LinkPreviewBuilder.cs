using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Services;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020002DF RID: 735
	internal abstract class LinkPreviewBuilder
	{
		// Token: 0x060018C8 RID: 6344 RVA: 0x00055BDC File Offset: 0x00053DDC
		public LinkPreviewBuilder(GetLinkPreviewRequest request, RequestDetailsLogger logger, Uri responseUri, bool isVideo)
		{
			this.id = request.Id;
			this.url = request.Url;
			this.requestStartTimeMilliseconds = request.RequestStartTimeMilliseconds;
			this.logger = logger;
			this.responseUri = responseUri;
			this.isVideo = isVideo;
		}

		// Token: 0x060018C9 RID: 6345
		internal abstract GetLinkPreviewResponse Execute();

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x00055C29 File Offset: 0x00053E29
		protected bool IsVideo
		{
			get
			{
				return this.isVideo;
			}
		}

		// Token: 0x060018CB RID: 6347 RVA: 0x00055C31 File Offset: 0x00053E31
		protected virtual LinkPreview CreateLinkPreviewInstance()
		{
			return new LinkPreview();
		}

		// Token: 0x060018CC RID: 6348 RVA: 0x00055C38 File Offset: 0x00053E38
		public static Match ExecuteRegEx(Regex regEx, string matchString, string propertyName)
		{
			Match result = null;
			try
			{
				result = regEx.Match(matchString);
			}
			catch (RegexMatchTimeoutException)
			{
				GetLinkPreview.ThrowInvalidRequestException("RegExTimeout", string.Format("The regex timed out on property {0}.", propertyName));
			}
			return result;
		}

		// Token: 0x060018CD RID: 6349 RVA: 0x00055C7C File Offset: 0x00053E7C
		protected static MatchCollection ExecuteRegExForMatchCollection(Regex regEx, string matchString, string propertyName)
		{
			MatchCollection result = null;
			try
			{
				result = regEx.Matches(matchString);
			}
			catch (RegexMatchTimeoutException)
			{
				GetLinkPreview.ThrowInvalidRequestException("RegExTimeout", string.Format("The regex timed out on property {0}.", propertyName));
			}
			return result;
		}

		// Token: 0x060018CE RID: 6350 RVA: 0x00055CC0 File Offset: 0x00053EC0
		protected static string ConvertToSafeHtml(string html)
		{
			string text = null;
			if (html != null)
			{
				HtmlToHtml htmlToHtml = new HtmlToHtml();
				htmlToHtml.FilterHtml = true;
				htmlToHtml.OutputHtmlFragment = true;
				using (TextReader textReader = new StringReader(html))
				{
					using (TextWriter textWriter = new StringWriter())
					{
						try
						{
							htmlToHtml.Convert(textReader, textWriter);
							text = textWriter.ToString().Trim();
							if (text.StartsWith("<div>", StringComparison.OrdinalIgnoreCase))
							{
								text = text.Substring("<div>".Length, text.Length - "<div>".Length - "</div>".Length);
							}
						}
						catch (ExchangeDataException localizedException)
						{
							GetLinkPreview.ThrowLocalizedException("HtmlConversionFailed", localizedException);
						}
					}
				}
			}
			return text;
		}

		// Token: 0x04000D43 RID: 3395
		protected const string TitlePropertyName = "title";

		// Token: 0x04000D44 RID: 3396
		protected const string DescriptionPropertyName = "description";

		// Token: 0x04000D45 RID: 3397
		protected const string ImagePropertyName = "image";

		// Token: 0x04000D46 RID: 3398
		protected const int TitleTruncationLength = 400;

		// Token: 0x04000D47 RID: 3399
		protected const int DescriptionTruncationLength = 1000;

		// Token: 0x04000D48 RID: 3400
		protected const int MaxImageUrlLength = 500;

		// Token: 0x04000D49 RID: 3401
		private readonly bool isVideo;

		// Token: 0x04000D4A RID: 3402
		protected readonly string id;

		// Token: 0x04000D4B RID: 3403
		protected readonly string url;

		// Token: 0x04000D4C RID: 3404
		protected readonly long requestStartTimeMilliseconds;

		// Token: 0x04000D4D RID: 3405
		protected readonly RequestDetailsLogger logger;

		// Token: 0x04000D4E RID: 3406
		protected readonly Uri responseUri;
	}
}
