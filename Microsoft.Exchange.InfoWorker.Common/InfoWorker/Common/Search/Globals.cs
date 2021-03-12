using System;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000220 RID: 544
	internal static class Globals
	{
		// Token: 0x04000A18 RID: 2584
		public const string LogItemDelimiter = ", ";

		// Token: 0x04000A19 RID: 2585
		public const string LogLineDelimiter = ";";

		// Token: 0x04000A1A RID: 2586
		public const string LogNameValueSeperator = ":";

		// Token: 0x04000A1B RID: 2587
		public const string LogMessageClass = "IPM.Note.Microsoft.Exchange.Search.Log";

		// Token: 0x04000A1C RID: 2588
		public const string LogAttachmentExtension = ".csv";

		// Token: 0x04000A1D RID: 2589
		public const string LogDefaultSubject = "Search Results";

		// Token: 0x04000A1E RID: 2590
		public const string LogMailTemplate = "LogMailTemplate.htm";

		// Token: 0x04000A1F RID: 2591
		public const string SimpleLogMailTemplate = "SimpleLogMailTemplate.htm";

		// Token: 0x04000A20 RID: 2592
		public const string StatusMailTemplateName = "StatusMailTemplate.htm";

		// Token: 0x04000A21 RID: 2593
		public const string RecycleFolderPrefix = "MailboxSearchRecycleFolder";

		// Token: 0x04000A22 RID: 2594
		public const string ClientInfoString = "Client=EDiscoverySearch;Action=Search;Interactive=False";

		// Token: 0x02000221 RID: 545
		public enum LogFields
		{
			// Token: 0x04000A24 RID: 2596
			LogMailHeader,
			// Token: 0x04000A25 RID: 2597
			LogMailHeaderInstructions,
			// Token: 0x04000A26 RID: 2598
			LogMailSeeAttachment,
			// Token: 0x04000A27 RID: 2599
			LogMailFooter,
			// Token: 0x04000A28 RID: 2600
			[LocDescription(Strings.IDs.LogFieldsLastStartTime)]
			LastStartTime,
			// Token: 0x04000A29 RID: 2601
			[LocDescription(Strings.IDs.LogFieldsLastEndTime)]
			LastEndTime,
			// Token: 0x04000A2A RID: 2602
			[LocDescription(Strings.IDs.LogFieldsCreatedBy)]
			CreatedBy,
			// Token: 0x04000A2B RID: 2603
			[LocDescription(Strings.IDs.LogFieldsName)]
			Name,
			// Token: 0x04000A2C RID: 2604
			[LocDescription(Strings.IDs.LogFieldsSearchQuery)]
			SearchQuery,
			// Token: 0x04000A2D RID: 2605
			[LocDescription(Strings.IDs.LogFieldsSenders)]
			Senders,
			// Token: 0x04000A2E RID: 2606
			[LocDescription(Strings.IDs.LogFieldsRecipients)]
			Recipients,
			// Token: 0x04000A2F RID: 2607
			[LocDescription(Strings.IDs.LogFieldsStartDate)]
			StartDate,
			// Token: 0x04000A30 RID: 2608
			[LocDescription(Strings.IDs.LogFieldsEndDate)]
			EndDate,
			// Token: 0x04000A31 RID: 2609
			[LocDescription(Strings.IDs.LogFieldsMessageTypes)]
			MessageTypes,
			// Token: 0x04000A32 RID: 2610
			[LocDescription(Strings.IDs.LogFieldsSourceRecipients)]
			SourceRecipients,
			// Token: 0x04000A33 RID: 2611
			[LocDescription(Strings.IDs.LogFieldsTargetMailbox)]
			TargetMailbox,
			// Token: 0x04000A34 RID: 2612
			[LocDescription(Strings.IDs.LogFieldsNumberSuccessfulMailboxes)]
			NumberSuccessfulMailboxes,
			// Token: 0x04000A35 RID: 2613
			[LocDescription(Strings.IDs.LogFieldsSuccessfulMailboxes)]
			SuccessfulMailboxes,
			// Token: 0x04000A36 RID: 2614
			[LocDescription(Strings.IDs.LogFieldsNumberUnsuccessfulMailboxes)]
			NumberUnsuccessfulMailboxes,
			// Token: 0x04000A37 RID: 2615
			[LocDescription(Strings.IDs.LogFieldsUnsuccessfulMailboxes)]
			UnsuccessfulMailboxes,
			// Token: 0x04000A38 RID: 2616
			[LocDescription(Strings.IDs.LogFieldsResume)]
			Resume,
			// Token: 0x04000A39 RID: 2617
			[LocDescription(Strings.IDs.LogFieldsIncludeKeywordStatistics)]
			IncludeKeywordStatistics,
			// Token: 0x04000A3A RID: 2618
			[LocDescription(Strings.IDs.LogFieldsSearchDumpster)]
			SearchDumpster,
			// Token: 0x04000A3B RID: 2619
			[LocDescription(Strings.IDs.LogFieldsSearchOperation)]
			SearchOperation,
			// Token: 0x04000A3C RID: 2620
			[LocDescription(Strings.IDs.LogFieldsLogLevel)]
			LogLevel,
			// Token: 0x04000A3D RID: 2621
			[LocDescription(Strings.IDs.LogFieldsNumberMailboxesToSearch)]
			NumberMailboxesToSearch,
			// Token: 0x04000A3E RID: 2622
			[LocDescription(Strings.IDs.LogFieldsStatusMailRecipients)]
			StatusMailRecipients,
			// Token: 0x04000A3F RID: 2623
			[LocDescription(Strings.IDs.LogFieldsManagedBy)]
			ManagedBy,
			// Token: 0x04000A40 RID: 2624
			[LocDescription(Strings.IDs.LogFieldsLastRunBy)]
			LastRunBy,
			// Token: 0x04000A41 RID: 2625
			[LocDescription(Strings.IDs.LogFieldsIdentity)]
			Identity,
			// Token: 0x04000A42 RID: 2626
			[LocDescription(Strings.IDs.LogFieldsErrors)]
			Errors,
			// Token: 0x04000A43 RID: 2627
			[LocDescription(Strings.IDs.LogFieldsKeywordHits)]
			KeywordHits,
			// Token: 0x04000A44 RID: 2628
			[LocDescription(Strings.IDs.LogFieldsStatus)]
			Status,
			// Token: 0x04000A45 RID: 2629
			[LocDescription(Strings.IDs.LogFieldsStoppedBy)]
			StoppedBy,
			// Token: 0x04000A46 RID: 2630
			[LocDescription(Strings.IDs.LogFieldsPercentComplete)]
			PercentComplete,
			// Token: 0x04000A47 RID: 2631
			[LocDescription(Strings.IDs.LogFieldsResultNumberEstimate)]
			ResultNumberEstimate,
			// Token: 0x04000A48 RID: 2632
			[LocDescription(Strings.IDs.LogFieldsResultNumber)]
			ResultNumber,
			// Token: 0x04000A49 RID: 2633
			[LocDescription(Strings.IDs.LogFieldsResultSizeEstimate)]
			ResultSizeEstimate,
			// Token: 0x04000A4A RID: 2634
			[LocDescription(Strings.IDs.LogFieldsResultSize)]
			ResultSize,
			// Token: 0x04000A4B RID: 2635
			[LocDescription(Strings.IDs.LogFieldsResultSizeCopied)]
			ResultSizeCopied,
			// Token: 0x04000A4C RID: 2636
			[LocDescription(Strings.IDs.LogFieldsResultsLink)]
			ResultsLink,
			// Token: 0x04000A4D RID: 2637
			[LocDescription(Strings.IDs.LogFieldsEstimateNotExcludeDuplicates)]
			EstimateNotExcludeDuplicates,
			// Token: 0x04000A4E RID: 2638
			[LocDescription(Strings.IDs.LogFieldsExcludeDuplicateMessages)]
			ExcludeDuplicateMessages
		}
	}
}
