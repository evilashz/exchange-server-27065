using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200033C RID: 828
	internal sealed class AddressBookMailboxPolicySchema : MailboxPolicySchema
	{
		// Token: 0x040017A0 RID: 6048
		public static readonly ADPropertyDefinition AssociatedUsers = new ADPropertyDefinition("AssociatedUsers", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchAddressBookPolicyBL", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.FilterOnly | ADPropertyDefinitionFlags.BackLink, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017A1 RID: 6049
		public static readonly ADPropertyDefinition AddressLists = new ADPropertyDefinition("AddressLists", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchAddressListsLink", ADPropertyDefinitionFlags.MultiValued | ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017A2 RID: 6050
		public static readonly ADPropertyDefinition GlobalAddressList = new ADPropertyDefinition("GlobalAddressList", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchGlobalAddressListLink", ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017A3 RID: 6051
		public static readonly ADPropertyDefinition RoomList = new ADPropertyDefinition("RoomList", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchAllRoomListLink", ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);

		// Token: 0x040017A4 RID: 6052
		public static readonly ADPropertyDefinition OfflineAddressBook = new ADPropertyDefinition("OfflineAddressBook", ExchangeObjectVersion.Exchange2010, typeof(ADObjectId), "msExchOfflineAddressBookLink", ADPropertyDefinitionFlags.Mandatory, null, PropertyDefinitionConstraint.None, PropertyDefinitionConstraint.None, null, null);
	}
}
