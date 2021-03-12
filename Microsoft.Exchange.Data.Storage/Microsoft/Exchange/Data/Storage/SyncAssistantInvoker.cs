using System;
using Microsoft.Exchange.Data.Storage.Principal;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Assistants;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DE6 RID: 3558
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class SyncAssistantInvoker
	{
		// Token: 0x06007A61 RID: 31329 RVA: 0x0021D23C File Offset: 0x0021B43C
		public static void SyncFolder(MailboxSession mailboxSession, StoreObjectId folderId)
		{
			Util.ThrowOnNullArgument(mailboxSession, "mailboxSession");
			Util.ThrowOnNullArgument(folderId, "folderId");
			using (AssistantsRpcClient assistantsRpcClient = new AssistantsRpcClient(mailboxSession.MailboxOwner.MailboxInfo.Location.ServerFqdn))
			{
				try
				{
					assistantsRpcClient.StartWithParams("CalendarSyncAssistant", mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid, mailboxSession.MailboxOwner.MailboxInfo.GetDatabaseGuid(), folderId.ToHexEntryId());
				}
				catch (RpcException arg)
				{
					ExTraceGlobals.SharingTracer.TraceDebug<IExchangePrincipal, StoreObjectId, RpcException>(0L, "{0}: SyncAssistantInvoker.SyncFolder for folder id {1} failed with exception {2}.", mailboxSession.MailboxOwner, folderId, arg);
				}
			}
		}

		// Token: 0x06007A62 RID: 31330 RVA: 0x0021D2FC File Offset: 0x0021B4FC
		public static bool MailboxServerSupportsSync(MailboxSession mailboxSession)
		{
			ServerVersion a = new ServerVersion(mailboxSession.MailboxOwner.MailboxInfo.Location.ServerVersion);
			return ServerVersion.Compare(a, SyncAssistantInvoker.OnlyCasSyncVersion) > 0;
		}

		// Token: 0x04005447 RID: 21575
		public const string CalendarSyncAssistant = "CalendarSyncAssistant";

		// Token: 0x04005448 RID: 21576
		private static readonly ServerVersion OnlyCasSyncVersion = new ServerVersion(14, 1, 138, 0);
	}
}
