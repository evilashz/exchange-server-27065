using System;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200053F RID: 1343
	public interface IEndpoint
	{
		// Token: 0x060020FC RID: 8444
		void Initialize();

		// Token: 0x060020FD RID: 8445
		bool DetectChange();

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x060020FE RID: 8446
		bool RestartOnChange { get; }

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x060020FF RID: 8447
		// (set) Token: 0x06002100 RID: 8448
		Exception Exception { get; set; }
	}
}
