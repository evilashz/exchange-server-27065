using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000164 RID: 356
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NewMoveRequestCommandBase : NewMrsRequestCommandBase
	{
		// Token: 0x06001151 RID: 4433 RVA: 0x00049562 File Offset: 0x00047762
		protected NewMoveRequestCommandBase(string cmdletName, ICollection<Type> ignoredExceptions) : base(cmdletName, ignoredExceptions)
		{
		}

		// Token: 0x17000527 RID: 1319
		// (set) Token: 0x06001152 RID: 4434 RVA: 0x0004956C File Offset: 0x0004776C
		public Fqdn RemoteHostName
		{
			set
			{
				base.AddParameter("RemoteHostName", value);
			}
		}

		// Token: 0x17000528 RID: 1320
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x0004957A File Offset: 0x0004777A
		public PSCredential RemoteCredential
		{
			set
			{
				if (value != null)
				{
					base.AddParameter("RemoteCredential", value);
				}
			}
		}

		// Token: 0x17000529 RID: 1321
		// (set) Token: 0x06001154 RID: 4436 RVA: 0x0004958B File Offset: 0x0004778B
		public bool AutoCleanup
		{
			set
			{
				base.AddParameter("CompletedRequestAgeLimit", value ? EnhancedTimeSpan.FromDays(0.0) : EnhancedTimeSpan.FromDays(7.0));
			}
		}

		// Token: 0x1700052A RID: 1322
		// (set) Token: 0x06001155 RID: 4437 RVA: 0x000495BE File Offset: 0x000477BE
		public DateTime? StartAfter
		{
			set
			{
				base.AddParameter("StartAfter", value);
			}
		}

		// Token: 0x1700052B RID: 1323
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x000495D1 File Offset: 0x000477D1
		public DateTime? CompleteAfter
		{
			set
			{
				base.AddParameter("CompleteAfter", value);
			}
		}
	}
}
