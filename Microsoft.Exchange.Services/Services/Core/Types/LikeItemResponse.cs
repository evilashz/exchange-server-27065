using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200052D RID: 1325
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class LikeItemResponse : ResponseMessage
	{
		// Token: 0x060025E1 RID: 9697 RVA: 0x000A6099 File Offset: 0x000A4299
		internal LikeItemResponse(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x060025E2 RID: 9698 RVA: 0x000A60A3 File Offset: 0x000A42A3
		public LikeItemResponse()
		{
		}

		// Token: 0x060025E3 RID: 9699 RVA: 0x000A60AB File Offset: 0x000A42AB
		public override ResponseType GetResponseType()
		{
			return ResponseType.LikeItemResponseMessage;
		}
	}
}
