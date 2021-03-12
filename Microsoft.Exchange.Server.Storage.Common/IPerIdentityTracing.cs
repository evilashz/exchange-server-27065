using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000045 RID: 69
	public interface IPerIdentityTracing<T>
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000494 RID: 1172
		bool IsConfigured { get; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000495 RID: 1173
		bool IsTurnedOn { get; }

		// Token: 0x06000496 RID: 1174
		bool IsEnabled(T identity);

		// Token: 0x06000497 RID: 1175
		void TurnOn();

		// Token: 0x06000498 RID: 1176
		void TurnOff();
	}
}
