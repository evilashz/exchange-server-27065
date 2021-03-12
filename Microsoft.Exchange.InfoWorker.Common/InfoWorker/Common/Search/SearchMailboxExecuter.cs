using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Conversations;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Win32;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000236 RID: 566
	internal class SearchMailboxExecuter
	{
		// Token: 0x06001012 RID: 4114 RVA: 0x00048ACC File Offset: 0x00046CCC
		internal SearchMailboxExecuter(IRecipientSession recipientSession, IConfigurationSession configurationSession, SearchMailboxCriteria searchMailboxCriteria, ADRecipient targetUser)
		{
			this.RecipientSession = recipientSession;
			this.ConfigurationSession = configurationSession;
			this.searchMailboxCriteria = searchMailboxCriteria;
			this.TargetUser = targetUser;
			this.searchCommunicator = new SearchCommunicator();
			this.threadLimit = 1;
			this.threadLimitPerServer = 1;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x00048B6C File Offset: 0x00046D6C
		internal SearchMailboxExecuter(IRecipientSession recipientSession, IConfigurationSession configurationSession, SearchMailboxCriteria searchMailboxCriteria, ADRecipient targetUser, HashSet<UniqueItemHash> processedMessages, HashSet<string> processedMessageIds, int maxThreadLimitPerSearch, int maxThreadLimitPerServer)
		{
			this.RecipientSession = recipientSession;
			this.ConfigurationSession = configurationSession;
			this.searchMailboxCriteria = searchMailboxCriteria;
			this.TargetUser = targetUser;
			this.searchCommunicator = new SearchCommunicator(processedMessages, processedMessageIds);
			this.threadLimit = maxThreadLimitPerSearch;
			this.threadLimitPerServer = maxThreadLimitPerServer;
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06001014 RID: 4116 RVA: 0x00048C10 File Offset: 0x00046E10
		// (set) Token: 0x06001015 RID: 4117 RVA: 0x00048C18 File Offset: 0x00046E18
		internal GenericIdentity OwnerIdentity
		{
			get
			{
				return this.ownerIdentity;
			}
			set
			{
				this.ownerIdentity = value;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x00048C21 File Offset: 0x00046E21
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x00048C29 File Offset: 0x00046E29
		internal ADRecipient TargetUser
		{
			get
			{
				return this.targetUser;
			}
			set
			{
				this.targetUser = value;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x00048C32 File Offset: 0x00046E32
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x00048C3A File Offset: 0x00046E3A
		internal string TargetFolder
		{
			get
			{
				return this.targetFolder;
			}
			set
			{
				this.targetFolder = value;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x00048C43 File Offset: 0x00046E43
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x00048C4B File Offset: 0x00046E4B
		internal ADRecipient[] ReviewRecipients
		{
			get
			{
				return this.reviewRecipients;
			}
			set
			{
				this.reviewRecipients = value;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x00048C54 File Offset: 0x00046E54
		internal bool EstimationPhase
		{
			get
			{
				return this.TargetUser == null;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00048C5F File Offset: 0x00046E5F
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x00048C67 File Offset: 0x00046E67
		internal IRecipientSession RecipientSession
		{
			get
			{
				return this.recipientSession;
			}
			set
			{
				this.recipientSession = value;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x00048C70 File Offset: 0x00046E70
		// (set) Token: 0x06001020 RID: 4128 RVA: 0x00048C78 File Offset: 0x00046E78
		internal IConfigurationSession ConfigurationSession
		{
			get
			{
				return this.configurationSession;
			}
			set
			{
				this.configurationSession = value;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x00048C81 File Offset: 0x00046E81
		// (set) Token: 0x06001022 RID: 4130 RVA: 0x00048C89 File Offset: 0x00046E89
		internal LoggingLevel LogLevel
		{
			get
			{
				return this.logLevel;
			}
			set
			{
				this.logLevel = value;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x00048C92 File Offset: 0x00046E92
		// (set) Token: 0x06001024 RID: 4132 RVA: 0x00048C9A File Offset: 0x00046E9A
		internal SearchState SearchState
		{
			get
			{
				return this.searchState;
			}
			set
			{
				this.searchState = value;
			}
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x00048CA3 File Offset: 0x00046EA3
		internal ExDateTime? SearchStartTime
		{
			get
			{
				return this.searchStartTime;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x00048CAB File Offset: 0x00046EAB
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x00048CB3 File Offset: 0x00046EB3
		internal EventHandler<SearchProgressEvent> ProgressHandler
		{
			get
			{
				return this.progressHandler;
			}
			set
			{
				this.progressHandler = value;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x00048CBC File Offset: 0x00046EBC
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x00048CC4 File Offset: 0x00046EC4
		internal EventHandler<SearchLoggingEvent> LoggingHandler
		{
			get
			{
				return this.loggingHandler;
			}
			set
			{
				this.loggingHandler = value;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x00048CCD File Offset: 0x00046ECD
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x00048CD5 File Offset: 0x00046ED5
		internal EventHandler<SearchExceptionEvent> SearchExceptionHandler
		{
			get
			{
				return this.searchExceptionHandler;
			}
			set
			{
				this.searchExceptionHandler = value;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00048CDE File Offset: 0x00046EDE
		// (set) Token: 0x0600102D RID: 4141 RVA: 0x00048CE6 File Offset: 0x00046EE6
		internal EventHandler<RequestLogBodyEvent> RequestLogBodyHandler
		{
			get
			{
				return this.requestBodyHandler;
			}
			set
			{
				this.requestBodyHandler = value;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x00048CEF File Offset: 0x00046EEF
		// (set) Token: 0x0600102F RID: 4143 RVA: 0x00048CF7 File Offset: 0x00046EF7
		internal bool WaitForWorkersWhenAborted
		{
			get
			{
				return this.waitForWorkersWhenAborted;
			}
			set
			{
				this.waitForWorkersWhenAborted = value;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001030 RID: 4144 RVA: 0x00048D00 File Offset: 0x00046F00
		internal SearchMailboxCriteria SearchMailboxCriteria
		{
			get
			{
				return this.searchMailboxCriteria;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001031 RID: 4145 RVA: 0x00048D08 File Offset: 0x00046F08
		// (set) Token: 0x06001032 RID: 4146 RVA: 0x00048D10 File Offset: 0x00046F10
		internal List<SearchMailboxAction> SearchActions
		{
			get
			{
				return this.searchActions;
			}
			set
			{
				this.searchActions = value;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001033 RID: 4147 RVA: 0x00048D19 File Offset: 0x00046F19
		// (set) Token: 0x06001034 RID: 4148 RVA: 0x00048D21 File Offset: 0x00046F21
		internal string Name { get; set; }

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x00048D2A File Offset: 0x00046F2A
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x00048D32 File Offset: 0x00046F32
		internal StreamLogItem StreamLogItem { get; set; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x00048D3B File Offset: 0x00046F3B
		// (set) Token: 0x06001038 RID: 4152 RVA: 0x00048D43 File Offset: 0x00046F43
		internal StoreId LogMessageId { get; set; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x00048D4C File Offset: 0x00046F4C
		internal int ThreadLimit
		{
			get
			{
				return this.threadLimit;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x00048D54 File Offset: 0x00046F54
		internal int ThreadLimitPerServer
		{
			get
			{
				return this.threadLimitPerServer;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x00048D5C File Offset: 0x00046F5C
		// (set) Token: 0x0600103C RID: 4156 RVA: 0x00048D64 File Offset: 0x00046F64
		internal bool ResumeSearch { get; set; }

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x00048D6D File Offset: 0x00046F6D
		// (set) Token: 0x0600103E RID: 4158 RVA: 0x00048D75 File Offset: 0x00046F75
		internal bool HasPreviousCompletedMailboxes { get; set; }

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x00048D7E File Offset: 0x00046F7E
		// (set) Token: 0x06001040 RID: 4160 RVA: 0x00048D8B File Offset: 0x00046F8B
		internal MultiValuedProperty<string> SuccessfulMailboxes
		{
			get
			{
				return this.searchCommunicator.SuccessfulMailboxes;
			}
			set
			{
				this.searchCommunicator.SuccessfulMailboxes = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001041 RID: 4161 RVA: 0x00048D99 File Offset: 0x00046F99
		// (set) Token: 0x06001042 RID: 4162 RVA: 0x00048DA6 File Offset: 0x00046FA6
		internal MultiValuedProperty<string> UnsuccessfulMailboxes
		{
			get
			{
				return this.searchCommunicator.UnsuccessfulMailboxes;
			}
			set
			{
				this.searchCommunicator.UnsuccessfulMailboxes = value;
			}
		}

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x00048DB4 File Offset: 0x00046FB4
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x00048DBC File Offset: 0x00046FBC
		internal SearchObject SearchObject { get; set; }

		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x00048DC5 File Offset: 0x00046FC5
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x00048DCD File Offset: 0x00046FCD
		internal ObjectId ExecutingUserIdentity { get; set; }

		// Token: 0x06001047 RID: 4167 RVA: 0x00048DD8 File Offset: 0x00046FD8
		internal IAsyncResult BeginExecuteSearch(AsyncCallback asyncCallback, object state, ExDateTime? searchStartTime)
		{
			SearchMailboxExecuter.Tracer.TraceFunction<SearchMailboxCriteria, ADRecipient, string>((long)this.GetHashCode(), "SearchMailboxExecuter::BeginExecuteSearch. SearchCriteria=[{0}], TargetUser={1}, TargetFolder={2}", this.searchMailboxCriteria, this.TargetUser, this.TargetFolder);
			this.PrepareSearch(searchStartTime);
			this.userCallback = asyncCallback;
			this.state = state;
			this.asyncResult = new SearchMailboxExecuter.SearchMailboxAsyncResult(this);
			Thread thread = new Thread(new ThreadStart(this.WatsonWrappedSearchEntry));
			thread.Start();
			return this.asyncResult;
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x00048E4C File Offset: 0x0004704C
		internal void EndExecuteSearch(IAsyncResult asyncResult)
		{
			SearchMailboxExecuter.Tracer.TraceFunction((long)this.GetHashCode(), "SearchMailboxExecuter::EndExecuteSearch.");
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			if (this.asyncResult == null)
			{
				throw new InvalidOperationException("No search initialized");
			}
			if (this.asyncResult != asyncResult)
			{
				throw new ArgumentException("Invalidate async result", "asyncResult");
			}
			this.asyncResult.AsyncWaitHandle.WaitOne();
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x00048EBA File Offset: 0x000470BA
		internal void ExecuteSearch()
		{
			SearchMailboxExecuter.Tracer.TraceFunction<SearchMailboxCriteria, ADRecipient, string>((long)this.GetHashCode(), "SearchMailboxExecuter::ExecuteSearch. SearchCriteria=[{0}], TargetUser={1}, TargetFolder={2}", this.searchMailboxCriteria, this.TargetUser, this.TargetFolder);
			this.PrepareSearch(new ExDateTime?(ExDateTime.UtcNow));
			this.SearchMainEntry();
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x00048EFC File Offset: 0x000470FC
		internal void Abort()
		{
			SearchMailboxExecuter.Tracer.TraceFunction((long)this.GetHashCode(), "Entering Abort()");
			this.SearchState = (this.SearchMailboxCriteria.EstimateOnly ? SearchState.EstimateStopped : SearchState.Stopped);
			if (this.searchCommunicator != null)
			{
				SearchMailboxExecuter.Tracer.TraceDebug((long)this.GetHashCode(), "The search is aborted");
				lock (this.searchCommunicator)
				{
					this.searchCommunicator.Abort();
				}
			}
			SearchMailboxExecuter.Tracer.TraceFunction((long)this.GetHashCode(), "Leaving Abort()");
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x00048FA4 File Offset: 0x000471A4
		internal void ResetSearchStartTime()
		{
			this.searchStartTime = null;
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x00048FB2 File Offset: 0x000471B2
		internal SearchMailboxResult GetSearchResult(int sourceIndex)
		{
			return this.searchMailboxWorkers[sourceIndex].SearchResult;
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x00048FC8 File Offset: 0x000471C8
		internal SearchMailboxResult[] GetSearchResult()
		{
			List<SearchMailboxResult> list = new List<SearchMailboxResult>();
			foreach (SearchMailboxWorker searchMailboxWorker in this.searchMailboxWorkers)
			{
				list.Add(searchMailboxWorker.SearchResult);
			}
			return list.ToArray();
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0004902C File Offset: 0x0004722C
		private void FireProgressEvent(LocalizedString activity, LocalizedString statusDescription, int percentCompleted, long resultItems, ByteQuantifiedSize resultSize)
		{
			if (this.ProgressHandler != null)
			{
				this.ProgressHandler(this, new SearchProgressEvent(activity, statusDescription, percentCompleted, resultItems, resultSize));
			}
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0004904E File Offset: 0x0004724E
		private void FireLoggingEvent(LocalizedString loggingMessage)
		{
			if (this.LoggingHandler != null)
			{
				this.LoggingHandler(this, new SearchLoggingEvent(loggingMessage));
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0004906A File Offset: 0x0004726A
		private void FireExceptionEvent(SearchMailboxWorker worker, Exception exception)
		{
			SearchMailboxExecuter.Tracer.TraceError<SearchMailboxWorker, Exception>((long)this.GetHashCode(), "ExceptionEvent: worker {0} is aborted due to exception {1}", worker, exception);
			if (this.SearchExceptionHandler != null)
			{
				this.SearchExceptionHandler(this, new SearchExceptionEvent(new int?(worker.WorkerId), exception));
			}
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x000490AC File Offset: 0x000472AC
		private void FireExceptionEvent(Exception exception)
		{
			SearchMailboxExecuter.Tracer.TraceError<Exception>((long)this.GetHashCode(), "ExceptionEvent: the main thread is aborted due to exception {0}", exception);
			if (this.SearchExceptionHandler != null)
			{
				this.SearchExceptionHandler(this, new SearchExceptionEvent(null, exception));
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x000490F3 File Offset: 0x000472F3
		private void FireRequestLogBodyEvent(Body itemBody)
		{
			if (this.RequestLogBodyHandler != null)
			{
				this.RequestLogBodyHandler(this, new RequestLogBodyEvent(itemBody));
			}
		}

		// Token: 0x06001053 RID: 4179 RVA: 0x00049110 File Offset: 0x00047310
		private void SetReviewerPermission(Folder folder)
		{
			PermissionSet permissionSet = folder.GetPermissionSet();
			permissionSet.Clear();
			if (this.ReviewRecipients != null && this.ReviewRecipients.Length > 0)
			{
				foreach (ADRecipient adRecipient in this.ReviewRecipients)
				{
					PermissionSecurityPrincipal securityPrincipal = new PermissionSecurityPrincipal(adRecipient);
					if (permissionSet.GetEntry(securityPrincipal) == null)
					{
						permissionSet.AddEntry(securityPrincipal, PermissionLevel.Reviewer);
					}
				}
			}
		}

		// Token: 0x06001054 RID: 4180 RVA: 0x0004917C File Offset: 0x0004737C
		private bool AddViewFolderPermission(Folder folder)
		{
			bool result = false;
			if (this.ReviewRecipients != null && this.ReviewRecipients.Length > 0)
			{
				PermissionSet permissionSet = folder.GetPermissionSet();
				foreach (ADRecipient adRecipient in this.ReviewRecipients)
				{
					PermissionSecurityPrincipal securityPrincipal = new PermissionSecurityPrincipal(adRecipient);
					if (permissionSet.GetEntry(securityPrincipal) == null)
					{
						Permission permission = permissionSet.AddEntry(securityPrincipal, PermissionLevel.None);
						permission.IsFolderVisible = true;
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06001055 RID: 4181 RVA: 0x000491F0 File Offset: 0x000473F0
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
						FolderSaveResult folderSaveResult = folder2.Save();
						if (folderSaveResult != null && folderSaveResult.OperationResult != OperationResult.Succeeded)
						{
							SearchMailboxExecuter.Tracer.TraceError<OperationResult>((long)this.GetHashCode(), "SearchExecuter save target folder failed {0}", folderSaveResult.OperationResult);
						}
						folder2.Load();
						this.SetReviewerPermission(folder2);
						folderSaveResult = folder2.Save();
						if (folderSaveResult != null && folderSaveResult.OperationResult != OperationResult.Succeeded)
						{
							SearchMailboxExecuter.Tracer.TraceError<OperationResult>((long)this.GetHashCode(), "SearchExecuter save permission on target folder failed {0}", folderSaveResult.OperationResult);
						}
						folder2.Load();
						result = folder2.Id;
					}
				}
				if (this.AddViewFolderPermission(folder))
				{
					FolderSaveResult folderSaveResult2 = folder.Save();
					if (folderSaveResult2 != null && folderSaveResult2.OperationResult != OperationResult.Succeeded)
					{
						SearchMailboxExecuter.Tracer.TraceError<OperationResult>((long)this.GetHashCode(), "SearchExecuter save permission on RootFolder failed {0}", folderSaveResult2.OperationResult);
					}
				}
			}
			return result;
		}

		// Token: 0x06001056 RID: 4182 RVA: 0x00049320 File Offset: 0x00047520
		private void PrepareSearch(ExDateTime? searchStartTime)
		{
			if (this.searchMailboxCriteria == null)
			{
				throw new ArgumentNullException("searchMailboxCriteria");
			}
			Dictionary<ADObjectId, object> dictionary = new Dictionary<ADObjectId, object>();
			if (this.TargetUser != null)
			{
				if (this.TargetUser.RecipientType != RecipientType.UserMailbox)
				{
					throw new ArgumentException("Target use must be a mailbox user");
				}
				if (dictionary.ContainsKey(this.targetUser.Id))
				{
					throw new ArgumentException("target mailbox user can't be searched");
				}
			}
			this.searchMailboxCriteria.ResolveQueryFilter(this.RecipientSession, this.ConfigurationSession);
			if (this.EstimationPhase)
			{
				this.searchMailboxCriteria.GenerateSubQueryFilters(this.RecipientSession, this.ConfigurationSession);
			}
			this.searchMailboxWorkers.Clear();
			this.searchCompleteEvent.Reset();
			this.searchCommunicator.Reset(this.searchMailboxCriteria.SearchUserScope.Length);
			if (!this.searchCommunicator.IsAborted)
			{
				this.SearchState = (this.searchMailboxCriteria.EstimateOnly ? SearchState.EstimateInProgress : SearchState.InProgress);
			}
			if (this.searchStartTime == null)
			{
				this.searchStartTime = ((searchStartTime == null) ? new ExDateTime?(ExDateTime.UtcNow) : searchStartTime);
			}
			this.StreamLogItem = null;
		}

		// Token: 0x06001057 RID: 4183 RVA: 0x00049440 File Offset: 0x00047640
		private void PostProcessSearch()
		{
			bool flag = true;
			bool flag2 = true;
			foreach (SearchMailboxWorker searchMailboxWorker in this.searchMailboxWorkers)
			{
				SearchMailboxResult searchResult = searchMailboxWorker.SearchResult;
				searchResult.LastException = searchMailboxWorker.LastException;
				searchResult.TargetMailbox = (this.EstimationPhase ? null : this.TargetUser.Id);
				flag2 = (flag2 && searchResult.Success);
				flag = (flag && !searchResult.Success);
				if (!this.EstimationPhase)
				{
					if (!string.IsNullOrEmpty(this.TargetFolder))
					{
						searchResult.TargetFolder = string.Format("\\{0}\\{1}", this.TargetFolder, searchResult.TargetFolder);
					}
					else
					{
						searchResult.TargetFolder = string.Format("\\{0}", searchResult.TargetFolder);
					}
				}
			}
			if (this.SearchState == SearchState.InProgress || this.SearchState == SearchState.EstimateInProgress)
			{
				if (flag2)
				{
					this.SearchState = (this.searchMailboxCriteria.EstimateOnly ? SearchState.EstimateSucceeded : SearchState.Succeeded);
					return;
				}
				if (flag && (this.SuccessfulMailboxes == null || this.SuccessfulMailboxes.Count == 0) && !this.HasPreviousCompletedMailboxes)
				{
					this.SearchState = (this.searchMailboxCriteria.EstimateOnly ? SearchState.EstimateFailed : SearchState.Failed);
					return;
				}
				this.SearchState = (this.searchMailboxCriteria.EstimateOnly ? SearchState.EstimatePartiallySucceeded : SearchState.PartiallySucceeded);
			}
		}

		// Token: 0x06001058 RID: 4184 RVA: 0x000495F4 File Offset: 0x000477F4
		private void WatsonWrappedSearchEntry()
		{
			bool searchDone = false;
			SearchUtils.ExWatsonWrappedCall(delegate()
			{
				this.SearchMainEntry();
				searchDone = true;
			}, delegate()
			{
				if (!searchDone)
				{
					this.SearchState = (this.SearchMailboxCriteria.EstimateOnly ? SearchState.EstimateFailed : SearchState.Failed);
				}
			});
		}

		// Token: 0x06001059 RID: 4185 RVA: 0x00049634 File Offset: 0x00047834
		private void CatchKnownExceptions(Action tryDelegate, Action<Exception> finallyDelegate)
		{
			Exception obj = null;
			try
			{
				tryDelegate();
			}
			catch (AccessDeniedException ex)
			{
				obj = ex;
			}
			catch (ObjectNotFoundException ex2)
			{
				obj = ex2;
			}
			catch (StorageTransientException ex3)
			{
				obj = ex3;
			}
			catch (StoragePermanentException ex4)
			{
				obj = ex4;
			}
			finally
			{
				finallyDelegate(obj);
			}
		}

		// Token: 0x0600105A RID: 4186 RVA: 0x00049794 File Offset: 0x00047994
		private void SearchMainEntry()
		{
			this.CatchKnownExceptions(delegate
			{
				this.InternalSearchMainEntry();
			}, delegate(Exception exception)
			{
				if (exception != null)
				{
					this.SearchState = (this.SearchMailboxCriteria.EstimateOnly ? SearchState.EstimateFailed : SearchState.Failed);
					this.FireExceptionEvent(exception);
					SearchMailboxExecuter.Tracer.TraceError<Exception>((long)this.GetHashCode(), "SearchMainEntry throws {0}", exception);
				}
				StreamLogItem streamLogItem = this.StreamLogItem;
				this.CatchKnownExceptions(delegate
				{
					this.searchCompleteEvent.Set();
					if (this.userCallback != null)
					{
						this.userCallback(this.asyncResult);
					}
				}, delegate(Exception e)
				{
					if (e != null)
					{
						SearchMailboxExecuter.Tracer.TraceError<Exception>((long)this.GetHashCode(), "UserCallBack in search throws {0}", e);
					}
					if (streamLogItem != null)
					{
						streamLogItem.Dispose();
					}
				});
			});
		}

		// Token: 0x0600105B RID: 4187 RVA: 0x00049950 File Offset: 0x00047B50
		private void ProcessCommunicatorEvents(SearchMailboxExecuter.ThreadThrottler threadThrottler, ref double searchProgress, ref int progressingWorkers, bool isAborted)
		{
			List<Pair<int, Exception>> list = new List<Pair<int, Exception>>();
			List<StreamLogItem.LogItem> logList = new List<StreamLogItem.LogItem>();
			List<SearchMailboxWorker> list2 = new List<SearchMailboxWorker>();
			double num = 0.0;
			lock (this.searchCommunicator)
			{
				num = this.searchCommunicator.OverallProgress;
				progressingWorkers = this.searchCommunicator.ProgressingWorkers;
				long overallResultItems = this.searchCommunicator.OverallResultItems;
				ByteQuantifiedSize overallResultSize = this.searchCommunicator.OverallResultSize;
				if (this.searchCommunicator.WorkerExceptions.Count > 0)
				{
					list.AddRange(this.searchCommunicator.WorkerExceptions);
					this.searchCommunicator.WorkerExceptions.Clear();
					List<int> list3 = new List<int>();
					List<Pair<int, Exception>> list4 = new List<Pair<int, Exception>>();
					foreach (Pair<int, Exception> pair in list)
					{
						SearchMailboxWorker searchMailboxWorker = this.searchMailboxWorkers[pair.First];
						Exception second = pair.Second;
						bool flag2 = false;
						if (list3.Contains(pair.First) || threadThrottler.WorkerQueue.Contains(searchMailboxWorker))
						{
							flag2 = true;
							list4.Add(pair);
						}
						if (!this.EstimationPhase && !this.searchMailboxCriteria.ExcludePurgesFromDumpster && !flag2 && this.IsRetryableException(second) && searchMailboxWorker.NumberOfRetry < this.retryThreshold)
						{
							list3.Add(pair.First);
							list4.Add(pair);
							if (!this.SearchMailboxCriteria.ExcludeDuplicateMessages || searchMailboxWorker.HasPendingHashSetVerification)
							{
								using (MailboxSession mailboxSession = SearchUtils.OpenMailboxSession(this.targetUser as ADUser, this.OwnerIdentity))
								{
									if (!this.SearchMailboxCriteria.ExcludeDuplicateMessages)
									{
										searchMailboxWorker.DeleteMailboxSearchResultFolder(mailboxSession);
									}
									else if (searchMailboxWorker.HasPendingHashSetVerification)
									{
										SearchMailboxExecuter.Tracer.TraceDebug<int, int>((long)this.GetHashCode(), "ProcessCommunicatorEvents -> Before rebuild hash set, ProcessedMessages={0}, ProcessedMessageIds={1}", this.searchCommunicator.ProcessedMessages.Count, this.searchCommunicator.ProcessedMessageIds.Count);
										this.BuildUniqueMessageHashSetBeforeRetry(mailboxSession, searchMailboxWorker.TargetFolderId);
										SearchMailboxExecuter.Tracer.TraceDebug<int, int>((long)this.GetHashCode(), "ProcessCommunicatorEvents -> After rebuild hash set, ProcessedMessages={0}, ProcessedMessageIds={1}", this.searchCommunicator.ProcessedMessages.Count, this.searchCommunicator.ProcessedMessageIds.Count);
										searchMailboxWorker.HasPendingHashSetVerification = false;
									}
								}
							}
							searchMailboxWorker.NumberOfRetry++;
							searchMailboxWorker.RunnableTime = this.GetWorkerRunnableTimeAfterRetry(searchMailboxWorker.NumberOfRetry);
							threadThrottler.WorkerQueue.Add(searchMailboxWorker);
							this.searchCommunicator.ResetWorker(searchMailboxWorker, false);
							num = this.searchCommunicator.OverallProgress;
							progressingWorkers = this.searchCommunicator.ProgressingWorkers;
							overallResultItems = this.searchCommunicator.OverallResultItems;
							overallResultSize = this.searchCommunicator.OverallResultSize;
						}
					}
					if (list4.Count > 0)
					{
						foreach (Pair<int, Exception> item in list4)
						{
							list.Remove(item);
						}
					}
				}
				if (this.searchCommunicator.WorkerLogs.Count > 0)
				{
					logList.AddRange(this.searchCommunicator.WorkerLogs);
					this.searchCommunicator.WorkerLogs.Clear();
				}
				if (this.searchCommunicator.CompletedWorkers.Count > 0)
				{
					list2.AddRange(this.searchCommunicator.CompletedWorkers);
					this.searchCommunicator.CompletedWorkers.Clear();
					if (this.LogLevel != LoggingLevel.Suppress)
					{
						using (MailboxSession targetMailbox = (!this.EstimationPhase) ? SearchUtils.OpenMailboxSession(this.targetUser as ADUser, this.OwnerIdentity) : null)
						{
							list2.ForEach(delegate(SearchMailboxWorker x)
							{
								bool flag3 = x.LastException == null;
								if (this.SearchMailboxCriteria.ExcludeDuplicateMessages || this.searchMailboxCriteria.ExcludePurgesFromDumpster)
								{
									flag3 = true;
								}
								if (!flag3)
								{
									x.DeleteMailboxSearchResultFolder(targetMailbox);
									if (x.NumberOfRetry >= this.retryThreshold)
									{
										this.searchCommunicator.ResetWorker(x, true);
									}
								}
								if (this.StreamLogItem != null)
								{
									this.FlushLogs(logList);
									try
									{
										this.StreamLogItem.ConsolidateLog(x.WorkerId, flag3);
									}
									catch (ArgumentException innerException)
									{
										this.SearchState = SearchState.Failed;
										this.FireExceptionEvent(new SearchLogFileCreateException(innerException));
									}
									catch (StoragePermanentException innerException2)
									{
										this.SearchState = SearchState.Failed;
										this.FireExceptionEvent(new SearchLogFileCreateException(innerException2));
									}
									catch (StorageTransientException innerException3)
									{
										this.SearchState = SearchState.Failed;
										this.FireExceptionEvent(new SearchLogFileCreateException(innerException3));
									}
								}
							});
						}
					}
				}
				if (searchProgress != num)
				{
					this.FireProgressEvent(Strings.ProgressSearching, Strings.ProgressSearchingInProgress, (int)num, overallResultItems, overallResultSize);
					searchProgress = num;
				}
			}
			if (!isAborted && list2.Count > 0)
			{
				list2.ForEach(delegate(SearchMailboxWorker x)
				{
					threadThrottler.OnEndWorker(x);
				});
			}
			if (threadThrottler.WorkerQueue.Count > 0)
			{
				int num2 = progressingWorkers - threadThrottler.WorkerQueue.Count;
				if (num2 < this.ThreadLimit)
				{
					threadThrottler.PumpWorkerThreads(this.ThreadLimit - num2);
				}
			}
			if (list.Count > 0)
			{
				foreach (Pair<int, Exception> pair2 in list)
				{
					this.FireExceptionEvent(this.searchMailboxWorkers[pair2.First], pair2.Second);
				}
				list.Clear();
			}
			this.FlushLogs(logList);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00049F28 File Offset: 0x00048128
		private void FlushLogs(List<StreamLogItem.LogItem> logList)
		{
			if (logList.Count > 0)
			{
				foreach (StreamLogItem.LogItem logItem in logList)
				{
					logItem.Logs.ForEach(delegate(LocalizedString x)
					{
						this.FireLoggingEvent(x);
					});
				}
				if (this.StreamLogItem != null)
				{
					this.StreamLogItem.WriteLogs(logList);
				}
				logList.Clear();
			}
		}

		// Token: 0x0600105D RID: 4189 RVA: 0x00049FB0 File Offset: 0x000481B0
		private StoreId GetSubFolderId(MailboxSession mailboxSession, StoreId targetFolderId)
		{
			using (Folder folder = Folder.Bind(mailboxSession, targetFolderId))
			{
				List<Pair<StoreId, string>> subFoldersWithIdAndName = folder.GetSubFoldersWithIdAndName();
				if (subFoldersWithIdAndName != null && subFoldersWithIdAndName.Count > 0)
				{
					return subFoldersWithIdAndName[0].First;
				}
			}
			return null;
		}

		// Token: 0x0600105E RID: 4190 RVA: 0x0004A008 File Offset: 0x00048208
		private string GetUniqueSubFolderName(int index, string[] uniqueSubFolderNames, MailboxSession mailboxSession, StoreId targetFolderId)
		{
			if (this.SearchMailboxCriteria.ExcludeDuplicateMessages)
			{
				string arg = uniqueSubFolderNames[0];
				if (mailboxSession != null && targetFolderId != null && this.ResumeSearch)
				{
					using (Folder folder = Folder.Bind(mailboxSession, targetFolderId))
					{
						List<Pair<StoreId, string>> subFoldersWithIdAndName = folder.GetSubFoldersWithIdAndName();
						if (subFoldersWithIdAndName != null && subFoldersWithIdAndName.Count > 0)
						{
							return subFoldersWithIdAndName[0].Second;
						}
					}
				}
				return string.Format("{0}-{1}", arg, this.SearchStartTime);
			}
			return string.Format("{0}-{1}", uniqueSubFolderNames[index], this.SearchStartTime);
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x0004A0B0 File Offset: 0x000482B0
		private string[] CreateUniqueSubFolderNames(SearchUser[] searchUserScope)
		{
			if (this.SearchMailboxCriteria.ExcludeDuplicateMessages)
			{
				return new string[]
				{
					Strings.TargetFolder
				};
			}
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			string[] array = new string[searchUserScope.Length];
			for (int i = 0; i < searchUserScope.Length; i++)
			{
				if (!hashSet.Contains(searchUserScope[i].DisplayName))
				{
					array[i] = searchUserScope[i].DisplayName;
				}
				else
				{
					string text = string.Format("{0}({1})", searchUserScope[i].DisplayName, searchUserScope[i].Id.DomainId.Name);
					if (!hashSet.Contains(text))
					{
						array[i] = text;
					}
					else
					{
						array[i] = string.Format("{0}({1})", searchUserScope[i].DisplayName, searchUserScope[i].Id.DistinguishedName);
					}
				}
				hashSet.Add(array[i]);
			}
			return array;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0004A18C File Offset: 0x0004838C
		private void InternalSearchMainEntry()
		{
			try
			{
				MailboxDataProvider.IncrementDiscoveryMailboxSearchPerfCounters(this.SearchMailboxCriteria.SearchUserScope.Length);
				this.FireProgressEvent(Strings.ProgressOpening, Strings.ProgressOpeningTarget, 0, 0L, ByteQuantifiedSize.Zero);
				using (Referenced<MailboxSession> referenced = this.EstimationPhase ? null : Referenced<MailboxSession>.Acquire(SearchUtils.OpenMailboxSession(this.targetUser as ADUser, this.OwnerIdentity)))
				{
					StoreId storeId = null;
					if (!this.EstimationPhase)
					{
						if (referenced == null)
						{
							return;
						}
						storeId = this.GetTargetFolderId(referenced);
					}
					if (storeId != null && this.LogLevel != LoggingLevel.Suppress)
					{
						this.StreamLogItem = new StreamLogItem(referenced, this.LogMessageId, storeId, string.Format("{0}-{1}", this.Name ?? "Search Results", this.SearchStartTime), this.Name ?? "Search Results");
						this.FireRequestLogBodyEvent(this.StreamLogItem.MessageItem.Body);
						this.StreamLogItem.Save(true);
					}
					string[] uniqueSubFolderNames = this.CreateUniqueSubFolderNames(this.SearchMailboxCriteria.SearchUserScope);
					MailboxSession mailboxSession = (referenced == null) ? null : referenced.Value;
					if (!this.EstimationPhase && this.ResumeSearch && this.SearchMailboxCriteria.ExcludeDuplicateMessages)
					{
						this.BuildUniqueMessageHashSetFromExistingSearchFolder(mailboxSession, storeId);
					}
					for (int i = 0; i < this.searchMailboxCriteria.SearchUserScope.Length; i++)
					{
						SearchMailboxWorker searchMailboxWorker = new SearchMailboxWorker(this.searchMailboxCriteria, i);
						searchMailboxWorker.SubfolderName = this.GetUniqueSubFolderName(i, uniqueSubFolderNames, mailboxSession, storeId);
						searchMailboxWorker.OwnerIdentity = this.OwnerIdentity;
						searchMailboxWorker.RecipientSession = this.recipientSession;
						searchMailboxWorker.TargetUser = (this.EstimationPhase ? null : this.TargetUser);
						searchMailboxWorker.TargetFolderId = storeId;
						searchMailboxWorker.SearchCommunicator = this.searchCommunicator;
						searchMailboxWorker.LoggingLevel = this.LogLevel;
						searchMailboxWorker.RunnableTime = ExDateTime.Now;
						searchMailboxWorker.SearchObject = this.SearchObject;
						searchMailboxWorker.ExecutingUserIdentity = this.ExecutingUserIdentity;
						ADUser aduser = this.targetUser as ADUser;
						if (aduser != null)
						{
							searchMailboxWorker.TargetMailboxQuota = aduser.ProhibitSendReceiveQuota;
						}
						else
						{
							searchMailboxWorker.TargetMailboxQuota = Unlimited<ByteQuantifiedSize>.UnlimitedValue;
						}
						if (this.searchActions != null && this.searchActions.Count > 0)
						{
							searchMailboxWorker.SearchActions = this.searchActions;
						}
						if (this.ResumeSearch && !this.SearchMailboxCriteria.ExcludeDuplicateMessages)
						{
							searchMailboxWorker.DeleteMailboxSearchResultFolder(mailboxSession);
						}
						this.searchMailboxWorkers.Add(searchMailboxWorker);
					}
					SearchMailboxExecuter.ThreadThrottler threadThrottler = new SearchMailboxExecuter.ThreadThrottler(this, this.searchMailboxWorkers);
					threadThrottler.PumpWorkerThreads(-1);
					this.FireProgressEvent(Strings.ProgressSearching, Strings.ProgressSearchingSources, 0, 0L, ByteQuantifiedSize.Zero);
					double num = 0.0;
					int count = this.searchMailboxWorkers.Count;
					WaitHandle[] waitHandles = new WaitHandle[]
					{
						this.searchCommunicator.AbortEvent,
						this.searchCommunicator.ProgressEvent
					};
					bool flag = count <= 0;
					while (!flag)
					{
						int num2 = WaitHandle.WaitAny(waitHandles, 5000);
						if (num2 == 0)
						{
							break;
						}
						this.ProcessCommunicatorEvents(threadThrottler, ref num, ref count, false);
						if (count == 0)
						{
							flag = true;
						}
					}
					if (!this.EstimationPhase && this.SearchMailboxCriteria.ExcludeDuplicateMessages)
					{
						StoreId subFolderId = this.GetSubFolderId(referenced, storeId);
						if (subFolderId != null)
						{
							this.DedupeAndCleanupMessageProperties(referenced, subFolderId);
							this.searchCommunicator.UpdateResults(referenced, subFolderId);
						}
					}
					while (count > threadThrottler.WorkerQueue.Count && this.WaitForWorkersWhenAborted)
					{
						this.searchCommunicator.ProgressEvent.WaitOne();
						this.ProcessCommunicatorEvents(threadThrottler, ref num, ref count, true);
					}
					if (flag)
					{
						this.PostProcessSearch();
						this.FireProgressEvent(Strings.ProgressCompleting, Strings.ProgressCompletingSearch, 100, this.searchCommunicator.OverallResultItems, this.searchCommunicator.OverallResultSize);
					}
				}
			}
			finally
			{
				if (this.searchState == SearchState.InProgress)
				{
					this.searchState = SearchState.Failed;
				}
				else if (this.searchState == SearchState.EstimateInProgress)
				{
					this.searchState = SearchState.EstimateFailed;
				}
				if (this.StreamLogItem != null)
				{
					this.StreamLogItem.CloseOpenedStream();
					this.FireRequestLogBodyEvent(this.StreamLogItem.MessageItem.Body);
					this.StreamLogItem.Save(true);
				}
				MailboxDataProvider.DecrementDiscoveryMailboxSearchPerfCounters(this.SearchMailboxCriteria.SearchUserScope.Length);
			}
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x0004A5F8 File Offset: 0x000487F8
		private void DedupeAndCleanupMessageProperties(Referenced<MailboxSession> targetMailbox, StoreId targetFolderId)
		{
			using (Folder folder = Folder.Bind(targetMailbox, targetFolderId))
			{
				using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, new SortBy[]
				{
					new SortBy(ConversationItemSchema.ConversationId, SortOrder.Ascending)
				}, new PropertyDefinition[]
				{
					ConversationItemSchema.ConversationId,
					ItemSchema.Id
				}))
				{
					HashSet<StoreId> bccItemIds = SearchUtils.GetBccItemIds(folder);
					for (;;)
					{
						object[][] rows = queryResult.GetRows(ResponseThrottler.MaxBulkSize);
						if (rows == null || rows.Length <= 0)
						{
							break;
						}
						List<StoreObjectId> list = new List<StoreObjectId>();
						for (int i = 0; i < rows.Length; i++)
						{
							if (rows[i][0] != null && !(rows[i][0] is PropertyError))
							{
								ConversationId conversationId = (ConversationId)rows[i][0];
								StoreId storeId = (StoreId)rows[i][1];
								StoreObjectId storeObjectId = StoreId.GetStoreObjectId(storeId);
								Conversation conversation = Conversation.Load(targetMailbox, conversationId, null, true, new PropertyDefinition[0]);
								if (conversation != null)
								{
									IConversationTreeNode conversationTreeNode = null;
									bool flag = conversation.ConversationTree.TryGetConversationTreeNode(storeObjectId, out conversationTreeNode);
									if (flag)
									{
										bool flag2 = !conversationTreeNode.HasChildren || conversation.ConversationNodeContainedInChildren(conversationTreeNode);
										bool flag3 = bccItemIds.Contains(storeId);
										if (conversationTreeNode.HasChildren && flag2 && !flag3)
										{
											list.Add(storeObjectId);
										}
									}
								}
							}
						}
						folder.DeleteObjects(DeleteItemFlags.HardDelete, list.ToArray());
					}
				}
			}
			using (Folder folder2 = Folder.Bind(targetMailbox, targetFolderId))
			{
				using (QueryResult queryResult2 = folder2.ItemQuery(ItemQueryType.None, null, new SortBy[]
				{
					new SortBy(ItemSchema.ReceivedTime, SortOrder.Ascending)
				}, new PropertyDefinition[]
				{
					ItemSchema.Id
				}))
				{
					folder2.MarkAllAsRead(true);
					for (;;)
					{
						object[][] rows2 = queryResult2.GetRows(ResponseThrottler.MaxBulkSize);
						if (rows2 == null || rows2.Length <= 0)
						{
							break;
						}
						for (int j = 0; j < rows2.Length; j++)
						{
							if (rows2[j][0] != null && !(rows2[j][0] is PropertyError))
							{
								StoreId storeId2 = (StoreId)rows2[j][0];
								using (Item item = Item.Bind(targetMailbox, storeId2))
								{
									item.SetOrDeleteProperty(ItemSchema.ConversationIndexTracking, null);
									item.SetOrDeleteProperty(ItemSchema.Importance, null);
									item.Categories.Clear();
									if (!(item is CalendarItemBase) && !(item is Task))
									{
										item.ClearFlag();
									}
									item.Save(SaveMode.ResolveConflicts);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0004A91C File Offset: 0x00048B1C
		internal void BuildUniqueMessageHashSetFromExistingSearchFolder(MailboxSession mailboxSession, StoreId targetFolderId)
		{
			this.searchCommunicator.ProcessedMessages.Clear();
			this.searchCommunicator.ProcessedMessageIds.Clear();
			if (targetFolderId == null)
			{
				return;
			}
			using (Folder folder = Folder.Bind(mailboxSession, targetFolderId))
			{
				List<Pair<StoreId, string>> subFoldersWithIdAndName = folder.GetSubFoldersWithIdAndName();
				if (subFoldersWithIdAndName != null && subFoldersWithIdAndName.Count != 0)
				{
					StoreId first = subFoldersWithIdAndName[0].First;
					using (Folder folder2 = Folder.Bind(mailboxSession, first))
					{
						using (QueryResult queryResult = folder2.ItemQuery(ItemQueryType.None, null, null, (this.LogLevel == LoggingLevel.Full) ? SearchMailboxWorker.ItemPreloadPropertiesWithLogging : SearchMailboxWorker.ItemPreloadProperties))
						{
							StoreId[] array = new StoreId[ResponseThrottler.MaxBulkSize];
							ResponseThrottler responseThrottler = new ResponseThrottler(this.searchCommunicator.AbortEvent);
							while (!this.searchCommunicator.IsAborted)
							{
								responseThrottler.BackOffFromStore(mailboxSession);
								object[][] rows = queryResult.GetRows(array.Length);
								if (rows == null || rows.Length <= 0)
								{
									break;
								}
								for (int i = 0; i < rows.Length; i++)
								{
									StoreObjectId storeObjectId = null;
									UniqueItemHash uniqueItemHash = SearchMailboxWorker.BuildUniqueItemHash(mailboxSession, rows[i], null, out storeObjectId);
									if (uniqueItemHash != null)
									{
										if (!this.searchCommunicator.ProcessedMessages.Contains(uniqueItemHash))
										{
											this.searchCommunicator.ProcessedMessages.Add(uniqueItemHash);
										}
									}
									else
									{
										string internetMessageId = SearchMailboxWorker.GetInternetMessageId(rows[i]);
										if (internetMessageId != null && !this.searchCommunicator.ProcessedMessageIds.Contains(internetMessageId))
										{
											this.searchCommunicator.ProcessedMessageIds.Add(internetMessageId);
										}
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x0004AAFC File Offset: 0x00048CFC
		internal void BuildUniqueMessageHashSetBeforeRetry(MailboxSession mailboxSession, StoreId targetFolderId)
		{
			lock (mailboxSession)
			{
				this.BuildUniqueMessageHashSetFromExistingSearchFolder(mailboxSession, targetFolderId);
			}
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x0004AB3C File Offset: 0x00048D3C
		private bool IsRetryableException(Exception ex)
		{
			return ex is StorageTransientException || (ex is SearchMailboxException && ex.InnerException != null && ex.InnerException is PartialCompletionException);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0004AB68 File Offset: 0x00048D68
		private ExDateTime GetWorkerRunnableTimeAfterRetry(int retry)
		{
			if (retry <= 0)
			{
				return ExDateTime.Now;
			}
			int num = this.retryWaitInterval;
			for (int i = 2; i <= retry; i++)
			{
				num *= this.retryWaitFactor;
			}
			return ExDateTime.Now.AddMilliseconds((double)num);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0004ABAC File Offset: 0x00048DAC
		internal static int GetSettingsValue(string regKey, int defaultValue)
		{
			int num = 0;
			using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(SearchMailboxExecuter.discoveryRegistryPath))
			{
				if (registryKey != null && registryKey.GetValue(regKey) != null)
				{
					object value = registryKey.GetValue(regKey);
					if (value != null && value is int)
					{
						num = (int)value;
					}
				}
			}
			if (num <= 0)
			{
				return defaultValue;
			}
			return num;
		}

		// Token: 0x04000AF8 RID: 2808
		private const int WaitEventTimeout = 5000;

		// Token: 0x04000AF9 RID: 2809
		protected static readonly Trace Tracer = ExTraceGlobals.SearchTracer;

		// Token: 0x04000AFA RID: 2810
		private readonly ManualResetEvent searchCompleteEvent = new ManualResetEvent(false);

		// Token: 0x04000AFB RID: 2811
		private IRecipientSession recipientSession;

		// Token: 0x04000AFC RID: 2812
		private IConfigurationSession configurationSession;

		// Token: 0x04000AFD RID: 2813
		private List<SearchMailboxWorker> searchMailboxWorkers = new List<SearchMailboxWorker>();

		// Token: 0x04000AFE RID: 2814
		private SearchCommunicator searchCommunicator;

		// Token: 0x04000AFF RID: 2815
		private readonly int threadLimit;

		// Token: 0x04000B00 RID: 2816
		private readonly int threadLimitPerServer;

		// Token: 0x04000B01 RID: 2817
		private object state;

		// Token: 0x04000B02 RID: 2818
		private AsyncCallback userCallback;

		// Token: 0x04000B03 RID: 2819
		private SearchMailboxExecuter.SearchMailboxAsyncResult asyncResult;

		// Token: 0x04000B04 RID: 2820
		private bool waitForWorkersWhenAborted;

		// Token: 0x04000B05 RID: 2821
		private GenericIdentity ownerIdentity;

		// Token: 0x04000B06 RID: 2822
		private SearchMailboxCriteria searchMailboxCriteria;

		// Token: 0x04000B07 RID: 2823
		private List<SearchMailboxAction> searchActions;

		// Token: 0x04000B08 RID: 2824
		private ADRecipient targetUser;

		// Token: 0x04000B09 RID: 2825
		private string targetFolder;

		// Token: 0x04000B0A RID: 2826
		private ADRecipient[] reviewRecipients;

		// Token: 0x04000B0B RID: 2827
		private LoggingLevel logLevel;

		// Token: 0x04000B0C RID: 2828
		private SearchState searchState = SearchState.EstimateInProgress;

		// Token: 0x04000B0D RID: 2829
		private ExDateTime? searchStartTime;

		// Token: 0x04000B0E RID: 2830
		private EventHandler<SearchProgressEvent> progressHandler;

		// Token: 0x04000B0F RID: 2831
		private EventHandler<SearchLoggingEvent> loggingHandler;

		// Token: 0x04000B10 RID: 2832
		private EventHandler<SearchExceptionEvent> searchExceptionHandler;

		// Token: 0x04000B11 RID: 2833
		private EventHandler<RequestLogBodyEvent> requestBodyHandler;

		// Token: 0x04000B12 RID: 2834
		private static string discoveryRegistryPath = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Discovery";

		// Token: 0x04000B13 RID: 2835
		private int retryThreshold = SearchMailboxExecuter.GetSettingsValue("RetryThreshold", 5);

		// Token: 0x04000B14 RID: 2836
		private int retryWaitInterval = SearchMailboxExecuter.GetSettingsValue("RetryWaitInterval", 5000);

		// Token: 0x04000B15 RID: 2837
		private int retryWaitFactor = SearchMailboxExecuter.GetSettingsValue("RetryWaitFactor", 2);

		// Token: 0x02000237 RID: 567
		internal class ThreadThrottler
		{
			// Token: 0x0600106C RID: 4204 RVA: 0x0004AC4C File Offset: 0x00048E4C
			internal ThreadThrottler(SearchMailboxExecuter searchExecuter, IEnumerable<SearchMailboxWorker> searchWorkers)
			{
				if (searchExecuter == null)
				{
					throw new ArgumentNullException("searchExecuter");
				}
				if (searchWorkers == null)
				{
					throw new ArgumentNullException("searchWorkers");
				}
				this.searchExecuter = searchExecuter;
				IEnumerable<IGrouping<string, SearchMailboxWorker>> source = searchWorkers.GroupBy((SearchMailboxWorker x) => x.SourceUser.ServerName, StringComparer.OrdinalIgnoreCase);
				this.serverThreadMap = source.ToDictionary((IGrouping<string, SearchMailboxWorker> x) => x.Key, (IGrouping<string, SearchMailboxWorker> x) => 0, StringComparer.OrdinalIgnoreCase);
				List<List<SearchMailboxWorker>> list = (from x in source
				select x.ToList<SearchMailboxWorker>()).ToList<List<SearchMailboxWorker>>();
				this.searchWorkerQueue = new List<SearchMailboxWorker>();
				while (list.Count > 0)
				{
					for (int i = list.Count - 1; i >= 0; i--)
					{
						this.searchWorkerQueue.Add(list[i][0]);
						list[i].RemoveAt(0);
						if (list[i].Count <= 0)
						{
							list.RemoveAt(i);
						}
					}
				}
			}

			// Token: 0x17000452 RID: 1106
			// (get) Token: 0x0600106D RID: 4205 RVA: 0x0004AD83 File Offset: 0x00048F83
			internal List<SearchMailboxWorker> WorkerQueue
			{
				get
				{
					return this.searchWorkerQueue;
				}
			}

			// Token: 0x0600106E RID: 4206 RVA: 0x0004AD8C File Offset: 0x00048F8C
			internal void PumpWorkerThreads(int maxWorkers)
			{
				if (maxWorkers == -1)
				{
					int num;
					ThreadPool.GetAvailableThreads(out maxWorkers, out num);
					maxWorkers /= 2;
					maxWorkers = Math.Min(maxWorkers, this.searchExecuter.ThreadLimit);
					if (maxWorkers <= 0)
					{
						throw new ResourcesException(Strings.LowSystemResource);
					}
				}
				int num2 = this.searchWorkerQueue.Count - 1;
				while (num2 >= 0 && maxWorkers > 0)
				{
					SearchMailboxWorker searchMailboxWorker = this.searchWorkerQueue[num2];
					if (this.serverThreadMap[searchMailboxWorker.SourceUser.ServerName] < this.searchExecuter.ThreadLimitPerServer && ExDateTime.Compare(ExDateTime.Now, searchMailboxWorker.RunnableTime) >= 0)
					{
						this.searchWorkerQueue.RemoveAt(num2);
						this.OnBeginWorker(searchMailboxWorker);
						maxWorkers--;
					}
					num2--;
				}
			}

			// Token: 0x0600106F RID: 4207 RVA: 0x0004AE44 File Offset: 0x00049044
			internal void OnBeginWorker(SearchMailboxWorker worker)
			{
				Dictionary<string, int> dictionary;
				string serverName;
				(dictionary = this.serverThreadMap)[serverName = worker.SourceUser.ServerName] = dictionary[serverName] + 1;
				ThreadPool.QueueUserWorkItem(new WaitCallback(worker.ProcessMailbox));
			}

			// Token: 0x06001070 RID: 4208 RVA: 0x0004AE88 File Offset: 0x00049088
			internal void OnEndWorker(SearchMailboxWorker worker)
			{
				Dictionary<string, int> dictionary;
				string serverName;
				(dictionary = this.serverThreadMap)[serverName = worker.SourceUser.ServerName] = dictionary[serverName] - 1;
			}

			// Token: 0x04000B1D RID: 2845
			private SearchMailboxExecuter searchExecuter;

			// Token: 0x04000B1E RID: 2846
			private List<SearchMailboxWorker> searchWorkerQueue;

			// Token: 0x04000B1F RID: 2847
			private Dictionary<string, int> serverThreadMap;
		}

		// Token: 0x02000238 RID: 568
		private class SearchMailboxAsyncResult : IAsyncResult
		{
			// Token: 0x06001075 RID: 4213 RVA: 0x0004AEB8 File Offset: 0x000490B8
			internal SearchMailboxAsyncResult(SearchMailboxExecuter searchMailboxExecuter)
			{
				this.searchMailboxExecuter = searchMailboxExecuter;
			}

			// Token: 0x17000453 RID: 1107
			// (get) Token: 0x06001076 RID: 4214 RVA: 0x0004AEC7 File Offset: 0x000490C7
			public object AsyncState
			{
				get
				{
					return this.searchMailboxExecuter.state;
				}
			}

			// Token: 0x17000454 RID: 1108
			// (get) Token: 0x06001077 RID: 4215 RVA: 0x0004AED4 File Offset: 0x000490D4
			public WaitHandle AsyncWaitHandle
			{
				get
				{
					return this.searchMailboxExecuter.searchCompleteEvent;
				}
			}

			// Token: 0x17000455 RID: 1109
			// (get) Token: 0x06001078 RID: 4216 RVA: 0x0004AEE1 File Offset: 0x000490E1
			public bool CompletedSynchronously
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000456 RID: 1110
			// (get) Token: 0x06001079 RID: 4217 RVA: 0x0004AEE4 File Offset: 0x000490E4
			public bool IsCompleted
			{
				get
				{
					return this.searchMailboxExecuter.searchCompleteEvent.WaitOne(0, false);
				}
			}

			// Token: 0x04000B24 RID: 2852
			private SearchMailboxExecuter searchMailboxExecuter;
		}
	}
}
