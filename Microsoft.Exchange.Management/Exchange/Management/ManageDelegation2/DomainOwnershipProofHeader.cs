using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Management.ManageDelegation2
{
	// Token: 0x02000DC0 RID: 3520
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://domains.live.com/Service/ManageDelegation2/V1.0")]
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "http://domains.live.com/Service/ManageDelegation2/V1.0", IsNullable = false)]
	[Serializable]
	public class DomainOwnershipProofHeader : SoapHeader
	{
		// Token: 0x04004156 RID: 16726
		public string Domain;

		// Token: 0x04004157 RID: 16727
		public string HashAlgorithm;

		// Token: 0x04004158 RID: 16728
		public string Signature;

		// Token: 0x04004159 RID: 16729
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr;
	}
}
