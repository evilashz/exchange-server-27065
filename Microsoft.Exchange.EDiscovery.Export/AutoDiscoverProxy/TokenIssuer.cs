using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200007E RID: 126
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[Serializable]
	public class TokenIssuer
	{
		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0001F517 File Offset: 0x0001D717
		// (set) Token: 0x06000835 RID: 2101 RVA: 0x0001F51F File Offset: 0x0001D71F
		[XmlElement(DataType = "anyURI", IsNullable = true)]
		public string Uri
		{
			get
			{
				return this.uriField;
			}
			set
			{
				this.uriField = value;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x0001F528 File Offset: 0x0001D728
		// (set) Token: 0x06000837 RID: 2103 RVA: 0x0001F530 File Offset: 0x0001D730
		[XmlElement(DataType = "anyURI", IsNullable = true)]
		public string Endpoint
		{
			get
			{
				return this.endpointField;
			}
			set
			{
				this.endpointField = value;
			}
		}

		// Token: 0x04000309 RID: 777
		private string uriField;

		// Token: 0x0400030A RID: 778
		private string endpointField;
	}
}
