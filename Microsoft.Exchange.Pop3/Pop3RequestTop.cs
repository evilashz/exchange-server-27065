using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000015 RID: 21
	internal sealed class Pop3RequestTop : Pop3RequestRetr
	{
		// Token: 0x06000081 RID: 129 RVA: 0x00003D3B File Offset: 0x00001F3B
		public Pop3RequestTop(Pop3ResponseFactory factory, string input) : base(factory, input, Pop3RequestWithIntParameters.ArgumentsTypes.two_mandatory)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_TOP_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_TOP_Failures;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003D68 File Offset: 0x00001F68
		public override ProtocolResponse ProcessMessage(Pop3Message message)
		{
			return base.ProcessMessage(message, base.Lines);
		}
	}
}
