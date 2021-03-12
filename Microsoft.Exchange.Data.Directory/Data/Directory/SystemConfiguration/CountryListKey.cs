using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000658 RID: 1624
	internal class CountryListKey
	{
		// Token: 0x06004C0E RID: 19470 RVA: 0x00118FA4 File Offset: 0x001171A4
		public CountryListKey(string countryListName)
		{
			if (string.IsNullOrEmpty(countryListName))
			{
				throw new ArgumentNullException("countryListName");
			}
			ADObjectId rootOrgContainerIdForLocalForest = ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest();
			this.Key = rootOrgContainerIdForLocalForest.GetDescendantId(CountryList.RdnContainer.GetChildId(countryListName.ToLower()));
			this.cachedHashCode = this.Key.DistinguishedName.ToLower().GetHashCode();
		}

		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x06004C0F RID: 19471 RVA: 0x00119007 File Offset: 0x00117207
		// (set) Token: 0x06004C10 RID: 19472 RVA: 0x0011900F File Offset: 0x0011720F
		internal ADObjectId Key { get; private set; }

		// Token: 0x06004C11 RID: 19473 RVA: 0x00119018 File Offset: 0x00117218
		public static bool operator ==(CountryListKey key1, CountryListKey key2)
		{
			return object.Equals(key1, key2);
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x00119021 File Offset: 0x00117221
		public static bool operator !=(CountryListKey key1, CountryListKey key2)
		{
			return !object.Equals(key1, key2);
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x0011902D File Offset: 0x0011722D
		public override int GetHashCode()
		{
			return this.cachedHashCode;
		}

		// Token: 0x06004C14 RID: 19476 RVA: 0x00119038 File Offset: 0x00117238
		public override bool Equals(object obj)
		{
			CountryListKey countryListKey = obj as CountryListKey;
			return !(null == countryListKey) && ADObjectId.Equals(this.Key, countryListKey.Key);
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x00119068 File Offset: 0x00117268
		public override string ToString()
		{
			return this.Key.DistinguishedName.ToLower();
		}

		// Token: 0x04003432 RID: 13362
		private readonly int cachedHashCode;
	}
}
