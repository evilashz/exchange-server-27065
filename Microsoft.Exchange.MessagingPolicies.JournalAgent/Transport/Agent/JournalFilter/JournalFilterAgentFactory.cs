using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MessagingPolicies.Journaling;

namespace Microsoft.Exchange.Transport.Agent.JournalFilter
{
	// Token: 0x02000010 RID: 16
	public sealed class JournalFilterAgentFactory : SmtpReceiveAgentFactory
	{
		// Token: 0x06000058 RID: 88 RVA: 0x00006BF0 File Offset: 0x00004DF0
		public JournalFilterAgentFactory()
		{
			PerfCounters.IncomingJournalReportsDropped.RawValue = 0L;
			PerfCounters.IncomingJournalReportsDroppedPerHour.RawValue = 0L;
			this.perfCountersWrapper = new JournalPerfCountersWrapper(new Tuple<ExPerformanceCounter, ExPerformanceCounter>[]
			{
				new Tuple<ExPerformanceCounter, ExPerformanceCounter>(PerfCounters.IncomingJournalReportsDropped, PerfCounters.IncomingJournalReportsDroppedPerHour)
			});
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00006C40 File Offset: 0x00004E40
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			return new JournalFilterAgent(this.perfCountersWrapper);
		}

		// Token: 0x0400006B RID: 107
		private JournalPerfCountersWrapper perfCountersWrapper;
	}
}
