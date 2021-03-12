using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Server
{
	// Token: 0x020009EC RID: 2540
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://microsoft.com/DRM/ServerService")]
	[DebuggerStepThrough]
	[Serializable]
	public class LicensorCertChain
	{
		// Token: 0x17000E1C RID: 3612
		// (get) Token: 0x06003771 RID: 14193 RVA: 0x0008CD2A File Offset: 0x0008AF2A
		// (set) Token: 0x06003772 RID: 14194 RVA: 0x0008CD32 File Offset: 0x0008AF32
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

		// Token: 0x04002F24 RID: 12068
		private XmlNode[] certificateChainField;
	}
}
