using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000048 RID: 72
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MruDictionaryElementRemovedEventArgs<TK, TV> : EventArgs
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x0000983F File Offset: 0x00007A3F
		public MruDictionaryElementRemovedEventArgs(KeyValuePair<TK, TV> keyValuePair)
		{
			this.KeyValuePair = keyValuePair;
		}

		// Token: 0x04000146 RID: 326
		public readonly KeyValuePair<TK, TV> KeyValuePair;
	}
}
