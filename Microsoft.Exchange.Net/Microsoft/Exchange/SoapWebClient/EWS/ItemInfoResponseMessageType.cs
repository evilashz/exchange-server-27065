using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000344 RID: 836
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DesignerCategory("code")]
	[XmlInclude(typeof(UpdateItemInRecoverableItemsResponseMessageType))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[XmlInclude(typeof(UpdateItemResponseMessageType))]
	[Serializable]
	public class ItemInfoResponseMessageType : ResponseMessageType
	{
		// Token: 0x040013F1 RID: 5105
		public ArrayOfRealItemsType Items;
	}
}
