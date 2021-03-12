using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
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
	// Token: 0x02000751 RID: 1873
	[Cmdlet("New", "MailboxSearch", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class NewMailboxSearch : NewTenantADTaskBase<MailboxDiscoverySearch>
	{
		// Token: 0x17001426 RID: 5158
		// (get) Token: 0x06004261 RID: 16993 RVA: 0x0010FBF0 File Offset: 0x0010DDF0
		// (set) Token: 0x06004262 RID: 16994 RVA: 0x0010FBFD File Offset: 0x0010DDFD
		[Parameter(Mandatory = true, Position = 0)]
		public string Name
		{
			get
			{
				return this.objectToSave.Name;
			}
			set
			{
				this.objectToSave.Name = value;
			}
		}

		// Token: 0x17001427 RID: 5159
		// (get) Token: 0x06004263 RID: 16995 RVA: 0x0010FC0B File Offset: 0x0010DE0B
		// (set) Token: 0x06004264 RID: 16996 RVA: 0x0010FC22 File Offset: 0x0010DE22
		[Parameter(Mandatory = false, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public RecipientIdParameter[] SourceMailboxes
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["SourceMailboxes"];
			}
			set
			{
				base.Fields["SourceMailboxes"] = value;
			}
		}

		// Token: 0x17001428 RID: 5160
		// (get) Token: 0x06004265 RID: 16997 RVA: 0x0010FC35 File Offset: 0x0010DE35
		// (set) Token: 0x06004266 RID: 16998 RVA: 0x0010FC4C File Offset: 0x0010DE4C
		[Parameter(Mandatory = false)]
		public MailboxIdParameter TargetMailbox
		{
			get
			{
				return (MailboxIdParameter)base.Fields["TargetMailbox"];
			}
			set
			{
				base.Fields["TargetMailbox"] = value;
			}
		}

		// Token: 0x17001429 RID: 5161
		// (get) Token: 0x06004267 RID: 16999 RVA: 0x0010FC5F File Offset: 0x0010DE5F
		// (set) Token: 0x06004268 RID: 17000 RVA: 0x0010FC6C File Offset: 0x0010DE6C
		[Parameter(Mandatory = false)]
		public string SearchQuery
		{
			get
			{
				return this.objectToSave.Query;
			}
			set
			{
				this.objectToSave.Query = value;
			}
		}

		// Token: 0x1700142A RID: 5162
		// (get) Token: 0x06004269 RID: 17001 RVA: 0x0010FC7A File Offset: 0x0010DE7A
		// (set) Token: 0x0600426A RID: 17002 RVA: 0x0010FC9A File Offset: 0x0010DE9A
		[Parameter(Mandatory = false)]
		public CultureInfo Language
		{
			get
			{
				return (CultureInfo)(base.Fields["Language"] ?? CultureInfo.CurrentCulture);
			}
			set
			{
				base.Fields["Language"] = value;
			}
		}

		// Token: 0x1700142B RID: 5163
		// (get) Token: 0x0600426B RID: 17003 RVA: 0x0010FCAD File Offset: 0x0010DEAD
		// (set) Token: 0x0600426C RID: 17004 RVA: 0x0010FCC4 File Offset: 0x0010DEC4
		[Parameter(Mandatory = false)]
		public PublicFolderIdParameter[] PublicFolderSources
		{
			get
			{
				return (PublicFolderIdParameter[])base.Fields["PublicFolderSources"];
			}
			set
			{
				base.Fields["PublicFolderSources"] = value;
			}
		}

		// Token: 0x1700142C RID: 5164
		// (get) Token: 0x0600426D RID: 17005 RVA: 0x0010FCD7 File Offset: 0x0010DED7
		// (set) Token: 0x0600426E RID: 17006 RVA: 0x0010FCF8 File Offset: 0x0010DEF8
		[Parameter(Mandatory = false)]
		public bool AllPublicFolderSources
		{
			get
			{
				return (bool)(base.Fields["AllPublicFolderSources"] ?? false);
			}
			set
			{
				base.Fields["AllPublicFolderSources"] = value;
			}
		}

		// Token: 0x1700142D RID: 5165
		// (get) Token: 0x0600426F RID: 17007 RVA: 0x0010FD10 File Offset: 0x0010DF10
		// (set) Token: 0x06004270 RID: 17008 RVA: 0x0010FD31 File Offset: 0x0010DF31
		[Parameter(Mandatory = false)]
		public bool AllSourceMailboxes
		{
			get
			{
				return (bool)(base.Fields["AllSourceMailboxes"] ?? false);
			}
			set
			{
				base.Fields["AllSourceMailboxes"] = value;
			}
		}

		// Token: 0x1700142E RID: 5166
		// (get) Token: 0x06004271 RID: 17009 RVA: 0x0010FD49 File Offset: 0x0010DF49
		// (set) Token: 0x06004272 RID: 17010 RVA: 0x0010FD60 File Offset: 0x0010DF60
		[Parameter(Mandatory = false)]
		public string[] Senders
		{
			get
			{
				return (string[])base.Fields["Senders"];
			}
			set
			{
				base.Fields["Senders"] = value;
			}
		}

		// Token: 0x1700142F RID: 5167
		// (get) Token: 0x06004273 RID: 17011 RVA: 0x0010FD73 File Offset: 0x0010DF73
		// (set) Token: 0x06004274 RID: 17012 RVA: 0x0010FD8A File Offset: 0x0010DF8A
		[Parameter(Mandatory = false)]
		public string[] Recipients
		{
			get
			{
				return (string[])base.Fields["Recipients"];
			}
			set
			{
				base.Fields["Recipients"] = value;
			}
		}

		// Token: 0x17001430 RID: 5168
		// (get) Token: 0x06004275 RID: 17013 RVA: 0x0010FD9D File Offset: 0x0010DF9D
		// (set) Token: 0x06004276 RID: 17014 RVA: 0x0010FDAA File Offset: 0x0010DFAA
		[Parameter(Mandatory = false)]
		public ExDateTime? StartDate
		{
			get
			{
				return this.objectToSave.StartDate;
			}
			set
			{
				this.objectToSave.StartDate = value;
			}
		}

		// Token: 0x17001431 RID: 5169
		// (get) Token: 0x06004277 RID: 17015 RVA: 0x0010FDB8 File Offset: 0x0010DFB8
		// (set) Token: 0x06004278 RID: 17016 RVA: 0x0010FDC5 File Offset: 0x0010DFC5
		[Parameter(Mandatory = false)]
		public ExDateTime? EndDate
		{
			get
			{
				return this.objectToSave.EndDate;
			}
			set
			{
				this.objectToSave.EndDate = value;
			}
		}

		// Token: 0x17001432 RID: 5170
		// (get) Token: 0x06004279 RID: 17017 RVA: 0x0010FDD3 File Offset: 0x0010DFD3
		// (set) Token: 0x0600427A RID: 17018 RVA: 0x0010FDEA File Offset: 0x0010DFEA
		[Parameter(Mandatory = false)]
		public KindKeyword[] MessageTypes
		{
			get
			{
				return (KindKeyword[])base.Fields["MessageTypes"];
			}
			set
			{
				base.Fields["MessageTypes"] = value;
			}
		}

		// Token: 0x17001433 RID: 5171
		// (get) Token: 0x0600427B RID: 17019 RVA: 0x0010FDFD File Offset: 0x0010DFFD
		// (set) Token: 0x0600427C RID: 17020 RVA: 0x0010FE2E File Offset: 0x0010E02E
		[Parameter(Mandatory = false)]
		public LoggingLevel LogLevel
		{
			get
			{
				object obj;
				if ((obj = base.Fields["LogLevel"]) == null)
				{
					obj = ((!this.EstimateOnly) ? LoggingLevel.Basic : LoggingLevel.Suppress);
				}
				return (LoggingLevel)obj;
			}
			set
			{
				base.Fields["LogLevel"] = value;
			}
		}

		// Token: 0x17001434 RID: 5172
		// (get) Token: 0x0600427D RID: 17021 RVA: 0x0010FE46 File Offset: 0x0010E046
		// (set) Token: 0x0600427E RID: 17022 RVA: 0x0010FE5D File Offset: 0x0010E05D
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] StatusMailRecipients
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["StatusMailRecipients"];
			}
			set
			{
				base.Fields["StatusMailRecipients"] = value;
			}
		}

		// Token: 0x17001435 RID: 5173
		// (get) Token: 0x0600427F RID: 17023 RVA: 0x0010FE70 File Offset: 0x0010E070
		// (set) Token: 0x06004280 RID: 17024 RVA: 0x0010FE87 File Offset: 0x0010E087
		internal RecipientIdParameter[] ManagedBy
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ManagedBy"];
			}
			set
			{
				base.Fields["ManagedBy"] = value;
			}
		}

		// Token: 0x17001436 RID: 5174
		// (get) Token: 0x06004281 RID: 17025 RVA: 0x0010FE9A File Offset: 0x0010E09A
		// (set) Token: 0x06004282 RID: 17026 RVA: 0x0010FEC0 File Offset: 0x0010E0C0
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeUnsearchableItems
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeUnsearchableItems"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeUnsearchableItems"] = value;
			}
		}

		// Token: 0x17001437 RID: 5175
		// (get) Token: 0x06004283 RID: 17027 RVA: 0x0010FED8 File Offset: 0x0010E0D8
		// (set) Token: 0x06004284 RID: 17028 RVA: 0x0010FEFE File Offset: 0x0010E0FE
		[Parameter(Mandatory = false)]
		public SwitchParameter EstimateOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["EstimateOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["EstimateOnly"] = value;
			}
		}

		// Token: 0x17001438 RID: 5176
		// (get) Token: 0x06004285 RID: 17029 RVA: 0x0010FF16 File Offset: 0x0010E116
		// (set) Token: 0x06004286 RID: 17030 RVA: 0x0010FF44 File Offset: 0x0010E144
		[Parameter(Mandatory = false)]
		public bool ExcludeDuplicateMessages
		{
			get
			{
				return (bool)(base.Fields["ExcludeDuplicateMessages"] ?? (!this.EstimateOnly));
			}
			set
			{
				base.Fields["ExcludeDuplicateMessages"] = value;
			}
		}

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x06004287 RID: 17031 RVA: 0x0010FF5C File Offset: 0x0010E15C
		// (set) Token: 0x06004288 RID: 17032 RVA: 0x0010FF82 File Offset: 0x0010E182
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

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x06004289 RID: 17033 RVA: 0x0010FF9A File Offset: 0x0010E19A
		// (set) Token: 0x0600428A RID: 17034 RVA: 0x0010FFC0 File Offset: 0x0010E1C0
		[Parameter(Mandatory = false)]
		public SwitchParameter IncludeKeywordStatistics
		{
			get
			{
				return (SwitchParameter)(base.Fields["IncludeKeywordStatistics"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["IncludeKeywordStatistics"] = value;
			}
		}

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x0600428B RID: 17035 RVA: 0x0010FFD8 File Offset: 0x0010E1D8
		// (set) Token: 0x0600428C RID: 17036 RVA: 0x0010FFF9 File Offset: 0x0010E1F9
		[Parameter(Mandatory = false)]
		public bool InPlaceHoldEnabled
		{
			get
			{
				return (bool)(base.Fields["InPlaceHoldEnabled"] ?? false);
			}
			set
			{
				base.Fields["InPlaceHoldEnabled"] = value;
			}
		}

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x0600428D RID: 17037 RVA: 0x00110011 File Offset: 0x0010E211
		// (set) Token: 0x0600428E RID: 17038 RVA: 0x00110036 File Offset: 0x0010E236
		[Parameter(Mandatory = false)]
		public Unlimited<EnhancedTimeSpan> ItemHoldPeriod
		{
			get
			{
				return (Unlimited<EnhancedTimeSpan>)(base.Fields["ItemHoldPeriod"] ?? Unlimited<EnhancedTimeSpan>.UnlimitedValue);
			}
			set
			{
				base.Fields["ItemHoldPeriod"] = value;
			}
		}

		// Token: 0x1700143D RID: 5181
		// (get) Token: 0x0600428F RID: 17039 RVA: 0x0011004E File Offset: 0x0010E24E
		// (set) Token: 0x06004290 RID: 17040 RVA: 0x00110065 File Offset: 0x0010E265
		[Parameter(Mandatory = false)]
		public string Description
		{
			get
			{
				return (string)base.Fields["Description"];
			}
			set
			{
				base.Fields["Description"] = value;
			}
		}

		// Token: 0x1700143E RID: 5182
		// (get) Token: 0x06004291 RID: 17041 RVA: 0x00110078 File Offset: 0x0010E278
		// (set) Token: 0x06004292 RID: 17042 RVA: 0x0011008F File Offset: 0x0010E28F
		[Parameter(Mandatory = false)]
		public string InPlaceHoldIdentity
		{
			get
			{
				return (string)base.Fields["InPlaceHoldIdentity"];
			}
			set
			{
				base.Fields["InPlaceHoldIdentity"] = value;
			}
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x001100C0 File Offset: 0x0010E2C0
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

		// Token: 0x06004295 RID: 17045 RVA: 0x00110178 File Offset: 0x0010E378
		private void PreSaveValidate(MailboxDiscoverySearch savedObject)
		{
			if (this.ExcludeDuplicateMessages && this.EstimateOnly)
			{
				base.WriteError(new MailboxSearchTaskException(Strings.ExcludeDuplicateMessagesParameterConflict), ErrorCategory.InvalidArgument, null);
			}
			if (savedObject.StatisticsOnly && savedObject.LogLevel != LoggingLevel.Suppress)
			{
				base.WriteError(new MailboxSearchTaskException(Strings.EstimateOnlyLogLevelParameterConflict(savedObject.LogLevel.ToString())), ErrorCategory.InvalidArgument, "LogLevel");
			}
			if (!savedObject.StatisticsOnly && savedObject.IncludeKeywordStatistics)
			{
				base.WriteError(new MailboxSearchTaskException(Strings.IncludeKeywordStatisticsParameterConflict), ErrorCategory.InvalidArgument, null);
			}
			if (base.ScopeSet != null)
			{
				foreach (string legacyExchangeDN in savedObject.Sources)
				{
					ADRecipient adrecipient = this.recipientSession.FindByLegacyExchangeDN(legacyExchangeDN);
					if (adrecipient == null)
					{
						base.WriteError(new ObjectNotFoundException(Strings.ExceptionSourceMailboxNotFound(savedObject.Target, savedObject.Name)), ErrorCategory.InvalidOperation, null);
					}
					Utils.VerifyIsInHoldScopes(savedObject.InPlaceHoldEnabled, base.ExchangeRunspaceConfig, adrecipient, "New-MailboxSearch", new Task.TaskErrorLoggingDelegate(base.WriteError));
					Utils.VerifyIsInScopes(adrecipient, base.ScopeSet, new Task.TaskErrorLoggingDelegate(base.WriteError));
				}
			}
			if (this.TargetMailbox != null)
			{
				try
				{
					this.recipientSession.SessionSettings.IncludeInactiveMailbox = false;
					ADRecipient adrecipient2 = this.recipientSession.FindByLegacyExchangeDN(savedObject.Target);
					if (adrecipient2 == null)
					{
						base.WriteError(new ObjectNotFoundException(Strings.ExceptionTargetMailboxNotFound(savedObject.Target, savedObject.Name)), ErrorCategory.InvalidOperation, null);
					}
					Utils.VerifyMailboxVersion(adrecipient2, new Task.TaskErrorLoggingDelegate(base.WriteError));
					if (base.ScopeSet != null)
					{
						Utils.VerifyIsInScopes(adrecipient2, base.ScopeSet, new Task.TaskErrorLoggingDelegate(base.WriteError));
					}
				}
				finally
				{
					if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
					{
						this.recipientSession.SessionSettings.IncludeInactiveMailbox = true;
					}
				}
			}
			if (!string.IsNullOrEmpty(this.InPlaceHoldIdentity))
			{
				Guid guid;
				if (Guid.TryParse(this.InPlaceHoldIdentity, out guid))
				{
					MailboxDiscoverySearch mailboxDiscoverySearch = ((DiscoverySearchDataProvider)base.DataSession).FindByInPlaceHoldIdentity(guid.ToString("N"));
					if (mailboxDiscoverySearch != null)
					{
						base.WriteError(new MailboxSearchTaskException(Strings.MailboxSearchInPlaceHoldIdentityExists(this.InPlaceHoldIdentity)), ErrorCategory.InvalidArgument, mailboxDiscoverySearch);
					}
				}
				else
				{
					base.WriteError(new MailboxSearchTaskException(Strings.MailboxSearchInPlaceHoldFormatError(this.InPlaceHoldIdentity)), ErrorCategory.InvalidArgument, savedObject);
				}
			}
			if (Utils.SameNameExists(savedObject.Name, (DiscoverySearchDataProvider)base.DataSession, Utils.GetMailboxDataProvider(base.RootOrgContainerId, base.CurrentOrganizationId, base.ExecutingUserOrganizationId, new Task.TaskErrorLoggingDelegate(base.WriteError))))
			{
				base.WriteError(new MailboxSearchTaskException(Strings.MailboxSearchObjectExist(savedObject.Name)), ErrorCategory.InvalidArgument, savedObject);
			}
			Exception ex = Utils.ValidateSourceAndTargetMailboxes((DiscoverySearchDataProvider)base.DataSession, savedObject);
			if (ex != null)
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x1700143F RID: 5183
		// (get) Token: 0x06004296 RID: 17046 RVA: 0x00110478 File Offset: 0x0010E678
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmNewMailboxSearchTask(this.Name);
			}
		}

		// Token: 0x06004297 RID: 17047 RVA: 0x00110488 File Offset: 0x0010E688
		protected override IConfigDataProvider CreateSession()
		{
			IRecipientSession recipientSession = Utils.CreateRecipientSession(base.DomainController, base.SessionSettings);
			this.recipientSession = recipientSession;
			return new DiscoverySearchDataProvider(base.CurrentOrganizationId);
		}

		// Token: 0x06004298 RID: 17048 RVA: 0x001104B9 File Offset: 0x0010E6B9
		protected override IConfigurable PrepareDataObject()
		{
			if (this.SourceMailboxes != null)
			{
				this.sourceMailboxIdParameters.AddRange(this.SourceMailboxes);
			}
			return base.PrepareDataObject();
		}

		// Token: 0x06004299 RID: 17049 RVA: 0x001104DA File Offset: 0x0010E6DA
		protected override void InternalProcessRecord()
		{
		}

		// Token: 0x0600429A RID: 17050 RVA: 0x001104FC File Offset: 0x0010E6FC
		protected override void InternalEndProcessing()
		{
			TaskLogger.LogEnter();
			this.PrepareDataObjectToSave();
			if (this.objectToSave != null)
			{
				try
				{
					this.PreSaveValidate(this.objectToSave);
					if (base.HasErrors)
					{
						return;
					}
					if (this.objectToSave.Identity != null)
					{
						base.WriteVerbose(TaskVerboseStringHelper.GetSaveObjectVerboseString(this.objectToSave, base.DataSession, typeof(MailboxDiscoverySearch)));
					}
					((DiscoverySearchDataProvider)base.DataSession).CreateOrUpdate<MailboxDiscoverySearch>(this.objectToSave);
					SearchEventLogger.Instance.LogDiscoveryAndHoldSavedEvent(this.objectToSave);
					this.DataObject = this.objectToSave;
					if (this.InPlaceHoldEnabled && this.sourceMailboxIdParameters != null && this.sourceMailboxIdParameters.Count > 0)
					{
						LocalizedString localizedString = this.objectToSave.SynchronizeHoldSettings((DiscoverySearchDataProvider)base.DataSession, this.recipientSession, true, delegate(int percentage)
						{
							base.WriteProgress(Strings.NewMailboxSearchActivity, Strings.ApplyingHoldSettings(this.objectToSave.Name), percentage);
						});
						if (localizedString != LocalizedString.Empty)
						{
							base.WriteError(new MailboxSearchTaskException(localizedString), ErrorCategory.InvalidOperation, this);
						}
						this.WriteWarning(Strings.WarningDiscoveryHoldDelay(COWSettings.COWCacheLifeTime.TotalMinutes));
					}
					if (!base.HasErrors)
					{
						this.WriteResult();
					}
				}
				catch (ObjectNotFoundException exception)
				{
					base.WriteError(exception, ErrorCategory.ObjectNotFound, null);
				}
				catch (DataSourceTransientException exception2)
				{
					base.WriteError(exception2, ErrorCategory.WriteError, null);
				}
				catch (ArgumentException exception3)
				{
					base.WriteError(exception3, ErrorCategory.WriteError, null);
				}
				catch (StorageTransientException innerException)
				{
					base.WriteError(new TaskException(Strings.ErrorMailboxSearchStorageTransient, innerException), ErrorCategory.WriteError, null);
				}
				catch (StoragePermanentException innerException2)
				{
					base.WriteError(new TaskException(Strings.ErrorMailboxSearchStoragePermanent, innerException2), ErrorCategory.WriteError, null);
				}
				finally
				{
					if (this.objectToSave.Identity != null)
					{
						base.WriteVerbose(TaskVerboseStringHelper.GetSourceVerboseString(base.DataSession));
					}
				}
			}
			base.InternalEndProcessing();
			TaskLogger.LogExit();
		}

		// Token: 0x0600429B RID: 17051 RVA: 0x00110798 File Offset: 0x0010E998
		protected override void WriteResult(IConfigurable result)
		{
			ADSessionSettingsFactory.RunWithInactiveMailboxVisibilityEnablerForDatacenter(delegate
			{
				MailboxDiscoverySearch discoverySearch = result as MailboxDiscoverySearch;
				this.<>n__FabricatedMethod5(new MailboxSearchObject(discoverySearch, ((DiscoverySearchDataProvider)this.DataSession).OrganizationId));
			});
		}

		// Token: 0x0600429C RID: 17052 RVA: 0x00110820 File Offset: 0x0010EA20
		private void PrepareDataObjectToSave()
		{
			if (base.ExchangeRunspaceConfig == null)
			{
				base.ThrowTerminatingError(new MailboxSearchTaskException(Strings.UnableToDetermineExecutingUser), ErrorCategory.InvalidOperation, null);
			}
			if (!(base.DataSession is DiscoverySearchDataProvider))
			{
				base.ThrowTerminatingError(new MailboxSearchTaskException(Strings.UnableToDetermineCreatingUser), ErrorCategory.InvalidOperation, null);
			}
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
					flag = base.ExchangeRunspaceConfig.IsCmdletAllowedInScope("New-MailboxSearch", paramNames, executingUser, ScopeLocation.RecipientWrite);
				}
				else
				{
					flag = base.ExchangeRunspaceConfig.IsCmdletAllowedInScope("New-MailboxSearch", paramNames, scopeSet);
				}
			}
			ADUser aduser = null;
			if (base.Fields["TargetMailbox"] != null)
			{
				aduser = (ADUser)base.GetDataObject<ADUser>(this.TargetMailbox, this.recipientSession, null, new LocalizedString?(Strings.ExceptionUserObjectNotFound(this.TargetMailbox.ToString())), new LocalizedString?(Strings.ExceptionUserObjectAmbiguous));
			}
			if (aduser != null)
			{
				if (aduser.RecipientType != RecipientType.UserMailbox)
				{
					base.ThrowTerminatingError(new MailboxSearchTaskException(Strings.ErrorInvalidRecipientType(aduser.ToString(), aduser.RecipientType.ToString())), ErrorCategory.InvalidArgument, "TargetMailbox");
				}
				bool flag2 = Utils.VerifyMailboxVersionIsSP1OrGreater(aduser);
				if ((this.EstimateOnly || this.ExcludeDuplicateMessages) && !flag2)
				{
					base.ThrowTerminatingError(new MailboxSearchTaskException(Strings.ErrorMailboxVersionTooOld(aduser.Id.ToString())), ErrorCategory.InvalidArgument, "TargetMailbox");
				}
				this.objectToSave.Target = aduser.LegacyExchangeDN;
			}
			if (flag)
			{
				this.objectToSave.AllSourceMailboxes = this.AllSourceMailboxes;
				if (this.AllSourceMailboxes)
				{
					if (this.sourceMailboxIdParameters != null && this.sourceMailboxIdParameters.Count > 0)
					{
						this.WriteWarning(Strings.AllSourceMailboxesParameterOverride("AllSourceMailboxes", "SourceMailboxes"));
					}
					this.sourceMailboxIdParameters = null;
				}
			}
			this.objectToSave.Sources = Utils.ConvertSourceMailboxesCollection(this.sourceMailboxIdParameters, this.InPlaceHoldEnabled, (RecipientIdParameter recipientId) => base.GetDataObject<ADRecipient>(recipientId, this.recipientSession, null, new LocalizedString?(Strings.ExceptionUserObjectNotFound(recipientId.ToString())), new LocalizedString?(Strings.ExceptionUserObjectAmbiguous)) as ADRecipient, this.recipientSession, new Task.TaskErrorLoggingDelegate(base.WriteError), new Action<LocalizedString>(this.WriteWarning), (LocalizedString locString) => this.Force || base.ShouldContinue(locString));
			if (this.objectToSave.Target != null && this.objectToSave.Sources != null && this.objectToSave.Sources.Contains(this.objectToSave.Target))
			{
				this.WriteWarning(Strings.TargetMailboxInSourceIsSkipped(this.DataObject.Target));
				this.objectToSave.Sources.Remove(this.objectToSave.Target);
				if (this.objectToSave.Sources.Count == 0)
				{
					base.WriteError(new MailboxSearchTaskException(Strings.TheOnlySourceMailboxIsTheTargetMailbox), ErrorCategory.InvalidArgument, null);
				}
			}
			if (flag)
			{
				this.objectToSave.AllPublicFolderSources = this.AllPublicFolderSources;
				if (this.AllPublicFolderSources)
				{
					if (this.PublicFolderSources != null)
					{
						this.WriteWarning(Strings.AllSourceMailboxesParameterOverride("AllPublicFolderSources", "PublicFolderSources"));
					}
					this.PublicFolderSources = null;
				}
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
						base.WriteError(exception2, ErrorCategory.PermissionDenied, this.Name);
					}
				}
				this.objectToSave.PublicFolderSources = Utils.ConvertCollectionToMultiValedProperty<string, string>(array, (string value, object param) => value, null, new MultiValuedProperty<string>(), new Task.TaskErrorLoggingDelegate(base.WriteError), "PublicFolderSources");
				this.objectToSave.Version = SearchObjectVersion.SecondVersion;
			}
			this.objectToSave.Senders = Utils.ConvertCollectionToMultiValedProperty<string, string>(this.Senders, (string value, object param) => value, null, null, new Task.TaskErrorLoggingDelegate(base.WriteError), SearchObjectSchema.Senders.Name);
			this.objectToSave.Recipients = Utils.ConvertCollectionToMultiValedProperty<string, string>(this.Recipients, (string value, object param) => value, null, null, new Task.TaskErrorLoggingDelegate(base.WriteError), SearchObjectSchema.Recipients.Name);
			this.objectToSave.MessageTypes = Utils.ConvertCollectionToMultiValedProperty<KindKeyword, KindKeyword>(this.MessageTypes, (KindKeyword kind, object param) => kind, null, null, new Task.TaskErrorLoggingDelegate(base.WriteError), SearchObjectSchema.MessageTypes.Name);
			this.objectToSave.CreatedBy = Utils.GetExecutingUserDisplayName(((DiscoverySearchDataProvider)base.DataSession).DisplayName, base.ExchangeRunspaceConfig);
			this.objectToSave.LogLevel = this.LogLevel;
			this.objectToSave.Language = this.Language.Name;
			this.objectToSave.IncludeUnsearchableItems = this.IncludeUnsearchableItems;
			this.objectToSave.StatisticsOnly = this.EstimateOnly;
			this.objectToSave.ExcludeDuplicateMessages = this.ExcludeDuplicateMessages;
			this.objectToSave.IncludeKeywordStatistics = this.IncludeKeywordStatistics;
			this.objectToSave.InPlaceHoldEnabled = this.InPlaceHoldEnabled;
			this.objectToSave.ItemHoldPeriod = this.ItemHoldPeriod;
			this.objectToSave.ManagedByOrganization = base.CurrentOrganizationId.ToString();
			this.objectToSave.Description = this.Description;
			this.objectToSave.StatusMailRecipients = Utils.ConvertCollectionToMultiValedProperty<RecipientIdParameter, ADObjectId>(this.StatusMailRecipients, new Utils.IdentityToRawIdDelegate<RecipientIdParameter, ADObjectId>(this.ADObjectIdFromRecipientIdParameter), null, null, new Task.TaskErrorLoggingDelegate(base.WriteError), SearchObjectSchema.StatusMailRecipients.Name);
			this.objectToSave.ManagedBy = Utils.ConvertCollectionToMultiValedProperty<RecipientIdParameter, ADObjectId>(this.ManagedBy, new Utils.IdentityToRawIdDelegate<RecipientIdParameter, ADObjectId>(this.ADObjectIdFromRecipientIdParameter), null, null, new Task.TaskErrorLoggingDelegate(base.WriteError), SearchObjectSchema.ManagedBy.Name);
			this.objectToSave.InPlaceHoldIdentity = this.InPlaceHoldIdentity;
			if (flag && (this.objectToSave.Sources == null || this.objectToSave.Sources.Count == 0) && (this.objectToSave.PublicFolderSources == null || this.objectToSave.PublicFolderSources.Count == 0) && !this.objectToSave.AllSourceMailboxes && !this.objectToSave.AllPublicFolderSources)
			{
				base.WriteError(new MailboxSearchTaskException(Strings.NoSourceMailboxesAndNoPublicFolderSourcesSet), ErrorCategory.InvalidArgument, null);
			}
			bool flag3 = this.InPlaceHoldEnabled && (this.objectToSave.Sources == null || this.objectToSave.Sources.Count == 0);
			bool flag4 = false;
			if (flag)
			{
				flag3 = (this.InPlaceHoldEnabled && this.objectToSave.AllSourceMailboxes);
				flag4 = (this.InPlaceHoldEnabled && (this.objectToSave.AllPublicFolderSources || (this.objectToSave.PublicFolderSources != null && this.objectToSave.PublicFolderSources.Count != 0)));
			}
			if (flag3)
			{
				base.WriteError(new MailboxSearchTaskException(Strings.InPlaceHoldNotAllowedForAllSourceMailboxes), ErrorCategory.InvalidArgument, null);
			}
			if (flag4)
			{
				base.WriteError(new MailboxSearchTaskException(Strings.InPlaceHoldNotAllowedForPublicFolders), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x040029B7 RID: 10679
		private const string ParameterSourceMailboxes = "SourceMailboxes";

		// Token: 0x040029B8 RID: 10680
		private const string ParameterTargetMailbox = "TargetMailbox";

		// Token: 0x040029B9 RID: 10681
		private const string ParameterSearchQuery = "SearchQuery";

		// Token: 0x040029BA RID: 10682
		private const string ParameterPublicFolderSources = "PublicFolderSources";

		// Token: 0x040029BB RID: 10683
		private const string ParameterAllPublicFolderSources = "AllPublicFolderSources";

		// Token: 0x040029BC RID: 10684
		private const string ParameterAllSourceMailboxes = "AllSourceMailboxes";

		// Token: 0x040029BD RID: 10685
		private const string ParameterSenders = "Senders";

		// Token: 0x040029BE RID: 10686
		private const string ParameterRecipients = "Recipients";

		// Token: 0x040029BF RID: 10687
		private const string ParameterLanguage = "Language";

		// Token: 0x040029C0 RID: 10688
		private const string ParameterMessageTypes = "MessageTypes";

		// Token: 0x040029C1 RID: 10689
		private const string ParameterStatusMailRecipients = "StatusMailRecipients";

		// Token: 0x040029C2 RID: 10690
		private const string ParameterManagedBy = "ManagedBy";

		// Token: 0x040029C3 RID: 10691
		private const string ParameterLogLevel = "LogLevel";

		// Token: 0x040029C4 RID: 10692
		private const string ParameterIncludeUnsearchableItems = "IncludeUnsearchableItems";

		// Token: 0x040029C5 RID: 10693
		private const string ParameterIncludeRemoteAccounts = "IncludeRemoteAccounts";

		// Token: 0x040029C6 RID: 10694
		private const string ParameterForce = "Force";

		// Token: 0x040029C7 RID: 10695
		private const string ParameterEstimateOnly = "EstimateOnly";

		// Token: 0x040029C8 RID: 10696
		private const string ParameterExcludeDuplicateMessages = "ExcludeDuplicateMessages";

		// Token: 0x040029C9 RID: 10697
		private const string ParameterInPlaceHoldEnabled = "InPlaceHoldEnabled";

		// Token: 0x040029CA RID: 10698
		private const string ParameterIncludeKeywordStatistics = "IncludeKeywordStatistics";

		// Token: 0x040029CB RID: 10699
		private const string ParameterItemHoldPeriod = "ItemHoldPeriod";

		// Token: 0x040029CC RID: 10700
		private const string ParameterDescription = "Description";

		// Token: 0x040029CD RID: 10701
		private const string ParameterInPlaceHoldIdentity = "InPlaceHoldIdentity";

		// Token: 0x040029CE RID: 10702
		private MailboxDiscoverySearch objectToSave = new MailboxDiscoverySearch();

		// Token: 0x040029CF RID: 10703
		private List<RecipientIdParameter> sourceMailboxIdParameters = new List<RecipientIdParameter>();

		// Token: 0x040029D0 RID: 10704
		private IRecipientSession recipientSession;
	}
}
