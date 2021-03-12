using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004D1 RID: 1233
	[XmlType("EmptyFolderResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class EmptyFolderResponse : BaseResponseMessage
	{
		// Token: 0x06002428 RID: 9256 RVA: 0x000A482D File Offset: 0x000A2A2D
		public EmptyFolderResponse() : base(ResponseType.EmptyFolderResponseMessage)
		{
		}
	}
}
