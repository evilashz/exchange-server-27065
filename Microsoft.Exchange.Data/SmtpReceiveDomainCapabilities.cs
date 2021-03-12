using System;
using System.Globalization;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001A8 RID: 424
	[Serializable]
	public class SmtpReceiveDomainCapabilities
	{
		// Token: 0x06000DCF RID: 3535 RVA: 0x0002CA49 File Offset: 0x0002AC49
		public SmtpReceiveDomainCapabilities(SmtpDomainWithSubdomains domain, SmtpReceiveCapabilities capabilities, SmtpX509Identifier x509Identifier)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("Domain or the Common Name in the X509 Identifier");
			}
			this.domain = domain;
			this.capabilities = capabilities;
			this.smtpX509Identifier = x509Identifier;
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x0002CA74 File Offset: 0x0002AC74
		public SmtpReceiveDomainCapabilities(string s)
		{
			if (!SmtpReceiveDomainCapabilities.InternalTryParse(s, false, out this.domain, out this.capabilities, out this.smtpX509Identifier))
			{
				throw new StrongTypeFormatException(DataStrings.InvalidSmtpReceiveDomainCapabilities(s), "DomainCapabilities");
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0002CAAD File Offset: 0x0002ACAD
		public SmtpDomainWithSubdomains Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0002CAB5 File Offset: 0x0002ACB5
		public SmtpReceiveCapabilities Capabilities
		{
			get
			{
				return this.capabilities;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000DD3 RID: 3539 RVA: 0x0002CABD File Offset: 0x0002ACBD
		public SmtpX509Identifier SmtpX509Identifier
		{
			get
			{
				return this.smtpX509Identifier;
			}
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x0002CAC5 File Offset: 0x0002ACC5
		public static SmtpReceiveDomainCapabilities Parse(string s)
		{
			return new SmtpReceiveDomainCapabilities(s);
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0002CAD0 File Offset: 0x0002ACD0
		public static bool TryParse(string s, out SmtpReceiveDomainCapabilities result)
		{
			result = null;
			SmtpDomainWithSubdomains smtpDomainWithSubdomains;
			SmtpReceiveCapabilities smtpReceiveCapabilities;
			SmtpX509Identifier x509Identifier;
			if (!SmtpReceiveDomainCapabilities.InternalTryParse(s, false, out smtpDomainWithSubdomains, out smtpReceiveCapabilities, out x509Identifier))
			{
				return false;
			}
			result = new SmtpReceiveDomainCapabilities(smtpDomainWithSubdomains, smtpReceiveCapabilities, x509Identifier);
			return true;
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x0002CAFC File Offset: 0x0002ACFC
		public static SmtpReceiveDomainCapabilities FromADString(string s)
		{
			SmtpDomainWithSubdomains smtpDomainWithSubdomains;
			SmtpReceiveCapabilities smtpReceiveCapabilities;
			SmtpX509Identifier x509Identifier;
			if (!SmtpReceiveDomainCapabilities.InternalTryParseFromAD(s, true, out smtpDomainWithSubdomains, out smtpReceiveCapabilities, out x509Identifier))
			{
				throw new StrongTypeFormatException(DataStrings.InvalidSmtpReceiveDomainCapabilities(s), "DomainCapabilities");
			}
			return new SmtpReceiveDomainCapabilities(smtpDomainWithSubdomains, smtpReceiveCapabilities, x509Identifier);
		}

		// Token: 0x06000DD7 RID: 3543 RVA: 0x0002CB38 File Offset: 0x0002AD38
		public override string ToString()
		{
			string arg = this.capabilities.ToString().Replace(" ", null);
			string arg2 = (this.SmtpX509Identifier != null) ? this.SmtpX509Identifier.ToString().Replace(":", "::") : this.Domain.ToString();
			return arg2 + ':' + arg;
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x0002CBA0 File Offset: 0x0002ADA0
		public string ToADString()
		{
			string text = (this.SmtpX509Identifier != null) ? (':' + this.SmtpX509Identifier.ToString().Replace(":", "::")) : string.Empty;
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}{2}", new object[]
			{
				this.domain,
				(int)this.capabilities,
				text
			});
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x0002CC18 File Offset: 0x0002AE18
		public bool Equals(SmtpReceiveDomainCapabilities rhs)
		{
			return rhs != null && this.Capabilities == rhs.Capabilities && this.Domain.Equals(rhs.Domain) && ((this.SmtpX509Identifier == null && rhs.SmtpX509Identifier == null) || (this.SmtpX509Identifier != null && rhs.SmtpX509Identifier != null && this.SmtpX509Identifier.Equals(rhs.SmtpX509Identifier)));
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x0002CC82 File Offset: 0x0002AE82
		public override bool Equals(object rhs)
		{
			return this.Equals(rhs as SmtpReceiveDomainCapabilities);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x0002CC90 File Offset: 0x0002AE90
		public override int GetHashCode()
		{
			int num = this.domain.GetHashCode() ^ this.capabilities.GetHashCode();
			if (this.SmtpX509Identifier != null)
			{
				num ^= this.SmtpX509Identifier.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0002CCD4 File Offset: 0x0002AED4
		private static bool InternalTryParse(string s, bool extendedFormat, out SmtpDomainWithSubdomains domain, out SmtpReceiveCapabilities capabilities, out SmtpX509Identifier x509Identifier)
		{
			domain = null;
			x509Identifier = null;
			capabilities = SmtpReceiveCapabilities.None;
			if (string.IsNullOrWhiteSpace(s))
			{
				return false;
			}
			int num;
			string stringPart = SmtpReceiveDomainCapabilities.GetStringPart(s, true, true, 0, out num);
			if (num == s.Length - 1 || num == s.Length)
			{
				return false;
			}
			SmtpX509Identifier smtpX509Identifier = null;
			if (SmtpX509Identifier.TryParse(stringPart, out smtpX509Identifier))
			{
				domain = smtpX509Identifier.SubjectCommonName;
				x509Identifier = smtpX509Identifier;
			}
			if (domain == null && !SmtpDomainWithSubdomains.TryParse(stringPart.Trim(), out domain))
			{
				return false;
			}
			int num2;
			string stringPart2 = SmtpReceiveDomainCapabilities.GetStringPart(s, extendedFormat, false, num + 1, out num2);
			if (!SmtpReceiveDomainCapabilities.TryGetCapabilities(stringPart2, out capabilities))
			{
				domain = null;
				x509Identifier = null;
				return false;
			}
			return true;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0002CD68 File Offset: 0x0002AF68
		private static bool InternalTryParseFromAD(string s, bool extendedFormat, out SmtpDomainWithSubdomains domain, out SmtpReceiveCapabilities capabilities, out SmtpX509Identifier x509Identifier)
		{
			domain = null;
			x509Identifier = null;
			capabilities = SmtpReceiveCapabilities.None;
			if (string.IsNullOrWhiteSpace(s))
			{
				return false;
			}
			int num = s.IndexOf(':');
			if (num <= 0 || num == s.Length - 1)
			{
				return false;
			}
			string text = s.Substring(0, num);
			if (!SmtpDomainWithSubdomains.TryParse(text.Trim(), out domain))
			{
				return false;
			}
			int num2;
			string stringPart = SmtpReceiveDomainCapabilities.GetStringPart(s, extendedFormat, false, num + 1, out num2);
			if (!SmtpReceiveDomainCapabilities.TryGetCapabilities(stringPart, out capabilities))
			{
				domain = null;
				return false;
			}
			if (num2 > num && num2 < s.Length)
			{
				int num3;
				string stringPart2 = SmtpReceiveDomainCapabilities.GetStringPart(s, extendedFormat, true, num2 + 1, out num3);
				SmtpX509Identifier smtpX509Identifier = null;
				if (SmtpX509Identifier.TryParse(stringPart2, out smtpX509Identifier))
				{
					domain = smtpX509Identifier.SubjectCommonName;
					x509Identifier = smtpX509Identifier;
				}
				else
				{
					x509Identifier = null;
				}
			}
			return true;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0002CE17 File Offset: 0x0002B017
		private static bool TryGetCapabilities(string capabilitiesPart, out SmtpReceiveCapabilities capabilities)
		{
			return EnumValidator.TryParse<SmtpReceiveCapabilities>(capabilitiesPart, EnumParseOptions.AllowNumericConstants | EnumParseOptions.IgnoreCase, out capabilities);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x0002CE28 File Offset: 0x0002B028
		private static string GetStringPart(string s, bool extendedFormat, bool domainSeparatorEncoded, int startIndex, out int endIndex)
		{
			endIndex = s.Length;
			int length = endIndex - startIndex;
			if (extendedFormat)
			{
				int startIndex2 = startIndex;
				int num;
				while ((num = s.IndexOf(':', startIndex2)) > 0)
				{
					if (!domainSeparatorEncoded || num + 1 >= endIndex || s[num + 1] != ':')
					{
						endIndex = num;
						length = endIndex - startIndex;
						break;
					}
					if (num + 2 >= endIndex)
					{
						break;
					}
					startIndex2 = num + 2;
				}
			}
			return s.Substring(startIndex, length).Replace("::", ":");
		}

		// Token: 0x04000889 RID: 2185
		private const char DomainSeparator = ':';

		// Token: 0x0400088A RID: 2186
		private const string DomainSeparatorString = ":";

		// Token: 0x0400088B RID: 2187
		private const string EncodedDomainSeparatorString = "::";

		// Token: 0x0400088C RID: 2188
		private SmtpDomainWithSubdomains domain;

		// Token: 0x0400088D RID: 2189
		private SmtpReceiveCapabilities capabilities;

		// Token: 0x0400088E RID: 2190
		private SmtpX509Identifier smtpX509Identifier;
	}
}
