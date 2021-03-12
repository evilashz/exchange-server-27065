using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.FastTransfer;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000008 RID: 8
	internal abstract class IcsDownloadContext : FastTransferDownloadContext
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public IcsDownloadContext() : base(Array<PropertyTag>.Empty)
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002CD0 File Offset: 0x00000ED0
		public ErrorCode Configure(MapiLogon logon, FastTransferSendOption sendOptions)
		{
			ErrorCode errorCode = base.Configure(logon, sendOptions, null);
			if (errorCode == ErrorCode.NoError)
			{
				this.icsState = new IcsState();
			}
			return errorCode;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002D00 File Offset: 0x00000F00
		public IcsState IcsState
		{
			get
			{
				return this.icsState;
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D08 File Offset: 0x00000F08
		public ErrorCode BeginUploadStateProperty(StorePropTag propTag, uint size)
		{
			base.ThrowIfNotValid(null);
			if (base.DownloadStarted)
			{
				throw new ExExceptionNoSupport((LID)46136U, "Uploading a state after download was started.");
			}
			return this.icsState.BeginUploadStateProperty(propTag, size);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002D3B File Offset: 0x00000F3B
		public ErrorCode ContinueUploadStateProperty(ArraySegment<byte> data)
		{
			base.ThrowIfNotValid(null);
			return this.icsState.ContinueUploadStateProperty(data);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002D50 File Offset: 0x00000F50
		public ErrorCode EndUploadStateProperty()
		{
			base.ThrowIfNotValid(null);
			return this.icsState.EndUploadStateProperty();
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D64 File Offset: 0x00000F64
		public ErrorCode OpenIcsStateDownloadContext(out FastTransferDownloadContext outputContext)
		{
			base.ThrowIfNotValid(null);
			if (ExTraceGlobals.IcsDownloadStateTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("CheckpointState=[");
				stringBuilder.Append(this.icsState.ToString());
				stringBuilder.Append("]");
				ExTraceGlobals.IcsDownloadStateTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			return this.icsState.OpenIcsStateDownloadContext(base.Logon, out outputContext);
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002DDC File Offset: 0x00000FDC
		protected override FastTransferDownloadContext CreateFastTransferDownloadContext()
		{
			if (this.icsState.StateUploadStarted)
			{
				throw new ExExceptionNoSupport((LID)62520U, "Should properly end the state property upload.");
			}
			return FastTransferDownloadContext.CreateForIcs(base.SendOptions, CTSGlobals.AsciiEncoding, NullResourceTracker.Instance, new PropertyFilterFactory(this.ExcludedPropertyTags), base.Logon.IsMoveUser);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E36 File Offset: 0x00001036
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IcsDownloadContext>(this);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E3E File Offset: 0x0000103E
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.icsState != null)
			{
				this.icsState.Dispose();
				this.icsState = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x0400001C RID: 28
		private IcsState icsState;
	}
}
