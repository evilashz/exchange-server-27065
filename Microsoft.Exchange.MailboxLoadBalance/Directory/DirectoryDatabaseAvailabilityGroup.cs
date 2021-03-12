using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x02000072 RID: 114
	[DataContract]
	internal class DirectoryDatabaseAvailabilityGroup : DirectoryContainerParent
	{
		// Token: 0x060003EF RID: 1007 RVA: 0x0000B236 File Offset: 0x00009436
		public DirectoryDatabaseAvailabilityGroup(IDirectoryProvider directory, DirectoryIdentity identity) : base(directory, identity)
		{
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x0000B240 File Offset: 0x00009440
		public IEnumerable<DirectoryServer> Servers
		{
			get
			{
				return base.Children.Cast<DirectoryServer>();
			}
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x0000B24D File Offset: 0x0000944D
		protected override IEnumerable<DirectoryObject> FetchChildren()
		{
			return base.Directory.GetServers(base.Identity);
		}
	}
}
