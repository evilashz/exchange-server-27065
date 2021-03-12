using System;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000B01 RID: 2817
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IOrganizationIdConvertor
	{
		// Token: 0x06006659 RID: 26201
		OrganizationId FromExternalDirectoryOrganizationId(Guid externalDirectoryOrganizationId);

		// Token: 0x0600665A RID: 26202
		string ToExternalDirectoryOrganizationId(OrganizationId orgId);
	}
}
