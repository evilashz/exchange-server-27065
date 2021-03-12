using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020001F5 RID: 501
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[Serializable]
	public class ConversationNodeType
	{
		// Token: 0x04000CE6 RID: 3302
		public string InternetMessageId;

		// Token: 0x04000CE7 RID: 3303
		public string ParentInternetMessageId;

		// Token: 0x04000CE8 RID: 3304
		public NonEmptyArrayOfAllItemsType Items;
	}
}
