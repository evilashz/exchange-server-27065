using System;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x0200005C RID: 92
	public interface ITargetLocation
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060006D6 RID: 1750
		string WorkingLocation { get; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060006D7 RID: 1751
		string ExportLocation { get; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060006D8 RID: 1752
		string UnsearchableExportLocation { get; }
	}
}
