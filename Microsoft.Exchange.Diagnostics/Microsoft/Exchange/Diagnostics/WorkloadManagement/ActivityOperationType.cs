using System;

namespace Microsoft.Exchange.Diagnostics.WorkloadManagement
{
	// Token: 0x020001E9 RID: 489
	internal enum ActivityOperationType
	{
		// Token: 0x04000A3C RID: 2620
		[DisplayName("ADR")]
		ADRead,
		// Token: 0x04000A3D RID: 2621
		[DisplayName("ADS")]
		ADSearch,
		// Token: 0x04000A3E RID: 2622
		[DisplayName("ADW")]
		ADWrite,
		// Token: 0x04000A3F RID: 2623
		[DisplayName("MB")]
		MailboxCall,
		// Token: 0x04000A40 RID: 2624
		[DisplayName("ST")]
		StoreCall,
		// Token: 0x04000A41 RID: 2625
		[DisplayName("STCPU")]
		StoreCpu,
		// Token: 0x04000A42 RID: 2626
		[DisplayName("RpcA")]
		ExRpcAdmin,
		// Token: 0x04000A43 RID: 2627
		[DisplayName("UserD")]
		UserDelay,
		// Token: 0x04000A44 RID: 2628
		[DisplayName("ResD")]
		ResourceDelay,
		// Token: 0x04000A45 RID: 2629
		[DisplayName("OBudg")]
		OverBudget,
		// Token: 0x04000A46 RID: 2630
		[DisplayName("QTime")]
		QueueTime,
		// Token: 0x04000A47 RID: 2631
		[DisplayName("ResB")]
		ResourceBlocked,
		// Token: 0x04000A48 RID: 2632
		[DisplayName("CCpu")]
		CustomCpu,
		// Token: 0x04000A49 RID: 2633
		[DisplayName("MBLB")]
		MailboxLogBytes,
		// Token: 0x04000A4A RID: 2634
		[DisplayName("MBMC")]
		MailboxMessagesCreated,
		// Token: 0x04000A4B RID: 2635
		[DisplayName("BudgUse")]
		BudgetUsed,
		// Token: 0x04000A4C RID: 2636
		[DisplayName("RPC")]
		RpcCount,
		// Token: 0x04000A4D RID: 2637
		[DisplayName("RPC")]
		RpcLatency,
		// Token: 0x04000A4E RID: 2638
		[DisplayName("ROP")]
		Rop,
		// Token: 0x04000A4F RID: 2639
		[DisplayName("MAPI")]
		MapiCount,
		// Token: 0x04000A50 RID: 2640
		[DisplayName("MAPI")]
		MapiLatency,
		// Token: 0x04000A51 RID: 2641
		[DisplayName("ATE")]
		ADObjToExchObjLatency,
		// Token: 0x04000A52 RID: 2642
		[DisplayName("CorrelationID")]
		CorrelationId
	}
}
