using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x02000077 RID: 119
	[DataContract]
	internal class DirectoryServer : DirectoryContainerParent
	{
		// Token: 0x0600040D RID: 1037 RVA: 0x0000B54F File Offset: 0x0000974F
		public DirectoryServer(IDirectoryProvider directory, DirectoryIdentity identity, string fqdn) : base(directory, identity)
		{
			this.Fqdn = fqdn;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600040E RID: 1038 RVA: 0x0000B560 File Offset: 0x00009760
		// (set) Token: 0x0600040F RID: 1039 RVA: 0x0000B568 File Offset: 0x00009768
		[DataMember]
		public string Fqdn { get; private set; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000410 RID: 1040 RVA: 0x0000B571 File Offset: 0x00009771
		public IEnumerable<DirectoryDatabase> Databases
		{
			get
			{
				return base.Children.Cast<DirectoryDatabase>();
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000B57E File Offset: 0x0000977E
		protected override IEnumerable<DirectoryObject> FetchChildren()
		{
			return base.Directory.GetDatabasesOwnedByServer(this);
		}
	}
}
