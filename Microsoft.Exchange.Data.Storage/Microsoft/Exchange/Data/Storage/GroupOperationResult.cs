using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200021A RID: 538
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class GroupOperationResult
	{
		// Token: 0x060018B1 RID: 6321 RVA: 0x0007862C File Offset: 0x0007682C
		public GroupOperationResult(OperationResult operationResult, StoreObjectId[] objectIds, LocalizedException storageException, StoreObjectId[] resultObjectIds, byte[][] resultChangeKeys)
		{
			EnumValidator.ThrowIfInvalid<OperationResult>(operationResult, "operationResult");
			this.OperationResult = operationResult;
			this.ObjectIds = new ReadOnlyCollection<StoreObjectId>(objectIds);
			this.Exception = storageException;
			this.ResultObjectIds = new ReadOnlyCollection<StoreObjectId>(resultObjectIds);
			this.ResultChangeKeys = new ReadOnlyCollection<byte[]>(resultChangeKeys);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0007867E File Offset: 0x0007687E
		public GroupOperationResult(OperationResult operationResult, IList<StoreObjectId> objectIds, LocalizedException storageException)
		{
			EnumValidator.ThrowIfInvalid<OperationResult>(operationResult, "operationResult");
			this.OperationResult = operationResult;
			this.ObjectIds = new ReadOnlyCollection<StoreObjectId>(objectIds);
			this.Exception = storageException;
			this.ResultObjectIds = null;
			this.ResultChangeKeys = null;
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x000786B9 File Offset: 0x000768B9
		// (set) Token: 0x060018B4 RID: 6324 RVA: 0x000786C1 File Offset: 0x000768C1
		public IList<StoreObjectId> ObjectIds { get; private set; }

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x000786CA File Offset: 0x000768CA
		// (set) Token: 0x060018B6 RID: 6326 RVA: 0x000786D2 File Offset: 0x000768D2
		public IList<StoreObjectId> ResultObjectIds { get; private set; }

		// Token: 0x170007B5 RID: 1973
		// (get) Token: 0x060018B7 RID: 6327 RVA: 0x000786DB File Offset: 0x000768DB
		// (set) Token: 0x060018B8 RID: 6328 RVA: 0x000786E3 File Offset: 0x000768E3
		public IList<byte[]> ResultChangeKeys { get; private set; }

		// Token: 0x04000F81 RID: 3969
		public readonly LocalizedException Exception;

		// Token: 0x04000F82 RID: 3970
		public readonly OperationResult OperationResult;
	}
}
