using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A0D RID: 2573
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailboxRegionalConfigurationSchema : UserConfigurationObjectSchema
	{
		// Token: 0x040034A0 RID: 13472
		public static readonly SimplePropertyDefinition RawDateFormat = new SimplePropertyDefinition("rawdateformat", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040034A1 RID: 13473
		public static readonly SimplePropertyDefinition RawTimeFormat = new SimplePropertyDefinition("rawtimeformat", ExchangeObjectVersion.Exchange2007, typeof(string), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040034A2 RID: 13474
		public static readonly SimplePropertyDefinition DefaultFolderNameMatchingUserLanguage = new SimplePropertyDefinition("defaultFolderNameMatchingUserLanguage", ExchangeObjectVersion.Exchange2010, typeof(bool), PropertyDefinitionFlags.None, false, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040034A3 RID: 13475
		public static readonly SimplePropertyDefinition TimeZone = new SimplePropertyDefinition("timezone", ExchangeObjectVersion.Exchange2010, typeof(ExTimeZoneValue), PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x040034A4 RID: 13476
		public static readonly SimplePropertyDefinition Language = new SimplePropertyDefinition("language", ExchangeObjectVersion.Exchange2010, typeof(CultureInfo), PropertyDefinitionFlags.None, null, null, new PropertyDefinitionConstraint[]
		{
			new DelegateConstraint(new ValidationDelegate(ConstraintDelegates.ValidateNonNeutralCulture))
		}, new PropertyDefinitionConstraint[]
		{
			new DelegateConstraint(new ValidationDelegate(ConstraintDelegates.ValidateNonNeutralCulture))
		});

		// Token: 0x040034A5 RID: 13477
		public static readonly SimplePropertyDefinition DateFormat = new SimplePropertyDefinition("dateformat", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.Calculated, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new SimplePropertyDefinition[]
		{
			MailboxRegionalConfigurationSchema.RawDateFormat,
			MailboxRegionalConfigurationSchema.Language
		}, null, new GetterDelegate(MailboxRegionalConfiguration.DateFormatGetter), new SetterDelegate(MailboxRegionalConfiguration.DateFormatSetter));

		// Token: 0x040034A6 RID: 13478
		public static readonly SimplePropertyDefinition TimeFormat = new SimplePropertyDefinition("timeformat", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.Calculated, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new SimplePropertyDefinition[]
		{
			MailboxRegionalConfigurationSchema.RawTimeFormat,
			MailboxRegionalConfigurationSchema.Language
		}, null, new GetterDelegate(MailboxRegionalConfiguration.TimeFormatGetter), new SetterDelegate(MailboxRegionalConfiguration.TimeFormatSetter));
	}
}
