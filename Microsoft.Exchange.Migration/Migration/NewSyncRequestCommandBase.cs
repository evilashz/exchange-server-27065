using System;
using System.Collections.Generic;
using System.Security;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000178 RID: 376
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NewSyncRequestCommandBase : NewMrsRequestCommandBase
	{
		// Token: 0x060011B9 RID: 4537 RVA: 0x0004AEFE File Offset: 0x000490FE
		protected NewSyncRequestCommandBase(string cmdletName, ICollection<Type> ignoredExceptions) : base(cmdletName, ignoredExceptions)
		{
		}

		// Token: 0x17000547 RID: 1351
		// (set) Token: 0x060011BA RID: 4538 RVA: 0x0004AF08 File Offset: 0x00049108
		public string RemoteServerName
		{
			set
			{
				base.AddParameter("RemoteServerName", value);
			}
		}

		// Token: 0x17000548 RID: 1352
		// (set) Token: 0x060011BB RID: 4539 RVA: 0x0004AF16 File Offset: 0x00049116
		public int RemoteServerPort
		{
			set
			{
				base.AddParameter("RemoteServerPort", value);
			}
		}

		// Token: 0x17000549 RID: 1353
		// (set) Token: 0x060011BC RID: 4540 RVA: 0x0004AF29 File Offset: 0x00049129
		public SecureString Password
		{
			set
			{
				base.AddParameter("Password", value);
			}
		}

		// Token: 0x1700054A RID: 1354
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x0004AF37 File Offset: 0x00049137
		public IMAPSecurityMechanism Security
		{
			set
			{
				base.AddParameter("Security", value);
			}
		}

		// Token: 0x1700054B RID: 1355
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x0004AF4A File Offset: 0x0004914A
		public AuthenticationMethod Authentication
		{
			set
			{
				base.AddParameter("Authentication", value);
			}
		}

		// Token: 0x1700054C RID: 1356
		// (set) Token: 0x060011BF RID: 4543 RVA: 0x0004AF5D File Offset: 0x0004915D
		public DateTime? StartAfter
		{
			set
			{
				base.AddParameter("StartAfter", value);
			}
		}

		// Token: 0x1700054D RID: 1357
		// (set) Token: 0x060011C0 RID: 4544 RVA: 0x0004AF70 File Offset: 0x00049170
		public DateTime? CompleteAfter
		{
			set
			{
				base.AddParameter("CompleteAfter", value);
			}
		}

		// Token: 0x04000626 RID: 1574
		internal const string StartAfterParameter = "StartAfter";

		// Token: 0x04000627 RID: 1575
		internal const string CompleteAfterParameter = "CompleteAfter";
	}
}
