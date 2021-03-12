using System;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000012 RID: 18
	internal sealed class Pop3RequestRset : Pop3Request
	{
		// Token: 0x06000078 RID: 120 RVA: 0x00003AA6 File Offset: 0x00001CA6
		public Pop3RequestRset(Pop3ResponseFactory factory, string input) : base(factory, input)
		{
			this.PerfCounterTotal = base.Pop3CountersInstance.PerfCounter_RSET_Total;
			this.PerfCounterFailures = base.Pop3CountersInstance.PerfCounter_RSET_Failures;
			base.AllowedStates = Pop3State.Authenticated;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003ADC File Offset: 0x00001CDC
		public override ProtocolResponse Process()
		{
			for (int i = 0; i < base.Factory.Messages.Count; i++)
			{
				base.Factory.Messages[i].IsDeleted = false;
				base.Factory.Messages[i].IsRead = false;
			}
			return new Pop3Response(Pop3Response.Type.ok);
		}
	}
}
