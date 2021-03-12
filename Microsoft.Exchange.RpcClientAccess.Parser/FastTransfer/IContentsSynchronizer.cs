using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer
{
	// Token: 0x02000182 RID: 386
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IContentsSynchronizer : IDisposable
	{
		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000793 RID: 1939
		ProgressInformation ProgressInformation { get; }

		// Token: 0x06000794 RID: 1940
		IEnumerator<IMessageChange> GetChanges();

		// Token: 0x06000795 RID: 1941
		IPropertyBag GetDeletions();

		// Token: 0x06000796 RID: 1942
		IPropertyBag GetReadUnreadStateChanges();

		// Token: 0x06000797 RID: 1943
		IIcsState GetFinalState();
	}
}
