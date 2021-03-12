using System;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000094 RID: 148
	[Serializable]
	public class SmtpDomain
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0000F458 File Offset: 0x0000D658
		public SmtpDomain(string domain) : this(domain, true)
		{
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x0000F462 File Offset: 0x0000D662
		protected SmtpDomain(string domain, bool check)
		{
			if (check && !SmtpAddress.IsValidDomain(domain))
			{
				throw new FormatException(DataStrings.InvalidSmtpDomainName(domain));
			}
			this.domain = domain;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000F494 File Offset: 0x0000D694
		public string Domain
		{
			get
			{
				return this.domain;
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x0000F49C File Offset: 0x0000D69C
		public static SmtpDomain Parse(string domain)
		{
			return new SmtpDomain(domain);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x0000F4A4 File Offset: 0x0000D6A4
		public static bool TryParse(string domain, out SmtpDomain obj)
		{
			obj = null;
			if (!string.IsNullOrEmpty(domain) && SmtpAddress.IsValidDomain(domain))
			{
				obj = new SmtpDomain(domain, false);
				return true;
			}
			return false;
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x0000F4C8 File Offset: 0x0000D6C8
		public static SmtpDomain GetDomainPart(RoutingAddress address)
		{
			string domainPart = address.DomainPart;
			if (domainPart != null)
			{
				return new SmtpDomain(domainPart, false);
			}
			return null;
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000F4E9 File Offset: 0x0000D6E9
		public bool Equals(SmtpDomain rhs)
		{
			return rhs != null && this.domain.Equals(rhs.domain, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x0000F504 File Offset: 0x0000D704
		public override bool Equals(object comparand)
		{
			SmtpDomain smtpDomain = comparand as SmtpDomain;
			return smtpDomain != null && this.Equals(smtpDomain);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x0000F524 File Offset: 0x0000D724
		public override int GetHashCode()
		{
			if (this.hashCode == -1)
			{
				this.hashCode = ((this.domain == null) ? 0 : this.domain.ToLowerInvariant().GetHashCode());
			}
			return this.hashCode;
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x0000F556 File Offset: 0x0000D756
		public override string ToString()
		{
			return this.domain;
		}

		// Token: 0x04000219 RID: 537
		public const int MaxLength = 255;

		// Token: 0x0400021A RID: 538
		private string domain;

		// Token: 0x0400021B RID: 539
		private int hashCode = -1;
	}
}
