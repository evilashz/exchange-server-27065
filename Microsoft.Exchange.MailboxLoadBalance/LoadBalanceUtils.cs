using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Description;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Config;

namespace Microsoft.Exchange.MailboxLoadBalance
{
	// Token: 0x02000009 RID: 9
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class LoadBalanceUtils
	{
		// Token: 0x06000036 RID: 54 RVA: 0x00002E28 File Offset: 0x00001028
		public static IList<Guid> GetNonMovableOrgsList(ILoadBalanceSettings loadBalanceSettings)
		{
			IList<Guid> list = new List<Guid>();
			foreach (string input in loadBalanceSettings.NonMovableOrganizationIds.Split(new string[]
			{
				";"
			}, StringSplitOptions.RemoveEmptyEntries))
			{
				Guid item;
				if (Guid.TryParse(input, out item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002E84 File Offset: 0x00001084
		public static object GetSyncRoot<TElement>(this ICollection<TElement> collection)
		{
			return ((ICollection)collection).SyncRoot;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002E94 File Offset: 0x00001094
		public static void SetDataContractSerializerBehavior(ContractDescription contract)
		{
			foreach (OperationDescription operationDescription in contract.Operations)
			{
				if (operationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>() != null)
				{
					operationDescription.Behaviors.Remove<DataContractSerializerOperationBehavior>();
				}
				LoadBalanceDataContractSerializationBehavior item = new LoadBalanceDataContractSerializationBehavior(operationDescription);
				operationDescription.Behaviors.Add(item);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002F08 File Offset: 0x00001108
		internal static void UpdateAndLogServiceEndpoint(ILogger logger, ServiceEndpoint endpoint)
		{
			LoadBalanceUtils.SetDataContractSerializerBehavior(endpoint.Contract);
			logger.LogVerbose("Connected endpoint {0} with binding {1} and contract {2} with session mode {3}", new object[]
			{
				endpoint.Address,
				endpoint.Binding.Name,
				endpoint.Contract.Name,
				endpoint.Contract.SessionMode
			});
			foreach (OperationDescription operationDescription in endpoint.Contract.Operations)
			{
				logger.LogVerbose("Operation:: {0}, {1}, {2}", new object[]
				{
					operationDescription.Name,
					operationDescription.ProtectionLevel,
					operationDescription.Behaviors.Find<DataContractSerializerOperationBehavior>()
				});
				foreach (MessageDescription messageDescription in operationDescription.Messages)
				{
					logger.LogVerbose("Operation[{0}]::Message({1}, {2})", new object[]
					{
						operationDescription.Name,
						messageDescription.Action,
						messageDescription.MessageType
					});
				}
			}
		}
	}
}
