using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001BC RID: 444
	[Serializable]
	public class TlsCertificate
	{
		// Token: 0x06000F8E RID: 3982 RVA: 0x0002F5E8 File Offset: 0x0002D7E8
		public TlsCertificate(string certificateName)
		{
			SmtpDomainWithSubdomains smtpDomainWithSubdomains = null;
			SmtpX509Identifier smtpX509Identifier = null;
			if (!TlsCertificate.InternalTryParse(certificateName, out smtpDomainWithSubdomains, out smtpX509Identifier))
			{
				string s = string.IsNullOrEmpty(certificateName) ? string.Empty : certificateName;
				throw new StrongTypeFormatException(DataStrings.InvalidTlsCertificateName(s), "TlsCertificateName");
			}
			this.tlsCertificateName = (smtpDomainWithSubdomains ?? smtpX509Identifier);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0002F63E File Offset: 0x0002D83E
		public TlsCertificate(SmtpDomainWithSubdomains fqdn, SmtpX509Identifier x509Identifier)
		{
			if ((fqdn == null && x509Identifier == null) || (fqdn != null && x509Identifier != null))
			{
				throw new ArgumentException("FQDN and X509Identifier both cannot be null or both have values");
			}
			this.tlsCertificateName = (fqdn ?? x509Identifier);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x0002F669 File Offset: 0x0002D869
		public static TlsCertificate Parse(string certificateName)
		{
			return new TlsCertificate(certificateName);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0002F674 File Offset: 0x0002D874
		public static bool TryParse(string certificateName, out TlsCertificate tlsCertificate)
		{
			tlsCertificate = null;
			SmtpDomainWithSubdomains fqdn = null;
			SmtpX509Identifier x509Identifier = null;
			if (!TlsCertificate.InternalTryParse(certificateName, out fqdn, out x509Identifier))
			{
				return false;
			}
			tlsCertificate = new TlsCertificate(fqdn, x509Identifier);
			return true;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x0002F6A0 File Offset: 0x0002D8A0
		public override string ToString()
		{
			return this.tlsCertificateName.ToString();
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x0002F6AD File Offset: 0x0002D8AD
		public override int GetHashCode()
		{
			return this.tlsCertificateName.GetHashCode();
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x0002F6BA File Offset: 0x0002D8BA
		public override bool Equals(object obj)
		{
			return this.Equals(obj as TlsCertificate);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x0002F6C8 File Offset: 0x0002D8C8
		public bool Equals(TlsCertificate rhs)
		{
			return rhs != null && this.tlsCertificateName.Equals(rhs.tlsCertificateName);
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000F96 RID: 3990 RVA: 0x0002F6E0 File Offset: 0x0002D8E0
		// (set) Token: 0x06000F97 RID: 3991 RVA: 0x0002F6E8 File Offset: 0x0002D8E8
		public object TlsCertificateName
		{
			get
			{
				return this.tlsCertificateName;
			}
			private set
			{
				this.tlsCertificateName = value;
			}
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x0002F6F1 File Offset: 0x0002D8F1
		private static bool InternalTryParse(string certificateName, out SmtpDomainWithSubdomains fqdn, out SmtpX509Identifier x509Identifier)
		{
			fqdn = null;
			x509Identifier = null;
			return SmtpDomainWithSubdomains.TryParse(certificateName, out fqdn) || SmtpX509Identifier.TryParse(certificateName, out x509Identifier);
		}

		// Token: 0x04000943 RID: 2371
		private object tlsCertificateName;
	}
}
