using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.DxStore;
using Microsoft.Exchange.DxStore.Common;

namespace Microsoft.Exchange.DxStore.Server
{
	// Token: 0x0200005E RID: 94
	public class GroupStatusCollector
	{
		// Token: 0x060003B4 RID: 948 RVA: 0x0000A432 File Offset: 0x00008632
		public GroupStatusCollector(string self, InstanceClientFactory instanceClientFactory, InstanceGroupConfig groupConfig, IDxStoreEventLogger eventLogger, double requiredSuccessPercent)
		{
			this.Initialize(self, instanceClientFactory, groupConfig, eventLogger, requiredSuccessPercent);
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000A452 File Offset: 0x00008652
		public static Microsoft.Exchange.Diagnostics.Trace Tracer
		{
			get
			{
				return ExTraceGlobals.HealthCheckerTracer;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000A459 File Offset: 0x00008659
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000A461 File Offset: 0x00008661
		public IDxStoreEventLogger EventLogger { get; set; }

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0000A46A File Offset: 0x0000866A
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x0000A472 File Offset: 0x00008672
		public GroupStatusInfo GroupStatusInfo { get; set; }

		// Token: 0x060003BA RID: 954 RVA: 0x0000A47B File Offset: 0x0000867B
		public void Initialize(string self, InstanceClientFactory instanceClientFactory, InstanceGroupConfig groupConfig, IDxStoreEventLogger eventLogger, double requiredSuccessPercent)
		{
			this.EventLogger = eventLogger;
			this.self = self;
			this.instanceClientFactory = instanceClientFactory;
			this.GroupStatusInfo = new GroupStatusInfo();
			this.groupConfig = groupConfig;
			this.completionEvent = new ManualResetEvent(false);
			this.requiredSuccessPercent = requiredSuccessPercent;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000A4BC File Offset: 0x000086BC
		public void Run(TimeSpan waitDuration)
		{
			GroupStatusCollector.Tracer.TraceDebug<string>((long)this.groupConfig.Identity.GetHashCode(), "{0}: Starting group status collection", this.groupConfig.Identity);
			Stopwatch stopwatch = new Stopwatch();
			bool flag = false;
			try
			{
				stopwatch.Start();
				this.GroupStatusInfo.CollectionStartTime = DateTimeOffset.Now;
				this.RunInternal(waitDuration);
				flag = true;
			}
			finally
			{
				this.GroupStatusInfo.CollectionDuration = stopwatch.Elapsed;
				this.GroupStatusInfo.Analyze(this.self, this.groupConfig);
				GroupStatusCollector.Tracer.TraceDebug<string, string, string>((long)this.groupConfig.Identity.GetHashCode(), "{0}: Group status collection {1} (Info: {2})", this.groupConfig.Identity, flag ? "success" : "failed", this.GroupStatusInfo.GetDebugString(this.groupConfig.Identity));
			}
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000A5DC File Offset: 0x000087DC
		private void RunInternal(TimeSpan waitDuration)
		{
			GroupStatusInfo groupStatusInfo = this.GroupStatusInfo;
			groupStatusInfo.TotalRequested = this.groupConfig.Members.Length;
			InstanceGroupMemberConfig[] members = this.groupConfig.Members;
			for (int i = 0; i < members.Length; i++)
			{
				InstanceGroupMemberConfig instanceGroupMemberConfig = members[i];
				string memberName = instanceGroupMemberConfig.Name;
				groupStatusInfo.StatusMap[memberName] = null;
				Task.Factory.StartNew(delegate()
				{
					this.CollectStatusForServer(memberName, waitDuration);
				});
			}
			try
			{
				if (this.completionEvent.WaitOne(waitDuration) && groupStatusInfo.TotalNoReplies > 0)
				{
					TimeSpan timeSpan = DateTimeOffset.Now - groupStatusInfo.CollectionStartTime + waitDuration;
					if (timeSpan.TotalMilliseconds > 0.0)
					{
						int millisecondsTimeout = Math.Min((int)timeSpan.TotalMilliseconds, 2000);
						Thread.Sleep(millisecondsTimeout);
					}
				}
			}
			finally
			{
				this.MarkCompletion();
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000A704 File Offset: 0x00008904
		private void MarkCompletion()
		{
			lock (this.locker)
			{
				this.isCompleted = true;
				if (this.completionEvent != null)
				{
					this.completionEvent.Dispose();
					this.completionEvent = null;
				}
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000A7A4 File Offset: 0x000089A4
		private void CollectStatusForServer(string serverName, TimeSpan waitDuration)
		{
			InstanceStatusInfo statusInfo = null;
			Exception ex = null;
			string label = string.Format("GetStatus({0})", serverName);
			if (!this.groupConfig.Settings.IsUseHttpTransportForInstanceCommunication)
			{
				ex = Utils.RunOperation(this.groupConfig.Identity, label, delegate
				{
					DxStoreInstanceClient client = this.instanceClientFactory.GetClient(serverName);
					statusInfo = client.GetStatus(null);
				}, this.EventLogger, LogOptions.LogException | this.groupConfig.Settings.AdditionalLogOptions, true, null, new TimeSpan?(this.groupConfig.Settings.PeriodicExceptionLoggingDuration), null, null, null);
			}
			else
			{
				try
				{
					string memberNetworkAddress = this.groupConfig.GetMemberNetworkAddress(serverName);
					Task<InstanceStatusInfo> statusAsync = HttpClient.GetStatusAsync(memberNetworkAddress, serverName, this.groupConfig.Name, this.self);
					statusAsync.Wait(waitDuration);
					statusInfo = statusAsync.Result;
				}
				catch (Exception ex2)
				{
					ex = ex2;
					this.EventLogger.Log(DxEventSeverity.Error, 0, "http send for GetStatusAsync failed: {0}", new object[]
					{
						ex2.ToString()
					});
				}
			}
			this.UpdateStatus(serverName, statusInfo, ex);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000A8FC File Offset: 0x00008AFC
		private void UpdateStatus(string serverName, InstanceStatusInfo statusInfo, Exception ex)
		{
			lock (this.locker)
			{
				this.UpdateStatusInternal(serverName, statusInfo, ex);
			}
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000A940 File Offset: 0x00008B40
		private void UpdateStatusInternal(string serverName, InstanceStatusInfo statusInfo, Exception ex)
		{
			GroupStatusInfo groupStatusInfo = this.GroupStatusInfo;
			if (!this.isCompleted)
			{
				groupStatusInfo.StatusMap[serverName] = statusInfo;
				if (ex == null)
				{
					groupStatusInfo.TotalSuccessful++;
				}
				else
				{
					groupStatusInfo.TotalFailed++;
				}
				if (!this.isSignaled)
				{
					if (this.completionEvent != null)
					{
						double num = (double)groupStatusInfo.TotalSuccessful * 100.0 / (double)groupStatusInfo.TotalRequested;
						if (groupStatusInfo.TotalRequested == groupStatusInfo.TotalSuccessful + groupStatusInfo.TotalFailed || num >= this.requiredSuccessPercent)
						{
							GroupStatusCollector.Tracer.TraceDebug((long)this.groupConfig.Identity.GetHashCode(), "{0}: Signaling completion (Total: {1}, Success: {2}, Failed: {3}", new object[]
							{
								this.groupConfig.Identity,
								groupStatusInfo.TotalRequested,
								groupStatusInfo.TotalSuccessful,
								groupStatusInfo.TotalFailed
							});
							this.completionEvent.Set();
							this.isSignaled = true;
							return;
						}
					}
				}
				else
				{
					GroupStatusCollector.Tracer.TraceDebug((long)this.groupConfig.Identity.GetHashCode(), "{0}: Already signalled but trying level best to receive max replies in the grace time (Total: {1}, Success: {2}, Failed: {3}", new object[]
					{
						this.groupConfig.Identity,
						groupStatusInfo.TotalRequested,
						groupStatusInfo.TotalSuccessful,
						groupStatusInfo.TotalFailed
					});
				}
			}
		}

		// Token: 0x040001DD RID: 477
		private readonly object locker = new object();

		// Token: 0x040001DE RID: 478
		private InstanceGroupConfig groupConfig;

		// Token: 0x040001DF RID: 479
		private double requiredSuccessPercent;

		// Token: 0x040001E0 RID: 480
		private string self;

		// Token: 0x040001E1 RID: 481
		private bool isCompleted;

		// Token: 0x040001E2 RID: 482
		private bool isSignaled;

		// Token: 0x040001E3 RID: 483
		private ManualResetEvent completionEvent;

		// Token: 0x040001E4 RID: 484
		private InstanceClientFactory instanceClientFactory;
	}
}
