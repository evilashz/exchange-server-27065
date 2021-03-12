using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001C1 RID: 449
	public interface IDestinationInfo
	{
		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000FF1 RID: 4081
		Uri Destination { get; }

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000FF2 RID: 4082
		IEnumerable<string> ChannelIds { get; }

		// Token: 0x06000FF3 RID: 4083
		void AddChannel(string channelId);
	}
}
