using System;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02000066 RID: 102
	public interface ISessionState
	{
		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600041F RID: 1055
		string CurrentPath { get; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000420 RID: 1056
		string CurrentPathProviderName { get; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000421 RID: 1057
		IVariableDictionary Variables { get; }
	}
}
