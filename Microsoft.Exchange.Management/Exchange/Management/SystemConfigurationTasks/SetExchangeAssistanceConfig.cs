using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200099B RID: 2459
	[Cmdlet("Set", "ExchangeAssistanceConfig", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetExchangeAssistanceConfig : SetMultitenancySingletonSystemConfigurationObjectTask<ExchangeAssistance>
	{
		// Token: 0x17001A35 RID: 6709
		// (get) Token: 0x060057F7 RID: 22519 RVA: 0x0016F6D7 File Offset: 0x0016D8D7
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				if (base.Identity != null)
				{
					return Strings.ConfirmationMessageSetExchangeAssistanceId(base.Identity.ToString());
				}
				return Strings.ConfirmationMessageSetExchangeAssistance;
			}
		}

		// Token: 0x17001A36 RID: 6710
		// (get) Token: 0x060057F8 RID: 22520 RVA: 0x0016F6F7 File Offset: 0x0016D8F7
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x17001A37 RID: 6711
		// (get) Token: 0x060057F9 RID: 22521 RVA: 0x0016F6FA File Offset: 0x0016D8FA
		protected override QueryFilter InternalFilter
		{
			get
			{
				return new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Name, NewExchangeAssistanceConfig.CurrentVersionContainerName);
			}
		}
	}
}
