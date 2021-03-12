using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.PerimeterConfig
{
	// Token: 0x0200005C RID: 92
	[Cmdlet("Get", "PerimeterConfig")]
	public sealed class GetPerimeterConfig : GetMultitenancySingletonSystemConfigurationObjectTask<PerimeterConfig>
	{
		// Token: 0x1700013E RID: 318
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000CAF9 File Offset: 0x0000ACF9
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}
	}
}
