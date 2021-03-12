using System;

namespace Microsoft.Exchange.Data.TextConverters
{
	// Token: 0x02000172 RID: 370
	internal interface IProducerConsumer
	{
		// Token: 0x06000FF9 RID: 4089
		void Run();

		// Token: 0x06000FFA RID: 4090
		bool Flush();
	}
}
