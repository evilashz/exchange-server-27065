using System;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000008 RID: 8
	public abstract class EsentResource : IDisposable
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002528 File Offset: 0x00000728
		~EsentResource()
		{
			this.Dispose(false);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002558 File Offset: 0x00000758
		protected bool HasResource
		{
			get
			{
				return this.hasResource;
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002560 File Offset: 0x00000760
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x0000256F File Offset: 0x0000076F
		protected virtual void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				if (this.hasResource)
				{
					this.ReleaseResource();
				}
				this.isDisposed = true;
				return;
			}
			bool flag = this.hasResource;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002591 File Offset: 0x00000791
		protected void CheckObjectIsNotDisposed()
		{
			if (this.isDisposed)
			{
				throw new ObjectDisposedException("EsentResource");
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000025A6 File Offset: 0x000007A6
		protected void ResourceWasAllocated()
		{
			this.CheckObjectIsNotDisposed();
			this.hasResource = true;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000025B5 File Offset: 0x000007B5
		protected void ResourceWasReleased()
		{
			this.CheckObjectIsNotDisposed();
			this.hasResource = false;
		}

		// Token: 0x06000017 RID: 23
		protected abstract void ReleaseResource();

		// Token: 0x04000021 RID: 33
		private bool hasResource;

		// Token: 0x04000022 RID: 34
		private bool isDisposed;
	}
}
