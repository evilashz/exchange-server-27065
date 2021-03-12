using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200029C RID: 668
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExExportManifest : IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C5F RID: 3167
		unsafe int Config(IStream iStream, SyncConfigFlags flags, IExchangeManifestCallback pCallback, SRestriction* lpRestriction, PropTag[] lpIncludeProps);

		// Token: 0x06000C60 RID: 3168
		int Synchronize(int ulFlags);

		// Token: 0x06000C61 RID: 3169
		int Checkpoint(IStream iStream, bool clearCnsets, long[] changeMids, long[] changeCns, long[] changeAssociatedCns, long[] deleteMids, long[] readCns);
	}
}
