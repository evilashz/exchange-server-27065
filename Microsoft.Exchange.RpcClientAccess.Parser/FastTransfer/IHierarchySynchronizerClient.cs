using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x0200018C RID: 396
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IHierarchySynchronizerClient : IDisposable
	{
		// Token: 0x060007CE RID: 1998
		IPropertyBag LoadFolderChanges();

		// Token: 0x060007CF RID: 1999
		IPropertyBag LoadFolderDeletion();

		// Token: 0x060007D0 RID: 2000
		IIcsState LoadFinalState();
	}
}
