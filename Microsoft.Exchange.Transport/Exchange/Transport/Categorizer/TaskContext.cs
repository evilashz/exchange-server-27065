using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Diagnostics.Components.Transport;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Transport.MessageDepot;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001C5 RID: 453
	internal class TaskContext : IDisposable
	{
		// Token: 0x060014B0 RID: 5296 RVA: 0x00053178 File Offset: 0x00051378
		internal TaskContext(int stage, TransportMailItem subjectMailItem, int latestMimeVersion, WeakReference lastKnownMimeDocument, Job job, IMExSession mexSession, AgentLatencyTracker agentLatencyTracker, AcceptedDomainCollection acceptedDomains)
		{
			this.Stage = stage;
			this.subjectTransportMailItem = subjectMailItem;
			this.latestMimeVersion = latestMimeVersion;
			this.lastKnownMimeDocument = lastKnownMimeDocument;
			this.job = job;
			if (stage >= this.job.Stages.Count)
			{
				throw new ArgumentOutOfRangeException("stage");
			}
			if (mexSession == null && agentLatencyTracker != null)
			{
				throw new InvalidOperationException("Non-null agentLatencyTracker for a null MEx session");
			}
			if (mexSession != null && agentLatencyTracker == null)
			{
				throw new InvalidOperationException("Null agentLatencyTracker for a non-null MEx session");
			}
			if (acceptedDomains == null)
			{
				throw new ArgumentNullException("acceptedDomains");
			}
			this.mexSession = mexSession;
			this.agentLatencyTracker = agentLatencyTracker;
			this.acceptedDomains = acceptedDomains;
			if (mexSession == null)
			{
				this.mexSession = MExEvents.GetExecutionContext(subjectMailItem, this.acceptedDomains, new Action(this.TrackAsyncMessage), new Action(this.TrackAsyncMessageCompleted), new Func<bool>(this.SavePoisonContext));
				this.agentLatencyTracker = new AgentLatencyTracker(this.mexSession);
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x00053271 File Offset: 0x00051471
		// (set) Token: 0x060014B2 RID: 5298 RVA: 0x00053279 File Offset: 0x00051479
		public TaskContext FriendNextTaskContext
		{
			get
			{
				return this.nextTaskContext;
			}
			set
			{
				this.nextTaskContext = value;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x00053282 File Offset: 0x00051482
		internal IMExSession MexSession
		{
			get
			{
				return this.mexSession;
			}
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060014B4 RID: 5300 RVA: 0x0005328A File Offset: 0x0005148A
		internal AgentLatencyTracker AgentLatencyTracker
		{
			get
			{
				return this.agentLatencyTracker;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x00053292 File Offset: 0x00051492
		internal TransportMailItem SubjectMailItem
		{
			get
			{
				return this.subjectTransportMailItem;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060014B6 RID: 5302 RVA: 0x0005329A File Offset: 0x0005149A
		// (set) Token: 0x060014B7 RID: 5303 RVA: 0x000532A2 File Offset: 0x000514A2
		internal bool MessageDeferred
		{
			get
			{
				return this.messageDeferred;
			}
			set
			{
				this.messageDeferred = value;
				if (this.messageDeferred)
				{
					this.job.MarkDeferred(this.SubjectMailItem);
				}
			}
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060014B8 RID: 5304 RVA: 0x000532C4 File Offset: 0x000514C4
		// (set) Token: 0x060014B9 RID: 5305 RVA: 0x000532CC File Offset: 0x000514CC
		internal TimeSpan DeferTime
		{
			get
			{
				return this.deferTime;
			}
			set
			{
				this.deferTime = value;
			}
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x000532D5 File Offset: 0x000514D5
		public static void ReleaseItem(TransportMailItem mailItem)
		{
			Job.ReleaseItem(mailItem);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x000532E0 File Offset: 0x000514E0
		public TaskCompletion Invoke()
		{
			TaskCompletion result;
			using (new MailItemTraceFilter(this.subjectTransportMailItem))
			{
				this.SavePoisonContext();
				this.BeginTrackStageLatency(this.subjectTransportMailItem);
				if (this.subjectTransportMailItem.ActivityScope != null)
				{
					ActivityContext.SetThreadScope(this.subjectTransportMailItem.ActivityScope);
				}
				TaskCompletion taskCompletion = TaskCompletion.Completed;
				try
				{
					taskCompletion = this.job.Stages[this.Stage].Handler(this.subjectTransportMailItem, this);
				}
				catch (Exception e)
				{
					if (!Components.CategorizerComponent.HandleComponentException(e, this))
					{
						throw;
					}
				}
				if (taskCompletion == TaskCompletion.Completed)
				{
					this.TaskCompletedSync();
				}
				result = taskCompletion;
			}
			return result;
		}

		// Token: 0x060014BC RID: 5308 RVA: 0x000533A4 File Offset: 0x000515A4
		public void TaskCompletedSync()
		{
			this.TaskCompleted(false);
		}

		// Token: 0x060014BD RID: 5309 RVA: 0x000533AD File Offset: 0x000515AD
		public void TaskCompletedAsync()
		{
			this.TaskCompleted(true);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x000533B6 File Offset: 0x000515B6
		public void TaskRetired()
		{
			this.Dispose();
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x000533BE File Offset: 0x000515BE
		public bool TryGetDeferToken(TransportMailItem mailItem, out AcquireToken deferToken)
		{
			return this.job.TryGetDeferToken(mailItem, out deferToken);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x000533D0 File Offset: 0x000515D0
		public void ChainItemToNext(TransportMailItem transportMailItem)
		{
			if (this.completed)
			{
				throw new InvalidOperationException("Cannot invoke ChainToNext for a completed task");
			}
			ExTraceGlobals.SchedulerTracer.TraceDebug<long>((long)this.GetHashCode(), "ChainItemToNext (msgId={0})", transportMailItem.RecordId);
			this.EndTrackStageLatency(transportMailItem);
			ActivityContext.ClearThreadScope();
			this.job.EnqueuePendingTask(this.Stage + 1, transportMailItem, this.latestMimeVersion, this.lastKnownMimeDocument, this.mexSession, this.agentLatencyTracker, this.acceptedDomains);
			this.mexSession = null;
			this.agentLatencyTracker = null;
		}

		// Token: 0x060014C1 RID: 5313 RVA: 0x00053458 File Offset: 0x00051658
		public TransportMailItem ForkItem(TransportMailItem continueTransportMailItem, IList<MailRecipient> continueRecipients)
		{
			if (continueRecipients.Count == 0)
			{
				ExTraceGlobals.SchedulerTracer.TraceDebug((long)this.GetHashCode(), "0 continue recipients to Fork.");
				return null;
			}
			List<MailRecipient> list = new List<MailRecipient>();
			MailRecipientCollection recipients = continueTransportMailItem.Recipients;
			for (int i = 0; i < recipients.Count; i++)
			{
				MailRecipient mailRecipient = recipients[i];
				if (mailRecipient.IsActive && !continueRecipients.Contains(mailRecipient))
				{
					list.Add(mailRecipient);
				}
			}
			if (list.Count == 0)
			{
				ExTraceGlobals.SchedulerTracer.TraceDebug((long)this.GetHashCode(), "0 defer recipients to Fork.");
				return null;
			}
			TransportMailItem transportMailItem = continueTransportMailItem.NewCloneWithoutRecipients();
			foreach (MailRecipient mailRecipient2 in list)
			{
				mailRecipient2.MoveTo(transportMailItem);
			}
			transportMailItem.CommitLazy();
			this.agentLatencyTracker.EndTrackingCurrentEvent(transportMailItem.LatencyTracker);
			this.ChainItemToSelf(transportMailItem, this.mexSession);
			return transportMailItem;
		}

		// Token: 0x060014C2 RID: 5314 RVA: 0x0005355C File Offset: 0x0005175C
		internal TransportMailItemWrapper CreatePublicWrapper(bool canAddRecipients)
		{
			if (this.currentTransportMailItemWrapper != null)
			{
				throw new InvalidOperationException("Can not create public TransportMailItemWrapper while a wrapper still exists");
			}
			this.currentTransportMailItemWrapper = new TransportMailItemWrapper(this.subjectTransportMailItem, this.mexSession, canAddRecipients);
			return this.currentTransportMailItemWrapper;
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x0005358F File Offset: 0x0005178F
		public void ClosePublicWrapper()
		{
			if (this.currentTransportMailItemWrapper == null)
			{
				throw new InvalidOperationException("No public TransportMailItemWrapper exists to close");
			}
			this.currentTransportMailItemWrapper.CloseWrapper();
			this.currentTransportMailItemWrapper = null;
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x000535B8 File Offset: 0x000517B8
		public void ChainItemToSelf(TransportMailItem transportMailItem, IMExSession mexSession)
		{
			if (this.completed)
			{
				throw new InvalidOperationException("Cannot invoke ChainItemToSelf for a completed task");
			}
			ExTraceGlobals.SchedulerTracer.TraceDebug<long>((long)this.GetHashCode(), "ChainItemToSelf (msgId={0})", transportMailItem.RecordId);
			this.EndTrackStageLatency(transportMailItem);
			ActivityContext.ClearThreadScope();
			IMExSession session = null;
			AgentLatencyTracker agentLatencyTracker = null;
			if (mexSession != null)
			{
				session = MExEvents.CloneExecutionContext(mexSession);
				agentLatencyTracker = new AgentLatencyTracker(session);
			}
			Components.CategorizerComponent.MailItemBifurcatedInCategorizer(transportMailItem);
			this.job.EnqueuePendingTask(this.Stage, transportMailItem, 0, null, session, agentLatencyTracker, this.acceptedDomains);
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x0005363C File Offset: 0x0005183C
		public void ChainItemToSelf(TransportMailItem transportMailItem)
		{
			this.ChainItemToSelf(transportMailItem, this.mexSession);
		}

		// Token: 0x060014C6 RID: 5318 RVA: 0x0005364C File Offset: 0x0005184C
		public void BeginTrackStageLatency(TransportMailItem mailItem)
		{
			if (this.job.Stages[this.Stage].LatencyComponent != LatencyComponent.None)
			{
				LatencyTracker.BeginTrackLatency(this.job.Stages[this.Stage].LatencyComponent, mailItem.LatencyTracker);
			}
		}

		// Token: 0x060014C7 RID: 5319 RVA: 0x0005369C File Offset: 0x0005189C
		public void EndTrackStageLatency(TransportMailItem mailItem)
		{
			if (this.job.Stages[this.Stage].LatencyComponent != LatencyComponent.None)
			{
				LatencyTracker.EndTrackLatency(this.job.Stages[this.Stage].LatencyComponent, mailItem.LatencyTracker);
				return;
			}
			if (this.agentLatencyTracker != null)
			{
				this.agentLatencyTracker.EndTrackingCurrentEvent(mailItem.LatencyTracker);
			}
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00053707 File Offset: 0x00051907
		public void SaveMimeVersion(TransportMailItem mailItem)
		{
			this.latestMimeVersion = mailItem.MimeDocument.Version;
			this.lastKnownMimeDocument = new WeakReference(mailItem.MimeDocument);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0005372C File Offset: 0x0005192C
		public void PromoteHeadersIfChanged(TransportMailItem mailItem)
		{
			object objA = (this.lastKnownMimeDocument == null) ? null : this.lastKnownMimeDocument.Target;
			if (!object.ReferenceEquals(objA, mailItem.MimeDocument) || this.latestMimeVersion != mailItem.MimeDocument.Version)
			{
				this.SaveMimeVersion(mailItem);
				mailItem.UpdateCachedHeaders();
			}
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0005377E File Offset: 0x0005197E
		private void TaskCompleted(bool async)
		{
			if (this.completed)
			{
				throw new InvalidOperationException("Cannot call Completed more than one for a Task");
			}
			this.completed = true;
			this.Dispose();
			this.job.RunningTaskCompleted(this, async);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x000537AD File Offset: 0x000519AD
		private bool SavePoisonContext()
		{
			return TransportMailItem.SetPoisonContext(this.subjectTransportMailItem.RecordId, this.subjectTransportMailItem.InternetMessageId, MessageProcessingSource.Categorizer);
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x000537CB File Offset: 0x000519CB
		private void TrackAsyncMessage()
		{
			TransportMailItem.TrackAsyncMessage(this.subjectTransportMailItem.InternetMessageId);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x000537DD File Offset: 0x000519DD
		private void TrackAsyncMessageCompleted()
		{
			TransportMailItem.TrackAsyncMessageCompleted(this.subjectTransportMailItem.InternetMessageId);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x000537F0 File Offset: 0x000519F0
		public void Dispose()
		{
			ExTraceGlobals.SchedulerTracer.TraceDebug((long)this.GetHashCode(), "Dispose TaskContext object");
			if (this.mexSession != null)
			{
				this.agentLatencyTracker.Dispose();
				this.agentLatencyTracker = null;
				MExEvents.FreeExecutionContext(this.mexSession);
				this.mexSession = null;
			}
		}

		// Token: 0x04000A73 RID: 2675
		public readonly int Stage;

		// Token: 0x04000A74 RID: 2676
		private readonly TransportMailItem subjectTransportMailItem;

		// Token: 0x04000A75 RID: 2677
		private readonly Job job;

		// Token: 0x04000A76 RID: 2678
		private IMExSession mexSession;

		// Token: 0x04000A77 RID: 2679
		private AgentLatencyTracker agentLatencyTracker;

		// Token: 0x04000A78 RID: 2680
		private AcceptedDomainCollection acceptedDomains;

		// Token: 0x04000A79 RID: 2681
		private bool messageDeferred;

		// Token: 0x04000A7A RID: 2682
		private TimeSpan deferTime;

		// Token: 0x04000A7B RID: 2683
		private TaskContext nextTaskContext;

		// Token: 0x04000A7C RID: 2684
		private bool completed;

		// Token: 0x04000A7D RID: 2685
		private TransportMailItemWrapper currentTransportMailItemWrapper;

		// Token: 0x04000A7E RID: 2686
		private int latestMimeVersion = int.MinValue;

		// Token: 0x04000A7F RID: 2687
		private WeakReference lastKnownMimeDocument;
	}
}
