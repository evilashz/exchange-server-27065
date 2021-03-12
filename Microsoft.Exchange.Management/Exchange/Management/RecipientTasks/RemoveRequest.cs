using System;
using System.Collections.Generic;
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
	// Token: 0x02000C67 RID: 3175
	public abstract class RemoveRequest<TIdentity> : SetRequestBase<TIdentity> where TIdentity : MRSRequestIdParameter
	{
		// Token: 0x06007937 RID: 31031 RVA: 0x001EDD78 File Offset: 0x001EBF78
		public RemoveRequest()
		{
			this.mdbGuid = Guid.Empty;
			this.requestCondition = RemoveRequest<TIdentity>.RequestCondition.None;
			this.brokenIndexEntry = null;
			this.indexEntries = new List<IRequestIndexEntry>();
			this.validationMessageString = string.Empty;
		}

		// Token: 0x17002581 RID: 9601
		// (get) Token: 0x06007938 RID: 31032 RVA: 0x001EDDAF File Offset: 0x001EBFAF
		// (set) Token: 0x06007939 RID: 31033 RVA: 0x001EDDB7 File Offset: 0x001EBFB7
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "Identity", Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override TIdentity Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x17002582 RID: 9602
		// (get) Token: 0x0600793A RID: 31034 RVA: 0x001EDDC0 File Offset: 0x001EBFC0
		// (set) Token: 0x0600793B RID: 31035 RVA: 0x001EDDD7 File Offset: 0x001EBFD7
		[ValidateNotNull]
		[Parameter(Mandatory = true, ParameterSetName = "MigrationRequestQueue")]
		public DatabaseIdParameter RequestQueue
		{
			get
			{
				return (DatabaseIdParameter)base.Fields["RequestQueue"];
			}
			set
			{
				base.Fields["RequestQueue"] = value;
			}
		}

		// Token: 0x17002583 RID: 9603
		// (get) Token: 0x0600793C RID: 31036 RVA: 0x001EDDEA File Offset: 0x001EBFEA
		// (set) Token: 0x0600793D RID: 31037 RVA: 0x001EDE0F File Offset: 0x001EC00F
		[Parameter(Mandatory = true, ParameterSetName = "MigrationRequestQueue")]
		public Guid RequestGuid
		{
			get
			{
				return (Guid)(base.Fields["RequestGuid"] ?? Guid.Empty);
			}
			set
			{
				base.Fields["RequestGuid"] = value;
			}
		}

		// Token: 0x17002584 RID: 9604
		// (get) Token: 0x0600793E RID: 31038 RVA: 0x001EDE28 File Offset: 0x001EC028
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.DataObject != null)
				{
					this.validationMessageString = this.DataObject.ValidationMessage.ToString();
				}
				switch (this.requestCondition)
				{
				case RemoveRequest<TIdentity>.RequestCondition.FailedValidation:
					return Strings.ConfirmRemovalOfCorruptRequest(this.GenerateIndexEntryString(base.IndexEntry), this.validationMessageString);
				case RemoveRequest<TIdentity>.RequestCondition.IndexEntryMissingData:
					return Strings.ConfirmRemoveIndexEntryMissingADData(this.GenerateIndexEntryString(this.brokenIndexEntry));
				case RemoveRequest<TIdentity>.RequestCondition.Completed:
					return Strings.ConfirmationMessageRemoveCompletedRequest(this.GenerateIndexEntryString(base.IndexEntry));
				case RemoveRequest<TIdentity>.RequestCondition.MdbDown:
					return Strings.ConfirmRemovalOfRequestForInaccessibleDatabase(this.GenerateIndexEntryString(base.IndexEntry), this.mdbGuid);
				}
				if (base.ParameterSetName.Equals("MigrationRequestQueue"))
				{
					return Strings.ConfirmationMessageRemoveRequestDebug(this.RequestGuid.ToString());
				}
				return Strings.ConfirmationMessageRemoveRequest(this.GenerateIndexEntryString(base.IndexEntry));
			}
		}

		// Token: 0x0600793F RID: 31039
		internal abstract string GenerateIndexEntryString(IRequestIndexEntry entry);

		// Token: 0x06007940 RID: 31040 RVA: 0x001EDF14 File Offset: 0x001EC114
		protected override void InternalStateReset()
		{
			base.InternalStateReset();
			this.mdbGuid = Guid.Empty;
			this.requestCondition = RemoveRequest<TIdentity>.RequestCondition.None;
			this.brokenIndexEntry = null;
			this.indexEntries.Clear();
			this.validationMessageString = string.Empty;
		}

		// Token: 0x06007941 RID: 31041 RVA: 0x001EDF4C File Offset: 0x001EC14C
		protected override IConfigurable PrepareDataObject()
		{
			this.brokenIndexEntry = null;
			this.indexEntries.Clear();
			try
			{
				if (base.ParameterSetName.Equals("Identity"))
				{
					TransactionalRequestJob transactionalRequestJob = (TransactionalRequestJob)base.PrepareDataObject();
					if (transactionalRequestJob == null)
					{
						if (this.requestCondition == RemoveRequest<TIdentity>.RequestCondition.None)
						{
							this.requestCondition = RemoveRequest<TIdentity>.RequestCondition.MissingRJ;
							this.indexEntries.Add(base.IndexEntry);
						}
						else
						{
							this.indexEntries.Add(this.brokenIndexEntry);
						}
						MrsTracer.Cmdlet.Warning("Request is missing from the MDB", new object[0]);
						transactionalRequestJob = RequestJobBase.CreateDummyObject<TransactionalRequestJob>();
					}
					else
					{
						this.indexEntries.AddRange(transactionalRequestJob.IndexEntries);
						if (!this.indexEntries.Contains(base.IndexEntry))
						{
							this.indexEntries.Add(base.IndexEntry);
						}
					}
					return transactionalRequestJob;
				}
				base.IndexEntry = null;
				MailboxDatabase mailboxDatabase = (MailboxDatabase)base.GetDataObject<MailboxDatabase>(this.RequestQueue, base.ConfigSession, null, new LocalizedString?(Strings.ErrorMailboxDatabaseNotFound(this.RequestQueue.ToString())), new LocalizedString?(Strings.ErrorMailboxDatabaseNotUnique(this.RequestQueue.ToString())));
				MailboxTaskHelper.VerifyDatabaseIsWithinScopeForRecipientCmdlets(base.CurrentOrgConfigSession.SessionSettings, mailboxDatabase, new Task.ErrorLoggerDelegate(base.WriteError));
				Guid guid = mailboxDatabase.Guid;
				this.requestQueueName = mailboxDatabase.ToString();
				base.RJProvider.AllowInvalid = true;
				TransactionalRequestJob transactionalRequestJob2 = null;
				try
				{
					transactionalRequestJob2 = (TransactionalRequestJob)base.RJProvider.Read<TransactionalRequestJob>(new RequestJobObjectId(this.RequestGuid, guid, null));
					if (transactionalRequestJob2 != null)
					{
						if (transactionalRequestJob2.TargetUser != null && transactionalRequestJob2.TargetUserId != null)
						{
							this.ResolveADUser(transactionalRequestJob2.TargetUserId);
						}
						if (transactionalRequestJob2.SourceUser != null && transactionalRequestJob2.SourceUserId != null)
						{
							this.ResolveADUser(transactionalRequestJob2.SourceUserId);
						}
					}
					TransactionalRequestJob result = transactionalRequestJob2;
					transactionalRequestJob2 = null;
					return result;
				}
				finally
				{
					if (transactionalRequestJob2 != null)
					{
						transactionalRequestJob2.Dispose();
						transactionalRequestJob2 = null;
					}
				}
			}
			catch (MapiExceptionMdbOffline e)
			{
				this.HandleMdbDownException(e);
				this.indexEntries.Add(base.IndexEntry);
			}
			catch (MapiExceptionLogonFailed e2)
			{
				this.HandleMdbDownException(e2);
				this.indexEntries.Add(base.IndexEntry);
			}
			return RequestJobBase.CreateDummyObject<TransactionalRequestJob>();
		}

		// Token: 0x06007942 RID: 31042 RVA: 0x001EE1A8 File Offset: 0x001EC3A8
		protected override void CheckIndexEntry()
		{
			if (base.IndexEntry != null)
			{
				RequestJobObjectId requestJobId = base.IndexEntry.GetRequestJobId();
				if (requestJobId == null || requestJobId.RequestGuid == Guid.Empty || requestJobId.MdbGuid == Guid.Empty)
				{
					this.requestCondition = RemoveRequest<TIdentity>.RequestCondition.IndexEntryMissingData;
					this.brokenIndexEntry = base.IndexEntry;
					base.IndexEntry = null;
				}
			}
		}

		// Token: 0x06007943 RID: 31043 RVA: 0x001EE20C File Offset: 0x001EC40C
		protected override void ValidateRequest(TransactionalRequestJob requestJob)
		{
			if (requestJob.IsFake)
			{
				return;
			}
			base.ValidateRequest(requestJob);
			if (!base.ParameterSetName.Equals("MigrationRequestQueue"))
			{
				base.ValidateRequestProtectionStatus(requestJob);
				if (requestJob.ValidationResult == RequestJobBase.ValidationResultEnum.Valid)
				{
					base.ValidateRequestIsActive(requestJob);
					if (requestJob.RequestJobState == JobProcessingState.InProgress && RequestJobStateNode.RequestStateIs(requestJob.StatusDetail, RequestState.Completion) && !RequestJobStateNode.RequestStateIs(requestJob.StatusDetail, RequestState.IncrementalSync) && requestJob.IdleTime < TimeSpan.FromMinutes(60.0))
					{
						base.WriteError(new CannotModifyCompletingRequestPermanentException(base.IndexEntry.ToString()), ErrorCategory.InvalidArgument, this.Identity);
					}
					if (requestJob.CancelRequest)
					{
						base.WriteVerbose(Strings.RequestAlreadyCanceled);
					}
					if (requestJob.Status == RequestStatus.Completed || requestJob.Status == RequestStatus.CompletedWithWarning)
					{
						if (this.requestCondition != RemoveRequest<TIdentity>.RequestCondition.None && this.requestCondition != RemoveRequest<TIdentity>.RequestCondition.Completed)
						{
							base.WriteError(new CannotRemoveCompletedDuringCancelPermanentException(base.IndexEntry.ToString()), ErrorCategory.InvalidArgument, this.Identity);
						}
						this.requestCondition = RemoveRequest<TIdentity>.RequestCondition.Completed;
						return;
					}
				}
				else
				{
					this.requestCondition = RemoveRequest<TIdentity>.RequestCondition.FailedValidation;
				}
			}
		}

		// Token: 0x06007944 RID: 31044 RVA: 0x001EE3C0 File Offset: 0x001EC5C0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject
			});
			try
			{
				TransactionalRequestJob requestJob = this.DataObject;
				switch (this.requestCondition)
				{
				case RemoveRequest<TIdentity>.RequestCondition.None:
					if (base.ParameterSetName.Equals("MigrationRequestQueue"))
					{
						if (requestJob != null)
						{
							if (requestJob.CheckIfUnderlyingMessageHasChanged())
							{
								base.WriteVerbose(Strings.ReloadingRequest);
								requestJob.Refresh();
								this.ValidateRequest(requestJob);
							}
							base.RJProvider.Delete(requestJob);
							CommonUtils.CatchKnownExceptions(delegate
							{
								ReportData reportData = new ReportData(requestJob.RequestGuid, requestJob.ReportVersion);
								reportData.Delete(this.RJProvider.SystemMailbox);
							}, null);
						}
						else
						{
							base.WriteError(new ManagementObjectNotFoundException(Strings.CouldNotRemoveRequest(this.RequestGuid.ToString())), ErrorCategory.InvalidArgument, this.Identity);
						}
					}
					else
					{
						base.InternalProcessRecord();
						this.CleanupIndexEntries();
					}
					break;
				case RemoveRequest<TIdentity>.RequestCondition.FailedValidation:
					base.WriteVerbose(Strings.RequestFailedValidation(this.validationMessageString));
					this.CleanupIndexEntries();
					break;
				case RemoveRequest<TIdentity>.RequestCondition.IndexEntryMissingData:
					base.WriteVerbose(Strings.IndexEntryIsMissingData);
					this.CleanupIndexEntries();
					break;
				case RemoveRequest<TIdentity>.RequestCondition.MissingRJ:
					base.WriteVerbose(Strings.RequestIsMissing);
					this.CleanupIndexEntries();
					break;
				case RemoveRequest<TIdentity>.RequestCondition.Completed:
					if (requestJob != null && !requestJob.IsFake)
					{
						if (requestJob.CheckIfUnderlyingMessageHasChanged())
						{
							base.WriteVerbose(Strings.ReloadingRequest);
							requestJob.Refresh();
							this.ValidateRequest(requestJob);
						}
						base.RJProvider.Delete(requestJob);
						CommonUtils.CatchKnownExceptions(delegate
						{
							ReportData reportData = new ReportData(requestJob.RequestGuid, requestJob.ReportVersion);
							reportData.Delete(this.RJProvider.SystemMailbox);
						}, null);
						this.CleanupIndexEntries();
					}
					else
					{
						base.WriteError(new ManagementObjectNotFoundException(Strings.CouldNotRemoveCompletedRequest(base.IndexEntry.ToString())), ErrorCategory.InvalidArgument, this.Identity);
					}
					break;
				case RemoveRequest<TIdentity>.RequestCondition.MdbDown:
					base.WriteVerbose(Strings.RequestOnInaccessibleDatabase);
					this.CleanupIndexEntries();
					break;
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06007945 RID: 31045 RVA: 0x001EE608 File Offset: 0x001EC808
		protected override void ModifyRequest(TransactionalRequestJob requestJob)
		{
			this.mdbGuid = requestJob.WorkItemQueueMdb.ObjectGuid;
			if (requestJob.TargetUser != null)
			{
				requestJob.DomainControllerToUpdate = requestJob.TargetUser.OriginatingServer;
			}
			else if (requestJob.SourceUser != null)
			{
				requestJob.DomainControllerToUpdate = requestJob.SourceUser.OriginatingServer;
			}
			if (!requestJob.CancelRequest)
			{
				requestJob.CancelRequest = true;
				base.WriteVerbose(Strings.MarkingMoveJobAsCanceled);
				ReportData reportData = new ReportData(requestJob.RequestGuid, requestJob.ReportVersion);
				ConnectivityRec connectivityRec = new ConnectivityRec(ServerKind.Cmdlet, VersionInformation.MRS);
				reportData.Append(MrsStrings.ReportRequestRemoved(base.ExecutingUserIdentity), connectivityRec);
				reportData.Flush(base.RJProvider.SystemMailbox);
			}
		}

		// Token: 0x06007946 RID: 31046 RVA: 0x001EE6B8 File Offset: 0x001EC8B8
		protected override void PostSaveAction()
		{
			if (!base.ParameterSetName.Equals("MigrationRequestQueue") && this.requestCondition == RemoveRequest<TIdentity>.RequestCondition.None && this.DataObject != null)
			{
				RequestTaskHelper.TickleMRS(this.DataObject, MoveRequestNotification.Canceled, this.mdbGuid, base.ConfigSession, base.UnreachableMrsServers);
			}
		}

		// Token: 0x06007947 RID: 31047 RVA: 0x001EE708 File Offset: 0x001EC908
		protected override ADUser ResolveADUser(ADObjectId userId)
		{
			return RequestTaskHelper.ResolveADUser(base.WriteableSession, base.GCSession, base.ServerSettings, new UserIdParameter(userId), base.OptionalIdentityData, base.DomainController, new DataAccessHelper.CategorizedGetDataObjectDelegate(base.GetDataObject<ADUser>), new Task.TaskVerboseLoggingDelegate(base.WriteVerbose), new Task.ErrorLoggerDelegate(base.WriteError), true);
		}

		// Token: 0x06007948 RID: 31048 RVA: 0x001EE768 File Offset: 0x001EC968
		private void HandleMdbDownException(Exception e)
		{
			if (base.ParameterSetName.Equals("Identity"))
			{
				MrsTracer.Cmdlet.Error("MailboxDatabase connection error when trying to read Request '{0}'.  Error details: {1}.", new object[]
				{
					this.Identity,
					CommonUtils.FullExceptionMessage(e)
				});
				this.requestCondition = RemoveRequest<TIdentity>.RequestCondition.MdbDown;
			}
			if (base.ParameterSetName.Equals("MigrationRequestQueue"))
			{
				MrsTracer.Cmdlet.Error("MailboxDatabase connection error when trying to read the Request from database '{0}'.  Error details: {1}.", new object[]
				{
					this.requestQueueName,
					CommonUtils.FullExceptionMessage(e)
				});
				base.WriteError(new DatabaseConnectionTransientException(this.requestQueueName), ErrorCategory.InvalidArgument, this.RequestQueue);
			}
		}

		// Token: 0x06007949 RID: 31049 RVA: 0x001EE81C File Offset: 0x001ECA1C
		private void CleanupIndexEntries()
		{
			foreach (IRequestIndexEntry requestIndexEntry in this.indexEntries)
			{
				base.WriteVerbose(Strings.RemovingIndexEntry(requestIndexEntry.ToString()));
				base.RJProvider.IndexProvider.Delete(requestIndexEntry);
			}
			TransactionalRequestJob dataObject = this.DataObject;
			if (dataObject != null)
			{
				dataObject.RemoveAsyncNotification();
			}
		}

		// Token: 0x04003C49 RID: 15433
		public const string ParameterRequestQueue = "RequestQueue";

		// Token: 0x04003C4A RID: 15434
		public const string ParameterRequestGuid = "RequestGuid";

		// Token: 0x04003C4B RID: 15435
		public const string RequestQueueSet = "MigrationRequestQueue";

		// Token: 0x04003C4C RID: 15436
		private Guid mdbGuid;

		// Token: 0x04003C4D RID: 15437
		private RemoveRequest<TIdentity>.RequestCondition requestCondition;

		// Token: 0x04003C4E RID: 15438
		private IRequestIndexEntry brokenIndexEntry;

		// Token: 0x04003C4F RID: 15439
		private List<IRequestIndexEntry> indexEntries;

		// Token: 0x04003C50 RID: 15440
		private string validationMessageString;

		// Token: 0x04003C51 RID: 15441
		private string requestQueueName;

		// Token: 0x02000C68 RID: 3176
		private enum RequestCondition
		{
			// Token: 0x04003C53 RID: 15443
			None,
			// Token: 0x04003C54 RID: 15444
			FailedValidation,
			// Token: 0x04003C55 RID: 15445
			IndexEntryMissingData,
			// Token: 0x04003C56 RID: 15446
			MissingRJ,
			// Token: 0x04003C57 RID: 15447
			Completed,
			// Token: 0x04003C58 RID: 15448
			MdbDown
		}
	}
}
