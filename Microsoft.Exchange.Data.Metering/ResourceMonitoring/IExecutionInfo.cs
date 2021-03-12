using System;

namespace Microsoft.Exchange.Data.Metering.ResourceMonitoring
{
	// Token: 0x02000029 RID: 41
	internal interface IExecutionInfo
	{
		// Token: 0x06000198 RID: 408
		void OnStart();

		// Token: 0x06000199 RID: 409
		void OnException(Exception ex);

		// Token: 0x0600019A RID: 410
		void OnFinish();
	}
}
