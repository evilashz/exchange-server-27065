using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000506 RID: 1286
	[DataContract(Name = "GetPeopleConnectStateResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetPeopleConnectStateResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetPeopleConnectStateResponse : BaseResponseMessage
	{
	}
}
