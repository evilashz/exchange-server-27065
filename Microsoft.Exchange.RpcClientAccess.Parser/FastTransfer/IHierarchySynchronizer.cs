using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x0200018B RID: 395
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IHierarchySynchronizer : IDisposable
	{
		// Token: 0x060007CB RID: 1995
		IEnumerator<IFolderChange> GetChanges();

		// Token: 0x060007CC RID: 1996
		IPropertyBag GetDeletions();

		// Token: 0x060007CD RID: 1997
		IIcsState GetFinalState();
	}
}
