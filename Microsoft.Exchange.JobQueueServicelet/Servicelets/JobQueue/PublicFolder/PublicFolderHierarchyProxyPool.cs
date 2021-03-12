﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.PublicFolder;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServiceHost.PublicFolder;
using Microsoft.Exchange.MailboxReplicationService;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Servicelets.JobQueue.PublicFolder
{
	// Token: 0x0200000C RID: 12
	internal sealed class PublicFolderHierarchyProxyPool : DisposableWrapper<IFxProxyPool>, IFxProxyPool, IDisposable
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002C3C File Offset: 0x00000E3C
		public PublicFolderHierarchyProxyPool(PublicFolderSynchronizerContext syncContext, IHierarchySyncExecutor syncExecutor, IFxProxyPool destinationProxyPool, TransientExceptionHandler transientExceptionHandler) : base(destinationProxyPool, false)
		{
			ArgumentValidator.ThrowIfNull("syncContext", syncContext);
			ArgumentValidator.ThrowIfNull("syncExecutor", syncExecutor);
			ArgumentValidator.ThrowIfNull("destinationProxyPool", destinationProxyPool);
			ArgumentValidator.ThrowIfNull("transientExceptionHandler", transientExceptionHandler);
			this.syncContext = syncContext;
			this.syncExecutor = syncExecutor;
			this.destinationMailbox = syncContext.DestinationMailbox;
			this.transientExceptionHandler = transientExceptionHandler;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002CA0 File Offset: 0x00000EA0
		public static byte[] GetDumpsterEntryIdFromFolderRec(FolderRec folderRec)
		{
			ArgumentValidator.ThrowIfNull("folderRec", folderRec);
			if (folderRec.AdditionalProps != null)
			{
				foreach (PropValueData data in folderRec.AdditionalProps)
				{
					PropValue native = DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(data);
					if (!native.IsNull() && !native.IsError() && native.PropTag == PropTag.IpmWasteBasketEntryId)
					{
						return native.GetBytes();
					}
				}
			}
			return null;
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002D12 File Offset: 0x00000F12
		EntryIdMap<byte[]> IFxProxyPool.GetFolderData()
		{
			return base.WrappedObject.GetFolderData();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002D1F File Offset: 0x00000F1F
		void IFxProxyPool.Flush()
		{
			base.WrappedObject.Flush();
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002D4C File Offset: 0x00000F4C
		void IFxProxyPool.SetItemProperties(ItemPropertiesBase props)
		{
			PublicFolderHierarchyProxyPool.<>c__DisplayClass1 CS$<>8__locals1 = new PublicFolderHierarchyProxyPool.<>c__DisplayClass1();
			CS$<>8__locals1.props = props;
			CS$<>8__locals1.<>4__this = this;
			this.transientExceptionHandler.ExecuteWithRetry(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Microsoft.Exchange.MailboxReplicationService.IFxProxyPool.SetItemProperties>b__0)));
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002DC8 File Offset: 0x00000FC8
		IFolderProxy IFxProxyPool.GetFolderProxy(byte[] sourceFolderId)
		{
			PublicFolderHierarchyProxyPool.<>c__DisplayClass4 CS$<>8__locals1 = new PublicFolderHierarchyProxyPool.<>c__DisplayClass4();
			CS$<>8__locals1.sourceFolderId = sourceFolderId;
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.folderProxy = null;
			this.transientExceptionHandler.ExecuteWithRetry(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Microsoft.Exchange.MailboxReplicationService.IFxProxyPool.GetFolderProxy>b__3)));
			return CS$<>8__locals1.folderProxy;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E0D File Offset: 0x0000100D
		List<byte[]> IFxProxyPool.GetUploadedMessageIDs()
		{
			return base.WrappedObject.GetUploadedMessageIDs();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E3C File Offset: 0x0000103C
		IFolderProxy IFxProxyPool.CreateFolder(FolderRec folder)
		{
			PublicFolderHierarchyProxyPool.<>c__DisplayClass7 CS$<>8__locals1 = new PublicFolderHierarchyProxyPool.<>c__DisplayClass7();
			CS$<>8__locals1.folder = folder;
			CS$<>8__locals1.<>4__this = this;
			if (CS$<>8__locals1.folder.FolderType == FolderType.Search)
			{
				return null;
			}
			CS$<>8__locals1.folderProxy = null;
			this.transientExceptionHandler.ExecuteWithRetry(new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<Microsoft.Exchange.MailboxReplicationService.IFxProxyPool.CreateFolder>b__6)));
			return CS$<>8__locals1.folderProxy;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E94 File Offset: 0x00001094
		private IFolderProxy CreateFolder(FolderRec folder)
		{
			IFolderProxy result = null;
			bool flag;
			folder.EntryId = this.syncContext.MapSourceToDestinationFolderId(folder.EntryId, out flag);
			folder.ParentId = this.syncContext.MapSourceToDestinationFolderId(folder.ParentId, out flag);
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IDestinationFolder folder2 = this.destinationMailbox.GetFolder(folder.EntryId);
				if (folder2 != null)
				{
					disposeGuard.Add<IDestinationFolder>(folder2);
					folder2.SetProps(CommonUtils.PropertiesToDelete);
					FolderRec folderRec = folder2.GetFolderRec(null, GetFolderRecFlags.None);
					if (!CommonUtils.IsSameEntryId(folderRec.ParentId, folder.ParentId))
					{
						try
						{
							this.destinationMailbox.MoveFolder(folderRec.EntryId, folderRec.ParentId, folder.ParentId);
						}
						catch (ObjectNotFoundException arg)
						{
							PublicFolderHierarchyProxyPool.Tracer.TraceWarning<FolderRec, FolderRec, ObjectNotFoundException>((long)this.GetHashCode(), "ObjectNotFoundException caught during MoveFolder. Calling to FixParentChain before reattempting the folder move. SourceFolder={0}. DestinationFolder={1}. Exception={2}", folder, folderRec, arg);
							this.FixParentChain(folder);
							this.destinationMailbox.MoveFolder(folderRec.EntryId, folderRec.ParentId, folder.ParentId);
						}
					}
					result = base.WrappedObject.GetFolderProxy(folder.EntryId);
					PublicFolderHierarchyProxyPool.Tracer.TraceDebug<FolderRec>((long)this.GetHashCode(), "Folder updated: {0}", folder);
					this.syncContext.Logger.LogFolderUpdated(folder.EntryId);
				}
				else
				{
					try
					{
						result = base.WrappedObject.CreateFolder(folder);
					}
					catch (ObjectNotFoundException arg2)
					{
						PublicFolderHierarchyProxyPool.Tracer.TraceWarning<FolderRec, ObjectNotFoundException>((long)this.GetHashCode(), "ObjectNotFoundException caught during CreateFolder. Calling to FixParentChain before reattempting the folder creation. Folder={0}. Exception={1}", folder, arg2);
						this.FixParentChain(folder);
						result = base.WrappedObject.CreateFolder(folder);
					}
					folder2 = this.destinationMailbox.GetFolder(folder.EntryId);
					disposeGuard.Add<IDestinationFolder>(folder2);
					PublicFolderHierarchyProxyPool.Tracer.TraceDebug<FolderRec>((long)this.GetHashCode(), "Folder created: {0}", folder);
					this.syncContext.Logger.LogFolderCreated(folder.EntryId);
				}
				byte[] dumpsterEntryIdFromFolderRec = PublicFolderHierarchyProxyPool.GetDumpsterEntryIdFromFolderRec(folder);
				if (dumpsterEntryIdFromFolderRec != null)
				{
					bool flag2;
					byte[] value = this.syncContext.MapSourceToDestinationFolderId(dumpsterEntryIdFromFolderRec, out flag2);
					folder2.SetProps(new PropValueData[]
					{
						new PropValueData(PropTag.IpmWasteBasketEntryId, value)
					});
				}
			}
			return result;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000030F8 File Offset: 0x000012F8
		private void FixParentChain(FolderRec sourceFolder)
		{
			this.syncContext.Logger.LogEvent(LogEventType.Warning, string.Format(CultureInfo.InvariantCulture, "PublicFolderHierarchyProxyPool is calling EnsureDestinationFolderHasParentChain for folder with missing parent. Folder {0}.", new object[]
			{
				sourceFolder
			}));
			this.syncExecutor.EnsureDestinationFolderHasParentChain(sourceFolder);
		}

		// Token: 0x04000037 RID: 55
		private const int MaxRetriesOnTransientException = 5;

		// Token: 0x04000038 RID: 56
		private static readonly Trace Tracer = ExTraceGlobals.PublicFolderSynchronizerTracer;

		// Token: 0x04000039 RID: 57
		private static readonly TimeSpan RetryDelayOnTransientException = TimeSpan.FromSeconds(10.0);

		// Token: 0x0400003A RID: 58
		private readonly PublicFolderSynchronizerContext syncContext;

		// Token: 0x0400003B RID: 59
		private readonly IHierarchySyncExecutor syncExecutor;

		// Token: 0x0400003C RID: 60
		private readonly IDestinationMailbox destinationMailbox;

		// Token: 0x0400003D RID: 61
		private readonly TransientExceptionHandler transientExceptionHandler;
	}
}
