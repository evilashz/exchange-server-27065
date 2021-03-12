using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020000E3 RID: 227
	internal sealed class OrgIdADObjectWrapper
	{
		// Token: 0x0600085D RID: 2141 RVA: 0x0001B8FC File Offset: 0x00019AFC
		public OrgIdADObjectWrapper(ADObjectId adObject, OrganizationId orgId)
		{
			if (adObject == null)
			{
				throw new ArgumentNullException("adObject");
			}
			if (orgId == null)
			{
				throw new ArgumentNullException("orgId");
			}
			this.AdObject = adObject;
			this.OrgId = orgId;
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x0001B934 File Offset: 0x00019B34
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x0001B93C File Offset: 0x00019B3C
		public ADObjectId AdObject { get; private set; }

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x0001B945 File Offset: 0x00019B45
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x0001B94D File Offset: 0x00019B4D
		public OrganizationId OrgId { get; private set; }

		// Token: 0x06000862 RID: 2146 RVA: 0x0001B958 File Offset: 0x00019B58
		public override bool Equals(object obj)
		{
			if (!(obj is OrgIdADObjectWrapper))
			{
				return false;
			}
			OrgIdADObjectWrapper orgIdADObjectWrapper = (OrgIdADObjectWrapper)obj;
			return this.AdObject.Equals(orgIdADObjectWrapper.AdObject) && this.OrgId.Equals(orgIdADObjectWrapper.OrgId);
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001B99C File Offset: 0x00019B9C
		public override int GetHashCode()
		{
			return this.AdObject.GetHashCode() ^ this.OrgId.GetHashCode();
		}

		// Token: 0x06000864 RID: 2148 RVA: 0x0001B9B5 File Offset: 0x00019BB5
		public override string ToString()
		{
			return this.AdObject + "-" + this.OrgId;
		}
	}
}
