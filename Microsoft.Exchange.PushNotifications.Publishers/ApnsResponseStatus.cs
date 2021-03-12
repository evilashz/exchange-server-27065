using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200003B RID: 59
	internal enum ApnsResponseStatus
	{
		// Token: 0x040000E7 RID: 231
		None,
		// Token: 0x040000E8 RID: 232
		ProcessingError,
		// Token: 0x040000E9 RID: 233
		MissingDeviceToken,
		// Token: 0x040000EA RID: 234
		MissingTopic,
		// Token: 0x040000EB RID: 235
		MissingPayload,
		// Token: 0x040000EC RID: 236
		InvalidTokenSize,
		// Token: 0x040000ED RID: 237
		InvalidTopicSize,
		// Token: 0x040000EE RID: 238
		InvalidPayloadSize,
		// Token: 0x040000EF RID: 239
		InvalidToken,
		// Token: 0x040000F0 RID: 240
		Unknown = 255
	}
}
