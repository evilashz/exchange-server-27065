using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000173 RID: 371
	internal interface IRestartable
	{
		// Token: 0x06000FFB RID: 4091
		bool CanRestart();

		// Token: 0x06000FFC RID: 4092
		void Restart();

		// Token: 0x06000FFD RID: 4093
		void DisableRestart();
	}
}
