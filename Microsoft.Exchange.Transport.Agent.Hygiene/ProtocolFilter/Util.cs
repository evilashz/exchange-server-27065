using System;

namespace Microsoft.Exchange.Transport.Agent.ProtocolFilter
{
	// Token: 0x02000027 RID: 39
	internal static class Util
	{
		// Token: 0x02000028 RID: 40
		public static class PerformanceCounters
		{
			// Token: 0x02000029 RID: 41
			public static class RecipientFilter
			{
				// Token: 0x060000F4 RID: 244 RVA: 0x00008CE1 File Offset: 0x00006EE1
				public static void RecipientRejectedByRecipientValidation()
				{
					RecipientFilterPerfCounters.TotalRecipientsRejectedByRecipientValidation.Increment();
				}

				// Token: 0x060000F5 RID: 245 RVA: 0x00008CEE File Offset: 0x00006EEE
				public static void RecipientRejectedByBlockList()
				{
					RecipientFilterPerfCounters.TotalRecipientsRejectedByBlockList.Increment();
				}

				// Token: 0x060000F6 RID: 246 RVA: 0x00008CFB File Offset: 0x00006EFB
				public static void RemoveCounters()
				{
					RecipientFilterPerfCounters.TotalRecipientsRejectedByRecipientValidation.RawValue = 0L;
					RecipientFilterPerfCounters.TotalRecipientsRejectedByBlockList.RawValue = 0L;
				}
			}

			// Token: 0x0200002A RID: 42
			public static class SenderFilter
			{
				// Token: 0x060000F7 RID: 247 RVA: 0x00008D15 File Offset: 0x00006F15
				public static void MessageEvaluatedBySenderFilter()
				{
					SenderFilterPerfCounters.TotalMessagesEvaluatedBySenderFilter.Increment();
				}

				// Token: 0x060000F8 RID: 248 RVA: 0x00008D22 File Offset: 0x00006F22
				public static void MessageFilteredBySenderFilter()
				{
					SenderFilterPerfCounters.TotalMessagesFilteredBySenderFilter.Increment();
				}

				// Token: 0x060000F9 RID: 249 RVA: 0x00008D2F File Offset: 0x00006F2F
				public static void SenderBlockedDueToPerRecipientBlockedSender()
				{
					SenderFilterPerfCounters.TotalPerRecipientSenderBlocks.Increment();
				}

				// Token: 0x060000FA RID: 250 RVA: 0x00008D3C File Offset: 0x00006F3C
				public static void RemoveCounters()
				{
					SenderFilterPerfCounters.TotalMessagesEvaluatedBySenderFilter.RawValue = 0L;
					SenderFilterPerfCounters.TotalMessagesFilteredBySenderFilter.RawValue = 0L;
					SenderFilterPerfCounters.TotalPerRecipientSenderBlocks.RawValue = 0L;
				}
			}
		}
	}
}
