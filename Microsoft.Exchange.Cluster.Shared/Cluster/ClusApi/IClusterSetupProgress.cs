using System;

namespace Microsoft.Exchange.Cluster.ClusApi
{
	// Token: 0x02000048 RID: 72
	internal interface IClusterSetupProgress
	{
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600029A RID: 666
		// (set) Token: 0x0600029B RID: 667
		int MaxPercentageDuringCallback { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600029C RID: 668
		// (set) Token: 0x0600029D RID: 669
		Exception LastException { get; set; }

		// Token: 0x0600029E RID: 670
		int ClusterSetupProgressCallback(IntPtr pvCallbackArg, ClusapiMethods.CLUSTER_SETUP_PHASE eSetupPhase, ClusapiMethods.CLUSTER_SETUP_PHASE_TYPE ePhaseType, ClusapiMethods.CLUSTER_SETUP_PHASE_SEVERITY ePhaseSeverity, uint dwPercentComplete, string lpszObjectName, uint dwStatus);
	}
}
