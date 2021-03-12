using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x0200006E RID: 110
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class CloudArchive : DirectoryMailbox
	{
		// Token: 0x060003D5 RID: 981 RVA: 0x0000AE0B File Offset: 0x0000900B
		public CloudArchive(IDirectoryProvider directory, DirectoryIdentity identity, IEnumerable<IPhysicalMailbox> physicalMailboxes) : base(directory, identity, physicalMailboxes, DirectoryMailboxType.Organization)
		{
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000AE17 File Offset: 0x00009017
		public override bool IsArchiveOnly
		{
			get
			{
				return true;
			}
		}
	}
}
