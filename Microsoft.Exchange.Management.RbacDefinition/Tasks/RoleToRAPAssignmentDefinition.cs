using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200000A RID: 10
	internal class RoleToRAPAssignmentDefinition
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000034AA File Offset: 0x000016AA
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000034B2 File Offset: 0x000016B2
		public RoleType Type { get; private set; }

		// Token: 0x06000042 RID: 66 RVA: 0x000034BB File Offset: 0x000016BB
		public RoleToRAPAssignmentDefinition(RoleType roleType, string[] neededFeatures, string introducedInBuild)
		{
			this.Type = roleType;
			this.neededFeatures = neededFeatures;
			this.introducedInBuild = introducedInBuild;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000034D8 File Offset: 0x000016D8
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

		// Token: 0x06000044 RID: 68 RVA: 0x000034F8 File Offset: 0x000016F8
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

		// Token: 0x0400001E RID: 30
		private string[] neededFeatures;

		// Token: 0x0400001F RID: 31
		private readonly string introducedInBuild;
	}
}
