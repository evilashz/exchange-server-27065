using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000160 RID: 352
	internal class FailureDsnComparer : IEqualityComparer<string>
	{
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x0003F10A File Offset: 0x0003D30A
		public static FailureDsnComparer Instance
		{
			get
			{
				return FailureDsnComparer.instance;
			}
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x0003F114 File Offset: 0x0003D314
		public bool Equals(string s1, string s2)
		{
			string x;
			string x2;
			string y;
			string y2;
			return object.ReferenceEquals(s1, s2) || (this.TryParseStatusCodes(s1, out x, out x2) && this.TryParseStatusCodes(s2, out y, out y2) && StringComparer.OrdinalIgnoreCase.Equals(x, y) && StringComparer.OrdinalIgnoreCase.Equals(x2, y2));
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x0003F164 File Offset: 0x0003D364
		public int GetHashCode(string s)
		{
			string text;
			string text2;
			if (!this.TryParseStatusCodes(s, out text, out text2))
			{
				return 0;
			}
			StringBuilder stringBuilder = new StringBuilder(text.Length + text2.Length + 1);
			stringBuilder.Append(text);
			stringBuilder.Append(' ');
			stringBuilder.Append(text2);
			return StringComparer.OrdinalIgnoreCase.GetHashCode(stringBuilder.ToString());
		}

		// Token: 0x06000F84 RID: 3972 RVA: 0x0003F1C0 File Offset: 0x0003D3C0
		private bool TryParseStatusCodes(string s, out string statusCode, out string enhancedStatusCode)
		{
			statusCode = null;
			enhancedStatusCode = null;
			if (!SmtpResponseParser.IsValidStatusCode(s))
			{
				return false;
			}
			statusCode = s.Substring(0, 3);
			EnhancedStatusCodeImpl enhancedStatusCodeImpl;
			enhancedStatusCode = (EnhancedStatusCodeImpl.TryParse(s, 4, out enhancedStatusCodeImpl) ? enhancedStatusCodeImpl.Value : string.Empty);
			return true;
		}

		// Token: 0x040007C7 RID: 1991
		private static readonly FailureDsnComparer instance = new FailureDsnComparer();
	}
}
