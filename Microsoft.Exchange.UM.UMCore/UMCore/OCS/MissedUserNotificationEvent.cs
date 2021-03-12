using System;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore.OCS
{
	// Token: 0x02000188 RID: 392
	internal class MissedUserNotificationEvent : UserNotificationEvent
	{
		// Token: 0x06000B96 RID: 2966 RVA: 0x000323B8 File Offset: 0x000305B8
		internal MissedUserNotificationEvent(string user, Guid recipientObjectGuid, Guid tenantGuid, XmlNode eventData) : base(user, recipientObjectGuid, tenantGuid, eventData)
		{
		}

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000B97 RID: 2967 RVA: 0x000323C5 File Offset: 0x000305C5
		protected override string MessageClass
		{
			get
			{
				return "IPM.Note.Microsoft.Missed.Voice";
			}
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x000323CC File Offset: 0x000305CC
		protected override void InternalRenderMessage(MessageContentBuilder content, MessageItem message, ContactInfo callerInfo)
		{
			message.Subject = content.GetMissedCallSubject(base.Subject);
			content.AddMissedCallBody(base.CallerId, callerInfo);
		}
	}
}
