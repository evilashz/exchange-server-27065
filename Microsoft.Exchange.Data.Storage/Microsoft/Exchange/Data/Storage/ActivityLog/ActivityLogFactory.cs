using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage.ActivityLog
{
	// Token: 0x0200033C RID: 828
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ActivityLogFactory
	{
		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x060024C8 RID: 9416 RVA: 0x000947A5 File Offset: 0x000929A5
		internal static Hookable<ActivityLogFactory> Instance
		{
			get
			{
				return ActivityLogFactory.HookableInstance;
			}
		}

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x060024C9 RID: 9417 RVA: 0x000947AC File Offset: 0x000929AC
		public static ActivityLogFactory Current
		{
			get
			{
				return ActivityLogFactory.HookableInstance.Value;
			}
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000947B8 File Offset: 0x000929B8
		public virtual IActivityLog Bind(MailboxSession mailboxSession)
		{
			return new AppendOnlyActivityLog(mailboxSession);
		}

		// Token: 0x04001656 RID: 5718
		private static readonly Hookable<ActivityLogFactory> HookableInstance = Hookable<ActivityLogFactory>.Create(true, new ActivityLogFactory());
	}
}
