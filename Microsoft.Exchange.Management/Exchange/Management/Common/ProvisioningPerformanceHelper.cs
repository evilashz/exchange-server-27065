using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.LatencyDetection;
using Microsoft.Exchange.Live.DomainServices;
using Microsoft.Exchange.Management.LiveServices;
using Microsoft.Mapi.Unmanaged;

namespace Microsoft.Exchange.Management.Common
{
	// Token: 0x02000129 RID: 297
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class ProvisioningPerformanceHelper
	{
		// Token: 0x06000AB0 RID: 2736 RVA: 0x00031B38 File Offset: 0x0002FD38
		internal static LatencyDetectionContext StartLatencyDetection(Task task)
		{
			IPerformanceDataProvider[] providers = new IPerformanceDataProvider[]
			{
				PerformanceContext.Current,
				RpcDataProvider.Instance,
				TaskPerformanceData.CmdletInvoked,
				TaskPerformanceData.BeginProcessingInvoked,
				TaskPerformanceData.ProcessRecordInvoked,
				TaskPerformanceData.EndProcessingInvoked,
				DomainServicesPerformanceData.DomainServicesConnection,
				DomainServicesPerformanceData.DomainServicesCall,
				LiveServicesPerformanceData.SPFConnection,
				LiveServicesPerformanceData.SPFCall,
				LiveServicesPerformanceData.CredentialServicesCall,
				LiveServicesPerformanceData.ProfileServicesCall,
				LiveServicesPerformanceData.NamespaceServicesCall
			};
			return ProvisioningPerformanceHelper.latencyDetectionContextFactory.CreateContext(ProvisioningPerformanceHelper.applicationVersion, task.CurrentTaskContext.InvocationInfo.CommandName, providers);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00031BDC File Offset: 0x0002FDDC
		internal static TaskPerformanceData[] StopLatencyDetection(LatencyDetectionContext latencyDetectionContext)
		{
			TaskPerformanceData[] result = null;
			if (latencyDetectionContext != null)
			{
				result = latencyDetectionContext.StopAndFinalizeCollection();
			}
			return result;
		}

		// Token: 0x04000571 RID: 1393
		private static readonly string applicationVersion = typeof(ProvisioningPerformanceHelper).GetApplicationVersion();

		// Token: 0x04000572 RID: 1394
		private static readonly LatencyDetectionContextFactory latencyDetectionContextFactory = LatencyDetectionContextFactory.CreateFactory("Provisioning.Cmdlets", LatencyReportingThreshold.MinimumThresholdValue, TimeSpan.FromSeconds(20.0));
	}
}
