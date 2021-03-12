using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002A7 RID: 679
	[Serializable]
	public abstract class ADNonExchangeObject : ADConfigurationObject
	{
		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001F87 RID: 8071 RVA: 0x0008C0C4 File Offset: 0x0008A2C4
		internal override QueryFilter VersioningFilter
		{
			get
			{
				return null;
			}
		}
	}
}
