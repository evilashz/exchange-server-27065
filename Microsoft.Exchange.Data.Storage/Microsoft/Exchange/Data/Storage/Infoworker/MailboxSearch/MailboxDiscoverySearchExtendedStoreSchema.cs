using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D14 RID: 3348
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxDiscoverySearchExtendedStoreSchema : ObjectSchema
	{
		// Token: 0x040050B0 RID: 20656
		internal static readonly Guid PropertySetId = new Guid("E27E00C0-86D5-4306-AC73-50F5397A8321");

		// Token: 0x040050B1 RID: 20657
		public static readonly ExtendedPropertyDefinition Target = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "Target", 25);

		// Token: 0x040050B2 RID: 20658
		public static readonly ExtendedPropertyDefinition StatisticsOnly = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "StatisticsOnly", 4);

		// Token: 0x040050B3 RID: 20659
		public static readonly ExtendedPropertyDefinition AllSourceMailboxes = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "AllSourceMailboxes", 4);

		// Token: 0x040050B4 RID: 20660
		public static readonly ExtendedPropertyDefinition AllPublicFolderSources = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "AllPublicFolderSources", 4);

		// Token: 0x040050B5 RID: 20661
		public static readonly ExtendedPropertyDefinition SearchStatistics = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "SearchStatistics", 26);

		// Token: 0x040050B6 RID: 20662
		public static readonly ExtendedPropertyDefinition Version = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "Version", 14);

		// Token: 0x040050B7 RID: 20663
		public static readonly ExtendedPropertyDefinition IncludeUnsearchableItems = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "IncludeUnsearchableItems", 4);

		// Token: 0x040050B8 RID: 20664
		public static readonly ExtendedPropertyDefinition Resume = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "Resume", 4);

		// Token: 0x040050B9 RID: 20665
		public static readonly ExtendedPropertyDefinition LastStartTime = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "LastStartTime", 23);

		// Token: 0x040050BA RID: 20666
		public static readonly ExtendedPropertyDefinition LastEndTime = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "LastEndTime", 23);

		// Token: 0x040050BB RID: 20667
		public static readonly ExtendedPropertyDefinition NumberOfMailboxes = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "NumberOfMailboxes", 14);

		// Token: 0x040050BC RID: 20668
		public static readonly ExtendedPropertyDefinition PercentComplete = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "PercentComplete", 14);

		// Token: 0x040050BD RID: 20669
		public static readonly ExtendedPropertyDefinition ResultItemCountCopied = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "ResultItemCountCopied", 16);

		// Token: 0x040050BE RID: 20670
		public static readonly ExtendedPropertyDefinition ResultItemCountEstimate = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "ResultItemCountEstimate", 16);

		// Token: 0x040050BF RID: 20671
		public static readonly ExtendedPropertyDefinition ResultUnsearchableItemCountEstimate = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "ResultUnsearchableItemCountEstimate", 16);

		// Token: 0x040050C0 RID: 20672
		public static readonly ExtendedPropertyDefinition ResultSizeCopied = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "ResultSizeCopied", 16);

		// Token: 0x040050C1 RID: 20673
		public static readonly ExtendedPropertyDefinition ResultSizeEstimate = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "ResultSizeEstimate", 16);

		// Token: 0x040050C2 RID: 20674
		public static readonly ExtendedPropertyDefinition ResultsPath = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "ResultsPath", 25);

		// Token: 0x040050C3 RID: 20675
		public static readonly ExtendedPropertyDefinition ResultsLink = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "ResultsLink", 25);

		// Token: 0x040050C4 RID: 20676
		public static readonly ExtendedPropertyDefinition PreviewResultsLink = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "PreviewResultsLink", 25);

		// Token: 0x040050C5 RID: 20677
		public static readonly ExtendedPropertyDefinition LogLevel = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "LogLevel", 14);

		// Token: 0x040050C6 RID: 20678
		public static readonly ExtendedPropertyDefinition StatusMailRecipients = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "StatusMailRecipients", 26);

		// Token: 0x040050C7 RID: 20679
		public static readonly ExtendedPropertyDefinition ManagedBy = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "ManagedBy", 26);

		// Token: 0x040050C8 RID: 20680
		public static readonly ExtendedPropertyDefinition Query = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "Query", 25);

		// Token: 0x040050C9 RID: 20681
		public static readonly ExtendedPropertyDefinition Senders = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "Senders", 26);

		// Token: 0x040050CA RID: 20682
		public static readonly ExtendedPropertyDefinition Recipients = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "Recipients", 26);

		// Token: 0x040050CB RID: 20683
		public static readonly ExtendedPropertyDefinition StartDate = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "StartDate", 23);

		// Token: 0x040050CC RID: 20684
		public static readonly ExtendedPropertyDefinition EndDate = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "EndDate", 23);

		// Token: 0x040050CD RID: 20685
		public static readonly ExtendedPropertyDefinition MessageTypes = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "MessageTypes", 26);

		// Token: 0x040050CE RID: 20686
		public static readonly ExtendedPropertyDefinition CalculatedQuery = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "CalculatedQuery", 25);

		// Token: 0x040050CF RID: 20687
		public static readonly ExtendedPropertyDefinition Language = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "Language", 25);

		// Token: 0x040050D0 RID: 20688
		public static readonly ExtendedPropertyDefinition CreatedBy = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "CreatedBy", 25);

		// Token: 0x040050D1 RID: 20689
		public static readonly ExtendedPropertyDefinition LastModifiedBy = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "LastModifiedBy", 25);

		// Token: 0x040050D2 RID: 20690
		public static readonly ExtendedPropertyDefinition ExcludeDuplicateMessages = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "ExcludeDuplicateMessages", 4);

		// Token: 0x040050D3 RID: 20691
		public static readonly ExtendedPropertyDefinition InPlaceHoldEnabled = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "InPlaceHoldEnabled", 4);

		// Token: 0x040050D4 RID: 20692
		public static readonly ExtendedPropertyDefinition ItemHoldPeriod = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "ItemHoldPeriod", 16);

		// Token: 0x040050D5 RID: 20693
		public static readonly ExtendedPropertyDefinition InPlaceHoldIdentity = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "InPlaceHoldIdentity", 25);

		// Token: 0x040050D6 RID: 20694
		public static readonly ExtendedPropertyDefinition LegacySearchObjectIdentity = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "LegacySearchObjectIdentity", 25);

		// Token: 0x040050D7 RID: 20695
		public static readonly ExtendedPropertyDefinition Description = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "Description", 25);

		// Token: 0x040050D8 RID: 20696
		public static readonly ExtendedPropertyDefinition ManagedByOrganization = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "ManagedByOrganization", 25);

		// Token: 0x040050D9 RID: 20697
		public static readonly ExtendedPropertyDefinition IncludeKeywordStatistics = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "IncludeKeywordStatistics", 4);

		// Token: 0x040050DA RID: 20698
		public static readonly ExtendedPropertyDefinition StatisticsStartIndex = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "StatisticsStartIndex", 14);

		// Token: 0x040050DB RID: 20699
		public static readonly ExtendedPropertyDefinition UserKeywords = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "UserKeywords", 26);

		// Token: 0x040050DC RID: 20700
		public static readonly ExtendedPropertyDefinition KeywordsQuery = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "KeywordsQuery", 25);

		// Token: 0x040050DD RID: 20701
		public static readonly ExtendedPropertyDefinition TotalKeywords = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "TotalKeywords", 14);

		// Token: 0x040050DE RID: 20702
		public static readonly ExtendedPropertyDefinition KeywordHits = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "KeywordHits", 26);

		// Token: 0x040050DF RID: 20703
		public static readonly ExtendedPropertyDefinition KeywordStatisticsDisabled = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "KeywordStatisticsDisabled", 4);

		// Token: 0x040050E0 RID: 20704
		public static readonly ExtendedPropertyDefinition PreviewDisabled = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "PreviewDisabled", 4);

		// Token: 0x040050E1 RID: 20705
		public static readonly ExtendedPropertyDefinition Information = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "Information", 26);

		// Token: 0x040050E2 RID: 20706
		public static readonly ExtendedPropertyDefinition ScenarioId = new ExtendedPropertyDefinition(MailboxDiscoverySearchExtendedStoreSchema.PropertySetId, "ScenarioId", 25);
	}
}
