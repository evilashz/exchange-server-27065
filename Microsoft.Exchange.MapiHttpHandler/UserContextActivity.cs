using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess;

namespace Microsoft.Exchange.MapiHttp
{
	// Token: 0x02000052 RID: 82
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UserContextActivity : BaseObject
	{
		// Token: 0x060002EB RID: 747 RVA: 0x0001276C File Offset: 0x0001096C
		public UserContextActivity(UserContext userContext)
		{
			this.userContext = userContext;
			this.userContext.AddReference();
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00012786 File Offset: 0x00010986
		public UserContext UserContext
		{
			get
			{
				base.CheckDisposed();
				return this.userContext;
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00012794 File Offset: 0x00010994
		protected override void InternalDispose()
		{
			this.userContext.ReleaseReference();
			base.InternalDispose();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000127A7 File Offset: 0x000109A7
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<UserContextActivity>(this);
		}

		// Token: 0x04000167 RID: 359
		private readonly UserContext userContext;
	}
}
