using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Mapi
{
	// Token: 0x0200004F RID: 79
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FxProxyAttachWrapper : FxProxyWrapper, IAttach, IMAPIProp
	{
		// Token: 0x060001F1 RID: 497 RVA: 0x0000ABCC File Offset: 0x00008DCC
		internal FxProxyAttachWrapper(IMapiFxCollector iFxCollector) : base(iFxCollector)
		{
		}
	}
}
