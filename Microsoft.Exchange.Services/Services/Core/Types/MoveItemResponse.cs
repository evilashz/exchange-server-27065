using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000534 RID: 1332
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("MoveItemResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class MoveItemResponse : ItemInfoResponse
	{
		// Token: 0x060025F5 RID: 9717 RVA: 0x000A6191 File Offset: 0x000A4391
		public MoveItemResponse() : base(ResponseType.MoveItemResponseMessage)
		{
		}
	}
}
