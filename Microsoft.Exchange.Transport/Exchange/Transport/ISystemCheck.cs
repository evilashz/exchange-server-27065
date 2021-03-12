using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000017 RID: 23
	internal interface ISystemCheck
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000082 RID: 130
		bool Enabled { get; }

		// Token: 0x06000083 RID: 131
		void Check();
	}
}
