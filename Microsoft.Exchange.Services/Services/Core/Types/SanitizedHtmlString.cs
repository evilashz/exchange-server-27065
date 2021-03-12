using System;
using System.Globalization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200086B RID: 2155
	public sealed class SanitizedHtmlString : SanitizedStringBase<OwaHtml>
	{
		// Token: 0x06003DC4 RID: 15812 RVA: 0x000D7DCC File Offset: 0x000D5FCC
		public SanitizedHtmlString()
		{
		}

		// Token: 0x06003DC5 RID: 15813 RVA: 0x000D7DD4 File Offset: 0x000D5FD4
		public SanitizedHtmlString(string rawValue) : base(rawValue)
		{
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06003DC6 RID: 15814 RVA: 0x000D7DDD File Offset: 0x000D5FDD
		public static SanitizedHtmlString Empty
		{
			get
			{
				return SanitizedHtmlString.empty;
			}
		}

		// Token: 0x06003DC7 RID: 15815 RVA: 0x000D7DE4 File Offset: 0x000D5FE4
		public static SanitizedHtmlString Format(string format, params object[] args)
		{
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString();
			sanitizedHtmlString.UntrustedValue = StringSanitizer<OwaHtml>.SanitizeFormat(CultureInfo.InvariantCulture, format, args);
			sanitizedHtmlString.DecreeToBeTrusted();
			return sanitizedHtmlString;
		}

		// Token: 0x06003DC8 RID: 15816 RVA: 0x000D7E10 File Offset: 0x000D6010
		public static SanitizedHtmlString Format(IFormatProvider provider, string format, params object[] args)
		{
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString();
			sanitizedHtmlString.UntrustedValue = StringSanitizer<OwaHtml>.SanitizeFormat(provider, format, args);
			sanitizedHtmlString.DecreeToBeTrusted();
			return sanitizedHtmlString;
		}

		// Token: 0x06003DC9 RID: 15817 RVA: 0x000D7E38 File Offset: 0x000D6038
		public static SanitizedHtmlString GetNonEncoded(LocalizedString localizedString)
		{
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString(localizedString);
			sanitizedHtmlString.DecreeToBeTrusted();
			return sanitizedHtmlString;
		}

		// Token: 0x06003DCA RID: 15818 RVA: 0x000D7E58 File Offset: 0x000D6058
		public static SanitizedHtmlString GetSanitizedStringWithoutEncoding(string s)
		{
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString(s);
			sanitizedHtmlString.DecreeToBeTrusted();
			return sanitizedHtmlString;
		}

		// Token: 0x0400237E RID: 9086
		private static readonly SanitizedHtmlString empty = new SanitizedHtmlString(string.Empty);
	}
}
