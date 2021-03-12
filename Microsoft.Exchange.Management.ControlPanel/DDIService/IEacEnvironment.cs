using System;

namespace Microsoft.Exchange.Management.DDIService
{
	// Token: 0x02000138 RID: 312
	public interface IEacEnvironment
	{
		// Token: 0x17001A45 RID: 6725
		// (get) Token: 0x060020F1 RID: 8433
		bool IsForefrontForOffice { get; }

		// Token: 0x17001A46 RID: 6726
		// (get) Token: 0x060020F2 RID: 8434
		bool IsDataCenter { get; }
	}
}
