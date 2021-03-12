using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Management.Tasks.MailboxSearch;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000754 RID: 1876
	[Cmdlet("Set", "MailboxSearch", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SetMailboxSearch : SetTenantADTaskBase<EwsStoreObjectIdParameter, MailboxDiscoverySearch, MailboxDiscoverySearch>
	{
		// Token: 0x17001446 RID: 5190
		// (get) Token: 0x060042B9 RID: 17081 RVA: 0x00111A44 File Offset: 0x0010FC44
		// (set) Token: 0x060042BA RID: 17082 RVA: 0x00111A5B File Offset: 0x0010FC5B
		[Parameter(Mandatory = false)]
		public string SearchQuery
		{
			get
			{
				return (string)base.Fields["SearchQuery"];
			}
			set
			{
				base.Fields["SearchQuery"] = value;
			}
		}

		// Token: 0x17001447 RID: 5191
		// (get) Token: 0x060042BB RID: 17083 RVA: 0x00111A6E File Offset: 0x0010FC6E
		// (set) Token: 0x060042BC RID: 17084 RVA: 0x00111A8A File Offset: 0x0010FC8A
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] SourceMailboxes
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[SearchObjectSchema.SourceMailboxes.Name];
			}
			set
			{
				base.Fields[SearchObjectSchema.SourceMailboxes.Name] = value;
			}
		}

		// Token: 0x17001448 RID: 5192
		// (get) Token: 0x060042BD RID: 17085 RVA: 0x00111AA2 File Offset: 0x0010FCA2
		// (set) Token: 0x060042BE RID: 17086 RVA: 0x00111ABE File Offset: 0x0010FCBE
		[Parameter(Mandatory = false)]
		public MailboxIdParameter TargetMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields[SearchObjectSchema.TargetMailbox.Name];
			}
			set
			{
				base.Fields[SearchObjectSchema.TargetMailbox.Name] = value;
			}
		}

		// Token: 0x17001449 RID: 5193
		// (get) Token: 0x060042BF RID: 17087 RVA: 0x00111AD6 File Offset: 0x0010FCD6
		// (set) Token: 0x060042C0 RID: 17088 RVA: 0x00111AF2 File Offset: 0x0010FCF2
		[Parameter(Mandatory = false)]
		public PublicFolderIdParameter[] PublicFolderSources
		{
			get
			{
				return (PublicFolderIdParameter[])base.Fields[MailboxDiscoverySearchSchema.PublicFolderSources.Name];
			}
			set
			{
				base.Fields[MailboxDiscoverySearchSchema.PublicFolderSources.Name] = value;
			}
		}

		// Token: 0x1700144A RID: 5194
		// (get) Token: 0x060042C1 RID: 17089 RVA: 0x00111B0A File Offset: 0x0010FD0A
		// (set) Token: 0x060042C2 RID: 17090 RVA: 0x00111B30 File Offset: 0x0010FD30
		[Parameter(Mandatory = false)]
		public bool AllPublicFolderSources
		{
			get
			{
				return (bool)(base.Fields[MailboxDiscoverySearchSchema.AllPublicFolderSources.Name] ?? false);
			}
			set
			{
				base.Fields[MailboxDiscoverySearchSchema.AllPublicFolderSources.Name] = value;
			}
		}

		// Token: 0x1700144B RID: 5195
		// (get) Token: 0x060042C3 RID: 17091 RVA: 0x00111B4D File Offset: 0x0010FD4D
		// (set) Token: 0x060042C4 RID: 17092 RVA: 0x00111B73 File Offset: 0x0010FD73
		[Parameter(Mandatory = false)]
		public bool AllSourceMailboxes
		{
			get
			{
				return (bool)(base.Fields[MailboxDiscoverySearchSchema.AllSourceMailboxes.Name] ?? false);
			}
			set
			{
				base.Fields[MailboxDiscoverySearchSchema.AllSourceMailboxes.Name] = value;
			}
		}

		// Token: 0x1700144C RID: 5196
		// (get) Token: 0x060042C5 RID: 17093 RVA: 0x00111B90 File Offset: 0x0010FD90
		// (set) Token: 0x060042C6 RID: 17094 RVA: 0x00111BAC File Offset: 0x0010FDAC
		[Parameter(Mandatory = false)]
		public string[] Senders
		{
			get
			{
				return (string[])base.Fields[SearchObjectSchema.Senders.Name];
			}
			set
			{
				base.Fields[SearchObjectSchema.Senders.Name] = value;
			}
		}

		// Token: 0x1700144D RID: 5197
		// (get) Token: 0x060042C7 RID: 17095 RVA: 0x00111BC4 File Offset: 0x0010FDC4
		// (set) Token: 0x060042C8 RID: 17096 RVA: 0x00111BE0 File Offset: 0x0010FDE0
		[Parameter(Mandatory = false)]
		public string[] Recipients
		{
			get
			{
				return (string[])base.Fields[SearchObjectSchema.Recipients.Name];
			}
			set
			{
				base.Fields[SearchObjectSchema.Recipients.Name] = value;
			}
		}

		// Token: 0x1700144E RID: 5198
		// (get) Token: 0x060042C9 RID: 17097 RVA: 0x00111BF8 File Offset: 0x0010FDF8
		// (set) Token: 0x060042CA RID: 17098 RVA: 0x00111C14 File Offset: 0x0010FE14
		[Parameter(Mandatory = false)]
		public ExDateTime? StartDate
		{
			get
			{
				return (ExDateTime?)base.Fields[SearchObjectSchema.StartDate.Name];
			}
			set
			{
				base.Fields[SearchObjectSchema.StartDate.Name] = value;
			}
		}

		// Token: 0x1700144F RID: 5199
		// (get) Token: 0x060042CB RID: 17099 RVA: 0x00111C31 File Offset: 0x0010FE31
		// (set) Token: 0x060042CC RID: 17100 RVA: 0x00111C4D File Offset: 0x0010FE4D
		[Parameter(Mandatory = false)]
		public ExDateTime? EndDate
		{
			get
			{
				return (ExDateTime?)base.Fields[SearchObjectSchema.EndDate.Name];
			}
			set
			{
				base.Fields[SearchObjectSchema.EndDate.Name] = value;
			}
		}

		// Token: 0x17001450 RID: 5200
		// (get) Token: 0x060042CD RID: 17101 RVA: 0x00111C6A File Offset: 0x0010FE6A
		// (set) Token: 0x060042CE RID: 17102 RVA: 0x00111C86 File Offset: 0x0010FE86
		[Parameter(Mandatory = false)]
		public KindKeyword[] MessageTypes
		{
			get
			{
				return (KindKeyword[])base.Fields[SearchObjectSchema.MessageTypes.Name];
			}
			set
			{
				base.Fields[SearchObjectSchema.MessageTypes.Name] = value;
			}
		}

		// Token: 0x17001451 RID: 5201
		// (get) Token: 0x060042CF RID: 17103 RVA: 0x00111C9E File Offset: 0x0010FE9E
		// (set) Token: 0x060042D0 RID: 17104 RVA: 0x00111CBA File Offset: 0x0010FEBA
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] StatusMailRecipients
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[SearchObjectSchema.StatusMailRecipients.Name];
			}
			set
			{
				base.Fields[SearchObjectSchema.StatusMailRecipients.Name] = value;
			}
		}

		// Token: 0x17001452 RID: 5202
		// (get) Token: 0x060042D1 RID: 17105 RVA: 0x00111CD2 File Offset: 0x0010FED2
		// (set) Token: 0x060042D2 RID: 17106 RVA: 0x00111CEE File Offset: 0x0010FEEE
		internal RecipientIdParameter[] ManagedBy
		{
			get
			{
				return (RecipientIdParameter[])base.Fields[SearchObjectSchema.ManagedBy.Name];
			}
			set
			{
				base.Fields[SearchObjectSchema.ManagedBy.Name] = value;
			}
		}

		// Token: 0x17001453 RID: 5203
		// (get) Token: 0x060042D3 RID: 17107 RVA: 0x00111D06 File Offset: 0x0010FF06
		// (set) Token: 0x060042D4 RID: 17108 RVA: 0x00111D2C File Offset: 0x0010FF2C
		[Parameter]
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

		// Token: 0x17001454 RID: 5204
		// (get) Token: 0x060042D5 RID: 17109 RVA: 0x00111D44 File Offset: 0x0010FF44
		// (set) Token: 0x060042D6 RID: 17110 RVA: 0x00111D6A File Offset: 0x0010FF6A
		[Parameter(Mandatory = false)]
		public bool EstimateOnly
		{
			get
			{
				return (bool)(base.Fields[SearchObjectSchema.EstimateOnly.Name] ?? false);
			}
			set
			{
				base.Fields[SearchObjectSchema.EstimateOnly.Name] = value;
			}
		}

		// Token: 0x17001455 RID: 5205
		// (get) Token: 0x060042D7 RID: 17111 RVA: 0x00111D87 File Offset: 0x0010FF87
		// (set) Token: 0x060042D8 RID: 17112 RVA: 0x00111DB2 File Offset: 0x0010FFB2
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeKeywordStatistics
		{
			get
			{
				return (SwitchParameter)(base.Fields[SearchObjectSchema.IncludeKeywordStatistics.Name] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields[SearchObjectSchema.IncludeKeywordStatistics.Name] = value;
			}
		}

		// Token: 0x17001456 RID: 5206
		// (get) Token: 0x060042D9 RID: 17113 RVA: 0x00111DCF File Offset: 0x0010FFCF
		// (set) Token: 0x060042DA RID: 17114 RVA: 0x00111DF5 File Offset: 0x0010FFF5
		[Parameter(Mandatory = false)]
		public int StatisticsStartIndex
		{
			get
			{
				return (int)(base.Fields[MailboxDiscoverySearchSchema.StatisticsStartIndex.Name] ?? 1);
			}
			set
			{
				base.Fields[MailboxDiscoverySearchSchema.StatisticsStartIndex.Name] = value;
			}
		}

		// Token: 0x060042DB RID: 17115 RVA: 0x00111E14 File Offset: 0x00110014
		private ADObjectId ADObjectIdFromRecipientIdParameter(RecipientIdParameter recipientId, object param)
		{
			ADRecipient adrecipient = base.GetDataObject<ADRecipient>(recipientId, this.recipientSession, null, new LocalizedString?(Strings.ExceptionUserObjectNotFound(recipientId.ToString())), new LocalizedString?(Strings.ExceptionUserObjectAmbiguous)) as ADRecipient;
			if (param != null)
			{
				RecipientType[] array = param as RecipientType[];
				foreach (RecipientType recipientType in array)
				{
					if (adrecipient.RecipientType == recipientType || RemoteMailbox.IsRemoteMailbox(adrecipient.RecipientTypeDetails))
					{
						return adrecipient.Id;
					}
				}
				base.WriteError(new MailboxSearchTaskException(Strings.ErrorInvalidRecipientType(adrecipient.ToString(), adrecipient.RecipientType.ToString())), ErrorCategory.InvalidArgument, null);
			}
			return adrecipient.Id;
		}

		// Token: 0x17001457 RID: 5207
		// (get) Token: 0x060042DC RID: 17116 RVA: 0x00111EC9 File Offset: 0x001100C9
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.SetMailboxSearchConfirmation(this.Identity.ToString());
			}
		}

		// Token: 0x060042DD RID: 17117 RVA: 0x00111EDC File Offset: 0x001100DC
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = Utils.CreateRecipientSession(base.DomainController, base.SessionSettings);
			this.recipientSession = recipientSession;
			return new DiscoverySearchDataProvider(base.CurrentOrganizationId);
		}

		// Token: 0x060042DE RID: 17118 RVA: 0x00111F10 File Offset: 0x00110110
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (!base.HasErrors)
			{
				if (this.DataObject.ExcludeDuplicateMessages && this.EstimateOnly)
				{
					base.WriteError(new MailboxSearchTaskException(Strings.ExcludeDuplicateMessagesParameterConflict), ErrorCategory.InvalidArgument, null);
				}
				if (this.DataObject.StatisticsOnly && this.DataObject.LogLevel != LoggingLevel.Suppress)
				{
					base.WriteError(new MailboxSearchTaskException(Strings.EstimateOnlyLogLevelParameterConflict(this.DataObject.LogLevel.ToString())), ErrorCategory.InvalidArgument, "LogLevel");
				}
				if (base.ExchangeRunspaceConfig == null)
				{
					base.WriteError(new MailboxSearchTaskException(Strings.UnableToDetermineExecutingUser), ErrorCategory.InvalidOperation, null);
				}
				if (!this.DataObject.StatisticsOnly && this.DataObject.IncludeKeywordStatistics)
				{
					base.WriteError(new MailboxSearchTaskException(Strings.IncludeKeywordStatisticsParameterConflict), ErrorCategory.InvalidArgument, null);
				}
				if (this.DataObject.Target != null)
				{
					ADRecipient adrecipient = this.recipientSession.FindByLegacyExchangeDN(this.DataObject.Target);
					if (adrecipient != null)
					{
						this.targetUser = (ADUser)this.recipientSession.Read(adrecipient.Id);
					}
					if (this.targetUser == null)
					{
						base.WriteError(new ObjectNotFoundException(Strings.ExceptionTargetMailboxNotFound(this.DataObject.Target, this.DataObject.Name)), ErrorCategory.InvalidOperation, null);
					}
					bool flag = Utils.VerifyMailboxVersionIsSP1OrGreater(this.targetUser);
					if ((this.EstimateOnly || this.DataObject.ExcludeDuplicateMessages) && !flag)
					{
						base.WriteError(new MailboxSearchTaskException(Strings.ErrorMailboxVersionTooOld(this.targetUser.Id.ToString())), ErrorCategory.InvalidOperation, null);
					}
				}
				if (this.DataObject.IsChanged(EwsStoreObjectSchema.AlternativeId) && Utils.SameNameExists(this.DataObject.Name, (DiscoverySearchDataProvider)base.DataSession, Utils.GetMailboxDataProvider(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, new Task.TaskErrorLoggingDelegate(base.WriteError))))
				{
					base.WriteError(new MailboxSearchNameIsNotUniqueException(this.DataObject.Name), ErrorCategory.InvalidArgument, this.DataObject);
				}
				Exception ex = Utils.ValidateSourceAndTargetMailboxes((DiscoverySearchDataProvider)base.DataSession, this.DataObject);
				if (ex != null)
				{
					base.WriteError(ex, ErrorCategory.InvalidArgument, null);
				}
				if (this.DataObject.IsChanged(MailboxDiscoverySearchSchema.Query) || this.DataObject.IsChanged(MailboxDiscoverySearchSchema.Sources) || this.DataObject.IsChanged(MailboxDiscoverySearchSchema.StatisticsOnly) || this.DataObject.IsChanged(MailboxDiscoverySearchSchema.IncludeUnsearchableItems) || this.DataObject.IsChanged(MailboxDiscoverySearchSchema.Language) || this.DataObject.IsChanged(MailboxDiscoverySearchSchema.LogLevel) || this.DataObject.IsChanged(MailboxDiscoverySearchSchema.StatusMailRecipients) || this.DataObject.IsChanged(MailboxDiscoverySearchSchema.Target))
				{
					Utils.CheckSearchRunningStatus(this.DataObject, new Task.TaskErrorLoggingDelegate(base.WriteError), Strings.MailboxSearchIsInProgress(this.DataObject.Name));
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x00112208 File Offset: 0x00110408
		protected override IConfigurable ResolveDataObject()
		{
			string text = this.Identity.ToString();
			MailboxDataProvider mailboxDataProvider = Utils.GetMailboxDataProvider(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, new Task.TaskErrorLoggingDelegate(base.WriteError));
			SearchObject searchObject;
			if (Utils.IsLegacySearchObjectIdentity(text))
			{
				searchObject = (SearchObject)base.GetDataObject<SearchObject>(new SearchObjectIdParameter(text), mailboxDataProvider, this.RootId, base.OptionalIdentityData, new LocalizedString?(Strings.ErrorManagementObjectNotFound(text)), new LocalizedString?(Strings.ErrorManagementObjectAmbiguous(text)));
			}
			else
			{
				searchObject = Utils.GetE14SearchObjectByName(text, mailboxDataProvider);
			}
			if (searchObject == null)
			{
				return base.ResolveDataObject();
			}
			if (!this.Force && !base.ShouldContinue(Strings.EditWillUpgradeSearchObject))
			{
				base.WriteError(new MailboxSearchTaskException(Strings.CannotEditLegacySearchObjectWithoutUpgrade(searchObject.Name)), ErrorCategory.InvalidArgument, text);
			}
			return Utils.UpgradeLegacySearchObject(searchObject, mailboxDataProvider, (DiscoverySearchDataProvider)base.DataSession, new Task.TaskErrorLoggingDelegate(base.WriteError), new Action<LocalizedString>(this.WriteWarning));
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x00112358 File Offset: 0x00110558
		protected override IConfigurable PrepareDataObject()
		{
			MailboxDiscoverySearch mailboxDiscoverySearch = (MailboxDiscoverySearch)base.PrepareDataObject();
			string[] paramNames = new string[]
			{
				"AllSourceMailboxes",
				"AllPublicFolderSources",
				"PublicFolderSources"
			};
			ScopeSet scopeSet = base.ScopeSet;
			bool flag = false;
			if (scopeSet == null)
			{
				scopeSet = ScopeSet.GetOrgWideDefaultScopeSet(base.CurrentOrganizationId);
			}
			if (scopeSet != null)
			{
				ADRawEntry executingUser = base.ExchangeRunspaceConfig.ExecutingUser;
				if (executingUser != null)
				{
					flag = base.ExchangeRunspaceConfig.IsCmdletAllowedInScope("Set-MailboxSearch", paramNames, executingUser, ScopeLocation.RecipientWrite);
				}
				else
				{
					flag = base.ExchangeRunspaceConfig.IsCmdletAllowedInScope("Set-MailboxSearch", paramNames, scopeSet);
				}
			}
			if (flag && mailboxDiscoverySearch.Version == SearchObjectVersion.Original && (mailboxDiscoverySearch.Sources == null || mailboxDiscoverySearch.Sources.Count == 0) && (mailboxDiscoverySearch.PublicFolderSources == null || mailboxDiscoverySearch.PublicFolderSources.Count == 0) && !mailboxDiscoverySearch.AllSourceMailboxes && !mailboxDiscoverySearch.AllPublicFolderSources)
			{
				this.AllSourceMailboxes = true;
			}
			if (flag)
			{
				mailboxDiscoverySearch.Version = SearchObjectVersion.SecondVersion;
			}
			if (base.Fields.IsModified(SearchObjectSchema.TargetMailbox.Name))
			{
				if (this.TargetMailbox != null)
				{
					try
					{
						this.recipientSession.SessionSettings.IncludeInactiveMailbox = false;
						ADUser aduser = (ADUser)base.GetDataObject<ADUser>(this.TargetMailbox, this.recipientSession, null, new LocalizedString?(Strings.ExceptionUserObjectNotFound(this.TargetMailbox.ToString())), new LocalizedString?(Strings.ExceptionUserObjectAmbiguous));
						if (aduser.RecipientType != RecipientType.UserMailbox)
						{
							base.ThrowTerminatingError(new MailboxSearchTaskException(Strings.ErrorInvalidRecipientType(aduser.ToString(), aduser.RecipientType.ToString())), ErrorCategory.InvalidArgument, SearchObjectSchema.TargetMailbox.Name);
						}
						mailboxDiscoverySearch.Target = aduser.LegacyExchangeDN;
						if (base.ScopeSet != null)
						{
							Utils.VerifyIsInScopes(aduser, base.ScopeSet, new Task.TaskErrorLoggingDelegate(base.WriteError));
						}
						Utils.VerifyMailboxVersion(aduser, new Task.TaskErrorLoggingDelegate(base.WriteError));
						goto IL_20B;
					}
					finally
					{
						if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
						{
							this.recipientSession.SessionSettings.IncludeInactiveMailbox = true;
						}
					}
				}
				mailboxDiscoverySearch.Target = null;
			}
			IL_20B:
			if (flag)
			{
				if (base.Fields.IsModified(MailboxDiscoverySearchSchema.AllSourceMailboxes.Name))
				{
					mailboxDiscoverySearch.AllSourceMailboxes = this.AllSourceMailboxes;
				}
				if (this.AllSourceMailboxes)
				{
					if (this.SourceMailboxes != null || mailboxDiscoverySearch.Sources != null)
					{
						this.WriteWarning(Strings.AllSourceMailboxesParameterOverride("AllSourceMailboxes", "SourceMailboxes"));
					}
					this.SourceMailboxes = null;
					mailboxDiscoverySearch.Sources = null;
				}
			}
			bool flag2 = base.Fields.IsModified(SearchObjectSchema.SourceMailboxes.Name);
			if (!flag2)
			{
				if (!mailboxDiscoverySearch.IsChanged(MailboxDiscoverySearchSchema.InPlaceHoldEnabled) || !mailboxDiscoverySearch.InPlaceHoldEnabled)
				{
					goto IL_442;
				}
			}
			try
			{
				if (mailboxDiscoverySearch.InPlaceHoldEnabled)
				{
					this.recipientSession.SessionSettings.IncludeSoftDeletedObjects = true;
				}
				IEnumerable<RecipientIdParameter> enumerable;
				if (!flag2)
				{
					if (mailboxDiscoverySearch.Sources != null)
					{
						enumerable = from legacyDn in mailboxDiscoverySearch.Sources
						select new RecipientIdParameter(legacyDn);
					}
					else
					{
						enumerable = null;
					}
				}
				else
				{
					enumerable = this.SourceMailboxes;
				}
				IEnumerable<RecipientIdParameter> recipientIds = enumerable;
				MultiValuedProperty<string> multiValuedProperty = Utils.ConvertSourceMailboxesCollection(recipientIds, mailboxDiscoverySearch.InPlaceHoldEnabled, (RecipientIdParameter recipientId) => base.GetDataObject<ADRecipient>(recipientId, this.recipientSession, null, new LocalizedString?(Strings.ExceptionUserObjectNotFound(recipientId.ToString())), new LocalizedString?(Strings.ExceptionUserObjectAmbiguous)) as ADRecipient, this.recipientSession, new Task.TaskErrorLoggingDelegate(base.WriteError), new Action<LocalizedString>(this.WriteWarning), (LocalizedString locString) => this.Force || base.ShouldContinue(locString)) ?? new MultiValuedProperty<string>();
				mailboxDiscoverySearch.Sources.CopyChangesFrom(multiValuedProperty);
				if (mailboxDiscoverySearch.Sources.Count != multiValuedProperty.Count)
				{
					mailboxDiscoverySearch.Sources = multiValuedProperty;
				}
				if (base.ScopeSet != null)
				{
					foreach (string legacyExchangeDN in mailboxDiscoverySearch.Sources)
					{
						ADRecipient adrecipient = this.recipientSession.FindByLegacyExchangeDN(legacyExchangeDN);
						if (adrecipient == null)
						{
							base.WriteError(new ObjectNotFoundException(Strings.ExceptionSourceMailboxNotFound(mailboxDiscoverySearch.Target, mailboxDiscoverySearch.Name)), ErrorCategory.InvalidOperation, null);
						}
						Utils.VerifyIsInHoldScopes(mailboxDiscoverySearch.InPlaceHoldEnabled, base.ExchangeRunspaceConfig, adrecipient, "Set-MailboxSearch", new Task.TaskErrorLoggingDelegate(base.WriteError));
						Utils.VerifyIsInScopes(adrecipient, base.ScopeSet, new Task.TaskErrorLoggingDelegate(base.WriteError));
					}
				}
			}
			finally
			{
				this.recipientSession.SessionSettings.IncludeSoftDeletedObjects = false;
			}
			IL_442:
			if (flag)
			{
				if (base.Fields.IsModified(MailboxDiscoverySearchSchema.AllPublicFolderSources.Name))
				{
					mailboxDiscoverySearch.AllPublicFolderSources = this.AllPublicFolderSources;
				}
				if (this.AllPublicFolderSources)
				{
					if (this.PublicFolderSources != null)
					{
						this.WriteWarning(Strings.AllSourceMailboxesParameterOverride("AllPublicFolderSources", "PublicFolderSources"));
					}
					this.PublicFolderSources = null;
				}
				if (base.Fields.IsModified(MailboxDiscoverySearchSchema.PublicFolderSources.Name))
				{
					string[] array = null;
					if (this.PublicFolderSources != null && this.PublicFolderSources.Length != 0)
					{
						array = new string[this.PublicFolderSources.Length];
						string action = "Get-PublicFolder";
						try
						{
							using (PublicFolderDataProvider publicFolderDataProvider = new PublicFolderDataProvider(this.ConfigurationSession, action, Guid.Empty))
							{
								for (int i = 0; i < this.PublicFolderSources.Length; i++)
								{
									PublicFolder publicFolder = null;
									array[i] = this.PublicFolderSources[i].ToString();
									try
									{
										publicFolder = (PublicFolder)publicFolderDataProvider.Read<PublicFolder>(this.PublicFolderSources[i].PublicFolderId);
									}
									catch (FormatException exception)
									{
										base.WriteError(exception, ErrorCategory.WriteError, null);
									}
									if (publicFolder == null)
									{
										base.WriteError(new MailboxSearchTaskException(Strings.PublicFolderSourcesFolderDoesnotExist(array[i])), ErrorCategory.InvalidArgument, null);
									}
								}
							}
						}
						catch (AccessDeniedException exception2)
						{
							base.WriteError(exception2, ErrorCategory.PermissionDenied, mailboxDiscoverySearch.Name);
						}
					}
					mailboxDiscoverySearch.PublicFolderSources = Utils.ConvertCollectionToMultiValedProperty<string, string>(array, (string value, object param) => value, null, new MultiValuedProperty<string>(), new Task.TaskErrorLoggingDelegate(base.WriteError), MailboxDiscoverySearchSchema.PublicFolderSources.Name);
				}
			}
			if (base.Fields.IsModified(SearchObjectSchema.Senders.Name))
			{
				MultiValuedProperty<string> senders = Utils.ConvertCollectionToMultiValedProperty<string, string>(this.Senders, (string value, object param) => value, null, new MultiValuedProperty<string>(), new Task.TaskErrorLoggingDelegate(base.WriteError), SearchObjectSchema.Senders.Name);
				mailboxDiscoverySearch.Senders = senders;
			}
			if (base.Fields.IsModified(SearchObjectSchema.Recipients.Name))
			{
				MultiValuedProperty<string> recipients = Utils.ConvertCollectionToMultiValedProperty<string, string>(this.Recipients, (string value, object param) => value, null, new MultiValuedProperty<string>(), new Task.TaskErrorLoggingDelegate(base.WriteError), SearchObjectSchema.Recipients.Name);
				mailboxDiscoverySearch.Recipients = recipients;
			}
			if (base.Fields.IsModified(SearchObjectSchema.StartDate.Name))
			{
				mailboxDiscoverySearch.StartDate = this.StartDate;
			}
			if (base.Fields.IsModified(SearchObjectSchema.EndDate.Name))
			{
				mailboxDiscoverySearch.EndDate = this.EndDate;
			}
			if (base.Fields.IsModified(SearchObjectSchema.MessageTypes.Name))
			{
				MultiValuedProperty<KindKeyword> messageTypes = Utils.ConvertCollectionToMultiValedProperty<KindKeyword, KindKeyword>(this.MessageTypes, (KindKeyword kind, object param) => kind, null, new MultiValuedProperty<KindKeyword>(), new Task.TaskErrorLoggingDelegate(base.WriteError), SearchObjectSchema.MessageTypes.Name);
				mailboxDiscoverySearch.MessageTypes = messageTypes;
			}
			if (base.Fields.IsModified(SearchObjectSchema.StatusMailRecipients.Name))
			{
				MultiValuedProperty<ADObjectId> multiValuedProperty2 = Utils.ConvertCollectionToMultiValedProperty<RecipientIdParameter, ADObjectId>(this.StatusMailRecipients, new Utils.IdentityToRawIdDelegate<RecipientIdParameter, ADObjectId>(this.ADObjectIdFromRecipientIdParameter), null, new MultiValuedProperty<ADObjectId>(), new Task.TaskErrorLoggingDelegate(base.WriteError), SearchObjectSchema.StatusMailRecipients.Name);
				mailboxDiscoverySearch.StatusMailRecipients.CopyChangesFrom(multiValuedProperty2);
				if (mailboxDiscoverySearch.StatusMailRecipients.Count != multiValuedProperty2.Count)
				{
					mailboxDiscoverySearch.StatusMailRecipients = multiValuedProperty2;
				}
			}
			if (base.Fields.IsModified(SearchObjectSchema.ManagedBy.Name))
			{
				MultiValuedProperty<ADObjectId> multiValuedProperty3 = Utils.ConvertCollectionToMultiValedProperty<RecipientIdParameter, ADObjectId>(this.ManagedBy, new Utils.IdentityToRawIdDelegate<RecipientIdParameter, ADObjectId>(this.ADObjectIdFromRecipientIdParameter), null, new MultiValuedProperty<ADObjectId>(), new Task.TaskErrorLoggingDelegate(base.WriteError), SearchObjectSchema.ManagedBy.Name);
				mailboxDiscoverySearch.ManagedBy.CopyChangesFrom(multiValuedProperty3);
				if (mailboxDiscoverySearch.ManagedBy.Count != multiValuedProperty3.Count)
				{
					mailboxDiscoverySearch.ManagedBy = multiValuedProperty3;
				}
			}
			if (base.Fields.IsModified("SearchQuery"))
			{
				mailboxDiscoverySearch.Query = this.SearchQuery;
			}
			if (base.Fields.IsModified(SearchObjectSchema.EstimateOnly.Name))
			{
				mailboxDiscoverySearch.StatisticsOnly = this.EstimateOnly;
				if (this.EstimateOnly)
				{
					mailboxDiscoverySearch.LogLevel = LoggingLevel.Suppress;
				}
				else
				{
					mailboxDiscoverySearch.IncludeKeywordStatistics = false;
				}
			}
			if (base.Fields.IsModified(SearchObjectSchema.IncludeKeywordStatistics.Name))
			{
				mailboxDiscoverySearch.IncludeKeywordStatistics = this.IncludeKeywordStatistics.ToBool();
			}
			if (base.Fields.IsModified(MailboxDiscoverySearchSchema.StatisticsStartIndex.Name))
			{
				mailboxDiscoverySearch.StatisticsStartIndex = this.StatisticsStartIndex;
			}
			if (flag && (mailboxDiscoverySearch.Sources == null || mailboxDiscoverySearch.Sources.Count == 0) && (mailboxDiscoverySearch.PublicFolderSources == null || mailboxDiscoverySearch.PublicFolderSources.Count == 0) && !mailboxDiscoverySearch.AllSourceMailboxes && !mailboxDiscoverySearch.AllPublicFolderSources)
			{
				base.WriteError(new MailboxSearchTaskException(Strings.NoSourceMailboxesAndNoPublicFolderSourcesSet), ErrorCategory.InvalidArgument, null);
			}
			bool flag3 = mailboxDiscoverySearch.InPlaceHoldEnabled && (mailboxDiscoverySearch.Sources == null || mailboxDiscoverySearch.Sources.Count == 0);
			bool flag4 = false;
			if (flag)
			{
				flag3 = (mailboxDiscoverySearch.InPlaceHoldEnabled && mailboxDiscoverySearch.AllSourceMailboxes);
				flag4 = (mailboxDiscoverySearch.InPlaceHoldEnabled && (mailboxDiscoverySearch.AllPublicFolderSources || (mailboxDiscoverySearch.PublicFolderSources != null && mailboxDiscoverySearch.PublicFolderSources.Count != 0)));
			}
			if (flag3)
			{
				base.WriteError(new MailboxSearchTaskException(Strings.InPlaceHoldNotAllowedForAllSourceMailboxes), ErrorCategory.InvalidArgument, null);
			}
			if (flag4)
			{
				base.WriteError(new MailboxSearchTaskException(Strings.InPlaceHoldNotAllowedForPublicFolders), ErrorCategory.InvalidArgument, null);
			}
			return mailboxDiscoverySearch;
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x00112DF8 File Offset: 0x00110FF8
		protected override void InternalProcessRecord()
		{
			bool flag = false;
			bool flag2 = this.IsObjectStateChanged();
			if (flag2)
			{
				if (base.ExchangeRunspaceConfig == null)
				{
					base.WriteError(new MailboxSearchTaskException(Strings.UnableToDetermineExecutingUser), ErrorCategory.InvalidOperation, null);
					return;
				}
				if (this.DataObject.IsChanged(MailboxDiscoverySearchSchema.InPlaceHoldEnabled) || (this.DataObject.InPlaceHoldEnabled && this.DataObject.IsChanged(MailboxDiscoverySearchSchema.Sources)))
				{
					flag = true;
				}
			}
			if (flag)
			{
				bool flag3 = false;
				if ((this.DataObject.IsChanged(MailboxDiscoverySearchSchema.InPlaceHoldEnabled) && !this.DataObject.InPlaceHoldEnabled) || this.DataObject.InPlaceHoldEnabled)
				{
					flag3 = this.DataObject.ShouldWarnForInactiveOnHold((DiscoverySearchDataProvider)base.DataSession, this.DataObject.InPlaceHoldEnabled ? this.recipientSession : this.GetRecipientSessionWithoutScopeSet(), this.DataObject.InPlaceHoldEnabled);
				}
				if (flag3 && !base.ShouldContinue(Strings.ContinueToRemoveHoldForInactive))
				{
					return;
				}
			}
			base.InternalProcessRecord();
			if (flag2)
			{
				SearchEventLogger.Instance.LogDiscoveryAndHoldSavedEvent(this.DataObject);
			}
			if (flag)
			{
				LocalizedString localizedString = this.DataObject.SynchronizeHoldSettings((DiscoverySearchDataProvider)base.DataSession, this.DataObject.InPlaceHoldEnabled ? this.recipientSession : this.GetRecipientSessionWithoutScopeSet(), this.DataObject.InPlaceHoldEnabled, delegate(int percentage)
				{
					base.WriteProgress(Strings.SetMailboxSearchActivity, Strings.ApplyingHoldSettings(this.DataObject.Name), percentage);
				});
				if (localizedString != LocalizedString.Empty)
				{
					base.WriteError(new MailboxSearchTaskException(localizedString), ErrorCategory.InvalidOperation, this);
				}
				this.WriteWarning(Strings.WarningDiscoveryHoldDelay(COWSettings.COWCacheLifeTime.TotalMinutes));
			}
		}

		// Token: 0x060042E2 RID: 17122 RVA: 0x00112F90 File Offset: 0x00111190
		private IRecipientSession GetRecipientSessionWithoutScopeSet()
		{
			ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, false);
			return DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, false, ConsistencyMode.PartiallyConsistent, sessionSettings, 989, "GetRecipientSessionWithoutScopeSet", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Search\\SetMailboxSearch.cs");
		}

		// Token: 0x040029D8 RID: 10712
		private const string ParameterSearchQuery = "SearchQuery";

		// Token: 0x040029D9 RID: 10713
		private IRecipientSession recipientSession;

		// Token: 0x040029DA RID: 10714
		private ADUser targetUser;
	}
}
