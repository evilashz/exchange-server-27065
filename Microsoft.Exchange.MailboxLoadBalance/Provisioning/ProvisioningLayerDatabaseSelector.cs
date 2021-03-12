using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.Provisioning
{
	// Token: 0x020000D6 RID: 214
	internal class ProvisioningLayerDatabaseSelector : DatabaseSelector
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x00012EF2 File Offset: 0x000110F2
		public ProvisioningLayerDatabaseSelector(IDirectoryProvider directory, ILogger logger) : base(logger)
		{
			this.directory = directory;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00012F02 File Offset: 0x00011102
		protected override IEnumerable<LoadContainer> GetAvailableDatabases()
		{
			return this.directory.GetCachedDatabasesForProvisioning().Select(new Func<DirectoryDatabase, LoadContainer>(this.CreateSimpleLoadContainer));
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00012F20 File Offset: 0x00011120
		private LoadContainer CreateSimpleLoadContainer(DirectoryDatabase database)
		{
			return database.ToLoadContainer();
		}

		// Token: 0x04000288 RID: 648
		private readonly IDirectoryProvider directory;
	}
}
