using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000124 RID: 292
	internal class BinaryFileDataObjectSchema : SimpleProviderObjectSchema
	{
		// Token: 0x0400063D RID: 1597
		public static readonly SimpleProviderPropertyDefinition FileData = new SimpleProviderPropertyDefinition("FileData", ExchangeObjectVersion.Exchange2003, typeof(byte[]), PropertyDefinitionFlags.Binary, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
