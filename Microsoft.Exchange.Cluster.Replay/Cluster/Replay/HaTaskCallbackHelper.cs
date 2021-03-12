using System;
using System.ComponentModel;
using System.Security.Principal;
using Microsoft.Exchange.Cluster.ClusApi;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000CF RID: 207
	internal static class HaTaskCallbackHelper
	{
		// Token: 0x06000861 RID: 2145 RVA: 0x0002871C File Offset: 0x0002691C
		static HaTaskCallbackHelper()
		{
			HaTaskCallbackHelper.ClusterCallbackLookupTableEntry[] array = new HaTaskCallbackHelper.ClusterCallbackLookupTableEntry[4];
			array[0] = new HaTaskCallbackHelper.ClusterCallbackLookupTableEntry(ClusapiMethods.CLUSTER_SETUP_PHASE.ClusterSetupPhaseValidateClusterNameAccount, ClusapiMethods.CLUSTER_SETUP_PHASE_TYPE.ClusterSetupPhaseEnd, ClusapiMethods.CLUSTER_SETUP_PHASE_SEVERITY.ClusterSetupPhaseFatal, delegate(string objectName, uint status)
			{
				Exception result;
				using (WindowsIdentity current = WindowsIdentity.GetCurrent())
				{
					if (status == 5U)
					{
						result = new DagTaskComputerAccountCouldNotBeValidatedAccessDeniedException(objectName, current.Name);
					}
					else
					{
						result = new DagTaskComputerAccountCouldNotBeValidatedException(objectName, current.Name, new Win32Exception((int)status).Message);
					}
				}
				return result;
			});
			array[1] = new HaTaskCallbackHelper.ClusterCallbackLookupTableEntry(ClusapiMethods.CLUSTER_SETUP_PHASE.ClusterSetupPhaseCreateClusterAccount, ClusapiMethods.CLUSTER_SETUP_PHASE_TYPE.ClusterSetupPhaseEnd, ClusapiMethods.CLUSTER_SETUP_PHASE_SEVERITY.ClusterSetupPhaseFatal, delegate(string objectName, uint status)
			{
				Exception result;
				using (WindowsIdentity current = WindowsIdentity.GetCurrent())
				{
					if (status == 5U)
					{
						result = new DagTaskComputerAccountCouldNotBeValidatedAccessDeniedException(objectName, current.Name);
					}
					else
					{
						result = new DagTaskComputerAccountCouldNotBeValidatedException(objectName, current.Name, new Win32Exception((int)status).Message);
					}
				}
				return result;
			});
			array[2] = new HaTaskCallbackHelper.ClusterCallbackLookupTableEntry(ClusapiMethods.CLUSTER_SETUP_PHASE.ClusterSetupPhaseValidateNetft, ClusapiMethods.CLUSTER_SETUP_PHASE_TYPE.ClusterSetupPhaseEnd, ClusapiMethods.CLUSTER_SETUP_PHASE_SEVERITY.ClusterSetupPhaseFatal, (string objectName, uint status) => new DagTaskNetFtProblemException((int)status));
			array[3] = new HaTaskCallbackHelper.ClusterCallbackLookupTableEntry(ClusapiMethods.CLUSTER_SETUP_PHASE.ClusterSetupPhaseValidateNodeState, ClusapiMethods.CLUSTER_SETUP_PHASE_TYPE.ClusterSetupPhaseEnd, ClusapiMethods.CLUSTER_SETUP_PHASE_SEVERITY.ClusterSetupPhaseFatal, delegate(string objectName, uint status)
			{
				if (status == 2147746132U)
				{
					return new DagTaskClusteringMustBeInstalledException(objectName);
				}
				if (status == 2147947451U)
				{
					return new DagTaskValidateNodeTimedOutException(objectName);
				}
				return null;
			});
			HaTaskCallbackHelper.m_lookupTable = array;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000287DC File Offset: 0x000269DC
		internal static Exception LookUpStatus(ClusapiMethods.CLUSTER_SETUP_PHASE eSetupPhase, ClusapiMethods.CLUSTER_SETUP_PHASE_TYPE ePhaseType, ClusapiMethods.CLUSTER_SETUP_PHASE_SEVERITY ePhaseSeverity, uint dwPercentComplete, string objectName, uint status)
		{
			foreach (HaTaskCallbackHelper.ClusterCallbackLookupTableEntry clusterCallbackLookupTableEntry in HaTaskCallbackHelper.m_lookupTable)
			{
				if (eSetupPhase == clusterCallbackLookupTableEntry.m_ESetupPhase && ePhaseType == clusterCallbackLookupTableEntry.m_EPhaseType && ePhaseSeverity == clusterCallbackLookupTableEntry.m_EPhaseSeverity)
				{
					Exception ex = clusterCallbackLookupTableEntry.m_ExceptionFactory(objectName, status);
					if (ex != null)
					{
						return ex;
					}
				}
			}
			return null;
		}

		// Token: 0x0400039A RID: 922
		private static HaTaskCallbackHelper.ClusterCallbackLookupTableEntry[] m_lookupTable;

		// Token: 0x020000D0 RID: 208
		internal class ClusterCallbackLookupTableEntry
		{
			// Token: 0x06000867 RID: 2151 RVA: 0x0002883B File Offset: 0x00026A3B
			internal ClusterCallbackLookupTableEntry(ClusapiMethods.CLUSTER_SETUP_PHASE eSetupPhase, ClusapiMethods.CLUSTER_SETUP_PHASE_TYPE ePhaseType, ClusapiMethods.CLUSTER_SETUP_PHASE_SEVERITY ePhaseSeverity, HaTaskCallbackHelper.ClusterCallbackLookupTableEntry.ExceptionFactory exceptionFactory)
			{
				this.m_ESetupPhase = eSetupPhase;
				this.m_EPhaseType = ePhaseType;
				this.m_EPhaseSeverity = ePhaseSeverity;
				this.m_ExceptionFactory = exceptionFactory;
			}

			// Token: 0x0400039F RID: 927
			internal ClusapiMethods.CLUSTER_SETUP_PHASE m_ESetupPhase;

			// Token: 0x040003A0 RID: 928
			internal ClusapiMethods.CLUSTER_SETUP_PHASE_TYPE m_EPhaseType;

			// Token: 0x040003A1 RID: 929
			internal ClusapiMethods.CLUSTER_SETUP_PHASE_SEVERITY m_EPhaseSeverity;

			// Token: 0x040003A2 RID: 930
			internal HaTaskCallbackHelper.ClusterCallbackLookupTableEntry.ExceptionFactory m_ExceptionFactory;

			// Token: 0x020000D1 RID: 209
			// (Invoke) Token: 0x06000869 RID: 2153
			internal delegate Exception ExceptionFactory(string objectName, uint status);
		}
	}
}
