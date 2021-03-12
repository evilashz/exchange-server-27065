using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Entities.People.Utilities
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class DiffResult<T, K>
	{
		// Token: 0x0600002E RID: 46 RVA: 0x00002D44 File Offset: 0x00000F44
		public DiffResult()
		{
			this.RemoveList = new List<T>();
			this.AddList = new List<T>();
			this.UpdateList = new Dictionary<T, ICollection<Tuple<K, object>>>();
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002D6D File Offset: 0x00000F6D
		// (set) Token: 0x06000030 RID: 48 RVA: 0x00002D75 File Offset: 0x00000F75
		public ICollection<T> RemoveList { get; private set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002D7E File Offset: 0x00000F7E
		// (set) Token: 0x06000032 RID: 50 RVA: 0x00002D86 File Offset: 0x00000F86
		public ICollection<T> AddList { get; private set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002D8F File Offset: 0x00000F8F
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002D97 File Offset: 0x00000F97
		public Dictionary<T, ICollection<Tuple<K, object>>> UpdateList { get; private set; }
	}
}
