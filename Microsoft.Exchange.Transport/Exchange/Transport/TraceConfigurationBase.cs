using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000077 RID: 119
	internal abstract class TraceConfigurationBase
	{
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000372 RID: 882 RVA: 0x0000F8D3 File Offset: 0x0000DAD3
		public bool IsUpdateNeeded
		{
			get
			{
				return this.exTraceConfigVersion != ExTraceConfiguration.Instance.Version;
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000F8EA File Offset: 0x0000DAEA
		public bool FilteredTracingEnabled
		{
			get
			{
				return this.exTraceConfiguration.PerThreadTracingConfigured;
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0000F8F7 File Offset: 0x0000DAF7
		public void Load(ExTraceConfiguration exTraceConfiguration)
		{
			this.exTraceConfiguration = exTraceConfiguration;
			this.exTraceConfigVersion = this.exTraceConfiguration.Version;
			this.OnLoad();
		}

		// Token: 0x06000375 RID: 885
		public abstract void OnLoad();

		// Token: 0x040001EC RID: 492
		protected ExTraceConfiguration exTraceConfiguration;

		// Token: 0x040001ED RID: 493
		protected int exTraceConfigVersion;
	}
}
