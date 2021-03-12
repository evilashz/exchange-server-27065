using System;

namespace Microsoft.Exchange.Data.Transport.Routing
{
	// Token: 0x02000083 RID: 131
	internal class RoutingEventBindings
	{
		// Token: 0x040001E7 RID: 487
		public const string EventOnSubmittedMessage = "OnSubmittedMessage";

		// Token: 0x040001E8 RID: 488
		public const string EventOnResolvedMessage = "OnResolvedMessage";

		// Token: 0x040001E9 RID: 489
		public const string EventOnRoutedMessage = "OnRoutedMessage";

		// Token: 0x040001EA RID: 490
		public const string EventOnCategorizedMessage = "OnCategorizedMessage";

		// Token: 0x040001EB RID: 491
		public static readonly string[] All = new string[]
		{
			"OnSubmittedMessage",
			"OnResolvedMessage",
			"OnRoutedMessage",
			"OnCategorizedMessage"
		};
	}
}
