using System;
using System.Diagnostics;

namespace System.Collections
{
	// Token: 0x02000476 RID: 1142
	[DebuggerDisplay("{value}", Name = "[{key}]", Type = "")]
	internal class KeyValuePairs
	{
		// Token: 0x060037AD RID: 14253 RVA: 0x000D5265 File Offset: 0x000D3465
		public KeyValuePairs(object key, object value)
		{
			this.value = value;
			this.key = key;
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x000D527B File Offset: 0x000D347B
		public object Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x060037AF RID: 14255 RVA: 0x000D5283 File Offset: 0x000D3483
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04001847 RID: 6215
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private object key;

		// Token: 0x04001848 RID: 6216
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private object value;
	}
}
