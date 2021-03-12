using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006CE RID: 1742
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class PropertyErrorNotFoundException : PropertyErrorException
	{
		// Token: 0x06004596 RID: 17814 RVA: 0x00127D62 File Offset: 0x00125F62
		internal PropertyErrorNotFoundException(PropertyError propertyError) : base(propertyError)
		{
		}
	}
}
