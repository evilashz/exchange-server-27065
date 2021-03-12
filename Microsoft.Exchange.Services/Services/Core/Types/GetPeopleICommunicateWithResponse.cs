using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200050A RID: 1290
	[XmlType(TypeName = "GetPeopleICommunicateWithResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Name = "GetPeopleICommunicateWithResponse", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public sealed class GetPeopleICommunicateWithResponse : BaseResponseMessage
	{
		// Token: 0x06002530 RID: 9520 RVA: 0x000A57AB File Offset: 0x000A39AB
		public GetPeopleICommunicateWithResponse() : base(ResponseType.GetPeopleICommunicateWithResponseMessage)
		{
		}
	}
}
