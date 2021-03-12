using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport.LoggingCommon;

namespace Microsoft.Exchange.MailboxTransport.Delivery
{
	// Token: 0x02000041 RID: 65
	internal interface IDeliveryThrottlingLog
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060002D3 RID: 723
		// (remove) Token: 0x060002D4 RID: 724
		event Action<string> TrackSummary;

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060002D5 RID: 725
		bool Enabled { get; }

		// Token: 0x060002D6 RID: 726
		void LogSummary(string sequenceNumber, ThrottlingScope scope, ThrottlingResource resource, double resourceThreshold, ThrottlingImpactUnits impactUnits, uint impact, double impactRate, Guid externalOrganizationId, string recipient, string mdbName, IList<KeyValuePair<string, double>> mdbHealth, IList<KeyValuePair<string, string>> customData);
	}
}
