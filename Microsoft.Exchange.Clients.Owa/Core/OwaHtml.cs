using System;
using System.IO;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020002DF RID: 735
	public class OwaHtml : ISanitizingPolicy
	{
		// Token: 0x06001C46 RID: 7238 RVA: 0x000A251C File Offset: 0x000A071C
		public string Sanitize(string str)
		{
			return Utilities.HtmlEncode(str);
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x000A2524 File Offset: 0x000A0724
		public void Sanitize(TextWriter writer, string str)
		{
			Utilities.HtmlEncode(str, writer);
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x000A2530 File Offset: 0x000A0730
		public string SanitizeFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
			SanitizingFormatProvider<OwaHtml> sanitizingFormatProvider = this.owaFormatProvider;
			if (sanitizingFormatProvider == null || sanitizingFormatProvider.InnerFormatProvider != formatProvider)
			{
				sanitizingFormatProvider = (this.owaFormatProvider = new SanitizingFormatProvider<OwaHtml>(formatProvider));
			}
			return string.Format(sanitizingFormatProvider, format, args);
		}

		// Token: 0x06001C49 RID: 7241 RVA: 0x000A2566 File Offset: 0x000A0766
		public string EscapeJScript(string rawValue)
		{
			return Utilities.JavascriptEncode(rawValue);
		}

		// Token: 0x040014E8 RID: 5352
		private SanitizingFormatProvider<OwaHtml> owaFormatProvider;
	}
}
