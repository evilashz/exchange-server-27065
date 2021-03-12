using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020002E0 RID: 736
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class AbstractPushNotificationSubscriptionItem : AbstractItem, IPushNotificationSubscriptionItem, IItem, IStoreObject, IStorePropertyBag, IPropertyBag, IReadOnlyPropertyBag, IDisposable
	{
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06001F74 RID: 8052 RVA: 0x00086599 File Offset: 0x00084799
		// (set) Token: 0x06001F75 RID: 8053 RVA: 0x000865A0 File Offset: 0x000847A0
		public virtual string SubscriptionId
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06001F76 RID: 8054 RVA: 0x000865A7 File Offset: 0x000847A7
		// (set) Token: 0x06001F77 RID: 8055 RVA: 0x000865AE File Offset: 0x000847AE
		public virtual ExDateTime LastUpdateTimeUTC
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06001F78 RID: 8056 RVA: 0x000865B5 File Offset: 0x000847B5
		// (set) Token: 0x06001F79 RID: 8057 RVA: 0x000865BC File Offset: 0x000847BC
		public virtual string SerializedNotificationSubscription
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
