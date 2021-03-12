using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B02 RID: 2818
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class OrganizationIdConvertor : IOrganizationIdConvertor
	{
		// Token: 0x0600665C RID: 26204 RVA: 0x001B2556 File Offset: 0x001B0756
		public OrganizationId FromExternalDirectoryOrganizationId(Guid externalDirectoryOrganizationId)
		{
			return OrganizationId.FromExternalDirectoryOrganizationId(externalDirectoryOrganizationId);
		}

		// Token: 0x0600665D RID: 26205 RVA: 0x001B255E File Offset: 0x001B075E
		public string ToExternalDirectoryOrganizationId(OrganizationId orgId)
		{
			return orgId.ToExternalDirectoryOrganizationId();
		}

		// Token: 0x04003A20 RID: 14880
		public static readonly OrganizationIdConvertor Default = new OrganizationIdConvertor();
	}
}
