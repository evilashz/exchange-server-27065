using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x0200018A RID: 394
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IFolderChange : IDisposable
	{
		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060007CA RID: 1994
		IPropertyBag FolderPropertyBag { get; }
	}
}
