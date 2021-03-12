using System;
using System.Collections.Generic;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Diagnostics;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation
{
	// Token: 0x02000023 RID: 35
	internal class MessageLogger
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00004E65 File Offset: 0x00003065
		protected MessageLogger()
		{
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00004E6D File Offset: 0x0000306D
		public static MessageLogger Instance
		{
			get
			{
				return MessageLogger.instance;
			}
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004E74 File Offset: 0x00003074
		public void LogMessageReceived(ComplianceMessage message)
		{
			message.ProtocolContext.QueueStartTime = new DateTime?(DateTime.UtcNow);
			IPerformanceCounterAccessor counter = this.GetCounter(message.ComplianceMessageType.ToString());
			if (counter != null)
			{
				counter.AddQueueEvent(QueueEvent.Enqueue);
			}
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004EB8 File Offset: 0x000030B8
		public void LogMessageBlockProcessing(ComplianceMessage message, string block)
		{
			if (message.ProtocolContext.QueueEndTime == null)
			{
				this.LogMessageProcessing(message);
			}
			IPerformanceCounterAccessor counter = this.GetCounter(block);
			if (counter != null)
			{
				counter.AddProcessorEvent(ProcessorEvent.StartProcessing);
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004EF4 File Offset: 0x000030F4
		public void LogMessageBlockProcessed(ComplianceMessage message, string block, long time)
		{
			IPerformanceCounterAccessor counter = this.GetCounter(block);
			if (counter != null)
			{
				counter.AddProcessorEvent(ProcessorEvent.EndProcessing);
				counter.AddProcessingCompletionEvent(ExceptionHandler.IsFaulted(message) ? ProcessingCompletionEvent.Failure : ProcessingCompletionEvent.Success, time);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004F28 File Offset: 0x00003128
		public void LogMessageProcessing(ComplianceMessage message)
		{
			message.ProtocolContext.QueueEndTime = new DateTime?(DateTime.UtcNow);
			message.ProtocolContext.ProcessStartTime = new DateTime?(DateTime.UtcNow);
			IPerformanceCounterAccessor counter = this.GetCounter(message.ComplianceMessageType.ToString());
			if (counter != null)
			{
				long latency = 0L;
				if (message.ProtocolContext.QueueStartTime != null && message.ProtocolContext.QueueEndTime != null)
				{
					latency = (long)(message.ProtocolContext.QueueEndTime.Value - message.ProtocolContext.QueueStartTime.Value).TotalMilliseconds;
				}
				counter.AddDequeueLatency(latency);
				counter.AddQueueEvent(QueueEvent.Dequeue);
				counter.AddProcessorEvent(ProcessorEvent.StartProcessing);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004FF3 File Offset: 0x000031F3
		public void LogMessageDispatching(ComplianceMessage message)
		{
			message.ProtocolContext.DispatchStartTime = new DateTime?(DateTime.UtcNow);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000500C File Offset: 0x0000320C
		public void LogMessageProcessed(ComplianceMessage message)
		{
			message.ProtocolContext.ProcessEndTime = new DateTime?(DateTime.UtcNow);
			IPerformanceCounterAccessor counter = this.GetCounter(message.ComplianceMessageType.ToString());
			if (counter != null)
			{
				long latency = 0L;
				if (message.ProtocolContext.ProcessStartTime != null && message.ProtocolContext.ProcessEndTime != null)
				{
					latency = (long)(message.ProtocolContext.ProcessEndTime.Value - message.ProtocolContext.ProcessStartTime.Value).TotalMilliseconds;
				}
				counter.AddProcessorEvent(ProcessorEvent.EndProcessing);
				counter.AddProcessingCompletionEvent(ExceptionHandler.IsFaulted(message) ? ProcessingCompletionEvent.Failure : ProcessingCompletionEvent.Success, latency);
			}
			if (message.ProtocolContext.Direction == ProtocolContext.MessageDirection.Return)
			{
				ProtocolContext.MessageDirection direction = message.ProtocolContext.Direction;
			}
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000050E1 File Offset: 0x000032E1
		public void LogMessageDispatched(ComplianceMessage message)
		{
			message.ProtocolContext.DispatchEndTime = new DateTime?(DateTime.UtcNow);
			ProtocolContext.MessageDirection direction = message.ProtocolContext.Direction;
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00005108 File Offset: 0x00003308
		public void LogMessageFaulted(ComplianceMessage message, FaultDefinition fault)
		{
			foreach (FaultRecord faultRecord in fault.Faults)
			{
				EventNotificationItem eventNotificationItem = new EventNotificationItem("TaskDistributionFabric", "TaskDistributionFabric", "Exception", ResultSeverityLevel.Error);
				eventNotificationItem.CustomProperties = new Dictionary<string, string>();
				foreach (KeyValuePair<string, string> keyValuePair in faultRecord.Data)
				{
					if (!string.IsNullOrEmpty(keyValuePair.Key) && !string.IsNullOrEmpty(keyValuePair.Value))
					{
						eventNotificationItem.CustomProperties[keyValuePair.Key] = keyValuePair.Value;
					}
				}
				eventNotificationItem.Publish(false);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000051EC File Offset: 0x000033EC
		private IPerformanceCounterAccessor GetCounter(string type)
		{
			IPerformanceCounterAccessorRegistry performanceCounterAccessorRegistry;
			FaultDefinition faultDefinition;
			if (Registry.Instance.TryGetInstance<IPerformanceCounterAccessorRegistry>(RegistryComponent.Common, CommonComponent.PerformanceCounterRegistry, out performanceCounterAccessorRegistry, out faultDefinition, "GetCounter", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionCommon\\Instrumentation\\MessageLogger.cs", 207))
			{
				return performanceCounterAccessorRegistry.GetOrAddPerformanceCounterAccessor(type);
			}
			return null;
		}

		// Token: 0x04000064 RID: 100
		private static MessageLogger instance = new MessageLogger();
	}
}
