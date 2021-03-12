using System;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000178 RID: 376
	internal static class HierachyNotificationHandlerFactory
	{
		// Token: 0x06000DC6 RID: 3526 RVA: 0x0003411C File Offset: 0x0003231C
		public static HierarchyNotificationHandler CreateHandler(string subscriptionId, UserContext userContext)
		{
			if (userContext.FeaturesManager.ServerSettings.InferenceUI.Enabled && userContext.MailboxSession.Mailbox != null && userContext.MailboxSession.Mailbox.GetValueOrDefault<bool>(MailboxSchema.InferenceUserUIReady, false))
			{
				return new InferenceHierarchyNotificationHandler(subscriptionId, userContext, userContext.MailboxSession.MailboxGuid);
			}
			return new HierarchyNotificationHandler(subscriptionId, userContext, userContext.MailboxSession.MailboxGuid);
		}
	}
}
