using System;
using System.Text;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000018 RID: 24
	internal class FolderMapping : FolderRecWrapper
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00007B10 File Offset: 0x00005D10
		public FolderMapping()
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007B18 File Offset: 0x00005D18
		public FolderMapping(FolderRec folderRec) : base(folderRec)
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007B24 File Offset: 0x00005D24
		public FolderMapping(string folderName, FolderType folderType, string folderClass)
		{
			base.FolderRec.FolderName = folderName;
			base.FolderRec.FolderType = folderType;
			base.FolderRec.FolderClass = folderClass;
			this.WKFType = WellKnownFolderType.None;
			this.Flags = FolderMappingFlags.None;
			this.SourceFolder = null;
			this.TargetFolder = null;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00007B77 File Offset: 0x00005D77
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00007B7F File Offset: 0x00005D7F
		public WellKnownFolderType WKFType { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00007B88 File Offset: 0x00005D88
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00007B90 File Offset: 0x00005D90
		public FolderMappingFlags Flags { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x00007B99 File Offset: 0x00005D99
		// (set) Token: 0x060000FA RID: 250 RVA: 0x00007BA1 File Offset: 0x00005DA1
		public FolderMapping SourceFolder { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00007BAA File Offset: 0x00005DAA
		// (set) Token: 0x060000FC RID: 252 RVA: 0x00007BB2 File Offset: 0x00005DB2
		public FolderMapping TargetFolder { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00007BBB File Offset: 0x00005DBB
		public bool IsIncluded
		{
			get
			{
				return (this.Flags & FolderMappingFlags.Exclude) == FolderMappingFlags.None && ((this.Flags & FolderMappingFlags.Include) != FolderMappingFlags.None || ((this.Flags & FolderMappingFlags.InheritedExclude) == FolderMappingFlags.None && (this.Flags & FolderMappingFlags.InheritedInclude) != FolderMappingFlags.None));
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00007BF0 File Offset: 0x00005DF0
		public new string FullFolderName
		{
			get
			{
				if (this.WKFType != WellKnownFolderType.None)
				{
					return string.Format("{0} [{1}]", base.FullFolderName, this.WKFType);
				}
				return base.FullFolderName;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00007C1C File Offset: 0x00005E1C
		public bool IsLegacyPublicFolder
		{
			get
			{
				WellKnownFolderType wkftype = this.WKFType;
				switch (wkftype)
				{
				case WellKnownFolderType.FreeBusy:
				case WellKnownFolderType.OfflineAddressBook:
					break;
				default:
					if (wkftype != WellKnownFolderType.FreeBusyData)
					{
						switch (wkftype)
						{
						case WellKnownFolderType.SchemaRoot:
						case WellKnownFolderType.EventsRoot:
							break;
						default:
							return this.FullFolderName.StartsWith("Public Root/NON_IPM_SUBTREE/StoreEvents") || this.FullFolderName.StartsWith("Public Root/NON_IPM_SUBTREE/OWAScratchPad");
						}
					}
					break;
				}
				return true;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00007C84 File Offset: 0x00005E84
		public bool IsSystemPublicFolder
		{
			get
			{
				WellKnownFolderType wkftype = this.WKFType;
				switch (wkftype)
				{
				case WellKnownFolderType.Root:
				case WellKnownFolderType.NonIpmSubtree:
				case WellKnownFolderType.IpmSubtree:
				case WellKnownFolderType.EFormsRegistry:
					break;
				default:
					switch (wkftype)
					{
					case WellKnownFolderType.PublicFolderDumpsterRoot:
					case WellKnownFolderType.PublicFolderTombstonesRoot:
					case WellKnownFolderType.PublicFolderAsyncDeleteState:
					case WellKnownFolderType.PublicFolderInternalSubmission:
						break;
					default:
						return false;
					}
					break;
				}
				return true;
			}
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00007CCE File Offset: 0x00005ECE
		public override bool AreRulesSupported()
		{
			return base.AreRulesSupported() && !FolderFilterParser.IsDumpster(this.WKFType);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00007CE8 File Offset: 0x00005EE8
		public void ApplyInheritanceFlags()
		{
			this.Flags &= ~(FolderMappingFlags.InheritedInclude | FolderMappingFlags.InheritedExclude);
			if ((this.Flags & FolderMappingFlags.Inherit) == FolderMappingFlags.None)
			{
				FolderMapping folderMapping = (FolderMapping)base.Parent;
				FolderMappingFlags folderMappingFlags = (folderMapping != null) ? folderMapping.Flags : FolderMappingFlags.None;
				if ((folderMappingFlags & FolderMappingFlags.InheritedInclude) != FolderMappingFlags.None)
				{
					this.Flags |= FolderMappingFlags.InheritedInclude;
				}
				if ((folderMappingFlags & FolderMappingFlags.InheritedExclude) != FolderMappingFlags.None)
				{
					this.Flags |= FolderMappingFlags.InheritedExclude;
				}
				return;
			}
			if ((this.Flags & FolderMappingFlags.Exclude) != FolderMappingFlags.None)
			{
				this.Flags |= FolderMappingFlags.InheritedExclude;
				return;
			}
			this.Flags |= FolderMappingFlags.InheritedInclude;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00007D7C File Offset: 0x00005F7C
		public override void EnsureDataLoaded(IFolder folder, FolderRecDataFlags dataToLoad, MailboxCopierBase mbxCtx)
		{
			if (mbxCtx.IsPublicFolderMigration)
			{
				switch (this.WKFType)
				{
				case WellKnownFolderType.Root:
				case WellKnownFolderType.NonIpmSubtree:
				case WellKnownFolderType.IpmSubtree:
				case WellKnownFolderType.EFormsRegistry:
					dataToLoad &= ~(FolderRecDataFlags.SecurityDescriptors | FolderRecDataFlags.FolderAcls | FolderRecDataFlags.ExtendedAclInformation);
					break;
				default:
					if (this.IsLegacyPublicFolder)
					{
						dataToLoad &= ~(FolderRecDataFlags.SecurityDescriptors | FolderRecDataFlags.FolderAcls | FolderRecDataFlags.ExtendedAclInformation);
					}
					break;
				}
			}
			base.EnsureDataLoaded(folder, dataToLoad, mbxCtx);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00007DD8 File Offset: 0x00005FD8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.FullFolderName);
			if (this.Flags != FolderMappingFlags.None)
			{
				stringBuilder.AppendFormat(" {0}", this.Flags);
			}
			if (this.TargetFolder != null)
			{
				stringBuilder.AppendFormat(" -> {0}", this.TargetFolder.ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00007E35 File Offset: 0x00006035
		protected override void AfterParentChange(FolderRecWrapper previousParent)
		{
			this.ApplyInheritanceFlags();
		}

		// Token: 0x04000055 RID: 85
		private const string StoreEventsPath = "Public Root/NON_IPM_SUBTREE/StoreEvents";

		// Token: 0x04000056 RID: 86
		private const string OWAScratchpadPath = "Public Root/NON_IPM_SUBTREE/OWAScratchPad";
	}
}
