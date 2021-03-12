using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x020009FA RID: 2554
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AggregatedAccountConfigurationSchema : UserConfigurationObjectSchema
	{
		// Token: 0x04003425 RID: 13349
		public static readonly SimplePropertyDefinition EmailAddressRaw = new SimplePropertyDefinition("EmailAddressRaw", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003426 RID: 13350
		public static readonly SimplePropertyDefinition EmailAddress = new SimplePropertyDefinition("EmailAddress", ExchangeObjectVersion.Exchange2012, typeof(SmtpAddress), PropertyDefinitionFlags.Calculated, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new SimplePropertyDefinition[]
		{
			AggregatedAccountConfigurationSchema.EmailAddressRaw
		}, null, new GetterDelegate(AggregatedAccountConfiguration.SmtpAddressGetter), new SetterDelegate(AggregatedAccountConfiguration.SmtpAddressSetter));

		// Token: 0x04003427 RID: 13351
		public static readonly SimplePropertyDefinition SyncFailureCode = new SimplePropertyDefinition("SyncFailureCode", ExchangeObjectVersion.Exchange2012, typeof(int), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003428 RID: 13352
		public static readonly SimplePropertyDefinition SyncFailureTimestamp = new SimplePropertyDefinition("SyncFailureTimestamp", ExchangeObjectVersion.Exchange2012, typeof(ExDateTime), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003429 RID: 13353
		public static readonly SimplePropertyDefinition SyncFailureType = new SimplePropertyDefinition("SyncFailureType", ExchangeObjectVersion.Exchange2012, typeof(string), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400342A RID: 13354
		public static readonly SimplePropertyDefinition SyncLastUpdateTimestamp = new SimplePropertyDefinition("SyncLastUpdateTimestamp", ExchangeObjectVersion.Exchange2012, typeof(ExDateTime), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400342B RID: 13355
		public static readonly SimplePropertyDefinition SyncQueuedTimestamp = new SimplePropertyDefinition("SyncQueuedTimestamp", ExchangeObjectVersion.Exchange2012, typeof(ExDateTime), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400342C RID: 13356
		public static readonly SimplePropertyDefinition SyncRequestGuidRaw = new SimplePropertyDefinition("SyncRequestGuidRaw", ExchangeObjectVersion.Exchange2012, typeof(byte[]), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400342D RID: 13357
		public static readonly SimplePropertyDefinition SyncRequestGuid = new SimplePropertyDefinition("SyncRequestGuid", ExchangeObjectVersion.Exchange2012, typeof(Guid), PropertyDefinitionFlags.Calculated, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new SimplePropertyDefinition[]
		{
			AggregatedAccountConfigurationSchema.SyncRequestGuidRaw
		}, null, new GetterDelegate(AggregatedAccountConfiguration.SyncRequestGuidGetter), new SetterDelegate(AggregatedAccountConfiguration.SyncRequestGuidSetter));

		// Token: 0x0400342E RID: 13358
		public static readonly SimplePropertyDefinition SyncStartTimestamp = new SimplePropertyDefinition("SyncStartTimestamp", ExchangeObjectVersion.Exchange2012, typeof(ExDateTime), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400342F RID: 13359
		public static readonly SimplePropertyDefinition SyncStatus = new SimplePropertyDefinition("SyncStatus", ExchangeObjectVersion.Exchange2012, typeof(RequestStatus), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003430 RID: 13360
		public static readonly SimplePropertyDefinition SyncSuspendedTimestamp = new SimplePropertyDefinition("SyncSuspendedTimestamp", ExchangeObjectVersion.Exchange2012, typeof(ExDateTime), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
