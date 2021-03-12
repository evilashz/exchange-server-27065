using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020000C5 RID: 197
	internal class DisposableWrapper<T> : DisposeTrackableBase where T : class, IDisposable
	{
		// Token: 0x060007B1 RID: 1969 RVA: 0x0000C8B4 File Offset: 0x0000AAB4
		public DisposableWrapper(T wrappedObject, bool ownsObject)
		{
			this.wrappedObject = wrappedObject;
			this.ownsObject = ownsObject;
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x060007B2 RID: 1970 RVA: 0x0000C8CA File Offset: 0x0000AACA
		public T WrappedObject
		{
			get
			{
				return this.wrappedObject;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x060007B3 RID: 1971 RVA: 0x0000C8D2 File Offset: 0x0000AAD2
		public bool OwnsObject
		{
			get
			{
				return this.ownsObject;
			}
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0000C8DA File Offset: 0x0000AADA
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose)
			{
				if (this.ownsObject && this.wrappedObject != null)
				{
					this.wrappedObject.Dispose();
				}
				this.wrappedObject = default(T);
			}
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0000C911 File Offset: 0x0000AB11
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DisposableWrapper<T>>(this);
		}

		// Token: 0x04000494 RID: 1172
		private T wrappedObject;

		// Token: 0x04000495 RID: 1173
		private bool ownsObject;
	}
}
