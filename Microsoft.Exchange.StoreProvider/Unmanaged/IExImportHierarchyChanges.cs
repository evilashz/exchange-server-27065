using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x0200029B RID: 667
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExImportHierarchyChanges : IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C5B RID: 3163
		int Config(IStream iStream, int ulFlags);

		// Token: 0x06000C5C RID: 3164
		int UpdateState(IStream iStream);

		// Token: 0x06000C5D RID: 3165
		unsafe int ImportFolderChange(int cpvalChanges, SPropValue* ppvalChanges);

		// Token: 0x06000C5E RID: 3166
		unsafe int ImportFolderDeletion(int ulFlags, _SBinaryArray* lpSrcEntryList);
	}
}
