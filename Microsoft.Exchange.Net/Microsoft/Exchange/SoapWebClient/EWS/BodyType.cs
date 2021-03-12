using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000206 RID: 518
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class BodyType
	{
		// Token: 0x04000D73 RID: 3443
		[XmlAttribute("BodyType")]
		public BodyTypeType BodyType1;

		// Token: 0x04000D74 RID: 3444
		[XmlAttribute]
		public bool IsTruncated;

		// Token: 0x04000D75 RID: 3445
		[XmlIgnore]
		public bool IsTruncatedSpecified;

		// Token: 0x04000D76 RID: 3446
		[XmlText]
		public string Value;
	}
}
