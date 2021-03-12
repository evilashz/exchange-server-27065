using System;
using System.Xml;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore.OCS
{
	// Token: 0x02000069 RID: 105
	internal class CallNotForwardedUserNotificationEvent : UserNotificationEventWithTarget
	{
		// Token: 0x060004AB RID: 1195 RVA: 0x000171F5 File Offset: 0x000153F5
		internal CallNotForwardedUserNotificationEvent(string user, Guid recipientObjectGuid, Guid tenantGuid, string template, XmlNode eventData) : base(user, recipientObjectGuid, tenantGuid, eventData)
		{
		}

		// Token: 0x17000137 RID: 311
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00017202 File Offset: 0x00015402
		protected override string MessageClass
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00017209 File Offset: 0x00015409
		protected override void InternalRenderMessage(MessageContentBuilder content, MessageItem message, ContactInfo callerInfo)
		{
			message.Subject = content.GetCallNotForwardedSubject(callerInfo, base.CallerId, base.Target, base.Subject);
			content.AddCallNotForwardedBody(base.Target, base.CallerId, callerInfo);
		}
	}
}
