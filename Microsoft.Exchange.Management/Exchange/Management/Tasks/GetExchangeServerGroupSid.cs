using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002BF RID: 703
	[Cmdlet("get", "ExchangeServerGroupSid", SupportsShouldProcess = true)]
	public sealed class GetExchangeServerGroupSid : SetupTaskBase
	{
		// Token: 0x060018B2 RID: 6322 RVA: 0x000690D3 File Offset: 0x000672D3
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.WriteObject(this.exs.Sid.ToString());
			TaskLogger.LogExit();
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x000690F8 File Offset: 0x000672F8
		protected override void InternalBeginProcessing()
		{
			TaskLogger.LogEnter();
			base.InternalBeginProcessing();
			this.exs = base.ResolveExchangeGroupGuid<ADGroup>(WellKnownGuid.ExSWkGuid);
			if (this.exs == null)
			{
				base.ThrowTerminatingError(new ExSGroupNotFoundException(WellKnownGuid.ExSWkGuid), ErrorCategory.ObjectNotFound, null);
			}
			base.LogReadObject(this.exs);
			TaskLogger.LogExit();
		}

		// Token: 0x04000AC0 RID: 2752
		private ADGroup exs;
	}
}
