using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Pim;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x0200044F RID: 1103
	[DataContract]
	public class SubscriptionItem : EnumValue
	{
		// Token: 0x0600363F RID: 13887 RVA: 0x000A7BC4 File Offset: 0x000A5DC4
		public SubscriptionItem(PimSubscriptionProxy subscription) : base(subscription.EmailAddress.ToString(), subscription.Subscription.SubscriptionIdentity.ToString())
		{
			this.IsValid = subscription.IsValid;
		}

		// Token: 0x17002137 RID: 8503
		// (get) Token: 0x06003640 RID: 13888 RVA: 0x000A7C07 File Offset: 0x000A5E07
		// (set) Token: 0x06003641 RID: 13889 RVA: 0x000A7C0F File Offset: 0x000A5E0F
		internal bool IsValid { get; set; }
	}
}
