using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A47 RID: 2631
	[Cmdlet("Enable", "HostedContentFilterRule", SupportsShouldProcess = true)]
	public sealed class EnableHostedContentFilterRule : EnableHygieneFilterRuleTaskBase
	{
		// Token: 0x06005E2A RID: 24106 RVA: 0x0018AE8B File Offset: 0x0018908B
		public EnableHostedContentFilterRule() : base("HostedContentFilterVersioned")
		{
		}

		// Token: 0x17001C5A RID: 7258
		// (get) Token: 0x06005E2B RID: 24107 RVA: 0x0018AE98 File Offset: 0x00189098
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageEnableHostedContentFilterRule(this.Identity.ToString());
			}
		}

		// Token: 0x06005E2C RID: 24108 RVA: 0x0018AEAA File Offset: 0x001890AA
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			FfoDualWriter.SaveToFfo<TransportRule>(this, this.DataObject, TenantSettingSyncLogType.DUALSYNCTR, null);
		}
	}
}
