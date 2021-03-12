using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Management.RecipientTasks
{
	// Token: 0x02000C7B RID: 3195
	[Cmdlet("Remove", "MoveRequest", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemoveMoveRequest : SetMoveRequestBase
	{
		// Token: 0x06007AAD RID: 31405 RVA: 0x001F6406 File Offset: 0x001F4606
		public RemoveMoveRequest()
		{
			this.mdbGuid = Guid.Empty;
			this.mrCondition = RemoveMoveRequest.MoveRequestCondition.None;
			this.brokenADUser = null;
			this.validationMessageString = string.Empty;
		}

		// Token: 0x17002601 RID: 9729
		// (get) Token: 0x06007AAE RID: 31406 RVA: 0x001F6432 File Offset: 0x001F4632
		// (set) Token: 0x06007AAF RID: 31407 RVA: 0x001F6449 File Offset: 0x001F4649
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "MigrationMoveRequestQueue")]
		public DatabaseIdParameter MoveRequestQueue
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["MoveRequestQueue"];
			}
			set
			{
				base.Fields["MoveRequestQueue"] = value;
			}
		}

		// Token: 0x17002602 RID: 9730
		// (get) Token: 0x06007AB0 RID: 31408 RVA: 0x001F645C File Offset: 0x001F465C
		// (set) Token: 0x06007AB1 RID: 31409 RVA: 0x001F6481 File Offset: 0x001F4681
		[Parameter(Mandatory = true, ParameterSetName = "MigrationMoveRequestQueue")]
		public Guid MailboxGuid
		{
			get
			{
				return (Guid)(base.Fields["MailboxGuid"] ?? Guid.Empty);
			}
			set
			{
				base.Fields["MailboxGuid"] = value;
			}
		}

		// Token: 0x17002603 RID: 9731
		// (get) Token: 0x06007AB2 RID: 31410 RVA: 0x001F649C File Offset: 0x001F469C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.DataObject != null)
				{
					this.validationMessageString = this.DataObject.ValidationMessage.ToString();
				}
				switch (this.mrCondition)
				{
				case RemoveMoveRequest.MoveRequestCondition.FailedValidation:
					return Strings.ConfirmRemovalOfCorruptMoveRequest(base.LocalADUser.ToString(), this.validationMessageString);
				case RemoveMoveRequest.MoveRequestCondition.MdbDown:
					return Strings.ConfirmOrphanCannotConnectToMailboxDatabase(base.LocalADUser.ToString(), base.MRProvider.MdbGuid.ToString());
				case RemoveMoveRequest.MoveRequestCondition.AdUserMissingMoveData:
					return Strings.ConfirmOrphanUserMissingADData(this.brokenADUser.ToString());
				case RemoveMoveRequest.MoveRequestCondition.MoveCompleted:
					return Strings.ConfirmationMessageRemoveCompletedMoveRequest(base.LocalADUser.ToString());
				}
				if (base.ParameterSetName.Equals("MigrationMoveRequestQueue"))
				{
					return Strings.ConfirmationMessageRemoveMoveRequestDebug(this.MailboxGuid.ToString());
				}
				return Strings.ConfirmationMessageRemoveMoveRequest(base.Identity.ToString());
			}
		}

		// Token: 0x06007AB3 RID: 31411 RVA: 0x001F6596 File Offset: 0x001F4796
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.mdbGuid = Guid.Empty;
			this.mrCondition = RemoveMoveRequest.MoveRequestCondition.None;
			this.brokenADUser = null;
			this.validationMessageString = string.Empty;
		}

		// Token: 0x06007AB4 RID: 31412 RVA: 0x001F65C4 File Offset: 0x001F47C4
		protected override IConfigurable PrepareDataObject()
		{
			try
			{
				if (base.ParameterSetName.Equals("Identity"))
				{
					TransactionalRequestJob transactionalRequestJob = (TransactionalRequestJob)base.PrepareDataObject();
					if (transactionalRequestJob == null)
					{
						if (this.mrCondition == RemoveMoveRequest.MoveRequestCondition.None)
						{
							this.mrCondition = RemoveMoveRequest.MoveRequestCondition.MissingMR;
						}
						MrsTracer.Cmdlet.Warning("Move Request is missing in the MDB", new object[0]);
						transactionalRequestJob = RequestJobBase.CreateDummyObject<TransactionalRequestJob>();
					}
					return transactionalRequestJob;
				}
				base.LocalADUser = null;
				MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.MoveRequestQueue, base.ConfigSession, null, new LocalizedString?(Strings.ErrorMailboxDatabaseNotFound(this.MoveRequestQueue.ToString())), new LocalizedString?(Strings.ErrorMailboxDatabaseNotUnique(this.MoveRequestQueue.ToString())));
				MailboxTaskHelper.VerifyDatabaseIsWithinScopeForRecipientCmdlets(base.SessionSettings, mailboxDatabase, new Task.ErrorLoggerDelegate(base.WriteError));
				Guid guid = mailboxDatabase.Guid;
				this.moveRequestQueueName = mailboxDatabase.ToString();
				base.MRProvider.AllowInvalid = true;
				return (TransactionalRequestJob)base.MRProvider.Read<TransactionalRequestJob>(new RequestJobObjectId(this.MailboxGuid, guid, null));
			}
			catch (MapiExceptionMdbOffline e)
			{
				this.HandleMdbDownException(e);
			}
			catch (MapiExceptionLogonFailed e2)
			{
				this.HandleMdbDownException(e2);
			}
			return RequestJobBase.CreateDummyObject<TransactionalRequestJob>();
		}

		// Token: 0x06007AB5 RID: 31413 RVA: 0x001F6704 File Offset: 0x001F4904
		protected override void CheckADUser()
		{
			base.CheckADUser();
			Guid a;
			Guid a2;
			RequestIndexEntryProvider.GetMoveGuids(base.LocalADUser, out a, out a2);
			if (a == Guid.Empty || a2 == Guid.Empty)
			{
				this.mrCondition = RemoveMoveRequest.MoveRequestCondition.AdUserMissingMoveData;
				this.brokenADUser = base.LocalADUser;
				base.LocalADUser = null;
			}
		}

		// Token: 0x06007AB6 RID: 31414 RVA: 0x001F675C File Offset: 0x001F495C
		protected override void ValidateMoveRequest(TransactionalRequestJob moveRequest)
		{
			if (moveRequest.IsFake)
			{
				return;
			}
			if (!base.ParameterSetName.Equals("MigrationMoveRequestQueue"))
			{
				base.ValidateMoveRequestProtectionStatus(moveRequest);
				if (moveRequest.ValidationResult == RequestJobBase.ValidationResultEnum.Valid)
				{
					base.ValidateMoveRequestIsActive(moveRequest);
					if (moveRequest.RequestJobState == JobProcessingState.InProgress && RequestJobStateNode.RequestStateIs(moveRequest.StatusDetail, RequestState.Cleanup) && moveRequest.IdleTime < TimeSpan.FromMinutes(60.0))
					{
						base.WriteError(new CannotModifyCompletingRequestPermanentException(base.LocalADUser.ToString()), ErrorCategory.InvalidArgument, base.Identity);
					}
					if (moveRequest.CancelRequest)
					{
						base.WriteVerbose(Strings.MoveAlreadyCanceled);
					}
					if (moveRequest.Status == RequestStatus.Completed || moveRequest.Status == RequestStatus.CompletedWithWarning)
					{
						this.mrCondition = RemoveMoveRequest.MoveRequestCondition.MoveCompleted;
						return;
					}
				}
				else
				{
					this.mrCondition = RemoveMoveRequest.MoveRequestCondition.FailedValidation;
				}
			}
		}

		// Token: 0x06007AB7 RID: 31415 RVA: 0x001F68C4 File Offset: 0x001F4AC4
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				TransactionalRequestJob moveRequest = this.DataObject;
				switch (this.mrCondition)
				{
				case RemoveMoveRequest.MoveRequestCondition.None:
					if (base.ParameterSetName.Equals("MigrationMoveRequestQueue"))
					{
						if (moveRequest != null)
						{
							if (moveRequest.CheckIfUnderlyingMessageHasChanged())
							{
								base.WriteVerbose(Strings.ReloadingMoveRequest);
								moveRequest.Refresh();
								this.ValidateMoveRequest(moveRequest);
							}
							base.MRProvider.Delete(moveRequest);
							CommonUtils.CatchKnownExceptions(delegate
							{
								ReportData reportData = new ReportData(moveRequest.ExchangeGuid, moveRequest.ReportVersion);
								reportData.Delete(this.MRProvider.SystemMailbox);
							}, null);
						}
						else
						{
							base.WriteError(new ManagementObjectNotFoundException(Strings.CouldNotRemoveMoveRequest(this.MailboxGuid.ToString())), ErrorCategory.InvalidArgument, base.Identity);
						}
					}
					else
					{
						base.InternalProcessRecord();
						this.CleanupADEntry(base.LocalADUser.Id, base.LocalADUser);
					}
					break;
				case RemoveMoveRequest.MoveRequestCondition.FailedValidation:
					base.WriteVerbose(Strings.MoveFailedValidation(this.validationMessageString));
					this.CleanupADEntry(base.LocalADUser.Id, base.LocalADUser);
					break;
				case RemoveMoveRequest.MoveRequestCondition.MdbDown:
					base.WriteVerbose(Strings.MailboxDatabaseIsDown);
					this.CleanupADEntry(base.LocalADUser.Id, base.LocalADUser);
					break;
				case RemoveMoveRequest.MoveRequestCondition.AdUserMissingMoveData:
					base.WriteVerbose(Strings.ADUserIsMissingData);
					this.CleanupADEntry(this.brokenADUser.Id, this.brokenADUser);
					break;
				case RemoveMoveRequest.MoveRequestCondition.MissingMR:
					base.WriteVerbose(Strings.MoveRequestIsMissing);
					this.CleanupADEntry(base.LocalADUser.Id, base.LocalADUser);
					break;
				case RemoveMoveRequest.MoveRequestCondition.MoveCompleted:
					if (moveRequest != null && !moveRequest.IsFake)
					{
						if (moveRequest.CheckIfUnderlyingMessageHasChanged())
						{
							base.WriteVerbose(Strings.ReloadingMoveRequest);
							moveRequest.Refresh();
							this.ValidateMoveRequest(moveRequest);
						}
						base.MRProvider.Delete(moveRequest);
						CommonUtils.CatchKnownExceptions(delegate
						{
							ReportData reportData = new ReportData(moveRequest.ExchangeGuid, moveRequest.ReportVersion);
							reportData.Delete(this.MRProvider.SystemMailbox);
						}, null);
						this.CleanupADEntry(base.LocalADUser.Id, base.LocalADUser);
					}
					else
					{
						base.WriteError(new ManagementObjectNotFoundException(Strings.CouldNotRemoveCompletedMoveRequest(base.LocalADUser.ToString())), ErrorCategory.InvalidArgument, base.Identity);
					}
					break;
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007AB8 RID: 31416 RVA: 0x001F6B6C File Offset: 0x001F4D6C
		protected override void ModifyMoveRequest(TransactionalRequestJob moveRequest)
		{
			this.mdbGuid = moveRequest.WorkItemQueueMdb.ObjectGuid;
			if (base.LocalADUser != null)
			{
				moveRequest.DomainControllerToUpdate = base.LocalADUser.OriginatingServer;
			}
			if (!moveRequest.CancelRequest)
			{
				moveRequest.CancelRequest = true;
				moveRequest.TimeTracker.SetTimestamp(RequestJobTimestamp.RequestCanceled, new DateTime?(DateTime.UtcNow));
				base.WriteVerbose(Strings.MarkingMoveJobAsCanceled);
				ReportData reportData = new ReportData(moveRequest.ExchangeGuid, moveRequest.ReportVersion);
				ConnectivityRec connectivityRec = new ConnectivityRec(ServerKind.Cmdlet, VersionInformation.MRS);
				reportData.Append(MrsStrings.ReportMoveRequestRemoved(base.ExecutingUserIdentity), connectivityRec);
				reportData.Flush(base.MRProvider.SystemMailbox);
			}
		}

		// Token: 0x06007AB9 RID: 31417 RVA: 0x001F6C18 File Offset: 0x001F4E18
		protected override void PostSaveAction()
		{
			if (!base.ParameterSetName.Equals("MigrationMoveRequestQueue") && this.mrCondition == RemoveMoveRequest.MoveRequestCondition.None && this.DataObject != null)
			{
				TransactionalRequestJob dataObject = this.DataObject;
				using (MailboxReplicationServiceClient mailboxReplicationServiceClient = dataObject.CreateMRSClient(base.ConfigSession, this.mdbGuid, base.UnreachableMrsServers))
				{
					if (mailboxReplicationServiceClient.ServerVersion[3])
					{
						mailboxReplicationServiceClient.RefreshMoveRequest2(this.DataObject.ExchangeGuid, this.mdbGuid, (int)dataObject.Flags, MoveRequestNotification.Canceled);
					}
					else
					{
						mailboxReplicationServiceClient.RefreshMoveRequest(this.DataObject.ExchangeGuid, this.mdbGuid, MoveRequestNotification.Canceled);
					}
				}
			}
		}

		// Token: 0x06007ABA RID: 31418 RVA: 0x001F6CCC File Offset: 0x001F4ECC
		private void CleanupADEntry(ADObjectId userId, ADUser adUser)
		{
			base.WriteVerbose(Strings.RemovingMoveJobFromAd(userId.ToString()));
			try
			{
				CommonUtils.CleanupMoveRequestInAD(base.WriteableSession, userId, adUser);
			}
			catch (UnableToReadADUserException)
			{
				base.WriteVerbose(Strings.UserNotInAd);
			}
		}

		// Token: 0x06007ABB RID: 31419 RVA: 0x001F6D18 File Offset: 0x001F4F18
		private void HandleMdbDownException(Exception e)
		{
			if (base.ParameterSetName.Equals("Identity"))
			{
				MrsTracer.Cmdlet.Error("MailboxDatabase connection error when trying to read MoveRequest '{0}'.  Error details: {1}.", new object[]
				{
					base.Identity,
					CommonUtils.FullExceptionMessage(e)
				});
				this.mrCondition = RemoveMoveRequest.MoveRequestCondition.MdbDown;
			}
			if (base.ParameterSetName.Equals("MigrationMoveRequestQueue"))
			{
				MrsTracer.Cmdlet.Error("MailboxDatabase connection error when trying to read all MoveRequests from database '{0}'.  Error details: {1}.", new object[]
				{
					this.moveRequestQueueName,
					CommonUtils.FullExceptionMessage(e)
				});
				base.WriteError(new DatabaseConnectionTransientException(this.moveRequestQueueName), ErrorCategory.InvalidArgument, this.MoveRequestQueue);
			}
		}

		// Token: 0x04003CB3 RID: 15539
		public const string ParameterMoveRequestQueue = "MoveRequestQueue";

		// Token: 0x04003CB4 RID: 15540
		public const string ParameterMailboxGuid = "MailboxGuid";

		// Token: 0x04003CB5 RID: 15541
		public const string MoveRequestQueueSet = "MigrationMoveRequestQueue";

		// Token: 0x04003CB6 RID: 15542
		private Guid mdbGuid;

		// Token: 0x04003CB7 RID: 15543
		private RemoveMoveRequest.MoveRequestCondition mrCondition;

		// Token: 0x04003CB8 RID: 15544
		private ADUser brokenADUser;

		// Token: 0x04003CB9 RID: 15545
		private string validationMessageString;

		// Token: 0x04003CBA RID: 15546
		private string moveRequestQueueName;

		// Token: 0x02000C7C RID: 3196
		private enum MoveRequestCondition
		{
			// Token: 0x04003CBC RID: 15548
			None,
			// Token: 0x04003CBD RID: 15549
			FailedValidation,
			// Token: 0x04003CBE RID: 15550
			MdbDown,
			// Token: 0x04003CBF RID: 15551
			AdUserMissingMoveData,
			// Token: 0x04003CC0 RID: 15552
			MissingMR,
			// Token: 0x04003CC1 RID: 15553
			MoveCompleted
		}
	}
}
