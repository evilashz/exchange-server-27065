using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x0200006C RID: 108
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class IcsDownloadPassThru : ServerObject, IServiceProvider<IcsStateHelper>, IIcsStateCheckpoint
	{
		// Token: 0x06000469 RID: 1129 RVA: 0x0001F920 File Offset: 0x0001DB20
		public IcsDownloadPassThru(ReferenceCount<CoreFolder> sourceFolder, int maxFastTransferBlockSize, Func<IcsDownloadPassThru, SynchronizerProviderBase> initialSynchronizerFactory, Logon logon) : base(logon)
		{
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				disposeGuard.Add<IcsDownloadPassThru>(this);
				this.icsStateHelper = new IcsStateHelper(sourceFolder);
				this.coreFolderReference = sourceFolder;
				this.coreFolderReference.AddRef();
				this.initialSynchronizerFactory = initialSynchronizerFactory;
				this.maxFastTransferBlockSize = maxFastTransferBlockSize;
				this.residualCacheSize = 0;
				this.doneInCache = false;
				this.passThruState = IcsDownloadPassThru.IcsDownloadPassThruState.Initial;
				disposeGuard.Success();
			}
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001F9B0 File Offset: 0x0001DBB0
		public static SynchronizerConfigFlags GetSynchronizerConfigFlags(SyncFlag syncFlags, SyncExtraFlag extraFlags, FastTransferSendOption sendOptions)
		{
			SynchronizerConfigFlags synchronizerConfigFlags = SynchronizerConfigFlags.None;
			if ((ushort)(syncFlags & SyncFlag.Unicode) == 1)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.Unicode;
			}
			if ((ushort)(syncFlags & SyncFlag.NoDeletions) == 2)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.NoDeletions;
			}
			if ((ushort)(syncFlags & SyncFlag.NoSoftDeletions) == 4)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.NoSoftDeletions;
			}
			if ((ushort)(syncFlags & SyncFlag.ReadState) == 8)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.ReadState;
			}
			if ((ushort)(syncFlags & SyncFlag.Associated) == 16)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.Associated;
			}
			if ((ushort)(syncFlags & SyncFlag.Normal) == 32)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.Normal;
			}
			if ((ushort)(syncFlags & SyncFlag.NoConflicts) == 64)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.NoConflicts;
			}
			if ((ushort)(syncFlags & SyncFlag.OnlySpecifiedProps) == 128)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.OnlySpecifiedProps;
			}
			if ((ushort)(syncFlags & SyncFlag.NoForeignKeys) == 256)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.NoForeignKeys;
			}
			if ((ushort)(syncFlags & SyncFlag.CatchUp) == 1024)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.Catchup;
			}
			if ((ushort)(syncFlags & SyncFlag.BestBody) == 8192)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.BestBody;
			}
			if ((ushort)(syncFlags & SyncFlag.IgnoreSpecifiedOnAssociated) == 16384)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.IgnoreSpecifiedOnAssociated;
			}
			if ((ushort)(syncFlags & SyncFlag.ProgressMode) == 32768)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.ProgressMode;
			}
			if ((extraFlags & SyncExtraFlag.OrderByDeliveryTime) == SyncExtraFlag.OrderByDeliveryTime)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.OrderByDeliveryTime;
			}
			if ((extraFlags & SyncExtraFlag.ManifestMode) == SyncExtraFlag.ManifestMode)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.ManifestMode;
			}
			if ((extraFlags & SyncExtraFlag.CatchUpFull) == SyncExtraFlag.CatchUpFull)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.CatchupFull;
			}
			if ((byte)(sendOptions & FastTransferSendOption.UseCpId) == 2)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.UseCpId;
			}
			if ((byte)(sendOptions & FastTransferSendOption.ForceUnicode) == 8)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.ForceUnicode;
			}
			if ((byte)(sendOptions & FastTransferSendOption.RecoverMode) == 4)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.FXRecoverMode;
			}
			if ((byte)(sendOptions & FastTransferSendOption.Unicode) == 1)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.Unicode;
			}
			if ((byte)(sendOptions & FastTransferSendOption.PartialItem) == 16)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.PartialItem;
			}
			if ((byte)(sendOptions & FastTransferSendOption.SendPropErrors) == 32)
			{
				synchronizerConfigFlags |= SynchronizerConfigFlags.SendPropErrors;
			}
			return synchronizerConfigFlags;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0001FB28 File Offset: 0x0001DD28
		public ReferenceCount<CoreFolder> FolderReference
		{
			get
			{
				return this.coreFolderReference;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001FB30 File Offset: 0x0001DD30
		public IcsState IcsState
		{
			get
			{
				base.CheckDisposed();
				return this.icsStateHelper.IcsState;
			}
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001FB44 File Offset: 0x0001DD44
		public int GetNextBuffer(ArraySegment<byte> buffer, out uint steps, out uint progress, out FastTransferState state)
		{
			base.CheckDisposed();
			this.EnsureSynchronizerInitialized();
			if (this.passThruState == IcsDownloadPassThru.IcsDownloadPassThruState.Initial)
			{
				this.passThruState = IcsDownloadPassThru.IcsDownloadPassThruState.InProgress;
			}
			int num = 0;
			steps = 0U;
			progress = 0U;
			state = FastTransferState.Partial;
			while (this.maxFastTransferBlockSize <= buffer.Count - num)
			{
				byte[] array;
				FastTransferState fastTransferState;
				this.synchronizer.GetBuffer(out array, out steps, out progress, out fastTransferState, out this.residualCacheSize, out this.doneInCache);
				state = (FastTransferState)fastTransferState;
				if (state == FastTransferState.Done)
				{
					this.passThruState = IcsDownloadPassThru.IcsDownloadPassThruState.Done;
				}
				if (array.Length > 0)
				{
					Array.Copy(array, 0, buffer.Array, buffer.Offset + num, array.Length);
					num += array.Length;
				}
				if (state == FastTransferState.Error || state == FastTransferState.Done)
				{
					return num;
				}
			}
			if (num == 0)
			{
				throw new BufferTooSmallException();
			}
			return num;
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001FBF4 File Offset: 0x0001DDF4
		public bool IsAvailableInCache(int sizeNeeded)
		{
			bool flag = false;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<bool>(3425054013U, ref flag);
			return (this.doneInCache || sizeNeeded <= this.residualCacheSize || sizeNeeded - this.residualCacheSize < this.maxFastTransferBlockSize) && !flag;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001FC3B File Offset: 0x0001DE3B
		protected override void InternalDispose()
		{
			Util.DisposeIfPresent(this.synchronizer);
			this.coreFolderReference.Release();
			Util.DisposeIfPresent(this.icsStateHelper);
			base.InternalDispose();
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001FC65 File Offset: 0x0001DE65
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<IcsDownloadPassThru>(this);
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001FC6D File Offset: 0x0001DE6D
		IcsStateHelper IServiceProvider<IcsStateHelper>.Get()
		{
			base.CheckDisposed();
			return this.icsStateHelper;
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001FC7C File Offset: 0x0001DE7C
		IFastTransferProcessor<FastTransferDownloadContext> IIcsStateCheckpoint.CreateIcsStateCheckpointFastTransferObject()
		{
			base.CheckDisposed();
			if (this.passThruState == IcsDownloadPassThru.IcsDownloadPassThruState.InProgress)
			{
				return new FastTransferInjectFailure(new RopExecutionException("Cannot retrieve ICS state if haven't completely downloaded ICS stream", (ErrorCode)2147746067U));
			}
			if (this.passThruState == IcsDownloadPassThru.IcsDownloadPassThruState.Done)
			{
				ISession session = new SessionAdaptor(this.coreFolderReference.ReferencedObject.Session);
				IPropertyBag propertyBag = new MemoryPropertyBag(session);
				IcsStateStream icsStateStream = new IcsStateStream(propertyBag);
				StorageIcsState state = icsStateStream.ToXsoState();
				this.synchronizer.GetFinalState(ref state);
				icsStateStream.FromXsoState(state);
				this.IcsState.Load(IcsStateOrigin.ServerFinal, propertyBag);
				this.passThruState = IcsDownloadPassThru.IcsDownloadPassThruState.Final;
			}
			return this.icsStateHelper.CreateIcsStateFastTransferObject();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001FD14 File Offset: 0x0001DF14
		private void EnsureSynchronizerInitialized()
		{
			if (this.synchronizer == null)
			{
				this.synchronizer = this.initialSynchronizerFactory(this);
			}
		}

		// Token: 0x04000189 RID: 393
		private readonly IcsStateHelper icsStateHelper;

		// Token: 0x0400018A RID: 394
		private readonly ReferenceCount<CoreFolder> coreFolderReference;

		// Token: 0x0400018B RID: 395
		private readonly Func<IcsDownloadPassThru, SynchronizerProviderBase> initialSynchronizerFactory;

		// Token: 0x0400018C RID: 396
		private readonly int maxFastTransferBlockSize;

		// Token: 0x0400018D RID: 397
		private SynchronizerProviderBase synchronizer;

		// Token: 0x0400018E RID: 398
		private int residualCacheSize;

		// Token: 0x0400018F RID: 399
		private bool doneInCache;

		// Token: 0x04000190 RID: 400
		private IcsDownloadPassThru.IcsDownloadPassThruState passThruState;

		// Token: 0x0200006D RID: 109
		private enum IcsDownloadPassThruState
		{
			// Token: 0x04000192 RID: 402
			Initial,
			// Token: 0x04000193 RID: 403
			InProgress,
			// Token: 0x04000194 RID: 404
			Done,
			// Token: 0x04000195 RID: 405
			Final
		}
	}
}
