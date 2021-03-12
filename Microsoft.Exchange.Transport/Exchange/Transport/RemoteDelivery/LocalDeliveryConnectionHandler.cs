using System;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x020003AF RID: 943
	internal class LocalDeliveryConnectionHandler
	{
		// Token: 0x06002A46 RID: 10822 RVA: 0x000A8030 File Offset: 0x000A6230
		public static void HandleConnection(NextHopConnection connection)
		{
			IStoreDriver storeDriver;
			if (!Components.TryGetStoreDriver(out storeDriver))
			{
				ExTraceGlobals.QueuingTracer.TraceError(0L, "No store driver found");
				return;
			}
			ExTraceGlobals.QueuingTracer.TraceDebug(0L, "Invoking the store driver");
			Components.StoreDriver.DoLocalDelivery(connection);
		}
	}
}
