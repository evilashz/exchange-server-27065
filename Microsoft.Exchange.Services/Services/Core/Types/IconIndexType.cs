using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000793 RID: 1939
	[XmlType(TypeName = "IconIndexType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum IconIndexType
	{
		// Token: 0x04002025 RID: 8229
		Default = -1,
		// Token: 0x04002026 RID: 8230
		PostItem = 1,
		// Token: 0x04002027 RID: 8231
		MailRead = 256,
		// Token: 0x04002028 RID: 8232
		MailUnread,
		// Token: 0x04002029 RID: 8233
		MailReplied = 261,
		// Token: 0x0400202A RID: 8234
		MailForwarded,
		// Token: 0x0400202B RID: 8235
		MailEncrypted = 272,
		// Token: 0x0400202C RID: 8236
		MailSmimeSigned,
		// Token: 0x0400202D RID: 8237
		MailEncryptedReplied = 275,
		// Token: 0x0400202E RID: 8238
		MailSmimeSignedReplied,
		// Token: 0x0400202F RID: 8239
		MailEncryptedForwarded,
		// Token: 0x04002030 RID: 8240
		MailSmimeSignedForwarded,
		// Token: 0x04002031 RID: 8241
		MailEncryptedRead,
		// Token: 0x04002032 RID: 8242
		MailSmimeSignedRead,
		// Token: 0x04002033 RID: 8243
		MailIrm = 306,
		// Token: 0x04002034 RID: 8244
		MailIrmForwarded,
		// Token: 0x04002035 RID: 8245
		MailIrmReplied,
		// Token: 0x04002036 RID: 8246
		SmsSubmitted = 336,
		// Token: 0x04002037 RID: 8247
		SmsRoutedToDeliveryPoint,
		// Token: 0x04002038 RID: 8248
		SmsRoutedToExternalMessagingSystem,
		// Token: 0x04002039 RID: 8249
		SmsDelivered,
		// Token: 0x0400203A RID: 8250
		OutlookDefaultForContacts = 512,
		// Token: 0x0400203B RID: 8251
		AppointmentItem = 1024,
		// Token: 0x0400203C RID: 8252
		AppointmentRecur,
		// Token: 0x0400203D RID: 8253
		AppointmentMeet,
		// Token: 0x0400203E RID: 8254
		AppointmentMeetRecur,
		// Token: 0x0400203F RID: 8255
		AppointmentMeetNY,
		// Token: 0x04002040 RID: 8256
		AppointmentMeetYes,
		// Token: 0x04002041 RID: 8257
		AppointmentMeetNo,
		// Token: 0x04002042 RID: 8258
		AppointmentMeetMaybe,
		// Token: 0x04002043 RID: 8259
		AppointmentMeetCancel,
		// Token: 0x04002044 RID: 8260
		AppointmentMeetInfo,
		// Token: 0x04002045 RID: 8261
		TaskItem = 1280,
		// Token: 0x04002046 RID: 8262
		TaskRecur,
		// Token: 0x04002047 RID: 8263
		TaskOwned,
		// Token: 0x04002048 RID: 8264
		TaskDelegated
	}
}
