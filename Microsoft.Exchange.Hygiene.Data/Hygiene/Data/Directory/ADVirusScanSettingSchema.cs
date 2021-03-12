using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Hygiene.Data.Directory
{
	// Token: 0x020000C9 RID: 201
	internal class ADVirusScanSettingSchema : ADObjectSchema
	{
		// Token: 0x04000419 RID: 1049
		public static readonly HygienePropertyDefinition ConfigurationIdProp = new HygienePropertyDefinition("configId", typeof(ADObjectId));

		// Token: 0x0400041A RID: 1050
		public static readonly HygienePropertyDefinition FlagsProp = new HygienePropertyDefinition("flags", typeof(VirusScanFlags));

		// Token: 0x0400041B RID: 1051
		public static readonly HygienePropertyDefinition SenderWarningNotificationIdProp = new HygienePropertyDefinition("senderWarningNotificationId", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400041C RID: 1052
		public static readonly HygienePropertyDefinition SenderRejectionNotificationIdProp = new HygienePropertyDefinition("senderRejectionNotificationId", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400041D RID: 1053
		public static readonly HygienePropertyDefinition RecipientNotificationIdProp = new HygienePropertyDefinition("recipientNotificationId", typeof(int), 0, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400041E RID: 1054
		public static readonly HygienePropertyDefinition AdminNotificationAddressProp = new HygienePropertyDefinition("adminNotificationAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);

		// Token: 0x0400041F RID: 1055
		public static readonly HygienePropertyDefinition OutboundAdminNotificationAddressProp = new HygienePropertyDefinition("outboundAdminNotificationAddress", typeof(string), string.Empty, ADPropertyDefinitionFlags.PersistDefaultValue);
	}
}
