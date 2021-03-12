using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004BA RID: 1210
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("CopyItemResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CopyItemResponse : ItemInfoResponse
	{
		// Token: 0x060023F2 RID: 9202 RVA: 0x000A45EE File Offset: 0x000A27EE
		public CopyItemResponse() : base(ResponseType.CopyItemResponseMessage)
		{
		}
	}
}
