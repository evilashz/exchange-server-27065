using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Threading.Tasks.Dataflow;
using Microsoft.Exchange.Compliance.TaskDistributionCommon;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Utility;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Routing
{
	// Token: 0x02000021 RID: 33
	internal class RoutingCache
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00004E36 File Offset: 0x00003036
		private RoutingCache()
		{
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004E4F File Offset: 0x0000304F
		public static RoutingCache Instance
		{
			get
			{
				return RoutingCache.instance;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00004E58 File Offset: 0x00003058
		public MemoryCache RoutingTable
		{
			get
			{
				if (this.routingTable == null)
				{
					FaultDefinition faultDefinition;
					Registry.Instance.TryGetInstance<MemoryCache>(RegistryComponent.Common, CommonComponent.CriticalCache, out this.routingTable, out faultDefinition, "RoutingTable", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\RoutingCache.cs", 84);
				}
				return this.routingTable;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00004E9C File Offset: 0x0000309C
		public MemoryCache DispatchQueue
		{
			get
			{
				if (this.dispatchQueue == null)
				{
					FaultDefinition faultDefinition;
					Registry.Instance.TryGetInstance<MemoryCache>(RegistryComponent.Common, CommonComponent.CriticalCache, out this.dispatchQueue, out faultDefinition, "DispatchQueue", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\RoutingCache.cs", 104);
				}
				return this.dispatchQueue;
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004EE0 File Offset: 0x000030E0
		public IncomingEntry ReceiveMessage(ComplianceMessage message, out bool shouldProcess)
		{
			shouldProcess = true;
			message.ProtocolContext.Direction = ProtocolContext.MessageDirection.Incoming;
			IncomingEntry incomingEntry = this.GetIncomingEntry(message, false);
			if (incomingEntry != null)
			{
				if (incomingEntry.Status == IncomingEntry.IncomingEntryStatus.Processed)
				{
					shouldProcess = false;
				}
				else if (incomingEntry.Status == IncomingEntry.IncomingEntryStatus.Completed)
				{
					shouldProcess = false;
					incomingEntry.RequestReissued();
				}
			}
			OutgoingEntry outgoingEntry = incomingEntry.ReturnOutgoingEntry(message);
			if (outgoingEntry != null && outgoingEntry.Status != OutgoingEntry.OutgoingEntryStatus.Completed)
			{
				message.ProtocolContext.Direction = ProtocolContext.MessageDirection.Return;
				if (!shouldProcess)
				{
					shouldProcess = true;
				}
			}
			return incomingEntry;
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004F50 File Offset: 0x00003150
		public OutgoingEntry SendMessage(ComplianceMessage message)
		{
			message.ProtocolContext.Direction = ProtocolContext.MessageDirection.Outgoing;
			IncomingEntry incomingEntry = this.GetIncomingEntry(message, true);
			return incomingEntry.AddOutgoingEntry(message);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004F7C File Offset: 0x0000317C
		public IncomingEntry ReturnMessage(ComplianceMessage message)
		{
			IncomingEntry incomingEntry = this.GetIncomingEntry(message, true);
			incomingEntry.CompleteOutgoingEntry(message);
			return incomingEntry;
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00004F9C File Offset: 0x0000319C
		public IncomingEntry ProcessedMessage(ComplianceMessage message)
		{
			IncomingEntry incomingEntry = this.GetIncomingEntry(message, false);
			if (message.ComplianceMessageType != ComplianceMessageType.RecordResult)
			{
				incomingEntry.Status = IncomingEntry.IncomingEntryStatus.Processed;
			}
			return incomingEntry;
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004FC4 File Offset: 0x000031C4
		public void DispatchedMessage(ComplianceMessage message)
		{
			IncomingEntry incomingEntry = message.ProtocolContext.DispatchData as IncomingEntry;
			if (incomingEntry != null)
			{
				incomingEntry.Status = IncomingEntry.IncomingEntryStatus.Completed;
				return;
			}
			OutgoingEntry outgoingEntry = message.ProtocolContext.DispatchData as OutgoingEntry;
			if (outgoingEntry != null)
			{
				outgoingEntry.Status = OutgoingEntry.OutgoingEntryStatus.Delivered;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x00005008 File Offset: 0x00003208
		public void RecordResult(ComplianceMessage message, Func<ResultBase, ResultBase> commitFunction)
		{
			IncomingEntry incomingEntry = this.GetIncomingEntry(message, true);
			incomingEntry.RecordResult(message, commitFunction);
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005028 File Offset: 0x00003228
		public void QueueDispatch(ComplianceMessage message)
		{
			string routingKey = MessageHelper.GetRoutingKey(message);
			CacheItemPolicy policy = new CacheItemPolicy
			{
				RemovedCallback = new CacheEntryRemovedCallback(this.DispatchQueueExpiry),
				SlidingExpiration = TaskDistributionSettings.DispatchQueueTime
			};
			ConcurrentBag<ComplianceMessage> concurrentBag = new ConcurrentBag<ComplianceMessage>();
			if (TaskDistributionSettings.EnableDispatchQueue && this.DispatchQueue != null)
			{
				ConcurrentBag<ComplianceMessage> concurrentBag2 = (this.dispatchQueue.AddOrGetExisting(routingKey, concurrentBag, policy, null) as ConcurrentBag<ComplianceMessage>) ?? concurrentBag;
				concurrentBag2.Add(message);
				return;
			}
			concurrentBag.Add(message);
			DataflowBlock.SendAsync<IEnumerable<ComplianceMessage>>(this.dispatchBlock, concurrentBag);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000050B0 File Offset: 0x000032B0
		private void DispatchQueueExpiry(CacheEntryRemovedArguments e)
		{
			ConcurrentBag<ComplianceMessage> concurrentBag = e.CacheItem.Value as ConcurrentBag<ComplianceMessage>;
			DataflowBlock.SendAsync<IEnumerable<ComplianceMessage>>(this.dispatchBlock, concurrentBag);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000050DC File Offset: 0x000032DC
		private IncomingEntry GetIncomingEntry(ComplianceMessage message, bool outbound)
		{
			if (!outbound && message.ComplianceMessageType == ComplianceMessageType.RecordResult)
			{
				outbound = true;
			}
			IncomingEntry incomingEntry = new IncomingEntry(message, outbound);
			return incomingEntry.UpdateCache(RoutingCache.Instance.RoutingTable) as IncomingEntry;
		}

		// Token: 0x04000041 RID: 65
		private static RoutingCache instance = new RoutingCache();

		// Token: 0x04000042 RID: 66
		private MemoryCache routingTable;

		// Token: 0x04000043 RID: 67
		private MemoryCache dispatchQueue;

		// Token: 0x04000044 RID: 68
		private ITargetBlock<IEnumerable<ComplianceMessage>> dispatchBlock = new DispatchBlock().GetDataflowBlock(null);
	}
}
