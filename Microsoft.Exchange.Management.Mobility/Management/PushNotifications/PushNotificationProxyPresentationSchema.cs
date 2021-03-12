using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x02000052 RID: 82
	internal sealed class PushNotificationProxyPresentationSchema : ADPresentationSchema
	{
		// Token: 0x0600036D RID: 877 RVA: 0x0000E013 File Offset: 0x0000C213
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<PushNotificationAppSchema>();
		}

		// Token: 0x040000F4 RID: 244
		public static readonly ADPropertyDefinition DisplayName = PushNotificationAppSchema.DisplayName;

		// Token: 0x040000F5 RID: 245
		public static readonly ADPropertyDefinition Enabled = PushNotificationAppSchema.Enabled;

		// Token: 0x040000F6 RID: 246
		public static readonly ADPropertyDefinition Organization = PushNotificationAppSchema.AuthenticationKey;

		// Token: 0x040000F7 RID: 247
		public static readonly ADPropertyDefinition Uri = PushNotificationAppSchema.Url;
	}
}
