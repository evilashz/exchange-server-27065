using System;
using System.Collections.Generic;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000058 RID: 88
	internal class PublicFolderMailboxMigrator : MailboxCopierBase
	{
		// Token: 0x06000485 RID: 1157 RVA: 0x0001B3C4 File Offset: 0x000195C4
		internal PublicFolderMailboxMigrator(TenantPublicFolderConfiguration publicFolderConfiguration, List<FolderToMailboxMapping> folderToMailboxMap, Guid targetMailboxGuid, MailboxCopierFlags copierFlags, TransactionalRequestJob migrationRequestJob, BaseJob publicFolderMigrationJob, LocalizedString sourceTracingID) : base(Guid.Empty, targetMailboxGuid, migrationRequestJob, publicFolderMigrationJob, copierFlags, sourceTracingID, new LocalizedString(targetMailboxGuid.ToString()))
		{
			MrsTracer.Service.Function("PublicFolderMailboxMigrator.Constructor", new object[0]);
			this.publicFolderConfiguration = publicFolderConfiguration;
			this.folderToMailboxMap = folderToMailboxMap;
			this.mailboxToADObjectIdMap = new Dictionary<Guid, ADObjectId>();
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001B425 File Offset: 0x00019625
		public void SetSourceDatabasesWrapper(SourceMailboxWrapper sourceDatabasesWrapper)
		{
			base.SourceMailboxWrapper = sourceDatabasesWrapper;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001B430 File Offset: 0x00019630
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
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(this.publicFolderConfiguration.OrganizationId);
			this.orgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(false, ConsistencyMode.PartiallyConsistent, sessionSettings, 170, "ConfigureProviders", "f:\\15.00.1497\\sources\\dev\\mrs\\src\\Service\\PublicFolderMailboxMigrator.cs");
			if (!CommonUtils.IsMultiTenantEnabled() && !base.Flags.HasFlag(MailboxCopierFlags.CrossOrg))
			{
				this.orgRecipientSession.EnforceDefaultScope = false;
				this.orgRecipientSession.UseGlobalCatalog = true;
			}
			ArgumentValidator.ThrowIfNull("orgRecipientSession", this.orgRecipientSession);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001B579 File Offset: 0x00019779
		public override void UnconfigureProviders()
		{
			base.SourceMailboxWrapper = null;
			base.UnconfigureProviders();
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001B588 File Offset: 0x00019788
		public override void PostCreateDestinationMailbox()
		{
			base.PostCreateDestinationMailbox();
			if (this.IsHierarchyMailboxCopier())
			{
				this.PreProcessHierarchy();
				this.CreateDumpsterFoldersForWellKnownFolders();
			}
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001B5CE File Offset: 0x000197CE
		public override FolderMap GetSourceFolderMap(GetFolderMapFlags flags)
		{
			base.SourceMailboxWrapper.LoadFolderMap(flags, delegate
			{
				FolderHierarchy folderHierarchy = new FolderHierarchy(FolderHierarchyFlags.None, base.SourceMailboxWrapper);
				folderHierarchy.LoadHierarchy(EnumerateFolderHierarchyFlags.None, null, false, this.GetAdditionalFolderPtags());
				return folderHierarchy;
			});
			return base.SourceMailboxWrapper.FolderMap;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001B638 File Offset: 0x00019838
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

		// Token: 0x0600048C RID: 1164 RVA: 0x0001B685 File Offset: 0x00019885
		public override void PreProcessHierarchy()
		{
			base.DestMailboxWrapper.LoadFolderMap(GetFolderMapFlags.None, delegate
			{
				FolderHierarchy folderHierarchy = new FolderHierarchy(FolderHierarchyFlags.PublicFolderMailbox, base.DestMailboxWrapper);
				folderHierarchy.LoadHierarchy(EnumerateFolderHierarchyFlags.WellKnownPublicFoldersOnly, null, false, null);
				return folderHierarchy;
			});
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001B6A0 File Offset: 0x000198A0
		public override bool ShouldCreateFolder(FolderMap.EnumFolderContext context, FolderRecWrapper sourceFolderRecWrapper)
		{
			FolderMapping folderMapping = sourceFolderRecWrapper as FolderMapping;
			Guid contentMailboxGuid = this.GetContentMailboxGuid(sourceFolderRecWrapper);
			bool flag = contentMailboxGuid == base.TargetMailboxGuid;
			if (!this.IsHierarchyMailboxCopier() && !flag && (!this.ShouldCreateUnderParentInSecondary() || !this.DoesAnySubFolderResideInTargetMailbox(sourceFolderRecWrapper)))
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

		// Token: 0x0600048E RID: 1166 RVA: 0x0001B74C File Offset: 0x0001994C
		protected override bool HasSourceFolderContents(FolderRecWrapper sourceFolderRec)
		{
			return this.GetContentMailboxGuid(sourceFolderRec) == base.TargetMailboxGuid;
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001B760 File Offset: 0x00019960
		protected override bool ShouldCopyFolderProperties(FolderRecWrapper sourceFolderRec)
		{
			return this.IsHierarchyMailboxCopier() || this.HasSourceFolderContents(sourceFolderRec);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001B774 File Offset: 0x00019974
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
				if (this.ShouldCreateUnderParentInSecondary())
				{
					parentId = base.DestMailbox.GetSessionSpecificEntryId(folderMapping.ParentId);
				}
				else
				{
					parentId = folderHierarchy.GetWellKnownFolder(WellKnownFolderType.IpmSubtree).EntryId;
				}
				break;
			}
			Guid contentMailboxGuid = this.GetContentMailboxGuid(sourceFolderRecWrapper);
			if (!this.IsHierarchyMailboxCopier())
			{
				createFolderFlags = CreateFolderFlags.None;
			}
			byte[] entryId = folderMapping.FolderRec.EntryId;
			byte[] parentId2 = folderMapping.FolderRec.ParentId;
			folderMapping.FolderRec.EntryId = sessionSpecificEntryId;
			folderMapping.FolderRec.ParentId = parentId;
			base.DestMailbox.CreateFolder(folderMapping.FolderRec, createFolderFlags, out newFolderEntryId);
			folderMapping.FolderRec.EntryId = entryId;
			folderMapping.FolderRec.ParentId = parentId2;
			List<PropValueData> list = new List<PropValueData>(2);
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
				if (this.IsHierarchyMailboxCopier() && !this.AssociatedDumpsterExists(folder))
				{
					byte[] value = this.CreateAssociatedDumpsterFolder(folderMapping.FolderName, bytesFromStringArray, sessionSpecificEntryId);
					list.Add(new PropValueData(PropTag.IpmWasteBasketEntryId, value));
				}
				folder.SetProps(list.ToArray());
			}
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001B978 File Offset: 0x00019B78
		public override PropTag[] GetAdditionalFolderPtags()
		{
			if (PublicFolderMailboxMigrator.additionalFolderPtagsToLoad == null)
			{
				PublicFolderMailboxMigrator.additionalFolderPtagsToLoad = new List<PropTag>(base.GetAdditionalFolderPtags())
				{
					PropTag.LTID
				}.ToArray();
			}
			return PublicFolderMailboxMigrator.additionalFolderPtagsToLoad;
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001B9B3 File Offset: 0x00019BB3
		public override byte[] GetSourceFolderEntryId(FolderRecWrapper destinationFolderRec)
		{
			return base.SourceMailbox.GetSessionSpecificEntryId(destinationFolderRec.EntryId);
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001B9C6 File Offset: 0x00019BC6
		protected override PropTag[] GetAdditionalExcludedFolderPtags()
		{
			return PublicFolderMailboxMigrator.AdditionalExcludedFolderPtags;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001B9CD File Offset: 0x00019BCD
		protected override bool ShouldCompareParentIDs()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001B9D4 File Offset: 0x00019BD4
		protected override EnumerateMessagesFlags GetAdditionalEnumerateMessagesFlagsForContentVerification()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001B9DC File Offset: 0x00019BDC
		public override void CopyFolderProperties(FolderRecWrapper sourceFolderRecWrapper, ISourceFolder sourceFolder, IDestinationFolder destFolder, FolderRecDataFlags dataToCopy, out bool wasPropertyCopyingSkipped)
		{
			Guid empty = Guid.Empty;
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
				if (!this.IsHierarchyMailboxCopier() || ((FolderMapping)sourceFolderRecWrapper).IsSystemPublicFolder)
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
				if (!this.IsHierarchyMailboxCopier() && !flag)
				{
					return;
				}
				List<PropValueData> list = new List<PropValueData>(9);
				bool flag2 = false;
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
						if (this.IsHierarchyMailboxCopier())
						{
							if (flag3)
							{
								a = this.LinkMailPublicFolder(LinkMailPublicFolderFlags.EntryId, sourceFolderRecWrapper.EntryId, empty, sourceFolderRecWrapper.EntryId);
							}
							else
							{
								a = this.LinkMailPublicFolder(LinkMailPublicFolderFlags.ObjectGuid, array2, empty, sourceFolderRecWrapper.EntryId);
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
				if (this.IsHierarchyMailboxCopier())
				{
					if (!flag2)
					{
						list.Add(new PropValueData(PropTag.PfProxy, Guid.Empty.ToByteArray()));
						list.Add(new PropValueData(PropTag.PfProxyRequired, false));
					}
					if (!flag)
					{
						dataToCopy &= (FolderRecDataFlags.SecurityDescriptors | FolderRecDataFlags.FolderAcls | FolderRecDataFlags.ExtendedAclInformation);
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
						list.Add(propValueData);
					}
				}
				if (list.Count > 0)
				{
					destFolder.SetProps(list.ToArray());
				}
				return;
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001BDF4 File Offset: 0x00019FF4
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

		// Token: 0x06000498 RID: 1176 RVA: 0x0001BE62 File Offset: 0x0001A062
		public override SyncContext CreateSyncContext()
		{
			return new PublicFolderMigrationSyncContext(base.SourceMailbox, this.GetSourceFolderMap(GetFolderMapFlags.ForceRefresh), base.DestMailbox, this.GetDestinationFolderMap(GetFolderMapFlags.ForceRefresh), base.IsRoot);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001BE8C File Offset: 0x0001A08C
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

		// Token: 0x0600049A RID: 1178 RVA: 0x0001BF38 File Offset: 0x0001A138
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

		// Token: 0x0600049B RID: 1179 RVA: 0x0001BF6C File Offset: 0x0001A16C
		protected override FolderRecWrapper CreateDestinationFolder(SyncContext syncContext, FolderRecWrapper srcFolderRec, FolderRecWrapper destParentRec)
		{
			CreateFolderFlags createFolderFlags = CreateFolderFlags.None;
			if (this.IsHierarchyMailboxCopier() && base.IsPublicFolderMigration && base.DestMailbox.IsCapabilitySupported(MRSProxyCapabilities.CanStoreCreatePFDumpster))
			{
				createFolderFlags = CreateFolderFlags.CreatePublicFolderDumpster;
			}
			byte[] entryId;
			this.CreateFolder(PublicFolderMailboxMigrator.dummyEnumFolderContext, srcFolderRec, createFolderFlags, out entryId);
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

		// Token: 0x0600049C RID: 1180 RVA: 0x0001C02C File Offset: 0x0001A22C
		private static string NormalizeFolderPath(string fullFolderName)
		{
			fullFolderName = fullFolderName.Substring("Public Root".Length, fullFolderName.Length - "Public Root".Length);
			return fullFolderName.Replace("/", "\\");
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001C158 File Offset: 0x0001A358
		private Guid GetContentMailboxGuid(FolderRecWrapper sourceFolderRec)
		{
			return sourceFolderRec.GetContentMailboxGuid(delegate(string fullFolderName)
			{
				fullFolderName = PublicFolderMailboxMigrator.NormalizeFolderPath(sourceFolderRec.FullFolderName);
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

		// Token: 0x0600049E RID: 1182 RVA: 0x0001C190 File Offset: 0x0001A390
		private bool ShouldCreateUnderParentInSecondary()
		{
			return base.MRSJob.CachedRequestJob.JobType >= MRSJobType.RequestJobE15_CreatePublicFoldersUnderParentInSecondary;
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001C1A9 File Offset: 0x0001A3A9
		private bool DoesAnySubFolderResideInTargetMailbox(FolderRecWrapper folderRec)
		{
			return PublicFolderMailboxMigrator.DoesAnySubFolderResideInTargetMailbox(folderRec, base.TargetMailboxGuid, this.folderToMailboxMap);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001C2CC File Offset: 0x0001A4CC
		internal static bool DoesAnySubFolderResideInTargetMailbox(FolderRecWrapper folderRec, Guid targetMailboxGuid, List<FolderToMailboxMapping> folderToMailboxMap)
		{
			return folderRec.IsTargetMailbox(targetMailboxGuid, delegate(string fullFolderName)
			{
				string text = PublicFolderMailboxMigrator.NormalizeFolderPath(folderRec.FullFolderName);
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

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001C308 File Offset: 0x0001A508
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

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001C498 File Offset: 0x0001A698
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

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001C560 File Offset: 0x0001A760
		private bool AssociatedDumpsterExists(IDestinationFolder folder)
		{
			PropValueData[] props = folder.GetProps(PublicFolderMailboxMigrator.DumpsterFolderPtag);
			return props != null && props.Length > 0 && props[0] != null && props[0].Value is byte[];
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001C599 File Offset: 0x0001A799
		private bool IsHierarchyMailboxCopier()
		{
			return this.publicFolderConfiguration.GetHierarchyMailboxInformation().HierarchyMailboxGuid == base.TargetMailboxGuid;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001C5B8 File Offset: 0x0001A7B8
		private Guid LinkMailPublicFolder(LinkMailPublicFolderFlags flags, byte[] objectId, Guid contentMailboxGuid, byte[] sourceFolderEntryId)
		{
			Guid result = Guid.Empty;
			try
			{
				ADObjectId contentMailbox = null;
				if (!this.mailboxToADObjectIdMap.TryGetValue(contentMailboxGuid, out contentMailbox))
				{
					ADUser aduser = this.orgRecipientSession.FindByExchangeGuid(contentMailboxGuid) as ADUser;
					if (aduser == null)
					{
						throw new RecipientNotFoundPermanentException(contentMailboxGuid);
					}
					this.mailboxToADObjectIdMap[contentMailboxGuid] = aduser.Id;
					contentMailbox = aduser.Id;
				}
				ADPublicFolder adpublicFolder = null;
				switch (flags)
				{
				case LinkMailPublicFolderFlags.ObjectGuid:
					adpublicFolder = (this.orgRecipientSession.Read(new ADObjectId(objectId)) as ADPublicFolder);
					if (adpublicFolder == null)
					{
						base.Report.Append(new ReportEntry(MrsStrings.MoveRequestMessageWarning(MrsStrings.MailPublicFolderWithObjectIdNotFound(new Guid(objectId)))));
					}
					break;
				case LinkMailPublicFolderFlags.EntryId:
				{
					string text = PublicFolderSession.ConvertToLegacyDN("e71f13d1-0178-42a7-8c47-24206de84a77", HexConverter.ByteArrayToHexString(objectId));
					ADRecipient[] array = this.orgRecipientSession.Find(null, QueryScope.SubTree, new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.LegacyExchangeDN, text), null, 2);
					if (array.Length == 1)
					{
						adpublicFolder = (array[0] as ADPublicFolder);
					}
					else if (array.Length > 1)
					{
						base.Report.Append(new ReportEntry(MrsStrings.MoveRequestMessageWarning(MrsStrings.MailPublicFolderWithMultipleEntriesFound(text))));
					}
					else
					{
						base.Report.Append(new ReportEntry(MrsStrings.MoveRequestMessageWarning(MrsStrings.MailPublicFolderWithLegacyExchangeDnNotFound(text))));
					}
					break;
				}
				default:
					throw new UnexpectedErrorPermanentException(-2147024809);
				}
				if (adpublicFolder != null)
				{
					adpublicFolder.ContentMailbox = contentMailbox;
					adpublicFolder.EntryId = HexConverter.ByteArrayToHexString(base.DestMailbox.GetSessionSpecificEntryId(sourceFolderEntryId));
					this.orgRecipientSession.Save(adpublicFolder);
					result = adpublicFolder.Guid;
				}
			}
			catch (LocalizedException ex)
			{
				base.Report.Append(new ReportEntry(new LocalizedString(ex.Message)));
				throw;
			}
			return result;
		}

		// Token: 0x040001EF RID: 495
		private const char EOF = 'ÿ';

		// Token: 0x040001F0 RID: 496
		private const string Backslash = "\\";

		// Token: 0x040001F1 RID: 497
		private static readonly PropTag[] DumpsterFolderPtag = new PropTag[]
		{
			PropTag.IpmWasteBasketEntryId
		};

		// Token: 0x040001F2 RID: 498
		private static readonly PropTag[] AdditionalExcludedFolderPtags = new PropTag[]
		{
			PropTag.ReplicaList,
			PropTag.AddressBookEntryId,
			PropTag.PfProxy,
			PropTag.PfProxyRequired,
			PropTag.LastConflict
		};

		// Token: 0x040001F3 RID: 499
		private static PropTag[] additionalFolderPtagsToLoad;

		// Token: 0x040001F4 RID: 500
		private static FolderMap.EnumFolderContext dummyEnumFolderContext = new FolderMap.EnumFolderContext();

		// Token: 0x040001F5 RID: 501
		private TenantPublicFolderConfiguration publicFolderConfiguration;

		// Token: 0x040001F6 RID: 502
		private List<FolderToMailboxMapping> folderToMailboxMap;

		// Token: 0x040001F7 RID: 503
		private IRecipientSession orgRecipientSession;

		// Token: 0x040001F8 RID: 504
		private Dictionary<Guid, ADObjectId> mailboxToADObjectIdMap;
	}
}
