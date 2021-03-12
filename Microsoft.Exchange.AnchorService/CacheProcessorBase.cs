using System;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x0200001B RID: 27
	internal abstract class CacheProcessorBase : IDiagnosable
	{
		// Token: 0x06000144 RID: 324 RVA: 0x000053B0 File Offset: 0x000035B0
		internal CacheProcessorBase(AnchorContext context, WaitHandle stopEvent)
		{
			this.Context = context;
			this.StopEvent = stopEvent;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000053C6 File Offset: 0x000035C6
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000053CE File Offset: 0x000035CE
		public AnchorContext Context { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000053D7 File Offset: 0x000035D7
		// (set) Token: 0x06000148 RID: 328 RVA: 0x000053DF File Offset: 0x000035DF
		public long Duration { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000053E8 File Offset: 0x000035E8
		// (set) Token: 0x0600014A RID: 330 RVA: 0x000053F0 File Offset: 0x000035F0
		public ExDateTime? LastRunTime { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000053F9 File Offset: 0x000035F9
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00005401 File Offset: 0x00003601
		public ExDateTime? LastWorkTime { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600014D RID: 333 RVA: 0x0000540A File Offset: 0x0000360A
		// (set) Token: 0x0600014E RID: 334 RVA: 0x00005412 File Offset: 0x00003612
		public ExDateTime? StartWorkTime { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000541B File Offset: 0x0000361B
		public virtual TimeSpan ActiveRunDelay
		{
			get
			{
				return this.Context.Config.GetConfig<TimeSpan>("ActiveRunDelay");
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000150 RID: 336
		internal abstract string Name { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00005432 File Offset: 0x00003632
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000543A File Offset: 0x0000363A
		private protected WaitHandle StopEvent { protected get; private set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00005443 File Offset: 0x00003643
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000544B File Offset: 0x0000364B
		private protected int SequenceNumber { protected get; private set; }

		// Token: 0x06000155 RID: 341 RVA: 0x00005454 File Offset: 0x00003654
		public virtual string GetDiagnosticComponentName()
		{
			return this.Name;
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000545C File Offset: 0x0000365C
		public virtual XElement GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			return new XElement(this.Name, new object[]
			{
				new XElement("duration", this.Duration),
				new XElement("lastRunTime", (this.LastRunTime != null) ? this.LastRunTime.Value.UniversalTime.ToString() : string.Empty),
				new XElement("lastWorkTime", (this.LastWorkTime != null) ? this.LastWorkTime.Value.UniversalTime.ToString() : string.Empty),
				new XElement("startWorkTime", (this.StartWorkTime != null) ? this.StartWorkTime.Value.UniversalTime.ToString() : string.Empty),
				new XElement("sequenceNumber", this.SequenceNumber)
			});
		}

		// Token: 0x06000157 RID: 343
		internal abstract bool ShouldProcess();

		// Token: 0x06000158 RID: 344
		internal abstract bool Process(JobCache data);

		// Token: 0x06000159 RID: 345 RVA: 0x000055AB File Offset: 0x000037AB
		internal void IncrementSequenceNumber()
		{
			this.SequenceNumber = (this.SequenceNumber + 1) % 100000;
		}
	}
}
