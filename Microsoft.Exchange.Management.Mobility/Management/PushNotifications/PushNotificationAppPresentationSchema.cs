using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.PushNotifications
{
	// Token: 0x02000050 RID: 80
	internal sealed class PushNotificationAppPresentationSchema : ADPresentationSchema
	{
		// Token: 0x06000346 RID: 838 RVA: 0x0000D687 File Offset: 0x0000B887
		internal override ADObjectSchema GetParentSchema()
		{
			return ObjectSchema.GetInstance<PushNotificationAppSchema>();
		}

		// Token: 0x040000D2 RID: 210
		public static readonly ADPropertyDefinition DisplayName = PushNotificationAppSchema.DisplayName;

		// Token: 0x040000D3 RID: 211
		public static readonly ADPropertyDefinition Platform = PushNotificationAppSchema.Platform;

		// Token: 0x040000D4 RID: 212
		public static readonly ADPropertyDefinition Enabled = PushNotificationAppSchema.Enabled;

		// Token: 0x040000D5 RID: 213
		public static readonly ADPropertyDefinition ExchangeMaximumVersion = PushNotificationAppSchema.ExchangeMaximumVersion;

		// Token: 0x040000D6 RID: 214
		public static readonly ADPropertyDefinition ExchangeMinimumVersion = PushNotificationAppSchema.ExchangeMinimumVersion;

		// Token: 0x040000D7 RID: 215
		public static readonly ADPropertyDefinition QueueSize = PushNotificationAppSchema.QueueSize;

		// Token: 0x040000D8 RID: 216
		public static readonly ADPropertyDefinition NumberOfChannels = PushNotificationAppSchema.NumberOfChannels;

		// Token: 0x040000D9 RID: 217
		public static readonly ADPropertyDefinition BackOffTimeInSeconds = PushNotificationAppSchema.BackOffTimeInSeconds;

		// Token: 0x040000DA RID: 218
		public static readonly ADPropertyDefinition AuthenticationId = PushNotificationAppSchema.AuthenticationId;

		// Token: 0x040000DB RID: 219
		public static readonly ADPropertyDefinition AuthenticationKey = PushNotificationAppSchema.AuthenticationKey;

		// Token: 0x040000DC RID: 220
		public static readonly ADPropertyDefinition IsAuthenticationKeyEncrypted = PushNotificationAppSchema.IsAuthenticationKeyEncrypted;

		// Token: 0x040000DD RID: 221
		public static readonly ADPropertyDefinition AuthenticationKeyFallback = PushNotificationAppSchema.AuthenticationKeyFallback;

		// Token: 0x040000DE RID: 222
		public static readonly ADPropertyDefinition UriTemplate = PushNotificationAppSchema.UriTemplate;

		// Token: 0x040000DF RID: 223
		public static readonly ADPropertyDefinition Url = PushNotificationAppSchema.Url;

		// Token: 0x040000E0 RID: 224
		public static readonly ADPropertyDefinition Port = PushNotificationAppSchema.Port;

		// Token: 0x040000E1 RID: 225
		public static readonly ADPropertyDefinition RegitrationEnabled = PushNotificationAppSchema.RegistrationEnabled;

		// Token: 0x040000E2 RID: 226
		public static readonly ADPropertyDefinition MultifactorRegistrationEnabled = PushNotificationAppSchema.MultifactorRegistrationEnabled;

		// Token: 0x040000E3 RID: 227
		public static readonly ADPropertyDefinition RegistrationTemplate = PushNotificationAppSchema.RegistrationTemplate;

		// Token: 0x040000E4 RID: 228
		public static readonly ADPropertyDefinition PartitionName = PushNotificationAppSchema.PartitionName;

		// Token: 0x040000E5 RID: 229
		public static readonly ADPropertyDefinition IsDefaultPartitionName = PushNotificationAppSchema.IsDefaultPartitionName;

		// Token: 0x040000E6 RID: 230
		public static readonly ADPropertyDefinition SecondaryUrl = PushNotificationAppSchema.SecondaryUrl;

		// Token: 0x040000E7 RID: 231
		public static readonly ADPropertyDefinition SecondaryPort = PushNotificationAppSchema.SecondaryPort;
	}
}
