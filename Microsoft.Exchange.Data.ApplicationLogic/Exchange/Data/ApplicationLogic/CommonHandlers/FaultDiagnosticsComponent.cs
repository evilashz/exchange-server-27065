using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.ApplicationLogic.CommonHandlers
{
	// Token: 0x020000C0 RID: 192
	public class FaultDiagnosticsComponent : ExchangeDiagnosableWrapper<FaultDiagnosticsInfo>
	{
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x00021A2E File Offset: 0x0001FC2E
		protected override string UsageText
		{
			get
			{
				return "This handler is generic exception handler for all diagnostics compoenents. This handler is not registered explicitly.";
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00021A35 File Offset: 0x0001FC35
		protected override string ComponentName
		{
			get
			{
				return this.componentName;
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x00021A3D File Offset: 0x0001FC3D
		internal override FaultDiagnosticsInfo GetExchangeDiagnosticsInfoData(DiagnosableParameters arguments)
		{
			return this.outputMessage;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x00021A45 File Offset: 0x0001FC45
		internal void SetComponentNameAndMessage(string componentName, int errorCode, string errorMessage)
		{
			this.componentName = componentName;
			this.outputMessage = new FaultDiagnosticsInfo(errorCode, errorMessage);
		}

		// Token: 0x040003AA RID: 938
		private string componentName;

		// Token: 0x040003AB RID: 939
		private FaultDiagnosticsInfo outputMessage;
	}
}
