using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x02000048 RID: 72
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ExRpcPerf
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x0000A46D File Offset: 0x0000866D
		static ExRpcPerf()
		{
			ExRpcModule.Bind();
			ExRpcPerf.pPerfData = ExRpcModule.pPerfData;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000A47E File Offset: 0x0000867E
		internal unsafe static void ConnectionCacheBirth(int maxConnections)
		{
			Interlocked.Increment(ref ((ExRpcPerfData*)((void*)ExRpcPerf.pPerfData))->ulNumConnectionCaches);
			ExRpcPerf.InterlockedAdd(ref ((ExRpcPerfData*)((void*)ExRpcPerf.pPerfData))->ulCacheTotalCapacity, maxConnections);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000A4AA File Offset: 0x000086AA
		internal unsafe static void ConnectionCacheGone(int maxConnections)
		{
			Interlocked.Decrement(ref ((ExRpcPerfData*)((void*)ExRpcPerf.pPerfData))->ulNumConnectionCaches);
			ExRpcPerf.InterlockedAdd(ref ((ExRpcPerfData*)((void*)ExRpcPerf.pPerfData))->ulCacheTotalCapacity, -maxConnections);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000A4D7 File Offset: 0x000086D7
		internal unsafe static void ConnectionCacheActiveAdd(int numConnections)
		{
			ExRpcPerf.InterlockedAdd(ref ((ExRpcPerfData*)((void*)ExRpcPerf.pPerfData))->ulCacheConnectionsActive, numConnections);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000A4EE File Offset: 0x000086EE
		internal unsafe static void ConnectionCacheIdleAdd(int numConnections)
		{
			ExRpcPerf.InterlockedAdd(ref ((ExRpcPerfData*)((void*)ExRpcPerf.pPerfData))->ulCacheConnectionsIdle, numConnections);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000A505 File Offset: 0x00008705
		internal unsafe static void ConnectionCacheNewOutOfLimitConnection()
		{
			Interlocked.Increment(ref ((ExRpcPerfData*)((void*)ExRpcPerf.pPerfData))->ulOutOfLimitCreations);
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000A51C File Offset: 0x0000871C
		internal unsafe static void ExRpcConnectionBirth()
		{
			Interlocked.Increment(ref ((ExRpcPerfData*)((void*)ExRpcPerf.pPerfData))->ulExRpcConnectionCreations);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000A533 File Offset: 0x00008733
		internal unsafe static void ExRpcConnectionGone()
		{
			Interlocked.Increment(ref ((ExRpcPerfData*)((void*)ExRpcPerf.pPerfData))->ulExRpcConnectionDisposals);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000A54C File Offset: 0x0000874C
		private static void InterlockedAdd(ref int totalValue, int valueToAdd)
		{
			int num;
			int value;
			do
			{
				num = totalValue;
				value = num + valueToAdd;
			}
			while (Interlocked.CompareExchange(ref totalValue, value, num) != num);
		}

		// Token: 0x04000441 RID: 1089
		internal static IntPtr pPerfData;
	}
}
