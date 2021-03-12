using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000023 RID: 35
	internal interface IApnsFeedbackFile : IApnsFeedbackProvider, IEquatable<IApnsFeedbackFile>
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600015C RID: 348
		ApnsFeedbackFileId Identifier { get; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600015D RID: 349
		ApnsFeedbackFileIO FileIO { get; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600015E RID: 350
		ITracer Tracer { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600015F RID: 351
		bool IsLoaded { get; }

		// Token: 0x06000160 RID: 352
		void Load();

		// Token: 0x06000161 RID: 353
		void Remove();

		// Token: 0x06000162 RID: 354
		bool HasExpired(TimeSpan expirationThreshold);
	}
}
