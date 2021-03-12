using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.DocumentLibrary
{
	// Token: 0x020006CD RID: 1741
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[Serializable]
	internal class PropertyErrorCorruptDataException : PropertyErrorException
	{
		// Token: 0x06004595 RID: 17813 RVA: 0x00127D59 File Offset: 0x00125F59
		internal PropertyErrorCorruptDataException(PropertyError propertyError) : base(propertyError)
		{
		}
	}
}
