using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.DataConverter;
using Microsoft.Exchange.Services.Core.Search;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002A9 RID: 681
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class BrowsePeopleInMailbox : FindPeopleImplementation
	{
		// Token: 0x06001248 RID: 4680 RVA: 0x00059A2E File Offset: 0x00057C2E
		public BrowsePeopleInMailbox(FindPeopleParameters parameters, MailboxSession mailboxSession, IdAndSession idAndSession) : base(parameters, BrowsePeopleInMailbox.AdditionalSupportedProperties, true)
		{
			ServiceCommandBase.ThrowIfNull(mailboxSession, "mailboxSession", "BrowsePeopleInMailbox::BrowsePeopleInMailbox");
			ServiceCommandBase.ThrowIfNull(idAndSession, "idAndSession", "BrowsePeopleInMailbox::BrowsePeopleInMailbox");
			this.mailboxSession = mailboxSession;
			this.idAndSession = idAndSession;
		}

		// Token: 0x06001249 RID: 4681 RVA: 0x00059A6B File Offset: 0x00057C6B
		public override void Validate()
		{
			base.Validate();
			if (base.AggregationRestriction != null)
			{
				throw new ServiceArgumentException((CoreResources.IDs)3784063568U);
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x00059A8B File Offset: 0x00057C8B
		protected override void ValidatePaging()
		{
			base.ValidatePaging();
			if (!(base.Paging is IndexedPageView) && !(base.Paging is SeekToConditionWithOffsetPageView))
			{
				throw new ServiceArgumentException(CoreResources.IDs.ErrorInvalidIndexedPagingParameters);
			}
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x00059AC0 File Offset: 0x00057CC0
		public override FindPeopleResult Execute()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			FindPeopleResult findPeopleResult = this.ExecuteInternal();
			stopwatch.Stop();
			base.Log(FindPeopleMetadata.PersonalSearchTime, stopwatch.ElapsedMilliseconds);
			base.Log(FindPeopleMetadata.PersonalCount, findPeopleResult.PersonaList.Length);
			base.Log(FindPeopleMetadata.TotalNumberOfPeopleInView, findPeopleResult.TotalNumberOfPeopleInView);
			return findPeopleResult;
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x00059B1C File Offset: 0x00057D1C
		private static void SeekToCondition(IQueryResult queryResult, SeekToConditionWithOffsetPageView pageView)
		{
			ServiceObjectToFilterConverter serviceObjectToFilterConverter = new ServiceObjectToFilterConverter();
			QueryFilter seekFilter = serviceObjectToFilterConverter.Convert(pageView.Condition.Item);
			queryResult.SeekToCondition((SeekReference)pageView.Origin, seekFilter);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x00059B50 File Offset: 0x00057D50
		private FindPeopleResult ExecuteInternal()
		{
			int totalNumberOfPeopleInView = -1;
			int num = -1;
			PropertyListForViewRowDeterminer propertyListForViewRowDeterminer = PropertyListForViewRowDeterminer.BuildForPersonObjects(base.PersonaShape);
			List<PropertyDefinition> list = new List<PropertyDefinition>(propertyListForViewRowDeterminer.GetPropertiesToFetch());
			SortBy[] sortColumns = Microsoft.Exchange.Services.Core.Search.SortResults.ToXsoSortBy(base.SortResults);
			QueryFilter queryFilter = base.GetRestrictionFilter();
			if (queryFilter != null)
			{
				queryFilter = BasePagingType.ApplyQueryAppend(queryFilter, base.Paging);
			}
			FindPeopleResult result;
			using (Folder folder = Folder.Bind(this.mailboxSession, this.idAndSession.Id, null))
			{
				IndexedPageView indexedPageView = null;
				using (IQueryResult queryResult = ((CoreFolder)folder.CoreObject).QueryExecutor.ItemQuery(ItemQueryType.ConversationView, queryFilter, sortColumns, BrowsePeopleInMailbox.conversationIdProperty))
				{
					if (base.Paging is IndexedPageView)
					{
						indexedPageView = (IndexedPageView)base.Paging;
					}
					else
					{
						SeekToConditionWithOffsetPageView seekToConditionWithOffsetPageView = (SeekToConditionWithOffsetPageView)base.Paging;
						BrowsePeopleInMailbox.SeekToCondition(queryResult, seekToConditionWithOffsetPageView);
						num = Math.Min(queryResult.CurrentRow, Math.Max(0, queryResult.EstimatedRowCount - 1));
						indexedPageView = new IndexedPageView
						{
							Offset = Math.Max(0, num + seekToConditionWithOffsetPageView.Offset),
							MaxRows = base.Paging.MaxRows
						};
					}
					totalNumberOfPeopleInView = queryResult.EstimatedRowCount;
				}
				using (IQueryResult queryResult2 = folder.PersonItemQuery(queryFilter, null, sortColumns, list))
				{
					BasePageResult basePageResult = BasePagingType.ApplyPostQueryPaging(queryResult2, indexedPageView);
					int offset = indexedPageView.Offset;
					Stopwatch stopwatch = Stopwatch.StartNew();
					Persona[] personaList = basePageResult.View.ConvertPersonViewToPersonaObjects(list.ToArray(), propertyListForViewRowDeterminer, this.idAndSession);
					stopwatch.Stop();
					base.Log(FindPeopleMetadata.PersonalDataConversion, stopwatch.ElapsedMilliseconds);
					result = FindPeopleResult.CreateMailboxBrowseResult(personaList, totalNumberOfPeopleInView, offset, num);
				}
			}
			return result;
		}

		// Token: 0x04000CFA RID: 3322
		private static readonly PropertyDefinition[] conversationIdProperty = new PropertyDefinition[]
		{
			ConversationItemSchema.ConversationId
		};

		// Token: 0x04000CFB RID: 3323
		private static readonly HashSet<PropertyPath> AdditionalSupportedProperties = new HashSet<PropertyPath>
		{
			PersonaSchema.DisplayNameFirstLastHeader.PropertyPath,
			PersonaSchema.DisplayNameLastFirstHeader.PropertyPath,
			PersonaSchema.ThirdPartyPhotoUrls.PropertyPath,
			PersonaSchema.Attributions.PropertyPath,
			PersonaSchema.Alias.PropertyPath,
			PersonaSchema.RelevanceScore.PropertyPath
		};

		// Token: 0x04000CFC RID: 3324
		private readonly MailboxSession mailboxSession;

		// Token: 0x04000CFD RID: 3325
		private readonly IdAndSession idAndSession;
	}
}
