using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.MessagingPolicies.AttachFilter
{
	// Token: 0x0200000F RID: 15
	[Cmdlet("get", "attachmentfilterlistconfig")]
	public class GetAttachmentFilterListConfig : GetSingletonSystemConfigurationObjectTask<AttachmentFilteringConfig>
	{
		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00003848 File Offset: 0x00001A48
		protected override ObjectId RootId
		{
			get
			{
				return ((IConfigurationSession)base.DataSession).GetOrgContainerId();
			}
		}
	}
}
