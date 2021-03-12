using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000212 RID: 530
	public class RequestJobStateNode
	{
		// Token: 0x06001B3B RID: 6971 RVA: 0x00039D2C File Offset: 0x00037F2C
		static RequestJobStateNode()
		{
			RequestJobStateNode.CreateNode(RequestState.None, RequestState.None, null, null);
			RequestJobStateNode.CreateNode(RequestState.OverallMove, RequestState.None, null, null);
			RequestJobStateNode.CreateNode(RequestState.Queued, RequestState.OverallMove, null, null);
			RequestJobStateNode.CreateNode(RequestState.InProgress, RequestState.OverallMove, null, (MailboxReplicationServicePerMdbPerformanceCountersInstance pc) => pc.ActiveMovesTotal);
			RequestJobStateNode.CreateNode(RequestState.InitializingMove, RequestState.InProgress, null, null);
			RequestJobStateNode.CreateNode(RequestState.InitialSeeding, RequestState.InProgress, null, (MailboxReplicationServicePerMdbPerformanceCountersInstance pc) => pc.ActiveMovesInitialSeeding);
			RequestJobStateNode.CreateNode(RequestState.CreatingMailbox, RequestState.InitialSeeding, null, null);
			RequestJobStateNode.CreateNode(RequestState.CreatingFolderHierarchy, RequestState.InitialSeeding, null, null);
			RequestJobStateNode.CreateNode(RequestState.CreatingInitialSyncCheckpoint, RequestState.InitialSeeding, null, null);
			RequestJobStateNode.CreateNode(RequestState.LoadingMessages, RequestState.InitialSeeding, null, null);
			RequestJobStateNode.CreateNode(RequestState.CopyingMessages, RequestState.InitialSeeding, null, null);
			RequestJobStateNode.CreateNode(RequestState.Completion, RequestState.InProgress, null, (MailboxReplicationServicePerMdbPerformanceCountersInstance pc) => pc.ActiveMovesCompletion);
			RequestJobStateNode.CreateNode(RequestState.IncrementalSync, RequestState.Completion, null, null);
			RequestJobStateNode.CreateNode(RequestState.Finalization, RequestState.Completion, null, null);
			RequestJobStateNode.CreateNode(RequestState.DataReplicationWait, RequestState.Finalization, null, null);
			RequestJobStateNode.CreateNode(RequestState.ADUpdate, RequestState.Finalization, null, null);
			RequestJobStateNode.CreateNode(RequestState.Cleanup, RequestState.Completion, null, null);
			RequestJobStateNode.CreateNode(RequestState.Stalled, RequestState.InProgress, (MDBPerfCounterHelper h) => h.StallsTotal, (MailboxReplicationServicePerMdbPerformanceCountersInstance pc) => pc.ActiveMovesStalledTotal);
			RequestJobStateNode.CreateNode(RequestState.StalledDueToHA, RequestState.Stalled, (MDBPerfCounterHelper h) => h.StallsHA, (MailboxReplicationServicePerMdbPerformanceCountersInstance pc) => pc.ActiveMovesStalledHA);
			RequestJobStateNode.CreateNode(RequestState.StalledDueToCI, RequestState.Stalled, (MDBPerfCounterHelper h) => h.StallsCI, (MailboxReplicationServicePerMdbPerformanceCountersInstance pc) => pc.ActiveMovesStalledCI);
			RequestJobStateNode.CreateNode(RequestState.StalledDueToMailboxLock, RequestState.Stalled, null, null);
			RequestJobStateNode.CreateNode(RequestState.StalledDueToWriteThrottle, RequestState.Stalled, null, null);
			RequestJobStateNode.CreateNode(RequestState.StalledDueToReadThrottle, RequestState.Stalled, null, null);
			RequestJobStateNode.CreateNode(RequestState.StalledDueToReadCpu, RequestState.Stalled, null, null);
			RequestJobStateNode.CreateNode(RequestState.StalledDueToWriteCpu, RequestState.Stalled, null, null);
			RequestJobStateNode.CreateNode(RequestState.StalledDueToReadUnknown, RequestState.Stalled, null, null);
			RequestJobStateNode.CreateNode(RequestState.StalledDueToWriteUnknown, RequestState.Stalled, null, null);
			RequestJobStateNode.CreateNode(RequestState.TransientFailure, RequestState.InProgress, (MDBPerfCounterHelper h) => h.TransientTotal, (MailboxReplicationServicePerMdbPerformanceCountersInstance pc) => pc.ActiveMovesTransientFailures);
			RequestJobStateNode.CreateNode(RequestState.NetworkFailure, RequestState.TransientFailure, (MDBPerfCounterHelper h) => h.NetworkFailures, (MailboxReplicationServicePerMdbPerformanceCountersInstance pc) => pc.ActiveMovesNetworkFailures);
			RequestJobStateNode.CreateNode(RequestState.MDBOffline, RequestState.TransientFailure, null, (MailboxReplicationServicePerMdbPerformanceCountersInstance pc) => pc.ActiveMovesMDBOffline);
			RequestJobStateNode.CreateNode(RequestState.ProxyBackoff, RequestState.TransientFailure, (MDBPerfCounterHelper h) => h.ProxyBackoff, null);
			RequestJobStateNode.CreateNode(RequestState.ServerBusyBackoff, RequestState.TransientFailure, null, null);
			RequestJobStateNode.CreateNode(RequestState.Suspended, RequestState.OverallMove, null, null);
			RequestJobStateNode.CreateNode(RequestState.AutoSuspended, RequestState.Suspended, null, null);
			RequestJobStateNode.CreateNode(RequestState.Relinquished, RequestState.OverallMove, null, null);
			RequestJobStateNode.CreateNode(RequestState.RelinquishedMDBFailover, RequestState.Relinquished, null, null);
			RequestJobStateNode.CreateNode(RequestState.RelinquishedDataGuarantee, RequestState.Relinquished, null, null);
			RequestJobStateNode.CreateNode(RequestState.RelinquishedCIStall, RequestState.Relinquished, null, null);
			RequestJobStateNode.CreateNode(RequestState.RelinquishedHAStall, RequestState.Relinquished, null, null);
			RequestJobStateNode.CreateNode(RequestState.RelinquishedWlmStall, RequestState.Relinquished, null, null);
			RequestJobStateNode.CreateNode(RequestState.Failed, RequestState.OverallMove, (MDBPerfCounterHelper h) => h.FailTotal, null);
			RequestJobStateNode.CreateNode(RequestState.FailedBadItemLimit, RequestState.Failed, (MDBPerfCounterHelper h) => h.FailBadItemLimit, null);
			RequestJobStateNode.CreateNode(RequestState.FailedNetwork, RequestState.Failed, (MDBPerfCounterHelper h) => h.FailNetwork, null);
			RequestJobStateNode.CreateNode(RequestState.FailedStallDueToCI, RequestState.Failed, (MDBPerfCounterHelper h) => h.FailStallCI, null);
			RequestJobStateNode.CreateNode(RequestState.FailedStallDueToHA, RequestState.Failed, (MDBPerfCounterHelper h) => h.FailStallHA, null);
			RequestJobStateNode.CreateNode(RequestState.FailedMAPI, RequestState.Failed, (MDBPerfCounterHelper h) => h.FailMAPI, null);
			RequestJobStateNode.CreateNode(RequestState.FailedOther, RequestState.Failed, (MDBPerfCounterHelper h) => h.FailOther, null);
			RequestJobStateNode.CreateNode(RequestState.FailedStuck, RequestState.Failed, (MDBPerfCounterHelper h) => h.FailOther, null);
			RequestJobStateNode.CreateNode(RequestState.Completed, RequestState.None, (MDBPerfCounterHelper h) => h.Completed, null);
			RequestJobStateNode.CreateNode(RequestState.CompletedWithWarnings, RequestState.Completed, (MDBPerfCounterHelper h) => h.CompletedWithWarnings, null);
			RequestJobStateNode.CreateNode(RequestState.Canceled, RequestState.None, (MDBPerfCounterHelper h) => h.Canceled, null);
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x0003A240 File Offset: 0x00038440
		private RequestJobStateNode(RequestState mrState, RequestState mrParent, GetCountRatePerfCounterDelegate getCountRatePerfCounter, GetPerfCounterDelegate getActivePerfCounter)
		{
			this.Children = new List<RequestJobStateNode>();
			this.MRState = mrState;
			RequestJobStateNode.states[mrState] = this;
			this.Parent = ((mrParent != RequestState.None) ? RequestJobStateNode.states[mrParent] : null);
			if (this.Parent != null)
			{
				this.Parent.Children.Add(this);
			}
			else
			{
				RequestJobStateNode.RootStates.Add(this);
			}
			this.GetCountRatePerfCounter = getCountRatePerfCounter;
			this.GetActivePerfCounter = getActivePerfCounter;
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06001B3D RID: 6973 RVA: 0x0003A2BD File Offset: 0x000384BD
		// (set) Token: 0x06001B3E RID: 6974 RVA: 0x0003A2C4 File Offset: 0x000384C4
		public static List<RequestJobStateNode> RootStates { get; private set; } = new List<RequestJobStateNode>();

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06001B3F RID: 6975 RVA: 0x0003A2CC File Offset: 0x000384CC
		// (set) Token: 0x06001B40 RID: 6976 RVA: 0x0003A2D4 File Offset: 0x000384D4
		public RequestState MRState { get; private set; }

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06001B41 RID: 6977 RVA: 0x0003A2DD File Offset: 0x000384DD
		// (set) Token: 0x06001B42 RID: 6978 RVA: 0x0003A2E5 File Offset: 0x000384E5
		public RequestJobStateNode Parent { get; private set; }

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06001B43 RID: 6979 RVA: 0x0003A2EE File Offset: 0x000384EE
		// (set) Token: 0x06001B44 RID: 6980 RVA: 0x0003A2F6 File Offset: 0x000384F6
		public List<RequestJobStateNode> Children { get; private set; }

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06001B45 RID: 6981 RVA: 0x0003A2FF File Offset: 0x000384FF
		// (set) Token: 0x06001B46 RID: 6982 RVA: 0x0003A307 File Offset: 0x00038507
		internal GetCountRatePerfCounterDelegate GetCountRatePerfCounter { get; private set; }

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06001B47 RID: 6983 RVA: 0x0003A310 File Offset: 0x00038510
		// (set) Token: 0x06001B48 RID: 6984 RVA: 0x0003A318 File Offset: 0x00038518
		internal GetPerfCounterDelegate GetActivePerfCounter { get; private set; }

		// Token: 0x06001B49 RID: 6985 RVA: 0x0003A324 File Offset: 0x00038524
		public static RequestJobStateNode GetState(RequestState mrState)
		{
			RequestJobStateNode result;
			if (RequestJobStateNode.states.TryGetValue(mrState, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06001B4A RID: 6986 RVA: 0x0003A344 File Offset: 0x00038544
		public static bool RequestStateIs(RequestState currentState, RequestState stateToCheck)
		{
			for (RequestJobStateNode requestJobStateNode = RequestJobStateNode.GetState(currentState); requestJobStateNode != null; requestJobStateNode = requestJobStateNode.Parent)
			{
				if (requestJobStateNode.MRState == stateToCheck)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001B4B RID: 6987 RVA: 0x0003A370 File Offset: 0x00038570
		private static void CreateNode(RequestState mrState, RequestState mrParent, GetCountRatePerfCounterDelegate getCountRatePerfCounter, GetPerfCounterDelegate getActivePerfCounter)
		{
			new RequestJobStateNode(mrState, mrParent, getCountRatePerfCounter, getActivePerfCounter);
		}

		// Token: 0x06001B4C RID: 6988 RVA: 0x0003A37C File Offset: 0x0003857C
		public override string ToString()
		{
			return this.MRState.ToString();
		}

		// Token: 0x04000B99 RID: 2969
		private static Dictionary<RequestState, RequestJobStateNode> states = new Dictionary<RequestState, RequestJobStateNode>();
	}
}
