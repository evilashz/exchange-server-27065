using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x02000073 RID: 115
	[DataContract]
	internal class DirectoryForest : DirectoryContainerParent
	{
		// Token: 0x060003F2 RID: 1010 RVA: 0x0000B260 File Offset: 0x00009460
		public DirectoryForest(IDirectoryProvider directory, DirectoryIdentity identity) : base(directory, identity)
		{
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060003F3 RID: 1011 RVA: 0x0000B26A File Offset: 0x0000946A
		public IEnumerable<DirectoryDatabaseAvailabilityGroup> DatabaseAvailabilityGroups
		{
			get
			{
				return base.Children.Cast<DirectoryDatabaseAvailabilityGroup>();
			}
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x0000B277 File Offset: 0x00009477
		protected override IEnumerable<DirectoryObject> FetchChildren()
		{
			return base.Directory.GetDatabaseAvailabilityGroups();
		}
	}
}
