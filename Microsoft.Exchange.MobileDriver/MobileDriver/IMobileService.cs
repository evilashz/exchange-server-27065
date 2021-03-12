using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x02000018 RID: 24
	internal interface IMobileService
	{
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000083 RID: 131
		IMobileServiceManager Manager { get; }

		// Token: 0x06000084 RID: 132
		void Send(IList<TextSendingPackage> textPackages, Message message, MobileRecipient sender);
	}
}
