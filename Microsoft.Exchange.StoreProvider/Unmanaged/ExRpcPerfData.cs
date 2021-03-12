using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x020002AC RID: 684
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal struct ExRpcPerfData
	{
		// Token: 0x0400118C RID: 4492
		internal int ulTotalLength;

		// Token: 0x0400118D RID: 4493
		internal int ulPid;

		// Token: 0x0400118E RID: 4494
		internal long i64ProcessStartTime;

		// Token: 0x0400118F RID: 4495
		internal Guid gPn10;

		// Token: 0x04001190 RID: 4496
		internal Guid gPn11;

		// Token: 0x04001191 RID: 4497
		internal Guid gPn12;

		// Token: 0x04001192 RID: 4498
		internal Guid gPn13;

		// Token: 0x04001193 RID: 4499
		internal Guid gPn14;

		// Token: 0x04001194 RID: 4500
		internal Guid gPn15;

		// Token: 0x04001195 RID: 4501
		internal Guid gPn16;

		// Token: 0x04001196 RID: 4502
		internal Guid gPn17;

		// Token: 0x04001197 RID: 4503
		internal Guid gPn20;

		// Token: 0x04001198 RID: 4504
		internal Guid gPn21;

		// Token: 0x04001199 RID: 4505
		internal Guid gPn22;

		// Token: 0x0400119A RID: 4506
		internal Guid gPn23;

		// Token: 0x0400119B RID: 4507
		internal Guid gPn24;

		// Token: 0x0400119C RID: 4508
		internal Guid gPn25;

		// Token: 0x0400119D RID: 4509
		internal Guid gPn26;

		// Token: 0x0400119E RID: 4510
		internal Guid gPn27;

		// Token: 0x0400119F RID: 4511
		internal int ulHdrEnd;

		// Token: 0x040011A0 RID: 4512
		internal int ulNumConnectionCaches;

		// Token: 0x040011A1 RID: 4513
		internal int ulCacheTotalCapacity;

		// Token: 0x040011A2 RID: 4514
		internal int ulCacheConnectionsActive;

		// Token: 0x040011A3 RID: 4515
		internal int ulCacheConnectionsIdle;

		// Token: 0x040011A4 RID: 4516
		internal int ulOutOfLimitCreations;

		// Token: 0x040011A5 RID: 4517
		internal int ulExRpcConnectionCreations;

		// Token: 0x040011A6 RID: 4518
		internal int ulExRpcConnectionDisposals;

		// Token: 0x040011A7 RID: 4519
		internal int ulExRpcConnectionOutstanding;

		// Token: 0x040011A8 RID: 4520
		internal int ulUnkObjectsTotal;

		// Token: 0x040011A9 RID: 4521
		internal int ulLogonObjects;

		// Token: 0x040011AA RID: 4522
		internal int ulFolderObjects;

		// Token: 0x040011AB RID: 4523
		internal int ulMessageObjects;

		// Token: 0x040011AC RID: 4524
		internal int ulRpcReqsSent;

		// Token: 0x040011AD RID: 4525
		internal int ulRpcReqsSucceeded;

		// Token: 0x040011AE RID: 4526
		internal int ulRpcReqsFailed;

		// Token: 0x040011AF RID: 4527
		internal int ulRpcReqsFailedWithException;

		// Token: 0x040011B0 RID: 4528
		internal int ulRpcReqsFailedTotal;

		// Token: 0x040011B1 RID: 4529
		internal int ulRpcReqsOutstanding;

		// Token: 0x040011B2 RID: 4530
		internal long i64RpcLatencyTotal;

		// Token: 0x040011B3 RID: 4531
		internal long i64RpcLatencyAverage;

		// Token: 0x040011B4 RID: 4532
		internal int ulRpcSlowReqsTotal;

		// Token: 0x040011B5 RID: 4533
		internal long i64RpcSlowReqsLatencyTotal;

		// Token: 0x040011B6 RID: 4534
		internal long i64RpcSlowReqsLatencyAverage;

		// Token: 0x040011B7 RID: 4535
		internal int ulRpcReqsBytesSent;

		// Token: 0x040011B8 RID: 4536
		internal int ulRpcReqsBytesSentAverage;

		// Token: 0x040011B9 RID: 4537
		internal int ulRpcReqsBytesReceived;

		// Token: 0x040011BA RID: 4538
		internal int ulRpcReqsBytesReceivedAverage;

		// Token: 0x040011BB RID: 4539
		internal int ulRopReqsSent;

		// Token: 0x040011BC RID: 4540
		internal int ulRopReqsComplete;

		// Token: 0x040011BD RID: 4541
		internal int ulRopReqsOutstanding;

		// Token: 0x040011BE RID: 4542
		internal int ulRpcPoolPools;

		// Token: 0x040011BF RID: 4543
		internal int ulRpcPoolContextHandles;

		// Token: 0x040011C0 RID: 4544
		internal int ulRpcPoolSessions;

		// Token: 0x040011C1 RID: 4545
		internal int ulRpcPoolThreadsActive;

		// Token: 0x040011C2 RID: 4546
		internal int ulRpcPoolThreadsTotal;

		// Token: 0x040011C3 RID: 4547
		internal long ulRpcPoolLatencyAverage;

		// Token: 0x040011C4 RID: 4548
		internal int ulRpcPoolSessionNotifReceived;

		// Token: 0x040011C5 RID: 4549
		internal int ulRpcPoolAsyncNotifReceived;

		// Token: 0x040011C6 RID: 4550
		internal int ulRpcPoolAsyncNotifParked;

		// Token: 0x040011C7 RID: 4551
		internal int ulAverageBase;

		// Token: 0x040011C8 RID: 4552
		internal long ulLargeAverageBase;
	}
}
