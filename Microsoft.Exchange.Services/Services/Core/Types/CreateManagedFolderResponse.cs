using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004BF RID: 1215
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("CreateManagedFolderResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class CreateManagedFolderResponse : FolderInfoResponse
	{
		// Token: 0x060023FA RID: 9210 RVA: 0x000A4663 File Offset: 0x000A2863
		public CreateManagedFolderResponse() : base(ResponseType.CreateManagedFolderResponseMessage)
		{
		}
	}
}
