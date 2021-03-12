using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D13 RID: 3347
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxDiscoverySearchSchema : EwsStoreObjectSchema
	{
		// Token: 0x04005075 RID: 20597
		public static readonly EwsStoreObjectPropertyDefinition Target = new EwsStoreObjectPropertyDefinition("Target", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, null, MailboxDiscoverySearchExtendedStoreSchema.Target);

		// Token: 0x04005076 RID: 20598
		public static readonly EwsStoreObjectPropertyDefinition Status = new EwsStoreObjectPropertyDefinition("Status", ExchangeObjectVersion.Exchange2012, typeof(SearchState), PropertyDefinitionFlags.PersistDefaultValue, SearchState.NotStarted, SearchState.NotStarted, ExtendedEwsStoreObjectSchema.Status);

		// Token: 0x04005077 RID: 20599
		public static readonly EwsStoreObjectPropertyDefinition StatisticsOnly = new EwsStoreObjectPropertyDefinition("StatisticsOnly", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, false, MailboxDiscoverySearchExtendedStoreSchema.StatisticsOnly);

		// Token: 0x04005078 RID: 20600
		public static readonly EwsStoreObjectPropertyDefinition IncludeUnsearchableItems = new EwsStoreObjectPropertyDefinition("IncludeUnsearchableItems", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, false, MailboxDiscoverySearchExtendedStoreSchema.IncludeUnsearchableItems);

		// Token: 0x04005079 RID: 20601
		public static readonly EwsStoreObjectPropertyDefinition Resume = new EwsStoreObjectPropertyDefinition("Resume", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, false, MailboxDiscoverySearchExtendedStoreSchema.Resume);

		// Token: 0x0400507A RID: 20602
		public static readonly EwsStoreObjectPropertyDefinition LastStartTime = new EwsStoreObjectPropertyDefinition("LastStartTime", ExchangeObjectVersion.Exchange2012, typeof(DateTime), PropertyDefinitionFlags.None, default(DateTime), default(DateTime), MailboxDiscoverySearchExtendedStoreSchema.LastStartTime);

		// Token: 0x0400507B RID: 20603
		public static readonly EwsStoreObjectPropertyDefinition LastEndTime = new EwsStoreObjectPropertyDefinition("LastEndTime", ExchangeObjectVersion.Exchange2012, typeof(DateTime), PropertyDefinitionFlags.None, default(DateTime), default(DateTime), MailboxDiscoverySearchExtendedStoreSchema.LastEndTime);

		// Token: 0x0400507C RID: 20604
		public static readonly EwsStoreObjectPropertyDefinition PercentComplete = new EwsStoreObjectPropertyDefinition("PercentComplete", ExchangeObjectVersion.Exchange2012, typeof(int), PropertyDefinitionFlags.None, 0, 0, MailboxDiscoverySearchExtendedStoreSchema.PercentComplete);

		// Token: 0x0400507D RID: 20605
		public static readonly EwsStoreObjectPropertyDefinition NumberOfMailboxes = new EwsStoreObjectPropertyDefinition("NumberOfMailboxes", ExchangeObjectVersion.Exchange2012, typeof(int), PropertyDefinitionFlags.None, 0, 0, MailboxDiscoverySearchExtendedStoreSchema.NumberOfMailboxes);

		// Token: 0x0400507E RID: 20606
		public static readonly EwsStoreObjectPropertyDefinition ResultItemCountCopied = new EwsStoreObjectPropertyDefinition("ResultItemCountCopied", ExchangeObjectVersion.Exchange2012, typeof(long), PropertyDefinitionFlags.None, 0L, 0L, MailboxDiscoverySearchExtendedStoreSchema.ResultItemCountCopied);

		// Token: 0x0400507F RID: 20607
		public static readonly EwsStoreObjectPropertyDefinition ResultItemCountEstimate = new EwsStoreObjectPropertyDefinition("ResultItemCountEstimate", ExchangeObjectVersion.Exchange2012, typeof(long), PropertyDefinitionFlags.None, 0L, 0L, MailboxDiscoverySearchExtendedStoreSchema.ResultItemCountEstimate);

		// Token: 0x04005080 RID: 20608
		public static readonly EwsStoreObjectPropertyDefinition ResultUnsearchableItemCountEstimate = new EwsStoreObjectPropertyDefinition("ResultUnsearchableItemCountEstimate", ExchangeObjectVersion.Exchange2012, typeof(long), PropertyDefinitionFlags.None, 0L, 0L, MailboxDiscoverySearchExtendedStoreSchema.ResultUnsearchableItemCountEstimate);

		// Token: 0x04005081 RID: 20609
		public static readonly EwsStoreObjectPropertyDefinition ResultSizeCopied = new EwsStoreObjectPropertyDefinition("ResultSizeCopied", ExchangeObjectVersion.Exchange2012, typeof(long), PropertyDefinitionFlags.None, 0L, 0L, MailboxDiscoverySearchExtendedStoreSchema.ResultSizeCopied);

		// Token: 0x04005082 RID: 20610
		public static readonly EwsStoreObjectPropertyDefinition ResultSizeEstimate = new EwsStoreObjectPropertyDefinition("ResultSizeEstimate", ExchangeObjectVersion.Exchange2012, typeof(long), PropertyDefinitionFlags.None, 0L, 0L, MailboxDiscoverySearchExtendedStoreSchema.ResultSizeEstimate);

		// Token: 0x04005083 RID: 20611
		public static readonly EwsStoreObjectPropertyDefinition ResultsPath = new EwsStoreObjectPropertyDefinition("ResultsPath", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.ReturnOnBind, null, null, MailboxDiscoverySearchExtendedStoreSchema.ResultsPath);

		// Token: 0x04005084 RID: 20612
		public static readonly EwsStoreObjectPropertyDefinition ResultsLink = new EwsStoreObjectPropertyDefinition("ResultsLink", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.ReturnOnBind, null, null, MailboxDiscoverySearchExtendedStoreSchema.ResultsLink);

		// Token: 0x04005085 RID: 20613
		public static readonly EwsStoreObjectPropertyDefinition PreviewResultsLink = new EwsStoreObjectPropertyDefinition("PreviewResultsLink", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.ReturnOnBind, null, null, MailboxDiscoverySearchExtendedStoreSchema.PreviewResultsLink);

		// Token: 0x04005086 RID: 20614
		public static readonly EwsStoreObjectPropertyDefinition LogLevel = new EwsStoreObjectPropertyDefinition("LogLevel", ExchangeObjectVersion.Exchange2012, typeof(LoggingLevel), PropertyDefinitionFlags.None, LoggingLevel.Suppress, LoggingLevel.Suppress, MailboxDiscoverySearchExtendedStoreSchema.LogLevel);

		// Token: 0x04005087 RID: 20615
		public static readonly EwsStoreObjectPropertyDefinition CompletedMailboxes = new EwsStoreObjectPropertyDefinition("CompletedMailboxes", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, ItemSchema.Attachments);

		// Token: 0x04005088 RID: 20616
		public static readonly EwsStoreObjectPropertyDefinition StatusMailRecipients = new EwsStoreObjectPropertyDefinition("StatusMailRecipients", ExchangeObjectVersion.Exchange2012, typeof(ADObjectId), PropertyDefinitionFlags.MultiValued, null, null, MailboxDiscoverySearchExtendedStoreSchema.StatusMailRecipients);

		// Token: 0x04005089 RID: 20617
		public static readonly EwsStoreObjectPropertyDefinition ManagedBy = new EwsStoreObjectPropertyDefinition("ManagedBy", ExchangeObjectVersion.Exchange2012, typeof(ADObjectId), PropertyDefinitionFlags.MultiValued, null, null, MailboxDiscoverySearchExtendedStoreSchema.ManagedBy);

		// Token: 0x0400508A RID: 20618
		public static readonly EwsStoreObjectPropertyDefinition Query = new EwsStoreObjectPropertyDefinition("Query", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.ReturnOnBind, string.Empty, string.Empty, MailboxDiscoverySearchExtendedStoreSchema.Query);

		// Token: 0x0400508B RID: 20619
		public static readonly EwsStoreObjectPropertyDefinition Senders = new EwsStoreObjectPropertyDefinition("Senders", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, MailboxDiscoverySearchExtendedStoreSchema.Senders);

		// Token: 0x0400508C RID: 20620
		public static readonly EwsStoreObjectPropertyDefinition Recipients = new EwsStoreObjectPropertyDefinition("Recipients", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, MailboxDiscoverySearchExtendedStoreSchema.Recipients);

		// Token: 0x0400508D RID: 20621
		public static readonly EwsStoreObjectPropertyDefinition StartDate = new EwsStoreObjectPropertyDefinition("StartDate", ExchangeObjectVersion.Exchange2012, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, null, MailboxDiscoverySearchExtendedStoreSchema.StartDate);

		// Token: 0x0400508E RID: 20622
		public static readonly EwsStoreObjectPropertyDefinition EndDate = new EwsStoreObjectPropertyDefinition("EndDate", ExchangeObjectVersion.Exchange2012, typeof(ExDateTime?), PropertyDefinitionFlags.None, null, null, MailboxDiscoverySearchExtendedStoreSchema.EndDate);

		// Token: 0x0400508F RID: 20623
		public static readonly EwsStoreObjectPropertyDefinition MessageTypes = new EwsStoreObjectPropertyDefinition("MessageTypes", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, MailboxDiscoverySearchExtendedStoreSchema.MessageTypes);

		// Token: 0x04005090 RID: 20624
		public static readonly EwsStoreObjectPropertyDefinition CalculatedQuery = new EwsStoreObjectPropertyDefinition("CalculatedQuery", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.ReturnOnBind, string.Empty, string.Empty, MailboxDiscoverySearchExtendedStoreSchema.CalculatedQuery);

		// Token: 0x04005091 RID: 20625
		public static readonly EwsStoreObjectPropertyDefinition Language = new EwsStoreObjectPropertyDefinition("Language", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, "en-US", "en-US", MailboxDiscoverySearchExtendedStoreSchema.Language);

		// Token: 0x04005092 RID: 20626
		public static readonly EwsStoreObjectPropertyDefinition Sources = new EwsStoreObjectPropertyDefinition("Sources", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, ItemSchema.Attachments);

		// Token: 0x04005093 RID: 20627
		public static readonly EwsStoreObjectPropertyDefinition AllSourceMailboxes = new EwsStoreObjectPropertyDefinition("AllSourceMailboxes", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, false, MailboxDiscoverySearchExtendedStoreSchema.AllSourceMailboxes);

		// Token: 0x04005094 RID: 20628
		public static readonly EwsStoreObjectPropertyDefinition PublicFolderSources = new EwsStoreObjectPropertyDefinition("PublicFolderSources", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, ItemSchema.Attachments);

		// Token: 0x04005095 RID: 20629
		public static readonly EwsStoreObjectPropertyDefinition AllPublicFolderSources = new EwsStoreObjectPropertyDefinition("AllPublicFolderSources", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, false, MailboxDiscoverySearchExtendedStoreSchema.AllPublicFolderSources);

		// Token: 0x04005096 RID: 20630
		public static readonly EwsStoreObjectPropertyDefinition SearchStatistics = new EwsStoreObjectPropertyDefinition("SearchStatistics", ExchangeObjectVersion.Exchange2012, typeof(DiscoverySearchStats), PropertyDefinitionFlags.MultiValued, null, null, MailboxDiscoverySearchExtendedStoreSchema.SearchStatistics);

		// Token: 0x04005097 RID: 20631
		public static readonly EwsStoreObjectPropertyDefinition Version = new EwsStoreObjectPropertyDefinition("Version", ExchangeObjectVersion.Exchange2012, typeof(SearchObjectVersion), PropertyDefinitionFlags.None, SearchObjectVersion.Original, SearchObjectVersion.Original, MailboxDiscoverySearchExtendedStoreSchema.Version);

		// Token: 0x04005098 RID: 20632
		public static readonly EwsStoreObjectPropertyDefinition CreatedTime = new EwsStoreObjectPropertyDefinition("CreatedTime", ExchangeObjectVersion.Exchange2012, typeof(DateTime), PropertyDefinitionFlags.ReadOnly, default(DateTime), default(DateTime), ItemSchema.DateTimeCreated);

		// Token: 0x04005099 RID: 20633
		public static readonly EwsStoreObjectPropertyDefinition CreatedBy = new EwsStoreObjectPropertyDefinition("CreatedBy", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, string.Empty, MailboxDiscoverySearchExtendedStoreSchema.CreatedBy);

		// Token: 0x0400509A RID: 20634
		public static readonly EwsStoreObjectPropertyDefinition LastModifiedTime = new EwsStoreObjectPropertyDefinition("LastModifiedTime", ExchangeObjectVersion.Exchange2012, typeof(DateTime), PropertyDefinitionFlags.ReadOnly, default(DateTime), default(DateTime), ItemSchema.LastModifiedTime);

		// Token: 0x0400509B RID: 20635
		public static readonly EwsStoreObjectPropertyDefinition LastModifiedBy = new EwsStoreObjectPropertyDefinition("LastModifiedBy", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, string.Empty, MailboxDiscoverySearchExtendedStoreSchema.LastModifiedBy);

		// Token: 0x0400509C RID: 20636
		public static readonly EwsStoreObjectPropertyDefinition ExcludeDuplicateMessages = new EwsStoreObjectPropertyDefinition("ExcludeDuplicateMessages", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, false, MailboxDiscoverySearchExtendedStoreSchema.ExcludeDuplicateMessages);

		// Token: 0x0400509D RID: 20637
		public static readonly EwsStoreObjectPropertyDefinition Errors = new EwsStoreObjectPropertyDefinition("Errors", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, ItemSchema.Attachments);

		// Token: 0x0400509E RID: 20638
		public static readonly EwsStoreObjectPropertyDefinition InPlaceHoldEnabled = new EwsStoreObjectPropertyDefinition("InPlaceHoldEnabled", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, false, MailboxDiscoverySearchExtendedStoreSchema.InPlaceHoldEnabled);

		// Token: 0x0400509F RID: 20639
		public static readonly EwsStoreObjectPropertyDefinition ItemHoldPeriod = new EwsStoreObjectPropertyDefinition("ItemHoldPeriod", ExchangeObjectVersion.Exchange2012, typeof(Unlimited<EnhancedTimeSpan>), PropertyDefinitionFlags.None, Unlimited<EnhancedTimeSpan>.UnlimitedValue, Unlimited<EnhancedTimeSpan>.UnlimitedValue, MailboxDiscoverySearchExtendedStoreSchema.ItemHoldPeriod);

		// Token: 0x040050A0 RID: 20640
		public static readonly EwsStoreObjectPropertyDefinition InPlaceHoldIdentity = new EwsStoreObjectPropertyDefinition("InPlaceHoldIdentity", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, string.Empty, MailboxDiscoverySearchExtendedStoreSchema.InPlaceHoldIdentity);

		// Token: 0x040050A1 RID: 20641
		public static readonly EwsStoreObjectPropertyDefinition LegacySearchObjectIdentity = new EwsStoreObjectPropertyDefinition("LegacySearchObjectIdentity", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, string.Empty, MailboxDiscoverySearchExtendedStoreSchema.LegacySearchObjectIdentity);

		// Token: 0x040050A2 RID: 20642
		public static readonly EwsStoreObjectPropertyDefinition Description = new EwsStoreObjectPropertyDefinition("Description", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, string.Empty, MailboxDiscoverySearchExtendedStoreSchema.Description);

		// Token: 0x040050A3 RID: 20643
		public static readonly EwsStoreObjectPropertyDefinition FailedToHoldMailboxes = new EwsStoreObjectPropertyDefinition("FailedToHoldMailboxes", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, ItemSchema.Attachments);

		// Token: 0x040050A4 RID: 20644
		public static readonly EwsStoreObjectPropertyDefinition InPlaceHoldErrors = new EwsStoreObjectPropertyDefinition("InPlaceHoldErrors", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, ItemSchema.Attachments);

		// Token: 0x040050A5 RID: 20645
		public static readonly EwsStoreObjectPropertyDefinition ManagedByOrganization = new EwsStoreObjectPropertyDefinition("ManagedByOrganization", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, string.Empty, MailboxDiscoverySearchExtendedStoreSchema.ManagedByOrganization);

		// Token: 0x040050A6 RID: 20646
		public static readonly EwsStoreObjectPropertyDefinition IncludeKeywordStatistics = new EwsStoreObjectPropertyDefinition("IncludeKeywordStatistics", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, false, MailboxDiscoverySearchExtendedStoreSchema.IncludeKeywordStatistics);

		// Token: 0x040050A7 RID: 20647
		public static readonly EwsStoreObjectPropertyDefinition StatisticsStartIndex = new EwsStoreObjectPropertyDefinition("StatisticsStartIndex", ExchangeObjectVersion.Exchange2012, typeof(int), PropertyDefinitionFlags.None, 1, 1, MailboxDiscoverySearchExtendedStoreSchema.StatisticsStartIndex);

		// Token: 0x040050A8 RID: 20648
		public static readonly EwsStoreObjectPropertyDefinition UserKeywords = new EwsStoreObjectPropertyDefinition("UserKeywords", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, MailboxDiscoverySearchExtendedStoreSchema.UserKeywords);

		// Token: 0x040050A9 RID: 20649
		public static readonly EwsStoreObjectPropertyDefinition KeywordsQuery = new EwsStoreObjectPropertyDefinition("KeywordsQuery", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.ReturnOnBind, string.Empty, string.Empty, MailboxDiscoverySearchExtendedStoreSchema.KeywordsQuery);

		// Token: 0x040050AA RID: 20650
		public static readonly EwsStoreObjectPropertyDefinition TotalKeywords = new EwsStoreObjectPropertyDefinition("TotalKeywords", ExchangeObjectVersion.Exchange2012, typeof(int), PropertyDefinitionFlags.None, 0, 0, MailboxDiscoverySearchExtendedStoreSchema.TotalKeywords);

		// Token: 0x040050AB RID: 20651
		public static readonly EwsStoreObjectPropertyDefinition KeywordHits = new EwsStoreObjectPropertyDefinition("KeywordHits", ExchangeObjectVersion.Exchange2012, typeof(KeywordHit), PropertyDefinitionFlags.MultiValued, null, null, MailboxDiscoverySearchExtendedStoreSchema.KeywordHits);

		// Token: 0x040050AC RID: 20652
		public static readonly EwsStoreObjectPropertyDefinition KeywordStatisticsDisabled = new EwsStoreObjectPropertyDefinition("KeywordStatisticsDisabled", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, false, MailboxDiscoverySearchExtendedStoreSchema.KeywordStatisticsDisabled);

		// Token: 0x040050AD RID: 20653
		public static readonly EwsStoreObjectPropertyDefinition PreviewDisabled = new EwsStoreObjectPropertyDefinition("PreviewDisabled", ExchangeObjectVersion.Exchange2012, typeof(bool), PropertyDefinitionFlags.None, false, false, MailboxDiscoverySearchExtendedStoreSchema.PreviewDisabled);

		// Token: 0x040050AE RID: 20654
		public static readonly EwsStoreObjectPropertyDefinition Information = new EwsStoreObjectPropertyDefinition("Information", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued, null, null, MailboxDiscoverySearchExtendedStoreSchema.Information);

		// Token: 0x040050AF RID: 20655
		public static readonly EwsStoreObjectPropertyDefinition ScenarioId = new EwsStoreObjectPropertyDefinition("ScenarioId", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, string.Empty, MailboxDiscoverySearchExtendedStoreSchema.ScenarioId);
	}
}
