using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000098 RID: 152
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class GetFederationInformationRequest : AutodiscoverRequest
	{
		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060008B5 RID: 2229 RVA: 0x0001F953 File Offset: 0x0001DB53
		// (set) Token: 0x060008B6 RID: 2230 RVA: 0x0001F95B File Offset: 0x0001DB5B
		[XmlElement(IsNullable = true)]
		public string Domain
		{
			get
			{
				return this.domainField;
			}
			set
			{
				this.domainField = value;
			}
		}

		// Token: 0x04000349 RID: 841
		private string domainField;
	}
}
