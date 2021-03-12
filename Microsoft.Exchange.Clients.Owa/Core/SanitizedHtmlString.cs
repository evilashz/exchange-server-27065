using System;
using System.Globalization;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020002E0 RID: 736
	public sealed class SanitizedHtmlString : SanitizedStringBase<OwaHtml>
	{
		// Token: 0x06001C4A RID: 7242 RVA: 0x000A256E File Offset: 0x000A076E
		public SanitizedHtmlString()
		{
		}

		// Token: 0x06001C4B RID: 7243 RVA: 0x000A2576 File Offset: 0x000A0776
		public SanitizedHtmlString(string rawValue) : base(rawValue)
		{
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06001C4C RID: 7244 RVA: 0x000A257F File Offset: 0x000A077F
		public static SanitizedHtmlString Empty
		{
			get
			{
				return SanitizedHtmlString.empty;
			}
		}

		// Token: 0x06001C4D RID: 7245 RVA: 0x000A2588 File Offset: 0x000A0788
		public static SanitizedHtmlString Format(string format, params object[] args)
		{
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString();
			sanitizedHtmlString.UntrustedValue = StringSanitizer<OwaHtml>.SanitizeFormat(CultureInfo.InvariantCulture, format, args);
			sanitizedHtmlString.DecreeToBeTrusted();
			return sanitizedHtmlString;
		}

		// Token: 0x06001C4E RID: 7246 RVA: 0x000A25B4 File Offset: 0x000A07B4
		public static SanitizedHtmlString Format(IFormatProvider provider, string format, params object[] args)
		{
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString();
			sanitizedHtmlString.UntrustedValue = StringSanitizer<OwaHtml>.SanitizeFormat(provider, format, args);
			sanitizedHtmlString.DecreeToBeTrusted();
			return sanitizedHtmlString;
		}

		// Token: 0x06001C4F RID: 7247 RVA: 0x000A25DC File Offset: 0x000A07DC
		public static SanitizedHtmlString FromStringId(Strings.IDs id)
		{
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString(LocalizedStrings.GetHtmlEncoded(id));
			sanitizedHtmlString.DecreeToBeTrusted();
			return sanitizedHtmlString;
		}

		// Token: 0x06001C50 RID: 7248 RVA: 0x000A25FC File Offset: 0x000A07FC
		public static SanitizedHtmlString FromStringId(Strings.IDs id, CultureInfo userCulture)
		{
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString(LocalizedStrings.GetHtmlEncoded(id, userCulture));
			sanitizedHtmlString.DecreeToBeTrusted();
			return sanitizedHtmlString;
		}

		// Token: 0x06001C51 RID: 7249 RVA: 0x000A2620 File Offset: 0x000A0820
		public static SanitizedHtmlString GetNonEncoded(Strings.IDs id)
		{
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString(LocalizedStrings.GetNonEncoded(id));
			sanitizedHtmlString.DecreeToBeTrusted();
			return sanitizedHtmlString;
		}

		// Token: 0x06001C52 RID: 7250 RVA: 0x000A2640 File Offset: 0x000A0840
		public static SanitizedHtmlString GetSanitizedStringWithoutEncoding(string s)
		{
			SanitizedHtmlString sanitizedHtmlString = new SanitizedHtmlString(s);
			sanitizedHtmlString.DecreeToBeTrusted();
			return sanitizedHtmlString;
		}

		// Token: 0x040014E9 RID: 5353
		private static readonly SanitizedHtmlString empty = new SanitizedHtmlString(string.Empty);
	}
}
