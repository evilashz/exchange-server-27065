using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001FE RID: 510
	internal enum IconIndex
	{
		// Token: 0x04000E65 RID: 3685
		Default = -1,
		// Token: 0x04000E66 RID: 3686
		PostItem = 1,
		// Token: 0x04000E67 RID: 3687
		BaseMail = 256,
		// Token: 0x04000E68 RID: 3688
		MailRead = 256,
		// Token: 0x04000E69 RID: 3689
		MailUnread,
		// Token: 0x04000E6A RID: 3690
		MailReplied = 261,
		// Token: 0x04000E6B RID: 3691
		MailForwarded,
		// Token: 0x04000E6C RID: 3692
		MailEncrypted = 272,
		// Token: 0x04000E6D RID: 3693
		MailSmimeSigned,
		// Token: 0x04000E6E RID: 3694
		MailEncryptedReplied = 275,
		// Token: 0x04000E6F RID: 3695
		MailSmimeSignedReplied,
		// Token: 0x04000E70 RID: 3696
		MailEncryptedForwarded,
		// Token: 0x04000E71 RID: 3697
		MailSmimeSignedForwarded,
		// Token: 0x04000E72 RID: 3698
		MailEncryptedRead,
		// Token: 0x04000E73 RID: 3699
		MailSmimeSignedRead,
		// Token: 0x04000E74 RID: 3700
		MailIrm = 306,
		// Token: 0x04000E75 RID: 3701
		MailIrmForwarded,
		// Token: 0x04000E76 RID: 3702
		MailIrmReplied,
		// Token: 0x04000E77 RID: 3703
		BaseSmsDeliveryStatus = 336,
		// Token: 0x04000E78 RID: 3704
		SmsSubmitted = 336,
		// Token: 0x04000E79 RID: 3705
		SmsRoutedToDeliveryPoint,
		// Token: 0x04000E7A RID: 3706
		SmsRoutedToExternalMessagingSystem,
		// Token: 0x04000E7B RID: 3707
		SmsDelivered,
		// Token: 0x04000E7C RID: 3708
		LastSmsDeliveryStatus = 339,
		// Token: 0x04000E7D RID: 3709
		OutlookDefaultForContacts = 512,
		// Token: 0x04000E7E RID: 3710
		BaseAppointment = 1024,
		// Token: 0x04000E7F RID: 3711
		AppointmentItem = 1024,
		// Token: 0x04000E80 RID: 3712
		AppointmentRecur,
		// Token: 0x04000E81 RID: 3713
		AppointmentMeet,
		// Token: 0x04000E82 RID: 3714
		AppointmentMeetRecur,
		// Token: 0x04000E83 RID: 3715
		AppointmentMeetNY,
		// Token: 0x04000E84 RID: 3716
		AppointmentMeetYes,
		// Token: 0x04000E85 RID: 3717
		AppointmentMeetNo,
		// Token: 0x04000E86 RID: 3718
		AppointmentMeetMaybe,
		// Token: 0x04000E87 RID: 3719
		AppointmentMeetCancel,
		// Token: 0x04000E88 RID: 3720
		AppointmentMeetInfo,
		// Token: 0x04000E89 RID: 3721
		BaseTask = 1280,
		// Token: 0x04000E8A RID: 3722
		TaskItem = 1280,
		// Token: 0x04000E8B RID: 3723
		TaskRecur,
		// Token: 0x04000E8C RID: 3724
		TaskOwned,
		// Token: 0x04000E8D RID: 3725
		TaskDelagated
	}
}
