using System;
using System.Data.Linq.Mapping;

namespace Microsoft.Office.Datacenter.WorkerTaskFramework
{
	// Token: 0x0200000A RID: 10
	public abstract class EndpointInfo : TableEntity
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000087 RID: 135
		// (set) Token: 0x06000088 RID: 136
		[Column]
		public abstract string Scope { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000089 RID: 137
		// (set) Token: 0x0600008A RID: 138
		[Column]
		public abstract bool IsLive { get; set; }
	}
}
