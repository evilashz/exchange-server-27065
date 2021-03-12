using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020004D4 RID: 1236
	[XmlType("ExpandDLResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class ExpandDLResponse : BaseInfoResponse
	{
		// Token: 0x06002430 RID: 9264 RVA: 0x000A489F File Offset: 0x000A2A9F
		public ExpandDLResponse() : base(ResponseType.ExpandDLResponseMessage)
		{
		}

		// Token: 0x06002431 RID: 9265 RVA: 0x000A48A9 File Offset: 0x000A2AA9
		internal override ResponseMessage CreateResponseMessage<TValue>(ServiceResultCode code, ServiceError error, TValue value)
		{
			return new ExpandDLResponseMessage(code, error, value as XmlNode);
		}
	}
}
