using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200053B RID: 1339
	[XmlType("RemoveAggregatedAccountResponseType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	public class RemoveAggregatedAccountResponse : ResponseMessage
	{
		// Token: 0x0600261D RID: 9757 RVA: 0x000A632D File Offset: 0x000A452D
		public RemoveAggregatedAccountResponse()
		{
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x000A6335 File Offset: 0x000A4535
		internal RemoveAggregatedAccountResponse(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000A633F File Offset: 0x000A453F
		public override ResponseType GetResponseType()
		{
			return ResponseType.RemoveAggregatedAccountResponseMessage;
		}
	}
}
