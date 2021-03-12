using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000570 RID: 1392
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("UnsubscribeResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class UnsubscribeResponse : BaseResponseMessage
	{
		// Token: 0x060026E9 RID: 9961 RVA: 0x000A6BC0 File Offset: 0x000A4DC0
		public UnsubscribeResponse() : base(ResponseType.UnsubscribeResponseMessage)
		{
		}
	}
}
