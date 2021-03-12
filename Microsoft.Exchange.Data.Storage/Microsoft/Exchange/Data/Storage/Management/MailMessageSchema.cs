using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Management
{
	// Token: 0x02000A74 RID: 2676
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class MailMessageSchema : XsoMailboxConfigurationObjectSchema
	{
		// Token: 0x04003784 RID: 14212
		public static readonly XsoDriverPropertyDefinition Subject = new XsoDriverPropertyDefinition(ItemSchema.Subject, "Subject", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, string.Empty, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003785 RID: 14213
		public static readonly XsoDriverPropertyDefinition RawFrom = new XsoDriverPropertyDefinition(ItemSchema.From, "From", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003786 RID: 14214
		public static readonly XsoDriverPropertyDefinition RawSender = new XsoDriverPropertyDefinition(ItemSchema.Sender, "Sender", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003787 RID: 14215
		public static readonly XsoDriverPropertyDefinition InternalMessageIdentity = new XsoDriverPropertyDefinition(ItemSchema.Id, "InternalMessageIdentity", ExchangeObjectVersion.Exchange2003, PropertyDefinitionFlags.None, null, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003788 RID: 14216
		public static readonly SimpleProviderPropertyDefinition Bcc = new SimpleProviderPropertyDefinition("Bcc", ExchangeObjectVersion.Exchange2003, typeof(ADRecipientOrAddress[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x04003789 RID: 14217
		public static readonly SimpleProviderPropertyDefinition Cc = new SimpleProviderPropertyDefinition("Cc", ExchangeObjectVersion.Exchange2003, typeof(ADRecipientOrAddress[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);

		// Token: 0x0400378A RID: 14218
		public static readonly SimpleProviderPropertyDefinition Identity = new SimpleProviderPropertyDefinition("Identity", ExchangeObjectVersion.Exchange2003, typeof(MailboxStoreObjectId), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated | PropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailMessageSchema.InternalMessageIdentity,
			XsoMailboxConfigurationObjectSchema.MailboxOwnerId
		}, null, new GetterDelegate(MailMessage.IdentityGetter), null);

		// Token: 0x0400378B RID: 14219
		public static readonly SimpleProviderPropertyDefinition From = new SimpleProviderPropertyDefinition("From", ExchangeObjectVersion.Exchange2003, typeof(ADRecipientOrAddress), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailMessageSchema.RawFrom
		}, null, new GetterDelegate(MailMessage.FromGetter), null);

		// Token: 0x0400378C RID: 14220
		public static readonly SimpleProviderPropertyDefinition Sender = new SimpleProviderPropertyDefinition("Sender", ExchangeObjectVersion.Exchange2003, typeof(ADRecipientOrAddress), PropertyDefinitionFlags.ReadOnly | PropertyDefinitionFlags.Calculated, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, new ProviderPropertyDefinition[]
		{
			MailMessageSchema.RawSender
		}, null, new GetterDelegate(MailMessage.SenderGetter), null);

		// Token: 0x0400378D RID: 14221
		public static readonly SimpleProviderPropertyDefinition To = new SimpleProviderPropertyDefinition("To", ExchangeObjectVersion.Exchange2003, typeof(ADRecipientOrAddress[]), PropertyDefinitionFlags.None, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None);
	}
}
