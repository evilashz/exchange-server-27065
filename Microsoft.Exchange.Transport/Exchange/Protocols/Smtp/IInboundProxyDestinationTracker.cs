using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x02000420 RID: 1056
	internal interface IInboundProxyDestinationTracker
	{
		// Token: 0x060030DF RID: 12511
		void IncrementProxyCount(string destination);

		// Token: 0x060030E0 RID: 12512
		void DecrementProxyCount(string destination);

		// Token: 0x060030E1 RID: 12513
		bool ShouldRejectMessage(string destination, out SmtpResponse rejectResponse);

		// Token: 0x060030E2 RID: 12514
		bool TryGetDiagnosticInfo(DiagnosableParameters parameters, out XElement diagnosticInfo);

		// Token: 0x060030E3 RID: 12515
		void UpdateReceiveConnectors(IEnumerable<ReceiveConnector> receiveConnectors);
	}
}
