using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000E3F RID: 3647
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface ICustomClonable
	{
		// Token: 0x06007E96 RID: 32406
		object CustomClone();
	}
}
