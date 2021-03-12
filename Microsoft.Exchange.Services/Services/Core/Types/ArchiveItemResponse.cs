using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004B1 RID: 1201
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("ArchiveItemResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class ArchiveItemResponse : ItemInfoResponse
	{
		// Token: 0x060023CF RID: 9167 RVA: 0x000A43A9 File Offset: 0x000A25A9
		public ArchiveItemResponse() : base(ResponseType.ArchiveItemResponseMessage)
		{
		}
	}
}
