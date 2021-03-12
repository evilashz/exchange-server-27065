using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D2E RID: 3374
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SearchStatusSchema : SearchObjectBaseSchema
	{
		// Token: 0x0400515E RID: 20830
		public static readonly ADPropertyDefinition Status = new ADPropertyDefinition("Status", ExchangeObjectVersion.Exchange2007, typeof(SearchState), "status", ADPropertyDefinitionFlags.None, SearchState.InProgress, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400515F RID: 20831
		public static readonly ADPropertyDefinition LastRunBy = new ADPropertyDefinition("LastRunBy", ExchangeObjectVersion.Exchange2007, typeof(ADObjectId), "lastRunBy", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005160 RID: 20832
		public static readonly ADPropertyDefinition LastRunByEx = new ADPropertyDefinition("LastRunBy", ExchangeObjectVersion.Exchange2007, typeof(string), "lastRunBy", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005161 RID: 20833
		public static readonly ADPropertyDefinition LastStartTime = new ADPropertyDefinition("LastStartTime", ExchangeObjectVersion.Exchange2007, typeof(ExDateTime?), "lastStartTime", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005162 RID: 20834
		public static readonly ADPropertyDefinition LastEndTime = new ADPropertyDefinition("LastEndTime", ExchangeObjectVersion.Exchange2007, typeof(ExDateTime?), "lastEndTime", ADPropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005163 RID: 20835
		public static readonly ADPropertyDefinition NumberMailboxesToSearch = new ADPropertyDefinition("NumberMailboxesToSearch", ExchangeObjectVersion.Exchange2007, typeof(int), "numberMailboxesToSearch", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005164 RID: 20836
		public static readonly ADPropertyDefinition PercentComplete = new ADPropertyDefinition("PercentComplete", ExchangeObjectVersion.Exchange2007, typeof(int), "percentComplete", ADPropertyDefinitionFlags.PersistDefaultValue, 0, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005165 RID: 20837
		public static readonly ADPropertyDefinition ResultNumber = new ADPropertyDefinition("ResultNumber", ExchangeObjectVersion.Exchange2007, typeof(long), "resultNumber", ADPropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005166 RID: 20838
		public static readonly ADPropertyDefinition ResultNumberEstimate = new ADPropertyDefinition("ResultNumberEstimate", ExchangeObjectVersion.Exchange2007, typeof(long), "resultNumberEstimate", ADPropertyDefinitionFlags.PersistDefaultValue, 0L, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005167 RID: 20839
		public static readonly ADPropertyDefinition ResultSize = new ADPropertyDefinition("ResultSize", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "resultSize", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005168 RID: 20840
		public static readonly ADPropertyDefinition ResultSizeEstimate = new ADPropertyDefinition("ResultSizeEstimate", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "resultSizeEstimate", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x04005169 RID: 20841
		public static readonly ADPropertyDefinition ResultSizeCopied = new ADPropertyDefinition("ResultSizeCopied", ExchangeObjectVersion.Exchange2007, typeof(ByteQuantifiedSize), "resultSizeCopied", ADPropertyDefinitionFlags.PersistDefaultValue, ByteQuantifiedSize.MinValue, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400516A RID: 20842
		internal static readonly ADPropertyDefinition ResultsPath = new ADPropertyDefinition("ResultsPath", ExchangeObjectVersion.Exchange2007, typeof(string), "resultsPath", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400516B RID: 20843
		public static readonly ADPropertyDefinition ResultsLink = new ADPropertyDefinition("ResultsLink", ExchangeObjectVersion.Exchange2007, typeof(string), "resultsLink", ADPropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400516C RID: 20844
		public static readonly ADPropertyDefinition Errors = new ADPropertyDefinition("Errors", ExchangeObjectVersion.Exchange2007, typeof(string), "errors", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400516D RID: 20845
		public static readonly ADPropertyDefinition KeywordHits = new ADPropertyDefinition("KeywordHits", ExchangeObjectVersion.Exchange2007, typeof(KeywordHit), "KeywordHits", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400516E RID: 20846
		public static readonly ADPropertyDefinition CompletedMailboxes = new ADPropertyDefinition("CompletedMailboxes", ExchangeObjectVersion.Exchange2007, typeof(string), "CompletedMailboxes", ADPropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
