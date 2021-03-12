using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000AEB RID: 2795
	[Cmdlet("Get", "SmimeConfig")]
	public sealed class GetSmimeConfig : GetMultitenancySingletonSystemConfigurationObjectTask<SmimeConfigurationContainer>
	{
		// Token: 0x17001E18 RID: 7704
		// (get) Token: 0x06006339 RID: 25401 RVA: 0x0019ED30 File Offset: 0x0019CF30
		protected override ObjectId RootId
		{
			get
			{
				return SmimeConfigurationContainer.GetWellKnownParentLocation(base.CurrentOrgContainerId);
			}
		}

		// Token: 0x17001E19 RID: 7705
		// (get) Token: 0x0600633A RID: 25402 RVA: 0x0019ED3D File Offset: 0x0019CF3D
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600633B RID: 25403 RVA: 0x0019ED40 File Offset: 0x0019CF40
		protected override void WriteResult<T>(IEnumerable<T> dataObjects)
		{
			TaskLogger.LogEnter(new object[]
			{
				dataObjects
			});
			if (dataObjects != null)
			{
				SmimeConfigurationContainer smimeConfigurationContainer = null;
				using (IEnumerator<T> enumerator = dataObjects.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						smimeConfigurationContainer = (enumerator.Current as SmimeConfigurationContainer);
					}
					else
					{
						smimeConfigurationContainer = new SmimeConfigurationContainer();
						smimeConfigurationContainer.OrganizationId = base.CurrentOrganizationId;
						smimeConfigurationContainer.SetId(this.RootId as ADObjectId);
					}
				}
				base.WriteResult(smimeConfigurationContainer);
			}
			TaskLogger.LogExit();
		}
	}
}
