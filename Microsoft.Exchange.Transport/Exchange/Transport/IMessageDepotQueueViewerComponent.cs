using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.MessageDepot;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000021 RID: 33
	internal interface IMessageDepotQueueViewerComponent : ITransportComponent, IDiagnosable
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B8 RID: 184
		IMessageDepotQueueViewer MessageDepotQueueViewer { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000B9 RID: 185
		bool Enabled { get; }

		// Token: 0x060000BA RID: 186
		void SetLoadTimeDependencies(IMessageDepotComponent messageDepotComponent, TransportAppConfig.ILegacyQueueConfig queueConfig);
	}
}
