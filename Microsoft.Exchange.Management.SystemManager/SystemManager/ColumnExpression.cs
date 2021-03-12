using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.SystemManager
{
	// Token: 0x0200009F RID: 159
	public sealed class ColumnExpression
	{
		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x00013CC2 File Offset: 0x00011EC2
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x00013CCA File Offset: 0x00011ECA
		public string ResultColumn { get; set; }

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x00013CD3 File Offset: 0x00011ED3
		public List<string> DependentColumns
		{
			get
			{
				return this.dependentColumns;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00013CDB File Offset: 0x00011EDB
		// (set) Token: 0x06000525 RID: 1317 RVA: 0x00013CE3 File Offset: 0x00011EE3
		public string Expression { get; set; }

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00013CEC File Offset: 0x00011EEC
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x00013CF4 File Offset: 0x00011EF4
		internal CachedDelegate CachedDelegate { get; set; }

		// Token: 0x040001B4 RID: 436
		private List<string> dependentColumns = new List<string>();
	}
}
