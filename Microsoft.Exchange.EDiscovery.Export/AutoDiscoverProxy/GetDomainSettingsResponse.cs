using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000092 RID: 146
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetDomainSettingsResponse : AutodiscoverResponse
	{
		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x0001F8AC File Offset: 0x0001DAAC
		// (set) Token: 0x060008A2 RID: 2210 RVA: 0x0001F8B4 File Offset: 0x0001DAB4
		[XmlArray(IsNullable = true)]
		public DomainResponse[] DomainResponses
		{
			get
			{
				return this.domainResponsesField;
			}
			set
			{
				this.domainResponsesField = value;
			}
		}

		// Token: 0x04000342 RID: 834
		private DomainResponse[] domainResponsesField;
	}
}
