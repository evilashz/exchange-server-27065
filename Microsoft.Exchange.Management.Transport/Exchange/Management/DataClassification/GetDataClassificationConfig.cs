using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DataClassification
{
	// Token: 0x02000021 RID: 33
	[Cmdlet("Get", "DataClassificationConfig", DefaultParameterSetName = "Identity")]
	public sealed class GetDataClassificationConfig : GetMultitenancySingletonSystemConfigurationObjectTask<DataClassificationConfig>
	{
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00005234 File Offset: 0x00003434
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00005237 File Offset: 0x00003437
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}
	}
}
