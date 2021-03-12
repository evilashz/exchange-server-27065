using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000C1 RID: 193
	internal class VerboseObjectErrorReporter : ExcludedObjectReporter
	{
		// Token: 0x06000613 RID: 1555 RVA: 0x0001A67E File Offset: 0x0001887E
		public VerboseObjectErrorReporter(Task.TaskVerboseLoggingDelegate verboseLoggingDelegate)
		{
			this.verboseLoggingDelegate = verboseLoggingDelegate;
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001A68D File Offset: 0x0001888D
		public override void ReportExcludedObject(SyncObjectId objectId, DirectoryObjectErrorCode errorCode, ProcessingStage processingStage)
		{
			base.ReportExcludedObject(objectId, errorCode, processingStage);
			this.verboseLoggingDelegate(Strings.BackSyncObjectExcluded(objectId.ToString(), errorCode, processingStage));
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001A6B0 File Offset: 0x000188B0
		public override void ReportExcludedObject(PropertyBag propertyBag, DirectoryObjectErrorCode errorCode, ProcessingStage processingStage)
		{
			base.ReportExcludedObject(propertyBag, errorCode, processingStage);
			DirectoryObjectClass directoryObjectClass = DirectoryObjectClass.Account;
			if (propertyBag.Contains(ADObjectSchema.ObjectClass))
			{
				directoryObjectClass = SyncRecipient.GetRecipientType(propertyBag);
			}
			ADObjectId adObjectId = (ADObjectId)propertyBag[ADObjectSchema.Id];
			string objectId = (string)propertyBag[SyncObjectSchema.ObjectId];
			this.verboseLoggingDelegate(Strings.BackSyncObjectExcludedExtended(directoryObjectClass, objectId, adObjectId, errorCode, processingStage));
		}

		// Token: 0x040002F3 RID: 755
		private Task.TaskVerboseLoggingDelegate verboseLoggingDelegate;
	}
}
