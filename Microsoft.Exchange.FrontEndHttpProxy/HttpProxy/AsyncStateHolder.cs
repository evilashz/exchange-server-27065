using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000082 RID: 130
	internal class AsyncStateHolder : DisposeTrackableBase
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00017CF6 File Offset: 0x00015EF6
		public AsyncStateHolder(object owner)
		{
			if (owner == null)
			{
				throw new ArgumentNullException("owner");
			}
			this.Owner = owner;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00017D13 File Offset: 0x00015F13
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00017D1B File Offset: 0x00015F1B
		public object Owner { get; private set; }

		// Token: 0x060003F4 RID: 1012 RVA: 0x00017D24 File Offset: 0x00015F24
		public static T Unwrap<T>(IAsyncResult asyncResult)
		{
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			return (T)((object)((AsyncStateHolder)asyncResult.AsyncState).Owner);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00017D49 File Offset: 0x00015F49
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<AsyncStateHolder>(this);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00017D51 File Offset: 0x00015F51
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.Owner = null;
			}
		}
	}
}
