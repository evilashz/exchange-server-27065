using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.Rules.Tasks;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A50 RID: 2640
	[Cmdlet("Remove", "HostedContentFilterRule", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveHostedContentFilterRule : RemoveRuleTaskBase
	{
		// Token: 0x06005E91 RID: 24209 RVA: 0x0018C220 File Offset: 0x0018A420
		public RemoveHostedContentFilterRule() : base("HostedContentFilterVersioned")
		{
		}

		// Token: 0x17001C7C RID: 7292
		// (get) Token: 0x06005E92 RID: 24210 RVA: 0x0018C22D File Offset: 0x0018A42D
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveHostedContentFilterRule(this.Identity.ToString());
			}
		}

		// Token: 0x06005E93 RID: 24211 RVA: 0x0018C23F File Offset: 0x0018A43F
		protected override void InternalProcessRecord()
		{
			base.InternalProcessRecord();
			FfoDualWriter.DeleteFromFfo<TransportRule>(this, base.DataObject, TenantSettingSyncLogType.DUALSYNCTR);
		}
	}
}
