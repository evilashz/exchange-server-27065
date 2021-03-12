using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x02000078 RID: 120
	[ClassAccessLevel(AccessLevel.Implementation)]
	[DataContract]
	internal class DummyDatabaseAvailabilityGroup : DirectoryDatabaseAvailabilityGroup
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0000B58C File Offset: 0x0000978C
		public DummyDatabaseAvailabilityGroup(IDirectoryProvider directory) : base(directory, new DirectoryIdentity(DirectoryObjectType.DatabaseAvailabilityGroup, Guid.Empty, string.Empty, Guid.Empty))
		{
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000B5AA File Offset: 0x000097AA
		protected override IEnumerable<DirectoryObject> FetchChildren()
		{
			return base.Directory.GetServers();
		}
	}
}
