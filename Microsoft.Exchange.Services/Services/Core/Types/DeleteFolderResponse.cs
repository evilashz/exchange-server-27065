using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004C8 RID: 1224
	[XmlType("DeleteFolderResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class DeleteFolderResponse : BaseResponseMessage
	{
		// Token: 0x06002413 RID: 9235 RVA: 0x000A4756 File Offset: 0x000A2956
		public DeleteFolderResponse() : base(ResponseType.DeleteFolderResponseMessage)
		{
		}
	}
}
