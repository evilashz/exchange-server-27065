using System;
using System.Collections.Generic;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x02000071 RID: 113
	public class ModifiedSearchFolders
	{
		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x0004AEE6 File Offset: 0x000490E6
		public ISet<ExchangeId> InsertedInto
		{
			get
			{
				return this.insertedInto;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x0004AEEE File Offset: 0x000490EE
		public ISet<ExchangeId> DeletedFrom
		{
			get
			{
				return this.deletedFrom;
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000878 RID: 2168 RVA: 0x0004AEF6 File Offset: 0x000490F6
		public ISet<ExchangeId> Updated
		{
			get
			{
				return this.updated;
			}
		}

		// Token: 0x04000426 RID: 1062
		private readonly ISet<ExchangeId> insertedInto = new HashSet<ExchangeId>();

		// Token: 0x04000427 RID: 1063
		private readonly ISet<ExchangeId> deletedFrom = new HashSet<ExchangeId>();

		// Token: 0x04000428 RID: 1064
		private readonly ISet<ExchangeId> updated = new HashSet<ExchangeId>();
	}
}
