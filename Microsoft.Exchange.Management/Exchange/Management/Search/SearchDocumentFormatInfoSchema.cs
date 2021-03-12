using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x0200015D RID: 349
	internal class SearchDocumentFormatInfoSchema : SimpleProviderObjectSchema
	{
		// Token: 0x0400062A RID: 1578
		public static SimpleProviderPropertyDefinition DocumentClass = new SimpleProviderPropertyDefinition("DocumentClass", ExchangeObjectVersion.Current, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400062B RID: 1579
		public static SimpleProviderPropertyDefinition Enabled = new SimpleProviderPropertyDefinition("Enabled", ExchangeObjectVersion.Current, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400062C RID: 1580
		public static SimpleProviderPropertyDefinition Extension = new SimpleProviderPropertyDefinition("Extension", ExchangeObjectVersion.Current, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400062D RID: 1581
		public static SimpleProviderPropertyDefinition FormatHandler = new SimpleProviderPropertyDefinition("FormatHandler", ExchangeObjectVersion.Current, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400062E RID: 1582
		public static SimpleProviderPropertyDefinition IsBindUserDefined = new SimpleProviderPropertyDefinition("IsBindUserDefined", ExchangeObjectVersion.Current, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400062F RID: 1583
		public static SimpleProviderPropertyDefinition IsFormatUserDefined = new SimpleProviderPropertyDefinition("IsFormatUserDefined", ExchangeObjectVersion.Current, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000630 RID: 1584
		public static SimpleProviderPropertyDefinition MimeType = new SimpleProviderPropertyDefinition("MimeType", ExchangeObjectVersion.Current, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000631 RID: 1585
		public static SimpleProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("Name", ExchangeObjectVersion.Current, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
