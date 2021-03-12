using System;
using System.Collections.Specialized;
using System.Web;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D8A RID: 3466
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AnonymousPublishingUrl
	{
		// Token: 0x06007756 RID: 30550 RVA: 0x0020E7B8 File Offset: 0x0020C9B8
		public AnonymousPublishingUrl(Uri url)
		{
			Util.ThrowOnNullArgument(url, "url");
			this.Url = url;
			if (!AnonymousPublishingUrl.IsValidAnonymousPublishingUrl(this.Url))
			{
				throw new AnonymousPublishingUrlValidationException(this.Url.ToString());
			}
			this.Resource = this.Url.Segments[this.Url.Segments.Length - 1];
			this.QueryString = new NameValueCollection();
			if (this.Url.Query.Length > 0)
			{
				HttpRequest httpRequest = new HttpRequest(string.Empty, this.Url.ToString(), this.Url.Query.Substring(1));
				foreach (string name in httpRequest.QueryString.AllKeys)
				{
					this.QueryString[name] = httpRequest.QueryString[name];
				}
			}
			this.ParameterSegments = new string[this.Url.Segments.Length - 4];
			int num = 0;
			for (int j = 3; j < this.Url.Segments.Length - 1; j++)
			{
				this.ParameterSegments[num++] = this.Url.Segments[j].Substring(0, this.Url.Segments[j].Length - 1);
			}
		}

		// Token: 0x17001FE5 RID: 8165
		// (get) Token: 0x06007757 RID: 30551 RVA: 0x0020E908 File Offset: 0x0020CB08
		// (set) Token: 0x06007758 RID: 30552 RVA: 0x0020E910 File Offset: 0x0020CB10
		public Uri Url { get; private set; }

		// Token: 0x17001FE6 RID: 8166
		// (get) Token: 0x06007759 RID: 30553 RVA: 0x0020E919 File Offset: 0x0020CB19
		// (set) Token: 0x0600775A RID: 30554 RVA: 0x0020E921 File Offset: 0x0020CB21
		public string Resource { get; private set; }

		// Token: 0x17001FE7 RID: 8167
		// (get) Token: 0x0600775B RID: 30555 RVA: 0x0020E92A File Offset: 0x0020CB2A
		// (set) Token: 0x0600775C RID: 30556 RVA: 0x0020E932 File Offset: 0x0020CB32
		public NameValueCollection QueryString { get; private set; }

		// Token: 0x17001FE8 RID: 8168
		// (get) Token: 0x0600775D RID: 30557 RVA: 0x0020E93B File Offset: 0x0020CB3B
		// (set) Token: 0x0600775E RID: 30558 RVA: 0x0020E943 File Offset: 0x0020CB43
		public string[] ParameterSegments { get; private set; }

		// Token: 0x0600775F RID: 30559 RVA: 0x0020E94C File Offset: 0x0020CB4C
		public static bool IsValidAnonymousPublishingUrl(Uri urlToCheck)
		{
			return urlToCheck != null && urlToCheck.Segments.Length >= 4 && string.Equals(urlToCheck.Segments[0], AnonymousPublishingUrl.baseSegments[0], StringComparison.OrdinalIgnoreCase) && string.Equals(urlToCheck.Segments[1], AnonymousPublishingUrl.baseSegments[1], StringComparison.OrdinalIgnoreCase) && string.Equals(urlToCheck.Segments[2], AnonymousPublishingUrl.baseSegments[2], StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06007760 RID: 30560 RVA: 0x0020E9B4 File Offset: 0x0020CBB4
		public static bool IsValidBaseAnonymousPublishingUrl(Uri urlToCheck)
		{
			return urlToCheck != null && urlToCheck.Segments.Length == 3 && string.Equals(urlToCheck.Segments[0], AnonymousPublishingUrl.baseSegments[0], StringComparison.OrdinalIgnoreCase) && string.Equals(urlToCheck.Segments[1], AnonymousPublishingUrl.baseSegments[1], StringComparison.OrdinalIgnoreCase) && (string.Equals(urlToCheck.Segments[2], AnonymousPublishingUrl.baseSegments[2], StringComparison.OrdinalIgnoreCase) || string.Equals(urlToCheck.Segments[2], "calendar", StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06007761 RID: 30561 RVA: 0x0020EA31 File Offset: 0x0020CC31
		public override string ToString()
		{
			return this.Url.OriginalString;
		}

		// Token: 0x040052A2 RID: 21154
		private const string OwaVdirName = "owa";

		// Token: 0x040052A3 RID: 21155
		private const string AnonymousVdirName = "calendar";

		// Token: 0x040052A4 RID: 21156
		private static readonly string[] baseSegments = new string[]
		{
			"/",
			"owa/",
			"calendar/"
		};
	}
}
