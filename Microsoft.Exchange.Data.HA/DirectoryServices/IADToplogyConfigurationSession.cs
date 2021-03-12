using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.HA.DirectoryServices
{
	// Token: 0x02000020 RID: 32
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IADToplogyConfigurationSession
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600018A RID: 394
		// (set) Token: 0x0600018B RID: 395
		bool UseConfigNC { get; set; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600018C RID: 396
		// (set) Token: 0x0600018D RID: 397
		bool UseGlobalCatalog { get; set; }

		// Token: 0x0600018E RID: 398
		IADServer FindServerByName(string serverShortName);

		// Token: 0x0600018F RID: 399
		IADDatabaseAvailabilityGroup FindDagByServer(IADServer server);

		// Token: 0x06000190 RID: 400
		IADComputer FindComputerByHostName(string hostName);

		// Token: 0x06000191 RID: 401
		IEnumerable<IADDatabase> GetAllDatabases(IADServer server);

		// Token: 0x06000192 RID: 402
		IEnumerable<IADDatabaseCopy> GetAllDatabaseCopies(IADServer server);

		// Token: 0x06000193 RID: 403
		TADWrapperObject ReadADObject<TADWrapperObject>(ADObjectId objectId) where TADWrapperObject : class, IADObjectCommon;

		// Token: 0x06000194 RID: 404
		TADWrapperObject[] Find<TADWrapperObject>(ADObjectId rootId, QueryScope scope, QueryFilter filter, SortBy sortBy, int maxResults) where TADWrapperObject : class, IADObjectCommon;

		// Token: 0x06000195 RID: 405
		IADDatabase FindDatabaseByGuid(Guid dbGuid);

		// Token: 0x06000196 RID: 406
		IADServer ReadMiniServer(ADObjectId entryId);

		// Token: 0x06000197 RID: 407
		IADServer FindMiniServerByName(string serverName);

		// Token: 0x06000198 RID: 408
		bool TryFindByExchangeLegacyDN(string legacyExchangeDN, out IADMiniClientAccessServerOrArray miniClientAccessServerOrArray);

		// Token: 0x06000199 RID: 409
		IADMiniClientAccessServerOrArray ReadMiniClientAccessServerOrArray(ADObjectId entryId);

		// Token: 0x0600019A RID: 410
		IADMiniClientAccessServerOrArray FindMiniClientAccessServerOrArrayByFqdn(string serverFqdn);

		// Token: 0x0600019B RID: 411
		IADSite GetLocalSite();
	}
}
