using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000097 RID: 151
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetOrganizationRelationshipSettingsRequest : AutodiscoverRequest
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060008B2 RID: 2226 RVA: 0x0001F93A File Offset: 0x0001DB3A
		// (set) Token: 0x060008B3 RID: 2227 RVA: 0x0001F942 File Offset: 0x0001DB42
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

		// Token: 0x04000348 RID: 840
		private string[] domainsField;
	}
}
