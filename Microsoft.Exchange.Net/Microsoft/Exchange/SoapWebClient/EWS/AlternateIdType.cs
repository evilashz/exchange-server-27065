using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002DA RID: 730
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AlternateIdType : AlternateIdBaseType
	{
		// Token: 0x04001255 RID: 4693
		[XmlAttribute]
		public string Id;

		// Token: 0x04001256 RID: 4694
		[XmlAttribute]
		public string Mailbox;

		// Token: 0x04001257 RID: 4695
		[XmlAttribute]
		public bool IsArchive;

		// Token: 0x04001258 RID: 4696
		[XmlIgnore]
		public bool IsArchiveSpecified;
	}
}
