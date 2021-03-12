using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000024 RID: 36
	[ClassAccessLevel(AccessLevel.Implementation)]
	public interface INamedObject
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000080 RID: 128
		string Name { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000081 RID: 129
		string DetailedName { get; }
	}
}
