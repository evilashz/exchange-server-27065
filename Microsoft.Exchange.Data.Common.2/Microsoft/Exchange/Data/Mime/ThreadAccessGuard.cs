using System;

namespace Microsoft.Exchange.Data.Mime
{
	// Token: 0x02000050 RID: 80
	internal class ThreadAccessGuard : IDisposable
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x00010162 File Offset: 0x0000E362
		private ThreadAccessGuard(ObjectThreadAccessToken token)
		{
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0001016A File Offset: 0x0000E36A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00010179 File Offset: 0x0000E379
		internal static IDisposable EnterPublic(ObjectThreadAccessToken token)
		{
			return null;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x0001017C File Offset: 0x0000E37C
		internal static IDisposable EnterPrivate(ObjectThreadAccessToken token)
		{
			return null;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x0001017F File Offset: 0x0000E37F
		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				this.isDisposed = true;
			}
		}

		// Token: 0x04000260 RID: 608
		private bool isDisposed;
	}
}
