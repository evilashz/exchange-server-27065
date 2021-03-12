using System;
using System.Threading;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class TimerComponent : DisposeTrackableBase, IStartStop
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000029D0 File Offset: 0x00000BD0
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000029D8 File Offset: 0x00000BD8
		protected DateTime PrepareToStopTime { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000029E1 File Offset: 0x00000BE1
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000029E9 File Offset: 0x00000BE9
		protected DateTime StartOfStopTime { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000029F2 File Offset: 0x00000BF2
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000029FA File Offset: 0x00000BFA
		protected DateTime EndOfStopTime { get; set; }

		// Token: 0x0600003F RID: 63 RVA: 0x00002A03 File Offset: 0x00000C03
		public TimerComponent(TimeSpan initialDueTime, TimeSpan period, string name)
		{
			this.m_name = name;
			this.m_initialDueTime = initialDueTime;
			this.m_period = period;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002A20 File Offset: 0x00000C20
		protected bool PrepareToStopCalled
		{
			get
			{
				return this.m_fShutdown;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002A28 File Offset: 0x00000C28
		protected TimeSpan Period
		{
			get
			{
				return this.m_period;
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002A30 File Offset: 0x00000C30
		public void Start()
		{
			lock (this)
			{
				if (this.m_fShutdown)
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: TimerComponent.Start() skipping since m_fShutdown is true.", this.m_name);
				}
				else
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<string, TimeSpan, TimeSpan>((long)this.GetHashCode(), "{0}: TimerComponent is now starting. m_initialDueTime = {1}, m_period = {2}", this.m_name, this.m_initialDueTime, this.m_period);
					this.m_timer = new Timer(new TimerCallback(this.TimerCallback), null, this.m_initialDueTime, this.m_period);
				}
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002AD8 File Offset: 0x00000CD8
		public void PrepareToStop()
		{
			lock (this)
			{
				if (this.m_fShutdown)
				{
					return;
				}
				this.m_fShutdown = true;
				this.PrepareToStopTime = DateTime.UtcNow;
			}
			if (this.m_timer != null)
			{
				this.m_timer.Change(-1, -1);
				this.m_disposedEvent = new ManualResetEvent(false);
				this.m_timer.Dispose(this.m_disposedEvent);
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002B60 File Offset: 0x00000D60
		public void ChangeTimer(TimeSpan dueTime, TimeSpan period)
		{
			lock (this)
			{
				if (!this.m_fShutdown)
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<string, TimeSpan, TimeSpan>((long)this.GetHashCode(), "{0}: TimerComponent.ChangeTimer(): Changing to: dueTime={1}, period={2}.", this.m_name, dueTime, period);
					this.m_timer.Change(dueTime, period);
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002BCC File Offset: 0x00000DCC
		public void StartNow()
		{
			lock (this)
			{
				if (!this.m_fShutdown)
				{
					if (!this.m_fInCallback)
					{
						this.m_fStartNow = false;
						this.m_timer.Change(TimeSpan.Zero, this.m_period);
					}
					else
					{
						this.m_fStartNow = true;
					}
				}
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002C38 File Offset: 0x00000E38
		public void Stop()
		{
			GC.SuppressFinalize(this);
			base.Dispose(true);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002C47 File Offset: 0x00000E47
		public new void Dispose()
		{
			this.Stop();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002C4F File Offset: 0x00000E4F
		protected virtual void StopInternal()
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002C54 File Offset: 0x00000E54
		public void TimerCallback(object ignoredState)
		{
			lock (this)
			{
				if (this.m_fShutdown)
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: TimerCallback() is bailing due to shutdown.", this.m_name);
					return;
				}
				if (this.m_fInCallback)
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}: TimerCallback() is bailing because another thread is already working.", this.m_name);
					return;
				}
				this.m_fInCallback = true;
			}
			try
			{
				this.TimerCallbackInternal();
			}
			finally
			{
				lock (this)
				{
					this.m_fInCallback = false;
					if (this.m_fStartNow)
					{
						this.StartNow();
					}
				}
			}
		}

		// Token: 0x0600004A RID: 74
		protected abstract void TimerCallbackInternal();

		// Token: 0x0600004B RID: 75 RVA: 0x00002D2C File Offset: 0x00000F2C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				bool flag = false;
				lock (this)
				{
					if (!this.m_fShutdown)
					{
						this.PrepareToStop();
					}
					if (!this.m_stopped)
					{
						this.m_stopped = true;
						flag = true;
					}
				}
				if (flag)
				{
					this.StartOfStopTime = DateTime.UtcNow;
					if (this.m_disposedEvent != null)
					{
						try
						{
							this.m_disposedEvent.WaitOne();
						}
						finally
						{
							this.m_disposedEvent.Close();
						}
						this.m_disposedEvent = null;
					}
					this.m_timer = null;
					this.EndOfStopTime = DateTime.UtcNow;
					this.StopInternal();
				}
			}
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002DE4 File Offset: 0x00000FE4
		protected void LogStopEventAndSetFinalStopTime(string instanceName)
		{
			this.EndOfStopTime = DateTime.UtcNow;
			TimerComponent.LogStopEvent(this.m_name, instanceName, this.PrepareToStopTime, this.StartOfStopTime, this.EndOfStopTime);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E10 File Offset: 0x00001010
		public static void LogStopEvent(string componentName, string instanceName, DateTime prepareToStopTime, DateTime startOfStopTime, DateTime endOfStopTime)
		{
			TimeSpan timeSpan = endOfStopTime - prepareToStopTime;
			ReplayCrimsonEvents.TimerComponentStopped.Log<string, string, TimeSpan, DateTime, DateTime, DateTime>(componentName, instanceName, timeSpan, prepareToStopTime, startOfStopTime, endOfStopTime);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E37 File Offset: 0x00001037
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<TimerComponent>(this);
		}

		// Token: 0x0400000B RID: 11
		private readonly string m_name;

		// Token: 0x0400000C RID: 12
		private readonly TimeSpan m_initialDueTime;

		// Token: 0x0400000D RID: 13
		private readonly TimeSpan m_period;

		// Token: 0x0400000E RID: 14
		private Timer m_timer;

		// Token: 0x0400000F RID: 15
		private ManualResetEvent m_disposedEvent;

		// Token: 0x04000010 RID: 16
		private bool m_fShutdown;

		// Token: 0x04000011 RID: 17
		private bool m_fInCallback;

		// Token: 0x04000012 RID: 18
		private bool m_stopped;

		// Token: 0x04000013 RID: 19
		private bool m_fStartNow;
	}
}
