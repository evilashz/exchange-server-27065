using System;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.MAPI.ExtensionMethods
{
	// Token: 0x02000007 RID: 7
	public static class MapiExtensionMethods
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002D92 File Offset: 0x00000F92
		public static byte[] UserSearchKey(this AddressInfo addressInfo)
		{
			return AddressBookEID.MakeSearchKey("EX", addressInfo.LegacyExchangeDN);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public static byte[] UserEntryId(this AddressInfo addressInfo)
		{
			return AddressBookEID.MakeAddressBookEntryID(addressInfo.LegacyExchangeDN, addressInfo.IsDistributionList);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002DB7 File Offset: 0x00000FB7
		public static int UserFlags(this AddressInfo addressInfo)
		{
			return 0;
		}
	}
}
