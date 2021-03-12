using System;
using System.Collections;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.FfoReporting.Providers
{
	// Token: 0x02000407 RID: 1031
	public interface ISmtpCheckerProvider
	{
		// Token: 0x0600242C RID: 9260
		IEnumerable GetMxRecords(Fqdn domain, IConfigDataProvider configDataProvider);

		// Token: 0x0600242D RID: 9261
		IEnumerable GetOutboundConnectors(Fqdn domain, IConfigDataProvider configDataProvider);

		// Token: 0x0600242E RID: 9262
		IEnumerable GetServiceDeliveries(SmtpAddress recipient, IConfigDataProvider configDataProvider);
	}
}
