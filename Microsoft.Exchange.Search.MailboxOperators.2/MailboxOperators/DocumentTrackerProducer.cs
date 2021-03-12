using System;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Search.Core.RpcEndpoint;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000007 RID: 7
	internal class DocumentTrackerProducer : ExchangeProducerBase<DocumentTrackerOperator>
	{
		// Token: 0x06000019 RID: 25 RVA: 0x0000243F File Offset: 0x0000063F
		public DocumentTrackerProducer(DocumentTrackerOperator documentTrackerOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(documentTrackerOperator, inputType, context, ExTraceGlobals.DocumentTrackerOperatorTracer)
		{
			this.flowIdentiferGuid = Guid.Parse(base.FlowIdentifier);
			RpcConnectionPool.RegisterCaller();
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001A RID: 26 RVA: 0x00002465 File Offset: 0x00000665
		// (set) Token: 0x0600001B RID: 27 RVA: 0x0000246D File Offset: 0x0000066D
		public int CorrelationIdIndex { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001C RID: 28 RVA: 0x00002476 File Offset: 0x00000676
		// (set) Token: 0x0600001D RID: 29 RVA: 0x0000247E File Offset: 0x0000067E
		public int CompositeItemIdIndex { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002487 File Offset: 0x00000687
		// (set) Token: 0x0600001F RID: 31 RVA: 0x0000248F File Offset: 0x0000068F
		public int DocumentIdIndex { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002498 File Offset: 0x00000698
		protected override bool StartOfFlow
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000249B File Offset: 0x0000069B
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<DocumentTrackerProducer>(this);
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024A4 File Offset: 0x000006A4
		public override void InternalProcessRecord(IRecord record)
		{
			if (record == null)
			{
				throw new ArgumentNullException("record");
			}
			if (base.Config.DocumentTrackingEnabled)
			{
				Guid primitiveGuidValue = ((IGuidField)record[this.CorrelationIdIndex]).PrimitiveGuidValue;
				long? nullableInt64Value = ((IInt64Field)record[this.DocumentIdIndex]).NullableInt64Value;
				if (nullableInt64Value != null)
				{
					string stringValue = ((IStringField)record[this.CompositeItemIdIndex]).StringValue;
					MdbItemIdentity mdbItemIdentity = MdbItemIdentity.Parse(stringValue);
					if (mdbItemIdentity.MailboxNumber > 99)
					{
						Guid mdbGuid = mdbItemIdentity.GetMdbGuid(MdbItemIdentity.Location.ExchangeMdb);
						SearchServiceRpcClient searchServiceRpcClient = null;
						bool discard = false;
						try
						{
							searchServiceRpcClient = RpcConnectionPool.GetSearchRpcClient();
							searchServiceRpcClient.RecordDocumentProcessing(mdbGuid, this.flowIdentiferGuid, primitiveGuidValue, nullableInt64Value.Value);
						}
						catch (RpcException arg)
						{
							base.Tracer.TraceError<Guid, RpcException>((long)base.TracingContext, "CorrelationId: {0} with Exception: {1}", primitiveGuidValue, arg);
							discard = true;
						}
						finally
						{
							if (searchServiceRpcClient != null)
							{
								RpcConnectionPool.ReturnSearchRpcClientToCache(ref searchServiceRpcClient, discard);
							}
						}
					}
				}
			}
			base.SetNextRecord(record);
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000025B4 File Offset: 0x000007B4
		protected override void Initialize()
		{
			base.Initialize();
			this.CorrelationIdIndex = base.InputType.RecordType.Position(base.Operator.CorrelationId);
			this.CompositeItemIdIndex = base.InputType.RecordType.Position(base.Operator.CompositeItemId);
			this.DocumentIdIndex = base.InputType.RecordType.Position(base.Operator.DocumentId);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000262A File Offset: 0x0000082A
		protected override void ReleaseManagedResources()
		{
			RpcConnectionPool.UnRegisterCaller();
			base.ReleaseManagedResources();
		}

		// Token: 0x04000004 RID: 4
		private readonly Guid flowIdentiferGuid;
	}
}
