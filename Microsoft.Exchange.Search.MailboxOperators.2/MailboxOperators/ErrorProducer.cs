using System;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.RpcEndpoint;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x0200000B RID: 11
	internal class ErrorProducer : ErrorProducerBase
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00002691 File Offset: 0x00000891
		public ErrorProducer(ErrorOperator errorOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(errorOperator, inputType, context)
		{
			this.tracingContext = this.GetHashCode();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000026A8 File Offset: 0x000008A8
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<ErrorProducer>(this);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000026B0 File Offset: 0x000008B0
		protected override long GetDocumentId(IIdentity identity)
		{
			MdbItemIdentity mdbItemIdentity = (MdbItemIdentity)identity;
			return IndexId.CreateIndexId(mdbItemIdentity.MailboxNumber, mdbItemIdentity.DocumentId);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026D8 File Offset: 0x000008D8
		protected override string GetMailboxId(IIdentity identity)
		{
			MdbItemIdentity mdbItemIdentity = (MdbItemIdentity)identity;
			return mdbItemIdentity.MailboxGuid.ToString();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00002700 File Offset: 0x00000900
		protected override string GetFolderId(IIdentity identity)
		{
			MdbItemIdentity mdbItemIdentity = (MdbItemIdentity)identity;
			byte[] parentEntryIdFromMessageEntryId = IdConverter.GetParentEntryIdFromMessageEntryId(mdbItemIdentity.ItemId.ProviderLevelItemId);
			return FolderIdHelper.GetIndexForFolderEntryId(parentEntryIdFromMessageEntryId);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000272B File Offset: 0x0000092B
		protected override int MapErrorCodeFromException(Exception e)
		{
			if (e is MailboxUnavailableException || e is WrongServerException)
			{
				return 4;
			}
			if (e is UnavailableSessionException)
			{
				return 11;
			}
			if (e is ConversionFailedException)
			{
				return 16;
			}
			return 1;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002758 File Offset: 0x00000958
		protected override void CleanupExchangeResources(Guid correlationId, Exception exception)
		{
			ExTraceGlobals.ErrorOperatorTracer.TraceDebug<Guid>((long)this.tracingContext, "Begin: Disposing items associated with correlation id: {0}", correlationId);
			ItemDepot.Instance.DisposeItems(base.FlowIdentifier);
			ItemDepot.Instance.ClearAssociatedRecordInformation(base.FlowIdentifier);
			ExTraceGlobals.ErrorOperatorTracer.TraceDebug<Guid>((long)this.tracingContext, "End: Disposing items associated with correlation id: {0}", correlationId);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000027B4 File Offset: 0x000009B4
		protected override void HandleEndlessParsingException(IIdentity identity, Guid correlationId, string errorMessage)
		{
			SearchServiceRpcClient searchServiceRpcClient = null;
			try
			{
				MdbItemIdentity mdbItemIdentity = (MdbItemIdentity)identity;
				long documentId = this.GetDocumentId(identity);
				searchServiceRpcClient = RpcConnectionPool.GetSearchRpcClient();
				searchServiceRpcClient.RecordDocumentFailure(mdbItemIdentity.GetMdbGuid(MdbItemIdentity.Location.ExchangeMdb), correlationId, documentId, errorMessage);
			}
			finally
			{
				if (searchServiceRpcClient != null)
				{
					RpcConnectionPool.ReturnSearchRpcClientToCache(ref searchServiceRpcClient, true);
				}
				Environment.Exit(1);
			}
		}

		// Token: 0x04000008 RID: 8
		private readonly int tracingContext;
	}
}
