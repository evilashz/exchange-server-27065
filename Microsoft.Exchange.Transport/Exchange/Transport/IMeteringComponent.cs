using System;
using Microsoft.Exchange.Data.Metering;
using Microsoft.Exchange.Data.Metering.Throttling;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200002D RID: 45
	internal interface IMeteringComponent : ITransportComponent
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000EE RID: 238
		ICountTracker<MeteredEntity, MeteredCount> Metering { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000EF RID: 239
		ICountTrackerDiagnostics<MeteredEntity, MeteredCount> MeteringDiagnostics { get; }

		// Token: 0x060000F0 RID: 240
		void SetLoadtimeDependencies(ICountTrackerConfig config, Trace tracer);
	}
}
