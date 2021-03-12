using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000047 RID: 71
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class MruDictionaryElementReplacedEventArgs<TK, TV> : EventArgs
	{
		// Token: 0x060001E6 RID: 486 RVA: 0x00009829 File Offset: 0x00007A29
		public MruDictionaryElementReplacedEventArgs(KeyValuePair<TK, TV> oldKeyValuePair, KeyValuePair<TK, TV> newKeyValuePair)
		{
			this.OldKeyValuePair = oldKeyValuePair;
			this.NewKeyValuePair = newKeyValuePair;
		}

		// Token: 0x04000144 RID: 324
		public readonly KeyValuePair<TK, TV> OldKeyValuePair;

		// Token: 0x04000145 RID: 325
		public readonly KeyValuePair<TK, TV> NewKeyValuePair;
	}
}
