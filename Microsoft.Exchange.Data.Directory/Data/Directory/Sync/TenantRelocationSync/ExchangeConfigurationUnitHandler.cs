using System;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Data.Directory;

namespace Microsoft.Exchange.Data.Directory.Sync.TenantRelocationSync
{
	// Token: 0x020007F2 RID: 2034
	internal class ExchangeConfigurationUnitHandler : ICustomObjectHandler
	{
		// Token: 0x170023B0 RID: 9136
		// (get) Token: 0x06006494 RID: 25748 RVA: 0x0015CBD1 File Offset: 0x0015ADD1
		public static ExchangeConfigurationUnitHandler Instance
		{
			get
			{
				return ExchangeConfigurationUnitHandler.instance.Value;
			}
		}

		// Token: 0x06006495 RID: 25749 RVA: 0x0015CBE0 File Offset: 0x0015ADE0
		public bool HandleObject(TenantRelocationSyncObject obj, ModifyRequest modRequest, UpdateData mData, TenantRelocationSyncData syncData, ITopologyConfigurationSession targetPartitionSession)
		{
			bool flag = false;
			return flag | this.HandleSupportedSharedConfigurationsProperty(obj, modRequest, mData, syncData, targetPartitionSession);
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x0015CC00 File Offset: 0x0015AE00
		private bool HandleSupportedSharedConfigurationsProperty(TenantRelocationSyncObject obj, ModifyRequest modRequest, UpdateData mData, TenantRelocationSyncData syncData, ITopologyConfigurationSession targetPartitionSession)
		{
			if (!TenantRelocationConfigImpl.GetConfig<bool>("TranslateSupportedSharedConfigurations"))
			{
				return false;
			}
			DirectoryAttributeModification directoryAttributeModification = null;
			MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)obj[OrganizationSchema.SupportedSharedConfigurations];
			MultiValuedProperty<ADObjectId> multiValuedProperty2;
			if (multiValuedProperty != null && multiValuedProperty.Count > 0)
			{
				OrganizationId organizationId = syncData.Source.OrganizationId;
				Exception ex;
				OrganizationId organizationId2 = SharedConfiguration.FindMostRecentSharedConfigurationInPartition(organizationId, syncData.Target.PartitionId, out ex);
				if (ex != null)
				{
					throw ex;
				}
				directoryAttributeModification = this.GetDirectoryAttributeModification(DirectoryAttributeOperation.Add);
				directoryAttributeModification.Add(organizationId2.ConfigurationUnit.DistinguishedName);
				modRequest.Modifications.Add(directoryAttributeModification);
			}
			else if (this.TryGetSupportedSharedConfigurations(targetPartitionSession, modRequest.DistinguishedName, syncData, out multiValuedProperty2) && multiValuedProperty2 != null && multiValuedProperty2.Count > 0)
			{
				directoryAttributeModification = this.GetDirectoryAttributeModification(DirectoryAttributeOperation.Delete);
				foreach (ADObjectId adobjectId in multiValuedProperty2)
				{
					directoryAttributeModification.Add(adobjectId.DistinguishedName);
				}
				modRequest.Modifications.Add(directoryAttributeModification);
			}
			if (directoryAttributeModification != null)
			{
				ExTraceGlobals.TenantRelocationTracer.TraceDebug<string, DirectoryAttributeOperation>((long)this.GetHashCode(), "GenerateModifyRequestLinkMetaDataHandler: add item: attribute {0}, op:{1}", directoryAttributeModification.Name, directoryAttributeModification.Operation);
				return true;
			}
			return false;
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x0015CD38 File Offset: 0x0015AF38
		private DirectoryAttributeModification GetDirectoryAttributeModification(DirectoryAttributeOperation operation)
		{
			return new DirectoryAttributeModification
			{
				Name = ExchangeConfigurationUnitHandler.supportedSharedConfigurationsAttributeName,
				Operation = operation
			};
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x0015CD60 File Offset: 0x0015AF60
		private bool TryGetSupportedSharedConfigurations(IConfigurationSession session, string cuObjectDN, TenantRelocationSyncData syncData, out MultiValuedProperty<ADObjectId> links)
		{
			ADObjectId adobjectId = new ADObjectId(cuObjectDN);
			bool useConfigNC = session.UseConfigNC;
			session.UseConfigNC = adobjectId.IsDescendantOf(syncData.Target.PartitionConfigNcRoot);
			bool result;
			try
			{
				ADRawEntry adrawEntry = session.ReadADRawEntry(adobjectId, ExchangeConfigurationUnitHandler.sharedConfigurationsPropertyList);
				if (adrawEntry == null)
				{
					links = null;
					result = false;
				}
				else
				{
					links = (MultiValuedProperty<ADObjectId>)adrawEntry[OrganizationSchema.SupportedSharedConfigurations];
					result = true;
				}
			}
			finally
			{
				session.UseConfigNC = useConfigNC;
			}
			return result;
		}

		// Token: 0x040042DC RID: 17116
		private static readonly Lazy<ExchangeConfigurationUnitHandler> instance = new Lazy<ExchangeConfigurationUnitHandler>(() => new ExchangeConfigurationUnitHandler());

		// Token: 0x040042DD RID: 17117
		private static readonly IEnumerable<PropertyDefinition> sharedConfigurationsPropertyList = new PropertyDefinition[]
		{
			OrganizationSchema.SupportedSharedConfigurations
		};

		// Token: 0x040042DE RID: 17118
		private static readonly string supportedSharedConfigurationsAttributeName = OrganizationSchema.SupportedSharedConfigurations.LdapDisplayName;
	}
}
