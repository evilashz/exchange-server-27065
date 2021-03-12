using System;
using Microsoft.Exchange.Data.Directory.ProvisioningCache;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x0200035B RID: 859
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class ADOrganizationalUnit : ADConfigurationObject, IProvisioningCacheInvalidation
	{
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x0600279B RID: 10139 RVA: 0x000A6CCE File Offset: 0x000A4ECE
		internal override ADObjectSchema Schema
		{
			get
			{
				return ADOrganizationalUnit.schema;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600279C RID: 10140 RVA: 0x000A6CD5 File Offset: 0x000A4ED5
		internal override string MostDerivedObjectClass
		{
			get
			{
				return ADOrganizationalUnit.MostDerivedClass;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600279E RID: 10142 RVA: 0x000A6CE4 File Offset: 0x000A4EE4
		public new OrganizationId OrganizationId
		{
			get
			{
				return (OrganizationId)this[ADOrganizationalUnitSchema.OrganizationId];
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x000A6CF6 File Offset: 0x000A4EF6
		internal ADObjectId ConfigurationUnitLink
		{
			get
			{
				return ADOrganizationalUnit.ConfigurationUnitLinkGetter(this.propertyBag);
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x000A6D03 File Offset: 0x000A4F03
		public bool IsOrganizationalUnitRoot
		{
			get
			{
				return this.propertyBag[ADOrganizationalUnitSchema.ConfigurationUnitLink] != null;
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x000A6D1B File Offset: 0x000A4F1B
		// (set) Token: 0x060027A2 RID: 10146 RVA: 0x000A6D2D File Offset: 0x000A4F2D
		public bool MSOSyncEnabled
		{
			get
			{
				return (bool)this[ADOrganizationalUnitSchema.MSOSyncEnabled];
			}
			internal set
			{
				this[ADOrganizationalUnitSchema.MSOSyncEnabled] = value;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060027A3 RID: 10147 RVA: 0x000A6D40 File Offset: 0x000A4F40
		// (set) Token: 0x060027A4 RID: 10148 RVA: 0x000A6D52 File Offset: 0x000A4F52
		public bool SMTPAddressCheckWithAcceptedDomain
		{
			get
			{
				return (bool)this[ADOrganizationalUnitSchema.SMTPAddressCheckWithAcceptedDomain];
			}
			internal set
			{
				this[ADOrganizationalUnitSchema.SMTPAddressCheckWithAcceptedDomain] = value;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060027A5 RID: 10149 RVA: 0x000A6D65 File Offset: 0x000A4F65
		public bool SyncMEUSMTPToMServ
		{
			get
			{
				return !(bool)this[ADOrganizationalUnitSchema.MSOSyncEnabled] || (bool)this[ADOrganizationalUnitSchema.SMTPAddressCheckWithAcceptedDomain];
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060027A6 RID: 10150 RVA: 0x000A6D8B File Offset: 0x000A4F8B
		// (set) Token: 0x060027A7 RID: 10151 RVA: 0x000A6DB4 File Offset: 0x000A4FB4
		public bool SyncMBXAndDLToMServ
		{
			get
			{
				return (bool)this[ADOrganizationalUnitSchema.SyncMBXAndDLToMserv] || !(bool)this[ADOrganizationalUnitSchema.MSOSyncEnabled];
			}
			internal set
			{
				this[ADOrganizationalUnitSchema.SyncMBXAndDLToMserv] = value;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060027A8 RID: 10152 RVA: 0x000A6DC7 File Offset: 0x000A4FC7
		// (set) Token: 0x060027A9 RID: 10153 RVA: 0x000A6DD9 File Offset: 0x000A4FD9
		internal bool RelocationInProgress
		{
			get
			{
				return (bool)this[ADOrganizationalUnitSchema.RelocationInProgress];
			}
			set
			{
				this[ADOrganizationalUnitSchema.RelocationInProgress] = value;
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x000A6DEC File Offset: 0x000A4FEC
		internal new ADObjectId OrganizationalUnitRoot
		{
			get
			{
				return ((OrganizationId)this[ADOrganizationalUnitSchema.OrganizationId]).OrganizationalUnit;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x060027AB RID: 10155 RVA: 0x000A6E03 File Offset: 0x000A5003
		// (set) Token: 0x060027AC RID: 10156 RVA: 0x000A6E1A File Offset: 0x000A501A
		internal new ADObjectId ConfigurationUnit
		{
			get
			{
				return ((OrganizationId)this[ADOrganizationalUnitSchema.OrganizationId]).ConfigurationUnit;
			}
			set
			{
				this[ADObjectSchema.ConfigurationUnit] = value;
				if (value == null)
				{
					this[ADObjectSchema.OrganizationalUnitRoot] = null;
					return;
				}
				this[ADObjectSchema.OrganizationalUnitRoot] = this[ADObjectSchema.Id];
			}
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000A6E50 File Offset: 0x000A5050
		internal static object OuOrganizationIdGetter(IPropertyBag propertyBag)
		{
			OrganizationId organizationId = (OrganizationId)ADObject.OrganizationIdGetter(propertyBag);
			if (organizationId.Equals(OrganizationId.ForestWideOrgId))
			{
				ADObjectId adobjectId = ADOrganizationalUnit.ConfigurationUnitLinkGetter(propertyBag);
				if (adobjectId != null)
				{
					organizationId = new OrganizationId((ADObjectId)propertyBag[ADObjectSchema.Id], adobjectId);
				}
			}
			return organizationId;
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000A6E98 File Offset: 0x000A5098
		private static ADObjectId ConfigurationUnitLinkGetter(IPropertyBag propertyBag)
		{
			ADObjectId result = null;
			if (propertyBag[ADOrganizationalUnitSchema.ConfigurationUnitLink] != null)
			{
				MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)propertyBag[ADOrganizationalUnitSchema.ConfigurationUnitLink];
				if (multiValuedProperty.Count > 0)
				{
					result = multiValuedProperty[0];
				}
			}
			return result;
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x060027AF RID: 10159 RVA: 0x000A6ED7 File Offset: 0x000A50D7
		public MultiValuedProperty<string> UPNSuffixes
		{
			get
			{
				return (MultiValuedProperty<string>)this.propertyBag[ADOrganizationalUnitSchema.UPNSuffixes];
			}
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000A6EEE File Offset: 0x000A50EE
		internal bool ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			orgId = null;
			keys = null;
			if (base.ObjectState == ObjectState.Deleted)
			{
				keys = new Guid[1];
				keys[0] = CannedProvisioningCacheKeys.OrganizationIdDictionary;
				return true;
			}
			return false;
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000A6F1C File Offset: 0x000A511C
		bool IProvisioningCacheInvalidation.ShouldInvalidProvisioningCache(out OrganizationId orgId, out Guid[] keys)
		{
			return this.ShouldInvalidProvisioningCache(out orgId, out keys);
		}

		// Token: 0x04001825 RID: 6181
		private static ADOrganizationalUnitSchema schema = ObjectSchema.GetInstance<ADOrganizationalUnitSchema>();

		// Token: 0x04001826 RID: 6182
		internal static string MostDerivedClass = "organizationalUnit";
	}
}
