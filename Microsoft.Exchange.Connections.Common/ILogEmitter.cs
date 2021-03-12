using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x02000008 RID: 8
	[ClassAccessLevel(AccessLevel.Implementation)]
	public interface ILogEmitter
	{
		// Token: 0x0600003B RID: 59
		void Emit(string formatString, params object[] args);
	}
}
