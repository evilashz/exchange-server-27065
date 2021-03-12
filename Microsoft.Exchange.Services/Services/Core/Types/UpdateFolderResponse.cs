using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000573 RID: 1395
	[XmlType("UpdateFolderResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class UpdateFolderResponse : FolderInfoResponse
	{
		// Token: 0x060026EF RID: 9967 RVA: 0x000A6BF8 File Offset: 0x000A4DF8
		public UpdateFolderResponse() : base(ResponseType.UpdateFolderResponseMessage)
		{
		}
	}
}
