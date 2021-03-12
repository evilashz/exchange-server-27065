using System;
using System.Collections.Generic;
using Microsoft.Exchange.LogAnalyzer.Analyzers.Perflog;
using Microsoft.ExLogAnalyzer;

namespace Microsoft.Exchange.Diagnostics.Service
{
	// Token: 0x02000015 RID: 21
	public class PrivateWorkingSetTrigger : PrivateWorkingSetTrigger
	{
		// Token: 0x0600007D RID: 125 RVA: 0x000068C0 File Offset: 0x00004AC0
		public PrivateWorkingSetTrigger(IJob job) : this(job, typeof(PrivateWorkingSetTrigger))
		{
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000068D3 File Offset: 0x00004AD3
		public PrivateWorkingSetTrigger(IJob job, Type type) : base(job, type, PrivateWorkingSetTrigger.excludedInstances)
		{
		}

		// Token: 0x04000058 RID: 88
		private static readonly HashSet<string> excludedInstances = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
		{
			"_Total",
			"_Global_",
			"Idle",
			"System",
			"lsass",
			"store"
		};

		// Token: 0x02000016 RID: 22
		public class Cafe : PrivateWorkingSetTrigger
		{
			// Token: 0x06000080 RID: 128 RVA: 0x0000694A File Offset: 0x00004B4A
			public Cafe(IJob job) : base(job, typeof(PrivateWorkingSetTrigger.Cafe))
			{
			}
		}

		// Token: 0x02000017 RID: 23
		public class Mailbox : PrivateWorkingSetTrigger
		{
			// Token: 0x06000081 RID: 129 RVA: 0x0000695D File Offset: 0x00004B5D
			public Mailbox(IJob job) : base(job, typeof(PrivateWorkingSetTrigger.Mailbox))
			{
			}
		}

		// Token: 0x02000018 RID: 24
		public class FfoWebService : PrivateWorkingSetTrigger
		{
			// Token: 0x06000082 RID: 130 RVA: 0x00006970 File Offset: 0x00004B70
			public FfoWebService(IJob job) : base(job, typeof(PrivateWorkingSetTrigger.FfoWebService))
			{
			}
		}
	}
}
