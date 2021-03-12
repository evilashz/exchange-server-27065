using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000149 RID: 329
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class NewMailboxImportRequestCommandBase : NewMrsRequestCommandBase
	{
		// Token: 0x0600108E RID: 4238 RVA: 0x00045818 File Offset: 0x00043A18
		protected NewMailboxImportRequestCommandBase(string cmdletName, ICollection<Type> ignoredExceptions) : base(cmdletName, ignoredExceptions)
		{
		}

		// Token: 0x170004F6 RID: 1270
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x00045822 File Offset: 0x00043A22
		public Fqdn RemoteHostName
		{
			set
			{
				base.AddParameter("RemoteHostName", value);
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (set) Token: 0x06001090 RID: 4240 RVA: 0x00045830 File Offset: 0x00043A30
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

		// Token: 0x170004F8 RID: 1272
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x00045841 File Offset: 0x00043A41
		public bool AutoCleanup
		{
			set
			{
				base.AddParameter("CompletedRequestAgeLimit", value ? EnhancedTimeSpan.FromDays(1.0) : EnhancedTimeSpan.FromDays(7.0));
			}
		}
	}
}
