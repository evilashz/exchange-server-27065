using System;

namespace Microsoft.Exchange.TextMessaging.MobileDriver
{
	// Token: 0x0200003E RID: 62
	internal static class SharedComponents
	{
		// Token: 0x040000CD RID: 205
		public const string InternalDeliveryAgentSignature = "Agent:TextMessagingInternalDelivery-86DB88E6-E880-4564-B1EC-25C9797FEBBE";

		// Token: 0x040000CE RID: 206
		public const string ExternalDeliveryAgentSignature = "Agent:TextMessagingExternalDelivery-803AF8EC-8F1B-42b3-854D-8CA8E8CB04FC";

		// Token: 0x040000CF RID: 207
		public const string XheaderTextMessagingMapiClass = "X-MS-Exchange-Organization-Text-Messaging-Mapi-Class";

		// Token: 0x040000D0 RID: 208
		public const string XheaderTextMessagingOriginator = "X-MS-Exchange-Organization-Text-Messaging-Originator";

		// Token: 0x040000D1 RID: 209
		public const string XheaderTextMessagingCountOfSettingsSegments = "X-MS-Exchange-Organization-Text-Messaging-Count-Of-Settings-Segments";

		// Token: 0x040000D2 RID: 210
		public const string XheaderTextMessagingSettingsSegmentPrefix = "X-MS-Exchange-Organization-Text-Messaging-Settings-Segment-";

		// Token: 0x040000D3 RID: 211
		public const string XheaderTextMessagingTimestamp = "X-MS-Exchange-Organization-Text-Messaging-Timestamp";

		// Token: 0x040000D4 RID: 212
		public const string XheaderTextMessagingNotificationPreferredCulture = "X-MS-Exchange-Organization-Text-Messaging-Notification-PreferredCulture";

		// Token: 0x040000D5 RID: 213
		public const string TimestampFormat = "yyyyMMddhhmmssfff";

		// Token: 0x040000D6 RID: 214
		public const string IpmNoteMobileMms = "IPM.Note.Mobile.MMS";
	}
}
