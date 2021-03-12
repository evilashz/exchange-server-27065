using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000028 RID: 40
	internal interface ISystemCheckComponent : ITransportComponent
	{
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D3 RID: 211
		bool Enabled { get; }

		// Token: 0x060000D4 RID: 212
		void SetLoadTimeDependencies(SystemCheckConfig systemCheckConfig, TransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration);
	}
}
