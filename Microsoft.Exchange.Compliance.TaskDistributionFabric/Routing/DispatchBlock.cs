using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Client;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Diagnostics;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Resolver;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Utility;
using Microsoft.Exchange.Compliance.TaskDistributionFabric.Blocks;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Routing
{
	// Token: 0x0200001B RID: 27
	internal class DispatchBlock : FinalBlockBase<IEnumerable<ComplianceMessage>>
	{
		// Token: 0x0600006E RID: 110 RVA: 0x000041B0 File Offset: 0x000023B0
		protected override void ProcessFinal(IEnumerable<ComplianceMessage> input)
		{
			ITargetResolver resolver = null;
			IRoutingManager routingManager = null;
			IEnumerable<ComplianceMessage> messages = input;
			IMessageSender sender;
			FaultDefinition faultDefinition;
			if (Registry.Instance.TryGetInstance<IRoutingManager>(RegistryComponent.TaskDistribution, TaskDistributionComponent.RoutingManager, out routingManager, out faultDefinition, "ProcessFinal", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\DispatchBlock.cs", 42) && Registry.Instance.TryGetInstance<ITargetResolver>(RegistryComponent.TaskDistribution, TaskDistributionComponent.TargetResolver, out resolver, out faultDefinition, "ProcessFinal", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\DispatchBlock.cs", 44) && Registry.Instance.TryGetInstance<IMessageSender>(RegistryComponent.Client, MessageHelper.GetClientType(input), out sender, out faultDefinition, "ProcessFinal", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\DispatchBlock.cs", 46))
			{
				ExceptionHandler.Proxy.TryRun(delegate
				{
					sender.SendMessageAsync(this.SetDispatchingStatus(resolver.Resolve(messages))).ContinueWith(delegate(Task<bool[]> task)
					{
						this.SetDispatchedStatus(messages, task.Result, routingManager);
					});
				}, TaskDistributionSettings.RemoteExecutionTime, out faultDefinition, null, null, default(CancellationToken), null, "ProcessFinal", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Routing\\Cache\\DispatchBlock.cs", 48);
			}
			if (faultDefinition != null)
			{
				foreach (ComplianceMessage message in messages)
				{
					ExceptionHandler.FaultMessage(message, faultDefinition, true);
				}
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004484 File Offset: 0x00002684
		private IEnumerable<ComplianceMessage> SetDispatchingStatus(IEnumerable<ComplianceMessage> messages)
		{
			foreach (ComplianceMessage message in messages)
			{
				MessageLogger.Instance.LogMessageDispatching(message);
				yield return message;
			}
			yield break;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000044B4 File Offset: 0x000026B4
		private void SetDispatchedStatus(IEnumerable<ComplianceMessage> messages, bool[] messageStatuses, IRoutingManager routingManager)
		{
			foreach (Tuple<ComplianceMessage, bool> tuple in messages.Zip(messageStatuses, (ComplianceMessage message, bool status) => new Tuple<ComplianceMessage, bool>(message, status)))
			{
				if (tuple.Item2)
				{
					routingManager.DispatchedMessage(tuple.Item1);
				}
			}
		}
	}
}
