using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020002DA RID: 730
	public class AddressListInfo
	{
		// Token: 0x06001C25 RID: 7205 RVA: 0x000A1DCD File Offset: 0x0009FFCD
		private AddressListInfo(string displayName, ADObjectId id, OrganizationId organizationId)
		{
			if (organizationId == null)
			{
				throw new ArgumentNullException("organizationId");
			}
			this.displayName = displayName;
			this.id = id;
			this.organizationId = organizationId;
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x000A1DFE File Offset: 0x0009FFFE
		internal AddressListInfo(AddressBookBase addressList)
		{
			if (addressList == null)
			{
				throw new ArgumentNullException("addressList");
			}
			this.DisplayName = addressList.DisplayName;
			this.Id = addressList.Id;
			this.organizationId = addressList.OrganizationId;
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x000A1E38 File Offset: 0x000A0038
		internal static AddressListInfo CreateEmpty(OrganizationId organizationId)
		{
			return new AddressListInfo(null, null, organizationId);
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001C28 RID: 7208 RVA: 0x000A1E42 File Offset: 0x000A0042
		internal bool IsEmpty
		{
			get
			{
				return this.DisplayName == null && this.Id == null;
			}
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x000A1E58 File Offset: 0x000A0058
		public string ToBase64String()
		{
			if (this.Id == null)
			{
				return "0000";
			}
			return Convert.ToBase64String(this.id.ObjectGuid.ToByteArray());
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x000A1E8C File Offset: 0x000A008C
		public virtual AddressBookBase ToAddressBookBase()
		{
			AddressBookBase addressBookBase = new AddressBookBase();
			addressBookBase.SetId(this.Id);
			addressBookBase.DisplayName = this.DisplayName;
			addressBookBase.OrganizationId = this.OrganizationId;
			return addressBookBase;
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x000A1EC4 File Offset: 0x000A00C4
		// (set) Token: 0x06001C2C RID: 7212 RVA: 0x000A1ECC File Offset: 0x000A00CC
		public string DisplayName
		{
			get
			{
				return this.displayName;
			}
			set
			{
				this.displayName = value;
			}
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001C2D RID: 7213 RVA: 0x000A1ED5 File Offset: 0x000A00D5
		// (set) Token: 0x06001C2E RID: 7214 RVA: 0x000A1EDD File Offset: 0x000A00DD
		public ADObjectId Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001C2F RID: 7215 RVA: 0x000A1EE6 File Offset: 0x000A00E6
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
		}

		// Token: 0x040014DF RID: 5343
		private string displayName;

		// Token: 0x040014E0 RID: 5344
		private ADObjectId id;

		// Token: 0x040014E1 RID: 5345
		private readonly OrganizationId organizationId;
	}
}
