using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Publish
{
	// Token: 0x020009D8 RID: 2520
	[XmlType(Namespace = "http://microsoft.com/DRM/PublishingService")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AcquireIssuanceLicenseResponse
	{
		// Token: 0x17000E03 RID: 3587
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x0008C4E7 File Offset: 0x0008A6E7
		// (set) Token: 0x06003706 RID: 14086 RVA: 0x0008C4EF File Offset: 0x0008A6EF
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

		// Token: 0x04002EE8 RID: 12008
		private XmlNode[] certificateChainField;
	}
}
