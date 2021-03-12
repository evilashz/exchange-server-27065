using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001AA RID: 426
	[Serializable]
	public class SmtpX509Identifier
	{
		// Token: 0x06000DE5 RID: 3557 RVA: 0x0002CF9F File Offset: 0x0002B19F
		public SmtpX509Identifier(string subject, SmtpDomainWithSubdomains subjectCN, string issuer)
		{
			ArgumentValidator.ThrowIfNull("subject", subject);
			ArgumentValidator.ThrowIfNull("subjectCN", subjectCN);
			this.CertificateSubject = subject;
			this.SubjectCommonName = subjectCN;
			this.CertificateIssuer = issuer;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x0002CFD4 File Offset: 0x0002B1D4
		public SmtpX509Identifier(string smtpX509Identifier)
		{
			string certificateSubject;
			SmtpDomainWithSubdomains subjectCommonName;
			string certificateIssuer;
			bool flag;
			if (!SmtpX509Identifier.InternalTryParse(smtpX509Identifier, out certificateSubject, out subjectCommonName, out certificateIssuer, out flag))
			{
				string s = string.IsNullOrEmpty(smtpX509Identifier) ? string.Empty : smtpX509Identifier;
				string message = flag ? DataStrings.InvalidDomainInSmtpX509Identifier(s) : DataStrings.InvalidSmtpX509Identifier(s);
				throw new StrongTypeFormatException(message, "SmtpX509Identifier");
			}
			this.CertificateIssuer = certificateIssuer;
			this.CertificateSubject = certificateSubject;
			this.SubjectCommonName = subjectCommonName;
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x0002D045 File Offset: 0x0002B245
		protected SmtpX509Identifier()
		{
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0002D04D File Offset: 0x0002B24D
		// (set) Token: 0x06000DE9 RID: 3561 RVA: 0x0002D055 File Offset: 0x0002B255
		public string CertificateSubject { get; protected set; }

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0002D05E File Offset: 0x0002B25E
		// (set) Token: 0x06000DEB RID: 3563 RVA: 0x0002D066 File Offset: 0x0002B266
		public string CertificateIssuer { get; protected set; }

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0002D06F File Offset: 0x0002B26F
		// (set) Token: 0x06000DED RID: 3565 RVA: 0x0002D077 File Offset: 0x0002B277
		public SmtpDomainWithSubdomains SubjectCommonName { get; protected set; }

		// Token: 0x06000DEE RID: 3566 RVA: 0x0002D080 File Offset: 0x0002B280
		public static SmtpX509Identifier Parse(string s)
		{
			return new SmtpX509Identifier(s);
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x0002D088 File Offset: 0x0002B288
		public static bool TryParse(string s, out SmtpX509Identifier result)
		{
			result = null;
			string subject;
			SmtpDomainWithSubdomains subjectCN;
			string issuer;
			bool flag;
			if (!SmtpX509Identifier.InternalTryParse(s, out subject, out subjectCN, out issuer, out flag))
			{
				return false;
			}
			result = new SmtpX509Identifier(subject, subjectCN, issuer);
			return true;
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x0002D0B5 File Offset: 0x0002B2B5
		public bool Equals(SmtpX509Identifier rhs)
		{
			return rhs != null && string.Equals(this.CertificateSubject, rhs.CertificateSubject, StringComparison.OrdinalIgnoreCase) && string.Equals(this.CertificateIssuer, rhs.CertificateIssuer, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06000DF1 RID: 3569 RVA: 0x0002D0E4 File Offset: 0x0002B2E4
		public override bool Equals(object rhs)
		{
			return this.Equals(rhs as SmtpX509Identifier);
		}

		// Token: 0x06000DF2 RID: 3570 RVA: 0x0002D0F4 File Offset: 0x0002B2F4
		public override string ToString()
		{
			string arg = string.Empty;
			if (!string.IsNullOrEmpty(this.CertificateIssuer))
			{
				arg = "<I>" + this.CertificateIssuer;
			}
			return string.Format("{0}{1}{2}", arg, "<S>", this.CertificateSubject);
		}

		// Token: 0x06000DF3 RID: 3571 RVA: 0x0002D13C File Offset: 0x0002B33C
		public override int GetHashCode()
		{
			string text = this.CertificateIssuer ?? string.Empty;
			return this.CertificateSubject.GetHashCode() ^ text.GetHashCode();
		}

		// Token: 0x06000DF4 RID: 3572 RVA: 0x0002D16C File Offset: 0x0002B36C
		private static bool InternalTryParse(string s, out string subject, out SmtpDomainWithSubdomains commonName, out string issuer, out bool invalidDomainError)
		{
			Match match = Regex.Match(s, "^(<I>([^<]+))?(<S>(.+))", RegexOptions.IgnoreCase);
			return SmtpX509Identifier.TryParseFromRegexMatch(match, out subject, out commonName, out issuer, out invalidDomainError);
		}

		// Token: 0x06000DF5 RID: 3573 RVA: 0x0002D194 File Offset: 0x0002B394
		protected static bool TryParseFromRegexMatch(Match match, out string subject, out SmtpDomainWithSubdomains commonName, out string issuer, out bool invalidDomainError)
		{
			subject = null;
			issuer = null;
			commonName = null;
			invalidDomainError = false;
			if (match.Success)
			{
				Match match2 = Regex.Match(match.Groups[4].Value, "CN=(.*?)($|,)", RegexOptions.IgnoreCase);
				if (!match2.Success)
				{
					return false;
				}
				if (!SmtpDomainWithSubdomains.TryParse(match2.Groups[1].Value, out commonName))
				{
					invalidDomainError = true;
					return false;
				}
				try
				{
					subject = new X500DistinguishedName(match.Groups[4].Value, X500DistinguishedNameFlags.None).Format(false);
					issuer = new X500DistinguishedName(match.Groups[2].Value, X500DistinguishedNameFlags.None).Format(false);
					return true;
				}
				catch (CryptographicException)
				{
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x0400088F RID: 2191
		public const string IssuerPrefix = "<I>";

		// Token: 0x04000890 RID: 2192
		public const string SubjectPrefix = "<S>";

		// Token: 0x04000891 RID: 2193
		private const string FormatRegularExpression = "^(<I>([^<]+))?(<S>(.+))";

		// Token: 0x04000892 RID: 2194
		private const string CommonNameRegularExpression = "CN=(.*?)($|,)";
	}
}
