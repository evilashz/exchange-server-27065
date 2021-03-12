using System;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Diagnostics;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionFabric.Routing;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Blocks
{
	// Token: 0x02000008 RID: 8
	internal class ReturnBlock : FinalBlockBase<ComplianceMessage>
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000029C8 File Offset: 0x00000BC8
		protected override void ProcessFinal(ComplianceMessage input)
		{
			IRoutingManager routingManager;
			FaultDefinition faultDefinition;
			if (Registry.Instance.TryGetInstance<IRoutingManager>(RegistryComponent.TaskDistribution, TaskDistributionComponent.RoutingManager, out routingManager, out faultDefinition, "ProcessFinal", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Blocks\\ReturnBlock.cs", 32))
			{
				routingManager.ReturnMessage(input);
			}
			if (faultDefinition != null)
			{
				ExceptionHandler.FaultMessage(input, faultDefinition, true);
			}
		}
	}
}
