using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000027 RID: 39
	[ClassAccessLevel(AccessLevel.Implementation)]
	public interface IConnection<TConnectionClass>
	{
		// Token: 0x0600008C RID: 140
		TConnectionClass Initialize();
	}
}
