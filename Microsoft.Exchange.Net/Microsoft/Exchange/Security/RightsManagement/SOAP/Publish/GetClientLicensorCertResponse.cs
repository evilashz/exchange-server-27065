using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Publish
{
	// Token: 0x020009D6 RID: 2518
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://microsoft.com/DRM/PublishingService")]
	[Serializable]
	public class GetClientLicensorCertResponse
	{
		// Token: 0x17000E01 RID: 3585
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x0008C4B5 File Offset: 0x0008A6B5
		// (set) Token: 0x06003700 RID: 14080 RVA: 0x0008C4BD File Offset: 0x0008A6BD
		[XmlArrayItem("Certificate")]
		public XmlNode[] CertificateChain
		{
			get
			{
				return this.certificateChainField;
			}
			set
			{
				this.certificateChainField = value;
			}
		}

		// Token: 0x04002EE6 RID: 12006
		private XmlNode[] certificateChainField;
	}
}
