using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000019 RID: 25
	public class ProcessProcessorTimeTrigger : ProcessProcessorTimeTrigger
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00006983 File Offset: 0x00004B83
		public ProcessProcessorTimeTrigger(IJob job) : this(job, typeof(ProcessProcessorTimeTrigger))
		{
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00006996 File Offset: 0x00004B96
		public ProcessProcessorTimeTrigger(IJob job, Type type) : base(job, type, ProcessProcessorTimeTrigger.excludedInstances)
		{
		}

		// Token: 0x04000059 RID: 89
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"_Total",
			"_Global_",
			"Idle",
			"System",
			"lsass",
			"store",
			"msexchangerepl"
		};

		// Token: 0x0200001A RID: 26
		public class Cafe : ProcessProcessorTimeTrigger
		{
			// Token: 0x06000086 RID: 134 RVA: 0x00006A1A File Offset: 0x00004C1A
			public Cafe(IJob job) : base(job, typeof(ProcessProcessorTimeTrigger.Cafe))
			{
			}
		}

		// Token: 0x0200001B RID: 27
		public class Mailbox : ProcessProcessorTimeTrigger
		{
			// Token: 0x06000087 RID: 135 RVA: 0x00006A2D File Offset: 0x00004C2D
			public Mailbox(IJob job) : base(job, typeof(ProcessProcessorTimeTrigger.Mailbox))
			{
			}
		}

		// Token: 0x0200001C RID: 28
		public class FfoWebService : ProcessProcessorTimeTrigger
		{
			// Token: 0x06000088 RID: 136 RVA: 0x00006A40 File Offset: 0x00004C40
			public FfoWebService(IJob job) : base(job, typeof(ProcessProcessorTimeTrigger.FfoWebService))
			{
			}
		}
	}
}
