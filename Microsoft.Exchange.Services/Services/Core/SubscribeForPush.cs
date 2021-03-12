using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x0200037E RID: 894
	internal sealed class SubscribeForPush : SubscribeCommandBase
	{
		// Token: 0x060018F7 RID: 6391 RVA: 0x0008A3D2 File Offset: 0x000885D2
		public SubscribeForPush(CallContext callContext, SubscribeRequest request) : base(callContext, request)
		{
			this.pushSubscriptionRequest = (base.Request.SubscriptionRequest as PushSubscriptionRequest);
		}

		// Token: 0x060018F8 RID: 6392 RVA: 0x0008A3F2 File Offset: 0x000885F2
		protected override void ValidateOperation()
		{
			base.ValidateOperation();
			SubscribeForPush.ValidateSubscriptionUrl(this.pushSubscriptionRequest.Url);
		}

		// Token: 0x060018F9 RID: 6393 RVA: 0x0008A40C File Offset: 0x0008860C
		private static void ValidateSubscriptionUrl(string url)
		{
			if (url.Length == 0 || url.Length > 2083)
			{
				throw new InvalidPushSubscriptionUrlException();
			}
			try
			{
				Uri uri = new Uri(url);
				string components = uri.GetComponents(UriComponents.Scheme, UriFormat.SafeUnescaped);
				if (!components.Equals("http", StringComparison.OrdinalIgnoreCase) && !components.Equals("https", StringComparison.OrdinalIgnoreCase))
				{
					throw new InvalidPushSubscriptionUrlException();
				}
			}
			catch (UriFormatException innerException)
			{
				throw new InvalidPushSubscriptionUrlException(innerException);
			}
		}

		// Token: 0x060018FA RID: 6394 RVA: 0x0008A480 File Offset: 0x00088680
		protected override SubscriptionBase CreateSubscriptionInstance(IdAndSession[] folderIds)
		{
			return new PushSubscription(this.pushSubscriptionRequest, folderIds, base.CallContext.EffectiveCaller.ObjectGuid, Subscriptions.Singleton);
		}

		// Token: 0x040010B1 RID: 4273
		private const int MaxUrlLength = 2083;

		// Token: 0x040010B2 RID: 4274
		private PushSubscriptionRequest pushSubscriptionRequest;
	}
}
