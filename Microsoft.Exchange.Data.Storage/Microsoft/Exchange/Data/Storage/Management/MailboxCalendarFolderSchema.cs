using System;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A03 RID: 2563
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxCalendarFolderSchema : UserConfigurationObjectSchema
	{
		// Token: 0x04003475 RID: 13429
		public static readonly SimpleProviderPropertyDefinition MailboxFolderId = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2003, typeof(MailboxFolderId), PropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003476 RID: 13430
		public static readonly SimpleProviderPropertyDefinition PublishEnabled = new SimpleProviderPropertyDefinition("PublishEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003477 RID: 13431
		public static readonly SimpleProviderPropertyDefinition PublishDateRangeFrom = new SimpleProviderPropertyDefinition("PublishDateRangeFrom", ExchangeObjectVersion.Exchange2010, typeof(DateRangeEnumType), PropertyDefinitionFlags.None, DateRangeEnumType.ThreeMonths, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003478 RID: 13432
		public static readonly SimpleProviderPropertyDefinition PublishDateRangeTo = new SimpleProviderPropertyDefinition("PublishDateRangeTo", ExchangeObjectVersion.Exchange2010, typeof(DateRangeEnumType), PropertyDefinitionFlags.None, DateRangeEnumType.ThreeMonths, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003479 RID: 13433
		public static readonly SimpleProviderPropertyDefinition DetailLevel = new SimpleProviderPropertyDefinition("DetailLevel", ExchangeObjectVersion.Exchange2010, typeof(DetailLevelEnumType), PropertyDefinitionFlags.None, DetailLevelEnumType.AvailabilityOnly, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400347A RID: 13434
		public static readonly SimpleProviderPropertyDefinition SearchableUrlEnabled = new SimpleProviderPropertyDefinition("SearchableUrlEnabled", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400347B RID: 13435
		public static readonly SimpleProviderPropertyDefinition PublishedCalendarUrl = new SimpleProviderPropertyDefinition("PublishedCalendarUrl", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400347C RID: 13436
		public static readonly SimpleProviderPropertyDefinition PublishedICalUrl = new SimpleProviderPropertyDefinition("PublishedICalUrl", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400347D RID: 13437
		public static readonly SimpleProviderPropertyDefinition PublishedCalendarUrlCalculated = new SimpleProviderPropertyDefinition("PublishedCalendarUrlCalculated", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxCalendarFolderSchema.PublishedCalendarUrl
		}, null, (IPropertyBag propertyBag) => MailboxCalendarFolder.PublishedUrlGetter(propertyBag, MailboxCalendarFolderSchema.PublishedCalendarUrl), delegate(object value, IPropertyBag propertyBag)
		{
			MailboxCalendarFolder.PublishedUrlSetter(value, propertyBag, MailboxCalendarFolderSchema.PublishedCalendarUrl);
		});

		// Token: 0x0400347E RID: 13438
		public static readonly SimpleProviderPropertyDefinition PublishedICalUrlCalculated = new SimpleProviderPropertyDefinition("PublishedICalUrlCalculated", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailboxCalendarFolderSchema.PublishedICalUrl
		}, null, (IPropertyBag propertyBag) => MailboxCalendarFolder.PublishedUrlGetter(propertyBag, MailboxCalendarFolderSchema.PublishedICalUrl), delegate(object value, IPropertyBag propertyBag)
		{
			MailboxCalendarFolder.PublishedUrlSetter(value, propertyBag, MailboxCalendarFolderSchema.PublishedICalUrl);
		});
	}
}
