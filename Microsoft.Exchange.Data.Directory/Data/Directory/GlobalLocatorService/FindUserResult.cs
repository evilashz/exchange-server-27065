using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000120 RID: 288
	internal class FindUserResult
	{
		// Token: 0x06000BFB RID: 3067 RVA: 0x000369DE File Offset: 0x00034BDE
		internal FindUserResult(string msaUserMemberName, Guid tenantId, IDictionary<TenantProperty, PropertyValue> tenantProperties)
		{
			this.MSAUserMemberName = msaUserMemberName;
			this.tenantId = tenantId;
			this.tenantProperties = tenantProperties;
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x000369FB File Offset: 0x00034BFB
		internal Guid TenantId
		{
			get
			{
				return this.tenantId;
			}
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00036A04 File Offset: 0x00034C04
		internal PropertyValue GetTenantPropertyValue(TenantProperty tenantProperty)
		{
			PropertyValue result;
			if (!this.tenantProperties.TryGetValue(tenantProperty, out result))
			{
				return PropertyValue.Create(null, tenantProperty);
			}
			return result;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00036A2A File Offset: 0x00034C2A
		internal bool HasTenantProperties()
		{
			return this.tenantProperties.Count > 0;
		}

		// Token: 0x04000634 RID: 1588
		private readonly Guid tenantId;

		// Token: 0x04000635 RID: 1589
		private IDictionary<TenantProperty, PropertyValue> tenantProperties;

		// Token: 0x04000636 RID: 1590
		internal readonly string MSAUserMemberName;
	}
}
