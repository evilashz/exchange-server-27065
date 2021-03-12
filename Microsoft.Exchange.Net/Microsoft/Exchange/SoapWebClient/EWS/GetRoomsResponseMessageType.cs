using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x020002B8 RID: 696
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetRoomsResponseMessageType : ResponseMessageType
	{
		// Token: 0x040011FA RID: 4602
		[XmlArrayItem("Room", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
		public RoomType[] Rooms;
	}
}
