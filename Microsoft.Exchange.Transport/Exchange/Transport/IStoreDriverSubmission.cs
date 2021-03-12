using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000027 RID: 39
	internal interface IStoreDriverSubmission : IStartableTransportComponent, ITransportComponent
	{
		// Token: 0x060000D1 RID: 209
		void ExpireOldSubmissionConnections();

		// Token: 0x060000D2 RID: 210
		void Retire();
	}
}
