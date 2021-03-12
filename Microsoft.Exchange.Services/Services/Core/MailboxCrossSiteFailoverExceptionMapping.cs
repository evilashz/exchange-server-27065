using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000220 RID: 544
	internal class MailboxCrossSiteFailoverExceptionMapping : MailboxFailoverExceptionMapping
	{
		// Token: 0x06000E1A RID: 3610 RVA: 0x000452EE File Offset: 0x000434EE
		public MailboxCrossSiteFailoverExceptionMapping() : base(typeof(MailboxCrossSiteFailoverException))
		{
		}

		// Token: 0x06000E1B RID: 3611 RVA: 0x00045300 File Offset: 0x00043500
		protected override void DoServiceErrorPostProcessing(LocalizedException exception, ServiceError error)
		{
			MailboxCrossSiteFailoverException ex = base.VerifyExceptionType<MailboxCrossSiteFailoverException>(exception);
			ExTraceGlobals.SessionCacheTracer.TraceDebug<string>(0L, "[MailboxCrossSiteFailoverExceptionMapping::DoServiceErrorPostProcessing] Encountered failover for mailbox database: '{0}'", ex.DatabaseLocationInfo.DatabaseLegacyDN);
			if (CallContext.Current != null)
			{
				CallContext.Current.SessionCache.ReturnStoreSessionsToCache = false;
			}
			base.DoServiceErrorPostProcessing(exception, error);
		}
	}
}
