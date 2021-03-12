using System;
using System.Diagnostics;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Exchange.Directory.TopologyService.Diagnostics
{
	// Token: 0x02000030 RID: 48
	public class AsyncPerfForestInspector : Attribute, IParameterInspector, IOperationBehavior
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		public object BeforeCall(string operationName, object[] inputs)
		{
			if (inputs == null || inputs.Length < 1)
			{
				return null;
			}
			string text = inputs[0] as string;
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			return new Tuple<string, long>(text, Stopwatch.GetTimestamp());
		}

		// Token: 0x06000212 RID: 530 RVA: 0x0000E210 File Offset: 0x0000C410
		public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
		{
			if (correlationState == null)
			{
				return;
			}
			Tuple<string, long> tuple = correlationState as Tuple<string, long>;
			if (tuple == null)
			{
				return;
			}
			ForestDiscoveryPerfCountersInstance instance = ForestDiscoveryPerfCounters.GetInstance(tuple.Item1);
			long num = Stopwatch.GetTimestamp() - tuple.Item2;
			instance.WebServiceGetServersAverageCallDuration.IncrementBy(num);
			instance.WebServiceGetServersAverageCallDurationBase.Increment();
			instance.WebServiceGetServersMaxCallDuration.RawValue = Math.Max(instance.WebServiceGetServersMaxCallDuration.RawValue, (long)TimeSpan.FromTicks(num).TotalSeconds);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000E28A File Offset: 0x0000C48A
		public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
		{
		}

		// Token: 0x06000214 RID: 532 RVA: 0x0000E28C File Offset: 0x0000C48C
		public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
		{
		}

		// Token: 0x06000215 RID: 533 RVA: 0x0000E290 File Offset: 0x0000C490
		public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
		{
			AsyncPerfForestInspector item = new AsyncPerfForestInspector();
			dispatchOperation.ParameterInspectors.Add(item);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x0000E2AF File Offset: 0x0000C4AF
		public void Validate(OperationDescription operationDescription)
		{
		}
	}
}
