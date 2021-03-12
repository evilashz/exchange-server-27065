using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;
using Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Task
{
	// Token: 0x0200005A RID: 90
	internal class ServerLookup : SearchTask<SearchSource>
	{
		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060003AE RID: 942 RVA: 0x000184B7 File Offset: 0x000166B7
		public ServerLookup.ServerLookupContext TaskContext
		{
			get
			{
				return (ServerLookup.ServerLookupContext)base.Context.TaskContext;
			}
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00018528 File Offset: 0x00016728
		public override void Process(IList<SearchSource> item)
		{
			Recorder.Trace(4L, TraceType.InfoTrace, "ServerLookup.Process Item:", item);
			item.FirstOrDefault<SearchSource>();
			IList<SearchSource> list = new List<SearchSource>();
			Func<SearchSource, string> func = delegate(SearchSource source)
			{
				if (source.MailboxInfo.IsRemoteMailbox)
				{
					return null;
				}
				if (source.MailboxInfo.IsArchive)
				{
					return source.MailboxInfo.ArchiveDatabase.ToString();
				}
				return source.MailboxInfo.MdbGuid.ToString();
			};
			foreach (SearchSource searchSource in item)
			{
				string text = func(searchSource);
				GroupId groupId;
				if (!string.IsNullOrEmpty(text) && this.TaskContext.LookupCache.TryGetValue(text, out groupId))
				{
					Recorder.Trace(4L, TraceType.InfoTrace, "ServerLookup.Process Cache Hit GroupId:", groupId);
					base.Executor.EnqueueNext(new FanoutParameters
					{
						GroupId = groupId,
						Source = searchSource
					});
				}
				else
				{
					list.Add(searchSource);
				}
			}
			if (list.Count > 0)
			{
				Recorder.Trace(4L, TraceType.InfoTrace, "ServerLookup.Process Cache Misses Count:", list.Count);
				IServerProvider serverProvider = SearchFactory.Current.GetServerProvider(base.Policy);
				foreach (FanoutParameters fanoutParameters in serverProvider.GetServer(base.Policy, list))
				{
					if (fanoutParameters.GroupId != null && fanoutParameters.GroupId.Uri != null && fanoutParameters.GroupId.GroupType != GroupType.SkippedError)
					{
						string text2 = func(fanoutParameters.Source);
						if (!string.IsNullOrEmpty(text2))
						{
							this.TaskContext.LookupCache.TryAdd(text2, fanoutParameters.GroupId);
						}
						base.Executor.EnqueueNext(fanoutParameters);
					}
					else
					{
						Recorder.Trace(4L, TraceType.InfoTrace, "ServerLookup.Process Ignoring an recipient group:", fanoutParameters.GroupId);
					}
				}
			}
		}

		// Token: 0x0200005B RID: 91
		public class ServerLookupContext
		{
			// Token: 0x060003B2 RID: 946 RVA: 0x00018714 File Offset: 0x00016914
			public ServerLookupContext()
			{
				this.LookupCache = new ConcurrentDictionary<string, GroupId>();
			}

			// Token: 0x17000129 RID: 297
			// (get) Token: 0x060003B3 RID: 947 RVA: 0x00018727 File Offset: 0x00016927
			// (set) Token: 0x060003B4 RID: 948 RVA: 0x0001872F File Offset: 0x0001692F
			public ConcurrentDictionary<string, GroupId> LookupCache { get; private set; }
		}
	}
}
