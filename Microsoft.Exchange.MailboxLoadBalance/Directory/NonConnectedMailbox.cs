using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Providers;

namespace Microsoft.Exchange.MailboxLoadBalance.Directory
{
	// Token: 0x0200007C RID: 124
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class NonConnectedMailbox : DirectoryMailbox
	{
		// Token: 0x0600044F RID: 1103 RVA: 0x0000B693 File Offset: 0x00009893
		public NonConnectedMailbox(IDirectoryProvider directory, DirectoryIdentity identity, IEnumerable<IPhysicalMailbox> physicalMailboxes) : base(directory, identity, physicalMailboxes, DirectoryMailboxType.Organization)
		{
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000B6A0 File Offset: 0x000098A0
		public bool IsSoftDeleted
		{
			get
			{
				IPhysicalMailbox physicalMailbox = base.PhysicalMailboxes.FirstOrDefault<IPhysicalMailbox>();
				return physicalMailbox != null && physicalMailbox.IsSoftDeleted;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000B6C4 File Offset: 0x000098C4
		public DateTime? DisconnectDate
		{
			get
			{
				IPhysicalMailbox physicalMailbox = base.PhysicalMailboxes.FirstOrDefault<IPhysicalMailbox>();
				if (physicalMailbox != null)
				{
					return physicalMailbox.DisconnectDate;
				}
				return null;
			}
		}
	}
}
