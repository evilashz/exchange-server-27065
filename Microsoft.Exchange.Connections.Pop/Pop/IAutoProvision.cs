using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Pop
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IAutoProvision
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000016 RID: 22
		string[] Hostnames { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23
		int[] ConnectivePorts { get; }
	}
}
