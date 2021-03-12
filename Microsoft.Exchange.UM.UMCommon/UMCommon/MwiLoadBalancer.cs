using System;
using System.Diagnostics;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x020000CD RID: 205
	internal class MwiLoadBalancer
	{
		// Token: 0x060006C9 RID: 1737 RVA: 0x0001A424 File Offset: 0x00018624
		internal MwiLoadBalancer(Microsoft.Exchange.Diagnostics.Trace tracer, IServerPicker<IMwiTarget, Guid> serverPicker, MwiFailureEventLogStrategy eventLogStrategy)
		{
			this.tracer = tracer;
			this.serverPicker = serverPicker;
			this.activeMessageCount = 0;
			this.shutdownInProgress = false;
			this.shutdownEvent = new AutoResetEvent(false);
			this.eventLogStrategy = eventLogStrategy;
			this.InitializePerfCounters();
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x0001A479 File Offset: 0x00018679
		internal AutoResetEvent ShutDownEvent
		{
			get
			{
				return this.shutdownEvent;
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0001A484 File Offset: 0x00018684
		internal void SendMessage(MwiMessage message)
		{
			try
			{
				this.TraceDebug("MwiLoadBalancer.SendMessage(message={0})", new object[]
				{
					message
				});
				if (this.perfCounters != null)
				{
					MwiDiagnostics.IncrementCounter(this.perfCounters.TotalMwiMessages);
				}
				this.SendMessageToNextTarget(message);
			}
			catch (DataSourceTransientException error)
			{
				this.HandleDeliveryFailure(message, error);
			}
			catch (DataValidationException error2)
			{
				this.HandleDeliveryFailure(message, error2);
			}
			catch (DataSourceOperationException error3)
			{
				this.HandleDeliveryFailure(message, error3);
			}
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001A514 File Offset: 0x00018714
		internal void Shutdown()
		{
			bool flag = false;
			lock (this)
			{
				this.shutdownInProgress = true;
				flag = (this.activeMessageCount > 0);
			}
			if (flag)
			{
				this.TraceDebug("MwiLoadBalancer.Shutdown: Waiting for shutdownEvent event.", new object[0]);
				if (!this.shutdownEvent.WaitOne(MwiLoadBalancer.ShutdownTimeout, false))
				{
					this.TraceWarning("MwiLoadBalancer.Shutdown: Timed out waiting for shutdown event", new object[0]);
				}
			}
			this.shutdownEvent.Close();
			this.shutdownEvent = null;
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x0001A5A8 File Offset: 0x000187A8
		internal void ShutdownAsync()
		{
			lock (this)
			{
				this.shutdownInProgress = true;
				if (this.activeMessageCount <= 0)
				{
					this.shutdownEvent.Set();
				}
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0001A5FC File Offset: 0x000187FC
		internal void CleanupAfterAsyncShutdown()
		{
			this.shutdownEvent.Close();
			this.shutdownEvent = null;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0001A610 File Offset: 0x00018810
		internal virtual IServerPicker<IMwiTarget, Guid> GetTargetPicker(Guid dialPlanGuid)
		{
			return this.serverPicker;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0001A618 File Offset: 0x00018818
		private void InitializePerfCounters()
		{
			string processName;
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				processName = currentProcess.ProcessName;
			}
			this.perfCounters = MwiDiagnostics.GetInstance(processName);
			this.perfCounters.Reset();
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001A668 File Offset: 0x00018868
		private void SendMessageToNextTarget(MwiMessage message)
		{
			this.TraceDebug("MwiLoadBalancer.SendMessageToNextTarget: GetNextAvailableTarget()", new object[0]);
			if (MwiLoadBalancer.outstandingRequestsCount >= MwiLoadBalancer.MaxNumOfOutstandingRequests)
			{
				this.HandleDeliveryFailure(message, new TooManyOustandingMwiRequestsException(message.UserName));
				return;
			}
			if (message.Expired)
			{
				this.HandleDeliveryFailure(message, new MwiMessageExpiredException(message.UserName));
				return;
			}
			IServerPicker<IMwiTarget, Guid> targetPicker = this.GetTargetPicker(message.DialPlanGuid);
			int num = 0;
			IMwiTarget mwiTarget = null;
			for (int i = 0; i < 2; i++)
			{
				mwiTarget = targetPicker.PickNextServer(message.DialPlanGuid, message.TenantGuid, out num);
				if (mwiTarget == null || !mwiTarget.Equals(message.CurrentTarget))
				{
					break;
				}
			}
			if (mwiTarget == null || mwiTarget.Equals(message.CurrentTarget) || message.NumberOfTargetsAttempted >= num)
			{
				this.HandleDeliveryFailure(message, new MwiNoTargetsAvailableException(message.UserName));
				return;
			}
			lock (this)
			{
				if (!this.shutdownInProgress)
				{
					this.activeMessageCount++;
					int num2 = Interlocked.Increment(ref MwiLoadBalancer.outstandingRequestsCount);
					message.SendAsync(mwiTarget, new SendMessageCompletedDelegate(this.SendMessageCompletedCallback));
					this.TraceDebug("MwiLB.SendMessageToNextTarget: MsgCount={0} TotalReqs={1}", new object[]
					{
						this.activeMessageCount,
						num2
					});
				}
				else
				{
					this.TraceWarning("MwiLB.SendMessageToNextTarget: Shutting down, discarding message", new object[0]);
				}
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001A7D4 File Offset: 0x000189D4
		private void SendMessageCompletedCallback(MwiMessage message, MwiDeliveryException error)
		{
			lock (this)
			{
				if (this.perfCounters != null)
				{
					TimeSpan timeSpan = ExDateTime.UtcNow.Subtract(message.SentTimeUtc);
					MwiDiagnostics.SetCounterValue(this.perfCounters.AverageMwiProcessingTime, this.averageMwiProcessingTime.Update((long)timeSpan.TotalMilliseconds));
				}
				this.activeMessageCount--;
				int num = Interlocked.Decrement(ref MwiLoadBalancer.outstandingRequestsCount);
				this.TraceWarning("LB.SendMessageCompletedCallback: Msg:{0} Err={1} MsgCnt={2} TotalReqs={3} Shutdown={4}", new object[]
				{
					message,
					error,
					this.activeMessageCount,
					num,
					this.shutdownInProgress
				});
				if (this.shutdownInProgress)
				{
					if (this.activeMessageCount == 0 && this.shutdownEvent != null)
					{
						this.TraceWarning("LB.SendMessageCompletedCallback: There are no pending requests->shutdownEvent.Set()", new object[0]);
						this.shutdownEvent.Set();
					}
					return;
				}
			}
			if (error != null)
			{
				message.DeliveryErrors.Add(error);
				IServerPicker<IMwiTarget, Guid> targetPicker = this.GetTargetPicker(message.DialPlanGuid);
				targetPicker.ServerUnavailable(message.CurrentTarget);
				this.SendMessageToNextTarget(message);
				return;
			}
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_MwiMessageDeliverySucceeded, null, new object[]
			{
				message.UnreadVoicemailCount,
				message.TotalVoicemailCount - message.UnreadVoicemailCount,
				message.MailboxDisplayName,
				message.UserExtension,
				message.CurrentTarget.Name
			});
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001A978 File Offset: 0x00018B78
		private void HandleDeliveryFailure(MwiMessage message, Exception error)
		{
			this.TraceError("MwiLoadBalancer.HandleDeliveryFailure: Message={0}. Error={1}", new object[]
			{
				message,
				error
			});
			if (this.perfCounters != null)
			{
				MwiDiagnostics.IncrementCounter(this.perfCounters.TotalFailedMwiMessages);
			}
			this.eventLogStrategy.LogFailure(message, error);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001A9C5 File Offset: 0x00018BC5
		private void TraceDebug(string format, params object[] args)
		{
			CallIdTracer.TraceDebug(this.tracer, this.GetHashCode(), format, args);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001A9DF File Offset: 0x00018BDF
		private void TraceWarning(string format, params object[] args)
		{
			CallIdTracer.TraceWarning(this.tracer, this.GetHashCode(), format, args);
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0001A9F9 File Offset: 0x00018BF9
		private void TraceError(string format, params object[] args)
		{
			CallIdTracer.TraceError(this.tracer, this.GetHashCode(), format, args);
		}

		// Token: 0x040003F7 RID: 1015
		internal static readonly TimeSpan ShutdownTimeout = new TimeSpan(0, 1, 0);

		// Token: 0x040003F8 RID: 1016
		internal static readonly int MaxNumOfOutstandingRequests = 500 * Environment.ProcessorCount;

		// Token: 0x040003F9 RID: 1017
		private static int outstandingRequestsCount = 0;

		// Token: 0x040003FA RID: 1018
		private MwiLoadBalancerPerformanceCountersInstance perfCounters;

		// Token: 0x040003FB RID: 1019
		private MovingAverage averageMwiProcessingTime = new MovingAverage(50);

		// Token: 0x040003FC RID: 1020
		private int activeMessageCount;

		// Token: 0x040003FD RID: 1021
		private bool shutdownInProgress;

		// Token: 0x040003FE RID: 1022
		private AutoResetEvent shutdownEvent;

		// Token: 0x040003FF RID: 1023
		private Microsoft.Exchange.Diagnostics.Trace tracer;

		// Token: 0x04000400 RID: 1024
		private IServerPicker<IMwiTarget, Guid> serverPicker;

		// Token: 0x04000401 RID: 1025
		private MwiFailureEventLogStrategy eventLogStrategy;
	}
}
