using System;
using System.Xml;

namespace Microsoft.Exchange.UM.UMCore.OCS
{
	// Token: 0x02000063 RID: 99
	internal abstract class UserNotificationEventWithTarget : UserNotificationEvent
	{
		// Token: 0x0600046E RID: 1134 RVA: 0x0001569A File Offset: 0x0001389A
		internal UserNotificationEventWithTarget(string user, Guid recipientObjectGuid, Guid tenantGuid, XmlNode eventData) : base(user, recipientObjectGuid, tenantGuid, eventData)
		{
			this.target = base.FindTarget(UserNotificationEvent.GetNodeValue(eventData, "Target"));
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x000156BF File Offset: 0x000138BF
		protected string Target
		{
			get
			{
				return this.target;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x000156C7 File Offset: 0x000138C7
		protected override string MessageClass
		{
			get
			{
				return "IPM.Note.Microsoft.Conversation.Voice";
			}
		}

		// Token: 0x04000198 RID: 408
		private string target;
	}
}
