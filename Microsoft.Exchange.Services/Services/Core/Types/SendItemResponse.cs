using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200054D RID: 1357
	[XmlType("SendItemResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SendItemResponse : BaseResponseMessage
	{
		// Token: 0x06002647 RID: 9799 RVA: 0x000A64CA File Offset: 0x000A46CA
		public SendItemResponse() : base(ResponseType.SendItemResponseMessage)
		{
		}
	}
}
