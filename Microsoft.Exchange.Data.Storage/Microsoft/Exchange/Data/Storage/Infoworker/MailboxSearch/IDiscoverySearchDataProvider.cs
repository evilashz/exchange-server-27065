using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D10 RID: 3344
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IDiscoverySearchDataProvider
	{
		// Token: 0x17001EA0 RID: 7840
		// (get) Token: 0x0600731E RID: 29470
		OrganizationId OrganizationId { get; }

		// Token: 0x17001EA1 RID: 7841
		// (get) Token: 0x0600731F RID: 29471
		string DisplayName { get; }

		// Token: 0x17001EA2 RID: 7842
		// (get) Token: 0x06007320 RID: 29472
		string DistinguishedName { get; }

		// Token: 0x17001EA3 RID: 7843
		// (get) Token: 0x06007321 RID: 29473
		string PrimarySmtpAddress { get; }

		// Token: 0x17001EA4 RID: 7844
		// (get) Token: 0x06007322 RID: 29474
		Guid ObjectGuid { get; }

		// Token: 0x06007323 RID: 29475
		IEnumerable<T> GetAll<T>() where T : DiscoverySearchBase, new();

		// Token: 0x06007324 RID: 29476
		T Find<T>(string searchId) where T : DiscoverySearchBase, new();

		// Token: 0x06007325 RID: 29477
		void CreateOrUpdate<T>(T discoverySearch) where T : DiscoverySearchBase;

		// Token: 0x06007326 RID: 29478
		void Delete<T>(string searchId) where T : DiscoverySearchBase, new();
	}
}
