using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000143 RID: 323
	internal class FsmVariableCache
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00026E58 File Offset: 0x00025058
		internal static Dictionary<string, object> Instance
		{
			get
			{
				return FsmVariableCache.delegateCache;
			}
		}

		// Token: 0x040008C5 RID: 2245
		private static Dictionary<string, object> delegateCache = new Dictionary<string, object>();
	}
}
