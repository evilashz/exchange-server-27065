using System;
using System.IO;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x0200017F RID: 383
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PrepareTargetMailboxOperation : SplitOperationBase
	{
		// Token: 0x06000F5C RID: 3932 RVA: 0x0005B6FC File Offset: 0x000598FC
		internal PrepareTargetMailboxOperation(IPublicFolderSplitState splitState, IPublicFolderSession publicFolderSession, IPublicFolderMailboxLoggerBase logger, IAssistantRunspaceFactory powershellFactory, IXSOFactory xsoFactory) : base(PrepareTargetMailboxOperation.OperationName, publicFolderSession, splitState, logger, powershellFactory, splitState.PrepareTargetMailboxState, SplitProgressState.PrepareTargetMailboxStarted, SplitProgressState.PrepareTargetMailboxCompleted)
		{
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0005B72C File Offset: 0x0005992C
		protected override void InvokeInternal()
		{
			try
			{
				if (!this.IsTargetMailboxValid())
				{
					this.splitOperationState.Error = new InvalidDataException(string.Format("The TargetMailboxGuid, {0}, is invalid.", this.splitState.TargetMailboxGuid.ToString()));
				}
				else
				{
					this.SyncPublicFolderMailbox(this.splitState.TargetMailboxGuid);
				}
			}
			catch (StorageTransientException error)
			{
				this.splitOperationState.Error = error;
			}
			catch (StoragePermanentException error2)
			{
				this.splitOperationState.Error = error2;
			}
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0005B7C4 File Offset: 0x000599C4
		private void SyncPublicFolderMailbox(Guid mailboxGuid)
		{
			if (PublicFolderSplitHelper.IsPrimaryHierarchy(mailboxGuid, base.CurrentPublicFolderSession.OrganizationId))
			{
				this.logger.LogEvent(LogEventType.Verbose, string.Format("The target mailbox, {0}, is the primary hierarchy. Skipping PrepareTargetMailboxOperation.", this.splitState.TargetMailboxGuid.ToString()));
				return;
			}
			bool flag = false;
			bool flag2 = PublicFolderSplitHelper.IsSyncRequired(mailboxGuid, base.CurrentPublicFolderSession.OrganizationId, out flag, this.xsoFactory, this.logger);
			if (flag2)
			{
				if (flag)
				{
					this.SyncAndReturn(mailboxGuid);
				}
				else
				{
					PublicFolderSplitHelper.SyncAndWaitForCompletion(mailboxGuid, base.CurrentPublicFolderSession.OrganizationId, this.xsoFactory, this.logger, this.splitOperationState);
				}
			}
			this.logger.LogEvent(LogEventType.Statistics, string.Format("PrepareTargetMailboxOperation::SyncPublicFolderMailbox - SR={0},IPS={1},PSC={2},RC={3}", new object[]
			{
				flag2,
				this.splitOperationState.PartialStep,
				this.splitOperationState.PartialStepCount,
				this.splitOperationState.RetryCount
			}));
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0005B8C8 File Offset: 0x00059AC8
		private void SyncAndReturn(Guid mailboxGuid)
		{
			this.splitOperationState.PartialStep = true;
			ExchangePrincipal contentMailboxPrincipal;
			if (!PublicFolderSession.TryGetPublicFolderMailboxPrincipal(base.CurrentPublicFolderSession.OrganizationId, mailboxGuid, false, out contentMailboxPrincipal))
			{
				throw new ObjectNotFoundException(ServerStrings.PublicFolderMailboxNotFound);
			}
			PublicFolderSyncJobState publicFolderSyncJobState = PublicFolderSyncJobRpc.StartSyncHierarchy(contentMailboxPrincipal, false);
			if (PublicFolderSplitHelper.HasSyncFailure(publicFolderSyncJobState))
			{
				this.splitOperationState.Error = publicFolderSyncJobState.LastError;
				return;
			}
			if ((int)this.splitOperationState.PartialStepCount > PrepareTargetMailboxOperation.PartialStepIterationLimit)
			{
				this.splitOperationState.Error = new PartialStepsOverLimitException("Long running sync operation", PrepareTargetMailboxOperation.PartialStepIterationLimit);
			}
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0005B950 File Offset: 0x00059B50
		private bool IsTargetMailboxValid()
		{
			return this.splitState.TargetMailboxGuid != Guid.Empty;
		}

		// Token: 0x040009B9 RID: 2489
		private static string OperationName = "PrepareTargetMailbox";

		// Token: 0x040009BA RID: 2490
		private readonly IXSOFactory xsoFactory;

		// Token: 0x040009BB RID: 2491
		internal static int PartialStepIterationLimit = 5;
	}
}
