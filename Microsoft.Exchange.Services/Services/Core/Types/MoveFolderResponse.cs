using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000533 RID: 1331
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("MoveFolderResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class MoveFolderResponse : FolderInfoResponse
	{
		// Token: 0x060025F4 RID: 9716 RVA: 0x000A6187 File Offset: 0x000A4387
		public MoveFolderResponse() : base(ResponseType.MoveFolderResponseMessage)
		{
		}
	}
}
