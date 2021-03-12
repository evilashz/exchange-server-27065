using System;
using System.IdentityModel.Policy;
using Microsoft.Exchange.Net.Claim;

namespace Microsoft.Exchange.Net.XropService
{
	// Token: 0x02000B8F RID: 2959
	internal sealed class EvaluationContextTracer
	{
		// Token: 0x06003F60 RID: 16224 RVA: 0x000A7FD3 File Offset: 0x000A61D3
		public EvaluationContextTracer(EvaluationContext evaluationContext)
		{
			this.evaluationContext = evaluationContext;
		}

		// Token: 0x06003F61 RID: 16225 RVA: 0x000A7FE2 File Offset: 0x000A61E2
		public override string ToString()
		{
			return this.evaluationContext.GetTraceString();
		}

		// Token: 0x0400373B RID: 14139
		private EvaluationContext evaluationContext;
	}
}
