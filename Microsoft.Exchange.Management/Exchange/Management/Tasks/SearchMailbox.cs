using System;
using System.Collections.Generic;
using System.IO;
using System.Management.Automation;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.InfoWorker.Common;
using Microsoft.Exchange.InfoWorker.Common.Search;
using Microsoft.Exchange.Management.Tasks.MailboxSearch;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200074D RID: 1869
	[Cmdlet("Search", "Mailbox", DefaultParameterSetName = "Identity", SupportsShouldProcess = true)]
	public sealed class SearchMailbox : RecipientObjectActionTask<MailboxOrMailUserIdParameter, ADUser>
	{
		// Token: 0x17001416 RID: 5142
		// (get) Token: 0x06004229 RID: 16937 RVA: 0x0010E91B File Offset: 0x0010CB1B
		// (set) Token: 0x0600422A RID: 16938 RVA: 0x0010E932 File Offset: 0x0010CB32
		[Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Position = 0)]
		public override MailboxOrMailUserIdParameter Identity
		{
			get
			{
				return (MailboxOrMailUserIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x17001417 RID: 5143
		// (get) Token: 0x0600422B RID: 16939 RVA: 0x0010E945 File Offset: 0x0010CB45
		// (set) Token: 0x0600422C RID: 16940 RVA: 0x0010E95C File Offset: 0x0010CB5C
		[Parameter(Mandatory = true, ParameterSetName = "Mailbox")]
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

		// Token: 0x17001418 RID: 5144
		// (get) Token: 0x0600422D RID: 16941 RVA: 0x0010E96F File Offset: 0x0010CB6F
		// (set) Token: 0x0600422E RID: 16942 RVA: 0x0010E986 File Offset: 0x0010CB86
		[Parameter(Mandatory = true, ParameterSetName = "Mailbox")]
		public string TargetFolder
		{
			get
			{
				return (string)base.Fields["TargetFolder"];
			}
			set
			{
				base.Fields["TargetFolder"] = value.Trim();
			}
		}

		// Token: 0x17001419 RID: 5145
		// (get) Token: 0x0600422F RID: 16943 RVA: 0x0010E99E File Offset: 0x0010CB9E
		// (set) Token: 0x06004230 RID: 16944 RVA: 0x0010E9C4 File Offset: 0x0010CBC4
		[Parameter(Mandatory = false, ParameterSetName = "Mailbox")]
		[Parameter(Mandatory = false, ParameterSetName = "Identity")]
		public SwitchParameter DeleteContent
		{
			get
			{
				return (SwitchParameter)(base.Fields["DeleteContent"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DeleteContent"] = value;
			}
		}

		// Token: 0x1700141A RID: 5146
		// (get) Token: 0x06004231 RID: 16945 RVA: 0x0010E9DC File Offset: 0x0010CBDC
		// (set) Token: 0x06004232 RID: 16946 RVA: 0x0010E9F3 File Offset: 0x0010CBF3
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

		// Token: 0x1700141B RID: 5147
		// (get) Token: 0x06004233 RID: 16947 RVA: 0x0010EA06 File Offset: 0x0010CC06
		// (set) Token: 0x06004234 RID: 16948 RVA: 0x0010EA2C File Offset: 0x0010CC2C
		[Parameter(Mandatory = false)]
		public SwitchParameter SearchDumpster
		{
			get
			{
				return (SwitchParameter)(base.Fields["SearchDumpster"] ?? new SwitchParameter(true));
			}
			set
			{
				base.Fields["SearchDumpster"] = value;
			}
		}

		// Token: 0x1700141C RID: 5148
		// (get) Token: 0x06004235 RID: 16949 RVA: 0x0010EA44 File Offset: 0x0010CC44
		// (set) Token: 0x06004236 RID: 16950 RVA: 0x0010EA6A File Offset: 0x0010CC6A
		[Parameter(Mandatory = false)]
		public SwitchParameter SearchDumpsterOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["SearchDumpsterOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["SearchDumpsterOnly"] = value;
			}
		}

		// Token: 0x1700141D RID: 5149
		// (get) Token: 0x06004237 RID: 16951 RVA: 0x0010EA82 File Offset: 0x0010CC82
		// (set) Token: 0x06004238 RID: 16952 RVA: 0x0010EAA3 File Offset: 0x0010CCA3
		[Parameter(Mandatory = false, ParameterSetName = "Mailbox")]
		public LoggingLevel LogLevel
		{
			get
			{
				return (LoggingLevel)(base.Fields["LogLevel"] ?? LoggingLevel.Basic);
			}
			set
			{
				base.Fields["LogLevel"] = value;
			}
		}

		// Token: 0x1700141E RID: 5150
		// (get) Token: 0x06004239 RID: 16953 RVA: 0x0010EABB File Offset: 0x0010CCBB
		// (set) Token: 0x0600423A RID: 16954 RVA: 0x0010EAE1 File Offset: 0x0010CCE1
		[Parameter(Mandatory = false, ParameterSetName = "Mailbox")]
		public SwitchParameter LogOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["LogOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["LogOnly"] = value;
			}
		}

		// Token: 0x1700141F RID: 5151
		// (get) Token: 0x0600423B RID: 16955 RVA: 0x0010EAF9 File Offset: 0x0010CCF9
		// (set) Token: 0x0600423C RID: 16956 RVA: 0x0010EB1F File Offset: 0x0010CD1F
		[Parameter(Mandatory = true, ParameterSetName = "EstimateResult")]
		public SwitchParameter EstimateResultOnly
		{
			get
			{
				return (SwitchParameter)(base.Fields["EstimateResultOnly"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["EstimateResultOnly"] = value;
			}
		}

		// Token: 0x17001420 RID: 5152
		// (get) Token: 0x0600423D RID: 16957 RVA: 0x0010EB37 File Offset: 0x0010CD37
		// (set) Token: 0x0600423E RID: 16958 RVA: 0x0010EB5D File Offset: 0x0010CD5D
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

		// Token: 0x17001421 RID: 5153
		// (get) Token: 0x0600423F RID: 16959 RVA: 0x0010EB75 File Offset: 0x0010CD75
		// (set) Token: 0x06004240 RID: 16960 RVA: 0x0010EB9B File Offset: 0x0010CD9B
		[Parameter(Mandatory = false)]
		public SwitchParameter DoNotIncludeArchive
		{
			get
			{
				return (SwitchParameter)(base.Fields["DoNotIncludeArchive"] ?? new SwitchParameter(false));
			}
			set
			{
				base.Fields["DoNotIncludeArchive"] = value;
			}
		}

		// Token: 0x17001422 RID: 5154
		// (get) Token: 0x06004241 RID: 16961 RVA: 0x0010EBB3 File Offset: 0x0010CDB3
		// (set) Token: 0x06004242 RID: 16962 RVA: 0x0010EBBB File Offset: 0x0010CDBB
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x06004244 RID: 16964 RVA: 0x0010EBF0 File Offset: 0x0010CDF0
		private void OnProgressEvent(object sender, SearchProgressEvent e)
		{
			int percentCompleted = e.PercentCompleted;
			base.WriteProgress(e.Activity, e.StatusDescription, percentCompleted);
		}

		// Token: 0x06004245 RID: 16965 RVA: 0x0010EC18 File Offset: 0x0010CE18
		private void OnExceptionEvent(object sender, SearchExceptionEvent e)
		{
			if (e.SourceIndex == null)
			{
				base.ThrowTerminatingError(e.Exception, ErrorCategory.InvalidArgument, null);
				return;
			}
			this.errorMessages.Add(Strings.SearchWorkerError(this.sourceUser.DisplayName, e.Exception.Message).ToString());
			this.WriteError(e.Exception, ErrorCategory.ReadError, e.SourceIndex, false);
		}

		// Token: 0x06004246 RID: 16966 RVA: 0x0010EC92 File Offset: 0x0010CE92
		private void OnRequestLogBodyEvent(object sender, RequestLogBodyEvent e)
		{
			this.ComposeLogItemBody(e.ItemBody);
		}

		// Token: 0x06004247 RID: 16967 RVA: 0x0010ECA0 File Offset: 0x0010CEA0
		private StoreId GetTargetFolderId(MailboxSession targetMailbox)
		{
			StoreId result = null;
			using (Folder folder = Folder.Bind(targetMailbox, DefaultFolderType.Root))
			{
				if (string.IsNullOrEmpty(this.TargetFolder))
				{
					result = folder.Id;
				}
				else
				{
					using (Folder folder2 = Folder.Create(targetMailbox, folder.Id, StoreObjectType.Folder, this.TargetFolder, CreateMode.OpenIfExists))
					{
						folder2.Save();
						folder2.Load();
						result = folder2.Id;
					}
				}
			}
			return result;
		}

		// Token: 0x06004248 RID: 16968 RVA: 0x0010ED2C File Offset: 0x0010CF2C
		private static void ReplaceLogFieldTags(StringBuilder sb, Globals.LogFields logField, object value)
		{
			sb = sb.Replace(logField.ToLabelTag(), LocalizedDescriptionAttribute.FromEnum(typeof(Globals.LogFields), logField) + ":");
			sb = sb.Replace(logField.ToValueTag(), string.Format("{0}", value));
		}

		// Token: 0x06004249 RID: 16969 RVA: 0x0010EDA8 File Offset: 0x0010CFA8
		private void ComposeLogItemBody(Body itemBody)
		{
			if (itemBody == null)
			{
				throw new ArgumentNullException("itemBody");
			}
			using (TextWriter textWriter = itemBody.OpenTextWriter(BodyFormat.TextHtml))
			{
				StringBuilder stringBuilder = new StringBuilder();
				using (StreamReader streamReader = new StreamReader(Assembly.GetAssembly(typeof(SearchMailboxExecuter)).GetManifestResourceStream("SimpleLogMailTemplate.htm")))
				{
					stringBuilder.Append(streamReader.ReadToEnd());
				}
				SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.LastStartTime, this.searchMailboxExecuter.SearchStartTime);
				SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.SearchQuery, this.SearchQuery);
				SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.TargetMailbox, this.targetUser.Id.DomainUserName());
				SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.SearchDumpster, this.SearchDumpster);
				SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.LogLevel, this.LogLevel);
				SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.SourceRecipients, (from x in this.searchMailboxExecuter.SearchMailboxCriteria.SearchUserScope
				select x.Id.DomainUserName()).AggregateOfDefault((string s, string x) => s + ", " + x));
				ADObjectId adobjectId = null;
				base.TryGetExecutingUserId(out adobjectId);
				SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.LastRunBy, (adobjectId == null) ? string.Empty : adobjectId.DomainUserName());
				SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.NumberMailboxesToSearch, this.searchMailboxExecuter.SearchMailboxCriteria.SearchUserScope.Length);
				if (this.DeleteContent.IsPresent)
				{
					if (base.ParameterSetName == "Mailbox")
					{
						SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.SearchOperation, Strings.CopyAndDeleteOperation);
					}
					else
					{
						SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.SearchOperation, Strings.DeleteOperation);
					}
				}
				else if (this.LogOnly.IsPresent)
				{
					SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.SearchOperation, Strings.LogOnlyOperation);
				}
				else
				{
					SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.SearchOperation, Strings.CopyOperation);
				}
				string str = this.errorMessages.AggregateOfDefault((string s, string x) => s + ", " + x);
				SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.Errors, str.ValueOrDefault(Strings.LogMailNone));
				long num = 0L;
				ByteQuantifiedSize byteQuantifiedSize = ByteQuantifiedSize.Zero;
				if (this.searchMailboxExecuter.SearchState != SearchState.InProgress)
				{
					foreach (SearchMailboxResult searchMailboxResult in this.searchMailboxExecuter.GetSearchResult())
					{
						num += (long)searchMailboxResult.ResultItemsCount;
						byteQuantifiedSize += searchMailboxResult.ResultItemsSize;
					}
				}
				SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.ResultNumber, num);
				SearchMailbox.ReplaceLogFieldTags(stringBuilder, Globals.LogFields.ResultSize, byteQuantifiedSize);
				SearchState searchState = (this.searchMailboxExecuter.SearchState == SearchState.InProgress) ? SearchState.Failed : this.searchMailboxExecuter.SearchState;
				stringBuilder = stringBuilder.Replace(Globals.LogFields.LogMailHeader.ToLabelTag(), Strings.LogMailSimpleHeader(searchState.ToString()));
				stringBuilder = stringBuilder.Replace(Globals.LogFields.LogMailSeeAttachment.ToLabelTag(), Strings.LogMailSeeAttachment);
				stringBuilder = stringBuilder.Replace(Globals.LogFields.LogMailFooter.ToLabelTag(), Strings.LogMailFooter);
				textWriter.Write(stringBuilder.ToString());
			}
		}

		// Token: 0x0600424A RID: 16970 RVA: 0x0010F104 File Offset: 0x0010D304
		private List<SearchMailboxAction> CreateSearchActions()
		{
			List<SearchMailboxAction> list = new List<SearchMailboxAction>();
			if (this.LogOnly.IsPresent)
			{
				list.Add(new LogSearchMailboxAction(this.LogLevel));
			}
			if (this.DeleteContent.IsPresent)
			{
				list.Add(new LogSearchMailboxAction(this.LogLevel));
				if (this.targetUser != null)
				{
					list.Add(new CopySearchMailboxAction());
				}
				list.Add(new DeleteSearchMailboxAction());
			}
			if (list.Count <= 0)
			{
				return null;
			}
			return list;
		}

		// Token: 0x0600424B RID: 16971 RVA: 0x0010F184 File Offset: 0x0010D384
		private void InternalPrevalidate()
		{
			this.WriteWarning(Strings.SearchMaxResultCountWarning(10000));
			if (this.DeleteContent.IsPresent)
			{
				if (this.IncludeUnsearchableItems == true)
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.DeletionAndUnsearchableNotPermitted), ErrorCategory.InvalidArgument, null);
				}
				if (this.LogOnly.IsPresent)
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.DeletionAndLogOnlyNotPermitted), ErrorCategory.InvalidArgument, null);
				}
			}
			if (this.LogOnly.IsPresent && this.LogLevel == LoggingLevel.Suppress)
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.WrongLogLevel), ErrorCategory.InvalidArgument, null);
			}
			if (this.SearchQuery != null && this.SearchQuery.Trim() == "")
			{
				base.ThrowTerminatingError(new ArgumentException(Strings.EmptySearchQuery), ErrorCategory.InvalidArgument, null);
			}
		}

		// Token: 0x0600424C RID: 16972 RVA: 0x0010F264 File Offset: 0x0010D464
		private void WriteResult(SearchMailboxResult[] searchResults)
		{
			foreach (SearchMailboxResult sendToPipeline in searchResults)
			{
				base.WriteObject(sendToPipeline);
			}
		}

		// Token: 0x17001423 RID: 5155
		// (get) Token: 0x0600424D RID: 16973 RVA: 0x0010F28C File Offset: 0x0010D48C
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (!this.DeleteContent.IsPresent)
				{
					return Strings.ConfirmSearchMailboxTask(this.Identity.ToString());
				}
				return Strings.ConfirmSearchMailboxDeleteContent(this.Identity.ToString());
			}
		}

		// Token: 0x0600424E RID: 16974 RVA: 0x0010F2E8 File Offset: 0x0010D4E8
		protected override IConfigurable ResolveDataObject()
		{
			IConfigurable dataObject = null;
			ADSessionSettingsFactory.RunWithInactiveMailboxVisibilityEnablerForDatacenter(delegate
			{
				dataObject = this.<>n__FabricatedMethod9();
			});
			return dataObject;
		}

		// Token: 0x0600424F RID: 16975 RVA: 0x0010F320 File Offset: 0x0010D520
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalBeginProcessing();
				if (base.ParameterSetName == "Identity" && !this.DeleteContent.IsPresent)
				{
					base.ThrowTerminatingError(new ArgumentException(Strings.EmptyTargetMailbox), ErrorCategory.InvalidArgument, null);
				}
				this.InternalPrevalidate();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06004250 RID: 16976 RVA: 0x0010F390 File Offset: 0x0010D590
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				base.InternalValidate();
				if (!base.HasErrors)
				{
					if (this.DeleteContent.IsPresent && !this.Force && !base.ShouldContinue(Strings.ConfirmSearchMailboxDeleteContent(this.Identity.ToString())))
					{
						TaskLogger.LogExit();
					}
					else
					{
						ADUser dataObject = this.DataObject;
						if (dataObject == null || (dataObject.RecipientType != RecipientType.UserMailbox && !RemoteMailbox.IsRemoteMailbox(dataObject.RecipientTypeDetails)))
						{
							this.WriteWarning(Strings.ErrorInvalidRecipientType(dataObject.ToString(), dataObject.RecipientType.ToString()));
						}
						else if (this.sourceUserIds.ContainsKey(dataObject.Id))
						{
							this.WriteWarning(Strings.SearchDuplicateSource(dataObject.ToString()));
						}
						else
						{
							Utils.VerifyMailboxVersion(dataObject, new Task.TaskErrorLoggingDelegate(base.WriteError));
							if (base.ScopeSet != null)
							{
								Utils.VerifyIsInScopes(dataObject, base.ScopeSet, new Task.TaskErrorLoggingDelegate(base.WriteError));
							}
							this.targetSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, ConfigScopes.TenantSubTree, 682, "InternalValidate", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Search\\SearchMailbox.cs");
							if (base.ParameterSetName == "Mailbox" && this.targetUser == null)
							{
								try
								{
									this.targetSession.SessionSettings.IncludeInactiveMailbox = false;
									this.targetUser = (base.GetDataObject<ADUser>(this.TargetMailbox, this.targetSession, this.RootId, new LocalizedString?(Strings.ErrorUserNotFound(this.TargetMailbox.ToString())), new LocalizedString?(Strings.ExceptionUserObjectAmbiguous)) as ADUser);
								}
								finally
								{
									if (VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
									{
										this.targetSession.SessionSettings.IncludeInactiveMailbox = true;
									}
								}
								if (this.targetUser == null || this.targetUser.RecipientType != RecipientType.UserMailbox)
								{
									base.ThrowTerminatingError(new ArgumentException(Strings.ErrorInvalidRecipientType(this.targetUser.ToString(), this.targetUser.RecipientType.ToString())), ErrorCategory.InvalidArgument, "TargetMailbox");
								}
								if (base.ScopeSet != null)
								{
									Utils.VerifyIsInScopes(this.targetUser, base.ScopeSet, new Task.TaskErrorLoggingDelegate(base.WriteError));
								}
							}
							if (base.ParameterSetName == "Mailbox" && dataObject.Id.Equals(this.targetUser.Id))
							{
								this.WriteWarning(Strings.SearchSourceTargetTheSame(dataObject.ToString()));
							}
							else
							{
								this.sourceUser = this.targetSession.ReadMiniRecipient(dataObject.Id, null);
							}
						}
					}
				}
			}
			catch (ManagementObjectNotFoundException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, base.CurrentObjectIndex);
			}
			catch (ManagementObjectAmbiguousException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, base.CurrentObjectIndex);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06004251 RID: 16977 RVA: 0x0010F6D0 File Offset: 0x0010D8D0
		protected override void InternalStateReset()
		{
			using (new ADSessionSettingsFactory.InactiveMailboxVisibilityEnabler())
			{
				base.InternalStateReset();
				this.sourceUser = null;
			}
		}

		// Token: 0x06004252 RID: 16978 RVA: 0x0010F70C File Offset: 0x0010D90C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.InternalProcessRecord();
			if (this.sourceUser != null)
			{
				this.sourceUserIds.Add(this.sourceUser.Id, null);
				this.sourceUserList.Add(this.sourceUser);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x06004253 RID: 16979 RVA: 0x0010F75C File Offset: 0x0010D95C
		protected override void InternalEndProcessing()
		{
			TaskLogger.LogEnter();
			try
			{
				if (!base.HasErrors && this.sourceUserList.Count > 0)
				{
					List<SearchUser> list = new List<SearchUser>();
					foreach (MiniRecipient miniRecipient in this.sourceUserList)
					{
						list.Add(new SearchUser(miniRecipient.Id, miniRecipient.DisplayName, miniRecipient.ServerLegacyDN));
					}
					SearchMailboxCriteria searchMailboxCriteria = new SearchMailboxCriteria(Thread.CurrentThread.CurrentCulture, this.SearchQuery, list.ToArray());
					searchMailboxCriteria.SearchDumpster = this.SearchDumpster.IsPresent;
					searchMailboxCriteria.SearchDumpsterOnly = this.SearchDumpsterOnly.IsPresent;
					searchMailboxCriteria.IncludeUnsearchableItems = this.IncludeUnsearchableItems;
					searchMailboxCriteria.IncludePersonalArchive = !this.DoNotIncludeArchive;
					searchMailboxCriteria.ExcludePurgesFromDumpster = this.DeleteContent.IsPresent;
					IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, null, base.SessionSettings, ConfigScopes.TenantSubTree, 829, "InternalEndProcessing", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\Search\\SearchMailbox.cs");
					this.searchMailboxExecuter = new SearchMailboxExecuter(this.targetSession, tenantOrTopologyConfigurationSession, searchMailboxCriteria, this.targetUser);
					GenericIdentity executingIdentityFromRunspace = SearchUtils.GetExecutingIdentityFromRunspace(base.ExchangeRunspaceConfig);
					this.searchMailboxExecuter.TargetFolder = this.TargetFolder;
					SearchMailboxExecuter searchMailboxExecuter = this.searchMailboxExecuter;
					searchMailboxExecuter.ProgressHandler = (EventHandler<SearchProgressEvent>)Delegate.Combine(searchMailboxExecuter.ProgressHandler, new EventHandler<SearchProgressEvent>(this.OnProgressEvent));
					SearchMailboxExecuter searchMailboxExecuter2 = this.searchMailboxExecuter;
					searchMailboxExecuter2.SearchExceptionHandler = (EventHandler<SearchExceptionEvent>)Delegate.Combine(searchMailboxExecuter2.SearchExceptionHandler, new EventHandler<SearchExceptionEvent>(this.OnExceptionEvent));
					SearchMailboxExecuter searchMailboxExecuter3 = this.searchMailboxExecuter;
					searchMailboxExecuter3.RequestLogBodyHandler = (EventHandler<RequestLogBodyEvent>)Delegate.Combine(searchMailboxExecuter3.RequestLogBodyHandler, new EventHandler<RequestLogBodyEvent>(this.OnRequestLogBodyEvent));
					this.searchMailboxExecuter.LogLevel = this.LogLevel;
					this.searchMailboxExecuter.SearchActions = this.CreateSearchActions();
					this.searchMailboxExecuter.OwnerIdentity = executingIdentityFromRunspace;
					this.searchMailboxExecuter.ExecuteSearch();
					if (this.searchMailboxExecuter.SearchState != SearchState.Stopped)
					{
						base.WriteProgress(Strings.ProgressCompleting, Strings.ProgressCompletingSearch, 100);
						this.WriteResult(this.searchMailboxExecuter.GetSearchResult());
					}
				}
			}
			catch (ParserException exception)
			{
				base.ThrowTerminatingError(exception, ErrorCategory.InvalidArgument, null);
			}
			catch (SearchQueryEmptyException exception2)
			{
				base.ThrowTerminatingError(exception2, ErrorCategory.InvalidArgument, null);
			}
			catch (StoragePermanentException exception3)
			{
				base.ThrowTerminatingError(exception3, ErrorCategory.InvalidArgument, null);
			}
			catch (StorageTransientException exception4)
			{
				base.ThrowTerminatingError(exception4, ErrorCategory.InvalidArgument, null);
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x06004254 RID: 16980 RVA: 0x0010FA78 File Offset: 0x0010DC78
		protected override void InternalStopProcessing()
		{
			TaskLogger.LogEnter();
			if (this.searchMailboxExecuter != null)
			{
				this.searchMailboxExecuter.Abort();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x0400299D RID: 10653
		private const string ParameterTargetMailbox = "TargetMailbox";

		// Token: 0x0400299E RID: 10654
		private const string ParameterTargetFolder = "TargetFolder";

		// Token: 0x0400299F RID: 10655
		private const string ParameterSearchQuery = "SearchQuery";

		// Token: 0x040029A0 RID: 10656
		private const string ParameterSearchDumpster = "SearchDumpster";

		// Token: 0x040029A1 RID: 10657
		private const string ParameterSearchDumpsterOnly = "SearchDumpsterOnly";

		// Token: 0x040029A2 RID: 10658
		private const string ParameterLogLevel = "LogLevel";

		// Token: 0x040029A3 RID: 10659
		private const string ParameterLogOnly = "LogOnly";

		// Token: 0x040029A4 RID: 10660
		private const string ParameterEstimateResultOnly = "EstimateResultOnly";

		// Token: 0x040029A5 RID: 10661
		private const string ParameterIncludeUnsearchableItems = "IncludeUnsearchableItems";

		// Token: 0x040029A6 RID: 10662
		private const string ParameterDoNotIncludeArchive = "DoNotIncludeArchive";

		// Token: 0x040029A7 RID: 10663
		private const string ParameterIncludeRemoteAccounts = "IncludeRemoteAccounts";

		// Token: 0x040029A8 RID: 10664
		private const string ParameterDeleteContent = "DeleteContent";

		// Token: 0x040029A9 RID: 10665
		private const string ParameterSetMailbox = "Mailbox";

		// Token: 0x040029AA RID: 10666
		private const string ParameterSetEstimateResult = "EstimateResult";

		// Token: 0x040029AB RID: 10667
		private Dictionary<ADObjectId, object> sourceUserIds = new Dictionary<ADObjectId, object>();

		// Token: 0x040029AC RID: 10668
		private List<MiniRecipient> sourceUserList = new List<MiniRecipient>();

		// Token: 0x040029AD RID: 10669
		private ADUser targetUser;

		// Token: 0x040029AE RID: 10670
		private MiniRecipient sourceUser;

		// Token: 0x040029AF RID: 10671
		private SearchMailboxExecuter searchMailboxExecuter;

		// Token: 0x040029B0 RID: 10672
		private List<string> errorMessages = new List<string>();

		// Token: 0x040029B1 RID: 10673
		private IRecipientSession targetSession;
	}
}
