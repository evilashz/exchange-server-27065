using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Management.Transport;

namespace Microsoft.Office.CompliancePolicy.Validators
{
	// Token: 0x0200013D RID: 317
	internal class SharepointSource
	{
		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003342C File Offset: 0x0003162C
		public static SharepointSource Parse(string identity)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("identity", identity);
			string[] array = identity.Split(SharepointSource.tokenSeparator, StringSplitOptions.RemoveEmptyEntries);
			if (array.Length == 0)
			{
				throw new SpIdentityFormatException(Strings.SpParserVersionNotSpecified);
			}
			int num;
			if (!int.TryParse(array[0], out num))
			{
				throw new SpIdentityFormatException(Strings.SpParserInvalidVersionType(array[0]));
			}
			if (!SharepointSource.identityParsers.ContainsKey(num))
			{
				throw new SpIdentityFormatException(Strings.SpParserVersionNotSupported(num));
			}
			return SharepointSource.identityParsers[num](array);
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x000334A8 File Offset: 0x000316A8
		private static SharepointSource VersionOneIdentityParser(string[] tokens)
		{
			if (6 != tokens.Length)
			{
				throw new SpIdentityFormatException(Strings.SpParserUnexpectedNumberOfTokens(1, 6, tokens.Length));
			}
			if (!string.Equals("Web", tokens[1], StringComparison.OrdinalIgnoreCase))
			{
				throw new SpIdentityFormatException(Strings.SpParserUnexpectedContainerType("Web", tokens[1]));
			}
			if (!Uri.IsWellFormedUriString(tokens[2], UriKind.Absolute))
			{
				throw new SpIdentityFormatException(Strings.SpParserInvalidSiteUrl(tokens[2]));
			}
			string siteUrl = tokens[2];
			string title = tokens[3];
			Guid siteId;
			if (!Guid.TryParse(tokens[4], out siteId))
			{
				throw new SpIdentityFormatException(Strings.SpParserInvalidSiteId(tokens[4]));
			}
			Guid webId;
			if (!Guid.TryParse(tokens[5], out webId))
			{
				throw new SpIdentityFormatException(Strings.SpParserInvalidWebId(tokens[5]));
			}
			return new SharepointSource(siteUrl, title, siteId, webId);
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x0003354C File Offset: 0x0003174C
		public SharepointSource(string siteUrl, string title, Guid siteId, Guid webId)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("siteUrl", siteUrl);
			ArgumentValidator.ThrowIfNullOrEmpty("title", title);
			this.SiteUrl = siteUrl;
			this.Title = title;
			this.SiteId = siteId;
			this.WebId = webId;
			this.SetIdentity();
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00033598 File Offset: 0x00031798
		private void SetIdentity()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(1);
			stringBuilder.Append(SharepointSource.tokenSeparator[0]);
			stringBuilder.Append("Web");
			stringBuilder.Append(SharepointSource.tokenSeparator[0]);
			stringBuilder.Append(this.SiteUrl);
			stringBuilder.Append(SharepointSource.tokenSeparator[0]);
			stringBuilder.Append(this.Title);
			stringBuilder.Append(SharepointSource.tokenSeparator[0]);
			stringBuilder.Append(this.SiteId);
			stringBuilder.Append(SharepointSource.tokenSeparator[0]);
			stringBuilder.Append(this.WebId);
			stringBuilder.Append(SharepointSource.tokenSeparator[0]);
			this.Identity = stringBuilder.ToString();
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0003365D File Offset: 0x0003185D
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x00033665 File Offset: 0x00031865
		public string Identity { get; private set; }

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06000DEE RID: 3566 RVA: 0x0003366E File Offset: 0x0003186E
		// (set) Token: 0x06000DEF RID: 3567 RVA: 0x00033676 File Offset: 0x00031876
		public Guid WebId { get; private set; }

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06000DF0 RID: 3568 RVA: 0x0003367F File Offset: 0x0003187F
		// (set) Token: 0x06000DF1 RID: 3569 RVA: 0x00033687 File Offset: 0x00031887
		public Guid SiteId { get; private set; }

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x06000DF2 RID: 3570 RVA: 0x00033690 File Offset: 0x00031890
		// (set) Token: 0x06000DF3 RID: 3571 RVA: 0x00033698 File Offset: 0x00031898
		public string SiteUrl { get; private set; }

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x000336A1 File Offset: 0x000318A1
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x000336A9 File Offset: 0x000318A9
		public string Title { get; private set; }

		// Token: 0x040004B3 RID: 1203
		private const int CurrentVersion = 1;

		// Token: 0x040004B4 RID: 1204
		private const string ContainerType = "Web";

		// Token: 0x040004B5 RID: 1205
		private static readonly char[] tokenSeparator = new char[]
		{
			';'
		};

		// Token: 0x040004B6 RID: 1206
		private static readonly Dictionary<int, SharepointSource.ParserDelegate> identityParsers = new Dictionary<int, SharepointSource.ParserDelegate>
		{
			{
				1,
				new SharepointSource.ParserDelegate(SharepointSource.VersionOneIdentityParser)
			}
		};

		// Token: 0x0200013E RID: 318
		// (Invoke) Token: 0x06000DF8 RID: 3576
		private delegate SharepointSource ParserDelegate(string[] tokens);
	}
}
