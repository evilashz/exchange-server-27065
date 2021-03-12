using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Data.PushNotifications;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003FE RID: 1022
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class BaseSubscribeToPushNotificationRequest : BaseRequest
	{
		// Token: 0x06001CFA RID: 7418 RVA: 0x0009ED8F File Offset: 0x0009CF8F
		public BaseSubscribeToPushNotificationRequest()
		{
		}

		// Token: 0x06001CFB RID: 7419 RVA: 0x0009ED97 File Offset: 0x0009CF97
		public BaseSubscribeToPushNotificationRequest(PushNotificationSubscription subscription)
		{
			this.SubscriptionRequest = subscription;
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x0009EDA6 File Offset: 0x0009CFA6
		// (set) Token: 0x06001CFD RID: 7421 RVA: 0x0009EDAE File Offset: 0x0009CFAE
		[DataMember(Name = "SubscriptionRequest", IsRequired = true)]
		public PushNotificationSubscription SubscriptionRequest { get; set; }

		// Token: 0x06001CFE RID: 7422 RVA: 0x0009EDB7 File Offset: 0x0009CFB7
		internal override BaseServerIdInfo GetProxyInfo(CallContext callContext)
		{
			return null;
		}

		// Token: 0x06001CFF RID: 7423 RVA: 0x0009EDBA File Offset: 0x0009CFBA
		internal override ResourceKey[] GetResources(CallContext callContext, int taskStep)
		{
			return base.GetResourceKeysFromProxyInfo(false, callContext);
		}
	}
}
