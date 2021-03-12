using System;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x0200006D RID: 109
	internal sealed class ComponentInstance
	{
		// Token: 0x0600028E RID: 654 RVA: 0x00007232 File Offset: 0x00005432
		private ComponentInstance(Guid id, string name, string serviceName)
		{
			this.Id = id;
			this.NameX = name;
			this.ServiceName = serviceName;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000724F File Offset: 0x0000544F
		// (set) Token: 0x06000290 RID: 656 RVA: 0x00007257 File Offset: 0x00005457
		internal Guid Id { get; private set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000291 RID: 657 RVA: 0x00007260 File Offset: 0x00005460
		// (set) Token: 0x06000292 RID: 658 RVA: 0x00007268 File Offset: 0x00005468
		internal string NameX { get; private set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00007271 File Offset: 0x00005471
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00007279 File Offset: 0x00005479
		internal string ServiceName { get; private set; }

		// Token: 0x0200006E RID: 110
		internal static class Globals
		{
			// Token: 0x0400011C RID: 284
			internal static readonly ComponentInstance Search = new ComponentInstance(new Guid("decafbad-0001-40eb-9233-00219b403a32"), "Search", "MSExchangeFastSearch");
		}
	}
}
