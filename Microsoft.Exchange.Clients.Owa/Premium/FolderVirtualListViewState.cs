using System;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium
{
	// Token: 0x020004B8 RID: 1208
	[OwaEventStruct("FVLVS")]
	[OwaEventObjectId(typeof(OwaStoreObjectId))]
	public class FolderVirtualListViewState : VirtualListViewState
	{
		// Token: 0x04001F8C RID: 8076
		public const string StructNamespace = "FVLVS";
	}
}
