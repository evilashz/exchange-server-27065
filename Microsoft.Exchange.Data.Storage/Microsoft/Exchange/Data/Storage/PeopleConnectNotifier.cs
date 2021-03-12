using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000516 RID: 1302
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PeopleConnectNotifier
	{
		// Token: 0x060037EE RID: 14318 RVA: 0x000E2348 File Offset: 0x000E0548
		public PeopleConnectNotifier(MailboxSession session)
		{
			Util.ThrowOnNullArgument(session, "session");
			this.session = session;
		}

		// Token: 0x060037EF RID: 14319 RVA: 0x000E2362 File Offset: 0x000E0562
		public void NotifyConnected(string provider)
		{
			Util.ThrowOnNullOrEmptyArgument(provider, "provider");
			this.Notify(provider, "IPM.Note.PeopleConnect.Notification.Connected");
		}

		// Token: 0x060037F0 RID: 14320 RVA: 0x000E237B File Offset: 0x000E057B
		public void NotifyDisconnected(string provider)
		{
			Util.ThrowOnNullOrEmptyArgument(provider, "provider");
			this.Notify(provider, "IPM.Note.PeopleConnect.Notification.Disconnected");
		}

		// Token: 0x060037F1 RID: 14321 RVA: 0x000E2394 File Offset: 0x000E0594
		public void NotifyNewTokenNeeded(string provider)
		{
			Util.ThrowOnNullOrEmptyArgument(provider, "provider");
			this.Notify(provider, "IPM.Note.PeopleConnect.Notification.NewTokenNeeded");
		}

		// Token: 0x060037F2 RID: 14322 RVA: 0x000E23AD File Offset: 0x000E05AD
		public void NotifyInitialSyncCompleted(string provider)
		{
			Util.ThrowOnNullOrEmptyArgument(provider, "provider");
			this.Notify(provider, "IPM.Note.PeopleConnect.Notification.InitialSyncCompleted");
		}

		// Token: 0x060037F3 RID: 14323 RVA: 0x000E23C8 File Offset: 0x000E05C8
		private void Notify(string provider, string connectionStatus)
		{
			StoreObjectId defaultFolderId = this.session.GetDefaultFolderId(DefaultFolderType.PeopleConnect);
			if (defaultFolderId == null)
			{
				return;
			}
			using (MessageItem messageItem = MessageItem.Create(this.session, defaultFolderId))
			{
				messageItem.ClassName = connectionStatus;
				messageItem.Subject = provider;
				messageItem.Save(SaveMode.ResolveConflicts);
			}
		}

		// Token: 0x04001DCC RID: 7628
		private readonly MailboxSession session;
	}
}
