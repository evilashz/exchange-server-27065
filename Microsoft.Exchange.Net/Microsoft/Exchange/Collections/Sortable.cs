using System;
using System.Diagnostics;

namespace Microsoft.Exchange.Collections
{
	// Token: 0x02000034 RID: 52
	[DebuggerDisplay("{Value}")]
	internal struct Sortable<T> : ISortKey<T> where T : IComparable<T>
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00006BA7 File Offset: 0x00004DA7
		public Sortable(T value)
		{
			this.Value = value;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006BB0 File Offset: 0x00004DB0
		public T SortKey
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x040000F6 RID: 246
		public T Value;
	}
}
