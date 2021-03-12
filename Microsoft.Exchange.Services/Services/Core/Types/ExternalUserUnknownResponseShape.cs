using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000782 RID: 1922
	internal class ExternalUserUnknownResponseShape : ExternalUserResponseShape
	{
		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06003962 RID: 14690 RVA: 0x000CB373 File Offset: 0x000C9573
		protected override List<PropertyPath> PropertiesAllowedForReadAccess
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x000CB376 File Offset: 0x000C9576
		protected override PropertyPath[] GetPropertiesForCustomPermissions(ItemResponseShape requestedResponseShape)
		{
			return null;
		}
	}
}
