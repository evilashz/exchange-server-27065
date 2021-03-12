using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Infrastructure;
using Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Model;

namespace Microsoft.Exchange.EDiscovery.MailboxSearch.WebService.Task
{
	// Token: 0x02000055 RID: 85
	internal class InitializeGetSearchablebleMailbox : SearchTask<GetSearchableMailboxesInputs>
	{
		// Token: 0x0600039E RID: 926 RVA: 0x00017A54 File Offset: 0x00015C54
		public override void Process(GetSearchableMailboxesInputs item)
		{
			Recorder.Trace(4L, TraceType.InfoTrace, new object[]
			{
				"InitializeGetSearchablebleMailbox.Process Item:",
				item,
				"Filter:",
				item.Filter,
				"ExpandGroups:",
				item.ExpandGroups
			});
			SearchSource[] source = new SearchSource[]
			{
				new SearchSource
				{
					ReferenceId = item.Filter,
					SourceType = SourceType.AllMailboxes
				}
			};
			DirectoryQueryParameters item2 = new DirectoryQueryParameters
			{
				ExpandPublicFolders = false,
				ExpandGroups = item.ExpandGroups,
				MatchRecipientsToSources = false,
				PageSize = (int)base.Policy.ExecutionSettings.DiscoveryDisplaySearchPageSize,
				Properties = SearchRecipient.DisplayProperties,
				RequestGroups = true,
				Sources = source.ToList<SearchSource>()
			};
			base.Executor.EnqueueNext(item2);
		}
	}
}
