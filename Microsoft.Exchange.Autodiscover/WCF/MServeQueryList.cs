using System;
using Microsoft.Exchange.Diagnostics.Components.Autodiscover;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000082 RID: 130
	internal sealed class MServeQueryList : QueryListBase<MServeQueryResult>
	{
		// Token: 0x0600036B RID: 875 RVA: 0x00015A35 File Offset: 0x00013C35
		protected override MServeQueryResult CreateResult(UserResultMapping userResultMapping)
		{
			return new MServeQueryResult(userResultMapping);
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00015A40 File Offset: 0x00013C40
		public override void Execute()
		{
			ExTraceGlobals.FrameworkTracer.TraceDebug<MServeQueryList, int>((long)this.GetHashCode(), "{0} Execute() called for {1} addresses.", this, this.resultDictionary.Values.Count);
			foreach (MServeQueryResult mserveQueryResult in this.resultDictionary.Values)
			{
				mserveQueryResult.RedirectServer = MServe.GetRedirectServer(mserveQueryResult.UserResultMapping.SmtpAddress.ToString());
			}
		}
	}
}
