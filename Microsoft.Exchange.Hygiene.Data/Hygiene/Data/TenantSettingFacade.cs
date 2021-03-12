using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Hygiene.Data
{
	// Token: 0x020000AA RID: 170
	internal class TenantSettingFacade<T> : FacadeBase where T : ADObject, new()
	{
		// Token: 0x060005B0 RID: 1456 RVA: 0x00012DB3 File Offset: 0x00010FB3
		public TenantSettingFacade(IConfigurable configurable) : base(configurable ?? FacadeBase.NewADObject<T>())
		{
		}

		// Token: 0x060005B1 RID: 1457 RVA: 0x00012DCA File Offset: 0x00010FCA
		public TenantSettingFacade() : this(null)
		{
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x00012DE0 File Offset: 0x00010FE0
		// (set) Token: 0x060005B3 RID: 1459 RVA: 0x00012E42 File Offset: 0x00011042
		public PropertyDefinition[] DeclaredProperties
		{
			get
			{
				if (this.declaredProperties == null)
				{
					this.declaredProperties = (from property in (base.InnerConfigurable as ADObject).ObjectSchema.AllProperties.OfType<ProviderPropertyDefinition>()
					where !property.IsCalculated
					select property).ToArray<ProviderPropertyDefinition>();
				}
				return this.declaredProperties;
			}
			set
			{
				this.declaredProperties = value;
			}
		}

		// Token: 0x170001EF RID: 495
		public override object this[PropertyDefinition propertyDefinition]
		{
			get
			{
				return base[propertyDefinition];
			}
			set
			{
				base[propertyDefinition] = value;
			}
		}

		// Token: 0x060005B6 RID: 1462 RVA: 0x00012E7C File Offset: 0x0001107C
		public override IEnumerable<PropertyDefinition> GetPropertyDefinitions(bool isChangedOnly)
		{
			T innerSetting = base.InnerConfigurable as T;
			IEnumerable<PropertyDefinition> enumerable = this.DeclaredProperties;
			if (isChangedOnly)
			{
				enumerable = from property in enumerable.OfType<ProviderPropertyDefinition>()
				where innerSetting.IsModified(property)
				select property;
			}
			return enumerable.Concat(new ADPropertyDefinition[]
			{
				ADObjectSchema.OrganizationalUnitRoot,
				ADObjectSchema.Id,
				ADObjectSchema.RawName,
				DalHelper.IsTracerTokenProp
			});
		}

		// Token: 0x060005B7 RID: 1463 RVA: 0x00012F1C File Offset: 0x0001111C
		public override void FixIdPropertiesForWebservice(IConfigDataProvider dataProvider, ADObjectId orgId, bool isServer)
		{
			if (typeof(T) == typeof(TenantInboundConnector))
			{
				MultiValuedProperty<ADObjectId> multiValuedProperty = (MultiValuedProperty<ADObjectId>)this[TenantInboundConnectorSchema.AssociatedAcceptedDomains];
				if (multiValuedProperty != null && multiValuedProperty.Count != 0)
				{
					QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.OrganizationalUnitRoot, orgId.ObjectGuid);
					IEnumerable<AcceptedDomain> source = dataProvider.Find<AcceptedDomain>(filter, null, false, null).OfType<AcceptedDomain>();
					for (int i = 0; i < multiValuedProperty.Count; i++)
					{
						ADObjectId domainId = multiValuedProperty[i];
						AcceptedDomain acceptedDomain2 = source.FirstOrDefault((AcceptedDomain acceptedDomain) => acceptedDomain.Name == domainId.Name);
						if (acceptedDomain2 != null)
						{
							domainId = new ADObjectId(domainId.DistinguishedName, acceptedDomain2.Id.ObjectGuid);
							multiValuedProperty[i] = domainId;
						}
					}
				}
			}
			base.FixIdPropertiesForWebservice(dataProvider, orgId, isServer);
		}

		// Token: 0x0400037E RID: 894
		private PropertyDefinition[] declaredProperties;
	}
}
