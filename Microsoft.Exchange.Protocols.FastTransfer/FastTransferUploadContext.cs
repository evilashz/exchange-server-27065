using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.LogicalDataModel;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000005 RID: 5
	internal class FastTransferUploadContext : FastTransferContext
	{
		// Token: 0x06000019 RID: 25 RVA: 0x00002398 File Offset: 0x00000598
		public ErrorCode Configure(MapiLogon logon, Func<MapiContext, IFastTransferProcessor<FastTransferUploadContext>> getProcessorDelegate, Func<MapiContext, bool> flushDelegate, Folder folderForQuotaCheck)
		{
			ErrorCode errorCode = base.Configure(logon);
			if (errorCode == ErrorCode.NoError)
			{
				this.getProcessorDelegate = getProcessorDelegate;
				this.flushDelegate = flushDelegate;
			}
			this.folderForQuotaCheck = folderForQuotaCheck;
			return errorCode;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000023D1 File Offset: 0x000005D1
		public bool IsMovingMailbox
		{
			get
			{
				return base.Logon.IsMoveUser;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000023DE File Offset: 0x000005DE
		public FastTransferState State
		{
			get
			{
				if (this.context != null)
				{
					return this.context.State;
				}
				return FastTransferState.Error;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000023F5 File Offset: 0x000005F5
		public bool UploadStarted
		{
			get
			{
				return this.context != null;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002403 File Offset: 0x00000603
		public Folder FolderForQuotaCheck
		{
			get
			{
				return this.folderForQuotaCheck;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000240C File Offset: 0x0000060C
		public void PutNextBuffer(MapiContext operationContext, ArraySegment<byte> buffer)
		{
			base.ThrowIfNotValid(null);
			if (this.context == null)
			{
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					FastTransferUploadContext fastTransferUploadContext = disposeGuard.Add<FastTransferUploadContext>(new FastTransferUploadContext(CTSGlobals.AsciiEncoding, NullResourceTracker.Instance, PropertyFilterFactory.IncludeAllFactory, base.Logon.IsMoveUser));
					IFastTransferProcessor<FastTransferUploadContext> fastTransferObject = disposeGuard.Add<IFastTransferProcessor<FastTransferUploadContext>>(this.GetFastTransferProcessor(operationContext));
					fastTransferUploadContext.PushInitial(fastTransferObject);
					disposeGuard.Success();
					this.context = fastTransferUploadContext;
				}
			}
			this.context.PutNextBuffer(buffer);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000024A8 File Offset: 0x000006A8
		public void Flush(MapiContext operationContext)
		{
			if (this.flushDelegate != null)
			{
				this.flushDelegate(operationContext);
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000024BF File Offset: 0x000006BF
		protected virtual IFastTransferProcessor<FastTransferUploadContext> GetFastTransferProcessor(MapiContext operationContext)
		{
			return this.getProcessorDelegate(operationContext);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000024CD File Offset: 0x000006CD
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferUploadContext>(this);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024D5 File Offset: 0x000006D5
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.context != null)
			{
				this.context.Dispose();
				this.context = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x04000008 RID: 8
		private Func<MapiContext, IFastTransferProcessor<FastTransferUploadContext>> getProcessorDelegate;

		// Token: 0x04000009 RID: 9
		private Func<MapiContext, bool> flushDelegate;

		// Token: 0x0400000A RID: 10
		private FastTransferUploadContext context;

		// Token: 0x0400000B RID: 11
		private Folder folderForQuotaCheck;
	}
}
