using System;
using System.Collections.Generic;
using Microsoft.Ceres.Evaluation.DataModel.Types;
using Microsoft.Ceres.Evaluation.Operators;
using Microsoft.Ceres.Evaluation.Processing;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Mdb;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x02000012 RID: 18
	internal abstract class XSOProducerBase<TOperator> : MailboxProducerBase<TOperator> where TOperator : OperatorBase
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00005EF1 File Offset: 0x000040F1
		protected XSOProducerBase(TOperator operatorInstance, IRecordSetTypeDescriptor inputType, IEvaluationContext context, Trace tracer) : base(operatorInstance, inputType, context, tracer)
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005F00 File Offset: 0x00004100
		protected Item ItemBind(StoreSession session, MdbItemIdentity identity, ICollection<PropertyDefinition> propertiesToLoad, LapTimer stopwatch, out MdbItemIdentity newIdentity)
		{
			Item result = null;
			newIdentity = null;
			try
			{
				result = Item.Bind(session, identity.ItemId, ItemBindOption.None, propertiesToLoad);
				base.Tracer.TracePerformance<double>((long)base.TracingContext, "Time for Item.Bind: {0} ms", stopwatch.GetLapTime().TotalMilliseconds);
			}
			catch (ObjectNotFoundException)
			{
				base.Tracer.TracePerformance<double>((long)base.TracingContext, "Time until Item.Bind threw exception: {0} ms", stopwatch.GetLapTime().TotalMilliseconds);
				using (Folder rootFolder = XsoUtil.GetRootFolder(session))
				{
					QueryFilter queryFilter = new ComparisonFilter(ComparisonOperator.Equal, ItemSchema.DocumentId, identity.DocumentId);
					using (QueryResult queryResult = rootFolder.ItemQuery(ItemQueryType.DocumentIdView, queryFilter, null, new PropertyDefinition[]
					{
						StoreObjectSchema.EntryId,
						StoreObjectSchema.ItemClass
					}))
					{
						object[][] rows = queryResult.GetRows(1);
						if (rows.Length == 1)
						{
							base.Tracer.TracePerformance<double>((long)base.TracingContext, "Time to ItemQuery/GetRows: {0} ms", stopwatch.GetLapTime().TotalMilliseconds);
							StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId((byte[])rows[0][0], ObjectClass.GetObjectType((string)rows[0][1]));
							newIdentity = new MdbItemIdentity(identity.PersistableTenantId, identity.GetMdbGuid(MdbItemIdentity.Location.FastCatalog), identity.MailboxGuid, identity.MailboxNumber, storeObjectId, identity.DocumentId, identity.IsPublicFolder);
							result = Item.Bind(session, storeObjectId, ItemBindOption.None, propertiesToLoad);
							base.Tracer.TracePerformance<double>((long)base.TracingContext, "Time for Item.Bind after ItemQuery: {0} ms", stopwatch.GetLapTime().TotalMilliseconds);
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000060E0 File Offset: 0x000042E0
		protected StoreSession GetStoreSession(MdbItemIdentity identity, bool isMoveDestination, bool isLocalMdb, bool sessionShouldBeOpenedForRMS, LapTimer stopwatch, out EvaluationErrors error, out Exception exception)
		{
			StoreSession result = null;
			try
			{
				result = StoreSessionManager.GetStoreSessionFromCache(identity, isMoveDestination, sessionShouldBeOpenedForRMS, isLocalMdb);
				base.Tracer.TracePerformance<double>((long)base.TracingContext, "Time to get the mailbox session: {0} ms", stopwatch.GetLapTime().TotalMilliseconds);
				exception = null;
				error = EvaluationErrors.None;
			}
			catch (UnavailableSessionException ex)
			{
				exception = ex;
				error = EvaluationErrors.SessionUnavailable;
			}
			catch (MailboxLockedException ex2)
			{
				exception = ex2;
				error = EvaluationErrors.MailboxLocked;
			}
			catch (MailboxQuarantinedException ex3)
			{
				exception = ex3;
				error = EvaluationErrors.MailboxQuarantined;
			}
			catch (MailboxLoginFailedException ex4)
			{
				exception = ex4;
				error = EvaluationErrors.LoginFailed;
			}
			if (error != EvaluationErrors.None)
			{
				base.Tracer.TraceError<EvaluationErrors>((long)base.TracingContext, "Could not get store session, error: {0}", error);
			}
			return result;
		}
	}
}
