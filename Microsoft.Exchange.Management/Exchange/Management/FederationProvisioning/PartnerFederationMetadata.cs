using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Xml;
using System.Xml.XPath;
using Microsoft.Exchange.Compliance.Xml;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.FederationProvisioning
{
	// Token: 0x02000338 RID: 824
	public abstract class PartnerFederationMetadata
	{
		// Token: 0x06001BE8 RID: 7144 RVA: 0x0007C3EE File Offset: 0x0007A5EE
		public PartnerFederationMetadata(WriteVerboseDelegate writeVerbose)
		{
			this.writeVerbose = writeVerbose;
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001BE9 RID: 7145 RVA: 0x0007C3FD File Offset: 0x0007A5FD
		// (set) Token: 0x06001BEA RID: 7146 RVA: 0x0007C405 File Offset: 0x0007A605
		public X509Certificate2 TokenIssuerCertificate
		{
			get
			{
				return this.tokenIssuerCertificate;
			}
			set
			{
				this.tokenIssuerCertificate = value;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001BEB RID: 7147 RVA: 0x0007C40E File Offset: 0x0007A60E
		// (set) Token: 0x06001BEC RID: 7148 RVA: 0x0007C416 File Offset: 0x0007A616
		public X509Certificate2 TokenIssuerPrevCertificate
		{
			get
			{
				return this.tokenIssuerPrevCertificate;
			}
			set
			{
				this.tokenIssuerPrevCertificate = value;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001BED RID: 7149 RVA: 0x0007C41F File Offset: 0x0007A61F
		// (set) Token: 0x06001BEE RID: 7150 RVA: 0x0007C427 File Offset: 0x0007A627
		public string PolicyReferenceUri
		{
			get
			{
				return this.policyReferenceUri;
			}
			set
			{
				this.policyReferenceUri = value;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001BEF RID: 7151 RVA: 0x0007C430 File Offset: 0x0007A630
		// (set) Token: 0x06001BF0 RID: 7152 RVA: 0x0007C438 File Offset: 0x0007A638
		public Uri TokenIssuerMetadataEpr
		{
			get
			{
				return this.tokenIssuerMetadataEpr;
			}
			set
			{
				this.tokenIssuerMetadataEpr = value;
			}
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06001BF1 RID: 7153 RVA: 0x0007C441 File Offset: 0x0007A641
		// (set) Token: 0x06001BF2 RID: 7154 RVA: 0x0007C449 File Offset: 0x0007A649
		public Uri TokenIssuerUri
		{
			get
			{
				return this.tokenIssuerUri;
			}
			set
			{
				this.tokenIssuerUri = value;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06001BF3 RID: 7155 RVA: 0x0007C452 File Offset: 0x0007A652
		// (set) Token: 0x06001BF4 RID: 7156 RVA: 0x0007C45A File Offset: 0x0007A65A
		public Uri TokenIssuerEpr
		{
			get
			{
				return this.tokenIssuerEpr;
			}
			set
			{
				this.tokenIssuerEpr = value;
			}
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06001BF5 RID: 7157 RVA: 0x0007C463 File Offset: 0x0007A663
		// (set) Token: 0x06001BF6 RID: 7158 RVA: 0x0007C46B File Offset: 0x0007A66B
		public Uri WebRequestorRedirectEpr
		{
			get
			{
				return this.webRequestorRedirectEpr;
			}
			set
			{
				this.webRequestorRedirectEpr = value;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06001BF7 RID: 7159 RVA: 0x0007C474 File Offset: 0x0007A674
		// (set) Token: 0x06001BF8 RID: 7160 RVA: 0x0007C47C File Offset: 0x0007A67C
		public string TokenIssuerCertReference
		{
			get
			{
				return this.tokenIssuerCertReference;
			}
			set
			{
				this.tokenIssuerCertReference = value;
			}
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06001BF9 RID: 7161 RVA: 0x0007C485 File Offset: 0x0007A685
		// (set) Token: 0x06001BFA RID: 7162 RVA: 0x0007C48D File Offset: 0x0007A68D
		public string TokenIssuerPrevCertReference
		{
			get
			{
				return this.tokenIssuerPrevCertReference;
			}
			set
			{
				this.tokenIssuerPrevCertReference = value;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06001BFB RID: 7163 RVA: 0x0007C496 File Offset: 0x0007A696
		protected WriteVerboseDelegate WriteVerbose
		{
			get
			{
				return this.writeVerbose;
			}
		}

		// Token: 0x06001BFC RID: 7164 RVA: 0x0007C49E File Offset: 0x0007A69E
		protected virtual void Parse(XPathDocument xmlFederationMetadata)
		{
		}

		// Token: 0x06001BFD RID: 7165 RVA: 0x0007C4A0 File Offset: 0x0007A6A0
		protected XPathDocument GetFederationMetadataXPathDocument(Uri partnerFederationMetadataEpr)
		{
			if (null == partnerFederationMetadataEpr)
			{
				throw new ArgumentNullException("PartnerFederationMetadataEpr");
			}
			this.WriteVerbose(Strings.RequestingFederationMetadataFromEndPoint(partnerFederationMetadataEpr.ToString()));
			Exception ex = null;
			string s = null;
			DateTime t = DateTime.UtcNow.Add(TimeSpan.FromMinutes(1.0));
			do
			{
				if (ex != null)
				{
					this.WriteVerbose(Strings.FailedToRetrieveFederationMetadata(ex.ToString()));
					Thread.Sleep(TimeSpan.FromSeconds(5.0));
					ex = null;
				}
				using (PartnerFederationMetadata.TimeOutWebClient timeOutWebClient = new PartnerFederationMetadata.TimeOutWebClient(59000))
				{
					timeOutWebClient.Credentials = CredentialCache.DefaultCredentials;
					WebProxy webProxy = LiveConfiguration.GetWebProxy(this.WriteVerbose);
					timeOutWebClient.Proxy = (webProxy ?? new WebProxy());
					timeOutWebClient.Headers.Add(HttpRequestHeader.UserAgent, "MicrosoftExchangeFedTrustManagement");
					try
					{
						s = timeOutWebClient.DownloadString(partnerFederationMetadataEpr);
					}
					catch (WebException ex2)
					{
						ex = ex2;
					}
					catch (IOException ex3)
					{
						ex = ex3;
					}
					catch (ProtocolViolationException ex4)
					{
						ex = ex4;
					}
				}
			}
			while (ex != null && DateTime.UtcNow < t);
			if (ex != null)
			{
				throw new FederationMetadataException(Strings.ErrorAccessingFederationMetadata(ex.Message));
			}
			XPathDocument result = null;
			try
			{
				StringReader textReader = new StringReader(s);
				result = SafeXmlFactory.CreateXPathDocument(textReader);
			}
			catch (XmlException ex5)
			{
				throw new FederationMetadataException(Strings.ErrorInvalidFederationMetadata(ex5.Message));
			}
			catch (XPathException ex6)
			{
				throw new FederationMetadataException(Strings.ErrorInvalidFederationMetadata(ex6.Message));
			}
			return result;
		}

		// Token: 0x04001810 RID: 6160
		public const string FederationSchemaNamespace = "http://schemas.xmlsoap.org/ws/2006/03/federation";

		// Token: 0x04001811 RID: 6161
		public const string WSSecurityExtSchemaNamespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd";

		// Token: 0x04001812 RID: 6162
		public const string DigitalSigSchemaNamespace = "http://www.w3.org/2000/09/xmldsig#";

		// Token: 0x04001813 RID: 6163
		public const string WebServiceUtilitySchemaNamespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd";

		// Token: 0x04001814 RID: 6164
		public const string WebServiceAddressingSchemaNamespace = "http://www.w3.org/2005/08/addressing";

		// Token: 0x04001815 RID: 6165
		public const string FederationSchemaNamespacePrefix = "fed";

		// Token: 0x04001816 RID: 6166
		public const string WSSecurityExtSchemaNamespacePrefix = "wsse";

		// Token: 0x04001817 RID: 6167
		public const string DigitalSigSchemaNamespacePrefix = "ds";

		// Token: 0x04001818 RID: 6168
		public const string WebServiceUtilitySchemaNamespacePrefix = "wsu";

		// Token: 0x04001819 RID: 6169
		public const string WebServiceAddressingSchemaNamespacePrefix = "wsa";

		// Token: 0x0400181A RID: 6170
		public const string FedTrustMetadataClientUserAgent = "MicrosoftExchangeFedTrustManagement";

		// Token: 0x0400181B RID: 6171
		private const int DefaultFederatedMetadataRequestTimeout = 59000;

		// Token: 0x0400181C RID: 6172
		private string tokenIssuerPrevCertReference;

		// Token: 0x0400181D RID: 6173
		private string tokenIssuerCertReference;

		// Token: 0x0400181E RID: 6174
		private Uri tokenIssuerMetadataEpr;

		// Token: 0x0400181F RID: 6175
		private string policyReferenceUri;

		// Token: 0x04001820 RID: 6176
		private X509Certificate2 tokenIssuerPrevCertificate;

		// Token: 0x04001821 RID: 6177
		private X509Certificate2 tokenIssuerCertificate;

		// Token: 0x04001822 RID: 6178
		private Uri tokenIssuerUri;

		// Token: 0x04001823 RID: 6179
		private Uri tokenIssuerEpr;

		// Token: 0x04001824 RID: 6180
		private Uri webRequestorRedirectEpr;

		// Token: 0x04001825 RID: 6181
		private WriteVerboseDelegate writeVerbose;

		// Token: 0x02000339 RID: 825
		private class TimeOutWebClient : WebClient
		{
			// Token: 0x06001BFE RID: 7166 RVA: 0x0007C64C File Offset: 0x0007A84C
			public TimeOutWebClient(int timeout)
			{
				this.Timeout = timeout;
			}

			// Token: 0x17000824 RID: 2084
			// (get) Token: 0x06001BFF RID: 7167 RVA: 0x0007C65B File Offset: 0x0007A85B
			// (set) Token: 0x06001C00 RID: 7168 RVA: 0x0007C663 File Offset: 0x0007A863
			public int Timeout { get; private set; }

			// Token: 0x06001C01 RID: 7169 RVA: 0x0007C66C File Offset: 0x0007A86C
			protected override WebRequest GetWebRequest(Uri uri)
			{
				WebRequest webRequest = base.GetWebRequest(uri);
				if (this.Timeout != 0)
				{
					webRequest.Timeout = this.Timeout;
				}
				return webRequest;
			}
		}
	}
}
