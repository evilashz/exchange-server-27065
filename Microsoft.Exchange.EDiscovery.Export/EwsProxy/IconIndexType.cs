using System;
using System.CodeDom.Compiler;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000189 RID: 393
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public enum IconIndexType
	{
		// Token: 0x04000B9F RID: 2975
		Default,
		// Token: 0x04000BA0 RID: 2976
		PostItem,
		// Token: 0x04000BA1 RID: 2977
		MailRead,
		// Token: 0x04000BA2 RID: 2978
		MailUnread,
		// Token: 0x04000BA3 RID: 2979
		MailReplied,
		// Token: 0x04000BA4 RID: 2980
		MailForwarded,
		// Token: 0x04000BA5 RID: 2981
		MailEncrypted,
		// Token: 0x04000BA6 RID: 2982
		MailSmimeSigned,
		// Token: 0x04000BA7 RID: 2983
		MailEncryptedReplied,
		// Token: 0x04000BA8 RID: 2984
		MailSmimeSignedReplied,
		// Token: 0x04000BA9 RID: 2985
		MailEncryptedForwarded,
		// Token: 0x04000BAA RID: 2986
		MailSmimeSignedForwarded,
		// Token: 0x04000BAB RID: 2987
		MailEncryptedRead,
		// Token: 0x04000BAC RID: 2988
		MailSmimeSignedRead,
		// Token: 0x04000BAD RID: 2989
		MailIrm,
		// Token: 0x04000BAE RID: 2990
		MailIrmForwarded,
		// Token: 0x04000BAF RID: 2991
		MailIrmReplied,
		// Token: 0x04000BB0 RID: 2992
		SmsSubmitted,
		// Token: 0x04000BB1 RID: 2993
		SmsRoutedToDeliveryPoint,
		// Token: 0x04000BB2 RID: 2994
		SmsRoutedToExternalMessagingSystem,
		// Token: 0x04000BB3 RID: 2995
		SmsDelivered,
		// Token: 0x04000BB4 RID: 2996
		OutlookDefaultForContacts,
		// Token: 0x04000BB5 RID: 2997
		AppointmentItem,
		// Token: 0x04000BB6 RID: 2998
		AppointmentRecur,
		// Token: 0x04000BB7 RID: 2999
		AppointmentMeet,
		// Token: 0x04000BB8 RID: 3000
		AppointmentMeetRecur,
		// Token: 0x04000BB9 RID: 3001
		AppointmentMeetNY,
		// Token: 0x04000BBA RID: 3002
		AppointmentMeetYes,
		// Token: 0x04000BBB RID: 3003
		AppointmentMeetNo,
		// Token: 0x04000BBC RID: 3004
		AppointmentMeetMaybe,
		// Token: 0x04000BBD RID: 3005
		AppointmentMeetCancel,
		// Token: 0x04000BBE RID: 3006
		AppointmentMeetInfo,
		// Token: 0x04000BBF RID: 3007
		TaskItem,
		// Token: 0x04000BC0 RID: 3008
		TaskRecur,
		// Token: 0x04000BC1 RID: 3009
		TaskOwned,
		// Token: 0x04000BC2 RID: 3010
		TaskDelegated
	}
}
