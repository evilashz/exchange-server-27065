using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001AB RID: 427
	[Serializable]
	public class SmtpX509IdentifierEx : SmtpX509Identifier
	{
		// Token: 0x06000DF6 RID: 3574 RVA: 0x0002D25C File Offset: 0x0002B45C
		public SmtpX509IdentifierEx(string subject, SmtpDomainWithSubdomains subjectCN, string issuer, SmtpDomainWithSubdomains[] domains) : base(subject, subjectCN, issuer)
		{
			this.CertificateDomains = domains;
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x0002D270 File Offset: 0x0002B470
		public SmtpX509IdentifierEx(string smtpX509Identifier)
		{
			string certificateSubject;
			SmtpDomainWithSubdomains subjectCommonName;
			string certificateIssuer;
			SmtpDomainWithSubdomains[] certificateDomains;
			bool flag;
			if (!SmtpX509IdentifierEx.InternalTryParse(smtpX509Identifier, out certificateSubject, out subjectCommonName, out certificateIssuer, out certificateDomains, out flag))
			{
				string s = string.IsNullOrEmpty(smtpX509Identifier) ? string.Empty : smtpX509Identifier;
				string message;
				if (flag)
				{
					message = DataStrings.InvalidDomainInSmtpX509Identifier(s);
				}
				else
				{
					message = DataStrings.InvalidSmtpX509Identifier(s);
				}
				throw new StrongTypeFormatException(message, "SmtpX509IdentifierEx");
			}
			base.CertificateIssuer = certificateIssuer;
			base.CertificateSubject = certificateSubject;
			base.SubjectCommonName = subjectCommonName;
			this.CertificateDomains = certificateDomains;
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0002D2F2 File Offset: 0x0002B4F2
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x0002D2FA File Offset: 0x0002B4FA
		public SmtpDomainWithSubdomains[] CertificateDomains { get; private set; }

		// Token: 0x06000DFA RID: 3578 RVA: 0x0002D303 File Offset: 0x0002B503
		public new static SmtpX509IdentifierEx Parse(string s)
		{
			return new SmtpX509IdentifierEx(s);
		}

		// Token: 0x06000DFB RID: 3579 RVA: 0x0002D30C File Offset: 0x0002B50C
		public static bool TryParse(string s, out SmtpX509IdentifierEx result)
		{
			result = null;
			string subject;
			SmtpDomainWithSubdomains subjectCN;
			string issuer;
			SmtpDomainWithSubdomains[] domains;
			bool flag;
			if (!SmtpX509IdentifierEx.InternalTryParse(s, out subject, out subjectCN, out issuer, out domains, out flag))
			{
				return false;
			}
			result = new SmtpX509IdentifierEx(subject, subjectCN, issuer, domains);
			return true;
		}

		// Token: 0x06000DFC RID: 3580 RVA: 0x0002D33C File Offset: 0x0002B53C
		public bool Matches(TlsCertificate certificate)
		{
			if (certificate == null)
			{
				return false;
			}
			if (certificate.TlsCertificateName is SmtpDomainWithSubdomains)
			{
				return this.Matches(certificate.TlsCertificateName as SmtpDomainWithSubdomains);
			}
			return certificate.TlsCertificateName is SmtpX509Identifier && this.Matches(certificate.TlsCertificateName as SmtpX509Identifier);
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0002D3C0 File Offset: 0x0002B5C0
		public bool Matches(SmtpDomainWithSubdomains fqdn)
		{
			if (fqdn == null)
			{
				return false;
			}
			string fqdnString = fqdn.ToString();
			return base.SubjectCommonName.Match(fqdnString) >= 0 || fqdn.Match(base.SubjectCommonName.ToString()) >= 0 || (this.CertificateDomains != null && this.CertificateDomains.Any((SmtpDomainWithSubdomains domain) => domain.Match(fqdnString) >= 0 || fqdn.Match(domain.ToString()) >= 0));
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0002D448 File Offset: 0x0002B648
		public bool Matches(SmtpX509Identifier certificate)
		{
			return certificate != null && string.Equals(base.CertificateSubject, certificate.CertificateSubject, StringComparison.OrdinalIgnoreCase) && (string.IsNullOrEmpty(base.CertificateIssuer) || string.IsNullOrEmpty(certificate.CertificateIssuer) || string.Equals(base.CertificateIssuer, certificate.CertificateIssuer, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0002D4A0 File Offset: 0x0002B6A0
		public bool Equals(SmtpX509IdentifierEx rhs)
		{
			if (!base.Equals(rhs))
			{
				return false;
			}
			if (this.CertificateDomains != null)
			{
				if (rhs.CertificateDomains == null)
				{
					return false;
				}
				if (this.CertificateDomains.Length != rhs.CertificateDomains.Length)
				{
					return false;
				}
				foreach (SmtpDomainWithSubdomains value in this.CertificateDomains)
				{
					if (!rhs.CertificateDomains.Contains(value))
					{
						return false;
					}
				}
			}
			else if (rhs.CertificateDomains != null)
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0002D518 File Offset: 0x0002B718
		public override bool Equals(object rhs)
		{
			return this.Equals(rhs as SmtpX509IdentifierEx);
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x0002D528 File Offset: 0x0002B728
		public override string ToString()
		{
			string arg = string.Empty;
			if (this.CertificateDomains != null && this.CertificateDomains.Length > 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (SmtpDomainWithSubdomains value in this.CertificateDomains)
				{
					stringBuilder.Append(value);
					stringBuilder.Append(";");
				}
				arg = "<D>" + stringBuilder.ToString(0, stringBuilder.Length - 1);
			}
			return string.Format("{0}{1}", base.ToString(), arg);
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0002D5B4 File Offset: 0x0002B7B4
		public override int GetHashCode()
		{
			SmtpDomainWithSubdomains[] array = this.CertificateDomains ?? new SmtpDomainWithSubdomains[0];
			return base.GetHashCode() ^ array.GetHashCode();
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x0002D5E0 File Offset: 0x0002B7E0
		private static bool InternalTryParse(string s, out string subject, out SmtpDomainWithSubdomains commonName, out string issuer, out SmtpDomainWithSubdomains[] domains, out bool invalidDomainError)
		{
			domains = null;
			Match match = Regex.Match(s, "^(<I>([^<]+))?(<S>([^<]+))(<D>(.+))?", RegexOptions.IgnoreCase);
			if (!SmtpX509Identifier.TryParseFromRegexMatch(match, out subject, out commonName, out issuer, out invalidDomainError))
			{
				return false;
			}
			if (match.Success && !string.IsNullOrEmpty(match.Groups[6].Value))
			{
				string[] array = match.Groups[6].Value.Split(new string[]
				{
					";"
				}, StringSplitOptions.RemoveEmptyEntries);
				domains = new SmtpDomainWithSubdomains[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					if (!SmtpDomainWithSubdomains.TryParse(array[i], out domains[i]))
					{
						invalidDomainError = true;
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x04000896 RID: 2198
		private const string DomainsPrefix = "<D>";

		// Token: 0x04000897 RID: 2199
		private const string DomainsSeparator = ";";

		// Token: 0x04000898 RID: 2200
		private const string FormatRegularExpression = "^(<I>([^<]+))?(<S>([^<]+))(<D>(.+))?";
	}
}
