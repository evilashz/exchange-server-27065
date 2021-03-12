using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Win32;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000034 RID: 52
	internal class AmClusterPropListDisposable : AmClusterPropList, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000218 RID: 536 RVA: 0x00009A99 File Offset: 0x00007C99
		public AmClusterPropListDisposable(SafeHGlobalHandle buffer, uint bufferSize) : base(buffer.DangerousGetHandle(), bufferSize)
		{
			this.m_disposeTracker = this.GetDisposeTracker();
			this.HBuffer = buffer;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000219 RID: 537 RVA: 0x00009ABB File Offset: 0x00007CBB
		// (set) Token: 0x0600021A RID: 538 RVA: 0x00009AC3 File Offset: 0x00007CC3
		private SafeHGlobalHandle HBuffer { get; set; }

		// Token: 0x0600021B RID: 539 RVA: 0x00009ACC File Offset: 0x00007CCC
		public void Dispose()
		{
			if (!this.m_isDisposed)
			{
				this.Dispose(true);
				GC.SuppressFinalize(this);
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009AE4 File Offset: 0x00007CE4
		public void Dispose(bool disposing)
		{
			lock (this)
			{
				if (this.HBuffer != null)
				{
					this.HBuffer.Dispose();
					this.HBuffer = null;
				}
				if (this.m_disposeTracker != null)
				{
					this.m_disposeTracker.Dispose();
					this.m_disposeTracker = null;
				}
				this.m_isDisposed = true;
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009B54 File Offset: 0x00007D54
		public DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<AmClusterPropListDisposable>(this);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009B5C File Offset: 0x00007D5C
		public void SuppressDisposeTracker()
		{
			if (this.m_disposeTracker != null)
			{
				this.m_disposeTracker.Suppress();
			}
		}

		// Token: 0x04000091 RID: 145
		private DisposeTracker m_disposeTracker;

		// Token: 0x04000092 RID: 146
		private bool m_isDisposed;
	}
}
