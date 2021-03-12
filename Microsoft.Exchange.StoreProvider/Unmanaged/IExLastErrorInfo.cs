using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200028D RID: 653
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExLastErrorInfo : IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000BB5 RID: 2997
		int GetLastError(int hResult, out int lpMapiError);

		// Token: 0x06000BB6 RID: 2998
		int GetExtendedErrorInfo(out DiagnosticContext pExtendedErrorInfo);
	}
}
