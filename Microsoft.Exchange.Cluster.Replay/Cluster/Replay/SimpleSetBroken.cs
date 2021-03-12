using System;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000154 RID: 340
	internal class SimpleSetBroken : ISetBroken, ISetDisconnected, IReplicaProgress
	{
		// Token: 0x06000D30 RID: 3376 RVA: 0x0003A42F File Offset: 0x0003862F
		public SimpleSetBroken(string database)
		{
			this.m_dbIdentity = database;
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0003A43E File Offset: 0x0003863E
		public bool Broken
		{
			get
			{
				return this.m_fBroken;
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0003A446 File Offset: 0x00038646
		public bool IsBroken
		{
			get
			{
				return this.Broken;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000D33 RID: 3379 RVA: 0x0003A44E File Offset: 0x0003864E
		public bool IsDisconnected
		{
			get
			{
				return this.m_fDisconnected;
			}
		}

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000D34 RID: 3380 RVA: 0x0003A456 File Offset: 0x00038656
		public LocalizedString ErrorMessage
		{
			get
			{
				return this.m_errorMessage;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000D35 RID: 3381 RVA: 0x0003A45E File Offset: 0x0003865E
		public Exception ExtendedErrorInformation
		{
			get
			{
				return this.m_exception;
			}
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0003A466 File Offset: 0x00038666
		public void SetBroken(FailureTag failureTag, ExEventLog.EventTuple failureNotificationEventTuple, ExEventLog.EventTuple setBrokenEventTuple, params string[] setBrokenArgs)
		{
			this.SetBroken(failureTag, setBrokenEventTuple, setBrokenArgs);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0003A472 File Offset: 0x00038672
		public void SetBroken(FailureTag failureTag, ExEventLog.EventTuple failureNotificationEventTuple, string[] failureNotificationMessageArgs, ExEventLog.EventTuple setBrokenEventTuple, params string[] setBrokenArgs)
		{
			this.SetBroken(failureTag, setBrokenEventTuple, setBrokenArgs);
		}

		// Token: 0x06000D38 RID: 3384 RVA: 0x0003A480 File Offset: 0x00038680
		public void SetBroken(FailureTag failureTag, ExEventLog.EventTuple setBrokenEventTuple, params string[] setBrokenArgs)
		{
			this.m_fBroken = true;
			string[] argumentsWithDb = ReplicaInstance.GetArgumentsWithDb(setBrokenArgs, this.m_dbIdentity);
			int num;
			string value = setBrokenEventTuple.EventLogToString(out num, argumentsWithDb);
			this.m_errorMessage = new LocalizedString(value);
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0003A4B7 File Offset: 0x000386B7
		public void SetBroken(FailureTag failureTag, ExEventLog.EventTuple setBrokenEventTuple, Exception exception, params string[] setBrokenArgs)
		{
			this.m_exception = exception;
			this.SetBroken(failureTag, setBrokenEventTuple, setBrokenArgs);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0003A4CA File Offset: 0x000386CA
		public void SetBroken(FailureTag failureTag, ExEventLog.EventTuple failureNotificationEventTuple, ExEventLog.EventTuple setBrokenEventTuple, Exception exception, params string[] setBrokenArgs)
		{
			this.m_exception = exception;
			this.SetBroken(failureTag, failureNotificationEventTuple, setBrokenEventTuple, setBrokenArgs);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0003A4DF File Offset: 0x000386DF
		public void SetDisconnected(FailureTag failureTag, ExEventLog.EventTuple failureNotificationEventTuple, ExEventLog.EventTuple setBrokenEventTuple, params string[] setBrokenArgs)
		{
			this.SetDisconnected(failureTag, setBrokenEventTuple, setBrokenArgs);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0003A4EC File Offset: 0x000386EC
		public void SetDisconnected(FailureTag failureTag, ExEventLog.EventTuple setBrokenEventTuple, params string[] setBrokenArgs)
		{
			this.m_fDisconnected = true;
			string[] argumentsWithDb = ReplicaInstance.GetArgumentsWithDb(setBrokenArgs, this.m_dbIdentity);
			int num;
			string value = setBrokenEventTuple.EventLogToString(out num, argumentsWithDb);
			this.m_errorMessage = new LocalizedString(value);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0003A523 File Offset: 0x00038723
		public void ClearBroken()
		{
			this.m_fBroken = false;
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0003A52C File Offset: 0x0003872C
		public void ClearDisconnected()
		{
			this.m_fDisconnected = false;
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003A535 File Offset: 0x00038735
		public void RestartInstanceSoon(bool fPrepareToStop)
		{
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0003A537 File Offset: 0x00038737
		public void RestartInstanceSoonAdminVisible()
		{
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0003A539 File Offset: 0x00038739
		public void RestartInstanceNow(ReplayConfigChangeHints restartReason)
		{
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0003A53B File Offset: 0x0003873B
		public void ReportOneLogCopied()
		{
		}

		// Token: 0x06000D43 RID: 3395 RVA: 0x0003A53D File Offset: 0x0003873D
		public void ReportLogsReplayed(long numLogs)
		{
		}

		// Token: 0x04000592 RID: 1426
		private bool m_fBroken;

		// Token: 0x04000593 RID: 1427
		private bool m_fDisconnected;

		// Token: 0x04000594 RID: 1428
		private LocalizedString m_errorMessage;

		// Token: 0x04000595 RID: 1429
		private string m_dbIdentity;

		// Token: 0x04000596 RID: 1430
		private Exception m_exception;
	}
}
