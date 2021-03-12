using System;
using System.Collections.Generic;
using Microsoft.Exchange.Services.Core.DataConverter;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200077A RID: 1914
	internal class ExternalUserCalendarFolderResponseShape : ExternalUserResponseShape
	{
		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06003935 RID: 14645 RVA: 0x000CA54A File Offset: 0x000C874A
		protected override List<PropertyPath> PropertiesAllowedForReadAccess
		{
			get
			{
				return ExternalUserCalendarFolderResponseShape.properties;
			}
		}

		// Token: 0x04001FE8 RID: 8168
		private static List<PropertyPath> properties = new List<PropertyPath>
		{
			ItemSchema.ItemId.PropertyPath,
			CalendarFolderSchema.SharingEffectiveRights.PropertyPath
		};
	}
}
