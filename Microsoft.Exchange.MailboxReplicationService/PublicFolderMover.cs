using System;
using System.Collections.Generic;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200005A RID: 90
	internal class PublicFolderMover : MailboxCopierBase
	{
		// Token: 0x060004B0 RID: 1200 RVA: 0x0001CA80 File Offset: 0x0001AC80
		internal PublicFolderMover(TransactionalRequestJob moveRequestJob, BaseJob publicFolderMoveJob, List<byte[]> hierarchyFolderEntryIds, MailboxCopierFlags copierFlags, LocalizedString sourceTracingID, LocalizedString targetTracingID) : base(moveRequestJob.SourceExchangeGuid, moveRequestJob.TargetExchangeGuid, moveRequestJob, publicFolderMoveJob, copierFlags, sourceTracingID, targetTracingID)
		{
			MrsTracer.Service.Function("PublicFolderMover.Constructor", new object[0]);
			this.hierarchyFolderEntryIds = hierarchyFolderEntryIds;
			this.sessionSpecificEntryIds = new Dictionary<byte[], byte[]>(this.hierarchyFolderEntryIds.Count, ArrayComparer<byte>.Comparer);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001CADE File Offset: 0x0001ACDE
		public override void ConfigureProviders()
		{
			base.ConfigDestinationMailbox(this.destinationMailbox);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001CAEC File Offset: 0x0001ACEC
		public override void UnconfigureProviders()
		{
			base.SourceMailboxWrapper = null;
			base.UnconfigureProviders();
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001CBA8 File Offset: 0x0001ADA8
		public override FolderMap GetSourceFolderMap(GetFolderMapFlags flags)
		{
			base.SourceMailboxWrapper.LoadFolderMap(flags, delegate
			{
				List<FolderRecWrapper> list = new List<FolderRecWrapper>(this.hierarchyFolderEntryIds.Count);
				foreach (byte[] entryId in this.hierarchyFolderEntryIds)
				{
					byte[] sessionSpecificEntryId = base.SourceMailbox.GetSessionSpecificEntryId(entryId);
					using (ISourceFolder folder = base.SourceMailbox.GetFolder(sessionSpecificEntryId))
					{
						list.Add(new FolderRecWrapper(folder.GetFolderRec(null, GetFolderRecFlags.None)));
					}
				}
				return new PublicFolderMap(list);
			});
			return base.SourceMailboxWrapper.FolderMap;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001CC7C File Offset: 0x0001AE7C
		public override FolderMap GetDestinationFolderMap(GetFolderMapFlags flags)
		{
			base.DestMailboxWrapper.LoadFolderMap(flags, delegate
			{
				List<FolderRecWrapper> list = new List<FolderRecWrapper>(this.hierarchyFolderEntryIds.Count);
				foreach (byte[] entryId in this.hierarchyFolderEntryIds)
				{
					byte[] sessionSpecificEntryId = base.DestMailbox.GetSessionSpecificEntryId(entryId);
					using (IDestinationFolder folder = base.DestMailbox.GetFolder(sessionSpecificEntryId))
					{
						list.Add(new FolderRecWrapper(folder.GetFolderRec(null, GetFolderRecFlags.None)));
					}
				}
				return new PublicFolderMap(list);
			});
			return base.DestMailboxWrapper.FolderMap;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001CCA1 File Offset: 0x0001AEA1
		public override IEnumerator<FolderRecWrapper> GetSourceHierarchyEnumeratorForChangedFolders()
		{
			return this.GetSourceFolderMap(GetFolderMapFlags.None).GetFolderHierarchyEnumerator(EnumHierarchyFlags.AllFolders);
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001CCB0 File Offset: 0x0001AEB0
		public override byte[] GetSourceFolderEntryId(FolderRecWrapper destinationFolderRec)
		{
			return base.SourceMailbox.GetSessionSpecificEntryId(destinationFolderRec.EntryId);
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001CCC3 File Offset: 0x0001AEC3
		public override byte[] GetDestinationFolderEntryId(byte[] srcFolderEntryId)
		{
			return base.DestMailbox.GetSessionSpecificEntryId(srcFolderEntryId);
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001CCD4 File Offset: 0x0001AED4
		public override IFxProxyPool GetDestinationFxProxyPool(ICollection<byte[]> folderIds)
		{
			IFxProxyPool fxProxyPool = base.DestMailbox.GetFxProxyPool(folderIds);
			return new TranslatorPFProxy(base.SourceMailbox, base.DestMailbox, fxProxyPool);
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001CD00 File Offset: 0x0001AF00
		public override bool IsContentAvailableInTargetMailbox(FolderRecWrapper destinationFolderRec)
		{
			return this.sessionSpecificEntryIds.ContainsKey(destinationFolderRec.EntryId);
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001CD13 File Offset: 0x0001AF13
		protected override bool ShouldCompareParentIDs()
		{
			return false;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001CD16 File Offset: 0x0001AF16
		protected override EnumerateMessagesFlags GetAdditionalEnumerateMessagesFlagsForContentVerification()
		{
			return EnumerateMessagesFlags.ReturnLongTermIDs;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001CD1C File Offset: 0x0001AF1C
		public override void CopyFolderProperties(FolderRecWrapper sourceFolderRecWrapper, ISourceFolder sourceFolder, IDestinationFolder destFolder, FolderRecDataFlags dataToCopy, out bool wasPropertyCopyingSkipped)
		{
			wasPropertyCopyingSkipped = false;
			if (this.sessionSpecificEntryIds.ContainsKey(sourceFolderRecWrapper.EntryId) && destFolder != null)
			{
				if (base.SupportsPerUserReadUnreadDataTransfer)
				{
					using (IFxProxy fxProxy = destFolder.GetFxProxy(FastTransferFlags.PassThrough))
					{
						using (IFxProxy fxProxy2 = base.CreateFxProxyTransmissionPipeline(fxProxy))
						{
							sourceFolder.CopyTo(fxProxy2, CopyPropertiesFlags.CopyFolderPerUserData, Array<PropTag>.Empty);
						}
					}
				}
				base.CopyFolderProperties(sourceFolderRecWrapper, sourceFolder, destFolder, FolderRecDataFlags.Rules, out wasPropertyCopyingSkipped);
			}
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001CDAC File Offset: 0x0001AFAC
		public override SyncContext CreateSyncContext()
		{
			return new PublicFolderMoveSyncContext(base.SourceMailbox, this.GetSourceFolderMap(GetFolderMapFlags.ForceRefresh), base.DestMailbox, this.GetDestinationFolderMap(GetFolderMapFlags.ForceRefresh));
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001CDCD File Offset: 0x0001AFCD
		protected override byte[] GetMessageKey(MessageRec messageRec, MailboxWrapperFlags flags)
		{
			return (byte[])messageRec[PropTag.LTID];
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001CDDF File Offset: 0x0001AFDF
		internal void SetMailboxWrappers(SourceMailboxWrapper sourceMailboxWrapper, IDestinationMailbox destinationMailbox)
		{
			base.SourceMailboxWrapper = sourceMailboxWrapper;
			this.destinationMailbox = destinationMailbox;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001CDF0 File Offset: 0x0001AFF0
		internal void UpdateSourceDestinationFolderIds()
		{
			foreach (byte[] entryId in this.hierarchyFolderEntryIds)
			{
				byte[] sessionSpecificEntryId = base.SourceMailbox.GetSessionSpecificEntryId(entryId);
				byte[] sessionSpecificEntryId2 = base.DestMailbox.GetSessionSpecificEntryId(entryId);
				this.sessionSpecificEntryIds[sessionSpecificEntryId] = sessionSpecificEntryId2;
				this.sessionSpecificEntryIds[sessionSpecificEntryId2] = sessionSpecificEntryId;
			}
		}

		// Token: 0x040001F9 RID: 505
		private IDestinationMailbox destinationMailbox;

		// Token: 0x040001FA RID: 506
		private List<byte[]> hierarchyFolderEntryIds;

		// Token: 0x040001FB RID: 507
		private Dictionary<byte[], byte[]> sessionSpecificEntryIds;
	}
}
