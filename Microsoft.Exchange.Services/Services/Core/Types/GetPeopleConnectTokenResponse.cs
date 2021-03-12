using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000508 RID: 1288
	[DataContract(Name = "GetPeopleConnectTokenResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "GetPeopleConnectTokenResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class GetPeopleConnectTokenResponse : BaseResponseMessage
	{
	}
}
