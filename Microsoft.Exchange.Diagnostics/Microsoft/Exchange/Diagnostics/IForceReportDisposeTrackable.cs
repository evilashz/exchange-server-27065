using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200001C RID: 28
	public interface IForceReportDisposeTrackable : IDisposeTrackable, IDisposable
	{
		// Token: 0x06000067 RID: 103
		void ForceLeakReport();
	}
}
