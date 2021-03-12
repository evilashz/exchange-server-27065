using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200055D RID: 1373
	internal sealed class ApplicationEventInfo : ReplicationEventBaseInfo
	{
		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x000C5BE6 File Offset: 0x000C3DE6
		public ExEventLog.EventTuple EventTuple
		{
			get
			{
				return this.m_eventTuple;
			}
		}

		// Token: 0x060030D5 RID: 12501 RVA: 0x000C5BF0 File Offset: 0x000C3DF0
		public ApplicationEventInfo(ExEventLog.EventTuple eventTuple) : base(ReplicationEventType.AppLog, false, null)
		{
			this.m_eventTuple = eventTuple;
		}

		// Token: 0x040022AC RID: 8876
		private ExEventLog.EventTuple m_eventTuple;
	}
}
