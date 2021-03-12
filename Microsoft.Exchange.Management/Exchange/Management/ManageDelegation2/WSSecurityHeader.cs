using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.ManageDelegation2
{
	// Token: 0x02000DBB RID: 3515
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlRoot("Security", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd", IsNullable = false)]
	[XmlType(Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-secext-1.0.xsd")]
	[Serializable]
	public class WSSecurityHeader : SoapHeader
	{
		// Token: 0x04004149 RID: 16713
		[XmlAnyElement(Name = "Timestamp", Namespace = "http://docs.oasis-open.org/wss/2004/01/oasis-200401-wss-wssecurity-utility-1.0.xsd")]
		public XmlElement Timestamp;

		// Token: 0x0400414A RID: 16714
		[XmlAnyElement(Name = "Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
		public XmlElement Signature;

		// Token: 0x0400414B RID: 16715
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr;
	}
}
