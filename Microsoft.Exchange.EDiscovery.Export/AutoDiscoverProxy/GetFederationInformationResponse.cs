using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000090 RID: 144
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public class GetFederationInformationResponse : AutodiscoverResponse
	{
		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x0001F836 File Offset: 0x0001DA36
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x0001F83E File Offset: 0x0001DA3E
		[XmlElement(DataType = "anyURI", IsNullable = true)]
		public string ApplicationUri
		{
			get
			{
				return this.applicationUriField;
			}
			set
			{
				this.applicationUriField = value;
			}
		}

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0001F847 File Offset: 0x0001DA47
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x0001F84F File Offset: 0x0001DA4F
		[XmlArray(IsNullable = true)]
		public TokenIssuer[] TokenIssuers
		{
			get
			{
				return this.tokenIssuersField;
			}
			set
			{
				this.tokenIssuersField = value;
			}
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x0001F858 File Offset: 0x0001DA58
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x0001F860 File Offset: 0x0001DA60
		[XmlArray(IsNullable = true)]
		[XmlArrayItem("Domain")]
		public string[] Domains
		{
			get
			{
				return this.domainsField;
			}
			set
			{
				this.domainsField = value;
			}
		}

		// Token: 0x0400033C RID: 828
		private string applicationUriField;

		// Token: 0x0400033D RID: 829
		private TokenIssuer[] tokenIssuersField;

		// Token: 0x0400033E RID: 830
		private string[] domainsField;
	}
}
