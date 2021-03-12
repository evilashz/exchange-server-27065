using System;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000064 RID: 100
	internal class FastTransferUpload : FastTransferServerObject<FastTransferUploadContext>
	{
		// Token: 0x06000421 RID: 1057 RVA: 0x0001E154 File Offset: 0x0001C354
		protected FastTransferUpload(FastTransferUploadContext context, Logon logon) : base(context, logon)
		{
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x0001E15E File Offset: 0x0001C35E
		public FastTransferUpload(IFastTransferProcessor<FastTransferUploadContext> fastTransferObject, IPropertyFilterFactory propertyFilterFactory, Logon logon) : this(new FastTransferUploadContext(logon.String8Encoding, logon.ResourceTracker, propertyFilterFactory, logon.Session.IsMoveUser), logon)
		{
			base.Context.PushInitial(fastTransferObject);
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x0001E190 File Offset: 0x0001C390
		public uint Progress
		{
			get
			{
				base.CheckDisposed();
				return 0U;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0001E199 File Offset: 0x0001C399
		public uint Steps
		{
			get
			{
				base.CheckDisposed();
				return 1U;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x0001E1A2 File Offset: 0x0001C3A2
		public bool IsMovingMailbox
		{
			get
			{
				base.CheckDisposed();
				return base.Context.IsMovingMailbox;
			}
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x0001E1B5 File Offset: 0x0001C3B5
		public void PutNextBuffer(ArraySegment<byte> buffer)
		{
			base.CheckDisposed();
			this.InternalPutNextBuffer(buffer);
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x0001E1C4 File Offset: 0x0001C3C4
		protected virtual void InternalPutNextBuffer(ArraySegment<byte> buffer)
		{
			base.Context.PutNextBuffer(buffer);
		}
	}
}
