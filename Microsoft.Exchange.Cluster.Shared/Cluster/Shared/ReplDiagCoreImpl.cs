using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000087 RID: 135
	internal class ReplDiagCoreImpl : IDiagCoreImpl
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x00012AC4 File Offset: 0x00010CC4
		public ReplDiagCoreImpl()
		{
			this.m_defaultEventSuppressionInterval = TimeSpan.FromSeconds((double)RegistryParameters.CrimsonPeriodicLoggingIntervalInSec);
			this.m_eventLog = new ExEventLog(ExTraceGlobals.ReplayApiTracer.Category, "MSExchangeRepl");
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x00012AF7 File Offset: 0x00010CF7
		public TimeSpan DefaultEventSuppressionInterval
		{
			get
			{
				return this.m_defaultEventSuppressionInterval;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x00012AFF File Offset: 0x00010CFF
		public ExEventLog EventLog
		{
			get
			{
				return this.m_eventLog;
			}
		}

		// Token: 0x040002AE RID: 686
		private readonly TimeSpan m_defaultEventSuppressionInterval;

		// Token: 0x040002AF RID: 687
		private readonly ExEventLog m_eventLog;
	}
}
