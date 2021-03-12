using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.Pickup
{
	// Token: 0x02000065 RID: 101
	internal interface IPickupSubmitHandler
	{
		// Token: 0x0600032C RID: 812
		void OnSubmit(TransportMailItem item, MailDirectionality direction, PickupType pickupType);
	}
}
