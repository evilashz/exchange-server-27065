using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004FA RID: 1274
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetFolderResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetFolderResponse : FolderInfoResponse
	{
		// Token: 0x060024F3 RID: 9459 RVA: 0x000A5518 File Offset: 0x000A3718
		public GetFolderResponse() : base(ResponseType.GetFolderResponseMessage)
		{
		}
	}
}
