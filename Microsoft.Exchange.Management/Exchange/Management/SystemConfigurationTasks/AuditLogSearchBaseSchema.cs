using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000092 RID: 146
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AuditLogSearchBaseSchema : ObjectSchema
	{
		// Token: 0x0400025A RID: 602
		public static readonly ProviderPropertyDefinition ObjectState = UserConfigurationObjectSchema.ObjectState;

		// Token: 0x0400025B RID: 603
		public static readonly ProviderPropertyDefinition ExchangeVersion = UserConfigurationObjectSchema.ExchangeVersion;

		// Token: 0x0400025C RID: 604
		public static readonly ProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2010, typeof(AuditLogSearchId), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400025D RID: 605
		public static readonly ProviderPropertyDefinition Name = new SimpleProviderPropertyDefinition("Name", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.None, string.Empty, PropertyDefinitionConstraint.None, new PropertyDefinitionConstraint[]
		{
			new StringLengthConstraint(0, 255)
		});

		// Token: 0x0400025E RID: 606
		public static readonly ProviderPropertyDefinition StartDateUtc = new SimpleProviderPropertyDefinition("StartDateUtc", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400025F RID: 607
		public static readonly ProviderPropertyDefinition EndDateUtc = new SimpleProviderPropertyDefinition("EndDateUtc", ExchangeObjectVersion.Exchange2010, typeof(DateTime?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000260 RID: 608
		public static readonly ProviderPropertyDefinition StatusMailRecipients = new SimpleProviderPropertyDefinition("StatusMailRecipients", ExchangeObjectVersion.Exchange2010, typeof(SmtpAddress), PropertyDefinitionFlags.MultiValued, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000261 RID: 609
		public static readonly ProviderPropertyDefinition CreatedByEx = new SimpleProviderPropertyDefinition("CreatedByEx", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), PropertyDefinitionFlags.TaskPopulated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000262 RID: 610
		public static readonly ProviderPropertyDefinition CreatedBy = new SimpleProviderPropertyDefinition("CreatedBy", ExchangeObjectVersion.Exchange2010, typeof(string), PropertyDefinitionFlags.TaskPopulated, string.Empty, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04000263 RID: 611
		public static readonly ProviderPropertyDefinition ExternalAccess = new SimpleProviderPropertyDefinition("ExternalAccess", ExchangeObjectVersion.Exchange2010, typeof(bool?), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
