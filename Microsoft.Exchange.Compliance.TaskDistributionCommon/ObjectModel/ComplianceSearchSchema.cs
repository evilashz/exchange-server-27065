using System;
using System.Globalization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.ObjectModel
{
	// Token: 0x0200002B RID: 43
	internal class ComplianceSearchSchema : ComplianceJobSchema
	{
		// Token: 0x040000A0 RID: 160
		public static readonly SimpleProviderPropertyDefinition Language = new SimpleProviderPropertyDefinition("Language", ExchangeObjectVersion.Exchange2007, typeof(CultureInfo), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000A1 RID: 161
		public static readonly SimpleProviderPropertyDefinition StatusMailRecipients = new SimpleProviderPropertyDefinition("StatusMailRecipients", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000A2 RID: 162
		public static readonly SimpleProviderPropertyDefinition LogLevel = new SimpleProviderPropertyDefinition("LogLevel", ExchangeObjectVersion.Exchange2007, typeof(ComplianceJobLogLevel), PropertyDefinitionFlags.None, ComplianceJobLogLevel.Basic, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000A3 RID: 163
		public static readonly SimpleProviderPropertyDefinition IncludeUnindexedItems = new SimpleProviderPropertyDefinition("IncludeUnindexedItems", ExchangeObjectVersion.Exchange2007, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000A4 RID: 164
		public static readonly SimpleProviderPropertyDefinition KeywordQuery = new SimpleProviderPropertyDefinition("KeywordQuery", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000A5 RID: 165
		public static readonly SimpleProviderPropertyDefinition StartDate = new SimpleProviderPropertyDefinition("StartDate", ExchangeObjectVersion.Exchange2007, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000A6 RID: 166
		public static readonly SimpleProviderPropertyDefinition EndDate = new SimpleProviderPropertyDefinition("EndDate", ExchangeObjectVersion.Exchange2007, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040000A7 RID: 167
		public static readonly SimpleProviderPropertyDefinition SearchType = new SimpleProviderPropertyDefinition("SearchType", ExchangeObjectVersion.Exchange2007, typeof(ComplianceSearch.ComplianceSearchType), PropertyDefinitionFlags.None, ComplianceSearch.ComplianceSearchType.EstimateSearch, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
