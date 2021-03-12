using System;
using System.Collections.Generic;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000011 RID: 17
	internal class FolderHierarchy : FolderMap
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00005300 File Offset: 0x00003500
		public FolderHierarchy(FolderHierarchyFlags flags, MailboxWrapper mbxWrapper) : base(flags)
		{
			this.mbxWrapper = mbxWrapper;
			this.warnings = new List<LocalizedString>();
			this.folderFilterApplied = false;
			this.wellKnownFolderMap = new FolderHierarchy.WellKnownFolderMap();
			this.mbxWrapper.NamedPropMapper.ByNamedProp.AddKey(FolderHierarchyUtils.SourceWKFTypeNamedProp);
			this.mbxWrapper.NamedPropMapper.ByNamedProp.AddKey(FolderHierarchyUtils.SourceEntryIDNamedProp);
			this.mbxWrapper.NamedPropMapper.ByNamedProp.AddKey(FolderHierarchyUtils.SourceLastModifiedTimestampNamedProp);
			this.mbxWrapper.NamedPropMapper.ByNamedProp.AddKey(FolderHierarchyUtils.SourceMessageClassNamedProp);
			this.mbxWrapper.NamedPropMapper.ByNamedProp.AddKey(FolderHierarchyUtils.SourceSyncMessageIdNamedProp);
			this.mbxWrapper.NamedPropMapper.ByNamedProp.AddKey(FolderHierarchyUtils.SourceSyncAccountNameNamedProp);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007D RID: 125 RVA: 0x000053D4 File Offset: 0x000035D4
		public List<LocalizedString> Warnings
		{
			get
			{
				return this.warnings;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000053DC File Offset: 0x000035DC
		public PropTag SourceWKFTypePtag
		{
			get
			{
				return this.mbxWrapper.NamedPropMapper.MapNamedProp(FolderHierarchyUtils.SourceWKFTypeNamedProp, PropType.Int);
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000053F4 File Offset: 0x000035F4
		public PropTag SourceLastModifiedTimestampPtag
		{
			get
			{
				return this.mbxWrapper.NamedPropMapper.MapNamedProp(FolderHierarchyUtils.SourceLastModifiedTimestampNamedProp, PropType.SysTime);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000540D File Offset: 0x0000360D
		public PropTag SourceEntryIDPtag
		{
			get
			{
				return this.mbxWrapper.NamedPropMapper.MapNamedProp(FolderHierarchyUtils.SourceEntryIDNamedProp, PropType.Binary);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000081 RID: 129 RVA: 0x00005429 File Offset: 0x00003629
		public PropTag SourceMessageClassPtag
		{
			get
			{
				return this.mbxWrapper.NamedPropMapper.MapNamedProp(FolderHierarchyUtils.SourceMessageClassNamedProp, PropType.String);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00005442 File Offset: 0x00003642
		public PropTag SourceSyncAccountNamePtag
		{
			get
			{
				return this.mbxWrapper.NamedPropMapper.MapNamedProp(FolderHierarchyUtils.SourceSyncAccountNameNamedProp, PropType.String);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000083 RID: 131 RVA: 0x0000545B File Offset: 0x0000365B
		public PropTag SourceSyncMessageIdPtag
		{
			get
			{
				return this.mbxWrapper.NamedPropMapper.MapNamedProp(FolderHierarchyUtils.SourceSyncMessageIdNamedProp, PropType.String);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00005474 File Offset: 0x00003674
		public PropTag SourceSyncFolderIdPtag
		{
			get
			{
				return this.mbxWrapper.NamedPropMapper.MapNamedProp(FolderHierarchyUtils.SourceSyncFolderIdNamedProp, PropType.Binary);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000085 RID: 133 RVA: 0x00005490 File Offset: 0x00003690
		public PropTag SharingInstanceGuidPtag
		{
			get
			{
				return this.mbxWrapper.NamedPropMapper.MapNamedProp(FolderHierarchyUtils.SharingInstanceGuidNamedProp, PropType.Guid);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000054A9 File Offset: 0x000036A9
		public PropTag CloudIdPtag
		{
			get
			{
				return this.mbxWrapper.NamedPropMapper.MapNamedProp(FolderHierarchyUtils.CloudIdNamedProp, PropType.String);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000087 RID: 135 RVA: 0x000054C2 File Offset: 0x000036C2
		public FolderMapping RootFolder
		{
			get
			{
				return this.rootFolder;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000088 RID: 136 RVA: 0x000054CA File Offset: 0x000036CA
		public MailboxWrapper MbxWrapper
		{
			get
			{
				return this.mbxWrapper;
			}
		}

		// Token: 0x06000089 RID: 137 RVA: 0x000054D2 File Offset: 0x000036D2
		public override void Clear()
		{
			base.Clear();
			this.warnings.Clear();
			this.wellKnownFolderMap.Clear();
			this.folderFilterApplied = false;
			this.rootFolder = null;
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00005538 File Offset: 0x00003738
		public void LoadHierarchy(EnumerateFolderHierarchyFlags flags, string rootFolderPath, bool createRootIfNeeded, PropTag[] additionalPtagsToLoad)
		{
			List<PropTag> list = new List<PropTag>();
			if (this.SourceWKFTypePtag != PropTag.Null)
			{
				list.Add(this.SourceWKFTypePtag);
			}
			if (this.SourceEntryIDPtag != PropTag.Null)
			{
				list.Add(this.SourceEntryIDPtag);
			}
			if (additionalPtagsToLoad != null)
			{
				list.AddRange(additionalPtagsToLoad);
			}
			List<FolderRecWrapper> folders = this.mbxWrapper.LoadFolders<FolderMapping>(flags, list.ToArray());
			base.Config(folders);
			this.DiscoverWellKnownFolders();
			if (string.IsNullOrEmpty(rootFolderPath))
			{
				this.rootFolder = (FolderMapping)this.RootRec;
			}
			else
			{
				FolderMappingFlags folderMappingFlags;
				this.rootFolder = this.FindFolderByPath(string.Empty, rootFolderPath, createRootIfNeeded, out folderMappingFlags);
				if (this.rootFolder == null)
				{
					throw new FolderIsMissingPermanentException(rootFolderPath);
				}
				if (!base.IsPublicFolderMailbox)
				{
					this.wellKnownFolderMap.Reset();
					this.wellKnownFolderMap.Clear();
					this.EnumerateSubtree(EnumHierarchyFlags.AllFolders, this.rootFolder, delegate(FolderRecWrapper fRec, FolderMap.EnumFolderContext ctx)
					{
						FolderMapping folderMapping = (FolderMapping)fRec;
						object obj = folderMapping.FolderRec[this.SourceWKFTypePtag];
						if (obj != null)
						{
							this.SetWellKnownFolder((WellKnownFolderType)((int)obj), folderMapping);
						}
					});
					if (this.GetWellKnownFolder(WellKnownFolderType.IpmSubtree) == null)
					{
						this.SetWellKnownFolder(WellKnownFolderType.IpmSubtree, this.rootFolder);
					}
				}
			}
			this.rootFolder.Flags |= FolderMappingFlags.Root;
			if (this.GetWellKnownFolder(WellKnownFolderType.IpmSubtree) == null && !base.IsPublicFolderMailbox)
			{
				throw new FolderIsMissingPermanentException("IPM subtree");
			}
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000573C File Offset: 0x0000393C
		public void SetFolderFilter(string[] includeFolders, bool includeDumpster, string[] excludeFolders, string sourceRootFolder, bool isLivePublicFolderMailboxRestore, Guid sourceMailboxGuid)
		{
			if (this.folderFilterApplied)
			{
				throw new ArgumentException("SetFolderFilter was already called");
			}
			this.folderFilterApplied = true;
			if (includeFolders == null || includeFolders.Length == 0)
			{
				includeFolders = new string[]
				{
					"/*"
				};
			}
			foreach (string folderPath in includeFolders)
			{
				this.MarkFolderByPath(sourceRootFolder, folderPath, true);
			}
			if (includeDumpster)
			{
				FolderMapping wellKnownFolder = this.GetWellKnownFolder(WellKnownFolderType.Dumpster);
				if (wellKnownFolder != null)
				{
					wellKnownFolder.Flags = (FolderMappingFlags.Include | FolderMappingFlags.Inherit);
				}
			}
			if (excludeFolders != null)
			{
				foreach (string folderPath2 in excludeFolders)
				{
					this.MarkFolderByPath(sourceRootFolder, folderPath2, false);
				}
			}
			base.EnumerateFolderHierarchy(EnumHierarchyFlags.AllFolders, delegate(FolderRecWrapper fRec, FolderMap.EnumFolderContext ctx)
			{
				FolderMapping folderMapping = (FolderMapping)fRec;
				WellKnownFolderType wkftype = folderMapping.WKFType;
				switch (wkftype)
				{
				case WellKnownFolderType.FreeBusy:
				case WellKnownFolderType.OfflineAddressBook:
					break;
				default:
					if (wkftype != WellKnownFolderType.System)
					{
						switch (wkftype)
						{
						case WellKnownFolderType.DumpsterAdminAuditLogs:
						case WellKnownFolderType.DumpsterAudits:
						case WellKnownFolderType.SpoolerQueue:
						case WellKnownFolderType.TransportQueue:
						case WellKnownFolderType.MRSSyncStates:
							break;
						case WellKnownFolderType.ConversationActionSettings:
							return;
						default:
							return;
						}
					}
					break;
				}
				folderMapping.Flags &= ~FolderMappingFlags.Include;
				folderMapping.Flags |= (FolderMappingFlags.Exclude | FolderMappingFlags.Inherit);
			});
			bool hasIncludedFolders = false;
			base.EnumerateFolderHierarchy(EnumHierarchyFlags.AllFolders, delegate(FolderRecWrapper fRec, FolderMap.EnumFolderContext ctx)
			{
				FolderMapping folderMapping = (FolderMapping)fRec;
				if (this.IsPublicFolderMailbox && folderMapping.IsPublicFolderDumpster && !includeDumpster)
				{
					return;
				}
				folderMapping.ApplyInheritanceFlags();
				if (includeDumpster)
				{
					this.MarkPublicFolderDumpster(fRec);
				}
				if ((folderMapping.Flags & FolderMappingFlags.Include) != FolderMappingFlags.None)
				{
					hasIncludedFolders = true;
				}
			});
			if (isLivePublicFolderMailboxRestore)
			{
				this.CheckFolderRestorePossible(sourceMailboxGuid);
			}
			if (!hasIncludedFolders)
			{
				this.Warnings.Add(MrsStrings.NoFoldersIncluded);
			}
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00005860 File Offset: 0x00003A60
		public FolderMapping GetWellKnownFolder(WellKnownFolderType wkfType)
		{
			return this.wellKnownFolderMap.GetWellKnownFolder(wkfType);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x0000586E File Offset: 0x00003A6E
		public WellKnownFolderType GetWellKnownFolderType(byte[] entryId)
		{
			return this.wellKnownFolderMap.GetWellKnownFolderType(entryId);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000059F0 File Offset: 0x00003BF0
		public void ComputeFolderMapping(FolderHierarchy targetHierarchy, bool createMissingFolderRecs)
		{
			if (!this.folderFilterApplied)
			{
				this.SetFolderFilter(null, false, null, string.Empty, false, Guid.Empty);
			}
			EntryIdMap<FolderMapping> targetBySourceEntryID = new EntryIdMap<FolderMapping>();
			targetHierarchy.EnumerateSubtree(EnumHierarchyFlags.AllFolders, targetHierarchy.RootFolder, delegate(FolderRecWrapper fRec, FolderMap.EnumFolderContext ctx)
			{
				FolderMapping folderMapping = (FolderMapping)fRec;
				byte[] array = folderMapping.FolderRec[targetHierarchy.SourceEntryIDPtag] as byte[];
				if (array != null && !targetBySourceEntryID.ContainsKey(array))
				{
					targetBySourceEntryID.Add(array, folderMapping);
				}
			});
			base.EnumerateFolderHierarchy(EnumHierarchyFlags.AllFolders, delegate(FolderRecWrapper fRec, FolderMap.EnumFolderContext ctx)
			{
				FolderMapping folderMapping = (FolderMapping)fRec;
				FolderMapping wellKnownFolder;
				if (folderMapping.WKFType != WellKnownFolderType.None)
				{
					wellKnownFolder = targetHierarchy.GetWellKnownFolder(folderMapping.WKFType);
					if (wellKnownFolder != null)
					{
						folderMapping.TargetFolder = wellKnownFolder;
						wellKnownFolder.SourceFolder = folderMapping;
						this.MapPublicFolderDumpster(folderMapping, wellKnownFolder);
						return;
					}
				}
				if (targetBySourceEntryID.TryGetValue(folderMapping.EntryId, out wellKnownFolder))
				{
					folderMapping.TargetFolder = wellKnownFolder;
					wellKnownFolder.SourceFolder = folderMapping;
					this.MapPublicFolderDumpster(folderMapping, wellKnownFolder);
				}
			});
			base.EnumerateFolderHierarchy(EnumHierarchyFlags.AllFolders, delegate(FolderRecWrapper fRec, FolderMap.EnumFolderContext ctx)
			{
				FolderMapping folderMapping = (FolderMapping)fRec;
				if (folderMapping.TargetFolder != null)
				{
					this.MapSubtreeByName(folderMapping, createMissingFolderRecs);
					ctx.Result = EnumHierarchyResult.SkipSubtree;
				}
			});
			FolderMapping ipmSubtree = this.GetWellKnownFolder(WellKnownFolderType.IpmSubtree);
			if (ipmSubtree != null)
			{
				base.EnumerateFolderHierarchy(EnumHierarchyFlags.AllFolders, delegate(FolderRecWrapper fRec, FolderMap.EnumFolderContext ctx)
				{
					FolderMapping folderMapping = (FolderMapping)fRec;
					if (folderMapping.IsIncluded && folderMapping.TargetFolder == null)
					{
						this.MapFolderByName(folderMapping, ipmSubtree.TargetFolder, createMissingFolderRecs);
						if (folderMapping.TargetFolder != null)
						{
							this.MapSubtreeByName(folderMapping, createMissingFolderRecs);
						}
						ctx.Result = EnumHierarchyResult.SkipSubtree;
					}
				});
			}
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00005D90 File Offset: 0x00003F90
		public void CreateMissingFolders()
		{
			IDestinationMailbox destMailbox = (IDestinationMailbox)this.MbxWrapper.Mailbox;
			base.EnumerateFolderHierarchy(EnumHierarchyFlags.AllFolders, delegate(FolderRecWrapper fRec, FolderMap.EnumFolderContext ctx)
			{
				if (fRec.EntryId != null)
				{
					return;
				}
				FolderMapping fm = (FolderMapping)fRec;
				fm.FolderRec.ParentId = fm.ParentId;
				ExecutionContext.Create(new DataContext[]
				{
					new FolderRecWrapperDataContext(fRec)
				}).Execute(delegate
				{
					byte[] array;
					destMailbox.CreateFolder(fRec.FolderRec, CreateFolderFlags.FailIfExists, out array);
					fm.FolderRec.EntryId = array;
					this[array] = fm;
					if (fm.SourceFolder != null)
					{
						List<PropValueData> list = new List<PropValueData>();
						list.Add(new PropValueData(this.SourceEntryIDPtag, fm.SourceFolder.EntryId));
						list.Add(new PropValueData(this.SourceLastModifiedTimestampPtag, fm.SourceFolder.FolderRec.LastModifyTimestamp));
						if (fm.SourceFolder.WKFType != WellKnownFolderType.None)
						{
							list.Add(new PropValueData(this.SourceWKFTypePtag, (int)fm.SourceFolder.WKFType));
						}
						using (IDestinationFolder folder = destMailbox.GetFolder(array))
						{
							if (folder != null)
							{
								folder.SetProps(list.ToArray());
							}
							else
							{
								MrsTracer.Service.Warning("Something just deleted newly created folder from under us. Ignoring.", new object[0]);
							}
						}
						if (fm.SourceFolder.WKFType != WellKnownFolderType.None && this.GetWellKnownFolder(fm.SourceFolder.WKFType) == null)
						{
							this.SetWellKnownFolder(fm.SourceFolder.WKFType, fm);
							if (this.RootFolder == this.RootRec && (this.MbxWrapper.Flags & MailboxWrapperFlags.Archive) == (MailboxWrapperFlags)0 && (this.MbxWrapper.Flags & MailboxWrapperFlags.PST) == (MailboxWrapperFlags)0)
							{
								this.WriteWellKnownFolderReference(fm.SourceFolder.WKFType, fm.EntryId);
							}
						}
					}
				});
			});
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00005DD3 File Offset: 0x00003FD3
		internal EntryIdMap<WellKnownFolderType> GetEntryIdToWKFTMap()
		{
			return this.wellKnownFolderMap.GetEntryIdToWkfTypeMap();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00005DE0 File Offset: 0x00003FE0
		private FolderMapping FindFolderByPath(string folderRoot, string folderPath, bool createIfNeeded, out FolderMappingFlags parsedInheritanceFlags)
		{
			WellKnownFolderType wkfType;
			List<string> list;
			FolderFilterParser.Parse(folderPath, out wkfType, out list, out parsedInheritanceFlags);
			FolderMapping folderMapping = null;
			if (base.IsPublicFolderMailbox && this.rootFolder != null && (string.Compare(folderPath, "/*") == 0 || !string.IsNullOrEmpty(folderRoot)))
			{
				folderMapping = this.rootFolder;
			}
			else
			{
				folderMapping = this.GetWellKnownFolder(wkfType);
			}
			if (folderMapping != null)
			{
				foreach (string text in list)
				{
					FolderMapping folderMapping2 = (FolderMapping)folderMapping.FindChildByName(text, base.TargetMailboxCulture);
					if (folderMapping2 == null)
					{
						if (!createIfNeeded)
						{
							return null;
						}
						folderMapping2 = new FolderMapping(text, FolderType.Generic, null);
						folderMapping2.Parent = folderMapping;
					}
					folderMapping = folderMapping2;
				}
				return folderMapping;
			}
			return folderMapping;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00005EAC File Offset: 0x000040AC
		private void MarkFolderByPath(string folderRoot, string folderPath, bool isIncluded)
		{
			FolderMappingFlags folderMappingFlags;
			FolderMapping folderMapping = this.FindFolderByPath(folderRoot, folderPath, false, out folderMappingFlags);
			if (folderMapping == null)
			{
				this.Warnings.Add(MrsStrings.FolderIsMissing(folderPath));
				return;
			}
			if ((folderMapping.Flags & FolderMappingFlags.InclusionFlags) != FolderMappingFlags.None && (folderMapping.Flags & FolderMappingFlags.Inherit) == folderMappingFlags)
			{
				if (isIncluded != ((folderMapping.Flags & FolderMappingFlags.Include) != FolderMappingFlags.None))
				{
					this.warnings.Add(MrsStrings.FolderReferencedAsBothIncludedAndExcluded(folderPath));
				}
				else
				{
					this.warnings.Add(MrsStrings.FolderReferencedMoreThanOnce(folderPath));
				}
			}
			folderMapping.Flags |= ((isIncluded ? FolderMappingFlags.Include : FolderMappingFlags.Exclude) | folderMappingFlags);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00005F3C File Offset: 0x0000413C
		private void FindNamedFolders(NamedFolderMapping[] map)
		{
			foreach (NamedFolderMapping namedFolderMapping in map)
			{
				FolderMapping wellKnownFolder = this.GetWellKnownFolder(namedFolderMapping.ParentType);
				if (wellKnownFolder != null)
				{
					FolderMapping folder = wellKnownFolder.FindChildByName(namedFolderMapping.FolderName, base.TargetMailboxCulture) as FolderMapping;
					this.SetWellKnownFolder(namedFolderMapping.WKFType, folder);
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00005F98 File Offset: 0x00004198
		private void DiscoverWellKnownFolders()
		{
			this.wellKnownFolderMap.Clear();
			this.SetWellKnownFolder(WellKnownFolderType.Root, (FolderMapping)this.RootRec);
			List<WellKnownFolder> list = this.MbxWrapper.Mailbox.DiscoverWellKnownFolders((int)this.Flags);
			if (list != null)
			{
				foreach (WellKnownFolder wellKnownFolder in list)
				{
					FolderMapping folder = (FolderMapping)base[wellKnownFolder.EntryId];
					this.SetWellKnownFolder((WellKnownFolderType)wellKnownFolder.WKFType, folder);
				}
			}
			this.FindNamedFolders(FolderHierarchyUtils.NamedFolderRefs);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00006040 File Offset: 0x00004240
		private void SetWellKnownFolder(WellKnownFolderType wkfType, FolderMapping folder)
		{
			if (folder != null && (this.MbxWrapper.Flags & MailboxWrapperFlags.Archive) != (MailboxWrapperFlags)0)
			{
				foreach (WellKnownFolderType wellKnownFolderType in FolderHierarchy.ArchiveMovedFolders)
				{
					if (wellKnownFolderType == wkfType)
					{
						folder = null;
						break;
					}
				}
			}
			if (wkfType != WellKnownFolderType.None)
			{
				this.wellKnownFolderMap.Remove(wkfType);
				if (folder != null)
				{
					this.wellKnownFolderMap.Add(wkfType, folder);
				}
			}
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000060E0 File Offset: 0x000042E0
		private void MapSubtreeByName(FolderMapping root, bool createIfNeeded)
		{
			this.EnumerateSubtree(EnumHierarchyFlags.AllFolders, root, delegate(FolderRecWrapper fRec, FolderMap.EnumFolderContext ctx)
			{
				FolderMapping folderMapping = (FolderMapping)fRec;
				if (folderMapping.IsIncluded && folderMapping.TargetFolder == null)
				{
					this.MapFolderChainByName(folderMapping, createIfNeeded);
				}
			});
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00006118 File Offset: 0x00004318
		private void MapFolderChainByName(FolderMapping fm, bool createIfNeeded)
		{
			if (fm.TargetFolder != null)
			{
				return;
			}
			this.MapFolderChainByName((FolderMapping)fm.Parent, createIfNeeded);
			FolderMapping targetFolder = ((FolderMapping)fm.Parent).TargetFolder;
			if (targetFolder == null)
			{
				return;
			}
			this.MapFolderByName(fm, targetFolder, createIfNeeded);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00006160 File Offset: 0x00004360
		private void MapFolderByName(FolderMapping sourceFolder, FolderMapping parentTargetFolder, bool createIfNeeded)
		{
			FolderMapping folderMapping = (FolderMapping)parentTargetFolder.FindChildByName(sourceFolder.FolderName, base.TargetMailboxCulture);
			if (folderMapping == null)
			{
				if (!createIfNeeded)
				{
					return;
				}
				folderMapping = new FolderMapping(sourceFolder.FolderName, sourceFolder.FolderType, sourceFolder.FolderClass);
				folderMapping.Parent = parentTargetFolder;
			}
			sourceFolder.TargetFolder = folderMapping;
			folderMapping.SourceFolder = sourceFolder;
			this.MapPublicFolderDumpster(sourceFolder, folderMapping);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000061C4 File Offset: 0x000043C4
		private void MarkPublicFolderDumpster(FolderRecWrapper sourceFolder)
		{
			if (!base.IsPublicFolderMailbox)
			{
				return;
			}
			FolderMapping folderMapping = (FolderMapping)sourceFolder;
			if (!folderMapping.IsPublicFolderDumpster && ((folderMapping.Flags & FolderMappingFlags.Include) != FolderMappingFlags.None || (folderMapping.Flags & FolderMappingFlags.InheritedInclude) != FolderMappingFlags.None))
			{
				FolderMapping folderMapping2 = (FolderMapping)sourceFolder.PublicFolderDumpster;
				if (folderMapping2 != null)
				{
					folderMapping2.Flags |= FolderMappingFlags.Include;
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000621C File Offset: 0x0000441C
		private void MapPublicFolderDumpster(FolderMapping sourceFolder, FolderMapping targetFolder)
		{
			if (!base.IsPublicFolderMailbox)
			{
				return;
			}
			FolderMapping folderMapping = (FolderMapping)sourceFolder.PublicFolderDumpster;
			FolderMapping folderMapping2 = (FolderMapping)targetFolder.PublicFolderDumpster;
			if (folderMapping != null && folderMapping2 != null)
			{
				folderMapping.TargetFolder = folderMapping2;
				folderMapping2.SourceFolder = folderMapping;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x000062E8 File Offset: 0x000044E8
		private void CheckFolderRestorePossible(Guid sourceMailboxGuid)
		{
			base.EnumerateFolderHierarchy(EnumHierarchyFlags.AllFolders, delegate(FolderRecWrapper fRec, FolderMap.EnumFolderContext ctx)
			{
				FolderMapping folderMapping = (FolderMapping)fRec;
				if ((folderMapping.Flags & FolderMappingFlags.Include) != FolderMappingFlags.None || (folderMapping.Flags & FolderMappingFlags.InheritedInclude) != FolderMappingFlags.None)
				{
					byte[] array = (byte[])folderMapping.FolderRec[PropTag.ReplicaList];
					if (array != null)
					{
						string[] stringArrayFromBytes = ReplicaListProperty.GetStringArrayFromBytes(array);
						Guid empty = Guid.Empty;
						if (stringArrayFromBytes.Length > 0 && GuidHelper.TryParseGuid(stringArrayFromBytes[0], out empty) && empty == sourceMailboxGuid)
						{
							throw new FolderIsLivePermanentException(HexConverter.ByteArrayToHexString(folderMapping.EntryId));
						}
					}
				}
			});
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00006318 File Offset: 0x00004518
		private void WriteEntryIdReference(PropTagFolderMapping ptfm, byte[] folderId, FolderHierarchy.GetPropsDelegate getProps, FolderHierarchy.SetPropsDelegate setProps)
		{
			InboxNamedPropFolderMapping inboxNamedPropFolderMapping = ptfm as InboxNamedPropFolderMapping;
			PropTag propTag;
			if (inboxNamedPropFolderMapping != null)
			{
				propTag = this.MbxWrapper.NamedPropMapper.MapNamedProp(inboxNamedPropFolderMapping.NamedPropData, PropType.Binary);
			}
			else
			{
				propTag = ptfm.Ptag;
			}
			MrsTracer.Service.Debug("Writing WKF reference {0}: {1} -> {2}", new object[]
			{
				ptfm.WKFType,
				propTag,
				TraceUtils.DumpEntryId(folderId)
			});
			InboxIndexedFolderMapping inboxIndexedFolderMapping = ptfm as InboxIndexedFolderMapping;
			object value;
			if (inboxIndexedFolderMapping != null)
			{
				List<byte[]> list = new List<byte[]>();
				PropValueData[] array = getProps(new PropTag[]
				{
					propTag
				});
				byte[][] array2 = array[0].Value as byte[][];
				if (array2 != null)
				{
					list.AddRange(array2);
				}
				while (list.Count < inboxIndexedFolderMapping.EntryIndex + 1)
				{
					list.Add(Array<byte>.Empty);
				}
				list[inboxIndexedFolderMapping.EntryIndex] = folderId;
				value = list.ToArray();
			}
			else
			{
				value = folderId;
			}
			PropValueData[] pvda = new PropValueData[]
			{
				new PropValueData(propTag, value)
			};
			setProps(pvda);
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00006454 File Offset: 0x00004654
		private void WriteWellKnownFolderReference(WellKnownFolderType wkfType, byte[] folderId)
		{
			WellKnownFolderMapping wellKnownFolderMapping = FolderHierarchyUtils.FindWKFMapping(wkfType, (int)this.Flags);
			PropTagFolderMapping propTagFolderMapping = wellKnownFolderMapping as PropTagFolderMapping;
			if (propTagFolderMapping != null)
			{
				if (propTagFolderMapping is MailboxRootFolderMapping)
				{
					IDestinationMailbox mbx = this.MbxWrapper.Mailbox as IDestinationMailbox;
					this.WriteEntryIdReference(propTagFolderMapping, folderId, (PropTag[] pta) => mbx.GetProps(pta), delegate(PropValueData[] pvda)
					{
						mbx.SetProps(pvda);
					});
					return;
				}
				if (propTagFolderMapping is InboxFolderMapping)
				{
					this.WriteWellKnownFolderReferenceToFolder(WellKnownFolderType.Inbox, wkfType, propTagFolderMapping, folderId);
					this.WriteWellKnownFolderReferenceToFolder(WellKnownFolderType.NonIpmSubtree, wkfType, propTagFolderMapping, folderId);
					return;
				}
			}
			else
			{
				if (wellKnownFolderMapping is NamedFolderMapping)
				{
					return;
				}
				MrsTracer.Service.Warning("Unable to write WKF reference {0} -> {1}", new object[]
				{
					wkfType,
					TraceUtils.DumpEntryId(folderId)
				});
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00006534 File Offset: 0x00004734
		private void WriteWellKnownFolderReferenceToFolder(WellKnownFolderType targetFolderType, WellKnownFolderType wkfType, PropTagFolderMapping ptfm, byte[] folderId)
		{
			FolderMapping wellKnownFolder = this.GetWellKnownFolder(targetFolderType);
			if (wellKnownFolder == null && targetFolderType == WellKnownFolderType.NonIpmSubtree)
			{
				wellKnownFolder = this.GetWellKnownFolder(WellKnownFolderType.Root);
			}
			if (wellKnownFolder == null)
			{
				MrsTracer.Service.Warning("There's no {0} in the target mailbox, not writing WKF reference for {1}", new object[]
				{
					targetFolderType,
					wkfType
				});
			}
			IDestinationMailbox destinationMailbox = this.MbxWrapper.Mailbox as IDestinationMailbox;
			using (IDestinationFolder folder = destinationMailbox.GetFolder(wellKnownFolder.EntryId))
			{
				if (folder == null)
				{
					MrsTracer.Service.Error("{0} disappeared??? Not writing WKF reference for {1}", new object[]
					{
						targetFolderType,
						wkfType
					});
				}
				else
				{
					this.WriteEntryIdReference(ptfm, folderId, (PropTag[] pta) => folder.GetProps(pta), delegate(PropValueData[] pvda)
					{
						folder.SetProps(pvda);
					});
				}
			}
		}

		// Token: 0x04000030 RID: 48
		private static readonly WellKnownFolderType[] ArchiveMovedFolders = new WellKnownFolderType[]
		{
			WellKnownFolderType.Inbox,
			WellKnownFolderType.Calendar,
			WellKnownFolderType.Contacts,
			WellKnownFolderType.Drafts,
			WellKnownFolderType.Journal,
			WellKnownFolderType.Tasks,
			WellKnownFolderType.Notes,
			WellKnownFolderType.JunkEmail,
			WellKnownFolderType.Outbox,
			WellKnownFolderType.SentItems,
			WellKnownFolderType.ConversationActionSettings
		};

		// Token: 0x04000031 RID: 49
		private MailboxWrapper mbxWrapper;

		// Token: 0x04000032 RID: 50
		private List<LocalizedString> warnings;

		// Token: 0x04000033 RID: 51
		private FolderHierarchy.WellKnownFolderMap wellKnownFolderMap;

		// Token: 0x04000034 RID: 52
		private bool folderFilterApplied;

		// Token: 0x04000035 RID: 53
		private FolderMapping rootFolder;

		// Token: 0x02000012 RID: 18
		// (Invoke) Token: 0x060000A3 RID: 163
		internal delegate PropValueData[] GetPropsDelegate(PropTag[] pta);

		// Token: 0x02000013 RID: 19
		// (Invoke) Token: 0x060000A7 RID: 167
		internal delegate void SetPropsDelegate(PropValueData[] pvda);

		// Token: 0x02000014 RID: 20
		private class WellKnownFolderMap
		{
			// Token: 0x060000AA RID: 170 RVA: 0x00006698 File Offset: 0x00004898
			public void Reset()
			{
				foreach (FolderMapping folderMapping in this.wellKnownFolders.Values)
				{
					folderMapping.WKFType = WellKnownFolderType.None;
				}
			}

			// Token: 0x060000AB RID: 171 RVA: 0x000066F0 File Offset: 0x000048F0
			public void Clear()
			{
				this.wellKnownFolders.Clear();
				this.wellKnownFolderTypes.Clear();
			}

			// Token: 0x060000AC RID: 172 RVA: 0x00006708 File Offset: 0x00004908
			public FolderMapping GetWellKnownFolder(WellKnownFolderType wkfType)
			{
				FolderMapping result;
				if (this.wellKnownFolders.TryGetValue(wkfType, out result))
				{
					return result;
				}
				return null;
			}

			// Token: 0x060000AD RID: 173 RVA: 0x00006728 File Offset: 0x00004928
			public WellKnownFolderType GetWellKnownFolderType(byte[] entryId)
			{
				WellKnownFolderType result;
				if (this.wellKnownFolderTypes.TryGetValue(entryId, out result))
				{
					return result;
				}
				return WellKnownFolderType.None;
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00006748 File Offset: 0x00004948
			public void Remove(WellKnownFolderType wkfType)
			{
				FolderMapping wellKnownFolder = this.GetWellKnownFolder(wkfType);
				if (wellKnownFolder != null)
				{
					wellKnownFolder.WKFType = WellKnownFolderType.None;
					this.wellKnownFolders.Remove(wkfType);
					byte[] array = null;
					foreach (KeyValuePair<byte[], WellKnownFolderType> keyValuePair in this.wellKnownFolderTypes)
					{
						if (keyValuePair.Value == wkfType)
						{
							array = keyValuePair.Key;
							break;
						}
					}
					if (array != null)
					{
						this.wellKnownFolderTypes.Remove(array);
					}
				}
			}

			// Token: 0x060000AF RID: 175 RVA: 0x000067DC File Offset: 0x000049DC
			public void Add(WellKnownFolderType wkfType, FolderMapping fm)
			{
				fm.WKFType = wkfType;
				this.wellKnownFolders[wkfType] = fm;
				if (fm.EntryId != null)
				{
					this.wellKnownFolderTypes[fm.EntryId] = wkfType;
				}
			}

			// Token: 0x060000B0 RID: 176 RVA: 0x0000680C File Offset: 0x00004A0C
			public EntryIdMap<WellKnownFolderType> GetEntryIdToWkfTypeMap()
			{
				EntryIdMap<WellKnownFolderType> entryIdMap = new EntryIdMap<WellKnownFolderType>();
				foreach (KeyValuePair<byte[], WellKnownFolderType> keyValuePair in this.wellKnownFolderTypes)
				{
					entryIdMap[keyValuePair.Key] = keyValuePair.Value;
				}
				return entryIdMap;
			}

			// Token: 0x04000037 RID: 55
			private Dictionary<WellKnownFolderType, FolderMapping> wellKnownFolders = new Dictionary<WellKnownFolderType, FolderMapping>();

			// Token: 0x04000038 RID: 56
			private EntryIdMap<WellKnownFolderType> wellKnownFolderTypes = new EntryIdMap<WellKnownFolderType>();
		}
	}
}
