using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x0200004D RID: 77
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SessionContextActivity : BaseObject
	{
		// Token: 0x060002BC RID: 700 RVA: 0x000111CF File Offset: 0x0000F3CF
		private SessionContextActivity(SessionContext sessionContext)
		{
			this.sessionContext = sessionContext;
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000111DE File Offset: 0x0000F3DE
		public SessionContext SessionContext
		{
			get
			{
				base.CheckDisposed();
				return this.sessionContext;
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x000111EC File Offset: 0x0000F3EC
		public static bool TryCreate(SessionContext sessionContext, out SessionContextActivity sessionContextActivity)
		{
			sessionContextActivity = null;
			bool flag = false;
			if (!sessionContext.TryAddReference())
			{
				return false;
			}
			try
			{
				sessionContextActivity = new SessionContextActivity(sessionContext);
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					sessionContext.ReleaseReference();
				}
			}
			return true;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00011230 File Offset: 0x0000F430
		protected override void InternalDispose()
		{
			this.sessionContext.ReleaseReference();
			base.InternalDispose();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00011243 File Offset: 0x0000F443
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<SessionContextActivity>(this);
		}

		// Token: 0x0400013B RID: 315
		private readonly SessionContext sessionContext;
	}
}
