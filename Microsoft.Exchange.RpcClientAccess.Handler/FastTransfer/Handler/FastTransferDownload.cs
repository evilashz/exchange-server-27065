using System;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000062 RID: 98
	internal class FastTransferDownload : FastTransferServerObject<FastTransferDownloadContext>
	{
		// Token: 0x06000415 RID: 1045 RVA: 0x0001DFFB File Offset: 0x0001C1FB
		protected FastTransferDownload(FastTransferDownloadContext context, Logon logon) : base(context, logon)
		{
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0001E005 File Offset: 0x0001C205
		public FastTransferDownload(FastTransferSendOption sendOptions, IFastTransferProcessor<FastTransferDownloadContext> fastTransferObject, uint steps, IPropertyFilterFactory propertyFilterFactory, Logon logon) : this(FastTransferDownloadContext.CreateForDownload(sendOptions, steps, logon.String8Encoding, logon.ResourceTracker, propertyFilterFactory, false), logon)
		{
			base.Context.PushInitial(fastTransferObject);
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000417 RID: 1047 RVA: 0x0001E033 File Offset: 0x0001C233
		public uint Progress
		{
			get
			{
				base.CheckDisposed();
				return base.Context.Progress;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000418 RID: 1048 RVA: 0x0001E046 File Offset: 0x0001C246
		public uint Steps
		{
			get
			{
				base.CheckDisposed();
				return base.Context.Steps;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000419 RID: 1049 RVA: 0x0001E059 File Offset: 0x0001C259
		public bool IsMovingMailbox
		{
			get
			{
				base.CheckDisposed();
				return base.Context.IsMovingMailbox;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600041A RID: 1050 RVA: 0x0001E06C File Offset: 0x0001C26C
		public FastTransferState State
		{
			get
			{
				base.CheckDisposed();
				return base.Context.State;
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0001E07F File Offset: 0x0001C27F
		public int GetNextBuffer(ArraySegment<byte> buffer)
		{
			base.CheckDisposed();
			return this.InternalGetNextBuffer(buffer);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0001E08E File Offset: 0x0001C28E
		protected virtual int InternalGetNextBuffer(ArraySegment<byte> buffer)
		{
			return base.Context.GetNextBuffer(buffer);
		}
	}
}
