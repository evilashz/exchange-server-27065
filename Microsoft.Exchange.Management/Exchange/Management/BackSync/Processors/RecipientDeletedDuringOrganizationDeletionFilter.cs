using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.BackSync.Processors
{
	// Token: 0x020000BC RID: 188
	internal class RecipientDeletedDuringOrganizationDeletionFilter : PipelineProcessor
	{
		// Token: 0x060005FC RID: 1532 RVA: 0x00019FF4 File Offset: 0x000181F4
		public RecipientDeletedDuringOrganizationDeletionFilter(IDataProcessor next, IPropertyLookup organizationLookup, ExcludedObjectReporter reporter) : base(next)
		{
			this.organizationLookup = organizationLookup;
			this.reporter = reporter;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x0001A00C File Offset: 0x0001820C
		protected override bool ProcessInternal(PropertyBag propertyBag)
		{
			if (!ProcessorHelper.IsDeletedObject(propertyBag))
			{
				return true;
			}
			ADObjectId tenantOU = ProcessorHelper.GetTenantOU(propertyBag);
			ADRawEntry properties = this.organizationLookup.GetProperties(tenantOU);
			if (!RecipientDeletedDuringOrganizationDeletionFilter.WasOrganizationForThisObjectDeleted(properties))
			{
				return true;
			}
			if (RecipientDeletedDuringOrganizationDeletionFilter.WasObjectDeletedBeforeOrganization(propertyBag, properties))
			{
				return true;
			}
			this.reporter.ReportExcludedObject(propertyBag, DirectoryObjectErrorCode.ObjectOutOfScope, ProcessingStage.RecipientDeletedDuringOrganizationDeletionFilter);
			return false;
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x0001A05C File Offset: 0x0001825C
		private static bool WasObjectDeletedBeforeOrganization(PropertyBag propertyBag, ADRawEntry org)
		{
			DateTime? dateTime = (DateTime?)org[ExchangeConfigurationUnitSchema.WhenOrganizationStatusSet];
			if (dateTime != null)
			{
				MultiValuedProperty<AttributeMetadata> multiValuedProperty = (MultiValuedProperty<AttributeMetadata>)propertyBag[ADRecipientSchema.AttributeMetadata];
				foreach (AttributeMetadata attributeMetadata in multiValuedProperty)
				{
					if (attributeMetadata.AttributeName.Equals(SyncObjectSchema.Deleted.LdapDisplayName, StringComparison.OrdinalIgnoreCase))
					{
						return attributeMetadata.LastWriteTime.ToUniversalTime() < dateTime.Value.ToUniversalTime();
					}
				}
				return false;
			}
			return false;
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x0001A114 File Offset: 0x00018314
		private static bool WasOrganizationForThisObjectDeleted(ADRawEntry org)
		{
			return ProcessorHelper.IsDeletedObject(org.propertyBag) || ExchangeConfigurationUnit.IsBeingDeleted((OrganizationStatus)org[ExchangeConfigurationUnitSchema.OrganizationStatus]);
		}

		// Token: 0x040002E9 RID: 745
		private readonly IPropertyLookup organizationLookup;

		// Token: 0x040002EA RID: 746
		private readonly ExcludedObjectReporter reporter;
	}
}
