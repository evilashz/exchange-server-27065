using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A45 RID: 2629
	[Cmdlet("Disable", "HostedContentFilterRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class DisableHostedContentFilterRule : DisableHygieneFilterRuleTaskBase
	{
		// Token: 0x06005E25 RID: 24101 RVA: 0x0018ADD3 File Offset: 0x00188FD3
		public DisableHostedContentFilterRule() : base("HostedContentFilterVersioned")
		{
		}

		// Token: 0x17001C59 RID: 7257
		// (get) Token: 0x06005E26 RID: 24102 RVA: 0x0018ADE0 File Offset: 0x00188FE0
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageDisableHostedContentFilterRule(this.Identity.ToString());
			}
		}

		// Token: 0x06005E27 RID: 24103 RVA: 0x0018ADF2 File Offset: 0x00188FF2
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			FfoDualWriter.SaveToFfo<TransportRule>(this, this.DataObject, TenantSettingSyncLogType.DUALSYNCTR, null);
		}
	}
}
