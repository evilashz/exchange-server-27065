using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000168 RID: 360
	internal interface ITempFile : IDisposeTrackable, IDisposable
	{
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000B6B RID: 2923
		string FilePath { get; }

		// Token: 0x06000B6C RID: 2924
		void KeepAlive();
	}
}
