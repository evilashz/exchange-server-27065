using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.Management.MessagingPolicies.AttachFilter
{
	// Token: 0x0200000D RID: 13
	internal class AFilterUtils
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00003608 File Offset: 0x00001808
		internal static AttachmentFilteringConfig GetAFilterConfig(IConfigDataProvider session)
		{
			ObjectId rootId = null;
			try
			{
				rootId = ((IConfigurationSession)session).GetOrgContainerId();
			}
			catch (OrgContainerNotFoundException)
			{
				throw new AttachmentFilterADEntryNotFoundException();
			}
			catch (TenantOrgContainerNotFoundException)
			{
				throw new AttachmentFilterADEntryNotFoundException();
			}
			IConfigurable[] array = session.Find<AttachmentFilteringConfig>(null, rootId, false, null);
			if (array.Length != 1)
			{
				throw new AttachmentFilterADEntryNotFoundException();
			}
			return array[0] as AttachmentFilteringConfig;
		}
	}
}
