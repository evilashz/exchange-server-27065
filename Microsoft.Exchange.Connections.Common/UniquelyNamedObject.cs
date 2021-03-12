using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000025 RID: 37
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UniquelyNamedObject : INamedObject
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00002998 File Offset: 0x00000B98
		public UniquelyNamedObject()
		{
			string text = Guid.NewGuid().ToString();
			this.Name = text;
			this.DetailedName = text;
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000029CD File Offset: 0x00000BCD
		// (set) Token: 0x06000084 RID: 132 RVA: 0x000029D5 File Offset: 0x00000BD5
		public string Name { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000029DE File Offset: 0x00000BDE
		// (set) Token: 0x06000086 RID: 134 RVA: 0x000029E6 File Offset: 0x00000BE6
		public string DetailedName { get; private set; }
	}
}
