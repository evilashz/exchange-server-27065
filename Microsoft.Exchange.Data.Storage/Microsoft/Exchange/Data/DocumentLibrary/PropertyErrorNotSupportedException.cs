using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006CF RID: 1743
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class PropertyErrorNotSupportedException : PropertyErrorException
	{
		// Token: 0x06004597 RID: 17815 RVA: 0x00127D6B File Offset: 0x00125F6B
		internal PropertyErrorNotSupportedException(PropertyError propertyError) : base(propertyError)
		{
		}
	}
}
