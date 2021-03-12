using System;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore.OCS
{
	// Token: 0x02000064 RID: 100
	internal class TeamPickUpUserNotificationEvent : UserNotificationEventWithTarget
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x000156CE File Offset: 0x000138CE
		internal TeamPickUpUserNotificationEvent(string user, Guid recipientObjectGuid, Guid tenantGuid, string template, XmlNode eventData) : base(user, recipientObjectGuid, tenantGuid, eventData)
		{
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x000156DB File Offset: 0x000138DB
		protected override void InternalRenderMessage(MessageContentBuilder content, MessageItem message, ContactInfo callerInfo)
		{
			message.Subject = content.GetTeamPickUpSubject(callerInfo, base.CallerId, base.Target, base.Subject);
			content.AddTeamPickUpBody(base.Target, base.CallerId, callerInfo);
		}
	}
}
