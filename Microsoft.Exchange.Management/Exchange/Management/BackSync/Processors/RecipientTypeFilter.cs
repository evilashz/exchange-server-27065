using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.BackSync;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000BD RID: 189
	internal class RecipientTypeFilter : PipelineProcessor
	{
		// Token: 0x06000600 RID: 1536 RVA: 0x0001A13A File Offset: 0x0001833A
		public RecipientTypeFilter(IDataProcessor next, RecipientTypeDetails acceptedRecipientTypes, ExcludedObjectReporter reporter) : base(next)
		{
			this.acceptedRecipientTypes = acceptedRecipientTypes;
			this.reporter = reporter;
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x0001A154 File Offset: 0x00018354
		protected override bool ProcessInternal(PropertyBag propertyBag)
		{
			if (!RecipientTypeFilter.HasObjectId(propertyBag))
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "RecipientTypeFilter:: - Skipping object {0}. No object id set on object.", new object[]
				{
					propertyBag[ADObjectSchema.Id]
				});
				this.reporter.ReportExcludedObject(propertyBag, DirectoryObjectErrorCode.ObjectOutOfScope, ProcessingStage.RecipientTypeFilter);
				return false;
			}
			if (ProcessorHelper.IsDeletedObject(propertyBag))
			{
				return !ProvisioningTasksConfigImpl.UseBecAPIsforLiveId || !ProcessorHelper.IsUserObject(propertyBag);
			}
			if (RecipientTypeFilter.IsObjectExcluded(propertyBag))
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug((long)SyncConfiguration.TraceId, "RecipientTypeFilter:: - Skipping object {0}. It's marked as excluded form backsync.", new object[]
				{
					propertyBag[ADObjectSchema.Id]
				});
				this.reporter.ReportExcludedObject(propertyBag, DirectoryObjectErrorCode.UnspecifiedError, ProcessingStage.RecipientTypeFilter);
				return false;
			}
			bool flag;
			if (!ProcessorHelper.IsObjectOrganizationUnit(propertyBag))
			{
				RecipientTypeDetails recipientTypeDetails = (RecipientTypeDetails)propertyBag[ADRecipientSchema.RecipientTypeDetails];
				flag = this.IsAcceptedRecipientType(recipientTypeDetails);
				if (!flag)
				{
					ExTraceGlobals.BackSyncTracer.TraceDebug<object, RecipientTypeDetails>((long)SyncConfiguration.TraceId, "RecipientTypeFilter:: - Skipping object {0}. Recipient type {1} is not included in backsync.", propertyBag[SyncObjectSchema.ObjectId], recipientTypeDetails);
					this.reporter.ReportExcludedObject(propertyBag, DirectoryObjectErrorCode.ObjectOutOfScope, ProcessingStage.RecipientTypeFilter);
				}
			}
			else
			{
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x0001A25C File Offset: 0x0001845C
		internal static bool HasObjectId(PropertyBag propertyBag)
		{
			return propertyBag.Contains(SyncObjectSchema.ObjectId) && !string.IsNullOrEmpty((string)propertyBag[SyncObjectSchema.ObjectId]) && RecipientTypeFilter.IsValidGuid((string)propertyBag[SyncObjectSchema.ObjectId]);
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x0001A299 File Offset: 0x00018499
		private static bool IsObjectExcluded(PropertyBag propertyBag)
		{
			return (bool)(propertyBag[ADRecipientSchema.ExcludedFromBackSync] ?? false);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0001A2B8 File Offset: 0x000184B8
		private static bool IsValidGuid(string objectId)
		{
			Guid a;
			try
			{
				a = new Guid(objectId);
			}
			catch (Exception arg)
			{
				ExTraceGlobals.BackSyncTracer.TraceDebug<string, Exception>((long)SyncConfiguration.TraceId, "RecipientTypeFilter:: - Skipping object {0}. Failed to convert object id to a guid. Exception: {1}.", objectId, arg);
				return false;
			}
			return a != Guid.Empty;
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0001A308 File Offset: 0x00018508
		private bool IsAcceptedRecipientType(RecipientTypeDetails recipientType)
		{
			return (this.acceptedRecipientTypes & recipientType) != RecipientTypeDetails.None;
		}

		// Token: 0x040002EB RID: 747
		private readonly RecipientTypeDetails acceptedRecipientTypes;

		// Token: 0x040002EC RID: 748
		private readonly ExcludedObjectReporter reporter;
	}
}
