using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x0200000B RID: 11
	internal sealed class Pop3RequestDele : Pop3RequestWithIntParameters
	{
		// Token: 0x0600004B RID: 75 RVA: 0x0000340E File Offset: 0x0000160E
		public Pop3RequestDele(Pop3ResponseFactory factory, string input) : base(factory, input, Pop3RequestWithIntParameters.ArgumentsTypes.one_mandatory)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_DELE_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_DELE_Failures;
			base.AllowedStates = Pop3State.Authenticated;
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003442 File Offset: 0x00001642
		public override ProtocolResponse ProcessMessage(Pop3Message message)
		{
			message.IsDeleted = true;
			return new Pop3Response(Pop3Response.Type.ok);
		}
	}
}
