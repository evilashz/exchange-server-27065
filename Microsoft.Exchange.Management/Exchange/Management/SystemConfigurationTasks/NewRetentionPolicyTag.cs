using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Approval;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.InfoWorker.Common.Search;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200031A RID: 794
	[Cmdlet("New", "RetentionPolicyTag", DefaultParameterSetName = "RetentionPolicy", SupportsShouldProcess = true)]
	public sealed class NewRetentionPolicyTag : NewMultitenancySystemConfigurationObjectTask<RetentionPolicyTag>
	{
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001AA7 RID: 6823 RVA: 0x00075F6D File Offset: 0x0007416D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewRetentionTag(base.Name.ToString());
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001AA8 RID: 6824 RVA: 0x00075F7F File Offset: 0x0007417F
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Dehydrateable;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001AA9 RID: 6825 RVA: 0x00075F91 File Offset: 0x00074191
		// (set) Token: 0x06001AAA RID: 6826 RVA: 0x00075FA8 File Offset: 0x000741A8
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> LocalizedRetentionPolicyTagName
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["LocalizedRetentionPolicyTagName"];
			}
			set
			{
				base.Fields["LocalizedRetentionPolicyTagName"] = value;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001AAB RID: 6827 RVA: 0x00075FBB File Offset: 0x000741BB
		// (set) Token: 0x06001AAC RID: 6828 RVA: 0x00075FC3 File Offset: 0x000741C3
		[Parameter(Mandatory = false)]
		public override SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001AAD RID: 6829 RVA: 0x00075FCC File Offset: 0x000741CC
		// (set) Token: 0x06001AAE RID: 6830 RVA: 0x00075FF2 File Offset: 0x000741F2
		[Parameter(Mandatory = false)]
		public SwitchParameter IsDefaultAutoGroupPolicyTag
		{
			get
			{
				return (SwitchParameter)(base.Fields["IsDefaultAutoGroupPolicyTag"] ?? false);
			}
			set
			{
				base.Fields["IsDefaultAutoGroupPolicyTag"] = value;
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x0007600A File Offset: 0x0007420A
		// (set) Token: 0x06001AB0 RID: 6832 RVA: 0x00076030 File Offset: 0x00074230
		[Parameter(Mandatory = false)]
		public SwitchParameter IsDefaultModeratedRecipientsPolicyTag
		{
			get
			{
				return (SwitchParameter)(base.Fields["IsDefaultModeratedRecipientsPolicyTag"] ?? false);
			}
			set
			{
				base.Fields["IsDefaultModeratedRecipientsPolicyTag"] = value;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001AB1 RID: 6833 RVA: 0x00076048 File Offset: 0x00074248
		// (set) Token: 0x06001AB2 RID: 6834 RVA: 0x0007605F File Offset: 0x0007425F
		[Parameter(Mandatory = false)]
		public string Comment
		{
			get
			{
				return (string)base.Fields["Comment"];
			}
			set
			{
				base.Fields["Comment"] = value;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001AB3 RID: 6835 RVA: 0x00076072 File Offset: 0x00074272
		// (set) Token: 0x06001AB4 RID: 6836 RVA: 0x00076089 File Offset: 0x00074289
		[Parameter(Mandatory = false, ParameterSetName = "RetentionPolicy")]
		public Guid RetentionId
		{
			get
			{
				return (Guid)base.Fields["RetentionId"];
			}
			set
			{
				base.Fields["RetentionId"] = value;
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001AB5 RID: 6837 RVA: 0x000760A1 File Offset: 0x000742A1
		// (set) Token: 0x06001AB6 RID: 6838 RVA: 0x000760B8 File Offset: 0x000742B8
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<string> LocalizedComment
		{
			get
			{
				return (MultiValuedProperty<string>)base.Fields["LocalizedComment"];
			}
			set
			{
				base.Fields["LocalizedComment"] = value;
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001AB7 RID: 6839 RVA: 0x000760CB File Offset: 0x000742CB
		// (set) Token: 0x06001AB8 RID: 6840 RVA: 0x000760E2 File Offset: 0x000742E2
		[Parameter(Mandatory = false)]
		public bool MustDisplayCommentEnabled
		{
			get
			{
				return (bool)base.Fields["MustDisplayCommentEnabled"];
			}
			set
			{
				base.Fields["MustDisplayCommentEnabled"] = value;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001AB9 RID: 6841 RVA: 0x000760FA File Offset: 0x000742FA
		// (set) Token: 0x06001ABA RID: 6842 RVA: 0x00076111 File Offset: 0x00074311
		[Parameter(Mandatory = false)]
		public ElcFolderType Type
		{
			get
			{
				return (ElcFolderType)base.Fields["Type"];
			}
			set
			{
				base.Fields["Type"] = value;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001ABB RID: 6843 RVA: 0x00076129 File Offset: 0x00074329
		// (set) Token: 0x06001ABC RID: 6844 RVA: 0x00076140 File Offset: 0x00074340
		[Parameter(Mandatory = false, ParameterSetName = "UpgradeManagedFolder")]
		public ELCFolderIdParameter ManagedFolderToUpgrade
		{
			get
			{
				return (ELCFolderIdParameter)base.Fields["ManagedFolderToUpgrade"];
			}
			set
			{
				base.Fields["ManagedFolderToUpgrade"] = value;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001ABD RID: 6845 RVA: 0x00076153 File Offset: 0x00074353
		// (set) Token: 0x06001ABE RID: 6846 RVA: 0x00076174 File Offset: 0x00074374
		[Parameter(Mandatory = false)]
		public bool SystemTag
		{
			get
			{
				return (bool)(base.Fields["SystemTag"] ?? false);
			}
			set
			{
				base.Fields["SystemTag"] = value;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x0007618C File Offset: 0x0007438C
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x00076199 File Offset: 0x00074399
		[Parameter(Mandatory = false, ParameterSetName = "RetentionPolicy")]
		public string MessageClass
		{
			get
			{
				return this.contentSettingsObject.MessageClass;
			}
			set
			{
				this.contentSettingsObject.MessageClass = value;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x000761A7 File Offset: 0x000743A7
		// (set) Token: 0x06001AC2 RID: 6850 RVA: 0x000761C8 File Offset: 0x000743C8
		[Parameter(Mandatory = false, ParameterSetName = "RetentionPolicy")]
		public bool RetentionEnabled
		{
			get
			{
				return (bool)(base.Fields["RetentionEnabled"] ?? true);
			}
			set
			{
				base.Fields["RetentionEnabled"] = value;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x000761E0 File Offset: 0x000743E0
		// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x000761ED File Offset: 0x000743ED
		[Parameter(Mandatory = false, ParameterSetName = "RetentionPolicy")]
		public RetentionActionType RetentionAction
		{
			get
			{
				return this.contentSettingsObject.RetentionAction;
			}
			set
			{
				this.contentSettingsObject.RetentionAction = value;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x000761FB File Offset: 0x000743FB
		// (set) Token: 0x06001AC6 RID: 6854 RVA: 0x00076208 File Offset: 0x00074408
		[Parameter(Mandatory = false, ParameterSetName = "RetentionPolicy")]
		public EnhancedTimeSpan? AgeLimitForRetention
		{
			get
			{
				return this.contentSettingsObject.AgeLimitForRetention;
			}
			set
			{
				this.contentSettingsObject.AgeLimitForRetention = value;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x00076216 File Offset: 0x00074416
		// (set) Token: 0x06001AC8 RID: 6856 RVA: 0x00076223 File Offset: 0x00074423
		[Parameter(Mandatory = false, ParameterSetName = "RetentionPolicy")]
		public bool JournalingEnabled
		{
			get
			{
				return this.contentSettingsObject.JournalingEnabled;
			}
			set
			{
				this.contentSettingsObject.JournalingEnabled = value;
			}
		}

		// Token: 0x170007D0 RID: 2000
		// (get) Token: 0x06001AC9 RID: 6857 RVA: 0x00076231 File Offset: 0x00074431
		// (set) Token: 0x06001ACA RID: 6858 RVA: 0x00076248 File Offset: 0x00074448
		[Parameter(Mandatory = false, ParameterSetName = "RetentionPolicy")]
		public RecipientIdParameter AddressForJournaling
		{
			get
			{
				return (RecipientIdParameter)base.Fields["AddressForJournaling"];
			}
			set
			{
				base.Fields["AddressForJournaling"] = value;
			}
		}

		// Token: 0x170007D1 RID: 2001
		// (get) Token: 0x06001ACB RID: 6859 RVA: 0x0007625B File Offset: 0x0007445B
		// (set) Token: 0x06001ACC RID: 6860 RVA: 0x00076268 File Offset: 0x00074468
		[Parameter(Mandatory = false, ParameterSetName = "RetentionPolicy")]
		public JournalingFormat MessageFormatForJournaling
		{
			get
			{
				return this.contentSettingsObject.MessageFormatForJournaling;
			}
			set
			{
				this.contentSettingsObject.MessageFormatForJournaling = value;
			}
		}

		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06001ACD RID: 6861 RVA: 0x00076276 File Offset: 0x00074476
		// (set) Token: 0x06001ACE RID: 6862 RVA: 0x00076283 File Offset: 0x00074483
		[Parameter(Mandatory = false, ParameterSetName = "RetentionPolicy")]
		public string LabelForJournaling
		{
			get
			{
				return this.contentSettingsObject.LabelForJournaling;
			}
			set
			{
				this.contentSettingsObject.LabelForJournaling = value;
			}
		}

		// Token: 0x06001ACF RID: 6863 RVA: 0x00076294 File Offset: 0x00074494
		public NewRetentionPolicyTag()
		{
			this.contentSettingsObject = new ElcContentSettings();
			this.contentSettingsObject.StampPersistableDefaultValues();
			this.contentSettingsObject.ResetChangeTracking();
			this.contentSettingsObject.MessageClass = ElcMessageClass.AllMailboxContent;
			this.contentSettingsObject.RetentionAction = RetentionActionType.DeleteAndAllowRecovery;
		}

		// Token: 0x06001AD0 RID: 6864 RVA: 0x000762E4 File Offset: 0x000744E4
		protected override IConfigurable PrepareDataObject()
		{
			if (!this.IgnoreDehydratedFlag && SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
			{
				base.WriteError(new ArgumentException(Strings.ErrorWriteOpOnDehydratedTenant), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			RetentionPolicyTag retentionPolicyTag = (RetentionPolicyTag)base.PrepareDataObject();
			IConfigurationSession session = base.DataSession as IConfigurationSession;
			retentionPolicyTag.SetId(session, base.Name);
			if (base.ParameterSetName == "UpgradeManagedFolder" && this.ElcFolderToUpgrade != null)
			{
				retentionPolicyTag.LegacyManagedFolder = this.ElcFolderToUpgrade.Id;
				retentionPolicyTag.LocalizedRetentionPolicyTagName = this.ElcFolderToUpgrade.LocalizedFolderName;
				retentionPolicyTag.Comment = this.ElcFolderToUpgrade.Comment;
				retentionPolicyTag.LocalizedComment = this.ElcFolderToUpgrade.LocalizedComment;
				retentionPolicyTag.MustDisplayCommentEnabled = this.ElcFolderToUpgrade.MustDisplayCommentEnabled;
				retentionPolicyTag.Type = this.ElcFolderToUpgrade.FolderType;
			}
			if (base.Fields.Contains("LocalizedRetentionPolicyTagName"))
			{
				retentionPolicyTag.LocalizedRetentionPolicyTagName = this.LocalizedRetentionPolicyTagName;
			}
			if (base.Fields.Contains("Comment"))
			{
				retentionPolicyTag.Comment = this.Comment;
			}
			if (base.Fields.Contains("LocalizedComment"))
			{
				retentionPolicyTag.LocalizedComment = this.LocalizedComment;
			}
			if (base.Fields.Contains("MustDisplayCommentEnabled"))
			{
				retentionPolicyTag.MustDisplayCommentEnabled = this.MustDisplayCommentEnabled;
			}
			if (base.Fields.Contains("Type"))
			{
				retentionPolicyTag.Type = this.Type;
			}
			if (base.Fields.Contains("RetentionId"))
			{
				retentionPolicyTag.RetentionId = this.RetentionId;
			}
			if (retentionPolicyTag.Type == ElcFolderType.ManagedCustomFolder)
			{
				retentionPolicyTag.Type = ElcFolderType.Personal;
			}
			retentionPolicyTag.SystemTag = this.SystemTag;
			if (NewRetentionPolicyTag.MessageClassNameMaps.ContainsKey(this.contentSettingsObject.MessageClass))
			{
				this.contentSettingsObject.MessageClass = NewRetentionPolicyTag.MessageClassNameMaps[this.contentSettingsObject.MessageClass];
			}
			if (!NewRetentionPolicyTag.MessageClassNameMaps.Values.Contains(this.contentSettingsObject.MessageClass, StringComparer.OrdinalIgnoreCase))
			{
				base.WriteError(new RetentionPolicyTagTaskException(Strings.MessageClassIsNotValid(this.contentSettingsObject.MessageClass)), ErrorCategory.InvalidArgument, null);
			}
			string text = base.Name;
			if (text.Length > 60)
			{
				text = text.Substring(0, 60);
			}
			text += "_cs";
			this.contentSettingsObject.SetId(retentionPolicyTag.Id.GetChildId(text));
			if (base.ParameterSetName != "UpgradeManagedFolder")
			{
				this.contentSettingsObject.RetentionEnabled = this.RetentionEnabled;
			}
			if (this.JournalingEnabled && this.AddressForJournaling != null)
			{
				IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, base.OrganizationId.ToADSessionSettings(), 481, "PrepareDataObject", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Elc\\NewRetentionPolicyTag.cs");
				ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(this.AddressForJournaling, tenantOrRootOrgRecipientSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.AddressForJournaling.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotUnique(this.AddressForJournaling.ToString())));
				if (adrecipient.EmailAddresses == null || adrecipient.EmailAddresses.FindPrimary(ProxyAddressPrefix.Smtp) == null)
				{
					base.WriteError(new ArgumentException(Strings.SmtpAddressMissingForAutocopy(this.AddressForJournaling.ToString()), "AddressForJournaling"), ErrorCategory.InvalidData, this);
				}
				this.contentSettingsObject.AddressForJournaling = adrecipient.Id;
			}
			if (this.DataObject.Type != ElcFolderType.All && this.DataObject.Type != ElcFolderType.Personal && this.DataObject.Type != ElcFolderType.RecoverableItems && this.RetentionAction == RetentionActionType.MoveToArchive)
			{
				base.WriteError(new RetentionPolicyTagTaskException(Strings.ErrorMoveToArchiveAppliedToSystemFolder), ErrorCategory.InvalidArgument, null);
			}
			return retentionPolicyTag;
		}

		// Token: 0x06001AD1 RID: 6865 RVA: 0x000766A4 File Offset: 0x000748A4
		protected override void WriteResult()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.DataObject.Identity
			});
			this.nonAtomicTagObject = this.DataObject;
			base.WriteVerbose(TaskVerboseStringHelper.GetReadObjectVerboseString(this.DataObject.Identity, base.DataSession, typeof(RetentionPolicyTag)));
			RetentionPolicyTag retentionPolicyTag = null;
			try
			{
				IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
				configurationSession.SessionSettings.IsSharedConfigChecked = true;
				retentionPolicyTag = configurationSession.Read<RetentionPolicyTag>(this.DataObject.Id);
			}
			finally
			{
				base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
			}
			if (retentionPolicyTag == null)
			{
				base.WriteError(new ManagementObjectNotFoundException(base.GetErrorMessageObjectNotFound(this.ResolveIdentityString(this.DataObject.Identity), typeof(RetentionPolicyTag).ToString(), (base.DataSession != null) ? base.DataSession.Source : null)), (ErrorCategory)1003, this.DataObject.Identity);
			}
			IConfigurationSession session = base.DataSession as IConfigurationSession;
			this.MakeContentSettingUniqueAndSave(session, this.DataObject, this.contentSettingsObject, this.contentSettingsObject.Name);
			this.nonAtomicTagObject = null;
			PresentationRetentionPolicyTag result = new PresentationRetentionPolicyTag(retentionPolicyTag, this.contentSettingsObject);
			this.WriteResult(result);
			TaskLogger.LogExit();
		}

		// Token: 0x06001AD2 RID: 6866 RVA: 0x000767F8 File Offset: 0x000749F8
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (this.IsDefaultAutoGroupPolicyTag && this.IsDefaultModeratedRecipientsPolicyTag)
			{
				base.WriteError(new ArgumentException(Strings.ErrorMultipleDefaultRetentionPolicyTag), ErrorCategory.InvalidArgument, this.DataObject.Identity);
			}
			if (this.IsDefaultAutoGroupPolicyTag)
			{
				this.DataObject.IsDefaultAutoGroupPolicyTag = true;
				this.existingDefaultPolicyTags = ApprovalUtils.GetDefaultRetentionPolicyTag((IConfigurationSession)base.DataSession, ApprovalApplicationId.AutoGroup, int.MaxValue);
			}
			else if (this.IsDefaultModeratedRecipientsPolicyTag)
			{
				this.DataObject.IsDefaultModeratedRecipientsPolicyTag = true;
				this.existingDefaultPolicyTags = ApprovalUtils.GetDefaultRetentionPolicyTag((IConfigurationSession)base.DataSession, ApprovalApplicationId.ModeratedRecipient, int.MaxValue);
			}
			if (base.ParameterSetName == "UpgradeManagedFolder" && this.ElcFolderToUpgrade == null)
			{
				return;
			}
			if (Datacenter.IsMicrosoftHostedOnly(false))
			{
				List<RetentionPolicyTag> allRetentionTags = AdTagReader.GetAllRetentionTags(this.ConfigurationSession, base.OrganizationId);
				if (allRetentionTags.Count >= 500)
				{
					base.WriteError(new RetentionPolicyTagTaskException(Strings.ErrorTenantRetentionTagLimitReached(500)), ErrorCategory.InvalidOperation, this.DataObject);
				}
			}
			if (this.DataObject.Type == ElcFolderType.RecoverableItems && !this.contentSettingsObject.RetentionAction.Equals(RetentionActionType.MoveToArchive))
			{
				base.WriteError(new ArgumentException(Strings.ErrorDumpsterTagWrongRetentionAction), ErrorCategory.InvalidArgument, this);
			}
			if (this.DataObject.Type != ElcFolderType.All && !this.contentSettingsObject.MessageClass.Equals(ElcMessageClass.AllMailboxContent))
			{
				base.WriteError(new RetentionPolicyTagTaskException(Strings.ErrorOnlyDefaultTagAllowCustomizedMessageClass), ErrorCategory.InvalidOperation, this.DataObject);
			}
			string tagName;
			if (this.DataObject.RetentionId != Guid.Empty && !(base.DataSession as IConfigurationSession).CheckForRetentionTagWithConflictingRetentionId(this.DataObject.RetentionId, out tagName))
			{
				base.WriteError(new RetentionPolicyTagTaskException(Strings.ErrorRetentionIdConflictsWithRetentionTag(this.DataObject.RetentionId.ToString(), tagName)), ErrorCategory.InvalidOperation, this.DataObject);
			}
			ValidationError[] array = this.contentSettingsObject.Validate();
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					this.WriteError(new DataValidationException(array[i]), (ErrorCategory)1003, this.contentSettingsObject.Identity, array.Length - 1 == i);
				}
			}
		}

		// Token: 0x06001AD3 RID: 6867 RVA: 0x00076AD0 File Offset: 0x00074CD0
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.DataObject != null && SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			if (this.existingDefaultPolicyTags != null && this.existingDefaultPolicyTags.Count > 0)
			{
				RetentionPolicyTagUtility.ClearDefaultPolicyTag(base.DataSession as IConfigurationSession, this.existingDefaultPolicyTags, this.DataObject.IsDefaultAutoGroupPolicyTag ? ApprovalApplicationId.AutoGroup : ApprovalApplicationId.ModeratedRecipient);
			}
			try
			{
				if (base.ParameterSetName == "RetentionPolicy")
				{
					base.InternalProcessRecord();
				}
				else if (base.ParameterSetName == "UpgradeManagedFolder")
				{
					this.ElcFolderToUpgrade = (base.GetDataObject<ELCFolder>(this.ManagedFolderToUpgrade, base.DataSession, null, new LocalizedString?(Strings.ErrorElcFolderNotFound(this.ManagedFolderToUpgrade.ToString())), new LocalizedString?(Strings.ErrorAmbiguousElcFolderId(this.ManagedFolderToUpgrade.ToString()))) as ELCFolder);
					ElcContentSettings[] array = this.ElcFolderToUpgrade.GetELCContentSettings().ToArray<ElcContentSettings>();
					bool flag = this.DataObject.Type == ElcFolderType.All;
					if (array.Length > 0)
					{
						this.upgradedContentSettings = this.GetUpgradedConentSettings(array, flag).ToArray<ElcContentSettings>();
						if (this.upgradedContentSettings.Length < 1)
						{
							base.WriteError(new RetentionPolicyTagTaskException(Strings.ErrorCannotUpgradeManagedFolder(this.ElcFolderToUpgrade.Name)), ErrorCategory.InvalidOperation, null);
						}
					}
					else
					{
						this.upgradedContentSettings = new ElcContentSettings[]
						{
							new ElcContentSettings
							{
								RetentionEnabled = false,
								MessageClass = ElcMessageClass.AllMailboxContent
							}
						};
					}
					if (flag)
					{
						Array.Sort<ElcContentSettings>(this.upgradedContentSettings, delegate(ElcContentSettings x, ElcContentSettings y)
						{
							if (string.Compare(x.MessageClass, y.MessageClass, true) == 0)
							{
								return 0;
							}
							if (x.MessageClass == ElcMessageClass.AllMailboxContent)
							{
								return -1;
							}
							if (y.MessageClass == ElcMessageClass.AllMailboxContent)
							{
								return 1;
							}
							return x.MessageClass.Split(new char[]
							{
								'.'
							}).Length - y.MessageClass.Split(new char[]
							{
								'.'
							}).Length;
						});
					}
					RetentionPolicyTag dataObject = this.DataObject;
					IConfigurationSession session = base.DataSession as IConfigurationSession;
					for (int i = 0; i < this.upgradedContentSettings.Length; i++)
					{
						this.contentSettingsObject = this.upgradedContentSettings[i];
						this.InternalValidate();
						base.InternalProcessRecord();
						this.DataObject = new RetentionPolicyTag();
						this.DataObject.CopyChangesFrom(dataObject);
						base.Name = this.GetUniqueName<RetentionPolicyTag>(session, dataObject.Id.Parent, dataObject.Name, i + 1);
					}
				}
			}
			catch (DataSourceTransientException exception)
			{
				base.WriteError(exception, (ErrorCategory)1002, null);
			}
			finally
			{
				if (this.nonAtomicTagObject != null)
				{
					base.DataSession.Delete(this.nonAtomicTagObject);
				}
			}
		}

		// Token: 0x06001AD4 RID: 6868 RVA: 0x00077254 File Offset: 0x00075454
		private ElcContentSettings[] GetUpgradedConentSettings(ElcContentSettings[] folderContentSettings, bool defaultTag)
		{
			List<ElcContentSettings> upgradedSettings = new List<ElcContentSettings>();
			ElcContentSettings[] array = (from x in folderContentSettings
			where x.RetentionEnabled || x.JournalingEnabled
			select x).ToArray<ElcContentSettings>();
			folderContentSettings.Except(array).ForEach(delegate(ElcContentSettings x)
			{
				this.WriteWarning(Strings.CouldNotUpgradeDisabledContentSettings(x.Name));
			});
			RetentionActionType[] allowedRetentionActions = new RetentionActionType[]
			{
				RetentionActionType.PermanentlyDelete,
				RetentionActionType.DeleteAndAllowRecovery,
				RetentionActionType.MoveToDeletedItems
			};
			if (!defaultTag)
			{
				ElcContentSettings elcContentSettings = new ElcContentSettings();
				elcContentSettings.JournalingEnabled = false;
				elcContentSettings.RetentionEnabled = false;
				elcContentSettings.RetentionAction = RetentionActionType.PermanentlyDelete;
				ElcContentSettings[] array2 = (from x in array
				where x.RetentionEnabled
				select x).ToArray<ElcContentSettings>();
				ElcContentSettings[] array3 = (from x in array2
				where allowedRetentionActions.Any((RetentionActionType y) => x.RetentionAction == y)
				select x).ToArray<ElcContentSettings>();
				array2.Except(array3).ForEach(delegate(ElcContentSettings x)
				{
					this.WriteWarning(Strings.CouldNotUpgradeSpecificContentSetting(x.Name));
				});
				ElcContentSettings[] array4 = (from x in array3
				where x.TriggerForRetention == RetentionDateType.WhenDelivered
				select x).ToArray<ElcContentSettings>();
				array3.Except(array4).ForEach(delegate(ElcContentSettings x)
				{
					this.WriteWarning(Strings.CouldNotUpgradeRetentionTrigger(x.TriggerForRetention.ToString(), x.Name, RetentionDateType.WhenDelivered.ToString()));
				});
				array4.Aggregate(elcContentSettings, delegate(ElcContentSettings a, ElcContentSettings x)
				{
					if (a.AgeLimitForRetention == null)
					{
						a.AgeLimitForRetention = x.AgeLimitForRetention;
					}
					else if (x.AgeLimitForRetention != null && a.AgeLimitForRetention < x.AgeLimitForRetention)
					{
						a.AgeLimitForRetention = x.AgeLimitForRetention;
					}
					if (Array.FindIndex<RetentionActionType>(allowedRetentionActions, (RetentionActionType y) => y == a.RetentionAction) < Array.FindIndex<RetentionActionType>(allowedRetentionActions, (RetentionActionType y) => y == x.RetentionAction))
					{
						a.RetentionAction = x.RetentionAction;
					}
					a.RetentionEnabled = true;
					return a;
				});
				if (elcContentSettings.RetentionEnabled)
				{
					elcContentSettings.TriggerForRetention = RetentionDateType.WhenDelivered;
					elcContentSettings.MessageClass = ElcMessageClass.AllMailboxContent;
					(from x in array4
					where string.Compare(x.MessageClass, ElcMessageClass.AllMailboxContent, StringComparison.OrdinalIgnoreCase) != 0
					select x).ForEach(delegate(ElcContentSettings x)
					{
						this.WriteWarning(Strings.ChangedMessageClass(x.Name, x.MessageClass));
					});
				}
				ElcContentSettings[] array5 = (from x in array
				where x.JournalingEnabled
				select x).ToArray<ElcContentSettings>();
				if (array5.Length > 0)
				{
					elcContentSettings.JournalingEnabled = true;
					elcContentSettings.MessageFormatForJournaling = array5[0].MessageFormatForJournaling;
					elcContentSettings.AddressForJournaling = array5[0].AddressForJournaling;
					elcContentSettings.LabelForJournaling = array5[0].LabelForJournaling;
					array5.Skip(1).ForEach(delegate(ElcContentSettings x)
					{
						this.WriteWarning(Strings.CouldNotUpgradeJournaling(x.Name));
					});
				}
				if (elcContentSettings.RetentionEnabled || elcContentSettings.JournalingEnabled)
				{
					upgradedSettings.Add(elcContentSettings);
				}
			}
			else
			{
				array.ForEach(delegate(ElcContentSettings x)
				{
					ElcContentSettings elcContentSettings2 = new ElcContentSettings();
					if (x.RetentionEnabled)
					{
						if (allowedRetentionActions.Any((RetentionActionType y) => x.RetentionAction == y))
						{
							if (x.TriggerForRetention != RetentionDateType.WhenDelivered)
							{
								this.WriteWarning(Strings.CouldNotUpgradeRetentionTrigger(x.TriggerForRetention.ToString(), x.Name, RetentionDateType.WhenDelivered.ToString()));
							}
							else
							{
								elcContentSettings2.RetentionAction = x.RetentionAction;
								elcContentSettings2.AgeLimitForRetention = x.AgeLimitForRetention;
								elcContentSettings2.TriggerForRetention = RetentionDateType.WhenDelivered;
								elcContentSettings2.RetentionEnabled = x.RetentionEnabled;
								if (x.MessageClass.Equals(ElcMessageClass.AllMailboxContent, StringComparison.OrdinalIgnoreCase) || x.MessageClass.Equals(ElcMessageClass.VoiceMail, StringComparison.OrdinalIgnoreCase))
								{
									elcContentSettings2.MessageClass = x.MessageClass;
								}
								else
								{
									elcContentSettings2.MessageClass = ElcMessageClass.AllMailboxContent;
									this.WriteWarning(Strings.ChangedMessageClass(x.Name, x.MessageClass));
								}
							}
						}
						else
						{
							this.WriteWarning(Strings.CouldNotUpgradeSpecificContentSetting(x.Name));
						}
					}
					if (x.JournalingEnabled)
					{
						elcContentSettings2.MessageFormatForJournaling = x.MessageFormatForJournaling;
						elcContentSettings2.JournalingEnabled = x.JournalingEnabled;
						elcContentSettings2.AddressForJournaling = x.AddressForJournaling;
						elcContentSettings2.LabelForJournaling = x.LabelForJournaling;
						if (x.MessageClass.Equals(ElcMessageClass.AllMailboxContent, StringComparison.OrdinalIgnoreCase) || x.MessageClass.Equals(ElcMessageClass.VoiceMail, StringComparison.OrdinalIgnoreCase))
						{
							elcContentSettings2.MessageClass = x.MessageClass;
						}
						else
						{
							elcContentSettings2.MessageClass = ElcMessageClass.AllMailboxContent;
							this.WriteWarning(Strings.ChangedMessageClass(x.Name, x.MessageClass));
						}
					}
					if (elcContentSettings2.RetentionEnabled || elcContentSettings2.JournalingEnabled)
					{
						upgradedSettings.Add(elcContentSettings2);
					}
				});
			}
			return upgradedSettings.ToArray();
		}

		// Token: 0x06001AD5 RID: 6869 RVA: 0x0007750C File Offset: 0x0007570C
		private static string GenerateName(RetentionPolicyTag newTag, ElcContentSettings oldContentSettings)
		{
			StringBuilder stringBuilder = new StringBuilder(63, 63);
			stringBuilder.Append(newTag.Name);
			stringBuilder.Append('-');
			stringBuilder.Append(oldContentSettings.Name);
			if (stringBuilder.Length >= 64)
			{
				return stringBuilder.ToString(0, 63);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001AD6 RID: 6870 RVA: 0x00077560 File Offset: 0x00075760
		private string GetUniqueName<TObject>(IConfigurationSession session, ADObjectId parentId, string baseName, int hint) where TObject : ADConfigurationObject, new()
		{
			string text = string.Format("{0}-{1}", baseName, hint);
			for (int i = hint; i < hint + 10; i++)
			{
				if (session.Read<TObject>(parentId.GetChildId(text)) == null)
				{
					return text;
				}
				text = string.Format("{0}-{1}", baseName, i + 1);
			}
			return string.Format("{0}-{1}", baseName, Guid.NewGuid().ToString());
		}

		// Token: 0x06001AD7 RID: 6871 RVA: 0x000775DC File Offset: 0x000757DC
		private void MakeContentSettingUniqueAndSave(IConfigurationSession session, RetentionPolicyTag newTag, ElcContentSettings newContentSettings, string baseName)
		{
			if (newContentSettings != null)
			{
				if (!newTag.OrganizationId.Equals(OrganizationId.ForestWideOrgId) && newContentSettings.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
				{
					newContentSettings.OrganizationId = newTag.OrganizationId;
				}
				bool flag = false;
				int num = 1;
				while (!flag || num < 10)
				{
					try
					{
						session.Save(newContentSettings);
						flag = true;
					}
					catch (ADObjectAlreadyExistsException)
					{
						newContentSettings.SetId(newTag.Id.GetChildId(baseName + "-" + num.ToString()));
					}
					num++;
				}
				if (!flag)
				{
					base.ThrowTerminatingError(new CouldNotSaveContentSetting(baseName), ErrorCategory.InvalidData, null);
				}
			}
		}

		// Token: 0x04000BA4 RID: 2980
		private const string propRetentionEnabled = "RetentionEnabled";

		// Token: 0x04000BA5 RID: 2981
		private const string propAddressForJournaling = "AddressForJournaling";

		// Token: 0x04000BA6 RID: 2982
		private const int TenantTagLimit = 500;

		// Token: 0x04000BA7 RID: 2983
		private IList<RetentionPolicyTag> existingDefaultPolicyTags;

		// Token: 0x04000BA8 RID: 2984
		internal static Dictionary<string, string> MessageClassNameMaps = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"*",
				ElcMessageClass.AllMailboxContent
			},
			{
				"AllMailboxContent",
				ElcMessageClass.AllMailboxContent
			},
			{
				"VoiceMail",
				ElcMessageClass.VoiceMail
			}
		};

		// Token: 0x04000BA9 RID: 2985
		private ElcContentSettings contentSettingsObject;

		// Token: 0x04000BAA RID: 2986
		private ElcContentSettings[] upgradedContentSettings;

		// Token: 0x04000BAB RID: 2987
		private RetentionPolicyTag nonAtomicTagObject;

		// Token: 0x04000BAC RID: 2988
		private ELCFolder ElcFolderToUpgrade;
	}
}
