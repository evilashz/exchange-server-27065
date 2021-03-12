using System;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Search.Mdb
{
	// Token: 0x02000017 RID: 23
	internal interface INotificationsEventSource : IDisposable
	{
		// Token: 0x0600006E RID: 110
		MapiEvent[] ReadEvents(long startCounter, int eventCountWanted, ReadEventsFlags flags, out long endCounter);

		// Token: 0x0600006F RID: 111
		MapiEvent ReadLastEvent();

		// Token: 0x06000070 RID: 112
		long ReadFirstEventCounter();

		// Token: 0x06000071 RID: 113
		long GetNetworkLatency(int samples);
	}
}
