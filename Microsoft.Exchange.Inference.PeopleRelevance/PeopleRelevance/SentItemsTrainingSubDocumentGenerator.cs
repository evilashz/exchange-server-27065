using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Inference;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Inference.Common;
using Microsoft.Exchange.Inference.Mdb;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000022 RID: 34
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class SentItemsTrainingSubDocumentGenerator
	{
		// Token: 0x06000125 RID: 293 RVA: 0x00006B8C File Offset: 0x00004D8C
		public SentItemsTrainingSubDocumentGenerator()
		{
			this.NumberOfDaysToSkip = 0;
			this.ItemsToFetchForDefaultModel = 400;
			this.ItemsToFetchForTrainedModel = 1000;
			this.DiagnosticsSession = Microsoft.Exchange.Search.Core.Diagnostics.DiagnosticsSession.CreateComponentDiagnosticsSession("SentItemsTrainingSubDocumentGenerator", ExTraceGlobals.MdbTrainingFeederTracer, (long)this.GetHashCode());
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006BD8 File Offset: 0x00004DD8
		// (set) Token: 0x06000127 RID: 295 RVA: 0x00006BE0 File Offset: 0x00004DE0
		public IDiagnosticsSession DiagnosticsSession { get; private set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006BE9 File Offset: 0x00004DE9
		// (set) Token: 0x06000129 RID: 297 RVA: 0x00006BF1 File Offset: 0x00004DF1
		public int NumberOfDaysToSkip { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00006BFA File Offset: 0x00004DFA
		// (set) Token: 0x0600012B RID: 299 RVA: 0x00006C02 File Offset: 0x00004E02
		public int ItemsToFetchForDefaultModel { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00006C0B File Offset: 0x00004E0B
		// (set) Token: 0x0600012D RID: 301 RVA: 0x00006C13 File Offset: 0x00004E13
		public int ItemsToFetchForTrainedModel { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006C1C File Offset: 0x00004E1C
		// (set) Token: 0x0600012F RID: 303 RVA: 0x00006C24 File Offset: 0x00004E24
		public CrawlerItemIterator<ExDateTime> ItemIterator { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006C2D File Offset: 0x00004E2D
		// (set) Token: 0x06000131 RID: 305 RVA: 0x00006C35 File Offset: 0x00004E35
		public ICrawlerFolderIterator FolderIterator { get; private set; }

		// Token: 0x06000132 RID: 306 RVA: 0x00006C40 File Offset: 0x00004E40
		internal IDocument RunTrainingQuery(MailboxSession session, PeopleModelItem modelItem)
		{
			int num = this.IsDefaultModel(modelItem) ? this.ItemsToFetchForDefaultModel : this.ItemsToFetchForTrainedModel;
			this.InitializeForQuery(num);
			ExDateTime queryWindowStartTime = this.GetQueryWindowStartTime(modelItem);
			ExDateTime queryWindowEndTime = this.GetQueryWindowEndTime(modelItem);
			IDocument document = MdbInferenceFactory.Current.CreateTrainingSubDocument(this.GetItems(session, queryWindowStartTime, queryWindowEndTime, modelItem), num, session.MailboxGuid, session.MdbGuid);
			this.DiagnosticsSession.TraceDebug<int>("CrawlerItemIterator returned the following number of items for training {0}", document.NestedDocuments.Count);
			return document;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00006CCC File Offset: 0x00004ECC
		private void InitializeForQuery(int totalItemsToFetch)
		{
			this.DiagnosticsSession.TraceDebug<int>("Creating CrawlerIterator with selection criteria for default model. Batch Size = {0}", totalItemsToFetch);
			this.FolderIterator = new SentItemsTrainingSubDocumentGenerator.SentItemsFolderIterator();
			this.ItemIterator = new CrawlerItemIterator<ExDateTime>(this.FolderIterator, totalItemsToFetch, ItemSchema.SentTime, (object[] values) => ObjectClass.IsMessage(values[0] as string, false), new PropertyDefinition[]
			{
				StoreObjectSchema.ItemClass
			});
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00006D39 File Offset: 0x00004F39
		private IEnumerable<MdbCompositeItemIdentity> GetItems(MailboxSession session, ExDateTime startTime, ExDateTime endTime, PeopleModelItem modelItem)
		{
			return this.ItemIterator.GetItems(session, startTime, endTime);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00006D49 File Offset: 0x00004F49
		private ExDateTime GetQueryWindowStartTime(PeopleModelItem modelItem)
		{
			if (!modelItem.IsDefaultModel)
			{
				return this.LastProcessedTime(modelItem);
			}
			return ExDateTime.UtcNow - SentItemsTrainingSubDocumentGenerator.DefaultTimeSpanBacklogToProcess;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00006D6C File Offset: 0x00004F6C
		private ExDateTime GetQueryWindowEndTime(PeopleModelItem modelItem)
		{
			return ExDateTime.UtcNow.AddDays((double)(-(double)this.NumberOfDaysToSkip)).ToUtc();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00006D96 File Offset: 0x00004F96
		private bool IsDefaultModel(PeopleModelItem modelItem)
		{
			return modelItem.IsDefaultModel;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00006DA0 File Offset: 0x00004FA0
		private ExDateTime LastProcessedTime(PeopleModelItem modelItem)
		{
			return new ExDateTime(ExTimeZone.UtcTimeZone, modelItem.LastProcessedMessageSentTime.ToUniversalTime());
		}

		// Token: 0x0400008B RID: 139
		private const string ComponentName = "SentItemsTrainingSubDocumentGenerator";

		// Token: 0x0400008C RID: 140
		private const int DefaultBatchSizeForDefaultModel = 400;

		// Token: 0x0400008D RID: 141
		private const int DefaultBatchSizeForTrainedModel = 1000;

		// Token: 0x0400008E RID: 142
		private const int DefaultDaysToSkipFromCurrent = 0;

		// Token: 0x0400008F RID: 143
		internal static readonly TimeSpan DefaultTimeSpanBacklogToProcess = TimeSpan.FromDays(180.0);

		// Token: 0x02000023 RID: 35
		private class SentItemsFolderIterator : ICrawlerFolderIterator
		{
			// Token: 0x0600013B RID: 315 RVA: 0x00006EB8 File Offset: 0x000050B8
			public IEnumerable<StoreObjectId> GetFolders(MailboxSession session)
			{
				yield return session.GetDefaultFolderId(DefaultFolderType.SentItems);
				yield break;
			}
		}
	}
}
