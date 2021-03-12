using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020001F6 RID: 502
	[Serializable]
	public class RequestStatisticsBase : RequestJobBase
	{
		// Token: 0x0600157D RID: 5501 RVA: 0x00030135 File Offset: 0x0002E335
		public RequestStatisticsBase()
		{
			this.report = null;
			this.positionInQueue = LocalizedString.Empty;
		}

		// Token: 0x0600157E RID: 5502 RVA: 0x0003014F File Offset: 0x0002E34F
		internal RequestStatisticsBase(SimpleProviderPropertyBag propertyBag) : base(propertyBag)
		{
			this.report = null;
			this.positionInQueue = LocalizedString.Empty;
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x0600157F RID: 5503 RVA: 0x0003016C File Offset: 0x0002E36C
		public virtual ByteQuantifiedSize? BytesTransferred
		{
			get
			{
				if (base.ProgressTracker != null)
				{
					return new ByteQuantifiedSize?(new ByteQuantifiedSize(base.ProgressTracker.BytesTransferred));
				}
				return null;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001580 RID: 5504 RVA: 0x000301A0 File Offset: 0x0002E3A0
		public virtual ByteQuantifiedSize? BytesTransferredPerMinute
		{
			get
			{
				if (base.ProgressTracker != null)
				{
					return new ByteQuantifiedSize?(new ByteQuantifiedSize(base.ProgressTracker.BytesPerMinute));
				}
				return null;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001581 RID: 5505 RVA: 0x000301D4 File Offset: 0x0002E3D4
		public virtual ulong? ItemsTransferred
		{
			get
			{
				if (base.ProgressTracker != null)
				{
					return new ulong?(base.ProgressTracker.ItemsTransferred);
				}
				return null;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001582 RID: 5506 RVA: 0x00030203 File Offset: 0x0002E403
		// (set) Token: 0x06001583 RID: 5507 RVA: 0x0003020B File Offset: 0x0002E40B
		public virtual LocalizedString PositionInQueue
		{
			get
			{
				return this.positionInQueue;
			}
			internal set
			{
				this.positionInQueue = value;
			}
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x06001584 RID: 5508 RVA: 0x00030214 File Offset: 0x0002E414
		// (set) Token: 0x06001585 RID: 5509 RVA: 0x0003021C File Offset: 0x0002E41C
		public virtual Report Report
		{
			get
			{
				return this.report;
			}
			internal set
			{
				this.report = value;
			}
		}

		// Token: 0x06001586 RID: 5510 RVA: 0x00030225 File Offset: 0x0002E425
		internal virtual void UpdateThroughputFromMoveRequestInfo(MoveRequestInfo moveRequestInfo)
		{
			if (moveRequestInfo != null && this.IsRequestActive())
			{
				base.ProgressTracker = moveRequestInfo.ProgressTracker;
				base.BadItemsEncountered = moveRequestInfo.BadItemsEncountered;
				base.PercentComplete = moveRequestInfo.PercentComplete;
			}
		}

		// Token: 0x06001587 RID: 5511 RVA: 0x00030256 File Offset: 0x0002E456
		private bool IsRequestActive()
		{
			return base.IdleTime < TimeSpan.FromMinutes(60.0) && base.RequestJobState == JobProcessingState.InProgress && (base.Status == RequestStatus.InProgress || base.Status == RequestStatus.CompletionInProgress);
		}

		// Token: 0x06001588 RID: 5512 RVA: 0x00030294 File Offset: 0x0002E494
		internal virtual void UpdateMessageFromMoveRequestInfo(MoveRequestInfo moveRequestInfo)
		{
			if (moveRequestInfo != null && !moveRequestInfo.Message.IsEmpty)
			{
				base.Message = (base.Message.IsEmpty ? moveRequestInfo.Message : ServerStrings.CompositeError(base.Message, moveRequestInfo.Message));
			}
		}

		// Token: 0x06001589 RID: 5513 RVA: 0x000302E4 File Offset: 0x0002E4E4
		internal void PopulateDiagnosticInfo(RequestStatisticsDiagnosticArgument arguments, string jobPickupFailureMessage)
		{
			RequestJobDiagnosticInfoXML requestJobDiagnosticInfoXML = new RequestJobDiagnosticInfoXML
			{
				PoisonCount = base.PoisonCount,
				LastPickupTime = (base.LastPickupTime ?? DateTime.MinValue),
				IsCanceled = base.CancelRequest,
				RetryCount = base.RetryCount,
				TotalRetryCount = base.TotalRetryCount,
				DomainController = base.DomainControllerToUpdate,
				SkippedItems = base.SkippedItemCounts,
				FailureHistory = base.FailureHistory
			};
			if (base.TimeTracker != null)
			{
				requestJobDiagnosticInfoXML.DoNotPickUntil = (base.TimeTracker.GetTimestamp(RequestJobTimestamp.DoNotPickUntil) ?? DateTime.MinValue);
				requestJobDiagnosticInfoXML.LastProgressTime = (base.TimeTracker.GetTimestamp(RequestJobTimestamp.LastProgressCheckpoint) ?? DateTime.MinValue);
				requestJobDiagnosticInfoXML.TimeTracker = base.TimeTracker.GetDiagnosticInfo(arguments);
			}
			if (base.ProgressTracker != null)
			{
				requestJobDiagnosticInfoXML.ProgressTracker = base.ProgressTracker.GetDiagnosticInfo(arguments);
			}
			if (!string.IsNullOrEmpty(jobPickupFailureMessage))
			{
				requestJobDiagnosticInfoXML.JobPickupFailureMessage = jobPickupFailureMessage;
			}
			base.DiagnosticInfo = requestJobDiagnosticInfoXML.Serialize(true);
		}

		// Token: 0x04000A85 RID: 2693
		private Report report;

		// Token: 0x04000A86 RID: 2694
		private LocalizedString positionInQueue;
	}
}
