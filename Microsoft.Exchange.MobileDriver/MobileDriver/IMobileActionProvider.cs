using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000033 RID: 51
	internal interface IMobileActionProvider
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000EE RID: 238
		IList<IMobileServiceManager> ServiceManagers { get; }

		// Token: 0x060000EF RID: 239
		void Send(Message message, MobileRecipient sender, ICollection<MobileRecipient> recipients, int maxSegmentsPerRecipient);
	}
}
