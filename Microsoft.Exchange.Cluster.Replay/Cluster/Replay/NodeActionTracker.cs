using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Cluster.ActiveManagerServer;
using Microsoft.Exchange.Cluster.ClusApi;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200011E RID: 286
	internal class NodeActionTracker
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x00030C14 File Offset: 0x0002EE14
		private void CheckForNodesJoining(ITaskOutputHelper output, AmServerName nodeName, NodeAction nodeAction, TimeSpan maxWaitDurationForJoining)
		{
			if (maxWaitDurationForJoining == TimeSpan.Zero)
			{
				return;
			}
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)RegistryParameters.NodeActionDelayBetweenIterationsInSec);
			if (maxWaitDurationForJoining < timeSpan)
			{
				timeSpan = maxWaitDurationForJoining;
			}
			bool flag = true;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			for (;;)
			{
				Dictionary<AmServerName, AmNodeState> allClusterNodeStates = this.GetAllClusterNodeStates(output);
				KeyValuePair<AmServerName, AmNodeState>[] array = (from kv in allClusterNodeStates
				where kv.Value == AmNodeState.Joining
				select kv).ToArray<KeyValuePair<AmServerName, AmNodeState>>();
				if (array.Length == 0)
				{
					break;
				}
				string nodeStatesAsSingleString = this.GetNodeStatesAsSingleString(array);
				if (flag)
				{
					output.AppendLogMessage("Delaying the action '{1}' for node '{0} since some nodes are still in joining state. (Nodes: {2})", new object[]
					{
						nodeName,
						nodeAction,
						nodeStatesAsSingleString
					});
					ReplayCrimsonEvents.ClusterNodeActionBlockedDueToJoiningNodes.Log<AmServerName, NodeAction, string>(nodeName, nodeAction, nodeStatesAsSingleString);
					flag = false;
				}
				if (stopwatch.Elapsed > maxWaitDurationForJoining)
				{
					return;
				}
				Thread.Sleep(timeSpan);
			}
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00030CF4 File Offset: 0x0002EEF4
		internal void PerformNodeAction(ITaskOutputHelper output, AmServerName nodeName, NodeAction nodeAction, Action clusterAction)
		{
			TimeSpan maxWaitDurationForJoining = TimeSpan.FromSeconds((double)RegistryParameters.NodeActionNodeStateJoiningWaitDurationInSec);
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)RegistryParameters.NodeActionInProgressWaitDurationInSec);
			TimeSpan timeSpan2 = TimeSpan.FromSeconds((double)RegistryParameters.NodeActionDelayBetweenIterationsInSec);
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			this.CheckForNodesJoining(output, nodeName, nodeAction, maxWaitDurationForJoining);
			TimeSpan elapsed = stopwatch.Elapsed;
			if (timeSpan < timeSpan2)
			{
				timeSpan2 = timeSpan;
			}
			bool flag = true;
			TimeSpan inProgressDuration = TimeSpan.Zero;
			Exception exception = null;
			try
			{
				Stopwatch stopwatch2 = new Stopwatch();
				stopwatch2.Start();
				ExDateTime t = ExDateTime.Now.Add(timeSpan);
				for (;;)
				{
					List<NodeActionTracker.NodeActionInfo> list = new List<NodeActionTracker.NodeActionInfo>(15);
					lock (this.locker)
					{
						ExDateTime now = ExDateTime.Now;
						if (this.nodeActionMap.Count > 0)
						{
							foreach (NodeActionTracker.NodeActionInfo nodeActionInfo in this.nodeActionMap.Values)
							{
								if (now - nodeActionInfo.StartTime < timeSpan)
								{
									list.Add(nodeActionInfo);
								}
							}
						}
						if (list.Count == 0 || now > t)
						{
							NodeActionTracker.NodeActionInfo value = new NodeActionTracker.NodeActionInfo(nodeName, nodeAction);
							this.nodeActionMap[nodeName] = value;
							break;
						}
					}
					string nodeInfoListAsString = this.GetNodeInfoListAsString(list);
					if (flag)
					{
						output.AppendLogMessage("Blocking '{1}' action for node '{0}'. InProgress: {2}", new object[]
						{
							nodeName,
							nodeAction,
							nodeInfoListAsString
						});
						ReplayCrimsonEvents.ClusterNodeActionBlockedDueToInProgress.Log<AmServerName, NodeAction, string>(nodeName, nodeAction, nodeInfoListAsString);
						flag = false;
					}
					Thread.Sleep(timeSpan2);
				}
				inProgressDuration = stopwatch2.Elapsed;
				this.LogStatusStarting(output, nodeName, nodeAction, elapsed, inProgressDuration);
				clusterAction();
			}
			catch (Exception ex)
			{
				exception = ex;
				throw;
			}
			finally
			{
				lock (this.locker)
				{
					this.nodeActionMap.Remove(nodeName);
				}
				this.LogStatusCompleted(output, nodeName, nodeAction, exception);
			}
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x00030F70 File Offset: 0x0002F170
		private void LogStatusStarting(ITaskOutputHelper output, AmServerName nodeName, NodeAction nodeAction, TimeSpan joiningDuration, TimeSpan inProgressDuration)
		{
			string allNodesStillInList = this.GetAllNodesStillInList(nodeName);
			if (!string.IsNullOrEmpty(allNodesStillInList))
			{
				output.AppendLogMessage("There are stale node action entries that are still present when attemping action '{1}' for node '{0}'. Stale: {2}", new object[]
				{
					nodeName,
					nodeAction,
					allNodesStillInList
				});
			}
			string nodeStatesAsSingleString = this.GetNodeStatesAsSingleString(this.GetAllClusterNodeStates(output));
			output.AppendLogMessage("State of the cluster nodes before performing action '{1}' for node '{0}': {2}", new object[]
			{
				nodeName,
				nodeAction,
				nodeStatesAsSingleString
			});
			ReplayCrimsonEvents.ClusterNodeActionStarted.Log<AmServerName, NodeAction, TimeSpan, TimeSpan, string, string>(nodeName, nodeAction, joiningDuration, inProgressDuration, allNodesStillInList, nodeStatesAsSingleString);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x00030FF8 File Offset: 0x0002F1F8
		private void LogStatusCompleted(ITaskOutputHelper output, AmServerName nodeName, NodeAction nodeAction, Exception exception)
		{
			string allNodesStillInList = this.GetAllNodesStillInList(nodeName);
			if (!string.IsNullOrEmpty(allNodesStillInList))
			{
				output.AppendLogMessage("Stale node action entries that are still present after attemping action '{1}' for node '{0}'. Stale: {2}", new object[]
				{
					nodeName,
					nodeAction,
					allNodesStillInList
				});
			}
			string nodeStatesAsSingleString = this.GetNodeStatesAsSingleString(this.GetAllClusterNodeStates(output));
			output.AppendLogMessage("State of the cluster nodes after performing action '{1}' for node '{0}': {2}", new object[]
			{
				nodeName,
				nodeAction,
				nodeStatesAsSingleString
			});
			ReplayCrimsonEvents.ClusterNodeActionCompleted.Log<AmServerName, NodeAction, string, string, string>(nodeName, nodeAction, allNodesStillInList, nodeStatesAsSingleString, (exception != null) ? exception.Message : "<none>");
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x0003108C File Offset: 0x0002F28C
		protected virtual Dictionary<AmServerName, AmNodeState> GetAllClusterNodeStates(ITaskOutputHelper output)
		{
			Dictionary<AmServerName, AmNodeState> dictionary = new Dictionary<AmServerName, AmNodeState>(15);
			try
			{
				using (IAmCluster amCluster = ClusterFactory.Instance.Open())
				{
					foreach (IAmClusterNode amClusterNode in amCluster.EnumerateNodes())
					{
						using (amClusterNode)
						{
							AmNodeState state = amClusterNode.GetState(false);
							dictionary[amClusterNode.Name] = state;
						}
					}
				}
			}
			catch (ClusterException ex)
			{
				output.AppendLogMessage("GetAllClusterNodeStates() failed with error {0}", new object[]
				{
					ex.Message
				});
			}
			return dictionary;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00031168 File Offset: 0x0002F368
		private string GetNodeStatesAsSingleString(IEnumerable<KeyValuePair<AmServerName, AmNodeState>> nodeStates)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<AmServerName, AmNodeState> keyValuePair in nodeStates)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat("{0} => {1}", keyValuePair.Key.NetbiosName, keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x00031200 File Offset: 0x0002F400
		private string GetAllNodesStillInList(AmServerName nodeToExclude)
		{
			string result = string.Empty;
			lock (this.locker)
			{
				if (this.nodeActionMap.Count > 0)
				{
					result = this.GetNodeInfoListAsString(from nodeInfo in this.nodeActionMap.Values
					where !AmServerName.IsEqual(nodeInfo.Name, nodeToExclude)
					select nodeInfo);
				}
			}
			return result;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x00031288 File Offset: 0x0002F488
		private string GetNodeInfoListAsString(IEnumerable<NodeActionTracker.NodeActionInfo> nodeList)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (NodeActionTracker.NodeActionInfo nodeActionInfo in nodeList)
			{
				stringBuilder.AppendLine();
				stringBuilder.Append(nodeActionInfo.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400049B RID: 1179
		private object locker = new object();

		// Token: 0x0400049C RID: 1180
		private Dictionary<AmServerName, NodeActionTracker.NodeActionInfo> nodeActionMap = new Dictionary<AmServerName, NodeActionTracker.NodeActionInfo>(15);

		// Token: 0x0200011F RID: 287
		private class NodeActionInfo
		{
			// Token: 0x06000AE3 RID: 2787 RVA: 0x0003130C File Offset: 0x0002F50C
			internal NodeActionInfo(AmServerName name, NodeAction actionInProgress) : this(name, actionInProgress, ExDateTime.Now)
			{
			}

			// Token: 0x06000AE4 RID: 2788 RVA: 0x0003131B File Offset: 0x0002F51B
			internal NodeActionInfo(AmServerName name, NodeAction actionInProgress, ExDateTime startTime)
			{
				this.Name = name;
				this.ActionInProgress = actionInProgress;
				this.StartTime = startTime;
			}

			// Token: 0x1700024F RID: 591
			// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x00031338 File Offset: 0x0002F538
			// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x00031340 File Offset: 0x0002F540
			internal AmServerName Name { get; set; }

			// Token: 0x17000250 RID: 592
			// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x00031349 File Offset: 0x0002F549
			// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x00031351 File Offset: 0x0002F551
			internal NodeAction ActionInProgress { get; set; }

			// Token: 0x17000251 RID: 593
			// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x0003135A File Offset: 0x0002F55A
			// (set) Token: 0x06000AEA RID: 2794 RVA: 0x00031362 File Offset: 0x0002F562
			internal ExDateTime StartTime { get; set; }

			// Token: 0x06000AEB RID: 2795 RVA: 0x0003136C File Offset: 0x0002F56C
			public override string ToString()
			{
				return string.Format("Node: {0} ActionInProgress: {1} StartTime: {2}", this.Name.NetbiosName, this.ActionInProgress, this.StartTime.ToString("o"));
			}
		}
	}
}
