using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200026E RID: 622
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal struct MDBEVENTWMRAW
	{
		// Token: 0x040010EB RID: 4331
		public Guid guidMailbox;

		// Token: 0x040010EC RID: 4332
		public Guid guidConsumer;

		// Token: 0x040010ED RID: 4333
		public ulong eventCounter;
	}
}
