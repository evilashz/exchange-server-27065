using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000C8 RID: 200
	internal class ADUserSettingSchema : ADObjectSchema
	{
		// Token: 0x04000412 RID: 1042
		public static readonly HygienePropertyDefinition SettingIdProp = new HygienePropertyDefinition("settingsId", typeof(ADObjectId));

		// Token: 0x04000413 RID: 1043
		public static readonly HygienePropertyDefinition ConfigurationIdProp = new HygienePropertyDefinition("configId", typeof(ADObjectId));

		// Token: 0x04000414 RID: 1044
		public static readonly ADPropertyDefinition SafeSendersProp = new HygienePropertyDefinition(ADRecipientSchema.SafeSendersHash.Name, ADRecipientSchema.SafeSendersHash.Type);

		// Token: 0x04000415 RID: 1045
		public static readonly ADPropertyDefinition BlockedSendersProp = new HygienePropertyDefinition(ADRecipientSchema.BlockedSendersHash.Name, ADRecipientSchema.BlockedSendersHash.Type);

		// Token: 0x04000416 RID: 1046
		public static readonly HygienePropertyDefinition DirectionIdProp = new HygienePropertyDefinition("directionId", typeof(MailDirection), MailDirection.Inbound, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000417 RID: 1047
		public static readonly HygienePropertyDefinition FlagsProp = new HygienePropertyDefinition("flags", typeof(UserSettingFlags), UserSettingFlags.None, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x04000418 RID: 1048
		public static readonly ADPropertyDefinition DisplayNameProp = new HygienePropertyDefinition(ADRecipientSchema.DisplayName.Name, ADRecipientSchema.DisplayName.Type);
	}
}
