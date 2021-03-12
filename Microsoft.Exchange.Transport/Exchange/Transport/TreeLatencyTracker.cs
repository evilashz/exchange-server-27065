using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020001AC RID: 428
	internal class TreeLatencyTracker : LatencyTracker
	{
		// Token: 0x06001396 RID: 5014 RVA: 0x0004DA54 File Offset: 0x0004BC54
		internal TreeLatencyTracker()
		{
			this.root = new TreeLatencyTracker.LatencyRecordNode(121, LatencyTracker.StopwatchProvider(), null);
			this.trackedCount += 1;
			this.pendingLeaf = this.root;
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0004DAA5 File Offset: 0x0004BCA5
		public override bool SupportsTreeFormatting
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x0004DAA8 File Offset: 0x0004BCA8
		public override bool HasCompletedComponents
		{
			get
			{
				return this.hasCompletedComponents;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0004DAB0 File Offset: 0x0004BCB0
		public override bool HasPendingComponents
		{
			get
			{
				return this.root.IsPending;
			}
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0004DAC0 File Offset: 0x0004BCC0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine("TreeLatencyTracker:");
			stringBuilder.AppendLine(string.Format("too many latency: {0}", (this.tooMany != null) ? this.tooMany.Value.CalculateLatency().ToString() : "null"));
			stringBuilder.AppendLine(string.Format("unknown pending start: {0}", (this.unknownComponentPending != null) ? this.unknownComponentPending.Value.StartTime.ToString() : "null"));
			stringBuilder.AppendLine(string.Format("has completed hasCompletedComponents: {0}", this.hasCompletedComponents));
			stringBuilder.AppendLine(string.Format("tracked count: {0}", this.trackedCount));
			stringBuilder.Append("preprocess latencies :");
			bool flag = false;
			foreach (LatencyRecord latencyRecord in this.preProcessLatencies)
			{
				if (flag)
				{
					stringBuilder.Append(",");
				}
				stringBuilder.Append(string.Format("{0}:{1}", latencyRecord.ComponentShortName, latencyRecord.Latency));
				flag = true;
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(string.Format("root: {0}", (this.root == null) ? "null" : this.root.ToString()));
			stringBuilder.AppendLine(string.Format("pending leaf: {0}", (this.pendingLeaf == null) ? "null" : this.pendingLeaf.ToString()));
			return stringBuilder.ToString();
		}

		// Token: 0x0600139B RID: 5019 RVA: 0x0004DC90 File Offset: 0x0004BE90
		public override IEnumerable<LatencyRecord> GetCompletedRecords()
		{
			List<LatencyRecord> list = new List<LatencyRecord>();
			list.AddRange(this.preProcessLatencies);
			list.AddRange(this.root.GetCompletedRecords());
			if (this.tooMany != null)
			{
				list.Add(this.tooMany.Value.AsCompletedRecord());
			}
			return list;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004DCE7 File Offset: 0x0004BEE7
		public override IEnumerable<PendingLatencyRecord> GetPendingRecords()
		{
			return this.root.GetPendingRecords();
		}

		// Token: 0x0600139D RID: 5021 RVA: 0x0004DCF4 File Offset: 0x0004BEF4
		public override void AppendLatencyString(StringBuilder builder, bool useTreeFormat, bool haveTotal, bool enableHeaderFolding)
		{
			if (!this.HasCompletedComponents && !this.HasPendingComponents)
			{
				return;
			}
			int lastFoldingPoint = builder.Length;
			if (haveTotal)
			{
				builder.Append('|');
				lastFoldingPoint = LatencyFormatter.AddFolding(builder, lastFoldingPoint, enableHeaderFolding);
			}
			bool flag = false;
			foreach (LatencyRecord record in this.preProcessLatencies)
			{
				if (flag)
				{
					builder.Append('|');
				}
				LatencyFormatter.AppendLatencyRecord(builder, record, null);
				flag = true;
				lastFoldingPoint = LatencyFormatter.AddFolding(builder, lastFoldingPoint, enableHeaderFolding);
			}
			if (flag)
			{
				builder.Append('|');
			}
			lastFoldingPoint = this.root.AppendComponentLatencyString(builder, lastFoldingPoint, enableHeaderFolding, useTreeFormat);
			if (this.tooMany != null)
			{
				LatencyFormatter.AddFolding(builder, lastFoldingPoint, enableHeaderFolding);
				builder.Append('|');
				LatencyFormatter.AppendLatencyRecord(builder, this.tooMany.Value.AsCompletedRecord(), null);
			}
		}

		// Token: 0x0600139E RID: 5022 RVA: 0x0004DDE8 File Offset: 0x0004BFE8
		protected override void Complete()
		{
			this.EndTrackLatency(121, 121, LatencyTracker.StopwatchProvider(), false);
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004DE00 File Offset: 0x0004C000
		protected override LatencyTracker Clone()
		{
			TreeLatencyTracker treeLatencyTracker = new TreeLatencyTracker();
			treeLatencyTracker.root = this.root.DeepCopy(null);
			treeLatencyTracker.preProcessLatencies = new List<LatencyRecord>(this.preProcessLatencies);
			treeLatencyTracker.pendingLeaf = treeLatencyTracker.root.GetPendingLeaf();
			treeLatencyTracker.tooMany = this.tooMany;
			treeLatencyTracker.unknownComponentPending = this.unknownComponentPending;
			treeLatencyTracker.hasCompletedComponents = this.hasCompletedComponents;
			treeLatencyTracker.trackedCount = this.trackedCount;
			return treeLatencyTracker;
		}

		// Token: 0x060013A0 RID: 5024 RVA: 0x0004DE78 File Offset: 0x0004C078
		protected override void BeginTrackLatency(ushort componentId, long startTime)
		{
			if (this.unknownComponentPending != null)
			{
				TimeSpan latency = LatencyTracker.TimeSpanFromTicks(this.unknownComponentPending.Value.StartTime, startTime);
				if (LatencyTracker.ShouldTrackComponent(latency, 118))
				{
					this.pendingLeaf.AddCompletedRecord(118, latency);
				}
				this.unknownComponentPending = null;
			}
			if (this.trackedCount >= 1000)
			{
				if (this.tooMany == null)
				{
					this.tooMany = new LatencyRecordPlus?(new LatencyRecordPlus(122, startTime));
				}
				return;
			}
			this.trackedCount += 1;
			this.pendingLeaf = this.pendingLeaf.BeginTrackLatency(componentId, startTime);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0004DF20 File Offset: 0x0004C120
		protected override TimeSpan EndTrackLatency(ushort pendingComponentId, ushort trackingComponentId, long endTime, bool shouldAggregate)
		{
			TimeSpan result = TimeSpan.Zero;
			if (this.pendingLeaf == null)
			{
				TreeLatencyTracker.EventLogger.LogEvent(TransportEventLogConstants.Tuple_NullLatencyTreeLeaf, "NullPendingLeaf", new object[]
				{
					this.ToString()
				});
				return result;
			}
			TreeLatencyTracker.LatencyRecordNode parent = this.pendingLeaf;
			bool flag = false;
			bool flag2 = false;
			while (parent.HasParent)
			{
				if (parent.ComponentId == pendingComponentId)
				{
					flag2 = true;
					result = parent.Parent.CompleteChild(trackingComponentId, endTime, shouldAggregate, out flag);
					this.pendingLeaf = parent.Parent;
					this.hasCompletedComponents = true;
					break;
				}
				parent = parent.Parent;
			}
			if (object.Equals(parent, this.root) && parent.ComponentId == pendingComponentId)
			{
				result = parent.Complete(trackingComponentId, endTime);
				this.pendingLeaf = null;
				this.hasCompletedComponents = true;
				LatencyTracker.UpdatePerfCounter(trackingComponentId, (long)(result.TotalSeconds + 0.5));
				return result;
			}
			if (!flag && flag2)
			{
				base.AggregatedUnderThresholdTicks += result.Ticks;
			}
			if (object.Equals(this.pendingLeaf, this.root) && this.unknownComponentPending == null)
			{
				this.unknownComponentPending = new LatencyRecordPlus?(new LatencyRecordPlus(118, LatencyTracker.StopwatchProvider()));
			}
			LatencyTracker.UpdatePerfCounter(trackingComponentId, (long)(result.TotalSeconds + 0.5));
			return result;
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0004E074 File Offset: 0x0004C274
		protected override void TrackPreProcessLatency(ushort componentId, DateTime startTime)
		{
			TimeSpan latency = LatencyTracker.TimeProvider() - startTime;
			if (this.trackedCount >= 1000)
			{
				return;
			}
			this.preProcessLatencies.Add(new LatencyRecord(componentId, latency));
			this.trackedCount += 1;
			this.hasCompletedComponents = true;
			LatencyTracker.UpdatePerfCounter(componentId, (long)latency.TotalSeconds);
			if (this.preProcessLatencies.Count > 1)
			{
				string text = string.Join(", ", from x in this.preProcessLatencies
				select x.ComponentShortName);
				TreeLatencyTracker.EventLogger.LogEvent(TransportEventLogConstants.Tuple_MultiplePreProcessLatencies, "MultiplePreProcessLatencies", new object[]
				{
					text
				});
			}
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004E138 File Offset: 0x0004C338
		protected override void TrackExternalLatency(ushort componentId, TimeSpan latency)
		{
			if (this.trackedCount >= 1000)
			{
				return;
			}
			this.pendingLeaf.AddCompletedRecord(componentId, latency);
			this.trackedCount += 1;
			this.hasCompletedComponents = true;
			LatencyTracker.UpdatePerfCounter(componentId, (long)latency.TotalSeconds);
		}

		// Token: 0x04000A10 RID: 2576
		internal static readonly ExEventLog EventLogger = new ExEventLog(ExTraceGlobals.GeneralTracer.Category, TransportEventLog.GetEventSource());

		// Token: 0x04000A11 RID: 2577
		private List<LatencyRecord> preProcessLatencies = new List<LatencyRecord>();

		// Token: 0x04000A12 RID: 2578
		private TreeLatencyTracker.LatencyRecordNode root;

		// Token: 0x04000A13 RID: 2579
		private TreeLatencyTracker.LatencyRecordNode pendingLeaf;

		// Token: 0x04000A14 RID: 2580
		private LatencyRecordPlus? tooMany;

		// Token: 0x04000A15 RID: 2581
		private LatencyRecordPlus? unknownComponentPending;

		// Token: 0x04000A16 RID: 2582
		private bool hasCompletedComponents;

		// Token: 0x04000A17 RID: 2583
		private ushort trackedCount;

		// Token: 0x020001AD RID: 429
		private class LatencyRecordNode
		{
			// Token: 0x060013A6 RID: 5030 RVA: 0x0004E19F File Offset: 0x0004C39F
			public LatencyRecordNode(ushort componentId, long startTime, TreeLatencyTracker.LatencyRecordNode parent)
			{
				this.latencyRecord = new LatencyRecordPlus(componentId, startTime);
				this.parent = parent;
			}

			// Token: 0x060013A7 RID: 5031 RVA: 0x0004E1C6 File Offset: 0x0004C3C6
			private LatencyRecordNode(TreeLatencyTracker.LatencyRecordNode parent)
			{
				this.parent = parent;
			}

			// Token: 0x060013A8 RID: 5032 RVA: 0x0004E1E0 File Offset: 0x0004C3E0
			private LatencyRecordNode(LatencyRecordPlus latencyRecord, TreeLatencyTracker.LatencyRecordNode parent)
			{
				this.latencyRecord = latencyRecord;
				this.parent = parent;
			}

			// Token: 0x17000566 RID: 1382
			// (get) Token: 0x060013A9 RID: 5033 RVA: 0x0004E201 File Offset: 0x0004C401
			public TreeLatencyTracker.LatencyRecordNode Parent
			{
				get
				{
					return this.parent;
				}
			}

			// Token: 0x17000567 RID: 1383
			// (get) Token: 0x060013AA RID: 5034 RVA: 0x0004E209 File Offset: 0x0004C409
			public bool HasParent
			{
				get
				{
					return this.parent != null;
				}
			}

			// Token: 0x17000568 RID: 1384
			// (get) Token: 0x060013AB RID: 5035 RVA: 0x0004E217 File Offset: 0x0004C417
			public bool HasPendingChild
			{
				get
				{
					return this.pendingChild != null;
				}
			}

			// Token: 0x17000569 RID: 1385
			// (get) Token: 0x060013AC RID: 5036 RVA: 0x0004E225 File Offset: 0x0004C425
			public bool HasCompletedChild
			{
				get
				{
					return this.completedChildren.Any<TreeLatencyTracker.LatencyRecordNode>();
				}
			}

			// Token: 0x1700056A RID: 1386
			// (get) Token: 0x060013AD RID: 5037 RVA: 0x0004E232 File Offset: 0x0004C432
			public bool IsPending
			{
				get
				{
					return !this.latencyRecord.IsComplete;
				}
			}

			// Token: 0x1700056B RID: 1387
			// (get) Token: 0x060013AE RID: 5038 RVA: 0x0004E242 File Offset: 0x0004C442
			public bool IsComplete
			{
				get
				{
					return this.latencyRecord.IsComplete;
				}
			}

			// Token: 0x1700056C RID: 1388
			// (get) Token: 0x060013AF RID: 5039 RVA: 0x0004E24F File Offset: 0x0004C44F
			public ushort ComponentId
			{
				get
				{
					return this.latencyRecord.ComponentId;
				}
			}

			// Token: 0x1700056D RID: 1389
			// (get) Token: 0x060013B0 RID: 5040 RVA: 0x0004E25C File Offset: 0x0004C45C
			private ushort LastCompletedChildComponentId
			{
				get
				{
					if (this.HasCompletedChild)
					{
						return this.completedChildren.Last<TreeLatencyTracker.LatencyRecordNode>().latencyRecord.ComponentId;
					}
					return 0;
				}
			}

			// Token: 0x1700056E RID: 1390
			// (get) Token: 0x060013B1 RID: 5041 RVA: 0x0004E27D File Offset: 0x0004C47D
			private TimeSpan LastCompletedChildLatency
			{
				get
				{
					if (this.HasCompletedChild)
					{
						return this.completedChildren.Last<TreeLatencyTracker.LatencyRecordNode>().latencyRecord.CalculateLatency();
					}
					return TimeSpan.Zero;
				}
			}

			// Token: 0x060013B2 RID: 5042 RVA: 0x0004E2A4 File Offset: 0x0004C4A4
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append((LatencyComponent)this.latencyRecord.ComponentId);
				foreach (TreeLatencyTracker.LatencyRecordNode latencyRecordNode in this.completedChildren)
				{
					stringBuilder.Append("(" + latencyRecordNode.ToString() + ")");
				}
				stringBuilder.Append("[" + ((this.pendingChild != null) ? this.pendingChild.ToString() : string.Empty) + "]");
				return stringBuilder.ToString();
			}

			// Token: 0x060013B3 RID: 5043 RVA: 0x0004E360 File Offset: 0x0004C560
			public int AppendComponentLatencyString(StringBuilder builder, int lastFoldingPoint, bool enableHeaderFolding, bool useTreeFormat)
			{
				bool flag = false;
				if (this.ComponentId != 121)
				{
					flag = true;
					if (this.IsComplete)
					{
						LatencyFormatter.AppendLatencyRecord(builder, this.latencyRecord.AsCompletedRecord(), this.latencyRecord.IsImplicitlyComplete ? "INC" : string.Empty);
					}
					else
					{
						LatencyFormatter.AppendPendingLatencyRecord(builder, this.latencyRecord.AsPendingRecord(), this.CalculateLatency(LatencyTracker.StopwatchProvider()));
					}
				}
				lastFoldingPoint = LatencyFormatter.AddFolding(builder, lastFoldingPoint, enableHeaderFolding);
				bool flag2 = false;
				if (this.HasCompletedChild || this.HasPendingChild)
				{
					if (useTreeFormat && flag)
					{
						builder.Append('(');
					}
					else if (flag)
					{
						builder.Append('|');
					}
					foreach (TreeLatencyTracker.LatencyRecordNode latencyRecordNode in this.completedChildren)
					{
						if (flag2)
						{
							builder.Append('|');
						}
						lastFoldingPoint = latencyRecordNode.AppendComponentLatencyString(builder, lastFoldingPoint, enableHeaderFolding, useTreeFormat);
						flag2 = true;
					}
					if (this.pendingChild != null)
					{
						if (flag2)
						{
							builder.Append('|');
						}
						lastFoldingPoint = this.pendingChild.AppendComponentLatencyString(builder, lastFoldingPoint, enableHeaderFolding, useTreeFormat);
					}
					if (useTreeFormat && flag)
					{
						builder.Append(')');
					}
				}
				return lastFoldingPoint;
			}

			// Token: 0x060013B4 RID: 5044 RVA: 0x0004E4A0 File Offset: 0x0004C6A0
			public TreeLatencyTracker.LatencyRecordNode DeepCopy(TreeLatencyTracker.LatencyRecordNode newParent)
			{
				TreeLatencyTracker.LatencyRecordNode latencyRecordNode = new TreeLatencyTracker.LatencyRecordNode(newParent);
				List<TreeLatencyTracker.LatencyRecordNode> list = new List<TreeLatencyTracker.LatencyRecordNode>(this.completedChildren);
				foreach (TreeLatencyTracker.LatencyRecordNode latencyRecordNode2 in list)
				{
					latencyRecordNode.completedChildren.Add(latencyRecordNode2.DeepCopy(latencyRecordNode));
				}
				TreeLatencyTracker.LatencyRecordNode latencyRecordNode3 = this.pendingChild;
				if (latencyRecordNode3 != null)
				{
					latencyRecordNode.pendingChild = latencyRecordNode3.DeepCopy(latencyRecordNode);
				}
				latencyRecordNode.latencyRecord = this.latencyRecord;
				return latencyRecordNode;
			}

			// Token: 0x060013B5 RID: 5045 RVA: 0x0004E534 File Offset: 0x0004C734
			public IEnumerable<LatencyRecord> GetCompletedRecords()
			{
				List<LatencyRecord> list = new List<LatencyRecord>();
				if (this.IsComplete)
				{
					list.Add(this.latencyRecord.AsCompletedRecord());
				}
				foreach (TreeLatencyTracker.LatencyRecordNode latencyRecordNode in this.completedChildren)
				{
					list.AddRange(latencyRecordNode.GetCompletedRecords());
				}
				if (this.HasPendingChild)
				{
					list.AddRange(this.pendingChild.GetCompletedRecords());
				}
				return list;
			}

			// Token: 0x060013B6 RID: 5046 RVA: 0x0004E5C8 File Offset: 0x0004C7C8
			public IEnumerable<PendingLatencyRecord> GetPendingRecords()
			{
				List<PendingLatencyRecord> list = new List<PendingLatencyRecord>();
				if (this.IsPending)
				{
					list.Add(this.latencyRecord.AsPendingRecord());
				}
				if (this.pendingChild != null)
				{
					list.AddRange(this.pendingChild.GetPendingRecords());
				}
				return list;
			}

			// Token: 0x060013B7 RID: 5047 RVA: 0x0004E60E File Offset: 0x0004C80E
			public TreeLatencyTracker.LatencyRecordNode BeginTrackLatency(ushort componentId, long startTime)
			{
				this.pendingChild = new TreeLatencyTracker.LatencyRecordNode(componentId, startTime, this);
				return this.pendingChild;
			}

			// Token: 0x060013B8 RID: 5048 RVA: 0x0004E624 File Offset: 0x0004C824
			public TimeSpan CompleteChild(ushort trackingComponentId, long endTime, bool shouldAggregate, out bool childTracked)
			{
				childTracked = true;
				TimeSpan timeSpan = this.pendingChild.Complete(trackingComponentId, endTime);
				if (shouldAggregate && this.HasCompletedChild && this.LastCompletedChildComponentId == trackingComponentId)
				{
					this.completedChildren.Last<TreeLatencyTracker.LatencyRecordNode>().latencyRecord = new LatencyRecordPlus(trackingComponentId, this.LastCompletedChildLatency + this.pendingChild.CalculateLatency(endTime));
					this.completedChildren.Last<TreeLatencyTracker.LatencyRecordNode>().completedChildren.AddRange(this.pendingChild.completedChildren);
					this.pendingChild = null;
					return timeSpan;
				}
				if (TreeLatencyTracker.LatencyRecordNode.ShouldTrackComponent(timeSpan))
				{
					this.completedChildren.Add(this.pendingChild);
				}
				else
				{
					childTracked = false;
				}
				this.pendingChild = null;
				return timeSpan;
			}

			// Token: 0x060013B9 RID: 5049 RVA: 0x0004E6D4 File Offset: 0x0004C8D4
			public TimeSpan Complete(ushort trackingComponentId, long endTime)
			{
				if (this.IsComplete)
				{
					TreeLatencyTracker.EventLogger.LogEvent(TransportEventLogConstants.Tuple_MultipleCompletions, "MultipleCompletions", new object[]
					{
						this.ToString(),
						trackingComponentId
					});
					return TimeSpan.Zero;
				}
				if (this.pendingChild != null)
				{
					this.pendingChild.ForceImplicitComplete(endTime);
					this.completedChildren.Add(this.pendingChild);
					this.pendingChild = null;
				}
				return this.latencyRecord.Complete(endTime, trackingComponentId, false);
			}

			// Token: 0x060013BA RID: 5050 RVA: 0x0004E758 File Offset: 0x0004C958
			public TimeSpan CalculateLatency(long currentTime)
			{
				return this.latencyRecord.CalculateLatency(currentTime);
			}

			// Token: 0x060013BB RID: 5051 RVA: 0x0004E768 File Offset: 0x0004C968
			public void AddCompletedRecord(ushort componentId, TimeSpan latency)
			{
				TreeLatencyTracker.LatencyRecordNode item = new TreeLatencyTracker.LatencyRecordNode(new LatencyRecordPlus(componentId, latency), this);
				this.completedChildren.Add(item);
			}

			// Token: 0x060013BC RID: 5052 RVA: 0x0004E78F File Offset: 0x0004C98F
			public TreeLatencyTracker.LatencyRecordNode GetPendingLeaf()
			{
				if (this.HasPendingChild)
				{
					return this.pendingChild.GetPendingLeaf();
				}
				return this;
			}

			// Token: 0x060013BD RID: 5053 RVA: 0x0004E7A6 File Offset: 0x0004C9A6
			private static bool ShouldTrackComponent(TimeSpan latency)
			{
				return LatencyTracker.ComponentLatencyTrackingEnabled && (LatencyTracker.HighPrecisionThresholdInterval == TimeSpan.Zero || !(latency < LatencyTracker.HighPrecisionThresholdInterval));
			}

			// Token: 0x060013BE RID: 5054 RVA: 0x0004E7D4 File Offset: 0x0004C9D4
			private void ForceImplicitComplete(long endTime)
			{
				if (this.pendingChild != null)
				{
					this.pendingChild.ForceImplicitComplete(endTime);
					this.completedChildren.Add(this.pendingChild);
					this.pendingChild = null;
				}
				this.latencyRecord.Complete(endTime, true);
			}

			// Token: 0x04000A19 RID: 2585
			private readonly TreeLatencyTracker.LatencyRecordNode parent;

			// Token: 0x04000A1A RID: 2586
			private readonly List<TreeLatencyTracker.LatencyRecordNode> completedChildren = new List<TreeLatencyTracker.LatencyRecordNode>();

			// Token: 0x04000A1B RID: 2587
			private TreeLatencyTracker.LatencyRecordNode pendingChild;

			// Token: 0x04000A1C RID: 2588
			private LatencyRecordPlus latencyRecord;
		}
	}
}
