using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Security.RightsManagement.SOAP.Publish
{
	// Token: 0x020009D7 RID: 2519
	[XmlType(Namespace = "http://microsoft.com/DRM/PublishingService")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "2.0.50727.1432")]
	[DebuggerStepThrough]
	[Serializable]
	public class GetClientLicensorCertParams
	{
		// Token: 0x17000E02 RID: 3586
		// (get) Token: 0x06003702 RID: 14082 RVA: 0x0008C4CE File Offset: 0x0008A6CE
		// (set) Token: 0x06003703 RID: 14083 RVA: 0x0008C4D6 File Offset: 0x0008A6D6
		[XmlArrayItem("Certificate")]
		public XmlNode[] PersonaCerts
		{
			get
			{
				return this.personaCertsField;
			}
			set
			{
				this.personaCertsField = value;
			}
		}

		// Token: 0x04002EE7 RID: 12007
		private XmlNode[] personaCertsField;
	}
}
