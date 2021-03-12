using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004BC RID: 1212
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("CreateFolderPathResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateFolderPathResponse : FolderInfoResponse
	{
		// Token: 0x060023F6 RID: 9206 RVA: 0x000A4634 File Offset: 0x000A2834
		public CreateFolderPathResponse() : base(ResponseType.CreateFolderPathResponseMessage)
		{
		}
	}
}
