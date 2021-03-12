using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200009E RID: 158
	internal class RehomeJob : LightJobBase
	{
		// Token: 0x060007F9 RID: 2041 RVA: 0x00036FAB File Offset: 0x000351AB
		public RehomeJob(Guid requestGuid, ADObjectId currentQueue, ADObjectId newQueue, MapiStore currentSystemMailbox, byte[] currentMessageId) : base(requestGuid, Guid.Empty, null, null)
		{
			this.CurrentRequestQueue = currentQueue;
			this.NewRequestQueue = newQueue;
			this.CurrentSystemMailbox = currentSystemMailbox;
			this.NewSystemMailbox = null;
			this.CurrentMessageId = currentMessageId;
			this.RehomeFailure = null;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060007FA RID: 2042 RVA: 0x00036FE7 File Offset: 0x000351E7
		// (set) Token: 0x060007FB RID: 2043 RVA: 0x00036FEF File Offset: 0x000351EF
		protected ADObjectId CurrentRequestQueue { get; set; }

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060007FC RID: 2044 RVA: 0x00036FF8 File Offset: 0x000351F8
		// (set) Token: 0x060007FD RID: 2045 RVA: 0x00037000 File Offset: 0x00035200
		protected ADObjectId NewRequestQueue { get; set; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060007FE RID: 2046 RVA: 0x00037009 File Offset: 0x00035209
		// (set) Token: 0x060007FF RID: 2047 RVA: 0x00037011 File Offset: 0x00035211
		protected MapiStore CurrentSystemMailbox { get; set; }

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000800 RID: 2048 RVA: 0x0003701A File Offset: 0x0003521A
		// (set) Token: 0x06000801 RID: 2049 RVA: 0x00037022 File Offset: 0x00035222
		protected MapiStore NewSystemMailbox { get; set; }

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x0003702B File Offset: 0x0003522B
		// (set) Token: 0x06000803 RID: 2051 RVA: 0x00037033 File Offset: 0x00035233
		protected byte[] CurrentMessageId { get; set; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x0003703C File Offset: 0x0003523C
		// (set) Token: 0x06000805 RID: 2053 RVA: 0x00037044 File Offset: 0x00035244
		protected Exception RehomeFailure { get; set; }

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000806 RID: 2054 RVA: 0x0003704D File Offset: 0x0003524D
		// (set) Token: 0x06000807 RID: 2055 RVA: 0x0003706E File Offset: 0x0003526E
		protected override Guid RequestQueueGuid
		{
			get
			{
				if (this.RehomeFailure == null)
				{
					return this.NewRequestQueue.ObjectGuid;
				}
				return this.CurrentRequestQueue.ObjectGuid;
			}
			set
			{
				base.RequestQueueGuid = value;
			}
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x00037077 File Offset: 0x00035277
		// (set) Token: 0x06000809 RID: 2057 RVA: 0x0003708E File Offset: 0x0003528E
		protected override MapiStore SystemMailbox
		{
			get
			{
				if (this.RehomeFailure == null)
				{
					return this.NewSystemMailbox;
				}
				return this.CurrentSystemMailbox;
			}
			set
			{
				base.SystemMailbox = value;
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x00037097 File Offset: 0x00035297
		// (set) Token: 0x0600080B RID: 2059 RVA: 0x000370A9 File Offset: 0x000352A9
		protected override byte[] MessageId
		{
			get
			{
				if (this.RehomeFailure == null)
				{
					return null;
				}
				return base.MessageId;
			}
			set
			{
				base.MessageId = value;
			}
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x00037630 File Offset: 0x00035830
		public override void Run()
		{
			Guid currentQueueGuid = this.CurrentRequestQueue.ObjectGuid;
			Guid newQueueGuid = this.NewRequestQueue.ObjectGuid;
			RequestJobObjectId currentId = new RequestJobObjectId(base.RequestGuid, currentQueueGuid, this.MessageId);
			RequestJobObjectId newId = new RequestJobObjectId(base.RequestGuid, newQueueGuid, null);
			CommonUtils.CatchKnownExceptions(delegate
			{
				DatabaseInformation databaseInformation = MapiUtils.FindServerForMdb(this.NewRequestQueue, null, null, FindServerFlags.None);
				if (databaseInformation.ServerVersion < Server.E15MinVersion)
				{
					throw new UnsupportedRehomeTargetVersionPermanentException(newQueueGuid.ToString(), new ServerVersion(databaseInformation.ServerVersion).ToString());
				}
				this.NewSystemMailbox = MapiUtils.GetSystemMailbox(newQueueGuid);
				using (RequestJobProvider currentQueueProvider = new RequestJobProvider(currentQueueGuid, this.CurrentSystemMailbox))
				{
					using (RequestJobProvider newQueueProvider = new RequestJobProvider(newQueueGuid, this.NewSystemMailbox))
					{
						using (TransactionalRequestJob requestJob = (TransactionalRequestJob)currentQueueProvider.Read<TransactionalRequestJob>(currentId))
						{
							if (requestJob != null)
							{
								RequestJobProvider origProvider = requestJob.Provider;
								MoveObjectInfo<RequestJobXML> origMO = requestJob.MoveObject;
								ReportData currentReport = new ReportData(requestJob.IdentifyingGuid, requestJob.ReportVersion);
								currentReport.Load(currentQueueProvider.SystemMailbox);
								ReportData newReport = new ReportData(currentReport.IdentifyingGuid, requestJob.ReportVersion);
								newReport.Append(currentReport.Entries);
								try
								{
									requestJob.Provider = null;
									requestJob.MoveObject = null;
									requestJob.RequestQueue = this.NewRequestQueue;
									requestJob.Identity = newId;
									requestJob.OriginatingMDBGuid = Guid.Empty;
									requestJob.RehomeRequest = false;
									if (requestJob.IndexEntries != null)
									{
										foreach (IRequestIndexEntry requestIndexEntry in requestJob.IndexEntries)
										{
											requestIndexEntry.StorageMDB = this.NewRequestQueue;
										}
									}
									newQueueProvider.Save(requestJob);
									CommonUtils.CatchKnownExceptions(delegate
									{
										newReport.Append(MrsStrings.ReportJobRehomed(currentQueueGuid.ToString(), newQueueGuid.ToString()));
										newReport.Flush(newQueueProvider.SystemMailbox);
									}, null);
									CommonUtils.CatchKnownExceptions(delegate
									{
										requestJob.Provider = origProvider;
										requestJob.MoveObject = origMO;
										requestJob.RequestQueue = this.CurrentRequestQueue;
										requestJob.Identity = currentId;
										requestJob.OriginatingMDBGuid = currentQueueGuid;
										requestJob.RehomeRequest = true;
										MapiUtils.RetryOnObjectChanged(delegate
										{
											currentQueueProvider.Delete(requestJob);
										});
										currentReport.Delete(currentQueueProvider.SystemMailbox);
									}, null);
								}
								catch
								{
									requestJob.Provider = origProvider;
									requestJob.MoveObject = origMO;
									requestJob.RequestQueue = this.CurrentRequestQueue;
									requestJob.Identity = currentId;
									requestJob.OriginatingMDBGuid = currentQueueGuid;
									requestJob.RehomeRequest = true;
									throw;
								}
							}
						}
					}
				}
			}, delegate(Exception failure)
			{
				MrsTracer.Service.Warning("Failed to rehome request.", new object[0]);
				this.RehomeFailure = failure;
			});
			base.Run();
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x000376C4 File Offset: 0x000358C4
		protected override RequestState RelinquishAction(TransactionalRequestJob requestJob, ReportData report)
		{
			if (this.RehomeFailure == null)
			{
				return requestJob.TimeTracker.CurrentState;
			}
			string stackTrace = this.RehomeFailure.StackTrace;
			if (CommonUtils.IsTransientException(this.RehomeFailure))
			{
				this.RehomeFailure = new RehomeRequestTransientException(this.RehomeFailure);
				report.Append(MrsStrings.ReportTransientException(CommonUtils.GetFailureType(this.RehomeFailure), 0, 0), this.RehomeFailure, ReportEntryFlags.None);
				if (requestJob.Status == RequestStatus.Completed || requestJob.Status == RequestStatus.CompletedWithWarning)
				{
					requestJob.RehomeRequest = false;
					return requestJob.TimeTracker.CurrentState;
				}
				requestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.DoNotPickUntil, new DateTime?(DateTime.UtcNow + TimeSpan.FromMinutes(5.0)));
				if (CommonUtils.ExceptionIs(this.RehomeFailure, new WellKnownException[]
				{
					WellKnownException.MapiMdbOffline
				}))
				{
					return RequestState.MDBOffline;
				}
				if (CommonUtils.ExceptionIs(this.RehomeFailure, new WellKnownException[]
				{
					WellKnownException.MapiNetworkError
				}))
				{
					return RequestState.NetworkFailure;
				}
				if (CommonUtils.ExceptionIs(this.RehomeFailure, new WellKnownException[]
				{
					WellKnownException.Mapi
				}))
				{
					return RequestState.TransientFailure;
				}
				return RequestState.TransientFailure;
			}
			else
			{
				if (!(this.RehomeFailure is UnsupportedRehomeTargetVersionPermanentException))
				{
					this.RehomeFailure = new RehomeRequestPermanentException(this.RehomeFailure);
				}
				requestJob.RehomeRequest = false;
				report.Append(MrsStrings.ReportFatalException(CommonUtils.GetFailureType(this.RehomeFailure)), this.RehomeFailure, ReportEntryFlags.Fatal);
				if (requestJob.Status == RequestStatus.Completed || requestJob.Status == RequestStatus.CompletedWithWarning)
				{
					return requestJob.TimeTracker.CurrentState;
				}
				requestJob.Suspend = true;
				requestJob.Status = RequestStatus.Failed;
				requestJob.TimeTracker.SetTimestamp(RequestJobTimestamp.Failure, new DateTime?(DateTime.UtcNow));
				requestJob.FailureCode = new int?(CommonUtils.HrFromException(this.RehomeFailure));
				requestJob.FailureType = CommonUtils.GetFailureType(this.RehomeFailure);
				requestJob.FailureSide = new ExceptionSide?((requestJob.Direction == RequestDirection.Push) ? ExceptionSide.Source : ExceptionSide.Target);
				requestJob.Message = MrsStrings.MoveRequestMessageError(CommonUtils.FullExceptionMessage(this.RehomeFailure));
				if (CommonUtils.ExceptionIs(this.RehomeFailure, new WellKnownException[]
				{
					WellKnownException.MapiMdbOffline
				}))
				{
					return RequestState.FailedOther;
				}
				if (CommonUtils.ExceptionIs(this.RehomeFailure, new WellKnownException[]
				{
					WellKnownException.MapiNetworkError
				}))
				{
					return RequestState.FailedNetwork;
				}
				if (CommonUtils.ExceptionIs(this.RehomeFailure, new WellKnownException[]
				{
					WellKnownException.Mapi
				}))
				{
					return RequestState.FailedMAPI;
				}
				return RequestState.FailedOther;
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x00037927 File Offset: 0x00035B27
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.NewSystemMailbox != null)
			{
				this.NewSystemMailbox.Dispose();
				this.NewSystemMailbox = null;
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0003794D File Offset: 0x00035B4D
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RehomeJob>(this);
		}
	}
}
