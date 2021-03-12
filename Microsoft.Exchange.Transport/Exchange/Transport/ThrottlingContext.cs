using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x020001A4 RID: 420
	internal class ThrottlingContext
	{
		// Token: 0x06001204 RID: 4612 RVA: 0x0004996D File Offset: 0x00047B6D
		public ThrottlingContext(DateTime startTime)
		{
			this.startTime = startTime;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0004998C File Offset: 0x00047B8C
		public ThrottlingContext(DateTime startTime, Cost cost)
		{
			this.startTime = startTime;
			this.cost = cost;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x000499B2 File Offset: 0x00047BB2
		public ThrottlingContext(Cost cost)
		{
			this.cost = cost;
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06001207 RID: 4615 RVA: 0x000499D1 File Offset: 0x00047BD1
		public DateTime CreationTime
		{
			get
			{
				return this.startTime;
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x000499D9 File Offset: 0x00047BD9
		public Cost Cost
		{
			get
			{
				return this.cost;
			}
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x000499E1 File Offset: 0x00047BE1
		public void AddMemoryCost(ByteQuantifiedSize bytesUsed)
		{
			if (this.cost != null)
			{
				this.cost.AddMemoryCost(bytesUsed);
			}
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x000499FE File Offset: 0x00047BFE
		public void AddBreadcrumb(CategorizerBreadcrumb breadcrumb)
		{
			this.breadcrumbs.Drop(breadcrumb);
		}

		// Token: 0x040009C7 RID: 2503
		private Breadcrumbs<CategorizerBreadcrumb> breadcrumbs = new Breadcrumbs<CategorizerBreadcrumb>(256);

		// Token: 0x040009C8 RID: 2504
		private readonly DateTime startTime;

		// Token: 0x040009C9 RID: 2505
		private readonly Cost cost;
	}
}
