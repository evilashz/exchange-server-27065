using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000560 RID: 1376
	public class SubscribeToHierarchyChangesResponseMessage : ResponseMessage
	{
		// Token: 0x06002684 RID: 9860 RVA: 0x000A6726 File Offset: 0x000A4926
		public SubscribeToHierarchyChangesResponseMessage()
		{
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x000A672E File Offset: 0x000A492E
		internal SubscribeToHierarchyChangesResponseMessage(ServiceResultCode code, ServiceError error) : base(code, error)
		{
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x000A6738 File Offset: 0x000A4938
		public override ResponseType GetResponseType()
		{
			return ResponseType.SubscribeToHierarchyChangesResponseMessage;
		}
	}
}
