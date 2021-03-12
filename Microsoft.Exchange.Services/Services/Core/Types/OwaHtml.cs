using System;
using System.IO;
using System.Net;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000839 RID: 2105
	public class OwaHtml : ISanitizingPolicy
	{
		// Token: 0x06003CBC RID: 15548 RVA: 0x000D6340 File Offset: 0x000D4540
		public string Sanitize(string str)
		{
			return WebUtility.HtmlEncode(str);
		}

		// Token: 0x06003CBD RID: 15549 RVA: 0x000D6348 File Offset: 0x000D4548
		public void Sanitize(TextWriter writer, string str)
		{
			WebUtility.HtmlEncode(str, writer);
		}

		// Token: 0x06003CBE RID: 15550 RVA: 0x000D6354 File Offset: 0x000D4554
		public string SanitizeFormat(IFormatProvider formatProvider, string format, params object[] args)
		{
			SanitizingFormatProvider<OwaHtml> sanitizingFormatProvider = this.owaFormatProvider;
			if (sanitizingFormatProvider == null || sanitizingFormatProvider.InnerFormatProvider != formatProvider)
			{
				sanitizingFormatProvider = (this.owaFormatProvider = new SanitizingFormatProvider<OwaHtml>(formatProvider));
			}
			return string.Format(sanitizingFormatProvider, format, args);
		}

		// Token: 0x04002183 RID: 8579
		private SanitizingFormatProvider<OwaHtml> owaFormatProvider;
	}
}
