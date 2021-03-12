using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x0200001B RID: 27
	public interface IDisposeTrackable : IDisposable
	{
		// Token: 0x06000065 RID: 101
		DisposeTracker GetDisposeTracker();

		// Token: 0x06000066 RID: 102
		void SuppressDisposeTracker();
	}
}
