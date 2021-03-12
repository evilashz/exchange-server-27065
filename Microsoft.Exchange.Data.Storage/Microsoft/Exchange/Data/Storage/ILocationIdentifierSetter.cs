using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200008D RID: 141
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ILocationIdentifierSetter
	{
		// Token: 0x060009B6 RID: 2486
		void SetLocationIdentifier(uint id);

		// Token: 0x060009B7 RID: 2487
		void SetLocationIdentifier(uint id, LastChangeAction action);
	}
}
