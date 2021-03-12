using System;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Performance;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.WorkingSet.SignalApiEx;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Data.Transport.StoreDriver;
using Microsoft.Exchange.Data.Transport.StoreDriverDelivery;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.WorkingSet;
using Microsoft.Exchange.Diagnostics.Performance;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents.WorkingSetActionProcessors;
using Microsoft.Exchange.WorkingSet.SignalApi;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x020000D5 RID: 213
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class WorkingSetAgent : StoreDriverDeliveryAgent
	{
		// Token: 0x06000662 RID: 1634 RVA: 0x00023CF1 File Offset: 0x00021EF1
		public WorkingSetAgent()
		{
			this.traceId = this.GetHashCode();
			this.actionProcessorFactory = new ActionProcessorFactory(this);
			base.OnPromotedMessage += this.OnPromotedMessageHandler;
		}

		// Token: 0x06000663 RID: 1635 RVA: 0x00023D24 File Offset: 0x00021F24
		public void OnPromotedMessageHandler(StoreDriverEventSource source, StoreDriverDeliveryEventArgs args)
		{
			WorkingSetAgent.tracer.TraceDebug((long)this.traceId, "WorkingSetAgent.OnPromotedMessageHandler: entering");
			StoreDriverDeliveryEventArgsImpl storeDriverDeliveryEventArgsImpl = args as StoreDriverDeliveryEventArgsImpl;
			if (storeDriverDeliveryEventArgsImpl == null)
			{
				WorkingSetAgent.tracer.TraceError((long)this.traceId, "WorkingSetAgent.OnPromotedMessageHandler: args is null or not a StoreDriverDeliveryEventArgsImpl; exiting");
				return;
			}
			if (!this.Validate(storeDriverDeliveryEventArgsImpl))
			{
				return;
			}
			string arg = storeDriverDeliveryEventArgsImpl.MailboxSession.MdbGuid.ToString();
			WorkingSetAgent.tracer.TraceDebug<string>((long)this.traceId, "WorkingSetAgent.OnPromotedMessageHandler: mdbGuid is {0}", arg);
			WorkingSetAgentPerfLogging logger = new WorkingSetAgentPerfLogging();
			DateTime utcNow = DateTime.UtcNow;
			bool workingSetSignalMailProcessed = false;
			try
			{
				WorkingSet.ProcessingAccepted.Increment();
				using (new StopwatchPerformanceTracker("OnPromotedMessageHandler", logger))
				{
					using (new CpuPerformanceTracker("OnPromotedMessageHandler", logger))
					{
						using (new StorePerformanceTracker("OnPromotedMessageHandler", logger))
						{
							this.Process(storeDriverDeliveryEventArgsImpl);
							workingSetSignalMailProcessed = true;
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (ex is OutOfMemoryException || ex is StackOverflowException || ex is ThreadAbortException)
				{
					throw;
				}
				if (ex is SmtpResponseException)
				{
					workingSetSignalMailProcessed = true;
					throw;
				}
				WorkingSetAgent.tracer.TraceError<Exception>((long)this.traceId, "WorkingSetAgent.OnPromotedMessageHandler encountered an exception: {0}", ex);
				StoreDriverDeliveryDiagnostics.LogEvent(MailboxTransportEventLogConstants.Tuple_WorkingSetAgentException, ex.Message, new object[]
				{
					ex
				});
				storeDriverDeliveryEventArgsImpl.MailRecipient.DsnRequested = DsnRequestedFlags.Never;
				throw new SmtpResponseException(WorkingSetAgent.UnprocessedSignalMailResponse, base.Name);
			}
			finally
			{
				this.UpdatePerfCounters(utcNow, workingSetSignalMailProcessed, logger);
				WorkingSetAgent.tracer.TraceDebug((long)this.traceId, "WorkingSetAgent.OnPromotedMessageHandler: exiting");
			}
		}

		// Token: 0x06000664 RID: 1636 RVA: 0x00023F0C File Offset: 0x0002210C
		private bool Validate(StoreDriverDeliveryEventArgsImpl argsImpl)
		{
			WorkingSetAgent.tracer.TraceDebug((long)this.traceId, "WorkingSetAgent.Validate: entering");
			if (!this.IsMessageInteresting(argsImpl))
			{
				WorkingSetAgent.tracer.TraceDebug((long)this.traceId, "WorkingSetAgent.Validate: not a signal mail; exiting");
				return false;
			}
			if (argsImpl.MailboxSession == null)
			{
				WorkingSetAgent.tracer.TraceError((long)this.traceId, "WorkingSetAgent.Validate: MailboxSession is null; exiting");
				return false;
			}
			if (!WorkingSetUtils.IsWorkingSetAgentFeatureEnabled(argsImpl.MailboxOwner))
			{
				WorkingSetAgent.tracer.TraceDebug((long)this.traceId, "WorkingSetAgent.Validate: received Signal mail while agent is disabled, throw Smtp response exception");
				WorkingSet.ProcessingRejected.Increment();
				argsImpl.MailRecipient.DsnRequested = DsnRequestedFlags.Never;
				throw new SmtpResponseException(WorkingSetAgent.UnprocessedSignalMailResponse, base.Name);
			}
			if (argsImpl.ReplayItem == null)
			{
				WorkingSetAgent.tracer.TraceDebug((long)this.traceId, "WorkingSetAgent.Validate: replay item is null; exiting");
				return false;
			}
			if (!this.IsAuthenticationSourceInternal(argsImpl.ReplayItem))
			{
				WorkingSetAgent.tracer.TraceDebug((long)this.traceId, "WorkingSetAgent.Validate: signal mail is not from internal source; exiting");
				return false;
			}
			return true;
		}

		// Token: 0x06000665 RID: 1637 RVA: 0x00024004 File Offset: 0x00022204
		private void Process(StoreDriverDeliveryEventArgsImpl argsImpl)
		{
			WorkingSetAgent.tracer.TraceDebug((long)this.traceId, "WorkingSetAgent.Process: entering");
			MessageItem replayItem = argsImpl.ReplayItem;
			Unpacker unpacker = new Unpacker();
			WorkingSetAgent.tracer.TraceDebug((long)this.traceId, "WorkingSetAgent.Process: unpacked the signal");
			using (Signal signal = unpacker.Unpack(replayItem))
			{
				if (signal.Operation != null)
				{
					throw new NotSupportedException(string.Format("Operation type not supported - {0}", signal.Operation));
				}
				this.actionProcessorFactory.GetActionProcessor(ActionProcessorType.AddActionProcessor).Process(argsImpl, signal.Action, this.traceId);
			}
		}

		// Token: 0x06000666 RID: 1638 RVA: 0x000240B4 File Offset: 0x000222B4
		private void UpdatePerfCounters(DateTime startTime, bool workingSetSignalMailProcessed, WorkingSetAgentPerfLogging logger)
		{
			WorkingSetAgent.tracer.TraceDebug((long)this.traceId, "WorkingSetAgent.UpdatePerfCounters: entering");
			TimeSpan timeSpan = DateTime.UtcNow.Subtract(startTime);
			WorkingSet.LastSignalProcessingTime.RawValue = (long)timeSpan.TotalMilliseconds;
			WorkingSet.AverageSignalProcessingTime.IncrementBy((long)timeSpan.TotalMilliseconds);
			WorkingSet.AverageSignalProcessingTimeBase.Increment();
			if (workingSetSignalMailProcessed)
			{
				WorkingSet.ProcessingSuccess.Increment();
			}
			else
			{
				WorkingSet.ProcessingFailed.Increment();
			}
			WorkingSetAgent.tracer.TraceDebug<TimeSpan>((long)this.traceId, "WorkingSetAgent.UpdatePerfCounters: StopwatchTime = {0}", logger.StopwatchTime);
			WorkingSetAgent.tracer.TraceDebug<TimeSpan>((long)this.traceId, "WorkingSetAgent.UpdatePerfCounters: CpuTime = {0}", logger.CpuTime);
			WorkingSetAgent.tracer.TraceDebug<uint>((long)this.traceId, "WorkingSetAgent.UpdatePerfCounters: StoreRPCs = {0}", logger.StoreRPCs);
			WorkingSet.AverageStopWatchTime.IncrementBy(logger.StopwatchTime.Ticks);
			WorkingSet.AverageStopWatchTimeBase.Increment();
			WorkingSet.AverageCpuTime.IncrementBy(logger.CpuTime.Ticks);
			WorkingSet.AverageCpuTimeBase.Increment();
			WorkingSet.AverageStoreRPCs.IncrementBy((long)((ulong)logger.StoreRPCs));
			WorkingSet.AverageStoreRPCsBase.Increment();
		}

		// Token: 0x06000667 RID: 1639 RVA: 0x000241EA File Offset: 0x000223EA
		private bool IsMessageInteresting(StoreDriverDeliveryEventArgsImpl argsImpl)
		{
			return argsImpl.MessageClass.Equals("IPM.WorkingSet.Signal", StringComparison.InvariantCulture);
		}

		// Token: 0x06000668 RID: 1640 RVA: 0x00024200 File Offset: 0x00022400
		private bool IsAuthenticationSourceInternal(MessageItem messageItem)
		{
			object obj = messageItem.TryGetProperty(MessageItemSchema.XMsExchOrganizationAuthAs);
			return obj != null && !(obj is PropertyError) && obj.ToString().Equals("internal", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000381 RID: 897
		private const string OnPromotedMessageHandlerMarker = "OnPromotedMessageHandler";

		// Token: 0x04000382 RID: 898
		private static readonly Trace tracer = ExTraceGlobals.WorkingSetAgentTracer;

		// Token: 0x04000383 RID: 899
		private static readonly SmtpResponse UnprocessedSignalMailResponse = new SmtpResponse("250", "2.1.6", new string[]
		{
			"WorkingSetAgent; Suppressing delivery of Signal mail"
		});

		// Token: 0x04000384 RID: 900
		private readonly int traceId;

		// Token: 0x04000385 RID: 901
		private readonly ActionProcessorFactory actionProcessorFactory;
	}
}
