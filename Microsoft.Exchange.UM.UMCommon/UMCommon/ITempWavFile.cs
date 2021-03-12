using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000169 RID: 361
	internal interface ITempWavFile : ITempFile, IDisposeTrackable, IDisposable
	{
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000B6D RID: 2925
		// (set) Token: 0x06000B6E RID: 2926
		string ExtraInfo { get; set; }
	}
}
