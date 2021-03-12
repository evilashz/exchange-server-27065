using System;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000007 RID: 7
	internal interface IStartStop
	{
		// Token: 0x06000036 RID: 54
		void Start();

		// Token: 0x06000037 RID: 55
		void PrepareToStop();

		// Token: 0x06000038 RID: 56
		void Stop();
	}
}
