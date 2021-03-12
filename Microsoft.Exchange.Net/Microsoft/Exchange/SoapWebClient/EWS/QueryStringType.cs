using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020003C7 RID: 967
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[Serializable]
	public class QueryStringType
	{
		// Token: 0x04001550 RID: 5456
		[XmlAttribute]
		public bool ResetCache;

		// Token: 0x04001551 RID: 5457
		[XmlIgnore]
		public bool ResetCacheSpecified;

		// Token: 0x04001552 RID: 5458
		[XmlAttribute]
		public bool ReturnHighlightTerms;

		// Token: 0x04001553 RID: 5459
		[XmlIgnore]
		public bool ReturnHighlightTermsSpecified;

		// Token: 0x04001554 RID: 5460
		[XmlAttribute]
		public bool ReturnDeletedItems;

		// Token: 0x04001555 RID: 5461
		[XmlIgnore]
		public bool ReturnDeletedItemsSpecified;

		// Token: 0x04001556 RID: 5462
		[XmlText]
		public string Value;
	}
}
