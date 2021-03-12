using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Management.Deployment.XforestTenantMigration
{
	// Token: 0x02000D84 RID: 3460
	internal sealed class Node
	{
		// Token: 0x17002952 RID: 10578
		// (get) Token: 0x060084EA RID: 34026 RVA: 0x0021F892 File Offset: 0x0021DA92
		// (set) Token: 0x060084EB RID: 34027 RVA: 0x0021F89A File Offset: 0x0021DA9A
		public string Name { get; set; }

		// Token: 0x17002953 RID: 10579
		// (get) Token: 0x060084EC RID: 34028 RVA: 0x0021F8A3 File Offset: 0x0021DAA3
		// (set) Token: 0x060084ED RID: 34029 RVA: 0x0021F8AB File Offset: 0x0021DAAB
		public DirectoryObject Value { get; set; }

		// Token: 0x17002954 RID: 10580
		// (get) Token: 0x060084EE RID: 34030 RVA: 0x0021F8B4 File Offset: 0x0021DAB4
		// (set) Token: 0x060084EF RID: 34031 RVA: 0x0021F8BC File Offset: 0x0021DABC
		public Dictionary<string, Node> Children { get; set; }

		// Token: 0x060084F0 RID: 34032 RVA: 0x0021F8C5 File Offset: 0x0021DAC5
		public Node(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentNullException(name);
			}
			this.Name = name;
			this.Children = new Dictionary<string, Node>();
			this.Value = null;
		}

		// Token: 0x060084F1 RID: 34033 RVA: 0x0021F8F5 File Offset: 0x0021DAF5
		public Node()
		{
		}
	}
}
