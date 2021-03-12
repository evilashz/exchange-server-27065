using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020006FA RID: 1786
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal interface IEventHandler
	{
		// Token: 0x060046D4 RID: 18132
		void Consume(Event newEvent);

		// Token: 0x060046D5 RID: 18133
		void HandleException(Exception exception);
	}
}
