using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200025A RID: 602
	[ComVisible(true)]
	public abstract class DeriveBytes : IDisposable
	{
		// Token: 0x06002164 RID: 8548
		public abstract byte[] GetBytes(int cb);

		// Token: 0x06002165 RID: 8549
		public abstract void Reset();

		// Token: 0x06002166 RID: 8550 RVA: 0x00076331 File Offset: 0x00074531
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x00076340 File Offset: 0x00074540
		protected virtual void Dispose(bool disposing)
		{
		}
	}
}
