using System;

namespace Microsoft.Exchange.ServiceHost.Common
{
	// Token: 0x02000006 RID: 6
	internal interface IServiceTask
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000021 RID: 33
		string Name { get; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000022 RID: 34
		bool IsRecurring { get; }

		// Token: 0x06000023 RID: 35
		void Run();
	}
}
