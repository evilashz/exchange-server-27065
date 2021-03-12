using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Task
{
	// Token: 0x02000052 RID: 82
	internal class DirectoryQueryFormatting : SearchTask<DirectoryQueryParameters>
	{
		// Token: 0x06000389 RID: 905 RVA: 0x00016E38 File Offset: 0x00015038
		public override void Process(DirectoryQueryParameters item)
		{
			Recorder.Trace(4L, TraceType.InfoTrace, "DirectoryQueryFormatting.Process Item:", item);
			SearchSource searchSource = null;
			SearchSource searchSource2 = null;
			List<SearchSource> list = new List<SearchSource>();
			List<DirectoryQueryFormatting.SourceFilterPair> list2 = new List<DirectoryQueryFormatting.SourceFilterPair>();
			foreach (SearchSource searchSource3 in item.Sources)
			{
				bool flag = true;
				if (searchSource3.SourceType == SourceType.AutoDetect)
				{
					searchSource3.SourceType = SearchSource.GetSourceType(searchSource3);
				}
				Recorder.Trace(4L, TraceType.InfoTrace, new object[]
				{
					"DirectoryQueryFormatting.Process Source:",
					searchSource3.ReferenceId,
					"Type:",
					searchSource3.SourceType
				});
				switch (searchSource3.SourceType)
				{
				case SourceType.LegacyExchangeDN:
				case SourceType.Recipient:
				case SourceType.MailboxGuid:
					if (searchSource == null)
					{
						if (!string.IsNullOrEmpty(searchSource3.ReferenceId))
						{
							QueryFilter sourceFilter = SearchRecipient.GetSourceFilter(searchSource3);
							if (sourceFilter != null)
							{
								list2.Add(new DirectoryQueryFormatting.SourceFilterPair
								{
									Source = searchSource3,
									Filter = sourceFilter
								});
								flag = false;
							}
						}
					}
					else
					{
						flag = false;
					}
					break;
				case SourceType.PublicFolder:
					if (searchSource2 == null)
					{
						if (!string.IsNullOrEmpty(searchSource3.ReferenceId))
						{
							list.Add(searchSource3);
							flag = false;
						}
					}
					else
					{
						flag = false;
					}
					break;
				case SourceType.AllPublicFolders:
					searchSource2 = searchSource3;
					flag = false;
					break;
				case SourceType.AllMailboxes:
					searchSource = searchSource3;
					flag = false;
					break;
				}
				if (flag)
				{
					Recorder.Trace(4L, TraceType.ErrorTrace, new object[]
					{
						"DirectoryQueryFormatting.Process FailedSource:",
						searchSource3.ReferenceId,
						"FailedType:",
						searchSource3.SourceType
					});
					base.Executor.Fail(new SearchException(KnownError.ErrorSearchableObjectNotFound)
					{
						ErrorSource = searchSource3
					});
				}
				if (searchSource != null && searchSource2 != null)
				{
					break;
				}
			}
			this.ProcessMailboxes(item, searchSource, list2);
			this.ProcessPublicFolders(item, searchSource2, list);
		}

		// Token: 0x0600038A RID: 906 RVA: 0x00017040 File Offset: 0x00015240
		private void ProcessMailboxes(DirectoryQueryParameters item, SearchSource allMailboxes, List<DirectoryQueryFormatting.SourceFilterPair> mailboxFilterPairs)
		{
			Recorder.Trace(4L, TraceType.InfoTrace, new object[]
			{
				"DirectoryQueryFormatting.ProcessMailboxes AllMailboxes:",
				allMailboxes,
				"MailboxFilterPairs:",
				mailboxFilterPairs
			});
			if (allMailboxes == null)
			{
				if (mailboxFilterPairs.Count > 0)
				{
					Recorder.Trace(4L, TraceType.InfoTrace, "DirectoryQueryFormatting.ProcessMailboxes Mailboxes Count:", mailboxFilterPairs.Count);
					int num;
					for (int i = 0; i < mailboxFilterPairs.Count; i += num)
					{
						num = Math.Min(mailboxFilterPairs.Count - i, item.PageSize);
						IEnumerable<DirectoryQueryFormatting.SourceFilterPair> source = mailboxFilterPairs.Skip(i).Take(num);
						this.Enqueue(item, from t in source
						select t.Source, source.Select((DirectoryQueryFormatting.SourceFilterPair t) => t.Filter));
					}
				}
				return;
			}
			Recorder.Trace(4L, TraceType.InfoTrace, "DirectoryQueryFormatting.ProcessMailboxes AllMailboxes");
			if (string.IsNullOrWhiteSpace(allMailboxes.ReferenceId))
			{
				this.Enqueue(item, new SearchSource[]
				{
					allMailboxes
				}, SearchRecipient.GetRecipientTypeFilter(item.RequestGroups));
				return;
			}
			Recorder.Trace(4L, TraceType.InfoTrace, "DirectoryQueryFormatting.ProcessMailboxes AllMailboxes Filter:", allMailboxes.ReferenceId);
			if (SearchRecipient.IsWildcard(allMailboxes.ReferenceId) && item.ExpandGroups)
			{
				Recorder.Trace(4L, TraceType.ErrorTrace, new object[]
				{
					"DirectoryQueryFormatting.ProcessMailboxes Failed AllMailboxes Filter:",
					allMailboxes.ReferenceId,
					"ExpandGroups:",
					item.ExpandGroups,
					"Wildcard with Group Expansion not Allowed"
				});
				throw new SearchException(KnownError.ErrorWildcardAndGroupExpansionNotAllowed);
			}
			if (SearchRecipient.IsSuffixSearchWildcard(allMailboxes.ReferenceId))
			{
				Recorder.Trace(4L, TraceType.ErrorTrace, new object[]
				{
					"DirectoryQueryFormatting.ProcessMailboxes Failed AllMailboxes Filter:",
					allMailboxes.ReferenceId,
					"ExpandGroups:",
					item.ExpandGroups,
					"Wildcard with Suffix not Allowed"
				});
				throw new SearchException(KnownError.ErrorSuffixSearchNotAllowed);
			}
			this.Enqueue(item, new SearchSource[]
			{
				allMailboxes
			}, SearchRecipient.GetRecipientTypeSearchFilter(allMailboxes.ReferenceId, item.RequestGroups));
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00017258 File Offset: 0x00015458
		private void ProcessPublicFolders(DirectoryQueryParameters item, SearchSource allPublicFolders, List<SearchSource> publicFolders)
		{
			Recorder.Trace(4L, TraceType.InfoTrace, new object[]
			{
				"DirectoryQueryFormatting.ProcessPublicFolders AllPublicFolders:",
				allPublicFolders,
				"MailboxFilterPairs:",
				publicFolders
			});
			List<QueryFilter> list = new List<QueryFilter>();
			List<string> list2 = new List<string>();
			ISourceConverter sourceConverter = SearchFactory.Current.GetSourceConverter(base.Policy, SourceType.PublicFolder);
			if (allPublicFolders != null || publicFolders.Count > 0)
			{
				List<SearchSource> list3 = new List<SearchSource>();
				IEnumerable<SearchSource> enumerable = (allPublicFolders == null) ? publicFolders : ((IEnumerable<SearchSource>)new SearchSource[]
				{
					allPublicFolders
				});
				Recorder.Trace(4L, TraceType.InfoTrace, "DirectoryQueryFormatting.ProcessPublicFolders PublicFolderList:", enumerable);
				foreach (SearchSource searchSource in sourceConverter.Convert(base.Policy, enumerable))
				{
					Recorder.Trace(4L, TraceType.InfoTrace, new object[]
					{
						"DirectoryQueryFormatting.ProcessPublicFolders Source:",
						searchSource.ReferenceId,
						"SourceType:",
						searchSource.SourceType
					});
					if (searchSource.SourceType != SourceType.PublicFolder)
					{
						QueryFilter sourceFilter = SearchRecipient.GetSourceFilter(searchSource);
						if (sourceFilter != null)
						{
							Recorder.Trace(4L, TraceType.InfoTrace, new object[]
							{
								"DirectoryQueryFormatting.ProcessPublicFolders Source:",
								searchSource.ReferenceId,
								"SourceType:",
								searchSource.SourceType,
								"Filter:",
								sourceFilter
							});
							list3.Add(searchSource);
							if (list2.Contains(searchSource.ReferenceId))
							{
								continue;
							}
							list.Add(sourceFilter);
							list2.Add(searchSource.ReferenceId);
							if (list.Count >= item.PageSize)
							{
								this.Enqueue(item, list3, list);
								list3.Clear();
								list2.Clear();
								list.Clear();
								continue;
							}
							continue;
						}
					}
					Recorder.Trace(4L, TraceType.ErrorTrace, "DirectoryQueryFormatting.ProcessPublicFolders FailedSource:", searchSource.ReferenceId);
					base.Executor.Fail(new SearchException(KnownError.ErrorSearchableObjectNotFound)
					{
						ErrorSource = searchSource
					});
				}
				this.Enqueue(item, list3, list);
			}
		}

		// Token: 0x0600038C RID: 908 RVA: 0x00017488 File Offset: 0x00015688
		private void Enqueue(DirectoryQueryParameters parameters, IEnumerable<SearchSource> sources, IEnumerable<QueryFilter> filters)
		{
			this.Enqueue(parameters, sources, SearchRecipient.CombineFilters(filters));
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00017498 File Offset: 0x00015698
		private void Enqueue(DirectoryQueryParameters parameters, IEnumerable<SearchSource> sources, QueryFilter filter)
		{
			Recorder.Trace(4L, TraceType.ErrorTrace, new object[]
			{
				"DirectoryQueryFormatting.Enque Sources:",
				sources,
				"Filter:",
				filter
			});
			List<SearchSource> list = sources.ToList<SearchSource>();
			if (list.Count > 0)
			{
				Recorder.Trace(4L, TraceType.ErrorTrace, "DirectoryQueryFormatting.Enque Count:", list.Count);
				DirectoryQueryParameters item = new DirectoryQueryParameters
				{
					ExpandPublicFolders = parameters.ExpandPublicFolders,
					ExpandGroups = parameters.ExpandGroups,
					MatchRecipientsToSources = parameters.MatchRecipientsToSources,
					PageSize = parameters.PageSize,
					Properties = parameters.Properties,
					RequestGroups = parameters.RequestGroups,
					Query = filter,
					Sources = list
				};
				base.Executor.EnqueueNext(item);
			}
		}

		// Token: 0x02000053 RID: 83
		private class SourceFilterPair
		{
			// Token: 0x17000122 RID: 290
			// (get) Token: 0x06000391 RID: 913 RVA: 0x0001756A File Offset: 0x0001576A
			// (set) Token: 0x06000392 RID: 914 RVA: 0x00017572 File Offset: 0x00015772
			public SearchSource Source { get; set; }

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x06000393 RID: 915 RVA: 0x0001757B File Offset: 0x0001577B
			// (set) Token: 0x06000394 RID: 916 RVA: 0x00017583 File Offset: 0x00015783
			public QueryFilter Filter { get; set; }
		}
	}
}
