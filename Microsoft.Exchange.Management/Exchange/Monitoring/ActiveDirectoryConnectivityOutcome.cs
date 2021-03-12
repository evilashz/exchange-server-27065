using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200000B RID: 11
	[Serializable]
	public sealed class ActiveDirectoryConnectivityOutcome : TransactionOutcomeBase
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000033FA File Offset: 0x000015FA
		internal ActiveDirectoryConnectivityOutcome(ActiveDirectoryConnectivityContext context, TestActiveDirectoryConnectivityTask.ScenarioId scenarioId, LocalizedString scenario, string performanceCounter, string domainController) : base(domainController, scenario, "", performanceCounter, string.Empty)
		{
			this.Id = scenarioId;
			this.ActiveDirectory = domainController;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003425 File Offset: 0x00001625
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000342D File Offset: 0x0000162D
		public string ActiveDirectory { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003436 File Offset: 0x00001636
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000343E File Offset: 0x0000163E
		public TestActiveDirectoryConnectivityTask.ScenarioId Id { get; private set; }

		// Token: 0x06000071 RID: 113 RVA: 0x00003447 File Offset: 0x00001647
		public override string ToString()
		{
			return string.Format("{0}, {1}", base.Scenario, base.Result);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000345F File Offset: 0x0000165F
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return ActiveDirectoryConnectivityOutcome.schema;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003466 File Offset: 0x00001666
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000346E File Offset: 0x0000166E
		internal TimeSpan? Timeout { get; set; }

		// Token: 0x06000075 RID: 117 RVA: 0x00003477 File Offset: 0x00001677
		internal void UpdateTarget(string targetServer)
		{
			this.ActiveDirectory = targetServer;
		}

		// Token: 0x04000034 RID: 52
		private static ActiveDirectoryConnectivityOutcomeSchema schema = ObjectSchema.GetInstance<ActiveDirectoryConnectivityOutcomeSchema>();
	}
}
