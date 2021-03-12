using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200077D RID: 1917
	internal class ExternalUserContactsFolderResponseShape : ExternalUserResponseShape
	{
		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06003941 RID: 14657 RVA: 0x000CAC95 File Offset: 0x000C8E95
		protected override List<PropertyPath> PropertiesAllowedForReadAccess
		{
			get
			{
				return ExternalUserContactsFolderResponseShape.properties;
			}
		}

		// Token: 0x04001FF0 RID: 8176
		private static List<PropertyPath> properties = new List<PropertyPath>
		{
			ItemSchema.ItemId.PropertyPath,
			ContactsFolderSchema.SharingEffectiveRights.PropertyPath
		};
	}
}
