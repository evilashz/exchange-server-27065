using System;
using Microsoft.Exchange.Data.Storage.Optics;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.WorkingSet.Publisher
{
	// Token: 0x02000EE8 RID: 3816
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IWorkingSetPublisherPerformanceTracker : IMailboxPerformanceTracker, IPerformanceTracker
	{
		// Token: 0x170022EA RID: 8938
		// (get) Token: 0x0600839C RID: 33692
		// (set) Token: 0x0600839D RID: 33693
		string OriginalMessageSender { get; set; }

		// Token: 0x170022EB RID: 8939
		// (get) Token: 0x0600839E RID: 33694
		// (set) Token: 0x0600839F RID: 33695
		string OriginalMessageSenderRecipientType { get; set; }

		// Token: 0x170022EC RID: 8940
		// (get) Token: 0x060083A0 RID: 33696
		// (set) Token: 0x060083A1 RID: 33697
		string OriginalMessageClass { get; set; }

		// Token: 0x170022ED RID: 8941
		// (get) Token: 0x060083A2 RID: 33698
		// (set) Token: 0x060083A3 RID: 33699
		string OriginalMessageId { get; set; }

		// Token: 0x170022EE RID: 8942
		// (get) Token: 0x060083A4 RID: 33700
		// (set) Token: 0x060083A5 RID: 33701
		string OriginalInternetMessageId { get; set; }

		// Token: 0x170022EF RID: 8943
		// (get) Token: 0x060083A6 RID: 33702
		// (set) Token: 0x060083A7 RID: 33703
		int ParticipantsInOriginalMessage { get; set; }

		// Token: 0x170022F0 RID: 8944
		// (get) Token: 0x060083A8 RID: 33704
		// (set) Token: 0x060083A9 RID: 33705
		string PublishedMessageId { get; set; }

		// Token: 0x170022F1 RID: 8945
		// (get) Token: 0x060083AA RID: 33706
		// (set) Token: 0x060083AB RID: 33707
		string PublishedIntnernetMessageId { get; set; }

		// Token: 0x170022F2 RID: 8946
		// (get) Token: 0x060083AC RID: 33708
		// (set) Token: 0x060083AD RID: 33709
		bool IsGroupParticipantAddedToParticipants { get; set; }

		// Token: 0x170022F3 RID: 8947
		// (get) Token: 0x060083AE RID: 33710
		// (set) Token: 0x060083AF RID: 33711
		long EnsureGroupParticipantAddedMilliseconds { get; set; }

		// Token: 0x170022F4 RID: 8948
		// (get) Token: 0x060083B0 RID: 33712
		// (set) Token: 0x060083B1 RID: 33713
		long DedupeParticipantsMilliseconds { get; set; }

		// Token: 0x060083B2 RID: 33714
		void IncrementParticipantsAddedToPublishedMessage();

		// Token: 0x060083B3 RID: 33715
		void IncrementParticipantsSkippedInPublishedMessage();

		// Token: 0x170022F5 RID: 8949
		// (get) Token: 0x060083B4 RID: 33716
		// (set) Token: 0x060083B5 RID: 33717
		bool HasWorkingSetUser { get; set; }

		// Token: 0x170022F6 RID: 8950
		// (get) Token: 0x060083B6 RID: 33718
		// (set) Token: 0x060083B7 RID: 33719
		string Exception { get; set; }
	}
}
