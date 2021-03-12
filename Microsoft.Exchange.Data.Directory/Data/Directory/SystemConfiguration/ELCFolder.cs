using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200041E RID: 1054
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ELCFolder : ADConfigurationObject
	{
		// Token: 0x06002F6A RID: 12138 RVA: 0x000BFD30 File Offset: 0x000BDF30
		internal ELCFolder(IConfigurationSession session, string name)
		{
			this.m_Session = session;
			base.SetId(session, name);
		}

		// Token: 0x06002F6B RID: 12139 RVA: 0x000BFD47 File Offset: 0x000BDF47
		public ELCFolder()
		{
		}

		// Token: 0x06002F6C RID: 12140 RVA: 0x000BFD4F File Offset: 0x000BDF4F
		internal static bool FolderTypeAllowsComments(ElcFolderType folderType)
		{
			return folderType != ElcFolderType.Calendar && folderType != ElcFolderType.Contacts && folderType != ElcFolderType.Notes && folderType != ElcFolderType.Journal && folderType != ElcFolderType.Tasks;
		}

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x06002F6D RID: 12141 RVA: 0x000BFD69 File Offset: 0x000BDF69
		internal override ADObjectSchema Schema
		{
			get
			{
				return ELCFolder.schema;
			}
		}

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x06002F6E RID: 12142 RVA: 0x000BFD70 File Offset: 0x000BDF70
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ELCFolder.mostDerivedClass;
			}
		}

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x06002F6F RID: 12143 RVA: 0x000BFD77 File Offset: 0x000BDF77
		internal override ADObjectId ParentPath
		{
			get
			{
				return ELCFolder.parentPath;
			}
		}

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x06002F70 RID: 12144 RVA: 0x000BFD7E File Offset: 0x000BDF7E
		// (set) Token: 0x06002F71 RID: 12145 RVA: 0x000BFD90 File Offset: 0x000BDF90
		public ElcFolderType FolderType
		{
			get
			{
				return (ElcFolderType)this[ELCFolderSchema.FolderType];
			}
			set
			{
				this[ELCFolderSchema.FolderType] = value;
			}
		}

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x06002F72 RID: 12146 RVA: 0x000BFDA3 File Offset: 0x000BDFA3
		public ElcFolderCategory Description
		{
			get
			{
				return (ElcFolderCategory)this[ELCFolderSchema.Description];
			}
		}

		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x06002F73 RID: 12147 RVA: 0x000BFDB5 File Offset: 0x000BDFB5
		// (set) Token: 0x06002F74 RID: 12148 RVA: 0x000BFDC7 File Offset: 0x000BDFC7
		[Parameter(Mandatory = false)]
		public string FolderName
		{
			get
			{
				return (string)this[ELCFolderSchema.FolderName];
			}
			set
			{
				this[ELCFolderSchema.FolderName] = value;
			}
		}

		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x06002F75 RID: 12149 RVA: 0x000BFDD5 File Offset: 0x000BDFD5
		// (set) Token: 0x06002F76 RID: 12150 RVA: 0x000BFDE7 File Offset: 0x000BDFE7
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> LocalizedFolderName
		{
			get
			{
				return (MultiValuedProperty<string>)this[ELCFolderSchema.LocalizedFolderName];
			}
			set
			{
				this[ELCFolderSchema.LocalizedFolderName] = value;
				this.locNameMap = null;
			}
		}

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x06002F77 RID: 12151 RVA: 0x000BFDFC File Offset: 0x000BDFFC
		// (set) Token: 0x06002F78 RID: 12152 RVA: 0x000BFE0E File Offset: 0x000BE00E
		[Parameter(Mandatory = false)]
		public Unlimited<ByteQuantifiedSize> StorageQuota
		{
			get
			{
				return (Unlimited<ByteQuantifiedSize>)this[ELCFolderSchema.StorageQuota];
			}
			set
			{
				this[ELCFolderSchema.StorageQuota] = value;
			}
		}

		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x06002F79 RID: 12153 RVA: 0x000BFE21 File Offset: 0x000BE021
		// (set) Token: 0x06002F7A RID: 12154 RVA: 0x000BFE33 File Offset: 0x000BE033
		[Parameter(Mandatory = false)]
		public string Comment
		{
			get
			{
				return (string)this[ELCFolderSchema.Comment];
			}
			set
			{
				this[ELCFolderSchema.Comment] = value;
			}
		}

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x06002F7B RID: 12155 RVA: 0x000BFE41 File Offset: 0x000BE041
		// (set) Token: 0x06002F7C RID: 12156 RVA: 0x000BFE53 File Offset: 0x000BE053
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> LocalizedComment
		{
			get
			{
				return (MultiValuedProperty<string>)this[ELCFolderSchema.LocalizedComment];
			}
			set
			{
				this[ELCFolderSchema.LocalizedComment] = value;
				this.locCommentMap = null;
			}
		}

		// Token: 0x17000D8C RID: 3468
		// (get) Token: 0x06002F7D RID: 12157 RVA: 0x000BFE68 File Offset: 0x000BE068
		// (set) Token: 0x06002F7E RID: 12158 RVA: 0x000BFE7A File Offset: 0x000BE07A
		[Parameter(Mandatory = false)]
		public bool MustDisplayCommentEnabled
		{
			get
			{
				return (bool)this[ELCFolderSchema.MustDisplayComment];
			}
			set
			{
				this[ELCFolderSchema.MustDisplayComment] = value;
			}
		}

		// Token: 0x17000D8D RID: 3469
		// (get) Token: 0x06002F7F RID: 12159 RVA: 0x000BFE8D File Offset: 0x000BE08D
		// (set) Token: 0x06002F80 RID: 12160 RVA: 0x000BFE9F File Offset: 0x000BE09F
		[Parameter(Mandatory = false)]
		public bool BaseFolderOnly
		{
			get
			{
				return (bool)this[ELCFolderSchema.BaseFolderOnly];
			}
			set
			{
				this[ELCFolderSchema.BaseFolderOnly] = value;
			}
		}

		// Token: 0x17000D8E RID: 3470
		// (get) Token: 0x06002F81 RID: 12161 RVA: 0x000BFEB2 File Offset: 0x000BE0B2
		public MultiValuedProperty<ADObjectId> TemplateIds
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ELCFolderSchema.TemplateIds];
			}
		}

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x06002F82 RID: 12162 RVA: 0x000BFEC4 File Offset: 0x000BE0C4
		public MultiValuedProperty<ADObjectId> RetentionPolicyTag
		{
			get
			{
				return (MultiValuedProperty<ADObjectId>)this[ELCFolderSchema.RetentionPolicyTag];
			}
		}

		// Token: 0x06002F83 RID: 12163 RVA: 0x000BFED8 File Offset: 0x000BE0D8
		internal ADPagedReader<ElcContentSettings> GetELCContentSettings()
		{
			return base.Session.FindPaged<ElcContentSettings>(base.Id, QueryScope.SubTree, null, null, 0);
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000BFF8C File Offset: 0x000BE18C
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.FolderType == ElcFolderType.ManagedCustomFolder && string.IsNullOrEmpty(this.FolderName))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorELCFolderNotSpecified, ELCFolderSchema.FolderName, this));
			}
			if (!string.IsNullOrEmpty(this.Comment) && base.IsChanged(ELCFolderSchema.Comment) && !ELCFolder.FolderTypeAllowsComments(this.FolderType))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorElcCommentNotAllowed, ELCFolderSchema.Comment, this));
			}
			if (this.MustDisplayCommentEnabled && string.IsNullOrEmpty(this.Comment))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorComment, ELCFolderSchema.Comment, this));
			}
			this.BuildMap(ref this.locNameMap, this.LocalizedFolderName, delegate
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorDuplicateLanguage, ELCFolderSchema.LocalizedFolderName, this));
			}, delegate
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorBadLocalizedFolderName, ELCFolderSchema.LocalizedFolderName, this));
			});
			this.BuildMap(ref this.locCommentMap, this.LocalizedComment, delegate
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorDuplicateLanguage, ELCFolderSchema.LocalizedComment, this));
			}, delegate
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorBadLocalizedComment, ELCFolderSchema.LocalizedComment, this));
			});
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000C00B1 File Offset: 0x000BE2B1
		internal string GetLocalizedFolderName(IEnumerable<CultureInfo> cultureList)
		{
			return this.GetLocalizedValue(cultureList, this.FolderName, this.LocalizedFolderName, ref this.locNameMap);
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000C00CC File Offset: 0x000BE2CC
		internal string GetLocalizedFolderComment(IEnumerable<CultureInfo> cultureList)
		{
			return this.GetLocalizedValue(cultureList, this.Comment, this.LocalizedComment, ref this.locCommentMap);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000C00E8 File Offset: 0x000BE2E8
		private string GetLocalizedValue(IEnumerable<CultureInfo> cultureList, string defaultString, MultiValuedProperty<string> localizedStrings, ref Dictionary<string, string> locMap)
		{
			if (localizedStrings == null || localizedStrings.Count == 0 || cultureList == null)
			{
				return defaultString;
			}
			if (locMap == null || localizedStrings.Changed)
			{
				this.BuildMap(ref locMap, localizedStrings, null, null);
			}
			using (IEnumerator<CultureInfo> enumerator = cultureList.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					CultureInfo cultureInfo = enumerator.Current;
					CultureInfo cultureInfo2 = null;
					string result = null;
					if (cultureInfo != null)
					{
						if (locMap.TryGetValue(cultureInfo.TwoLetterISOLanguageName, out result))
						{
							return result;
						}
						if (locMap.TryGetValue(cultureInfo.EnglishName, out result))
						{
							return result;
						}
						cultureInfo2 = cultureInfo.Parent;
					}
					if (cultureInfo2 != null)
					{
						if (locMap.TryGetValue(cultureInfo2.TwoLetterISOLanguageName, out result))
						{
							return result;
						}
						if (locMap.TryGetValue(cultureInfo2.EnglishName, out result))
						{
							return result;
						}
					}
				}
			}
			return defaultString;
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000C01C4 File Offset: 0x000BE3C4
		private void BuildMap(ref Dictionary<string, string> map, MultiValuedProperty<string> localizedStrings, ELCFolder.ErrorDelegate duplicateLangError, ELCFolder.ErrorDelegate badSyntaxError)
		{
			if (map == null)
			{
				map = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);
			}
			else
			{
				map.Clear();
			}
			if (localizedStrings == null || localizedStrings.Count == 0)
			{
				return;
			}
			foreach (string text in localizedStrings)
			{
				string[] array = text.Split(ELCFolder.langSep, 2);
				if (array == null || array.Length != 2)
				{
					if (badSyntaxError != null)
					{
						badSyntaxError();
					}
				}
				else if (map.ContainsKey(array[0]))
				{
					if (duplicateLangError != null)
					{
						duplicateLangError();
					}
				}
				else
				{
					map.Add(array[0], array[1]);
				}
			}
		}

		// Token: 0x04002010 RID: 8208
		private static readonly char[] langSep = new char[]
		{
			':'
		};

		// Token: 0x04002011 RID: 8209
		private static ELCFolderSchema schema = ObjectSchema.GetInstance<ELCFolderSchema>();

		// Token: 0x04002012 RID: 8210
		private static string mostDerivedClass = "msExchELCFolder";

		// Token: 0x04002013 RID: 8211
		internal static string ElcFolderContainerName = "CN=ELC Folders Container";

		// Token: 0x04002014 RID: 8212
		private static ADObjectId parentPath = new ADObjectId(ELCFolder.ElcFolderContainerName);

		// Token: 0x04002015 RID: 8213
		private Dictionary<string, string> locNameMap;

		// Token: 0x04002016 RID: 8214
		private Dictionary<string, string> locCommentMap;

		// Token: 0x0200041F RID: 1055
		// (Invoke) Token: 0x06002F8B RID: 12171
		internal delegate void ErrorDelegate();
	}
}
