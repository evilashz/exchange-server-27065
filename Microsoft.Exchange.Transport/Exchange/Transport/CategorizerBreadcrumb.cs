using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020001A3 RID: 419
	internal enum CategorizerBreadcrumb
	{
		// Token: 0x0400098F RID: 2447
		Stage1,
		// Token: 0x04000990 RID: 2448
		Stage2Completed,
		// Token: 0x04000991 RID: 2449
		Stage2,
		// Token: 0x04000992 RID: 2450
		Stage3,
		// Token: 0x04000993 RID: 2451
		Stage4,
		// Token: 0x04000994 RID: 2452
		Stage4Completed,
		// Token: 0x04000995 RID: 2453
		Stage5,
		// Token: 0x04000996 RID: 2454
		Stage6,
		// Token: 0x04000997 RID: 2455
		Stage6NoConversion,
		// Token: 0x04000998 RID: 2456
		ConversionException,
		// Token: 0x04000999 RID: 2457
		ConversionExceptionAck,
		// Token: 0x0400099A RID: 2458
		ConversionCompleted,
		// Token: 0x0400099B RID: 2459
		Stage7,
		// Token: 0x0400099C RID: 2460
		Stage7Completed,
		// Token: 0x0400099D RID: 2461
		Stage8,
		// Token: 0x0400099E RID: 2462
		Stage9,
		// Token: 0x0400099F RID: 2463
		Stage9Completed,
		// Token: 0x040009A0 RID: 2464
		Defer,
		// Token: 0x040009A1 RID: 2465
		DeferAgentCompleted,
		// Token: 0x040009A2 RID: 2466
		AgentDefer,
		// Token: 0x040009A3 RID: 2467
		AgentDelete,
		// Token: 0x040009A4 RID: 2468
		AgentDeleteCompleted,
		// Token: 0x040009A5 RID: 2469
		AgentForkParent,
		// Token: 0x040009A6 RID: 2470
		AgentForkChild,
		// Token: 0x040009A7 RID: 2471
		AgentExpandNoFork,
		// Token: 0x040009A8 RID: 2472
		AgentExpand,
		// Token: 0x040009A9 RID: 2473
		DeferredByAgent,
		// Token: 0x040009AA RID: 2474
		UnhandledException,
		// Token: 0x040009AB RID: 2475
		ExceptionDeleted,
		// Token: 0x040009AC RID: 2476
		ExceptionDeferred,
		// Token: 0x040009AD RID: 2477
		ExceptionHandledTransient,
		// Token: 0x040009AE RID: 2478
		ExceptionHandledPermanent,
		// Token: 0x040009AF RID: 2479
		ExceptionHandledDeletedDeferred,
		// Token: 0x040009B0 RID: 2480
		ResubmitFail,
		// Token: 0x040009B1 RID: 2481
		DsnGenerated,
		// Token: 0x040009B2 RID: 2482
		BifurcateAndFail,
		// Token: 0x040009B3 RID: 2483
		Fail,
		// Token: 0x040009B4 RID: 2484
		Deleted,
		// Token: 0x040009B5 RID: 2485
		NotifyFinished,
		// Token: 0x040009B6 RID: 2486
		NotifyCM,
		// Token: 0x040009B7 RID: 2487
		Enqueue,
		// Token: 0x040009B8 RID: 2488
		EnqueueDelivery,
		// Token: 0x040009B9 RID: 2489
		ParentBifurcate,
		// Token: 0x040009BA RID: 2490
		ChildBifurcate,
		// Token: 0x040009BB RID: 2491
		MexResult,
		// Token: 0x040009BC RID: 2492
		MexResultException,
		// Token: 0x040009BD RID: 2493
		EventCallback,
		// Token: 0x040009BE RID: 2494
		EventExceptionUnhandled,
		// Token: 0x040009BF RID: 2495
		EventException,
		// Token: 0x040009C0 RID: 2496
		PostEventProcessing,
		// Token: 0x040009C1 RID: 2497
		PostEventDeleted,
		// Token: 0x040009C2 RID: 2498
		PostEventDeferred,
		// Token: 0x040009C3 RID: 2499
		PostEventNotDeferred,
		// Token: 0x040009C4 RID: 2500
		MessageDroppedOnDeferInAgent,
		// Token: 0x040009C5 RID: 2501
		MessageDroppedOnDeferInCategorizer,
		// Token: 0x040009C6 RID: 2502
		MessageNDRedOnDeferInCategorizer
	}
}
