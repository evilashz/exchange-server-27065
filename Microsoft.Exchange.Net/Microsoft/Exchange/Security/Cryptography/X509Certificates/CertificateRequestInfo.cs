using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Security.Cryptography.X509Certificates
{
	// Token: 0x02000A80 RID: 2688
	internal class CertificateRequestInfo
	{
		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06003A10 RID: 14864 RVA: 0x000938E5 File Offset: 0x00091AE5
		// (set) Token: 0x06003A11 RID: 14865 RVA: 0x000938F2 File Offset: 0x00091AF2
		public bool IsExportable
		{
			get
			{
				return (this.options & CertificateCreationOption.Exportable) == CertificateCreationOption.Exportable;
			}
			set
			{
				if (value)
				{
					this.options |= CertificateCreationOption.Exportable;
					return;
				}
				this.options &= ~CertificateCreationOption.Exportable;
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003A12 RID: 14866 RVA: 0x00093915 File Offset: 0x00091B15
		// (set) Token: 0x06003A13 RID: 14867 RVA: 0x0009391D File Offset: 0x00091B1D
		public X500DistinguishedName Subject
		{
			get
			{
				return this.subject;
			}
			set
			{
				this.subject = value;
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06003A14 RID: 14868 RVA: 0x00093926 File Offset: 0x00091B26
		// (set) Token: 0x06003A15 RID: 14869 RVA: 0x0009392E File Offset: 0x00091B2E
		public IEnumerable<string> AlternativeDomainNames
		{
			get
			{
				return this.subjectAlternativeNames;
			}
			set
			{
				this.subjectAlternativeNames = value;
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003A16 RID: 14870 RVA: 0x00093937 File Offset: 0x00091B37
		// (set) Token: 0x06003A17 RID: 14871 RVA: 0x0009394E File Offset: 0x00091B4E
		public int KeySize
		{
			get
			{
				if (this.SourceProvider != CertificateCreationOption.DSSProvider)
				{
					return this.keySize;
				}
				return 1024;
			}
			set
			{
				this.keySize = value;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003A18 RID: 14872 RVA: 0x00093957 File Offset: 0x00091B57
		// (set) Token: 0x06003A19 RID: 14873 RVA: 0x00093968 File Offset: 0x00091B68
		public string FriendlyName
		{
			get
			{
				return this.friendlyName ?? "Microsoft Exchange";
			}
			set
			{
				this.friendlyName = value;
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003A1A RID: 14874 RVA: 0x00093971 File Offset: 0x00091B71
		// (set) Token: 0x06003A1B RID: 14875 RVA: 0x00093981 File Offset: 0x00091B81
		public CertificateCreationOption SourceProvider
		{
			get
			{
				if ((this.options & CertificateCreationOption.DSSProvider) != CertificateCreationOption.DSSProvider)
				{
					return CertificateCreationOption.RSAProvider;
				}
				return CertificateCreationOption.DSSProvider;
			}
			set
			{
				this.options &= ~(CertificateCreationOption.RSAProvider | CertificateCreationOption.DSSProvider);
				this.options |= ((value == CertificateCreationOption.DSSProvider) ? CertificateCreationOption.DSSProvider : CertificateCreationOption.RSAProvider);
			}
		}

		// Token: 0x06003A1C RID: 14876 RVA: 0x000939A8 File Offset: 0x00091BA8
		public void ValidateDomainNamesAndSetSubject()
		{
			if (this.subjectAlternativeNames != null)
			{
				foreach (string text in this.subjectAlternativeNames)
				{
					if (!Dns.IsValidName(text) && (!Dns.IsValidWildcardName(text) || text.Length <= 2))
					{
						throw new ArgumentException(string.Format("Invalid FQDN '{0}' in list of alternate domain names", text), "AlternativeDomainNames");
					}
					if (this.subject == null && text.Length < 64 && text[0] != '*')
					{
						this.subject = new X500DistinguishedName("cn=" + text);
					}
				}
			}
			if (this.subject == null)
			{
				throw new ArgumentException("No valid subject was found");
			}
		}

		// Token: 0x06003A1D RID: 14877 RVA: 0x00093A70 File Offset: 0x00091C70
		public X509ExtensionCollection GetExtensions()
		{
			X509ExtensionCollection x509ExtensionCollection = new X509ExtensionCollection();
			if (this.SourceProvider == CertificateCreationOption.RSAProvider)
			{
				x509ExtensionCollection.Add(new X509KeyUsageExtension(X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature, true));
			}
			else
			{
				x509ExtensionCollection.Add(new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature, true));
			}
			if (this.subjectAlternativeNames != null)
			{
				x509ExtensionCollection.Add(new X509SubjectAltNameExtension(this.subjectAlternativeNames, false));
			}
			x509ExtensionCollection.Add(new X509BasicConstraintsExtension(false, false, 0, true));
			return x509ExtensionCollection;
		}

		// Token: 0x04003245 RID: 12869
		private const int DssKeySize = 1024;

		// Token: 0x04003246 RID: 12870
		private IEnumerable<string> subjectAlternativeNames;

		// Token: 0x04003247 RID: 12871
		private CertificateCreationOption options;

		// Token: 0x04003248 RID: 12872
		private int keySize = 2048;

		// Token: 0x04003249 RID: 12873
		private string friendlyName;

		// Token: 0x0400324A RID: 12874
		private X500DistinguishedName subject;
	}
}
