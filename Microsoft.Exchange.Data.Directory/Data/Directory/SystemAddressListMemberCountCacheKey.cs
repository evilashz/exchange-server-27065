using System;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000655 RID: 1621
	public class SystemAddressListMemberCountCacheKey : IEquatable<SystemAddressListMemberCountCacheKey>
	{
		// Token: 0x06004BFF RID: 19455 RVA: 0x001189A8 File Offset: 0x00116BA8
		public SystemAddressListMemberCountCacheKey(OrganizationId orgId, string systemAddressListName)
		{
			if (orgId == null)
			{
				throw new ArgumentNullException("orgId");
			}
			if (string.IsNullOrEmpty(systemAddressListName))
			{
				throw new ArgumentNullException("systemAddressListName");
			}
			if (orgId.ConfigurationUnit == null || string.IsNullOrEmpty(orgId.ConfigurationUnit.DistinguishedName))
			{
				this.configUnitDN = Guid.Empty.ToString();
			}
			else
			{
				this.configUnitDN = orgId.ConfigurationUnit.DistinguishedName.ToLower();
			}
			this.systemAddressListName = systemAddressListName.ToLower();
		}

		// Token: 0x06004C00 RID: 19456 RVA: 0x00118A39 File Offset: 0x00116C39
		public static bool Equals(SystemAddressListMemberCountCacheKey key1, SystemAddressListMemberCountCacheKey key2)
		{
			return string.Equals(key1.configUnitDN, key2.configUnitDN) && string.Equals(key1.systemAddressListName, key2.systemAddressListName);
		}

		// Token: 0x06004C01 RID: 19457 RVA: 0x00118A61 File Offset: 0x00116C61
		public bool Equals(SystemAddressListMemberCountCacheKey other)
		{
			return string.Equals(this.configUnitDN, other.configUnitDN) && string.Equals(this.systemAddressListName, other.systemAddressListName);
		}

		// Token: 0x06004C02 RID: 19458 RVA: 0x00118A8C File Offset: 0x00116C8C
		public override bool Equals(object o)
		{
			SystemAddressListMemberCountCacheKey systemAddressListMemberCountCacheKey = o as SystemAddressListMemberCountCacheKey;
			return systemAddressListMemberCountCacheKey != null && string.Equals(this.configUnitDN, systemAddressListMemberCountCacheKey.configUnitDN) && string.Equals(this.systemAddressListName, systemAddressListMemberCountCacheKey.systemAddressListName);
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x00118ACB File Offset: 0x00116CCB
		public override int GetHashCode()
		{
			return this.configUnitDN.GetHashCode() ^ this.systemAddressListName.GetHashCode();
		}

		// Token: 0x04003423 RID: 13347
		private string configUnitDN;

		// Token: 0x04003424 RID: 13348
		private string systemAddressListName;
	}
}
