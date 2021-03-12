using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.EventLog;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x0200021C RID: 540
	internal class WebServiceMailboxSearchGroup : MailboxSearchGroup
	{
		// Token: 0x06000EC4 RID: 3780 RVA: 0x0004185C File Offset: 0x0003FA5C
		public WebServiceMailboxSearchGroup(GroupId groupId, MailboxInfo[] mailboxes, SearchCriteria searchCriteria, PagingInfo pagingInfo, CallerInfo executingUser) : base(mailboxes, searchCriteria, pagingInfo, executingUser)
		{
			Util.ThrowOnNull(groupId.Uri, "uri");
			Util.ThrowOnNull(mailboxes, "mailboxes");
			Util.ThrowOnNull(searchCriteria, "searchCriteria");
			Util.ThrowOnNull(pagingInfo, "pagingInfo");
			Util.ThrowOnNull(executingUser, "executingUser");
			this.groupId = groupId;
			this.client = Factory.Current.CreateDiscoveryEwsClient(groupId, base.Mailboxes, base.SearchCriteria, base.PagingInfo, executingUser);
			PerformanceCounters.TotalFanOutSearchesInProgress.Increment();
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x00041908 File Offset: 0x0003FB08
		internal WebServiceMailboxSearchGroup(GroupId groupId, WebServiceMailboxSearchGroup.FindMailboxInfoHandler findMailboxInfo, SearchCriteria searchCriteria, PagingInfo pagingInfo, CallerInfo executingUser) : base(null, searchCriteria, pagingInfo, executingUser)
		{
			Util.ThrowOnNull(findMailboxInfo, "findMailboxInfo");
			this.groupId = groupId;
			this.OnFindMailboxInfo = findMailboxInfo;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06000EC6 RID: 3782 RVA: 0x00041957 File Offset: 0x0003FB57
		public Uri Uri
		{
			get
			{
				return this.groupId.Uri;
			}
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x00041964 File Offset: 0x0003FB64
		internal void MergeSearchResults(SearchMailboxesResult result)
		{
			this.CreateSuccessResult(result);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0004196D File Offset: 0x0003FB6D
		internal ISearchResult GetResultAggregator()
		{
			return base.ResultAggregator;
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x00041978 File Offset: 0x0003FB78
		protected override void ExecuteSearch()
		{
			lock (this.locker)
			{
				this.watch.Start();
				this.Dispatch();
			}
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x000419C4 File Offset: 0x0003FBC4
		protected override void StopSearch()
		{
			lock (this.locker)
			{
				if (!this.completed)
				{
					this.cancelled = true;
					foreach (MailboxInfo mailboxInfo in base.Mailboxes)
					{
						this.MergeMailboxResult(mailboxInfo, new SearchMailboxTaskCancelledException(mailboxInfo));
					}
					this.ReportCompletion();
					this.watch.Stop();
					Factory.Current.GeneralTracer.TraceError<Guid, string, long>((long)this.GetHashCode(), "Correlation Id:{0}. Web service search call for group with URI {1} was stopped. It ran for {2}ms.", base.ExecutingUser.QueryCorrelationId, this.groupId.Uri.AbsoluteUri, this.watch.ElapsedMilliseconds);
					base.ResultAggregator.ProtocolLog.Add("LongestFanoutTime", this.watch.ElapsedMilliseconds);
				}
			}
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x00041AB4 File Offset: 0x0003FCB4
		protected override void ReportCompletion()
		{
			PerformanceCounters.TotalFanOutSearchesInProgress.Decrement();
			base.ReportCompletion();
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x00041AC8 File Offset: 0x0003FCC8
		private static VersionedId EwsIdToVersionedId(string ewsId, string changeKey, out MailboxId mailboxId)
		{
			IdHeaderInformation idHeaderInformation = ServiceIdConverter.ConvertFromConcatenatedId(ewsId, BasicTypes.Item, null);
			mailboxId = idHeaderInformation.MailboxId;
			byte[] byteArrayChangeKey;
			StoreObjectType objectType;
			WebServiceMailboxSearchGroup.ParseChangeKeyString(changeKey, out byteArrayChangeKey, out objectType);
			return VersionedId.Deserialize(idHeaderInformation.StoreIdBytes, byteArrayChangeKey, objectType);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x00041B00 File Offset: 0x0003FD00
		private static void ParseChangeKeyString(string changeKey, out byte[] realChangeKey, out StoreObjectType objectType)
		{
			try
			{
				byte[] array = Convert.FromBase64String(changeKey);
				int num = array.Length;
				int num2 = 0;
				if (num < 4)
				{
					throw new WebServiceProxyInvalidResponseException(Strings.InvalidChangeKeyReturned);
				}
				objectType = (StoreObjectType)BitConverter.ToInt32(array, num2);
				num2 += 4;
				if (!EnumValidator.IsValidValue<StoreObjectType>(objectType))
				{
					throw new WebServiceProxyInvalidResponseException(Strings.InvalidChangeKeyReturned);
				}
				if (num2 == num)
				{
					realChangeKey = null;
				}
				else
				{
					int num3 = BitConverter.ToInt32(array, num2);
					num2 += 4;
					if (num3 <= 0)
					{
						realChangeKey = null;
					}
					else
					{
						if (num3 > 512)
						{
							throw new WebServiceProxyInvalidResponseException(Strings.InvalidChangeKeyReturned);
						}
						if (num3 != num - num2)
						{
							throw new WebServiceProxyInvalidResponseException(Strings.InvalidChangeKeyReturned);
						}
						realChangeKey = new byte[num3];
						Array.Copy(array, num2, realChangeKey, 0, num3);
						num2 += num3;
					}
				}
			}
			catch (FormatException innerException)
			{
				throw new WebServiceProxyInvalidResponseException(Strings.InvalidChangeKeyReturned, innerException);
			}
			catch (ArgumentOutOfRangeException innerException2)
			{
				throw new WebServiceProxyInvalidResponseException(Strings.InvalidChangeKeyReturned, innerException2);
			}
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x00041BE0 File Offset: 0x0003FDE0
		private static Participant GetParticipant(string displayName, string emailAddress, string routingType)
		{
			return new Participant(displayName, emailAddress, routingType);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x00041BEC File Offset: 0x0003FDEC
		private void Dispatch()
		{
			this.retryCount--;
			Factory.Current.GeneralTracer.TraceDebug<Guid, string, int>((long)this.GetHashCode(), "Correlation Id:{0}. Beginning web service search call for group with URI {1}. {2} attempts remaining.", base.ExecutingUser.QueryCorrelationId, this.groupId.Uri.AbsoluteUri, this.retryCount);
			this.asyncResult = this.client.BeginEwsCall(new AsyncCallback(this.SearchCompletedCallback), null);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0004200B File Offset: 0x0004020B
		private void SearchCompletedCallback(IAsyncResult result)
		{
			Util.HandleExceptions(delegate
			{
				lock (this.locker)
				{
					Exception ex = null;
					bool flag2 = false;
					try
					{
						Factory.Current.GeneralTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Reading response for the web service search call for group with URI {1}.", base.ExecutingUser.QueryCorrelationId, this.groupId.Uri.AbsoluteUri);
						SearchMailboxesResponse searchMailboxesResponse = (SearchMailboxesResponse)this.client.EndEwsCall(this.asyncResult);
						if (!this.cancelled)
						{
							if (searchMailboxesResponse.ErrorCode != null)
							{
								throw new WebServiceProxyInvalidResponseException(Strings.CrossServerCallFailed(string.Format("EDiscoveryError:E003::Server:{0}::", this.groupId.Uri.AbsoluteUri), searchMailboxesResponse.ErrorCode.ToString(), searchMailboxesResponse.ErrorMessage));
							}
							this.CreateSuccessResult(searchMailboxesResponse.SearchResult);
						}
					}
					catch (ServiceResponseException ex2)
					{
						if (this.retryCount > 0 && WebServiceMailboxSearchGroup.TransientServiceErrors.Contains(ex2.ErrorCode))
						{
							flag2 = true;
						}
						ex = ex2;
					}
					catch (ServiceRemoteException ex3)
					{
						if (this.retryCount > 0)
						{
							flag2 = true;
						}
						ex = ex3;
					}
					catch (ServiceXmlDeserializationException ex4)
					{
						ex = ex4;
					}
					catch (WebServiceProxyInvalidResponseException ex5)
					{
						ex = ex5;
					}
					if (!this.cancelled)
					{
						if (flag2)
						{
							Factory.Current.GeneralTracer.TraceDebug<Guid, string, string>((long)this.GetHashCode(), "Correlation Id:{0}. The web service search call for group with URI {1} hit a retryable exception {2}.", base.ExecutingUser.QueryCorrelationId, this.groupId.Uri.AbsoluteUri, ex.ToString());
							this.Dispatch();
						}
						else
						{
							if (ex != null)
							{
								Factory.Current.GeneralTracer.TraceError<Guid, string, string>((long)this.GetHashCode(), "Correlation Id:{0}. Web service search call for group with URI {1} failed with the error {2}", base.ExecutingUser.QueryCorrelationId, this.groupId.Uri.AbsoluteUri, ex.ToString());
								Factory.Current.EventLog.LogEvent(InfoWorkerEventLogConstants.Tuple_DiscoveryFanoutError, null, new object[]
								{
									base.ExecutingUser.QueryCorrelationId.ToString(),
									this.Uri.ToString(),
									ex.ToString()
								});
								this.CreateErrorResult(ex);
							}
							this.ReportCompletion();
							this.completed = true;
							this.watch.Stop();
							base.ResultAggregator.ProtocolLog.Add("LongestFanoutTime", this.watch.ElapsedMilliseconds);
							Factory.Current.GeneralTracer.TraceError<Guid, string, long>((long)this.GetHashCode(), "Correlation Id:{0}. Web service search call for group with URI {1} completed successfully. It ran for {2}ms.", base.ExecutingUser.QueryCorrelationId, this.groupId.Uri.AbsoluteUri, this.watch.ElapsedMilliseconds);
						}
					}
				}
			}, delegate(GrayException ex)
			{
				this.CreateErrorResult(new WebServiceProxyInvalidResponseException(Strings.UnexpectedError, ex));
				Factory.Current.GeneralTracer.TraceError<Guid, string, string>((long)this.GetHashCode(), "Correlation Id:{0}. Web service search call for group with URI {1} failed with the error {2}", base.ExecutingUser.QueryCorrelationId, this.groupId.Uri.AbsoluteUri, ex.ToString());
				this.ReportCompletion();
				this.completed = true;
				this.watch.Stop();
				base.ResultAggregator.ProtocolLog.Add("LongestFanoutTime", this.watch.ElapsedMilliseconds);
			});
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0004202C File Offset: 0x0004022C
		internal void MergeMailboxResult(MailboxInfo info, Exception ex)
		{
			if (base.SearchCriteria.SearchType == SearchType.Preview || base.SearchCriteria.SearchType == SearchType.NonIndexedItemPreview || base.SearchCriteria.SearchType == SearchType.NonIndexedItemStatistics)
			{
				base.ResultAggregator.MergeSearchResult(new SearchMailboxResult(info, ex));
			}
			if (base.SearchCriteria.SearchType == SearchType.Statistics)
			{
				base.ResultAggregator.MergeSearchResult(new SearchMailboxResult(info, new KeywordHit(base.SearchCriteria.QueryString, info, ex)));
			}
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000420A8 File Offset: 0x000402A8
		private void CreateErrorResult(Exception ex)
		{
			foreach (MailboxInfo info in base.Mailboxes)
			{
				this.MergeMailboxResult(info, ex);
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000420D8 File Offset: 0x000402D8
		internal void CreateSuccessResult(SearchMailboxesResult ewsResult)
		{
			SortedResultPage sortedResultPage = null;
			List<Pair<MailboxInfo, Exception>> previewErrors = null;
			Dictionary<string, List<IRefinerResult>> dictionary = null;
			List<MailboxStatistics> mailboxStatistics = null;
			Dictionary<string, IKeywordHit> dictionary2 = null;
			if (base.SearchCriteria.SearchType == SearchType.Preview || base.SearchCriteria.SearchType == SearchType.NonIndexedItemPreview || base.SearchCriteria.SearchType == SearchType.NonIndexedItemStatistics)
			{
				sortedResultPage = this.ConvertToPreviewItems(ewsResult.PreviewItems);
				if (ewsResult.FailedMailboxes != null && ewsResult.FailedMailboxes.Length != 0)
				{
					previewErrors = this.ConvertToPreviewErrors(ewsResult.FailedMailboxes);
				}
				bool emptyResults = sortedResultPage != null && sortedResultPage.ResultCount == 0;
				dictionary = null;
				mailboxStatistics = this.ConvertMailboxStatistics(ewsResult.MailboxStats, emptyResults);
				if (sortedResultPage == null && (ewsResult.ItemCount > 0L || dictionary != null || ewsResult.Size > 0UL))
				{
					throw new WebServiceProxyInvalidResponseException(Strings.InvalidPreviewSearchResults(this.Uri.AbsoluteUri));
				}
			}
			if (base.SearchCriteria.SearchType == SearchType.Statistics)
			{
				dictionary2 = new Dictionary<string, IKeywordHit>();
				foreach (KeywordStatisticsSearchResult keywordStatisticsSearchResult in ewsResult.KeywordStats)
				{
					KeywordHit value = new KeywordHit(keywordStatisticsSearchResult.Keyword, (ulong)((long)keywordStatisticsSearchResult.ItemHits), new ByteQuantifiedSize(keywordStatisticsSearchResult.Size));
					dictionary2.Add(keywordStatisticsSearchResult.Keyword, value);
				}
			}
			base.ResultAggregator.MergeSearchResult(new ResultAggregator(sortedResultPage, dictionary, (ulong)ewsResult.ItemCount, new ByteQuantifiedSize(ewsResult.Size), previewErrors, dictionary2, mailboxStatistics));
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00042228 File Offset: 0x00040428
		private SortedResultPage ConvertToPreviewItems(SearchPreviewItem[] ewsPreviewItems)
		{
			if (ewsPreviewItems == null)
			{
				return new SortedResultPage(new PreviewItem[0], base.PagingInfo);
			}
			List<PreviewItem> list = new List<PreviewItem>(ewsPreviewItems.Length);
			foreach (SearchPreviewItem searchPreviewItem in ewsPreviewItems)
			{
				try
				{
					MailboxId mailboxId;
					VersionedId value = WebServiceMailboxSearchGroup.EwsIdToVersionedId(searchPreviewItem.Id.UniqueId, searchPreviewItem.Id.ChangeKey, out mailboxId);
					MailboxInfo mailboxInfo = this.FindMailboxInfoByAnyIdentifier(searchPreviewItem, searchPreviewItem.Mailbox.MailboxId, mailboxId.MailboxGuid, null);
					if (mailboxInfo == null)
					{
						throw new WebServiceProxyInvalidResponseException(Strings.InvalidUnknownMailboxInPreviewResult(this.Uri.AbsoluteUri, searchPreviewItem.Mailbox.MailboxId, mailboxId.MailboxGuid));
					}
					Dictionary<PropertyDefinition, object> dictionary = new Dictionary<PropertyDefinition, object>();
					dictionary.Add(ItemSchema.Id, value);
					dictionary.Add(StoreObjectSchema.ParentItemId, StoreId.EwsIdToStoreObjectId(searchPreviewItem.ParentId.UniqueId));
					dictionary.Add(StoreObjectSchema.ItemClass, searchPreviewItem.ItemClass);
					dictionary.Add(MessageItemSchema.SenderDisplayName, searchPreviewItem.Sender);
					dictionary.Add(ItemSchema.DisplayTo, this.GetSemicolonSeparated(searchPreviewItem.ToRecipients));
					dictionary.Add(ItemSchema.DisplayCc, this.GetSemicolonSeparated(searchPreviewItem.CcRecipients));
					dictionary.Add(ItemSchema.DisplayBcc, this.GetSemicolonSeparated(searchPreviewItem.BccRecipients));
					dictionary.Add(StoreObjectSchema.CreationTime, WebServiceMailboxSearchGroup.CreateExDateTime(base.PagingInfo.TimeZone, searchPreviewItem.CreatedTime, this));
					dictionary.Add(ItemSchema.ReceivedTime, WebServiceMailboxSearchGroup.CreateExDateTime(base.PagingInfo.TimeZone, searchPreviewItem.ReceivedTime, this));
					dictionary.Add(ItemSchema.SentTime, WebServiceMailboxSearchGroup.CreateExDateTime(base.PagingInfo.TimeZone, searchPreviewItem.SentTime, this));
					dictionary.Add(ItemSchema.Subject, searchPreviewItem.Subject);
					dictionary.Add(ItemSchema.Size, (int)searchPreviewItem.Size);
					dictionary.Add(ItemSchema.Importance, searchPreviewItem.Importance.ToString());
					dictionary.Add(MessageItemSchema.IsRead, searchPreviewItem.Read);
					dictionary.Add(ItemSchema.HasAttachment, searchPreviewItem.HasAttachment);
					if (searchPreviewItem.OwaLink == null)
					{
						Factory.Current.GeneralTracer.TraceError<Guid, string, string>((long)this.GetHashCode(), "Correlation Id:{0}. Null OWA URI in item with Id {1} in mailbox {2}", base.ExecutingUser.QueryCorrelationId, searchPreviewItem.Id.UniqueId, searchPreviewItem.Mailbox.PrimarySmtpAddress);
						throw new WebServiceProxyInvalidResponseException(Strings.InvalidOwaUrlInPreviewResult(string.Format("EDiscoveryError:E002::Mailbox:{0}::Item:{1}::", searchPreviewItem.Mailbox.PrimarySmtpAddress, searchPreviewItem.Subject), this.Uri.AbsoluteUri));
					}
					PreviewItem item = new PreviewItem(dictionary, mailboxInfo.MailboxGuid.Equals(Guid.Empty) ? Guid.Parse(mailboxId.MailboxGuid) : mailboxInfo.MailboxGuid, new Uri(searchPreviewItem.OwaLink), this.GetReferenceItem(searchPreviewItem.SortValue), this.GetUniqueItemHash(searchPreviewItem.UniqueHash))
					{
						MailboxInfo = mailboxInfo
					};
					list.Add(item);
				}
				catch (InvalidIdMalformedException ex)
				{
					Factory.Current.GeneralTracer.TraceError((long)this.GetHashCode(), "Correlation Id:{0}. Error processing item with Id {1} in mailbox {2}: {3}", new object[]
					{
						base.ExecutingUser.QueryCorrelationId,
						searchPreviewItem.Id.UniqueId,
						searchPreviewItem.Mailbox.PrimarySmtpAddress,
						ex.ToString()
					});
					throw new WebServiceProxyInvalidResponseException(Strings.InvalidIdInPreviewResult(this.Uri.AbsoluteUri), ex);
				}
				catch (UriFormatException ex2)
				{
					Factory.Current.GeneralTracer.TraceError((long)this.GetHashCode(), "Correlation Id:{0}. Invalid OWA URI in item with Id {1} in mailbox {2}: {3}", new object[]
					{
						base.ExecutingUser.QueryCorrelationId,
						searchPreviewItem.Id.UniqueId,
						searchPreviewItem.Mailbox.PrimarySmtpAddress,
						ex2.ToString()
					});
					throw new WebServiceProxyInvalidResponseException(Strings.InvalidOwaUrlInPreviewResult(string.Format("EDiscoveryError:E002::Mailbox:{0}::Item:{1}::", searchPreviewItem.Mailbox.PrimarySmtpAddress, searchPreviewItem.Subject), this.Uri.AbsoluteUri), ex2);
				}
			}
			return new SortedResultPage(list.ToArray(), base.PagingInfo);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000426A8 File Offset: 0x000408A8
		private static ExDateTime CreateExDateTime(ExTimeZone timeZone, DateTime dateTime, WebServiceMailboxSearchGroup searchGroup = null)
		{
			ExDateTime result = ExDateTime.MinValue;
			IList<ExDateTime> list = ExDateTime.Create(timeZone, dateTime);
			if (list.Count > 0)
			{
				result = list[0];
			}
			else
			{
				Factory.Current.GeneralTracer.TraceDebug<ExTimeZone, DateTime>((long)((searchGroup != null) ? searchGroup.GetHashCode() : 0), "DateTime could not be converted to ExDateTime since call to ExchangeSystem.ExDateTime.Create(ExTimeZone: {0}, DateTime: {1}) return nothing", timeZone, dateTime);
			}
			return result;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x00042734 File Offset: 0x00040934
		private List<Pair<MailboxInfo, Exception>> ConvertToPreviewErrors(FailedSearchMailbox[] failedMailboxes)
		{
			List<Pair<MailboxInfo, Exception>> list = new List<Pair<MailboxInfo, Exception>>(failedMailboxes.Length);
			List<MailboxInfo> list2 = new List<MailboxInfo>(failedMailboxes.Length);
			for (int i = 0; i < failedMailboxes.Length; i++)
			{
				FailedSearchMailbox failedSearchMailbox = failedMailboxes[i];
				if (failedSearchMailbox == null)
				{
					Factory.Current.GeneralTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Null failed mailbox encountered in search result from {1}", base.ExecutingUser.QueryCorrelationId, this.Uri.AbsoluteUri);
					throw new WebServiceProxyInvalidResponseException(Strings.InvalidFailedMailboxesResultWebServiceResponse(this.Uri.AbsoluteUri));
				}
				MailboxInfo info = this.FindMailboxInfoByAnyIdentifier(failedSearchMailbox, failedSearchMailbox.Mailbox, null, new bool?(failedSearchMailbox.IsArchive));
				if (info == null || string.IsNullOrEmpty(failedSearchMailbox.ErrorMessage))
				{
					Factory.Current.GeneralTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Invalid mailbox encountered in search result from {1}", base.ExecutingUser.QueryCorrelationId, this.Uri.AbsoluteUri);
					throw new WebServiceProxyInvalidResponseException(Strings.InvalidFailedMailboxesResultWebServiceResponse(this.Uri.AbsoluteUri));
				}
				if ((from x in list2
				where string.Equals(x.LegacyExchangeDN, info.LegacyExchangeDN, StringComparison.OrdinalIgnoreCase) && x.Type == info.Type
				select x).FirstOrDefault<MailboxInfo>() != null)
				{
					Factory.Current.GeneralTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Duplicate failed mailbox encountered in search result from {1}", base.ExecutingUser.QueryCorrelationId, this.Uri.AbsoluteUri);
					throw new WebServiceProxyInvalidResponseException(Strings.InvalidFailedMailboxesResultDuplicateEntries(this.Uri.AbsoluteUri));
				}
				list2.Add(info);
				list.Add(new Pair<MailboxInfo, Exception>(info, new Exception(failedSearchMailbox.ErrorMessage)));
			}
			return list;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x000428CC File Offset: 0x00040ACC
		private Dictionary<string, List<IRefinerResult>> ConvertRefiners(SearchRefinerItem[] ewsRefiners, bool emptyResults)
		{
			if (!emptyResults && (ewsRefiners == null || ewsRefiners.Length == 0))
			{
				Factory.Current.GeneralTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Empty refiners encountered in search result from {1}", base.ExecutingUser.QueryCorrelationId, this.Uri.AbsoluteUri);
				throw new WebServiceProxyInvalidResponseException(Strings.EmptyRefinerServerResponse(this.Uri.AbsoluteUri));
			}
			Dictionary<string, List<IRefinerResult>> dictionary = new Dictionary<string, List<IRefinerResult>>();
			foreach (SearchRefinerItem searchRefinerItem in ewsRefiners)
			{
				List<IRefinerResult> list;
				if (!dictionary.TryGetValue(searchRefinerItem.Name, out list))
				{
					list = new List<IRefinerResult>();
					dictionary.Add(searchRefinerItem.Name, list);
				}
				if (string.IsNullOrEmpty(searchRefinerItem.Value) || searchRefinerItem.Count == 0L)
				{
					Factory.Current.GeneralTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Null refiner encountered in search result from {1}", base.ExecutingUser.QueryCorrelationId, this.Uri.AbsoluteUri);
					throw new WebServiceProxyInvalidResponseException(Strings.RefinerValueNullOrCountZero(this.Uri.AbsoluteUri));
				}
				list.Add(new RefinerResult(searchRefinerItem.Value, searchRefinerItem.Count));
			}
			return dictionary;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000429EC File Offset: 0x00040BEC
		private List<MailboxStatistics> ConvertMailboxStatistics(MailboxStatisticsItem[] ewsMailboxStatistics, bool emptyResults)
		{
			if (!emptyResults && (ewsMailboxStatistics == null || ewsMailboxStatistics.Length == 0))
			{
				throw new WebServiceProxyInvalidResponseException(Strings.EmptyMailboxStatsServerResponse(this.Uri.AbsoluteUri));
			}
			List<MailboxStatistics> list = null;
			if (ewsMailboxStatistics != null)
			{
				list = new List<MailboxStatistics>(ewsMailboxStatistics.Length);
				foreach (MailboxStatisticsItem mailboxStatisticsItem in ewsMailboxStatistics)
				{
					MailboxInfo mailboxInfo = this.FindMailboxInfoByAnyIdentifier(mailboxStatisticsItem, mailboxStatisticsItem.MailboxId, null, null);
					if (mailboxInfo == null)
					{
						Factory.Current.GeneralTracer.TraceError<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Invalid mailbox encountered in statistics result from {1}", base.ExecutingUser.QueryCorrelationId, this.Uri.AbsoluteUri);
						throw new WebServiceProxyInvalidResponseException(Strings.InvalidMailboxInMailboxStatistics(this.Uri.AbsoluteUri));
					}
					list.Add(new MailboxStatistics(mailboxInfo, (ulong)mailboxStatisticsItem.ItemCount, new ByteQuantifiedSize(mailboxStatisticsItem.Size)));
				}
			}
			return list;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00042ACC File Offset: 0x00040CCC
		private string GetSemicolonSeparated(string[] recipients)
		{
			if (recipients == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string value in recipients)
			{
				if (!string.IsNullOrEmpty(value))
				{
					stringBuilder.Append(value);
					stringBuilder.Append(";");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00042B1C File Offset: 0x00040D1C
		private UniqueItemHash GetUniqueItemHash(string hash)
		{
			UniqueItemHash result;
			try
			{
				result = UniqueItemHash.Parse(hash);
			}
			catch (ArgumentException innerException)
			{
				throw new WebServiceProxyInvalidResponseException(Strings.InvalidItemHashInPreviewResult(this.Uri.AbsoluteUri), innerException);
			}
			return result;
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x00042B5C File Offset: 0x00040D5C
		private ReferenceItem GetReferenceItem(string sortValue)
		{
			ReferenceItem result;
			try
			{
				result = ReferenceItem.Parse(base.PagingInfo.SortBy, sortValue);
			}
			catch (ArgumentException innerException)
			{
				throw new WebServiceProxyInvalidResponseException(Strings.InvalidReferenceItemInPreviewResult(this.Uri.AbsoluteUri), innerException);
			}
			return result;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x00042BA8 File Offset: 0x00040DA8
		private MailboxInfo FindMailboxInfoByAnyIdentifier(object item, string legacyDn, string mailboxGuid = null, bool? isArchive = null)
		{
			if (this.OnFindMailboxInfo != null)
			{
				return this.OnFindMailboxInfo(item);
			}
			if (!string.IsNullOrEmpty(mailboxGuid))
			{
				return this.FindMailboxInfoByGuid(legacyDn, mailboxGuid);
			}
			return this.FindMailboxInfo(legacyDn, isArchive);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x00042BDC File Offset: 0x00040DDC
		private MailboxInfo FindMailboxInfo(string legacyDn, bool? isArchive = null)
		{
			foreach (MailboxInfo mailboxInfo in base.Mailboxes)
			{
				if (isArchive == null || mailboxInfo.IsArchive == isArchive)
				{
					MailboxInfo result;
					if (string.Equals(mailboxInfo.LegacyExchangeDN, legacyDn, StringComparison.OrdinalIgnoreCase))
					{
						result = mailboxInfo;
					}
					else
					{
						ProxyAddressCollection emailAddresses = mailboxInfo.EmailAddresses;
						if (emailAddresses != null)
						{
							foreach (ProxyAddress proxyAddress in emailAddresses)
							{
								if (proxyAddress.Prefix == ProxyAddressPrefix.X500 && string.Equals(proxyAddress.AddressString, legacyDn, StringComparison.OrdinalIgnoreCase))
								{
									return mailboxInfo;
								}
							}
							goto IL_AE;
						}
						goto IL_AE;
					}
					return result;
				}
				IL_AE:;
			}
			return null;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x00042CBC File Offset: 0x00040EBC
		private MailboxInfo FindMailboxInfoByGuid(string legacyDn, string mailboxGuidString)
		{
			if (string.IsNullOrEmpty(mailboxGuidString))
			{
				return this.FindMailboxInfo(legacyDn, null);
			}
			Guid guid;
			if (!Guid.TryParse(mailboxGuidString, out guid) || guid.Equals(Guid.Empty))
			{
				return this.FindMailboxInfo(legacyDn, null);
			}
			MailboxInfo mailboxInfo = this.FindMailboxInfo(legacyDn, new bool?(false));
			if (mailboxInfo != null && guid.Equals(mailboxInfo.MailboxGuid))
			{
				return mailboxInfo;
			}
			MailboxInfo mailboxInfo2 = this.FindMailboxInfo(legacyDn, new bool?(true));
			if (mailboxInfo2 != null && guid.Equals(mailboxInfo2.MailboxGuid))
			{
				return mailboxInfo2;
			}
			if (mailboxInfo != null && Guid.Empty.Equals(mailboxInfo.MailboxGuid))
			{
				return mailboxInfo;
			}
			return null;
		}

		// Token: 0x04000A04 RID: 2564
		internal static readonly ServiceError[] TransientServiceErrors = new ServiceError[]
		{
			6,
			8,
			75,
			126,
			128,
			363,
			101
		};

		// Token: 0x04000A05 RID: 2565
		private readonly object locker = new object();

		// Token: 0x04000A06 RID: 2566
		private readonly GroupId groupId;

		// Token: 0x04000A07 RID: 2567
		private readonly IEwsClient client;

		// Token: 0x04000A08 RID: 2568
		private readonly Stopwatch watch = new Stopwatch();

		// Token: 0x04000A09 RID: 2569
		private IAsyncResult asyncResult;

		// Token: 0x04000A0A RID: 2570
		private bool cancelled;

		// Token: 0x04000A0B RID: 2571
		private int retryCount = 3;

		// Token: 0x04000A0C RID: 2572
		private bool completed;

		// Token: 0x04000A0D RID: 2573
		private WebServiceMailboxSearchGroup.FindMailboxInfoHandler OnFindMailboxInfo;

		// Token: 0x0200021D RID: 541
		// (Invoke) Token: 0x06000EE3 RID: 3811
		internal delegate MailboxInfo FindMailboxInfoHandler(object state);
	}
}
