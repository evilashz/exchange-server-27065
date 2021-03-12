using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000625 RID: 1573
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class COWResults
	{
		// Token: 0x060040D1 RID: 16593 RVA: 0x00110CC8 File Offset: 0x0010EEC8
		internal COWResults(StoreSession session, ICollection<StoreObjectId> itemIds)
		{
			this.session = session;
			if (itemIds != null)
			{
				this.itemIds = new StoreObjectId[itemIds.Count];
				itemIds.CopyTo(this.itemIds, 0);
				this.topOperationResults = new List<GroupOperationResult>(itemIds.Count);
			}
			else
			{
				this.topOperationResults = new List<GroupOperationResult>(0);
			}
			this.ResetPartialResults();
		}

		// Token: 0x060040D2 RID: 16594 RVA: 0x00110D28 File Offset: 0x0010EF28
		internal COWResults(StoreSession session, ICollection<StoreObjectId> itemIds, ConflictResolutionResult conflictResolutionResult) : this(session, itemIds)
		{
			this.conflictResolutionResult = conflictResolutionResult;
		}

		// Token: 0x060040D3 RID: 16595 RVA: 0x00110D3C File Offset: 0x0010EF3C
		internal COWResults(StoreSession session, StoreObjectId itemId, bool itemOperationSuccess) : this(session, new StoreObjectId[]
		{
			itemId
		})
		{
			this.itemOperationSuccess = itemOperationSuccess;
		}

		// Token: 0x060040D4 RID: 16596 RVA: 0x00110D63 File Offset: 0x0010EF63
		internal void ResetPartialResults()
		{
			this.partialOperationResults = new List<GroupOperationResult>(1);
		}

		// Token: 0x060040D5 RID: 16597 RVA: 0x00110D71 File Offset: 0x0010EF71
		internal List<GroupOperationResult> GetPartialResults()
		{
			return this.partialOperationResults;
		}

		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x00110D79 File Offset: 0x0010EF79
		internal ConflictResolutionResult ConflictResolutionResult
		{
			get
			{
				return this.conflictResolutionResult;
			}
		}

		// Token: 0x1700133A RID: 4922
		// (get) Token: 0x060040D7 RID: 16599 RVA: 0x00110D81 File Offset: 0x0010EF81
		internal bool ItemOperationSuccess
		{
			get
			{
				return this.itemOperationSuccess;
			}
		}

		// Token: 0x1700133B RID: 4923
		// (get) Token: 0x060040D8 RID: 16600 RVA: 0x00110D89 File Offset: 0x0010EF89
		// (set) Token: 0x060040D9 RID: 16601 RVA: 0x00110D91 File Offset: 0x0010EF91
		internal StoreObjectId CopyOnWriteGeneratedId
		{
			get
			{
				return this.copyOnWriteGeneratedId;
			}
			set
			{
				this.copyOnWriteGeneratedId = value;
			}
		}

		// Token: 0x1700133C RID: 4924
		// (get) Token: 0x060040DA RID: 16602 RVA: 0x00110D9A File Offset: 0x0010EF9A
		// (set) Token: 0x060040DB RID: 16603 RVA: 0x00110DA2 File Offset: 0x0010EFA2
		internal StoreObjectId CalendarLogGeneratedId
		{
			get
			{
				return this.calendarLogGeneratedId;
			}
			set
			{
				this.calendarLogGeneratedId = value;
			}
		}

		// Token: 0x060040DC RID: 16604 RVA: 0x00110DAC File Offset: 0x0010EFAC
		internal void AddPartialResult(GroupOperationResult result)
		{
			this.partialOperationResults.Add(result);
			if (ExTraceGlobals.SessionTracer.IsTraceEnabled(TraceType.WarningTrace))
			{
				if (result.OperationResult == OperationResult.PartiallySucceeded)
				{
					ExTraceGlobals.SessionTracer.TraceWarning<string, int>((long)this.session.GetHashCode(), "Dumpster internal operation partial success: exception {0},  item id count {1}.", (result.Exception != null) ? result.Exception.ToString() : "null", (result.ObjectIds != null) ? result.ObjectIds.Count : 0);
					return;
				}
				if (result.OperationResult == OperationResult.Failed)
				{
					ExTraceGlobals.SessionTracer.TraceWarning<string, int>((long)this.session.GetHashCode(), "Dumpster internal operation failure: exception {0},  item id count {1}.", (result.Exception != null) ? result.Exception.ToString() : "null", (result.ObjectIds != null) ? result.ObjectIds.Count : 0);
				}
			}
		}

		// Token: 0x060040DD RID: 16605 RVA: 0x00110E80 File Offset: 0x0010F080
		internal bool AnyPartialResultFailure()
		{
			foreach (GroupOperationResult groupOperationResult in this.partialOperationResults)
			{
				if (groupOperationResult.OperationResult == OperationResult.Failed)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060040DE RID: 16606 RVA: 0x00110EDC File Offset: 0x0010F0DC
		internal bool AnyResultNotSucceeded()
		{
			foreach (GroupOperationResult groupOperationResult in this.topOperationResults)
			{
				if (groupOperationResult.OperationResult == OperationResult.PartiallySucceeded || groupOperationResult.OperationResult == OperationResult.Failed)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060040DF RID: 16607 RVA: 0x00110F44 File Offset: 0x0010F144
		internal void AddResult(GroupOperationResult result)
		{
			this.topOperationResults.Add(result);
			if (ExTraceGlobals.SessionTracer.IsTraceEnabled(TraceType.WarningTrace))
			{
				if (result.OperationResult == OperationResult.PartiallySucceeded)
				{
					ExTraceGlobals.SessionTracer.TraceWarning<string, int>((long)this.session.GetHashCode(), "Dumpster top item operation partial success: exception {0},  item id count {1}.", (result.Exception != null) ? result.Exception.ToString() : "null", (result.ObjectIds != null) ? result.ObjectIds.Count : 0);
					return;
				}
				if (result.OperationResult == OperationResult.Failed)
				{
					ExTraceGlobals.SessionTracer.TraceWarning<string, int>((long)this.session.GetHashCode(), "Dumpster top item operation failure: exception {0},  item id count {1}.", (result.Exception != null) ? result.Exception.ToString() : "null", (result.ObjectIds != null) ? result.ObjectIds.Count : 0);
				}
			}
		}

		// Token: 0x060040E0 RID: 16608 RVA: 0x00111018 File Offset: 0x0010F218
		internal void AddResult(IEnumerable<GroupOperationResult> results)
		{
			if (results != null)
			{
				foreach (GroupOperationResult result in results)
				{
					this.AddResult(result);
				}
			}
		}

		// Token: 0x060040E1 RID: 16609 RVA: 0x00111064 File Offset: 0x0010F264
		internal GroupOperationResult GetResults()
		{
			OperationResult operationResult = OperationResult.Succeeded;
			LocalizedException storageException = null;
			foreach (GroupOperationResult groupOperationResult in this.topOperationResults)
			{
				if (groupOperationResult.OperationResult == OperationResult.Failed)
				{
					operationResult = OperationResult.Failed;
					storageException = groupOperationResult.Exception;
					break;
				}
				if (operationResult != OperationResult.PartiallySucceeded && groupOperationResult.OperationResult == OperationResult.PartiallySucceeded)
				{
					operationResult = OperationResult.PartiallySucceeded;
					storageException = groupOperationResult.Exception;
				}
			}
			return new GroupOperationResult(operationResult, this.itemIds, storageException);
		}

		// Token: 0x040023DA RID: 9178
		private readonly StoreSession session;

		// Token: 0x040023DB RID: 9179
		private readonly StoreObjectId[] itemIds;

		// Token: 0x040023DC RID: 9180
		private ConflictResolutionResult conflictResolutionResult;

		// Token: 0x040023DD RID: 9181
		private bool itemOperationSuccess;

		// Token: 0x040023DE RID: 9182
		private List<GroupOperationResult> partialOperationResults;

		// Token: 0x040023DF RID: 9183
		private List<GroupOperationResult> topOperationResults;

		// Token: 0x040023E0 RID: 9184
		private StoreObjectId copyOnWriteGeneratedId;

		// Token: 0x040023E1 RID: 9185
		private StoreObjectId calendarLogGeneratedId;
	}
}
