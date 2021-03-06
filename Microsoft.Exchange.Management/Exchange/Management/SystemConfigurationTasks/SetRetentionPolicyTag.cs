using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000320 RID: 800
	[Cmdlet("Set", "RetentionPolicyTag", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetRetentionPolicyTag : SetSystemConfigurationObjectTask<RetentionPolicyTagIdParameter, RetentionPolicyTag>
	{
		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x00077BF7 File Offset: 0x00075DF7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (this.Mailbox != null)
				{
					return Strings.ConfirmationMessageSetRetentionTagOnMailbox(this.Mailbox);
				}
				return Strings.ConfirmationMessageSetRetentionTag(this.Identity);
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06001B08 RID: 6920 RVA: 0x00077C18 File Offset: 0x00075E18
		// (set) Token: 0x06001B09 RID: 6921 RVA: 0x00077C25 File Offset: 0x00075E25
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool RetentionEnabled
		{
			get
			{
				return this.contentSettingsInstance.RetentionEnabled;
			}
			set
			{
				this.contentSettingsInstance.RetentionEnabled = value;
			}
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001B0A RID: 6922 RVA: 0x00077C33 File Offset: 0x00075E33
		// (set) Token: 0x06001B0B RID: 6923 RVA: 0x00077C40 File Offset: 0x00075E40
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public RetentionActionType RetentionAction
		{
			get
			{
				return this.contentSettingsInstance.RetentionAction;
			}
			set
			{
				this.contentSettingsInstance.RetentionAction = value;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x00077C4E File Offset: 0x00075E4E
		// (set) Token: 0x06001B0D RID: 6925 RVA: 0x00077C5B File Offset: 0x00075E5B
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string MessageClass
		{
			get
			{
				return this.contentSettingsInstance.MessageClass;
			}
			set
			{
				this.contentSettingsInstance.MessageClass = value;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x00077C69 File Offset: 0x00075E69
		// (set) Token: 0x06001B0F RID: 6927 RVA: 0x00077C76 File Offset: 0x00075E76
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public EnhancedTimeSpan? AgeLimitForRetention
		{
			get
			{
				return this.contentSettingsInstance.AgeLimitForRetention;
			}
			set
			{
				this.contentSettingsInstance.AgeLimitForRetention = value;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x00077C84 File Offset: 0x00075E84
		// (set) Token: 0x06001B11 RID: 6929 RVA: 0x00077C91 File Offset: 0x00075E91
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public bool JournalingEnabled
		{
			get
			{
				return this.contentSettingsInstance.JournalingEnabled;
			}
			set
			{
				this.contentSettingsInstance.JournalingEnabled = value;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x00077C9F File Offset: 0x00075E9F
		// (set) Token: 0x06001B13 RID: 6931 RVA: 0x00077CB6 File Offset: 0x00075EB6
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
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

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x00077CC9 File Offset: 0x00075EC9
		// (set) Token: 0x06001B15 RID: 6933 RVA: 0x00077CD6 File Offset: 0x00075ED6
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public JournalingFormat MessageFormatForJournaling
		{
			get
			{
				return this.contentSettingsInstance.MessageFormatForJournaling;
			}
			set
			{
				this.contentSettingsInstance.MessageFormatForJournaling = value;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x00077CE4 File Offset: 0x00075EE4
		// (set) Token: 0x06001B17 RID: 6935 RVA: 0x00077CF1 File Offset: 0x00075EF1
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public string LabelForJournaling
		{
			get
			{
				return this.contentSettingsInstance.LabelForJournaling;
			}
			set
			{
				this.contentSettingsInstance.LabelForJournaling = value;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x00077CFF File Offset: 0x00075EFF
		// (set) Token: 0x06001B19 RID: 6937 RVA: 0x00077D16 File Offset: 0x00075F16
		[Parameter(Mandatory = false)]
		public ELCFolderIdParameter LegacyManagedFolder
		{
			get
			{
				return (ELCFolderIdParameter)base.Fields["LegacyManagedFolder"];
			}
			set
			{
				base.Fields["LegacyManagedFolder"] = value;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x00077D29 File Offset: 0x00075F29
		// (set) Token: 0x06001B1B RID: 6939 RVA: 0x00077D40 File Offset: 0x00075F40
		[Parameter(Mandatory = true, ParameterSetName = "ParameterSetMailboxTask")]
		public MailboxIdParameter Mailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["Mailbox"];
			}
			set
			{
				base.Fields["Mailbox"] = value;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x00077D53 File Offset: 0x00075F53
		// (set) Token: 0x06001B1D RID: 6941 RVA: 0x00077D6A File Offset: 0x00075F6A
		[Parameter(Mandatory = false, ParameterSetName = "ParameterSetMailboxTask")]
		public RetentionPolicyTagIdParameter[] OptionalInMailbox
		{
			get
			{
				return (RetentionPolicyTagIdParameter[])base.Fields["OptionalInMailbox"];
			}
			set
			{
				base.Fields["OptionalInMailbox"] = value;
			}
		}

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x00077D7D File Offset: 0x00075F7D
		// (set) Token: 0x06001B1F RID: 6943 RVA: 0x00077DA3 File Offset: 0x00075FA3
		[Parameter(Mandatory = false)]
		public SwitchParameter Force
		{
			get
			{
				return (SwitchParameter)(base.Fields["Force"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["Force"] = value;
			}
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x00077E08 File Offset: 0x00076008
		private void ValidateRetentionPolicy()
		{
			IConfigurationSession session = base.DataSession as IConfigurationSession;
			session.SessionSettings.IsSharedConfigChecked = true;
			MultiValuedProperty<ADObjectId> first = (MultiValuedProperty<ADObjectId>)this.DataObject[RetentionPolicyTagSchema.PolicyIds];
			RetentionPolicy[] array = (from x in first
			select session.Read<RetentionPolicy>(x) into x
			where x != null
			select x).ToArray<RetentionPolicy>();
			PresentationRetentionPolicyTag[] second = new PresentationRetentionPolicyTag[]
			{
				new PresentationRetentionPolicyTag(this.DataObject, this.contentSettingsObject)
			};
			foreach (RetentionPolicy retentionPolicy in array)
			{
				PresentationRetentionPolicyTag[] retentionTags = (from x in retentionPolicy.RetentionPolicyTagLinks
				where !x.Equals(this.DataObject.Id)
				select session.Read<RetentionPolicyTag>(x) into x
				select new PresentationRetentionPolicyTag(x)).Concat(second).ToArray<PresentationRetentionPolicyTag>();
				if (this.DataObject.Type == ElcFolderType.All)
				{
					RetentionPolicyValidator.ValicateDefaultTags(retentionPolicy, retentionTags, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
				RetentionPolicyValidator.ValidateSystemFolderTags(retentionPolicy, retentionTags, new Task.TaskErrorLoggingDelegate(base.WriteError));
			}
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00077F90 File Offset: 0x00076190
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			if (!base.Fields.Contains("Mailbox"))
			{
				SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			if (base.ParameterSetName == "ParameterSetMailboxTask")
			{
				return;
			}
			if (base.Fields.IsModified("LegacyManagedFolder"))
			{
				if (this.LegacyManagedFolder != null)
				{
					ELCFolder elcfolder = (ELCFolder)base.GetDataObject<ELCFolder>(this.LegacyManagedFolder, base.DataSession, null, new LocalizedString?(Strings.ErrorElcFolderNotFound(this.LegacyManagedFolder.ToString())), new LocalizedString?(Strings.ErrorAmbiguousElcFolderId(this.LegacyManagedFolder.ToString())));
					this.DataObject.LegacyManagedFolder = elcfolder.Id;
				}
				else
				{
					this.DataObject.LegacyManagedFolder = null;
				}
			}
			base.InternalValidate();
			if (this.contentSettingsObject.IsChanged(ElcContentSettingsSchema.RetentionAction))
			{
				RetentionActionType[] source = new RetentionActionType[]
				{
					RetentionActionType.MoveToFolder
				};
				if (source.Any((RetentionActionType x) => x == this.contentSettingsObject.RetentionAction))
				{
					base.WriteError(new RetentionPolicyTagTaskException(Strings.ErrorRetentionActionNowAllowed), ErrorCategory.InvalidOperation, null);
				}
				if (this.DataObject.Type == ElcFolderType.RecoverableItems && !this.contentSettingsObject.RetentionAction.Equals(RetentionActionType.MoveToArchive))
				{
					base.WriteError(new ArgumentException(Strings.ErrorDumpsterTagWrongRetentionAction), ErrorCategory.InvalidArgument, this);
				}
				if (this.DataObject.Type != ElcFolderType.All && this.DataObject.Type != ElcFolderType.Personal && this.DataObject.Type != ElcFolderType.RecoverableItems && this.RetentionAction == RetentionActionType.MoveToArchive)
				{
					base.WriteError(new RetentionPolicyTagTaskException(Strings.ErrorMoveToArchiveAppliedToSystemFolder), ErrorCategory.InvalidArgument, null);
				}
			}
			if (this.contentSettingsObject.IsChanged(ElcContentSettingsSchema.MessageClass) && this.DataObject.Type != ElcFolderType.All && !this.contentSettingsObject.MessageClass.Equals(ElcMessageClass.AllMailboxContent))
			{
				base.WriteError(new RetentionPolicyTagTaskException(Strings.ErrorOnlyDefaultTagAllowCustomizedMessageClass), ErrorCategory.InvalidOperation, this.DataObject);
			}
			string tagName;
			if (this.DataObject.IsChanged(RetentionPolicyTagSchema.RetentionId) && !(base.DataSession as IConfigurationSession).CheckForRetentionTagWithConflictingRetentionId(this.DataObject.RetentionId, this.DataObject.Identity.ToString(), out tagName))
			{
				base.WriteError(new RetentionPolicyTagTaskException(Strings.ErrorRetentionIdConflictsWithRetentionTag(this.DataObject.RetentionId.ToString(), tagName)), ErrorCategory.InvalidOperation, this.DataObject);
			}
			if (this.contentSettingsObject.IsChanged(ElcContentSettingsSchema.RetentionAction) || this.contentSettingsObject.IsChanged(ElcContentSettingsSchema.RetentionEnabled) || this.contentSettingsObject.IsChanged(ElcContentSettingsSchema.MessageClass))
			{
				this.ValidateRetentionPolicy();
			}
			if (base.Fields.IsModified("AddressForJournaling"))
			{
				if (this.AddressForJournaling != null)
				{
					ADRecipient adrecipient = (ADRecipient)base.GetDataObject<ADRecipient>(this.AddressForJournaling, base.TenantGlobalCatalogSession, null, new LocalizedString?(Strings.ErrorMailboxAddressNotFound(this.AddressForJournaling.ToString())), new LocalizedString?(Strings.ErrorMailboxAddressNotUnique(this.AddressForJournaling.ToString())));
					if (!this.DataObject.OrganizationId.Equals(OrganizationId.ForestWideOrgId))
					{
						RecipientTaskHelper.CheckRecipientInSameOrganizationWithDataObject(this.DataObject, adrecipient, new Task.ErrorLoggerDelegate(base.WriteError));
					}
					if (adrecipient.EmailAddresses == null || adrecipient.EmailAddresses.FindPrimary(ProxyAddressPrefix.Smtp) == null)
					{
						base.WriteError(new ArgumentException(Strings.SmtpAddressMissingForAutocopy(this.AddressForJournaling.ToString()), "AddressForJournaling"), ErrorCategory.InvalidData, this);
					}
					this.contentSettingsObject.AddressForJournaling = adrecipient.Id;
				}
				else
				{
					this.contentSettingsObject.AddressForJournaling = null;
				}
			}
			ValidationError[] array = this.contentSettingsObject.Validate();
			if (array.Length > 0)
			{
				for (int i = 0; i < array.Length; i++)
				{
					this.WriteError(new DataValidationException(array[i]), (ErrorCategory)1003, this.contentSettingsObject.Identity, array.Length - 1 == i);
				}
			}
			if (base.HasErrors)
			{
				return;
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x000783AC File Offset: 0x000765AC
		protected override IConfigurable PrepareDataObject()
		{
			RetentionPolicyTag retentionPolicyTag = (RetentionPolicyTag)base.PrepareDataObject();
			ElcContentSettings[] array = retentionPolicyTag.GetELCContentSettings().ToArray<ElcContentSettings>();
			if (array.Length > 1)
			{
				base.WriteError(new RetentionPolicyTagTaskException(Strings.ErrorManagedConentSettinsNonUnique(retentionPolicyTag.Identity.ToString())), ErrorCategory.InvalidOperation, null);
			}
			this.contentSettingsObject = array[0];
			this.contentSettingsObject.CopyChangesFrom(this.contentSettingsInstance);
			if (this.contentSettingsObject.IsChanged(ElcContentSettingsSchema.MessageClass))
			{
				if (NewRetentionPolicyTag.MessageClassNameMaps.ContainsKey(this.contentSettingsObject.MessageClass))
				{
					this.contentSettingsObject.MessageClass = NewRetentionPolicyTag.MessageClassNameMaps[this.contentSettingsObject.MessageClass];
				}
				if (!NewRetentionPolicyTag.MessageClassNameMaps.Values.Contains(this.contentSettingsObject.MessageClass, StringComparer.OrdinalIgnoreCase))
				{
					base.WriteError(new RetentionPolicyTagTaskException(Strings.MessageClassIsNotValid(this.contentSettingsObject.MessageClass)), ErrorCategory.InvalidArgument, null);
				}
			}
			return retentionPolicyTag;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000784A0 File Offset: 0x000766A0
		protected override bool IsObjectStateChanged()
		{
			return base.IsObjectStateChanged() || this.contentSettingsObject.ObjectState == ObjectState.Changed;
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000784BC File Offset: 0x000766BC
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (this.DataObject != null && SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			if (this.Mailbox != null)
			{
				ExchangePrincipal exchangePrincipal = this.ProcessPrimaryMailbox();
				IRecipientSession recipientSession;
				ADUser user = StoreRetentionPolicyTagHelper.FetchRecipientFromMailboxId(base.DomainController, this.Mailbox, out recipientSession, exchangePrincipal.MailboxInfo.OrganizationId);
				if (exchangePrincipal != null && StoreRetentionPolicyTagHelper.HasOnPremArchiveMailbox(user))
				{
					this.ProcessArchiveMailbox();
				}
			}
			else
			{
				bool flag = this.DataObject.IsChanged(ADObjectSchema.Name);
				if (this.DataObject.IsChanged(RetentionPolicyTagSchema.RetentionId) && !this.Force && !base.ShouldContinue(Strings.WarningRetentionTagIdChange(this.DataObject.Identity.ToString())))
				{
					return;
				}
				base.InternalProcessRecord();
				if (this.contentSettingsObject.ObjectState == ObjectState.Changed)
				{
					if (flag)
					{
						ElcContentSettings elcContentSettings = this.DataObject.GetELCContentSettings().FirstOrDefault<ElcContentSettings>();
						elcContentSettings.CopyChangesFrom(this.contentSettingsObject);
						this.contentSettingsObject = elcContentSettings;
					}
					base.DataSession.Save(this.contentSettingsObject);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x00078604 File Offset: 0x00076804
		internal static List<StoreTagData> ResolveTags(IConfigurationSession session, RetentionPolicyTagIdParameter[] tags)
		{
			List<StoreTagData> list = new List<StoreTagData>((tags != null) ? tags.Length : 1);
			if (tags != null)
			{
				foreach (RetentionPolicyTagIdParameter tagId in tags)
				{
					StoreTagData storeTagData = SetRetentionPolicyTag.ResolveTag(session, tagId);
					if (storeTagData != null)
					{
						list.Add(storeTagData);
					}
				}
			}
			return list;
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x00078650 File Offset: 0x00076850
		internal static StoreTagData ResolveTag(IConfigurationSession session, RetentionPolicyTagIdParameter tagId)
		{
			RetentionPolicyTag retentionPolicyTag = null;
			IEnumerable<RetentionPolicyTag> objects = tagId.GetObjects<RetentionPolicyTag>(null, session);
			if (objects != null)
			{
				foreach (RetentionPolicyTag retentionPolicyTag2 in objects)
				{
					if (retentionPolicyTag != null)
					{
						throw new ManagementObjectAmbiguousException(Strings.ErrorAmbiguousRetentionPolicyTagId(tagId.ToString()));
					}
					retentionPolicyTag = retentionPolicyTag2;
				}
			}
			if (retentionPolicyTag == null)
			{
				throw new ManagementObjectNotFoundException(Strings.ErrorRetentionTagNotFound(tagId.ToString()));
			}
			AdTagData adTagData = AdTagReader.FetchTagContentSettings(retentionPolicyTag);
			return new StoreTagData(adTagData);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x000786E4 File Offset: 0x000768E4
		protected override IConfigDataProvider CreateSession()
		{
			if (SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
			{
				return SharedConfiguration.CreateScopedToSharedConfigADSession(base.CurrentOrganizationId);
			}
			IConfigurationSession result;
			if (!MobileDeviceTaskHelper.IsRunningUnderMyOptionsRole(this, base.TenantGlobalCatalogSession, base.SessionSettings))
			{
				result = (IConfigurationSession)base.CreateSession();
			}
			else
			{
				ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, true);
				result = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 597, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Elc\\SetRetentionPolicyTag.cs");
			}
			return result;
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x00078770 File Offset: 0x00076970
		private ExchangePrincipal ProcessPrimaryMailbox()
		{
			ExchangePrincipal result;
			this.Process(false, out result);
			return result;
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x00078788 File Offset: 0x00076988
		private void ProcessArchiveMailbox()
		{
			LocalizedString? localizedString = null;
			ExchangePrincipal exchangePrincipal;
			try
			{
				this.Process(true, out exchangePrincipal);
			}
			catch (StorageTransientException ex)
			{
				localizedString = new LocalizedString?(Strings.ExceptionStorageOther(ex.ErrorCode, ex.Message));
			}
			catch (StoragePermanentException ex2)
			{
				if (ex2 is AccessDeniedException)
				{
					localizedString = new LocalizedString?(Strings.ExceptionStorageAccessDenied(ex2.ErrorCode, ex2.Message));
				}
				else
				{
					localizedString = new LocalizedString?(Strings.ExceptionStorageOther(ex2.ErrorCode, ex2.Message));
				}
			}
			if (localizedString != null)
			{
				base.WriteError(new RetentionPolicyTagTaskException(Strings.WarningArchiveMailboxNotReachable(this.Mailbox.ToString())), ErrorCategory.ResourceUnavailable, null);
			}
			exchangePrincipal = null;
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x0007884C File Offset: 0x00076A4C
		private void Process(bool archiveMailbox, out ExchangePrincipal exchPrincipal)
		{
			ExchangePrincipal exchangePrincipal = null;
			try
			{
				using (StoreRetentionPolicyTagHelper storeRetentionPolicyTagHelper = StoreRetentionPolicyTagHelper.FromMailboxId(base.DomainController, this.Mailbox, archiveMailbox, base.CurrentOrganizationId))
				{
					exchangePrincipal = storeRetentionPolicyTagHelper.UserPrincipal;
					ELCTaskHelper.VerifyIsInScopes(storeRetentionPolicyTagHelper.Mailbox, base.ScopeSet, new Task.TaskErrorLoggingDelegate(base.WriteError));
					if (storeRetentionPolicyTagHelper.Mailbox.ExchangeVersion.IsOlderThan(ExchangeObjectVersion.Exchange2010))
					{
						base.WriteError(new InvalidOperationException(Strings.OptInNotSupportedForPre14Mailbox(ExchangeObjectVersion.Exchange2010.ToString(), storeRetentionPolicyTagHelper.Mailbox.Identity.ToString(), storeRetentionPolicyTagHelper.Mailbox.ExchangeVersion.ToString())), ErrorCategory.InvalidOperation, storeRetentionPolicyTagHelper.Mailbox.Identity);
					}
					if (storeRetentionPolicyTagHelper.Mailbox.RetentionPolicy == null && !storeRetentionPolicyTagHelper.Mailbox.ShouldUseDefaultRetentionPolicy)
					{
						base.WriteError(new ArgumentException(Strings.RetentionPolicyNotEnabled, "Mailbox"), ErrorCategory.InvalidArgument, null);
					}
					IConfigurationSession configurationSession = base.DataSession as IConfigurationSession;
					configurationSession.SessionSettings.IsSharedConfigChecked = true;
					List<StoreTagData> optionalStoreTags = SetRetentionPolicyTag.ResolveTags(configurationSession, this.OptionalInMailbox);
					this.ProcessOptionalTags(storeRetentionPolicyTagHelper.TagData, optionalStoreTags, archiveMailbox);
					storeRetentionPolicyTagHelper.Save();
				}
			}
			catch (ElcUserConfigurationException exception)
			{
				base.WriteError(exception, ErrorCategory.ResourceUnavailable, null);
			}
			exchPrincipal = exchangePrincipal;
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000789C4 File Offset: 0x00076BC4
		private void ProcessOptionalTags(Dictionary<Guid, StoreTagData> tagData, List<StoreTagData> optionalStoreTags, bool archiveMailboxTag)
		{
			if (tagData != null && tagData.Values != null)
			{
				foreach (StoreTagData storeTagData in tagData.Values)
				{
					if (storeTagData.OptedInto)
					{
						storeTagData.IsVisible = false;
					}
				}
			}
			foreach (StoreTagData storeTagData2 in optionalStoreTags)
			{
				if (!archiveMailboxTag || !storeTagData2.Tag.IsArchiveTag)
				{
					if (storeTagData2.Tag.Type != ElcFolderType.Personal)
					{
						base.WriteError(new ArgumentException(Strings.OptionalRetentionPolicyTagsMustBePersonalTags(storeTagData2.Tag.Name, storeTagData2.Tag.Type.ToString())), ErrorCategory.InvalidArgument, storeTagData2);
					}
					StoreTagData storeTagData3 = null;
					if (tagData.TryGetValue(storeTagData2.Tag.RetentionId, out storeTagData3))
					{
						if (!storeTagData3.IsVisible)
						{
							storeTagData3.OptedInto = true;
							storeTagData3.IsVisible = true;
						}
					}
					else
					{
						storeTagData2.OptedInto = true;
						storeTagData2.IsVisible = true;
						tagData.Add(storeTagData2.Tag.RetentionId, storeTagData2);
					}
				}
			}
		}

		// Token: 0x04000BB5 RID: 2997
		private const string propAddressForJournaling = "AddressForJournaling";

		// Token: 0x04000BB6 RID: 2998
		private const string propType = "Type";

		// Token: 0x04000BB7 RID: 2999
		private const string legacyManagedFolder = "LegacyManagedFolder";

		// Token: 0x04000BB8 RID: 3000
		public const string ParameterSetMailboxTask = "ParameterSetMailboxTask";

		// Token: 0x04000BB9 RID: 3001
		private ElcContentSettings contentSettingsInstance = new ElcContentSettings();

		// Token: 0x04000BBA RID: 3002
		private ElcContentSettings contentSettingsObject;
	}
}
