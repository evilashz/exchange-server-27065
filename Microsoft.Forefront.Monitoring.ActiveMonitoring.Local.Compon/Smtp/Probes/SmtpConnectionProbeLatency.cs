using System;
using System.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes
{
	// Token: 0x0200024C RID: 588
	public class SmtpConnectionProbeLatency
	{
		// Token: 0x060013D1 RID: 5073 RVA: 0x0003ABBE File Offset: 0x00038DBE
		public SmtpConnectionProbeLatency(string reason, bool startNow)
		{
			this.Reason = reason;
			if (startNow)
			{
				this.StartRecording();
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0003ABD6 File Offset: 0x00038DD6
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x0003ABDE File Offset: 0x00038DDE
		public ExDateTime StartTime { get; private set; }

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x0003ABE7 File Offset: 0x00038DE7
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x0003ABEF File Offset: 0x00038DEF
		public string Reason { get; private set; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060013D6 RID: 5078 RVA: 0x0003ABF8 File Offset: 0x00038DF8
		public long Latency
		{
			get
			{
				if (this.Stopwatch != null)
				{
					return this.Stopwatch.ElapsedMilliseconds;
				}
				return 0L;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0003AC10 File Offset: 0x00038E10
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0003AC18 File Offset: 0x00038E18
		private Stopwatch Stopwatch { get; set; }

		// Token: 0x060013D9 RID: 5081 RVA: 0x0003AC21 File Offset: 0x00038E21
		public void StartRecording()
		{
			this.Stopwatch = new Stopwatch();
			this.StartTime = ExDateTime.Now;
			this.Stopwatch.Start();
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x0003AC44 File Offset: 0x00038E44
		public void StopRecording()
		{
			if (this.Stopwatch != null)
			{
				this.Stopwatch.Stop();
			}
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x0003AC59 File Offset: 0x00038E59
		public override string ToString()
		{
			return string.Format("{0}={1}", this.Reason, this.Stopwatch.ElapsedMilliseconds);
		}
	}
}
