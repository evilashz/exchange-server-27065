using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.DataClassification
{
	// Token: 0x02000023 RID: 35
	[Cmdlet("Set", "DataClassificationConfig", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetDataClassificationConfig : SetMultitenancySingletonSystemConfigurationObjectTask<DataClassificationConfig>
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000533A File Offset: 0x0000353A
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetDataClassificationConfig;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00005341 File Offset: 0x00003541
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}
	}
}
