using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000004 RID: 4
	internal class FastTransferDownloadContext : FastTransferContext
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002226 File Offset: 0x00000426
		public FastTransferDownloadContext(ICollection<PropertyTag> excludedPropertyTags)
		{
			this.ExcludedPropertyTags = excludedPropertyTags;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002238 File Offset: 0x00000438
		public ErrorCode Configure(MapiLogon logon, FastTransferSendOption sendOptions, Func<MapiContext, IFastTransferProcessor<FastTransferDownloadContext>> getProcessorDelegate)
		{
			ErrorCode errorCode = base.Configure(logon);
			if (errorCode == ErrorCode.NoError)
			{
				this.sendOptions = sendOptions;
				this.getProcessorDelegate = getProcessorDelegate;
			}
			return errorCode;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002269 File Offset: 0x00000469
		public bool IsMovingMailbox
		{
			get
			{
				return base.Logon.IsMoveUser;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002276 File Offset: 0x00000476
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000228D File Offset: 0x0000048D
		public FastTransferSendOption SendOptions
		{
			get
			{
				return this.sendOptions;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002295 File Offset: 0x00000495
		public bool DownloadStarted
		{
			get
			{
				return this.context != null;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000022A4 File Offset: 0x000004A4
		public int GetNextBuffer(MapiContext operationContext, ArraySegment<byte> buffer)
		{
			base.ThrowIfNotValid(null);
			if (this.context == null)
			{
				using (DisposeGuard disposeGuard = default(DisposeGuard))
				{
					FastTransferDownloadContext fastTransferDownloadContext = disposeGuard.Add<FastTransferDownloadContext>(this.CreateFastTransferDownloadContext());
					IFastTransferProcessor<FastTransferDownloadContext> fastTransferObject = disposeGuard.Add<IFastTransferProcessor<FastTransferDownloadContext>>(this.GetFastTransferProcessor(operationContext));
					fastTransferDownloadContext.PushInitial(fastTransferObject);
					disposeGuard.Success();
					this.context = fastTransferDownloadContext;
				}
			}
			return this.context.GetNextBuffer(buffer);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002328 File Offset: 0x00000528
		public virtual IChunked PrepareIndexes(MapiContext operationContext)
		{
			return null;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000232B File Offset: 0x0000052B
		protected virtual FastTransferDownloadContext CreateFastTransferDownloadContext()
		{
			return FastTransferDownloadContext.CreateForDownload(this.sendOptions, 1U, CTSGlobals.AsciiEncoding, NullResourceTracker.Instance, new PropertyFilterFactory(this.ExcludedPropertyTags), base.Logon.IsMoveUser);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002359 File Offset: 0x00000559
		protected virtual IFastTransferProcessor<FastTransferDownloadContext> GetFastTransferProcessor(MapiContext operationContext)
		{
			return this.getProcessorDelegate(operationContext);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002367 File Offset: 0x00000567
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<FastTransferDownloadContext>(this);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000236F File Offset: 0x0000056F
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.context != null)
			{
				this.context.Dispose();
				this.context = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x04000004 RID: 4
		private FastTransferSendOption sendOptions;

		// Token: 0x04000005 RID: 5
		private Func<MapiContext, IFastTransferProcessor<FastTransferDownloadContext>> getProcessorDelegate;

		// Token: 0x04000006 RID: 6
		private FastTransferDownloadContext context;

		// Token: 0x04000007 RID: 7
		protected readonly ICollection<PropertyTag> ExcludedPropertyTags;
	}
}
