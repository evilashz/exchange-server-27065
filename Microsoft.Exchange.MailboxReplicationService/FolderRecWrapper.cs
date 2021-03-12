using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.AccessControl;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000017 RID: 23
	internal class FolderRecWrapper
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00006892 File Offset: 0x00004A92
		public FolderRecWrapper()
		{
			this.FolderRec = new FolderRec();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x000068A5 File Offset: 0x00004AA5
		public FolderRecWrapper(FolderRec folderRec)
		{
			this.FolderRec = new FolderRec(folderRec);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000068B9 File Offset: 0x00004AB9
		public FolderRecWrapper(FolderRecWrapper folderRecWrapper)
		{
			this.FolderRec = new FolderRec();
			this.CopyFrom(folderRecWrapper);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000068D3 File Offset: 0x00004AD3
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x000068DB File Offset: 0x00004ADB
		public FolderRec FolderRec { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000068E4 File Offset: 0x00004AE4
		public bool IsSpoolerQueue
		{
			get
			{
				return this.FullFolderName == "/Spooler Queue" || this.FullFolderName == "Spooler Queue";
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000690C File Offset: 0x00004B0C
		public bool IsPublicFolderDumpster
		{
			get
			{
				if (this.FolderRec[PropTag.TimeInServer] != null)
				{
					ELCFolderFlags elcfolderFlags = (ELCFolderFlags)this.FolderRec[PropTag.TimeInServer];
					return elcfolderFlags.HasFlag(ELCFolderFlags.DumpsterFolder);
				}
				return false;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00006958 File Offset: 0x00004B58
		public bool IsInternalAccess
		{
			get
			{
				object obj = this.FolderRec[PropTag.InternalAccess];
				return obj != null && obj is bool && (bool)obj;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00006989 File Offset: 0x00004B89
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00006991 File Offset: 0x00004B91
		public List<FolderRecWrapper> Children { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000699A File Offset: 0x00004B9A
		// (set) Token: 0x060000BD RID: 189 RVA: 0x000069A4 File Offset: 0x00004BA4
		public FolderRecWrapper Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				FolderRecWrapper folderRecWrapper = this.parent;
				if (folderRecWrapper == value)
				{
					return;
				}
				if (folderRecWrapper != null && folderRecWrapper.Children != null)
				{
					folderRecWrapper.Children.Remove(this);
				}
				this.parent = value;
				if (this.parent != null)
				{
					if (this.parent.Children == null)
					{
						this.parent.Children = new List<FolderRecWrapper>(0);
					}
					this.parent.Children.Add(this);
				}
				this.AfterParentChange(folderRecWrapper);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00006A1A File Offset: 0x00004C1A
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00006A22 File Offset: 0x00004C22
		public FolderRecWrapper PublicFolderDumpster
		{
			get
			{
				return this.publicFolderDumpster;
			}
			set
			{
				this.publicFolderDumpster = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00006A2B File Offset: 0x00004C2B
		public byte[] EntryId
		{
			get
			{
				return this.FolderRec.EntryId;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00006A38 File Offset: 0x00004C38
		public string FolderName
		{
			get
			{
				return this.FolderRec.FolderName;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00006A45 File Offset: 0x00004C45
		public FolderType FolderType
		{
			get
			{
				return this.FolderRec.FolderType;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00006A52 File Offset: 0x00004C52
		public string FolderClass
		{
			get
			{
				return this.FolderRec.FolderClass;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00006A5F File Offset: 0x00004C5F
		public byte[] ParentId
		{
			get
			{
				if (this.Parent != null)
				{
					return this.Parent.EntryId;
				}
				return this.FolderRec.ParentId;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00006A80 File Offset: 0x00004C80
		public string FullFolderName
		{
			get
			{
				if (this.Parent == null)
				{
					return this.FolderName;
				}
				return this.Parent.FullFolderName + "/" + this.FolderName;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00006AAC File Offset: 0x00004CAC
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00006AB4 File Offset: 0x00004CB4
		public RuleData[] Rules { get; private set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00006ABD File Offset: 0x00004CBD
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00006AC5 File Offset: 0x00004CC5
		public RestrictionData SearchFolderRestriction { get; private set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00006ACE File Offset: 0x00004CCE
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00006AD6 File Offset: 0x00004CD6
		public byte[][] SearchFolderScopeIDs { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00006ADF File Offset: 0x00004CDF
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00006AE7 File Offset: 0x00004CE7
		public SearchState SearchFolderState { get; private set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00006AF0 File Offset: 0x00004CF0
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00006AF8 File Offset: 0x00004CF8
		public RawSecurityDescriptor FolderNTSD { get; private set; }

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00006B01 File Offset: 0x00004D01
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00006B09 File Offset: 0x00004D09
		public RawSecurityDescriptor FolderFreeBusyNTSD { get; private set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00006B12 File Offset: 0x00004D12
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00006B1A File Offset: 0x00004D1A
		public PropValueData[][] FolderACL { get; private set; }

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00006B23 File Offset: 0x00004D23
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00006B2B File Offset: 0x00004D2B
		public PropValueData[][] FolderFreeBusyACL { get; private set; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00006B34 File Offset: 0x00004D34
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00006B3C File Offset: 0x00004D3C
		public FolderRecDataFlags LoadedData { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00006B45 File Offset: 0x00004D45
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00006B4D File Offset: 0x00004D4D
		public FolderRecDataFlags MappedData { get; private set; }

		// Token: 0x060000DA RID: 218 RVA: 0x00006B58 File Offset: 0x00004D58
		public static List<FolderRecWrapper> WrapList<TFolderRec>(List<FolderRec> input) where TFolderRec : FolderRecWrapper, new()
		{
			List<FolderRecWrapper> list = new List<FolderRecWrapper>(input.Count);
			foreach (FolderRec sourceRec in input)
			{
				TFolderRec tfolderRec = Activator.CreateInstance<TFolderRec>();
				tfolderRec.FolderRec.CopyFrom(sourceRec);
				list.Add(tfolderRec);
			}
			return list;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006BD4 File Offset: 0x00004DD4
		public bool FieldIsLoaded(FolderRecDataFlags field)
		{
			return (this.LoadedData & field) != FolderRecDataFlags.None;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00006BE4 File Offset: 0x00004DE4
		public bool FieldIsMapped(FolderRecDataFlags field)
		{
			return (this.MappedData & field) != FolderRecDataFlags.None;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006BF4 File Offset: 0x00004DF4
		public virtual void EnsureDataLoaded(IFolder folder, FolderRecDataFlags dataToLoad, MailboxCopierBase mbxCtx)
		{
			FolderRecDataFlags folderRecDataFlags = dataToLoad & FolderRecDataFlags.ExtendedData;
			if (folderRecDataFlags != FolderRecDataFlags.None && !this.FieldIsLoaded(folderRecDataFlags))
			{
				GetFolderRecFlags getFolderRecFlags = GetFolderRecFlags.None;
				if (this.FolderType == FolderType.Generic)
				{
					if ((dataToLoad & FolderRecDataFlags.PromotedProperties) != FolderRecDataFlags.None && !this.FieldIsLoaded(FolderRecDataFlags.PromotedProperties))
					{
						getFolderRecFlags |= GetFolderRecFlags.PromotedProperties;
					}
					if ((dataToLoad & FolderRecDataFlags.Restrictions) != FolderRecDataFlags.None && !this.FieldIsLoaded(FolderRecDataFlags.Restrictions))
					{
						getFolderRecFlags |= GetFolderRecFlags.Restrictions;
					}
				}
				if ((this.FolderType == FolderType.Generic || this.FolderType == FolderType.Search) && (dataToLoad & FolderRecDataFlags.Views) != FolderRecDataFlags.None && !this.FieldIsLoaded(FolderRecDataFlags.Views))
				{
					getFolderRecFlags |= GetFolderRecFlags.Views;
				}
				if (getFolderRecFlags != GetFolderRecFlags.None)
				{
					FolderRec folderRec = folder.GetFolderRec(null, getFolderRecFlags);
					this.FolderRec.SetPromotedProperties(folderRec.GetPromotedProperties());
					this.FolderRec.Views = folderRec.Views;
					this.FolderRec.ICSViews = folderRec.ICSViews;
					this.FolderRec.Restrictions = folderRec.Restrictions;
				}
				this.LoadedData |= folderRecDataFlags;
			}
			if ((dataToLoad & FolderRecDataFlags.SearchCriteria) != FolderRecDataFlags.None && !this.IsSpoolerQueue && !this.FieldIsLoaded(FolderRecDataFlags.SearchCriteria))
			{
				this.ReadSearchCriteria(folder, new Action<List<BadMessageRec>>(mbxCtx.ReportBadItems), new Func<byte[], IFolder>(mbxCtx.SourceMailboxWrapper.GetFolder));
				this.LoadedData |= FolderRecDataFlags.SearchCriteria;
			}
			if ((dataToLoad & FolderRecDataFlags.Rules) != FolderRecDataFlags.None && !this.FieldIsLoaded(FolderRecDataFlags.Rules))
			{
				this.ReadRules(folder, FolderRecWrapper.extraTags, new Action<List<BadMessageRec>>(mbxCtx.ReportBadItems), new Func<byte[], IFolder>(mbxCtx.SourceMailboxWrapper.GetFolder));
				this.LoadedData |= FolderRecDataFlags.Rules;
				MrsTracer.Service.Debug("Rules loaded: {0}", new object[]
				{
					new RulesDataContext(this.Rules)
				});
			}
			if ((dataToLoad & FolderRecDataFlags.SecurityDescriptors) != FolderRecDataFlags.None && !this.FieldIsLoaded(FolderRecDataFlags.SecurityDescriptors))
			{
				this.FolderNTSD = this.ReadSD(folder, SecurityProp.NTSD, new Action<List<BadMessageRec>>(mbxCtx.ReportBadItems), new Func<byte[], IFolder>(mbxCtx.SourceMailboxWrapper.GetFolder));
				this.FolderFreeBusyNTSD = this.ReadSD(folder, SecurityProp.FreeBusyNTSD, new Action<List<BadMessageRec>>(mbxCtx.ReportBadItems), new Func<byte[], IFolder>(mbxCtx.SourceMailboxWrapper.GetFolder));
				this.LoadedData |= FolderRecDataFlags.SecurityDescriptors;
				MrsTracer.Service.Debug("FolderSDs loaded: NTSD {0}, FreeBusyNTSD {1}", new object[]
				{
					CommonUtils.GetSDDLString(this.FolderNTSD),
					CommonUtils.GetSDDLString(this.FolderFreeBusyNTSD)
				});
			}
			if (dataToLoad.HasFlag(FolderRecDataFlags.FolderAcls) && !this.FieldIsLoaded(FolderRecDataFlags.FolderAcls))
			{
				this.FolderACL = this.ReadACL(folder, SecurityProp.NTSD, new Action<List<BadMessageRec>>(mbxCtx.ReportBadItems), new Func<byte[], IFolder>(mbxCtx.SourceMailboxWrapper.GetFolder));
				this.FolderFreeBusyACL = this.ReadACL(folder, SecurityProp.FreeBusyNTSD, new Action<List<BadMessageRec>>(mbxCtx.ReportBadItems), new Func<byte[], IFolder>(mbxCtx.SourceMailboxWrapper.GetFolder));
				this.LoadedData |= FolderRecDataFlags.FolderAcls;
				MrsTracer.Service.Debug("FolderAcls are loaded: ACL {0}, FreeBusyACL {1}", new object[]
				{
					new PropValuesDataContext(this.FolderACL).ToString(),
					new PropValuesDataContext(this.FolderFreeBusyACL).ToString()
				});
			}
			if (dataToLoad.HasFlag(FolderRecDataFlags.ExtendedAclInformation) && !this.FieldIsLoaded(FolderRecDataFlags.ExtendedAclInformation))
			{
				this.FolderACL = this.ReadExtendedAcl(folder, AclFlags.FolderAcl, new Action<List<BadMessageRec>>(mbxCtx.ReportBadItems), new Func<byte[], IFolder>(mbxCtx.SourceMailboxWrapper.GetFolder));
				this.FolderFreeBusyACL = this.ReadExtendedAcl(folder, AclFlags.FreeBusyAcl, new Action<List<BadMessageRec>>(mbxCtx.ReportBadItems), new Func<byte[], IFolder>(mbxCtx.SourceMailboxWrapper.GetFolder));
				this.LoadedData |= FolderRecDataFlags.ExtendedAclInformation;
				MrsTracer.Service.Debug("FolderExtendedAcls are loaded: Acl {0}, FreeBusyAcl {1}", new object[]
				{
					new PropValuesDataContext(this.FolderACL).ToString(),
					new PropValuesDataContext(this.FolderFreeBusyACL).ToString()
				});
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006FDC File Offset: 0x000051DC
		public void EnumerateMappableData(MailboxCopierBase mbxCopier)
		{
			if (this.FieldIsLoaded(FolderRecDataFlags.PromotedProperties) && !this.FieldIsMapped(FolderRecDataFlags.PromotedProperties))
			{
				mbxCopier.NamedPropTranslator.EnumeratePropTags(this.FolderRec.GetPromotedProperties());
			}
			if (this.FieldIsLoaded(FolderRecDataFlags.Views) && !this.FieldIsMapped(FolderRecDataFlags.Views) && this.FolderRec.Views != null)
			{
				foreach (SortOrderData sortOrder in this.FolderRec.Views)
				{
					mbxCopier.NamedPropTranslator.EnumerateSortOrder(sortOrder);
				}
			}
			if (this.FieldIsLoaded(FolderRecDataFlags.Restrictions) && !this.FieldIsMapped(FolderRecDataFlags.Restrictions) && this.FolderRec.Restrictions != null)
			{
				foreach (RestrictionData rest in this.FolderRec.Restrictions)
				{
					mbxCopier.NamedPropTranslator.EnumerateRestriction(this.FolderRec, BadItemKind.CorruptFolderRestriction, rest);
				}
			}
			if (this.FieldIsLoaded(FolderRecDataFlags.SearchCriteria) && !this.FieldIsMapped(FolderRecDataFlags.SearchCriteria) && this.FolderType == FolderType.Search)
			{
				mbxCopier.NamedPropTranslator.EnumerateRestriction(this.FolderRec, BadItemKind.CorruptSearchFolderCriteria, this.SearchFolderRestriction);
			}
			if (mbxCopier.PrincipalTranslator != null && this.FieldIsLoaded(FolderRecDataFlags.SecurityDescriptors) && !this.FieldIsMapped(FolderRecDataFlags.SecurityDescriptors))
			{
				mbxCopier.PrincipalTranslator.EnumerateSecurityDescriptor(this.FolderNTSD);
				mbxCopier.PrincipalTranslator.EnumerateSecurityDescriptor(this.FolderFreeBusyNTSD);
			}
			if (this.FieldIsLoaded(FolderRecDataFlags.Rules) && !this.FieldIsMapped(FolderRecDataFlags.Rules))
			{
				mbxCopier.NamedPropTranslator.EnumerateRules(this.Rules);
				if (mbxCopier.PrincipalTranslator != null)
				{
					mbxCopier.PrincipalTranslator.EnumerateRules(this.Rules);
				}
			}
			if (mbxCopier.PrincipalTranslator != null && ((this.FieldIsLoaded(FolderRecDataFlags.FolderAcls) && !this.FieldIsMapped(FolderRecDataFlags.FolderAcls)) || (this.FieldIsLoaded(FolderRecDataFlags.ExtendedAclInformation) && !this.FieldIsMapped(FolderRecDataFlags.ExtendedAclInformation))))
			{
				mbxCopier.PrincipalTranslator.EnumerateFolderACL(this.FolderACL);
				mbxCopier.PrincipalTranslator.EnumerateFolderACL(this.FolderFreeBusyACL);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000071BC File Offset: 0x000053BC
		public void TranslateMappableData(MailboxCopierBase mbxCopier)
		{
			if (this.FieldIsLoaded(FolderRecDataFlags.PromotedProperties) && !this.FieldIsMapped(FolderRecDataFlags.PromotedProperties))
			{
				PropTag[] promotedProperties = this.FolderRec.GetPromotedProperties();
				mbxCopier.NamedPropTranslator.TranslatePropTags(promotedProperties);
				this.FolderRec.SetPromotedProperties(promotedProperties);
				this.MappedData |= FolderRecDataFlags.PromotedProperties;
			}
			if (this.FieldIsLoaded(FolderRecDataFlags.Views) && !this.FieldIsMapped(FolderRecDataFlags.Views) && this.FolderRec.Views != null)
			{
				foreach (SortOrderData so in this.FolderRec.Views)
				{
					mbxCopier.NamedPropTranslator.TranslateSortOrder(so);
				}
				this.MappedData |= FolderRecDataFlags.Views;
			}
			if (this.FieldIsLoaded(FolderRecDataFlags.Restrictions) && !this.FieldIsMapped(FolderRecDataFlags.Restrictions) && this.FolderRec.Restrictions != null)
			{
				foreach (RestrictionData restrictionData in this.FolderRec.Restrictions)
				{
					mbxCopier.NamedPropTranslator.TranslateRestriction(restrictionData);
					if (mbxCopier.FolderIdTranslator != null)
					{
						mbxCopier.FolderIdTranslator.TranslateRestriction(restrictionData);
					}
				}
				this.MappedData |= FolderRecDataFlags.Restrictions;
			}
			if (this.FieldIsLoaded(FolderRecDataFlags.SearchCriteria) && !this.FieldIsMapped(FolderRecDataFlags.SearchCriteria))
			{
				if (this.FolderType == FolderType.Search)
				{
					mbxCopier.NamedPropTranslator.TranslateRestriction(this.SearchFolderRestriction);
					if (mbxCopier.FolderIdTranslator != null)
					{
						mbxCopier.FolderIdTranslator.TranslateRestriction(this.SearchFolderRestriction);
						this.SearchFolderScopeIDs = mbxCopier.FolderIdTranslator.TranslateFolderIds(this.SearchFolderScopeIDs);
					}
				}
				this.MappedData |= FolderRecDataFlags.SearchCriteria;
			}
			if (mbxCopier.PrincipalTranslator != null && this.FieldIsLoaded(FolderRecDataFlags.SecurityDescriptors) && !this.FieldIsMapped(FolderRecDataFlags.SecurityDescriptors))
			{
				mbxCopier.PrincipalTranslator.TranslateSecurityDescriptor(this.FolderNTSD, TranslateSecurityDescriptorFlags.None);
				mbxCopier.PrincipalTranslator.TranslateSecurityDescriptor(this.FolderFreeBusyNTSD, TranslateSecurityDescriptorFlags.None);
				this.MappedData |= FolderRecDataFlags.SecurityDescriptors;
			}
			if (this.Rules != null && this.FieldIsLoaded(FolderRecDataFlags.Rules) && !this.FieldIsMapped(FolderRecDataFlags.Rules))
			{
				mbxCopier.NamedPropTranslator.TranslateRules(this.Rules);
				if (!mbxCopier.SourceMailbox.IsCapabilitySupported(MRSProxyCapabilities.InMailboxExternalRules) && mbxCopier.DestMailbox.IsCapabilitySupported(MRSProxyCapabilities.InMailboxExternalRules))
				{
					this.PatchRulesForDownlevelSources(this.Rules, mbxCopier.GetSourceFolderMap(GetFolderMapFlags.None));
				}
				if (mbxCopier.SourceMailbox.IsCapabilitySupported(MRSProxyCapabilities.InMailboxExternalRules) && !mbxCopier.DestMailbox.IsCapabilitySupported(MRSProxyCapabilities.InMailboxExternalRules))
				{
					this.PatchRulesForDownlevelDestinations(this.Rules);
				}
				if (mbxCopier.FolderIdTranslator != null)
				{
					mbxCopier.FolderIdTranslator.TranslateRules(this.Rules);
				}
				if (mbxCopier.PrincipalTranslator != null)
				{
					mbxCopier.PrincipalTranslator.TranslateRules(this.Rules);
				}
				this.MappedData |= FolderRecDataFlags.Rules;
			}
			if (mbxCopier.PrincipalTranslator != null && ((this.FieldIsLoaded(FolderRecDataFlags.FolderAcls) && !this.FieldIsMapped(FolderRecDataFlags.FolderAcls)) || (this.FieldIsLoaded(FolderRecDataFlags.ExtendedAclInformation) && !this.FieldIsMapped(FolderRecDataFlags.ExtendedAclInformation))))
			{
				mbxCopier.PrincipalTranslator.TranslateFolderACL(this.FolderACL);
				mbxCopier.PrincipalTranslator.TranslateFolderACL(this.FolderFreeBusyACL);
				this.MappedData |= (this.FieldIsLoaded(FolderRecDataFlags.ExtendedAclInformation) ? FolderRecDataFlags.ExtendedAclInformation : FolderRecDataFlags.FolderAcls);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000074E4 File Offset: 0x000056E4
		public virtual void CopyFrom(FolderRecWrapper sourceRec)
		{
			this.FolderRec.CopyFrom(sourceRec.FolderRec);
			this.Rules = sourceRec.Rules;
			this.FolderNTSD = sourceRec.FolderNTSD;
			this.FolderFreeBusyNTSD = sourceRec.FolderFreeBusyNTSD;
			this.FolderACL = sourceRec.FolderACL;
			this.FolderFreeBusyACL = sourceRec.FolderFreeBusyACL;
			this.SearchFolderRestriction = sourceRec.SearchFolderRestriction;
			this.SearchFolderScopeIDs = sourceRec.SearchFolderScopeIDs;
			this.SearchFolderState = sourceRec.SearchFolderState;
			this.LoadedData = sourceRec.LoadedData;
			this.MappedData = sourceRec.MappedData;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000757A File Offset: 0x0000577A
		public Guid GetContentMailboxGuid(Func<string, Guid> getContentMailboxGuidDelegate)
		{
			if (this.contentMailboxGuid == Guid.Empty)
			{
				this.contentMailboxGuid = getContentMailboxGuidDelegate(this.FullFolderName);
			}
			return this.contentMailboxGuid;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000075A6 File Offset: 0x000057A6
		public bool IsTargetMailbox(Guid mailboxGuid, Func<string, HashSet<Guid>> getTargetMailboxesDelegate)
		{
			if (this.pfTargetMailboxes == null)
			{
				this.pfTargetMailboxes = (getTargetMailboxesDelegate(this.FullFolderName) ?? FolderRecWrapper.EmptyHashSet);
			}
			return this.pfTargetMailboxes.Contains(mailboxGuid);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000075D7 File Offset: 0x000057D7
		public virtual bool AreRulesSupported()
		{
			return this.FolderType != FolderType.Search;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000075E8 File Offset: 0x000057E8
		public FolderRecWrapper FindChildByName(string subfolderName, CultureInfo cultureInfo)
		{
			if (this.Children != null)
			{
				foreach (FolderRecWrapper folderRecWrapper in this.Children)
				{
					if (cultureInfo != null)
					{
						if (string.Compare(subfolderName, folderRecWrapper.FolderName, cultureInfo, CompareOptions.IgnoreCase) == 0)
						{
							return folderRecWrapper;
						}
					}
					else if (string.Compare(subfolderName, folderRecWrapper.FolderName, StringComparison.OrdinalIgnoreCase) == 0)
					{
						return folderRecWrapper;
					}
				}
			}
			return null;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000766C File Offset: 0x0000586C
		public bool IsChildOf(FolderRecWrapper anotherFolder)
		{
			for (FolderRecWrapper folderRecWrapper = this; folderRecWrapper != null; folderRecWrapper = folderRecWrapper.Parent)
			{
				if (folderRecWrapper == anotherFolder)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007710 File Offset: 0x00005910
		public void WriteRules(IDestinationFolder targetFolder, Action<List<BadMessageRec>> reportBadItemsDelegate)
		{
			if (this.FolderType == FolderType.Search)
			{
				return;
			}
			CommonUtils.ProcessKnownExceptions(delegate
			{
				targetFolder.SetRules(this.Rules);
			}, delegate(Exception ex)
			{
				if (reportBadItemsDelegate != null && CommonUtils.ExceptionIsAny(ex, new WellKnownException[]
				{
					WellKnownException.DataProviderPermanent,
					WellKnownException.MapiNotEnoughMemory
				}))
				{
					List<BadMessageRec> list = new List<BadMessageRec>(1);
					list.Add(BadMessageRec.Folder(this.FolderRec, BadItemKind.CorruptFolderRule, ex));
					reportBadItemsDelegate(list);
					return true;
				}
				return false;
			});
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000775F File Offset: 0x0000595F
		public override string ToString()
		{
			return this.FullFolderName;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007767 File Offset: 0x00005967
		protected virtual void AfterParentChange(FolderRecWrapper previousParent)
		{
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000780C File Offset: 0x00005A0C
		private T ReadFolderData<T>(Func<T> getDataDelegate, Action<List<BadMessageRec>> reportBadItemsDelegate, Func<byte[], IFolder> openFolderDelegate) where T : class
		{
			T result = default(T);
			CommonUtils.ProcessKnownExceptions(delegate
			{
				CommonUtils.TreatMissingFolderAsTransient(delegate
				{
					result = getDataDelegate();
				}, this.EntryId, openFolderDelegate);
			}, delegate(Exception ex)
			{
				if (reportBadItemsDelegate != null && CommonUtils.ExceptionIsAny(ex, new WellKnownException[]
				{
					WellKnownException.DataProviderPermanent,
					WellKnownException.CorruptData,
					WellKnownException.NonCanonicalACL
				}))
				{
					List<BadMessageRec> list = new List<BadMessageRec>(1);
					list.Add(BadMessageRec.Folder(this.FolderRec, BadItemKind.CorruptFolderACL, ex));
					reportBadItemsDelegate(list);
					return true;
				}
				return false;
			});
			return result;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000078D8 File Offset: 0x00005AD8
		private void ReadSearchCriteria(IFolder folder, Action<List<BadMessageRec>> reportBadItemsDelegate, Func<byte[], IFolder> openFolderDelegate)
		{
			if (this.FolderType != FolderType.Search)
			{
				return;
			}
			this.SearchFolderRestriction = this.ReadFolderData<RestrictionData>(delegate
			{
				RestrictionData restrictionData;
				byte[][] array;
				SearchState searchState;
				folder.GetSearchCriteria(out restrictionData, out array, out searchState);
				this.SearchFolderScopeIDs = array;
				this.SearchFolderState = searchState;
				MrsTracer.Service.Debug("Search criteria loaded: {0}, scopes: {1}, state {2}", new object[]
				{
					restrictionData,
					new EntryIDsDataContext(array),
					searchState
				});
				return restrictionData;
			}, reportBadItemsDelegate, openFolderDelegate);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00007938 File Offset: 0x00005B38
		private void ReadRules(IFolder folder, PropTag[] extraPtags, Action<List<BadMessageRec>> reportBadItemsDelegate, Func<byte[], IFolder> openFolderDelegate)
		{
			this.Rules = Array<RuleData>.Empty;
			if (this.FolderType == FolderType.Search)
			{
				return;
			}
			this.Rules = this.ReadFolderData<RuleData[]>(() => folder.GetRules(extraPtags), reportBadItemsDelegate, openFolderDelegate);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000079A4 File Offset: 0x00005BA4
		private RawSecurityDescriptor ReadSD(IFolder folder, SecurityProp secProp, Action<List<BadMessageRec>> reportBadItemsDelegate, Func<byte[], IFolder> openFolderDelegate)
		{
			return this.ReadFolderData<RawSecurityDescriptor>(() => folder.GetSecurityDescriptor(secProp), reportBadItemsDelegate, openFolderDelegate);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000079F8 File Offset: 0x00005BF8
		private PropValueData[][] ReadACL(IFolder folder, SecurityProp secProp, Action<List<BadMessageRec>> reportBadItemsDelegate, Func<byte[], IFolder> openFolderDelegate)
		{
			return this.ReadFolderData<PropValueData[][]>(() => folder.GetACL(secProp), reportBadItemsDelegate, openFolderDelegate);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00007A4C File Offset: 0x00005C4C
		private PropValueData[][] ReadExtendedAcl(IFolder folder, AclFlags flags, Action<List<BadMessageRec>> reportBadItemsDelegate, Func<byte[], IFolder> openFolderDelegate)
		{
			return this.ReadFolderData<PropValueData[][]>(() => folder.GetExtendedAcl(flags), reportBadItemsDelegate, openFolderDelegate);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x00007AA0 File Offset: 0x00005CA0
		private void PatchRulesForDownlevelSources(RuleData[] rules, FolderMap sourceFolderMap)
		{
			RuleData.ConvertRulesToUplevel(rules, (byte[] entryID) => sourceFolderMap[entryID] != null);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00007ACC File Offset: 0x00005CCC
		private void PatchRulesForDownlevelDestinations(RuleData[] rules)
		{
			RuleData.ConvertRulesToDownlevel(rules);
		}

		// Token: 0x04000043 RID: 67
		private static PropTag[] extraTags = new PropTag[]
		{
			PropTag.ReportTime,
			(PropTag)1627389955U,
			(PropTag)1627455491U
		};

		// Token: 0x04000044 RID: 68
		public static readonly HashSet<Guid> EmptyHashSet = new HashSet<Guid>();

		// Token: 0x04000045 RID: 69
		private FolderRecWrapper parent;

		// Token: 0x04000046 RID: 70
		private FolderRecWrapper publicFolderDumpster;

		// Token: 0x04000047 RID: 71
		private Guid contentMailboxGuid;

		// Token: 0x04000048 RID: 72
		private HashSet<Guid> pfTargetMailboxes;
	}
}
