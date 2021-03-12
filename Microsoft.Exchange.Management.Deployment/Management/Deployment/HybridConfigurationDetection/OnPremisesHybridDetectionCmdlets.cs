using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Deployment.HybridConfigurationDetection
{
	// Token: 0x02000032 RID: 50
	internal class OnPremisesHybridDetectionCmdlets : IOnPremisesHybridDetectionCmdlets
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00004D47 File Offset: 0x00002F47
		public OnPremisesHybridDetectionCmdlets()
		{
			this.monadProvider = new MonadProvider();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00004D5C File Offset: 0x00002F5C
		public IEnumerable<AcceptedDomain> GetAcceptedDomain()
		{
			IEnumerable<AcceptedDomain> result;
			try
			{
				object[] array = this.monadProvider.ExecuteCommand("Get-AcceptedDomain");
				if (array != null && array.Length > 0)
				{
					AcceptedDomain[] array2 = new AcceptedDomain[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = (AcceptedDomain)array[i];
					}
					result = array2;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00004DC4 File Offset: 0x00002FC4
		public IEnumerable<OrganizationRelationship> GetOrganizationRelationship()
		{
			IEnumerable<OrganizationRelationship> result;
			try
			{
				object[] array = this.monadProvider.ExecuteCommand("Get-OrganizationRelationship");
				if (array != null && array.Length > 0)
				{
					OrganizationRelationship[] array2 = new OrganizationRelationship[array.Length];
					for (int i = 0; i < array.Length; i++)
					{
						array2[i] = (OrganizationRelationship)array[i];
					}
					result = array2;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040000B2 RID: 178
		private const string GetAcceptedDomainCmdlet = "Get-AcceptedDomain";

		// Token: 0x040000B3 RID: 179
		private const string GetOrganizationRelationshipCmdlet = "Get-OrganizationRelationship";

		// Token: 0x040000B4 RID: 180
		private MonadProvider monadProvider;
	}
}
