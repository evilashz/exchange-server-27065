using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxReplicationService;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.PublicFolder
{
	// Token: 0x02000170 RID: 368
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MoveContentOperation : SplitOperationBase
	{
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000EC5 RID: 3781 RVA: 0x000579FB File Offset: 0x00055BFB
		protected override int MaxRetryCount
		{
			get
			{
				return PublicFolderSplitConfig.Instance.MoveInProgressRetryCount;
			}
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x00057A08 File Offset: 0x00055C08
		internal MoveContentOperation(IPublicFolderSplitState splitState, IPublicFolderSession publicFolderSession, IPublicFolderMailboxLoggerBase logger, IAssistantRunspaceFactory powershellFactory, IXSOFactory xsoFactory) : base(MoveContentOperation.OperationName, publicFolderSession, splitState, logger, powershellFactory, splitState.MoveContentState, SplitProgressState.MoveContentStarted, SplitProgressState.MoveContentCompleted)
		{
			this.xsoFactory = xsoFactory;
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00057A38 File Offset: 0x00055C38
		protected override void InvokeInternal()
		{
			if (!this.AreSplitPlanFoldersValid())
			{
				this.splitOperationState.Error = new SplitPlanFoldersInvalidException();
				return;
			}
			PublicFolderMoveRequest publicFolderMoveRequest = this.GetPublicFolderMoveRequest();
			if (this.splitOperationState.Error == null && this.TryHandleExistingRequest(publicFolderMoveRequest))
			{
				Unlimited<ByteQuantifiedSize> totalItemSize = PublicFolderSplitHelper.GetTotalItemSize(this.logger, this.powershellFactory, this.splitState.TargetMailboxGuid, base.CurrentPublicFolderSession.OrganizationId, this.splitOperationState);
				if (this.splitOperationState.Error != null)
				{
					return;
				}
				Unlimited<ByteQuantifiedSize> mailboxQuota = PublicFolderSplitHelper.GetMailboxQuota(this.logger, this.powershellFactory, this.splitState.TargetMailboxGuid, base.CurrentPublicFolderSession.OrganizationId, this.splitOperationState);
				if (this.splitOperationState.Error != null)
				{
					return;
				}
				if (totalItemSize.Value.ToBytes() + this.splitState.SplitPlan.TotalSizeToSplit >= mailboxQuota.Value.ToBytes() * PublicFolderSplitConfig.Instance.SplitThreshold / 100UL)
				{
					this.splitOperationState.Error = new TargetMailboxOutofQuotaException(base.CurrentPublicFolderSession.OrganizationId.OrganizationalUnit.Name, this.splitState.TargetMailboxGuid.ToString());
					return;
				}
				try
				{
					if (!PublicFolderSplitHelper.IsPrimaryHierarchy(this.splitState.TargetMailboxGuid, base.CurrentPublicFolderSession.OrganizationId))
					{
						bool flag = false;
						bool flag2 = PublicFolderSplitHelper.IsSyncRequired(this.splitState.TargetMailboxGuid, base.CurrentPublicFolderSession.OrganizationId, out flag, this.xsoFactory, this.logger);
						if (flag2)
						{
							PublicFolderSplitHelper.SyncAndWaitForCompletion(this.splitState.TargetMailboxGuid, base.CurrentPublicFolderSession.OrganizationId, this.xsoFactory, this.logger, this.splitOperationState);
						}
					}
					else
					{
						this.logger.LogEvent(LogEventType.Verbose, string.Format("The target mailbox, {0}, is the primary hierarchy. Skipped sync.", this.splitState.TargetMailboxGuid.ToString()));
					}
				}
				catch (StorageTransientException error)
				{
					this.splitOperationState.Error = error;
					return;
				}
				catch (StoragePermanentException error2)
				{
					this.splitOperationState.Error = error2;
					return;
				}
				this.IssueMoveRequest();
				if (this.splitOperationState.Error == null)
				{
					this.splitOperationState.PartialStep = true;
					this.logger.LogEvent(LogEventType.Verbose, "MoveContentOperation::InvokeInternal - Successfully issued a new public folder move request.");
				}
			}
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x00057C9C File Offset: 0x00055E9C
		private bool TryHandleExistingRequest(PublicFolderMoveRequest existingRequest)
		{
			bool result = true;
			if (existingRequest != null)
			{
				result = false;
				ADObjectId sourceMailbox = this.GetSourceMailbox(existingRequest);
				if (this.IsMoveRequestInteresting(existingRequest, sourceMailbox))
				{
					this.logger.LogEvent(LogEventType.Verbose, string.Format("MoveContentOperation::InvokeInternal - Existing move request found for the current mailbox '{0}\\{1}'. Its status is {2}", base.CurrentPublicFolderSession.OrganizationId.OrganizationalUnit.Name, base.CurrentPublicFolderSession.MailboxGuid.ToString(), existingRequest.Status.ToString()));
					RequestStatus status = existingRequest.Status;
					switch (status)
					{
					case RequestStatus.Queued:
					case RequestStatus.InProgress:
					case RequestStatus.CompletionInProgress:
						this.logger.LogEvent(LogEventType.Verbose, "MoveContentOperation::InvokeInternal - Waiting for the existing move request to complete. Exiting MoveContentOperation.");
						this.splitOperationState.Error = new PublicFolderMoveInProgressException();
						return result;
					case RequestStatus.AutoSuspended:
					case RequestStatus.Synced:
						break;
					case (RequestStatus)6:
					case (RequestStatus)7:
					case (RequestStatus)8:
					case (RequestStatus)9:
						goto IL_20E;
					case RequestStatus.Completed:
					case RequestStatus.CompletedWithWarning:
						this.RemovePublicFolderMoveRequest(existingRequest);
						if (this.splitOperationState.Error != null)
						{
							return result;
						}
						this.logger.LogEvent(LogEventType.Verbose, "MoveContentOperation::InvokeInternal - Removed the successfully completed move request.");
						this.ResetIsExcludedFromServingHierarchy();
						if (this.splitOperationState.Error == null)
						{
							this.logger.LogEvent(LogEventType.Verbose, string.Format("MoveContentOperation::InvokeInternal - Successfully reset the IsExcludedFromServingHierarchy flag on the mailbox '{0}\\{1}'.", base.CurrentPublicFolderSession.OrganizationId.OrganizationalUnit.Name, base.CurrentPublicFolderSession.MailboxGuid.ToString()));
							return result;
						}
						return result;
					default:
						switch (status)
						{
						case RequestStatus.Suspended:
							break;
						case RequestStatus.Failed:
						{
							PublicFolderMoveRequestStatistics publicFolderMoveRequestStatistics = this.GetPublicFolderMoveRequestStatistics(existingRequest, true);
							if (publicFolderMoveRequestStatistics != null && publicFolderMoveRequestStatistics.Report != null && publicFolderMoveRequestStatistics.Report.Failures != null && publicFolderMoveRequestStatistics.Report.Failures.Count > 0)
							{
								this.splitOperationState.Error = new PublicFolderMoveFailedException(publicFolderMoveRequestStatistics.Report.Failures[publicFolderMoveRequestStatistics.Report.Failures.Count - 1].FailureType);
								return result;
							}
							this.splitOperationState.Error = new PublicFolderMoveFailedException(string.Empty);
							return result;
						}
						default:
							goto IL_20E;
						}
						break;
					}
					this.splitOperationState.Error = new PublicFolderMoveSuspendedException();
					return result;
					IL_20E:
					this.splitOperationState.Error = new UnexpectedMoveStateException(existingRequest.Status.ToString());
				}
				else
				{
					this.splitOperationState.PartialStep = true;
					this.logger.LogEvent(LogEventType.Verbose, string.Format("MoveContentOperation::InvokeInternal - Existing move request found for a different mailbox '{0}\\{1}' . The move operation will resume after it is removed.", existingRequest.OrganizationId.ToString(), (sourceMailbox != null) ? sourceMailbox.ToString() : string.Empty));
				}
			}
			return result;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00057F18 File Offset: 0x00056118
		private ADObjectId GetSourceMailbox(PublicFolderMoveRequest request)
		{
			if (request.SourceMailbox != null)
			{
				return request.SourceMailbox;
			}
			PublicFolderMoveRequestStatistics publicFolderMoveRequestStatistics = this.GetPublicFolderMoveRequestStatistics(request, false);
			if (publicFolderMoveRequestStatistics != null)
			{
				return publicFolderMoveRequestStatistics.SourceMailbox;
			}
			return null;
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x00057F48 File Offset: 0x00056148
		private bool AreSplitPlanFoldersValid()
		{
			return this.splitState.SplitPlan != null && this.splitState.SplitPlan.FoldersToSplit != null && this.splitState.SplitPlan.FoldersToSplit.Count > 0 && this.splitState.SplitPlan.TotalSizeToSplit > 0UL;
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00057FA2 File Offset: 0x000561A2
		private bool IsMoveRequestInteresting(PublicFolderMoveRequest moveRequest, ADObjectId sourceMailbox)
		{
			return sourceMailbox != null && sourceMailbox.Equals(base.CurrentPublicFolderSession.MailboxPrincipal.ObjectId);
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0005803C File Offset: 0x0005623C
		private PublicFolderMoveRequest IssueMoveRequest()
		{
			List<SplitPlanFolder> foldersToSplit = this.splitState.SplitPlan.FoldersToSplit;
			List<string> list = new List<string>();
			foreach (SplitPlanFolder splitPlanFolder in foldersToSplit)
			{
				list.Add(splitPlanFolder.PublicFolderId.MapiFolderPath.ToString());
			}
			string publicFolderMoveRequestName = MoveContentOperation.PublicFolderMoveRequestPrefix + DateTime.UtcNow.ToString("HHmmddss");
			PSCommand cmd = new PSCommand();
			cmd.AddCommand("New-PublicFolderMoveRequest");
			cmd.AddParameter("AllowLargeItems");
			cmd.AddParameter("Name", publicFolderMoveRequestName);
			cmd.AddParameter("Folders", list.ToArray());
			cmd.AddParameter("TargetMailbox", this.splitState.TargetMailboxGuid.ToString());
			cmd.AddParameter("CompletedRequestAgeLimit", PublicFolderSplitConfig.Instance.CompletedPublicFolderMoveRequestAgeLimit);
			cmd.AddParameter("Organization", base.CurrentPublicFolderSession.OrganizationId.OrganizationalUnit.Name);
			PublicFolderMoveRequest moveRequest = null;
			PublicFolderSplitHelper.PowerShellExceptionHandler(delegate(out string originOfException, out ErrorRecord error)
			{
				originOfException = "MoveContentOperation::IssueMoveRequest - RunPSCommand - New-PublicFolderMoveRequest";
				IAssistantRunspaceProxy assistantRunspaceProxy = this.powershellFactory.CreateRunspaceForDatacenterAdmin(this.CurrentPublicFolderSession.OrganizationId);
				moveRequest = assistantRunspaceProxy.RunPSCommand<PublicFolderMoveRequest>(cmd, out error, this.logger);
				if (moveRequest != null)
				{
					this.splitState.PublicFolderMoveRequestName = publicFolderMoveRequestName;
				}
			}, this.splitOperationState);
			if (this.splitOperationState.Error == null && moveRequest == null)
			{
				this.splitOperationState.Error = new IssuePublicFolderMoveRequestFailedException();
			}
			return moveRequest;
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00058250 File Offset: 0x00056450
		private void ResetIsExcludedFromServingHierarchy()
		{
			string value = base.CurrentPublicFolderSession.OrganizationId.OrganizationalUnit.Name + "\\" + this.splitState.TargetMailboxGuid.ToString();
			PSCommand cmd = new PSCommand();
			cmd.AddCommand("Set-Mailbox");
			cmd.AddParameter("PublicFolder");
			cmd.AddParameter("Identity", value);
			cmd.AddParameter("IsExcludedFromServingHierarchy", false);
			PublicFolderSplitHelper.PowerShellExceptionHandler(delegate(out string originOfException, out ErrorRecord error)
			{
				originOfException = "MoveContentOperation::ResetIsExcludedFromServingHierarchy - RunPSCommand - Set-Mailbox";
				IAssistantRunspaceProxy assistantRunspaceProxy = this.powershellFactory.CreateRunspaceForDatacenterAdmin(this.CurrentPublicFolderSession.OrganizationId);
				assistantRunspaceProxy.RunPSCommand<Mailbox>(cmd, out error, this.logger);
			}, this.splitOperationState);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0005836C File Offset: 0x0005656C
		private PublicFolderMoveRequest GetPublicFolderMoveRequest()
		{
			PSCommand cmd = new PSCommand();
			cmd.AddCommand("Get-PublicFolderMoveRequest");
			cmd.AddParameter("Organization", base.CurrentPublicFolderSession.OrganizationId.OrganizationalUnit.Name);
			PublicFolderMoveRequest moveRequest = null;
			PublicFolderSplitHelper.PowerShellExceptionHandler(delegate(out string originOfException, out ErrorRecord error)
			{
				originOfException = "MoveContentOperation::GetPublicFolderMoveRequest - RunPSCommand - Get-PublicFolderMoveRequest";
				IAssistantRunspaceProxy assistantRunspaceProxy = this.powershellFactory.CreateRunspaceForDatacenterAdmin(this.CurrentPublicFolderSession.OrganizationId);
				moveRequest = assistantRunspaceProxy.RunPSCommand<PublicFolderMoveRequest>(cmd, out error, this.logger);
			}, this.splitOperationState);
			return moveRequest;
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00058444 File Offset: 0x00056644
		private void RemovePublicFolderMoveRequest(PublicFolderMoveRequest moveRequest)
		{
			string value = base.CurrentPublicFolderSession.OrganizationId.OrganizationalUnit.Name + "\\" + moveRequest.RequestGuid.ToString();
			PSCommand cmd = new PSCommand();
			cmd.AddCommand("Remove-PublicFolderMoveRequest");
			cmd.AddParameter("Identity", value);
			PublicFolderSplitHelper.PowerShellExceptionHandler(delegate(out string originOfException, out ErrorRecord error)
			{
				originOfException = "MoveContentOperation::RemovePublicFolderMoveRequest - RunPSCommand - Remove-PublicFolderMoveRequest";
				IAssistantRunspaceProxy assistantRunspaceProxy = this.powershellFactory.CreateRunspaceForDatacenterAdmin(this.CurrentPublicFolderSession.OrganizationId);
				assistantRunspaceProxy.RunPSCommand<PublicFolderMoveRequest>(cmd, out error, this.logger);
			}, this.splitOperationState);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00058534 File Offset: 0x00056734
		private PublicFolderMoveRequestStatistics GetPublicFolderMoveRequestStatistics(PublicFolderMoveRequest moveRequest, bool includeReport)
		{
			PSCommand cmd = new PSCommand();
			cmd.AddCommand("Get-PublicFolderMoveRequestStatistics");
			cmd.AddParameter("Identity", moveRequest.Identity);
			if (includeReport)
			{
				cmd.AddParameter("IncludeReport");
			}
			PublicFolderMoveRequestStatistics moveRequestStats = null;
			PublicFolderSplitHelper.PowerShellExceptionHandler(delegate(out string originOfException, out ErrorRecord error)
			{
				originOfException = "MoveContentOperation::GetPublicFolderMoveRequestStatistics - RunPSCommand - Get-PublicFolderMoveRequestStatistics";
				IAssistantRunspaceProxy assistantRunspaceProxy = this.powershellFactory.CreateRunspaceForDatacenterAdmin(this.CurrentPublicFolderSession.OrganizationId);
				moveRequestStats = assistantRunspaceProxy.RunPSCommand<PublicFolderMoveRequestStatistics>(cmd, out error, this.logger);
			}, this.splitOperationState);
			return moveRequestStats;
		}

		// Token: 0x04000954 RID: 2388
		private static string OperationName = "MoveContent";

		// Token: 0x04000955 RID: 2389
		private static string PublicFolderMoveRequestPrefix = "PFMoveRequest_";

		// Token: 0x04000956 RID: 2390
		private readonly IXSOFactory xsoFactory;
	}
}
