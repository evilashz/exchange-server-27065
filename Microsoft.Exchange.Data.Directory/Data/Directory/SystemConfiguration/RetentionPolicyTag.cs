using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200042A RID: 1066
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class RetentionPolicyTag : ADConfigurationObject
	{
		// Token: 0x06002FE5 RID: 12261 RVA: 0x000C185B File Offset: 0x000BFA5B
		internal RetentionPolicyTag(IConfigurationSession session, string name)
		{
			this.m_Session = session;
			base.SetId(session, name);
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000C1872 File Offset: 0x000BFA72
		public RetentionPolicyTag()
		{
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06002FE7 RID: 12263 RVA: 0x000C187A File Offset: 0x000BFA7A
		internal override ADObjectSchema Schema
		{
			get
			{
				return RetentionPolicyTag.schema;
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x000C1881 File Offset: 0x000BFA81
		internal override string MostDerivedObjectClass
		{
			get
			{
				return RetentionPolicyTag.mostDerivedClass;
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x000C1888 File Offset: 0x000BFA88
		internal override ADObjectId ParentPath
		{
			get
			{
				return RetentionPolicyTag.parentPath;
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x000C188F File Offset: 0x000BFA8F
		internal override bool IsShareable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06002FEB RID: 12267 RVA: 0x000C1892 File Offset: 0x000BFA92
		// (set) Token: 0x06002FEC RID: 12268 RVA: 0x000C18A4 File Offset: 0x000BFAA4
		public ElcFolderType Type
		{
			get
			{
				return (ElcFolderType)this[RetentionPolicyTagSchema.Type];
			}
			set
			{
				this[RetentionPolicyTagSchema.Type] = value;
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06002FED RID: 12269 RVA: 0x000C18B7 File Offset: 0x000BFAB7
		// (set) Token: 0x06002FEE RID: 12270 RVA: 0x000C18C9 File Offset: 0x000BFAC9
		public bool IsDefaultAutoGroupPolicyTag
		{
			get
			{
				return (bool)this[RetentionPolicyTagSchema.IsDefaultAutoGroupPolicyTag];
			}
			set
			{
				this[RetentionPolicyTagSchema.IsDefaultAutoGroupPolicyTag] = value;
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06002FEF RID: 12271 RVA: 0x000C18DC File Offset: 0x000BFADC
		// (set) Token: 0x06002FF0 RID: 12272 RVA: 0x000C18EE File Offset: 0x000BFAEE
		public bool IsDefaultModeratedRecipientsPolicyTag
		{
			get
			{
				return (bool)this[RetentionPolicyTagSchema.IsDefaultModeratedRecipientsPolicyTag];
			}
			set
			{
				this[RetentionPolicyTagSchema.IsDefaultModeratedRecipientsPolicyTag] = value;
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06002FF1 RID: 12273 RVA: 0x000C1901 File Offset: 0x000BFB01
		// (set) Token: 0x06002FF2 RID: 12274 RVA: 0x000C191C File Offset: 0x000BFB1C
		[Parameter(Mandatory = false)]
		public bool SystemTag
		{
			get
			{
				return ((int)this[RetentionPolicyTagSchema.PolicyTagFlags] & 1) != 0;
			}
			set
			{
				ElcTagType elcTagType = (ElcTagType)((int)this[RetentionPolicyTagSchema.PolicyTagFlags]);
				if (value)
				{
					elcTagType |= ElcTagType.SystemTag;
				}
				else
				{
					elcTagType &= ~ElcTagType.SystemTag;
				}
				this[RetentionPolicyTagSchema.PolicyTagFlags] = (int)elcTagType;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06002FF3 RID: 12275 RVA: 0x000C1959 File Offset: 0x000BFB59
		// (set) Token: 0x06002FF4 RID: 12276 RVA: 0x000C1974 File Offset: 0x000BFB74
		internal bool IsPrimary
		{
			get
			{
				return ((int)this[RetentionPolicyTagSchema.PolicyTagFlags] & 4) != 0;
			}
			set
			{
				ElcTagType elcTagType = (ElcTagType)((int)this[RetentionPolicyTagSchema.PolicyTagFlags]);
				if (value)
				{
					elcTagType |= ElcTagType.PrimaryDefault;
				}
				else
				{
					elcTagType &= ~ElcTagType.PrimaryDefault;
				}
				this[RetentionPolicyTagSchema.PolicyTagFlags] = (int)elcTagType;
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06002FF5 RID: 12277 RVA: 0x000C19B1 File Offset: 0x000BFBB1
		// (set) Token: 0x06002FF6 RID: 12278 RVA: 0x000C19C3 File Offset: 0x000BFBC3
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> LocalizedRetentionPolicyTagName
		{
			get
			{
				return (MultiValuedProperty<string>)this[RetentionPolicyTagSchema.LocalizedRetentionPolicyTagName];
			}
			set
			{
				this[RetentionPolicyTagSchema.LocalizedRetentionPolicyTagName] = value;
				this.locNameMap = null;
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06002FF7 RID: 12279 RVA: 0x000C19D8 File Offset: 0x000BFBD8
		// (set) Token: 0x06002FF8 RID: 12280 RVA: 0x000C19EA File Offset: 0x000BFBEA
		[Parameter(Mandatory = false)]
		public string Comment
		{
			get
			{
				return (string)this[RetentionPolicyTagSchema.Comment];
			}
			set
			{
				this[RetentionPolicyTagSchema.Comment] = value;
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06002FF9 RID: 12281 RVA: 0x000C19F8 File Offset: 0x000BFBF8
		// (set) Token: 0x06002FFA RID: 12282 RVA: 0x000C1A2D File Offset: 0x000BFC2D
		[Parameter(Mandatory = false)]
		public Guid RetentionId
		{
			get
			{
				if ((Guid)this[RetentionPolicyTagSchema.RetentionId] == Guid.Empty)
				{
					return base.Guid;
				}
				return (Guid)this[RetentionPolicyTagSchema.RetentionId];
			}
			set
			{
				this[RetentionPolicyTagSchema.RetentionId] = value;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06002FFB RID: 12283 RVA: 0x000C1A40 File Offset: 0x000BFC40
		// (set) Token: 0x06002FFC RID: 12284 RVA: 0x000C1A52 File Offset: 0x000BFC52
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> LocalizedComment
		{
			get
			{
				return (MultiValuedProperty<string>)this[RetentionPolicyTagSchema.LocalizedComment];
			}
			set
			{
				this[RetentionPolicyTagSchema.LocalizedComment] = value;
				this.locCommentMap = null;
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06002FFD RID: 12285 RVA: 0x000C1A67 File Offset: 0x000BFC67
		// (set) Token: 0x06002FFE RID: 12286 RVA: 0x000C1A84 File Offset: 0x000BFC84
		[Parameter(Mandatory = false)]
		public bool MustDisplayCommentEnabled
		{
			get
			{
				return ((ElcTagType)this[RetentionPolicyTagSchema.PolicyTagFlags] & ElcTagType.MustDisplayComment) != ElcTagType.None;
			}
			set
			{
				ElcTagType elcTagType = (ElcTagType)this[RetentionPolicyTagSchema.PolicyTagFlags];
				if (value)
				{
					elcTagType |= ElcTagType.MustDisplayComment;
				}
				else
				{
					elcTagType &= ~ElcTagType.MustDisplayComment;
				}
				if (elcTagType != (ElcTagType)this[RetentionPolicyTagSchema.PolicyTagFlags])
				{
					this[RetentionPolicyTagSchema.PolicyTagFlags] = (int)elcTagType;
				}
			}
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000C1AD4 File Offset: 0x000BFCD4
		internal static bool FolderTypeAllowsComments(ElcFolderType folderType)
		{
			return folderType != ElcFolderType.Calendar && folderType != ElcFolderType.Contacts && folderType != ElcFolderType.Notes && folderType != ElcFolderType.Journal && folderType != ElcFolderType.Tasks;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000C1AF0 File Offset: 0x000BFCF0
		internal ADPagedReader<ElcContentSettings> GetELCContentSettings()
		{
			return base.Session.FindPaged<ElcContentSettings>((ADObjectId)this.Identity, QueryScope.SubTree, null, null, 0);
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06003001 RID: 12289 RVA: 0x000C1B19 File Offset: 0x000BFD19
		// (set) Token: 0x06003002 RID: 12290 RVA: 0x000C1B2B File Offset: 0x000BFD2B
		public ADObjectId LegacyManagedFolder
		{
			get
			{
				return (ADObjectId)this[RetentionPolicyTagSchema.LegacyManagedFolder];
			}
			set
			{
				this[RetentionPolicyTagSchema.LegacyManagedFolder] = value;
			}
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x000C1B39 File Offset: 0x000BFD39
		internal EnhancedTimeSpan? TimeSpanForRetention
		{
			get
			{
				return this.GetTimeSpanForRetention();
			}
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000C1BD4 File Offset: 0x000BFDD4
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			this.BuildMap(ref this.locNameMap, this.LocalizedRetentionPolicyTagName, delegate
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorDuplicateLanguage, RetentionPolicyTagSchema.LocalizedRetentionPolicyTagName, this));
			}, delegate
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorBadLocalizedFolderName, RetentionPolicyTagSchema.LocalizedRetentionPolicyTagName, this));
			});
			this.BuildMap(ref this.locCommentMap, this.LocalizedComment, delegate
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorDuplicateLanguage, RetentionPolicyTagSchema.LocalizedComment, this));
			}, delegate
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.ErrorBadLocalizedComment, RetentionPolicyTagSchema.LocalizedComment, this));
			});
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000C1C55 File Offset: 0x000BFE55
		internal string GetLocalizedFolderName(IEnumerable<CultureInfo> cultureList)
		{
			return this.GetLocalizedValue(cultureList, base.Name, this.LocalizedRetentionPolicyTagName, ref this.locNameMap);
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000C1C70 File Offset: 0x000BFE70
		internal string GetLocalizedFolderComment(IEnumerable<CultureInfo> cultureList)
		{
			return this.GetLocalizedValue(cultureList, this.Comment, this.LocalizedComment, ref this.locCommentMap);
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000C1C8C File Offset: 0x000BFE8C
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

		// Token: 0x06003008 RID: 12296 RVA: 0x000C1D68 File Offset: 0x000BFF68
		private void BuildMap(ref Dictionary<string, string> map, MultiValuedProperty<string> localizedStrings, RetentionPolicyTag.ErrorDelegate duplicateLangError, RetentionPolicyTag.ErrorDelegate badSyntaxError)
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
				string[] array = text.Split(RetentionPolicyTag.langSep, 2);
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

		// Token: 0x06003009 RID: 12297 RVA: 0x000C1E1C File Offset: 0x000C001C
		internal EnhancedTimeSpan? GetTimeSpanForRetention()
		{
			ADPagedReader<ElcContentSettings> elccontentSettings = this.GetELCContentSettings();
			EnhancedTimeSpan? result = null;
			if (elccontentSettings != null)
			{
				foreach (ElcContentSettings elcContentSettings in elccontentSettings)
				{
					if (elcContentSettings.RetentionEnabled && elcContentSettings.AgeLimitForRetention != null && (elcContentSettings.RetentionAction == RetentionActionType.DeleteAndAllowRecovery || elcContentSettings.RetentionAction == RetentionActionType.PermanentlyDelete || elcContentSettings.RetentionAction == RetentionActionType.MoveToDeletedItems))
					{
						if (result != null)
						{
							return null;
						}
						result = elcContentSettings.AgeLimitForRetention;
					}
				}
			}
			if (result == null)
			{
				result = new EnhancedTimeSpan?(TimeSpan.MaxValue);
			}
			return result;
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x0600300A RID: 12298 RVA: 0x000C1EE4 File Offset: 0x000C00E4
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return RetentionPolicy.RetentionPolicyVersion;
			}
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000C1EEB File Offset: 0x000C00EB
		internal override void Initialize()
		{
			if (base.ExchangeVersion == RetentionPolicy.E14RetentionPolicyMajorVersion)
			{
				this.propertyBag.SetField(this.propertyBag.ObjectVersionPropertyDefinition, RetentionPolicy.E14RetentionPolicyFullVersion);
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x0600300C RID: 12300 RVA: 0x000C1F1C File Offset: 0x000C011C
		internal override QueryFilter VersioningFilter
		{
			get
			{
				ExchangeObjectVersion e14RetentionPolicyMajorVersion = RetentionPolicy.E14RetentionPolicyMajorVersion;
				ExchangeObjectVersion nextMajorVersion = e14RetentionPolicyMajorVersion.NextMajorVersion;
				return new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, ADObjectSchema.ExchangeVersion, e14RetentionPolicyMajorVersion),
					new ComparisonFilter(ComparisonOperator.LessThan, ADObjectSchema.ExchangeVersion, nextMajorVersion)
				});
			}
		}

		// Token: 0x0400205D RID: 8285
		private static readonly char[] langSep = new char[]
		{
			':'
		};

		// Token: 0x0400205E RID: 8286
		private static RetentionPolicyTagSchema schema = ObjectSchema.GetInstance<RetentionPolicyTagSchema>();

		// Token: 0x0400205F RID: 8287
		private static string mostDerivedClass = "msExchELCFolder";

		// Token: 0x04002060 RID: 8288
		private static ADObjectId parentPath = new ADObjectId("CN=Retention Policy Tag Container");

		// Token: 0x04002061 RID: 8289
		private Dictionary<string, string> locNameMap;

		// Token: 0x04002062 RID: 8290
		private Dictionary<string, string> locCommentMap;

		// Token: 0x0200042B RID: 1067
		// (Invoke) Token: 0x0600300F RID: 12303
		internal delegate void ErrorDelegate();
	}
}
