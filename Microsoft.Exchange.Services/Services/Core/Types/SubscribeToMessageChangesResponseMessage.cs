using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000561 RID: 1377
	public class SubscribeToMessageChangesResponseMessage : ResponseMessage
	{
		// Token: 0x06002687 RID: 9863 RVA: 0x000A673F File Offset: 0x000A493F
		public SubscribeToMessageChangesResponseMessage()
		{
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x000A6747 File Offset: 0x000A4947
		internal SubscribeToMessageChangesResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x000A6751 File Offset: 0x000A4951
		public override ResponseType GetResponseType()
		{
			return ResponseType.SubscribeToMessageChangesResponseMessage;
		}
	}
}
