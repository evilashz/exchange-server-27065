using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020006F8 RID: 1784
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IRecoveryEventSink
	{
		// Token: 0x060046BD RID: 18109
		bool RecoveryConsume(MapiEvent mapiEvent);

		// Token: 0x060046BE RID: 18110
		void EndRecovery();

		// Token: 0x17001492 RID: 5266
		// (get) Token: 0x060046BF RID: 18111
		EventWatermark FirstMissedEventWatermark { get; }

		// Token: 0x17001493 RID: 5267
		// (get) Token: 0x060046C0 RID: 18112
		long LastMissedEventWatermark { get; }
	}
}
