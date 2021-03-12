using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000057 RID: 87
	internal class PublicFolderMigrator : MailboxCopierBase
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x0001A16C File Offset: 0x0001836C
		internal PublicFolderMigrator(TenantPublicFolderConfiguration publicFolderConfiguration, List<FolderToMailboxMapping> folderToMailboxMap, Guid targetMailboxGuid, MailboxCopierFlags copierFlags, TransactionalRequestJob migrationRequestJob, BaseJob publicFolderMigrationJob, LocalizedString sourceTracingID) : base(Guid.Empty, targetMailboxGuid, migrationRequestJob, publicFolderMigrationJob, copierFlags, sourceTracingID, new LocalizedString(targetMailboxGuid.ToString()))
		{
			MrsTracer.Service.Function("PublicFolderMigrator.Constructor", new object[0]);
			this.publicFolderConfiguration = publicFolderConfiguration;
			this.folderToMailboxMap = folderToMailboxMap;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001A1C2 File Offset: 0x000183C2
		public void SetSourceDatabasesWrapper(SourceMailboxWrapper sourceDatabasesWrapper)
		{
			base.SourceMailboxWrapper = sourceDatabasesWrapper;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x0001A1CB File Offset: 0x000183CB
		public void SetHierarchyMailbox(IDestinationMailbox hierarchyMailbox)
		{
			this.hierarchyMailbox = hierarchyMailbox;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x0001A1D4 File Offset: 0x000183D4
		public override void ConfigureProviders()
		{
			RequestStatisticsBase cachedRequestJob = base.MRSJob.CachedRequestJob;
			PublicFolderRecipient localMailboxRecipient = this.publicFolderConfiguration.GetLocalMailboxRecipient(base.TargetMailboxGuid);
			if (localMailboxRecipient == null)
			{
				throw new RecipientNotFoundPermanentException(base.TargetMailboxGuid);
			}
			List<MRSProxyCapabilities> list = new List<MRSProxyCapabilities>();
			ProxyServerSettings proxyServerSettings = CommonUtils.MapDatabaseToProxyServer(localMailboxRecipient.Database.ObjectGuid);
			list.Add(MRSProxyCapabilities.PublicFolderMigration);
			IDestinationMailbox destinationMailbox = this.GetDestinationMailbox(localMailboxRecipient.Database.ObjectGuid, proxyServerSettings.ExtraFlags | LocalMailboxFlags.Move, list);
			destinationMailbox.Config(base.MRSJob.GetReservation(localMailboxRecipient.Database.ObjectGuid, ReservationFlags.Write), base.TargetMailboxGuid, base.TargetMailboxGuid, CommonUtils.GetPartitionHint(cachedRequestJob.OrganizationId), localMailboxRecipient.Database.ObjectGuid, MailboxType.DestMailboxIntraOrg, null);
			base.ConfigDestinationMailbox(destinationMailbox);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0001A2A1 File Offset: 0x000184A1
		public override void UnconfigureProviders()
		{
			base.SourceMailboxWrapper = null;
			base.UnconfigureProviders();
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001A2B0 File Offset: 0x000184B0
		public override void PostCreateDestinationMailbox()
		{
			base.PostCreateDestinationMailbox();
			if (base.IsRoot)
			{
				this.PreProcessHierarchy();
				this.CreateDumpsterFoldersForWellKnownFolders();
			}
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x0001A2F8 File Offset: 0x000184F8
		public override FolderMap GetSourceFolderMap(GetFolderMapFlags flags)
		{
			if (base.IsRoot)
			{
				base.SourceMailboxWrapper.LoadFolderMap(flags, delegate
				{
					FolderHierarchy folderHierarchy = new FolderHierarchy(FolderHierarchyFlags.None, base.SourceMailboxWrapper);
					folderHierarchy.LoadHierarchy(EnumerateFolderHierarchyFlags.None, null, false, this.GetAdditionalFolderPtags());
					return folderHierarchy;
				});
			}
			return base.SourceMailboxWrapper.FolderMap;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0001A37C File Offset: 0x0001857C
		public override FolderMap GetDestinationFolderMap(GetFolderMapFlags flags)
		{
			base.DestMailboxWrapper.LoadFolderMap(flags, delegate
			{
				FolderHierarchy folderHierarchy = new FolderHierarchy(FolderHierarchyFlags.PublicFolderMailbox, base.DestMailboxWrapper);
				folderHierarchy.LoadHierarchy(EnumerateFolderHierarchyFlags.None, null, false, new PropTag[]
				{
					PropTag.ReplicaList,
					PropTag.LTID,
					PropTag.TimeInServer
				});
				return folderHierarchy;
			});
			return base.DestMailboxWrapper.FolderMap;
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0001A3C9 File Offset: 0x000185C9
		public override void PreProcessHierarchy()
		{
			base.DestMailboxWrapper.LoadFolderMap(GetFolderMapFlags.None, delegate
			{
				FolderHierarchy folderHierarchy = new FolderHierarchy(FolderHierarchyFlags.PublicFolderMailbox, base.DestMailboxWrapper);
				folderHierarchy.LoadHierarchy(EnumerateFolderHierarchyFlags.WellKnownPublicFoldersOnly, null, false, null);
				return folderHierarchy;
			});
		}

		// Token: 0x0600046A RID: 1130 RVA: 0x0001A3E4 File Offset: 0x000185E4
		public override bool ShouldCreateFolder(FolderMap.EnumFolderContext context, FolderRecWrapper sourceFolderRecWrapper)
		{
			FolderMapping folderMapping = sourceFolderRecWrapper as FolderMapping;
			Guid contentMailboxGuid = this.GetContentMailboxGuid(sourceFolderRecWrapper);
			bool isRoot = base.IsRoot;
			bool flag = contentMailboxGuid == base.TargetMailboxGuid;
			if (!isRoot && !flag && (!this.ShouldCreateUnderParentInSecondary() || !this.DoesAnySubFolderResideInTargetMailbox(sourceFolderRecWrapper)))
			{
				MrsTracer.Service.Debug("Not creating folder \"{0}\" in mailbox {1}. This is neither the primary hierarchy mailbox nor the content mailbox of this folder or any of its subfolders.", new object[]
				{
					sourceFolderRecWrapper.FullFolderName,
					base.TargetMailboxGuid
				});
				return false;
			}
			switch (folderMapping.WKFType)
			{
			case WellKnownFolderType.Root:
			case WellKnownFolderType.NonIpmSubtree:
			case WellKnownFolderType.IpmSubtree:
			case WellKnownFolderType.EFormsRegistry:
				return false;
			default:
				if (folderMapping.IsLegacyPublicFolder)
				{
					context.Result = EnumHierarchyResult.SkipSubtree;
					return false;
				}
				return true;
			}
		}

		// Token: 0x0600046B RID: 1131 RVA: 0x0001A496 File Offset: 0x00018696
		protected override bool HasSourceFolderContents(FolderRecWrapper sourceFolderRec)
		{
			return this.GetContentMailboxGuid(sourceFolderRec) == base.TargetMailboxGuid;
		}

		// Token: 0x0600046C RID: 1132 RVA: 0x0001A4AA File Offset: 0x000186AA
		protected override bool ShouldCopyFolderProperties(FolderRecWrapper sourceFolderRec)
		{
			return base.IsRoot || this.HasSourceFolderContents(sourceFolderRec);
		}

		// Token: 0x0600046D RID: 1133 RVA: 0x0001A4C0 File Offset: 0x000186C0
		public override void CreateFolder(FolderMap.EnumFolderContext context, FolderRecWrapper sourceFolderRecWrapper, CreateFolderFlags createFolderFlags, out byte[] newFolderEntryId)
		{
			if (sourceFolderRecWrapper.IsInternalAccess)
			{
				throw new InternalAccessFolderCreationIsNotSupportedException();
			}
			newFolderEntryId = null;
			FolderMapping folderMapping = sourceFolderRecWrapper as FolderMapping;
			FolderHierarchy folderHierarchy = base.DestMailboxWrapper.FolderMap as FolderHierarchy;
			byte[] sessionSpecificEntryId = base.DestMailbox.GetSessionSpecificEntryId(folderMapping.EntryId);
			FolderMapping folderMapping2 = folderMapping.Parent as FolderMapping;
			bool isRoot = base.IsRoot;
			byte[] parentId;
			switch (folderMapping2.WKFType)
			{
			case WellKnownFolderType.Root:
			case WellKnownFolderType.NonIpmSubtree:
			case WellKnownFolderType.IpmSubtree:
			case WellKnownFolderType.EFormsRegistry:
				parentId = folderHierarchy.GetWellKnownFolder(folderMapping2.WKFType).EntryId;
				break;
			default:
				if (isRoot || this.ShouldCreateUnderParentInSecondary())
				{
					parentId = base.DestMailbox.GetSessionSpecificEntryId(folderMapping.ParentId);
				}
				else
				{
					parentId = folderHierarchy.GetWellKnownFolder(WellKnownFolderType.IpmSubtree).EntryId;
				}
				break;
			}
			byte[] entryId = folderMapping.FolderRec.EntryId;
			byte[] parentId2 = folderMapping.FolderRec.ParentId;
			folderMapping.FolderRec.EntryId = sessionSpecificEntryId;
			folderMapping.FolderRec.ParentId = parentId;
			base.DestMailbox.CreateFolder(folderMapping.FolderRec, createFolderFlags, out newFolderEntryId);
			folderMapping.FolderRec.EntryId = entryId;
			folderMapping.FolderRec.ParentId = parentId2;
			List<PropValueData> list = new List<PropValueData>(2);
			Guid contentMailboxGuid = this.GetContentMailboxGuid(sourceFolderRecWrapper);
			StorePropertyDefinition replicaList = CoreFolderSchema.ReplicaList;
			byte[] bytesFromStringArray = ReplicaListProperty.GetBytesFromStringArray(new string[]
			{
				contentMailboxGuid.ToString()
			});
			list.Add(new PropValueData(PropTag.ReplicaList, bytesFromStringArray));
			using (IDestinationFolder folder = base.DestMailbox.GetFolder(sessionSpecificEntryId))
			{
				if (folder == null)
				{
					MrsTracer.Service.Error("Something deleted destination folder from under us", new object[0]);
					throw new UnexpectedErrorPermanentException(-2147221238);
				}
				if (isRoot && !this.AssociatedDumpsterExists(folder))
				{
					byte[] value = this.CreateAssociatedDumpsterFolder(folderMapping.FolderName, bytesFromStringArray, sessionSpecificEntryId);
					list.Add(new PropValueData(PropTag.IpmWasteBasketEntryId, value));
				}
				folder.SetProps(list.ToArray());
			}
		}

		// Token: 0x0600046E RID: 1134 RVA: 0x0001A6C4 File Offset: 0x000188C4
		public override PropTag[] GetAdditionalFolderPtags()
		{
			if (PublicFolderMigrator.additionalFolderPtagsToLoad == null)
			{
				PublicFolderMigrator.additionalFolderPtagsToLoad = new List<PropTag>(base.GetAdditionalFolderPtags())
				{
					PropTag.LTID
				}.ToArray();
			}
			return PublicFolderMigrator.additionalFolderPtagsToLoad;
		}

		// Token: 0x0600046F RID: 1135 RVA: 0x0001A6FF File Offset: 0x000188FF
		public override byte[] GetSourceFolderEntryId(FolderRecWrapper destinationFolderRec)
		{
			return base.SourceMailbox.GetSessionSpecificEntryId(destinationFolderRec.EntryId);
		}

		// Token: 0x06000470 RID: 1136 RVA: 0x0001A712 File Offset: 0x00018912
		protected override PropTag[] GetAdditionalExcludedFolderPtags()
		{
			return PublicFolderMigrator.AdditionalExcludedFolderPtags;
		}

		// Token: 0x06000471 RID: 1137 RVA: 0x0001A719 File Offset: 0x00018919
		protected override bool ShouldCompareParentIDs()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0001A720 File Offset: 0x00018920
		protected override EnumerateMessagesFlags GetAdditionalEnumerateMessagesFlagsForContentVerification()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000473 RID: 1139 RVA: 0x0001A728 File Offset: 0x00018928
		public override void CopyFolderProperties(FolderRecWrapper sourceFolderRecWrapper, ISourceFolder sourceFolder, IDestinationFolder destFolder, FolderRecDataFlags dataToCopy, out bool wasPropertyCopyingSkipped)
		{
			Guid empty = Guid.Empty;
			bool isRoot = base.IsRoot;
			bool flag = false;
			wasPropertyCopyingSkipped = false;
			FolderMapping folderMapping = sourceFolderRecWrapper as FolderMapping;
			while (folderMapping.WKFType != WellKnownFolderType.Root)
			{
				if (folderMapping.IsLegacyPublicFolder)
				{
					return;
				}
				folderMapping = (folderMapping.Parent as FolderMapping);
			}
			if (destFolder == null)
			{
				if (!isRoot || ((FolderMapping)sourceFolderRecWrapper).IsSystemPublicFolder)
				{
					MrsTracer.Service.Debug("Skipping final property copying for \"{0}\" folder since it's contents don't reside in this mailbox", new object[]
					{
						sourceFolderRecWrapper.FullFolderName
					});
					return;
				}
				throw new FolderCopyFailedPermanentException(sourceFolderRecWrapper.FullFolderName);
			}
			else
			{
				PropValueData[] props = destFolder.GetProps(new PropTag[]
				{
					PropTag.ReplicaList,
					PropTag.IpmWasteBasketEntryId
				});
				IDataConverter<PropValue, PropValueData> dataConverter = new PropValueConverter();
				PropValue nativeRepresentation = dataConverter.GetNativeRepresentation(props[0]);
				byte[] array = nativeRepresentation.Value as byte[];
				if (!nativeRepresentation.IsNull() && !nativeRepresentation.IsError() && array != null)
				{
					StorePropertyDefinition replicaList = CoreFolderSchema.ReplicaList;
					string[] stringArrayFromBytes = ReplicaListProperty.GetStringArrayFromBytes(array);
					if (stringArrayFromBytes.Length > 0 && GuidHelper.TryParseGuid(stringArrayFromBytes[0], out empty))
					{
						flag = (empty == base.TargetMailboxGuid);
					}
				}
				FolderStateSnapshot folderStateSnapshot = base.ICSSyncState[sourceFolderRecWrapper.EntryId];
				FolderState state = folderStateSnapshot.State;
				if (sourceFolder.GetFolderRec(this.GetAdditionalFolderPtags(), GetFolderRecFlags.None).IsGhosted)
				{
					folderStateSnapshot.State |= FolderState.IsGhosted;
				}
				else
				{
					folderStateSnapshot.State &= ~FolderState.IsGhosted;
				}
				if (state != folderStateSnapshot.State)
				{
					base.SaveICSSyncState(false);
				}
				if (!isRoot && !flag)
				{
					return;
				}
				List<PropValueData> list = new List<PropValueData>(2);
				bool flag2 = false;
				if (flag)
				{
					PropValue nativeRepresentation2 = dataConverter.GetNativeRepresentation(sourceFolder.GetProps(new PropTag[]
					{
						PropTag.PfProxy
					})[0]);
					if (!nativeRepresentation2.IsNull() && !nativeRepresentation2.IsError())
					{
						byte[] array2 = nativeRepresentation2.Value as byte[];
						if (array2 != null && array2.Length == 16 && new Guid(array2) != Guid.Empty)
						{
							Guid a = Guid.Empty;
							bool flag3 = base.Flags.HasFlag(MailboxCopierFlags.CrossOrg);
							if (flag3)
							{
								a = destFolder.LinkMailPublicFolder(LinkMailPublicFolderFlags.EntryId, sourceFolderRecWrapper.EntryId);
							}
							else
							{
								a = destFolder.LinkMailPublicFolder(LinkMailPublicFolderFlags.ObjectGuid, array2);
							}
							if (a != Guid.Empty)
							{
								list.Add(new PropValueData(PropTag.PfProxy, a.ToByteArray()));
								list.Add(new PropValueData(PropTag.PfProxyRequired, true));
								flag2 = true;
							}
							else
							{
								base.Report.Append(new ReportEntry(MrsStrings.ReportFailedToLinkADPublicFolder(sourceFolderRecWrapper.FullFolderName, BitConverter.ToString(array2), BitConverter.ToString(sourceFolderRecWrapper.EntryId)), ReportEntryType.Warning));
							}
						}
					}
				}
				if (!flag2)
				{
					list.Add(new PropValueData(PropTag.PfProxy, Guid.Empty.ToByteArray()));
					list.Add(new PropValueData(PropTag.PfProxyRequired, false));
				}
				List<PropValueData> list2 = new List<PropValueData>(9);
				if (isRoot)
				{
					if (!flag)
					{
						dataToCopy &= (FolderRecDataFlags.SecurityDescriptors | FolderRecDataFlags.FolderAcls | FolderRecDataFlags.ExtendedAclInformation);
					}
					list2.AddRange(list);
				}
				else
				{
					byte[] sessionSpecificEntryId = this.hierarchyMailbox.GetSessionSpecificEntryId(sourceFolderRecWrapper.EntryId);
					using (IDestinationFolder folder = this.hierarchyMailbox.GetFolder(sessionSpecificEntryId))
					{
						if (folder == null)
						{
							MrsTracer.Service.Error("Something deleted destination hierarchy folder from under us", new object[0]);
							throw new UnexpectedErrorPermanentException(-2147221238);
						}
						if (list.Count > 0)
						{
							folder.SetProps(list.ToArray());
						}
					}
				}
				base.CopyFolderProperties(sourceFolderRecWrapper, sourceFolder, destFolder, dataToCopy, out wasPropertyCopyingSkipped);
				PropTag[] pta = new PropTag[]
				{
					PropTag.DisablePeruserRead,
					PropTag.OverallAgeLimit,
					PropTag.RetentionAgeLimit,
					PropTag.PfQuotaStyle,
					PropTag.PfOverHardQuotaLimit,
					PropTag.PfStorageQuota,
					PropTag.PfMsgSizeLimit
				};
				foreach (PropValueData propValueData in sourceFolder.GetProps(pta))
				{
					PropValue nativeRepresentation3 = dataConverter.GetNativeRepresentation(propValueData);
					if (!nativeRepresentation3.IsNull() && !nativeRepresentation3.IsError())
					{
						if (propValueData.PropTag == 1721303043 && (int)propValueData.Value > 0)
						{
							propValueData.Value = (int)EnhancedTimeSpan.FromDays((double)((int)propValueData.Value)).TotalSeconds;
						}
						list2.Add(propValueData);
					}
				}
				if (list2.Count > 0)
				{
					destFolder.SetProps(list2.ToArray());
				}
				return;
			}
		}

		// Token: 0x06000474 RID: 1140 RVA: 0x0001ABB8 File Offset: 0x00018DB8
		public override bool IsContentAvailableInTargetMailbox(FolderRecWrapper destinationFolderRec)
		{
			FolderMapping folderMapping = destinationFolderRec as FolderMapping;
			if (folderMapping.IsSystemPublicFolder || destinationFolderRec.IsPublicFolderDumpster)
			{
				return false;
			}
			byte[] array = destinationFolderRec.FolderRec[PropTag.ReplicaList] as byte[];
			if (array != null)
			{
				StorePropertyDefinition replicaList = CoreFolderSchema.ReplicaList;
				string[] stringArrayFromBytes = ReplicaListProperty.GetStringArrayFromBytes(array);
				Guid a;
				return stringArrayFromBytes.Length > 0 && GuidHelper.TryParseGuid(stringArrayFromBytes[0], out a) && a == base.TargetMailboxGuid;
			}
			return true;
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x0001AC26 File Offset: 0x00018E26
		public override SyncContext CreateSyncContext()
		{
			return new PublicFolderMigrationSyncContext(base.SourceMailbox, this.GetSourceFolderMap(GetFolderMapFlags.ForceRefresh), base.DestMailbox, this.GetDestinationFolderMap(GetFolderMapFlags.ForceRefresh), base.IsRoot);
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001AC50 File Offset: 0x00018E50
		public override void ReportBadItems(List<BadMessageRec> badItems)
		{
			List<BadMessageRec> list = new List<BadMessageRec>(badItems);
			if (!base.IsRoot)
			{
				foreach (BadMessageRec badMessageRec in badItems)
				{
					switch (badMessageRec.Kind)
					{
					case BadItemKind.CorruptFolderACL:
					case BadItemKind.CorruptFolderRule:
					case BadItemKind.CorruptFolderProperty:
						base.Report.AppendDebug(string.Format("A corrupted folder ({0}) was encountered while copying folders to content mailbox: {1}", badMessageRec, base.TargetMailboxGuid));
						list.Remove(badMessageRec);
						break;
					}
				}
			}
			base.ReportBadItems(list);
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x0001ACFC File Offset: 0x00018EFC
		public override bool ShouldReportEntry(ReportEntryKind reportEntryKind)
		{
			switch (reportEntryKind)
			{
			case ReportEntryKind.SignaturePreservation:
				return false;
			case ReportEntryKind.StartingFolderHierarchyCreation:
			case ReportEntryKind.AggregatedSoftDeletedMessages:
			case ReportEntryKind.HierarchyChanges:
				return base.IsRoot;
			default:
				return true;
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0001AD30 File Offset: 0x00018F30
		protected override FolderRecWrapper CreateDestinationFolder(SyncContext syncContext, FolderRecWrapper srcFolderRec, FolderRecWrapper destParentRec)
		{
			CreateFolderFlags createFolderFlags = CreateFolderFlags.None;
			if (base.IsRoot && base.IsPublicFolderMigration && base.DestMailbox.IsCapabilitySupported(MRSProxyCapabilities.CanStoreCreatePFDumpster))
			{
				createFolderFlags = CreateFolderFlags.CreatePublicFolderDumpster;
			}
			byte[] entryId;
			this.CreateFolder(PublicFolderMigrator.dummyEnumFolderContext, srcFolderRec, createFolderFlags, out entryId);
			FolderRecWrapper folderRecWrapper = syncContext.CreateTargetFolderRec(srcFolderRec);
			folderRecWrapper.FolderRec.EntryId = entryId;
			folderRecWrapper.FolderRec.ParentId = destParentRec.EntryId;
			List<PropValueData> list = new List<PropValueData>(folderRecWrapper.FolderRec.AdditionalProps);
			list.Add(new PropValueData(PropTag.ReplicaList, ReplicaListProperty.GetBytesFromStringArray(new string[]
			{
				this.GetContentMailboxGuid(srcFolderRec).ToString()
			})));
			folderRecWrapper.FolderRec.AdditionalProps = list.ToArray();
			return folderRecWrapper;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x0001ADF0 File Offset: 0x00018FF0
		private static string NormalizeFolderPath(string fullFolderName)
		{
			fullFolderName = fullFolderName.Substring("Public Root".Length, fullFolderName.Length - "Public Root".Length);
			return fullFolderName.Replace("/", "\\");
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x0001AF1C File Offset: 0x0001911C
		private Guid GetContentMailboxGuid(FolderRecWrapper sourceFolderRec)
		{
			return sourceFolderRec.GetContentMailboxGuid(delegate(string fullFolderName)
			{
				fullFolderName = PublicFolderMigrator.NormalizeFolderPath(sourceFolderRec.FullFolderName);
				if (string.IsNullOrWhiteSpace(fullFolderName))
				{
					return this.publicFolderConfiguration.GetHierarchyMailboxInformation().HierarchyMailboxGuid;
				}
				Guid result = this.publicFolderConfiguration.GetHierarchyMailboxInformation().HierarchyMailboxGuid;
				int num = 0;
				foreach (FolderToMailboxMapping folderToMailboxMapping in this.folderToMailboxMap)
				{
					string text = folderToMailboxMapping.FolderName;
					text = ((text.IndexOf('ÿ') > -1) ? text.Replace('ÿ', "\\"[0]) : text);
					if (fullFolderName.StartsWith(text, StringComparison.OrdinalIgnoreCase) && text.Length >= num)
					{
						num = text.Length;
						result = folderToMailboxMapping.MailboxGuid;
					}
				}
				return result;
			});
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x0001AF54 File Offset: 0x00019154
		private bool ShouldCreateUnderParentInSecondary()
		{
			return base.MRSJob.CachedRequestJob.JobType >= MRSJobType.RequestJobE15_CreatePublicFoldersUnderParentInSecondary;
		}

		// Token: 0x0600047C RID: 1148 RVA: 0x0001AF6D File Offset: 0x0001916D
		private bool DoesAnySubFolderResideInTargetMailbox(FolderRecWrapper folderRec)
		{
			return PublicFolderMigrator.DoesAnySubFolderResideInTargetMailbox(folderRec, base.TargetMailboxGuid, this.folderToMailboxMap);
		}

		// Token: 0x0600047D RID: 1149 RVA: 0x0001B090 File Offset: 0x00019290
		internal static bool DoesAnySubFolderResideInTargetMailbox(FolderRecWrapper folderRec, Guid targetMailboxGuid, List<FolderToMailboxMapping> folderToMailboxMap)
		{
			return folderRec.IsTargetMailbox(targetMailboxGuid, delegate(string fullFolderName)
			{
				string text = PublicFolderMigrator.NormalizeFolderPath(folderRec.FullFolderName);
				HashSet<Guid> hashSet = null;
				if (!string.IsNullOrWhiteSpace(text))
				{
					int num = text.Length;
					if (text.EndsWith("\\", StringComparison.OrdinalIgnoreCase))
					{
						num = text.Length - 1;
					}
					foreach (FolderToMailboxMapping folderToMailboxMapping in folderToMailboxMap)
					{
						string text2 = folderToMailboxMapping.FolderName;
						text2 = ((text2.IndexOf('ÿ') > -1) ? text2.Replace('ÿ', "\\"[0]) : text2);
						if (text2.Length > num + 1 && text2[num] == "\\"[0] && text2.StartsWith(text, StringComparison.OrdinalIgnoreCase))
						{
							if (hashSet == null)
							{
								hashSet = new HashSet<Guid>();
							}
							hashSet.Add(folderToMailboxMapping.MailboxGuid);
						}
					}
				}
				return hashSet;
			});
		}

		// Token: 0x0600047E RID: 1150 RVA: 0x0001B0CC File Offset: 0x000192CC
		private void CreateDumpsterFoldersForWellKnownFolders()
		{
			FolderHierarchy folderHierarchy = base.DestMailboxWrapper.FolderMap as FolderHierarchy;
			foreach (FolderMapping folderMapping in new List<FolderMapping>(8)
			{
				folderHierarchy.GetWellKnownFolder(WellKnownFolderType.Root),
				folderHierarchy.GetWellKnownFolder(WellKnownFolderType.IpmSubtree),
				folderHierarchy.GetWellKnownFolder(WellKnownFolderType.NonIpmSubtree),
				folderHierarchy.GetWellKnownFolder(WellKnownFolderType.EFormsRegistry),
				folderHierarchy.GetWellKnownFolder(WellKnownFolderType.PublicFolderDumpsterRoot),
				folderHierarchy.GetWellKnownFolder(WellKnownFolderType.PublicFolderTombstonesRoot),
				folderHierarchy.GetWellKnownFolder(WellKnownFolderType.PublicFolderAsyncDeleteState),
				folderHierarchy.GetWellKnownFolder(WellKnownFolderType.PublicFolderInternalSubmission)
			})
			{
				List<PropValueData> list = new List<PropValueData>(2);
				using (IDestinationFolder folder = base.DestMailbox.GetFolder(folderMapping.EntryId))
				{
					if (folder == null)
					{
						MrsTracer.Service.Error("Something deleted destination folder from under us", new object[0]);
						throw new UnexpectedErrorPermanentException(-2147221238);
					}
					if (!this.AssociatedDumpsterExists(folder))
					{
						byte[] value = this.CreateAssociatedDumpsterFolder(folderMapping.FolderName, ReplicaListProperty.GetBytesFromStringArray(new string[]
						{
							base.TargetMailboxGuid.ToString()
						}), folderMapping.EntryId);
						list.Add(new PropValueData(PropTag.IpmWasteBasketEntryId, value));
						folder.SetProps(list.ToArray());
					}
				}
			}
		}

		// Token: 0x0600047F RID: 1151 RVA: 0x0001B25C File Offset: 0x0001945C
		private byte[] CreateAssociatedDumpsterFolder(string sourceFolderName, byte[] replicaListValue, byte[] destinationFolderEntryId)
		{
			FolderHierarchy folderHierarchy = base.DestMailboxWrapper.FolderMap as FolderHierarchy;
			FolderRec folderRec = new FolderRec();
			folderRec.ParentId = folderHierarchy.GetWellKnownFolder(WellKnownFolderType.PublicFolderDumpsterRoot).EntryId;
			folderRec.FolderName = PublicFolderCOWSession.GenerateUniqueFolderName(sourceFolderName);
			folderRec.FolderType = FolderType.Generic;
			byte[] array;
			base.DestMailbox.CreateFolder(folderRec, CreateFolderFlags.FailIfExists, out array);
			using (IDestinationFolder folder = base.DestMailbox.GetFolder(array))
			{
				folder.SetProps(new PropValueData[]
				{
					new PropValueData(PropTag.ReplicaList, replicaListValue),
					new PropValueData(PropTag.IpmWasteBasketEntryId, destinationFolderEntryId),
					new PropValueData(PropTag.TimeInServer, 64)
				});
			}
			return array;
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x0001B324 File Offset: 0x00019524
		private bool AssociatedDumpsterExists(IDestinationFolder folder)
		{
			PropValueData[] props = folder.GetProps(PublicFolderMigrator.DumpsterFolderPtag);
			return props != null && props.Length > 0 && props[0] != null && props[0].Value is byte[];
		}

		// Token: 0x040001E6 RID: 486
		private const char EOF = 'ÿ';

		// Token: 0x040001E7 RID: 487
		private const string Backslash = "\\";

		// Token: 0x040001E8 RID: 488
		private static readonly PropTag[] DumpsterFolderPtag = new PropTag[]
		{
			PropTag.IpmWasteBasketEntryId
		};

		// Token: 0x040001E9 RID: 489
		private static readonly PropTag[] AdditionalExcludedFolderPtags = new PropTag[]
		{
			PropTag.ReplicaList,
			PropTag.AddressBookEntryId,
			PropTag.PfProxy,
			PropTag.PfProxyRequired,
			PropTag.LastConflict
		};

		// Token: 0x040001EA RID: 490
		private static PropTag[] additionalFolderPtagsToLoad;

		// Token: 0x040001EB RID: 491
		private static FolderMap.EnumFolderContext dummyEnumFolderContext = new FolderMap.EnumFolderContext();

		// Token: 0x040001EC RID: 492
		private TenantPublicFolderConfiguration publicFolderConfiguration;

		// Token: 0x040001ED RID: 493
		private List<FolderToMailboxMapping> folderToMailboxMap;

		// Token: 0x040001EE RID: 494
		private IDestinationMailbox hierarchyMailbox;
	}
}
