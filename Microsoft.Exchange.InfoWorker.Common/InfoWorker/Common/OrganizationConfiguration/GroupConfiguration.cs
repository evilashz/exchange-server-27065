using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.InfoWorker.Common.OrganizationConfiguration
{
	// Token: 0x0200015E RID: 350
	internal class GroupConfiguration
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00028BB1 File Offset: 0x00026DB1
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00028BB9 File Offset: 0x00026DB9
		internal Guid Id { get; private set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00028BC2 File Offset: 0x00026DC2
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00028BCA File Offset: 0x00026DCA
		internal DateTime Version { get; private set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00028BD3 File Offset: 0x00026DD3
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x00028BDB File Offset: 0x00026DDB
		internal IEnumerable<Guid> GroupGuids { get; private set; }

		// Token: 0x060009AC RID: 2476 RVA: 0x00028BE4 File Offset: 0x00026DE4
		internal GroupConfiguration(Guid id, DateTime version, IEnumerable<Guid> groups)
		{
			this.Id = id;
			this.Version = version;
			this.GroupGuids = groups;
		}
	}
}
