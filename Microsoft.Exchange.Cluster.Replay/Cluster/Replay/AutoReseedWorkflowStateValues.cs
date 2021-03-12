using System;
using Microsoft.Exchange.Cluster.Common.ConfigurableParameters;
using Microsoft.Exchange.Cluster.Shared;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000286 RID: 646
	internal class AutoReseedWorkflowStateValues : ConfigurableParameterAccessorBase
	{
		// Token: 0x06001922 RID: 6434 RVA: 0x00067774 File Offset: 0x00065974
		public AutoReseedWorkflowStateValues(Guid dbGuid, AutoReseedWorkflowType workflowType) : base(AutoReseedWorkflowStateDefinitions.Instance, Dependencies.Assert)
		{
			this.m_stateKey = string.Format("{0}\\{1}\\{2}", "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\AutoReseed", dbGuid.ToString(), workflowType.ToString());
			base.LoadInitialValues();
		}

		// Token: 0x06001923 RID: 6435 RVA: 0x000677C4 File Offset: 0x000659C4
		protected override StateAccessor GetStateAccessor()
		{
			return new RegistryStateAccess(this.m_stateKey);
		}

		// Token: 0x04000A0A RID: 2570
		private const string StateRootKey = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\AutoReseed";

		// Token: 0x04000A0B RID: 2571
		private const string StateKeyFormat = "{0}\\{1}\\{2}";

		// Token: 0x04000A0C RID: 2572
		private readonly string m_stateKey;
	}
}
