using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004BD RID: 1213
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("CreateFolderResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateFolderResponse : FolderInfoResponse
	{
		// Token: 0x060023F7 RID: 9207 RVA: 0x000A463D File Offset: 0x000A283D
		public CreateFolderResponse() : base(ResponseType.CreateFolderResponseMessage)
		{
		}
	}
}
