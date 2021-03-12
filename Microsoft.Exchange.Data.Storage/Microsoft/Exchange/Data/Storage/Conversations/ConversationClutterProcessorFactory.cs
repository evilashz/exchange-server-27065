using System;
using Microsoft.Exchange.Data.Storage.Clutter;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Conversations
{
	// Token: 0x02000F6C RID: 3948
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ConversationClutterProcessorFactory
	{
		// Token: 0x060086F9 RID: 34553 RVA: 0x002502BC File Offset: 0x0024E4BC
		public static IConversationClutterProcessor Create(IStoreSession session)
		{
			return ConversationClutterProcessorFactory.testHook.Value(session);
		}

		// Token: 0x060086FA RID: 34554 RVA: 0x002502D0 File Offset: 0x0024E4D0
		private static IConversationClutterProcessor CreateInternal(IStoreSession session)
		{
			IConversationClutterProcessor result = null;
			MailboxSession mailboxSession = session as MailboxSession;
			if (mailboxSession != null && ClutterUtilities.IsClutterEnabled(mailboxSession, mailboxSession.MailboxOwner.GetConfiguration()))
			{
				result = new FolderBasedConversationClutterProcessor(mailboxSession);
			}
			return result;
		}

		// Token: 0x04005A3A RID: 23098
		internal static readonly Hookable<Func<IStoreSession, IConversationClutterProcessor>> testHook = Hookable<Func<IStoreSession, IConversationClutterProcessor>>.Create(true, new Func<IStoreSession, IConversationClutterProcessor>(ConversationClutterProcessorFactory.CreateInternal));
	}
}
