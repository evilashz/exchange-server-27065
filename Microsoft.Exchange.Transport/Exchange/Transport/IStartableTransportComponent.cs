using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200000B RID: 11
	internal interface IStartableTransportComponent : ITransportComponent
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000038 RID: 56
		string CurrentState { get; }

		// Token: 0x06000039 RID: 57
		void Start(bool initiallyPaused, ServiceState intendedState);

		// Token: 0x0600003A RID: 58
		void Stop();

		// Token: 0x0600003B RID: 59
		void Pause();

		// Token: 0x0600003C RID: 60
		void Continue();
	}
}
