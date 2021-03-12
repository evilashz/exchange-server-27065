using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Management.BackSync.Configuration;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000AA RID: 170
	internal class FullSyncObjectErrorReporter : ExcludedObjectReporter
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x000186D4 File Offset: 0x000168D4
		public FullSyncObjectErrorReporter(PerformanceCounterSession performanceCounterSession)
		{
			this.performanceCounterSession = performanceCounterSession;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x000186EE File Offset: 0x000168EE
		public override DirectoryObjectError[] GetDirectoryObjectErrors()
		{
			return this.errors.ToArray();
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x000186FC File Offset: 0x000168FC
		public override void ReportExcludedObject(SyncObjectId objectId, DirectoryObjectErrorCode errorCode, ProcessingStage processingStage)
		{
			base.ReportExcludedObject(objectId, errorCode, processingStage);
			DirectoryObjectError directoryObjectError = new DirectoryObjectError();
			directoryObjectError.ErrorCode = errorCode;
			directoryObjectError.ObjectId = objectId.ObjectId;
			directoryObjectError.ContextId = objectId.ContextId;
			directoryObjectError.ObjectClass = objectId.ObjectClass;
			directoryObjectError.ObjectClassSpecified = true;
			this.errors.Add(directoryObjectError);
			if (this.performanceCounterSession != null)
			{
				this.performanceCounterSession.IncrementUserError();
			}
		}

		// Token: 0x060005AD RID: 1453 RVA: 0x0001876C File Offset: 0x0001696C
		public override void ReportExcludedObject(PropertyBag propertyBag, DirectoryObjectErrorCode errorCode, ProcessingStage processingStage)
		{
			SyncObjectId syncObjectId = FullSyncObjectErrorReporter.GetSyncObjectId(propertyBag);
			if (syncObjectId != null)
			{
				this.ReportExcludedObject(syncObjectId, errorCode, processingStage);
				return;
			}
			base.ReportExcludedObject(propertyBag, errorCode, processingStage);
			DirectoryObjectError directoryObjectError = new DirectoryObjectError();
			directoryObjectError.ErrorCode = errorCode;
			directoryObjectError.ObjectId = (string)propertyBag[SyncObjectSchema.ObjectId];
			if (propertyBag.Contains(ADObjectSchema.ObjectClass))
			{
				directoryObjectError.ObjectClass = SyncRecipient.GetRecipientType(propertyBag);
			}
			if (propertyBag.Contains(SyncObjectSchema.ContextId))
			{
				directoryObjectError.ContextId = (string)propertyBag[SyncObjectSchema.ContextId];
			}
			this.errors.Add(directoryObjectError);
			if (this.performanceCounterSession != null)
			{
				this.performanceCounterSession.IncrementUserError();
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x0001881A File Offset: 0x00016A1A
		private static SyncObjectId GetSyncObjectId(PropertyBag propertyBag)
		{
			return (SyncObjectId)propertyBag[SyncObjectSchema.SyncObjectId];
		}

		// Token: 0x040002CB RID: 715
		private readonly List<DirectoryObjectError> errors = new List<DirectoryObjectError>();

		// Token: 0x040002CC RID: 716
		private PerformanceCounterSession performanceCounterSession;
	}
}
