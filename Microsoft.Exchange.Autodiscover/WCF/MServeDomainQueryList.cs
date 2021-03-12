using System;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000083 RID: 131
	internal sealed class MServeDomainQueryList : QueryListBase<MServeQueryResult>
	{
		// Token: 0x0600036E RID: 878 RVA: 0x00015AE4 File Offset: 0x00013CE4
		protected override MServeQueryResult CreateResult(UserResultMapping userResultMapping)
		{
			return new MServeQueryResult(userResultMapping);
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00015AEC File Offset: 0x00013CEC
		public override void Execute()
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<MServeDomainQueryList, int>((long)this.GetHashCode(), "{0} Execute() called for {1} addresses.", this, this.resultDictionary.Values.Count);
			foreach (MServeQueryResult mserveQueryResult in this.resultDictionary.Values)
			{
				mserveQueryResult.RedirectServer = MserveDomainCache.Singleton.Get(mserveQueryResult.UserResultMapping.SmtpAddress.Domain);
			}
		}
	}
}
