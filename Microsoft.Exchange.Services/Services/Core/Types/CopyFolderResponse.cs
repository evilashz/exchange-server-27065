using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004B9 RID: 1209
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("CopyFolderResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CopyFolderResponse : FolderInfoResponse
	{
		// Token: 0x060023F1 RID: 9201 RVA: 0x000A45E5 File Offset: 0x000A27E5
		public CopyFolderResponse() : base(ResponseType.CopyFolderResponseMessage)
		{
		}
	}
}
