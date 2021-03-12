using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200000D RID: 13
	internal class RoleAssignmentDefinition
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003615 File Offset: 0x00001815
		// (set) Token: 0x06000050 RID: 80 RVA: 0x0000361D File Offset: 0x0000181D
		public RoleType RoleType { get; private set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003626 File Offset: 0x00001826
		// (set) Token: 0x06000052 RID: 82 RVA: 0x0000362E File Offset: 0x0000182E
		public RoleAssignmentDelegationType DelegationType { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003637 File Offset: 0x00001837
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000363F File Offset: 0x0000183F
		public bool UseSafeRole { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003648 File Offset: 0x00001848
		public ExchangeBuild IntroducedInBuild
		{
			get
			{
				if (this.introducedInBuild == null)
				{
					return new ExchangeBuild(0, 0, 0, 0);
				}
				return ExchangeBuild.Parse(this.introducedInBuild);
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00003667 File Offset: 0x00001867
		public RoleAssignmentDefinition(RoleType roleType, RoleAssignmentDelegationType delegationType, string[] neededFeatures, string introducedInBuild, bool useSafeVersionOfRole)
		{
			this.RoleType = roleType;
			this.DelegationType = delegationType;
			this.neededFeatures = neededFeatures;
			this.introducedInBuild = introducedInBuild;
			this.UseSafeRole = useSafeVersionOfRole;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003694 File Offset: 0x00001894
		public bool SatisfyCondition(List<string> enabledFeatures)
		{
			bool result = false;
			if (enabledFeatures == null || this.neededFeatures == null)
			{
				result = true;
			}
			else
			{
				foreach (string item in this.neededFeatures)
				{
					if (enabledFeatures.Contains(item))
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000036DC File Offset: 0x000018DC
		public bool SatisfyCondition(List<string> enabledFeatures, RoleGroupRoleMapping[] assignments)
		{
			bool flag = this.SatisfyCondition(enabledFeatures);
			if (!flag && this.DelegationType == RoleAssignmentDelegationType.Regular)
			{
				foreach (RoleGroupRoleMapping roleGroupRoleMapping in assignments)
				{
					foreach (RoleAssignmentDefinition roleAssignmentDefinition in roleGroupRoleMapping.Assignments)
					{
						if (roleAssignmentDefinition.RoleType == this.RoleType && roleAssignmentDefinition.DelegationType != RoleAssignmentDelegationType.Regular && roleAssignmentDefinition.SatisfyCondition(enabledFeatures))
						{
							return true;
						}
					}
				}
			}
			return flag;
		}

		// Token: 0x04000024 RID: 36
		private string[] neededFeatures;

		// Token: 0x04000025 RID: 37
		private readonly string introducedInBuild;
	}
}
