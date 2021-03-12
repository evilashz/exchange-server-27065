using System;
using Microsoft.Exchange.Entities.DataModel;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000E73 RID: 3699
	internal static class LocationDataEntityConverter
	{
		// Token: 0x06006049 RID: 24649 RVA: 0x0012CB08 File Offset: 0x0012AD08
		internal static Location ToLocation(this Location dataEntityLocation)
		{
			if (dataEntityLocation == null)
			{
				return null;
			}
			return new Location
			{
				DisplayName = dataEntityLocation.DisplayName
			};
		}

		// Token: 0x0600604A RID: 24650 RVA: 0x0012CB30 File Offset: 0x0012AD30
		internal static Location ToDataEntityLocation(this Location location)
		{
			if (location == null)
			{
				return null;
			}
			return new Location
			{
				DisplayName = location.DisplayName
			};
		}
	}
}
