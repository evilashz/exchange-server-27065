using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200055C RID: 1372
	[XmlType("SubscribeResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SubscribeResponse : BaseResponseMessage
	{
		// Token: 0x06002678 RID: 9848 RVA: 0x000A669A File Offset: 0x000A489A
		public SubscribeResponse() : base(ResponseType.SubscribeResponseMessage)
		{
		}
	}
}
