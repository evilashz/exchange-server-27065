using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002B4 RID: 692
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class PerformReminderActionResponseMessageType : ResponseMessageType
	{
		// Token: 0x040011EB RID: 4587
		[XmlArrayItem("ItemId", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public ItemIdType[] UpdatedItemIds;
	}
}
