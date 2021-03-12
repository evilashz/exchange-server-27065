using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000095 RID: 149
	[Serializable]
	public class SmtpDomainWithSubdomains : IEquatable<SmtpDomainWithSubdomains>
	{
		// Token: 0x06000453 RID: 1107 RVA: 0x0000F55E File Offset: 0x0000D75E
		public SmtpDomainWithSubdomains(SmtpDomain domain, bool includeSubdomains)
		{
			if (domain == null && !includeSubdomains)
			{
				throw new StrongTypeFormatException(DataStrings.InvalidSmtpDomain, "Domain");
			}
			this.domain = domain;
			this.includeSubdomains = includeSubdomains;
		}

		// Token: 0x06000454 RID: 1108 RVA: 0x0000F596 File Offset: 0x0000D796
		public SmtpDomainWithSubdomains(string s) : this(s, false)
		{
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0000F5A0 File Offset: 0x0000D7A0
		public SmtpDomainWithSubdomains(string s, bool includeSubdomains)
		{
			if (SmtpDomainWithSubdomains.InternalTryParse(s, out this.domain, out this.includeSubdomains))
			{
				this.includeSubdomains = (this.includeSubdomains || includeSubdomains);
				return;
			}
			throw new StrongTypeFormatException(DataStrings.InvalidSmtpDomainName(s), "Domain");
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000F5F2 File Offset: 0x0000D7F2
		public string Address
		{
			get
			{
				return this.ToString();
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000457 RID: 1111 RVA: 0x0000F5FA File Offset: 0x0000D7FA
		public bool IncludeSubDomains
		{
			get
			{
				return this.includeSubdomains;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000F602 File Offset: 0x0000D802
		public string Domain
		{
			get
			{
				if (!this.IsStar)
				{
					return this.domain.Domain;
				}
				return "*";
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x0000F61D File Offset: 0x0000D81D
		public SmtpDomain SmtpDomain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000F625 File Offset: 0x0000D825
		public bool IsStar
		{
			get
			{
				return this.domain == null;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x0000F630 File Offset: 0x0000D830
		public static SmtpDomainWithSubdomains Parse(string s)
		{
			SmtpDomainWithSubdomains result;
			if (SmtpDomainWithSubdomains.TryParse(s, out result))
			{
				return result;
			}
			throw new StrongTypeFormatException(DataStrings.InvalidSmtpDomainName(s), "Domain");
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0000F660 File Offset: 0x0000D860
		public static bool TryParse(string s, out SmtpDomainWithSubdomains result)
		{
			SmtpDomain smtpDomain;
			bool flag;
			if (SmtpDomainWithSubdomains.InternalTryParse(s, out smtpDomain, out flag))
			{
				result = new SmtpDomainWithSubdomains(smtpDomain, flag);
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0000F688 File Offset: 0x0000D888
		public override string ToString()
		{
			if (this.domain == null)
			{
				return "*";
			}
			if (this.includeSubdomains)
			{
				return "*." + this.domain.Domain;
			}
			return this.domain.Domain;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0000F6C4 File Offset: 0x0000D8C4
		public int Match(string toMatch)
		{
			if (this.domain == null)
			{
				return 0;
			}
			if (this.includeSubdomains)
			{
				if (toMatch.Length >= this.Domain.Length && toMatch.EndsWith(this.Domain, StringComparison.OrdinalIgnoreCase))
				{
					string text = toMatch.Substring(0, toMatch.Length - this.Domain.Length);
					if (text.Length == 0 || text.EndsWith("."))
					{
						return this.Domain.Length;
					}
				}
			}
			else if (toMatch.Length == this.Domain.Length && toMatch.Equals(this.Domain, StringComparison.OrdinalIgnoreCase))
			{
				return this.Domain.Length;
			}
			return -1;
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0000F770 File Offset: 0x0000D970
		private static bool InternalTryParse(string s, out SmtpDomain domain, out bool includeSubdomains)
		{
			domain = null;
			includeSubdomains = false;
			if (string.IsNullOrEmpty(s) || s.Trim().Length == 0)
			{
				return false;
			}
			if (s.Length == 1 && string.Equals(s, "*", StringComparison.OrdinalIgnoreCase))
			{
				domain = null;
				includeSubdomains = true;
				return true;
			}
			includeSubdomains = s.StartsWith("*.", StringComparison.OrdinalIgnoreCase);
			if (includeSubdomains)
			{
				s = s.Substring(2);
			}
			return SmtpDomain.TryParse(s, out domain);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x0000F7DB File Offset: 0x0000D9DB
		public bool Equals(SmtpDomainWithSubdomains rhs)
		{
			if (this.includeSubdomains != rhs.includeSubdomains)
			{
				return false;
			}
			if (this.domain == null)
			{
				return rhs.domain == null;
			}
			return rhs.domain != null && this.domain.Equals(rhs.domain);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x0000F81C File Offset: 0x0000DA1C
		public override bool Equals(object comparand)
		{
			SmtpDomainWithSubdomains smtpDomainWithSubdomains = comparand as SmtpDomainWithSubdomains;
			return smtpDomainWithSubdomains != null && this.Equals(smtpDomainWithSubdomains);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0000F83C File Offset: 0x0000DA3C
		public override int GetHashCode()
		{
			if (this.hashCode == -1)
			{
				int num = (this.domain == null) ? 0 : this.domain.GetHashCode();
				num ^= (this.includeSubdomains ? 1 : 0);
				this.hashCode = num;
			}
			return this.hashCode;
		}

		// Token: 0x0400021C RID: 540
		private const string Star = "*";

		// Token: 0x0400021D RID: 541
		private const string Dot = ".";

		// Token: 0x0400021E RID: 542
		private const string StarDot = "*.";

		// Token: 0x0400021F RID: 543
		public const int MaxLength = 257;

		// Token: 0x04000220 RID: 544
		private readonly SmtpDomain domain;

		// Token: 0x04000221 RID: 545
		private readonly bool includeSubdomains;

		// Token: 0x04000222 RID: 546
		private int hashCode = -1;

		// Token: 0x04000223 RID: 547
		public static readonly SmtpDomainWithSubdomains StarDomain = new SmtpDomainWithSubdomains("*");
	}
}
