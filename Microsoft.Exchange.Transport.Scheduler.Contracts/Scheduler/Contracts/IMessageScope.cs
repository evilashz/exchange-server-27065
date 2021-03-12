using System;

namespace Microsoft.Exchange.Transport.Scheduler.Contracts
{
	// Token: 0x02000003 RID: 3
	internal interface IMessageScope : IEquatable<IMessageScope>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1
		string Display { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2
		MessageScopeType Type { get; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000003 RID: 3
		object Value { get; }
	}
}
