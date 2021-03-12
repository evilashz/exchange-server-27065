using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000116 RID: 278
	internal class FindDomainResult
	{
		// Token: 0x06000BB4 RID: 2996 RVA: 0x00035848 File Offset: 0x00033A48
		internal FindDomainResult(string domain, Guid tenantId, IDictionary<TenantProperty, PropertyValue> tenantProperties, IDictionary<DomainProperty, PropertyValue> domainProperties)
		{
			this.Domain = domain;
			this.tenantId = tenantId;
			this.tenantProperties = tenantProperties;
			this.domainProperties = domainProperties;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000BB5 RID: 2997 RVA: 0x0003586D File Offset: 0x00033A6D
		internal Guid TenantId
		{
			get
			{
				return this.tenantId;
			}
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00035878 File Offset: 0x00033A78
		internal PropertyValue GetTenantPropertyValue(TenantProperty property)
		{
			PropertyValue result;
			if (!this.tenantProperties.TryGetValue(property, out result))
			{
				return PropertyValue.Create(null, property);
			}
			return result;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000358A0 File Offset: 0x00033AA0
		internal PropertyValue GetDomainPropertyValue(DomainProperty property)
		{
			PropertyValue result;
			if (!this.domainProperties.TryGetValue(property, out result))
			{
				return PropertyValue.Create(null, property);
			}
			return result;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000358C6 File Offset: 0x00033AC6
		internal bool HasDomainProperties()
		{
			return this.domainProperties.Count > 0;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000358D6 File Offset: 0x00033AD6
		internal bool HasTenantProperties()
		{
			return this.tenantProperties.Count > 0;
		}

		// Token: 0x040005DE RID: 1502
		private readonly Guid tenantId;

		// Token: 0x040005DF RID: 1503
		private IDictionary<DomainProperty, PropertyValue> domainProperties;

		// Token: 0x040005E0 RID: 1504
		private IDictionary<TenantProperty, PropertyValue> tenantProperties;

		// Token: 0x040005E1 RID: 1505
		internal readonly string Domain;
	}
}
