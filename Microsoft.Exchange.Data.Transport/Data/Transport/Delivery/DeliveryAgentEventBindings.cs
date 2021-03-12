using System;

namespace Microsoft.Exchange.Data.Transport.Delivery
{
	// Token: 0x02000054 RID: 84
	internal class DeliveryAgentEventBindings
	{
		// Token: 0x04000175 RID: 373
		public const string OnOpenConnection = "OnOpenConnection";

		// Token: 0x04000176 RID: 374
		public const string OnDeliverMailItem = "OnDeliverMailItem";

		// Token: 0x04000177 RID: 375
		public const string OnCloseConnection = "OnCloseConnection";

		// Token: 0x04000178 RID: 376
		public static readonly string[] All = new string[]
		{
			"OnOpenConnection",
			"OnDeliverMailItem",
			"OnCloseConnection"
		};
	}
}
