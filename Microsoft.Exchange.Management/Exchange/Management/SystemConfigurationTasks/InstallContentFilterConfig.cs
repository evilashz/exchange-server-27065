using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A9B RID: 2715
	[Cmdlet("Install", "ContentFilterConfig")]
	public sealed class InstallContentFilterConfig : InstallAntispamConfig<ContentFilterConfig>
	{
		// Token: 0x17001D1E RID: 7454
		// (get) Token: 0x06006044 RID: 24644 RVA: 0x001915B5 File Offset: 0x0018F7B5
		protected override string CanonicalName
		{
			get
			{
				return "ContentFilterConfig";
			}
		}

		// Token: 0x06006045 RID: 24645 RVA: 0x001915BC File Offset: 0x0018F7BC
		protected override IConfigurable PrepareDataObject()
		{
			ContentFilterConfig contentFilterConfig = (ContentFilterConfig)base.PrepareDataObject();
			contentFilterConfig.SCLRejectEnabled = true;
			contentFilterConfig.OutlookEmailPostmarkValidationEnabled = true;
			return contentFilterConfig;
		}
	}
}
