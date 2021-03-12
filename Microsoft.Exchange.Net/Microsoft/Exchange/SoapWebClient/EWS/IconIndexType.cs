using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x0200026A RID: 618
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public enum IconIndexType
	{
		// Token: 0x04000FF1 RID: 4081
		Default,
		// Token: 0x04000FF2 RID: 4082
		PostItem,
		// Token: 0x04000FF3 RID: 4083
		MailRead,
		// Token: 0x04000FF4 RID: 4084
		MailUnread,
		// Token: 0x04000FF5 RID: 4085
		MailReplied,
		// Token: 0x04000FF6 RID: 4086
		MailForwarded,
		// Token: 0x04000FF7 RID: 4087
		MailEncrypted,
		// Token: 0x04000FF8 RID: 4088
		MailSmimeSigned,
		// Token: 0x04000FF9 RID: 4089
		MailEncryptedReplied,
		// Token: 0x04000FFA RID: 4090
		MailSmimeSignedReplied,
		// Token: 0x04000FFB RID: 4091
		MailEncryptedForwarded,
		// Token: 0x04000FFC RID: 4092
		MailSmimeSignedForwarded,
		// Token: 0x04000FFD RID: 4093
		MailEncryptedRead,
		// Token: 0x04000FFE RID: 4094
		MailSmimeSignedRead,
		// Token: 0x04000FFF RID: 4095
		MailIrm,
		// Token: 0x04001000 RID: 4096
		MailIrmForwarded,
		// Token: 0x04001001 RID: 4097
		MailIrmReplied,
		// Token: 0x04001002 RID: 4098
		SmsSubmitted,
		// Token: 0x04001003 RID: 4099
		SmsRoutedToDeliveryPoint,
		// Token: 0x04001004 RID: 4100
		SmsRoutedToExternalMessagingSystem,
		// Token: 0x04001005 RID: 4101
		SmsDelivered,
		// Token: 0x04001006 RID: 4102
		OutlookDefaultForContacts,
		// Token: 0x04001007 RID: 4103
		AppointmentItem,
		// Token: 0x04001008 RID: 4104
		AppointmentRecur,
		// Token: 0x04001009 RID: 4105
		AppointmentMeet,
		// Token: 0x0400100A RID: 4106
		AppointmentMeetRecur,
		// Token: 0x0400100B RID: 4107
		AppointmentMeetNY,
		// Token: 0x0400100C RID: 4108
		AppointmentMeetYes,
		// Token: 0x0400100D RID: 4109
		AppointmentMeetNo,
		// Token: 0x0400100E RID: 4110
		AppointmentMeetMaybe,
		// Token: 0x0400100F RID: 4111
		AppointmentMeetCancel,
		// Token: 0x04001010 RID: 4112
		AppointmentMeetInfo,
		// Token: 0x04001011 RID: 4113
		TaskItem,
		// Token: 0x04001012 RID: 4114
		TaskRecur,
		// Token: 0x04001013 RID: 4115
		TaskOwned,
		// Token: 0x04001014 RID: 4116
		TaskDelegated
	}
}
