using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IServiceComponent
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000188 RID: 392
		string Name { get; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000189 RID: 393
		FacilityEnum Facility { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600018A RID: 394
		bool IsCritical { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600018B RID: 395
		bool IsEnabled { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600018C RID: 396
		bool IsRetriableOnError { get; }

		// Token: 0x0600018D RID: 397
		bool Start();

		// Token: 0x0600018E RID: 398
		void Stop();

		// Token: 0x0600018F RID: 399
		void Invoke(Action toInvoke);
	}
}
