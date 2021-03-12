using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200023C RID: 572
	internal sealed class FeatureDefinition : PropertyDefinition
	{
		// Token: 0x06001326 RID: 4902 RVA: 0x0005488B File Offset: 0x00052A8B
		public FeatureDefinition(string name, Type type, ServicePlanSkus servicePlanSkus, params FeatureCategory[] categories) : base(name, type)
		{
			this.categories = categories.ToList<FeatureCategory>();
			this.servicePlanSkus = servicePlanSkus;
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x000548AC File Offset: 0x00052AAC
		public FeatureDefinition(string name, FeatureCategory category, Type type, ServicePlanSkus servicePlanSkus) : this(name, type, servicePlanSkus, new FeatureCategory[]
		{
			category
		})
		{
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06001328 RID: 4904 RVA: 0x000548CF File Offset: 0x00052ACF
		// (set) Token: 0x06001329 RID: 4905 RVA: 0x000548D7 File Offset: 0x00052AD7
		public List<FeatureCategory> Categories
		{
			get
			{
				return this.categories;
			}
			set
			{
				this.categories = value;
			}
		}

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600132A RID: 4906 RVA: 0x000548E0 File Offset: 0x00052AE0
		// (set) Token: 0x0600132B RID: 4907 RVA: 0x000548E8 File Offset: 0x00052AE8
		public ServicePlanSkus ServicePlanSkus
		{
			get
			{
				return this.servicePlanSkus;
			}
			set
			{
				this.servicePlanSkus = value;
			}
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x000548F4 File Offset: 0x00052AF4
		public object DefaultValue
		{
			get
			{
				if (base.Type == typeof(bool))
				{
					return false;
				}
				if (base.Type == typeof(string))
				{
					return null;
				}
				if (base.Type.BaseType == typeof(Enum))
				{
					return Enum.GetValues(base.Type).GetValue(0);
				}
				if (base.Type == typeof(Unlimited<int>))
				{
					return new Unlimited<int>(0);
				}
				throw new NotImplementedException();
			}
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0005498E File Offset: 0x00052B8E
		public bool IsValueEqual(object a, object b)
		{
			return object.Equals(a, b);
		}

		// Token: 0x0400083C RID: 2108
		private List<FeatureCategory> categories;

		// Token: 0x0400083D RID: 2109
		private ServicePlanSkus servicePlanSkus;
	}
}
