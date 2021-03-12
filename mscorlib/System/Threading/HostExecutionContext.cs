using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004CE RID: 1230
	public class HostExecutionContext : IDisposable
	{
		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06003B1E RID: 15134 RVA: 0x000DF541 File Offset: 0x000DD741
		// (set) Token: 0x06003B1F RID: 15135 RVA: 0x000DF549 File Offset: 0x000DD749
		protected internal object State
		{
			get
			{
				return this.state;
			}
			set
			{
				this.state = value;
			}
		}

		// Token: 0x06003B20 RID: 15136 RVA: 0x000DF552 File Offset: 0x000DD752
		public HostExecutionContext()
		{
		}

		// Token: 0x06003B21 RID: 15137 RVA: 0x000DF55A File Offset: 0x000DD75A
		public HostExecutionContext(object state)
		{
			this.state = state;
		}

		// Token: 0x06003B22 RID: 15138 RVA: 0x000DF56C File Offset: 0x000DD76C
		[SecuritySafeCritical]
		public virtual HostExecutionContext CreateCopy()
		{
			object obj = this.state;
			if (this.state is IUnknownSafeHandle)
			{
				obj = ((IUnknownSafeHandle)this.state).Clone();
			}
			return new HostExecutionContext(this.state);
		}

		// Token: 0x06003B23 RID: 15139 RVA: 0x000DF5A9 File Offset: 0x000DD7A9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06003B24 RID: 15140 RVA: 0x000DF5B8 File Offset: 0x000DD7B8
		public virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x040018E0 RID: 6368
		private object state;
	}
}
