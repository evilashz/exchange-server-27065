using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000042 RID: 66
	public interface IPerformanceCounterCategory
	{
		// Token: 0x0600017F RID: 383
		bool InstanceExists(string instanceName);

		// Token: 0x06000180 RID: 384
		string[] GetInstanceNames();
	}
}
