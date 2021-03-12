using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A30 RID: 2608
	[Cmdlet("Set", "ContentFilterConfig", SupportsShouldProcess = true)]
	public sealed class SetContentFilterConfig : SetSingletonSystemConfigurationObjectTask<ContentFilterConfig>
	{
		// Token: 0x17001BF8 RID: 7160
		// (get) Token: 0x06005D48 RID: 23880 RVA: 0x001892A6 File Offset: 0x001874A6
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetContentFilterConfig;
			}
		}

		// Token: 0x17001BF9 RID: 7161
		// (get) Token: 0x06005D49 RID: 23881 RVA: 0x001892AD File Offset: 0x001874AD
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
