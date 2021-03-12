using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Fips
{
	// Token: 0x02000509 RID: 1289
	internal interface IFileDeletionPolicy
	{
		// Token: 0x06001FD1 RID: 8145
		bool ShouldDelete(string filePath);
	}
}
