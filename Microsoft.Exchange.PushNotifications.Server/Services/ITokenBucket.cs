using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.PushNotifications.Server.Services
{
	// Token: 0x02000021 RID: 33
	internal interface ITokenBucket
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000D5 RID: 213
		uint CurrentBalance { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000D6 RID: 214
		ExDateTime NextRecharge { get; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000D7 RID: 215
		bool IsFull { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000D8 RID: 216
		bool IsEmpty { get; }

		// Token: 0x060000D9 RID: 217
		bool TryTakeToken();
	}
}
