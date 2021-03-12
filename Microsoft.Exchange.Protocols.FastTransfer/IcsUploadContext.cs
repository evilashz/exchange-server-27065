using System;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.FastTransfer;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.PropTags;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Protocols.FastTransfer
{
	// Token: 0x02000011 RID: 17
	internal abstract class IcsUploadContext : MapiBase
	{
		// Token: 0x060000C7 RID: 199 RVA: 0x0000622A File Offset: 0x0000442A
		public IcsUploadContext() : base(MapiObjectType.IcsUploadContext)
		{
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00006234 File Offset: 0x00004434
		public IcsState IcsState
		{
			get
			{
				return this.icsState;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000623C File Offset: 0x0000443C
		public static bool ValidChangeKey(byte[] changeKey)
		{
			return IcsUploadContext.ValidForeignOrInternalId(changeKey);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00006244 File Offset: 0x00004444
		public static bool ValidSourceKey(byte[] sourceKey)
		{
			return IcsUploadContext.ValidForeignOrInternalId(sourceKey);
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000624C File Offset: 0x0000444C
		public static bool ValidForeignOrInternalId(byte[] id)
		{
			return id != null && id.Length > 16 && id.Length < 256;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00006264 File Offset: 0x00004464
		public virtual ErrorCode Configure(MapiContext operationContext, MapiFolder folder)
		{
			base.Logon = folder.Logon;
			this.icsState = new IcsState();
			base.IsValid = true;
			return ErrorCode.NoError;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006289 File Offset: 0x00004489
		public ErrorCode BeginUploadStateProperty(StorePropTag propTag, uint size)
		{
			base.ThrowIfNotValid(null);
			return this.icsState.BeginUploadStateProperty(propTag, size);
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000629F File Offset: 0x0000449F
		public ErrorCode ContinueUploadStateProperty(ArraySegment<byte> data)
		{
			base.ThrowIfNotValid(null);
			return this.icsState.ContinueUploadStateProperty(data);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000062B4 File Offset: 0x000044B4
		public ErrorCode EndUploadStateProperty()
		{
			base.ThrowIfNotValid(null);
			return this.icsState.EndUploadStateProperty();
		}

		// Token: 0x060000D0 RID: 208
		public abstract ErrorCode ImportDelete(MapiContext operationContext, byte[][] sourceKeys);

		// Token: 0x060000D1 RID: 209 RVA: 0x000062C8 File Offset: 0x000044C8
		public ErrorCode OpenIcsStateDownloadContext(out FastTransferDownloadContext outputContext)
		{
			base.ThrowIfNotValid(null);
			this.icsState.ReloadIfNecessary();
			if (ExTraceGlobals.IcsUploadStateTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				StringBuilder stringBuilder = new StringBuilder(100);
				stringBuilder.Append("CheckpointState=[");
				stringBuilder.Append(this.icsState.ToString());
				stringBuilder.Append("]");
				ExTraceGlobals.IcsUploadStateTracer.TraceDebug(0L, stringBuilder.ToString());
			}
			return this.icsState.OpenIcsStateDownloadContext(base.Logon, out outputContext);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000634C File Offset: 0x0000454C
		protected void TraceInitialState(MapiContext operationContext)
		{
			if (!this.initialStateTraced)
			{
				this.initialStateTraced = true;
				this.icsState.ReloadIfNecessary();
				if (ExTraceGlobals.IcsUploadStateTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(100);
					stringBuilder.Append("InitialState=[");
					stringBuilder.Append(this.icsState.ToString());
					stringBuilder.Append("]");
					ExTraceGlobals.IcsUploadStateTracer.TraceDebug(0L, stringBuilder.ToString());
				}
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000063C4 File Offset: 0x000045C4
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<IcsUploadContext>(this);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000063CC File Offset: 0x000045CC
		protected override void InternalDispose(bool calledFromDispose)
		{
			if (calledFromDispose && this.icsState != null)
			{
				this.icsState.Dispose();
				this.icsState = null;
			}
			base.InternalDispose(calledFromDispose);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000063F4 File Offset: 0x000045F4
		protected ExchangeId MessageIdFromSourceKey(MapiContext operationContext, ExchangeId folderId, ref byte[] sourceKey)
		{
			if (IcsUploadContext.IsValid22ByteId(sourceKey))
			{
				byte[] bytes = sourceKey;
				sourceKey = null;
				ExchangeId result = ExchangeId.CreateFrom22ByteArray(operationContext, base.Logon.StoreMailbox.ReplidGuidMap, bytes);
				if (!result.IsValid || result.Counter != 0UL)
				{
					return result;
				}
			}
			throw new StoreException((LID)54328U, ErrorCodeValue.InvalidParameter);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006454 File Offset: 0x00004654
		protected ExchangeId FolderIdFromSourceKey(MapiContext operationContext, ref byte[] sourceKey)
		{
			if (IcsUploadContext.IsValid22ByteId(sourceKey))
			{
				byte[] bytes = sourceKey;
				sourceKey = null;
				return ExchangeId.CreateFrom22ByteArray(operationContext, base.Logon.StoreMailbox.ReplidGuidMap, bytes);
			}
			throw new StoreException((LID)42040U, ErrorCodeValue.InvalidParameter);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x0000649C File Offset: 0x0000469C
		private static bool IsValid22ByteId(byte[] sourceKey)
		{
			return sourceKey != null && sourceKey.Length == 22;
		}

		// Token: 0x04000066 RID: 102
		private IcsState icsState;

		// Token: 0x04000067 RID: 103
		private bool initialStateTraced;
	}
}
