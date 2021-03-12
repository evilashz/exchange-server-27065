using System;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x020007BF RID: 1983
	internal interface IExcludedObjectReporter
	{
		// Token: 0x0600628C RID: 25228
		void ReportExcludedObject(PropertyBag propertyBag, DirectoryObjectErrorCode errorCode, ProcessingStage processingStage);

		// Token: 0x0600628D RID: 25229
		void ReportExcludedObject(SyncObjectId objectId, DirectoryObjectErrorCode errorCode, ProcessingStage processingStage);

		// Token: 0x0600628E RID: 25230
		DirectoryObjectError[] GetDirectoryObjectErrors();
	}
}
