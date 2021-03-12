using System;
using Microsoft.Exchange.Data.Directory.ResourceHealth;

namespace Microsoft.Exchange.WorkloadManagement
{
	// Token: 0x02000025 RID: 37
	internal sealed class ProcessorResourceKey : ResourceKey
	{
		// Token: 0x06000131 RID: 305 RVA: 0x00005F06 File Offset: 0x00004106
		private ProcessorResourceKey() : base(ResourceMetricType.Processor, null)
		{
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00005F10 File Offset: 0x00004110
		public static ProcessorResourceKey Local
		{
			get
			{
				return ProcessorResourceKey.local;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00005F17 File Offset: 0x00004117
		protected internal override CacheableResourceHealthMonitor CreateMonitor()
		{
			return new ProcessorResourceLoadMonitor(this);
		}

		// Token: 0x040000A8 RID: 168
		private static ProcessorResourceKey local = new ProcessorResourceKey();
	}
}
