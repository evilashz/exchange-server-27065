using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002BB RID: 699
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetRoomListsResponseMessageType : ResponseMessageType
	{
		// Token: 0x040011FC RID: 4604
		[XmlArrayItem("Address", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public EmailAddressType[] RoomLists;
	}
}
