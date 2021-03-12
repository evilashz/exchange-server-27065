using System;
using System.IO;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.PopImap.Core
{
	// Token: 0x02000039 RID: 57
	internal class StreamResponseItem : IResponseItem, IDisposeTrackable, IDisposable
	{
		// Token: 0x060003CE RID: 974 RVA: 0x000110E8 File Offset: 0x0000F2E8
		public StreamResponseItem(Stream s) : this(s, null)
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x000110F2 File Offset: 0x0000F2F2
		public StreamResponseItem(Stream s, BaseSession.SendCompleteDelegate sendCompleteDelegate)
		{
			this.disposeTracker = this.GetDisposeTracker();
			this.sendCompleteDelegate = sendCompleteDelegate;
			this.responseStream = s;
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00011114 File Offset: 0x0000F314
		public BaseSession.SendCompleteDelegate SendCompleteDelegate
		{
			get
			{
				return this.sendCompleteDelegate;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0001111C File Offset: 0x0000F31C
		public int GetNextChunk(BaseSession session, out byte[] buffer, out int offset)
		{
			if (this.sendBuffer == null)
			{
				this.sendBuffer = StreamResponseItem.sendBufferPool.Acquire();
			}
			buffer = this.sendBuffer;
			offset = 0;
			int num = this.responseStream.Read(buffer, 0, buffer.Length);
			if (num == 0)
			{
				this.responseStream.Close();
				this.responseStream = null;
			}
			else
			{
				ProtocolSession protocolSession = session as ProtocolSession;
				if (protocolSession != null)
				{
					protocolSession.LogSend("{0} bytes", num);
				}
			}
			return num;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001118D File Offset: 0x0000F38D
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<StreamResponseItem>(this);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00011195 File Offset: 0x0000F395
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000111AA File Offset: 0x0000F3AA
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000111BC File Offset: 0x0000F3BC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.disposeTracker != null)
			{
				this.disposeTracker.Dispose();
				this.disposeTracker = null;
			}
			if (this.responseStream != null)
			{
				this.responseStream.Close();
				this.responseStream = null;
			}
			if (this.sendBuffer != null)
			{
				StreamResponseItem.sendBufferPool.Release(this.sendBuffer);
				this.sendBuffer = null;
			}
		}

		// Token: 0x040001F8 RID: 504
		private static BufferPool sendBufferPool = new BufferPool(4096);

		// Token: 0x040001F9 RID: 505
		private Stream responseStream;

		// Token: 0x040001FA RID: 506
		private BaseSession.SendCompleteDelegate sendCompleteDelegate;

		// Token: 0x040001FB RID: 507
		private byte[] sendBuffer;

		// Token: 0x040001FC RID: 508
		private DisposeTracker disposeTracker;
	}
}
