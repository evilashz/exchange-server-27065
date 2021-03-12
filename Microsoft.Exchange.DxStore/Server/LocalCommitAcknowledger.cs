using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DxStore;
using Microsoft.Exchange.DxStore.Common;

namespace Microsoft.Exchange.DxStore.Server
{
	// Token: 0x02000060 RID: 96
	public class LocalCommitAcknowledger
	{
		// Token: 0x060003CB RID: 971 RVA: 0x0000AC48 File Offset: 0x00008E48
		public LocalCommitAcknowledger(DxStoreInstance instance)
		{
			this.instance = instance;
			this.OldestItemTime = DateTimeOffset.Now;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060003CC RID: 972 RVA: 0x0000AC83 File Offset: 0x00008E83
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.CommitAckTracer;
			}
		}

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000AC8A File Offset: 0x00008E8A
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0000AC92 File Offset: 0x00008E92
		public DateTimeOffset OldestItemTime { get; set; }

		// Token: 0x060003CF RID: 975 RVA: 0x0000AC9C File Offset: 0x00008E9C
		public void AddCommand(DxStoreCommand command, WriteOptions options)
		{
			lock (this.locker)
			{
				int count = this.instance.StateMachine.Paxos.ConfigurationHint.Acceptors.Count;
				LocalCommitAcknowledger.Container container = new LocalCommitAcknowledger.Container(command.CommandId, command.TimeInitiated, count, options);
				LocalCommitAcknowledger.Tracer.TraceDebug((long)this.instance.IdentityHash, "{0}: Adding command {1} - Id# {2} (Initiator: {3}, TotalNodes: {4}, MinimumRequired: {5})", new object[]
				{
					this.instance.Identity,
					command.GetTypeId(),
					command.CommandId,
					command.Initiator,
					container.TotalNodesCount,
					container.MinimumNodesRequired
				});
				LinkedListNode<LocalCommitAcknowledger.Container> value = this.containerList.AddFirst(container);
				this.containerMap[command.CommandId] = value;
				this.UpdateOldestItemTime();
			}
		}

		// Token: 0x060003D0 RID: 976 RVA: 0x0000ADAC File Offset: 0x00008FAC
		public void HandleAcknowledge(Guid commandId, string sender)
		{
			lock (this.locker)
			{
				LocalCommitAcknowledger.Tracer.TraceDebug<string, Guid, string>((long)this.instance.IdentityHash, "{0}: Received ack for Id# {1} from {2}", this.instance.Identity, commandId, sender);
				bool flag2 = false;
				bool flag3 = false;
				LocalCommitAcknowledger.Container container = null;
				LinkedListNode<LocalCommitAcknowledger.Container> linkedListNode;
				if (this.containerMap.TryGetValue(commandId, out linkedListNode))
				{
					container = linkedListNode.Value;
					if (container != null)
					{
						container.CompletionTimes[sender] = (int)(DateTimeOffset.Now - container.InitiatedTime).TotalMilliseconds;
						if (container.CompletionTimes.Count >= container.MinimumNodesRequired)
						{
							flag2 = true;
						}
						if (container.WaitingForAck.Count == 0)
						{
							flag3 = true;
						}
						if (flag2 && flag3 && !container.IsCompleted)
						{
							container.IsCompleted = true;
							try
							{
								container.CompletionEvent.Set();
							}
							catch (ObjectDisposedException)
							{
								LocalCommitAcknowledger.Tracer.TraceError<string, Guid>((long)this.instance.IdentityHash, "{0}: Ack event for Id# {1} is already disposed", this.instance.Identity, commandId);
							}
						}
					}
				}
				if (container == null)
				{
					LocalCommitAcknowledger.Tracer.TraceWarning<string, Guid>((long)this.instance.IdentityHash, "{0}: Container for Id# {1} does not exist. Possibly timed out and removed", this.instance.Identity, commandId);
				}
				else
				{
					LocalCommitAcknowledger.Tracer.TraceDebug((long)this.instance.IdentityHash, "{0}: Finished processing ack for Id# {1} - Roundtrip {2}ms. (IsMajority: {3}, IsRequiredNodesComplete: {4}, IsComplete: {5})", new object[]
					{
						this.instance.Identity,
						commandId,
						container.CompletionTimes[sender],
						flag2,
						flag3,
						container.IsCompleted
					});
				}
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0000AFC8 File Offset: 0x000091C8
		public WriteResult.ResponseInfo[] RemoveCommand(Guid commandId)
		{
			WriteResult.ResponseInfo[] result = null;
			lock (this.locker)
			{
				LocalCommitAcknowledger.Tracer.TraceDebug<string, Guid>((long)this.instance.IdentityHash, "{0}: Removing command Id# {1} from wait list", this.instance.Identity, commandId);
				LocalCommitAcknowledger.Container container = null;
				LinkedListNode<LocalCommitAcknowledger.Container> linkedListNode;
				if (this.containerMap.TryGetValue(commandId, out linkedListNode))
				{
					container = linkedListNode.Value;
					this.containerMap.Remove(commandId);
					this.containerList.Remove(linkedListNode);
					this.UpdateOldestItemTime();
					if (container != null)
					{
						result = (from kvp in container.CompletionTimes
						select new WriteResult.ResponseInfo
						{
							Name = kvp.Key,
							LatencyInMs = kvp.Value
						}).ToArray<WriteResult.ResponseInfo>();
						if (container.CompletionEvent != null)
						{
							container.CompletionEvent.Dispose();
						}
					}
				}
				if (container == null)
				{
					LocalCommitAcknowledger.Tracer.TraceWarning<string, Guid>((long)this.instance.IdentityHash, "{0}: Remove ignored for command Id# {1} since it might have already been removed", this.instance.Identity, commandId);
				}
			}
			return result;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0000B0D4 File Offset: 0x000092D4
		public bool WaitForExecution(Guid commandId, TimeSpan timeout)
		{
			ManualResetEvent manualResetEvent = null;
			LocalCommitAcknowledger.Tracer.TraceDebug<string, Guid>((long)this.instance.IdentityHash, "{0}: Waiting for command Id# {1} to satisfy write constraints", this.instance.Identity, commandId);
			lock (this.locker)
			{
				LinkedListNode<LocalCommitAcknowledger.Container> linkedListNode;
				if (this.containerMap.TryGetValue(commandId, out linkedListNode) && linkedListNode != null && linkedListNode.Value != null)
				{
					manualResetEvent = linkedListNode.Value.CompletionEvent;
				}
			}
			if (manualResetEvent == null)
			{
				LocalCommitAcknowledger.Tracer.TraceWarning<string, Guid>((long)this.instance.IdentityHash, "{0}: Completion event does not exist for command Id# {1}", this.instance.Identity, commandId);
				return true;
			}
			WaitHandle[] waitHandles = new WaitHandle[]
			{
				manualResetEvent,
				this.instance.StopEvent
			};
			if (WaitHandle.WaitAny(waitHandles, timeout) == 0)
			{
				LocalCommitAcknowledger.Tracer.TraceDebug<string, Guid>((long)this.instance.IdentityHash, "{0}: Completion event triggered command Id# {1}", this.instance.Identity, commandId);
				return true;
			}
			LocalCommitAcknowledger.Tracer.TraceError<string, Guid>((long)this.instance.IdentityHash, "{0}: Completion event timedout for command Id# {1}", this.instance.Identity, commandId);
			return false;
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000B208 File Offset: 0x00009408
		private void UpdateOldestItemTime()
		{
			LinkedListNode<LocalCommitAcknowledger.Container> last = this.containerList.Last;
			if (last != null && last.Value != null)
			{
				this.OldestItemTime = last.Value.InitiatedTime;
				return;
			}
			this.OldestItemTime = DateTimeOffset.Now;
		}

		// Token: 0x040001EB RID: 491
		private readonly LinkedList<LocalCommitAcknowledger.Container> containerList = new LinkedList<LocalCommitAcknowledger.Container>();

		// Token: 0x040001EC RID: 492
		private readonly Dictionary<Guid, LinkedListNode<LocalCommitAcknowledger.Container>> containerMap = new Dictionary<Guid, LinkedListNode<LocalCommitAcknowledger.Container>>();

		// Token: 0x040001ED RID: 493
		private readonly object locker = new object();

		// Token: 0x040001EE RID: 494
		private readonly DxStoreInstance instance;

		// Token: 0x02000061 RID: 97
		internal class Container
		{
			// Token: 0x060003D5 RID: 981 RVA: 0x0000B258 File Offset: 0x00009458
			internal Container(Guid id, DateTimeOffset initiatedTime, int totalNodesCount, WriteOptions options)
			{
				this.Id = id;
				this.InitiatedTime = initiatedTime;
				this.Options = options;
				this.TotalNodesCount = totalNodesCount;
				this.CompletionEvent = new ManualResetEvent(false);
				this.CompletionTimes = new Dictionary<string, int>();
				this.WaitingForAck = new HashSet<string>();
				if (options != null)
				{
					this.MinimumNodesRequired = (int)Math.Ceiling((double)this.TotalNodesCount * options.PercentageOfNodesToSucceed / 100.0);
					if (options.WaitForNodes != null)
					{
						EnumerableEx.ForEach<string>(options.WaitForNodes, delegate(string node)
						{
							this.WaitingForAck.Add(node);
						});
					}
				}
			}

			// Token: 0x1700013A RID: 314
			// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000B2FB File Offset: 0x000094FB
			// (set) Token: 0x060003D7 RID: 983 RVA: 0x0000B303 File Offset: 0x00009503
			public Guid Id { get; set; }

			// Token: 0x1700013B RID: 315
			// (get) Token: 0x060003D8 RID: 984 RVA: 0x0000B30C File Offset: 0x0000950C
			// (set) Token: 0x060003D9 RID: 985 RVA: 0x0000B314 File Offset: 0x00009514
			public DateTimeOffset InitiatedTime { get; set; }

			// Token: 0x1700013C RID: 316
			// (get) Token: 0x060003DA RID: 986 RVA: 0x0000B31D File Offset: 0x0000951D
			// (set) Token: 0x060003DB RID: 987 RVA: 0x0000B325 File Offset: 0x00009525
			public WriteOptions Options { get; set; }

			// Token: 0x1700013D RID: 317
			// (get) Token: 0x060003DC RID: 988 RVA: 0x0000B32E File Offset: 0x0000952E
			// (set) Token: 0x060003DD RID: 989 RVA: 0x0000B336 File Offset: 0x00009536
			public bool IsCompleted { get; set; }

			// Token: 0x1700013E RID: 318
			// (get) Token: 0x060003DE RID: 990 RVA: 0x0000B33F File Offset: 0x0000953F
			// (set) Token: 0x060003DF RID: 991 RVA: 0x0000B347 File Offset: 0x00009547
			public ManualResetEvent CompletionEvent { get; set; }

			// Token: 0x1700013F RID: 319
			// (get) Token: 0x060003E0 RID: 992 RVA: 0x0000B350 File Offset: 0x00009550
			// (set) Token: 0x060003E1 RID: 993 RVA: 0x0000B358 File Offset: 0x00009558
			public Dictionary<string, int> CompletionTimes { get; set; }

			// Token: 0x17000140 RID: 320
			// (get) Token: 0x060003E2 RID: 994 RVA: 0x0000B361 File Offset: 0x00009561
			// (set) Token: 0x060003E3 RID: 995 RVA: 0x0000B369 File Offset: 0x00009569
			public int TotalNodesCount { get; set; }

			// Token: 0x17000141 RID: 321
			// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000B372 File Offset: 0x00009572
			// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000B37A File Offset: 0x0000957A
			public int MinimumNodesRequired { get; set; }

			// Token: 0x17000142 RID: 322
			// (get) Token: 0x060003E6 RID: 998 RVA: 0x0000B383 File Offset: 0x00009583
			// (set) Token: 0x060003E7 RID: 999 RVA: 0x0000B38B File Offset: 0x0000958B
			public HashSet<string> WaitingForAck { get; set; }
		}
	}
}
