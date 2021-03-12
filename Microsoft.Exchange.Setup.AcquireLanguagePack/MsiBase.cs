using System;

namespace Microsoft.Exchange.Setup.AcquireLanguagePack
{
	// Token: 0x02000009 RID: 9
	internal abstract class MsiBase : IDisposable
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00002A98 File Offset: 0x00000C98
		protected MsiBase()
		{
			this.disposed = false;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002AA7 File Offset: 0x00000CA7
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002AAF File Offset: 0x00000CAF
		public SafeMsiHandle Handle { get; set; }

		// Token: 0x0600002D RID: 45 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002AC7 File Offset: 0x00000CC7
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing && this.Handle != null && !this.Handle.IsInvalid)
				{
					this.Handle.Dispose();
				}
				this.Handle = null;
				this.disposed = true;
			}
		}

		// Token: 0x0400001A RID: 26
		private bool disposed;
	}
}
