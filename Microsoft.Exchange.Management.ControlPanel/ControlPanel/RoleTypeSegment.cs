using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003AA RID: 938
	internal class RoleTypeSegment
	{
		// Token: 0x0600317D RID: 12669 RVA: 0x00098A3B File Offset: 0x00096C3B
		public RoleTypeSegment(RbacSettings rbacSettings)
		{
			this.rbacSettings = rbacSettings;
		}

		// Token: 0x0600317E RID: 12670 RVA: 0x00098AA0 File Offset: 0x00096CA0
		public IList<RoleType> GetAllowedFeatures()
		{
			IList<RoleType> result = null;
			IList<RoleTypeConstraint> constraints = this.GetEnabledConstraints();
			if (constraints != null)
			{
				IEnumerable<RoleType> allFeatures = this.GetAllFeatures();
				result = (from roleType in allFeatures
				where constraints.Any((RoleTypeConstraint constraint) => constraint.Validate(roleType))
				select roleType).ToList<RoleType>().AsReadOnly();
			}
			return result;
		}

		// Token: 0x0600317F RID: 12671 RVA: 0x00098AF8 File Offset: 0x00096CF8
		private IList<RoleTypeConstraint> GetEnabledConstraints()
		{
			IList<RoleTypeConstraint> result = null;
			if (!this.IsAllFeaturesEnabled())
			{
				List<RoleTypeConstraint> list = new List<RoleTypeConstraint>();
				if (this.rbacSettings.AdminEnabled)
				{
					list.Add(RoleTypeConstraint.AdminRoleTypeConstraint);
				}
				if (this.rbacSettings.OwaOptionsEnabled)
				{
					list.Add(RoleTypeConstraint.EndUserRoleTypeConstraint);
				}
				result = list.AsReadOnly();
			}
			return result;
		}

		// Token: 0x06003180 RID: 12672 RVA: 0x00098B4D File Offset: 0x00096D4D
		private bool IsAllFeaturesEnabled()
		{
			return this.rbacSettings.AdminEnabled && this.rbacSettings.OwaOptionsEnabled;
		}

		// Token: 0x06003181 RID: 12673 RVA: 0x00098B69 File Offset: 0x00096D69
		private IEnumerable<RoleType> GetAllFeatures()
		{
			return Enum.GetValues(typeof(RoleType)).OfType<RoleType>();
		}

		// Token: 0x04002405 RID: 9221
		private RbacSettings rbacSettings;
	}
}
