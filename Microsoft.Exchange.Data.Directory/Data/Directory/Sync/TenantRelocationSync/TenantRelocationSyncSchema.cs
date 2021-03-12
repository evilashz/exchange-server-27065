using System;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x0200080C RID: 2060
	internal class TenantRelocationSyncSchema : ADObjectSchema
	{
		// Token: 0x0400436B RID: 17259
		public static readonly ADPropertyDefinition AllAttributes = new ADPropertyDefinition("AllAttributes", ExchangeObjectVersion.Exchange2003, typeof(Guid), "*", ADPropertyDefinitionFlags.Binary | ADPropertyDefinitionFlags.DoNotProvisionalClone, System.Guid.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x0400436C RID: 17260
		public static ADPropertyDefinition ObjectId = SyncObjectSchema.ObjectId;

		// Token: 0x0400436D RID: 17261
		public static ADPropertyDefinition LastKnownParent = SyncObjectSchema.LastKnownParent;

		// Token: 0x0400436E RID: 17262
		public static ADPropertyDefinition Deleted = SyncObjectSchema.Deleted;

		// Token: 0x0400436F RID: 17263
		public static ADPropertyDefinition ConfigurationXMLRaw = ADRecipientSchema.ConfigurationXMLRaw;
	}
}
