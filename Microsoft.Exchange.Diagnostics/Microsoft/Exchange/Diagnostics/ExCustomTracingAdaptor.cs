using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000086 RID: 134
	internal abstract class ExCustomTracingAdaptor<T>
	{
		// Token: 0x06000317 RID: 791 RVA: 0x0000B258 File Offset: 0x00009458
		public bool IsTracingEnabled(T identity)
		{
			ExTraceConfiguration instance = ExTraceConfiguration.Instance;
			if (!instance.PerThreadTracingConfigured)
			{
				return false;
			}
			if (this.traceEnabledFields == null || this.tracingConfigVersion < instance.Version)
			{
				lock (this)
				{
					if (this.traceEnabledFields == null || this.tracingConfigVersion < instance.Version)
					{
						this.traceEnabledFields = this.LoadEnabledIdentities(instance);
						this.tracingConfigVersion = instance.Version;
					}
				}
			}
			return this.traceEnabledFields.Contains(identity);
		}

		// Token: 0x06000318 RID: 792
		protected abstract HashSet<T> LoadEnabledIdentities(ExTraceConfiguration currentInstance);

		// Token: 0x040002D9 RID: 729
		private int tracingConfigVersion;

		// Token: 0x040002DA RID: 730
		private HashSet<T> traceEnabledFields;
	}
}
