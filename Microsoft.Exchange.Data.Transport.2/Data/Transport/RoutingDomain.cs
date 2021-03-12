using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x02000085 RID: 133
	[Serializable]
	public struct RoutingDomain : IEquatable<RoutingDomain>, IComparable<RoutingDomain>
	{
		// Token: 0x060002FF RID: 767 RVA: 0x00007A48 File Offset: 0x00005C48
		public RoutingDomain(string domain)
		{
			string text;
			if (!RoutingDomain.TryParse(domain, out text))
			{
				throw new FormatException(string.Format("The format of the specified domain '{0}' isn't valid", domain ?? string.Empty));
			}
			this.domain = text;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x00007A80 File Offset: 0x00005C80
		public RoutingDomain(string domain, string type)
		{
			if (string.Equals(type, "smtp", StringComparison.OrdinalIgnoreCase))
			{
				if (!RoutingAddress.IsValidDomain(domain))
				{
					throw new FormatException(string.Format("The format of the specified domain '{0}' isn't valid", domain ?? string.Empty));
				}
			}
			else
			{
				if (string.IsNullOrEmpty(type))
				{
					throw new FormatException("A null or empty routing type isn't valid");
				}
				if (string.IsNullOrEmpty(domain))
				{
					throw new FormatException("A null or empty domain isn't valid");
				}
				if (type.IndexOfAny(RoutingDomain.CharactersNotAllowedInType) != -1)
				{
					throw new FormatException(string.Format("Domain type '{0}' contains at least one invalid character. You can't use the following characters when specifying domain types: '{1}'", type, RoutingDomain.CharactersNotAllowedInType));
				}
			}
			this.domain = type + RoutingDomain.Separator + domain;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00007B20 File Offset: 0x00005D20
		private RoutingDomain(string domain, bool requiresValidation)
		{
			if (!requiresValidation)
			{
				this.domain = domain;
				return;
			}
			string text;
			if (!RoutingDomain.TryParse(domain, out text))
			{
				throw new FormatException(string.Format("The format of the specified domain '{0}' isn't valid", domain ?? string.Empty));
			}
			this.domain = text;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000302 RID: 770 RVA: 0x00007B64 File Offset: 0x00005D64
		public string Domain
		{
			get
			{
				if (string.IsNullOrEmpty(this.domain))
				{
					return string.Empty;
				}
				int num = this.domain.IndexOf(RoutingDomain.Separator);
				return this.domain.Substring(num + 1);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00007BA4 File Offset: 0x00005DA4
		public string Type
		{
			get
			{
				if (string.IsNullOrEmpty(this.domain))
				{
					return string.Empty;
				}
				int length = this.domain.IndexOf(RoutingDomain.Separator);
				return this.domain.Substring(0, length);
			}
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00007BE2 File Offset: 0x00005DE2
		public static RoutingDomain Parse(string domain)
		{
			return new RoutingDomain(domain);
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00007BEC File Offset: 0x00005DEC
		public static bool TryParse(string domain, out RoutingDomain routingDomain)
		{
			string text;
			if (!string.IsNullOrEmpty(domain) && RoutingDomain.TryParse(domain, out text))
			{
				routingDomain = new RoutingDomain(text, false);
				return true;
			}
			routingDomain = RoutingDomain.Empty;
			return false;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00007C28 File Offset: 0x00005E28
		public static RoutingDomain GetDomainPart(RoutingAddress address)
		{
			string domainPart = address.DomainPart;
			if (domainPart != null)
			{
				return new RoutingDomain(domainPart);
			}
			return RoutingDomain.Empty;
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00007C4C File Offset: 0x00005E4C
		public static bool operator ==(RoutingDomain value1, RoutingDomain value2)
		{
			return string.Equals(value1.domain, value2.domain, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x00007C62 File Offset: 0x00005E62
		public static bool operator !=(RoutingDomain value1, RoutingDomain value2)
		{
			return !(value1 == value2);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x00007C6E File Offset: 0x00005E6E
		public bool IsSmtp()
		{
			return this.domain.StartsWith("smtp:", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x00007C81 File Offset: 0x00005E81
		public bool IsValid()
		{
			return !string.IsNullOrEmpty(this.domain);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x00007C93 File Offset: 0x00005E93
		public override int GetHashCode()
		{
			if (!string.IsNullOrEmpty(this.domain))
			{
				return this.domain.GetHashCode();
			}
			return 0;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00007CAF File Offset: 0x00005EAF
		public int CompareTo(RoutingDomain value)
		{
			return string.Compare(this.ToString(), value.ToString(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00007CD0 File Offset: 0x00005ED0
		public int CompareTo(object domain)
		{
			if (!(domain is RoutingDomain))
			{
				throw new FormatException(string.Format("The domain must be of type RoutingDomain.  Actual type: '{0}'", (domain == null) ? "null" : domain.GetType().ToString()));
			}
			return this.CompareTo((RoutingDomain)domain);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00007D0B File Offset: 0x00005F0B
		public bool Equals(RoutingDomain domain)
		{
			return this == domain;
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00007D19 File Offset: 0x00005F19
		public override bool Equals(object domain)
		{
			return domain is RoutingDomain && this == (RoutingDomain)domain;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00007D36 File Offset: 0x00005F36
		public override string ToString()
		{
			return this.domain ?? string.Empty;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00007D48 File Offset: 0x00005F48
		private static bool TryParse(string domainRepresentation, out string domain)
		{
			domain = string.Empty;
			if (string.IsNullOrEmpty(domainRepresentation))
			{
				return false;
			}
			int num = domainRepresentation.IndexOf(RoutingDomain.Separator);
			if (num != -1)
			{
				string text = domainRepresentation.Substring(0, num);
				string value = domainRepresentation.Substring(num + 1);
				if (string.IsNullOrEmpty(text) || string.IsNullOrEmpty(value) || text.IndexOfAny(RoutingDomain.CharactersNotAllowedInType) != -1)
				{
					return false;
				}
				if (text.Equals("smtp", StringComparison.OrdinalIgnoreCase) && !RoutingAddress.IsValidDomain(value))
				{
					return false;
				}
				domain = domainRepresentation;
			}
			else
			{
				if (!RoutingAddress.IsValidDomain(domainRepresentation))
				{
					return false;
				}
				domain = "smtp:" + domainRepresentation;
			}
			return true;
		}

		// Token: 0x040001EC RID: 492
		public const string Smtp = "smtp";

		// Token: 0x040001ED RID: 493
		private const string SmtpWithSeperator = "smtp:";

		// Token: 0x040001EE RID: 494
		public static readonly RoutingDomain Empty = default(RoutingDomain);

		// Token: 0x040001EF RID: 495
		internal static readonly char Separator = ':';

		// Token: 0x040001F0 RID: 496
		private static readonly char[] CharactersNotAllowedInType = new char[]
		{
			':'
		};

		// Token: 0x040001F1 RID: 497
		private string domain;
	}
}
