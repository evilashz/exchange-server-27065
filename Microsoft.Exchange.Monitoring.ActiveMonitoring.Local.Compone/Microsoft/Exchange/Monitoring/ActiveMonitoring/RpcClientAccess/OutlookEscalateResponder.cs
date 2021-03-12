using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.RpcClientAccess
{
	// Token: 0x02000208 RID: 520
	internal class OutlookEscalateResponder<TInterpretedResult> : EscalateResponder where TInterpretedResult : InterpretedResult, new()
	{
		// Token: 0x06000EB9 RID: 3769 RVA: 0x000618A2 File Offset: 0x0005FAA2
		public static ResponderDefinition Configure(ResponderDefinition definition)
		{
			definition.SetType(typeof(OutlookEscalateResponder<TInterpretedResult>));
			return definition;
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0006194C File Offset: 0x0005FB4C
		internal override void BeforeContentGeneration(ResponseMessageReader propertyReader)
		{
			base.BeforeContentGeneration(propertyReader);
			propertyReader.AddObjectResolver<TInterpretedResult>("MapiProbe", delegate
			{
				TInterpretedResult result = Activator.CreateInstance<TInterpretedResult>();
				result.RawResult = (ProbeResult)propertyReader.EnsureNotNull((ResponseMessageReader pr) => pr.GetObject("Probe"));
				return result;
			});
		}
	}
}
