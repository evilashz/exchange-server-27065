using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000336 RID: 822
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class FindItemResponseMessageType : ResponseMessageType
	{
		// Token: 0x04001395 RID: 5013
		public FindItemParentType RootFolder;

		// Token: 0x04001396 RID: 5014
		[XmlArrayItem("Term", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public HighlightTermType[] HighlightTerms;
	}
}
