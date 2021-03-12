using System;
using System.Diagnostics;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser
{
	// Token: 0x0200015F RID: 351
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class FastTransferDownloadContext : FastTransferContext<FastTransferWriter>
	{
		// Token: 0x06000681 RID: 1665 RVA: 0x00013117 File Offset: 0x00011317
		private FastTransferDownloadContext(bool isIcs, FastTransferSendOption options, uint steps, Encoding encoding, IResourceTracker resourceTracker, IPropertyFilterFactory propertyFilterFactory, bool isMovingMailbox) : base(resourceTracker, propertyFilterFactory, isMovingMailbox)
		{
			this.isIcs = isIcs;
			this.options = options;
			this.steps = steps;
			this.encoding = encoding;
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x00013142 File Offset: 0x00011342
		public static FastTransferDownloadContext CreateForIcs(FastTransferSendOption options, Encoding encoding, IResourceTracker resourceTracker, IPropertyFilterFactory propertyFilterFactory, bool isMovingMailbox)
		{
			return new FastTransferDownloadContext(true, options, 1U, encoding, resourceTracker, propertyFilterFactory, isMovingMailbox);
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x00013151 File Offset: 0x00011351
		public static FastTransferDownloadContext CreateForDownload(FastTransferSendOption options, uint steps, Encoding encoding, IResourceTracker resourceTracker, IPropertyFilterFactory propertyFilterFactory, bool isMovingMailbox)
		{
			return new FastTransferDownloadContext(false, options, steps, encoding, resourceTracker, propertyFilterFactory, isMovingMailbox);
		}

		// Token: 0x06000684 RID: 1668 RVA: 0x00013164 File Offset: 0x00011364
		public int GetNextBuffer(ArraySegment<byte> buffer)
		{
			base.CheckDisposed();
			int position;
			using (FastTransferWriter fastTransferWriter = new FastTransferWriter(buffer))
			{
				this.Process(fastTransferWriter);
				position = fastTransferWriter.Position;
			}
			return position;
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000131AC File Offset: 0x000113AC
		public void PushInitial(IFastTransferProcessor<FastTransferDownloadContext> fastTransferObject)
		{
			base.PushInitial(this.CreateStateMachine(fastTransferObject));
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x000131BB File Offset: 0x000113BB
		public uint Progress
		{
			get
			{
				base.CheckDisposed();
				return this.progress;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000131C9 File Offset: 0x000113C9
		public uint Steps
		{
			get
			{
				base.CheckDisposed();
				return this.steps;
			}
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x000131D7 File Offset: 0x000113D7
		public void IncrementProgress()
		{
			base.CheckDisposed();
			this.progress += 1U;
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x000131ED File Offset: 0x000113ED
		internal bool IsIcs
		{
			get
			{
				return this.isIcs;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x0600068A RID: 1674 RVA: 0x000131F5 File Offset: 0x000113F5
		internal bool UseCpidOrUnicode
		{
			get
			{
				return this.options.UseCpidOrUnicode();
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00013202 File Offset: 0x00011402
		internal bool UseCpid
		{
			get
			{
				return this.options.UseCpid();
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600068C RID: 1676 RVA: 0x0001320F File Offset: 0x0001140F
		internal bool SendPropertyErrors
		{
			get
			{
				return (byte)(this.options & FastTransferSendOption.SendPropErrors) != 0;
			}
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x00013221 File Offset: 0x00011421
		internal FastTransferStateMachine CreateStateMachine(IFastTransferProcessor<FastTransferDownloadContext> fastTransferObject)
		{
			return FastTransferContext<FastTransferWriter>.CreateStateMachine<FastTransferDownloadContext>(this, fastTransferObject);
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600068E RID: 1678 RVA: 0x0001322A File Offset: 0x0001142A
		public Encoding String8Encoding
		{
			get
			{
				return this.encoding;
			}
		}

		// Token: 0x0600068F RID: 1679 RVA: 0x00013232 File Offset: 0x00011432
		protected override void Process(FastTransferWriter dataInterface)
		{
			if (dataInterface.TryWriteOverflow(ref this.overflowBytes))
			{
				base.Process(dataInterface);
				this.overflowBytes = dataInterface.GetOverflowBytes();
			}
			if (base.State != FastTransferState.Error && this.overflowBytes.Count > 0)
			{
				base.State = FastTransferState.Partial;
			}
		}

		// Token: 0x06000690 RID: 1680 RVA: 0x00013272 File Offset: 0x00011472
		[DebuggerNonUserCode]
		protected override bool CanContinue()
		{
			return !base.DataInterface.IsBufferFull;
		}

		// Token: 0x04000351 RID: 849
		private readonly bool isIcs;

		// Token: 0x04000352 RID: 850
		private readonly FastTransferSendOption options;

		// Token: 0x04000353 RID: 851
		private readonly Encoding encoding;

		// Token: 0x04000354 RID: 852
		private readonly uint steps;

		// Token: 0x04000355 RID: 853
		private uint progress;

		// Token: 0x04000356 RID: 854
		private ArraySegment<byte> overflowBytes;
	}
}
