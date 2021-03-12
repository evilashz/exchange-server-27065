using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D2C RID: 3372
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SearchObjectSchema : SearchObjectBaseSchema
	{
		// Token: 0x04005143 RID: 20803
		internal static readonly char[] AqsReservedChars = new char[]
		{
			'"',
			'<',
			'>',
			'=',
			'*',
			'(',
			')',
			',',
			';'
		};

		// Token: 0x04005144 RID: 20804
		public static readonly ADPropertyDefinition CreatedBy = new ADPropertyDefinition("CreatedBy", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "createdBy", ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005145 RID: 20805
		public static readonly ADPropertyDefinition CreatedByEx = new ADPropertyDefinition("CreatedBy", ExchangeObjectVersion.Exchange2007, typeof(string), "createdBy", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005146 RID: 20806
		public static readonly ADPropertyDefinition SourceMailboxes = new ADPropertyDefinition("SourceMailboxes", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "sourceMailboxes", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005147 RID: 20807
		public static readonly ADPropertyDefinition TargetMailbox = new ADPropertyDefinition("TargetMailbox", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "targetMailbox", ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005148 RID: 20808
		public static readonly ADPropertyDefinition SearchQuery = new ADPropertyDefinition("SearchQuery", ExchangeObjectVersion.Exchange2007, typeof(string), "searchQuery", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 10240)
		}, null, null);

		// Token: 0x04005149 RID: 20809
		public static readonly ADPropertyDefinition Language = new ADPropertyDefinition("Language", ExchangeObjectVersion.Exchange2007, typeof(CultureInfo), "language", ADPropertyDefinitionFlags.None, new CultureInfo("en-US"), PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400514A RID: 20810
		public static readonly ADPropertyDefinition Senders = new ADPropertyDefinition("Senders", ExchangeObjectVersion.Exchange2007, typeof(string), "senders", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NotNullOrEmptyConstraint(),
			new CharacterConstraint(SearchObjectSchema.AqsReservedChars, false)
		}, null, null);

		// Token: 0x0400514B RID: 20811
		public static readonly ADPropertyDefinition Recipients = new ADPropertyDefinition("Recipients", ExchangeObjectVersion.Exchange2007, typeof(string), "recipients", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new NotNullOrEmptyConstraint(),
			new CharacterConstraint(SearchObjectSchema.AqsReservedChars, false)
		}, null, null);

		// Token: 0x0400514C RID: 20812
		public static readonly ADPropertyDefinition StartDate = new ADPropertyDefinition("StartDate", ExchangeObjectVersion.Exchange2007, typeof(ExDateTime?), "startDate", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400514D RID: 20813
		public static readonly ADPropertyDefinition EndDate = new ADPropertyDefinition("EndDate", ExchangeObjectVersion.Exchange2007, typeof(ExDateTime?), "endDate", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400514E RID: 20814
		public static readonly ADPropertyDefinition MessageTypes = new ADPropertyDefinition("MessageTypes", ExchangeObjectVersion.Exchange2007, typeof(KindKeyword), "messageTypes", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400514F RID: 20815
		public static readonly ADPropertyDefinition IncludeUnsearchableItems = new ADPropertyDefinition("IncludeUnsearchableItems", ExchangeObjectVersion.Exchange2007, typeof(bool), "includeUnsearchableItems", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005150 RID: 20816
		public static readonly ADPropertyDefinition IncludePersonalArchive = new ADPropertyDefinition("IncludePersonalArchive", ExchangeObjectVersion.Exchange2007, typeof(bool), "includePersonalArchive", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005151 RID: 20817
		public static readonly ADPropertyDefinition IncludeRemoteAccounts = new ADPropertyDefinition("IncludeRemoteAccounts", ExchangeObjectVersion.Exchange2007, typeof(bool), "includeMirroredMailboxes", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005152 RID: 20818
		public static readonly ADPropertyDefinition SearchDumpster = new ADPropertyDefinition("SearchDumpster", ExchangeObjectVersion.Exchange2007, typeof(bool), "searchDumpster", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005153 RID: 20819
		public static readonly ADPropertyDefinition LogLevel = new ADPropertyDefinition("LogLevel", ExchangeObjectVersion.Exchange2007, typeof(LoggingLevel), "logLevel", ADPropertyDefinitionFlags.None, LoggingLevel.Suppress, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005154 RID: 20820
		public static readonly ADPropertyDefinition StatusMailRecipients = new ADPropertyDefinition("StatusMailRecipients", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "statusMailRecipients", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005155 RID: 20821
		public static readonly ADPropertyDefinition ManagedBy = new ADPropertyDefinition("ManagedBy", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "managedBy", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005156 RID: 20822
		public static readonly ADPropertyDefinition EstimateOnly = new ADPropertyDefinition("EstimateOnly", ExchangeObjectVersion.Exchange2007, typeof(bool), "EstimateOnly", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005157 RID: 20823
		public static readonly ADPropertyDefinition ExcludeDuplicateMessages = new ADPropertyDefinition("ExcludeDuplicateMessages", ExchangeObjectVersion.Exchange2007, typeof(bool), "excludeDuplicateMessages ", ADPropertyDefinitionFlags.None, true, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005158 RID: 20824
		public static readonly ADPropertyDefinition Resume = new ADPropertyDefinition("Resume", ExchangeObjectVersion.Exchange2007, typeof(bool), "resume", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005159 RID: 20825
		public static readonly ADPropertyDefinition IncludeKeywordStatistics = new ADPropertyDefinition("IncludeKeywordStatistics", ExchangeObjectVersion.Exchange2007, typeof(bool), "includeKeywordStatistics", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400515A RID: 20826
		public static readonly ADPropertyDefinition KeywordStatisticsDisabled = new ADPropertyDefinition("KeywordStatisticsDisabled", ExchangeObjectVersion.Exchange2007, typeof(bool), "keywordStatisticsDisabled", ADPropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400515B RID: 20827
		public static readonly ADPropertyDefinition Information = new ADPropertyDefinition("Information", ExchangeObjectVersion.Exchange2007, typeof(string), "information", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400515C RID: 20828
		public static readonly ADPropertyDefinition AqsQuery = new ADPropertyDefinition("AqsQuery", ExchangeObjectVersion.Exchange2007, typeof(string), null, ADPropertyDefinitionFlags.ReadOnly | ADPropertyDefinitionFlags.Calculated, string.Empty, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 11264)
		}, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			SearchObjectSchema.SearchQuery,
			SearchObjectSchema.Senders,
			SearchObjectSchema.Recipients,
			SearchObjectSchema.StartDate,
			SearchObjectSchema.EndDate,
			SearchObjectSchema.MessageTypes
		}, null, new GetterDelegate(SearchObject.AqsQueryGetter), null, null, null);
	}
}
