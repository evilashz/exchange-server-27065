using System;
using System.Threading;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000A9 RID: 169
	internal class ExcludedObjectReporter : IExcludedObjectReporter
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x00018618 File Offset: 0x00016818
		public virtual void ReportExcludedObject(PropertyBag propertyBag, DirectoryObjectErrorCode errorCode, ProcessingStage processingStage)
		{
			ExcludedObjectReporter.LogExcludedObject(ExcludedObjectReporter.GetId(propertyBag), errorCode, processingStage);
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00018627 File Offset: 0x00016827
		public virtual void ReportExcludedObject(SyncObjectId objectId, DirectoryObjectErrorCode errorCode, ProcessingStage processingStage)
		{
			ExcludedObjectReporter.LogExcludedObject(objectId.ToString(), errorCode, processingStage);
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x00018636 File Offset: 0x00016836
		public virtual DirectoryObjectError[] GetDirectoryObjectErrors()
		{
			return null;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0001863C File Offset: 0x0001683C
		private static string GetId(PropertyBag propertyBag)
		{
			if (propertyBag.Contains(ADObjectSchema.Id))
			{
				return ((ADObjectId)propertyBag[ADObjectSchema.Id]).DistinguishedName;
			}
			if (propertyBag.Contains(ADObjectSchema.DistinguishedName))
			{
				return (string)propertyBag[ADObjectSchema.DistinguishedName];
			}
			if (propertyBag.Contains(SyncObjectSchema.ObjectId))
			{
				return (string)propertyBag[SyncObjectSchema.ObjectId];
			}
			return string.Empty;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x000186AD File Offset: 0x000168AD
		private static void LogExcludedObject(string objectId, DirectoryObjectErrorCode errorCode, ProcessingStage processingStage)
		{
			ExTraceGlobals.BackSyncTracer.TraceDebug<string, DirectoryObjectErrorCode, ProcessingStage>((long)Thread.CurrentThread.ManagedThreadId, "Object '{0}' excluded from backsyc stream. Reason: {1}. Stage: {2}", objectId, errorCode, processingStage);
		}
	}
}
