using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000553 RID: 1363
	[XmlType("SetAggregatedAccountResponseMessageType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class SetAggregatedAccountResponseMessage : ResponseMessage
	{
		// Token: 0x0600265B RID: 9819 RVA: 0x000A6598 File Offset: 0x000A4798
		public SetAggregatedAccountResponseMessage()
		{
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000A65A0 File Offset: 0x000A47A0
		internal SetAggregatedAccountResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}
	}
}
