using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core.Directory
{
	// Token: 0x020002DD RID: 733
	public class GlobalAddressListInfo : AddressListInfo
	{
		// Token: 0x06001C42 RID: 7234 RVA: 0x000A24CC File Offset: 0x000A06CC
		internal GlobalAddressListInfo(AddressBookBase addressList, GlobalAddressListInfo.GalOrigin origin) : base(addressList)
		{
			this.origin = origin;
		}

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x000A24DC File Offset: 0x000A06DC
		public GlobalAddressListInfo.GalOrigin Origin
		{
			get
			{
				return this.origin;
			}
		}

		// Token: 0x06001C44 RID: 7236 RVA: 0x000A24E4 File Offset: 0x000A06E4
		public override AddressBookBase ToAddressBookBase()
		{
			if (this.origin == GlobalAddressListInfo.GalOrigin.EmptyGlobalAddressList)
			{
				return new AddressBookBase
				{
					OrganizationId = base.OrganizationId
				};
			}
			return base.ToAddressBookBase();
		}

		// Token: 0x040014E2 RID: 5346
		private GlobalAddressListInfo.GalOrigin origin;

		// Token: 0x020002DE RID: 734
		public enum GalOrigin
		{
			// Token: 0x040014E4 RID: 5348
			DefaultGlobalAddressList,
			// Token: 0x040014E5 RID: 5349
			QueryBaseDNAddressList,
			// Token: 0x040014E6 RID: 5350
			QueryBaseDNSubTree,
			// Token: 0x040014E7 RID: 5351
			EmptyGlobalAddressList
		}
	}
}
