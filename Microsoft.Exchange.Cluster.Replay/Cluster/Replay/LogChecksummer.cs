using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Isam.Esent.Interop;
using Microsoft.Isam.Esent.Interop.Unpublished;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000117 RID: 279
	internal class LogChecksummer : IDisposeTrackable, IDisposable
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0002FB1D File Offset: 0x0002DD1D
		public static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.LogCopyTracer;
			}
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x0002FB24 File Offset: 0x0002DD24
		public LogChecksummer(string basename)
		{
			bool flag = false;
			EseHelper.GlobalInit();
			this.m_disposeTracker = this.GetDisposeTracker();
			this.m_instanceName = string.Format("LogChecksummer {0} {1}", basename, this.GetHashCode());
			Api.JetCreateInstance(out this.m_instance, this.m_instanceName);
			this.m_basename = basename;
			bool jettermNeeded = true;
			try
			{
				InstanceParameters instanceParameters = new InstanceParameters(this.Instance);
				instanceParameters.Recovery = false;
				instanceParameters.MaxTemporaryTables = 0;
				instanceParameters.NoInformationEvent = true;
				instanceParameters.BaseName = basename;
				jettermNeeded = false;
				Api.JetInit(ref this.m_instance);
				jettermNeeded = true;
				Api.JetBeginSession(this.Instance, out this.m_sesid, null, null);
				this.AssertNotTerminated();
				flag = true;
				LogChecksummer.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "LogChecksummer created: {0}. DispTracker=0x{1:X}", this.m_instanceName, this.m_disposeTracker.GetHashCode());
			}
			finally
			{
				if (!flag)
				{
					this.Term(jettermNeeded);
				}
			}
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0002FC18 File Offset: 0x0002DE18
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<LogChecksummer>(this);
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0002FC20 File Offset: 0x0002DE20
		public void SuppressDisposeTracker()
		{
			if (this.m_disposeTracker != null)
			{
				this.m_disposeTracker.Suppress();
			}
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0002FC35 File Offset: 0x0002DE35
		public void Dispose()
		{
			if (!this.m_isDisposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0002FC4C File Offset: 0x0002DE4C
		public void Dispose(bool disposing)
		{
			lock (this)
			{
				if (!this.m_isDisposed)
				{
					if (disposing && this.m_disposeTracker != null)
					{
						this.m_disposeTracker.Dispose();
					}
					this.Term(true);
					this.m_isDisposed = true;
				}
			}
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0002FCB0 File Offset: 0x0002DEB0
		private void Term(bool jettermNeeded)
		{
			lock (this)
			{
				if (!this.m_terminated)
				{
					LogChecksummer.Tracer.TraceDebug<string>((long)this.GetHashCode(), "LogChecksummer terminating: {0}", this.m_instanceName);
					if (jettermNeeded)
					{
						Api.JetTerm(this.Instance);
					}
					this.m_terminated = true;
				}
			}
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0002FD20 File Offset: 0x0002DF20
		public EsentErrorException Verify(string logfile, byte[] logInMemory)
		{
			EsentErrorException ex = null;
			lock (this)
			{
				this.AssertNotTerminated();
				try
				{
					UnpublishedApi.ChecksumLogFromMemory(this.Sesid, logfile, this.m_basename, logInMemory);
					LogChecksummer.Tracer.TraceDebug<string>((long)this.GetHashCode(), "LogChecksummer({0}) verified.", logfile);
				}
				catch (EsentLogFileCorruptException ex2)
				{
					ex = ex2;
				}
				catch (EsentErrorException ex3)
				{
					ex = ex3;
				}
				if (ex != null)
				{
					LogChecksummer.Tracer.TraceError<string, EsentErrorException>((long)this.GetHashCode(), "LogChecksummer({0}) failed:{1}", logfile, ex);
				}
			}
			return ex;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0002FDCC File Offset: 0x0002DFCC
		private void AssertNotTerminated()
		{
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x0002FDCE File Offset: 0x0002DFCE
		private JET_INSTANCE Instance
		{
			get
			{
				this.AssertNotTerminated();
				return this.m_instance;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000AA2 RID: 2722 RVA: 0x0002FDDC File Offset: 0x0002DFDC
		private JET_SESID Sesid
		{
			get
			{
				this.AssertNotTerminated();
				return this.m_sesid;
			}
		}

		// Token: 0x04000477 RID: 1143
		private DisposeTracker m_disposeTracker;

		// Token: 0x04000478 RID: 1144
		private bool m_isDisposed;

		// Token: 0x04000479 RID: 1145
		private readonly JET_INSTANCE m_instance;

		// Token: 0x0400047A RID: 1146
		private readonly JET_SESID m_sesid;

		// Token: 0x0400047B RID: 1147
		private readonly string m_basename;

		// Token: 0x0400047C RID: 1148
		private bool m_terminated;

		// Token: 0x0400047D RID: 1149
		private readonly string m_instanceName;
	}
}
