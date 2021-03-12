using System;
using System.Collections.Concurrent;
using System.Runtime.Caching;
using System.Threading;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Diagnostics;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Routing
{
	// Token: 0x0200001F RID: 31
	internal class IncomingEntry : Entry
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00004940 File Offset: 0x00002B40
		public IncomingEntry(ComplianceMessage message, bool outbound)
		{
			base.MessageId = (outbound ? message.MessageSourceId : message.MessageId);
			base.Message = (outbound ? null : message);
			base.CorrelationId = message.CorrelationId;
			this.Status = IncomingEntry.IncomingEntryStatus.Initialized;
			base.ExpiryTime = TaskDistributionSettings.IncomingEntryExpiryTime;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000049A0 File Offset: 0x00002BA0
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000049A8 File Offset: 0x00002BA8
		public ResultBase Aggregation
		{
			get
			{
				return this.result;
			}
			set
			{
				this.result = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000049B1 File Offset: 0x00002BB1
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000049B9 File Offset: 0x00002BB9
		public IncomingEntry.IncomingEntryStatus Status
		{
			get
			{
				return this.status;
			}
			set
			{
				if (this.status < value)
				{
					base.RetryCount = 0;
					this.status = value;
					this.EvaluateState(false);
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000049DC File Offset: 0x00002BDC
		public override string GetKey()
		{
			return string.Format("INCOMING:{0}:{1}", base.CorrelationId, base.MessageId);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004A08 File Offset: 0x00002C08
		public void RecordResult(ComplianceMessage message, Func<ResultBase, ResultBase> commitFunction)
		{
			ResultBase resultBase = this.result;
			while (Interlocked.CompareExchange<ResultBase>(ref this.result, commitFunction(resultBase), resultBase) != resultBase)
			{
				resultBase = this.result;
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004A3C File Offset: 0x00002C3C
		public OutgoingEntry ReturnOutgoingEntry(ComplianceMessage message)
		{
			if (message.ComplianceMessageType == ComplianceMessageType.RecordResult)
			{
				OutgoingEntry outgoingEntry = this.AddOutgoingEntry(message);
				if (outgoingEntry != null)
				{
					outgoingEntry.Status = OutgoingEntry.OutgoingEntryStatus.Returned;
					return outgoingEntry;
				}
			}
			return null;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00004A68 File Offset: 0x00002C68
		public OutgoingEntry AddOutgoingEntry(ComplianceMessage message)
		{
			OutgoingEntry outgoingEntry = this.GetOutgoingEntry(message, false);
			if (outgoingEntry != null && outgoingEntry.Status != OutgoingEntry.OutgoingEntryStatus.Completed)
			{
				int value = 0;
				if (this.outgoingKeys.TryAdd(outgoingEntry.GetKey(), value))
				{
					outgoingEntry.EvaluateState(false);
				}
			}
			return outgoingEntry;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004AA8 File Offset: 0x00002CA8
		public void CompleteOutgoingEntry(ComplianceMessage message)
		{
			OutgoingEntry outgoingEntry = this.GetOutgoingEntry(message, true);
			if (outgoingEntry != null)
			{
				int num;
				this.outgoingKeys.TryRemove(outgoingEntry.GetKey(), out num);
				outgoingEntry.Status = OutgoingEntry.OutgoingEntryStatus.Completed;
			}
			if (this.outgoingKeys.Count == 0 && this.Status == IncomingEntry.IncomingEntryStatus.Processed)
			{
				this.Status = IncomingEntry.IncomingEntryStatus.Returned;
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004AFC File Offset: 0x00002CFC
		public OutgoingEntry GetOutgoingEntry(ComplianceMessage message, bool onlyIfExists = false)
		{
			OutgoingEntry outgoingEntry = new OutgoingEntry(message);
			if (!onlyIfExists)
			{
				return outgoingEntry.UpdateCache(RoutingCache.Instance.RoutingTable) as OutgoingEntry;
			}
			CacheItem cacheItem = RoutingCache.Instance.RoutingTable.GetCacheItem(outgoingEntry.GetKey(), null);
			if (cacheItem != null)
			{
				return cacheItem.Value as OutgoingEntry;
			}
			return null;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004B50 File Offset: 0x00002D50
		public void RequestReissued()
		{
			this.ReturnMessage();
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004B58 File Offset: 0x00002D58
		public override void EvaluateState(bool expired)
		{
			if (this.Status != IncomingEntry.IncomingEntryStatus.Failed && ExceptionHandler.IsFaulted(base.Message))
			{
				this.Status = IncomingEntry.IncomingEntryStatus.Failed;
				return;
			}
			switch (this.Status)
			{
			case IncomingEntry.IncomingEntryStatus.Initialized:
				if (base.RetryCount > TaskDistributionSettings.IncomingEntryRetriesToFailure)
				{
					ExceptionHandler.FaultMessage(base.Message, FaultDefinition.FromErrorString("Processing Failed", "EvaluateState", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\IncomingEntry.cs", 278), true);
					this.Status = IncomingEntry.IncomingEntryStatus.Failed;
				}
				break;
			case IncomingEntry.IncomingEntryStatus.Processed:
				if (base.RetryCount > TaskDistributionSettings.IncomingEntryRetriesToFailure)
				{
					ExceptionHandler.FaultMessage(base.Message, FaultDefinition.FromErrorString("Ougoing messages not retunred", "EvaluateState", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\IncomingEntry.cs", 287), true);
					this.Status = IncomingEntry.IncomingEntryStatus.Failed;
				}
				if (this.outgoingKeys.Count == 0)
				{
					this.Status = IncomingEntry.IncomingEntryStatus.Returned;
				}
				break;
			case IncomingEntry.IncomingEntryStatus.Returned:
				if (base.RetryCount > TaskDistributionSettings.IncomingEntryRetriesToFailure)
				{
					ExceptionHandler.FaultMessage(base.Message, FaultDefinition.FromErrorString("Recording of results Failed", "EvaluateState", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\IncomingEntry.cs", 303), true);
					this.Status = IncomingEntry.IncomingEntryStatus.Failed;
				}
				this.ReturnMessage();
				break;
			case IncomingEntry.IncomingEntryStatus.Failed:
				if (!ExceptionHandler.IsFaulted(base.Message))
				{
					ExceptionHandler.FaultMessage(base.Message, FaultDefinition.FromErrorString("Unknown failure", "EvaluateState", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\IncomingEntry.cs", 313), true);
				}
				this.FailMessage();
				break;
			}
			if (base.RetryCount > TaskDistributionSettings.IncomingEntryRetriesToAbandon)
			{
				base.KeepAlive = false;
				return;
			}
			base.KeepAlive = true;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004CD4 File Offset: 0x00002ED4
		protected override void UpdateExistingEntry(Entry existing)
		{
			IncomingEntry incomingEntry = (IncomingEntry)existing;
			if (incomingEntry.Message == null && base.Message != null)
			{
				incomingEntry.Message = base.Message;
				incomingEntry.EvaluateState(false);
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004D0C File Offset: 0x00002F0C
		private void ReturnMessage()
		{
			if (base.Message != null)
			{
				if (ExceptionHandler.IsFaulted(base.Message))
				{
					this.FailMessage();
					return;
				}
				if (this.result != null)
				{
					ComplianceMessage complianceMessage = base.Message.Clone();
					Target messageTarget = complianceMessage.MessageTarget;
					complianceMessage.MessageTarget = complianceMessage.MessageSource;
					complianceMessage.MessageSource = messageTarget;
					complianceMessage.ComplianceMessageType = ComplianceMessageType.RecordResult;
					complianceMessage.ProtocolContext.DispatchData = this;
					complianceMessage.Payload = ComplianceSerializer.Serialize<WorkPayload>(WorkPayload.Description, new WorkPayload
					{
						WorkDefinition = this.result.GetSerializedResult(),
						WorkDefinitionType = complianceMessage.WorkDefinitionType
					});
					RoutingCache.Instance.QueueDispatch(complianceMessage);
				}
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004DB8 File Offset: 0x00002FB8
		private void FailMessage()
		{
			if (ExceptionHandler.IsFaulted(base.Message))
			{
				ComplianceMessage complianceMessage = base.Message.Clone();
				Target messageTarget = complianceMessage.MessageTarget;
				complianceMessage.MessageTarget = complianceMessage.MessageSource;
				complianceMessage.MessageSource = messageTarget;
				complianceMessage.ComplianceMessageType = ComplianceMessageType.RecordResult;
				complianceMessage.ProtocolContext.DispatchData = this;
				complianceMessage.Payload = ComplianceSerializer.Serialize<WorkPayload>(WorkPayload.Description, ExceptionHandler.GetFaultDefinition(base.Message).ToPayload());
				RoutingCache.Instance.QueueDispatch(complianceMessage);
			}
		}

		// Token: 0x04000038 RID: 56
		private IncomingEntry.IncomingEntryStatus status;

		// Token: 0x04000039 RID: 57
		private ResultBase result;

		// Token: 0x0400003A RID: 58
		private ConcurrentDictionary<string, int> outgoingKeys = new ConcurrentDictionary<string, int>();

		// Token: 0x02000020 RID: 32
		public enum IncomingEntryStatus
		{
			// Token: 0x0400003C RID: 60
			Initialized,
			// Token: 0x0400003D RID: 61
			Processed,
			// Token: 0x0400003E RID: 62
			Returned,
			// Token: 0x0400003F RID: 63
			Failed,
			// Token: 0x04000040 RID: 64
			Completed
		}
	}
}
