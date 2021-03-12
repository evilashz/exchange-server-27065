using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;

namespace Microsoft.Exchange.Management.Deployment.XforestTenantMigration
{
	// Token: 0x02000D7E RID: 3454
	[Serializable]
	public sealed class DirectoryObjectCollection : List<DirectoryObject>
	{
		// Token: 0x06008496 RID: 33942 RVA: 0x0021DDD4 File Offset: 0x0021BFD4
		public DirectoryObjectCollection()
		{
		}

		// Token: 0x06008497 RID: 33943 RVA: 0x0021DDDC File Offset: 0x0021BFDC
		public DirectoryObjectCollection(string searchRoot, SearchResultCollection searchResults)
		{
			if (searchRoot == null)
			{
				throw new ArgumentNullException("searchRoot");
			}
			if (searchResults == null)
			{
				throw new ArgumentNullException("searchResults");
			}
			foreach (object obj in searchResults)
			{
				SearchResult searchResult = (SearchResult)obj;
				base.Add(new DirectoryObject(searchRoot, searchResult));
			}
		}

		// Token: 0x17002934 RID: 10548
		public DirectoryObject this[string name]
		{
			get
			{
				return base.Find((DirectoryObject p) => p.DistinguishedName.Equals(name, StringComparison.CurrentCultureIgnoreCase));
			}
		}

		// Token: 0x06008499 RID: 33945 RVA: 0x0021DED4 File Offset: 0x0021C0D4
		internal DirectoryObject GetDirectoryObjectByLdapDisplayName(string ldapDisplayName)
		{
			return this.FirstOrDefault((DirectoryObject p) => p.Properties["lDAPDisplayName"][0].ToString().Equals(ldapDisplayName, StringComparison.CurrentCultureIgnoreCase));
		}
	}
}
