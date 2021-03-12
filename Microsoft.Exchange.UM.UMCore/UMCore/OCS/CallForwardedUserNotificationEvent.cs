using System;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore.OCS
{
	// Token: 0x02000065 RID: 101
	internal class CallForwardedUserNotificationEvent : TeamPickUpUserNotificationEvent
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x0001570F File Offset: 0x0001390F
		internal CallForwardedUserNotificationEvent(string user, Guid recipientObjectGuid, Guid tenantGuid, string template, XmlNode eventData) : base(user, recipientObjectGuid, tenantGuid, template, eventData)
		{
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001571E File Offset: 0x0001391E
		protected override void InternalRenderMessage(MessageContentBuilder content, MessageItem message, ContactInfo callerInfo)
		{
			message.Subject = content.GetCallForwardedSubject(callerInfo, base.CallerId, base.Target, base.Subject);
			content.AddCallForwardedBody(base.Target, base.CallerId, callerInfo);
		}
	}
}
