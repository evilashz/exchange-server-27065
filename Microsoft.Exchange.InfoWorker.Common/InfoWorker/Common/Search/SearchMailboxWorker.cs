﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.ActiveManager;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.InfoWorker.Common;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.ELC;
using Microsoft.Mapi;

namespace Microsoft.Exchange.InfoWorker.Common.Search
{
	// Token: 0x02000233 RID: 563
	internal class SearchMailboxWorker
	{
		// Token: 0x06000F97 RID: 3991 RVA: 0x00044DCC File Offset: 0x00042FCC
		internal SearchMailboxWorker(SearchMailboxCriteria searchMailboxCriteria, int workId)
		{
			if (searchMailboxCriteria == null)
			{
				throw new ArgumentNullException("searchMailboxCriteria");
			}
			this.searchMailboxCriteria = searchMailboxCriteria;
			this.SearchResult = new SearchMailboxResult(searchMailboxCriteria.SearchUserScope[workId].Id);
			this.workerId = workId;
			this.NumberOfRetry = 0;
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06000F98 RID: 3992 RVA: 0x00044EAD File Offset: 0x000430AD
		internal SearchUser SourceUser
		{
			get
			{
				return this.searchMailboxCriteria.SearchUserScope[this.workerId];
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x00044EC1 File Offset: 0x000430C1
		// (set) Token: 0x06000F9A RID: 3994 RVA: 0x00044EC9 File Offset: 0x000430C9
		public GenericIdentity OwnerIdentity
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

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x00044ED2 File Offset: 0x000430D2
		internal int WorkerId
		{
			get
			{
				return this.workerId;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x00044EDA File Offset: 0x000430DA
		// (set) Token: 0x06000F9D RID: 3997 RVA: 0x00044EE2 File Offset: 0x000430E2
		internal int NumberOfRetry { get; set; }

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x00044EEB File Offset: 0x000430EB
		// (set) Token: 0x06000F9F RID: 3999 RVA: 0x00044EF3 File Offset: 0x000430F3
		internal ExDateTime RunnableTime { get; set; }

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000FA0 RID: 4000 RVA: 0x00044EFC File Offset: 0x000430FC
		// (set) Token: 0x06000FA1 RID: 4001 RVA: 0x00044F04 File Offset: 0x00043104
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

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x00044F0D File Offset: 0x0004310D
		// (set) Token: 0x06000FA3 RID: 4003 RVA: 0x00044F15 File Offset: 0x00043115
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

		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x00044F1E File Offset: 0x0004311E
		// (set) Token: 0x06000FA5 RID: 4005 RVA: 0x00044F26 File Offset: 0x00043126
		internal MailboxSession TargetMailbox
		{
			get
			{
				return this.targetMailbox;
			}
			set
			{
				this.targetMailbox = value;
			}
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00044F2F File Offset: 0x0004312F
		// (set) Token: 0x06000FA7 RID: 4007 RVA: 0x00044F37 File Offset: 0x00043137
		internal StoreId TargetFolderId
		{
			get
			{
				return this.targetFolderId;
			}
			set
			{
				this.targetFolderId = value;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000FA8 RID: 4008 RVA: 0x00044F40 File Offset: 0x00043140
		// (set) Token: 0x06000FA9 RID: 4009 RVA: 0x00044F48 File Offset: 0x00043148
		internal List<SearchMailboxAction> SearchActions
		{
			get
			{
				return this.searchActions;
			}
			set
			{
				if (value == null)
				{
					this.searchActions = null;
					return;
				}
				List<SearchMailboxAction> list = new List<SearchMailboxAction>(value.Count);
				foreach (SearchMailboxAction searchMailboxAction in value)
				{
					SearchMailboxAction item = searchMailboxAction.Clone();
					list.Add(item);
				}
				this.searchActions = list;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x00044FBC File Offset: 0x000431BC
		// (set) Token: 0x06000FAB RID: 4011 RVA: 0x00044FC4 File Offset: 0x000431C4
		internal string SubfolderName
		{
			get
			{
				return this.subFolderName;
			}
			set
			{
				this.subFolderName = value;
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x00044FCD File Offset: 0x000431CD
		// (set) Token: 0x06000FAD RID: 4013 RVA: 0x00044FD5 File Offset: 0x000431D5
		internal StoreId TargetSubFolderId
		{
			get
			{
				return this.targetSubFolderId;
			}
			set
			{
				this.targetSubFolderId = value;
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x00044FDE File Offset: 0x000431DE
		internal bool SearchDumpster
		{
			get
			{
				return this.searchMailboxCriteria.SearchDumpster;
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x00044FEB File Offset: 0x000431EB
		internal QueryFilter SearchFilter
		{
			get
			{
				return this.searchMailboxCriteria.SearchFilter;
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x00044FF8 File Offset: 0x000431F8
		internal bool IncludeUnsearchableItems
		{
			get
			{
				return this.searchMailboxCriteria.IncludeUnsearchableItems;
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00045005 File Offset: 0x00043205
		internal bool IncludePersonalArchive
		{
			get
			{
				return this.searchMailboxCriteria.IncludePersonalArchive;
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x00045012 File Offset: 0x00043212
		internal bool IncludeRemoteAccounts
		{
			get
			{
				return this.searchMailboxCriteria.IncludeRemoteAccounts;
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0004501F File Offset: 0x0004321F
		internal bool ExcludeDuplicateMessages
		{
			get
			{
				return this.searchMailboxCriteria.ExcludeDuplicateMessages;
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0004502C File Offset: 0x0004322C
		// (set) Token: 0x06000FB5 RID: 4021 RVA: 0x00045034 File Offset: 0x00043234
		internal SearchCommunicator SearchCommunicator
		{
			get
			{
				return this.searchCommunicator;
			}
			set
			{
				this.searchCommunicator = value;
			}
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0004503D File Offset: 0x0004323D
		internal bool EstimationPhase
		{
			get
			{
				return this.TargetUser == null && !this.searchMailboxCriteria.ExcludePurgesFromDumpster;
			}
		}

		// Token: 0x17000426 RID: 1062
		// (get) Token: 0x06000FB7 RID: 4023 RVA: 0x00045057 File Offset: 0x00043257
		// (set) Token: 0x06000FB8 RID: 4024 RVA: 0x0004505F File Offset: 0x0004325F
		internal LoggingLevel LoggingLevel
		{
			get
			{
				return this.loggingLevel;
			}
			set
			{
				this.loggingLevel = value;
			}
		}

		// Token: 0x17000427 RID: 1063
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00045068 File Offset: 0x00043268
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x00045070 File Offset: 0x00043270
		internal SearchMailboxResult SearchResult
		{
			get
			{
				return this.searchResult;
			}
			set
			{
				this.searchResult = value;
			}
		}

		// Token: 0x17000428 RID: 1064
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00045079 File Offset: 0x00043279
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x00045081 File Offset: 0x00043281
		internal Exception LastException
		{
			get
			{
				return this.lastException;
			}
			set
			{
				this.lastException = value;
			}
		}

		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x0004508A File Offset: 0x0004328A
		internal bool IsFullLogging
		{
			get
			{
				return this.LoggingLevel == LoggingLevel.Full;
			}
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x00045095 File Offset: 0x00043295
		internal double CurrentProgress
		{
			get
			{
				return this.mailboxProgresses.Average();
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x000450A2 File Offset: 0x000432A2
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x000450AA File Offset: 0x000432AA
		internal Unlimited<ByteQuantifiedSize> TargetMailboxQuota { get; set; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x000450B3 File Offset: 0x000432B3
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x000450BB File Offset: 0x000432BB
		internal bool HasPendingHashSetVerification { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x000450C4 File Offset: 0x000432C4
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x000450D3 File Offset: 0x000432D3
		public double MailboxProgress
		{
			get
			{
				return this.mailboxProgresses[this.currentMailboxId];
			}
			set
			{
				this.mailboxProgresses[this.currentMailboxId] = value;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x000450E3 File Offset: 0x000432E3
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x000450EB File Offset: 0x000432EB
		internal SearchObject SearchObject { get; set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x000450F4 File Offset: 0x000432F4
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x000450FC File Offset: 0x000432FC
		internal ObjectId ExecutingUserIdentity { get; set; }

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00045105 File Offset: 0x00043305
		private int TotalMailboxes
		{
			get
			{
				if (this.SearchObject.SearchStatus == null)
				{
					return 0;
				}
				return this.SearchObject.SearchStatus.NumberMailboxesToSearch;
			}
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x00045128 File Offset: 0x00043328
		internal bool InternalProcessMailbox(ExchangePrincipal principal, AlternateMailbox alternateMailbox)
		{
			bool result;
			using (MailboxSession mailboxSession = SearchUtils.OpenMailboxSession(principal, this.OwnerIdentity))
			{
				if (mailboxSession == null)
				{
					SearchMailboxWorker.Tracer.TraceDebug((long)this.GetHashCode(), "OpenMailboxSession returned a null session!");
					result = false;
				}
				else
				{
					this.UpdateProgress(5.0);
					this.sourceStoreThrotter = new ResponseThrottler(this.searchCommunicator.AbortEvent);
					if (this.SearchFilter != null && !this.IsContentIndexingOnMailboxDatabaseEnabled(mailboxSession))
					{
						this.ReportException(new SearchMailboxException(Strings.CISearchFailed(string.Format("{0}\\{1}", mailboxSession.MailboxOwner.ObjectId.DomainUserName(), principal.MailboxInfo.MailboxGuid))));
						result = false;
					}
					else
					{
						if (this.TargetUser == null)
						{
							this.SearchAndCopyMailbox(mailboxSession, null, null, null);
						}
						else
						{
							using (this.TargetMailbox = SearchUtils.OpenMailboxSession(this.TargetUser as ADUser, this.OwnerIdentity))
							{
								if (this.TargetMailbox == null)
								{
									SearchMailboxWorker.Tracer.TraceDebug((long)this.GetHashCode(), "OpenMailboxSession for the target returned a null session!");
									return false;
								}
								if (this.targetStoreThrotter == null)
								{
									this.targetStoreThrotter = new ResponseThrottler(this.searchCommunicator.AbortEvent);
								}
								string text = string.Empty;
								if (alternateMailbox == null)
								{
									if (principal.MailboxInfo.IsArchive)
									{
										text = Strings.ArchiveMailbox;
									}
									else
									{
										text = Strings.PrimaryMailbox;
									}
								}
								else if (alternateMailbox.Type == AlternateMailbox.AlternateMailboxFlags.Subscription)
								{
									text = Strings.RemoteMailbox(alternateMailbox.Name);
								}
								string[] targetSubfolders = new string[]
								{
									this.SubfolderName,
									text
								};
								this.SearchAndCopyMailbox(mailboxSession, this.TargetMailbox, this.TargetFolderId, targetSubfolders);
							}
							this.TargetMailbox = null;
						}
						result = true;
					}
				}
			}
			return result;
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x0004532C File Offset: 0x0004352C
		private void InternalProcessRemoteMailbox(ExchangePrincipal principal)
		{
			ADUser aduser = null;
			ADObjectId adobjectId = this.ExecutingUserIdentity as ADObjectId;
			if (adobjectId != null)
			{
				aduser = this.recipientSession.FindADUserByObjectId(adobjectId);
			}
			if (aduser == null)
			{
				throw new SearchMailboxException(ServerStrings.ADUserNotFoundId(this.ExecutingUserIdentity.ToString()));
			}
			using (MailboxSearchEwsClient mailboxSearchEwsClient = new MailboxSearchEwsClient(principal, aduser))
			{
				IList<KeywordHit> list = mailboxSearchEwsClient.FindMailboxStatisticsByKeywords(principal, this.SearchObject);
				foreach (KeywordHit keywordHit in list)
				{
					if (keywordHit.Phrase == this.searchMailboxCriteria.UserQuery)
					{
						this.SearchResult.ResultItemsCount += keywordHit.Count;
						this.SearchResult.ResultItemsSize += keywordHit.Size;
					}
					this.CollectKeywordResults(new KeywordHit
					{
						Phrase = keywordHit.Phrase,
						Count = keywordHit.Count
					});
				}
			}
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x0004548C File Offset: 0x0004368C
		internal void InternalProcessMailbox()
		{
			ADRecipient adrecipient = this.recipientSession.Read(this.SourceUser.Id);
			try
			{
				if (adrecipient == null || (adrecipient.RecipientType != RecipientType.UserMailbox && !RemoteMailbox.IsRemoteMailbox(adrecipient.RecipientTypeDetails)))
				{
					throw new SearchMailboxException(new LocalizedString("Source user must be a mailbox user"));
				}
				ADUser aduser = (ADUser)adrecipient;
				Thread.CurrentThread.Name = string.Format("SearchMailboxWorker on {0}", this.SourceUser.DisplayName);
				SearchMailboxWorker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "SearchMailboxWorker started on user '{0}'", this.SourceUser.DisplayName);
				if (this.SearchCommunicator.IsAborted)
				{
					return;
				}
				this.legalHoldEnabled = (aduser.LitigationHoldEnabled || aduser.SingleItemRecoveryEnabled);
				this.discoveryHoldEnabled = (aduser.InPlaceHolds != null && aduser.InPlaceHolds.Count > 0);
				List<ExchangePrincipal> mailboxList = new List<ExchangePrincipal>();
				ExchangePrincipal primaryEP = ExchangePrincipal.FromADUser(aduser.OrganizationId.ToADSessionSettings(), aduser, RemotingOptions.AllowCrossSite | RemotingOptions.AllowCrossPremise);
				mailboxList.Add(primaryEP);
				if (this.IncludePersonalArchive && aduser.ArchiveGuid != Guid.Empty)
				{
					ExchangePrincipal archiveExchangePrincipal = primaryEP.GetArchiveExchangePrincipal(RemotingOptions.AllowCrossSite | RemotingOptions.AllowCrossPremise);
					mailboxList.Add(archiveExchangePrincipal);
				}
				if (this.IncludeRemoteAccounts)
				{
					aduser.AggregatedMailboxGuids.ForEach(delegate(Guid x)
					{
						mailboxList.Add(ExchangePrincipal.FromMailboxGuid(primaryEP.MailboxInfo.OrganizationId.ToADSessionSettings(), x, RemotingOptions.AllowCrossSite | RemotingOptions.AllowCrossPremise, null));
					});
				}
				this.ResetProgresses(mailboxList.Count);
				this.SearchResult.Success = true;
				int discoveryMaxMailboxes = (int)SearchUtils.GetDiscoveryMaxMailboxes(this.RecipientSession);
				int num = 0;
				while (num < mailboxList.Count && !this.SearchCommunicator.IsAborted)
				{
					this.currentMailboxId = num;
					if (mailboxList[num].MailboxInfo.IsRemote && this.EstimationPhase && this.SearchObject != null && SearchUtils.CollectKeywordStats(this.SearchObject, this.TotalMailboxes, discoveryMaxMailboxes))
					{
						this.InternalProcessRemoteMailbox(mailboxList[num]);
						SearchMailboxWorker.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "Search succeeded on estimation phase of remote Mailbox/Archive {0}.", mailboxList[num].MailboxInfo.MailboxGuid);
					}
					else
					{
						if (!this.InternalProcessMailbox(mailboxList[num], null))
						{
							SearchMailboxWorker.Tracer.TraceError<Guid>((long)this.GetHashCode(), "Search failed on Mailbox {0}.", mailboxList[num].MailboxInfo.MailboxGuid);
							this.SearchResult.Success = false;
							break;
						}
						this.unsearchableItemSet = null;
					}
					num++;
				}
			}
			catch (BackoffAbortedException)
			{
				SearchMailboxWorker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "SearchAbortException: the search on {0} is aborted by user", this.SourceUser.Id.DistinguishedName);
			}
			catch (OverBudgetException ex)
			{
				this.LastException = ex;
			}
			catch (AccessDeniedException ex2)
			{
				this.LastException = ex2;
			}
			catch (StorageTransientException ex3)
			{
				this.LastException = ex3;
			}
			catch (StoragePermanentException ex4)
			{
				this.LastException = ex4;
			}
			catch (ADTransientException ex5)
			{
				this.LastException = ex5;
			}
			catch (SearchMailboxException ex6)
			{
				this.LastException = ex6;
			}
			catch (SearchFolderTimeoutException ex7)
			{
				this.LastException = ex7;
			}
			if (this.LastException != null)
			{
				SearchMailboxWorker.Tracer.TraceDebug<string, Exception>((long)this.GetHashCode(), "SearchMailboxWorker: Mailbox {0} has exception {1}", this.SourceUser.Id.DistinguishedName, this.LastException);
				this.ReportException(this.LastException);
			}
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x000459AC File Offset: 0x00043BAC
		internal void ProcessMailbox(object state)
		{
			ExWatson.SendReportOnUnhandledException(delegate()
			{
				try
				{
					GrayException.MapAndReportGrayExceptions(delegate()
					{
						this.InternalProcessMailbox();
					});
				}
				catch (GrayException arg)
				{
					SearchMailboxWorker.Tracer.TraceError<string, GrayException>((long)this.GetHashCode(), "ProcessMailbox {0}: GrayException {1}", this.searchMailboxCriteria.SearchUserScope[this.workerId].Id.DistinguishedName, arg);
					this.SearchResult.Success = false;
				}
				finally
				{
					this.ReportCompletion();
				}
			}, delegate(object exception)
			{
				SearchMailboxWorker.Tracer.TraceError<string, object>((long)this.GetHashCode(), "ProcessMailbox {0}: Unhandled exception {1}", this.searchMailboxCriteria.SearchUserScope[this.workerId].Id.DistinguishedName, exception);
				return !(exception is GrayException);
			});
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x000459CC File Offset: 0x00043BCC
		internal void DeleteMailboxSearchResultFolder(MailboxSession targetMailbox)
		{
			if (this.searchMailboxCriteria.EstimateOnly || this.EstimationPhase || this.TargetFolderId == null)
			{
				return;
			}
			using (Folder folder = Folder.Bind(targetMailbox, this.TargetFolderId))
			{
				StoreId subFolderIdByName = folder.GetSubFolderIdByName(this.SubfolderName);
				if (subFolderIdByName != null)
				{
					SearchMailboxWorker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "DeleteMailboxSearchResultFolder: delete folder '{0}'", this.SubfolderName);
					targetMailbox.Delete(DeleteItemFlags.HardDelete, new StoreId[]
					{
						subFolderIdByName
					});
				}
			}
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x00045A60 File Offset: 0x00043C60
		internal static UniqueItemHash BuildUniqueItemHash(MailboxSession mailboxSession, object[] rowResult, StoreId sentItemsStoreId, out StoreObjectId itemObjectId)
		{
			itemObjectId = null;
			UniqueItemHash result = null;
			if (SearchMailboxWorker.PropertyExists(rowResult[0]) && SearchMailboxWorker.PropertyExists(rowResult[2]) && SearchMailboxWorker.PropertyExists(rowResult[4]) && SearchMailboxWorker.PropertyExists(rowResult[5]) && SearchMailboxWorker.PropertyExists(rowResult[6]))
			{
				StoreId storeId = (StoreId)rowResult[0];
				itemObjectId = StoreId.GetStoreObjectId(storeId);
				string internetMsgId = rowResult[4] as string;
				string text = rowResult[6] as string;
				StoreId storeId2 = rowResult[2] as StoreId;
				bool flag = false;
				if (sentItemsStoreId != null)
				{
					flag = storeId2.Equals(sentItemsStoreId);
				}
				if (!flag && !SearchMailboxWorker.PropertyExists(rowResult[3]) && SearchMailboxWorker.PropertyExists(rowResult[8]) && ObjectClass.IsMessage(rowResult[8] as string, false))
				{
					using (Item item = Item.Bind(mailboxSession, storeId))
					{
						if (item.Body != null)
						{
							rowResult[3] = item.Body.CalculateBodyTag();
							SearchMailboxWorker.Tracer.TraceDebug<string, object>((long)rowResult.GetHashCode(), "Calculating body tag for session {0} with internet message id {1}", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, rowResult[4]);
						}
					}
				}
				byte[] array = rowResult[3] as byte[];
				result = new UniqueItemHash(internetMsgId, text ?? string.Empty, (array != null) ? BodyTagInfo.FromByteArray(array) : null, flag);
			}
			return result;
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x00045BAC File Offset: 0x00043DAC
		internal static string GetInternetMessageId(object[] rowResult)
		{
			if (SearchMailboxWorker.PropertyExists(rowResult[4]))
			{
				return rowResult[4] as string;
			}
			return null;
		}

		// Token: 0x06000FD1 RID: 4049 RVA: 0x00045BC2 File Offset: 0x00043DC2
		private static bool PropertyExists(object property)
		{
			return property != null && !(property is PropertyError);
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00045BD5 File Offset: 0x00043DD5
		private void ResetProgresses(int mailboxCount)
		{
			this.mailboxProgresses = new double[mailboxCount];
			this.currentMailboxId = 0;
		}

		// Token: 0x06000FD3 RID: 4051 RVA: 0x00045BEA File Offset: 0x00043DEA
		private void UpdateProgress(double progress, double minstep)
		{
			if (progress >= this.MailboxProgress + minstep)
			{
				this.UpdateProgress(progress);
			}
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x00045C00 File Offset: 0x00043E00
		private void UpdateProgress(double progress)
		{
			this.MailboxProgress = progress;
			lock (this.SearchCommunicator)
			{
				this.SearchCommunicator.UpdateProgress(this);
			}
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00045C50 File Offset: 0x00043E50
		private double CalcProgress(int copiedItemCount, int totalItemCount, double startProgress, double maxProgress)
		{
			if (totalItemCount == 0)
			{
				return maxProgress;
			}
			double val = startProgress + (maxProgress - startProgress) * (double)copiedItemCount / (double)totalItemCount;
			return Math.Min(Math.Min(val, startProgress), maxProgress);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00045C80 File Offset: 0x00043E80
		private void ReportException(Exception e)
		{
			this.SearchResult.Success = false;
			lock (this.SearchCommunicator)
			{
				this.SearchCommunicator.ReportException(this.WorkerId, e);
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00045CD8 File Offset: 0x00043ED8
		private void HandleCopyFailure(Exception ex)
		{
			this.nonCriticalFails++;
			if (this.nonCriticalFails == 3 || ex.GetType() == typeof(MapiExceptionPartialCompletion) || ex.GetType() == typeof(PartialCompletionException))
			{
				StoragePermanentException e = new StoragePermanentException(Strings.CopyItemsFailed, ex);
				this.ReportException(e);
				return;
			}
			SearchMailboxException e2 = new SearchMailboxException(Strings.CopyItemsFailed, ex);
			this.ReportException(e2);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00045D51 File Offset: 0x00043F51
		internal void ReportActionException(Exception e)
		{
			this.nonCriticalFails++;
			if (this.nonCriticalFails < 3)
			{
				this.ReportException(e);
				return;
			}
			throw e;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00045D74 File Offset: 0x00043F74
		private void ReportCompletion()
		{
			lock (this.SearchCommunicator)
			{
				SearchMailboxWorker.Tracer.TraceDebug<string>((long)this.GetHashCode(), "SearchMailboxWorker finished on mailbox '{0}'", this.searchMailboxCriteria.SearchUserScope[this.workerId].Id.DistinguishedName);
				if ((this.searchMailboxCriteria.EstimateOnly || !this.EstimationPhase) && !this.searchCommunicator.IsAborted)
				{
					if (this.searchCommunicator.SuccessfulMailboxes != null && this.SearchResult.Success)
					{
						string item = this.SourceUser.Id.DomainUserName();
						if (!this.searchCommunicator.SuccessfulMailboxes.Contains(item))
						{
							this.searchCommunicator.SuccessfulMailboxes.Add(item);
						}
						this.searchCommunicator.UnsuccessfulMailboxes.Remove(item);
					}
					if (this.searchCommunicator.UnsuccessfulMailboxes != null && !this.SearchResult.Success)
					{
						string item2 = this.SourceUser.Id.DomainUserName();
						if (!this.searchCommunicator.UnsuccessfulMailboxes.Contains(item2))
						{
							this.searchCommunicator.UnsuccessfulMailboxes.Add(item2);
						}
					}
				}
				this.SearchCommunicator.ReportCompletion(this);
			}
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00045ED0 File Offset: 0x000440D0
		private StoreId CreateFolder(MailboxSession mailboxSession, StoreId parentId, string displayName, bool sourceBasedFolder)
		{
			StoreId result = null;
			using (Folder folder = Folder.Create(mailboxSession, parentId, StoreObjectType.Folder, displayName, CreateMode.OpenIfExists))
			{
				if (sourceBasedFolder)
				{
					folder[FolderSchema.OwaViewStateSortColumn] = "DeliveryTime";
				}
				FolderSaveResult folderSaveResult = folder.Save();
				if (folderSaveResult.OperationResult != OperationResult.Succeeded)
				{
					SearchMailboxWorker.Tracer.TraceError<string, FolderSaveResult>((long)this.GetHashCode(), "Folder.Save operation failed on mailbox {0} with operation result {1} ", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, folderSaveResult);
					throw folderSaveResult.ToException(Strings.CreateFolderFailed(displayName));
				}
				folder.Load();
				result = folder.Id;
			}
			return result;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00045F6C File Offset: 0x0004416C
		private StoreId CreateTargetFolder(FolderNode folderNode)
		{
			Stack<FolderNode> stack = new Stack<FolderNode>();
			FolderNode folderNode2 = folderNode;
			while (folderNode2.TargetFolderId == null)
			{
				stack.Push(folderNode2);
				folderNode2 = folderNode2.Parent;
			}
			FolderNode folderNode3 = folderNode2;
			while (stack.Count > 0)
			{
				bool sourceBasedFolder = folderNode.SourceFolderId != null;
				folderNode2 = stack.Pop();
				folderNode2.TargetFolderId = this.CreateFolder(this.TargetMailbox, folderNode3.TargetFolderId, folderNode2.DisplayName, sourceBasedFolder);
				folderNode3 = folderNode2;
			}
			return folderNode.TargetFolderId;
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00045FE4 File Offset: 0x000441E4
		private SearchFolder CreateSearchFolder(MailboxSession mailboxSession)
		{
			string displayName = "SearchMailboxFolder" + Guid.NewGuid().ToString();
			SearchFolder searchFolder = SearchFolder.Create(mailboxSession, mailboxSession.GetDefaultFolderId(DefaultFolderType.SearchFolders), displayName, CreateMode.OpenIfExists);
			searchFolder.Save();
			searchFolder.Load();
			return searchFolder;
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00046030 File Offset: 0x00044230
		private void PopulateSearchFolder(MailboxSession mailboxSession, SearchFolder searchFolder, QueryFilter queryFilter, bool deepTraversal, bool useCiForComplexQueries, bool estimateCountOnly, params StoreId[] folderScope)
		{
			QueryFilter searchQuery = new CountFilter(10000U, queryFilter);
			SearchFolderCriteria searchFolderCriteria = new SearchFolderCriteria(searchQuery, folderScope);
			searchFolderCriteria.DeepTraversal = deepTraversal;
			searchFolderCriteria.UseCiForComplexQueries = useCiForComplexQueries;
			searchFolderCriteria.FailNonContentIndexedSearch = true;
			if (estimateCountOnly)
			{
				searchFolderCriteria.EstimateCountOnly = true;
			}
			else
			{
				searchFolderCriteria.StatisticsOnly = this.EstimationPhase;
			}
			IAsyncResult asyncResult = searchFolder.BeginApplyOneTimeSearch(searchFolderCriteria, null, null);
			bool flag = asyncResult.AsyncWaitHandle.WaitOne(180000, false);
			if (flag)
			{
				searchFolder.EndApplyOneTimeSearch(asyncResult);
				if (!this.checkCI)
				{
					this.CheckContentIndexingIsRunningFlag(mailboxSession, searchFolder);
					this.checkCI = true;
				}
				searchFolder.Save();
				searchFolder.Load();
				return;
			}
			throw new SearchFolderTimeoutException(this.searchMailboxCriteria.SearchUserScope[this.workerId].Id.DistinguishedName);
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x000460F0 File Offset: 0x000442F0
		private object[][] FilterDuplicates(MailboxSession mailboxSession, object[][] rowResults, HashSet<StoreId> bccItemIds, StoreId sentItemsStoreId)
		{
			IList<object[]> list = new List<object[]>();
			int i = 0;
			while (i < rowResults.Length)
			{
				StoreId item = (StoreId)rowResults[i][0];
				StoreObjectId storeObjectId = null;
				UniqueItemHash uniqueItemHash = SearchMailboxWorker.BuildUniqueItemHash(mailboxSession, rowResults[i], sentItemsStoreId, out storeObjectId);
				if (uniqueItemHash != null)
				{
					lock (this.SearchCommunicator)
					{
						if (!this.SearchCommunicator.ProcessedMessages.Contains(uniqueItemHash) || bccItemIds.Contains(item))
						{
							this.SearchCommunicator.ProcessedMessages.Add(uniqueItemHash);
							list.Add(rowResults[i]);
							this.HasPendingHashSetVerification = true;
							SearchMailboxWorker.Tracer.TraceDebug<string, object, bool>((long)this.GetHashCode(), "Message added to row results for session {0} with internet message id {1} and bcc {2}", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, rowResults[i][4], bccItemIds.Contains(item));
						}
						goto IL_165;
					}
					goto IL_C6;
				}
				goto IL_C6;
				IL_165:
				i++;
				continue;
				IL_C6:
				string internetMessageId = SearchMailboxWorker.GetInternetMessageId(rowResults[i]);
				if (internetMessageId != null)
				{
					lock (this.SearchCommunicator)
					{
						if (!this.SearchCommunicator.ProcessedMessageIds.Contains(internetMessageId))
						{
							this.SearchCommunicator.ProcessedMessageIds.Add(internetMessageId);
							list.Add(rowResults[i]);
							this.HasPendingHashSetVerification = true;
						}
						goto IL_165;
					}
				}
				list.Add(rowResults[i]);
				SearchMailboxWorker.Tracer.TraceDebug<string, object, object>((long)this.GetHashCode(), "No item hash and no internet message id for session {0} for item id {1} of class {2}", mailboxSession.MailboxOwner.MailboxInfo.DisplayName, rowResults[i][0], rowResults[i][8]);
				goto IL_165;
			}
			return list.ToArray<object[]>();
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x00046294 File Offset: 0x00044494
		private void PopulateFolderNodeMap(MailboxSession sourceMailbox, Dictionary<StoreId, FolderNode> folderNodeMap, StoreId folderId)
		{
			Stack<FolderNode> stack = new Stack<FolderNode>();
			while (!folderNodeMap.ContainsKey(folderId))
			{
				using (Folder folder = Folder.Bind(sourceMailbox, folderId, new PropertyDefinition[]
				{
					StoreObjectSchema.ParentItemId,
					FolderSchema.DisplayName,
					StoreObjectSchema.IsSoftDeleted
				}))
				{
					bool valueOrDefault = folder.GetValueOrDefault<bool>(StoreObjectSchema.IsSoftDeleted);
					stack.Push(new FolderNode(folderId, null, folder.DisplayName, valueOrDefault, null));
					if (folderId.Equals(folder.ParentId))
					{
						throw new CorruptDataException(Strings.CorruptedFolder(sourceMailbox.MailboxOwner.MailboxInfo.DisplayName));
					}
					folderId = folder.ParentId;
				}
			}
			FolderNode parent = folderNodeMap[folderId];
			while (stack.Count > 0)
			{
				FolderNode folderNode = stack.Pop();
				folderNode.Parent = parent;
				folderNodeMap.Add(folderNode.SourceFolderId, folderNode);
				parent = folderNode;
			}
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x00046388 File Offset: 0x00044588
		private bool CopyFetchedItemsInBuffer(MailboxSession sourceMailbox, Dictionary<StoreId, FolderNode> folderNodeMap, StoreId sourceFolderId, int fetchedItemCount)
		{
			this.PopulateFolderNodeMap(sourceMailbox, folderNodeMap, sourceFolderId);
			FolderNode folderNode = folderNodeMap[sourceFolderId];
			bool result;
			using (Folder folder = Folder.Bind(sourceMailbox, sourceFolderId))
			{
				this.sourceStoreThrotter.BackOffFromStore(sourceMailbox);
				this.targetStoreThrotter.BackOffFromStore(this.TargetMailbox);
				StoreId storeId = null;
				if (this.SearchCommunicator.IsAborted)
				{
					result = false;
				}
				else
				{
					if (this.ExcludeDuplicateMessages)
					{
						folderNode = this.CreateAndGetTargetSubFolderNode(folderNodeMap);
					}
					else if (folderNode.TargetFolderId == null)
					{
						this.CreateTargetFolder(folderNode);
					}
					storeId = folderNode.TargetFolderId;
					int num = 0;
					ByteQuantifiedSize zero = ByteQuantifiedSize.Zero;
					bool flag = false;
					try
					{
						if (!this.ExcludeDuplicateMessages)
						{
							SearchUtils.GetFolderItemsCountAndSize(this.TargetMailbox, storeId, out num, out zero);
							SearchMailboxWorker.Tracer.TraceDebug<int, ByteQuantifiedSize>((long)this.GetHashCode(), "CopyFetchedItemsInBuffer -> Before copy, folder items count={0}, folder items size={1}", num, zero);
						}
						List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
						long num2 = 0L;
						int num3 = 0;
						for (int i = 0; i < fetchedItemCount; i++)
						{
							num2 += (long)this.copyItemSizeBuffer[i];
							if (num2 > this.batchWriteSize || i + 1 == fetchedItemCount)
							{
								list.Add(new KeyValuePair<int, int>(num3, i - num3 + 1));
								num2 = 0L;
								num3 = i + 1;
							}
						}
						int num4 = 0;
						foreach (KeyValuePair<int, int> keyValuePair in list)
						{
							StoreId[] array = new StoreId[keyValuePair.Value];
							Array.Copy(this.copyItemBuffer, keyValuePair.Key, array, 0, keyValuePair.Value);
							GroupOperationResult groupOperationResult = folder.CopyItems(this.TargetMailbox, storeId, false, array);
							if (groupOperationResult.OperationResult != OperationResult.Succeeded)
							{
								SearchMailboxWorker.Tracer.TraceError<OperationResult, LocalizedException>((long)this.GetHashCode(), "CopyItems operation failed with operation result: {0} and exception: {1}", groupOperationResult.OperationResult, groupOperationResult.Exception);
								num4++;
								this.HandleCopyFailure(groupOperationResult.Exception);
							}
							else
							{
								Thread.Sleep(this.delayBatchWriteInterval);
							}
						}
						flag = (num4 == 0);
					}
					catch (ArgumentException ex)
					{
						SearchMailboxWorker.Tracer.TraceError<ArgumentException>((long)this.GetHashCode(), "CopyItems operation failed due to item validation failed, exception: {0}", ex);
						this.HandleCopyFailure(ex);
					}
					catch (MapiExceptionPartialCompletion mapiExceptionPartialCompletion)
					{
						SearchMailboxWorker.Tracer.TraceError<MapiExceptionPartialCompletion>((long)this.GetHashCode(), "CopyItems operation failed due to insufficient space in target mailbox, exception: {0}", mapiExceptionPartialCompletion);
						this.HandleCopyFailure(mapiExceptionPartialCompletion);
						return false;
					}
					catch (PartialCompletionException ex2)
					{
						SearchMailboxWorker.Tracer.TraceError<PartialCompletionException>((long)this.GetHashCode(), "CopyItems operation failed due to insufficient space in target mailbox, exception: {0}", ex2);
						this.HandleCopyFailure(ex2);
						return false;
					}
					finally
					{
						if (!this.ExcludeDuplicateMessages)
						{
							int num5 = 0;
							ByteQuantifiedSize zero2 = ByteQuantifiedSize.Zero;
							SearchUtils.GetFolderItemsCountAndSize(this.TargetMailbox, storeId, out num5, out zero2);
							SearchMailboxWorker.Tracer.TraceDebug<int, ByteQuantifiedSize>((long)this.GetHashCode(), "CopyFetchedItemsInBuffer -> After copy, folder items count={0}, folder items size={1}", num5, zero2);
							int num6 = num5 - num;
							this.SearchResult.ResultItemsCount += num6;
							this.SearchResult.ResultItemsSize += zero2 - zero;
							MailboxDataProvider.IncrementDiscoveryCopyItemsRatePerfCounter(num6);
							SearchMailboxWorker.Tracer.TraceDebug<int, string>((long)this.GetHashCode(), "SearchMailboxWoker progressed with {0} message copies on mailbox {1}", num6, this.searchMailboxCriteria.SearchUserScope[this.workerId].Id.DistinguishedName);
						}
						else
						{
							this.SearchResult.ResultItemsCount += fetchedItemCount;
							MailboxDataProvider.IncrementDiscoveryCopyItemsRatePerfCounter(fetchedItemCount);
							SearchMailboxWorker.Tracer.TraceDebug<string, int, string>((long)this.GetHashCode(), "SearchMailboxWorker {0} progressed with {1} fetched message copies on mailbox {2}", flag ? "successfully" : "unsuccessfully", fetchedItemCount, this.searchMailboxCriteria.SearchUserScope[this.workerId].Id.DistinguishedName);
						}
					}
					result = flag;
				}
			}
			return result;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x000467C4 File Offset: 0x000449C4
		private FolderNode CreateAndGetTargetSubFolderNode(Dictionary<StoreId, FolderNode> folderNodeMap)
		{
			FolderNode result;
			try
			{
				FolderNode value = folderNodeMap.Single((KeyValuePair<StoreId, FolderNode> f) => f.Value.DisplayName == this.SubfolderName).Value;
				if (value.TargetFolderId == null)
				{
					this.CreateTargetFolder(value);
				}
				this.TargetSubFolderId = value.TargetFolderId;
				result = value;
			}
			catch (InvalidOperationException innerException)
			{
				throw new SearchMailboxException(Strings.TargetFolderNotFound(this.SubfolderName), innerException);
			}
			return result;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x00046838 File Offset: 0x00044A38
		private static string QuoteValueIfRequired(string value)
		{
			if (!string.IsNullOrEmpty(value) && value.IndexOfAny(new char[]
			{
				',',
				'"'
			}) != -1)
			{
				value = value.Replace("\"", "\"\"");
				return string.Format("\"{0}\"", value);
			}
			return value;
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x00046888 File Offset: 0x00044A88
		private void ReportLogging(MailboxSession sourceMailbox, Dictionary<StoreId, FolderNode> folderNodeMap, StoreId folderId, object[][] copyItemProps, int itemCount)
		{
			this.PopulateFolderNodeMap(sourceMailbox, folderNodeMap, folderId);
			LocalizedString[] array = new LocalizedString[itemCount];
			for (int i = 0; i < itemCount; i++)
			{
				VersionedId versionedId = (VersionedId)copyItemProps[i][0];
				string value = SearchMailboxWorker.PropertyExists(copyItemProps[i][9]) ? ((string)copyItemProps[i][9]) : string.Empty;
				bool? flag = null;
				if (SearchMailboxWorker.PropertyExists(copyItemProps[i][10]))
				{
					flag = new bool?((bool)copyItemProps[i][10]);
				}
				ExDateTime? exDateTime = null;
				if (SearchMailboxWorker.PropertyExists(copyItemProps[i][11]))
				{
					exDateTime = new ExDateTime?((ExDateTime)copyItemProps[i][11]);
				}
				ExDateTime? exDateTime2 = null;
				if (SearchMailboxWorker.PropertyExists(copyItemProps[i][12]))
				{
					exDateTime2 = new ExDateTime?((ExDateTime)copyItemProps[i][12]);
				}
				Participant participant = SearchMailboxWorker.PropertyExists(copyItemProps[i][13]) ? ((Participant)copyItemProps[i][13]) : null;
				string text = SearchMailboxWorker.PropertyExists(copyItemProps[i][14]) ? ((string)copyItemProps[i][14]) : null;
				string value2 = (participant != null) ? participant.DisplayName : string.Empty;
				string text2 = (participant != null) ? participant.EmailAddress : string.Empty;
				string text3 = SearchMailboxWorker.PropertyExists(copyItemProps[i][15]) ? copyItemProps[i][15].ToString() : string.Empty;
				string text4 = SearchMailboxWorker.PropertyExists(copyItemProps[i][16]) ? copyItemProps[i][16].ToString() : string.Empty;
				ExDateTime? exDateTime3 = null;
				if (SearchMailboxWorker.PropertyExists(copyItemProps[i][17]))
				{
					exDateTime3 = new ExDateTime?((ExDateTime)copyItemProps[i][17]);
				}
				ExDateTime? exDateTime4 = null;
				if (SearchMailboxWorker.PropertyExists(copyItemProps[i][18]))
				{
					exDateTime4 = new ExDateTime?((ExDateTime)copyItemProps[i][18]);
				}
				string text5 = SearchMailboxWorker.PropertyExists(copyItemProps[i][20]) ? ((string)copyItemProps[i][20]) : string.Empty;
				string text6 = SearchMailboxWorker.PropertyExists(copyItemProps[i][21]) ? Enum.Format(typeof(FlagStatus), copyItemProps[i][21], "g") : string.Empty;
				string value3 = SearchMailboxWorker.PropertyExists(copyItemProps[i][22]) ? ((string)copyItemProps[i][22]) : string.Empty;
				bool? flag2 = null;
				if (SearchMailboxWorker.PropertyExists(copyItemProps[i][29]))
				{
					flag2 = new bool?((bool)copyItemProps[i][29]);
				}
				string text7 = SearchMailboxWorker.PropertyExists(copyItemProps[i][19]) ? Enum.Format(typeof(ItemColor), copyItemProps[i][19], "g") : string.Empty;
				string text8 = SearchMailboxWorker.PropertyExists(copyItemProps[i][23]) ? Enum.Format(typeof(TaskStatus), copyItemProps[i][23], "g") : string.Empty;
				ExDateTime? exDateTime5 = null;
				if (SearchMailboxWorker.PropertyExists(copyItemProps[i][24]))
				{
					exDateTime5 = new ExDateTime?((ExDateTime)copyItemProps[i][24]);
				}
				ExDateTime? exDateTime6 = null;
				if (SearchMailboxWorker.PropertyExists(copyItemProps[i][25]))
				{
					exDateTime6 = new ExDateTime?((ExDateTime)copyItemProps[i][25]);
				}
				bool? flag3 = null;
				if (SearchMailboxWorker.PropertyExists(copyItemProps[i][26]))
				{
					flag3 = new bool?((bool)copyItemProps[i][26]);
				}
				string text9 = SearchMailboxWorker.PropertyExists(copyItemProps[i][27]) ? ((double)copyItemProps[i][27]).ToString("###P") : string.Empty;
				bool? flag4 = null;
				if (SearchMailboxWorker.PropertyExists(copyItemProps[i][28]))
				{
					flag4 = new bool?((bool)copyItemProps[i][28]);
				}
				string value4 = SearchMailboxWorker.PropertyExists(copyItemProps[i][30]) ? string.Join(",", (string[])copyItemProps[i][30]) : string.Empty;
				array[i] = new LocalizedString(string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24}", new object[]
				{
					SearchMailboxWorker.QuoteValueIfRequired(sourceMailbox.MailboxOwner.MailboxInfo.DisplayName),
					SearchMailboxWorker.QuoteValueIfRequired(folderNodeMap[folderId].DisplayName),
					SearchMailboxWorker.QuoteValueIfRequired(value),
					flag,
					exDateTime,
					exDateTime2,
					SearchMailboxWorker.QuoteValueIfRequired(value2),
					SearchMailboxWorker.QuoteValueIfRequired(text ?? text2),
					text3,
					text4,
					versionedId.ObjectId,
					exDateTime3,
					exDateTime4,
					text5,
					text6,
					SearchMailboxWorker.QuoteValueIfRequired(value3),
					flag2,
					text7,
					text8,
					exDateTime5,
					exDateTime6,
					flag3,
					text9,
					flag4,
					SearchMailboxWorker.QuoteValueIfRequired(value4)
				}));
			}
			lock (this.searchCommunicator)
			{
				StreamLogItem.LogItem logItem = new StreamLogItem.LogItem(this.WorkerId, array);
				this.searchCommunicator.ReportLogs(logItem);
			}
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x00046E04 File Offset: 0x00045004
		private int CopyFolderItems(Folder folder, MailboxSession sourceMailbox, Dictionary<StoreId, FolderNode> folderNodeMap, double maxProgress)
		{
			double mailboxProgress = this.MailboxProgress;
			int resultItemsCount = this.SearchResult.ResultItemsCount;
			StoreId defaultFolderId = sourceMailbox.GetDefaultFolderId(DefaultFolderType.SentItems);
			int num = 0;
			using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, new SortBy[]
			{
				new SortBy(StoreObjectSchema.ParentEntryId, SortOrder.Ascending)
			}, this.IsFullLogging ? SearchMailboxWorker.ItemPreloadPropertiesWithLogging : SearchMailboxWorker.ItemPreloadProperties))
			{
				StoreId storeId = null;
				int num2 = 0;
				HashSet<StoreId> bccItemIds = new HashSet<StoreId>();
				if (this.ExcludeDuplicateMessages)
				{
					bccItemIds = SearchUtils.GetBccItemIds(folder);
				}
				bool flag = false;
				while (!this.SearchCommunicator.IsAborted)
				{
					this.sourceStoreThrotter.BackOffFromStore(sourceMailbox);
					object[][] array = queryResult.GetRows(this.copyItemBuffer.Length);
					if (array == null || array.Length <= 0)
					{
						break;
					}
					if (this.IsFullLogging)
					{
						StoreId storeId2 = null;
						int num3 = 0;
						for (int i = 0; i < array.Length; i++)
						{
							if (SearchMailboxWorker.PropertyExists(array[i][0]) && SearchMailboxWorker.PropertyExists(array[i][2]))
							{
								StoreId storeId3 = (StoreId)array[i][0];
								StoreId storeId4 = (StoreId)array[i][2];
								if (storeId2 == null)
								{
									storeId2 = storeId4;
								}
								if (num3 >= this.copyItemProps.Length || !storeId2.Equals(storeId4))
								{
									this.ReportLogging(sourceMailbox, folderNodeMap, storeId2, this.copyItemProps, num3);
									num3 = 0;
									storeId2 = storeId4;
								}
								this.copyItemProps[num3++] = array[i];
							}
						}
						if (num3 > 0)
						{
							this.ReportLogging(sourceMailbox, folderNodeMap, storeId2, this.copyItemProps, num3);
						}
					}
					if (this.ExcludeDuplicateMessages)
					{
						array = this.FilterDuplicates(sourceMailbox, array, bccItemIds, defaultFolderId);
					}
					for (int j = 0; j < array.Length; j++)
					{
						if (SearchMailboxWorker.PropertyExists(array[j][0]) && SearchMailboxWorker.PropertyExists(array[j][2]))
						{
							StoreId storeId5 = (StoreId)array[j][0];
							StoreId storeId6 = (StoreId)array[j][2];
							if (storeId == null)
							{
								storeId = storeId6;
							}
							if (num2 >= this.copyItemBuffer.Length || !storeId.Equals(storeId6))
							{
								flag = this.CopyFetchedItemsInBuffer(sourceMailbox, folderNodeMap, storeId, num2);
								num2 = 0;
								storeId = storeId6;
								if (this.SearchCommunicator.IsAborted || !flag)
								{
									break;
								}
							}
							this.copyItemBuffer[num2] = storeId5;
							if (SearchMailboxWorker.PropertyExists(array[j][1]))
							{
								this.copyItemSizeBuffer[num2] = (int)array[j][1];
							}
							num2++;
							StoreObjectId storeObjectId = StoreId.GetStoreObjectId(storeId5);
							if (this.unsearchableItemSet != null && storeObjectId != null && this.unsearchableItemSet.Contains(storeObjectId))
							{
								this.unsearchableItemSet.Remove(storeObjectId);
							}
						}
						else
						{
							SearchMailboxWorker.Tracer.TraceDebug((long)this.GetHashCode(), "Item is skipped because the itemId or ParentItemId is unavailable");
						}
						int num4 = this.SearchResult.ResultItemsCount - resultItemsCount;
						double progress = this.CalcProgress(num4, Math.Max(queryResult.EstimatedRowCount, num4), mailboxProgress, maxProgress);
						this.UpdateProgress(progress, 10.0);
					}
					num += array.Length;
				}
				if (!this.SearchCommunicator.IsAborted)
				{
					if (num2 > 0)
					{
						flag = this.CopyFetchedItemsInBuffer(sourceMailbox, folderNodeMap, storeId, num2);
					}
					if (this.unsearchableItemSet != null && this.unsearchableItemSet.Count > 0)
					{
						int num5 = this.SearchResult.ResultItemsCount - resultItemsCount;
						double progress2 = this.CalcProgress(num5, Math.Max(queryResult.EstimatedRowCount, num5), mailboxProgress, maxProgress);
						this.UpdateProgress(progress2, 10.0);
					}
					else
					{
						this.UpdateProgress(maxProgress);
					}
				}
				this.HasPendingHashSetVerification = !flag;
			}
			return num;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000471B8 File Offset: 0x000453B8
		private void EstimateFolderItems(MailboxSession sourceMailbox, Folder folder, double maxProgress, KeywordHit searchResult)
		{
			double mailboxProgress = this.MailboxProgress;
			int resultItemsCount = this.SearchResult.ResultItemsCount;
			sourceMailbox.GetDefaultFolderId(DefaultFolderType.SentItems);
			using (QueryResult queryResult = folder.ItemQuery(ItemQueryType.None, null, null, SearchMailboxWorker.ItemPreloadProperties))
			{
				while (this.searchCommunicator == null || !this.SearchCommunicator.IsAborted)
				{
					this.sourceStoreThrotter.BackOffFromStore(sourceMailbox);
					object[][] rows = queryResult.GetRows(this.copyItemBuffer.Length);
					if (rows == null || rows.Length <= 0)
					{
						break;
					}
					for (int i = 0; i < rows.Length; i++)
					{
						StoreObjectId storeObjectId = StoreId.GetStoreObjectId((StoreId)rows[i][0]);
						if (this.unsearchableItemSet != null && storeObjectId != null && this.unsearchableItemSet.Contains(storeObjectId))
						{
							this.unsearchableItemSet.Remove(storeObjectId);
						}
						if (SearchMailboxWorker.PropertyExists(rows[i][(int)(checked((IntPtr)1L))]))
						{
							searchResult.Size += (int)rows[i][1];
						}
						searchResult.Count++;
					}
					if (this.SearchCommunicator != null)
					{
						int num = this.searchResult.ResultItemsCount - resultItemsCount;
						double progress = this.CalcProgress(num, Math.Max(folder.ItemCount, num), mailboxProgress, maxProgress);
						this.UpdateProgress(progress, 10.0);
					}
				}
			}
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00047330 File Offset: 0x00045530
		private void EstimateFolderItemsInStatisticsOnlySearch(Folder folder, double maxProgress, KeywordHit keywordhit)
		{
			double mailboxProgress = this.MailboxProgress;
			int resultItemsCount = this.SearchResult.ResultItemsCount;
			if (this.EstimationPhase)
			{
				keywordhit.Count = folder.ItemCount;
				folder.Load(new PropertyDefinition[]
				{
					FolderSchema.ExtendedSize
				});
				object obj = folder.TryGetProperty(FolderSchema.ExtendedSize);
				if (obj is long && (long)obj > 0L)
				{
					keywordhit.Size = new ByteQuantifiedSize((ulong)((long)obj));
				}
				if (this.SearchCommunicator != null)
				{
					int num = this.searchResult.ResultItemsCount - resultItemsCount;
					double progress = this.CalcProgress(num, Math.Max(folder.ItemCount, num), mailboxProgress, maxProgress);
					this.UpdateProgress(progress, 10.0);
				}
			}
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0004743C File Offset: 0x0004563C
		private string[] GetUniqueFolderName(MailboxSession mailboxStore, StoreId folderId, string[] suggestedNames)
		{
			List<string> subFolderNames = new List<string>();
			using (Folder folder = Folder.Bind(mailboxStore, folderId))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.None, null, null, new PropertyDefinition[]
				{
					FolderSchema.DisplayName
				}))
				{
					ElcMailboxHelper.ForeachQueryResult(queryResult, delegate(object[] rowProps, ref bool breakLoop)
					{
						if (SearchMailboxWorker.PropertyExists(rowProps[0]))
						{
							subFolderNames.Add((string)rowProps[0]);
						}
					});
				}
			}
			string[] array = new string[suggestedNames.Length];
			for (int i = 0; i < suggestedNames.Length; i++)
			{
				string folderName = suggestedNames[i];
				List<string> list = (from x in subFolderNames
				where x.StartsWith(folderName, StringComparison.OrdinalIgnoreCase)
				select x).ToList<string>();
				for (int j = 0; j < list.Count + 1; j++)
				{
					if (list.Find((string x) => x.Equals(folderName, StringComparison.OrdinalIgnoreCase)) == null)
					{
						break;
					}
					folderName = string.Format("{0}-{1}", suggestedNames[i], j + 1);
				}
				array[i] = folderName;
			}
			return array;
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x000475D4 File Offset: 0x000457D4
		private void AddToFolderFilterSet(MailboxSession mailboxStore, StoreId folderId, HashSet<StoreObjectId> folderFilterSet)
		{
			using (Folder folder = Folder.Bind(mailboxStore, folderId))
			{
				using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, null, null, new PropertyDefinition[]
				{
					FolderSchema.Id
				}))
				{
					ElcMailboxHelper.ForeachQueryResult(queryResult, delegate(object[] rowProps, ref bool breakLoop)
					{
						if (SearchMailboxWorker.PropertyExists(rowProps[0]))
						{
							StoreObjectId storeObjectId2 = StoreId.GetStoreObjectId((StoreId)rowProps[0]);
							if (!folderFilterSet.Contains(storeObjectId2))
							{
								folderFilterSet.Add(storeObjectId2);
							}
						}
					});
				}
			}
			StoreObjectId storeObjectId = StoreId.GetStoreObjectId(folderId);
			if (!folderFilterSet.Contains(storeObjectId))
			{
				folderFilterSet.Add(storeObjectId);
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000476A8 File Offset: 0x000458A8
		private void GroupCopyUnsearchableItems(MailboxSession sourceMailbox, List<object[]> bufferedItemProperties, Dictionary<StoreId, FolderNode> folderNodeMap)
		{
			StoreId defaultFolderId = sourceMailbox.GetDefaultFolderId(DefaultFolderType.SentItems);
			IEnumerable<IGrouping<StoreId, object[]>> enumerable = from x in bufferedItemProperties
			group x by (StoreId)x[2];
			bool flag = false;
			foreach (IGrouping<StoreId, object[]> grouping in enumerable)
			{
				this.copyItemBuffer = (from x in grouping
				select (StoreId)x[0]).ToArray<StoreId>();
				if (this.IsFullLogging)
				{
					this.ReportLogging(sourceMailbox, folderNodeMap, grouping.Key, grouping.ToArray<object[]>(), this.copyItemBuffer.Length);
				}
				if (this.ExcludeDuplicateMessages)
				{
					this.copyItemBuffer = (from x in this.FilterDuplicates(sourceMailbox, grouping.ToArray<object[]>(), SearchUtils.GetBccItemIds(sourceMailbox, grouping.Key), defaultFolderId)
					select (StoreId)x[0]).ToArray<StoreId>();
				}
				flag = this.CopyFetchedItemsInBuffer(sourceMailbox, folderNodeMap, grouping.Key, this.copyItemBuffer.Length);
				if (!flag)
				{
					break;
				}
			}
			this.HasPendingHashSetVerification = !flag;
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x000477E8 File Offset: 0x000459E8
		private void CollectUnsearchableItems(MailboxSession sourceMailbox, StoreId[] folderScope)
		{
			HashSet<StoreObjectId> hashSet = null;
			if (folderScope != null)
			{
				hashSet = new HashSet<StoreObjectId>();
				foreach (StoreId folderId in folderScope)
				{
					this.AddToFolderFilterSet(sourceMailbox, folderId, hashSet);
				}
			}
			this.unsearchableItemSet = new HashSet<StoreObjectId>();
			try
			{
				using (Stream unsearchableItems = sourceMailbox.GetUnsearchableItems())
				{
					using (BinaryReader binaryReader = new BinaryReader(unsearchableItems))
					{
						try
						{
							for (;;)
							{
								byte[] entryId = UnsearchableItemsStream.GetEntryId(binaryReader);
								try
								{
									StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(entryId, StoreObjectType.Unknown);
									StoreObjectId parentIdFromMessageId = IdConverter.GetParentIdFromMessageId(storeObjectId);
									if (hashSet == null || hashSet.Contains(parentIdFromMessageId))
									{
										this.unsearchableItemSet.Add(storeObjectId);
									}
								}
								catch (CorruptDataException arg)
								{
									SearchMailboxWorker.Tracer.TraceDebug<CorruptDataException>((long)this.GetHashCode(), "Item is skipped because of exception: {0}", arg);
								}
							}
						}
						catch (EndOfStreamException)
						{
							SearchMailboxWorker.Tracer.TraceDebug((long)this.GetHashCode(), "SearchMailboxWoker::CollectUnsearchableItems hit expected end of stream exception");
						}
					}
				}
			}
			catch (MapiExceptionVersion arg2)
			{
				SearchMailboxWorker.Tracer.TraceError<string, MapiExceptionVersion>((long)this.GetHashCode(), "CollectUnsearchableItems on {0}, MapiExceptionVersion: {1}", sourceMailbox.MailboxOwner.MailboxInfo.DisplayName, arg2);
			}
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x00047934 File Offset: 0x00045B34
		private void CopyUnsearchableItems(MailboxSession sourceMailbox, Dictionary<StoreId, FolderNode> folderNodeMap)
		{
			List<object[]> list = new List<object[]>();
			double num = 100.0;
			double mailboxProgress = this.MailboxProgress;
			int resultItemsCount = this.SearchResult.ResultItemsCount;
			PropertyDefinition[] array = this.IsFullLogging ? SearchMailboxWorker.ItemPreloadPropertiesWithLogging : SearchMailboxWorker.ItemPreloadProperties;
			foreach (StoreObjectId storeObjectId in this.unsearchableItemSet)
			{
				try
				{
					this.sourceStoreThrotter.BackOffFromStore(sourceMailbox);
					using (Item item = Item.Bind(sourceMailbox, storeObjectId, array))
					{
						list.Add(item.GetProperties(array));
					}
				}
				catch (ObjectNotFoundException arg)
				{
					SearchMailboxWorker.Tracer.TraceDebug<StoreObjectId, ObjectNotFoundException>((long)this.GetHashCode(), "Unable to bind to failed item {0} because of exception {1}", storeObjectId, arg);
				}
				if (list.Count >= ResponseThrottler.MaxBulkSize)
				{
					this.GroupCopyUnsearchableItems(sourceMailbox, list, folderNodeMap);
					list.Clear();
					double progress = this.CalcProgress(this.SearchResult.ResultItemsCount - resultItemsCount, this.unsearchableItemSet.Count, mailboxProgress, num);
					this.UpdateProgress(progress, 10.0);
				}
				if (this.SearchCommunicator.IsAborted)
				{
					break;
				}
			}
			if (!this.SearchCommunicator.IsAborted)
			{
				if (list.Count > 0)
				{
					this.GroupCopyUnsearchableItems(sourceMailbox, list, folderNodeMap);
				}
				this.UpdateProgress(num);
			}
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x00047AB0 File Offset: 0x00045CB0
		private void EstimateUnsearchableItems(MailboxSession sourceMailbox, KeywordHit searchResult)
		{
			double num = 100.0;
			double mailboxProgress = this.MailboxProgress;
			int count = searchResult.Count;
			foreach (StoreObjectId storeObjectId in this.unsearchableItemSet)
			{
				if (this.SearchCommunicator != null)
				{
					if (this.SearchCommunicator.IsAborted)
					{
						break;
					}
				}
				try
				{
					using (Item item = Item.Bind(sourceMailbox, storeObjectId, SearchMailboxWorker.ItemPreloadProperties))
					{
						searchResult.Count++;
						searchResult.Size += (int)item.Size();
					}
				}
				catch (ObjectNotFoundException arg)
				{
					SearchMailboxWorker.Tracer.TraceDebug<StoreObjectId, ObjectNotFoundException>((long)this.GetHashCode(), "Unable to bind to failed item {0} because of exception {1}", storeObjectId, arg);
				}
				if (this.SearchCommunicator != null)
				{
					double progress = this.CalcProgress(searchResult.Count - count, this.unsearchableItemSet.Count, mailboxProgress, num);
					this.UpdateProgress(progress, 10.0);
				}
			}
			if (this.SearchCommunicator != null && !this.SearchCommunicator.IsAborted)
			{
				this.UpdateProgress(num);
			}
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x00047C00 File Offset: 0x00045E00
		private bool IsStatisticsOnly(MailboxSession sourceMailbox)
		{
			bool flag = true;
			if (sourceMailbox.MailboxOwner.MailboxInfo.GetDatabaseGuid() != Guid.Empty)
			{
				ActiveManager activeManagerInstance = ActiveManager.GetActiveManagerInstance();
				DatabaseLocationInfo serverForDatabase = activeManagerInstance.GetServerForDatabase(sourceMailbox.MailboxOwner.MailboxInfo.GetDatabaseGuid());
				int serverVersion = serverForDatabase.ServerVersion;
				flag = (serverVersion >= Server.E14SP1MinVersion);
			}
			return flag && this.EstimationPhase && this.SearchFilter != null;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x00047C80 File Offset: 0x00045E80
		private void EstimateSearchResults(MailboxSession sourceMailbox, Folder[] folders, KeywordHit result)
		{
			double mailboxProgress = this.MailboxProgress;
			int totalItemCount = folders.Aggregate(0, (int s, Folder f) => s + f.ItemCount) + ((this.unsearchableItemSet == null) ? 0 : this.unsearchableItemSet.Count);
			int num = 0;
			int num2 = 0;
			while (num2 < folders.Length && (this.SearchCommunicator == null || !this.SearchCommunicator.IsAborted))
			{
				double maxProgress = this.CalcProgress(num + folders[num2].ItemCount, totalItemCount, mailboxProgress, 100.0);
				if (this.IsStatisticsOnly(sourceMailbox))
				{
					this.EstimateFolderItemsInStatisticsOnlySearch(folders[num2], maxProgress, result);
				}
				else
				{
					this.EstimateFolderItems(sourceMailbox, folders[num2], maxProgress, result);
				}
				num += folders[num2].ItemCount;
				num2++;
			}
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x00047D44 File Offset: 0x00045F44
		private void CollectKeywordResults(KeywordHit result)
		{
			if (this.SearchResult.SubQueryResults.ContainsKey(result.Phrase))
			{
				this.SearchResult.SubQueryResults[result.Phrase].Count += result.Count;
				this.SearchResult.SubQueryResults[result.Phrase].Size += result.Size;
				return;
			}
			this.SearchResult.SubQueryResults.Add(result.Phrase, result);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x00047DD8 File Offset: 0x00045FD8
		private void GenerateKeywordStatistics(MailboxSession sourceMailbox, SearchFolder searchFolder, Folder[] resultFolders, KeywordHit keywordsBefore)
		{
			if (this.searchMailboxCriteria.SubSearchFilters != null)
			{
				sourceMailbox.GetDefaultFolderId(DefaultFolderType.Root);
				StoreId[] folderScope = this.searchMailboxCriteria.GetFolderScope(sourceMailbox);
				int num = 0;
				int num2 = 10;
				int count = this.searchMailboxCriteria.SubSearchFilters.Keys.Count;
				foreach (string keyword in this.searchMailboxCriteria.SubSearchFilters.Keys)
				{
					if (this.SearchCommunicator.IsAborted)
					{
						break;
					}
					this.PerformSingleKeywordEstimationSearch(sourceMailbox, searchFolder, folderScope, resultFolders, keyword, ref keywordsBefore);
					num++;
					if (this.delayStatsSearch && num % num2 == 0 && num < count)
					{
						SearchMailboxWorker.Tracer.TraceDebug<int, int>((long)this.GetHashCode(), "Sleep for {0} milliseconds for each batch of {1} keyword(s).", this.delayStatsInterval, num2);
						Thread.Sleep(this.delayStatsInterval);
					}
				}
			}
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x00047ECC File Offset: 0x000460CC
		internal void PerformSingleKeywordEstimationSearch(MailboxSession sourceMailbox, SearchFolder searchFolder, StoreId[] folderScope, Folder[] resultFolders, string keyword, ref KeywordHit keywordsBefore)
		{
			KeywordHit keywordHit = new KeywordHit
			{
				Phrase = keyword
			};
			try
			{
				this.PopulateSearchFolder(sourceMailbox, searchFolder, this.searchMailboxCriteria.SubSearchFilters[keyword], true, true, true, folderScope);
			}
			catch (SearchMailboxException)
			{
				this.PopulateSearchFolder(sourceMailbox, searchFolder, this.searchMailboxCriteria.SubSearchFilters[keyword], true, true, false, folderScope);
			}
			this.EstimateSearchResults(sourceMailbox, resultFolders, keywordHit);
			if (keywordsBefore == null)
			{
				this.CollectKeywordResults(keywordHit);
			}
			else if (keywordHit.Count >= keywordsBefore.Count && keywordHit.Size >= keywordsBefore.Size)
			{
				this.CollectKeywordResults(new KeywordHit
				{
					Phrase = keywordHit.Phrase,
					Count = keywordHit.Count - keywordsBefore.Count,
					Size = keywordHit.Size - keywordsBefore.Size
				});
			}
			keywordsBefore = keywordHit;
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00047FD0 File Offset: 0x000461D0
		private void CopySearchResults(MailboxSession sourceMailbox, Folder[] folders, StoreId targetRootId, string[] targetSubfolders)
		{
			StoreId defaultFolderId = sourceMailbox.GetDefaultFolderId(DefaultFolderType.Root);
			StoreId defaultFolderId2 = sourceMailbox.GetDefaultFolderId(DefaultFolderType.RecoverableItemsRoot);
			FolderNode folderNode = new FolderNode(null, targetRootId, null, null);
			FolderNode folderNode2 = folderNode;
			if (this.ExcludeDuplicateMessages)
			{
				FolderNode folderNode3 = new FolderNode(null, null, this.SubfolderName, folderNode2);
				folderNode2 = folderNode3;
			}
			else
			{
				foreach (string displayName in targetSubfolders)
				{
					FolderNode folderNode4 = new FolderNode(null, null, displayName, folderNode2);
					folderNode2 = folderNode4;
				}
			}
			FolderNode folderNode5 = folderNode2;
			folderNode5.SourceFolderId = defaultFolderId;
			this.SearchResult.TargetFolder = targetSubfolders[0];
			Dictionary<StoreId, FolderNode> dictionary = new Dictionary<StoreId, FolderNode>();
			dictionary.Add(folderNode5.SourceFolderId, folderNode5);
			string displayName2 = null;
			string displayName3 = null;
			if (this.SearchDumpster || this.IncludeUnsearchableItems)
			{
				string[] uniqueFolderName = this.GetUniqueFolderName(sourceMailbox, defaultFolderId, new string[]
				{
					Strings.RecoverableItems,
					Strings.Unsearchable
				});
				displayName2 = uniqueFolderName[0];
				displayName3 = uniqueFolderName[1];
			}
			if (this.SearchDumpster && defaultFolderId2 != null)
			{
				dictionary.Add(defaultFolderId2, new FolderNode(defaultFolderId2, null, displayName2, folderNode5));
			}
			double mailboxProgress = this.MailboxProgress;
			int totalItemCount = folders.Aggregate(0, (int s, Folder f) => s + f.ItemCount) + ((this.unsearchableItemSet == null) ? 0 : this.unsearchableItemSet.Count);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			while (num3 < folders.Length && !this.SearchCommunicator.IsAborted)
			{
				double maxProgress = this.CalcProgress(num + folders[num3].ItemCount, totalItemCount, mailboxProgress, 100.0);
				num2 += this.CopyFolderItems(folders[num3], sourceMailbox, dictionary, maxProgress);
				num += folders[num3].ItemCount;
				num3++;
			}
			if (this.ExcludeDuplicateMessages && num2 == 0)
			{
				this.CreateAndGetTargetSubFolderNode(dictionary);
				this.UpdateProgress(this.MailboxProgress);
			}
			if (this.unsearchableItemSet != null && !this.SearchCommunicator.IsAborted)
			{
				if (!this.ExcludeDuplicateMessages)
				{
					dictionary.Clear();
					folderNode5 = new FolderNode(null, folderNode5.TargetFolderId, folderNode5.DisplayName, folderNode5.Parent);
					FolderNode folderNode6 = new FolderNode(defaultFolderId, null, displayName3, folderNode5);
					dictionary.Add(defaultFolderId, folderNode6);
					if (this.SearchDumpster && defaultFolderId2 != null)
					{
						dictionary.Add(defaultFolderId2, new FolderNode(defaultFolderId2, null, displayName2, folderNode6));
					}
				}
				this.CopyUnsearchableItems(sourceMailbox, dictionary);
			}
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x00048234 File Offset: 0x00046434
		internal SearchFolder CreateSearchFolder(MailboxSession sourceMailbox, ref List<Folder> dumpsterFolders, ref Folder[] resultFolders)
		{
			if (this.sourceStoreThrotter == null)
			{
				if (this.searchCommunicator != null)
				{
					this.sourceStoreThrotter = new ResponseThrottler(this.searchCommunicator.AbortEvent);
				}
				else
				{
					this.sourceStoreThrotter = new ResponseThrottler();
				}
			}
			return this.CreateSearchFolder(sourceMailbox, ref dumpsterFolders, ref resultFolders, this.SearchFilter, this.IncludeUnsearchableItems);
		}

		// Token: 0x06000FF4 RID: 4084 RVA: 0x000482A4 File Offset: 0x000464A4
		private SearchFolder CreateSearchFolder(MailboxSession sourceMailbox, ref List<Folder> dumpsterFolders, ref Folder[] resultFolders, QueryFilter searchFilter, bool collectUnsearchableItems)
		{
			SearchFolder searchFolder = null;
			sourceMailbox.GetDefaultFolderId(DefaultFolderType.Root);
			List<Folder> list = null;
			StoreId defaultFolderId = sourceMailbox.GetDefaultFolderId(DefaultFolderType.RecoverableItemsRoot);
			if (this.SearchFilter == null)
			{
				if (!this.searchMailboxCriteria.SearchDumpsterOnly)
				{
					if (sourceMailbox.GetDefaultFolderId(DefaultFolderType.AllItems) == null)
					{
						sourceMailbox.CreateDefaultFolder(DefaultFolderType.AllItems);
					}
					searchFolder = SearchFolder.Bind(sourceMailbox, DefaultFolderType.AllItems);
					resultFolders = new Folder[]
					{
						searchFolder
					};
				}
				else
				{
					resultFolders = new Folder[0];
				}
				if ((this.SearchDumpster || this.searchMailboxCriteria.SearchDumpsterOnly) && defaultFolderId != null)
				{
					QueryFilter queryFilter = DumpsterFolderHelper.ExcludeAuditFoldersFilter;
					if (this.legalHoldEnabled && this.searchMailboxCriteria.ExcludePurgesFromDumpster)
					{
						queryFilter = new AndFilter(new QueryFilter[]
						{
							queryFilter,
							new ComparisonFilter(ComparisonOperator.NotEqual, StoreObjectSchema.DisplayName, "Purges")
						});
					}
					if (this.discoveryHoldEnabled && this.searchMailboxCriteria.ExcludePurgesFromDumpster)
					{
						queryFilter = new AndFilter(new QueryFilter[]
						{
							queryFilter,
							new ComparisonFilter(ComparisonOperator.NotEqual, StoreObjectSchema.DisplayName, "DiscoveryHolds")
						});
					}
					using (Folder folder = Folder.Bind(sourceMailbox, defaultFolderId))
					{
						using (QueryResult queryResult = folder.FolderQuery(FolderQueryFlags.DeepTraversal, queryFilter, null, new PropertyDefinition[]
						{
							FolderSchema.Id
						}))
						{
							list = new List<Folder>();
							list.AddRange(from fid in queryResult.Enumerator<StoreId>()
							select Folder.Bind(sourceMailbox, fid));
						}
					}
				}
				if (list != null && list.Count > 0)
				{
					resultFolders = resultFolders.Concat(list).ToArray<Folder>();
					dumpsterFolders = list;
				}
				return searchFolder;
			}
			StoreId[] folderScope = this.searchMailboxCriteria.GetFolderScope(sourceMailbox);
			if (folderScope.Length == 0)
			{
				return null;
			}
			if (collectUnsearchableItems)
			{
				this.CollectUnsearchableItems(sourceMailbox, folderScope);
			}
			searchFolder = this.CreateAndPopulateSearchFolder(sourceMailbox, folderScope, searchFilter);
			resultFolders = new Folder[]
			{
				searchFolder
			};
			return searchFolder;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000484F0 File Offset: 0x000466F0
		private SearchFolder CreateAndPopulateSearchFolder(MailboxSession sourceMailbox, StoreId[] folderScope, QueryFilter searchFilter)
		{
			SearchFolder searchFolder = null;
			try
			{
				searchFolder = this.CreateSearchFolder(sourceMailbox);
				this.PopulateSearchFolder(sourceMailbox, searchFolder, searchFilter, true, true, false, folderScope);
			}
			catch (Exception)
			{
				if (searchFolder != null)
				{
					searchFolder.Dispose();
					searchFolder = null;
				}
				throw;
			}
			return searchFolder;
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x00048540 File Offset: 0x00046740
		private void SearchAndCopyMailbox(MailboxSession sourceMailbox, MailboxSession targetMailbox, StoreId targetRootId, string[] targetSubfolders)
		{
			StoreId storeId = null;
			List<Folder> list = null;
			Folder[] array = null;
			SearchFolder searchFolder = null;
			try
			{
				searchFolder = this.CreateSearchFolder(sourceMailbox, ref list, ref array, this.SearchFilter, this.IncludeUnsearchableItems);
				this.UpdateProgress(15.0);
				if (searchFolder != null || this.searchMailboxCriteria.SearchDumpsterOnly)
				{
					storeId = ((searchFolder != null) ? searchFolder.Id : null);
					if (this.searchActions != null && this.searchActions.Count > 0)
					{
						this.ExecuteSearchActions(sourceMailbox, array, targetMailbox, targetRootId, targetSubfolders);
					}
					else if (this.EstimationPhase)
					{
						KeywordHit keywordHit = new KeywordHit
						{
							Phrase = this.searchMailboxCriteria.UserQuery
						};
						this.PerformInitialEstimation(sourceMailbox, array, ref keywordHit);
						int discoveryMaxMailboxes = (int)SearchUtils.GetDiscoveryMaxMailboxes(this.RecipientSession);
						if (keywordHit.Count > 0 && this.SearchObject != null && SearchUtils.CollectKeywordStats(this.SearchObject, this.TotalMailboxes, discoveryMaxMailboxes))
						{
							this.GenerateKeywordStatistics(sourceMailbox, searchFolder, array, keywordHit);
						}
					}
					else
					{
						this.CopySearchResults(sourceMailbox, array, targetRootId, targetSubfolders);
					}
					if (!this.SearchCommunicator.IsAborted)
					{
						this.UpdateProgress(100.0);
					}
				}
			}
			finally
			{
				if (list != null)
				{
					list.ForEach(delegate(Folder folder)
					{
						folder.Dispose();
					});
				}
				if (searchFolder != null)
				{
					searchFolder.Dispose();
				}
				if (storeId != null)
				{
					try
					{
						sourceMailbox.Delete(DeleteItemFlags.HardDelete, new StoreId[]
						{
							storeId
						});
					}
					catch (StorageTransientException arg)
					{
						SearchMailboxWorker.Tracer.TraceDebug<StoreId, StorageTransientException>((long)this.GetHashCode(), "Delete searchFolderId '{0}' is skipped because of exception: {1}!", storeId, arg);
					}
				}
			}
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x000486F8 File Offset: 0x000468F8
		internal void PerformInitialEstimation(MailboxSession sourceMailbox, Folder[] resultFolders, ref KeywordHit searchHit)
		{
			this.EstimateSearchResults(sourceMailbox, resultFolders, searchHit);
			this.SearchResult.ResultItemsCount += searchHit.Count;
			this.SearchResult.ResultItemsSize += new ByteQuantifiedSize((ulong)searchHit.Size);
			this.CollectKeywordResults(searchHit);
			KeywordHit keywordHit = new KeywordHit
			{
				Phrase = "652beee2-75f7-4ca0-8a02-0698a3919cb9"
			};
			if (this.unsearchableItemSet != null)
			{
				this.EstimateUnsearchableItems(sourceMailbox, keywordHit);
				this.SearchResult.ResultItemsCount += keywordHit.Count;
				this.SearchResult.ResultItemsSize += new ByteQuantifiedSize((ulong)keywordHit.Size);
				this.CollectKeywordResults(keywordHit);
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000487C0 File Offset: 0x000469C0
		private void ExecuteSearchActions(MailboxSession sourceMailbox, Folder[] folders, MailboxSession targetMailbox, StoreId targetRootId, string[] targetSubfolders)
		{
			SearchResultProcessor searchResultProcessor = new SearchResultProcessor(sourceMailbox, targetMailbox, targetRootId, targetSubfolders, this.searchActions, ref this.unsearchableItemSet, this.searchCommunicator, this);
			if (targetMailbox != null)
			{
				this.SearchResult.TargetFolder = targetSubfolders[0];
			}
			searchResultProcessor.Process(folders);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x00048808 File Offset: 0x00046A08
		private bool IsContentIndexingOnMailboxDatabaseEnabled(MailboxSession mailboxSession)
		{
			mailboxSession.Mailbox.Load(new PropertyDefinition[]
			{
				MailboxSchema.IsContentIndexingEnabled
			});
			return mailboxSession.Mailbox.IsContentIndexingEnabled;
		}

		// Token: 0x06000FFA RID: 4090 RVA: 0x0004883C File Offset: 0x00046A3C
		private void CheckContentIndexingIsRunningFlag(MailboxSession mailboxSession, SearchFolder searchFolder)
		{
			SearchFolderCriteria searchCriteria = searchFolder.GetSearchCriteria();
			if ((searchCriteria.SearchState & SearchState.FailNonContentIndexedSearch) == SearchState.FailNonContentIndexedSearch && (searchCriteria.SearchState & SearchState.Failed) == SearchState.Failed)
			{
				throw new SearchMailboxException(Strings.CISearchFailed(string.Format("{0}\\{1}", mailboxSession.MailboxOwner.ObjectId.DomainUserName(), mailboxSession.MailboxOwner.MailboxInfo.MailboxGuid)));
			}
		}

		// Token: 0x04000AA5 RID: 2725
		internal const uint DiscoverySearchFolderMaxResultCount = 10000U;

		// Token: 0x04000AA6 RID: 2726
		protected static readonly Trace Tracer = ExTraceGlobals.SearchTracer;

		// Token: 0x04000AA7 RID: 2727
		private readonly int delayBatchWriteInterval = SearchMailboxExecuter.GetSettingsValue("BatchWriteDelayInterval", 1000);

		// Token: 0x04000AA8 RID: 2728
		private IRecipientSession recipientSession;

		// Token: 0x04000AA9 RID: 2729
		private StoreId[] copyItemBuffer = new StoreId[ResponseThrottler.MaxBulkSize];

		// Token: 0x04000AAA RID: 2730
		private int[] copyItemSizeBuffer = new int[ResponseThrottler.MaxBulkSize];

		// Token: 0x04000AAB RID: 2731
		private object[][] copyItemProps = new object[ResponseThrottler.MaxBulkSize][];

		// Token: 0x04000AAC RID: 2732
		private ResponseThrottler sourceStoreThrotter;

		// Token: 0x04000AAD RID: 2733
		private ResponseThrottler targetStoreThrotter;

		// Token: 0x04000AAE RID: 2734
		private HashSet<StoreObjectId> unsearchableItemSet;

		// Token: 0x04000AAF RID: 2735
		private SearchMailboxCriteria searchMailboxCriteria;

		// Token: 0x04000AB0 RID: 2736
		private List<SearchMailboxAction> searchActions;

		// Token: 0x04000AB1 RID: 2737
		private ADRecipient targetUser;

		// Token: 0x04000AB2 RID: 2738
		private MailboxSession targetMailbox;

		// Token: 0x04000AB3 RID: 2739
		private StoreId targetFolderId;

		// Token: 0x04000AB4 RID: 2740
		private string subFolderName;

		// Token: 0x04000AB5 RID: 2741
		private StoreId targetSubFolderId;

		// Token: 0x04000AB6 RID: 2742
		private GenericIdentity ownerIdentity;

		// Token: 0x04000AB7 RID: 2743
		private LoggingLevel loggingLevel;

		// Token: 0x04000AB8 RID: 2744
		private int workerId;

		// Token: 0x04000AB9 RID: 2745
		private SearchCommunicator searchCommunicator;

		// Token: 0x04000ABA RID: 2746
		private SearchMailboxResult searchResult;

		// Token: 0x04000ABB RID: 2747
		private Exception lastException;

		// Token: 0x04000ABC RID: 2748
		private double[] mailboxProgresses = new double[1];

		// Token: 0x04000ABD RID: 2749
		private int currentMailboxId;

		// Token: 0x04000ABE RID: 2750
		private int nonCriticalFails;

		// Token: 0x04000ABF RID: 2751
		private bool checkCI;

		// Token: 0x04000AC0 RID: 2752
		private readonly bool delayStatsSearch = SearchMailboxExecuter.GetSettingsValue("NoStatsDelay", 0) == 0;

		// Token: 0x04000AC1 RID: 2753
		private readonly int delayStatsInterval = SearchMailboxExecuter.GetSettingsValue("StatsDelayInterval", 1000);

		// Token: 0x04000AC2 RID: 2754
		private bool legalHoldEnabled;

		// Token: 0x04000AC3 RID: 2755
		private readonly long batchWriteSize = (long)SearchMailboxExecuter.GetSettingsValue("BatchWriteSize", 4000000);

		// Token: 0x04000AC4 RID: 2756
		private bool discoveryHoldEnabled;

		// Token: 0x04000AC5 RID: 2757
		internal static readonly PropertyDefinition[] ItemPreloadProperties = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.Size,
			StoreObjectSchema.ParentItemId,
			ItemSchema.BodyTag,
			ItemSchema.InternetMessageId,
			ItemSchema.ConversationId,
			ItemSchema.ConversationTopic,
			ItemSchema.ConversationIndexTracking,
			StoreObjectSchema.ItemClass
		};

		// Token: 0x04000AC6 RID: 2758
		internal static readonly PropertyDefinition[] ItemPreloadPropertiesWithLogging = new PropertyDefinition[]
		{
			ItemSchema.Id,
			ItemSchema.Size,
			StoreObjectSchema.ParentItemId,
			ItemSchema.BodyTag,
			ItemSchema.InternetMessageId,
			ItemSchema.ConversationId,
			ItemSchema.ConversationTopic,
			ItemSchema.ConversationIndexTracking,
			StoreObjectSchema.ItemClass,
			ItemSchema.Subject,
			MessageItemSchema.IsRead,
			ItemSchema.SentTime,
			ItemSchema.ReceivedTime,
			ItemSchema.Sender,
			MessageItemSchema.SenderSmtpAddress,
			ItemSchema.Importance,
			ItemSchema.Sensitivity,
			ItemSchema.FlagCompleteTime,
			ItemSchema.CompleteDate,
			ItemSchema.ItemColor,
			ItemSchema.FlagRequest,
			ItemSchema.FlagStatus,
			ItemSchema.FlagSubject,
			ItemSchema.TaskStatus,
			TaskSchema.StartDate,
			TaskSchema.DueDate,
			ItemSchema.IsComplete,
			ItemSchema.PercentComplete,
			ItemSchema.IsToDoItem,
			ItemSchema.IsFlagSetForRecipient,
			ItemSchema.Categories
		};

		// Token: 0x02000234 RID: 564
		internal enum ItemPropertyIndex
		{
			// Token: 0x04000AD4 RID: 2772
			Id,
			// Token: 0x04000AD5 RID: 2773
			Size,
			// Token: 0x04000AD6 RID: 2774
			ParentItemId,
			// Token: 0x04000AD7 RID: 2775
			BodyTag,
			// Token: 0x04000AD8 RID: 2776
			InternetMessageId,
			// Token: 0x04000AD9 RID: 2777
			ConversationId,
			// Token: 0x04000ADA RID: 2778
			ConversationTopic,
			// Token: 0x04000ADB RID: 2779
			ConversationIndexTracking,
			// Token: 0x04000ADC RID: 2780
			ItemClass,
			// Token: 0x04000ADD RID: 2781
			Subject,
			// Token: 0x04000ADE RID: 2782
			IsRead,
			// Token: 0x04000ADF RID: 2783
			SentTime,
			// Token: 0x04000AE0 RID: 2784
			ReceivedTime,
			// Token: 0x04000AE1 RID: 2785
			Sender,
			// Token: 0x04000AE2 RID: 2786
			SenderSmtpAddress,
			// Token: 0x04000AE3 RID: 2787
			Importance,
			// Token: 0x04000AE4 RID: 2788
			Sensitivity,
			// Token: 0x04000AE5 RID: 2789
			FlagCompleteTime,
			// Token: 0x04000AE6 RID: 2790
			CompleteDate,
			// Token: 0x04000AE7 RID: 2791
			ItemColor,
			// Token: 0x04000AE8 RID: 2792
			FlagRequest,
			// Token: 0x04000AE9 RID: 2793
			FlagStatus,
			// Token: 0x04000AEA RID: 2794
			FlagSubject,
			// Token: 0x04000AEB RID: 2795
			TaskStatus,
			// Token: 0x04000AEC RID: 2796
			StartDate,
			// Token: 0x04000AED RID: 2797
			DueDate,
			// Token: 0x04000AEE RID: 2798
			IsComplete,
			// Token: 0x04000AEF RID: 2799
			PercentComplete,
			// Token: 0x04000AF0 RID: 2800
			IsToDoItem,
			// Token: 0x04000AF1 RID: 2801
			IsFlagSetForRecipient,
			// Token: 0x04000AF2 RID: 2802
			Categories
		}
	}
}
