using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000434 RID: 1076
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType("GetEventsType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class GetEventsRequest : BasePullRequest
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x000A0E48 File Offset: 0x0009F048
		// (set) Token: 0x06001F93 RID: 8083 RVA: 0x000A0E50 File Offset: 0x0009F050
		[XmlElement]
		[DataMember(IsRequired = true)]
		public string Watermark { get; set; }

		// Token: 0x06001F94 RID: 8084 RVA: 0x000A0E59 File Offset: 0x0009F059
		internal override ServiceCommandBase GetServiceCommand(CallContext callContext)
		{
			return new GetEvents(callContext, this);
		}
	}
}
