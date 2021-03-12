using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000242 RID: 578
	public abstract class BooleanFeatureBag
	{
		// Token: 0x0600134B RID: 4939 RVA: 0x000568B8 File Offset: 0x00054AB8
		protected virtual void InitializeDependencies()
		{
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x0600134C RID: 4940
		internal abstract ServicePlanElementSchema Schema { get; }

		// Token: 0x0600134D RID: 4941 RVA: 0x000568BA File Offset: 0x00054ABA
		protected BooleanFeatureBag()
		{
			this.propertyBag = new PropertyBag();
			this.dependencies = new List<DependencyEntry>();
			this.InitializeDependencies();
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x0600134E RID: 4942 RVA: 0x000568DE File Offset: 0x00054ADE
		internal List<DependencyEntry> Dependencies
		{
			get
			{
				return this.dependencies;
			}
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x000568E6 File Offset: 0x00054AE6
		internal PropertyBag PropertyBag
		{
			get
			{
				return this.propertyBag;
			}
		}

		// Token: 0x170005E2 RID: 1506
		internal object this[FeatureDefinition featureDefinition]
		{
			get
			{
				return this.propertyBag[featureDefinition] ?? featureDefinition.DefaultValue;
			}
			set
			{
				this.propertyBag[featureDefinition] = value;
			}
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x00056918 File Offset: 0x00054B18
		public List<string> GetEnabledPermissionFeatures()
		{
			return this.GetEnabledFeatures(new FeatureCategory[]
			{
				FeatureCategory.MailboxPlanPermissions,
				FeatureCategory.AdminPermissions
			});
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x0005693C File Offset: 0x00054B3C
		public List<string> GetEnabledRoleGroupRoleAssignmentFeatures()
		{
			return this.GetEnabledFeatures(new FeatureCategory[]
			{
				FeatureCategory.RoleGroupRoleAssignment
			});
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x0005695C File Offset: 0x00054B5C
		public List<string> GetEnabledMailboxPlanPermissionsFeatures()
		{
			return this.GetEnabledFeatures(new FeatureCategory[]
			{
				FeatureCategory.MailboxPlanPermissions
			});
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x0005697C File Offset: 0x00054B7C
		public List<string> GetEnabledMailboxPlanRoleAssignmentFeatures()
		{
			return this.GetEnabledFeatures(new FeatureCategory[]
			{
				FeatureCategory.MailboxPlanRoleAssignment
			});
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000569B4 File Offset: 0x00054BB4
		private List<string> GetEnabledFeatures(params FeatureCategory[] features)
		{
			if (features == null)
			{
				throw new ArgumentNullException("Parameter features should not be null.");
			}
			List<string> list = new List<string>();
			list.Add("*");
			foreach (object obj in ((IEnumerable)this.Schema))
			{
				FeatureDefinition featureDefinition = (FeatureDefinition)obj;
				if (featureDefinition.Type.Equals(typeof(bool)) && (bool)this[featureDefinition])
				{
					if (featureDefinition.Categories.Any((FeatureCategory x) => features.Contains(x)))
					{
						list.Add(featureDefinition.Name);
					}
				}
			}
			return list;
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x00056A90 File Offset: 0x00054C90
		internal List<ValidationError> ValidateDependencies(ServicePlan sp)
		{
			List<ValidationError> list = new List<ValidationError>();
			foreach (DependencyEntry dependencyEntry in this.dependencies)
			{
				bool flag = dependencyEntry.GetDependencyValue(sp);
				bool flag2 = dependencyEntry.GetFeatureValue();
				if (flag2 && !flag)
				{
					list.Add(new DependencyValidationError(dependencyEntry.FeatureName, flag2, dependencyEntry.DependencyFeatureName, flag));
				}
			}
			return list;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00056B1C File Offset: 0x00054D1C
		internal List<ValidationError> ValidateFeaturesAllowedForSKU()
		{
			List<ValidationError> list = new List<ValidationError>();
			ServicePlanSkus servicePlanSkus;
			if (Datacenter.IsPartnerHostedOnly(false))
			{
				servicePlanSkus = ServicePlanSkus.Hosted;
			}
			else
			{
				servicePlanSkus = ServicePlanSkus.Datacenter;
			}
			foreach (object obj in this.propertyBag.Keys)
			{
				FeatureDefinition featureDefinition = (FeatureDefinition)obj;
				if ((byte)(featureDefinition.ServicePlanSkus & servicePlanSkus) == 0)
				{
					list.Add(new ServicePlanFeaturesValidationError(featureDefinition.Name, servicePlanSkus.ToString()));
				}
			}
			return list;
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00056BB4 File Offset: 0x00054DB4
		internal void FixDependencies(ServicePlan sp)
		{
			foreach (DependencyEntry dependencyEntry in this.dependencies)
			{
				if (dependencyEntry.GetFeatureValue() && !dependencyEntry.GetDependencyValue(sp))
				{
					dependencyEntry.SetDependencyValue(sp, true);
				}
			}
		}

		// Token: 0x040008F1 RID: 2289
		private PropertyBag propertyBag;

		// Token: 0x040008F2 RID: 2290
		private List<DependencyEntry> dependencies;
	}
}
