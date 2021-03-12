using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Data.Storage.StoreConfigurableType;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net.JobQueues;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SiteMailbox
{
	// Token: 0x0200022E RID: 558
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SiteMailboxAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x06001500 RID: 5376 RVA: 0x00078429 File Offset: 0x00076629
		public SiteMailboxAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x06001501 RID: 5377 RVA: 0x00078434 File Offset: 0x00076634
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00078438 File Offset: 0x00076638
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
			MailboxSession mailboxSession = invokeArgs.StoreSession as MailboxSession;
			if (mailboxSession == null)
			{
				return;
			}
			LoggingContext loggingContext = null;
			try
			{
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					loggingContext = new LoggingContext(mailboxSession.MailboxGuid, mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), string.Empty, null);
					UserConfiguration mailboxConfiguration = UserConfigurationHelper.GetMailboxConfiguration(mailboxSession, "SiteMailboxAssistantCycleLog", UserConfigurationTypes.Stream, true);
					disposeGuard.Add<UserConfiguration>(mailboxConfiguration);
					Stream stream = mailboxConfiguration.GetStream();
					disposeGuard.Add<Stream>(stream);
					UserConfiguration mailboxConfiguration2 = UserConfigurationHelper.GetMailboxConfiguration(mailboxSession, "SiteMailboxAssistantConfigurations", UserConfigurationTypes.Dictionary, true);
					disposeGuard.Add<UserConfiguration>(mailboxConfiguration2);
					loggingContext.LoggingStream = stream;
					SiteMailboxAssistant.EnqueueTeamMailboxSyncRequest(mailboxSession, QueueType.TeamMailboxMaintenanceSync, loggingContext);
					SiteMailboxAssistant.EnqueueTeamMailboxSyncRequest(mailboxSession, QueueType.TeamMailboxMembershipSync, loggingContext);
					SiteMailboxAssistant.EnqueueTeamMailboxSyncRequest(mailboxSession, QueueType.TeamMailboxDocumentSync, loggingContext);
				}
			}
			catch (StoragePermanentException exception)
			{
				ProtocolLog.LogError(ProtocolLog.Component.Assistant, loggingContext, string.Format("Failed with StoragePermanentException for site mailbox:{0}", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()), exception);
			}
			catch (StorageTransientException exception2)
			{
				ProtocolLog.LogError(ProtocolLog.Component.Assistant, loggingContext, string.Format("Failed with StorageTransientException for site mailbox:{0}", mailboxSession.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()), exception2);
			}
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x0007859C File Offset: 0x0007679C
		private static void EnqueueTeamMailboxSyncRequest(MailboxSession session, QueueType queueType, LoggingContext loggingContext)
		{
			string text = "Undefined";
			switch (queueType)
			{
			case QueueType.TeamMailboxDocumentSync:
				text = "document sync";
				break;
			case QueueType.TeamMailboxMembershipSync:
				text = "membership sync";
				break;
			case QueueType.TeamMailboxMaintenanceSync:
				text = "maintenance sync";
				break;
			}
			EnqueueResult enqueueResult = RpcClientWrapper.EnqueueTeamMailboxSyncRequest(session.MailboxOwner.MailboxInfo.Location.ServerFqdn, session.MailboxOwner.MailboxInfo.MailboxGuid, queueType, session.MailboxOwner.MailboxInfo.OrganizationId, "Site Mailbox Assistant", null, SyncOption.Default);
			if (enqueueResult.Result == EnqueueResultType.Successful)
			{
				ProtocolLog.LogInformation(ProtocolLog.Component.Assistant, loggingContext, string.Format("Successfully triggered {0} for site mailbox:{1}", text, session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString()));
				return;
			}
			if (enqueueResult.Result == EnqueueResultType.AlreadyPending)
			{
				ProtocolLog.LogInformation(ProtocolLog.Component.Assistant, loggingContext, string.Format("Skipped pending {0} for site mailbox:{1}. Detail:{2}", text, session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), enqueueResult.ResultDetail));
				return;
			}
			if (enqueueResult.Result == EnqueueResultType.RequestThrottled)
			{
				ProtocolLog.LogInformation(ProtocolLog.Component.Assistant, loggingContext, string.Format("Skipped throttled {0} for site mailbox:{1}. Detail:{2}", text, session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), enqueueResult.ResultDetail));
				return;
			}
			if (enqueueResult.Result == EnqueueResultType.UnlinkedTeamMailbox || enqueueResult.Result == EnqueueResultType.PendingDeleteTeamMailbox || enqueueResult.Result == EnqueueResultType.ClosedTeamMailbox || enqueueResult.Result == EnqueueResultType.NonexistentTeamMailbox)
			{
				ProtocolLog.LogInformation(ProtocolLog.Component.Assistant, loggingContext, string.Format("Skipped {0} for site mailbox:{1}. Detail:{2}", text, session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(), enqueueResult.ResultDetail));
				return;
			}
			ProtocolLog.LogError(ProtocolLog.Component.Assistant, loggingContext, string.Format("Failed to trigger {0} for site mailbox:{1}. Error:{2}; Detail:{3}", new object[]
			{
				text,
				session.MailboxOwner.MailboxInfo.PrimarySmtpAddress.ToString(),
				enqueueResult.Result,
				enqueueResult.ResultDetail
			}), new OperationCanceledException());
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x000787A3 File Offset: 0x000769A3
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x000787AB File Offset: 0x000769AB
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06001506 RID: 5382 RVA: 0x000787B3 File Offset: 0x000769B3
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}
	}
}
