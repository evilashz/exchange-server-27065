using System;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.EseRepl;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000345 RID: 837
	internal abstract class LogSource
	{
		// Token: 0x06002225 RID: 8741 RVA: 0x0009F85E File Offset: 0x0009DA5E
		internal static LogSource Construct(IReplayConfiguration config, IPerfmonCounters perfmonCounters, NetworkPath initialNetworkPath, int timeoutMs)
		{
			return new LogCopyClient(config, perfmonCounters, initialNetworkPath, timeoutMs);
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0009F869 File Offset: 0x0009DA69
		internal static int GetLogShipTimeoutInMsec(bool runningAcll)
		{
			if (!runningAcll)
			{
				return RegistryParameters.LogShipTimeoutInMsec;
			}
			return RegistryParameters.LogShipACLLTimeoutInMsec;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x0009F879 File Offset: 0x0009DA79
		public virtual void SetTimeoutInMsec(int timeoutInMs)
		{
			this.m_defaultTimeoutInMs = timeoutInMs;
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x0009F882 File Offset: 0x0009DA82
		internal void RecordThruput(long byteCount)
		{
			if (this.m_perfmonCounters != null)
			{
				this.m_perfmonCounters.RecordLogCopyThruput(byteCount);
			}
		}

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06002229 RID: 8745 RVA: 0x0009F898 File Offset: 0x0009DA98
		public long CachedEndOfLog
		{
			get
			{
				return this.m_endOfLog.Generation;
			}
		}

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x0009F8A5 File Offset: 0x0009DAA5
		public DateTime CachedEndOfLogWriteTimeUtc
		{
			get
			{
				return this.m_endOfLog.Utc;
			}
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x0009F8B2 File Offset: 0x0009DAB2
		public bool IsLogInRange(long prospect)
		{
			return prospect <= this.m_endOfLog.Generation;
		}

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x0600222C RID: 8748 RVA: 0x0009F8C5 File Offset: 0x0009DAC5
		public virtual string SourcePath
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x0600222D RID: 8749 RVA: 0x0009F8C8 File Offset: 0x0009DAC8
		public int DefaultTimeoutInMs
		{
			get
			{
				return this.m_defaultTimeoutInMs;
			}
		}

		// Token: 0x0600222E RID: 8750
		public abstract void Cancel();

		// Token: 0x0600222F RID: 8751
		public abstract void Close();

		// Token: 0x06002230 RID: 8752
		public abstract long QueryLogRange();

		// Token: 0x06002231 RID: 8753
		public abstract long QueryEndOfLog();

		// Token: 0x06002232 RID: 8754
		public abstract void CopyLog(long logNum, string toFile, out DateTime writeTimeUtc);

		// Token: 0x06002233 RID: 8755 RVA: 0x0009F8D0 File Offset: 0x0009DAD0
		public void CopyLog(long logNum, string toFile)
		{
			DateTime dateTime;
			this.CopyLog(logNum, toFile, out dateTime);
		}

		// Token: 0x06002234 RID: 8756
		public abstract long GetE00Generation();

		// Token: 0x06002235 RID: 8757
		public abstract bool LogExists(long logNum);

		// Token: 0x06002236 RID: 8758 RVA: 0x0009F8E8 File Offset: 0x0009DAE8
		protected void AllocateBuffer()
		{
			lock (this)
			{
				if (this.m_buffer == null)
				{
					this.m_buffer = new byte[1048576];
				}
			}
		}

		// Token: 0x04000E28 RID: 3624
		public const int LogFileSize = 1048576;

		// Token: 0x04000E29 RID: 3625
		protected byte[] m_buffer;

		// Token: 0x04000E2A RID: 3626
		protected IReplayConfiguration m_config;

		// Token: 0x04000E2B RID: 3627
		protected IPerfmonCounters m_perfmonCounters;

		// Token: 0x04000E2C RID: 3628
		protected bool m_cancelling;

		// Token: 0x04000E2D RID: 3629
		protected EndOfLog m_endOfLog = new EndOfLog();

		// Token: 0x04000E2E RID: 3630
		protected int m_defaultTimeoutInMs;
	}
}
