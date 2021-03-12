using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000523 RID: 1315
	[DataContract(Name = "GetUserPhotoResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetUserPhotoResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetUserPhotoResponse : BaseResponseMessage
	{
		// Token: 0x060025B6 RID: 9654 RVA: 0x000A5DEF File Offset: 0x000A3FEF
		public GetUserPhotoResponse() : base(ResponseType.GetUserPhotoResponseMessage)
		{
		}
	}
}
