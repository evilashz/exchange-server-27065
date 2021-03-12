using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x0200017C RID: 380
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class FastTransferUploadContext : FastTransferContext<IFastTransferReader>
	{
		// Token: 0x06000764 RID: 1892 RVA: 0x00019F7B File Offset: 0x0001817B
		internal FastTransferUploadContext(Encoding encoding, IResourceTracker resourceTracker, IPropertyFilterFactory propertyFilterFactory, bool isMovingMailbox) : base(resourceTracker, propertyFilterFactory, isMovingMailbox)
		{
			this.encoding = encoding;
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x00019F95 File Offset: 0x00018195
		public void PutNextBuffer(IFastTransferReader reader)
		{
			base.CheckDisposed();
			if (this.noMoreBuffers)
			{
				throw new InvalidOperationException();
			}
			this.Process(reader);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x00019FB4 File Offset: 0x000181B4
		public void PutNextBuffer(ArraySegment<byte> buffer)
		{
			base.CheckDisposed();
			using (IFastTransferReader fastTransferReader = this.CreateReader(buffer))
			{
				this.PutNextBuffer(fastTransferReader);
			}
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x00019FF4 File Offset: 0x000181F4
		public void Flush()
		{
			base.CheckDisposed();
			this.noMoreBuffers = true;
			using (IFastTransferReader fastTransferReader = this.CreateReader(new ArraySegment<byte>(Array<byte>.Empty)))
			{
				try
				{
					this.Process(fastTransferReader);
				}
				catch (BufferParseException)
				{
					throw new BufferParseException(string.Format("Unexpected end of input FastTransfer stream. Final state: {0}", this.ToString()));
				}
			}
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0001A068 File Offset: 0x00018268
		public void PushInitial(IFastTransferProcessor<FastTransferUploadContext> fastTransferObject)
		{
			base.PushInitial(this.CreateStateMachine(fastTransferObject));
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0001A077 File Offset: 0x00018277
		public bool NoMoreData
		{
			get
			{
				base.CheckDisposed();
				return this.noMoreBuffers && !base.DataInterface.IsDataAvailable;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0001A097 File Offset: 0x00018297
		public Encoding String8Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0001A09F File Offset: 0x0001829F
		protected override void Process(IFastTransferReader dataInterface)
		{
			base.Process(dataInterface);
			this.OnEndOfBuffer();
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0001A0AE File Offset: 0x000182AE
		internal FastTransferStateMachine CreateStateMachine(IFastTransferProcessor<FastTransferUploadContext> fastTransferObject)
		{
			return FastTransferContext<IFastTransferReader>.CreateStateMachine<FastTransferUploadContext>(this, fastTransferObject);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0001A0B7 File Offset: 0x000182B7
		internal void SetEndOfBufferAction(Action action)
		{
			this.endOfBufferAction = action;
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0001A0C0 File Offset: 0x000182C0
		internal void AllowEndOfBufferActions(bool allow)
		{
			this.allowEndOfBufferActions = allow;
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0001A0C9 File Offset: 0x000182C9
		protected override bool CanContinue()
		{
			return base.DataInterface.IsDataAvailable || this.noMoreBuffers;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0001A0E0 File Offset: 0x000182E0
		protected virtual IFastTransferReader CreateReader(ArraySegment<byte> buffer)
		{
			return new FastTransferReader(buffer);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0001A0E8 File Offset: 0x000182E8
		private void OnEndOfBuffer()
		{
			if (this.allowEndOfBufferActions && this.endOfBufferAction != null)
			{
				this.endOfBufferAction();
			}
		}

		// Token: 0x040003A5 RID: 933
		private readonly Encoding encoding;

		// Token: 0x040003A6 RID: 934
		private bool noMoreBuffers;

		// Token: 0x040003A7 RID: 935
		private Action endOfBufferAction;

		// Token: 0x040003A8 RID: 936
		private bool allowEndOfBufferActions = true;
	}
}
