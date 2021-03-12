using System;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;

namespace Microsoft.Exchange.Data.Storage.UnifiedPolicy
{
	// Token: 0x02000E97 RID: 3735
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ExBindingStoreObjectSchema : EwsStoreObjectSchema
	{
		// Token: 0x04005726 RID: 22310
		public static readonly EwsStoreObjectPropertyDefinition PolicyId = EwsStoreObjectSchema.AlternativeId;

		// Token: 0x04005727 RID: 22311
		public static readonly EwsStoreObjectPropertyDefinition Name = new EwsStoreObjectPropertyDefinition("Name", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, string.Empty, ExBindingStoreObjectExtendedStoreSchema.Name);

		// Token: 0x04005728 RID: 22312
		public static readonly EwsStoreObjectPropertyDefinition MasterIdentity = new EwsStoreObjectPropertyDefinition("MasterIdentity", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, string.Empty, string.Empty, ExBindingStoreObjectExtendedStoreSchema.MasterIdentity);

		// Token: 0x04005729 RID: 22313
		public static readonly EwsStoreObjectPropertyDefinition Workload = new EwsStoreObjectPropertyDefinition("Workload", ExchangeObjectVersion.Exchange2012, typeof(Workload), PropertyDefinitionFlags.PersistDefaultValue, Microsoft.Office.CompliancePolicy.PolicyConfiguration.Workload.Exchange, Microsoft.Office.CompliancePolicy.PolicyConfiguration.Workload.Exchange, ExBindingStoreObjectExtendedStoreSchema.Workload);

		// Token: 0x0400572A RID: 22314
		public static readonly EwsStoreObjectPropertyDefinition PolicyVersion = new EwsStoreObjectPropertyDefinition("PolicyVersion", ExchangeObjectVersion.Exchange2012, typeof(Guid), PropertyDefinitionFlags.None, Guid.Empty, Guid.Empty, ExBindingStoreObjectExtendedStoreSchema.PolicyVersion);

		// Token: 0x0400572B RID: 22315
		public static readonly EwsStoreObjectPropertyDefinition RawAppliedScopes = new EwsStoreObjectPropertyDefinition("RawAppliedScopes", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.MultiValued | PropertyDefinitionFlags.ReturnOnBind, null, null, ItemSchema.Attachments);

		// Token: 0x0400572C RID: 22316
		public static readonly EwsStoreObjectPropertyDefinition WhenCreated = new EwsStoreObjectPropertyDefinition("WhenCreated", ExchangeObjectVersion.Exchange2012, typeof(DateTime), PropertyDefinitionFlags.ReadOnly, null, null, ItemSchema.DateTimeCreated);

		// Token: 0x0400572D RID: 22317
		public static readonly EwsStoreObjectPropertyDefinition WhenChanged = new EwsStoreObjectPropertyDefinition("WhenChanged", ExchangeObjectVersion.Exchange2012, typeof(DateTime?), PropertyDefinitionFlags.ReadOnly, null, null, ItemSchema.LastModifiedTime);
	}
}
