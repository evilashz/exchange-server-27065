using System;
using Microsoft.Ceres.Evaluation.DataModel;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.DataModel.Types.BuiltIn;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Mdb;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.Search.TokenOperators;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000013 RID: 19
	internal class FolderUpdateRetrieverProducer : XSOProducerBase<FolderUpdateRetrieverOperator>
	{
		// Token: 0x060000DA RID: 218 RVA: 0x000061B4 File Offset: 0x000043B4
		public FolderUpdateRetrieverProducer(FolderUpdateRetrieverOperator retrieverOperator, IRecordSetTypeDescriptor inputType, IEvaluationContext context) : base(retrieverOperator, inputType, context, ExTraceGlobals.RetrieverOperatorTracer)
		{
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000DB RID: 219 RVA: 0x000061C4 File Offset: 0x000043C4
		// (set) Token: 0x060000DC RID: 220 RVA: 0x000061CC File Offset: 0x000043CC
		internal IUpdateableRecord Holder { get; private set; }

		// Token: 0x060000DD RID: 221 RVA: 0x000061D8 File Offset: 0x000043D8
		public override void InternalProcessRecord(IRecord record)
		{
			LapTimer lapTimer = new LapTimer();
			this.Holder.UpdateFrom(record, record.Descriptor.Count);
			base.Tracer.TraceDebug<Guid>((long)base.TracingContext, "Begin processing record with CorrelationId: {0}", this.correlationIdField.PrimitiveGuidValue);
			MdbItemIdentity mdbItemIdentity = MdbItemIdentity.Parse(this.compositeItemIdField.StringValue);
			Guid mdbGuid = mdbItemIdentity.GetMdbGuid(MdbItemIdentity.Location.ExchangeMdb);
			string stringValue = this.instanceNameField.StringValue;
			this.SetInstance(mdbGuid, stringValue ?? mdbGuid.ToString());
			bool booleanValue = this.isMoveDestinationField.BooleanValue;
			bool booleanValue2 = this.isLocalMdbField.BooleanValue;
			lapTimer.GetLapTime();
			this.midField.Int64Value = 0L;
			this.isReadField.BooleanValue = false;
			this.folderUpdateErrorCodeField.Int32Value = 0;
			this.folderUpdateErrorMessageField.StringValue = string.Empty;
			this.folderUpdateMailboxGuidField.StringValue = mdbItemIdentity.MailboxGuid.ToString();
			ItemDepot.Instance.SaveAssociatedRecordInformation(base.FlowIdentifier, this.correlationIdField.PrimitiveGuidValue, IndexId.CreateIndexId(mdbItemIdentity.MailboxNumber, mdbItemIdentity.DocumentId), mdbItemIdentity.MailboxGuid, mdbItemIdentity, 0, 0, this.indexSystemNameField.StringValue);
			int arg = 0;
			bool flag;
			do
			{
				flag = false;
				bool discard = true;
				EvaluationErrors evaluationErrors;
				Exception ex;
				StoreSession storeSession = base.GetStoreSession(mdbItemIdentity, booleanValue, booleanValue2, false, lapTimer, out evaluationErrors, out ex);
				try
				{
					if (evaluationErrors != EvaluationErrors.None)
					{
						this.folderUpdateErrorCodeField.Int32Value = (int)evaluationErrors;
						this.folderUpdateErrorMessageField.StringValue = ((ex != null) ? ex.ToString() : evaluationErrors.ToString());
						base.SetNextRecord(this.Holder);
						return;
					}
					byte[] parentEntryIdFromMessageEntryId = IdConverter.GetParentEntryIdFromMessageEntryId(mdbItemIdentity.ItemId.ProviderLevelItemId);
					this.folderIdField.StringValue = FolderIdHelper.GetIndexForFolderEntryId(parentEntryIdFromMessageEntryId);
					this.midField.Int64Value = storeSession.IdConverter.GetMidFromMessageId(mdbItemIdentity.ItemId);
					this.isReadField.BooleanValue = true;
					discard = false;
				}
				catch (ConnectionFailedTransientException)
				{
					if (arg++ == base.Config.MaxRetryCount)
					{
						throw;
					}
					base.Tracer.TraceDebug<int, int, int>((long)base.TracingContext, "Retry opening message of Document Id {0}. ({1}/{2})", mdbItemIdentity.DocumentId, arg, base.Config.MaxRetryCount);
					flag = true;
				}
				finally
				{
					StoreSessionManager.ReturnStoreSessionToCache(ref storeSession, discard);
				}
			}
			while (flag);
			TimeSpan splitTime = lapTimer.GetSplitTime();
			base.Tracer.TracePerformance<double>((long)base.TracingContext, "Time using the item (including bind): {0} ms", splitTime.TotalMilliseconds);
			base.SetNextRecord(this.Holder);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00006474 File Offset: 0x00004674
		public override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<FolderUpdateRetrieverProducer>(this);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000647C File Offset: 0x0000467C
		protected override void Initialize()
		{
			base.Initialize();
			this.recordImpl = RecordImplDescriptors.StandardImplementation(base.OutputType.RecordType);
			this.Holder = this.recordImpl.CreateUpdatableRecord();
			base.OutputProperties = new RecordSetProperties(base.OutputType, this.Holder, this.recordImpl);
			this.correlationIdField = (IGuidField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.CorrelationId)];
			this.indexSystemNameField = (IStringField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.IndexSystemName)];
			this.instanceNameField = (IStringField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.InstanceName)];
			this.isMoveDestinationField = (IBooleanField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.IsMoveDestination)];
			this.isLocalMdbField = (IBooleanField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.IsLocalMdb)];
			this.compositeItemIdField = (IUpdateableStringField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.CompositeItemId)];
			this.folderIdField = (IUpdateableStringField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.FolderId)];
			this.midField = (IUpdateableInt64Field)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.MidField)];
			this.isReadField = (IUpdateableBooleanField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.IsReadField)];
			this.folderUpdateErrorCodeField = (IUpdateableInt32Field)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.FolderUpdateErrorCodeField)];
			this.folderUpdateErrorMessageField = (IUpdateableStringField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.FolderUpdateErrorMessageField)];
			this.folderUpdateMailboxGuidField = (IUpdateableStringField)this.Holder[base.InputType.RecordType.Position(base.OperatorInstance.FolderUpdateMailboxGuidField)];
		}

		// Token: 0x040000D4 RID: 212
		private IRecordImplDescriptor recordImpl;

		// Token: 0x040000D5 RID: 213
		private IGuidField correlationIdField;

		// Token: 0x040000D6 RID: 214
		private IStringField indexSystemNameField;

		// Token: 0x040000D7 RID: 215
		private IStringField instanceNameField;

		// Token: 0x040000D8 RID: 216
		private IUpdateableStringField compositeItemIdField;

		// Token: 0x040000D9 RID: 217
		private IBooleanField isLocalMdbField;

		// Token: 0x040000DA RID: 218
		private IBooleanField isMoveDestinationField;

		// Token: 0x040000DB RID: 219
		private IUpdateableStringField folderIdField;

		// Token: 0x040000DC RID: 220
		private IUpdateableInt64Field midField;

		// Token: 0x040000DD RID: 221
		private IUpdateableBooleanField isReadField;

		// Token: 0x040000DE RID: 222
		private IUpdateableInt32Field folderUpdateErrorCodeField;

		// Token: 0x040000DF RID: 223
		private IUpdateableStringField folderUpdateErrorMessageField;

		// Token: 0x040000E0 RID: 224
		private IUpdateableStringField folderUpdateMailboxGuidField;
	}
}
