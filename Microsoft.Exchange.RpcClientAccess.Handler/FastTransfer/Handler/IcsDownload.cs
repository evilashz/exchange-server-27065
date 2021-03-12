using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.Diagnostics;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x0200006B RID: 107
	internal class IcsDownload : FastTransferDownload, IServiceProvider<IcsStateHelper>, IIcsStateCheckpoint, WatsonHelper.IProvideWatsonReportData
	{
		// Token: 0x0600045F RID: 1119 RVA: 0x0001F7C4 File Offset: 0x0001D9C4
		public IcsDownload(ReferenceCount<CoreFolder> sourceFolder, IncrementalConfigOption configOptions, FastTransferSendOption sendOptions, Func<IcsDownload, IFastTransferProcessor<FastTransferDownloadContext>> initialFastTransferObjectFactory, Logon logon) : base(FastTransferDownloadContext.CreateForIcs(sendOptions, logon.String8Encoding, logon.ResourceTracker, PropertyFilterFactory.IncludeAllFactory, false), logon)
		{
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				disposeGuard.Add<IcsDownload>(this);
				this.initialFastTransferObjectFactory = initialFastTransferObjectFactory;
				this.icsStateHelper = new IcsStateHelper(sourceFolder);
				this.coreFolderReference = sourceFolder;
				this.coreFolderReference.AddRef();
				disposeGuard.Success();
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0001F854 File Offset: 0x0001DA54
		public ReferenceCount<CoreFolder> FolderReference
		{
			get
			{
				return this.coreFolderReference;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0001F85C File Offset: 0x0001DA5C
		public IcsState IcsState
		{
			get
			{
				base.CheckDisposed();
				return this.icsStateHelper.IcsState;
			}
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001F86F File Offset: 0x0001DA6F
		protected override int InternalGetNextBuffer(ArraySegment<byte> buffer)
		{
			this.EnsureContextInitialized();
			return base.InternalGetNextBuffer(buffer);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001F87E File Offset: 0x0001DA7E
		protected override void InternalDispose()
		{
			this.coreFolderReference.Release();
			Util.DisposeIfPresent(this.icsStateHelper);
			base.InternalDispose();
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001F89D File Offset: 0x0001DA9D
		IcsStateHelper IServiceProvider<IcsStateHelper>.Get()
		{
			base.CheckDisposed();
			return this.icsStateHelper;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001F8AB File Offset: 0x0001DAAB
		IFastTransferProcessor<FastTransferDownloadContext> IIcsStateCheckpoint.CreateIcsStateCheckpointFastTransferObject()
		{
			base.CheckDisposed();
			return this.icsStateHelper.CreateIcsStateFastTransferObject();
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0001F8BE File Offset: 0x0001DABE
		protected override WatsonReportActionType WatsonReportActionType
		{
			get
			{
				return WatsonReportActionType.IcsDownload;
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001F8C1 File Offset: 0x0001DAC1
		private void EnsureContextInitialized()
		{
			if (!this.isInitialFastTransferObjectCreated)
			{
				base.Context.PushInitial(this.initialFastTransferObjectFactory(this));
				this.isInitialFastTransferObjectCreated = true;
			}
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001F8E9 File Offset: 0x0001DAE9
		string WatsonHelper.IProvideWatsonReportData.GetWatsonReportString()
		{
			base.CheckDisposed();
			return string.Format("SyncRootFolder.DisplayName: {0}\r\nFastTransferContext: {1}", this.coreFolderReference.ReferencedObject.PropertyBag.GetValueOrDefault<string>(CoreFolderSchema.DisplayName, string.Empty), base.Context);
		}

		// Token: 0x04000185 RID: 389
		private readonly Func<IcsDownload, IFastTransferProcessor<FastTransferDownloadContext>> initialFastTransferObjectFactory;

		// Token: 0x04000186 RID: 390
		private readonly IcsStateHelper icsStateHelper;

		// Token: 0x04000187 RID: 391
		private readonly ReferenceCount<CoreFolder> coreFolderReference;

		// Token: 0x04000188 RID: 392
		private bool isInitialFastTransferObjectCreated;
	}
}
