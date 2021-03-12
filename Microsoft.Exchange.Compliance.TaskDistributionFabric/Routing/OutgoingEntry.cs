using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Diagnostics;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Routing
{
	// Token: 0x0200001D RID: 29
	internal class OutgoingEntry : Entry
	{
		// Token: 0x06000087 RID: 135 RVA: 0x000046A0 File Offset: 0x000028A0
		public OutgoingEntry(ComplianceMessage message)
		{
			base.MessageId = message.MessageId;
			base.CorrelationId = message.CorrelationId;
			base.Message = message;
			base.ExpiryTime = TaskDistributionSettings.OutgoingEntryExpiryTime;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000046D2 File Offset: 0x000028D2
		// (set) Token: 0x06000089 RID: 137 RVA: 0x000046DA File Offset: 0x000028DA
		public OutgoingEntry.OutgoingEntryStatus Status
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

		// Token: 0x0600008A RID: 138 RVA: 0x000046FC File Offset: 0x000028FC
		public override string GetKey()
		{
			return string.Format("OUTGOING:{0}:{1}", base.CorrelationId, base.MessageId);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00004728 File Offset: 0x00002928
		public override void EvaluateState(bool expired)
		{
			if (this.Status != OutgoingEntry.OutgoingEntryStatus.Failed && ExceptionHandler.IsFaulted(base.Message))
			{
				this.Status = OutgoingEntry.OutgoingEntryStatus.Failed;
				return;
			}
			switch (this.Status)
			{
			case OutgoingEntry.OutgoingEntryStatus.Initialized:
				if (base.RetryCount > TaskDistributionSettings.OutgoingEntryRetriesToFailure)
				{
					ExceptionHandler.FaultMessage(base.Message, FaultDefinition.FromErrorString("Delivery Failed", "EvaluateState", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\OutgoingEntry.cs", 135), true);
					this.Status = OutgoingEntry.OutgoingEntryStatus.Failed;
				}
				else
				{
					this.SendMessage();
				}
				break;
			case OutgoingEntry.OutgoingEntryStatus.Delivered:
				if (base.RetryCount > TaskDistributionSettings.OutgoingEntryRetriesToFailure)
				{
					ExceptionHandler.FaultMessage(base.Message, FaultDefinition.FromErrorString("Delivery message was not returned", "EvaluateState", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\OutgoingEntry.cs", 148), true);
					this.Status = OutgoingEntry.OutgoingEntryStatus.Failed;
				}
				else if (expired)
				{
					this.SendMessage();
				}
				break;
			case OutgoingEntry.OutgoingEntryStatus.Returned:
				if (base.RetryCount > TaskDistributionSettings.OutgoingEntryRetriesToFailure)
				{
					ExceptionHandler.FaultMessage(base.Message, FaultDefinition.FromErrorString("Failed to commit results", "EvaluateState", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\OutgoingEntry.cs", 165), true);
					this.Status = OutgoingEntry.OutgoingEntryStatus.Failed;
				}
				break;
			case OutgoingEntry.OutgoingEntryStatus.Failed:
				if (!ExceptionHandler.IsFaulted(base.Message))
				{
					ExceptionHandler.FaultMessage(base.Message, FaultDefinition.FromErrorString("Unknown failure", "EvaluateState", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\OutgoingEntry.cs", 174), true);
				}
				this.FailMessage();
				break;
			}
			if (base.RetryCount > TaskDistributionSettings.OutgoingEntryRetriesToAbandon)
			{
				base.KeepAlive = false;
				return;
			}
			base.KeepAlive = true;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00004897 File Offset: 0x00002A97
		protected override void UpdateExistingEntry(Entry existing)
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00004899 File Offset: 0x00002A99
		private void SendMessage()
		{
			base.Message.ProtocolContext.DispatchData = this;
			RoutingCache.Instance.QueueDispatch(base.Message);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000048BC File Offset: 0x00002ABC
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
				complianceMessage.Payload = ComplianceSerializer.Serialize<WorkPayload>(WorkPayload.Description, base.Message.ProtocolContext.FaultDefinition.ToPayload());
				RoutingCache.Instance.QueueDispatch(complianceMessage);
			}
		}

		// Token: 0x04000031 RID: 49
		private OutgoingEntry.OutgoingEntryStatus status;

		// Token: 0x0200001E RID: 30
		public enum OutgoingEntryStatus
		{
			// Token: 0x04000033 RID: 51
			Initialized,
			// Token: 0x04000034 RID: 52
			Delivered,
			// Token: 0x04000035 RID: 53
			Returned,
			// Token: 0x04000036 RID: 54
			Failed,
			// Token: 0x04000037 RID: 55
			Completed
		}
	}
}
