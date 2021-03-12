using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200028C RID: 652
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExInterface : IDisposeTrackable, IDisposable
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000BB3 RID: 2995
		bool IsInvalid { get; }

		// Token: 0x06000BB4 RID: 2996
		int QueryInterface(Guid riid, out IExInterface iObj);
	}
}
