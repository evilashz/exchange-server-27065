using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000034 RID: 52
	internal class MailboxMerger : MailboxCopierBase
	{
		// Token: 0x06000297 RID: 663 RVA: 0x000107F5 File Offset: 0x0000E9F5
		public MailboxMerger(Guid sourceMailboxGuid, Guid targetMailboxGuid, TransactionalRequestJob requestJob, BaseJob mrsJob, MailboxCopierFlags flags, LocalizedString sourceTracingID, LocalizedString targetTracingID) : base(sourceMailboxGuid, targetMailboxGuid, requestJob, mrsJob, flags, sourceTracingID, targetTracingID)
		{
		}

		// Token: 0x06000298 RID: 664 RVA: 0x00010808 File Offset: 0x0000EA08
		protected override IFxProxyPool GetFxProxyPoolTransmissionPipeline(EntryIdMap<byte[]> sourceMap)
		{
			IFxProxyPool result;
			using (DisposeGuard disposeGuard = default(DisposeGuard))
			{
				IFxProxyPool fxProxyPool = base.DestMailbox.GetFxProxyPool(sourceMap.Keys);
				disposeGuard.Add<IFxProxyPool>(fxProxyPool);
				IFxProxyPool fxProxyPool2 = new MailboxMerger.SimpleTranslatingProxyPool(fxProxyPool, sourceMap);
				disposeGuard.Success();
				disposeGuard.Add<IFxProxyPool>(fxProxyPool2);
				IFxProxyPool fxProxyPool3 = base.CreateFxProxyPoolTransmissionPipeline(fxProxyPool2);
				disposeGuard.Success();
				result = fxProxyPool3;
			}
			return result;
		}

		// Token: 0x06000299 RID: 665 RVA: 0x00010884 File Offset: 0x0000EA84
		public override void ConfigureProviders()
		{
			base.ConfigureProviders();
			RequestStatisticsBase cachedRequestJob = base.MRSJob.CachedRequestJob;
			LocalMailboxFlags localMailboxFlags = LocalMailboxFlags.None;
			LocalMailboxFlags localMailboxFlags2 = LocalMailboxFlags.None;
			if (cachedRequestJob.RequestType != MRSRequestType.MailboxRestore && cachedRequestJob.IgnoreRuleLimitErrors)
			{
				localMailboxFlags |= LocalMailboxFlags.StripLargeRulesForDownlevelTargets;
			}
			if (cachedRequestJob.OrganizationId == null)
			{
				OrganizationId forestWideOrgId = OrganizationId.ForestWideOrgId;
			}
			switch (cachedRequestJob.RequestType)
			{
			case MRSRequestType.Merge:
				if (cachedRequestJob.SourceIsLocal)
				{
					localMailboxFlags |= LocalMailboxFlags.UseHomeMDB;
				}
				else
				{
					localMailboxFlags |= LocalMailboxFlags.PureMAPI;
				}
				if (cachedRequestJob.TargetIsLocal)
				{
					localMailboxFlags2 |= LocalMailboxFlags.UseHomeMDB;
				}
				else
				{
					localMailboxFlags2 |= LocalMailboxFlags.PureMAPI;
				}
				break;
			case MRSRequestType.MailboxImport:
				localMailboxFlags2 |= LocalMailboxFlags.UseHomeMDB;
				break;
			case MRSRequestType.MailboxExport:
				localMailboxFlags |= LocalMailboxFlags.UseHomeMDB;
				break;
			case MRSRequestType.MailboxRestore:
				localMailboxFlags2 |= LocalMailboxFlags.UseHomeMDB;
				localMailboxFlags |= LocalMailboxFlags.Restore;
				break;
			case MRSRequestType.Sync:
				localMailboxFlags2 |= LocalMailboxFlags.UseHomeMDB;
				if (cachedRequestJob.Flags.HasFlag(RequestFlags.TargetIsAggregatedMailbox))
				{
					localMailboxFlags2 |= LocalMailboxFlags.AggregatedMailbox;
				}
				if (cachedRequestJob.SyncProtocol == SyncProtocol.Olc)
				{
					localMailboxFlags2 |= LocalMailboxFlags.OlcSync;
				}
				break;
			}
			ISourceMailbox sourceMailbox = null;
			IDestinationMailbox destinationMailbox = null;
			switch (cachedRequestJob.RequestType)
			{
			case MRSRequestType.Merge:
			case MRSRequestType.MailboxExport:
			case MRSRequestType.MailboxRestore:
			{
				Guid primaryMailboxGuid = base.SourceMailboxGuid;
				Guid guid = cachedRequestJob.SourceMDBGuid;
				if (cachedRequestJob.SourceUser != null)
				{
					primaryMailboxGuid = cachedRequestJob.SourceUser.ExchangeGuid;
					if (localMailboxFlags.HasFlag(LocalMailboxFlags.UseHomeMDB))
					{
						ADObjectId adobjectId = cachedRequestJob.SourceIsArchive ? cachedRequestJob.SourceUser.ArchiveDatabase : cachedRequestJob.SourceUser.Database;
						if (adobjectId != null)
						{
							guid = adobjectId.ObjectGuid;
						}
					}
				}
				sourceMailbox = this.GetSourceMailbox(new ADObjectId(guid, PartitionId.LocalForest.ForestFQDN), localMailboxFlags, null);
				sourceMailbox.Config(base.MRSJob.GetReservation(cachedRequestJob.SourceMDBGuid, ReservationFlags.Read), primaryMailboxGuid, base.SourceMailboxGuid, CommonUtils.GetPartitionHint(cachedRequestJob.OrganizationId), guid, MailboxType.SourceMailbox, null);
				if (cachedRequestJob.RequestType == MRSRequestType.MailboxRestore)
				{
					sourceMailbox.ConfigRestore((cachedRequestJob.MailboxRestoreFlags != null) ? cachedRequestJob.MailboxRestoreFlags.Value : MailboxRestoreType.None);
				}
				break;
			}
			case MRSRequestType.MailboxImport:
				sourceMailbox = this.GetSourceMailbox(new ADObjectId(Guid.Empty), localMailboxFlags, null);
				sourceMailbox.ConfigPst(cachedRequestJob.FilePath, cachedRequestJob.ContentCodePage);
				break;
			case MRSRequestType.Sync:
			{
				sourceMailbox = this.GetSourceMailbox(new ADObjectId(Guid.Empty), localMailboxFlags, null);
				SyncProtocol syncProtocol = cachedRequestJob.SyncProtocol;
				if (syncProtocol == SyncProtocol.Eas)
				{
					sourceMailbox.ConfigEas(cachedRequestJob.RemoteCredential, cachedRequestJob.EmailAddress, cachedRequestJob.TargetExchangeGuid, cachedRequestJob.SourceServer);
				}
				break;
			}
			}
			switch (cachedRequestJob.RequestType)
			{
			case MRSRequestType.Merge:
			case MRSRequestType.MailboxImport:
			case MRSRequestType.MailboxRestore:
			case MRSRequestType.Sync:
			{
				Guid primaryMailboxGuid2 = base.TargetMailboxGuid;
				Guid mdbGuid = cachedRequestJob.TargetMDBGuid;
				if (cachedRequestJob.TargetUser != null)
				{
					primaryMailboxGuid2 = cachedRequestJob.TargetUser.ExchangeGuid;
					ADObjectId adobjectId2 = cachedRequestJob.TargetIsArchive ? cachedRequestJob.TargetUser.ArchiveDatabase : cachedRequestJob.TargetUser.Database;
					if (adobjectId2 != null)
					{
						mdbGuid = adobjectId2.ObjectGuid;
					}
				}
				destinationMailbox = this.GetDestinationMailbox(mdbGuid, localMailboxFlags2, null);
				if (cachedRequestJob.RequestType == MRSRequestType.Sync && localMailboxFlags2.HasFlag(LocalMailboxFlags.AggregatedMailbox) && !string.IsNullOrEmpty(cachedRequestJob.DomainControllerToUpdate))
				{
					destinationMailbox.ConfigPreferredADConnection(cachedRequestJob.DomainControllerToUpdate);
				}
				destinationMailbox.Config(base.MRSJob.GetReservation(cachedRequestJob.TargetMDBGuid, ReservationFlags.Write), primaryMailboxGuid2, base.TargetMailboxGuid, CommonUtils.GetPartitionHint(cachedRequestJob.OrganizationId), mdbGuid, cachedRequestJob.TargetIsLocal ? MailboxType.DestMailboxIntraOrg : MailboxType.DestMailboxCrossOrg, null);
				if (cachedRequestJob.IsPublicFolderMailboxRestore && cachedRequestJob.RequestType == MRSRequestType.MailboxRestore)
				{
					MailboxRestoreType restoreFlags = (cachedRequestJob.MailboxRestoreFlags != null) ? (cachedRequestJob.MailboxRestoreFlags.Value & ~MailboxRestoreType.Recovery) : MailboxRestoreType.None;
					destinationMailbox.ConfigRestore(restoreFlags);
				}
				break;
			}
			case MRSRequestType.MailboxExport:
				destinationMailbox = this.GetDestinationMailbox(Guid.Empty, localMailboxFlags2, null);
				destinationMailbox.ConfigPst(cachedRequestJob.FilePath, null);
				destinationMailbox.Config(null, Guid.Empty, Guid.Empty, cachedRequestJob.PartitionHint, Guid.Empty, cachedRequestJob.TargetIsLocal ? MailboxType.DestMailboxIntraOrg : MailboxType.DestMailboxCrossOrg, null);
				break;
			}
			if (cachedRequestJob.RequestType == MRSRequestType.Merge && cachedRequestJob.RequestStyle == RequestStyle.CrossOrg)
			{
				bool credentialIsAdmin = cachedRequestJob.IsAdministrativeCredential == null || cachedRequestJob.IsAdministrativeCredential.Value;
				bool useNTLMAuth = cachedRequestJob.AuthenticationMethod != null && cachedRequestJob.AuthenticationMethod.Value == AuthenticationMethod.Ntlm;
				if (cachedRequestJob.Direction == RequestDirection.Pull)
				{
					((MapiSourceMailbox)sourceMailbox).ConfigRPCHTTP(cachedRequestJob.RemoteMailboxLegacyDN, cachedRequestJob.RemoteUserLegacyDN, cachedRequestJob.RemoteMailboxServerLegacyDN, cachedRequestJob.OutlookAnywhereHostName, cachedRequestJob.RemoteCredential, credentialIsAdmin, useNTLMAuth);
				}
				else
				{
					((MapiDestinationMailbox)destinationMailbox).ConfigRPCHTTP(cachedRequestJob.RemoteMailboxLegacyDN, cachedRequestJob.RemoteUserLegacyDN, cachedRequestJob.RemoteMailboxServerLegacyDN, cachedRequestJob.OutlookAnywhereHostName, cachedRequestJob.RemoteCredential, credentialIsAdmin, useNTLMAuth);
				}
			}
			base.Config(sourceMailbox, destinationMailbox);
			FolderHierarchyFlags folderHierarchyFlags = FolderHierarchyFlags.None;
			if (cachedRequestJob.IsPublicFolderMailboxRestore)
			{
				folderHierarchyFlags |= FolderHierarchyFlags.PublicFolderMailbox;
			}
			this.SourceHierarchy = new FolderHierarchy(folderHierarchyFlags, base.SourceMailboxWrapper);
			this.DestHierarchy = new FolderHierarchy(folderHierarchyFlags, base.DestMailboxWrapper);
			FolderIdTranslator folderIdTranslator = new FolderIdTranslator(this.SourceHierarchy, this.DestHierarchy);
			PrincipalTranslator principalTranslator;
			if (cachedRequestJob.RequestStyle == RequestStyle.CrossOrg)
			{
				principalTranslator = new PrincipalTranslator(base.SourceMailboxWrapper.PrincipalMapper, base.DestMailboxWrapper.PrincipalMapper);
			}
			else
			{
				principalTranslator = null;
			}
			base.ConfigTranslators(principalTranslator, folderIdTranslator);
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00010E13 File Offset: 0x0000F013
		// (set) Token: 0x0600029B RID: 667 RVA: 0x00010E1B File Offset: 0x0000F01B
		public FolderHierarchy SourceHierarchy { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00010E24 File Offset: 0x0000F024
		// (set) Token: 0x0600029D RID: 669 RVA: 0x00010E2C File Offset: 0x0000F02C
		public FolderHierarchy DestHierarchy { get; private set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00010E35 File Offset: 0x0000F035
		// (set) Token: 0x0600029F RID: 671 RVA: 0x00010E3D File Offset: 0x0000F03D
		public RestrictionData ContentRestriction { get; set; }

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00010E46 File Offset: 0x0000F046
		// (set) Token: 0x060002A1 RID: 673 RVA: 0x00010E5D File Offset: 0x0000F05D
		public ReplaySyncState ReplaySyncState
		{
			get
			{
				if (base.DestMailboxWrapper != null)
				{
					return base.DestMailboxWrapper.ReplaySyncState;
				}
				return null;
			}
			set
			{
				base.DestMailboxWrapper.ReplaySyncState = value;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00010E6B File Offset: 0x0000F06B
		public bool CanReplay
		{
			get
			{
				return base.SourceMailbox.IsMailboxCapabilitySupported(MailboxCapabilities.ReplayActions) && base.DestMailbox.IsMailboxCapabilitySupported(MailboxCapabilities.PagedGetActions);
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00010E91 File Offset: 0x0000F091
		public override void ClearCachedData()
		{
			base.ClearCachedData();
			if (this.SourceHierarchy != null)
			{
				this.SourceHierarchy.Clear();
			}
			if (this.DestHierarchy != null)
			{
				this.DestHierarchy.Clear();
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00010EBF File Offset: 0x0000F0BF
		public void SaveReplaySyncState()
		{
			base.DestMailboxWrapper.SaveReplaySyncState();
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00010ECC File Offset: 0x0000F0CC
		public override byte[] GetSourceFolderEntryId(FolderRecWrapper targetFolder)
		{
			FolderMapping folderMapping = (FolderMapping)targetFolder;
			if (folderMapping.SourceFolder != null)
			{
				return folderMapping.SourceFolder.EntryId;
			}
			return folderMapping.FolderRec[this.DestHierarchy.SourceEntryIDPtag] as byte[];
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00010F6C File Offset: 0x0000F16C
		public override byte[] GetDestinationFolderEntryId(byte[] sourceId)
		{
			FolderMapping result = (FolderMapping)this.SourceHierarchy[sourceId];
			if (result != null && result.TargetFolder != null)
			{
				return result.TargetFolder.EntryId;
			}
			result = null;
			this.DestHierarchy.EnumerateFolderHierarchy(EnumHierarchyFlags.AllFolders, delegate(FolderRecWrapper fRec, FolderMap.EnumFolderContext ctx)
			{
				FolderMapping folderMapping = (FolderMapping)fRec;
				byte[] eid = folderMapping.FolderRec[this.DestHierarchy.SourceEntryIDPtag] as byte[];
				if (CommonUtils.IsSameEntryId(sourceId, eid))
				{
					result = folderMapping;
					ctx.Result = EnumHierarchyResult.Cancel;
				}
			});
			if (result == null)
			{
				return null;
			}
			return result.EntryId;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00011004 File Offset: 0x0000F204
		public void CopyFolderData(FolderMapping fm, ISourceFolder srcFolder, IDestinationFolder destFolder)
		{
			MrsTracer.Service.Debug("Loading mappable folder properties", new object[0]);
			FolderRecDataFlags dataToCopy = FolderRecDataFlags.SearchCriteria;
			if (!base.MRSJob.CachedRequestJob.IsPublicFolderMailboxRestore)
			{
				bool flag;
				this.CopyFolderProperties(fm, srcFolder, destFolder, dataToCopy, out flag);
			}
			if (fm.FolderType != FolderType.Search)
			{
				this.MergeFolderContents(fm, srcFolder, destFolder);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00011058 File Offset: 0x0000F258
		public void CopyFolderPropertiesAndContents(FolderMapping folder, FolderContentsCrawler sourceFolderCrawler, IDestinationFolder destFolder, bool copyProperties, TimeSpan maxOperationDuration)
		{
			MrsTracer.Service.Function("CopyFolderPropertiesAndContents('{0}')", new object[]
			{
				folder.FullFolderName
			});
			if (copyProperties)
			{
				MrsTracer.Service.Debug("Loading mappable folder properties", new object[0]);
				FolderRecDataFlags dataToCopy = FolderRecDataFlags.SearchCriteria;
				if (!base.MRSJob.CachedRequestJob.IsPublicFolderMailboxRestore)
				{
					bool flag;
					this.CopyFolderProperties(folder, sourceFolderCrawler.WrappedObject, destFolder, dataToCopy, out flag);
				}
			}
			if (folder.FolderType != FolderType.Search)
			{
				this.MergeFolderContentsPaged(folder, sourceFolderCrawler, destFolder, maxOperationDuration);
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000110D7 File Offset: 0x0000F2D7
		public override FolderHierarchy GetSourceFolderHierarchy()
		{
			if (this.SourceHierarchy == null)
			{
				return base.GetSourceFolderHierarchy();
			}
			return this.SourceHierarchy;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x000110F0 File Offset: 0x0000F2F0
		public override void CopyFolderProperties(FolderRecWrapper folderRec, ISourceFolder sourceFolder, IDestinationFolder destFolder, FolderRecDataFlags dataToCopy, out bool wasPropertyCopyingSkipped)
		{
			base.CopyFolderProperties(folderRec, sourceFolder, destFolder, dataToCopy, out wasPropertyCopyingSkipped);
			MrsTracer.Service.Debug("Stamping additional properties on the target folder", new object[0]);
			List<PropValueData> list = new List<PropValueData>(3);
			list.Add(new PropValueData(this.DestHierarchy.SourceEntryIDPtag, folderRec.EntryId));
			list.Add(new PropValueData(this.DestHierarchy.SourceLastModifiedTimestampPtag, folderRec.FolderRec.LastModifyTimestamp));
			WellKnownFolderType wellKnownFolderType = WellKnownFolderType.None;
			if (this.SourceHierarchy != null)
			{
				wellKnownFolderType = this.SourceHierarchy.GetWellKnownFolderType(folderRec.EntryId);
			}
			if (wellKnownFolderType != WellKnownFolderType.None)
			{
				list.Add(new PropValueData(this.DestHierarchy.SourceWKFTypePtag, (int)wellKnownFolderType));
			}
			destFolder.SetProps(list.ToArray());
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000111B0 File Offset: 0x0000F3B0
		protected override PropTag[] GetAdditionalExcludedFolderPtags()
		{
			return MailboxCopierBase.MidsetDeletedPropTags;
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000111B8 File Offset: 0x0000F3B8
		public void MergeFolderContents(FolderMapping fm, ISourceFolder srcFolder, IDestinationFolder destFolder)
		{
			MrsTracer.Service.Function("MailboxMerger.MergeFolderContents({0})", new object[]
			{
				fm.FullFolderName
			});
			if (this.ContentRestriction != null)
			{
				srcFolder.SetContentsRestriction(this.ContentRestriction);
			}
			FolderContentsMapper folderContentsMapper = FolderContentsMapper.Create(fm, srcFolder, this.SourceHierarchy, destFolder, this.DestHierarchy, this.GetConflictResolutionOption(fm), base.MRSJob.CachedRequestJob.AssociatedMessagesCopyOption ?? FAICopyOption.DoNotCopy, (base.MRSJob.CachedRequestJob.SyncProtocol == SyncProtocol.Imap) ? FolderContentsMapperFlags.ImapSync : FolderContentsMapperFlags.None);
			int num;
			ulong num2;
			List<MessageRec> list;
			List<MessageRec> list2;
			folderContentsMapper.ComputeMapping((base.SyncState == null) ? null : base.SyncState.BadItems, out num, out num2, out list, out list2);
			SyncProtocol syncProtocol = base.MRSJob.CachedRequestJob.SyncProtocol;
			foreach (MessageRec messageRec in list2)
			{
				destFolder.SetMessageProps(messageRec.EntryId, messageRec.AdditionalProps);
			}
			base.MailboxSizeTracker.TrackFolder(fm.EntryId, list, num, num2);
			int num3;
			ulong num4;
			base.MailboxSizeTracker.GetFolderSize(fm.EntryId, out num3, out num4);
			base.Report.Append(MrsStrings.ReportFolderMergeStats(num3 - num, new ByteQuantifiedSize(num4 - num2).ToString(), num, new ByteQuantifiedSize(num2).ToString()));
			base.MRSJob.TotalMessages = base.MailboxSizeTracker.MessageCount;
			base.MRSJob.TotalMessageByteSize = base.MailboxSizeTracker.TotalMessageSize;
			base.MRSJob.MessagesWritten = base.MailboxSizeTracker.AlreadyCopiedCount;
			base.MRSJob.MessageSizeWritten = base.MailboxSizeTracker.AlreadyCopiedSize;
			this.CopyMessageBatch(folderContentsMapper, list, fm);
			base.MailboxSizeTracker.TrackFolder(fm.EntryId, null, num3, num4);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000113C0 File Offset: 0x0000F5C0
		public void CopyMailboxProperties()
		{
			MrsTracer.Service.Function("MailboxMerger.CopyMailboxProperties", new object[0]);
			PropValue[] native = DataConverter<PropValueConverter, PropValue, PropValueData>.GetNative(base.SourceMailbox.GetProps(MailboxMerger.MailboxPtags));
			List<PropValue> list = new List<PropValue>();
			foreach (PropValue item in native)
			{
				if (!item.IsNull() && !item.IsError())
				{
					if (item.PropTag == PropTag.SentMailEntryId)
					{
						byte[] array2 = item.Value as byte[];
						if (array2 != null)
						{
							FolderMapping folderMapping = this.SourceHierarchy[array2] as FolderMapping;
							if (folderMapping != null && folderMapping.IsIncluded && folderMapping.TargetFolder != null)
							{
								MrsTracer.Service.Debug("Remapped SentItemsEntryId from {0} to {1}", new object[]
								{
									TraceUtils.DumpEntryId(array2),
									TraceUtils.DumpEntryId(folderMapping.TargetFolder.EntryId)
								});
								list.Add(new PropValue(item.PropTag, folderMapping.TargetFolder.EntryId));
							}
						}
					}
					else
					{
						list.Add(item);
					}
				}
			}
			if (list.Count > 0)
			{
				base.DestMailbox.SetProps(DataConverter<PropValueConverter, PropValue, PropValueData>.GetData(list.ToArray()));
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0001150D File Offset: 0x0000F70D
		public override SyncContext CreateSyncContext()
		{
			return new MergeSyncContext(this);
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00011518 File Offset: 0x0000F718
		public override PropTag[] GetAdditionalFolderPtags()
		{
			if (base.MRSJob.CachedRequestJob.IsPublicFolderMailboxRestore)
			{
				List<PropTag> list = new List<PropTag>();
				list.Add(PropTag.LTID);
				list.Add(PropTag.IpmWasteBasketEntryId);
				list.Add(PropTag.TimeInServer);
				if (base.MRSJob.CachedRequestJob.IsLivePublicFolderMailboxRestore)
				{
					list.Add(PropTag.ReplicaList);
				}
				list.AddRange(base.GetAdditionalFolderPtags());
				return list.ToArray();
			}
			return base.GetAdditionalFolderPtags();
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00011594 File Offset: 0x0000F794
		public void MergeFolderContentsPaged(FolderMapping folder, FolderContentsCrawler sourceFolderCrawler, IDestinationFolder destFolder, TimeSpan maxOperationDuration)
		{
			MrsTracer.Service.Function("MailboxMerger.MergeFolderContentsPaged({0})", new object[]
			{
				folder.FullFolderName
			});
			ISourceFolder wrappedObject = sourceFolderCrawler.WrappedObject;
			if (this.ContentRestriction != null)
			{
				wrappedObject.SetContentsRestriction(this.ContentRestriction);
			}
			FolderContentsMapper folderContentsMapper = FolderContentsMapper.Create(folder, wrappedObject, this.SourceHierarchy, destFolder, this.DestHierarchy, this.GetConflictResolutionOption(folder), base.MRSJob.CachedRequestJob.AssociatedMessagesCopyOption ?? FAICopyOption.DoNotCopy, (base.MRSJob.CachedRequestJob.SyncProtocol == SyncProtocol.Imap) ? FolderContentsMapperFlags.ImapSync : FolderContentsMapperFlags.None);
			DateTime utcNow = DateTime.UtcNow;
			int alreadyCopiedCount;
			ulong alreadyCopiedSize;
			List<MessageRec> itemsToCopy;
			List<MessageRec> list;
			while (folderContentsMapper.ComputeMappingPaged(sourceFolderCrawler, (base.SyncState == null) ? null : base.SyncState.BadItems, out alreadyCopiedCount, out alreadyCopiedSize, out itemsToCopy, out list))
			{
				SyncProtocol syncProtocol = base.MRSJob.CachedRequestJob.SyncProtocol;
				foreach (MessageRec messageRec in list)
				{
					destFolder.SetMessageProps(messageRec.EntryId, messageRec.AdditionalProps);
				}
				base.MailboxSizeTracker.TrackFolder(folder.EntryId, sourceFolderCrawler.TotalMessageCount, alreadyCopiedCount, alreadyCopiedSize);
				base.MRSJob.MessagesWritten += base.MailboxSizeTracker.AlreadyCopiedCount;
				base.MRSJob.MessageSizeWritten += base.MailboxSizeTracker.AlreadyCopiedSize;
				base.MRSJob.TotalMessages = base.MailboxSizeTracker.MessageCount;
				base.MRSJob.TotalMessageByteSize = base.MailboxSizeTracker.TotalMessageSize;
				this.CopyMessageBatch(folderContentsMapper, itemsToCopy, folder);
				DateTime utcNow2 = DateTime.UtcNow;
				if (utcNow2 - utcNow >= maxOperationDuration)
				{
					MrsTracer.Service.Debug("MergeFolderContentsPaged times out for assigned duration {0}. Start:{1}, End:{2}", new object[]
					{
						maxOperationDuration,
						utcNow,
						utcNow2
					});
					return;
				}
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000117AC File Offset: 0x0000F9AC
		public override void ApplyContentsChanges(SyncContext ctx, MailboxChanges changes)
		{
			MrsTracer.Service.Function("MailboxMerger.ApplyContentsChanges", new object[0]);
			if (changes.HasFolderRecoverySync)
			{
				return;
			}
			foreach (FolderChangesManifest folderChangesManifest in changes.FolderChanges.Values)
			{
				FolderMapping folderMapping = (FolderMapping)this.SourceHierarchy[folderChangesManifest.FolderId];
				if (folderMapping == null)
				{
					MrsTracer.Service.Warning("Folder {0} is not discovered in source, will not apply changes", new object[]
					{
						TraceUtils.DumpEntryId(folderChangesManifest.FolderId)
					});
					return;
				}
				using (ISourceFolder folder = base.SourceMailbox.GetFolder(folderMapping.EntryId))
				{
					if (folder == null)
					{
						MrsTracer.Service.Warning("Folder {0} disappeared from source, will not apply changes", new object[]
						{
							TraceUtils.DumpEntryId(folderChangesManifest.FolderId)
						});
						return;
					}
					byte[] array = base.FolderIdTranslator.TranslateFolderId(folderChangesManifest.FolderId);
					if (array == null)
					{
						MrsTracer.Service.Warning("Source folder {0} is not present in target mailbox, will not apply changes", new object[]
						{
							TraceUtils.DumpEntryId(folderChangesManifest.FolderId)
						});
						return;
					}
					using (IDestinationFolder folder2 = base.DestMailbox.GetFolder(array))
					{
						if (folder2 == null)
						{
							MrsTracer.Service.Warning("Destination folder {0} disappeared, will not apply changes", new object[]
							{
								TraceUtils.DumpEntryId(array)
							});
							return;
						}
						this.ApplyFolderChanges(ctx, folderChangesManifest, folderMapping, folder, folder2);
					}
				}
			}
			base.ReportContentChangesSynced(ctx);
			base.ICSSyncState.ProviderState = base.SourceMailbox.GetMailboxSyncState();
			base.SaveICSSyncState(false);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000119AC File Offset: 0x0000FBAC
		public ReplayActionsQueue GetAndTranslateActions(string replaySyncState, int maxNumberOfActions, MergeSyncContext syncContext, out string retrieveSyncState, out bool moreActions)
		{
			MrsTracer.Service.Function("MailboxMerger.GetAndTranslateActions", new object[0]);
			List<ReplayAction> actions = base.DestMailbox.GetActions(replaySyncState, maxNumberOfActions);
			moreActions = (actions.Count == maxNumberOfActions);
			retrieveSyncState = ((actions.Count == 0) ? replaySyncState : actions[actions.Count - 1].Watermark);
			ReplayActionsQueue replayActionsQueue = new ReplayActionsQueue(actions.Count);
			foreach (ReplayAction action in actions)
			{
				replayActionsQueue.Enqueue(action);
			}
			EntryIdMap<List<byte[]>> entryIdMap = new EntryIdMap<List<byte[]>>();
			foreach (ReplayAction replayAction in actions)
			{
				if (!replayAction.Ignored)
				{
					foreach (KeyValuePair<byte[], byte[]> keyValuePair in replayAction.GetMessageEntryIdsToTranslate())
					{
						byte[] key = keyValuePair.Key;
						byte[] value = keyValuePair.Value;
						List<byte[]> list;
						if (!entryIdMap.TryGetValue(key, out list))
						{
							list = new List<byte[]>();
							entryIdMap.Add(key, list);
						}
						list.Add(value);
					}
				}
			}
			syncContext.PrefetchSourceMessageIdsFromTargetMessageIds(entryIdMap);
			foreach (ReplayAction replayAction2 in actions)
			{
				if (!replayAction2.Ignored)
				{
					replayAction2.TranslateEntryIds(syncContext);
				}
			}
			return replayActionsQueue;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00011B68 File Offset: 0x0000FD68
		public void ReplayActions(ReplayActionsQueue actionsQueue, MergeSyncContext syncContext)
		{
			MrsTracer.Service.Function("MailboxMerger.ReplayActions", new object[0]);
			ReplayAction replayAction;
			while (actionsQueue.TryQueue(out replayAction))
			{
				if (replayAction.Ignored)
				{
					syncContext.NumberOfActionsIgnored++;
					base.Report.AppendDebug(string.Format("Ignored action: {0}", replayAction));
					syncContext.LastActionProcessed = replayAction;
				}
				else
				{
					List<ReplayActionResult> list = null;
					try
					{
						List<ReplayAction> list2 = new List<ReplayAction>(1);
						list2.Add(replayAction);
						list = base.SourceMailbox.ReplayActions(list2);
						syncContext.NumberOfActionsReplayed++;
						base.Report.AppendDebug(string.Format("Replayed action: {0}", replayAction));
					}
					catch (MailboxReplicationPermanentException ex)
					{
						MrsTracer.Service.Error("Replay the action {0} failed: {1}", new object[]
						{
							replayAction,
							ex
						});
						syncContext.NumberOfActionsIgnored++;
						base.Report.AppendDebug(string.Format("Permanently failed action: {0}", replayAction));
					}
					syncContext.LastActionProcessed = replayAction;
					if (list != null)
					{
						this.PostReplay(replayAction, list[0]);
					}
				}
			}
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00011C8C File Offset: 0x0000FE8C
		protected override bool ShouldCompareParentIDs()
		{
			return false;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00011C90 File Offset: 0x0000FE90
		protected override PropTag[] GetEnumerateMessagesPropsForContentVerification(MailboxWrapperFlags flags)
		{
			if (flags == MailboxWrapperFlags.Target)
			{
				return new PropTag[]
				{
					this.DestHierarchy.SourceEntryIDPtag
				};
			}
			return null;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00011CB9 File Offset: 0x0000FEB9
		protected override RestrictionData GetContentRestriction()
		{
			return this.ContentRestriction;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00011CC1 File Offset: 0x0000FEC1
		protected override byte[] GetMessageKey(MessageRec messageRec, MailboxWrapperFlags flags)
		{
			if (flags == MailboxWrapperFlags.Target)
			{
				return (messageRec[this.DestHierarchy.SourceEntryIDPtag] as byte[]) ?? messageRec.EntryId;
			}
			return messageRec.EntryId;
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00011CF0 File Offset: 0x0000FEF0
		protected override bool IsIgnorableItem(MessageRec msg)
		{
			if (base.IsIgnorableItem(msg))
			{
				return true;
			}
			if (msg.CreationTimestamp > ((MergeJob)base.MRSJob).MailboxSnapshotTimestamp)
			{
				return true;
			}
			if (msg.IsFAI && base.MRSJob.CachedRequestJob.AssociatedMessagesCopyOption != FAICopyOption.Copy)
			{
				MrsTracer.Service.Debug("Ignoring FAI item because FAICopyOption!=Copy", new object[0]);
				return true;
			}
			string text;
			if (FolderContentsMapper.ShouldItemBeIgnored(msg, (base.SyncState == null) ? null : base.SyncState.BadItems, base.MRSJob.CachedRequestJob.AssociatedMessagesCopyOption ?? FAICopyOption.DoNotCopy, out text))
			{
				return true;
			}
			this.EnsureDumpsterContentsIsLoaded();
			if (this.targetDeletedContents.Contains(msg.EntryId))
			{
				MrsTracer.Service.Debug("Item is deleted, ignoring.", new object[0]);
				return true;
			}
			return false;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00011DE8 File Offset: 0x0000FFE8
		private void ApplyFolderChanges(SyncContext ctx, FolderChangesManifest folderChanges, FolderMapping fm, ISourceFolder srcFolder, IDestinationFolder destFolder)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			int num4 = 0;
			int num5 = 0;
			FolderContentsMapper folderContentsMapper = FolderContentsMapper.Create(fm, srcFolder, this.SourceHierarchy, destFolder, this.DestHierarchy, base.MRSJob.CachedRequestJob.ConflictResolutionOption ?? ConflictResolutionOption.KeepSourceItem, base.MRSJob.CachedRequestJob.AssociatedMessagesCopyOption ?? FAICopyOption.DoNotCopy, (base.MRSJob.CachedRequestJob.SyncProtocol == SyncProtocol.Imap) ? FolderContentsMapperFlags.ImapSync : FolderContentsMapperFlags.None);
			List<MessageRec> list;
			byte[][] array;
			byte[][] array2;
			byte[][] array3;
			int skipped;
			folderContentsMapper.ComputeIncrementalMapping(folderChanges, (base.SyncState == null) ? null : base.SyncState.BadItems, out list, out array, out array2, out array3, out skipped);
			this.CopyMessageBatch(folderContentsMapper, list, fm);
			destFolder.DeleteMessages(array);
			destFolder.SetReadFlagsOnMessages(SetReadFlags.None, array2);
			destFolder.SetReadFlagsOnMessages(SetReadFlags.ClearRead, array3);
			if (list != null)
			{
				foreach (MessageRec messageRec in list)
				{
					if (messageRec.IsNew)
					{
						num++;
					}
					else
					{
						num2++;
					}
				}
			}
			if (array != null)
			{
				num3 += array.Length;
			}
			if (array2 != null)
			{
				num4 += array2.Length;
			}
			if (array3 != null)
			{
				num5 += array3.Length;
			}
			ctx.CopyMessagesCount += new CopyMessagesCount(num, num2, num3, num4, num5, skipped);
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00011FA8 File Offset: 0x000101A8
		private void CopyMessageBatch(FolderContentsMapper mapper, List<MessageRec> itemsToCopy, FolderMapping fm)
		{
			if (itemsToCopy == null || itemsToCopy.Count == 0)
			{
				return;
			}
			MrsTracer.Service.Debug("Sorting {0} messages in folder '{1}'", new object[]
			{
				itemsToCopy.Count,
				fm.FullFolderName
			});
			MessageRecSorter messageRecSorter = new MessageRecSorter();
			Queue<List<MessageRec>> queue = messageRecSorter.Sort(itemsToCopy, (base.MRSJob.CachedRequestJob.SyncProtocol == SyncProtocol.Imap) ? MessageRecSortBy.SkipSort : MessageRecSortBy.DescendingTimeStamp);
			byte[][] folderIDs = new byte[][]
			{
				fm.TargetFolder.EntryId
			};
			while (queue.Count > 0)
			{
				base.MRSJob.CheckServersHealth();
				List<MessageRec> list = queue.Dequeue();
				ulong num = 0UL;
				foreach (MessageRec messageRec in list)
				{
					num += (ulong)((long)messageRec.MessageSize);
				}
				MrsTracer.Service.Debug("Copying a batch of {0} messages, {1}", new object[]
				{
					list.Count,
					new ByteQuantifiedSize(num)
				});
				List<BadMessageRec> list2 = new List<BadMessageRec>();
				MapiUtils.ExportMessagesWithBadItemDetection(base.SourceMailbox, list, delegate
				{
					IFxProxyPool fxProxyPool = this.DestMailbox.GetFxProxyPool(folderIDs);
					IFxProxyPool fxProxyTransformer = mapper.GetFxProxyTransformer(fxProxyPool);
					return this.CreateFxProxyPoolTransmissionPipeline(fxProxyTransformer);
				}, ExportMessagesFlags.OneByOne, ((base.Flags & MailboxCopierFlags.TargetIsPST) != MailboxCopierFlags.None) ? MailboxCopierBase.PSTIncludeMessagePtags : null, null, base.MRSJob.TestIntegration, ref list2);
				if (list2 != null && list2.Count > 0)
				{
					List<BadMessageRec> list3 = new List<BadMessageRec>();
					foreach (BadMessageRec badMessageRec in list2)
					{
						if (badMessageRec.Kind == BadItemKind.MissingItem)
						{
							MrsTracer.Service.Warning("Message {0} is missing in source, skipping.", new object[]
							{
								TraceUtils.DumpEntryId(badMessageRec.EntryId)
							});
						}
						else
						{
							list3.Add(badMessageRec);
						}
					}
					this.ReportBadItems(list3);
				}
				if (base.MRSJob.TestIntegration.LogContentDetails)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendLine(string.Format("CopyMessageBatch: {0} items copied", list.Count));
					foreach (MessageRec messageRec2 in list)
					{
						stringBuilder.AppendLine(string.Format("ItemID {0}, FolderID {1}{2}", TraceUtils.DumpEntryId(messageRec2.EntryId), TraceUtils.DumpEntryId(messageRec2.FolderId), messageRec2.IsFAI ? ", FAI" : string.Empty));
					}
					base.MRSJob.Report.AppendDebug(stringBuilder.ToString());
				}
				base.MRSJob.MessagesWritten += list.Count;
				base.MRSJob.MessageSizeWritten += num;
				base.MRSJob.ProgressTracker.AddItems((uint)list.Count);
				SaveStateFlags saveStateFlags = SaveStateFlags.Lazy;
				if (num > 0UL)
				{
					base.UpdateTimestampWhenPersistentProgressWasMade();
					saveStateFlags |= SaveStateFlags.RelinquishLongRunningJob;
				}
				base.MRSJob.SaveState(saveStateFlags, null);
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00012300 File Offset: 0x00010500
		private ConflictResolutionOption GetConflictResolutionOption(FolderMapping fm)
		{
			WellKnownFolderType wkftype = fm.WKFType;
			switch (wkftype)
			{
			case WellKnownFolderType.DumpsterDeletions:
			case WellKnownFolderType.DumpsterVersions:
			case WellKnownFolderType.DumpsterPurges:
				break;
			default:
				switch (wkftype)
				{
				case WellKnownFolderType.DumpsterAdminAuditLogs:
				case WellKnownFolderType.DumpsterAudits:
					break;
				default:
				{
					ConflictResolutionOption? conflictResolutionOption = base.MRSJob.CachedRequestJob.ConflictResolutionOption;
					if (conflictResolutionOption == null)
					{
						return ConflictResolutionOption.KeepSourceItem;
					}
					return conflictResolutionOption.GetValueOrDefault();
				}
				}
				break;
			}
			return ConflictResolutionOption.KeepAll;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00012364 File Offset: 0x00010564
		private void EnsureDumpsterContentsIsLoaded()
		{
			if (this.targetDeletedContents != null)
			{
				return;
			}
			HashSet<byte[]> hashset = new HashSet<byte[]>(ArrayComparer<byte>.EqualityComparer);
			this.HashFolderMessages(WellKnownFolderType.DeletedItems, hashset);
			this.HashFolderMessages(WellKnownFolderType.DumpsterDeletions, hashset);
			this.targetDeletedContents = hashset;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x000123A0 File Offset: 0x000105A0
		private void HashFolderMessages(WellKnownFolderType wkfType, HashSet<byte[]> hashset)
		{
			MrsTracer.Service.Debug("Enumerating target {0} folder to check for deleted messages", new object[]
			{
				wkfType
			});
			FolderMapping wellKnownFolder = this.DestHierarchy.GetWellKnownFolder(wkfType);
			if (wellKnownFolder != null)
			{
				using (IDestinationFolder folder = base.DestMailbox.GetFolder(wellKnownFolder.EntryId))
				{
					if (folder != null)
					{
						List<MessageRec> list = folder.EnumerateMessages(EnumerateMessagesFlags.RegularMessages, this.GetEnumerateMessagesPropsForContentVerification(MailboxWrapperFlags.Target));
						foreach (MessageRec messageRec in list)
						{
							byte[] array = messageRec[this.DestHierarchy.SourceEntryIDPtag] as byte[];
							if (array != null && !hashset.Contains(array))
							{
								hashset.Add(array);
							}
						}
					}
				}
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0001248C File Offset: 0x0001068C
		private void PostReplay(ReplayAction action, ReplayActionResult result)
		{
			byte[] originalFolderId = action.OriginalFolderId;
			byte[] originalItemId = action.OriginalItemId;
			ActionId id = action.Id;
			switch (id)
			{
			case ActionId.Move:
			{
				MoveActionResult moveActionResult = (MoveActionResult)result;
				MrsTracer.Service.Debug("Result of move action: {0}", new object[]
				{
					moveActionResult
				});
				if (ArrayComparer<byte>.EqualityComparer.Equals(moveActionResult.ItemId, moveActionResult.PreviousItemId))
				{
					return;
				}
				if (moveActionResult.MoveAsDelete)
				{
					MrsTracer.Service.Debug("Move action {0} resulted in deletion of an item in source. removing from target as well.", new object[]
					{
						action
					});
					this.DeleteItem(originalFolderId, originalItemId);
					return;
				}
				MrsTracer.Service.Debug("Stamping new source message id after move action: {0}", new object[]
				{
					action
				});
				this.UpdateSourceId(originalFolderId, originalItemId, moveActionResult.ItemId);
				return;
			}
			case ActionId.Send:
				MrsTracer.Service.Debug("Deleting the message after send action: {0}", new object[]
				{
					action
				});
				try
				{
					this.DeleteItem(originalFolderId, originalItemId);
					return;
				}
				finally
				{
					this.ReplaySyncState.ProviderState = action.Watermark;
					this.SaveReplaySyncState();
				}
				break;
			default:
				if (id != ActionId.CreateCalendarEvent)
				{
					return;
				}
				break;
			}
			CreateCalendarEventActionResult createCalendarEventActionResult = (CreateCalendarEventActionResult)result;
			MrsTracer.Service.Debug("Result of create calendar event: {0}", new object[]
			{
				createCalendarEventActionResult
			});
			MrsTracer.Service.Debug("Stamping source message id after create calendar event action: {0}", new object[]
			{
				action
			});
			this.UpdateSourceId(originalFolderId, originalItemId, createCalendarEventActionResult.SourceItemId);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00012608 File Offset: 0x00010808
		private void DeleteItem(byte[] destinationFolderId, byte[] destinationMessageId)
		{
			using (IDestinationFolder folder = base.DestMailbox.GetFolder(destinationFolderId))
			{
				if (folder == null)
				{
					MrsTracer.Service.Warning("Destination folder {0} disappeared", new object[]
					{
						TraceUtils.DumpEntryId(destinationFolderId)
					});
				}
				else
				{
					folder.DeleteMessages(new byte[][]
					{
						destinationMessageId
					});
				}
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00012678 File Offset: 0x00010878
		private void UpdateSourceId(byte[] destinationFolderId, byte[] destinationMessageId, byte[] sourceId)
		{
			using (IDestinationFolder folder = base.DestMailbox.GetFolder(destinationFolderId))
			{
				if (folder == null)
				{
					MrsTracer.Service.Warning("Destination folder {0} disappeared", new object[]
					{
						TraceUtils.DumpEntryId(destinationFolderId)
					});
				}
				else
				{
					folder.SetMessageProps(destinationMessageId, new PropValueData[]
					{
						new PropValueData(this.DestHierarchy.SourceEntryIDPtag, sourceId)
					});
				}
			}
		}

		// Token: 0x04000121 RID: 289
		public static readonly PropTag[] MailboxPtags = new PropTag[]
		{
			PropTag.SentMailEntryId,
			PropTag.OofState,
			PropTag.DeleteAfterSubmit
		};

		// Token: 0x04000122 RID: 290
		private HashSet<byte[]> targetDeletedContents;

		// Token: 0x02000035 RID: 53
		private class SimpleTranslatingProxyPool : DisposableWrapper<IFxProxyPool>, IFxProxyPool, IDisposable
		{
			// Token: 0x060002C2 RID: 706 RVA: 0x0001272A File Offset: 0x0001092A
			public SimpleTranslatingProxyPool(IFxProxyPool destinationProxyPool, EntryIdMap<byte[]> sourceMap) : base(destinationProxyPool, true)
			{
				this.sourceMap = sourceMap;
			}

			// Token: 0x060002C3 RID: 707 RVA: 0x0001273B File Offset: 0x0001093B
			EntryIdMap<byte[]> IFxProxyPool.GetFolderData()
			{
				return this.sourceMap;
			}

			// Token: 0x060002C4 RID: 708 RVA: 0x00012743 File Offset: 0x00010943
			void IFxProxyPool.Flush()
			{
				base.WrappedObject.Flush();
			}

			// Token: 0x060002C5 RID: 709 RVA: 0x00012750 File Offset: 0x00010950
			void IFxProxyPool.SetItemProperties(ItemPropertiesBase props)
			{
				base.WrappedObject.SetItemProperties(props);
			}

			// Token: 0x060002C6 RID: 710 RVA: 0x0001275E File Offset: 0x0001095E
			IFolderProxy IFxProxyPool.CreateFolder(FolderRec folder)
			{
				string.Format("SimpleTranslatingProxyPool.IFxProxyPool.CreateFolder({0}). Consider using FolderContentsMapper.GetFxProxyPoolTransformer() if create folder is needed.", folder.FolderName);
				throw new NotImplementedPermanentException("SimpleTranslatingProxyPool.IFxProxyPool.CreateFolder");
			}

			// Token: 0x060002C7 RID: 711 RVA: 0x0001277C File Offset: 0x0001097C
			IFolderProxy IFxProxyPool.GetFolderProxy(byte[] sourceFolderId)
			{
				byte[] folderId;
				if (!this.sourceMap.TryGetValue(sourceFolderId, out folderId))
				{
					throw new FolderIsMissingTransientException();
				}
				return base.WrappedObject.GetFolderProxy(folderId);
			}

			// Token: 0x060002C8 RID: 712 RVA: 0x000127AB File Offset: 0x000109AB
			List<byte[]> IFxProxyPool.GetUploadedMessageIDs()
			{
				return base.WrappedObject.GetUploadedMessageIDs();
			}

			// Token: 0x04000126 RID: 294
			private EntryIdMap<byte[]> sourceMap;
		}
	}
}
