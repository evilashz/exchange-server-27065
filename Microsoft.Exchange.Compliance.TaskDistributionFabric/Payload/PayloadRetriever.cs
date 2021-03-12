using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Client;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Diagnostics;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Extensibility;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Serialization;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Payload
{
	// Token: 0x02000017 RID: 23
	internal class PayloadRetriever : IPayloadRetriever
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00003AFC File Offset: 0x00001CFC
		public bool TryGetPayload<T>(ComplianceSerializationDescription<T> description, byte[] blob, out T payload, out FaultDefinition faultDefinition) where T : Payload, new()
		{
			if (ComplianceSerializer.TryDeserialize<T>(description, blob, out payload, out faultDefinition, "TryGetPayload", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Payload\\PayloadRetriever.cs", 39))
			{
				return true;
			}
			PayloadReference reference;
			if (ComplianceSerializer.TryDeserialize<PayloadReference>(PayloadReference.Description, blob, out reference, out faultDefinition, "TryGetPayload", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Payload\\PayloadRetriever.cs", 48))
			{
				PayloadRetriever.DriverRetriever retriever = new PayloadRetriever.DriverRetriever();
				T currentPayload = default(T);
				if (ExceptionHandler.Proxy.TryRun(delegate
				{
					currentPayload = retriever.RetrievePayload<T>(description, reference, int.MaxValue).FirstOrDefault<T>();
				}, TaskDistributionSettings.DataLookupTime, out faultDefinition, null, null, default(CancellationToken), null, "TryGetPayload", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Payload\\PayloadRetriever.cs", 53))
				{
					payload = currentPayload;
					return true;
				}
				faultDefinition = retriever.FaultDefinition;
			}
			return false;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003C24 File Offset: 0x00001E24
		public IEnumerable<T> GetAllPayloads<T>(ComplianceSerializationDescription<T> description, byte[] blob, out FaultDefinition faultDefinition) where T : Payload, new()
		{
			T t;
			if (ComplianceSerializer.TryDeserialize<T>(description, blob, out t, out faultDefinition, "GetAllPayloads", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Payload\\PayloadRetriever.cs", 88))
			{
				return new T[]
				{
					t
				};
			}
			PayloadReference reference;
			if (ComplianceSerializer.TryDeserialize<PayloadReference>(PayloadReference.Description, blob, out reference, out faultDefinition, "GetAllPayloads", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Payload\\PayloadRetriever.cs", 95))
			{
				PayloadRetriever.DriverRetriever retriever = new PayloadRetriever.DriverRetriever();
				IEnumerable<T> payloads = null;
				if (ExceptionHandler.Proxy.TryRun(delegate
				{
					payloads = retriever.RetrievePayload<T>(description, reference, int.MaxValue);
				}, TaskDistributionSettings.DataLookupTime, out faultDefinition, null, null, default(CancellationToken), null, "GetAllPayloads", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Payload\\PayloadRetriever.cs", 99))
				{
					return payloads;
				}
				faultDefinition = retriever.FaultDefinition;
			}
			return null;
		}

		// Token: 0x02000018 RID: 24
		private class DriverRetriever
		{
			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600005E RID: 94 RVA: 0x00003D0F File Offset: 0x00001F0F
			public FaultDefinition FaultDefinition
			{
				get
				{
					return this.faultDefinition;
				}
			}

			// Token: 0x0600005F RID: 95 RVA: 0x00004088 File Offset: 0x00002288
			public IEnumerable<T> RetrievePayload<T>(ComplianceSerializationDescription<T> description, PayloadReference reference, int count = 2147483647) where T : Payload, new()
			{
				ComplianceMessage message = new ComplianceMessage
				{
					CorrelationId = Guid.NewGuid(),
					ComplianceMessageType = ComplianceMessageType.RetrieveRequest,
					WorkDefinitionType = WorkDefinitionType.Unrecognized
				};
				bool isComplete = false;
				string bookmark = null;
				DriverClientBase driverClient;
				if (Registry.Instance.TryGetInstance<DriverClientBase>(RegistryComponent.Client, ClientType.DriverClient, out driverClient, out this.faultDefinition, "RetrievePayload", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Payload\\PayloadRetriever.cs", 161))
				{
					for (;;)
					{
						reference.Bookmark = bookmark;
						reference.Count = count;
						message.Payload = ComplianceSerializer.Serialize<PayloadReference>(PayloadReference.Description, reference);
						Task<ComplianceMessage> payloadTask = driverClient.GetResponseAsync(message);
						if (!payloadTask.Wait(TaskDistributionSettings.DataLookupTime))
						{
							break;
						}
						ComplianceMessage payloadResponse = payloadTask.Result;
						RetrievedPayload retrievedPayload;
						if (ComplianceSerializer.TryDeserialize<RetrievedPayload>(RetrievedPayload.Description, payloadResponse.Payload, out retrievedPayload, out this.faultDefinition, "RetrievePayload", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Payload\\PayloadRetriever.cs", 174))
						{
							bookmark = retrievedPayload.Bookmark;
							isComplete = retrievedPayload.IsComplete;
							foreach (byte[] childBlob in retrievedPayload.Children)
							{
								T payload;
								if (ComplianceSerializer.TryDeserialize<T>(description, childBlob, out payload, out this.faultDefinition, "RetrievePayload", "f:\\15.00.1497\\sources\\dev\\EDiscovery\\src\\TaskDistributionSystem\\TaskDistributionFabric\\Payload\\PayloadRetriever.cs", 181))
								{
									yield return payload;
								}
							}
						}
						if (count <= 1)
						{
							goto IL_236;
						}
						if (isComplete)
						{
							goto Block_5;
						}
					}
					throw new TimeoutException();
					Block_5:
					IL_236:
					yield break;
				}
				throw new InvalidOperationException();
			}

			// Token: 0x04000027 RID: 39
			private FaultDefinition faultDefinition;
		}
	}
}
