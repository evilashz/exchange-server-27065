using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000299 RID: 665
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExExportChanges : IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C52 RID: 3154
		unsafe int Config(IStream iStream, int ulFlags, IntPtr iCollector, SRestriction* lpRestriction, PropTag[] lpIncludeProps, PropTag[] lpExcludeProps, int ulBufferSize);

		// Token: 0x06000C53 RID: 3155
		int Synchronize(out int lpulSteps, out int lpulProgress);

		// Token: 0x06000C54 RID: 3156
		int UpdateState(IStream iStream);
	}
}
