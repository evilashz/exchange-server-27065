using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x02000249 RID: 585
	[Cmdlet("Set", "InformationStoreService")]
	public class SetInformationStoreService : ConfigureService
	{
		// Token: 0x060015CC RID: 5580 RVA: 0x0005B938 File Offset: 0x00059B38
		public SetInformationStoreService()
		{
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.FailureActionsFlag = true;
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x060015CD RID: 5581 RVA: 0x0005B98F File Offset: 0x00059B8F
		protected override string Name
		{
			get
			{
				return "MSExchangeIS";
			}
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0005B996 File Offset: 0x00059B96
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.ConfigureFailureActions();
			base.ConfigureFailureActionsFlag();
			TaskLogger.LogExit();
		}

		// Token: 0x0400097C RID: 2428
		private const string ServiceShortName = "MSExchangeIS";
	}
}
