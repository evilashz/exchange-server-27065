using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000F35 RID: 3893
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class LogTableSchema<T>
	{
		// Token: 0x060085D9 RID: 34265 RVA: 0x0024AF9C File Offset: 0x0024919C
		public LogTableSchema(string columnName, Func<T, string> getter)
		{
			this.ColumnName = columnName;
			this.Getter = getter;
		}

		// Token: 0x17002376 RID: 9078
		// (get) Token: 0x060085DA RID: 34266 RVA: 0x0024AFB2 File Offset: 0x002491B2
		// (set) Token: 0x060085DB RID: 34267 RVA: 0x0024AFBA File Offset: 0x002491BA
		public string ColumnName { get; private set; }

		// Token: 0x17002377 RID: 9079
		// (get) Token: 0x060085DC RID: 34268 RVA: 0x0024AFC3 File Offset: 0x002491C3
		// (set) Token: 0x060085DD RID: 34269 RVA: 0x0024AFCB File Offset: 0x002491CB
		public Func<T, string> Getter { get; private set; }
	}
}
