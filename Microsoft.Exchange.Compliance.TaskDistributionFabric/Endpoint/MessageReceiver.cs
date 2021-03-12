using System;
using System.ServiceModel;
using System.Threading.Tasks;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Contract;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Diagnostics;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;
using Microsoft.Exchange.Compliance.TaskDistributionFabric.Dataflow;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Endpoint
{
	// Token: 0x02000014 RID: 20
	[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
	internal class MessageReceiver : IMessageReceiver
	{
		// Token: 0x06000055 RID: 85 RVA: 0x0000384C File Offset: 0x00001A4C
		public async Task<byte[][]> ReceiveMessagesAsync(byte[][] messageBlobs)
		{
			return await Task.Run<byte[][]>(delegate()
			{
				StatusPayload statusPayload = new StatusPayload();
				foreach (byte[] blob in messageBlobs)
				{
					ComplianceMessage complianceMessage;
					FaultDefinition fault;
					if (ComplianceSerializer.TryDeserialize<ComplianceMessage>(ComplianceMessage.Description, blob, out complianceMessage, out fault, "ReceiveMessagesAsync", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Endpoint\\MessageReceiver.cs", 56))
					{
						MessageReceiverBase messageReceiverBase;
						if (Registry.Instance.TryGetInstance<MessageReceiverBase>(RegistryComponent.MessageReceiver, complianceMessage.ComplianceMessageType, out messageReceiverBase, out fault, "ReceiveMessagesAsync", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Endpoint\\MessageReceiver.cs", 58))
						{
							if (messageReceiverBase.ReceiveMessage(complianceMessage))
							{
								statusPayload.QueuedMessages.Add(complianceMessage.MessageId);
							}
						}
						else if (complianceMessage.ComplianceMessageType == ComplianceMessageType.EchoRequest)
						{
							statusPayload.QueuedMessages.Add(complianceMessage.MessageId);
						}
						else
						{
							ExceptionHandler.FaultMessage(complianceMessage, fault, true);
						}
					}
				}
				return new byte[][]
				{
					ComplianceSerializer.Serialize<ComplianceMessage>(ComplianceMessage.Description, new ComplianceMessage
					{
						ComplianceMessageType = ComplianceMessageType.Status,
						Payload = ComplianceSerializer.Serialize<StatusPayload>(StatusPayload.Description, statusPayload)
					})
				};
			});
		}
	}
}
