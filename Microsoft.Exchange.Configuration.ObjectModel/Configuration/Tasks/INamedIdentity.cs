using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000124 RID: 292
	public interface INamedIdentity
	{
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000A7F RID: 2687
		string Identity { get; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000A80 RID: 2688
		string DisplayName { get; }
	}
}
