using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.MessageDepot;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000020 RID: 32
	internal interface IMessageDepotComponent : ITransportComponent, IDiagnosable
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000B5 RID: 181
		IMessageDepot MessageDepot { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B6 RID: 182
		bool Enabled { get; }

		// Token: 0x060000B7 RID: 183
		void SetLoadTimeDependencies(MessageDepotConfig messageDepotConfig);
	}
}
