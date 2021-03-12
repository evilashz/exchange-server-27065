using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x0200016A RID: 362
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class IdentifyTargetMailboxOperation : SplitOperationBase
	{
		// Token: 0x06000E93 RID: 3731 RVA: 0x0005773D File Offset: 0x0005593D
		protected override void InvokeInternal()
		{
			this.IdentifyTarget();
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00057748 File Offset: 0x00055948
		internal IdentifyTargetMailboxOperation(IPublicFolderSplitState splitState, IPublicFolderSession publicFolderSession, IPublicFolderMailboxLoggerBase logger, IAssistantRunspaceFactory powershellFactory) : base(IdentifyTargetMailboxOperation.OperationName, publicFolderSession, splitState, logger, powershellFactory, splitState.IdentifyTargetMailboxState, SplitProgressState.IdentifyTargetMailboxStarted, SplitProgressState.IdentifyTargetMailboxCompleted)
		{
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00057770 File Offset: 0x00055970
		private void IdentifyTarget()
		{
			if (this.CanReusePreviousTargetMailbox())
			{
				this.splitState.TargetMailboxGuid = this.splitState.PreviousSplitJobState.TargetMailboxGuid;
				return;
			}
			Mailbox mailbox = this.CreatePublicFolderMailbox();
			if (mailbox != null)
			{
				this.splitState.TargetMailboxGuid = mailbox.ExchangeGuid;
			}
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x000577BC File Offset: 0x000559BC
		private bool CanReusePreviousTargetMailbox()
		{
			bool result = false;
			if (this.splitState.PreviousSplitJobState != null && this.splitState.PreviousSplitJobState.OverallSplitState != null && this.splitState.PreviousSplitJobState.OverallSplitState.Error != null)
			{
				Guid targetMailboxGuid = this.splitState.PreviousSplitJobState.TargetMailboxGuid;
				Unlimited<ByteQuantifiedSize> totalItemSize = PublicFolderSplitHelper.GetTotalItemSize(this.logger, this.powershellFactory, this.splitState.PreviousSplitJobState.TargetMailboxGuid, base.CurrentPublicFolderSession.OrganizationId, this.splitOperationState);
				Unlimited<ByteQuantifiedSize> mailboxQuota = PublicFolderSplitHelper.GetMailboxQuota(this.logger, this.powershellFactory, this.splitState.PreviousSplitJobState.TargetMailboxGuid, base.CurrentPublicFolderSession.OrganizationId, this.splitOperationState);
				if (totalItemSize.Value.ToBytes() <= mailboxQuota.Value.ToBytes() * PublicFolderSplitConfig.Instance.MaxTargetOccupiedThreshold / 100UL)
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x00057914 File Offset: 0x00055B14
		private Mailbox CreatePublicFolderMailbox()
		{
			string value = IdentifyTargetMailboxOperation.PublicFolderMailboxPrefix + DateTime.UtcNow.ToString("HHmmddss");
			PSCommand cmd = new PSCommand();
			cmd.AddCommand("New-Mailbox");
			cmd.AddParameter("PublicFolder");
			cmd.AddParameter("IsExcludedFromServingHierarchy");
			cmd.AddParameter("Name", value);
			cmd.AddParameter("Organization", base.CurrentPublicFolderSession.OrganizationId.OrganizationalUnit.Name);
			Mailbox publicFolderMailbox = null;
			PublicFolderSplitHelper.PowerShellExceptionHandler(delegate(out string originOfException, out ErrorRecord error)
			{
				originOfException = "IdentifyTargetMailboxOperation::IsSplitNeeded::CreatePublicFolderMailbox - RunPSCommand - New-Mailbox -PublicFolder";
				IAssistantRunspaceProxy assistantRunspaceProxy = this.powershellFactory.CreateRunspaceForDatacenterAdmin(this.CurrentPublicFolderSession.OrganizationId);
				publicFolderMailbox = assistantRunspaceProxy.RunPSCommand<Mailbox>(cmd, out error, this.logger);
			}, this.splitOperationState);
			return publicFolderMailbox;
		}

		// Token: 0x04000952 RID: 2386
		private static string OperationName = "IdentifyTargetMailbox";

		// Token: 0x04000953 RID: 2387
		private static string PublicFolderMailboxPrefix = "AutoSplitPFMBX_";
	}
}
